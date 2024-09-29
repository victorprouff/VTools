using FluentMigrator;

namespace DbMigration.Migrations;

[Migration(202409291254)]
public class CreateLoanTable : Migration
{
    public override void Up()
    {
        Create.Table("Loan")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().Unique().WithDefaultValue(SystemMethods.NewGuid)
            .WithColumn("Titre").AsString().NotNullable()
            .WithColumn("Borrower").AsString().NotNullable()
            .WithColumn("IsRendered").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("IsVisible").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("LoanStartDate").AsDateTimeOffset().NotNullable()
            .WithColumn("LoanEndDate").AsDateTimeOffset().Nullable();
    }

    public override void Down()
    {
        Delete.Table("Loan");
    }
}