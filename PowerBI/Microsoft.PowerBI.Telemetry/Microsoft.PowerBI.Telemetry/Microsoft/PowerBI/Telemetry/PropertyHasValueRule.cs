using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000011 RID: 17
	public class PropertyHasValueRule : IEventTransmissionRule
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002AC2 File Offset: 0x00000CC2
		public PropertyHasValueRule(string name, string value)
		{
			this.propName = name;
			this.propValue = value;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public bool ShouldAccept(ITelemetryEvent telemetryEvent)
		{
			string text = null;
			return telemetryEvent.Properties.TryGetValue(this.propName, out text) && this.propValue.Equals(text);
		}

		// Token: 0x04000038 RID: 56
		private string propName;

		// Token: 0x04000039 RID: 57
		private string propValue;
	}
}
