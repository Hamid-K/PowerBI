using System;

namespace Microsoft.Identity.Json
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	internal class JsonExtensionDataAttribute : Attribute
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002FE8 File Offset: 0x000011E8
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00002FF0 File Offset: 0x000011F0
		public bool WriteData { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002FF9 File Offset: 0x000011F9
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003001 File Offset: 0x00001201
		public bool ReadData { get; set; }

		// Token: 0x0600009E RID: 158 RVA: 0x0000300A File Offset: 0x0000120A
		public JsonExtensionDataAttribute()
		{
			this.WriteData = true;
			this.ReadData = true;
		}
	}
}
