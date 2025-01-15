using System;
using AngleSharp.Dom.Css;
using AngleSharp.Html;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001E0 RID: 480
	public class CssParseEvent : Event
	{
		// Token: 0x06000FE0 RID: 4064 RVA: 0x00047037 File Offset: 0x00045237
		public CssParseEvent(ICssStyleSheet styleSheet, bool completed)
			: base(completed ? EventNames.ParseEnd : EventNames.ParseStart, false, false)
		{
			this.StyleSheet = styleSheet;
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00047057 File Offset: 0x00045257
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x0004705F File Offset: 0x0004525F
		public ICssStyleSheet StyleSheet { get; private set; }
	}
}
