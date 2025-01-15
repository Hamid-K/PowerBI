using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.BIServer.HostingEnvironment.Storage;

namespace Microsoft.ReportingServices.CatalogAccess.Streams
{
	// Token: 0x02000030 RID: 48
	internal sealed class ExtendedContentWritableStream : Stream
	{
		// Token: 0x06000144 RID: 324 RVA: 0x000072E0 File Offset: 0x000054E0
		public ExtendedContentWritableStream(Guid catalogItemId, ExtendedContentType extendedContentType, DateTime modifiedDate)
		{
			this._length = 0L;
			this._position = 0L;
			this._transaction = CatalogAccessFactory.NewTransaction("CatalogExtendedContent");
			this._catalogItemId = catalogItemId;
			this._extendedContentType = extendedContentType;
			this._modifiedDate = modifiedDate;
			string text = "InitializeCatalogExtendedContentWrite";
			var <>f__AnonymousType = new
			{
				CatalogItemID = catalogItemId,
				ContentType = extendedContentType.ToString()
			};
			this._id = this._transaction.QueryFirstOrDefaultAsync<int>(text, <>f__AnonymousType).Result;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00007274 File Offset: 0x00005474
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00007271 File Offset: 0x00005471
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00007274 File Offset: 0x00005474
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000735A File Offset: 0x0000555A
		public override long Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00007362 File Offset: 0x00005562
		// (set) Token: 0x0600014A RID: 330 RVA: 0x0000726A File Offset: 0x0000546A
		public override long Position
		{
			get
			{
				return this._position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000726A File Offset: 0x0000546A
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000736C File Offset: 0x0000556C
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			byte[] array;
			if (offset == 0)
			{
				array = buffer;
			}
			else
			{
				array = new byte[count];
				Array.Copy(buffer, offset, array, 0, count);
			}
			string text = "WriteCatalogExtendedContentChunkById";
			var <>f__AnonymousType = new
			{
				Id = this._id,
				Chunk = array,
				Offset = this._position,
				Length = count
			};
			this._position += (long)count;
			this._length = this._position;
			return this._transaction.ExecuteAsync(text, <>f__AnonymousType);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000073D4 File Offset: 0x000055D4
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.WriteAsync(buffer, offset, count, default(CancellationToken)).Wait();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000073F8 File Offset: 0x000055F8
		public override void Flush()
		{
			this.UpdateLastModifieDate(this._catalogItemId, this._extendedContentType, this._modifiedDate);
			this._transaction.Commit();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000741D File Offset: 0x0000561D
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._transaction.Dispose();
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000726A File Offset: 0x0000546A
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000726A File Offset: 0x0000546A
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007430 File Offset: 0x00005630
		private void UpdateLastModifieDate(Guid catalogItemId, ExtendedContentType extendedContentType, DateTime modifiedDate)
		{
			string text = "UpdateCatalogExtendedContentModifiedDate";
			var <>f__AnonymousType = new
			{
				CatalogItemID = catalogItemId,
				ContentType = extendedContentType.ToString(),
				ModifiedDate = modifiedDate
			};
			this._transaction.ExecuteAsync(text, <>f__AnonymousType).GetAwaiter().GetResult();
		}

		// Token: 0x040000AF RID: 175
		private long _length;

		// Token: 0x040000B0 RID: 176
		private long _position;

		// Token: 0x040000B1 RID: 177
		private readonly int _id;

		// Token: 0x040000B2 RID: 178
		private readonly ScopedSqlTransaction _transaction;

		// Token: 0x040000B3 RID: 179
		private readonly Guid _catalogItemId;

		// Token: 0x040000B4 RID: 180
		private readonly ExtendedContentType _extendedContentType;

		// Token: 0x040000B5 RID: 181
		private readonly DateTime _modifiedDate;
	}
}
