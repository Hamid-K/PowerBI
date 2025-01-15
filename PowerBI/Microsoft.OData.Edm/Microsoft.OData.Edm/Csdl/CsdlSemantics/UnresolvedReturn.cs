using System;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A7 RID: 423
	internal class UnresolvedReturn : BadElement, IEdmOperationReturn, IEdmElement, IEdmVocabularyAnnotatable, IUnresolvedElement
	{
		// Token: 0x06000BD4 RID: 3028 RVA: 0x00020EEC File Offset: 0x0001F0EC
		public UnresolvedReturn(IEdmOperation declaringOperation, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedReturn, Strings.Bad_UnresolvedReturn(declaringOperation.Name))
			})
		{
			this.DeclaringOperation = declaringOperation;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x00020F25 File Offset: 0x0001F125
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, UnresolvedReturn.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00020F39 File Offset: 0x0001F139
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x00020F41 File Offset: 0x0001F141
		public IEdmOperation DeclaringOperation { get; private set; }

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00004C9B File Offset: 0x00002E9B
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x040006EF RID: 1775
		private readonly Cache<UnresolvedReturn, IEdmTypeReference> type = new Cache<UnresolvedReturn, IEdmTypeReference>();

		// Token: 0x040006F0 RID: 1776
		private static readonly Func<UnresolvedReturn, IEdmTypeReference> ComputeTypeFunc = (UnresolvedReturn me) => me.ComputeType();
	}
}
