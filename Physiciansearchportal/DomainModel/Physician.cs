namespace Physiciansearchportal.DomainModel
{
    public class Physician
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string City { get; set; }

        public long Phonenumber { get; set; }

        public string Speciality { get; set; }

        public string Credentials { get; set; }
        public string ProfilePhotoUrl { get; internal set; }



        internal bool Any()
        {
            throw new NotImplementedException();
        }
    }
}
