using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003134 RID: 12596
	[Serializable]
	internal class Int64ValueRestriction : SimpleValueRestriction<long, Int64Value>
	{
		// Token: 0x1700996C RID: 39276
		// (get) Token: 0x0601B520 RID: 111904 RVA: 0x00376474 File Offset: 0x00374674
		protected override long MinValue
		{
			get
			{
				return long.MinValue;
			}
		}

		// Token: 0x1700996D RID: 39277
		// (get) Token: 0x0601B521 RID: 111905 RVA: 0x0037647F File Offset: 0x0037467F
		protected override long MaxValue
		{
			get
			{
				return long.MaxValue;
			}
		}

		// Token: 0x1700996E RID: 39278
		// (get) Token: 0x0601B522 RID: 111906 RVA: 0x0014213C File Offset: 0x0014033C
		// (set) Token: 0x0601B523 RID: 111907 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Long;
			}
			set
			{
			}
		}
	}
}
