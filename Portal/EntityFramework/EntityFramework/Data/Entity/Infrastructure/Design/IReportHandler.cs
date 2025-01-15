using System;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x0200029F RID: 671
	public interface IReportHandler
	{
		// Token: 0x0600216C RID: 8556
		void OnError(string message);

		// Token: 0x0600216D RID: 8557
		void OnWarning(string message);

		// Token: 0x0600216E RID: 8558
		void OnInformation(string message);

		// Token: 0x0600216F RID: 8559
		void OnVerbose(string message);
	}
}
