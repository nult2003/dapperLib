using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperRepositoryLib
{
    public class StoredProcedure
    {
        /* CUSTOMER REQUEST [CUSTOMERREQUEST] */
        public const string CUSTOMERREQUEST_GETTEMPLATEBYID = "ClientDigitalPlatform.CustomerRequest_GetTemplateById";
        public const string CUSTOMERREQUEST_GETDRAFTBYHELLOID = "ClientDigitalPlatform.CustomerRequest_GetDraftByHelloId";
        public const string CUSTOMERREQUEST_GETTEMPLATESNAMEBYHELLOID = "ClientDigitalPlatform.CustomerRequest_GetTemplatesNameByHelloId";
        public const string CUSTOMERREQUEST_INSERTORUPDATE = "ClientDigitalPlatform.CustomerRequest_InsertOrUpdate";
        public const string CUSTOMERREQUEST_DELETE = "ClientDigitalPlatform.CustomerRequest_Delete";
        public const string CUSTOMERREQUEST_DELETEBYNAME = "ClientDigitalPlatform.CustomerRequest_DeleteByName";

        /* CUSTOMER REQUEST TYPE [CUSTOMERREQUESTTYPE] */
        public const string CUSTOMERREQUESTTYPE_GETALL = "ClientDigitalPlatform.CustomerRequestType_GetAll";

        /* PACKAGE & PACKAGE DANGEROUS [PACKAGE & PACKAGEDANGEROUS] */
        public const string PACKAGE_INSERTORUPDATE = "ClientDigitalPlatform.Package_InsertOrUpdate";
        public const string PACKAGEDANGEROUS_INSERTORUPDATE = "ClientDigitalPlatform.PackageDangerous_InsertOrUpdate";

        /* CONTAINER & CONTAINER DANGEROUS [CONTAINER & CONTAINERDANGEROUS] */
        public const string CONTAINER_INSERTORUPDATE = "ClientDigitalPlatform.Container_InsertOrUpdate";
        public const string CONTAINERDANGEROUS_INSERTORUPDATE = "ClientDigitalPlatform.ContainerDangerous_InsertOrUpdate";

        /* JOB TITLE [JOBTITLE] */
        public const string JOBTITLE_GETALL = "Helpers.JobTitles";

        /* INCOTERM [INCOTERM] */
        public const string INCOTERM_GETALL = "Helpers.Incoterm";

        /* MEASURE [MEASURE] */
        public const string MEASURE_GETALL = "Helpers.Measures";

        /* CURRENCY [CURRENCY] */
        public const string CURRENCY_GETALL = "Helpers.Currency";

        /* CLASS TYPE [CLASSTYPE] */
        public const string CLASSTYPE_GETALL = "Helpers.ClassType";

        /* DANGEROUS CLASS [DANGEROUSCLASS] */
        public const string DANGEROUS_GETALL = "Helpers.DangerousClass";

        /* CONTAINER TYPE [CONTAINERTYPE] */
        public const string CONTAINERTYPE_GETALL = "Helpers.ContainerType";

        /* PACKAGE TYPE [PACKAGETYPE] */
        public const string PACKAGETYPE_GETALL = "Helpers.PackageType";

        /* LANGUAGE [LANGUAGE] */
        public const string LANGUAGE_GETALL = "Helpers.SearchLanguage";

        /* GEOGRAPHICAL AREA [GEOGRAPHICALAREA] */
        public const string GEOGRAPHICALAREA_GETCITIES = "Helpers.Cities";
        public const string GEOGRAPHICALAREA_GETCOUNTRIES = "Helpers.Countries";
        public const string GEOGRAPHICALAREA_GETSTATES = "Helpers.States";
        public const string GEOGRAPHICALAREA_GETLOCATIONS = "Helpers.Locations";

        /* CONCERTO INVITATION [CONCERTOINVITATION] */
        public const string CONCERTOINVITATION_INSERTORUPDATE = "ClientDigitalPlatform.ConcertoInvitation_InsertOrUpdate";
        public const string CONCERTOINVITATION_GETALLBYEMAIL = "ClientDigitalPlatform.ConcertoInvitation_GetAllByEmail";
        public const string CONCERTOINVITATION_INSERTMULTIPLE = "ClientDigitalPlatform.ConcertoInvitation_InsertMultiple";
        public const string CONCERTOINVITATION_SETINVITATIONRESPONSE = "ClientDigitalPlatform.ConcertoInvitation_SetResponse";
        public const string CONCERTOINVITATION_SETINVITATIONASINACTIVE = "ClientDigitalPlatform.ConcertoInvitation_SetInvitationsAsInactive";
    }

}
