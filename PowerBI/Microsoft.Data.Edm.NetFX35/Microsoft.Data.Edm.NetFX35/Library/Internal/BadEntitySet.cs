using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x020000F8 RID: 248
	internal class BadEntitySet : BadElement, IEdmEntitySet, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0000C608 File Offset: 0x0000A808
		public BadEntitySet(string name, IEdmEntityContainer container, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
			this.container = container;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000C628 File Offset: 0x0000A828
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000C630 File Offset: 0x0000A830
		public IEdmEntityType ElementType
		{
			get
			{
				return new BadEntityType(string.Empty, base.Errors);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000C642 File Offset: 0x0000A842
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000C645 File Offset: 0x0000A845
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000C64D File Offset: 0x0000A84D
		public IEnumerable<IEdmNavigationTargetMapping> NavigationTargets
		{
			get
			{
				return Enumerable.Empty<IEdmNavigationTargetMapping>();
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000C654 File Offset: 0x0000A854
		public IEdmEntitySet FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}

		// Token: 0x040001CA RID: 458
		private readonly string name;

		// Token: 0x040001CB RID: 459
		private readonly IEdmEntityContainer container;
	}
}
