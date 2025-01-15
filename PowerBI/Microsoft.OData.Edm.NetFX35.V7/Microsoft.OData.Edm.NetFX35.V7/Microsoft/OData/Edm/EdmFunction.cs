using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005F RID: 95
	public class EdmFunction : EdmOperation, IEdmFunction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0000AEA6 File Offset: 0x000090A6
		public EdmFunction(string namespaceName, string name, IEdmTypeReference returnType, bool isBound, IEdmPathExpression entitySetPathExpression, bool isComposable)
			: base(namespaceName, name, returnType, isBound, entitySetPathExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(returnType, "returnType");
			this.IsComposable = isComposable;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000AEC9 File Offset: 0x000090C9
		public EdmFunction(string namespaceName, string name, IEdmTypeReference returnType)
			: this(namespaceName, name, returnType, false, null, false)
		{
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00009215 File Offset: 0x00007415
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000AED7 File Offset: 0x000090D7
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000AEDF File Offset: 0x000090DF
		public bool IsComposable { get; private set; }
	}
}
