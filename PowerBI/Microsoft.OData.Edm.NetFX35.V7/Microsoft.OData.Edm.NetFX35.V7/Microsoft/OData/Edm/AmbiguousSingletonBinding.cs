using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000025 RID: 37
	internal class AmbiguousSingletonBinding : AmbiguousBinding<IEdmSingleton>, IEdmSingleton, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmNavigationSource
	{
		// Token: 0x06000222 RID: 546 RVA: 0x00008F0C File Offset: 0x0000710C
		public AmbiguousSingletonBinding(IEdmSingleton first, IEdmSingleton second)
			: base(first, second)
		{
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00008F16 File Offset: 0x00007116
		public IEdmType Type
		{
			get
			{
				return new BadEntityType(string.Empty, base.Errors);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00008D57 File Offset: 0x00006F57
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.Singleton;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00008F28 File Offset: 0x00007128
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmPathExpression Path
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00008DBD File Offset: 0x00006FBD
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return Enumerable.Empty<IEdmNavigationPropertyBinding>();
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
		{
			return null;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			return null;
		}
	}
}
