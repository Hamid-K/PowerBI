using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000073 RID: 115
	public class EdmSingleton : EdmNavigationSource, IEdmSingleton, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmNavigationSource
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0000C136 File Offset: 0x0000A336
		public EdmSingleton(IEdmEntityContainer container, string name, IEdmEntityType entityType)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(entityType, "entityType");
			this.container = container;
			this.entityType = entityType;
			this.path = new EdmPathExpression(name);
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00008D57 File Offset: 0x00006F57
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.Singleton;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000C171 File Offset: 0x0000A371
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000C179 File Offset: 0x0000A379
		public override IEdmType Type
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000C181 File Offset: 0x0000A381
		public override IEdmPathExpression Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x040000FD RID: 253
		private readonly IEdmEntityContainer container;

		// Token: 0x040000FE RID: 254
		private readonly IEdmEntityType entityType;

		// Token: 0x040000FF RID: 255
		private IEdmPathExpression path;
	}
}
