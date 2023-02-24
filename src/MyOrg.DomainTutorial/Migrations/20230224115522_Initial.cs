using FluentMigrator;
using MyOrg.DomainTutorial.Domain;
using Smartstore.Core.Data.Migrations;

namespace MyOrg.DomainTutorial.Migrations
{
    [MigrationVersion("2023-02-24 11:55:22", "DomainTutorial: Initial")]
    internal class _20230224115522_Initial : Migration
    {
        public override void Up()
        {
            // Tablename is taken from Domain->Attribute->Table
            var tableName = "Notification";

            if (!Schema.Table(tableName).Exists())
            {
                Create.Table(tableName)
                    .WithIdColumn() // Adds the Id column, which defaults to primary key.
                    .WithColumn(nameof(Notification.AuthorId)).AsInt32().NotNullable()
                        .Indexed("IX_Notification_AuthorId")
                    .WithColumn(nameof(Notification.Published)).AsDateTime2().NotNullable()
                        .Indexed("IX_Notification_Published")
                    .WithColumn(nameof(Notification.Message)).AsString().NotNullable();
            }
        }

        public override void Down()
        {
            // Ignore this.
        }
    }
}