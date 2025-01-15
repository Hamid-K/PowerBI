using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000107 RID: 263
	public class EdmSingleton : EdmNavigationSource, IEdmSingleton, IEdmEntityContainerElement, IEdmVocabularyAnnotatable, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x0000D607 File Offset: 0x0000B807
		public EdmSingleton(IEdmEntityContainer container, string name, IEdmEntityType entityType)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(entityType, "entityType");
			this.container = container;
			this.entityType = entityType;
			this.path = new EdmPathExpression(name);
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000D642 File Offset: 0x0000B842
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.Singleton;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000D645 File Offset: 0x0000B845
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0000D64D File Offset: 0x0000B84D
		public override IEdmType Type
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000D655 File Offset: 0x0000B855
		public override IEdmPathExpression Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x040001F6 RID: 502
		private readonly IEdmEntityContainer container;

		// Token: 0x040001F7 RID: 503
		private readonly IEdmEntityType entityType;

		// Token: 0x040001F8 RID: 504
		private IEdmPathExpression path;
	}
}
