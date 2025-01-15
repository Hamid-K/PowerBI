using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing.Ffp
{
	// Token: 0x0200067D RID: 1661
	public class FlatFileProcessorTraceContainer : TraceContainer
	{
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x060037A9 RID: 14249 RVA: 0x00006F04 File Offset: 0x00005104
		public static bool ConstructorNeedsInstanceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000BBDE4 File Offset: 0x000B9FE4
		static FlatFileProcessorTraceContainer()
		{
			TracePointPropertyInformation tracePointPropertyInformation = new TracePointPropertyInformation("Read Or Write", 0, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("Read", 0),
				new TracePointPropertyEnumerationValue("Write", 1)
			});
			TracePointPropertyInformation tracePointPropertyInformation2 = new TracePointPropertyInformation("Source Or Destination", 1, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("File", 0),
				new TracePointPropertyEnumerationValue("Bytes", 1),
				new TracePointPropertyEnumerationValue("Stream", 2)
			});
			TracePointPropertyInformation tracePointPropertyInformation3 = new TracePointPropertyInformation("Single- Or Multi- Threaded", 2, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("Single", 0),
				new TracePointPropertyEnumerationValue("Multi", 1)
			});
			TracePointPropertyInformation tracePointPropertyInformation4 = new TracePointPropertyInformation("Aggregate Converter Type", 3, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("Base", 0),
				new TracePointPropertyEnumerationValue("DPC", 1)
			});
			TracePointPropertyInformation tracePointPropertyInformation5 = new TracePointPropertyInformation("Primitive Converter Type", 4, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("System Z", 0),
				new TracePointPropertyEnumerationValue("System I", 1)
			});
			List<ITracePointPropertyInformation> list = new List<ITracePointPropertyInformation>();
			list.Add(tracePointPropertyInformation);
			list.Add(tracePointPropertyInformation2);
			list.Add(tracePointPropertyInformation3);
			List<ITracePointPropertyInformation> list2 = new List<ITracePointPropertyInformation>();
			list2.Add(tracePointPropertyInformation4);
			TracePointInformation tracePointInformation = new TracePointInformation(false, "Primitive Converter", 2, new List<ITracePointPropertyInformation> { tracePointPropertyInformation5 }, null);
			TracePointInformation tracePointInformation2 = new TracePointInformation(false, "Aggregate Converter", 1, list2, new List<ITracePointInformation> { tracePointInformation });
			TracePointInformation tracePointInformation3 = new TracePointInformation(false, "Flat File Processor", 0, list, new List<ITracePointInformation> { tracePointInformation2 });
			FlatFileProcessorTraceContainer.tracePoints = new List<ITracePointInformation>();
			FlatFileProcessorTraceContainer.tracePoints.Add(tracePointInformation3);
			FlatFileProcessorTraceContainer.commonTracePointIdentifierToSpecific = TraceContainers.InitializeIntArrayToMinus1(3);
			FlatFileProcessorTraceContainer.commonTracePointIdentifierToSpecific[0] = 1;
			FlatFileProcessorTraceContainer.commonTracePointIdentifierToSpecific[1] = 2;
			FlatFileProcessorTraceContainer.commonTracePointPropertyIdentifierToSpecific = new int[3][];
			FlatFileProcessorTraceContainer.commonTracePointPropertyIdentifierToSpecific[0] = TraceContainers.InitializeIntArrayToMinus1(1);
			FlatFileProcessorTraceContainer.commonTracePointPropertyIdentifierToSpecific[0][0] = 3;
			FlatFileProcessorTraceContainer.commonTracePointPropertyIdentifierToSpecific[1] = TraceContainers.InitializeIntArrayToMinus1(1);
			FlatFileProcessorTraceContainer.commonTracePointPropertyIdentifierToSpecific[1][0] = 4;
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000BC020 File Offset: 0x000BA220
		public FlatFileProcessorTraceContainer()
			: base(false, false, "FFP", "Flat File Processor", "", ContainerIdentifier.FlatFileProcessor, FlatFileProcessorTraceContainer.tracePoints, FlatFileProcessorTraceContainer.commonTracePointIdentifierToSpecific, FlatFileProcessorTraceContainer.commonTracePointPropertyIdentifierToSpecific, false)
		{
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000BC058 File Offset: 0x000BA258
		public FlatFileProcessorTraceContainer(string configurationFileName)
			: base(false, false, "FFP", "Flat File Processor", "", ContainerIdentifier.FlatFileProcessor, FlatFileProcessorTraceContainer.tracePoints, configurationFileName)
		{
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000BC083 File Offset: 0x000BA283
		public FlatFileProcessorTraceContainer(TraceContainer traceContainer)
			: base(traceContainer)
		{
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000BC08C File Offset: 0x000BA28C
		public override TraceContainer GetTraceDefinition(TraceContainer traceContainer)
		{
			return new FlatFileProcessorTraceContainer(traceContainer);
		}

		// Token: 0x04001FEA RID: 8170
		private static List<ITracePointInformation> tracePoints;

		// Token: 0x04001FEB RID: 8171
		private static int[] commonTracePointIdentifierToSpecific;

		// Token: 0x04001FEC RID: 8172
		private static int[][] commonTracePointPropertyIdentifierToSpecific;
	}
}
