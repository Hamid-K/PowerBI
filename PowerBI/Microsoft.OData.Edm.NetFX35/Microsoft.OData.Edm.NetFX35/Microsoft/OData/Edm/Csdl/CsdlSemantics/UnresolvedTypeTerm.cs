using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000057 RID: 87
	internal class UnresolvedTypeTerm : UnresolvedVocabularyTerm, IEdmEntityType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public UnresolvedTypeTerm(string qualifiedName)
			: base(qualifiedName)
		{
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00003BF1 File Offset: 0x00001DF1
		public IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return Enumerable.Empty<IEdmStructuralProperty>();
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00003BFB File Offset: 0x00001DFB
		public bool IsOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00003BFE File Offset: 0x00001DFE
		public bool HasStream
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00003C01 File Offset: 0x00001E01
		public IEdmStructuredType BaseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003C04 File Offset: 0x00001E04
		public IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return Enumerable.Empty<IEdmProperty>();
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00003C0B File Offset: 0x00001E0B
		public EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003C0E File Offset: 0x00001E0E
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00003C11 File Offset: 0x00001E11
		public override EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Type;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00003C14 File Offset: 0x00001E14
		public IEdmProperty FindProperty(string name)
		{
			return null;
		}
	}
}
