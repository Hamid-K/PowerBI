using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200003C RID: 60
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class DbLength
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000657C File Offset: 0x0000477C
		public static DBLENGTH Zero
		{
			get
			{
				return DbLength.GetLength(0);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00006584 File Offset: 0x00004784
		public static DBLENGTH One
		{
			get
			{
				return DbLength.GetLength(1);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000658C File Offset: 0x0000478C
		public static DBLENGTH Two
		{
			get
			{
				return DbLength.GetLength(2);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00006594 File Offset: 0x00004794
		public static DBLENGTH Four
		{
			get
			{
				return DbLength.GetLength(4);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000659C File Offset: 0x0000479C
		public static DBLENGTH Eight
		{
			get
			{
				return DbLength.GetLength(8);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000065A4 File Offset: 0x000047A4
		public static DBLENGTH Sixteen
		{
			get
			{
				return DbLength.GetLength(16);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000065AD File Offset: 0x000047AD
		public static DBLENGTH MaxValue
		{
			get
			{
				return DBLENGTH.MaxValue;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000065B4 File Offset: 0x000047B4
		public static DBLENGTH Double
		{
			get
			{
				return DbLength.GetLength(8);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000065BC File Offset: 0x000047BC
		public unsafe static DBLENGTH Currency
		{
			get
			{
				return DbLength.GetLength(sizeof(Currency));
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000065C9 File Offset: 0x000047C9
		public static DBLENGTH Decimal
		{
			get
			{
				return DbLength.GetLength(16);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000065D2 File Offset: 0x000047D2
		public unsafe static DBLENGTH Variant
		{
			get
			{
				return DbLength.GetLength(sizeof(VARIANT));
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000065DF File Offset: 0x000047DF
		public static DBLENGTH Date
		{
			get
			{
				return DbLength.Double;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000065E6 File Offset: 0x000047E6
		public unsafe static DBLENGTH DbDate
		{
			get
			{
				return DbLength.GetLength(sizeof(DBDATE));
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000065F3 File Offset: 0x000047F3
		public unsafe static DBLENGTH DbTime2
		{
			get
			{
				return DbLength.GetLength(sizeof(DBTIME2));
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00006600 File Offset: 0x00004800
		public unsafe static DBLENGTH TimeStamp
		{
			get
			{
				return DbLength.GetLength(sizeof(DBTIMESTAMP));
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000660D File Offset: 0x0000480D
		public unsafe static DBLENGTH TimeStampOffset
		{
			get
			{
				return DbLength.GetLength(sizeof(DBTIMESTAMPOFFSET));
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000661A File Offset: 0x0000481A
		public static DBLENGTH Guid
		{
			get
			{
				return DbLength.Sixteen;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00006621 File Offset: 0x00004821
		public unsafe static DBLENGTH Numeric
		{
			get
			{
				return DbLength.GetLength(sizeof(NUMERIC));
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000662E File Offset: 0x0000482E
		public static DBLENGTH VariantBool
		{
			get
			{
				return DbLength.GetLength(2);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00006636 File Offset: 0x00004836
		public static DBLENGTH Error
		{
			get
			{
				return DbLength.GetLength(4);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000663E File Offset: 0x0000483E
		public unsafe static DBLENGTH Duration
		{
			get
			{
				return DbLength.GetLength(sizeof(DBDURATION));
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000664B File Offset: 0x0000484B
		public static DBLENGTH Pointer
		{
			get
			{
				return DbLength.GetLength(IntPtr.Size);
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006658 File Offset: 0x00004858
		private static DBLENGTH GetLength(uint value)
		{
			return new DBLENGTH
			{
				Value = (ulong)value
			};
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006677 File Offset: 0x00004877
		private static DBLENGTH GetLength(int value)
		{
			return DbLength.GetLength((uint)value);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000667F File Offset: 0x0000487F
		public static DBLENGTH GetLength(string value)
		{
			return DbLength.GetLength(value.Length * 2);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000668E File Offset: 0x0000488E
		public static DBLENGTH GetLength(byte[] value)
		{
			return DbLength.GetLength(value.Length);
		}
	}
}
