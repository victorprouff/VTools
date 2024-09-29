using FluentMigrator;

namespace DbMigration.Migrations;

[Migration(202409282057)]
public class CreateIdentitySchema : Migration
{
    public override void Up()
    {
        Execute.Script("InstallExtension.sql");

        Create.Table("AspNetRoles")
            .WithColumn("Id").AsString().NotNullable().PrimaryKey().Unique()
            .WithColumn("Name").AsString().Nullable()
            .WithColumn("NormalizedName").AsString().Nullable()
            .WithColumn("ConcurrencyStamp").AsString().Nullable();

        Create.Table("AspNetUsers")
            .WithColumn("Id").AsString().NotNullable().PrimaryKey().Unique()
            .WithColumn("UserName").AsString().Nullable()
            .WithColumn("NormalizedUserName").AsString().Nullable()
            .WithColumn("NormalizedEmail").AsString().Nullable()
            .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
            .WithColumn("PasswordHash").AsString().Nullable()
            .WithColumn("SecurityStamp").AsString().Nullable()
            .WithColumn("ConcurrencyStamp").AsString().Nullable()
            .WithColumn("PhoneNumber").AsString().Nullable()
            .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
            .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
            .WithColumn("LockoutEnd").AsDateTimeOffset().Nullable()
            .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
            .WithColumn("AccessFailedCount").AsInt32().NotNullable();

        Create.Table("AspNetRoleClaims")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Unique()
            .WithColumn("RoleId").AsString().NotNullable()
            .WithColumn("ClaimType").AsString().Nullable()
            .WithColumn("ClaimValue").AsString().Nullable();

        Create.Table("AspNetUserClaims")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Unique()
            .WithColumn("UserId").AsString().NotNullable()
            .WithColumn("ClaimType").AsString().Nullable()
            .WithColumn("ClaimValue").AsString().Nullable();

        Create.Table("AspNetUserRoles")
            .WithColumn("UserId").AsString().NotNullable().PrimaryKey().Unique()
            .WithColumn("RoleId").AsString().NotNullable();

        Create.Table("AspNetUserTokens")
            .WithColumn("UserId").AsString().NotNullable().PrimaryKey().Unique()
            .WithColumn("LoginProvider").AsString().NotNullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Value").AsString().Nullable();

    }

    public override void Down()
    {
        Delete.Table("AspNetRoles");
        Delete.Table("AspNetUsers");
        Delete.Table("AspNetRoleClaims");
        Delete.Table("AspNetUserClaims");
        Delete.Table("AspNetUserRoles");
        Delete.Table("AspNetUserTokens");
    }
}