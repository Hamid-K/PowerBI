using System;
using System.IO;
using System.Xml;
using Microsoft.Data.Common;
using Microsoft.Data.SqlTypes;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200009B RID: 155
	internal sealed class SqlStream : Stream
	{
		// Token: 0x06000C75 RID: 3189 RVA: 0x00025948 File Offset: 0x00023B48
		internal SqlStream(SqlDataReader reader, bool addByteOrderMark, bool processAllRows)
			: this(0, reader, addByteOrderMark, processAllRows, true)
		{
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00025955 File Offset: 0x00023B55
		internal SqlStream(int columnOrdinal, SqlDataReader reader, bool addByteOrderMark, bool processAllRows, bool advanceReader)
		{
			this._columnOrdinal = columnOrdinal;
			this._reader = reader;
			this._bom = (addByteOrderMark ? 65279 : 0);
			this._processAllRows = processAllRows;
			this._advanceReader = advanceReader;
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00025577 File Offset: 0x00023777
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00025577 File Offset: 0x00023777
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x00025577 File Offset: 0x00023777
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

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002598C File Offset: 0x00023B8C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._advanceReader && this._reader != null && !this._reader.IsClosed)
				{
					this._reader.Close();
				}
				this._reader = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00025577 File Offset: 0x00023777
		public override void Flush()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x000259E8 File Offset: 0x00023BE8
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			int num2 = 0;
			if (this._reader == null)
			{
				throw ADP.StreamClosed("Read");
			}
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw ADP.ArgumentOutOfRange(string.Empty, (offset < 0) ? "offset" : "count");
			}
			if (buffer.Length - offset < count)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			if (this._bom > 0)
			{
				this._bufferedData = new byte[2];
				num2 = this.ReadBytes(this._bufferedData, 0, 2);
				if (num2 < 2 || (this._bufferedData[0] == 223 && this._bufferedData[1] == 255))
				{
					this._bom = 0;
				}
				while (count > 0 && this._bom > 0)
				{
					buffer[offset] = (byte)this._bom;
					this._bom >>= 8;
					offset++;
					count--;
					num++;
				}
			}
			if (num2 > 0)
			{
				while (count > 0)
				{
					buffer[offset++] = this._bufferedData[0];
					num++;
					count--;
					if (num2 > 1 && count > 0)
					{
						buffer[offset++] = this._bufferedData[1];
						num++;
						count--;
						break;
					}
				}
				this._bufferedData = null;
			}
			return num + this.ReadBytes(buffer, offset, count);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00025B2C File Offset: 0x00023D2C
		private static bool AdvanceToNextRow(SqlDataReader reader)
		{
			while (!reader.Read())
			{
				if (!reader.NextResult())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00025B44 File Offset: 0x00023D44
		private int ReadBytes(byte[] buffer, int offset, int count)
		{
			bool flag = true;
			int num = 0;
			if (this._reader.IsClosed || this._endOfColumn)
			{
				return 0;
			}
			try
			{
				while (count > 0)
				{
					if (this._advanceReader && this._bytesCol == 0L)
					{
						flag = false;
						if ((!this._readFirstRow || this._processAllRows) && SqlStream.AdvanceToNextRow(this._reader))
						{
							this._readFirstRow = true;
							if (this._reader.IsDBNull(this._columnOrdinal))
							{
								continue;
							}
							flag = true;
						}
					}
					if (!flag)
					{
						break;
					}
					int num2 = (int)this._reader.GetBytesInternal(this._columnOrdinal, this._bytesCol, buffer, offset, count);
					if (num2 < count)
					{
						this._bytesCol = 0L;
						flag = false;
						if (!this._advanceReader)
						{
							this._endOfColumn = true;
						}
					}
					else
					{
						this._bytesCol += (long)num2;
					}
					count -= num2;
					offset += num2;
					num += num2;
				}
				if (!flag && this._advanceReader)
				{
					this._reader.Close();
				}
			}
			catch (Exception ex)
			{
				if (this._advanceReader && ADP.IsCatchableExceptionType(ex))
				{
					this._reader.Close();
				}
				throw;
			}
			return num;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00025C68 File Offset: 0x00023E68
		internal XmlReader ToXmlReader(bool async = false)
		{
			return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(this, true, async);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00025577 File Offset: 0x00023777
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00025577 File Offset: 0x00023777
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00025577 File Offset: 0x00023777
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0400032C RID: 812
		private SqlDataReader _reader;

		// Token: 0x0400032D RID: 813
		private readonly int _columnOrdinal;

		// Token: 0x0400032E RID: 814
		private long _bytesCol;

		// Token: 0x0400032F RID: 815
		private int _bom;

		// Token: 0x04000330 RID: 816
		private byte[] _bufferedData;

		// Token: 0x04000331 RID: 817
		private readonly bool _processAllRows;

		// Token: 0x04000332 RID: 818
		private readonly bool _advanceReader;

		// Token: 0x04000333 RID: 819
		private bool _readFirstRow;

		// Token: 0x04000334 RID: 820
		private bool _endOfColumn;
	}
}
