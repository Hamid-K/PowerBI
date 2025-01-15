using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004A RID: 74
	internal class AmbiguousEntityContainerBinding : AmbiguousBinding<IEdmEntityContainer>, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public AmbiguousEntityContainerBinding(IEdmEntityContainer first, IEdmEntityContainer second)
			: base(first, second)
		{
			this.namespaceName = first.Namespace ?? string.Empty;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(this.namespaceName, base.Name);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000039FB File Offset: 0x00001BFB
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00004AFE File Offset: 0x00002CFE
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00004B06 File Offset: 0x00002D06
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00004B0E File Offset: 0x00002D0E
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return Enumerable.Empty<IEdmEntityContainerElement>();
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmEntitySet FindEntitySet(string name)
		{
			return null;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmSingleton FindSingleton(string name)
		{
			return null;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEnumerable<IEdmOperationImport> FindOperationImports(string operationName)
		{
			return null;
		}

		// Token: 0x0400008E RID: 142
		private readonly string namespaceName;

		// Token: 0x0400008F RID: 143
		private readonly string fullName;
	}
}
