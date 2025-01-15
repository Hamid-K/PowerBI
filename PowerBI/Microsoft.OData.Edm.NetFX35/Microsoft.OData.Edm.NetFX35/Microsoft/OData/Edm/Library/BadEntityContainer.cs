using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000132 RID: 306
	internal class BadEntityContainer : BadElement, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x0000E336 File Offset: 0x0000C536
		public BadEntityContainer(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000E35E File Offset: 0x0000C55E
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return Enumerable.Empty<IEdmEntityContainerElement>();
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000E365 File Offset: 0x0000C565
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000E36D File Offset: 0x0000C56D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000E375 File Offset: 0x0000C575
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000E378 File Offset: 0x0000C578
		public IEdmEntitySet FindEntitySet(string setName)
		{
			return null;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000E37B File Offset: 0x0000C57B
		public IEdmSingleton FindSingleton(string singletonName)
		{
			return null;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000E37E File Offset: 0x0000C57E
		public IEnumerable<IEdmOperationImport> FindOperationImports(string operationName)
		{
			return null;
		}

		// Token: 0x04000236 RID: 566
		private readonly string namespaceName;

		// Token: 0x04000237 RID: 567
		private readonly string name;
	}
}
