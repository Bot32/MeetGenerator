using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Model.Repositories
{
    public interface IPlaceRepository
    {
        void CreatePlace(Place place);
        Place GetPlace(Guid id);
        void UpdatePlaceInfo(Place place);
        void DeletePlace(Guid id);
    }
}
