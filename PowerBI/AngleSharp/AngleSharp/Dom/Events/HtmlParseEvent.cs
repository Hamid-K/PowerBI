using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001E7 RID: 487
	public class HtmlParseEvent : Event
	{
		// Token: 0x06001016 RID: 4118 RVA: 0x00047424 File Offset: 0x00045624
		public HtmlParseEvent(IDocument document, bool completed)
			: base(completed ? EventNames.ParseEnd : EventNames.ParseStart, false, false)
		{
			this.Document = document;
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00047444 File Offset: 0x00045644
		// (set) Token: 0x06001018 RID: 4120 RVA: 0x0004744C File Offset: 0x0004564C
		public IDocument Document { get; private set; }
	}
}
