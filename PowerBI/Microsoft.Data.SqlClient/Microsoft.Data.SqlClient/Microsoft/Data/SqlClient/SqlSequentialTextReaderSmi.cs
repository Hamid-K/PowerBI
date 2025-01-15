using System;
using System.IO;
using Microsoft.Data.Common;
using Microsoft.Data.SqlClient.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000F3 RID: 243
	internal sealed class SqlSequentialTextReaderSmi : TextReader
	{
		// Token: 0x060012E2 RID: 4834 RVA: 0x0004C57A File Offset: 0x0004A77A
		internal SqlSequentialTextReaderSmi(SmiEventSink_Default sink, ITypedGettersV3 getters, int columnIndex, long length)
		{
			this._sink = sink;
			this._getters = getters;
			this._columnIndex = columnIndex;
			this._length = length;
			this._position = 0L;
			this._peekedChar = -1;
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x0004C5AE File Offset: 0x0004A7AE
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0004C5B6 File Offset: 0x0004A7B6
		public override int Peek()
		{
			if (!this.HasPeekedChar)
			{
				this._peekedChar = this.Read();
			}
			return this._peekedChar;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0004C5D4 File Offset: 0x0004A7D4
		public override int Read()
		{
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			int num = -1;
			if (this.HasPeekedChar)
			{
				num = this._peekedChar;
				this._peekedChar = -1;
			}
			else if (this._position < this._length)
			{
				char[] array = new char[1];
				int chars_Unchecked = ValueUtilsSmi.GetChars_Unchecked(this._sink, this._getters, this._columnIndex, this._position, array, 0, 1);
				if (chars_Unchecked == 1)
				{
					num = (int)array[0];
					this._position += 1L;
				}
			}
			return num;
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0004C658 File Offset: 0x0004A858
		public override int Read(char[] buffer, int index, int count)
		{
			SqlSequentialTextReader.ValidateReadParameters(buffer, index, count);
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			int num = 0;
			if (count > 0 && this.HasPeekedChar)
			{
				buffer[index + num] = (char)this._peekedChar;
				num++;
				this._peekedChar = -1;
			}
			int num2 = (int)Math.Min((long)(count - num), this._length - this._position);
			if (num2 > 0)
			{
				int chars_Unchecked = ValueUtilsSmi.GetChars_Unchecked(this._sink, this._getters, this._columnIndex, this._position, buffer, index + num, num2);
				this._position += (long)chars_Unchecked;
				num += chars_Unchecked;
			}
			return num;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0004C6F4 File Offset: 0x0004A8F4
		internal void SetClosed()
		{
			this._sink = null;
			this._getters = null;
			this._peekedChar = -1;
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0004C70B File Offset: 0x0004A90B
		private bool IsClosed
		{
			get
			{
				return this._sink == null || this._getters == null;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0004C720 File Offset: 0x0004A920
		private bool HasPeekedChar
		{
			get
			{
				return this._peekedChar >= 0;
			}
		}

		// Token: 0x040007D9 RID: 2009
		private SmiEventSink_Default _sink;

		// Token: 0x040007DA RID: 2010
		private ITypedGettersV3 _getters;

		// Token: 0x040007DB RID: 2011
		private int _columnIndex;

		// Token: 0x040007DC RID: 2012
		private long _position;

		// Token: 0x040007DD RID: 2013
		private long _length;

		// Token: 0x040007DE RID: 2014
		private int _peekedChar;
	}
}
