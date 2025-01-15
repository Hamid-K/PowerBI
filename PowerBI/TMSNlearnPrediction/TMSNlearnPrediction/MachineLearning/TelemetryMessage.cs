using System;
using System.Collections.Generic;

namespace Microsoft.MachineLearning
{
	// Token: 0x02000144 RID: 324
	public abstract class TelemetryMessage
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x0002311C File Offset: 0x0002131C
		public static TelemetryMessage CreateCommand(string commandName, string commandText)
		{
			return new TelemetryTrace(commandText, commandName, "Command");
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0002312A File Offset: 0x0002132A
		public static TelemetryMessage CreateTrainer(string trainerName, string trainerParams)
		{
			return new TelemetryTrace(trainerParams, trainerName, "Trainer");
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00023138 File Offset: 0x00021338
		public static TelemetryMessage CreateTransform(string transformName, string transformParams)
		{
			return new TelemetryTrace(transformParams, transformName, "Transform");
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00023146 File Offset: 0x00021346
		public static TelemetryMessage CreateMetric(string metricName, double metricValue, Dictionary<string, string> properties)
		{
			return new TelemetryMetric(metricName, metricValue, properties);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00023150 File Offset: 0x00021350
		public static TelemetryMessage CreateException(Exception exception)
		{
			return new TelemetryException(exception);
		}
	}
}
