using System;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x0200002F RID: 47
	internal sealed class RandomNumberGenerator
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000065B5 File Offset: 0x000047B5
		public static RandomNumberGenerator Current
		{
			get
			{
				if (RandomNumberGenerator.t_random == null)
				{
					RandomNumberGenerator.t_random = new RandomNumberGenerator();
				}
				return RandomNumberGenerator.t_random;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000065D0 File Offset: 0x000047D0
		[SecuritySafeCritical]
		public unsafe RandomNumberGenerator()
		{
			do
			{
				Guid guid = Guid.NewGuid();
				Guid guid2 = Guid.NewGuid();
				ulong* ptr = (ulong*)(&guid);
				ulong* ptr2 = (ulong*)(&guid2);
				this._s0 = *ptr;
				this._s1 = ptr[1];
				this._s2 = *ptr2;
				this._s3 = ptr2[1];
				this._s0 = (this._s0 & 1152921504606846975UL) | (this._s1 & 17293822569102704640UL);
				this._s2 = (this._s2 & 1152921504606846975UL) | (this._s3 & 17293822569102704640UL);
				this._s1 = (this._s1 & 18446744073709551423UL) | (this._s0 & 192UL);
				this._s3 = (this._s3 & 18446744073709551423UL) | (this._s2 & 192UL);
			}
			while ((this._s0 | this._s1 | this._s2 | this._s3) == 0UL);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000066CB File Offset: 0x000048CB
		private ulong Rol64(ulong x, int k)
		{
			return (x << k) | (x >> 64 - k);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000066E0 File Offset: 0x000048E0
		public long Next()
		{
			ulong num = this.Rol64(this._s1 * 5UL, 7) * 9UL;
			ulong num2 = this._s1 << 17;
			this._s2 ^= this._s0;
			this._s3 ^= this._s1;
			this._s1 ^= this._s2;
			this._s0 ^= this._s3;
			this._s2 ^= num2;
			this._s3 = this.Rol64(this._s3, 45);
			return (long)num;
		}

		// Token: 0x04000098 RID: 152
		[ThreadStatic]
		private static RandomNumberGenerator t_random;

		// Token: 0x04000099 RID: 153
		private ulong _s0;

		// Token: 0x0400009A RID: 154
		private ulong _s1;

		// Token: 0x0400009B RID: 155
		private ulong _s2;

		// Token: 0x0400009C RID: 156
		private ulong _s3;
	}
}
