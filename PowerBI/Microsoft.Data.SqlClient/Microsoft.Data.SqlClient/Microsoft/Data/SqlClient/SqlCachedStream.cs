using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200009C RID: 156
	internal sealed class SqlCachedStream : Stream
	{
		// Token: 0x06000C86 RID: 3206 RVA: 0x00025C72 File Offset: 0x00023E72
		internal SqlCachedStream(SqlCachedBuffer sqlBuf)
		{
			this._cachedBytes = sqlBuf.CachedBytes;
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00025C86 File Offset: 0x00023E86
		public override long Length
		{
			get
			{
				return this.TotalLength;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00025C90 File Offset: 0x00023E90
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x00025CD7 File Offset: 0x00023ED7
		public override long Position
		{
			get
			{
				long num = 0L;
				if (this._currentArrayIndex > 0)
				{
					for (int i = 0; i < this._currentArrayIndex; i++)
					{
						num += (long)this._cachedBytes[i].Length;
					}
				}
				return num + (long)this._currentPosition;
			}
			set
			{
				if (this._cachedBytes == null)
				{
					throw ADP.StreamClosed("set_Position");
				}
				this.SetInternalPosition(value, "set_Position");
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00025CF8 File Offset: 0x00023EF8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._cachedBytes != null)
				{
					this._cachedBytes.Clear();
				}
				this._cachedBytes = null;
				this._currentPosition = 0;
				this._currentArrayIndex = 0;
				this._totalLength = 0L;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00025577 File Offset: 0x00023777
		public override void Flush()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00025D54 File Offset: 0x00023F54
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			if (this._cachedBytes == null)
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
			if (this._cachedBytes.Count <= this._currentArrayIndex)
			{
				return 0;
			}
			while (count > 0)
			{
				if (this._cachedBytes[this._currentArrayIndex].Length <= this._currentPosition)
				{
					this._currentArrayIndex++;
					if (this._cachedBytes.Count <= this._currentArrayIndex)
					{
						break;
					}
					this._currentPosition = 0;
				}
				int num2 = this._cachedBytes[this._currentArrayIndex].Length - this._currentPosition;
				if (num2 > count)
				{
					num2 = count;
				}
				Buffer.BlockCopy(this._cachedBytes[this._currentArrayIndex], this._currentPosition, buffer, offset, num2);
				this._currentPosition += num2;
				count -= num2;
				offset += num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00025E7C File Offset: 0x0002407C
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num = 0L;
			if (this._cachedBytes == null)
			{
				throw ADP.StreamClosed("Seek");
			}
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.SetInternalPosition(offset, "offset");
				break;
			case SeekOrigin.Current:
				num = offset + this.Position;
				this.SetInternalPosition(num, "offset");
				break;
			case SeekOrigin.End:
				num = this.TotalLength + offset;
				this.SetInternalPosition(num, "offset");
				break;
			default:
				throw ADP.InvalidSeekOrigin("offset");
			}
			return num;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00025577 File Offset: 0x00023777
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00025577 File Offset: 0x00023777
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00025EFC File Offset: 0x000240FC
		private void SetInternalPosition(long lPos, string argumentName)
		{
			long num = lPos;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
			for (int i = 0; i < this._cachedBytes.Count; i++)
			{
				if (num <= (long)this._cachedBytes[i].Length)
				{
					this._currentArrayIndex = i;
					this._currentPosition = (int)num;
					return;
				}
				num -= (long)this._cachedBytes[i].Length;
			}
			if (num > 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00025F70 File Offset: 0x00024170
		private long TotalLength
		{
			get
			{
				if (this._totalLength == 0L && this._cachedBytes != null)
				{
					long num = 0L;
					for (int i = 0; i < this._cachedBytes.Count; i++)
					{
						num += (long)this._cachedBytes[i].Length;
					}
					this._totalLength = num;
				}
				return this._totalLength;
			}
		}

		// Token: 0x04000335 RID: 821
		private int _currentPosition;

		// Token: 0x04000336 RID: 822
		private int _currentArrayIndex;

		// Token: 0x04000337 RID: 823
		private List<byte[]> _cachedBytes;

		// Token: 0x04000338 RID: 824
		private long _totalLength;
	}
}
