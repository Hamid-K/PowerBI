using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000009 RID: 9
	internal sealed class EdmCoreModelComplexType : EdmType, IEdmComplexType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmCoreModelElement, IEdmFullNamedElement
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002683 File Offset: 0x00000883
		private EdmCoreModelComplexType()
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000268B File Offset: 0x0000088B
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002691 File Offset: 0x00000891
		public string Name
		{
			get
			{
				return "ComplexType";
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002698 File Offset: 0x00000898
		public string Namespace
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000269F File Offset: 0x0000089F
		public string FullName
		{
			get
			{
				return "Edm.ComplexType";
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000268E File Offset: 0x0000088E
		public bool IsAbstract
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000026A6 File Offset: 0x000008A6
		public bool IsOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000026A9 File Offset: 0x000008A9
		public IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return Enumerable.Empty<IEdmProperty>();
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000026B0 File Offset: 0x000008B0
		IEdmStructuredType IEdmStructuredType.BaseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmProperty FindProperty(string name)
		{
			return null;
		}

		// Token: 0x04000007 RID: 7
		public static readonly EdmCoreModelComplexType Instance = new EdmCoreModelComplexType();

		// Token: 0x04000008 RID: 8
		public IEdmStructuredType BaseType;
	}
}
