using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.BIServer.HostingEnvironment.Storage;

namespace Microsoft.ReportingServices.CatalogAccess.Streams
{
	// Token: 0x0200002F RID: 47
	public class VarbinaryReadableStream : Stream
	{
		// Token: 0x06000135 RID: 309 RVA: 0x000070E1 File Offset: 0x000052E1
		public VarbinaryReadableStream(string getContentStoredProcedure, Dictionary<string, object> parameters)
		{
			this._parameters = parameters;
			this._getContentStoredProcedure = getContentStoredProcedure;
			this._connection = CatalogAccessFactory.NewConnection();
			this.InitializeDatabaseAccess();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007108 File Offset: 0x00005308
		private void InitializeDatabaseAccess()
		{
			this._dataReader = this._connection.ExecuteReaderAsync(this._getContentStoredProcedure, this._parameters, CommandBehavior.SequentialAccess).Result;
			if (!this._dataReader.HasRows)
			{
				throw new IOException(string.Format("There is no data in the database for {0}", string.Join(";", this._parameters.Select((KeyValuePair<string, object> x) => x.Key + "=" + x.Value))));
			}
			this._dataReader.Read();
			this._length = this._dataReader.GetInt64(this._dataReader.GetOrdinal("ContentLength"));
			this._position = 0L;
			this._contentStream = this._dataReader.GetStream(this._dataReader.GetOrdinal("Content"));
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000071E0 File Offset: 0x000053E0
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.ReadAsync(buffer, offset, count, default(CancellationToken)).Result;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007204 File Offset: 0x00005404
		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			int num = await this._contentStream.ReadAsync(buffer, offset, count, cancellationToken);
			this._position += (long)num;
			return num;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000726A File Offset: 0x0000546A
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00007271 File Offset: 0x00005471
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00007274 File Offset: 0x00005474
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00007274 File Offset: 0x00005474
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00007277 File Offset: 0x00005477
		public override long Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000727F File Offset: 0x0000547F
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000726A File Offset: 0x0000546A
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

		// Token: 0x06000140 RID: 320 RVA: 0x00007288 File Offset: 0x00005488
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._contentStream != null)
				{
					this._contentStream.Dispose();
					this._contentStream = null;
				}
				if (this._dataReader != null)
				{
					this._dataReader.Dispose();
					this._dataReader = null;
				}
				if (this._connection != null)
				{
					this._connection.Dispose();
				}
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000726A File Offset: 0x0000546A
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000726A File Offset: 0x0000546A
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000726A File Offset: 0x0000546A
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040000A8 RID: 168
		private readonly ISqlAccess _connection;

		// Token: 0x040000A9 RID: 169
		private readonly string _getContentStoredProcedure;

		// Token: 0x040000AA RID: 170
		private readonly Dictionary<string, object> _parameters;

		// Token: 0x040000AB RID: 171
		private DbDataReader _dataReader;

		// Token: 0x040000AC RID: 172
		private long _length;

		// Token: 0x040000AD RID: 173
		private long _position;

		// Token: 0x040000AE RID: 174
		private Stream _contentStream;
	}
}
