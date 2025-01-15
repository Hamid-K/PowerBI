using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200053C RID: 1340
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public sealed class AnalysisPayloadAttribute : Attribute
	{
		// Token: 0x060028D5 RID: 10453 RVA: 0x0009269B File Offset: 0x0009089B
		public AnalysisPayloadAttribute(int payloadIndex)
		{
			this.PayloadIndex = payloadIndex;
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x000926AA File Offset: 0x000908AA
		// (set) Token: 0x060028D7 RID: 10455 RVA: 0x000926B2 File Offset: 0x000908B2
		public int PayloadIndex { get; private set; }
	}
}
