using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000045 RID: 69
	public class EdmAction : EdmOperation, IEdmAction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600015E RID: 350 RVA: 0x0000481F File Offset: 0x00002A1F
		public EdmAction(string namespaceName, string name, IEdmTypeReference returnType, bool isBound, IEdmPathExpression entitySetPathExpression)
			: base(namespaceName, name, returnType, isBound, entitySetPathExpression)
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000482E File Offset: 0x00002A2E
		public EdmAction(string namespaceName, string name, IEdmTypeReference returnType)
			: base(namespaceName, name, returnType)
		{
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000268B File Offset: 0x0000088B
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Action;
			}
		}
	}
}
