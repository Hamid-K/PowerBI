using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200313A RID: 12602
	[Serializable]
	internal class DecimalValueRestriction : SimpleValueRestriction<decimal, DecimalValue>
	{
		// Token: 0x1700997E RID: 39294
		// (get) Token: 0x0601B540 RID: 111936 RVA: 0x003764E2 File Offset: 0x003746E2
		protected override decimal MinValue
		{
			get
			{
				return decimal.MinValue;
			}
		}

		// Token: 0x1700997F RID: 39295
		// (get) Token: 0x0601B541 RID: 111937 RVA: 0x003764F2 File Offset: 0x003746F2
		protected override decimal MaxValue
		{
			get
			{
				return decimal.MaxValue;
			}
		}

		// Token: 0x17009980 RID: 39296
		// (get) Token: 0x0601B542 RID: 111938 RVA: 0x00243592 File Offset: 0x00241792
		// (set) Token: 0x0601B543 RID: 111939 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Decimal;
			}
			set
			{
			}
		}
	}
}
