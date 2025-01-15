using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B8 RID: 184
	public abstract class EdmOperationImport : EdmNamedElement, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x0000B3AA File Offset: 0x000095AA
		protected EdmOperationImport(IEdmEntityContainer container, IEdmOperation operation, string name, IEdmExpression entitySet)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			EdmUtil.CheckArgumentNull<IEdmOperation>(operation, this.OperationArgumentNullParameterName());
			this.Container = container;
			this.Operation = operation;
			this.EntitySet = entitySet;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000B3E2 File Offset: 0x000095E2
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000B3EA File Offset: 0x000095EA
		public IEdmOperation Operation { get; private set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000B3F3 File Offset: 0x000095F3
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000B3FB File Offset: 0x000095FB
		public IEdmExpression EntitySet { get; private set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000461 RID: 1121
		public abstract EdmContainerElementKind ContainerElementKind { get; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000B404 File Offset: 0x00009604
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0000B40C File Offset: 0x0000960C
		public IEdmEntityContainer Container { get; private set; }

		// Token: 0x06000464 RID: 1124
		protected abstract string OperationArgumentNullParameterName();
	}
}
