using System;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	internal class JsonExtensionDataAttribute : Attribute
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002FFC File Offset: 0x000011FC
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003004 File Offset: 0x00001204
		public bool WriteData { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000300D File Offset: 0x0000120D
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003015 File Offset: 0x00001215
		public bool ReadData { get; set; }

		// Token: 0x0600009E RID: 158 RVA: 0x0000301E File Offset: 0x0000121E
		public JsonExtensionDataAttribute()
		{
			this.WriteData = true;
			this.ReadData = true;
		}
	}
}
