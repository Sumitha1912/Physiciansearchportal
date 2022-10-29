using Physiciansearchportal.DomainModel;


namespace Physiciansearchportal.Repository
{
    public interface IPhysicianRepository
    {
       

        Task<IEnumerable<Physician>> GetAllAsync();

            Task<Physician> GetAsync(Guid id);

            Task<Physician> AddAsync(Physician physician);

            Task<Physician> DeleteAsync(Guid id);

            Task<Physician> UpdateAsync(Guid id, Physician physician);

        Task<bool> UpdateProfilePhoto(Guid id, string profilePhotoUrl);

        IEnumerable<Physician> SearchAsync(DataModel.SearchPhysicianRequest searchPhysicianRequest);



        Task SaveChangesAsync();
        object Where(Func<object, bool> p);
        Task<bool> Exists(Guid id);
        
    }
    }

