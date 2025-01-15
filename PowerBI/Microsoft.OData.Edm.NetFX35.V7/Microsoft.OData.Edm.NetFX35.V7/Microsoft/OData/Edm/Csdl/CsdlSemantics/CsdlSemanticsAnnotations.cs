using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000170 RID: 368
	internal class CsdlSemanticsAnnotations
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x0001AB50 File Offset: 0x00018D50
		public CsdlSemanticsAnnotations(CsdlSemanticsSchema context, CsdlAnnotations annotations)
		{
			this.context = context;
			this.annotations = annotations;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0001AB66 File Offset: 0x00018D66
		public CsdlSemanticsSchema Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0001AB6E File Offset: 0x00018D6E
		public CsdlAnnotations Annotations
		{
			get
			{
				return this.annotations;
			}
		}

		// Token: 0x040005CF RID: 1487
		private readonly CsdlAnnotations annotations;

		// Token: 0x040005D0 RID: 1488
		private readonly CsdlSemanticsSchema context;
	}
}
