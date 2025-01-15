using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000042 RID: 66
	public class EdmFunctionImport : EdmOperationImport, IEdmFunctionImport, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00004738 File Offset: 0x00002938
		public EdmFunctionImport(IEdmEntityContainer container, string name, IEdmFunction function)
			: this(container, name, function, null, false)
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004745 File Offset: 0x00002945
		public EdmFunctionImport(IEdmEntityContainer container, string name, IEdmFunction function, IEdmExpression entitySetExpression, bool includeInServiceDocument)
			: base(container, function, name, entitySetExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmFunction>(function, "function");
			this.Function = function;
			this.IncludeInServiceDocument = includeInServiceDocument;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000476D File Offset: 0x0000296D
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00004775 File Offset: 0x00002975
		public IEdmFunction Function { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000268B File Offset: 0x0000088B
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.FunctionImport;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000477E File Offset: 0x0000297E
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00004786 File Offset: 0x00002986
		public bool IncludeInServiceDocument { get; private set; }

		// Token: 0x06000152 RID: 338 RVA: 0x0000478F File Offset: 0x0000298F
		protected override string OperationArgumentNullParameterName()
		{
			return "function";
		}

		// Token: 0x0400007C RID: 124
		private const string FunctionArgumentNullParameterName = "function";
	}
}
