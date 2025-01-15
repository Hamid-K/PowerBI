using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000070 RID: 112
	public abstract class EdmProperty : EdmNamedElement, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x0000BFE5 File Offset: 0x0000A1E5
		protected EdmProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(declaringType, "declaringType");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.declaringType = declaringType;
			this.type = type;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000C014 File Offset: 0x0000A214
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000C01C File Offset: 0x0000A21C
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003FD RID: 1021
		public abstract EdmPropertyKind PropertyKind { get; }

		// Token: 0x040000F7 RID: 247
		private readonly IEdmStructuredType declaringType;

		// Token: 0x040000F8 RID: 248
		private readonly IEdmTypeReference type;
	}
}
