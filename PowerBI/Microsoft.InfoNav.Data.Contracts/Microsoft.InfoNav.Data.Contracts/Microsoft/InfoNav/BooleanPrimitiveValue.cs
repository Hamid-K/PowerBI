using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000054 RID: 84
	[ImmutableObject(true)]
	public sealed class BooleanPrimitiveValue : PrimitiveValue<bool>
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00002FF5 File Offset: 0x000011F5
		private BooleanPrimitiveValue(bool value)
			: base(value)
		{
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00002FFE File Offset: 0x000011FE
		public override ConceptualPrimitiveType Type
		{
			get
			{
				return ConceptualPrimitiveType.Boolean;
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00003001 File Offset: 0x00001201
		public new static implicit operator BooleanPrimitiveValue(bool value)
		{
			if (!value)
			{
				return BooleanPrimitiveValue.False;
			}
			return BooleanPrimitiveValue.True;
		}

		// Token: 0x0400010F RID: 271
		internal static readonly BooleanPrimitiveValue True = new BooleanPrimitiveValue(true);

		// Token: 0x04000110 RID: 272
		internal static readonly BooleanPrimitiveValue False = new BooleanPrimitiveValue(false);
	}
}
