using System;

namespace System.Reflection
{
	// Token: 0x020002C9 RID: 713
	[AttributeUsage(1, AllowMultiple = true, Inherited = false)]
	internal sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x06001B86 RID: 7046 RVA: 0x0005902D File Offset: 0x0005722D
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x00059043 File Offset: 0x00057243
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x0005904B File Offset: 0x0005724B
		public string Key { get; set; }

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x00059054 File Offset: 0x00057254
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x0005905C File Offset: 0x0005725C
		public string Value { get; set; }
	}
}
