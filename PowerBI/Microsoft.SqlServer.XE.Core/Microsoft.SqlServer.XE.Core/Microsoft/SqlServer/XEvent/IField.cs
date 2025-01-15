using System;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000025 RID: 37
	public interface IField
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009E RID: 158
		string Name { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009F RID: 159
		object Value { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A0 RID: 160
		Type Type { get; }
	}
}
