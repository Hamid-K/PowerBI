using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BE0 RID: 3040
	internal class ExchangeEmailMessageCatalog : ExchangeCatalog
	{
		// Token: 0x060052E1 RID: 21217 RVA: 0x0011805F File Offset: 0x0011625F
		public ExchangeEmailMessageCatalog(ExchangeVersion exchangeVersion)
			: base(exchangeVersion, ExchangeEmailMessageCatalog.ItemSchemaType, "IPM.Note", "IPF.Note")
		{
		}

		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x060052E2 RID: 21218 RVA: 0x00118077 File Offset: 0x00116277
		public override SearchFilter BaseItemSearchFilter
		{
			get
			{
				return new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter[]
				{
					base.BaseItemSearchFilter,
					new SearchFilter.IsEqualTo(ItemSchema.ItemClass, "REPORT.IPM.Note.NDR")
				});
			}
		}

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x060052E3 RID: 21219 RVA: 0x001180A0 File Offset: 0x001162A0
		protected override PropertyDefinitionBase[] TopLevelPropertiesAllowedList
		{
			get
			{
				if (this.topLevelPropertiesAllowedList == null)
				{
					this.topLevelPropertiesAllowedList = new PropertyDefinitionBase[]
					{
						ItemSchema.Subject,
						EmailMessageSchema.Sender,
						ItemSchema.DisplayTo,
						ItemSchema.DisplayCc,
						EmailMessageSchema.ToRecipients,
						EmailMessageSchema.CcRecipients,
						EmailMessageSchema.BccRecipients,
						ItemSchema.DateTimeSent,
						ItemSchema.DateTimeReceived,
						ItemSchema.Importance,
						ItemSchema.Categories,
						EmailMessageSchema.IsRead,
						ItemSchema.HasAttachments,
						ItemSchema.Attachments,
						ItemSchema.Preview
					};
				}
				return this.topLevelPropertiesAllowedList;
			}
		}

		// Token: 0x04002DBA RID: 11706
		public const string ItemClass = "IPM.Note";

		// Token: 0x04002DBB RID: 11707
		public const string ItemClass2 = "REPORT.IPM.Note.NDR";

		// Token: 0x04002DBC RID: 11708
		public const string FolderClass = "IPF.Note";

		// Token: 0x04002DBD RID: 11709
		public static readonly Type ItemSchemaType = typeof(EmailMessageSchema);
	}
}
