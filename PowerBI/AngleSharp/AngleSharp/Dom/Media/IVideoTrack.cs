using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001D7 RID: 471
	[DomName("VideoTrack")]
	public interface IVideoTrack
	{
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000FC7 RID: 4039
		[DomName("id")]
		string Id { get; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000FC8 RID: 4040
		[DomName("kind")]
		string Kind { get; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000FC9 RID: 4041
		[DomName("label")]
		string Label { get; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000FCA RID: 4042
		[DomName("language")]
		string Language { get; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000FCB RID: 4043
		// (set) Token: 0x06000FCC RID: 4044
		[DomName("selected")]
		bool IsSelected { get; set; }
	}
}
