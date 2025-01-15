using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000033 RID: 51
	internal class AmbiguousSingletonBinding : AmbiguousBinding<IEdmSingleton>, IEdmSingleton, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmNavigationSource
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x000039DF File Offset: 0x00001BDF
		public AmbiguousSingletonBinding(IEdmSingleton first, IEdmSingleton second)
			: base(first, second)
		{
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000039E9 File Offset: 0x00001BE9
		public IEdmType Type
		{
			get
			{
				return new BadEntityType(string.Empty, base.Errors);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000039FB File Offset: 0x00001BFB
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.Singleton;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003A00 File Offset: 0x00001C00
		public IEdmEntityContainer Container
		{
			get
			{
				IEdmSingleton edmSingleton = base.Bindings.FirstOrDefault<IEdmSingleton>();
				if (edmSingleton == null)
				{
					return null;
				}
				return edmSingleton.Container;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmPathExpression Path
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003A24 File Offset: 0x00001C24
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return Enumerable.Empty<IEdmNavigationPropertyBinding>();
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
		{
			return null;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			return null;
		}
	}
}
