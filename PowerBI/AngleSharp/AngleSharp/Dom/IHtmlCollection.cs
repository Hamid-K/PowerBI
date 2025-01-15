using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000189 RID: 393
	[DomName("HTMLCollection")]
	public interface IHtmlCollection<T> : IEnumerable<T>, IEnumerable where T : IElement
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000E3F RID: 3647
		[DomName("length")]
		int Length { get; }

		// Token: 0x170002BE RID: 702
		[DomName("item")]
		[DomAccessor(Accessors.Getter)]
		T this[int index] { get; }

		// Token: 0x170002BF RID: 703
		[DomName("namedItem")]
		[DomAccessor(Accessors.Getter)]
		T this[string id] { get; }
	}
}
