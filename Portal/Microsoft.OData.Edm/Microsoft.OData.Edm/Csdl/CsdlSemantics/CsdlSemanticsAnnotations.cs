using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200017F RID: 383
	internal class CsdlSemanticsAnnotations
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x0001CC98 File Offset: 0x0001AE98
		public CsdlSemanticsAnnotations(CsdlSemanticsSchema context, CsdlAnnotations annotations)
		{
			this.context = context;
			this.annotations = annotations;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0001CCAE File Offset: 0x0001AEAE
		public CsdlSemanticsSchema Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0001CCB6 File Offset: 0x0001AEB6
		public CsdlAnnotations Annotations
		{
			get
			{
				return this.annotations;
			}
		}

		// Token: 0x0400064B RID: 1611
		private readonly CsdlAnnotations annotations;

		// Token: 0x0400064C RID: 1612
		private readonly CsdlSemanticsSchema context;
	}
}
