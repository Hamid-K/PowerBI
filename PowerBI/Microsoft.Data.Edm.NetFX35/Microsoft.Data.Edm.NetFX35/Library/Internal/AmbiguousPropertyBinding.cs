using System;
using Microsoft.Data.Edm.Internal;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x020000E2 RID: 226
	internal class AmbiguousPropertyBinding : AmbiguousBinding<IEdmProperty>, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x0000C144 File Offset: 0x0000A344
		public AmbiguousPropertyBinding(IEdmStructuredType declaringType, IEdmProperty first, IEdmProperty second)
			: base(first, second)
		{
			this.declaringType = declaringType;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000C160 File Offset: 0x0000A360
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.None;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000C163 File Offset: 0x0000A363
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, AmbiguousPropertyBinding.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000C177 File Offset: 0x0000A377
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000C17F File Offset: 0x0000A37F
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x040001B1 RID: 433
		private readonly IEdmStructuredType declaringType;

		// Token: 0x040001B2 RID: 434
		private readonly Cache<AmbiguousPropertyBinding, IEdmTypeReference> type = new Cache<AmbiguousPropertyBinding, IEdmTypeReference>();

		// Token: 0x040001B3 RID: 435
		private static readonly Func<AmbiguousPropertyBinding, IEdmTypeReference> ComputeTypeFunc = (AmbiguousPropertyBinding me) => me.ComputeType();
	}
}
