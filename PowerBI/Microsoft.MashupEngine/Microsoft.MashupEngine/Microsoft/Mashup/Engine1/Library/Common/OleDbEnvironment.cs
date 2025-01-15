using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010F6 RID: 4342
	internal abstract class OleDbEnvironment : DbEnvironment, IDisposable
	{
		// Token: 0x06007189 RID: 29065 RVA: 0x001864E0 File Offset: 0x001846E0
		protected OleDbEnvironment(IEngineHost host, IResource resource, string dataSourceName, string sourcePath, bool isTempFile, Value options)
			: base(host, resource, dataSourceName, sourcePath, null, options, null, null)
		{
			this.sourcePath = sourcePath;
			this.isTempFile = isTempFile;
		}

		// Token: 0x0600718A RID: 29066 RVA: 0x00186510 File Offset: 0x00184710
		~OleDbEnvironment()
		{
			this.Dispose(false);
		}

		// Token: 0x17001FDC RID: 8156
		// (get) Token: 0x0600718B RID: 29067 RVA: 0x00186540 File Offset: 0x00184740
		public string SourcePath
		{
			get
			{
				return this.sourcePath;
			}
		}

		// Token: 0x17001FDD RID: 8157
		// (get) Token: 0x0600718C RID: 29068 RVA: 0x00186548 File Offset: 0x00184748
		protected bool IsTempFile
		{
			get
			{
				return this.isTempFile;
			}
		}

		// Token: 0x0600718D RID: 29069 RVA: 0x00186550 File Offset: 0x00184750
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600718E RID: 29070 RVA: 0x0018655F File Offset: 0x0018475F
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (this.isTempFile)
				{
					FileOperations.TryDelete(base.Host, this.sourcePath, this.Resource);
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600718F RID: 29071 RVA: 0x00002105 File Offset: 0x00000305
		public override bool SupportsSkip(TableTypeValue type)
		{
			return false;
		}

		// Token: 0x06007190 RID: 29072 RVA: 0x00002139 File Offset: 0x00000339
		public override bool SupportsTake(TableTypeValue type)
		{
			return true;
		}

		// Token: 0x04003ECB RID: 16075
		private bool disposed;

		// Token: 0x04003ECC RID: 16076
		private readonly bool isTempFile;

		// Token: 0x04003ECD RID: 16077
		private readonly string sourcePath;
	}
}
