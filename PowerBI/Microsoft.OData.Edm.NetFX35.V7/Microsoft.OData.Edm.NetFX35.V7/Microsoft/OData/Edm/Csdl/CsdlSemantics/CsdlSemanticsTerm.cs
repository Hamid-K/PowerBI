using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001AA RID: 426
	[DebuggerDisplay("CsdlSemanticsTerm({Name})")]
	internal class CsdlSemanticsTerm : CsdlSemanticsElement, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x0001FB9C File Offset: 0x0001DD9C
		public CsdlSemanticsTerm(CsdlSemanticsSchema context, CsdlTerm valueTerm)
			: base(valueTerm)
		{
			this.Context = context;
			this.term = valueTerm;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0001FBBE File Offset: 0x0001DDBE
		public string Name
		{
			get
			{
				return this.term.Name;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0001FBCB File Offset: 0x0001DDCB
		public string Namespace
		{
			get
			{
				return this.Context.Namespace;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00008F68 File Offset: 0x00007168
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Term;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0001FBD8 File Offset: 0x0001DDD8
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsTerm.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0001FBEC File Offset: 0x0001DDEC
		public string AppliesTo
		{
			get
			{
				return this.term.AppliesTo;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0001FBF9 File Offset: 0x0001DDF9
		public string DefaultValue
		{
			get
			{
				return this.term.DefaultValue;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0001FC06 File Offset: 0x0001DE06
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.Context.Model;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0001FC13 File Offset: 0x0001DE13
		public override CsdlElement Element
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0001FC1B File Offset: 0x0001DE1B
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0001FC2F File Offset: 0x0001DE2F
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.Context, this.term.Type);
		}

		// Token: 0x04000694 RID: 1684
		protected readonly CsdlSemanticsSchema Context;

		// Token: 0x04000695 RID: 1685
		protected CsdlTerm term;

		// Token: 0x04000696 RID: 1686
		private readonly Cache<CsdlSemanticsTerm, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsTerm, IEdmTypeReference>();

		// Token: 0x04000697 RID: 1687
		private static readonly Func<CsdlSemanticsTerm, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsTerm me) => me.ComputeType();
	}
}
