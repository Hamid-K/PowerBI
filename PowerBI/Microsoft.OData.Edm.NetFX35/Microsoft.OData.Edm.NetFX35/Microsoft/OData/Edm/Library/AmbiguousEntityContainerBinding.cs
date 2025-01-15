using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000118 RID: 280
	internal class AmbiguousEntityContainerBinding : AmbiguousBinding<IEdmEntityContainer>, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x0000DD92 File Offset: 0x0000BF92
		public AmbiguousEntityContainerBinding(IEdmEntityContainer first, IEdmEntityContainer second)
			: base(first, second)
		{
			this.namespaceName = first.Namespace ?? string.Empty;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0000DDB1 File Offset: 0x0000BFB1
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000DDB4 File Offset: 0x0000BFB4
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return Enumerable.Empty<IEdmEntityContainerElement>();
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000DDC3 File Offset: 0x0000BFC3
		public IEdmEntitySet FindEntitySet(string name)
		{
			return null;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000DDC6 File Offset: 0x0000BFC6
		public IEdmSingleton FindSingleton(string name)
		{
			return null;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000DDC9 File Offset: 0x0000BFC9
		public IEnumerable<IEdmOperationImport> FindOperationImports(string operationName)
		{
			return null;
		}

		// Token: 0x0400021B RID: 539
		private readonly string namespaceName;
	}
}
