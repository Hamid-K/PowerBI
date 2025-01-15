using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x0200007B RID: 123
	internal class ErrorContext
	{
		// Token: 0x0600066A RID: 1642 RVA: 0x0001BAB0 File Offset: 0x00019CB0
		internal ErrorContext([Nullable(2)] object originalObject, [Nullable(2)] object member, string path, Exception error)
		{
			this.OriginalObject = originalObject;
			this.Member = member;
			this.Error = error;
			this.Path = path;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001BAD5 File Offset: 0x00019CD5
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x0001BADD File Offset: 0x00019CDD
		internal bool Traced { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001BAE6 File Offset: 0x00019CE6
		public Exception Error { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001BAEE File Offset: 0x00019CEE
		[Nullable(2)]
		public object OriginalObject
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001BAF6 File Offset: 0x00019CF6
		[Nullable(2)]
		public object Member
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001BAFE File Offset: 0x00019CFE
		public string Path { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001BB06 File Offset: 0x00019D06
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0001BB0E File Offset: 0x00019D0E
		public bool Handled { get; set; }
	}
}
