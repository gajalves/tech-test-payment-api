using AutoMapper;
using tech_test_payment.application.Mappings;

namespace tech_test_payment.application;

public static class AutoMapperConfiguration
{
    public static MapperConfiguration Create()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AllowNullCollections = true;
            cfg.AddProfile(new MappingProfile());
        });

        return mapperConfig;

    }
}
