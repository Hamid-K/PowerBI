using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000060 RID: 96
	public class EdmFunctionImport : EdmOperationImport, IEdmFunctionImport, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000374 RID: 884 RVA: 0x0000AEE8 File Offset: 0x000090E8
		public EdmFunctionImport(IEdmEntityContainer container, string name, IEdmFunction function)
			: this(container, name, function, null, false)
		{
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000AEF5 File Offset: 0x000090F5
		public EdmFunctionImport(IEdmEntityContainer container, string name, IEdmFunction function, IEdmExpression entitySetExpression, bool includeInServiceDocument)
			: base(container, function, name, entitySetExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmFunction>(function, "function");
			this.Function = function;
			this.IncludeInServiceDocument = includeInServiceDocument;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000AF1D File Offset: 0x0000911D
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000AF25 File Offset: 0x00009125
		public IEdmFunction Function { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00009097 File Offset: 0x00007297
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.FunctionImport;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000AF2E File Offset: 0x0000912E
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000AF36 File Offset: 0x00009136
		public bool IncludeInServiceDocument { get; private set; }

		// Token: 0x0600037B RID: 891 RVA: 0x0000AF3F File Offset: 0x0000913F
		protected override string OperationArgumentNullParameterName()
		{
			return "function";
		}

		// Token: 0x040000C3 RID: 195
		private const string FunctionArgumentNullParameterName = "function";
	}
}
