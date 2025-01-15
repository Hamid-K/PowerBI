using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing.TiWip
{
	// Token: 0x020006EA RID: 1770
	public class TiWipTraceContainer : TraceContainer
	{
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06003880 RID: 14464 RVA: 0x00006F04 File Offset: 0x00005104
		public static bool ConstructorNeedsInstanceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000BD8F4 File Offset: 0x000BBAF4
		static TiWipTraceContainer()
		{
			TracePointPropertyInformation tracePointPropertyInformation = new TracePointPropertyInformation("RE Name", 0, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation2 = new TracePointPropertyInformation("Class", 1, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation3 = new TracePointPropertyInformation("Method Name", 2, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation4 = new TracePointPropertyInformation("Persistence", 3, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("NonPersistent", 0),
				new TracePointPropertyEnumerationValue("Open", 1),
				new TracePointPropertyEnumerationValue("Use", 2),
				new TracePointPropertyEnumerationValue("Close", 3)
			});
			TracePointPropertyInformation tracePointPropertyInformation5 = new TracePointPropertyInformation("State Machine Type", 4, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("DPC", 0),
				new TracePointPropertyEnumerationValue("Link", 1),
				new TracePointPropertyEnumerationValue("User Data", 2),
				new TracePointPropertyEnumerationValue("IMS Connect", 3)
			});
			TracePointPropertyInformation tracePointPropertyInformation6 = new TracePointPropertyInformation("Transport Type", 5, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("DPC", 0),
				new TracePointPropertyEnumerationValue("ELM", 1),
				new TracePointPropertyEnumerationValue("HTTP", 2),
				new TracePointPropertyEnumerationValue("IMS Connect", 3),
				new TracePointPropertyEnumerationValue("SNA", 4),
				new TracePointPropertyEnumerationValue("TCP", 5),
				new TracePointPropertyEnumerationValue("TRM", 6)
			});
			TracePointPropertyInformation tracePointPropertyInformation7 = new TracePointPropertyInformation("Server Address", 6, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation8 = new TracePointPropertyInformation("Port", 7, PropertyType.Integer, null);
			TracePointPropertyInformation tracePointPropertyInformation9 = new TracePointPropertyInformation("Local LU", 8, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation10 = new TracePointPropertyInformation("Remote LU", 9, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation11 = new TracePointPropertyInformation("Aggregate Converter Type", 10, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("Base", 0),
				new TracePointPropertyEnumerationValue("DPC", 1)
			});
			TracePointPropertyInformation tracePointPropertyInformation12 = new TracePointPropertyInformation("Primitive Converter Type", 11, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("System Z", 0),
				new TracePointPropertyEnumerationValue("System I", 1)
			});
			List<ITracePointPropertyInformation> list = new List<ITracePointPropertyInformation>();
			list.Add(tracePointPropertyInformation);
			list.Add(tracePointPropertyInformation2);
			list.Add(tracePointPropertyInformation3);
			list.Add(tracePointPropertyInformation4);
			List<ITracePointPropertyInformation> list2 = new List<ITracePointPropertyInformation>();
			list2.Add(tracePointPropertyInformation5);
			List<ITracePointPropertyInformation> list3 = new List<ITracePointPropertyInformation>();
			list3.Add(tracePointPropertyInformation6);
			list3.Add(tracePointPropertyInformation7);
			list3.Add(tracePointPropertyInformation8);
			list3.Add(tracePointPropertyInformation9);
			list3.Add(tracePointPropertyInformation10);
			List<ITracePointPropertyInformation> list4 = new List<ITracePointPropertyInformation>();
			list4.Add(tracePointPropertyInformation11);
			List<ITracePointPropertyInformation> list5 = new List<ITracePointPropertyInformation>();
			list5.Add(tracePointPropertyInformation12);
			TracePointInformation tracePointInformation = new TracePointInformation(false, "State Machine", 1, list2, null);
			TracePointInformation tracePointInformation2 = new TracePointInformation(true, "Transport", 2, list3, null);
			TracePointInformation tracePointInformation3 = new TracePointInformation(false, "Primitive Converter", 4, list5, null);
			TracePointInformation tracePointInformation4 = new TracePointInformation(false, "Aggregate Converter", 3, list4, new List<ITracePointInformation> { tracePointInformation3 });
			TracePointInformation tracePointInformation5 = new TracePointInformation(false, "TBGen", 0, list, new List<ITracePointInformation> { tracePointInformation, tracePointInformation2, tracePointInformation4 });
			TiWipTraceContainer.tracePoints = new List<ITracePointInformation>();
			TiWipTraceContainer.tracePoints.Add(tracePointInformation5);
			TiWipTraceContainer.commonTracePointIdentifierToSpecific = TraceContainers.InitializeIntArrayToMinus1(3);
			TiWipTraceContainer.commonTracePointIdentifierToSpecific[0] = 3;
			TiWipTraceContainer.commonTracePointIdentifierToSpecific[1] = 4;
			TiWipTraceContainer.commonTracePointIdentifierToSpecific[2] = 2;
			TiWipTraceContainer.commonTracePointPropertyIdentifierToSpecific = new int[3][];
			TiWipTraceContainer.commonTracePointPropertyIdentifierToSpecific[0] = TraceContainers.InitializeIntArrayToMinus1(1);
			TiWipTraceContainer.commonTracePointPropertyIdentifierToSpecific[0][0] = 10;
			TiWipTraceContainer.commonTracePointPropertyIdentifierToSpecific[1] = TraceContainers.InitializeIntArrayToMinus1(1);
			TiWipTraceContainer.commonTracePointPropertyIdentifierToSpecific[1][0] = 11;
			TiWipTraceContainer.commonTracePointPropertyIdentifierToSpecific[2] = TraceContainers.InitializeIntArrayToMinus1(5);
			TiWipTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][0] = 5;
			TiWipTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][1] = 6;
			TiWipTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][2] = 7;
			TiWipTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][3] = 8;
			TiWipTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][4] = 9;
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000BDCF8 File Offset: 0x000BBEF8
		public TiWipTraceContainer()
			: base(false, false, "TI WIP", "Transaction Integrator WIP", "", ContainerIdentifier.TIWip, TiWipTraceContainer.tracePoints, TiWipTraceContainer.commonTracePointIdentifierToSpecific, TiWipTraceContainer.commonTracePointPropertyIdentifierToSpecific, false)
		{
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000BDD30 File Offset: 0x000BBF30
		public TiWipTraceContainer(string configurationFileName)
			: base(false, false, "TI WIP", "Transaction Integrator WIP", "", ContainerIdentifier.TIWip, TiWipTraceContainer.tracePoints, configurationFileName)
		{
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000BC083 File Offset: 0x000BA283
		public TiWipTraceContainer(TraceContainer traceContainer)
			: base(traceContainer)
		{
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000BDD5B File Offset: 0x000BBF5B
		public override TraceContainer GetTraceDefinition(TraceContainer traceContainer)
		{
			return new TiWipTraceContainer(traceContainer);
		}

		// Token: 0x040020CC RID: 8396
		private static List<ITracePointInformation> tracePoints;

		// Token: 0x040020CD RID: 8397
		private static int[] commonTracePointIdentifierToSpecific;

		// Token: 0x040020CE RID: 8398
		private static int[][] commonTracePointPropertyIdentifierToSpecific;
	}
}
