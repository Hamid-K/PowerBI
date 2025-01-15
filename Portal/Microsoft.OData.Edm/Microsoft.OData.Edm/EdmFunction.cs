using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000044 RID: 68
	public class EdmFunction : EdmOperation, IEdmFunction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000159 RID: 345 RVA: 0x000047DA File Offset: 0x000029DA
		public EdmFunction(string namespaceName, string name, IEdmTypeReference returnType, bool isBound, IEdmPathExpression entitySetPathExpression, bool isComposable)
			: base(namespaceName, name, returnType, isBound, entitySetPathExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(returnType, "returnType");
			this.IsComposable = isComposable;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000047FD File Offset: 0x000029FD
		public EdmFunction(string namespaceName, string name, IEdmTypeReference returnType)
			: this(namespaceName, name, returnType, false, null, false)
		{
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000480B File Offset: 0x00002A0B
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000480E File Offset: 0x00002A0E
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00004816 File Offset: 0x00002A16
		public bool IsComposable { get; private set; }
	}
}
