using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing.SI
{
	// Token: 0x020006DD RID: 1757
	public class SiTraceContainer : TraceContainer
	{
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600386A RID: 14442 RVA: 0x00006F04 File Offset: 0x00005104
		public static bool ConstructorNeedsInstanceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000BD63C File Offset: 0x000BB83C
		static SiTraceContainer()
		{
			TracePointPropertyInformation tracePointPropertyInformation = new TracePointPropertyInformation("LU Name(LU0)", 0, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation2 = new TracePointPropertyInformation("LU Name(LU2)", 1, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation3 = new TracePointPropertyInformation("Destination LU Name", 2, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation4 = new TracePointPropertyInformation("Transport Type", 3, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("TN", 0),
				new TracePointPropertyEnumerationValue("SNA", 1)
			});
			TracePointPropertyInformation tracePointPropertyInformation5 = new TracePointPropertyInformation("TN Server", 4, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation6 = new TracePointPropertyInformation("Port", 5, PropertyType.Integer, null);
			TracePointPropertyInformation tracePointPropertyInformation7 = new TracePointPropertyInformation("Security", 6, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("None", 0),
				new TracePointPropertyEnumerationValue("TLS", 1)
			});
			TracePointPropertyInformation tracePointPropertyInformation8 = new TracePointPropertyInformation("Script File", 7, PropertyType.String, null);
			List<ITracePointPropertyInformation> list = new List<ITracePointPropertyInformation>();
			list.Add(tracePointPropertyInformation);
			list.Add(tracePointPropertyInformation3);
			List<ITracePointPropertyInformation> list2 = new List<ITracePointPropertyInformation>();
			list2.Add(tracePointPropertyInformation2);
			list2.Add(tracePointPropertyInformation4);
			list2.Add(tracePointPropertyInformation5);
			list2.Add(tracePointPropertyInformation6);
			list2.Add(tracePointPropertyInformation7);
			List<ITracePointPropertyInformation> list3 = new List<ITracePointPropertyInformation>();
			list3.Add(tracePointPropertyInformation8);
			TracePointInformation tracePointInformation = new TracePointInformation(false, "Terminal", 2, null, null);
			List<ITracePointInformation> list4 = new List<ITracePointInformation>();
			list4.Add(tracePointInformation);
			TracePointInformation tracePointInformation2 = new TracePointInformation(false, "Tcp Automaton", 6, null, null);
			TracePointInformation tracePointInformation3 = new TracePointInformation(false, "Tn Automaton", 5, null, new List<ITracePointInformation> { tracePointInformation2 });
			TracePointInformation tracePointInformation4 = new TracePointInformation(false, "3270 Automaton", 4, null, new List<ITracePointInformation> { tracePointInformation3 });
			list4.Add(tracePointInformation4);
			TracePointInformation tracePointInformation5 = new TracePointInformation(true, "LU 2", 1, list2, list4);
			TracePointInformation tracePointInformation6 = new TracePointInformation(true, "LU 0", 0, list, null);
			TracePointInformation tracePointInformation7 = new TracePointInformation(true, "Scripting", 3, list3, null);
			SiTraceContainer.tracePoints = new List<ITracePointInformation>();
			SiTraceContainer.tracePoints.Add(tracePointInformation6);
			SiTraceContainer.tracePoints.Add(tracePointInformation5);
			SiTraceContainer.tracePoints.Add(tracePointInformation7);
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000BD84C File Offset: 0x000BBA4C
		public SiTraceContainer()
			: base(false, false, "SI", "Session Integrator", "", ContainerIdentifier.SI, SiTraceContainer.tracePoints, true)
		{
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000BD878 File Offset: 0x000BBA78
		public SiTraceContainer(string configurationFileName)
			: base(false, false, "SI", "Session Integrator", "", ContainerIdentifier.SI, SiTraceContainer.tracePoints, configurationFileName)
		{
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000BC083 File Offset: 0x000BA283
		public SiTraceContainer(TraceContainer traceContainer)
			: base(traceContainer)
		{
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000BD8A3 File Offset: 0x000BBAA3
		public override TraceContainer GetTraceDefinition(TraceContainer traceContainer)
		{
			return new SiTraceContainer(traceContainer);
		}

		// Token: 0x040020AE RID: 8366
		private static List<ITracePointInformation> tracePoints;
	}
}
