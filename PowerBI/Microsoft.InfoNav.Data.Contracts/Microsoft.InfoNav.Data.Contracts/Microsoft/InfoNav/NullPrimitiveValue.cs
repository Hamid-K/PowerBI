using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000059 RID: 89
	public sealed class NullPrimitiveValue : PrimitiveValue
	{
		// Token: 0x06000163 RID: 355 RVA: 0x0000307F File Offset: 0x0000127F
		private NullPrimitiveValue()
		{
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00003087 File Offset: 0x00001287
		public override ConceptualPrimitiveType Type
		{
			get
			{
				return ConceptualPrimitiveType.Null;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000308A File Offset: 0x0000128A
		public override bool Equals(PrimitiveValue other)
		{
			return other == NullPrimitiveValue.Instance;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00003094 File Offset: 0x00001294
		public override int GetHashCode()
		{
			return 233684719;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000309B File Offset: 0x0000129B
		public override object GetValueAsObject()
		{
			return null;
		}

		// Token: 0x04000111 RID: 273
		internal static readonly NullPrimitiveValue Instance = new NullPrimitiveValue();
	}
}
