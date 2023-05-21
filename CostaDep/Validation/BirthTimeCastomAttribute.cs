using System.ComponentModel.DataAnnotations;

namespace Costa.Validation
{
    public class BirthTimeCastomAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                int age = CalculateAge(dateOfBirth);
                if (age < 18 || age > 65)
                {
                    return new ValidationResult($"Возраст должен быть между 18 и 65 годами.");
                }
            }
            return ValidationResult.Success;
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - dateOfBirth.Year;
            if (now < dateOfBirth.AddYears(age))
            {
                age--;
            }
            return age;
        }
    }
}
