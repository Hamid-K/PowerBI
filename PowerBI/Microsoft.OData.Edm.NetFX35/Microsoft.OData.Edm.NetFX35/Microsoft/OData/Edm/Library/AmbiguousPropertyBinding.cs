using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200011F RID: 287
	internal class AmbiguousPropertyBinding : AmbiguousBinding<IEdmProperty>, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x0000DF2B File Offset: 0x0000C12B
		public AmbiguousPropertyBinding(IEdmStructuredType declaringType, IEdmProperty first, IEdmProperty second)
			: base(first, second)
		{
			this.declaringType = declaringType;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000DF47 File Offset: 0x0000C147
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.None;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000DF4A File Offset: 0x0000C14A
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, AmbiguousPropertyBinding.ComputeTypeFunc, null);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000DF5E File Offset: 0x0000C15E
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000DF66 File Offset: 0x0000C166
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x04000221 RID: 545
		private readonly IEdmStructuredType declaringType;

		// Token: 0x04000222 RID: 546
		private readonly Cache<AmbiguousPropertyBinding, IEdmTypeReference> type = new Cache<AmbiguousPropertyBinding, IEdmTypeReference>();

		// Token: 0x04000223 RID: 547
		private static readonly Func<AmbiguousPropertyBinding, IEdmTypeReference> ComputeTypeFunc = (AmbiguousPropertyBinding me) => me.ComputeType();
	}
}
