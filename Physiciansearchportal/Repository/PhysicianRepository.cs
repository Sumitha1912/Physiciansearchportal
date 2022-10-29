using Microsoft.EntityFrameworkCore;
using Physiciansearchportal.Data;
using Physiciansearchportal.DomainModel;

namespace Physiciansearchportal.Repository
{
    public class PhysicianRepository : IPhysicianRepository
    {

        private readonly PhysiciansearchDbContext physiciansearchDbContext;

        public PhysicianRepository(PhysiciansearchDbContext physiciansearchDbContext)
        {
            this.physiciansearchDbContext = physiciansearchDbContext;
        }

        public async Task<Physician> AddAsync(Physician physician)
        {
            physician.Id = Guid.NewGuid();
            await physiciansearchDbContext.AddAsync(physician);
            await physiciansearchDbContext.SaveChangesAsync();
            return physician;
        }


        public async Task<Physician> DeleteAsync(Guid id)
        {
            var physician = await physiciansearchDbContext.Physicians.FirstOrDefaultAsync(x => x.Id == id);

            if (physician == null)
            {
                return null;
            }

            // Delete the region
            physiciansearchDbContext.Physicians.Remove(physician);
            await physiciansearchDbContext.SaveChangesAsync();
            return physician;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await physiciansearchDbContext.Physicians.AnyAsync(x => x.Id == id);
        }



        public async Task<Physician> GetAsync(Guid id)
        {
            return await physiciansearchDbContext.Physicians.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Physician>> GetAllAsync()
        {
            return await physiciansearchDbContext.Physicians.ToListAsync();
        }
        public IEnumerable<Physician> SearchAsync(DataModel.SearchPhysicianRequest searchPhysicianRequest)
        {
            if (searchPhysicianRequest.Firstname != null && searchPhysicianRequest.Lastname != null && searchPhysicianRequest.City != null && searchPhysicianRequest.Speciality != null)
                return physiciansearchDbContext.Physicians.Where(p => p.Firstname == searchPhysicianRequest.Firstname &&
                                                                       p.Lastname == searchPhysicianRequest.Lastname &&
                                                                       p.City == searchPhysicianRequest.City &&
                                                                       p.Speciality == searchPhysicianRequest.Speciality);
            else if (searchPhysicianRequest.Firstname != null && searchPhysicianRequest.Lastname != null && searchPhysicianRequest.City == null && searchPhysicianRequest.Speciality != null)
                return physiciansearchDbContext.Physicians.Where(p => p.Firstname == searchPhysicianRequest.Firstname &&
                                                                   p.Lastname == searchPhysicianRequest.Lastname);
            else
                return Array.Empty<Physician>();
        }



        public async Task<Physician> UpdateAsync(Guid id, Physician physician)
        {
            var existingPhysician = await physiciansearchDbContext.Physicians.FirstOrDefaultAsync(x => x.Id == id);

            if (existingPhysician == null)
            {
                return null;
            }

            existingPhysician.Firstname = physician.Firstname;
            existingPhysician.Lastname = physician.Lastname;
            existingPhysician.Age = physician.Age;
            existingPhysician.Gender = physician.Gender;
            existingPhysician.City = physician.City;
            existingPhysician.Phonenumber = physician.Phonenumber;
            existingPhysician.Speciality = physician.Speciality;
            existingPhysician.Credentials = physician.Credentials;
            existingPhysician.ProfilePhotoUrl = physician.ProfilePhotoUrl;

            await physiciansearchDbContext.SaveChangesAsync();

            return existingPhysician;
        }

        public async Task<bool> UpdateProfilePhoto(Guid id, string profilePhotoUrl)
        {
            var physician = await GetAsync(id);
             
            if(physician != null)
            {
                physician.ProfilePhotoUrl = profilePhotoUrl;
                await physiciansearchDbContext.SaveChangesAsync();
                return true;
            }

            return false;

            
        }

       

        public object Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }

       
    }
}

