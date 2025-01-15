using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000098 RID: 152
	internal class CsdlSemanticsAnnotations
	{
		// Token: 0x0600029F RID: 671 RVA: 0x00006858 File Offset: 0x00004A58
		public CsdlSemanticsAnnotations(CsdlSemanticsSchema context, CsdlAnnotations annotations)
		{
			this.context = context;
			this.annotations = annotations;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000686E File Offset: 0x00004A6E
		public CsdlSemanticsSchema Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00006876 File Offset: 0x00004A76
		public CsdlAnnotations Annotations
		{
			get
			{
				return this.annotations;
			}
		}

		// Token: 0x0400010E RID: 270
		private readonly CsdlAnnotations annotations;

		// Token: 0x0400010F RID: 271
		private readonly CsdlSemanticsSchema context;
	}
}
