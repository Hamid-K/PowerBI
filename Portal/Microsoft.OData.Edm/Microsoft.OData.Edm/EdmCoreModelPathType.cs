using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200000E RID: 14
	internal sealed class EdmCoreModelPathType : EdmType, IEdmPathType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmCoreModelElement, IEdmFullNamedElement
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002756 File Offset: 0x00000956
		public EdmCoreModelPathType(EdmPathTypeKind pathKind)
		{
			this.Name = pathKind.ToString();
			this.PathKind = pathKind;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002778 File Offset: 0x00000978
		public EdmPathTypeKind PathKind { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002780 File Offset: 0x00000980
		public string FullName
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002623 File Offset: 0x00000823
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Path;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002698 File Offset: 0x00000898
		public string Namespace
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002798 File Offset: 0x00000998
		public string Name { get; }
	}
}
