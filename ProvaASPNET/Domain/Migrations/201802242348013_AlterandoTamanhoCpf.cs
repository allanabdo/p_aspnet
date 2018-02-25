namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterandoTamanhoCpf : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tbl_cliente", "IX_cpfCli");
            AlterColumn("dbo.tbl_cliente", "cpf", c => c.String(nullable: false, maxLength: 14));
            CreateIndex("dbo.tbl_cliente", "cpf", unique: true, name: "IX_cpfCli");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tbl_cliente", "IX_cpfCli");
            AlterColumn("dbo.tbl_cliente", "cpf", c => c.String(nullable: false, maxLength: 11));
            CreateIndex("dbo.tbl_cliente", "cpf", unique: true, name: "IX_cpfCli");
        }
    }
}
