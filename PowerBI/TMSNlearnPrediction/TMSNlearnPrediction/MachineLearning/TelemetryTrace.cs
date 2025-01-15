using System;

namespace Microsoft.MachineLearning
{
	// Token: 0x02000145 RID: 325
	public sealed class TelemetryTrace : TelemetryMessage
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x00023160 File Offset: 0x00021360
		public TelemetryTrace(string text, string name, string type)
		{
			this.Text = text;
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x0400035C RID: 860
		public readonly string Text;

		// Token: 0x0400035D RID: 861
		public readonly string Name;

		// Token: 0x0400035E RID: 862
		public readonly string Type;
	}
}
