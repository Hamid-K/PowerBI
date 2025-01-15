using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200001F RID: 31
	internal class AmbiguousEntityContainerBinding : AmbiguousBinding<IEdmEntityContainer>, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x00008D38 File Offset: 0x00006F38
		public AmbiguousEntityContainerBinding(IEdmEntityContainer first, IEdmEntityContainer second)
			: base(first, second)
		{
			this.namespaceName = first.Namespace ?? string.Empty;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00008D57 File Offset: 0x00006F57
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00008D5A File Offset: 0x00006F5A
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00008D62 File Offset: 0x00006F62
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return Enumerable.Empty<IEdmEntityContainerElement>();
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmEntitySet FindEntitySet(string name)
		{
			return null;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmSingleton FindSingleton(string name)
		{
			return null;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEnumerable<IEdmOperationImport> FindOperationImports(string operationName)
		{
			return null;
		}

		// Token: 0x04000038 RID: 56
		private readonly string namespaceName;
	}
}
