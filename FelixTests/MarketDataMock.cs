using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Library;
using Felix.Data;

namespace FelixTests
{
    public class MarketDataMock
    {
        static MarketDataMock()
        {
            AutoMapperConfig.Register();

        }
        public static IEnumerable<Contract> Contracts
        {
            get
            {
                return new List<Contract>()
                {
                    new Contract {ContractId=1, MarketId=1, Name="YMH18", Symbol="YMH18", BeginDate=new DateTime(2017, 3, 17), EndDate=new DateTime(2018, 3, 16), Bars=new List<IBar>()},
                    new Contract {ContractId=3, MarketId=1, Name="YMM18", Symbol="YMM18", BeginDate=new DateTime(2017, 6, 16), EndDate=new DateTime(2018, 6, 15), Bars=new List<IBar>()},
                    new Contract {ContractId=4, MarketId=1, Name="YMU18", Symbol="YMU18", BeginDate=new DateTime(2017, 9, 15), EndDate=new DateTime(2018, 9, 21), Bars=new List<IBar>()},
                    new Contract {ContractId=5, MarketId=1, Name="YMZ18", Symbol="YMZ18", BeginDate=new DateTime(2017, 12, 15), EndDate=new DateTime(2018, 12, 21), Bars=new List<IBar>()},
                    new Contract {ContractId=6, MarketId=1, Name="YMH19", Symbol="YMH19", BeginDate=new DateTime(2017, 3, 16), EndDate=new DateTime(2019, 3, 15), Bars=new List<IBar>()}
                };
            }
        }

        public static IEnumerable<MetaMapping> MetaMappings
        {
            get
            {
                return new List<MetaMapping>()
                {
                    new MetaMapping{MetaMappingId=1, MarketId=1, Property="A0", Value=4, DValue=null},
                    new MetaMapping{MetaMappingId=2, MarketId=1, Property="A1", Value=9, DValue=null},
                    new MetaMapping{MetaMappingId=3, MarketId=1, Property="A2", Value=18, DValue=null},
                    new MetaMapping{MetaMappingId=4, MarketId=1, Property="A3", Value=27, DValue=null},
                    new MetaMapping{MetaMappingId=5, MarketId=1, Property="A4", Value=36, DValue=null},
                    new MetaMapping{MetaMappingId=6, MarketId=1, Property="gamma", Value=null, DValue=1.0}

                };
            }
        }
    }
}
