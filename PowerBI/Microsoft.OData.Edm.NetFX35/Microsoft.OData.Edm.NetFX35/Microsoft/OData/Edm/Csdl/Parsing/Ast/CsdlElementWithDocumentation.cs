using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200001C RID: 28
	internal abstract class CsdlElementWithDocumentation : CsdlElement
	{
		// Token: 0x06000090 RID: 144 RVA: 0x0000341B File Offset: 0x0000161B
		public CsdlElementWithDocumentation(CsdlDocumentation documentation, CsdlLocation location)
			: base(location)
		{
			this.documentation = documentation;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000342B File Offset: 0x0000162B
		public CsdlDocumentation Documentation
		{
			get
			{
				return this.documentation;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003433 File Offset: 0x00001633
		public override bool HasDirectValueAnnotations
		{
			get
			{
				return this.documentation != null || base.HasDirectValueAnnotations;
			}
		}

		// Token: 0x0400002B RID: 43
		private readonly CsdlDocumentation documentation;
	}
}
