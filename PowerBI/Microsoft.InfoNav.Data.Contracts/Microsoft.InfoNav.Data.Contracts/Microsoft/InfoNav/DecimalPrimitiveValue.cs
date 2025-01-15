using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000056 RID: 86
	[ImmutableObject(true)]
	public sealed class DecimalPrimitiveValue : PrimitiveValue<decimal>
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00003043 File Offset: 0x00001243
		internal DecimalPrimitiveValue(decimal value)
			: base(value)
		{
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000304C File Offset: 0x0000124C
		public override ConceptualPrimitiveType Type
		{
			get
			{
				return ConceptualPrimitiveType.Decimal;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000304F File Offset: 0x0000124F
		public new static implicit operator DecimalPrimitiveValue(decimal value)
		{
			return new DecimalPrimitiveValue(value);
		}
	}
}
