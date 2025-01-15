using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002078 RID: 8312
	internal sealed class HashStream : Stream
	{
		// Token: 0x0600CB65 RID: 52069 RVA: 0x002885F7 File Offset: 0x002867F7
		public HashStream(HashAlgorithm hash)
		{
			this.hash = hash;
		}

		// Token: 0x170030FB RID: 12539
		// (get) Token: 0x0600CB66 RID: 52070 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170030FC RID: 12540
		// (get) Token: 0x0600CB67 RID: 52071 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170030FD RID: 12541
		// (get) Token: 0x0600CB68 RID: 52072 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170030FE RID: 12542
		// (get) Token: 0x0600CB69 RID: 52073 RVA: 0x000033E7 File Offset: 0x000015E7
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170030FF RID: 12543
		// (get) Token: 0x0600CB6A RID: 52074 RVA: 0x000033E7 File Offset: 0x000015E7
		// (set) Token: 0x0600CB6B RID: 52075 RVA: 0x000033E7 File Offset: 0x000015E7
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600CB6C RID: 52076 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Flush()
		{
		}

		// Token: 0x0600CB6D RID: 52077 RVA: 0x00288606 File Offset: 0x00286806
		private void Finish()
		{
			this.hash.TransformFinalBlock(EmptyArray<byte>.Instance, 0, 0);
		}

		// Token: 0x0600CB6E RID: 52078 RVA: 0x0028861B File Offset: 0x0028681B
		public byte[] FinishAndGetHash()
		{
			this.Finish();
			return this.hash.Hash;
		}

		// Token: 0x0600CB6F RID: 52079 RVA: 0x000033E7 File Offset: 0x000015E7
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600CB70 RID: 52080 RVA: 0x0028862E File Offset: 0x0028682E
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.hash.TransformBlock(buffer, offset, count, null, 0);
		}

		// Token: 0x0600CB71 RID: 52081 RVA: 0x000033E7 File Offset: 0x000015E7
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600CB72 RID: 52082 RVA: 0x000033E7 File Offset: 0x000015E7
		public override void SetLength(long length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04006744 RID: 26436
		private readonly HashAlgorithm hash;
	}
}
