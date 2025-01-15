using System;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000196 RID: 406
	internal class UnresolvedParameter : BadElement, IEdmOperationParameter, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IUnresolvedElement
	{
		// Token: 0x06000AFB RID: 2811 RVA: 0x0001E730 File Offset: 0x0001C930
		public UnresolvedParameter(IEdmOperation declaringOperation, string name, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedParameter, Strings.Bad_UnresolvedParameter(name))
			})
		{
			this.Name = name ?? string.Empty;
			this.DeclaringOperation = declaringOperation;
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0001E77F File Offset: 0x0001C97F
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x0001E787 File Offset: 0x0001C987
		public string Name { get; private set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0001E790 File Offset: 0x0001C990
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, UnresolvedParameter.ComputeTypeFunc, null);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0001E7A4 File Offset: 0x0001C9A4
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x0001E7AC File Offset: 0x0001C9AC
		public IEdmOperation DeclaringOperation { get; private set; }

		// Token: 0x06000B01 RID: 2817 RVA: 0x00008EE2 File Offset: 0x000070E2
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x04000663 RID: 1635
		private readonly Cache<UnresolvedParameter, IEdmTypeReference> type = new Cache<UnresolvedParameter, IEdmTypeReference>();

		// Token: 0x04000664 RID: 1636
		private static readonly Func<UnresolvedParameter, IEdmTypeReference> ComputeTypeFunc = (UnresolvedParameter me) => me.ComputeType();
	}
}
