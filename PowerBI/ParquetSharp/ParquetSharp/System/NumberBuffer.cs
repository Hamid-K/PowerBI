using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x020000C9 RID: 201
	internal ref struct NumberBuffer
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00019768 File Offset: 0x00017968
		public Span<byte> Digits
		{
			get
			{
				return new Span<byte>(Unsafe.AsPointer<byte>(ref this._b0), 51);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001977C File Offset: 0x0001797C
		public unsafe byte* UnsafeDigits
		{
			get
			{
				return (byte*)Unsafe.AsPointer<byte>(ref this._b0);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001978C File Offset: 0x0001798C
		public int NumDigits
		{
			get
			{
				return this.Digits.IndexOf(0);
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001979C File Offset: 0x0001799C
		[Conditional("DEBUG")]
		public void CheckConsistency()
		{
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x000197A0 File Offset: 0x000179A0
		public unsafe override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append('"');
			Span<byte> digits = this.Digits;
			for (int i = 0; i < 51; i++)
			{
				byte b = *digits[i];
				if (b == 0)
				{
					break;
				}
				stringBuilder.Append((char)b);
			}
			stringBuilder.Append('"');
			stringBuilder.Append(", Scale = " + this.Scale);
			stringBuilder.Append(", IsNegative   = " + this.IsNegative.ToString());
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x040001F0 RID: 496
		public int Scale;

		// Token: 0x040001F1 RID: 497
		public bool IsNegative;

		// Token: 0x040001F2 RID: 498
		public const int BufferSize = 51;

		// Token: 0x040001F3 RID: 499
		private byte _b0;

		// Token: 0x040001F4 RID: 500
		private byte _b1;

		// Token: 0x040001F5 RID: 501
		private byte _b2;

		// Token: 0x040001F6 RID: 502
		private byte _b3;

		// Token: 0x040001F7 RID: 503
		private byte _b4;

		// Token: 0x040001F8 RID: 504
		private byte _b5;

		// Token: 0x040001F9 RID: 505
		private byte _b6;

		// Token: 0x040001FA RID: 506
		private byte _b7;

		// Token: 0x040001FB RID: 507
		private byte _b8;

		// Token: 0x040001FC RID: 508
		private byte _b9;

		// Token: 0x040001FD RID: 509
		private byte _b10;

		// Token: 0x040001FE RID: 510
		private byte _b11;

		// Token: 0x040001FF RID: 511
		private byte _b12;

		// Token: 0x04000200 RID: 512
		private byte _b13;

		// Token: 0x04000201 RID: 513
		private byte _b14;

		// Token: 0x04000202 RID: 514
		private byte _b15;

		// Token: 0x04000203 RID: 515
		private byte _b16;

		// Token: 0x04000204 RID: 516
		private byte _b17;

		// Token: 0x04000205 RID: 517
		private byte _b18;

		// Token: 0x04000206 RID: 518
		private byte _b19;

		// Token: 0x04000207 RID: 519
		private byte _b20;

		// Token: 0x04000208 RID: 520
		private byte _b21;

		// Token: 0x04000209 RID: 521
		private byte _b22;

		// Token: 0x0400020A RID: 522
		private byte _b23;

		// Token: 0x0400020B RID: 523
		private byte _b24;

		// Token: 0x0400020C RID: 524
		private byte _b25;

		// Token: 0x0400020D RID: 525
		private byte _b26;

		// Token: 0x0400020E RID: 526
		private byte _b27;

		// Token: 0x0400020F RID: 527
		private byte _b28;

		// Token: 0x04000210 RID: 528
		private byte _b29;

		// Token: 0x04000211 RID: 529
		private byte _b30;

		// Token: 0x04000212 RID: 530
		private byte _b31;

		// Token: 0x04000213 RID: 531
		private byte _b32;

		// Token: 0x04000214 RID: 532
		private byte _b33;

		// Token: 0x04000215 RID: 533
		private byte _b34;

		// Token: 0x04000216 RID: 534
		private byte _b35;

		// Token: 0x04000217 RID: 535
		private byte _b36;

		// Token: 0x04000218 RID: 536
		private byte _b37;

		// Token: 0x04000219 RID: 537
		private byte _b38;

		// Token: 0x0400021A RID: 538
		private byte _b39;

		// Token: 0x0400021B RID: 539
		private byte _b40;

		// Token: 0x0400021C RID: 540
		private byte _b41;

		// Token: 0x0400021D RID: 541
		private byte _b42;

		// Token: 0x0400021E RID: 542
		private byte _b43;

		// Token: 0x0400021F RID: 543
		private byte _b44;

		// Token: 0x04000220 RID: 544
		private byte _b45;

		// Token: 0x04000221 RID: 545
		private byte _b46;

		// Token: 0x04000222 RID: 546
		private byte _b47;

		// Token: 0x04000223 RID: 547
		private byte _b48;

		// Token: 0x04000224 RID: 548
		private byte _b49;

		// Token: 0x04000225 RID: 549
		private byte _b50;
	}
}
