using System;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat.Parameters
{
	// Token: 0x02000BB6 RID: 2998
	public enum StringOperator
	{
		// Token: 0x04004E8D RID: 20109
		GreaterThan = 4,
		// Token: 0x04004E8E RID: 20110
		LessThan = 1,
		// Token: 0x04004E8F RID: 20111
		Equal,
		// Token: 0x04004E90 RID: 20112
		NotEqual = 5,
		// Token: 0x04004E91 RID: 20113
		NotLessThan,
		// Token: 0x04004E92 RID: 20114
		NotGreaterThan = 3,
		// Token: 0x04004E93 RID: 20115
		Contains = 10,
		// Token: 0x04004E94 RID: 20116
		Excludes = 13,
		// Token: 0x04004E95 RID: 20117
		ContainsGeneric = 26,
		// Token: 0x04004E96 RID: 20118
		ExcludesGeneric = 29,
		// Token: 0x04004E97 RID: 20119
		Like = 18,
		// Token: 0x04004E98 RID: 20120
		NotLike = 21
	}
}
