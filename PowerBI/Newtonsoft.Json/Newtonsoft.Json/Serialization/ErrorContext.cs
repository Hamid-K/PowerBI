using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007B RID: 123
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorContext
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0001C058 File Offset: 0x0001A258
		internal ErrorContext([Nullable(2)] object originalObject, [Nullable(2)] object member, string path, Exception error)
		{
			this.OriginalObject = originalObject;
			this.Member = member;
			this.Error = error;
			this.Path = path;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001C07D File Offset: 0x0001A27D
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0001C085 File Offset: 0x0001A285
		internal bool Traced { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001C08E File Offset: 0x0001A28E
		public Exception Error { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001C096 File Offset: 0x0001A296
		[Nullable(2)]
		public object OriginalObject
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001C09E File Offset: 0x0001A29E
		[Nullable(2)]
		public object Member
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001C0A6 File Offset: 0x0001A2A6
		public string Path { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001C0AE File Offset: 0x0001A2AE
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0001C0B6 File Offset: 0x0001A2B6
		public bool Handled { get; set; }
	}
}
