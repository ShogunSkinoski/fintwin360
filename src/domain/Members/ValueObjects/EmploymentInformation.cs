using SharedKernel.Primitives;

namespace Domain.Members.ValueObjects
{
    public enum EmploymentIndustry
    {
        Agriculture,
        Mining,
        Construction,
        Manufacturing,
        Wholesale,
        Retail,
        Transportation,
        Information,
        Finance,
        RealEstate,
        Professional,
        Management,
        Administrative,
        WasteManagement,
        Educational,
        HealthCare,
        Arts,
        Entertainment,
        Recreation,
        Accommodation,
        FoodServices,
        OtherServices,
        PublicAdministration
    }

    public enum EmploymentStatus
    {
        Employed,
        Unemployed,
        Student,
        Retired,
        Other
    }

    public record class EmploymentInformation 
    {
        public EmploymentInformation(EmploymentIndustry industry, EmploymentStatus status)
        {
            EmploymentIndustry = industry;
            Status = status;
        }
        public static EmploymentInformation Create(EmploymentIndustry industry, EmploymentStatus status)
        {
            return new EmploymentInformation(industry, status);
        }
        public EmploymentIndustry EmploymentIndustry { get; private set; }
        public EmploymentStatus Status { get; private set; }

        
    }
}