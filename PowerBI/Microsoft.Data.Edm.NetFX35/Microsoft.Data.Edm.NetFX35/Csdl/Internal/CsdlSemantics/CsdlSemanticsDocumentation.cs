using System;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x0200016B RID: 363
	internal class CsdlSemanticsDocumentation : CsdlSemanticsElement, IEdmDocumentation, IEdmDirectValueAnnotation, IEdmNamedElement, IEdmElement
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x00014C1B File Offset: 0x00012E1B
		public CsdlSemanticsDocumentation(CsdlDocumentation documentation, CsdlSemanticsModel model)
			: base(documentation)
		{
			this.documentation = documentation;
			this.model = model;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00014C32 File Offset: 0x00012E32
		public string Summary
		{
			get
			{
				return this.documentation.Summary;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00014C3F File Offset: 0x00012E3F
		public string Description
		{
			get
			{
				return this.documentation.LongDescription;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00014C4C File Offset: 0x00012E4C
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00014C54 File Offset: 0x00012E54
		public override CsdlElement Element
		{
			get
			{
				return this.documentation;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00014C5C File Offset: 0x00012E5C
		public string NamespaceUri
		{
			get
			{
				return "http://schemas.microsoft.com/ado/2011/04/edm/documentation";
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00014C63 File Offset: 0x00012E63
		public string Name
		{
			get
			{
				return "Documentation";
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00014C6A File Offset: 0x00012E6A
		object IEdmDirectValueAnnotation.Value
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040003D4 RID: 980
		private readonly CsdlDocumentation documentation;

		// Token: 0x040003D5 RID: 981
		private readonly CsdlSemanticsModel model;
	}
}
