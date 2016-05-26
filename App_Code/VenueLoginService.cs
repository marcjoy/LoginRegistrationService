using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VenueLoginService" in code, svc and config file together.
public class VenueLoginService : IVenueLoginService
{
    ShowTrackerEntities db = new ShowTrackerEntities();

    public bool RegisterVenue(VenueInfo vi)
    {
        bool result = true;
        int pass = db.usp_RegisterVenue(
            vi.VenueName,
            vi.VenueAddress,
            vi.VenueCity,
            vi.VenueState,
            vi.VenueZipCode,
            vi.VenuePhone, vi.VenueEmail,
            vi.VenueWebPage,
            vi.VenueAgeRestriction,
            vi.VenueLoginUserName,
            vi.VenueLoginPasswordPlain
            );

        if (pass == -1)
        {
            result = false;
        }

        return result;
    }

   
    public int venueLogin(string username, string password)
    {
        int venueKey = 0;
        int result = db.usp_venueLogin(username, password);
        if (result != -1)
        {
            var key = (from vi in db.Venues where vi.VenueName.Equals(username) select new { vi.VenueKey }).FirstOrDefault();

            venueKey = (int)key.VenueKey;
        }

        return venueKey;
    }

}
