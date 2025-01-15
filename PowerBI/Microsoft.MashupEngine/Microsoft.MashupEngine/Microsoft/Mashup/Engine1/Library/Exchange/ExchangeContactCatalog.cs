using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BDF RID: 3039
	internal class ExchangeContactCatalog : ExchangeCatalog
	{
		// Token: 0x060052DE RID: 21214 RVA: 0x00117F84 File Offset: 0x00116184
		public ExchangeContactCatalog(ExchangeVersion exchangeVersion)
			: base(exchangeVersion, ExchangeContactCatalog.ItemSchemaType, "IPM.Contact", "IPF.Contact")
		{
		}

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x060052DF RID: 21215 RVA: 0x00117F9C File Offset: 0x0011619C
		protected override PropertyDefinitionBase[] TopLevelPropertiesAllowedList
		{
			get
			{
				if (this.topLevelPropertiesAllowedList == null)
				{
					this.topLevelPropertiesAllowedList = new PropertyDefinitionBase[]
					{
						ContactSchema.DisplayName,
						ContactSchema.Surname,
						ContactSchema.GivenName,
						ContactSchema.MiddleName,
						ContactSchema.CompanyName,
						ContactSchema.JobTitle,
						ContactSchema.FileAs,
						ContactSchema.OfficeLocation,
						ContactSchema.EmailAddresses,
						ContactSchema.ImAddresses,
						ContactSchema.PhoneNumbers,
						ItemSchema.Categories,
						ItemSchema.HasAttachments,
						ItemSchema.Attachments,
						ContactSchema.Notes,
						ItemSchema.Preview
					};
				}
				return this.topLevelPropertiesAllowedList;
			}
		}

		// Token: 0x04002DB7 RID: 11703
		public const string ItemClass = "IPM.Contact";

		// Token: 0x04002DB8 RID: 11704
		public const string FolderClass = "IPF.Contact";

		// Token: 0x04002DB9 RID: 11705
		public static readonly Type ItemSchemaType = typeof(ContactSchema);
	}
}
