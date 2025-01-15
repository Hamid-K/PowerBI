using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000835 RID: 2101
	public enum SeverityCode
	{
		// Token: 0x04002EE5 RID: 12005
		Info,
		// Token: 0x04002EE6 RID: 12006
		Warning = 4,
		// Token: 0x04002EE7 RID: 12007
		Error = 8,
		// Token: 0x04002EE8 RID: 12008
		Severe = 16,
		// Token: 0x04002EE9 RID: 12009
		AccessDamage = 32,
		// Token: 0x04002EEA RID: 12010
		PermanentDamage = 64,
		// Token: 0x04002EEB RID: 12011
		SessionDamage = 128
	}
}
