using System;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001BE RID: 446
	internal class UnresolvedParameter : BadElement, IEdmOperationParameter, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement, IUnresolvedElement
	{
		// Token: 0x06000963 RID: 2403 RVA: 0x000194EC File Offset: 0x000176EC
		public UnresolvedParameter(IEdmOperation declaringOperation, string name, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedParameter, Strings.Bad_UnresolvedParameter(name))
			})
		{
			this.Name = name ?? string.Empty;
			this.DeclaringOperation = declaringOperation;
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001953D File Offset: 0x0001773D
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x00019545 File Offset: 0x00017745
		public string Name { get; private set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0001954E File Offset: 0x0001774E
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, UnresolvedParameter.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00019562 File Offset: 0x00017762
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0001956A File Offset: 0x0001776A
		public IEdmOperation DeclaringOperation { get; private set; }

		// Token: 0x06000969 RID: 2409 RVA: 0x00019573 File Offset: 0x00017773
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x0400049E RID: 1182
		private readonly Cache<UnresolvedParameter, IEdmTypeReference> type = new Cache<UnresolvedParameter, IEdmTypeReference>();

		// Token: 0x0400049F RID: 1183
		private static readonly Func<UnresolvedParameter, IEdmTypeReference> ComputeTypeFunc = (UnresolvedParameter me) => me.ComputeType();
	}
}
