using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000191 RID: 401
	[ServiceObjectDefinition("ContactsFolder")]
	public class ContactsFolder : Folder
	{
		// Token: 0x060011D4 RID: 4564 RVA: 0x00033749 File Offset: 0x00032749
		public ContactsFolder(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00033752 File Offset: 0x00032752
		public new static ContactsFolder Bind(ExchangeService service, FolderId id, PropertySet propertySet)
		{
			return service.BindToFolder<ContactsFolder>(id, propertySet);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0003375C File Offset: 0x0003275C
		public new static ContactsFolder Bind(ExchangeService service, FolderId id)
		{
			return ContactsFolder.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0003376A File Offset: 0x0003276A
		public new static ContactsFolder Bind(ExchangeService service, WellKnownFolderName name, PropertySet propertySet)
		{
			return ContactsFolder.Bind(service, new FolderId(name), propertySet);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00033779 File Offset: 0x00032779
		public new static ContactsFolder Bind(ExchangeService service, WellKnownFolderName name)
		{
			return ContactsFolder.Bind(service, new FolderId(name), PropertySet.FirstClassProperties);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0003378C File Offset: 0x0003278C
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}
	}
}
