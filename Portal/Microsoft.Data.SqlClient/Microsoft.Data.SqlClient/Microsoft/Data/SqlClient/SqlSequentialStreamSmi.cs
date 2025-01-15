using System;
using System.IO;
using Microsoft.Data.Common;
using Microsoft.Data.SqlClient.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000F2 RID: 242
	internal sealed class SqlSequentialStreamSmi : Stream
	{
		// Token: 0x060012D3 RID: 4819 RVA: 0x0004C47D File Offset: 0x0004A67D
		internal SqlSequentialStreamSmi(SmiEventSink_Default sink, ITypedGettersV3 getters, int columnIndex, long length)
		{
			this._sink = sink;
			this._getters = getters;
			this._columnIndex = columnIndex;
			this._length = length;
			this._position = 0L;
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0004C4AA File Offset: 0x0004A6AA
		public override bool CanRead
		{
			get
			{
				return this._sink != null && this._getters != null;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void Flush()
		{
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00025577 File Offset: 0x00023777
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00025577 File Offset: 0x00023777
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x00025577 File Offset: 0x00023777
		public override long Position
		{
			get
			{
				throw ADP.NotSupported();
			}
			set
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0004C4BF File Offset: 0x0004A6BF
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0004C4C8 File Offset: 0x0004A6C8
		public override int Read(byte[] buffer, int offset, int count)
		{
			SqlSequentialStream.ValidateReadParameters(buffer, offset, count);
			if (!this.CanRead)
			{
				throw ADP.ObjectDisposed(this);
			}
			int num3;
			try
			{
				int num = (int)Math.Min((long)count, this._length - this._position);
				int num2 = 0;
				if (num > 0)
				{
					num2 = ValueUtilsSmi.GetBytes_Unchecked(this._sink, this._getters, this._columnIndex, this._position, buffer, offset, num);
					this._position += (long)num2;
				}
				num3 = num2;
			}
			catch (SqlException ex)
			{
				throw ADP.ErrorReadingFromStream(ex);
			}
			return num3;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00025577 File Offset: 0x00023777
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00025577 File Offset: 0x00023777
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00025577 File Offset: 0x00023777
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0004C558 File Offset: 0x0004A758
		internal void SetClosed()
		{
			this._sink = null;
			this._getters = null;
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0004C568 File Offset: 0x0004A768
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetClosed();
			}
			base.Dispose(disposing);
		}

		// Token: 0x040007D4 RID: 2004
		private SmiEventSink_Default _sink;

		// Token: 0x040007D5 RID: 2005
		private ITypedGettersV3 _getters;

		// Token: 0x040007D6 RID: 2006
		private int _columnIndex;

		// Token: 0x040007D7 RID: 2007
		private long _position;

		// Token: 0x040007D8 RID: 2008
		private long _length;
	}
}
