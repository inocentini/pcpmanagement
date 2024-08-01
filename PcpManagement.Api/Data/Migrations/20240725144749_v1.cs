using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcpManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VirtualMachines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hostname = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    VCpu = table.Column<int>(type: "int", nullable: true),
                    Memoria = table.Column<int>(type: "int", nullable: true),
                    HD = table.Column<int>(type: "int", nullable: true),
                    Ambiente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Emprestimo = table.Column<bool>(type: "bit", nullable: true),
                    Resolucao = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Environment = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SistemaOperacional = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Funcionalidade = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Farm = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualMachines", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VirtualMachines");
        }
    }
}
