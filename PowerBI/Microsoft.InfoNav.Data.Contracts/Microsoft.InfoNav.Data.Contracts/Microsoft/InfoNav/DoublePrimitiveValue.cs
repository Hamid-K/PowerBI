using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000057 RID: 87
	[ImmutableObject(true)]
	public sealed class DoublePrimitiveValue : PrimitiveValue<double>
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00003057 File Offset: 0x00001257
		internal DoublePrimitiveValue(double value)
			: base(value)
		{
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00003060 File Offset: 0x00001260
		public override ConceptualPrimitiveType Type
		{
			get
			{
				return ConceptualPrimitiveType.Double;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003063 File Offset: 0x00001263
		public new static implicit operator DoublePrimitiveValue(double value)
		{
			return new DoublePrimitiveValue(value);
		}
	}
}
