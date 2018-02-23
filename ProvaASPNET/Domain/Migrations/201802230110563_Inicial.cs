namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_cliente",
                c => new
                    {
                        id_cliente = c.Guid(nullable: false, identity: true),
                        codigo = c.String(nullable: false, maxLength: 30),
                        nome = c.String(nullable: false, maxLength: 200),
                        cpf = c.String(nullable: false, maxLength: 11),
                        data_nascimento = c.DateTime(nullable: false),
                        data_cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_cliente)
                .Index(t => t.codigo, unique: true, name: "IX_codigoCli")
                .Index(t => t.cpf, unique: true, name: "IX_cpfCli");
            
            CreateTable(
                "dbo.tbl_pedido",
                c => new
                    {
                        id_pedido = c.Guid(nullable: false, identity: true),
                        codigo = c.String(nullable: false, maxLength: 30),
                        id_cliente = c.Guid(nullable: false),
                        valor_total = c.Double(nullable: false),
                        data_pedido = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_pedido)
                .ForeignKey("dbo.tbl_cliente", t => t.id_cliente, cascadeDelete: true)
                .Index(t => t.codigo, unique: true, name: "IX_codigoPedi")
                .Index(t => t.id_cliente);
            
            CreateTable(
                "dbo.tbl_produto",
                c => new
                    {
                        id_produto = c.Guid(nullable: false, identity: true),
                        codigo = c.String(nullable: false, maxLength: 30),
                        codigo_barra = c.String(nullable: false, maxLength: 15),
                        descricao = c.String(nullable: false, maxLength: 255),
                        valor_venda = c.Double(nullable: false),
                        data_cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_produto)
                .Index(t => t.codigo, unique: true, name: "IX_codigoProd")
                .Index(t => t.codigo_barra, unique: true, name: "IX_codigoBarraProd");
            
            CreateTable(
                "dbo.tbl_pedido_produto",
                c => new
                    {
                        id_pedido = c.Guid(nullable: false),
                        id_produto = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.id_pedido, t.id_produto })
                .ForeignKey("dbo.tbl_pedido", t => t.id_pedido, cascadeDelete: true)
                .ForeignKey("dbo.tbl_produto", t => t.id_produto, cascadeDelete: true)
                .Index(t => t.id_pedido)
                .Index(t => t.id_produto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_pedido_produto", "id_produto", "dbo.tbl_produto");
            DropForeignKey("dbo.tbl_pedido_produto", "id_pedido", "dbo.tbl_pedido");
            DropForeignKey("dbo.tbl_pedido", "id_cliente", "dbo.tbl_cliente");
            DropIndex("dbo.tbl_pedido_produto", new[] { "id_produto" });
            DropIndex("dbo.tbl_pedido_produto", new[] { "id_pedido" });
            DropIndex("dbo.tbl_produto", "IX_codigoBarraProd");
            DropIndex("dbo.tbl_produto", "IX_codigoProd");
            DropIndex("dbo.tbl_pedido", new[] { "id_cliente" });
            DropIndex("dbo.tbl_pedido", "IX_codigoPedi");
            DropIndex("dbo.tbl_cliente", "IX_cpfCli");
            DropIndex("dbo.tbl_cliente", "IX_codigoCli");
            DropTable("dbo.tbl_pedido_produto");
            DropTable("dbo.tbl_produto");
            DropTable("dbo.tbl_pedido");
            DropTable("dbo.tbl_cliente");
        }
    }
}
