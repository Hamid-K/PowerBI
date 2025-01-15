using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x020001A1 RID: 417
	[DomName("DOMTokenList")]
	public interface ITokenList : IEnumerable<string>, IEnumerable
	{
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000EC3 RID: 3779
		[DomName("length")]
		int Length { get; }

		// Token: 0x17000305 RID: 773
		[DomName("item")]
		[DomAccessor(Accessors.Getter)]
		string this[int index] { get; }

		// Token: 0x06000EC5 RID: 3781
		[DomName("contains")]
		bool Contains(string token);

		// Token: 0x06000EC6 RID: 3782
		[DomName("add")]
		void Add(params string[] tokens);

		// Token: 0x06000EC7 RID: 3783
		[DomName("remove")]
		void Remove(params string[] tokens);

		// Token: 0x06000EC8 RID: 3784
		[DomName("toggle")]
		bool Toggle(string token, bool force = false);
	}
}
