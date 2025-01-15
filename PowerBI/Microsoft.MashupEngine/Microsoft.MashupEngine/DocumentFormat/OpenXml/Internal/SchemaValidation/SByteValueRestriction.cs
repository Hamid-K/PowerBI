using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003131 RID: 12593
	[Serializable]
	internal class SByteValueRestriction : SimpleValueRestriction<sbyte, SByteValue>
	{
		// Token: 0x17009963 RID: 39267
		// (get) Token: 0x0601B511 RID: 111889 RVA: 0x0037643F File Offset: 0x0037463F
		protected override sbyte MinValue
		{
			get
			{
				return sbyte.MinValue;
			}
		}

		// Token: 0x17009964 RID: 39268
		// (get) Token: 0x0601B512 RID: 111890 RVA: 0x00376443 File Offset: 0x00374643
		protected override sbyte MaxValue
		{
			get
			{
				return sbyte.MaxValue;
			}
		}

		// Token: 0x17009965 RID: 39269
		// (get) Token: 0x0601B513 RID: 111891 RVA: 0x000E78AE File Offset: 0x000E5AAE
		// (set) Token: 0x0601B514 RID: 111892 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Byte;
			}
			set
			{
			}
		}
	}
}
