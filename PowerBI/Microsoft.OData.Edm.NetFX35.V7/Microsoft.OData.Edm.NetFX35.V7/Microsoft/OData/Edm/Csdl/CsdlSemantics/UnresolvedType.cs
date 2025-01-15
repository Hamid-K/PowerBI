using System;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A8 RID: 424
	internal class UnresolvedType : BadType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IUnresolvedElement
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x0001FB3C File Offset: 0x0001DD3C
		public UnresolvedType(string qualifiedName, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedType, Strings.Bad_UnresolvedType(qualifiedName))
			})
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0001FB88 File Offset: 0x0001DD88
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0001FB90 File Offset: 0x0001DD90
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000692 RID: 1682
		private readonly string namespaceName;

		// Token: 0x04000693 RID: 1683
		private readonly string name;
	}
}
