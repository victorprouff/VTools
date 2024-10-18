using FluentMigrator;

namespace DbMigration.Migrations;

[Migration(202410181626)]
public class CreateBadDayPostTable : Migration
{
    public override void Up()
    {
        Create.Table("bad_day_post")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().Unique().WithDefaultValue(SystemMethods.NewGuid)
            .WithColumn("url").AsString().NotNullable().Unique()
            .WithColumn("instagram_id").AsString().NotNullable().Unique()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().Nullable();
    }

    public override void Down()
    {
        Delete.Table("bad_day_post");
    }
}