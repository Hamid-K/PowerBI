using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200010D RID: 269
	public class EdmFunctionImport : EdmOperationImport, IEdmFunctionImport, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x0000D92F File Offset: 0x0000BB2F
		public EdmFunctionImport(IEdmEntityContainer container, string name, IEdmFunction function)
			: this(container, name, function, null, false)
		{
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000D93C File Offset: 0x0000BB3C
		public EdmFunctionImport(IEdmEntityContainer container, string name, IEdmFunction function, IEdmExpression entitySetExpression, bool includeInServiceDocument)
			: base(container, function, name, entitySetExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmFunction>(function, "function");
			this.Function = function;
			this.IncludeInServiceDocument = includeInServiceDocument;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0000D964 File Offset: 0x0000BB64
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0000D96C File Offset: 0x0000BB6C
		public IEdmFunction Function { get; private set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0000D975 File Offset: 0x0000BB75
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.FunctionImport;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000D978 File Offset: 0x0000BB78
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0000D980 File Offset: 0x0000BB80
		public bool IncludeInServiceDocument { get; private set; }

		// Token: 0x0600054B RID: 1355 RVA: 0x0000D989 File Offset: 0x0000BB89
		protected override string OperationArgumentNullParameterName()
		{
			return "function";
		}

		// Token: 0x04000204 RID: 516
		private const string FunctionArgumentNullParameterName = "function";
	}
}
