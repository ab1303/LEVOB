using AutoMapper;

namespace Glossary.WebApi.Mapping
{
    public static class AutoMapperConfiguration
    {
        private static MapperConfiguration _mapperConfiguration;

        public static IMapper CreateMapper()
        {
            return _mapperConfiguration.CreateMapper();
        }

        public static void Configure()
        {
            _mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<GlossaryProfile>();
            });
            _mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}