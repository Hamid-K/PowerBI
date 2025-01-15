using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000058 RID: 88
	public abstract class EdmEntitySetBase : EdmNavigationSource, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x0600034A RID: 842 RVA: 0x0000AB04 File Offset: 0x00008D04
		protected EdmEntitySetBase(string name, IEdmEntityType elementType)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(elementType, "elementType");
			this.type = new EdmCollectionType(new EdmEntityTypeReference(elementType, false));
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000AB2B File Offset: 0x00008D2B
		public override IEdmType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040000B5 RID: 181
		private readonly IEdmCollectionType type;
	}
}
