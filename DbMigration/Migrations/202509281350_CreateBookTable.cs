using FluentMigrator;

namespace DbMigration.Migrations;

[Migration(202509281350)]
public class CreateBookTable : Migration
{
    public override void Up()
    {
        Create.Table("books")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().Unique().WithDefaultValue(SystemMethods.NewGuid)
            .WithColumn("title").AsString().NotNullable()
            .WithColumn("author").AsString().NotNullable()
            .WithColumn("is_reading").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("comment").AsString().Nullable()
            .WithColumn("end_reading_date").AsDateTimeOffset().Nullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable().Indexed()
            .WithColumn("updated_at").AsDateTimeOffset().Nullable().Indexed();
    }

    public override void Down()
    {
        Delete.Table("books");
    }
}