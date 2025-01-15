using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000150 RID: 336
	public sealed class PathSelectItem : SelectItem
	{
		// Token: 0x06000ECB RID: 3787 RVA: 0x0002AC4E File Offset: 0x00028E4E
		public PathSelectItem(ODataSelectPath selectedPath)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataSelectPath>(selectedPath, "selectedPath");
			this.selectedPath = selectedPath;
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0002AC69 File Offset: 0x00028E69
		public ODataSelectPath SelectedPath
		{
			get
			{
				return this.selectedPath;
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0002AC71 File Offset: 0x00028E71
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0002AC7A File Offset: 0x00028E7A
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}

		// Token: 0x0400078D RID: 1933
		private readonly ODataSelectPath selectedPath;
	}
}
