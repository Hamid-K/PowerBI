using System;

namespace Microsoft.HostIntegration.CounterTelemetry.VS
{
	// Token: 0x020005FB RID: 1531
	public class DesignerCounterTelemetryContainer : CounterTelemetryContainer
	{
		// Token: 0x0600341B RID: 13339 RVA: 0x000ADF28 File Offset: 0x000AC128
		static DesignerCounterTelemetryContainer()
		{
			Array values = Enum.GetValues(typeof(VsIntegrationSubFeature));
			DesignerCounterTelemetryContainer.featureInformation = new TelemetryFeatureInformation(7U, (uint)values.Length);
			TelemetryCounterCollection[] array = new TelemetryCounterCollection[1];
			Array values2 = Enum.GetValues(typeof(DesignerProcess));
			array[0] = new TelemetryEnumCounterCollection((uint)values2.Length);
			DesignerCounterTelemetryContainer.subFeatureInformation = new TelemetrySubFeatureInformation(0U, array);
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000ADF87 File Offset: 0x000AC187
		public DesignerCounterTelemetryContainer()
			: base(DesignerCounterTelemetryContainer.featureInformation, DesignerCounterTelemetryContainer.subFeatureInformation)
		{
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000ADF99 File Offset: 0x000AC199
		public void Increment(DesignerProcess counterIdentifier)
		{
			base.Increment((uint)counterIdentifier);
		}

		// Token: 0x04001D67 RID: 7527
		private static TelemetryFeatureInformation featureInformation;

		// Token: 0x04001D68 RID: 7528
		private static TelemetrySubFeatureInformation subFeatureInformation;
	}
}
