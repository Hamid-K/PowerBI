using System;
using System.Collections.Generic;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x0200197B RID: 6523
	public class ProgressRecorder : IRecordProgress, IProgressReader
	{
		// Token: 0x0600A583 RID: 42371 RVA: 0x00224106 File Offset: 0x00222306
		public ProgressRecorder()
		{
			this.syncRoot = new object();
		}

		// Token: 0x0600A584 RID: 42372 RVA: 0x0022411C File Offset: 0x0022231C
		public void RecordProgress(byte[] progressData)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.lastProgress = progressData;
			}
		}

		// Token: 0x0600A585 RID: 42373 RVA: 0x00224160 File Offset: 0x00222360
		public IEnumerable<byte[]> ReadAllProgress()
		{
			object obj = this.syncRoot;
			IEnumerable<byte[]> enumerable;
			lock (obj)
			{
				if (this.lastProgress != null)
				{
					enumerable = new byte[][] { this.lastProgress };
				}
				else
				{
					enumerable = new byte[0][];
				}
			}
			return enumerable;
		}

		// Token: 0x0400562F RID: 22063
		private readonly object syncRoot;

		// Token: 0x04005630 RID: 22064
		private byte[] lastProgress;
	}
}
