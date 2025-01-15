using System;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000291 RID: 657
	[SqlClientPermission(SecurityAction.Assert, Unrestricted = true, AllowBlankPassword = true)]
	internal sealed class SqlChunkReadStream : SnapshotChunkStreamBase, IHasMimeType
	{
		// Token: 0x060017FD RID: 6141 RVA: 0x00061B55 File Offset: 0x0005FD55
		internal SqlChunkReadStream(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType, ChunkConnectionManager connectionManager, bool isForUpgrade)
			: base(snapshotDataID, isPermanentSnapshot, chunkName, chunkType)
		{
			this.m_isForUpgrade = isForUpgrade;
			this.OpenReadStream(connectionManager);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00061B74 File Offset: 0x0005FD74
		private void OpenReadStream(ChunkConnectionManager connectionManager)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ChunkReadStream - constructor({0}, '{1}', {2})", new object[] { this.m_snapshotDataID, this.m_chunkName, this.m_chunkType });
			}
			this.m_position = 0L;
			this.m_IsAvailable = false;
			this.m_isOpen = false;
			SqlChunkAccess sqlChunkAccess = new SqlChunkAccess(this, connectionManager, this.m_isForUpgrade);
			this.m_isConnectionOwner = connectionManager.IsConnectionOwner;
			if (sqlChunkAccess.OpenExistingChunk(out this.m_chunkPointer, out this.m_chunkLength, out this.m_mimeType, out this.m_chunkFlags, out this.m_version, out this.m_storage))
			{
				this.m_IsAvailable = true;
				this.m_isOpen = true;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00005BEF File Offset: 0x00003DEF
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00061C30 File Offset: 0x0005FE30
		public override void Close()
		{
			if (!this.m_isOpen)
			{
				return;
			}
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ChunkReadStream.Close() - closing... id={0}, name='{1}'", new object[] { this.m_snapshotDataID, this.m_chunkName });
			}
			if (!this.m_isConnectionOwner)
			{
				this.m_isOpen = false;
				return;
			}
			this.m_storage.Commit();
			this.m_isOpen = false;
			this.m_storage.DisconnectStorage();
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ChunkReadStream - Closed! id={0}, name='{1}'", new object[] { this.m_snapshotDataID, this.m_chunkName });
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00061CE5 File Offset: 0x0005FEE5
		public override long Length
		{
			get
			{
				return this.m_chunkLength;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00061CED File Offset: 0x0005FEED
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x00061185 File Offset: 0x0005F385
		public override long Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00061CF8 File Offset: 0x0005FEF8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.m_isOpen)
			{
				throw new InternalCatalogException("Stream is already closed and can't be accessed.");
			}
			if (this.m_position >= this.m_chunkLength)
			{
				return 0;
			}
			long num = this.m_chunkLength - this.m_position;
			if ((long)count > num)
			{
				count = (int)num;
			}
			if (!this.m_storage.ReadChunkPortion(this.m_chunkPointer, this.m_isPermanentSnapshot, this.m_position, buffer, offset, count))
			{
				throw new InternalCatalogException("Can't read cunk portion from the database");
			}
			this.m_position += (long)count;
			return count;
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00061D7C File Offset: 0x0005FF7C
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Length + offset;
				break;
			default:
				throw new InternalCatalogException("Incorrect seek origin");
			}
			if (num < 0L)
			{
				throw new InternalCatalogException("Can't seek before the beginning");
			}
			this.m_position = num;
			return num;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00061DDA File Offset: 0x0005FFDA
		public override void Flush()
		{
			throw new InternalCatalogException("Flush not supported");
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00061828 File Offset: 0x0005FA28
		public override void SetLength(long value)
		{
			throw new InternalCatalogException("SetLength not supported");
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00061DE6 File Offset: 0x0005FFE6
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InternalCatalogException("Write not supported");
		}

		// Token: 0x040008A3 RID: 2211
		private ChunkStorage m_storage;

		// Token: 0x040008A4 RID: 2212
		private object m_chunkPointer;

		// Token: 0x040008A5 RID: 2213
		private long m_chunkLength;

		// Token: 0x040008A6 RID: 2214
		private long m_position;

		// Token: 0x040008A7 RID: 2215
		private bool m_isOpen;

		// Token: 0x040008A8 RID: 2216
		private bool m_isConnectionOwner;

		// Token: 0x040008A9 RID: 2217
		private readonly bool m_isForUpgrade;
	}
}
