using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000193 RID: 403
	[DebuggerDisplay("CsdlSemanticsTerm({Name})")]
	internal class CsdlSemanticsTerm : CsdlSemanticsElement, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x06000B0C RID: 2828 RVA: 0x0001E230 File Offset: 0x0001C430
		public CsdlSemanticsTerm(CsdlSemanticsSchema context, CsdlTerm valueTerm)
			: base(valueTerm)
		{
			this.Context = context;
			this.term = valueTerm;
			CsdlSemanticsSchema context2 = this.Context;
			string text = ((context2 != null) ? context2.Namespace : null);
			CsdlTerm csdlTerm = this.term;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(text, (csdlTerm != null) ? csdlTerm.Name : null);
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0001E28C File Offset: 0x0001C48C
		public string Name
		{
			get
			{
				return this.term.Name;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0001E299 File Offset: 0x0001C499
		public string Namespace
		{
			get
			{
				return this.Context.Namespace;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0001E2A6 File Offset: 0x0001C4A6
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00002732 File Offset: 0x00000932
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Term;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0001E2AE File Offset: 0x0001C4AE
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsTerm.ComputeTypeFunc, null);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0001E2C2 File Offset: 0x0001C4C2
		public string AppliesTo
		{
			get
			{
				return this.term.AppliesTo;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0001E2CF File Offset: 0x0001C4CF
		public string DefaultValue
		{
			get
			{
				return this.term.DefaultValue;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0001E2DC File Offset: 0x0001C4DC
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.Context.Model;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0001E2E9 File Offset: 0x0001C4E9
		public override CsdlElement Element
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0001E2F1 File Offset: 0x0001C4F1
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001E305 File Offset: 0x0001C505
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.Context, this.term.Type);
		}

		// Token: 0x04000695 RID: 1685
		protected readonly CsdlSemanticsSchema Context;

		// Token: 0x04000696 RID: 1686
		private readonly string fullName;

		// Token: 0x04000697 RID: 1687
		protected CsdlTerm term;

		// Token: 0x04000698 RID: 1688
		private readonly Cache<CsdlSemanticsTerm, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsTerm, IEdmTypeReference>();

		// Token: 0x04000699 RID: 1689
		private static readonly Func<CsdlSemanticsTerm, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsTerm me) => me.ComputeType();
	}
}
