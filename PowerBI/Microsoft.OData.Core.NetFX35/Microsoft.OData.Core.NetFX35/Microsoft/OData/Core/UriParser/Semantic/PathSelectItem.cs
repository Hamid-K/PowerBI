using System;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000257 RID: 599
	public sealed class PathSelectItem : SelectItem
	{
		// Token: 0x06001539 RID: 5433 RVA: 0x0004AF1A File Offset: 0x0004911A
		public PathSelectItem(ODataSelectPath selectedPath)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataSelectPath>(selectedPath, "selectedPath");
			this.selectedPath = selectedPath;
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0004AF34 File Offset: 0x00049134
		public ODataSelectPath SelectedPath
		{
			get
			{
				return this.selectedPath;
			}
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0004AF3C File Offset: 0x0004913C
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0004AF45 File Offset: 0x00049145
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}

		// Token: 0x040008D9 RID: 2265
		private readonly ODataSelectPath selectedPath;
	}
}
