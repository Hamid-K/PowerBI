using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003133 RID: 12595
	[Serializable]
	internal class Int32ValueRestriction : SimpleValueRestriction<int, Int32Value>
	{
		// Token: 0x17009969 RID: 39273
		// (get) Token: 0x0601B51B RID: 111899 RVA: 0x00376465 File Offset: 0x00374665
		protected override int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x1700996A RID: 39274
		// (get) Token: 0x0601B51C RID: 111900 RVA: 0x00178ECC File Offset: 0x001770CC
		protected override int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700996B RID: 39275
		// (get) Token: 0x0601B51D RID: 111901 RVA: 0x00140DB6 File Offset: 0x0013EFB6
		// (set) Token: 0x0601B51E RID: 111902 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Int;
			}
			set
			{
			}
		}
	}
}
