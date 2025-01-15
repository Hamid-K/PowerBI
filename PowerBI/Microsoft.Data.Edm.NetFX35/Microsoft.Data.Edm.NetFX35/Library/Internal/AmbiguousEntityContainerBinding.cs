using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x020000DB RID: 219
	internal class AmbiguousEntityContainerBinding : AmbiguousBinding<IEdmEntityContainer>, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x0000BF3B File Offset: 0x0000A13B
		public AmbiguousEntityContainerBinding(IEdmEntityContainer first, IEdmEntityContainer second)
			: base(first, second)
		{
			this.namespaceName = first.Namespace ?? string.Empty;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000BF5A File Offset: 0x0000A15A
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000BF5D File Offset: 0x0000A15D
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000BF65 File Offset: 0x0000A165
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return Enumerable.Empty<IEdmEntityContainerElement>();
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000BF6C File Offset: 0x0000A16C
		public IEdmEntitySet FindEntitySet(string name)
		{
			return null;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000BF6F File Offset: 0x0000A16F
		public IEnumerable<IEdmFunctionImport> FindFunctionImports(string name)
		{
			return null;
		}

		// Token: 0x040001AC RID: 428
		private readonly string namespaceName;
	}
}
