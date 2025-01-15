using System;
using AngleSharp.Attributes;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200036F RID: 879
	[DomHistorical]
	internal sealed class HtmlMarqueeElement : HtmlElement
	{
		// Token: 0x06001B80 RID: 7040 RVA: 0x00053591 File Offset: 0x00051791
		public HtmlMarqueeElement(Document owner, string prefix = null)
			: base(owner, TagNames.Marquee, prefix, NodeFlags.Special | NodeFlags.Scoped)
		{
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x000535A2 File Offset: 0x000517A2
		// (set) Token: 0x06001B82 RID: 7042 RVA: 0x000535AA File Offset: 0x000517AA
		public int MinimumDelay { get; private set; }

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x000535B3 File Offset: 0x000517B3
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x000535BB File Offset: 0x000517BB
		public int ScrollAmount { get; set; }

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x000535C4 File Offset: 0x000517C4
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x000535CC File Offset: 0x000517CC
		public int ScrollDelay { get; set; }

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x000535D5 File Offset: 0x000517D5
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x000535DD File Offset: 0x000517DD
		public int Loop { get; set; }

		// Token: 0x06001B89 RID: 7049 RVA: 0x000535E6 File Offset: 0x000517E6
		public void Start()
		{
			this.FireSimpleEvent(EventNames.Play, false, false);
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x000535F6 File Offset: 0x000517F6
		public void Stop()
		{
			this.FireSimpleEvent(EventNames.Pause, false, false);
		}
	}
}
