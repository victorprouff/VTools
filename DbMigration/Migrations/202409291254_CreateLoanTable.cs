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
            .WithColumn("isRendered").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("isVisible").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("loanStartDate").AsDateTimeOffset().NotNullable()
            .WithColumn("loanEndDate").AsDateTimeOffset().Nullable();
    }

    public override void Down()
    {
        Delete.Table("Loan");
    }
}