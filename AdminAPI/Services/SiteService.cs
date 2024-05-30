﻿using Common;
using Repository.Database;

namespace AdminAPI.Services
{

    [Service(Lifetime = ServiceLifetime.Scoped)]
    public class SiteService(DatabaseContext db, IDHelper idHelper)
    {
        public bool SetSiteInfo(string key, string? value)
        {

            if (value != null)
            {

                var appSetting = db.TAppSetting.Where(t => t.Module == "Site" && t.Key == key).FirstOrDefault();

                if (appSetting == null)
                {
                    appSetting = new()
                    {
                        Id = idHelper.GetId(),
                        Module = "Site",
                        Key = key,
                        Value = value
                    };
                    db.TAppSetting.Add(appSetting);
                }
                else
                {
                    appSetting.Value = value;
                }

                db.SaveChanges();
            }

            return true;
        }
    }
}
