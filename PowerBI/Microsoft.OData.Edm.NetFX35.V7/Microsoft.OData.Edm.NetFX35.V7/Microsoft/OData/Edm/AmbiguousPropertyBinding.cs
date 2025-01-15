using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000024 RID: 36
	internal class AmbiguousPropertyBinding : AmbiguousBinding<IEdmProperty>, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00008EA7 File Offset: 0x000070A7
		public AmbiguousPropertyBinding(IEdmStructuredType declaringType, IEdmProperty first, IEdmProperty second)
			: base(first, second)
		{
			this.declaringType = declaringType;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00008EC3 File Offset: 0x000070C3
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.None;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00008EC6 File Offset: 0x000070C6
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, AmbiguousPropertyBinding.ComputeTypeFunc, null);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00008EDA File Offset: 0x000070DA
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008EE2 File Offset: 0x000070E2
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x0400003D RID: 61
		private readonly IEdmStructuredType declaringType;

		// Token: 0x0400003E RID: 62
		private readonly Cache<AmbiguousPropertyBinding, IEdmTypeReference> type = new Cache<AmbiguousPropertyBinding, IEdmTypeReference>();

		// Token: 0x0400003F RID: 63
		private static readonly Func<AmbiguousPropertyBinding, IEdmTypeReference> ComputeTypeFunc = (AmbiguousPropertyBinding me) => me.ComputeType();
	}
}
