using System;
using System.Data;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200028E RID: 654
	internal class FileChunkWriteStream : WriteSnapshotChunkStreamBase
	{
		// Token: 0x060017DC RID: 6108 RVA: 0x0006166C File Offset: 0x0005F86C
		internal FileChunkWriteStream(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType, string mimeType, ChunkFlags flags, bool forUpdate)
			: base(snapshotDataID, isPermanentSnapshot, chunkName, chunkType, mimeType)
		{
			RSTrace.ChunkTracer.Assert(!isPermanentSnapshot, "cannot create permanent chunk in file system");
			this.m_chunkFlags = flags;
			ChunkStorage chunkStorage = new ChunkStorage();
			try
			{
				chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.Explicit, IsolationLevel.ReadCommitted);
				chunkStorage.ConnectionManager.WillDisconnectStorage();
				short num;
				if (!forUpdate)
				{
					chunkStorage.CreateChunk(this.m_snapshotDataID, this.m_isPermanentSnapshot, this.m_chunkName, this.m_chunkType, this.m_mimeType, this.m_chunkFlags, ChunkHeader.CurrentVersion, new byte[0], 0, 0, out this.m_chunkPointer);
				}
				else if (!chunkStorage.GetChunkPointerAndLength(this.m_snapshotDataID, this.m_isPermanentSnapshot, this.m_chunkName, this.m_chunkType, out this.m_chunkPointer, out this.m_length, out this.m_mimeType, out this.m_chunkFlags, out num))
				{
					throw new InternalCatalogException("Could not find chunk information in catalog");
				}
				this.OpenFileStream(forUpdate);
				chunkStorage.Commit();
				this.m_IsAvailable = true;
			}
			catch (Exception)
			{
				chunkStorage.AbortTransaction();
				this.m_IsAvailable = false;
				throw;
			}
			finally
			{
				chunkStorage.DisconnectStorage();
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00061798 File Offset: 0x0005F998
		private void OpenFileStream(bool forUpdate)
		{
			this.m_isOpen = true;
			if (!forUpdate)
			{
				Global.PartitionManager.DeleteFile(this.FolderName, this.FileName);
			}
			this.m_stream = Global.PartitionManager.CreateFile(this.FolderName, this.FileName, forUpdate);
			this.m_length = this.m_stream.Length;
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x000617F4 File Offset: 0x0005F9F4
		public override long Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x000617FC File Offset: 0x0005F9FC
		// (set) Token: 0x060017E0 RID: 6112 RVA: 0x00061185 File Offset: 0x0005F385
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

		// Token: 0x060017E1 RID: 6113 RVA: 0x00061809 File Offset: 0x0005FA09
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_stream.Read(buffer, offset, count);
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00061819 File Offset: 0x0005FA19
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_stream.Seek(offset, origin);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00061828 File Offset: 0x0005FA28
		public override void SetLength(long value)
		{
			throw new InternalCatalogException("SetLength not supported");
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x00061834 File Offset: 0x0005FA34
		public override bool CanSeek
		{
			get
			{
				return this.m_stream.CanSeek;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00061841 File Offset: 0x0005FA41
		public override bool CanWrite
		{
			get
			{
				return this.m_stream.CanWrite;
			}
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0006184E File Offset: 0x0005FA4E
		public override void Flush()
		{
			this.m_stream.Flush();
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x0006185B File Offset: 0x0005FA5B
		private string FileName
		{
			get
			{
				return this.m_chunkName;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x00061863 File Offset: 0x0005FA63
		private string FolderName
		{
			get
			{
				return PartitionManager.ConvertGuidToDirectoryName(this.m_snapshotDataID);
			}
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00061870 File Offset: 0x0005FA70
		protected override bool DeleteOpenChunk()
		{
			RSTrace.CatalogTrace.Assert(this.m_isOpen && this.m_stream != null, "DeleteOpenChunk called when stream wasn't open");
			this.m_stream.Flush();
			this.m_stream.Close();
			bool flag = this.DeleteSqlChunk();
			if (flag)
			{
				flag = Global.PartitionManager.DeleteFile(this.m_stream);
				if (flag)
				{
					this.m_isOpen = false;
					this.m_stream = null;
				}
			}
			return flag;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x000618E8 File Offset: 0x0005FAE8
		protected override bool DeleteClosedChunk()
		{
			RSTrace.CatalogTrace.Assert(!this.m_isOpen, "DeleteClosedChunk called when stream was open");
			bool flag = this.DeleteSqlChunk();
			if (flag)
			{
				flag = Global.PartitionManager.DeleteFile(this.FolderName, this.FileName);
			}
			return flag;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00061930 File Offset: 0x0005FB30
		private bool DeleteSqlChunk()
		{
			ChunkStorage chunkStorage = new ChunkStorage();
			bool flag;
			try
			{
				chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.Explicit, IsolationLevel.ReadCommitted);
				chunkStorage.ConnectionManager.WillDisconnectStorage();
				flag = chunkStorage.DeleteOneChunk(this.m_snapshotDataID, this.m_isPermanentSnapshot, this.m_chunkName, this.m_chunkType);
			}
			finally
			{
				if (chunkStorage != null)
				{
					chunkStorage.DisconnectStorage();
				}
			}
			return flag;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0006199C File Offset: 0x0005FB9C
		public override void Close()
		{
			base.Close();
			if (this.m_stream != null)
			{
				this.m_stream.Flush();
				if (this.m_stream.Length != this.Length)
				{
					this.m_stream.SetLength(this.Length);
				}
				this.m_stream.Close();
				this.m_isOpen = false;
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x000619F8 File Offset: 0x0005FBF8
		public override void Write(byte[] buffer, int offset, int length)
		{
			this.m_stream.Write(buffer, offset, length);
			this.m_length += (long)length;
			this.m_length = Math.Min(this.Position, this.m_length);
		}

		// Token: 0x04000897 RID: 2199
		private PartitionFileStream m_stream;

		// Token: 0x04000898 RID: 2200
		private object m_chunkPointer;

		// Token: 0x04000899 RID: 2201
		private long m_length;
	}
}
