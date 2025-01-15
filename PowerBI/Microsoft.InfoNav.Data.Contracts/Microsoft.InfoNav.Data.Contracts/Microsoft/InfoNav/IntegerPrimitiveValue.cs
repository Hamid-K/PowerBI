using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000058 RID: 88
	[ImmutableObject(true)]
	public sealed class IntegerPrimitiveValue : PrimitiveValue<long>
	{
		// Token: 0x06000160 RID: 352 RVA: 0x0000306B File Offset: 0x0000126B
		internal IntegerPrimitiveValue(long value)
			: base(value)
		{
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00003074 File Offset: 0x00001274
		public override ConceptualPrimitiveType Type
		{
			get
			{
				return ConceptualPrimitiveType.Integer;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00003077 File Offset: 0x00001277
		public new static implicit operator IntegerPrimitiveValue(long value)
		{
			return new IntegerPrimitiveValue(value);
		}
	}
}
