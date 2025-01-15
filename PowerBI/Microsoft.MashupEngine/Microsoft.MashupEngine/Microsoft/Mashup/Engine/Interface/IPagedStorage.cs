using System;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000051 RID: 81
	public interface IPagedStorage : IDisposable
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000154 RID: 340
		int PageSize { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000155 RID: 341
		int MaxPageCount { get; }

		// Token: 0x06000156 RID: 342
		Stream OpenPage(int pageIndex, out bool created);

		// Token: 0x06000157 RID: 343
		void CommitPage(Stream stream);
	}
}
