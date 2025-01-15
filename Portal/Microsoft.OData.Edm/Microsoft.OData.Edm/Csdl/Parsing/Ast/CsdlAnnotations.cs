using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001DD RID: 477
	internal class CsdlAnnotations
	{
		// Token: 0x06000D59 RID: 3417 RVA: 0x00025C30 File Offset: 0x00023E30
		public CsdlAnnotations(IEnumerable<CsdlAnnotation> annotations, string target, string qualifier)
		{
			this.annotations = new List<CsdlAnnotation>(annotations);
			this.target = target;
			this.qualifier = qualifier;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00025C52 File Offset: 0x00023E52
		public IEnumerable<CsdlAnnotation> Annotations
		{
			get
			{
				return this.annotations;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x00025C5A File Offset: 0x00023E5A
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00025C62 File Offset: 0x00023E62
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x0400075A RID: 1882
		private readonly List<CsdlAnnotation> annotations;

		// Token: 0x0400075B RID: 1883
		private readonly string target;

		// Token: 0x0400075C RID: 1884
		private readonly string qualifier;
	}
}
