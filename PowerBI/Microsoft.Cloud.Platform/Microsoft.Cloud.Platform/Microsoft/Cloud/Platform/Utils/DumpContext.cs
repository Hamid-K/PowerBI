using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001E5 RID: 485
	[Serializable]
	public sealed class DumpContext
	{
		// Token: 0x06000C9D RID: 3229 RVA: 0x0002C162 File Offset: 0x0002A362
		public DumpContext(Type exceptionType, string stack, string hash, bool fatal, bool duplicate)
		{
			this.ExceptionType = exceptionType;
			this.Stack = stack;
			this.Hash = hash;
			this.Fatal = fatal;
			this.Duplicate = duplicate;
			this.Timestamp = DateTime.UtcNow;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0002C19A File Offset: 0x0002A39A
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x0002C1A2 File Offset: 0x0002A3A2
		public Type ExceptionType { get; private set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0002C1AB File Offset: 0x0002A3AB
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x0002C1B3 File Offset: 0x0002A3B3
		public string Stack { get; private set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0002C1BC File Offset: 0x0002A3BC
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0002C1C4 File Offset: 0x0002A3C4
		public string Hash { get; private set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0002C1CD File Offset: 0x0002A3CD
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x0002C1D5 File Offset: 0x0002A3D5
		public bool Fatal { get; private set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0002C1DE File Offset: 0x0002A3DE
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0002C1E6 File Offset: 0x0002A3E6
		public bool Duplicate { get; private set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0002C1EF File Offset: 0x0002A3EF
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		public DateTime Timestamp { get; private set; }
	}
}
