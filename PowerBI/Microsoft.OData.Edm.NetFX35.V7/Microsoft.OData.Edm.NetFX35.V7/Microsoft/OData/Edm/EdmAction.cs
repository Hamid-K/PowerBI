using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000046 RID: 70
	public class EdmAction : EdmOperation, IEdmAction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060002CF RID: 719 RVA: 0x00009B9F File Offset: 0x00007D9F
		public EdmAction(string namespaceName, string name, IEdmTypeReference returnType, bool isBound, IEdmPathExpression entitySetPathExpression)
			: base(namespaceName, name, returnType, isBound, entitySetPathExpression)
		{
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00009BAE File Offset: 0x00007DAE
		public EdmAction(string namespaceName, string name, IEdmTypeReference returnType)
			: base(namespaceName, name, returnType)
		{
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00009097 File Offset: 0x00007297
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Action;
			}
		}
	}
}
