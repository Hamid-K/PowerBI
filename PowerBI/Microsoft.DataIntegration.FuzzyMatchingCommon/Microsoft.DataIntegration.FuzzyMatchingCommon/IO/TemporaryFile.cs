using System;
using System.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.IO
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	public class TemporaryFile : IDisposable
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00011188 File Offset: 0x0000F388
		public TemporaryFile()
			: this(false)
		{
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00011191 File Offset: 0x0000F391
		public TemporaryFile(bool shortLived)
		{
			this.m_path = TemporaryFile.CreateTemporaryFile(shortLived);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000111A5 File Offset: 0x0000F3A5
		public string Path
		{
			get
			{
				return this.m_path;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000111AD File Offset: 0x0000F3AD
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000111B5 File Offset: 0x0000F3B5
		public bool Keep
		{
			get
			{
				return this.m_keep;
			}
			set
			{
				this.m_keep = value;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000111BE File Offset: 0x0000F3BE
		public void Dispose()
		{
			this.Dispose(false);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000111C7 File Offset: 0x0000F3C7
		protected void Dispose(bool disposing)
		{
			if (!this.m_isDisposed)
			{
				this.m_isDisposed = true;
				if (!this.m_keep)
				{
					this.TryDelete();
				}
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000111E8 File Offset: 0x0000F3E8
		private void TryDelete()
		{
			try
			{
				File.Delete(this.m_path);
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00011224 File Offset: 0x0000F424
		public static string CreateTemporaryFile(bool shortLived)
		{
			string tempFileName = global::System.IO.Path.GetTempFileName();
			if (shortLived)
			{
				File.SetAttributes(tempFileName, File.GetAttributes(tempFileName) | 256);
			}
			return tempFileName;
		}

		// Token: 0x04000034 RID: 52
		private bool m_isDisposed;

		// Token: 0x04000035 RID: 53
		private bool m_keep;

		// Token: 0x04000036 RID: 54
		private string m_path;
	}
}
