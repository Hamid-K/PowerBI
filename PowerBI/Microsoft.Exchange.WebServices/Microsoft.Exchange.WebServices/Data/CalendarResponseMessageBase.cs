using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001A2 RID: 418
	[EditorBrowsable(1)]
	public abstract class CalendarResponseMessageBase<TMessage> : ResponseObject<TMessage> where TMessage : EmailMessage
	{
		// Token: 0x0600142D RID: 5165 RVA: 0x000370CE File Offset: 0x000360CE
		internal CalendarResponseMessageBase(Item referenceItem)
			: base(referenceItem)
		{
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x000370D7 File Offset: 0x000360D7
		public new CalendarActionResults Save(FolderId destinationFolderId)
		{
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			return new CalendarActionResults(base.InternalCreate(destinationFolderId, MessageDisposition.SaveOnly));
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x000370F1 File Offset: 0x000360F1
		public new CalendarActionResults Save(WellKnownFolderName destinationFolderName)
		{
			return new CalendarActionResults(base.InternalCreate(new FolderId(destinationFolderName), MessageDisposition.SaveOnly));
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x00037105 File Offset: 0x00036105
		public new CalendarActionResults Save()
		{
			return new CalendarActionResults(base.InternalCreate(null, MessageDisposition.SaveOnly));
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x00037114 File Offset: 0x00036114
		public new CalendarActionResults Send()
		{
			return new CalendarActionResults(base.InternalCreate(null, MessageDisposition.SendOnly));
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00037123 File Offset: 0x00036123
		public new CalendarActionResults SendAndSaveCopy(FolderId destinationFolderId)
		{
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			return new CalendarActionResults(base.InternalCreate(destinationFolderId, MessageDisposition.SendAndSaveCopy));
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0003713D File Offset: 0x0003613D
		public new CalendarActionResults SendAndSaveCopy(WellKnownFolderName destinationFolderName)
		{
			return new CalendarActionResults(base.InternalCreate(new FolderId(destinationFolderName), MessageDisposition.SendAndSaveCopy));
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00037151 File Offset: 0x00036151
		public new CalendarActionResults SendAndSaveCopy()
		{
			return new CalendarActionResults(base.InternalCreate(null, MessageDisposition.SendAndSaveCopy));
		}
	}
}
