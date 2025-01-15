using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing.ConversionPipeline
{
	// Token: 0x02000692 RID: 1682
	public class ConversionPipelineTraceContainer : TraceContainer
	{
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x00006F04 File Offset: 0x00005104
		public static bool ConstructorNeedsInstanceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000BC3D4 File Offset: 0x000BA5D4
		static ConversionPipelineTraceContainer()
		{
			TracePointPropertyInformation tracePointPropertyInformation = new TracePointPropertyInformation("Direction", 0, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("To Host", 0),
				new TracePointPropertyEnumerationValue("From Host", 1)
			});
			TracePointPropertyInformation tracePointPropertyInformation2 = new TracePointPropertyInformation("MetaData File Name", 1, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation3 = new TracePointPropertyInformation("Method Name", 2, PropertyType.String, null);
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
			TracePointInformation tracePointInformation3 = new TracePointInformation(false, "Pipeline", 0, list, new List<ITracePointInformation> { tracePointInformation2 });
			ConversionPipelineTraceContainer.tracePoints = new List<ITracePointInformation>();
			ConversionPipelineTraceContainer.tracePoints.Add(tracePointInformation3);
			ConversionPipelineTraceContainer.commonTracePointIdentifierToSpecific = TraceContainers.InitializeIntArrayToMinus1(3);
			ConversionPipelineTraceContainer.commonTracePointIdentifierToSpecific[0] = 1;
			ConversionPipelineTraceContainer.commonTracePointIdentifierToSpecific[1] = 2;
			ConversionPipelineTraceContainer.commonTracePointPropertyIdentifierToSpecific = new int[3][];
			ConversionPipelineTraceContainer.commonTracePointPropertyIdentifierToSpecific[0] = TraceContainers.InitializeIntArrayToMinus1(1);
			ConversionPipelineTraceContainer.commonTracePointPropertyIdentifierToSpecific[0][0] = 3;
			ConversionPipelineTraceContainer.commonTracePointPropertyIdentifierToSpecific[1] = TraceContainers.InitializeIntArrayToMinus1(1);
			ConversionPipelineTraceContainer.commonTracePointPropertyIdentifierToSpecific[1][0] = 4;
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000BC5A8 File Offset: 0x000BA7A8
		public ConversionPipelineTraceContainer()
			: base(false, false, "Conversion Pipeline", "Conversion Pipeline", "", ContainerIdentifier.ConversionPipeline, ConversionPipelineTraceContainer.tracePoints, ConversionPipelineTraceContainer.commonTracePointIdentifierToSpecific, ConversionPipelineTraceContainer.commonTracePointPropertyIdentifierToSpecific, false)
		{
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000BC5E0 File Offset: 0x000BA7E0
		public ConversionPipelineTraceContainer(string configurationFileName)
			: base(false, false, "Conversion Pipeline", "Conversion Pipeline", "", ContainerIdentifier.ConversionPipeline, ConversionPipelineTraceContainer.tracePoints, configurationFileName)
		{
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000BC083 File Offset: 0x000BA283
		public ConversionPipelineTraceContainer(TraceContainer traceContainer)
			: base(traceContainer)
		{
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000BC60B File Offset: 0x000BA80B
		public override TraceContainer GetTraceDefinition(TraceContainer traceContainer)
		{
			return new ConversionPipelineTraceContainer(traceContainer);
		}

		// Token: 0x04002011 RID: 8209
		private static List<ITracePointInformation> tracePoints;

		// Token: 0x04002012 RID: 8210
		private static int[] commonTracePointIdentifierToSpecific;

		// Token: 0x04002013 RID: 8211
		private static int[][] commonTracePointPropertyIdentifierToSpecific;
	}
}
