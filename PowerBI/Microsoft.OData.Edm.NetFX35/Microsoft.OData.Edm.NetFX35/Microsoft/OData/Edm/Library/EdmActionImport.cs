using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200010E RID: 270
	public class EdmActionImport : EdmOperationImport, IEdmActionImport, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x0000D990 File Offset: 0x0000BB90
		public EdmActionImport(IEdmEntityContainer container, string name, IEdmAction action)
			: this(container, name, action, null)
		{
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000D99C File Offset: 0x0000BB9C
		public EdmActionImport(IEdmEntityContainer container, string name, IEdmAction action, IEdmExpression entitySetExpression)
			: base(container, action, name, entitySetExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmAction>(action, "action");
			this.Action = action;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0000D9BC File Offset: 0x0000BBBC
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		public IEdmAction Action { get; private set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0000D9CD File Offset: 0x0000BBCD
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.ActionImport;
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
		protected override string OperationArgumentNullParameterName()
		{
			return "action";
		}

		// Token: 0x04000207 RID: 519
		private const string ActionArgumentNullParameterName = "action";
	}
}
