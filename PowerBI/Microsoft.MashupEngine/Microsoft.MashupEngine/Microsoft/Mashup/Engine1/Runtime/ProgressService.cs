using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015D6 RID: 5590
	public static class ProgressService
	{
		// Token: 0x06008C95 RID: 35989 RVA: 0x001D7D19 File Offset: 0x001D5F19
		public static IHostProgress GetNullHostProgress()
		{
			return ProgressService.nullHostProgress;
		}

		// Token: 0x06008C96 RID: 35990 RVA: 0x001D7D20 File Offset: 0x001D5F20
		public static IHostProgress GetHostProgress(IEngineHost host, string resourceKind, string dataSource)
		{
			IProgressService progressService = host.QueryService<IProgressService>();
			if (progressService != null)
			{
				IExtensibilityService extensibilityService = host.QueryService<IExtensibilityService>();
				if (extensibilityService != null && extensibilityService.CurrentResource != null)
				{
					resourceKind = extensibilityService.CurrentResource.Kind;
					dataSource = extensibilityService.CurrentResource.Path;
				}
				return progressService.GetHostProgress(resourceKind, dataSource);
			}
			return ProgressService.nullHostProgress;
		}

		// Token: 0x04004CB9 RID: 19641
		private static readonly ProgressService.DummyHostProgress nullHostProgress = new ProgressService.DummyHostProgress();

		// Token: 0x020015D7 RID: 5591
		private class DummyHostProgress : IHostProgress
		{
			// Token: 0x06008C98 RID: 35992 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordRowRead()
			{
			}

			// Token: 0x06008C99 RID: 35993 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordRowWritten()
			{
			}

			// Token: 0x06008C9A RID: 35994 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordRowsRead(long rowsRead)
			{
			}

			// Token: 0x06008C9B RID: 35995 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordRowsWritten(long rowsWritten)
			{
			}

			// Token: 0x06008C9C RID: 35996 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordBytesRead(long bytesRead)
			{
			}

			// Token: 0x06008C9D RID: 35997 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordBytesWritten(long bytesWritten)
			{
			}

			// Token: 0x06008C9E RID: 35998 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordPercentComplete(int percent)
			{
			}

			// Token: 0x06008C9F RID: 35999 RVA: 0x0000336E File Offset: 0x0000156E
			public void StartRequest()
			{
			}

			// Token: 0x06008CA0 RID: 36000 RVA: 0x0000336E File Offset: 0x0000156E
			public void StopRequest()
			{
			}
		}
	}
}
