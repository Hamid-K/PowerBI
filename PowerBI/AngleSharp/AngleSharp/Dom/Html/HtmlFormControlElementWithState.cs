using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200035A RID: 858
	internal abstract class HtmlFormControlElementWithState : HtmlFormControlElement
	{
		// Token: 0x06001A6A RID: 6762 RVA: 0x00051CC0 File Offset: 0x0004FEC0
		public HtmlFormControlElementWithState(Document owner, string name, string prefix, NodeFlags flags = NodeFlags.None)
			: base(owner, name, prefix, flags)
		{
			this.CanContainRangeEndpoint = false;
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x00051CD4 File Offset: 0x0004FED4
		// (set) Token: 0x06001A6C RID: 6764 RVA: 0x00051CDC File Offset: 0x0004FEDC
		internal bool CanContainRangeEndpoint { get; private set; }

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x00051CE5 File Offset: 0x0004FEE5
		// (set) Token: 0x06001A6E RID: 6766 RVA: 0x00051CED File Offset: 0x0004FEED
		internal bool ShouldSaveAndRestoreFormControlState { get; private set; }

		// Token: 0x06001A6F RID: 6767
		internal abstract FormControlState SaveControlState();

		// Token: 0x06001A70 RID: 6768
		internal abstract void RestoreFormControlState(FormControlState state);
	}
}
