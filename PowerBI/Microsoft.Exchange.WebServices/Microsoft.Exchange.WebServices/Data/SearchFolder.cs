using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000192 RID: 402
	[ServiceObjectDefinition("SearchFolder")]
	public class SearchFolder : Folder
	{
		// Token: 0x060011DA RID: 4570 RVA: 0x0003378F File Offset: 0x0003278F
		public new static SearchFolder Bind(ExchangeService service, FolderId id, PropertySet propertySet)
		{
			return service.BindToFolder<SearchFolder>(id, propertySet);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00033799 File Offset: 0x00032799
		public new static SearchFolder Bind(ExchangeService service, FolderId id)
		{
			return SearchFolder.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000337A7 File Offset: 0x000327A7
		public new static SearchFolder Bind(ExchangeService service, WellKnownFolderName name, PropertySet propertySet)
		{
			return SearchFolder.Bind(service, new FolderId(name), propertySet);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000337B6 File Offset: 0x000327B6
		public new static SearchFolder Bind(ExchangeService service, WellKnownFolderName name)
		{
			return SearchFolder.Bind(service, new FolderId(name), PropertySet.FirstClassProperties);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000337C9 File Offset: 0x000327C9
		public SearchFolder(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000337D2 File Offset: 0x000327D2
		internal override ServiceObjectSchema GetSchema()
		{
			return SearchFolderSchema.Instance;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x000337D9 File Offset: 0x000327D9
		internal override void Validate()
		{
			base.Validate();
			if (this.SearchParameters != null)
			{
				this.SearchParameters.Validate();
			}
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x000337F4 File Offset: 0x000327F4
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x000337F7 File Offset: 0x000327F7
		public SearchFolderParameters SearchParameters
		{
			get
			{
				return (SearchFolderParameters)base.PropertyBag[SearchFolderSchema.SearchParameters];
			}
		}
	}
}
