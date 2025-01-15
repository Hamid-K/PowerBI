using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Io
{
	// Token: 0x020001C6 RID: 454
	[DomName("FileList")]
	public interface IFileList : IEnumerable<IFile>, IEnumerable
	{
		// Token: 0x17000333 RID: 819
		[DomName("item")]
		[DomAccessor(Accessors.Getter)]
		IFile this[int index] { get; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000F46 RID: 3910
		[DomName("length")]
		int Length { get; }

		// Token: 0x06000F47 RID: 3911
		void Add(IFile file);

		// Token: 0x06000F48 RID: 3912
		bool Remove(IFile file);

		// Token: 0x06000F49 RID: 3913
		void Clear();
	}
}
