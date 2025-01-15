using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020000FA RID: 250
	internal class AmbiguousSingletonBinding : AmbiguousBinding<IEdmSingleton>, IEdmSingleton, IEdmEntityContainerElement, IEdmVocabularyAnnotatable, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x0000D133 File Offset: 0x0000B333
		public AmbiguousSingletonBinding(IEdmSingleton first, IEdmSingleton second)
			: base(first, second)
		{
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0000D13D File Offset: 0x0000B33D
		public IEdmType Type
		{
			get
			{
				return new BadEntityType(string.Empty, base.Errors);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000D14F File Offset: 0x0000B34F
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.Singleton;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000D154 File Offset: 0x0000B354
		public IEdmEntityContainer Container
		{
			get
			{
				IEdmSingleton edmSingleton = Enumerable.FirstOrDefault<IEdmSingleton>(base.Bindings);
				if (edmSingleton == null)
				{
					return null;
				}
				return edmSingleton.Container;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000D178 File Offset: 0x0000B378
		public IEdmPathExpression Path
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000D17B File Offset: 0x0000B37B
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return Enumerable.Empty<IEdmNavigationPropertyBinding>();
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000D182 File Offset: 0x0000B382
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}
	}
}
