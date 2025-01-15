using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000034 RID: 52
	internal class CsdlAnnotations
	{
		// Token: 0x060000DE RID: 222 RVA: 0x000037EF File Offset: 0x000019EF
		public CsdlAnnotations(IEnumerable<CsdlAnnotation> annotations, string target, string qualifier)
		{
			this.annotations = new List<CsdlAnnotation>(annotations);
			this.target = target;
			this.qualifier = qualifier;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003811 File Offset: 0x00001A11
		public IEnumerable<CsdlAnnotation> Annotations
		{
			get
			{
				return this.annotations;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003819 File Offset: 0x00001A19
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003821 File Offset: 0x00001A21
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x0400004F RID: 79
		private readonly List<CsdlAnnotation> annotations;

		// Token: 0x04000050 RID: 80
		private readonly string target;

		// Token: 0x04000051 RID: 81
		private readonly string qualifier;
	}
}
