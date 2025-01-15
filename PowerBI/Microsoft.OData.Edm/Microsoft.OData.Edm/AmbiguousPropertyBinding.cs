using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004F RID: 79
	internal class AmbiguousPropertyBinding : AmbiguousBinding<IEdmProperty>, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00004C63 File Offset: 0x00002E63
		public AmbiguousPropertyBinding(IEdmStructuredType declaringType, IEdmProperty first, IEdmProperty second)
			: base(first, second)
		{
			this.declaringType = declaringType;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000026A6 File Offset: 0x000008A6
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.None;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00004C7F File Offset: 0x00002E7F
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, AmbiguousPropertyBinding.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00004C93 File Offset: 0x00002E93
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00004C9B File Offset: 0x00002E9B
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x04000095 RID: 149
		private readonly IEdmStructuredType declaringType;

		// Token: 0x04000096 RID: 150
		private readonly Cache<AmbiguousPropertyBinding, IEdmTypeReference> type = new Cache<AmbiguousPropertyBinding, IEdmTypeReference>();

		// Token: 0x04000097 RID: 151
		private static readonly Func<AmbiguousPropertyBinding, IEdmTypeReference> ComputeTypeFunc = (AmbiguousPropertyBinding me) => me.ComputeType();
	}
}
