using System;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200028D RID: 653
	internal sealed class SqlChunkWriteStream : WriteSnapshotChunkStreamBase
	{
		// Token: 0x060017CA RID: 6090 RVA: 0x000610EC File Offset: 0x0005F2EC
		internal SqlChunkWriteStream(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType, string mimeType, ChunkFlags flags, ChunkConnectionManager connectionManager)
			: this(snapshotDataID, isPermanentSnapshot, chunkName, chunkType, mimeType, flags, connectionManager, false)
		{
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0006110C File Offset: 0x0005F30C
		internal SqlChunkWriteStream(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType, string mimeType, ChunkFlags flags, ChunkConnectionManager connectionManager, bool forUpdate)
			: base(snapshotDataID, isPermanentSnapshot, chunkName, chunkType, mimeType)
		{
			this.m_chunkFlags = flags;
			this.m_position = 0L;
			this.m_length = 0L;
			this.m_storage = null;
			this.m_isChunkOpened = false;
			this.m_isOpen = true;
			this.m_connectionManager = connectionManager;
			this.m_forUpdate = forUpdate;
			if (this.m_forUpdate)
			{
				this.EnsureChunkOpen(null, 0, 0);
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x00061175 File Offset: 0x0005F375
		public override long Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0006117D File Offset: 0x0005F37D
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x00061185 File Offset: 0x0005F385
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

		// Token: 0x060017D2 RID: 6098 RVA: 0x00061190 File Offset: 0x0005F390
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
			if (num > this.m_length)
			{
				throw new InternalCatalogException("Can't seek past the end");
			}
			this.m_position = num;
			return num;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00061204 File Offset: 0x0005F404
		public override void Close()
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ChunkWriteStream.Close() - closing... , id={0}, name='{1}'", new object[] { this.m_snapshotDataID, this.m_chunkName });
			}
			if (!this.m_isOpen)
			{
				return;
			}
			base.Close();
			if (!this.IsConnectionOwner)
			{
				return;
			}
			if (this.m_storage != null)
			{
				if (this.Length > this.Position)
				{
					if (RSTrace.ChunkTracer.TraceVerbose)
					{
						RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ChunkWriteStream.Close() - truncating from length {0} to length {1}..., id={1}, name='{2}'", new object[] { this.Length, this.Position, this.m_snapshotDataID, this.m_chunkName });
					}
					this.m_storage.TruncateChunk(this.m_chunkPointer, base.IsPermanent, this.Position);
				}
				this.m_storage.Commit();
				this.m_isOpen = false;
				this.m_storage.DisconnectStorage();
				this.m_storage = null;
			}
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ChunkWriteStream - Closed! id={0}, name='{1}'", new object[] { this.m_snapshotDataID, this.m_chunkName });
			}
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public override void Flush()
		{
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0006134C File Offset: 0x0005F54C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.m_isOpen)
			{
				throw new InternalCatalogException("Stream is already closed and can't be accessed.");
			}
			this.EnsureChunkOpen(null, 0, 0);
			if (this.Position >= this.Length)
			{
				return 0;
			}
			long num = this.Length - this.Position;
			if ((long)count > num)
			{
				count = (int)num;
			}
			if (!this.m_storage.ReadChunkPortion(this.m_chunkPointer, base.IsPermanent, this.Position, buffer, offset, count))
			{
				throw new InternalCatalogException("Can't read cunk portion from the database");
			}
			this.m_position += (long)count;
			return count;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000613DA File Offset: 0x0005F5DA
		public override void SetLength(long value)
		{
			throw new InternalCatalogException("SetLength() not supported");
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x000613E6 File Offset: 0x0005F5E6
		private bool IsConnectionOwner
		{
			get
			{
				return this.m_connectionManager.IsConnectionOwner;
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000613F4 File Offset: 0x0005F5F4
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.m_isOpen)
			{
				throw new InternalCatalogException("Stream is already closed and can't be accessed.");
			}
			ChunkStorage.WriteMode writeMode = ChunkStorage.WriteMode.Overwrite;
			if (this.m_position == this.m_length)
			{
				writeMode = ChunkStorage.WriteMode.Append;
			}
			if (this.EnsureChunkOpen(buffer, offset, count))
			{
				RSTrace.CatalogTrace.Assert(this.m_position == (long)count, "m_position = count");
			}
			else
			{
				this.m_storage.WriteChunkPortion(this.m_chunkPointer, this.m_isPermanentSnapshot, this.m_position, buffer, offset, count, writeMode, this.m_length);
				this.m_position += (long)count;
			}
			this.m_length = Math.Max(this.m_length, this.m_position);
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00061499 File Offset: 0x0005F699
		protected override bool DeleteOpenChunk()
		{
			if (!this.IsConnectionOwner)
			{
				return false;
			}
			if (this.m_storage != null)
			{
				this.m_storage.AbortTransaction();
				this.m_storage.DisconnectStorage();
			}
			this.m_storage = null;
			this.m_isOpen = false;
			return true;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x000614D4 File Offset: 0x0005F6D4
		protected override bool DeleteClosedChunk()
		{
			bool flag;
			try
			{
				if (this.m_storage == null)
				{
					this.m_storage = this.m_connectionManager.DbInterface;
				}
				if (this.IsConnectionOwner)
				{
					this.m_storage.ConnectionManager.WillDisconnectStorage();
				}
				flag = this.m_storage.DeleteOneChunk(this.m_snapshotDataID, this.m_isPermanentSnapshot, this.m_chunkName, this.m_chunkType);
			}
			finally
			{
				if (this.m_storage != null && this.IsConnectionOwner)
				{
					this.m_storage.DisconnectStorage();
				}
				this.m_storage = null;
			}
			return flag;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0006156C File Offset: 0x0005F76C
		private bool EnsureChunkOpen(byte[] buffer, int offset, int count)
		{
			bool flag = false;
			if (!this.m_isChunkOpened)
			{
				SqlChunkAccess sqlChunkAccess = null;
				try
				{
					sqlChunkAccess = new SqlChunkAccess(this, this.m_connectionManager, false);
				}
				catch (VersionMismatchException)
				{
					RSTrace.ChunkTracer.Assert(false, "VersionMismatchException when opening chunk for write");
					throw;
				}
				RSTrace.ChunkTracer.Assert(sqlChunkAccess != null, "access");
				if (this.m_forUpdate)
				{
					string text;
					ChunkFlags chunkFlags;
					short num;
					if (!sqlChunkAccess.OpenExistingChunk(out this.m_chunkPointer, out this.m_length, out text, out chunkFlags, out num, out this.m_storage))
					{
						throw new InternalCatalogException("Failed to open up chunk for update.");
					}
					this.m_position = 0L;
				}
				else
				{
					if (buffer == null)
					{
						buffer = new byte[0];
						offset = 0;
						count = 0;
					}
					this.m_storage = sqlChunkAccess.CreateChunk(buffer, offset, count, out this.m_chunkPointer);
					this.m_position += (long)count;
					this.m_length = (long)count;
					flag = true;
				}
				this.m_isChunkOpened = true;
			}
			RSTrace.CatalogTrace.Assert(this.m_storage != null, "m_storage");
			return flag;
		}

		// Token: 0x04000890 RID: 2192
		private ChunkStorage m_storage;

		// Token: 0x04000891 RID: 2193
		private bool m_isChunkOpened;

		// Token: 0x04000892 RID: 2194
		private long m_position;

		// Token: 0x04000893 RID: 2195
		private long m_length;

		// Token: 0x04000894 RID: 2196
		private object m_chunkPointer;

		// Token: 0x04000895 RID: 2197
		private readonly ChunkConnectionManager m_connectionManager;

		// Token: 0x04000896 RID: 2198
		private bool m_forUpdate;
	}
}
