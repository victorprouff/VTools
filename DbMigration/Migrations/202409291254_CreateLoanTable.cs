using FluentMigrator;

namespace DbMigration.Migrations;

[Migration(202409291254)]
public class CreateLoanTable : Migration
{
    public override void Up()
    {
        Create.Table("loans")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().Unique().WithDefaultValue(SystemMethods.NewGuid)
            .WithColumn("title").AsString().NotNullable()
            .WithColumn("borrower").AsString().NotNullable()
            .WithColumn("is_rendered").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("is_visible").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("loan_start_date").AsDateTimeOffset().NotNullable()
            .WithColumn("loan_end_date").AsDateTimeOffset().Nullable();
    }

    public override void Down()
    {
        Delete.Table("loans");
    }
}