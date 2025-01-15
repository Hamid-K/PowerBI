using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000022 RID: 34
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	internal sealed class LibraryImportAttribute : Attribute
	{
		// Token: 0x06000131 RID: 305 RVA: 0x000032A6 File Offset: 0x000014A6
		public LibraryImportAttribute(string libraryName)
		{
			this.LibraryName = libraryName;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000032B5 File Offset: 0x000014B5
		public string LibraryName { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000032BD File Offset: 0x000014BD
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000032C5 File Offset: 0x000014C5
		public string EntryPoint { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000032CE File Offset: 0x000014CE
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000032D6 File Offset: 0x000014D6
		public StringMarshalling StringMarshalling { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000032DF File Offset: 0x000014DF
		// (set) Token: 0x06000138 RID: 312 RVA: 0x000032E7 File Offset: 0x000014E7
		public Type StringMarshallingCustomType { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000032F0 File Offset: 0x000014F0
		// (set) Token: 0x0600013A RID: 314 RVA: 0x000032F8 File Offset: 0x000014F8
		public bool SetLastError { get; set; }
	}
}
