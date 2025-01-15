using System;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000248 RID: 584
	internal class MemoryThenFileStream : MemoryUntilThresholdStream, IDisposable
	{
		// Token: 0x06001542 RID: 5442 RVA: 0x00054634 File Offset: 0x00052834
		public static MemoryThenFileStream LoadFromPartition(string fileName)
		{
			Stream file = Global.PartitionManager.GetFile(fileName);
			if (file != null)
			{
				return new MemoryThenFileStream(file, Global.ResponseBufferSizeBytes);
			}
			return null;
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0005465D File Offset: 0x0005285D
		public MemoryThenFileStream(CanWriteFileStream writeOnClose)
			: this(writeOnClose, Global.ResponseBufferSizeBytes, Global.PartitionManager)
		{
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00054670 File Offset: 0x00052870
		private MemoryThenFileStream(CanWriteFileStream writeOnClose, int threshold, PartitionManager partitionManager)
			: this(threshold, partitionManager)
		{
			this.m_writeOnClose = writeOnClose;
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00054681 File Offset: 0x00052881
		public MemoryThenFileStream()
			: this(Global.ResponseBufferSizeBytes, Global.PartitionManager)
		{
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00054693 File Offset: 0x00052893
		private MemoryThenFileStream(int threshold, PartitionManager partitionManager)
			: base(threshold)
		{
			this.m_partitionManager = partitionManager;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x000546B8 File Offset: 0x000528B8
		private MemoryThenFileStream(Stream partitionStream, int threshold)
			: this(threshold, Global.PartitionManager)
		{
			this.m_thresholdReached = true;
			this.m_bufferStream = partitionStream;
			this.m_ownsFile = false;
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x000546DC File Offset: 0x000528DC
		~MemoryThenFileStream()
		{
			try
			{
				this.Dispose(false);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00054718 File Offset: 0x00052918
		public void CloseFileStream()
		{
			this.CloseBufferStream();
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00054720 File Offset: 0x00052920
		protected override void ThresholdReached()
		{
			long position = this.m_bufferStream.Position;
			PartitionFileStream partitionFileStream = this.m_partitionManager.CreateFile(this.m_bufferStream, false);
			partitionFileStream.Flush();
			base.ThresholdReached();
			this.m_bufferStream = partitionFileStream;
			this.m_bufferStream.Position = position;
			GC.ReRegisterForFinalize(this);
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00054771 File Offset: 0x00052971
		public override long Length
		{
			get
			{
				if (this.m_savedLength >= 0L)
				{
					return this.m_savedLength;
				}
				return base.Length;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0005478C File Offset: 0x0005298C
		public string FileName
		{
			get
			{
				string text = null;
				if (this.m_thresholdReached)
				{
					text = ((PartitionFileStream)this.m_bufferStream).FullFileName;
				}
				return text;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x000547B8 File Offset: 0x000529B8
		public MemoryStream CachedBytesStream
		{
			get
			{
				MemoryStream memoryStream = null;
				if (this.m_cachedBytes != null)
				{
					memoryStream = this.m_cachedBytes.NewMemoryStream();
				}
				return memoryStream;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x000547DC File Offset: 0x000529DC
		public bool OwnsFile
		{
			get
			{
				return this.m_ownsFile;
			}
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x000547E4 File Offset: 0x000529E4
		public void ReleaseFileOwnerShip()
		{
			this.m_ownsFile = false;
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x000547F0 File Offset: 0x000529F0
		public void ReopenFileReadOnly()
		{
			if (this.m_thresholdReached)
			{
				PartitionFileStream partitionFileStream = this.m_bufferStream as PartitionFileStream;
				RSTrace.CatalogTrace.Assert(partitionFileStream != null, "fileStream");
				RSTrace.CatalogTrace.Assert(!partitionFileStream.IsDeleteOnClose, "!fileStream.IsDeleteOnClose");
				if (partitionFileStream.CanWrite)
				{
					string fileName = partitionFileStream.FileName;
					partitionFileStream.Close();
					this.m_bufferStream = Global.PartitionManager.GetFile(fileName);
					RSTrace.CatalogTrace.Assert(this.m_bufferStream != null, "m_bufferStream");
					RSTrace.CatalogTrace.Assert(!this.m_bufferStream.CanWrite, "!m_bufferStream.CanWrite");
				}
			}
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0005489C File Offset: 0x00052A9C
		private void FinallyWriteTo()
		{
			if (this.m_writeOnClose == null)
			{
				return;
			}
			try
			{
				if (this.m_thresholdReached)
				{
					this.CloseFileStream();
					RevertImpersonationContext.RunFromRestrictedCasContext(delegate
					{
						new FileIOPermission(PermissionState.Unrestricted).Assert();
						this.m_writeOnClose.WriteFile(((PartitionFileStream)this.m_bufferStream).FullFileName);
					});
				}
				else
				{
					((MemoryStream)this.m_bufferStream).WriteTo(this.m_writeOnClose);
				}
				RSTrace.CatalogTrace.Assert(this.m_writeOnClose != this.m_bufferStream, "m_writeOnClose != m_bufferStream");
				this.m_writeOnClose.Close();
			}
			finally
			{
				this.m_writeOnClose = null;
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00054930 File Offset: 0x00052B30
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				try
				{
					if (disposing)
					{
						this.FinallyWriteTo();
						this.CloseBufferStream();
						GC.SuppressFinalize(this);
					}
					this.DeleteFileIfOwner(disposing);
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
			this.m_disposed = true;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00054984 File Offset: 0x00052B84
		private void CloseBufferStream()
		{
			if (!this.m_closed && this.m_savedLength < 0L)
			{
				RSTrace.CatalogTrace.Assert(this.m_bufferStream != null);
				this.m_savedLength = this.m_bufferStream.Length;
				this.m_bufferStream.Close();
			}
			RSTrace.CatalogTrace.Assert(this.m_savedLength >= 0L);
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x000549EC File Offset: 0x00052BEC
		private void DeleteFileIfOwner(bool disposing)
		{
			if (!this.m_thresholdReached)
			{
				return;
			}
			if (!this.OwnsFile)
			{
				if (disposing && RSTrace.CatalogTrace.TraceVerbose)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "File '{0}' is not owned by this stream and will not be deleted.", new object[] { this.FileName });
				}
				return;
			}
			if (disposing && RSTrace.CatalogTrace.TraceVerbose)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Attempting deletion of file '{0}'", new object[] { this.FileName });
			}
			if (this.m_bufferStream != null)
			{
				this.m_bufferStream.Close();
				this.m_partitionManager.DeleteFile((PartitionFileStream)this.m_bufferStream);
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00005BF2 File Offset: 0x00003DF2
		[Conditional("DEBUG")]
		private void CaptureAllocatingStack()
		{
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00054A94 File Offset: 0x00052C94
		[Conditional("DEBUG")]
		private void DisplayAllocatingStack()
		{
			StackTrace stackTrace = null;
			if (stackTrace != null && RSTrace.CatalogTrace.TraceWarning)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Warning, "MemoryThenFileStream allocating stack: " + stackTrace.ToString());
			}
		}

		// Token: 0x040007BE RID: 1982
		private CanWriteFileStream m_writeOnClose;

		// Token: 0x040007BF RID: 1983
		private CachedMemory m_cachedBytes;

		// Token: 0x040007C0 RID: 1984
		private bool m_disposed;

		// Token: 0x040007C1 RID: 1985
		private bool m_ownsFile = true;

		// Token: 0x040007C2 RID: 1986
		private PartitionManager m_partitionManager;

		// Token: 0x040007C3 RID: 1987
		private long m_savedLength = -1L;
	}
}
