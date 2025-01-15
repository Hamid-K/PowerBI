using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001CC RID: 460
	[DomName("AudioTrack")]
	public interface IAudioTrack
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000F4A RID: 3914
		[DomName("id")]
		string Id { get; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000F4B RID: 3915
		[DomName("kind")]
		string Kind { get; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000F4C RID: 3916
		[DomName("label")]
		string Label { get; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000F4D RID: 3917
		[DomName("language")]
		string Language { get; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000F4E RID: 3918
		// (set) Token: 0x06000F4F RID: 3919
		[DomName("enabled")]
		bool IsEnabled { get; set; }
	}
}
