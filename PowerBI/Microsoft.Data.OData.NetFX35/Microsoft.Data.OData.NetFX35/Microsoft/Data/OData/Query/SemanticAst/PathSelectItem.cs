using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000074 RID: 116
	public sealed class PathSelectItem : SelectItem
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000A7C4 File Offset: 0x000089C4
		public PathSelectItem(ODataSelectPath selectedPath)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataSelectPath>(selectedPath, "selectedPath");
			this.selectedPath = selectedPath;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000A7DE File Offset: 0x000089DE
		public ODataSelectPath SelectedPath
		{
			get
			{
				return this.selectedPath;
			}
		}

		// Token: 0x040000BE RID: 190
		private readonly ODataSelectPath selectedPath;
	}
}
