using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E91 RID: 7825
	public static class DbLength
	{
		// Token: 0x0600C169 RID: 49513 RVA: 0x0026E4C4 File Offset: 0x0026C6C4
		private static DBLENGTH GetLength(uint value)
		{
			return new DBLENGTH
			{
				value = (ulong)value
			};
		}

		// Token: 0x0600C16A RID: 49514 RVA: 0x0026E4E3 File Offset: 0x0026C6E3
		public static DBLENGTH GetLength(int value)
		{
			return DbLength.GetLength((uint)value);
		}

		// Token: 0x0600C16B RID: 49515 RVA: 0x0026E4EB File Offset: 0x0026C6EB
		public static DBLENGTH GetLength(string value)
		{
			return DbLength.GetLength(value.Length * 2);
		}

		// Token: 0x0600C16C RID: 49516 RVA: 0x0026E4FA File Offset: 0x0026C6FA
		public static DBLENGTH GetLength(byte[] value)
		{
			return DbLength.GetLength(value.Length);
		}

		// Token: 0x17002F3E RID: 12094
		// (get) Token: 0x0600C16D RID: 49517 RVA: 0x0026E504 File Offset: 0x0026C704
		public static DBLENGTH Zero
		{
			get
			{
				return DbLength.GetLength(0);
			}
		}

		// Token: 0x17002F3F RID: 12095
		// (get) Token: 0x0600C16E RID: 49518 RVA: 0x0026E50C File Offset: 0x0026C70C
		public static DBLENGTH One
		{
			get
			{
				return DbLength.GetLength(1);
			}
		}

		// Token: 0x17002F40 RID: 12096
		// (get) Token: 0x0600C16F RID: 49519 RVA: 0x0026E514 File Offset: 0x0026C714
		public static DBLENGTH Two
		{
			get
			{
				return DbLength.GetLength(2);
			}
		}

		// Token: 0x17002F41 RID: 12097
		// (get) Token: 0x0600C170 RID: 49520 RVA: 0x0026E51C File Offset: 0x0026C71C
		public static DBLENGTH Four
		{
			get
			{
				return DbLength.GetLength(4);
			}
		}

		// Token: 0x17002F42 RID: 12098
		// (get) Token: 0x0600C171 RID: 49521 RVA: 0x0026E524 File Offset: 0x0026C724
		public static DBLENGTH Eight
		{
			get
			{
				return DbLength.GetLength(8);
			}
		}

		// Token: 0x17002F43 RID: 12099
		// (get) Token: 0x0600C172 RID: 49522 RVA: 0x0026E52C File Offset: 0x0026C72C
		public static DBLENGTH Sixteen
		{
			get
			{
				return DbLength.GetLength(16);
			}
		}

		// Token: 0x17002F44 RID: 12100
		// (get) Token: 0x0600C173 RID: 49523 RVA: 0x0026E535 File Offset: 0x0026C735
		public static DBLENGTH MaxValue
		{
			get
			{
				return DBLENGTH.MaxValue;
			}
		}

		// Token: 0x17002F45 RID: 12101
		// (get) Token: 0x0600C174 RID: 49524 RVA: 0x0026E524 File Offset: 0x0026C724
		public static DBLENGTH Double
		{
			get
			{
				return DbLength.GetLength(8);
			}
		}

		// Token: 0x17002F46 RID: 12102
		// (get) Token: 0x0600C175 RID: 49525 RVA: 0x0026E53C File Offset: 0x0026C73C
		public unsafe static DBLENGTH Currency
		{
			get
			{
				return DbLength.GetLength(sizeof(Currency));
			}
		}

		// Token: 0x17002F47 RID: 12103
		// (get) Token: 0x0600C176 RID: 49526 RVA: 0x0026E52C File Offset: 0x0026C72C
		public static DBLENGTH Decimal
		{
			get
			{
				return DbLength.GetLength(16);
			}
		}

		// Token: 0x17002F48 RID: 12104
		// (get) Token: 0x0600C177 RID: 49527 RVA: 0x0026E549 File Offset: 0x0026C749
		public unsafe static DBLENGTH Variant
		{
			get
			{
				return DbLength.GetLength(sizeof(VARIANT));
			}
		}

		// Token: 0x17002F49 RID: 12105
		// (get) Token: 0x0600C178 RID: 49528 RVA: 0x0026E556 File Offset: 0x0026C756
		public static DBLENGTH Date
		{
			get
			{
				return DbLength.Double;
			}
		}

		// Token: 0x17002F4A RID: 12106
		// (get) Token: 0x0600C179 RID: 49529 RVA: 0x0026E55D File Offset: 0x0026C75D
		public unsafe static DBLENGTH DbDate
		{
			get
			{
				return DbLength.GetLength(sizeof(DBDATE));
			}
		}

		// Token: 0x17002F4B RID: 12107
		// (get) Token: 0x0600C17A RID: 49530 RVA: 0x0026E56A File Offset: 0x0026C76A
		public unsafe static DBLENGTH DbTime2
		{
			get
			{
				return DbLength.GetLength(sizeof(DBTIME2));
			}
		}

		// Token: 0x17002F4C RID: 12108
		// (get) Token: 0x0600C17B RID: 49531 RVA: 0x0026E577 File Offset: 0x0026C777
		public unsafe static DBLENGTH TimeStamp
		{
			get
			{
				return DbLength.GetLength(sizeof(DBTIMESTAMP));
			}
		}

		// Token: 0x17002F4D RID: 12109
		// (get) Token: 0x0600C17C RID: 49532 RVA: 0x0026E584 File Offset: 0x0026C784
		public unsafe static DBLENGTH TimeStampOffset
		{
			get
			{
				return DbLength.GetLength(sizeof(DBTIMESTAMPOFFSET));
			}
		}

		// Token: 0x17002F4E RID: 12110
		// (get) Token: 0x0600C17D RID: 49533 RVA: 0x0026E591 File Offset: 0x0026C791
		public static DBLENGTH Guid
		{
			get
			{
				return DbLength.Sixteen;
			}
		}

		// Token: 0x17002F4F RID: 12111
		// (get) Token: 0x0600C17E RID: 49534 RVA: 0x0026E598 File Offset: 0x0026C798
		public unsafe static DBLENGTH Numeric
		{
			get
			{
				return DbLength.GetLength(sizeof(NUMERIC));
			}
		}

		// Token: 0x17002F50 RID: 12112
		// (get) Token: 0x0600C17F RID: 49535 RVA: 0x0026E514 File Offset: 0x0026C714
		public static DBLENGTH VariantBool
		{
			get
			{
				return DbLength.GetLength(2);
			}
		}

		// Token: 0x17002F51 RID: 12113
		// (get) Token: 0x0600C180 RID: 49536 RVA: 0x0026E51C File Offset: 0x0026C71C
		public static DBLENGTH Error
		{
			get
			{
				return DbLength.GetLength(4);
			}
		}

		// Token: 0x17002F52 RID: 12114
		// (get) Token: 0x0600C181 RID: 49537 RVA: 0x0026E5A5 File Offset: 0x0026C7A5
		public unsafe static DBLENGTH Duration
		{
			get
			{
				return DbLength.GetLength(sizeof(DBDURATION));
			}
		}

		// Token: 0x17002F53 RID: 12115
		// (get) Token: 0x0600C182 RID: 49538 RVA: 0x0026E5B2 File Offset: 0x0026C7B2
		public static DBLENGTH Pointer
		{
			get
			{
				return DbLength.GetLength(IntPtr.Size);
			}
		}
	}
}
