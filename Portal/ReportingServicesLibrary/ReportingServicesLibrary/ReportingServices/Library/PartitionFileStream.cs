using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000083 RID: 131
	internal class PartitionFileStream : Stream
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x0001689C File Offset: 0x00014A9C
		public PartitionFileStream(FileStream fileStream, PartitionManager manager, bool deleteOnClose)
			: this(manager, deleteOnClose)
		{
			if (fileStream == null)
			{
				throw new ArgumentNullException("fileStream");
			}
			this.m_stream = fileStream;
			this.m_fullFileName = this.m_stream.Name;
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(this.m_fullFileName));
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000168F0 File Offset: 0x00014AF0
		public PartitionFileStream(string path, PartitionManager manager, bool deleteOnClose)
			: this(manager, deleteOnClose)
		{
			FileMode fileMode = FileMode.Open;
			FileAccess fileAccess = FileAccess.Read;
			FileShare fileShare = FileShare.Read;
			this.m_stream = null;
			int num = 10;
			while (this.m_stream == null && num > 1)
			{
				num--;
				SafeFileHandle safeFileHandle;
				int num2 = Native.NativeCreateFile(path, fileMode, fileAccess, fileShare, out safeFileHandle);
				if (num2 != 0)
				{
					if (num2 != 32)
					{
						this.m_stream = new FileStream(path, fileMode, fileAccess, fileShare);
					}
					else
					{
						if (RSTrace.CatalogTrace.TraceWarning)
						{
							string text = string.Format(CultureInfo.InvariantCulture, "Sharing violation trying to access file '{0}', will retry {1} more time(s).", path, num);
							RSTrace.CatalogTrace.Trace(TraceLevel.Warning, text);
						}
						Thread.Sleep(200);
					}
				}
				else
				{
					RSTrace.CatalogTrace.Assert(!safeFileHandle.IsInvalid, "!handle.IsInvalid");
					this.m_stream = new FileStream(safeFileHandle, fileAccess);
				}
			}
			if (this.m_stream == null && num-- == 1)
			{
				this.m_stream = new FileStream(path, fileMode, fileAccess, fileShare);
			}
			RSTrace.CatalogTrace.Assert(this.m_stream != null, "m_stream != null");
			this.m_fullFileName = Path.GetFullPath(path);
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(this.m_fullFileName));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00016A16 File Offset: 0x00014C16
		private PartitionFileStream(PartitionManager manager, bool deleteOnClose)
		{
			this.m_owningManager = manager;
			this.m_deleteOnClose = deleteOnClose;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00016A37 File Offset: 0x00014C37
		public string FullFileName
		{
			get
			{
				return this.m_fullFileName;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00016A3F File Offset: 0x00014C3F
		public string FileName
		{
			get
			{
				return Path.GetFileName(this.FullFileName);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00016A4C File Offset: 0x00014C4C
		public bool IsDeleteOnClose
		{
			get
			{
				return this.m_deleteOnClose;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00016A54 File Offset: 0x00014C54
		public override bool CanRead
		{
			get
			{
				return this.m_stream.CanRead;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00016A61 File Offset: 0x00014C61
		public override bool CanSeek
		{
			get
			{
				return this.m_stream.CanSeek;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00016A6E File Offset: 0x00014C6E
		public override bool CanWrite
		{
			get
			{
				return this.m_stream.CanWrite;
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00016A7C File Offset: 0x00014C7C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.m_stream.Close();
					if (this.m_deleteOnClose && this.m_owningManager != null)
					{
						this.m_owningManager.DeleteFile(this);
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00016AD0 File Offset: 0x00014CD0
		public override void Flush()
		{
			this.m_stream.Flush();
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00016ADD File Offset: 0x00014CDD
		public override long Length
		{
			get
			{
				return this.m_stream.Length;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00016AEA File Offset: 0x00014CEA
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00016AF7 File Offset: 0x00014CF7
		public override long Position
		{
			get
			{
				return this.m_stream.Position;
			}
			set
			{
				this.m_stream.Position = value;
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00016B05 File Offset: 0x00014D05
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_stream.Read(buffer, offset, count);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00016B15 File Offset: 0x00014D15
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_stream.Seek(offset, origin);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00016B24 File Offset: 0x00014D24
		public override void SetLength(long val)
		{
			this.m_stream.SetLength(val);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00016B32 File Offset: 0x00014D32
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_stream.Write(buffer, offset, count);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00016B42 File Offset: 0x00014D42
		public override int ReadByte()
		{
			return this.m_stream.ReadByte();
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00016B4F File Offset: 0x00014D4F
		public override void WriteByte(byte val)
		{
			this.m_stream.WriteByte(val);
		}

		// Token: 0x040002ED RID: 749
		private FileStream m_stream;

		// Token: 0x040002EE RID: 750
		private PartitionManager m_owningManager;

		// Token: 0x040002EF RID: 751
		private bool m_deleteOnClose;

		// Token: 0x040002F0 RID: 752
		private readonly string m_fullFileName = "[Unknown]";
	}
}
