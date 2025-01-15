using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000198 RID: 408
	[ServiceObjectDefinition("DistributionList")]
	public class ContactGroup : Item
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x000354AA File Offset: 0x000344AA
		public ContactGroup(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000354B3 File Offset: 0x000344B3
		internal ContactGroup(ItemAttachment parentAttachment)
			: base(parentAttachment)
		{
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x000354BC File Offset: 0x000344BC
		[RequiredServerVersion(ExchangeVersion.Exchange2010)]
		public string FileAs
		{
			get
			{
				return (string)base.PropertyBag[ContactSchema.FileAs];
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x000354D3 File Offset: 0x000344D3
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x000354EA File Offset: 0x000344EA
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

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x000354FD File Offset: 0x000344FD
		[RequiredServerVersion(ExchangeVersion.Exchange2010)]
		public GroupMemberCollection Members
		{
			get
			{
				return (GroupMemberCollection)base.PropertyBag[ContactGroupSchema.Members];
			}
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00035514 File Offset: 0x00034514
		public new static ContactGroup Bind(ExchangeService service, ItemId id, PropertySet propertySet)
		{
			return service.BindToItem<ContactGroup>(id, propertySet);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0003551E File Offset: 0x0003451E
		public new static ContactGroup Bind(ExchangeService service, ItemId id)
		{
			return ContactGroup.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0003552C File Offset: 0x0003452C
		internal override ServiceObjectSchema GetSchema()
		{
			return ContactGroupSchema.Instance;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00035533 File Offset: 0x00034533
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00035536 File Offset: 0x00034536
		internal override void SetSubject(string subject)
		{
			throw new ServiceObjectPropertyException(Strings.PropertyIsReadOnly, ItemSchema.Subject);
		}
	}
}
