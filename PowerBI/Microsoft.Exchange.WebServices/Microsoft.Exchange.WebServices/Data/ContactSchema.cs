using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001B7 RID: 439
	[Schema]
	public class ContactSchema : ItemSchema
	{
		// Token: 0x060014D1 RID: 5329 RVA: 0x00039654 File Offset: 0x00038654
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(ContactSchema.FileAs);
			base.RegisterProperty(ContactSchema.FileAsMapping);
			base.RegisterProperty(ContactSchema.DisplayName);
			base.RegisterProperty(ContactSchema.GivenName);
			base.RegisterProperty(ContactSchema.Initials);
			base.RegisterProperty(ContactSchema.MiddleName);
			base.RegisterProperty(ContactSchema.NickName);
			base.RegisterProperty(ContactSchema.CompleteName);
			base.RegisterProperty(ContactSchema.CompanyName);
			base.RegisterProperty(ContactSchema.EmailAddresses);
			base.RegisterProperty(ContactSchema.PhysicalAddresses);
			base.RegisterProperty(ContactSchema.PhoneNumbers);
			base.RegisterProperty(ContactSchema.AssistantName);
			base.RegisterProperty(ContactSchema.Birthday);
			base.RegisterProperty(ContactSchema.BusinessHomePage);
			base.RegisterProperty(ContactSchema.Children);
			base.RegisterProperty(ContactSchema.Companies);
			base.RegisterProperty(ContactSchema.ContactSource);
			base.RegisterProperty(ContactSchema.Department);
			base.RegisterProperty(ContactSchema.Generation);
			base.RegisterProperty(ContactSchema.ImAddresses);
			base.RegisterProperty(ContactSchema.JobTitle);
			base.RegisterProperty(ContactSchema.Manager);
			base.RegisterProperty(ContactSchema.Mileage);
			base.RegisterProperty(ContactSchema.OfficeLocation);
			base.RegisterProperty(ContactSchema.PostalAddressIndex);
			base.RegisterProperty(ContactSchema.Profession);
			base.RegisterProperty(ContactSchema.SpouseName);
			base.RegisterProperty(ContactSchema.Surname);
			base.RegisterProperty(ContactSchema.WeddingAnniversary);
			base.RegisterProperty(ContactSchema.HasPicture);
			base.RegisterProperty(ContactSchema.PhoneticFullName);
			base.RegisterProperty(ContactSchema.PhoneticFirstName);
			base.RegisterProperty(ContactSchema.PhoneticLastName);
			base.RegisterProperty(ContactSchema.Alias);
			base.RegisterProperty(ContactSchema.Notes);
			base.RegisterProperty(ContactSchema.Photo);
			base.RegisterProperty(ContactSchema.UserSMIMECertificate);
			base.RegisterProperty(ContactSchema.MSExchangeCertificate);
			base.RegisterProperty(ContactSchema.DirectoryId);
			base.RegisterProperty(ContactSchema.ManagerMailbox);
			base.RegisterProperty(ContactSchema.DirectReports);
			base.RegisterIndexedProperty(ContactSchema.EmailAddress1);
			base.RegisterIndexedProperty(ContactSchema.EmailAddress2);
			base.RegisterIndexedProperty(ContactSchema.EmailAddress3);
			base.RegisterIndexedProperty(ContactSchema.ImAddress1);
			base.RegisterIndexedProperty(ContactSchema.ImAddress2);
			base.RegisterIndexedProperty(ContactSchema.ImAddress3);
			base.RegisterIndexedProperty(ContactSchema.AssistantPhone);
			base.RegisterIndexedProperty(ContactSchema.BusinessFax);
			base.RegisterIndexedProperty(ContactSchema.BusinessPhone);
			base.RegisterIndexedProperty(ContactSchema.BusinessPhone2);
			base.RegisterIndexedProperty(ContactSchema.Callback);
			base.RegisterIndexedProperty(ContactSchema.CarPhone);
			base.RegisterIndexedProperty(ContactSchema.CompanyMainPhone);
			base.RegisterIndexedProperty(ContactSchema.HomeFax);
			base.RegisterIndexedProperty(ContactSchema.HomePhone);
			base.RegisterIndexedProperty(ContactSchema.HomePhone2);
			base.RegisterIndexedProperty(ContactSchema.Isdn);
			base.RegisterIndexedProperty(ContactSchema.MobilePhone);
			base.RegisterIndexedProperty(ContactSchema.OtherFax);
			base.RegisterIndexedProperty(ContactSchema.OtherTelephone);
			base.RegisterIndexedProperty(ContactSchema.Pager);
			base.RegisterIndexedProperty(ContactSchema.PrimaryPhone);
			base.RegisterIndexedProperty(ContactSchema.RadioPhone);
			base.RegisterIndexedProperty(ContactSchema.Telex);
			base.RegisterIndexedProperty(ContactSchema.TtyTddPhone);
			base.RegisterIndexedProperty(ContactSchema.BusinessAddressStreet);
			base.RegisterIndexedProperty(ContactSchema.BusinessAddressCity);
			base.RegisterIndexedProperty(ContactSchema.BusinessAddressState);
			base.RegisterIndexedProperty(ContactSchema.BusinessAddressCountryOrRegion);
			base.RegisterIndexedProperty(ContactSchema.BusinessAddressPostalCode);
			base.RegisterIndexedProperty(ContactSchema.HomeAddressStreet);
			base.RegisterIndexedProperty(ContactSchema.HomeAddressCity);
			base.RegisterIndexedProperty(ContactSchema.HomeAddressState);
			base.RegisterIndexedProperty(ContactSchema.HomeAddressCountryOrRegion);
			base.RegisterIndexedProperty(ContactSchema.HomeAddressPostalCode);
			base.RegisterIndexedProperty(ContactSchema.OtherAddressStreet);
			base.RegisterIndexedProperty(ContactSchema.OtherAddressCity);
			base.RegisterIndexedProperty(ContactSchema.OtherAddressState);
			base.RegisterIndexedProperty(ContactSchema.OtherAddressCountryOrRegion);
			base.RegisterIndexedProperty(ContactSchema.OtherAddressPostalCode);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x000399ED File Offset: 0x000389ED
		internal ContactSchema()
		{
		}

		// Token: 0x04000B59 RID: 2905
		public static readonly PropertyDefinition FileAs = new StringPropertyDefinition("FileAs", "contacts:FileAs", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B5A RID: 2906
		public static readonly PropertyDefinition FileAsMapping = new GenericPropertyDefinition<FileAsMapping>("FileAsMapping", "contacts:FileAsMapping", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B5B RID: 2907
		public static readonly PropertyDefinition DisplayName = new StringPropertyDefinition("DisplayName", "contacts:DisplayName", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B5C RID: 2908
		public static readonly PropertyDefinition GivenName = new StringPropertyDefinition("GivenName", "contacts:GivenName", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B5D RID: 2909
		public static readonly PropertyDefinition Initials = new StringPropertyDefinition("Initials", "contacts:Initials", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B5E RID: 2910
		public static readonly PropertyDefinition MiddleName = new StringPropertyDefinition("MiddleName", "contacts:MiddleName", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B5F RID: 2911
		public static readonly PropertyDefinition NickName = new StringPropertyDefinition("Nickname", "contacts:Nickname", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B60 RID: 2912
		public static readonly PropertyDefinition CompleteName = new ComplexPropertyDefinition<CompleteName>("CompleteName", "contacts:CompleteName", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new CompleteName());

		// Token: 0x04000B61 RID: 2913
		public static readonly PropertyDefinition CompanyName = new StringPropertyDefinition("CompanyName", "contacts:CompanyName", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B62 RID: 2914
		public static readonly PropertyDefinition EmailAddresses = new ComplexPropertyDefinition<EmailAddressDictionary>("EmailAddresses", "contacts:EmailAddresses", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate, ExchangeVersion.Exchange2007_SP1, () => new EmailAddressDictionary());

		// Token: 0x04000B63 RID: 2915
		public static readonly PropertyDefinition PhysicalAddresses = new ComplexPropertyDefinition<PhysicalAddressDictionary>("PhysicalAddresses", "contacts:PhysicalAddresses", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate, ExchangeVersion.Exchange2007_SP1, () => new PhysicalAddressDictionary());

		// Token: 0x04000B64 RID: 2916
		public static readonly PropertyDefinition PhoneNumbers = new ComplexPropertyDefinition<PhoneNumberDictionary>("PhoneNumbers", "contacts:PhoneNumbers", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate, ExchangeVersion.Exchange2007_SP1, () => new PhoneNumberDictionary());

		// Token: 0x04000B65 RID: 2917
		public static readonly PropertyDefinition AssistantName = new StringPropertyDefinition("AssistantName", "contacts:AssistantName", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B66 RID: 2918
		public static readonly PropertyDefinition Birthday = new DateTimePropertyDefinition("Birthday", "contacts:Birthday", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B67 RID: 2919
		public static readonly PropertyDefinition BusinessHomePage = new StringPropertyDefinition("BusinessHomePage", "contacts:BusinessHomePage", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B68 RID: 2920
		public static readonly PropertyDefinition Children = new ComplexPropertyDefinition<StringList>("Children", "contacts:Children", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new StringList());

		// Token: 0x04000B69 RID: 2921
		public static readonly PropertyDefinition Companies = new ComplexPropertyDefinition<StringList>("Companies", "contacts:Companies", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new StringList());

		// Token: 0x04000B6A RID: 2922
		public static readonly PropertyDefinition ContactSource = new GenericPropertyDefinition<ContactSource>("ContactSource", "contacts:ContactSource", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B6B RID: 2923
		public static readonly PropertyDefinition Department = new StringPropertyDefinition("Department", "contacts:Department", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B6C RID: 2924
		public static readonly PropertyDefinition Generation = new StringPropertyDefinition("Generation", "contacts:Generation", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B6D RID: 2925
		public static readonly PropertyDefinition ImAddresses = new ComplexPropertyDefinition<ImAddressDictionary>("ImAddresses", "contacts:ImAddresses", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate, ExchangeVersion.Exchange2007_SP1, () => new ImAddressDictionary());

		// Token: 0x04000B6E RID: 2926
		public static readonly PropertyDefinition JobTitle = new StringPropertyDefinition("JobTitle", "contacts:JobTitle", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B6F RID: 2927
		public static readonly PropertyDefinition Manager = new StringPropertyDefinition("Manager", "contacts:Manager", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B70 RID: 2928
		public static readonly PropertyDefinition Mileage = new StringPropertyDefinition("Mileage", "contacts:Mileage", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B71 RID: 2929
		public static readonly PropertyDefinition OfficeLocation = new StringPropertyDefinition("OfficeLocation", "contacts:OfficeLocation", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B72 RID: 2930
		public static readonly PropertyDefinition PostalAddressIndex = new GenericPropertyDefinition<PhysicalAddressIndex>("PostalAddressIndex", "contacts:PostalAddressIndex", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B73 RID: 2931
		public static readonly PropertyDefinition Profession = new StringPropertyDefinition("Profession", "contacts:Profession", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B74 RID: 2932
		public static readonly PropertyDefinition SpouseName = new StringPropertyDefinition("SpouseName", "contacts:SpouseName", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B75 RID: 2933
		public static readonly PropertyDefinition Surname = new StringPropertyDefinition("Surname", "contacts:Surname", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B76 RID: 2934
		public static readonly PropertyDefinition WeddingAnniversary = new DateTimePropertyDefinition("WeddingAnniversary", "contacts:WeddingAnniversary", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000B77 RID: 2935
		public static readonly PropertyDefinition HasPicture = new BoolPropertyDefinition("HasPicture", "contacts:HasPicture", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010);

		// Token: 0x04000B78 RID: 2936
		public static readonly PropertyDefinition PhoneticFullName = new StringPropertyDefinition("PhoneticFullName", "contacts:PhoneticFullName", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B79 RID: 2937
		public static readonly PropertyDefinition PhoneticFirstName = new StringPropertyDefinition("PhoneticFirstName", "contacts:PhoneticFirstName", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B7A RID: 2938
		public static readonly PropertyDefinition PhoneticLastName = new StringPropertyDefinition("PhoneticLastName", "contacts:PhoneticLastName", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B7B RID: 2939
		public static readonly PropertyDefinition Alias = new StringPropertyDefinition("Alias", "contacts:Alias", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B7C RID: 2940
		public static readonly PropertyDefinition Notes = new StringPropertyDefinition("Notes", "contacts:Notes", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B7D RID: 2941
		public static readonly PropertyDefinition Photo = new ByteArrayPropertyDefinition("Photo", "contacts:Photo", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B7E RID: 2942
		public static readonly PropertyDefinition UserSMIMECertificate = new ComplexPropertyDefinition<ByteArrayArray>("UserSMIMECertificate", "contacts:UserSMIMECertificate", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new ByteArrayArray());

		// Token: 0x04000B7F RID: 2943
		public static readonly PropertyDefinition MSExchangeCertificate = new ComplexPropertyDefinition<ByteArrayArray>("MSExchangeCertificate", "contacts:MSExchangeCertificate", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new ByteArrayArray());

		// Token: 0x04000B80 RID: 2944
		public static readonly PropertyDefinition DirectoryId = new StringPropertyDefinition("DirectoryId", "contacts:DirectoryId", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1);

		// Token: 0x04000B81 RID: 2945
		public static readonly PropertyDefinition ManagerMailbox = new ContainedPropertyDefinition<EmailAddress>("ManagerMailbox", "contacts:ManagerMailbox", "Mailbox", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new EmailAddress());

		// Token: 0x04000B82 RID: 2946
		public static readonly PropertyDefinition DirectReports = new ComplexPropertyDefinition<EmailAddressCollection>("DirectReports", "contacts:DirectReports", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010_SP1, () => new EmailAddressCollection());

		// Token: 0x04000B83 RID: 2947
		public static readonly IndexedPropertyDefinition EmailAddress1 = new IndexedPropertyDefinition("contacts:EmailAddress", "EmailAddress1");

		// Token: 0x04000B84 RID: 2948
		public static readonly IndexedPropertyDefinition EmailAddress2 = new IndexedPropertyDefinition("contacts:EmailAddress", "EmailAddress2");

		// Token: 0x04000B85 RID: 2949
		public static readonly IndexedPropertyDefinition EmailAddress3 = new IndexedPropertyDefinition("contacts:EmailAddress", "EmailAddress3");

		// Token: 0x04000B86 RID: 2950
		public static readonly IndexedPropertyDefinition ImAddress1 = new IndexedPropertyDefinition("contacts:ImAddress", "ImAddress1");

		// Token: 0x04000B87 RID: 2951
		public static readonly IndexedPropertyDefinition ImAddress2 = new IndexedPropertyDefinition("contacts:ImAddress", "ImAddress2");

		// Token: 0x04000B88 RID: 2952
		public static readonly IndexedPropertyDefinition ImAddress3 = new IndexedPropertyDefinition("contacts:ImAddress", "ImAddress3");

		// Token: 0x04000B89 RID: 2953
		public static readonly IndexedPropertyDefinition AssistantPhone = new IndexedPropertyDefinition("contacts:PhoneNumber", "AssistantPhone");

		// Token: 0x04000B8A RID: 2954
		public static readonly IndexedPropertyDefinition BusinessFax = new IndexedPropertyDefinition("contacts:PhoneNumber", "BusinessFax");

		// Token: 0x04000B8B RID: 2955
		public static readonly IndexedPropertyDefinition BusinessPhone = new IndexedPropertyDefinition("contacts:PhoneNumber", "BusinessPhone");

		// Token: 0x04000B8C RID: 2956
		public static readonly IndexedPropertyDefinition BusinessPhone2 = new IndexedPropertyDefinition("contacts:PhoneNumber", "BusinessPhone2");

		// Token: 0x04000B8D RID: 2957
		public static readonly IndexedPropertyDefinition Callback = new IndexedPropertyDefinition("contacts:PhoneNumber", "Callback");

		// Token: 0x04000B8E RID: 2958
		public static readonly IndexedPropertyDefinition CarPhone = new IndexedPropertyDefinition("contacts:PhoneNumber", "CarPhone");

		// Token: 0x04000B8F RID: 2959
		public static readonly IndexedPropertyDefinition CompanyMainPhone = new IndexedPropertyDefinition("contacts:PhoneNumber", "CompanyMainPhone");

		// Token: 0x04000B90 RID: 2960
		public static readonly IndexedPropertyDefinition HomeFax = new IndexedPropertyDefinition("contacts:PhoneNumber", "HomeFax");

		// Token: 0x04000B91 RID: 2961
		public static readonly IndexedPropertyDefinition HomePhone = new IndexedPropertyDefinition("contacts:PhoneNumber", "HomePhone");

		// Token: 0x04000B92 RID: 2962
		public static readonly IndexedPropertyDefinition HomePhone2 = new IndexedPropertyDefinition("contacts:PhoneNumber", "HomePhone2");

		// Token: 0x04000B93 RID: 2963
		public static readonly IndexedPropertyDefinition Isdn = new IndexedPropertyDefinition("contacts:PhoneNumber", "Isdn");

		// Token: 0x04000B94 RID: 2964
		public static readonly IndexedPropertyDefinition MobilePhone = new IndexedPropertyDefinition("contacts:PhoneNumber", "MobilePhone");

		// Token: 0x04000B95 RID: 2965
		public static readonly IndexedPropertyDefinition OtherFax = new IndexedPropertyDefinition("contacts:PhoneNumber", "OtherFax");

		// Token: 0x04000B96 RID: 2966
		public static readonly IndexedPropertyDefinition OtherTelephone = new IndexedPropertyDefinition("contacts:PhoneNumber", "OtherTelephone");

		// Token: 0x04000B97 RID: 2967
		public static readonly IndexedPropertyDefinition Pager = new IndexedPropertyDefinition("contacts:PhoneNumber", "Pager");

		// Token: 0x04000B98 RID: 2968
		public static readonly IndexedPropertyDefinition PrimaryPhone = new IndexedPropertyDefinition("contacts:PhoneNumber", "PrimaryPhone");

		// Token: 0x04000B99 RID: 2969
		public static readonly IndexedPropertyDefinition RadioPhone = new IndexedPropertyDefinition("contacts:PhoneNumber", "RadioPhone");

		// Token: 0x04000B9A RID: 2970
		public static readonly IndexedPropertyDefinition Telex = new IndexedPropertyDefinition("contacts:PhoneNumber", "Telex");

		// Token: 0x04000B9B RID: 2971
		public static readonly IndexedPropertyDefinition TtyTddPhone = new IndexedPropertyDefinition("contacts:PhoneNumber", "TtyTddPhone");

		// Token: 0x04000B9C RID: 2972
		public static readonly IndexedPropertyDefinition BusinessAddressStreet = new IndexedPropertyDefinition("contacts:PhysicalAddress:Street", "Business");

		// Token: 0x04000B9D RID: 2973
		public static readonly IndexedPropertyDefinition BusinessAddressCity = new IndexedPropertyDefinition("contacts:PhysicalAddress:City", "Business");

		// Token: 0x04000B9E RID: 2974
		public static readonly IndexedPropertyDefinition BusinessAddressState = new IndexedPropertyDefinition("contacts:PhysicalAddress:State", "Business");

		// Token: 0x04000B9F RID: 2975
		public static readonly IndexedPropertyDefinition BusinessAddressCountryOrRegion = new IndexedPropertyDefinition("contacts:PhysicalAddress:CountryOrRegion", "Business");

		// Token: 0x04000BA0 RID: 2976
		public static readonly IndexedPropertyDefinition BusinessAddressPostalCode = new IndexedPropertyDefinition("contacts:PhysicalAddress:PostalCode", "Business");

		// Token: 0x04000BA1 RID: 2977
		public static readonly IndexedPropertyDefinition HomeAddressStreet = new IndexedPropertyDefinition("contacts:PhysicalAddress:Street", "Home");

		// Token: 0x04000BA2 RID: 2978
		public static readonly IndexedPropertyDefinition HomeAddressCity = new IndexedPropertyDefinition("contacts:PhysicalAddress:City", "Home");

		// Token: 0x04000BA3 RID: 2979
		public static readonly IndexedPropertyDefinition HomeAddressState = new IndexedPropertyDefinition("contacts:PhysicalAddress:State", "Home");

		// Token: 0x04000BA4 RID: 2980
		public static readonly IndexedPropertyDefinition HomeAddressCountryOrRegion = new IndexedPropertyDefinition("contacts:PhysicalAddress:CountryOrRegion", "Home");

		// Token: 0x04000BA5 RID: 2981
		public static readonly IndexedPropertyDefinition HomeAddressPostalCode = new IndexedPropertyDefinition("contacts:PhysicalAddress:PostalCode", "Home");

		// Token: 0x04000BA6 RID: 2982
		public static readonly IndexedPropertyDefinition OtherAddressStreet = new IndexedPropertyDefinition("contacts:PhysicalAddress:Street", "Other");

		// Token: 0x04000BA7 RID: 2983
		public static readonly IndexedPropertyDefinition OtherAddressCity = new IndexedPropertyDefinition("contacts:PhysicalAddress:City", "Other");

		// Token: 0x04000BA8 RID: 2984
		public static readonly IndexedPropertyDefinition OtherAddressState = new IndexedPropertyDefinition("contacts:PhysicalAddress:State", "Other");

		// Token: 0x04000BA9 RID: 2985
		public static readonly IndexedPropertyDefinition OtherAddressCountryOrRegion = new IndexedPropertyDefinition("contacts:PhysicalAddress:CountryOrRegion", "Other");

		// Token: 0x04000BAA RID: 2986
		public static readonly IndexedPropertyDefinition OtherAddressPostalCode = new IndexedPropertyDefinition("contacts:PhysicalAddress:PostalCode", "Other");

		// Token: 0x04000BAB RID: 2987
		internal new static readonly ContactSchema Instance = new ContactSchema();

		// Token: 0x020001B8 RID: 440
		private static class FieldUris
		{
			// Token: 0x04000BB7 RID: 2999
			public const string FileAs = "contacts:FileAs";

			// Token: 0x04000BB8 RID: 3000
			public const string FileAsMapping = "contacts:FileAsMapping";

			// Token: 0x04000BB9 RID: 3001
			public const string DisplayName = "contacts:DisplayName";

			// Token: 0x04000BBA RID: 3002
			public const string GivenName = "contacts:GivenName";

			// Token: 0x04000BBB RID: 3003
			public const string Initials = "contacts:Initials";

			// Token: 0x04000BBC RID: 3004
			public const string MiddleName = "contacts:MiddleName";

			// Token: 0x04000BBD RID: 3005
			public const string NickName = "contacts:Nickname";

			// Token: 0x04000BBE RID: 3006
			public const string CompleteName = "contacts:CompleteName";

			// Token: 0x04000BBF RID: 3007
			public const string CompanyName = "contacts:CompanyName";

			// Token: 0x04000BC0 RID: 3008
			public const string EmailAddress = "contacts:EmailAddress";

			// Token: 0x04000BC1 RID: 3009
			public const string EmailAddresses = "contacts:EmailAddresses";

			// Token: 0x04000BC2 RID: 3010
			public const string PhysicalAddresses = "contacts:PhysicalAddresses";

			// Token: 0x04000BC3 RID: 3011
			public const string PhoneNumber = "contacts:PhoneNumber";

			// Token: 0x04000BC4 RID: 3012
			public const string PhoneNumbers = "contacts:PhoneNumbers";

			// Token: 0x04000BC5 RID: 3013
			public const string AssistantName = "contacts:AssistantName";

			// Token: 0x04000BC6 RID: 3014
			public const string Birthday = "contacts:Birthday";

			// Token: 0x04000BC7 RID: 3015
			public const string BusinessHomePage = "contacts:BusinessHomePage";

			// Token: 0x04000BC8 RID: 3016
			public const string Children = "contacts:Children";

			// Token: 0x04000BC9 RID: 3017
			public const string Companies = "contacts:Companies";

			// Token: 0x04000BCA RID: 3018
			public const string ContactSource = "contacts:ContactSource";

			// Token: 0x04000BCB RID: 3019
			public const string Department = "contacts:Department";

			// Token: 0x04000BCC RID: 3020
			public const string Generation = "contacts:Generation";

			// Token: 0x04000BCD RID: 3021
			public const string ImAddress = "contacts:ImAddress";

			// Token: 0x04000BCE RID: 3022
			public const string ImAddresses = "contacts:ImAddresses";

			// Token: 0x04000BCF RID: 3023
			public const string JobTitle = "contacts:JobTitle";

			// Token: 0x04000BD0 RID: 3024
			public const string Manager = "contacts:Manager";

			// Token: 0x04000BD1 RID: 3025
			public const string Mileage = "contacts:Mileage";

			// Token: 0x04000BD2 RID: 3026
			public const string OfficeLocation = "contacts:OfficeLocation";

			// Token: 0x04000BD3 RID: 3027
			public const string PhysicalAddressCity = "contacts:PhysicalAddress:City";

			// Token: 0x04000BD4 RID: 3028
			public const string PhysicalAddressCountryOrRegion = "contacts:PhysicalAddress:CountryOrRegion";

			// Token: 0x04000BD5 RID: 3029
			public const string PhysicalAddressState = "contacts:PhysicalAddress:State";

			// Token: 0x04000BD6 RID: 3030
			public const string PhysicalAddressStreet = "contacts:PhysicalAddress:Street";

			// Token: 0x04000BD7 RID: 3031
			public const string PhysicalAddressPostalCode = "contacts:PhysicalAddress:PostalCode";

			// Token: 0x04000BD8 RID: 3032
			public const string PostalAddressIndex = "contacts:PostalAddressIndex";

			// Token: 0x04000BD9 RID: 3033
			public const string Profession = "contacts:Profession";

			// Token: 0x04000BDA RID: 3034
			public const string SpouseName = "contacts:SpouseName";

			// Token: 0x04000BDB RID: 3035
			public const string Surname = "contacts:Surname";

			// Token: 0x04000BDC RID: 3036
			public const string WeddingAnniversary = "contacts:WeddingAnniversary";

			// Token: 0x04000BDD RID: 3037
			public const string HasPicture = "contacts:HasPicture";

			// Token: 0x04000BDE RID: 3038
			public const string PhoneticFullName = "contacts:PhoneticFullName";

			// Token: 0x04000BDF RID: 3039
			public const string PhoneticFirstName = "contacts:PhoneticFirstName";

			// Token: 0x04000BE0 RID: 3040
			public const string PhoneticLastName = "contacts:PhoneticLastName";

			// Token: 0x04000BE1 RID: 3041
			public const string Alias = "contacts:Alias";

			// Token: 0x04000BE2 RID: 3042
			public const string Notes = "contacts:Notes";

			// Token: 0x04000BE3 RID: 3043
			public const string Photo = "contacts:Photo";

			// Token: 0x04000BE4 RID: 3044
			public const string UserSMIMECertificate = "contacts:UserSMIMECertificate";

			// Token: 0x04000BE5 RID: 3045
			public const string MSExchangeCertificate = "contacts:MSExchangeCertificate";

			// Token: 0x04000BE6 RID: 3046
			public const string DirectoryId = "contacts:DirectoryId";

			// Token: 0x04000BE7 RID: 3047
			public const string ManagerMailbox = "contacts:ManagerMailbox";

			// Token: 0x04000BE8 RID: 3048
			public const string DirectReports = "contacts:DirectReports";
		}
	}
}
