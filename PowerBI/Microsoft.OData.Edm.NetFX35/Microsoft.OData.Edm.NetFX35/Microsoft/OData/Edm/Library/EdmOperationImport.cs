using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200010C RID: 268
	public abstract class EdmOperationImport : EdmNamedElement, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x0000D8C4 File Offset: 0x0000BAC4
		protected EdmOperationImport(IEdmEntityContainer container, IEdmOperation operation, string name, IEdmExpression entitySet)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			EdmUtil.CheckArgumentNull<IEdmOperation>(operation, this.OperationArgumentNullParameterName());
			this.Container = container;
			this.Operation = operation;
			this.EntitySet = entitySet;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0000D8FC File Offset: 0x0000BAFC
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0000D904 File Offset: 0x0000BB04
		public IEdmOperation Operation { get; private set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0000D90D File Offset: 0x0000BB0D
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0000D915 File Offset: 0x0000BB15
		public IEdmExpression EntitySet { get; private set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000540 RID: 1344
		public abstract EdmContainerElementKind ContainerElementKind { get; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000D91E File Offset: 0x0000BB1E
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000D926 File Offset: 0x0000BB26
		public IEdmEntityContainer Container { get; private set; }

		// Token: 0x06000543 RID: 1347
		protected abstract string OperationArgumentNullParameterName();
	}
}
