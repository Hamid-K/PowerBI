using System;
using System.IO;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200011F RID: 287
	internal class SmiGettersStream : Stream
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x00060A59 File Offset: 0x0005EC59
		internal SmiGettersStream(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			this._sink = sink;
			this._getters = getters;
			this._ordinal = ordinal;
			this._readPosition = 0L;
			this._metaData = metaData;
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00060A88 File Offset: 0x0005EC88
		public override long Length
		{
			get
			{
				return ValueUtilsSmi.GetBytesInternal(this._sink, this._getters, this._ordinal, this._metaData, 0L, null, 0, 0, false);
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00060AB8 File Offset: 0x0005ECB8
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x00060AC0 File Offset: 0x0005ECC0
		public override long Position
		{
			get
			{
				return this._readPosition;
			}
			set
			{
				throw SQL.StreamSeekNotSupported();
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00060AC7 File Offset: 0x0005ECC7
		public override void Flush()
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00060AC0 File Offset: 0x0005ECC0
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw SQL.StreamSeekNotSupported();
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00060AC7 File Offset: 0x0005ECC7
		public override void SetLength(long value)
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00060AD0 File Offset: 0x0005ECD0
		public override int Read(byte[] buffer, int offset, int count)
		{
			long bytesInternal = ValueUtilsSmi.GetBytesInternal(this._sink, this._getters, this._ordinal, this._metaData, this._readPosition, buffer, offset, count, false);
			this._readPosition += bytesInternal;
			return checked((int)bytesInternal);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00060AC7 File Offset: 0x0005ECC7
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x04000905 RID: 2309
		private SmiEventSink_Default _sink;

		// Token: 0x04000906 RID: 2310
		private ITypedGettersV3 _getters;

		// Token: 0x04000907 RID: 2311
		private int _ordinal;

		// Token: 0x04000908 RID: 2312
		private long _readPosition;

		// Token: 0x04000909 RID: 2313
		private SmiMetaData _metaData;
	}
}
