using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001CE RID: 462
	internal class CsdlAnnotations
	{
		// Token: 0x06000CA4 RID: 3236 RVA: 0x00023A6B File Offset: 0x00021C6B
		public CsdlAnnotations(IEnumerable<CsdlAnnotation> annotations, string target, string qualifier)
		{
			this.annotations = new List<CsdlAnnotation>(annotations);
			this.target = target;
			this.qualifier = qualifier;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x00023A8D File Offset: 0x00021C8D
		public IEnumerable<CsdlAnnotation> Annotations
		{
			get
			{
				return this.annotations;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00023A95 File Offset: 0x00021C95
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00023A9D File Offset: 0x00021C9D
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x040006E1 RID: 1761
		private readonly List<CsdlAnnotation> annotations;

		// Token: 0x040006E2 RID: 1762
		private readonly string target;

		// Token: 0x040006E3 RID: 1763
		private readonly string qualifier;
	}
}
