using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000038 RID: 56
	public abstract class EdmEntitySetBase : EdmNavigationSource, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00003F74 File Offset: 0x00002174
		protected EdmEntitySetBase(string name, IEdmEntityType elementType)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(elementType, "elementType");
			this.type = new EdmCollectionType(new EdmEntityTypeReference(elementType, false));
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003F9B File Offset: 0x0000219B
		public override IEdmType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000063 RID: 99
		private readonly IEdmCollectionType type;
	}
}
