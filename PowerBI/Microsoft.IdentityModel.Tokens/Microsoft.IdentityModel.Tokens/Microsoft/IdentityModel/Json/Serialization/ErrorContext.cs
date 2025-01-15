using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x0200007C RID: 124
	[NullableContext(1)]
	[Nullable(0)]
	internal class ErrorContext
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x0001C084 File Offset: 0x0001A284
		internal ErrorContext([Nullable(2)] object originalObject, [Nullable(2)] object member, string path, Exception error)
		{
			this.OriginalObject = originalObject;
			this.Member = member;
			this.Error = error;
			this.Path = path;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001C0A9 File Offset: 0x0001A2A9
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0001C0B1 File Offset: 0x0001A2B1
		internal bool Traced { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001C0BA File Offset: 0x0001A2BA
		public Exception Error { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001C0C2 File Offset: 0x0001A2C2
		[Nullable(2)]
		public object OriginalObject
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001C0CA File Offset: 0x0001A2CA
		[Nullable(2)]
		public object Member
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001C0D2 File Offset: 0x0001A2D2
		public string Path { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001C0DA File Offset: 0x0001A2DA
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0001C0E2 File Offset: 0x0001A2E2
		public bool Handled { get; set; }
	}
}
