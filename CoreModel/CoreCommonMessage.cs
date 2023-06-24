using System;

namespace astute.CoreModel
{
    public static class CoreCommonMessage
    {
        public static string DataSuccessfullyFound = "Data successfully found.";
        public static string DataNotFound = "Data not found.";
        public static string AddedSuccessfully = " added successfully.";
        public static string UpdatedSuccessfully = " updated successfully.";
        public static string DeletedSuccessfully = " deleted successfully.";
        public static string AlreadyExists = " already exists.";
        public static string ParameterMismatched = "Parameter mismatched!";
        public static string InteralServerError = "Internal server error.";

        #region Category Master
        public static string CategoryMaster = "Category";
        public static string CategoryMasterCreated = CategoryMaster + AddedSuccessfully;
        public static string CategoryMasterUpdated = CategoryMaster + UpdatedSuccessfully;
        public static string CategoryMasterDeleted = CategoryMaster + DeletedSuccessfully;
        public static string CategoryExists = CategoryMaster + AlreadyExists;
        #endregion

        #region Category Value
        public static string CategoryValue = "Category value";
        public static string CategoryValueCreated = CategoryValue + AddedSuccessfully;
        public static string CategoryValueUpdated = CategoryValue + UpdatedSuccessfully;
        public static string CategoryValueDeleted = CategoryValue + DeletedSuccessfully;
        #endregion

        #region Supplier Value
        public static string SupplierValue = "Supplier value";
        public static string SupplierValueCreated = SupplierValue + AddedSuccessfully;
        public static string SupplierValueUpdated = SupplierValue + UpdatedSuccessfully;
        public static string SupplierValueDeleted = SupplierValue + DeletedSuccessfully;
        #endregion

        #region Country
        public static string Country = "Country";
        public static string CountryCreated = Country + AddedSuccessfully;
        public static string CountryUpdated = Country + UpdatedSuccessfully;
        public static string CountryDeleted = Country + DeletedSuccessfully;
        public static string CountryExists = Country + AlreadyExists;
        #endregion

        #region State
        public static string State = "State";
        public static string StateCreated = State + AddedSuccessfully;
        public static string StateUpdated = State + UpdatedSuccessfully;
        public static string StateDeleted = State + DeletedSuccessfully;
        #endregion

        #region City
        public static string City = "City";
        public static string CityCreated = City + AddedSuccessfully;
        public static string CityUpdated = City + UpdatedSuccessfully;
        public static string CityDeleted = City + DeletedSuccessfully;
        #endregion

        #region Terms
        public static string Terms = "Terms";
        public static string TermsCreated = Terms + AddedSuccessfully;
        public static string TermsUpdated = Terms + UpdatedSuccessfully;
        public static string TermsDeleted = Terms + DeletedSuccessfully;
        #endregion

        #region Proccess Master
        public static string ProccessMaster = "Proccess master";
        public static string ProccessMasterCreated = ProccessMaster + AddedSuccessfully;
        public static string ProccessMasterUpdated = ProccessMaster + UpdatedSuccessfully;
        public static string ProccessMasterDeleted = ProccessMaster + DeletedSuccessfully;
        #endregion

        #region Currency
        public static string Currency = "Currency";
        public static string CurrencyCreated = Currency + AddedSuccessfully;
        public static string CurrencyUpdated = Currency + UpdatedSuccessfully;
        public static string CurrencyDeleted = Currency + DeletedSuccessfully;
        #endregion
    }
}
