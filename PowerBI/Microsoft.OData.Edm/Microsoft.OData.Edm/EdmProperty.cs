using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C3 RID: 195
	public abstract class EdmProperty : EdmNamedElement, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		protected EdmProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(declaringType, "declaringType");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.declaringType = declaringType;
			this.type = type;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000BE13 File Offset: 0x0000A013
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000BE1B File Offset: 0x0000A01B
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060004AB RID: 1195
		public abstract EdmPropertyKind PropertyKind { get; }

		// Token: 0x04000172 RID: 370
		private readonly IEdmStructuredType declaringType;

		// Token: 0x04000173 RID: 371
		private readonly IEdmTypeReference type;
	}
}
