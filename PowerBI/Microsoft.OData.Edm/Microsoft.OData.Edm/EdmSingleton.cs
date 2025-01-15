using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200003D RID: 61
	public class EdmSingleton : EdmNavigationSource, IEdmSingleton, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmNavigationSource
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00004435 File Offset: 0x00002635
		public EdmSingleton(IEdmEntityContainer container, string name, IEdmEntityType entityType)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(entityType, "entityType");
			this.container = container;
			this.entityType = entityType;
			this.path = new EdmPathExpression(name);
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000039FB File Offset: 0x00001BFB
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.Singleton;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004470 File Offset: 0x00002670
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004478 File Offset: 0x00002678
		public override IEdmType Type
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004480 File Offset: 0x00002680
		public override IEdmPathExpression Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x04000070 RID: 112
		private readonly IEdmEntityContainer container;

		// Token: 0x04000071 RID: 113
		private readonly IEdmEntityType entityType;

		// Token: 0x04000072 RID: 114
		private IEdmPathExpression path;
	}
}
