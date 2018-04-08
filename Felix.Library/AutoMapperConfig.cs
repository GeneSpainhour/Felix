using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Felix.Interfaces;
using Felix.Data;

namespace Felix.Library
{
    public class AutoMapperConfig
    {
        private static bool Registered = false;

        public static void Register()
        {
            if (!Registered)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Felix.MarketData.Market, IMarket>();
                    cfg.CreateMap<Felix.MarketData.Contract, IContract>();
                    cfg.CreateMap<Felix.MarketData.MetaMapping, IMetaMapping>();
                    cfg.CreateMap<IBar, Felix.MarketData.Bar>();
                    cfg.CreateMap<Felix.MarketData.Trend, Trend>();
                });

                Registered = true;
            }
        }
    }
}
