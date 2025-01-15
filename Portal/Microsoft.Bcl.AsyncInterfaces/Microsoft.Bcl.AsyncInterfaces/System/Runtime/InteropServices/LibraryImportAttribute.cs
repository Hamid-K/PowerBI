using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	internal sealed class LibraryImportAttribute : Attribute
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000021AC File Offset: 0x000003AC
		public LibraryImportAttribute(string libraryName)
		{
			this.LibraryName = libraryName;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000021BB File Offset: 0x000003BB
		public string LibraryName { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000021C3 File Offset: 0x000003C3
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000021CB File Offset: 0x000003CB
		public string EntryPoint { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000021D4 File Offset: 0x000003D4
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000021DC File Offset: 0x000003DC
		public StringMarshalling StringMarshalling { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000021E5 File Offset: 0x000003E5
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000021ED File Offset: 0x000003ED
		public Type StringMarshallingCustomType { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000021F6 File Offset: 0x000003F6
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000021FE File Offset: 0x000003FE
		public bool SetLastError { get; set; }
	}
}
