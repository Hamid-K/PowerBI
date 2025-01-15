using System;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A8 RID: 424
	internal class CsdlSemanticsDocumentation : CsdlSemanticsElement, IEdmDocumentation, IEdmDirectValueAnnotation, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x000163E3 File Offset: 0x000145E3
		public CsdlSemanticsDocumentation(CsdlDocumentation documentation, CsdlSemanticsModel model)
			: base(documentation)
		{
			this.documentation = documentation;
			this.model = model;
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x000163FA File Offset: 0x000145FA
		public string Summary
		{
			get
			{
				return this.documentation.Summary;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00016407 File Offset: 0x00014607
		public string Description
		{
			get
			{
				return this.documentation.LongDescription;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00016414 File Offset: 0x00014614
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0001641C File Offset: 0x0001461C
		public override CsdlElement Element
		{
			get
			{
				return this.documentation;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00016424 File Offset: 0x00014624
		public string NamespaceUri
		{
			get
			{
				return "http://schemas.microsoft.com/ado/2011/04/edm/documentation";
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0001642B File Offset: 0x0001462B
		public string Name
		{
			get
			{
				return "Documentation";
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00016432 File Offset: 0x00014632
		object IEdmDirectValueAnnotation.Value
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0400043A RID: 1082
		private readonly CsdlDocumentation documentation;

		// Token: 0x0400043B RID: 1083
		private readonly CsdlSemanticsModel model;
	}
}
