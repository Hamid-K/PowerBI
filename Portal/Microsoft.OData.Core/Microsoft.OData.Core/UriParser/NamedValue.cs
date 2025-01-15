using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000149 RID: 329
	public sealed class NamedValue
	{
		// Token: 0x060010EE RID: 4334 RVA: 0x0002FB54 File Offset: 0x0002DD54
		public NamedValue(string name, LiteralToken value)
		{
			ExceptionUtils.CheckArgumentNotNull<LiteralToken>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0002FB76 File Offset: 0x0002DD76
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0002FB7E File Offset: 0x0002DD7E
		public LiteralToken Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040007DA RID: 2010
		private readonly string name;

		// Token: 0x040007DB RID: 2011
		private readonly LiteralToken value;
	}
}
