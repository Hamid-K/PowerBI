using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Io
{
	// Token: 0x020001C5 RID: 453
	[DomName("File")]
	public interface IFile : IBlob, IDisposable
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000F43 RID: 3907
		[DomName("name")]
		string Name { get; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000F44 RID: 3908
		[DomName("lastModified")]
		DateTime LastModified { get; }
	}
}
