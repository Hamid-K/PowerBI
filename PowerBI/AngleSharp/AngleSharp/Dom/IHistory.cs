using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000187 RID: 391
	[DomName("History")]
	public interface IHistory
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000E36 RID: 3638
		[DomName("length")]
		int Length { get; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000E37 RID: 3639
		int Index { get; }

		// Token: 0x170002BB RID: 699
		IDocument this[int index] { get; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000E39 RID: 3641
		[DomName("state")]
		object State { get; }

		// Token: 0x06000E3A RID: 3642
		[DomName("go")]
		void Go(int delta = 0);

		// Token: 0x06000E3B RID: 3643
		[DomName("back")]
		void Back();

		// Token: 0x06000E3C RID: 3644
		[DomName("forward")]
		void Forward();

		// Token: 0x06000E3D RID: 3645
		[DomName("pushState")]
		void PushState(object data, string title, string url = null);

		// Token: 0x06000E3E RID: 3646
		[DomName("replaceState")]
		void ReplaceState(object data, string title, string url = null);
	}
}
