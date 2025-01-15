using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000111 RID: 273
	public class EdmAction : EdmOperation, IEdmAction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x0000DB4E File Offset: 0x0000BD4E
		public EdmAction(string namespaceName, string name, IEdmTypeReference returnType, bool isBound, IEdmPathExpression entitySetPathExpression)
			: base(namespaceName, name, returnType, isBound, entitySetPathExpression)
		{
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000DB5D File Offset: 0x0000BD5D
		public EdmAction(string namespaceName, string name, IEdmTypeReference returnType)
			: base(namespaceName, name, returnType)
		{
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0000DB68 File Offset: 0x0000BD68
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Action;
			}
		}
	}
}
