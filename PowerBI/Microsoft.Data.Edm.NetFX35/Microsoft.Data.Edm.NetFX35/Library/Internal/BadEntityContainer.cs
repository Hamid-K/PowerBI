using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x020000F5 RID: 245
	internal class BadEntityContainer : BadElement, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x0000C596 File Offset: 0x0000A796
		public BadEntityContainer(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000C5BE File Offset: 0x0000A7BE
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return Enumerable.Empty<IEdmEntityContainerElement>();
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000C5C5 File Offset: 0x0000A7C5
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000C5CD File Offset: 0x0000A7CD
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000C5D5 File Offset: 0x0000A7D5
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		public IEdmEntitySet FindEntitySet(string setName)
		{
			return null;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000C5DB File Offset: 0x0000A7DB
		public IEnumerable<IEdmFunctionImport> FindFunctionImports(string functionName)
		{
			return null;
		}

		// Token: 0x040001C7 RID: 455
		private readonly string namespaceName;

		// Token: 0x040001C8 RID: 456
		private readonly string name;
	}
}
