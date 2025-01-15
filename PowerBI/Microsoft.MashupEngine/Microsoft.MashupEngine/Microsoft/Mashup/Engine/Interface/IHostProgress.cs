using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200007E RID: 126
	public interface IHostProgress
	{
		// Token: 0x060001D9 RID: 473
		void StartRequest();

		// Token: 0x060001DA RID: 474
		void StopRequest();

		// Token: 0x060001DB RID: 475
		void RecordRowRead();

		// Token: 0x060001DC RID: 476
		void RecordRowsRead(long rowsRead);

		// Token: 0x060001DD RID: 477
		void RecordBytesRead(long bytesRead);

		// Token: 0x060001DE RID: 478
		void RecordRowWritten();

		// Token: 0x060001DF RID: 479
		void RecordRowsWritten(long rowsWritten);

		// Token: 0x060001E0 RID: 480
		void RecordBytesWritten(long bytesWritten);

		// Token: 0x060001E1 RID: 481
		void RecordPercentComplete(int percent);
	}
}
