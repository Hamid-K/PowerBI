using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000047 RID: 71
	public class EdmActionImport : EdmOperationImport, IEdmActionImport, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x00009BB9 File Offset: 0x00007DB9
		public EdmActionImport(IEdmEntityContainer container, string name, IEdmAction action)
			: this(container, name, action, null)
		{
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00009BC5 File Offset: 0x00007DC5
		public EdmActionImport(IEdmEntityContainer container, string name, IEdmAction action, IEdmExpression entitySetExpression)
			: base(container, action, name, entitySetExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmAction>(action, "action");
			this.Action = action;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00009BE5 File Offset: 0x00007DE5
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00009BED File Offset: 0x00007DED
		public IEdmAction Action { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00008F68 File Offset: 0x00007168
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.ActionImport;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00009BF6 File Offset: 0x00007DF6
		protected override string OperationArgumentNullParameterName()
		{
			return "action";
		}

		// Token: 0x0400006D RID: 109
		private const string ActionArgumentNullParameterName = "action";
	}
}
