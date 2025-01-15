using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000018 RID: 24
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	internal sealed class LibraryImportAttribute : Attribute
	{
		// Token: 0x0600003C RID: 60 RVA: 0x0000274C File Offset: 0x0000094C
		public LibraryImportAttribute(string libraryName)
		{
			this.LibraryName = libraryName;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000275B File Offset: 0x0000095B
		public string LibraryName { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002763 File Offset: 0x00000963
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000276B File Offset: 0x0000096B
		public string EntryPoint { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002774 File Offset: 0x00000974
		// (set) Token: 0x06000041 RID: 65 RVA: 0x0000277C File Offset: 0x0000097C
		public StringMarshalling StringMarshalling { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002785 File Offset: 0x00000985
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000278D File Offset: 0x0000098D
		public Type StringMarshallingCustomType { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002796 File Offset: 0x00000996
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000279E File Offset: 0x0000099E
		public bool SetLastError { get; set; }
	}
}
