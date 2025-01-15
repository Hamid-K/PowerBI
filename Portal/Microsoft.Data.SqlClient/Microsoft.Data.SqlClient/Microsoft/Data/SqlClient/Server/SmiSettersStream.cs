using System;
using System.IO;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200012C RID: 300
	internal class SmiSettersStream : Stream
	{
		// Token: 0x0600174B RID: 5963 RVA: 0x00062184 File Offset: 0x00060384
		internal SmiSettersStream(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData)
		{
			this._sink = sink;
			this._setters = setters;
			this._ordinal = ordinal;
			this._lengthWritten = 0L;
			this._metaData = metaData;
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x000621B1 File Offset: 0x000603B1
		public override long Length
		{
			get
			{
				return this._lengthWritten;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x000621B1 File Offset: 0x000603B1
		// (set) Token: 0x06001751 RID: 5969 RVA: 0x00060AC0 File Offset: 0x0005ECC0
		public override long Position
		{
			get
			{
				return this._lengthWritten;
			}
			set
			{
				throw SQL.StreamSeekNotSupported();
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000621B9 File Offset: 0x000603B9
		public override void Flush()
		{
			this._lengthWritten = ValueUtilsSmi.SetBytesLength(this._sink, this._setters, this._ordinal, this._metaData, this._lengthWritten);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00060AC0 File Offset: 0x0005ECC0
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw SQL.StreamSeekNotSupported();
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x000621E4 File Offset: 0x000603E4
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw ADP.ArgumentOutOfRange("value");
			}
			ValueUtilsSmi.SetBytesLength(this._sink, this._setters, this._ordinal, this._metaData, value);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00062215 File Offset: 0x00060415
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw SQL.StreamReadNotSupported();
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0006221C File Offset: 0x0006041C
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._lengthWritten += ValueUtilsSmi.SetBytes(this._sink, this._setters, this._ordinal, this._metaData, this._lengthWritten, buffer, offset, count);
		}

		// Token: 0x04000966 RID: 2406
		private SmiEventSink_Default _sink;

		// Token: 0x04000967 RID: 2407
		private ITypedSettersV3 _setters;

		// Token: 0x04000968 RID: 2408
		private int _ordinal;

		// Token: 0x04000969 RID: 2409
		private long _lengthWritten;

		// Token: 0x0400096A RID: 2410
		private SmiMetaData _metaData;
	}
}
