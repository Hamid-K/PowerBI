using System;
using System.ComponentModel;

namespace Microsoft.IdentityModel.Protocols
{
	// Token: 0x02000004 RID: 4
	public class ConfigurationValidationResult
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000268E File Offset: 0x0000088E
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002696 File Offset: 0x00000896
		public string ErrorMessage { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000269F File Offset: 0x0000089F
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000026A7 File Offset: 0x000008A7
		[DefaultValue(false)]
		public bool Succeeded { get; set; }
	}
}
