using AutoMapper;

namespace Physiciansearchportal.Profiles
{
    public class PhysiciansProfile : Profile
    {
        public PhysiciansProfile()
        {
            CreateMap<DomainModel.Physician, DataModel.Physician>()
                .ReverseMap();
        }
    }

}
