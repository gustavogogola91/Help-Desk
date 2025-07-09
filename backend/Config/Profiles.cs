using AutoMapper;
using backend.DTO;
using backend.Helpers;
using backend.Model;

namespace backend.Config
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Chamado, ChamadoDTO>()
                .ForMember(
                    dest => dest.NomeEquipamento,
                    opt => opt.MapFrom(src => src.Equipamento!.Nome)
                ).ForMember(
                    dest => dest.NomeSetorSolicitante,
                    opt => opt.MapFrom(src => src.SetorSolicitante!.Nome)
                ).ForMember(
                    dest => dest.NomeEstabelecimento,
                    opt => opt.MapFrom(src => src.Estabelecimento!.Nome)
                );
            CreateMap<ChamadoPostDTO, Chamado>();

            CreateMap<ChamadoAcompanhamento, ChamadoAcompanhamentoDTO>()
                .ForMember(
                    dest => dest.NomeUsuario,
                    opt => opt.MapFrom(src => src.Usuario!.Nome)
                );
            CreateMap<ChamadoAcompanhamentoPostDTO, ChamadoAcompanhamento>();

            CreateMap<ChamadoAtendimento, ChamadoAtendimentoDTO>()
                .ForMember(
                    dest => dest.NomeUsuarioAtendimento,
                    opt => opt.MapFrom(src => src.UsuarioAtendimento == null ? null : src.UsuarioAtendimento.Nome)
                ).ForMember(
                    dest => dest.NomeSetorAtual,
                    opt => opt.MapFrom(src => src.SetorAtual!.Nome)
                ).ForMember(
                    dest => dest.NomeSetorTransferencia,
                    opt => opt.MapFrom(src => src.SetorTransferencia == null ? null : src.SetorTransferencia.Nome)
                );

            CreateMap<Equipamento, EquipamentoDTO>()
                .ForMember(
                    dest => dest.NomeSetor,
                    opt => opt.MapFrom(src => src.Setor!.Nome)
                );
            CreateMap<EquipamentoPostDTO, Equipamento>();

            CreateMap<Estabelecimento, EstabelecimentoDTO>();
            CreateMap<EstabelecimentoPostDTO, Estabelecimento>();

            CreateMap<Setor, SetorDTO>()
                .ForMember(
                    dest => dest.NomeEstabelecimento,
                    opt => opt.MapFrom(src => src.Estabelecimento!.Nome)
                );
            CreateMap<SetorPostDTO, Setor>();

            CreateMap<SetorUsuario, SetorUsuarioDTO>();

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(
                    dest => dest.SetoresSuporte,
                    opt => opt.MapFrom(src => src.SetoresSuporte.Select(su => su.Setor!.Nome))
                );
            CreateMap<UsuarioPostDTO, Usuario>();
        }
    }
}