using System;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x020001E5 RID: 485
	internal class UnresolvedType : BadType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement, IUnresolvedElement
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x00021358 File Offset: 0x0001F558
		public UnresolvedType(string qualifiedName, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedType, Strings.Bad_UnresolvedType(qualifiedName))
			})
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x000213A6 File Offset: 0x0001F5A6
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x000213A9 File Offset: 0x0001F5A9
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x000213B1 File Offset: 0x0001F5B1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000553 RID: 1363
		private readonly string namespaceName;

		// Token: 0x04000554 RID: 1364
		private readonly string name;
	}
}
