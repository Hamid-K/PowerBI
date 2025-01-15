using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing.MqClient
{
	// Token: 0x02000683 RID: 1667
	public class MqTraceContainer : TraceContainer
	{
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x060037B4 RID: 14260 RVA: 0x00006F04 File Offset: 0x00005104
		public static bool ConstructorNeedsInstanceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000BC0C4 File Offset: 0x000BA2C4
		static MqTraceContainer()
		{
			TracePointPropertyInformation tracePointPropertyInformation = new TracePointPropertyInformation("Api Object Type", 0, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("Queue Manager", 0),
				new TracePointPropertyEnumerationValue("Queue", 1)
			});
			TracePointPropertyInformation tracePointPropertyInformation2 = new TracePointPropertyInformation("Api Object Name", 1, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation3 = new TracePointPropertyInformation("Queue Manager Name", 2, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation4 = new TracePointPropertyInformation("Channel Name", 3, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation5 = new TracePointPropertyInformation("Queue Name", 4, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation6 = new TracePointPropertyInformation("Server", 5, PropertyType.String, null);
			TracePointPropertyInformation tracePointPropertyInformation7 = new TracePointPropertyInformation("Port", 6, PropertyType.Integer, null);
			List<ITracePointPropertyInformation> list = new List<ITracePointPropertyInformation>();
			list.Add(tracePointPropertyInformation);
			list.Add(tracePointPropertyInformation2);
			List<ITracePointPropertyInformation> list2 = new List<ITracePointPropertyInformation>();
			list2.Add(tracePointPropertyInformation3);
			list2.Add(tracePointPropertyInformation4);
			List<ITracePointPropertyInformation> list3 = new List<ITracePointPropertyInformation>();
			list3.Add(tracePointPropertyInformation5);
			List<ITracePointPropertyInformation> list4 = new List<ITracePointPropertyInformation>();
			list4.Add(tracePointPropertyInformation6);
			list4.Add(tracePointPropertyInformation7);
			TracePointInformation tracePointInformation = new TracePointInformation(false, "Queue Manager Automaton", 5, null, null);
			List<ITracePointInformation> list5 = new List<ITracePointInformation>();
			list5.Add(tracePointInformation);
			TracePointInformation tracePointInformation2 = new TracePointInformation(false, "Queue Automaton", 7, null, null);
			List<ITracePointInformation> list6 = new List<ITracePointInformation>();
			list6.Add(tracePointInformation2);
			TracePointInformation tracePointInformation3 = new TracePointInformation(false, "Tcp Automaton", 9, null, null);
			List<ITracePointInformation> list7 = new List<ITracePointInformation>();
			list7.Add(tracePointInformation3);
			TracePointInformation tracePointInformation4 = new TracePointInformation(true, "Api", 3, list, null);
			TracePointInformation tracePointInformation5 = new TracePointInformation(true, "Queue Manager", 4, list2, list5);
			TracePointInformation tracePointInformation6 = new TracePointInformation(true, "Queue", 6, list3, list6);
			TracePointInformation tracePointInformation7 = new TracePointInformation(true, "Connection", 8, list4, list7);
			List<ITracePointInformation> list8 = new List<ITracePointInformation>();
			list8.Add(tracePointInformation4);
			list8.Add(tracePointInformation5);
			list8.Add(tracePointInformation6);
			list8.Add(tracePointInformation7);
			TracePointInformation tracePointInformation8 = new TracePointInformation(false, "Pooling", 2, null, null);
			TracePointInformation tracePointInformation9 = new TracePointInformation(false, "Application", 1, null, list8);
			TracePointInformation tracePointInformation10 = new TracePointInformation(false, "Administration", 0, null, null);
			MqTraceContainer.tracePoints = new List<ITracePointInformation>();
			MqTraceContainer.tracePoints.Add(tracePointInformation10);
			MqTraceContainer.tracePoints.Add(tracePointInformation8);
			MqTraceContainer.tracePoints.Add(tracePointInformation9);
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000BC2EC File Offset: 0x000BA4EC
		public MqTraceContainer()
			: base(false, false, "MQClient", "Microsoft Client for MQ", "", ContainerIdentifier.MqClient, MqTraceContainer.tracePoints, true)
		{
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000BC318 File Offset: 0x000BA518
		public MqTraceContainer(string configurationFileName)
			: base(false, false, "MQClient", "Microsoft Client for MQ", "", ContainerIdentifier.MqClient, MqTraceContainer.tracePoints, configurationFileName)
		{
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000BC083 File Offset: 0x000BA283
		public MqTraceContainer(TraceContainer traceContainer)
			: base(traceContainer)
		{
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000BC343 File Offset: 0x000BA543
		public override TraceContainer GetTraceDefinition(TraceContainer traceContainer)
		{
			return new MqTraceContainer(traceContainer);
		}

		// Token: 0x04002003 RID: 8195
		private static List<ITracePointInformation> tracePoints;
	}
}
