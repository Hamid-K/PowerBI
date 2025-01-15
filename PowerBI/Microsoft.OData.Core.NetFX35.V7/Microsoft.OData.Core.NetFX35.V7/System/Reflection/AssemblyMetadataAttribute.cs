using System;

namespace System.Reflection
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(1, AllowMultiple = true, Inherited = false)]
	internal sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002188 File Offset: 0x00000388
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000219E File Offset: 0x0000039E
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000021A6 File Offset: 0x000003A6
		public string Key { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021AF File Offset: 0x000003AF
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021B7 File Offset: 0x000003B7
		public string Value { get; set; }
	}
}
