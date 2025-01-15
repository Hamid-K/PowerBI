using System;
using Microsoft.HostIntegration.TI;

namespace Microsoft.HostIntegration.CounterTelemetry.TI
{
	// Token: 0x020005FF RID: 1535
	public class TiWipCounterTelemetryContainer : CounterTelemetryContainer
	{
		// Token: 0x06003427 RID: 13351 RVA: 0x000AE100 File Offset: 0x000AC300
		static TiWipCounterTelemetryContainer()
		{
			Array values = Enum.GetValues(typeof(ApplicationIntegrationSubFeature));
			TiWipCounterTelemetryContainer.featureInformation = new TelemetryFeatureInformation(3U, (uint)values.Length);
			TelemetryCounterCollection[] array = new TelemetryCounterCollection[1];
			Array values2 = Enum.GetValues(typeof(DynamicRemoteEnvironmentTypes));
			uint[] array2 = new uint[values2.Length];
			int num = 0;
			foreach (object obj in values2)
			{
				uint num2 = (uint)((int)obj);
				array2[num++] = num2;
			}
			array[0] = new TelemetryIntegerCounterCollection(array2);
			TiWipCounterTelemetryContainer.subFeatureInformation = new TelemetrySubFeatureInformation(0U, array);
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000AE1B8 File Offset: 0x000AC3B8
		public TiWipCounterTelemetryContainer()
			: base(TiWipCounterTelemetryContainer.featureInformation, TiWipCounterTelemetryContainer.subFeatureInformation)
		{
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000ADF99 File Offset: 0x000AC199
		public void Increment(DynamicRemoteEnvironmentTypes counterIdentifier)
		{
			base.Increment((uint)counterIdentifier);
		}

		// Token: 0x04001D6F RID: 7535
		private static TelemetryFeatureInformation featureInformation;

		// Token: 0x04001D70 RID: 7536
		private static TelemetrySubFeatureInformation subFeatureInformation;
	}
}
