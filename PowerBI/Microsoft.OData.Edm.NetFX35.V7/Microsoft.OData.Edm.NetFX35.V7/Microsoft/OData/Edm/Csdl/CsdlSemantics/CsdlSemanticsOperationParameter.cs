using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200018F RID: 399
	internal class CsdlSemanticsOperationParameter : CsdlSemanticsElement, IEdmOperationParameter, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001CE24 File Offset: 0x0001B024
		public CsdlSemanticsOperationParameter(CsdlSemanticsOperation declaringOperation, CsdlOperationParameter parameter)
			: base(parameter)
		{
			this.parameter = parameter;
			this.declaringOperation = declaringOperation;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0001CE46 File Offset: 0x0001B046
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringOperation.Model;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0001CE53 File Offset: 0x0001B053
		public override CsdlElement Element
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0001CE5B File Offset: 0x0001B05B
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsOperationParameter.ComputeTypeFunc, null);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0001CE6F File Offset: 0x0001B06F
		public string Name
		{
			get
			{
				return this.parameter.Name;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0001CE7C File Offset: 0x0001B07C
		public IEdmOperation DeclaringOperation
		{
			get
			{
				return this.declaringOperation;
			}
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0001CE84 File Offset: 0x0001B084
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringOperation.Context);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001CE9D File Offset: 0x0001B09D
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.declaringOperation.Context, this.parameter.Type);
		}

		// Token: 0x04000645 RID: 1605
		private readonly CsdlSemanticsOperation declaringOperation;

		// Token: 0x04000646 RID: 1606
		private readonly CsdlOperationParameter parameter;

		// Token: 0x04000647 RID: 1607
		private readonly Cache<CsdlSemanticsOperationParameter, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsOperationParameter, IEdmTypeReference>();

		// Token: 0x04000648 RID: 1608
		private static readonly Func<CsdlSemanticsOperationParameter, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsOperationParameter me) => me.ComputeType();
	}
}
