using System;

namespace System.Reflection
{
	// Token: 0x02000091 RID: 145
	[AttributeUsage(1, AllowMultiple = true, Inherited = false)]
	internal sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x00009F79 File Offset: 0x00008179
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00009F8F File Offset: 0x0000818F
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00009F97 File Offset: 0x00008197
		public string Key { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00009FA0 File Offset: 0x000081A0
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00009FA8 File Offset: 0x000081A8
		public string Value { get; set; }
	}
}
