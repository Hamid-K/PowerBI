using System;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001B7 RID: 439
	internal class UnresolvedType : BadType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IUnresolvedElement, IEdmFullNamedElement
	{
		// Token: 0x06000C32 RID: 3122 RVA: 0x00021F70 File Offset: 0x00020170
		public UnresolvedType(string qualifiedName, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedType, Strings.Bad_UnresolvedType(qualifiedName))
			})
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name, out this.fullName);
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00021FC2 File Offset: 0x000201C2
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00021FCA File Offset: 0x000201CA
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00021FD2 File Offset: 0x000201D2
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x0400070E RID: 1806
		private readonly string namespaceName;

		// Token: 0x0400070F RID: 1807
		private readonly string name;

		// Token: 0x04000710 RID: 1808
		private readonly string fullName;
	}
}
