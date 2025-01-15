using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000671 RID: 1649
	public class TracePointInformation : ITracePointInformation
	{
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x0600377E RID: 14206 RVA: 0x000BAE48 File Offset: 0x000B9048
		public int Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x0600377F RID: 14207 RVA: 0x000BAE50 File Offset: 0x000B9050
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06003780 RID: 14208 RVA: 0x000BAE58 File Offset: 0x000B9058
		public bool AllowPropertyUpdates
		{
			get
			{
				return this.allowPropertyUpdates;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06003781 RID: 14209 RVA: 0x000BAE60 File Offset: 0x000B9060
		public List<ITracePointPropertyInformation> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06003782 RID: 14210 RVA: 0x000BAE68 File Offset: 0x000B9068
		public List<ITracePointInformation> TracePoints
		{
			get
			{
				return this.tracePoints;
			}
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000BAE70 File Offset: 0x000B9070
		public TracePointInformation(bool canUpdateProperties, string tracePointName, int tracePointIdentifier, List<ITracePointPropertyInformation> tracePointProperties, List<ITracePointInformation> childTracePoints)
		{
			this.name = tracePointName;
			this.identifier = tracePointIdentifier;
			this.properties = tracePointProperties;
			this.tracePoints = childTracePoints;
			this.allowPropertyUpdates = canUpdateProperties;
		}

		// Token: 0x04001FC0 RID: 8128
		private int identifier;

		// Token: 0x04001FC1 RID: 8129
		private string name;

		// Token: 0x04001FC2 RID: 8130
		private List<ITracePointPropertyInformation> properties;

		// Token: 0x04001FC3 RID: 8131
		private List<ITracePointInformation> tracePoints;

		// Token: 0x04001FC4 RID: 8132
		private bool allowPropertyUpdates;
	}
}
