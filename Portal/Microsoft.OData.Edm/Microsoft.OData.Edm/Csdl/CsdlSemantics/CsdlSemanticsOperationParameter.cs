using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200019E RID: 414
	internal class CsdlSemanticsOperationParameter : CsdlSemanticsElement, IEdmOperationParameter, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x0001F530 File Offset: 0x0001D730
		public CsdlSemanticsOperationParameter(CsdlSemanticsOperation declaringOperation, CsdlOperationParameter parameter)
			: base(parameter)
		{
			this.parameter = parameter;
			this.declaringOperation = declaringOperation;
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0001F552 File Offset: 0x0001D752
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringOperation.Model;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0001F55F File Offset: 0x0001D75F
		public override CsdlElement Element
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0001F567 File Offset: 0x0001D767
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsOperationParameter.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0001F57B File Offset: 0x0001D77B
		public string Name
		{
			get
			{
				return this.parameter.Name;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0001F588 File Offset: 0x0001D788
		public IEdmOperation DeclaringOperation
		{
			get
			{
				return this.declaringOperation;
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0001F590 File Offset: 0x0001D790
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringOperation.Context);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0001F5A9 File Offset: 0x0001D7A9
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.declaringOperation.Context, this.parameter.Type);
		}

		// Token: 0x040006C8 RID: 1736
		private readonly CsdlSemanticsOperation declaringOperation;

		// Token: 0x040006C9 RID: 1737
		private readonly CsdlOperationParameter parameter;

		// Token: 0x040006CA RID: 1738
		private readonly Cache<CsdlSemanticsOperationParameter, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsOperationParameter, IEdmTypeReference>();

		// Token: 0x040006CB RID: 1739
		private static readonly Func<CsdlSemanticsOperationParameter, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsOperationParameter me) => me.ComputeType();
	}
}
