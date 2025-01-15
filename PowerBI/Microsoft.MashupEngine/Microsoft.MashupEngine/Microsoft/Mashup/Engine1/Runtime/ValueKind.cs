using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016A7 RID: 5799
	public enum ValueKind
	{
		// Token: 0x04004EB1 RID: 20145
		Null,
		// Token: 0x04004EB2 RID: 20146
		Time,
		// Token: 0x04004EB3 RID: 20147
		Date,
		// Token: 0x04004EB4 RID: 20148
		DateTime,
		// Token: 0x04004EB5 RID: 20149
		DateTimeZone,
		// Token: 0x04004EB6 RID: 20150
		Duration,
		// Token: 0x04004EB7 RID: 20151
		Number,
		// Token: 0x04004EB8 RID: 20152
		Logical,
		// Token: 0x04004EB9 RID: 20153
		Text,
		// Token: 0x04004EBA RID: 20154
		Binary,
		// Token: 0x04004EBB RID: 20155
		List,
		// Token: 0x04004EBC RID: 20156
		Record,
		// Token: 0x04004EBD RID: 20157
		Table,
		// Token: 0x04004EBE RID: 20158
		Function,
		// Token: 0x04004EBF RID: 20159
		Type,
		// Token: 0x04004EC0 RID: 20160
		Action,
		// Token: 0x04004EC1 RID: 20161
		Count,
		// Token: 0x04004EC2 RID: 20162
		Any = -1,
		// Token: 0x04004EC3 RID: 20163
		None = -2,
		// Token: 0x04004EC4 RID: 20164
		Skipped = -3,
		// Token: 0x04004EC5 RID: 20165
		Reference = -4
	}
}
