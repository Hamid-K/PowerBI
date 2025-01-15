using System;
using System.Collections.ObjectModel;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000356 RID: 854
	public sealed class MonitoredErrorParameterFactory : FlattenedParameterFactoryBase
	{
		// Token: 0x06001949 RID: 6473 RVA: 0x0005E113 File Offset: 0x0005C313
		public MonitoredErrorParameterFactory()
			: base(new ReadOnlyCollection<FlattenedPropertyMetadata>(MonitoredErrorParameterFactory.c_parameters), typeof(IMonitoredError), typeof(FlattenedMonitoredError), "an Exception derived from MonitoredException or MessageDeliveryStatus or any other object implementing IMonitoredError.")
		{
		}

		// Token: 0x040008BA RID: 2234
		private static readonly FlattenedPropertyMetadata[] c_parameters = new FlattenedPropertyMetadata[]
		{
			new FlattenedPropertyMetadata(typeof(long), "ErrorCorrelationId.CorrelationId", "correlationId"),
			new FlattenedPropertyMetadata(typeof(int), "ErrorCorrelationId.SequenceNumber", "sequenceNumber"),
			new FlattenedPropertyMetadata(typeof(string), "ToStringLimitedLength(true)", "text"),
			new FlattenedPropertyMetadata(typeof(string), "InnerMessageLimitedLength()", "innerMessage"),
			new FlattenedPropertyMetadata(typeof(string), "MonitoringScopeNameOrEmptyString()", "monitoringScope")
		};

		// Token: 0x040008BB RID: 2235
		private const string c_validTypes = "an Exception derived from MonitoredException or MessageDeliveryStatus or any other object implementing IMonitoredError.";
	}
}
