using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200019F RID: 415
	internal class CsdlSemanticsOperationReturn : CsdlSemanticsElement, IEdmOperationReturn, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000B88 RID: 2952 RVA: 0x0001F5DD File Offset: 0x0001D7DD
		public CsdlSemanticsOperationReturn(CsdlSemanticsOperation declaringOperation, CsdlOperationReturn operationReturn)
			: base(operationReturn)
		{
			this.declaringOperation = declaringOperation;
			this.operationReturn = operationReturn;
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0001F5FF File Offset: 0x0001D7FF
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringOperation.Model;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0001F60C File Offset: 0x0001D80C
		public override CsdlElement Element
		{
			get
			{
				return this.operationReturn;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0001F614 File Offset: 0x0001D814
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsOperationReturn.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0001F628 File Offset: 0x0001D828
		public IEdmOperation DeclaringOperation
		{
			get
			{
				return this.declaringOperation;
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0001F630 File Offset: 0x0001D830
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringOperation.Context);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0001F649 File Offset: 0x0001D849
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.declaringOperation.Context, this.operationReturn.ReturnType);
		}

		// Token: 0x040006CC RID: 1740
		private readonly CsdlSemanticsOperation declaringOperation;

		// Token: 0x040006CD RID: 1741
		private readonly CsdlOperationReturn operationReturn;

		// Token: 0x040006CE RID: 1742
		private readonly Cache<CsdlSemanticsOperationReturn, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsOperationReturn, IEdmTypeReference>();

		// Token: 0x040006CF RID: 1743
		private static readonly Func<CsdlSemanticsOperationReturn, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsOperationReturn me) => me.ComputeType();
	}
}
