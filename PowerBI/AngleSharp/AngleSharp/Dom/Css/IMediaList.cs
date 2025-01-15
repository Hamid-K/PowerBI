using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000335 RID: 821
	[DomName("MediaList")]
	public interface IMediaList : ICssNode, IStyleFormattable, IEnumerable<ICssMedium>, IEnumerable
	{
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600190B RID: 6411
		// (set) Token: 0x0600190C RID: 6412
		[DomName("mediaText")]
		string MediaText { get; set; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600190D RID: 6413
		[DomName("length")]
		int Length { get; }

		// Token: 0x17000714 RID: 1812
		[DomAccessor(Accessors.Getter)]
		[DomName("item")]
		string this[int index] { get; }

		// Token: 0x0600190F RID: 6415
		[DomName("appendMedium")]
		void Add(string medium);

		// Token: 0x06001910 RID: 6416
		[DomName("removeMedium")]
		void Remove(string medium);

		// Token: 0x06001911 RID: 6417
		bool Validate(RenderDevice device);
	}
}
