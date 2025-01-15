using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing.TiHip
{
	// Token: 0x020006C1 RID: 1729
	public class TiHipTraceContainer : TraceContainer
	{
		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x0600382E RID: 14382 RVA: 0x00002B16 File Offset: 0x00000D16
		public static bool ConstructorNeedsInstanceName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000BCF58 File Offset: 0x000BB158
		static TiHipTraceContainer()
		{
			TracePointPropertyInformation tracePointPropertyInformation = new TracePointPropertyInformation("Class", 0, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation2 = new TracePointPropertyInformation("Method Name", 1, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation3 = new TracePointPropertyInformation("Persistence", 2, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("NonPersistent", 0),
				new TracePointPropertyEnumerationValue("Open", 1),
				new TracePointPropertyEnumerationValue("Use", 2),
				new TracePointPropertyEnumerationValue("Close", 3)
			});
			TracePointPropertyInformation tracePointPropertyInformation4 = new TracePointPropertyInformation("Flow Control Type", 3, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("DPL", 0),
				new TracePointPropertyEnumerationValue("ELM", 1),
				new TracePointPropertyEnumerationValue("End Point", 2),
				new TracePointPropertyEnumerationValue("HTTP", 3),
				new TracePointPropertyEnumerationValue("TRM", 4),
				new TracePointPropertyEnumerationValue("User Data", 5)
			});
			TracePointPropertyInformation tracePointPropertyInformation5 = new TracePointPropertyInformation("Transport Type", 5, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("SNA", 4),
				new TracePointPropertyEnumerationValue("TCP", 5),
				new TracePointPropertyEnumerationValue("HTTP", 2)
			});
			TracePointPropertyInformation tracePointPropertyInformation6 = new TracePointPropertyInformation("Server Address", 6, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation7 = new TracePointPropertyInformation("Port", 7, PropertyType.Integer, null);
			TracePointPropertyInformation tracePointPropertyInformation8 = new TracePointPropertyInformation("Local LU", 8, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation9 = new TracePointPropertyInformation("Remote LU", 9, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation10 = new TracePointPropertyInformation("Listener Type", 4, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("SNA", 4),
				new TracePointPropertyEnumerationValue("TCP", 5)
			});
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
			List<ITracePointPropertyInformation> list2 = new List<ITracePointPropertyInformation>();
			list2.Add(tracePointPropertyInformation4);
			List<ITracePointPropertyInformation> list3 = new List<ITracePointPropertyInformation>();
			list3.Add(tracePointPropertyInformation5);
			list3.Add(tracePointPropertyInformation6);
			list3.Add(tracePointPropertyInformation7);
			list3.Add(tracePointPropertyInformation8);
			list3.Add(tracePointPropertyInformation9);
			List<ITracePointPropertyInformation> list4 = new List<ITracePointPropertyInformation>();
			list4.Add(tracePointPropertyInformation10);
			List<ITracePointPropertyInformation> list5 = new List<ITracePointPropertyInformation>();
			list5.Add(tracePointPropertyInformation11);
			List<ITracePointPropertyInformation> list6 = new List<ITracePointPropertyInformation>();
			list6.Add(tracePointPropertyInformation12);
			TracePointInformation tracePointInformation = new TracePointInformation(false, "Invoker", 8, null, null);
			TracePointInformation tracePointInformation2 = new TracePointInformation(false, "Flow Control", 4, list2, new List<ITracePointInformation> { tracePointInformation });
			TracePointInformation tracePointInformation3 = new TracePointInformation(true, "Transport", 5, list3, null);
			TracePointInformation tracePointInformation4 = new TracePointInformation(false, "Primitive Converter", 7, list6, null);
			TracePointInformation tracePointInformation5 = new TracePointInformation(false, "Aggregate Converter", 6, list5, new List<ITracePointInformation> { tracePointInformation4 });
			TracePointInformation tracePointInformation6 = new TracePointInformation(false, "Call Processing", 3, list, new List<ITracePointInformation> { tracePointInformation2, tracePointInformation3, tracePointInformation5 });
			TracePointInformation tracePointInformation7 = new TracePointInformation(true, "Listener", 2, list4, null);
			TracePointInformation tracePointInformation8 = new TracePointInformation(false, "Administration", 1, null, null);
			TracePointInformation tracePointInformation9 = new TracePointInformation(false, "Service", 0, null, new List<ITracePointInformation> { tracePointInformation7, tracePointInformation8 });
			TiHipTraceContainer.tracePoints = new List<ITracePointInformation>();
			TiHipTraceContainer.tracePoints.Add(tracePointInformation9);
			TiHipTraceContainer.tracePoints.Add(tracePointInformation6);
			TiHipTraceContainer.commonTracePointIdentifierToSpecific = TraceContainers.InitializeIntArrayToMinus1(3);
			TiHipTraceContainer.commonTracePointIdentifierToSpecific[0] = 6;
			TiHipTraceContainer.commonTracePointIdentifierToSpecific[1] = 7;
			TiHipTraceContainer.commonTracePointIdentifierToSpecific[2] = 5;
			TiHipTraceContainer.commonTracePointPropertyIdentifierToSpecific = new int[3][];
			TiHipTraceContainer.commonTracePointPropertyIdentifierToSpecific[0] = TraceContainers.InitializeIntArrayToMinus1(1);
			TiHipTraceContainer.commonTracePointPropertyIdentifierToSpecific[0][0] = 10;
			TiHipTraceContainer.commonTracePointPropertyIdentifierToSpecific[1] = TraceContainers.InitializeIntArrayToMinus1(1);
			TiHipTraceContainer.commonTracePointPropertyIdentifierToSpecific[1][0] = 11;
			TiHipTraceContainer.commonTracePointPropertyIdentifierToSpecific[2] = TraceContainers.InitializeIntArrayToMinus1(5);
			TiHipTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][0] = 5;
			TiHipTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][1] = 6;
			TiHipTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][2] = 7;
			TiHipTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][3] = 8;
			TiHipTraceContainer.commonTracePointPropertyIdentifierToSpecific[2][4] = 9;
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000BD3E4 File Offset: 0x000BB5E4
		public TiHipTraceContainer(string instanceName)
			: base(true, false, "TI HIP", "Transaction Integrator HIP", instanceName, ContainerIdentifier.TIHip, TiHipTraceContainer.tracePoints, TiHipTraceContainer.commonTracePointIdentifierToSpecific, TiHipTraceContainer.commonTracePointPropertyIdentifierToSpecific, false)
		{
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000BD418 File Offset: 0x000BB618
		public TiHipTraceContainer(string configurationFileName, string instanceName)
			: base(true, false, "TI HIP", "Transaction Integrator HIP", instanceName, ContainerIdentifier.TIHip, TiHipTraceContainer.tracePoints, configurationFileName)
		{
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000BC083 File Offset: 0x000BA283
		public TiHipTraceContainer(TraceContainer traceContainer)
			: base(traceContainer)
		{
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000BD43F File Offset: 0x000BB63F
		public override TraceContainer GetTraceDefinition(TraceContainer traceContainer)
		{
			return new TiHipTraceContainer(traceContainer);
		}

		// Token: 0x04002072 RID: 8306
		private static List<ITracePointInformation> tracePoints;

		// Token: 0x04002073 RID: 8307
		private static int[] commonTracePointIdentifierToSpecific;

		// Token: 0x04002074 RID: 8308
		private static int[][] commonTracePointPropertyIdentifierToSpecific;
	}
}
