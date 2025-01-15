using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x0200005E RID: 94
	[ImmutableObject(true)]
	public sealed class TextPrimitiveValue : PrimitiveValue<string>
	{
		// Token: 0x0600017F RID: 383 RVA: 0x000032D0 File Offset: 0x000014D0
		internal TextPrimitiveValue(string value)
			: base(value)
		{
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000032D9 File Offset: 0x000014D9
		public override ConceptualPrimitiveType Type
		{
			get
			{
				return ConceptualPrimitiveType.Text;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000032DC File Offset: 0x000014DC
		public new static implicit operator TextPrimitiveValue(string value)
		{
			return new TextPrimitiveValue(value);
		}
	}
}
