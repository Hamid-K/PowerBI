using System;
using System.IO;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000146 RID: 326
	internal sealed class DummyStream : Stream
	{
		// Token: 0x0600197F RID: 6527 RVA: 0x0006B441 File Offset: 0x00069641
		private void DontDoIt()
		{
			throw new Exception(StringsHelper.GetString(Strings.Sql_InternalError, Array.Empty<object>()));
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x0006B457 File Offset: 0x00069657
		// (set) Token: 0x06001984 RID: 6532 RVA: 0x0006B45F File Offset: 0x0006965F
		public override long Position
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x0006B457 File Offset: 0x00069657
		public override long Length
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0006B45F File Offset: 0x0006965F
		public override void SetLength(long value)
		{
			this._size = value;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0006B468 File Offset: 0x00069668
		public override long Seek(long value, SeekOrigin loc)
		{
			this.DontDoIt();
			return -1L;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void Flush()
		{
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0006B472 File Offset: 0x00069672
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.DontDoIt();
			return -1;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0006B47B File Offset: 0x0006967B
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._size += (long)count;
		}

		// Token: 0x040009D0 RID: 2512
		private long _size;
	}
}
