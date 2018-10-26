using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperRepositoryLib
{
    public static class AutoMapperConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                /* */
                //cfg.AddProfile(new CustomerRequestProfile());
                //cfg.AddProfile(new QuotationProfile());

                ///* BLL Auto Mapping */
                //cfg.AddProfile(new BLLQuotationProfile());
                //cfg.AddProfile(new BLLHelpListProfile());
                cfg.AddProfile(new BLLProfileProfile());
            });
        }
    }
}
