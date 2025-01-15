using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200000D RID: 13
	internal sealed class EdmCoreModelEntityType : EdmType, IEdmEntityType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmCoreModelElement, IEdmFullNamedElement
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002683 File Offset: 0x00000883
		private EdmCoreModelEntityType()
		{
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002732 File Offset: 0x00000932
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002735 File Offset: 0x00000935
		public string Name
		{
			get
			{
				return "EntityType";
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002698 File Offset: 0x00000898
		public string Namespace
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000273C File Offset: 0x0000093C
		public string FullName
		{
			get
			{
				return "Edm.EntityType";
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000026A6 File Offset: 0x000008A6
		public bool HasStream
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000268E File Offset: 0x0000088E
		public bool IsAbstract
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000026A6 File Offset: 0x000008A6
		public bool IsOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmStructuredType BaseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000026A9 File Offset: 0x000008A9
		public IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return Enumerable.Empty<IEdmProperty>();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002743 File Offset: 0x00000943
		public IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return Enumerable.Empty<IEdmStructuralProperty>();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmProperty FindProperty(string name)
		{
			return null;
		}

		// Token: 0x0400000D RID: 13
		public static readonly EdmCoreModelEntityType Instance = new EdmCoreModelEntityType();
	}
}
