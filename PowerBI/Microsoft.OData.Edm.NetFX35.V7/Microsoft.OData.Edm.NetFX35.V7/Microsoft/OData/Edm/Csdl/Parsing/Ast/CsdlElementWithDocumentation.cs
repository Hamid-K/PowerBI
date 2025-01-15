using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E0 RID: 480
	internal abstract class CsdlElementWithDocumentation : CsdlElement
	{
		// Token: 0x06000CD9 RID: 3289 RVA: 0x00023E0C File Offset: 0x0002200C
		public CsdlElementWithDocumentation(CsdlDocumentation documentation, CsdlLocation location)
			: base(location)
		{
			this.documentation = documentation;
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00023E1C File Offset: 0x0002201C
		public CsdlDocumentation Documentation
		{
			get
			{
				return this.documentation;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00023E24 File Offset: 0x00022024
		public override bool HasDirectValueAnnotations
		{
			get
			{
				return this.documentation != null || base.HasDirectValueAnnotations;
			}
		}

		// Token: 0x040006FA RID: 1786
		private readonly CsdlDocumentation documentation;
	}
}
