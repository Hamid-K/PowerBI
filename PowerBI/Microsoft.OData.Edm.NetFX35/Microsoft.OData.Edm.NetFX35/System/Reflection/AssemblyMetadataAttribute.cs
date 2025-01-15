using System;

namespace System.Reflection
{
	// Token: 0x0200027F RID: 639
	[AttributeUsage(1, AllowMultiple = true, Inherited = false)]
	internal sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x0002DDF1 File Offset: 0x0002BFF1
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0002DE07 File Offset: 0x0002C007
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x0002DE0F File Offset: 0x0002C00F
		public string Key { get; set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0002DE18 File Offset: 0x0002C018
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x0002DE20 File Offset: 0x0002C020
		public string Value { get; set; }
	}
}
