using System;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000021 RID: 33
	public abstract class ReportObjectModelProxy : MarshalByRefObject, IReportObjectModelProxyForCustomCode
	{
		// Token: 0x06000084 RID: 132 RVA: 0x0000227E File Offset: 0x0000047E
		internal void SetReportObjectModel(ObjectModel reportObjectModel)
		{
			this.m_reportObjectModel = reportObjectModel;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002287 File Offset: 0x00000487
		protected Fields Fields
		{
			get
			{
				return this.m_reportObjectModel.Fields;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002294 File Offset: 0x00000494
		protected Parameters Parameters
		{
			get
			{
				return this.m_reportObjectModel.Parameters;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000022A1 File Offset: 0x000004A1
		protected Globals Globals
		{
			get
			{
				return this.m_reportObjectModel.Globals;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000022AE File Offset: 0x000004AE
		protected User User
		{
			get
			{
				return this.m_reportObjectModel.User;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000022BB File Offset: 0x000004BB
		protected ReportItems ReportItems
		{
			get
			{
				return this.m_reportObjectModel.ReportItems;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000022C8 File Offset: 0x000004C8
		protected Aggregates Aggregates
		{
			get
			{
				return this.m_reportObjectModel.Aggregates;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000022D5 File Offset: 0x000004D5
		protected DataSets DataSets
		{
			get
			{
				return this.m_reportObjectModel.DataSets;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000022E2 File Offset: 0x000004E2
		protected DataSources DataSources
		{
			get
			{
				return this.m_reportObjectModel.DataSources;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000022EF File Offset: 0x000004EF
		protected bool InScope(string scope)
		{
			return this.m_reportObjectModel.InScope(scope);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000022FD File Offset: 0x000004FD
		protected int Level()
		{
			return this.m_reportObjectModel.RecursiveLevel(null);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000230B File Offset: 0x0000050B
		protected int Level(string scope)
		{
			return this.m_reportObjectModel.RecursiveLevel(scope);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002319 File Offset: 0x00000519
		protected string CreateDrillthroughContext()
		{
			return this.m_reportObjectModel.CreateDrillthroughContext();
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002326 File Offset: 0x00000526
		Parameters IReportObjectModelProxyForCustomCode.Parameters
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000232E File Offset: 0x0000052E
		Globals IReportObjectModelProxyForCustomCode.Globals
		{
			get
			{
				return this.Globals;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002336 File Offset: 0x00000536
		User IReportObjectModelProxyForCustomCode.User
		{
			get
			{
				return this.User;
			}
		}

		// Token: 0x04000004 RID: 4
		private ObjectModel m_reportObjectModel;
	}
}
