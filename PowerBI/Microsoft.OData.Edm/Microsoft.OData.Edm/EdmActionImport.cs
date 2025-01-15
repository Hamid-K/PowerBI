using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000043 RID: 67
	public class EdmActionImport : EdmOperationImport, IEdmActionImport, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00004796 File Offset: 0x00002996
		public EdmActionImport(IEdmEntityContainer container, string name, IEdmAction action)
			: this(container, name, action, null)
		{
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000047A2 File Offset: 0x000029A2
		public EdmActionImport(IEdmEntityContainer container, string name, IEdmAction action, IEdmExpression entitySetExpression)
			: base(container, action, name, entitySetExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmAction>(action, "action");
			this.Action = action;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000047C2 File Offset: 0x000029C2
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000047CA File Offset: 0x000029CA
		public IEdmAction Action { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00002732 File Offset: 0x00000932
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.ActionImport;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000047D3 File Offset: 0x000029D3
		protected override string OperationArgumentNullParameterName()
		{
			return "action";
		}

		// Token: 0x0400007F RID: 127
		private const string ActionArgumentNullParameterName = "action";
	}
}
