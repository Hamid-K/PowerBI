using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000102 RID: 258
	public abstract class EdmEntitySetBase : EdmNavigationSource, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0000D444 File Offset: 0x0000B644
		protected EdmEntitySetBase(string name, IEdmEntityType elementType)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(elementType, "elementType");
			this.type = new EdmCollectionType(new EdmEntityTypeReference(elementType, false));
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000D46B File Offset: 0x0000B66B
		public override IEdmType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040001EA RID: 490
		private readonly IEdmCollectionType type;
	}
}
