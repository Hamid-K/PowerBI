using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000038 RID: 56
	internal class UnresolvedTypeTerm : UnresolvedVocabularyTerm, IEdmEntityType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00002F7B File Offset: 0x0000117B
		public UnresolvedTypeTerm(string qualifiedName)
			: base(qualifiedName)
		{
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002F84 File Offset: 0x00001184
		public IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return Enumerable.Empty<IEdmStructuralProperty>();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002F8B File Offset: 0x0000118B
		public bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002F8E File Offset: 0x0000118E
		public bool IsOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002F91 File Offset: 0x00001191
		public IEdmStructuredType BaseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002F94 File Offset: 0x00001194
		public IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return Enumerable.Empty<IEdmProperty>();
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002F9B File Offset: 0x0000119B
		public EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002F9E File Offset: 0x0000119E
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002FA1 File Offset: 0x000011A1
		public override EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Type;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00002FA4 File Offset: 0x000011A4
		public IEdmProperty FindProperty(string name)
		{
			return null;
		}
	}
}
