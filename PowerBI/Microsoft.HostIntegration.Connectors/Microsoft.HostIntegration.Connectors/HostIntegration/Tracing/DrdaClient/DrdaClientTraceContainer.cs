using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing.DrdaClient
{
	// Token: 0x0200069D RID: 1693
	public class DrdaClientTraceContainer : TraceContainer
	{
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x060037EE RID: 14318 RVA: 0x00006F04 File Offset: 0x00005104
		public static bool ConstructorNeedsInstanceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000BC954 File Offset: 0x000BAB54
		static DrdaClientTraceContainer()
		{
			TracePointInformation tracePointInformation = new TracePointInformation(false, "TCP Connection Manager", 1, null, null);
			TracePointInformation tracePointInformation2 = new TracePointInformation(false, "XA Manager", 2, null, null);
			TracePointInformation tracePointInformation3 = new TracePointInformation(false, "Supervisor", 3, null, null);
			TracePointInformation tracePointInformation4 = new TracePointInformation(false, "SQL Application Manager", 6, null, null);
			TracePointInformation tracePointInformation5 = new TracePointInformation(false, "Kerberos Manager", 5, null, null);
			TracePointInformation tracePointInformation6 = new TracePointInformation(false, "Security Manager", 4, null, new List<ITracePointInformation> { tracePointInformation5 });
			TracePointInformation tracePointInformation7 = new TracePointInformation(false, "Application Requester", 0, null, new List<ITracePointInformation> { tracePointInformation, tracePointInformation2, tracePointInformation3, tracePointInformation4, tracePointInformation6 });
			TracePointInformation tracePointInformation8 = new TracePointInformation(false, "Drda Common", 8, null, null);
			TracePointInformation tracePointInformation9 = new TracePointInformation(false, "Drda Client", 7, null, null);
			DrdaClientTraceContainer.tracePoints = new List<ITracePointInformation>();
			DrdaClientTraceContainer.tracePoints.Add(tracePointInformation7);
			DrdaClientTraceContainer.tracePoints.Add(tracePointInformation8);
			DrdaClientTraceContainer.tracePoints.Add(tracePointInformation9);
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000BCA60 File Offset: 0x000BAC60
		public DrdaClientTraceContainer()
			: base(false, false, "DRDA Client", "DRDA Client", "", ContainerIdentifier.DrdaClient, DrdaClientTraceContainer.tracePoints, true)
		{
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x000BCA8C File Offset: 0x000BAC8C
		public DrdaClientTraceContainer(string configurationFileName)
			: base(false, false, "DRDA Client", "DRDA Client", "", ContainerIdentifier.DrdaClient, DrdaClientTraceContainer.tracePoints, configurationFileName)
		{
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x000BC083 File Offset: 0x000BA283
		public DrdaClientTraceContainer(TraceContainer traceContainer)
			: base(traceContainer)
		{
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000BCAB7 File Offset: 0x000BACB7
		public override TraceContainer GetTraceDefinition(TraceContainer traceContainer)
		{
			return new DrdaClientTraceContainer(traceContainer);
		}

		// Token: 0x04002030 RID: 8240
		private static List<ITracePointInformation> tracePoints;
	}
}
