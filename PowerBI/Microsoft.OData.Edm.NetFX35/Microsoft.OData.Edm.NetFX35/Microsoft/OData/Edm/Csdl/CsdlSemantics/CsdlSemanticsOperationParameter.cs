using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001AE RID: 430
	internal class CsdlSemanticsOperationParameter : CsdlSemanticsElement, IEdmOperationParameter, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x00016F97 File Offset: 0x00015197
		public CsdlSemanticsOperationParameter(CsdlSemanticsOperation declaringOperation, CsdlOperationParameter parameter)
			: base(parameter)
		{
			this.parameter = parameter;
			this.declaringOperation = declaringOperation;
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00016FB9 File Offset: 0x000151B9
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringOperation.Model;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00016FC6 File Offset: 0x000151C6
		public override CsdlElement Element
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00016FCE File Offset: 0x000151CE
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsOperationParameter.ComputeTypeFunc, null);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00016FE2 File Offset: 0x000151E2
		public string Name
		{
			get
			{
				return this.parameter.Name;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00016FEF File Offset: 0x000151EF
		public IEdmOperation DeclaringOperation
		{
			get
			{
				return this.declaringOperation;
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00016FF7 File Offset: 0x000151F7
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringOperation.Context);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00017010 File Offset: 0x00015210
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.declaringOperation.Context, this.parameter.Type);
		}

		// Token: 0x04000460 RID: 1120
		private readonly CsdlSemanticsOperation declaringOperation;

		// Token: 0x04000461 RID: 1121
		private readonly CsdlOperationParameter parameter;

		// Token: 0x04000462 RID: 1122
		private readonly Cache<CsdlSemanticsOperationParameter, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsOperationParameter, IEdmTypeReference>();

		// Token: 0x04000463 RID: 1123
		private static readonly Func<CsdlSemanticsOperationParameter, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsOperationParameter me) => me.ComputeType();
	}
}
