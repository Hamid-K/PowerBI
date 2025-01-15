using System;
using Microsoft.Data.Edm.Internal;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001D6 RID: 470
	public abstract class EdmProperty : EdmNamedElement, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000B2E RID: 2862 RVA: 0x00020A00 File Offset: 0x0001EC00
		protected EdmProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(declaringType, "declaringType");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.declaringType = declaringType;
			this.type = type;
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00020A3A File Offset: 0x0001EC3A
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00020A42 File Offset: 0x0001EC42
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000B31 RID: 2865
		public abstract EdmPropertyKind PropertyKind { get; }

		// Token: 0x04000540 RID: 1344
		private readonly IEdmStructuredType declaringType;

		// Token: 0x04000541 RID: 1345
		private readonly HashSetInternal<IDependent> dependents = new HashSetInternal<IDependent>();

		// Token: 0x04000542 RID: 1346
		private readonly IEdmTypeReference type;
	}
}
