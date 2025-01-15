using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003132 RID: 12594
	[Serializable]
	internal class Int16ValueRestriction : SimpleValueRestriction<short, Int16Value>
	{
		// Token: 0x17009966 RID: 39270
		// (get) Token: 0x0601B516 RID: 111894 RVA: 0x0037644F File Offset: 0x0037464F
		protected override short MinValue
		{
			get
			{
				return short.MinValue;
			}
		}

		// Token: 0x17009967 RID: 39271
		// (get) Token: 0x0601B517 RID: 111895 RVA: 0x00376456 File Offset: 0x00374656
		protected override short MaxValue
		{
			get
			{
				return short.MaxValue;
			}
		}

		// Token: 0x17009968 RID: 39272
		// (get) Token: 0x0601B518 RID: 111896 RVA: 0x001AA8D9 File Offset: 0x001A8AD9
		// (set) Token: 0x0601B519 RID: 111897 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Short;
			}
			set
			{
			}
		}
	}
}
