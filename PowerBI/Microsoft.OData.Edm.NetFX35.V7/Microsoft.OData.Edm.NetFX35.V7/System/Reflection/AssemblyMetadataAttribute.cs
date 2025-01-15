using System;

namespace System.Reflection
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(1, AllowMultiple = true, Inherited = false)]
	internal sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002066 File Offset: 0x00000266
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000206E File Offset: 0x0000026E
		public string Key { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002077 File Offset: 0x00000277
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000207F File Offset: 0x0000027F
		public string Value { get; set; }
	}
}
