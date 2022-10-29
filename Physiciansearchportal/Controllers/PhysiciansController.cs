using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Physiciansearchportal.DomainModel;
using Physiciansearchportal.Repository;
    
namespace Physiciansearchportal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class PhysiciansController : Controller
    {
        private readonly IPhysicianRepository physicianRepository;
        private readonly IMapper mapper;
        private readonly IPhotoRepository photoRepository;

        public PhysiciansController(IPhysicianRepository physicianRepository, IMapper mapper, IPhotoRepository photoRepository)
        {
            this.physicianRepository = physicianRepository;
            this.mapper = mapper;
            this.photoRepository = photoRepository;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllPhysiciansAsync()
        {
            var physicians = await physicianRepository.GetAllAsync();
            var physiciansData = mapper.Map<List<DataModel.Physician>>(physicians);

            return Ok(physiciansData);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetPhysicianAsync")]

        public async Task<IActionResult> GetPhysicianAsync(Guid id)
        {
            var physician = await physicianRepository.GetAsync(id);

            if (physician == null)
            {
                return NotFound();
            }

            var physicianData = mapper.Map<DataModel.Physician>(physician);
            return Ok(physicianData);
        }

      

        [HttpPost]
        public async Task<IActionResult> AddPhysicianAsync(DataModel.AddPhysicianRequest addPhysicianRequest)
        {
           
            var physician = new DomainModel.Physician()
            {
                Firstname = addPhysicianRequest.Firstname,
                Lastname = addPhysicianRequest.Lastname,
                Age = addPhysicianRequest.Age,
                Gender = addPhysicianRequest.Gender,
                City = addPhysicianRequest.City,
                Phonenumber = addPhysicianRequest.Phonenumber,
                Speciality = addPhysicianRequest.Speciality,
                Credentials = addPhysicianRequest.Credentials
            };

            
            
            physician = await physicianRepository.AddAsync(physician);

            

            var physicianData = new DataModel.Physician
            {
                Id = physician.Id,
                Firstname = physician.Firstname,
                Lastname = physician.Lastname,
                Age = physician.Age,
                Gender = physician.Gender,
                City = physician.Gender,
                Phonenumber = physician.Phonenumber,
                Speciality = physician.Speciality,
                Credentials = physician.Credentials
            };

            return CreatedAtAction(nameof(GetPhysicianAsync), new { id = physicianData.Id }, physicianData);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeletePhysicianAsync(Guid id)
        {
            
            var physician = await physicianRepository.DeleteAsync(id);

            
            if (physician == null)
            {
                return NotFound();
            }

            
            var physicianData = new DataModel.Physician
            {
                Id = physician.Id,
                Firstname = physician.Firstname,
                Lastname = physician.Lastname,
                Age = physician.Age,
                Gender = physician.Gender,
                City = physician.Gender,
                Phonenumber = physician.Phonenumber,
                Speciality = physician.Speciality,
                Credentials = physician.Credentials
            };


            
            return Ok(physicianData);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePhysicianAsync([FromRoute] Guid id,
            [FromBody] DataModel.UpdatePhysicianRequest updatePhysicianRequest)
        {
            var physician = new DomainModel.Physician()
            {
                Firstname = updatePhysicianRequest.Firstname,
                Lastname = updatePhysicianRequest.Lastname,
                Age = updatePhysicianRequest.Age,
                Gender = updatePhysicianRequest.Gender,
                City = updatePhysicianRequest.City,
                Phonenumber = updatePhysicianRequest.Phonenumber,
                Speciality = updatePhysicianRequest.Speciality,
                Credentials = updatePhysicianRequest.Credentials
            };
            physician = await physicianRepository.UpdateAsync(id, physician);
            if (physician == null)
            {
                return NotFound();
            }
            var physicianData = new DataModel.Physician
            {
                Id = physician.Id,
                Firstname = physician.Firstname,
                Lastname = physician.Lastname,
                Age = physician.Age,
                Gender = physician.Gender,
                City = physician.Gender,
                Phonenumber = physician.Phonenumber,
                Speciality = physician.Speciality,
                Credentials = physician.Credentials
            };
            return Ok(physicianData);
        }

        [HttpPost]
        [Route("[controller]/{id:guid}/upload-photo")]
        public async Task<IActionResult> UploadPhoto([FromRoute] Guid id, IFormFile profilePhoto)
        {  
            if (await physicianRepository.Exists(id))
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(profilePhoto.FileName);
                var filePhotoPath = await photoRepository.Upload(profilePhoto, fileName);
                if(await physicianRepository.UpdateProfilePhoto(id, filePhotoPath))
                {
                    return Ok(filePhotoPath);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading photo");
            }
            return NotFound();
            
        }
       
        
        [HttpPost]
        [Route("[controller]/search")]
        public IActionResult SearchPhysicianAsync(DataModel.SearchPhysicianRequest searchPhysicianRequest)
        {
            var searchedPhysician =  physicianRepository.SearchAsync(searchPhysicianRequest);

            
            
                return Ok(searchedPhysician);
            
            
        }


    }

    }