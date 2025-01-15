using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Navigator;

namespace AngleSharp.Dom
{
	// Token: 0x020001A4 RID: 420
	[DomName("Window")]
	public interface IWindow : IEventTarget, IGlobalEventHandlers, IWindowEventHandlers, IWindowTimers
	{
		// Token: 0x06000EEA RID: 3818
		[DomName("getComputedStyle")]
		ICssStyleDeclaration GetComputedStyle(IElement element, string pseudo = null);

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000EEB RID: 3819
		[DomName("document")]
		IDocument Document { get; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000EEC RID: 3820
		[DomName("location")]
		[DomPutForwards("href")]
		ILocation Location { get; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000EED RID: 3821
		[DomName("closed")]
		bool IsClosed { get; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000EEE RID: 3822
		// (set) Token: 0x06000EEF RID: 3823
		[DomName("status")]
		string Status { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000EF0 RID: 3824
		// (set) Token: 0x06000EF1 RID: 3825
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000EF2 RID: 3826
		[DomName("outerHeight")]
		int OuterHeight { get; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000EF3 RID: 3827
		[DomName("outerWidth")]
		int OuterWidth { get; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000EF4 RID: 3828
		[DomName("screenX")]
		int ScreenX { get; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000EF5 RID: 3829
		[DomName("screenY")]
		int ScreenY { get; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000EF6 RID: 3830
		[DomName("window")]
		[DomName("frames")]
		[DomName("self")]
		IWindow Proxy { get; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000EF7 RID: 3831
		[DomName("navigator")]
		INavigator Navigator { get; }

		// Token: 0x06000EF8 RID: 3832
		[DomName("close")]
		void Close();

		// Token: 0x06000EF9 RID: 3833
		IWindow Open(string url = "about:blank", string name = null, string features = null, string replace = null);

		// Token: 0x06000EFA RID: 3834
		[DomName("stop")]
		void Stop();

		// Token: 0x06000EFB RID: 3835
		[DomName("focus")]
		void Focus();

		// Token: 0x06000EFC RID: 3836
		[DomName("blur")]
		void Blur();

		// Token: 0x06000EFD RID: 3837
		[DomName("alert")]
		void Alert(string message);

		// Token: 0x06000EFE RID: 3838
		[DomName("confirm")]
		bool Confirm(string message);

		// Token: 0x06000EFF RID: 3839
		[DomName("print")]
		void Print();

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000F00 RID: 3840
		[DomName("history")]
		IHistory History { get; }

		// Token: 0x06000F01 RID: 3841
		[DomName("matchMedia")]
		IMediaQueryList MatchMedia(string media);
	}
}
