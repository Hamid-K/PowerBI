using System;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000292 RID: 658
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal sealed class FileChunkReadStream : SnapshotChunkStreamBase
	{
		// Token: 0x0600180B RID: 6155 RVA: 0x00061DF4 File Offset: 0x0005FFF4
		internal FileChunkReadStream(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType)
			: base(snapshotDataID, isPermanentSnapshot, chunkName, chunkType)
		{
			RSTrace.ChunkTracer.Assert(!isPermanentSnapshot, "cannot read permanent chunnk from filesystem");
			for (;;)
			{
				TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, 100);
				TimeSpan timeSpan2 = new TimeSpan(0L);
				try
				{
					this.m_stream = Global.PartitionManager.GetFile(this.FolderName, this.FileName);
				}
				catch (IOException ex)
				{
					if (RSTrace.ChunkTracer.TraceInfo)
					{
						RSTrace.ChunkTracer.Trace(TraceLevel.Info, "Error getting snapshot file, sleeping to try again. Error = {0}", new object[] { ex.Message });
					}
					Thread.Sleep(timeSpan);
					timeSpan2 += timeSpan;
					bool flag = false;
					if (Globals.Configuration.DBQueryTimeout == 0)
					{
						if (timeSpan2 > new TimeSpan(0, 0, 5, 0, 0))
						{
							flag = true;
						}
					}
					else if (timeSpan2.TotalSeconds > (double)Globals.Configuration.DBQueryTimeout)
					{
						flag = true;
					}
					if (flag)
					{
						if (RSTrace.ChunkTracer.TraceInfo)
						{
							RSTrace.ChunkTracer.Trace(TraceLevel.Info, "Timout reached waiting for a snapshot file");
						}
						throw ex;
					}
					continue;
				}
				break;
			}
			if (this.m_stream == null)
			{
				this.m_IsAvailable = false;
				return;
			}
			this.m_IsAvailable = true;
			this.m_isOpen = true;
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00061F20 File Offset: 0x00060120
		public static bool DoesChunkExist(Guid snapshotDataID, string chunkName)
		{
			return Global.PartitionManager.Exists(snapshotDataID.ToString(), chunkName);
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0006185B File Offset: 0x0005FA5B
		private string FileName
		{
			get
			{
				return this.m_chunkName;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00061F3A File Offset: 0x0006013A
		private string FolderName
		{
			get
			{
				return this.m_snapshotDataID.ToString();
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x00061F4D File Offset: 0x0006014D
		public override long Length
		{
			get
			{
				return this.m_stream.Length;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x00061F5A File Offset: 0x0006015A
		public override bool CanRead
		{
			get
			{
				return this.m_stream.CanRead;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x00061F67 File Offset: 0x00060167
		public override bool CanSeek
		{
			get
			{
				return this.m_stream.CanSeek;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00005BEF File Offset: 0x00003DEF
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00061DDA File Offset: 0x0005FFDA
		public override void Flush()
		{
			throw new InternalCatalogException("Flush not supported");
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x00061F74 File Offset: 0x00060174
		// (set) Token: 0x06001815 RID: 6165 RVA: 0x00061185 File Offset: 0x0005F385
		public override long Position
		{
			get
			{
				return this.m_stream.Position;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00061F81 File Offset: 0x00060181
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_stream.Read(buffer, offset, count);
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00061828 File Offset: 0x0005FA28
		public override void SetLength(long value)
		{
			throw new InternalCatalogException("SetLength not supported");
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00061828 File Offset: 0x0005FA28
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InternalCatalogException("SetLength not supported");
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00061F91 File Offset: 0x00060191
		public override void Close()
		{
			base.Close();
			this.m_isOpen = false;
			this.m_stream.Close();
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00061FAB File Offset: 0x000601AB
		private int RandomAccessRead(long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			if (!this.m_isOpen)
			{
				throw new InternalCatalogException("Stream is already closed and can't be accessed.");
			}
			this.Seek(dataIndex, SeekOrigin.Begin);
			return this.m_stream.Read(buffer, bufferIndex, length);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00061FD8 File Offset: 0x000601D8
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_stream.Seek(offset, origin);
		}

		// Token: 0x040008AA RID: 2218
		private PartitionFileStream m_stream;

		// Token: 0x040008AB RID: 2219
		private bool m_isOpen;
	}
}
