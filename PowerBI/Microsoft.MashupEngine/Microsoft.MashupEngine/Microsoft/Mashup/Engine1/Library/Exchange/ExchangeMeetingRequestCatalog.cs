using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BE4 RID: 3044
	internal class ExchangeMeetingRequestCatalog : ExchangeCatalog
	{
		// Token: 0x060052FD RID: 21245 RVA: 0x00118C15 File Offset: 0x00116E15
		public ExchangeMeetingRequestCatalog(ExchangeVersion exchangeVersion)
			: base(exchangeVersion, ExchangeMeetingRequestCatalog.ItemSchemaType, "IPM.Schedule.Meeting.Request", "IPF.Note")
		{
		}

		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x060052FE RID: 21246 RVA: 0x00118C2D File Offset: 0x00116E2D
		public override SearchFilter BaseItemSearchFilter
		{
			get
			{
				return new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter[]
				{
					base.BaseItemSearchFilter,
					new SearchFilter.IsEqualTo(ItemSchema.ItemClass, "IPM.Schedule.Meeting.Canceled"),
					new SearchFilter.IsEqualTo(ItemSchema.ItemClass, "IPM.Schedule.Meeting.Resp")
				});
			}
		}

		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x060052FF RID: 21247 RVA: 0x00118C68 File Offset: 0x00116E68
		protected override PropertyDefinitionBase[] TopLevelPropertiesAllowedList
		{
			get
			{
				if (this.topLevelPropertiesAllowedList == null)
				{
					this.topLevelPropertiesAllowedList = new PropertyDefinitionBase[]
					{
						ItemSchema.Subject,
						MeetingRequestSchema.Location,
						MeetingRequestSchema.Start,
						MeetingRequestSchema.End,
						ItemSchema.DisplayTo,
						ItemSchema.DisplayCc,
						MeetingRequestSchema.RequiredAttendees,
						MeetingRequestSchema.OptionalAttendees,
						MeetingRequestSchema.IsAllDayEvent,
						MeetingRequestSchema.IsCancelled,
						MeetingRequestSchema.LegacyFreeBusyStatus,
						ItemSchema.IsReminderSet,
						ItemSchema.ReminderMinutesBeforeStart,
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

		// Token: 0x04002DD1 RID: 11729
		public const string ItemClass1 = "IPM.Schedule.Meeting.Request";

		// Token: 0x04002DD2 RID: 11730
		public const string ItemClass2 = "IPM.Schedule.Meeting.Canceled";

		// Token: 0x04002DD3 RID: 11731
		public const string ItemClass3 = "IPM.Schedule.Meeting.Resp";

		// Token: 0x04002DD4 RID: 11732
		public const string FolderClass = "IPF.Note";

		// Token: 0x04002DD5 RID: 11733
		public static readonly Type ItemSchemaType = typeof(MeetingRequestSchema);
	}
}
