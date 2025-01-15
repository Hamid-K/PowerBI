using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003135 RID: 12597
	[Serializable]
	internal class UInt16ValueRestriction : SimpleValueRestriction<ushort, UInt16Value>
	{
		// Token: 0x1700996F RID: 39279
		// (get) Token: 0x0601B525 RID: 111909 RVA: 0x00002105 File Offset: 0x00000305
		protected override ushort MinValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17009970 RID: 39280
		// (get) Token: 0x0601B526 RID: 111910 RVA: 0x0037372E File Offset: 0x0037192E
		protected override ushort MaxValue
		{
			get
			{
				return ushort.MaxValue;
			}
		}

		// Token: 0x17009971 RID: 39281
		// (get) Token: 0x0601B527 RID: 111911 RVA: 0x00227072 File Offset: 0x00225272
		// (set) Token: 0x0601B528 RID: 111912 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.UnsignedShort;
			}
			set
			{
			}
		}
	}
}
