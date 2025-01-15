using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000C05 RID: 3077
	internal class ExchangeTaskCatalog : ExchangeCatalog
	{
		// Token: 0x060053BB RID: 21435 RVA: 0x0011C2BB File Offset: 0x0011A4BB
		public ExchangeTaskCatalog(ExchangeVersion exchangeVersion)
			: base(exchangeVersion, ExchangeTaskCatalog.ItemSchemaType, "IPM.Task", "IPF.Task")
		{
		}

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x060053BC RID: 21436 RVA: 0x0011C2D4 File Offset: 0x0011A4D4
		protected override PropertyDefinitionBase[] TopLevelPropertiesAllowedList
		{
			get
			{
				if (this.topLevelPropertiesAllowedList == null)
				{
					this.topLevelPropertiesAllowedList = new PropertyDefinitionBase[]
					{
						ItemSchema.Subject,
						TaskSchema.StartDate,
						TaskSchema.DueDate,
						ItemSchema.IsReminderSet,
						ItemSchema.ReminderDueBy,
						ItemSchema.ReminderMinutesBeforeStart,
						TaskSchema.Status,
						TaskSchema.PercentComplete,
						TaskSchema.Owner,
						TaskSchema.IsRecurring,
						TaskSchema.Recurrence,
						TaskSchema.IsComplete,
						ItemSchema.Importance,
						ItemSchema.Categories,
						ItemSchema.HasAttachments,
						ItemSchema.Attachments,
						ItemSchema.Preview
					};
				}
				return this.topLevelPropertiesAllowedList;
			}
		}

		// Token: 0x04002E61 RID: 11873
		public const string ItemClass = "IPM.Task";

		// Token: 0x04002E62 RID: 11874
		public const string FolderClass = "IPF.Task";

		// Token: 0x04002E63 RID: 11875
		public static readonly Type ItemSchemaType = typeof(TaskSchema);
	}
}
