using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006AA RID: 1706
	public class DrdaAsTraceContainer : TraceContainer
	{
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x00006F04 File Offset: 0x00005104
		public static bool ConstructorNeedsInstanceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06003802 RID: 14338 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool CanBeUsed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000BCB08 File Offset: 0x000BAD08
		static DrdaAsTraceContainer()
		{
			TracePointPropertyInformation tracePointPropertyInformation = new TracePointPropertyInformation("Primitive Converter Type", 1, PropertyType.Enumeration, new List<ITracePointPropertyEnumerationValue>
			{
				new TracePointPropertyEnumerationValue("System Z", 0),
				new TracePointPropertyEnumerationValue("System I", 1)
			});
			TracePointInformation tracePointInformation = new TracePointInformation(false, "Primitive Converter", 15, new List<ITracePointPropertyInformation> { tracePointPropertyInformation }, null);
			TracePointInformation tracePointInformation2 = new TracePointInformation(false, "Service", 0, null, null);
			TracePointInformation tracePointInformation3 = new TracePointInformation(false, "Configuration Manager", 1, null, null);
			TracePointInformation tracePointInformation4 = new TracePointInformation(false, "TCP Communication Manager", 2, null, null);
			TracePointInformation tracePointInformation5 = new TracePointInformation(false, "SNA Communication Manager", 3, null, null);
			TracePointInformation tracePointInformation6 = new TracePointInformation(false, "Session Manager", 4, null, null);
			TracePointInformation tracePointInformation7 = new TracePointInformation(false, "Supervisor", 9, null, null);
			TracePointInformation tracePointInformation8 = new TracePointInformation(false, "XA Manager", 10, null, null);
			TracePointInformation tracePointInformation9 = new TracePointInformation(false, "Security Manager", 11, null, null);
			TracePointInformation tracePointInformation10 = new TracePointInformation(false, "Sync Point Manager", 12, null, null);
			TracePointInformation tracePointInformation11 = new TracePointInformation(false, "Data Type Mapping", 13, null, null);
			TracePointInformation tracePointInformation12 = new TracePointInformation(false, "DDM Reader", 16, null, null);
			TracePointInformation tracePointInformation13 = new TracePointInformation(false, "DDM Writer", 17, null, null);
			TracePointInformation tracePointInformation14 = new TracePointInformation(false, "Database", 8, null, null);
			TracePointInformation tracePointInformation15 = new TracePointInformation(false, "SQL Application Manager", 6, null, null);
			TracePointInformation tracePointInformation16 = new TracePointInformation(false, "Resynchronization Manager", 7, null, null);
			TracePointInformation tracePointInformation17 = new TracePointInformation(false, "Data Converter", 14, null, new List<ITracePointInformation> { tracePointInformation });
			TracePointInformation tracePointInformation18 = new TracePointInformation(false, "Session", 5, null, new List<ITracePointInformation>
			{
				tracePointInformation17, tracePointInformation14, tracePointInformation15, tracePointInformation7, tracePointInformation8, tracePointInformation9, tracePointInformation10, tracePointInformation16, tracePointInformation11, tracePointInformation12,
				tracePointInformation13
			});
			DrdaAsTraceContainer.tracePoints = new List<ITracePointInformation>();
			DrdaAsTraceContainer.tracePoints.Add(tracePointInformation2);
			DrdaAsTraceContainer.tracePoints.Add(tracePointInformation4);
			DrdaAsTraceContainer.tracePoints.Add(tracePointInformation5);
			DrdaAsTraceContainer.tracePoints.Add(tracePointInformation6);
			DrdaAsTraceContainer.tracePoints.Add(tracePointInformation3);
			DrdaAsTraceContainer.tracePoints.Add(tracePointInformation18);
			DrdaAsTraceContainer.commonTracePointIdentifierToSpecific = TraceContainers.InitializeIntArrayToMinus1(3);
			DrdaAsTraceContainer.commonTracePointIdentifierToSpecific[1] = 15;
			DrdaAsTraceContainer.commonTracePointPropertyIdentifierToSpecific = new int[3][];
			DrdaAsTraceContainer.commonTracePointPropertyIdentifierToSpecific[1] = TraceContainers.InitializeIntArrayToMinus1(1);
			DrdaAsTraceContainer.commonTracePointPropertyIdentifierToSpecific[1][0] = 1;
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000BCDC0 File Offset: 0x000BAFC0
		public DrdaAsTraceContainer()
			: base(false, false, "DRDA AS", "DRDA Appllication Server", "", ContainerIdentifier.DrdaAs, DrdaAsTraceContainer.tracePoints, DrdaAsTraceContainer.drdaAsColumnNames.Length, DrdaAsTraceContainer.drdaAsFilterableColumns, DrdaAsTraceContainer.drdaAsColumnNames, DrdaAsTraceContainer.drdaAsLongestColumnValues, DrdaAsTraceContainer.drdaAsColumnsHaveMeanings, true)
		{
			this.sessionId = 0;
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000BCE10 File Offset: 0x000BB010
		public DrdaAsTraceContainer(int sessionId)
			: base(false, false, "DRDA AS", "DRDA Appllication Server", "", ContainerIdentifier.DrdaAs, DrdaAsTraceContainer.tracePoints, DrdaAsTraceContainer.drdaAsColumnNames.Length, DrdaAsTraceContainer.drdaAsFilterableColumns, DrdaAsTraceContainer.drdaAsColumnNames, DrdaAsTraceContainer.drdaAsLongestColumnValues, DrdaAsTraceContainer.drdaAsColumnsHaveMeanings, true)
		{
			this.sessionId = sessionId;
			base.ExtraValues = new object[] { sessionId };
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x000BCE74 File Offset: 0x000BB074
		public DrdaAsTraceContainer(string configurationFileName)
			: base(false, false, "DRDA AS", "DRDA Appllication Server", "", ContainerIdentifier.DrdaAs, DrdaAsTraceContainer.tracePoints, configurationFileName)
		{
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000BC083 File Offset: 0x000BA283
		public DrdaAsTraceContainer(TraceContainer traceContainer)
			: base(traceContainer)
		{
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000BCE9F File Offset: 0x000BB09F
		public override TraceContainer GetTraceDefinition(TraceContainer traceContainer)
		{
			return new DrdaAsTraceContainer(traceContainer);
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06003809 RID: 14345 RVA: 0x000BCEA7 File Offset: 0x000BB0A7
		public int SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x04002047 RID: 8263
		private static List<ITracePointInformation> tracePoints;

		// Token: 0x04002048 RID: 8264
		private static string[] drdaAsColumnNames = new string[] { "Session Id" };

		// Token: 0x04002049 RID: 8265
		private static string[] drdaAsLongestColumnValues = new string[] { "Session Id" };

		// Token: 0x0400204A RID: 8266
		private static bool drdaAsColumnsHaveMeanings = false;

		// Token: 0x0400204B RID: 8267
		private static bool[] drdaAsFilterableColumns = new bool[] { true };

		// Token: 0x0400204C RID: 8268
		private static int[] commonTracePointIdentifierToSpecific;

		// Token: 0x0400204D RID: 8269
		private static int[][] commonTracePointPropertyIdentifierToSpecific;

		// Token: 0x0400204E RID: 8270
		private int sessionId;
	}
}
