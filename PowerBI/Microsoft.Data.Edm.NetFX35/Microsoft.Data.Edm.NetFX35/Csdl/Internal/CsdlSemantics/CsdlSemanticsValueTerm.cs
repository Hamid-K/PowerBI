using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Internal;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x020000A3 RID: 163
	internal class CsdlSemanticsValueTerm : CsdlSemanticsElement, IEdmValueTerm, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060002AA RID: 682 RVA: 0x00006D34 File Offset: 0x00004F34
		public CsdlSemanticsValueTerm(CsdlSemanticsSchema context, CsdlValueTerm valueTerm)
			: base(valueTerm)
		{
			this.Context = context;
			this.valueTerm = valueTerm;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00006D56 File Offset: 0x00004F56
		public string Name
		{
			get
			{
				return this.valueTerm.Name;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00006D63 File Offset: 0x00004F63
		public string Namespace
		{
			get
			{
				return this.Context.Namespace;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00006D70 File Offset: 0x00004F70
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.ValueTerm;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00006D73 File Offset: 0x00004F73
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00006D76 File Offset: 0x00004F76
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsValueTerm.ComputeTypeFunc, null);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00006D8A File Offset: 0x00004F8A
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.Context.Model;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00006D97 File Offset: 0x00004F97
		public override CsdlElement Element
		{
			get
			{
				return this.valueTerm;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00006D9F File Offset: 0x00004F9F
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00006DB3 File Offset: 0x00004FB3
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.Context, this.valueTerm.Type);
		}

		// Token: 0x04000135 RID: 309
		protected readonly CsdlSemanticsSchema Context;

		// Token: 0x04000136 RID: 310
		protected CsdlValueTerm valueTerm;

		// Token: 0x04000137 RID: 311
		private readonly Cache<CsdlSemanticsValueTerm, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsValueTerm, IEdmTypeReference>();

		// Token: 0x04000138 RID: 312
		private static readonly Func<CsdlSemanticsValueTerm, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsValueTerm me) => me.ComputeType();
	}
}
