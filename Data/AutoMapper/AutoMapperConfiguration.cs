using AutoMapper;

namespace Data.AutoMapper;

public class AutoMapperConfiguration
{
    public static MapperConfiguration RegisterMapping()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AllowNullCollections = true;

            cfg.AddProfile(new AutoMapperProfile());

        });

    }
}
