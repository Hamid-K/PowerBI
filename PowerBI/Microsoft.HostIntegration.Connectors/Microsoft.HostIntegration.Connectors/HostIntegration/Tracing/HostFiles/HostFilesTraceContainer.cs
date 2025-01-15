using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing.HostFiles
{
	// Token: 0x02000698 RID: 1688
	public class HostFilesTraceContainer : TraceContainer
	{
		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x060037DF RID: 14303 RVA: 0x00006F04 File Offset: 0x00005104
		public static bool ConstructorNeedsInstanceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000BC630 File Offset: 0x000BA830
		static HostFilesTraceContainer()
		{
			TracePointPropertyInformation tracePointPropertyInformation = new TracePointPropertyInformation("Connection String", 0, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation2 = new TracePointPropertyInformation("MetaData File Name", 1, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation3 = new TracePointPropertyInformation("Transport Type", 2, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("SNA", 4),
				new TracePointPropertyEnumerationValue("TCP", 5)
			});
			TracePointPropertyInformation tracePointPropertyInformation4 = new TracePointPropertyInformation("IP Address", 3, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation5 = new TracePointPropertyInformation("Port", 4, PropertyType.Integer, null);
			TracePointPropertyInformation tracePointPropertyInformation6 = new TracePointPropertyInformation("Local LU", 5, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation7 = new TracePointPropertyInformation("Remote LU", 6, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation8 = new TracePointPropertyInformation("Aggregate Converter Type", 7, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("Base", 0),
				new TracePointPropertyEnumerationValue("DPC", 1)
			});
			TracePointPropertyInformation tracePointPropertyInformation9 = new TracePointPropertyInformation("Primitive Converter Type", 8, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("System Z", 0),
				new TracePointPropertyEnumerationValue("System I", 1)
			});
			List<ITracePointPropertyInformation> list = new List<ITracePointPropertyInformation>();
			list.Add(tracePointPropertyInformation);
			list.Add(tracePointPropertyInformation2);
			List<ITracePointPropertyInformation> list2 = new List<ITracePointPropertyInformation>();
			list2.Add(tracePointPropertyInformation3);
			list2.Add(tracePointPropertyInformation4);
			list2.Add(tracePointPropertyInformation5);
			list2.Add(tracePointPropertyInformation6);
			list2.Add(tracePointPropertyInformation7);
			List<ITracePointPropertyInformation> list3 = new List<ITracePointPropertyInformation>();
			list3.Add(tracePointPropertyInformation8);
			List<ITracePointPropertyInformation> list4 = new List<ITracePointPropertyInformation>();
			list4.Add(tracePointPropertyInformation9);
			TracePointInformation tracePointInformation = new TracePointInformation(false, "Transport", 1, list2, null);
			TracePointInformation tracePointInformation2 = new TracePointInformation(false, "Primitive Converter", 3, list4, null);
			TracePointInformation tracePointInformation3 = new TracePointInformation(false, "Aggregate Converter", 2, list3, new List<ITracePointInformation> { tracePointInformation2 });
			TracePointInformation tracePointInformation4 = new TracePointInformation(false, "MsHostFileClient", 0, list, new List<ITracePointInformation> { tracePointInformation, tracePointInformation3 });
			HostFilesTraceContainer.tracePoints = new List<ITracePointInformation>();
			HostFilesTraceContainer.tracePoints.Add(tracePointInformation4);
			HostFilesTraceContainer.commonTracePointIdentifierToSpecific = TraceContainers.InitializeIntArrayToMinus1(3);
			HostFilesTraceContainer.commonTracePointIdentifierToSpecific[0] = 2;
			HostFilesTraceContainer.commonTracePointIdentifierToSpecific[1] = 3;
			HostFilesTraceContainer.commonTracePointIdentifierToSpecific[2] = 1;
			HostFilesTraceContainer.commonTracePointPropertyIdentifierToSpecific = new int[3][];
			HostFilesTraceContainer.commonTracePointPropertyIdentifierToSpecific[0] = TraceContainers.InitializeIntArrayToMinus1(1);
			HostFilesTraceContainer.commonTracePointPropertyIdentifierToSpecific[0][0] = 7;
			HostFilesTraceContainer.commonTracePointPropertyIdentifierToSpecific[1] = TraceContainers.InitializeIntArrayToMinus1(1);
			HostFilesTraceContainer.commonTracePointPropertyIdentifierToSpecific[1][0] = 8;
			HostFilesTraceContainer.commonTracePointPropertyIdentifierToSpecific[2] = TraceContainers.InitializeIntArrayToMinus1(5);
			HostFilesTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][0] = 2;
			HostFilesTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][1] = 3;
			HostFilesTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][2] = 4;
			HostFilesTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][3] = 5;
			HostFilesTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][4] = 6;
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x000BC8CC File Offset: 0x000BAACC
		public HostFilesTraceContainer()
			: base(false, false, "HostFiles", "Host Files", "", ContainerIdentifier.HostFiles, HostFilesTraceContainer.tracePoints, HostFilesTraceContainer.commonTracePointIdentifierToSpecific, HostFilesTraceContainer.commonTracePointPropertyIdentifierToSpecific, false)
		{
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000BC904 File Offset: 0x000BAB04
		public HostFilesTraceContainer(string configurationFileName)
			: base(false, false, "HostFiles", "Host Files", "", ContainerIdentifier.HostFiles, HostFilesTraceContainer.tracePoints, configurationFileName)
		{
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000BC083 File Offset: 0x000BA283
		public HostFilesTraceContainer(TraceContainer traceContainer)
			: base(traceContainer)
		{
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000BC92F File Offset: 0x000BAB2F
		public override TraceContainer GetTraceDefinition(TraceContainer traceContainer)
		{
			return new HostFilesTraceContainer(traceContainer);
		}

		// Token: 0x04002023 RID: 8227
		private static List<ITracePointInformation> tracePoints;

		// Token: 0x04002024 RID: 8228
		private static int[] commonTracePointIdentifierToSpecific;

		// Token: 0x04002025 RID: 8229
		private static int[][] commonTracePointPropertyIdentifierToSpecific;
	}
}
