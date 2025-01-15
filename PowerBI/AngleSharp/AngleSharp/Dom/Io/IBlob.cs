using System;
using System.IO;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Io
{
	// Token: 0x020001C4 RID: 452
	[DomName("Blob")]
	public interface IBlob : IDisposable
	{
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000F3D RID: 3901
		[DomName("size")]
		int Length { get; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000F3E RID: 3902
		[DomName("type")]
		string Type { get; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000F3F RID: 3903
		[DomName("isClosed")]
		bool IsClosed { get; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000F40 RID: 3904
		Stream Body { get; }

		// Token: 0x06000F41 RID: 3905
		[DomName("slice")]
		IBlob Slice(int start = 0, int end = 2147483647, string contentType = null);

		// Token: 0x06000F42 RID: 3906
		[DomName("close")]
		void Close();
	}
}
