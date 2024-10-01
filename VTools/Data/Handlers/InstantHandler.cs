using System.Data;
using Dapper;
using NodaTime;

namespace VTools.Data.Handlers;

public class InstantHandler : SqlMapper.TypeHandler<Instant>
{
    public override void SetValue(IDbDataParameter parameter, Instant value)
    {
        parameter.Value = value.ToDateTimeUtc();
        parameter.DbType = DbType.DateTime;
    }

    // This is not necessary since Npgsql already provide the correct typed value
    public override Instant Parse(object value) => Instant.FromDateTimeUtc((DateTime)value);
}