using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000055 RID: 85
	[ImmutableObject(true)]
	public sealed class DateTimePrimitiveValue : PrimitiveValue<DateTime>
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00003029 File Offset: 0x00001229
		internal DateTimePrimitiveValue(DateTime value)
			: base(DateTime.SpecifyKind(value, DateTimeKind.Unspecified))
		{
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00003038 File Offset: 0x00001238
		public override ConceptualPrimitiveType Type
		{
			get
			{
				return ConceptualPrimitiveType.DateTime;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000303B File Offset: 0x0000123B
		public new static implicit operator DateTimePrimitiveValue(DateTime value)
		{
			return new DateTimePrimitiveValue(value);
		}
	}
}
