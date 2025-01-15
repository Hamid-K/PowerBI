using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000110 RID: 272
	public class EdmFunction : EdmOperation, IEdmFunction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x0000DB09 File Offset: 0x0000BD09
		public EdmFunction(string namespaceName, string name, IEdmTypeReference returnType, bool isBound, IEdmPathExpression entitySetPathExpression, bool isComposable)
			: base(namespaceName, name, returnType, isBound, entitySetPathExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(returnType, "returnType");
			this.IsComposable = isComposable;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		public EdmFunction(string namespaceName, string name, IEdmTypeReference returnType)
			: this(namespaceName, name, returnType, false, null, false)
		{
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000DB3A File Offset: 0x0000BD3A
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0000DB3D File Offset: 0x0000BD3D
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0000DB45 File Offset: 0x0000BD45
		public bool IsComposable { get; private set; }
	}
}
