using System;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001EE RID: 494
	internal sealed class NamedValue
	{
		// Token: 0x060011F3 RID: 4595 RVA: 0x00041128 File Offset: 0x0003F328
		public NamedValue(string name, LiteralToken value)
		{
			ExceptionUtils.CheckArgumentNotNull<LiteralToken>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00041149 File Offset: 0x0003F349
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00041151 File Offset: 0x0003F351
		public LiteralToken Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040007B6 RID: 1974
		private readonly string name;

		// Token: 0x040007B7 RID: 1975
		private readonly LiteralToken value;
	}
}
