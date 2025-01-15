using System;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A6 RID: 422
	internal class UnresolvedParameter : BadElement, IEdmOperationParameter, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IUnresolvedElement
	{
		// Token: 0x06000BCC RID: 3020 RVA: 0x00020E50 File Offset: 0x0001F050
		public UnresolvedParameter(IEdmOperation declaringOperation, string name, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedParameter, Strings.Bad_UnresolvedParameter(name))
			})
		{
			this.Name = name ?? string.Empty;
			this.DeclaringOperation = declaringOperation;
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00020E9F File Offset: 0x0001F09F
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x00020EA7 File Offset: 0x0001F0A7
		public string Name { get; private set; }

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00020EB0 File Offset: 0x0001F0B0
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, UnresolvedParameter.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00020EC4 File Offset: 0x0001F0C4
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x00020ECC File Offset: 0x0001F0CC
		public IEdmOperation DeclaringOperation { get; private set; }

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00004C9B File Offset: 0x00002E9B
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x040006EB RID: 1771
		private readonly Cache<UnresolvedParameter, IEdmTypeReference> type = new Cache<UnresolvedParameter, IEdmTypeReference>();

		// Token: 0x040006EC RID: 1772
		private static readonly Func<UnresolvedParameter, IEdmTypeReference> ComputeTypeFunc = (UnresolvedParameter me) => me.ComputeType();
	}
}
