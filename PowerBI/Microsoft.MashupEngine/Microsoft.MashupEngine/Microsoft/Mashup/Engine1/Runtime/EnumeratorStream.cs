using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012E7 RID: 4839
	internal class EnumeratorStream : Stream
	{
		// Token: 0x06008028 RID: 32808 RVA: 0x001B539F File Offset: 0x001B359F
		public EnumeratorStream(IEnumerator<IValueReference> enumerator)
		{
			this.enumerator = enumerator;
		}

		// Token: 0x170022BF RID: 8895
		// (get) Token: 0x06008029 RID: 32809 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170022C0 RID: 8896
		// (get) Token: 0x0600802A RID: 32810 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170022C1 RID: 8897
		// (get) Token: 0x0600802B RID: 32811 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600802C RID: 32812 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Flush()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170022C2 RID: 8898
		// (get) Token: 0x0600802D RID: 32813 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Length
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600802E RID: 32814 RVA: 0x001B53AE File Offset: 0x001B35AE
		protected override void Dispose(bool disposing)
		{
			this.enumerator.Dispose();
		}

		// Token: 0x170022C3 RID: 8899
		// (get) Token: 0x0600802F RID: 32815 RVA: 0x0000EE09 File Offset: 0x0000D009
		// (set) Token: 0x06008030 RID: 32816 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Position
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06008031 RID: 32817 RVA: 0x001B53BB File Offset: 0x001B35BB
		public override int ReadByte()
		{
			if (!this.enumerator.MoveNext())
			{
				return -1;
			}
			return (int)this.enumerator.Current.Value.AsNumber.ToByte();
		}

		// Token: 0x06008032 RID: 32818 RVA: 0x001B53E8 File Offset: 0x001B35E8
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			while (num < count && this.enumerator.MoveNext())
			{
				buffer[offset + num] = this.enumerator.Current.Value.AsNumber.ToByte();
				num++;
			}
			return num;
		}

		// Token: 0x06008033 RID: 32819 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008034 RID: 32820 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void SetLength(long value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008035 RID: 32821 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x040045CD RID: 17869
		private IEnumerator<IValueReference> enumerator;
	}
}
