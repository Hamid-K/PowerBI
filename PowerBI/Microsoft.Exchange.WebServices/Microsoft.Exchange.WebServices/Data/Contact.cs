using System;
using System.IO;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000197 RID: 407
	[Attachable]
	[ServiceObjectDefinition("Contact")]
	public class Contact : Item
	{
		// Token: 0x060012B6 RID: 4790 RVA: 0x00034C92 File Offset: 0x00033C92
		public Contact(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00034C9B File Offset: 0x00033C9B
		internal Contact(ItemAttachment parentAttachment)
			: base(parentAttachment)
		{
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00034CA4 File Offset: 0x00033CA4
		public new static Contact Bind(ExchangeService service, ItemId id, PropertySet propertySet)
		{
			return service.BindToItem<Contact>(id, propertySet);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00034CAE File Offset: 0x00033CAE
		public new static Contact Bind(ExchangeService service, ItemId id)
		{
			return Contact.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00034CBC File Offset: 0x00033CBC
		internal override ServiceObjectSchema GetSchema()
		{
			return ContactSchema.Instance;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00034CC3 File Offset: 0x00033CC3
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00034CC8 File Offset: 0x00033CC8
		public void SetContactPicture(byte[] content)
		{
			EwsUtilities.ValidateMethodVersion(base.Service, ExchangeVersion.Exchange2010, "SetContactPicture");
			this.InternalRemoveContactPicture();
			FileAttachment fileAttachment = base.Attachments.AddFileAttachment("ContactPicture.jpg", content);
			fileAttachment.IsContactPhoto = true;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00034D08 File Offset: 0x00033D08
		public void SetContactPicture(Stream contentStream)
		{
			EwsUtilities.ValidateMethodVersion(base.Service, ExchangeVersion.Exchange2010, "SetContactPicture");
			this.InternalRemoveContactPicture();
			FileAttachment fileAttachment = base.Attachments.AddFileAttachment("ContactPicture.jpg", contentStream);
			fileAttachment.IsContactPhoto = true;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00034D48 File Offset: 0x00033D48
		public void SetContactPicture(string fileName)
		{
			EwsUtilities.ValidateMethodVersion(base.Service, ExchangeVersion.Exchange2010, "SetContactPicture");
			this.InternalRemoveContactPicture();
			FileAttachment fileAttachment = base.Attachments.AddFileAttachment(Path.GetFileName(fileName), fileName);
			fileAttachment.IsContactPhoto = true;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00034D88 File Offset: 0x00033D88
		public FileAttachment GetContactPictureAttachment()
		{
			EwsUtilities.ValidateMethodVersion(base.Service, ExchangeVersion.Exchange2010, "GetContactPictureAttachment");
			if (!base.PropertyBag.IsPropertyLoaded(ItemSchema.Attachments))
			{
				throw new PropertyException(Strings.AttachmentCollectionNotLoaded);
			}
			foreach (Attachment attachment in base.Attachments)
			{
				FileAttachment fileAttachment = (FileAttachment)attachment;
				if (fileAttachment.IsContactPhoto)
				{
					return fileAttachment;
				}
			}
			return null;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00034E18 File Offset: 0x00033E18
		private void InternalRemoveContactPicture()
		{
			for (int i = base.Attachments.Count - 1; i >= 0; i--)
			{
				FileAttachment fileAttachment = base.Attachments[i] as FileAttachment;
				if (fileAttachment != null && fileAttachment.IsContactPhoto)
				{
					base.Attachments.Remove(fileAttachment);
				}
			}
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00034E67 File Offset: 0x00033E67
		public void RemoveContactPicture()
		{
			EwsUtilities.ValidateMethodVersion(base.Service, ExchangeVersion.Exchange2010, "RemoveContactPicture");
			if (!base.PropertyBag.IsPropertyLoaded(ItemSchema.Attachments))
			{
				throw new PropertyException(Strings.AttachmentCollectionNotLoaded);
			}
			this.InternalRemoveContactPicture();
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00034EA4 File Offset: 0x00033EA4
		internal override void Validate()
		{
			base.Validate();
			object obj;
			if (base.TryGetProperty(ContactSchema.FileAsMapping, out obj))
			{
				EwsUtilities.ValidateEnumVersionValue((FileAsMapping)obj, base.Service.RequestedServerVersion);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00034EE1 File Offset: 0x00033EE1
		// (set) Token: 0x060012C4 RID: 4804 RVA: 0x00034EF8 File Offset: 0x00033EF8
		public string FileAs
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.FileAs];
			}
			set
			{
				base.PropertyBag[ContactSchema.FileAs] = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x00034F0B File Offset: 0x00033F0B
		// (set) Token: 0x060012C6 RID: 4806 RVA: 0x00034F22 File Offset: 0x00033F22
		public FileAsMapping FileAsMapping
		{
			get
			{
				return (FileAsMapping)base.PropertyBag[ContactSchema.FileAsMapping];
			}
			set
			{
				base.PropertyBag[ContactSchema.FileAsMapping] = value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x00034F3A File Offset: 0x00033F3A
		// (set) Token: 0x060012C8 RID: 4808 RVA: 0x00034F51 File Offset: 0x00033F51
		public string DisplayName
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.DisplayName];
			}
			set
			{
				base.PropertyBag[ContactSchema.DisplayName] = value;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00034F64 File Offset: 0x00033F64
		// (set) Token: 0x060012CA RID: 4810 RVA: 0x00034F7B File Offset: 0x00033F7B
		public string GivenName
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.GivenName];
			}
			set
			{
				base.PropertyBag[ContactSchema.GivenName] = value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00034F8E File Offset: 0x00033F8E
		// (set) Token: 0x060012CC RID: 4812 RVA: 0x00034FA5 File Offset: 0x00033FA5
		public string Initials
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.Initials];
			}
			set
			{
				base.PropertyBag[ContactSchema.Initials] = value;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x00034FB8 File Offset: 0x00033FB8
		// (set) Token: 0x060012CE RID: 4814 RVA: 0x00034FCF File Offset: 0x00033FCF
		public string MiddleName
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.MiddleName];
			}
			set
			{
				base.PropertyBag[ContactSchema.MiddleName] = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x00034FE2 File Offset: 0x00033FE2
		// (set) Token: 0x060012D0 RID: 4816 RVA: 0x00034FF9 File Offset: 0x00033FF9
		public string NickName
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.NickName];
			}
			set
			{
				base.PropertyBag[ContactSchema.NickName] = value;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0003500C File Offset: 0x0003400C
		public CompleteName CompleteName
		{
			get
			{
				return (CompleteName)base.PropertyBag[ContactSchema.CompleteName];
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00035023 File Offset: 0x00034023
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x0003503A File Offset: 0x0003403A
		public string CompanyName
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.CompanyName];
			}
			set
			{
				base.PropertyBag[ContactSchema.CompanyName] = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0003504D File Offset: 0x0003404D
		public EmailAddressDictionary EmailAddresses
		{
			get
			{
				return (EmailAddressDictionary)base.PropertyBag[ContactSchema.EmailAddresses];
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00035064 File Offset: 0x00034064
		public PhysicalAddressDictionary PhysicalAddresses
		{
			get
			{
				return (PhysicalAddressDictionary)base.PropertyBag[ContactSchema.PhysicalAddresses];
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0003507B File Offset: 0x0003407B
		public PhoneNumberDictionary PhoneNumbers
		{
			get
			{
				return (PhoneNumberDictionary)base.PropertyBag[ContactSchema.PhoneNumbers];
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00035092 File Offset: 0x00034092
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x000350A9 File Offset: 0x000340A9
		public string AssistantName
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.AssistantName];
			}
			set
			{
				base.PropertyBag[ContactSchema.AssistantName] = value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x000350BC File Offset: 0x000340BC
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x000350D3 File Offset: 0x000340D3
		public DateTime? Birthday
		{
			get
			{
				return (DateTime?)base.PropertyBag[ContactSchema.Birthday];
			}
			set
			{
				base.PropertyBag[ContactSchema.Birthday] = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x000350EB File Offset: 0x000340EB
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x00035102 File Offset: 0x00034102
		public string BusinessHomePage
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.BusinessHomePage];
			}
			set
			{
				base.PropertyBag[ContactSchema.BusinessHomePage] = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x00035115 File Offset: 0x00034115
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x0003512C File Offset: 0x0003412C
		public StringList Children
		{
			get
			{
				return (StringList)base.PropertyBag[ContactSchema.Children];
			}
			set
			{
				base.PropertyBag[ContactSchema.Children] = value;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0003513F File Offset: 0x0003413F
		// (set) Token: 0x060012E0 RID: 4832 RVA: 0x00035156 File Offset: 0x00034156
		public StringList Companies
		{
			get
			{
				return (StringList)base.PropertyBag[ContactSchema.Companies];
			}
			set
			{
				base.PropertyBag[ContactSchema.Companies] = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x00035169 File Offset: 0x00034169
		public ContactSource? ContactSource
		{
			get
			{
				return (ContactSource?)base.PropertyBag[ContactSchema.ContactSource];
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00035180 File Offset: 0x00034180
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x00035197 File Offset: 0x00034197
		public string Department
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.Department];
			}
			set
			{
				base.PropertyBag[ContactSchema.Department] = value;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x000351AA File Offset: 0x000341AA
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x000351C1 File Offset: 0x000341C1
		public string Generation
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.Generation];
			}
			set
			{
				base.PropertyBag[ContactSchema.Generation] = value;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x000351D4 File Offset: 0x000341D4
		public ImAddressDictionary ImAddresses
		{
			get
			{
				return (ImAddressDictionary)base.PropertyBag[ContactSchema.ImAddresses];
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x000351EB File Offset: 0x000341EB
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x00035202 File Offset: 0x00034202
		public string JobTitle
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.JobTitle];
			}
			set
			{
				base.PropertyBag[ContactSchema.JobTitle] = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00035215 File Offset: 0x00034215
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x0003522C File Offset: 0x0003422C
		public string Manager
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.Manager];
			}
			set
			{
				base.PropertyBag[ContactSchema.Manager] = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0003523F File Offset: 0x0003423F
		// (set) Token: 0x060012EC RID: 4844 RVA: 0x00035256 File Offset: 0x00034256
		public string Mileage
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.Mileage];
			}
			set
			{
				base.PropertyBag[ContactSchema.Mileage] = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00035269 File Offset: 0x00034269
		// (set) Token: 0x060012EE RID: 4846 RVA: 0x00035280 File Offset: 0x00034280
		public string OfficeLocation
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.OfficeLocation];
			}
			set
			{
				base.PropertyBag[ContactSchema.OfficeLocation] = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x00035293 File Offset: 0x00034293
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x000352AA File Offset: 0x000342AA
		public PhysicalAddressIndex? PostalAddressIndex
		{
			get
			{
				return (PhysicalAddressIndex?)base.PropertyBag[ContactSchema.PostalAddressIndex];
			}
			set
			{
				base.PropertyBag[ContactSchema.PostalAddressIndex] = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x000352C2 File Offset: 0x000342C2
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x000352D9 File Offset: 0x000342D9
		public string Profession
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.Profession];
			}
			set
			{
				base.PropertyBag[ContactSchema.Profession] = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x000352EC File Offset: 0x000342EC
		// (set) Token: 0x060012F4 RID: 4852 RVA: 0x00035303 File Offset: 0x00034303
		public string SpouseName
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.SpouseName];
			}
			set
			{
				base.PropertyBag[ContactSchema.SpouseName] = value;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00035316 File Offset: 0x00034316
		// (set) Token: 0x060012F6 RID: 4854 RVA: 0x0003532D File Offset: 0x0003432D
		public string Surname
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.Surname];
			}
			set
			{
				base.PropertyBag[ContactSchema.Surname] = value;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x00035340 File Offset: 0x00034340
		// (set) Token: 0x060012F8 RID: 4856 RVA: 0x00035357 File Offset: 0x00034357
		public DateTime? WeddingAnniversary
		{
			get
			{
				return (DateTime?)base.PropertyBag[ContactSchema.WeddingAnniversary];
			}
			set
			{
				base.PropertyBag[ContactSchema.WeddingAnniversary] = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0003536F File Offset: 0x0003436F
		public bool HasPicture
		{
			get
			{
				return (bool)base.PropertyBag[ContactSchema.HasPicture];
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00035386 File Offset: 0x00034386
		public string PhoneticFullName
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.PhoneticFullName];
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x0003539D File Offset: 0x0003439D
		public string PhoneticFirstName
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.PhoneticFirstName];
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x000353B4 File Offset: 0x000343B4
		public string PhoneticLastName
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.PhoneticLastName];
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x000353CB File Offset: 0x000343CB
		public string Alias
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.Alias];
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x000353E2 File Offset: 0x000343E2
		public string Notes
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.Notes];
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x000353F9 File Offset: 0x000343F9
		public byte[] DirectoryPhoto
		{
			get
			{
				return (byte[])base.PropertyBag[ContactSchema.Photo];
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00035410 File Offset: 0x00034410
		public byte[][] UserSMIMECertificate
		{
			get
			{
				ByteArrayArray byteArrayArray = (ByteArrayArray)base.PropertyBag[ContactSchema.UserSMIMECertificate];
				return byteArrayArray.Content;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x0003543C File Offset: 0x0003443C
		public byte[][] MSExchangeCertificate
		{
			get
			{
				ByteArrayArray byteArrayArray = (ByteArrayArray)base.PropertyBag[ContactSchema.MSExchangeCertificate];
				return byteArrayArray.Content;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00035465 File Offset: 0x00034465
		public string DirectoryId
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.DirectoryId];
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0003547C File Offset: 0x0003447C
		public EmailAddress ManagerMailbox
		{
			get
			{
				return (EmailAddress)base.PropertyBag[ContactSchema.ManagerMailbox];
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00035493 File Offset: 0x00034493
		public EmailAddressCollection DirectReports
		{
			get
			{
				return (EmailAddressCollection)base.PropertyBag[ContactSchema.DirectReports];
			}
		}

		// Token: 0x04000A09 RID: 2569
		private const string ContactPictureName = "ContactPicture.jpg";
	}
}
