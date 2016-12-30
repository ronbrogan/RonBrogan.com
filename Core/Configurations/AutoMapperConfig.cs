using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Models.Authentication;
using ViewModels.Authentication;
using MapAction = System.Action<AutoMapper.IMapperConfigurationExpression>;

namespace Core.Configurations
{
    public static class AutoMapperConfig
    {

        public static IConfigurationProvider Configure()
        {
            MapAction authMaps = config =>
            {
                config.CreateMap<User, CurrentUserViewModel>().ReverseMap();
            };

            MapAction miscMaps = config =>
            {

            };

            Initialize(authMaps, miscMaps);
            return Mapper.Configuration;
        }


        private static void Initialize(params MapAction[] maps)
        {
            var masterMap = maps.Aggregate((current, map) => current + map);

            Mapper.Initialize(masterMap);
        }
    }
}
