using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class _080701 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:status_atendimento", "aguardando,em_atendimento,concluido")
                .Annotation("Npgsql:Enum:tipo_usuario", "suporte,adm");

            migrationBuilder.CreateTable(
                name: "tb_estabelecimento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_estabelecimento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Username = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_setor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    EstabelecimentoId = table.Column<long>(type: "bigint", nullable: false),
                    Suporte = table.Column<bool>(type: "boolean", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_setor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_setor_tb_estabelecimento_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "tb_estabelecimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_equipamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    SetorId = table.Column<long>(type: "bigint", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_equipamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_equipamento_tb_setor_SetorId",
                        column: x => x.SetorId,
                        principalTable: "tb_setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_setor_usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    SetorId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_setor_usuario", x => new { x.UsuarioId, x.SetorId });
                    table.ForeignKey(
                        name: "FK_tb_setor_usuario_tb_setor_SetorId",
                        column: x => x.SetorId,
                        principalTable: "tb_setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_setor_usuario_tb_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_chamado",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Protocolo = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    NomeSolicitante = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFinalizado = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EquipamentoId = table.Column<long>(type: "bigint", nullable: false),
                    SetorSolicitanteId = table.Column<long>(type: "bigint", nullable: false),
                    SetorDestinoId = table.Column<long>(type: "bigint", nullable: false),
                    EstabelecimentoId = table.Column<long>(type: "bigint", nullable: false),
                    Ramal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    Computador = table.Column<string>(type: "text", nullable: false),
                    Ip = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Anexo = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_chamado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_chamado_tb_equipamento_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "tb_equipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_chamado_tb_estabelecimento_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "tb_estabelecimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_chamado_tb_setor_SetorDestinoId",
                        column: x => x.SetorDestinoId,
                        principalTable: "tb_setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_chamado_tb_setor_SetorSolicitanteId",
                        column: x => x.SetorSolicitanteId,
                        principalTable: "tb_setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_chamado_acompanhamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChamadoId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    Conteudo = table.Column<string>(type: "text", nullable: false),
                    DataMensagem = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_chamado_acompanhamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_chamado_acompanhamento_tb_chamado_ChamadoId",
                        column: x => x.ChamadoId,
                        principalTable: "tb_chamado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_chamado_acompanhamento_tb_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_chamado_atendimento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChamadoId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioAtendimentoId = table.Column<long>(type: "bigint", nullable: true),
                    SetorAtualId = table.Column<long>(type: "bigint", nullable: false),
                    SetorTransferenciaId = table.Column<long>(type: "bigint", nullable: true),
                    InicioAtendimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataTransferencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataFinalizado = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ObservacaoTransferencia = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_chamado_atendimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_chamado_atendimento_tb_chamado_ChamadoId",
                        column: x => x.ChamadoId,
                        principalTable: "tb_chamado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_chamado_atendimento_tb_setor_SetorAtualId",
                        column: x => x.SetorAtualId,
                        principalTable: "tb_setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_chamado_atendimento_tb_setor_SetorTransferenciaId",
                        column: x => x.SetorTransferenciaId,
                        principalTable: "tb_setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_chamado_atendimento_tb_usuario_UsuarioAtendimentoId",
                        column: x => x.UsuarioAtendimentoId,
                        principalTable: "tb_usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_chamado_EquipamentoId",
                table: "tb_chamado",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_chamado_EstabelecimentoId",
                table: "tb_chamado",
                column: "EstabelecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_chamado_SetorDestinoId",
                table: "tb_chamado",
                column: "SetorDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_chamado_SetorSolicitanteId",
                table: "tb_chamado",
                column: "SetorSolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_chamado_acompanhamento_ChamadoId",
                table: "tb_chamado_acompanhamento",
                column: "ChamadoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_chamado_acompanhamento_UsuarioId",
                table: "tb_chamado_acompanhamento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_chamado_atendimento_ChamadoId",
                table: "tb_chamado_atendimento",
                column: "ChamadoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_chamado_atendimento_SetorAtualId",
                table: "tb_chamado_atendimento",
                column: "SetorAtualId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_chamado_atendimento_SetorTransferenciaId",
                table: "tb_chamado_atendimento",
                column: "SetorTransferenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_chamado_atendimento_UsuarioAtendimentoId",
                table: "tb_chamado_atendimento",
                column: "UsuarioAtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_equipamento_SetorId",
                table: "tb_equipamento",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_setor_EstabelecimentoId",
                table: "tb_setor",
                column: "EstabelecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_setor_usuario_SetorId",
                table: "tb_setor_usuario",
                column: "SetorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_chamado_acompanhamento");

            migrationBuilder.DropTable(
                name: "tb_chamado_atendimento");

            migrationBuilder.DropTable(
                name: "tb_setor_usuario");

            migrationBuilder.DropTable(
                name: "tb_chamado");

            migrationBuilder.DropTable(
                name: "tb_usuario");

            migrationBuilder.DropTable(
                name: "tb_equipamento");

            migrationBuilder.DropTable(
                name: "tb_setor");

            migrationBuilder.DropTable(
                name: "tb_estabelecimento");
        }
    }
}
