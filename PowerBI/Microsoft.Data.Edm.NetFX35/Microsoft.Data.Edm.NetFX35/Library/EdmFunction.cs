using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001D2 RID: 466
	public class EdmFunction : EdmFunctionBase, IEdmFunction, IEdmFunctionBase, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000B11 RID: 2833 RVA: 0x000206B2 File Offset: 0x0001E8B2
		public EdmFunction(string namespaceName, string name, IEdmTypeReference returnType)
			: this(namespaceName, name, returnType, null)
		{
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x000206BE File Offset: 0x0001E8BE
		public EdmFunction(string namespaceName, string name, IEdmTypeReference returnType, string definingExpression)
			: base(name, returnType)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(returnType, "returnType");
			this.namespaceName = namespaceName;
			this.definingExpression = definingExpression;
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x000206EF File Offset: 0x0001E8EF
		public string DefiningExpression
		{
			get
			{
				return this.definingExpression;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x000206F7 File Offset: 0x0001E8F7
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000206FA File Offset: 0x0001E8FA
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x04000532 RID: 1330
		private readonly string namespaceName;

		// Token: 0x04000533 RID: 1331
		private readonly string definingExpression;
	}
}
