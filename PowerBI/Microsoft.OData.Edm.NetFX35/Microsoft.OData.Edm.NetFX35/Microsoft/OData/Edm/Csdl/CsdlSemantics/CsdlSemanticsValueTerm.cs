using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000CC RID: 204
	[DebuggerDisplay("CsdlSemanticsValueTerm({Name})")]
	internal class CsdlSemanticsValueTerm : CsdlSemanticsElement, IEdmValueTerm, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000376 RID: 886 RVA: 0x00008028 File Offset: 0x00006228
		public CsdlSemanticsValueTerm(CsdlSemanticsSchema context, CsdlTerm valueTerm)
			: base(valueTerm)
		{
			this.Context = context;
			this.valueTerm = valueTerm;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000804A File Offset: 0x0000624A
		public string Name
		{
			get
			{
				return this.valueTerm.Name;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00008057 File Offset: 0x00006257
		public string Namespace
		{
			get
			{
				return this.Context.Namespace;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00008064 File Offset: 0x00006264
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.ValueTerm;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00008067 File Offset: 0x00006267
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000806A File Offset: 0x0000626A
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsValueTerm.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000807E File Offset: 0x0000627E
		public string AppliesTo
		{
			get
			{
				return this.valueTerm.AppliesTo;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000808B File Offset: 0x0000628B
		public string DefaultValue
		{
			get
			{
				return this.valueTerm.DefaultValue;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00008098 File Offset: 0x00006298
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.Context.Model;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000080A5 File Offset: 0x000062A5
		public override CsdlElement Element
		{
			get
			{
				return this.valueTerm;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000080AD File Offset: 0x000062AD
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000080C1 File Offset: 0x000062C1
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.Context, this.valueTerm.Type);
		}

		// Token: 0x04000176 RID: 374
		protected readonly CsdlSemanticsSchema Context;

		// Token: 0x04000177 RID: 375
		protected CsdlTerm valueTerm;

		// Token: 0x04000178 RID: 376
		private readonly Cache<CsdlSemanticsValueTerm, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsValueTerm, IEdmTypeReference>();

		// Token: 0x04000179 RID: 377
		private static readonly Func<CsdlSemanticsValueTerm, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsValueTerm me) => me.ComputeType();
	}
}
