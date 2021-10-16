using FluentValidation;

namespace OracleTask.Models
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MarkAs { get; set; }
        public string UserId { get; set; }
        public string UserUsername { get; set; }
    }


    public class LocationValidator : AbstractValidator<LocationDTO>
    {
        public LocationValidator()
        {
            RuleFor(u => u.MarkAs).NotEmpty()
                                   .WithMessage("Mark cannot be empty")
                                   .Length(0, 50)
                                   .WithMessage("Minimum length of Name must be 1 and maximum length must be 50");

            RuleFor(u => u.Latitude).NotEmpty()
                                     .WithMessage("FullLocation cannot be empty")
                                     .Length(0, 50)
                                     .WithMessage("Minimum length of Surname must be 1 and maximum length must be 50");

            RuleFor(u => u.Longitude).NotEmpty()
                                   .WithMessage("City Name cannot be empty")
                                   .Length(0, 50)
                                   .WithMessage("Minimum length of Username must be 1 and maximum length must be 50");

            RuleFor(u => u.MarkAs).NotEmpty()
                                  .WithMessage("City Name cannot be empty")
                                  .Length(0, 50)
                                  .WithMessage("Minimum length of Username must be 1 and maximum length must be 50");


            RuleFor(u => u.UserId).NotEmpty()
                                 .WithMessage("City Id cannot be empty")
                                 .Length(0, 50)
                                 .WithMessage("Minimum length of Email must be 1 and maximum length must be 50");

          
        }
    }
}
