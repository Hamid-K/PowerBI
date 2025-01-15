using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000189 RID: 393
	internal class CsdlSemanticsDocumentation : CsdlSemanticsElement, IEdmDocumentation, IEdmDirectValueAnnotation, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000A6C RID: 2668 RVA: 0x0001C234 File Offset: 0x0001A434
		public CsdlSemanticsDocumentation(CsdlDocumentation documentation, CsdlSemanticsModel model)
			: base(documentation)
		{
			this.documentation = documentation;
			this.model = model;
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0001C24B File Offset: 0x0001A44B
		public string Summary
		{
			get
			{
				return this.documentation.Summary;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0001C258 File Offset: 0x0001A458
		public string Description
		{
			get
			{
				return this.documentation.LongDescription;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0001C265 File Offset: 0x0001A465
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0001C26D File Offset: 0x0001A46D
		public override CsdlElement Element
		{
			get
			{
				return this.documentation;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0001C275 File Offset: 0x0001A475
		public string NamespaceUri
		{
			get
			{
				return "http://schemas.microsoft.com/ado/2011/04/edm/documentation";
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0001C27C File Offset: 0x0001A47C
		public string Name
		{
			get
			{
				return "Documentation";
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0001402B File Offset: 0x0001222B
		object IEdmDirectValueAnnotation.Value
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000622 RID: 1570
		private readonly CsdlDocumentation documentation;

		// Token: 0x04000623 RID: 1571
		private readonly CsdlSemanticsModel model;
	}
}
