using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Resources.Infrastructure.DbRepositories.Configurations
{
    public class JsonValueConverter<T> : ValueConverter<T, string>
    {
        public JsonValueConverter()
            : base(
                  v => JsonConvert.SerializeObject(v),
                  v => JsonConvert.DeserializeObject<T>(v))
        {
        }
    }

    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<T> HasJsonValueConversion<T>(this PropertyBuilder<T> builder)
        {
            return builder.HasConversion(new JsonValueConverter<T>());
        }
    }
}
