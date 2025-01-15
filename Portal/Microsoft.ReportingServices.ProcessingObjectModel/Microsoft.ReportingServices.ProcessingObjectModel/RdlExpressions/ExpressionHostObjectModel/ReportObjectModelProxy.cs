using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200004B RID: 75
	public abstract class ReportObjectModelProxy : MarshalByRefObject, IReportObjectModelProxyForCustomCode
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00002BCF File Offset: 0x00000DCF
		internal void SetReportObjectModel(OnDemandObjectModel reportObjectModel)
		{
			this.m_reportObjectModel = reportObjectModel;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00002BD8 File Offset: 0x00000DD8
		protected Fields Fields
		{
			get
			{
				return this.m_reportObjectModel.Fields;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00002BE5 File Offset: 0x00000DE5
		protected Parameters Parameters
		{
			get
			{
				return this.m_reportObjectModel.Parameters;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00002BF2 File Offset: 0x00000DF2
		protected Globals Globals
		{
			get
			{
				return this.m_reportObjectModel.Globals;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00002BFF File Offset: 0x00000DFF
		protected User User
		{
			get
			{
				return this.m_reportObjectModel.User;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00002C0C File Offset: 0x00000E0C
		protected ReportItems ReportItems
		{
			get
			{
				return this.m_reportObjectModel.ReportItems;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00002C19 File Offset: 0x00000E19
		protected Aggregates Aggregates
		{
			get
			{
				return this.m_reportObjectModel.Aggregates;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00002C26 File Offset: 0x00000E26
		protected Lookups Lookups
		{
			get
			{
				return this.m_reportObjectModel.Lookups;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00002C33 File Offset: 0x00000E33
		protected DataSets DataSets
		{
			get
			{
				return this.m_reportObjectModel.DataSets;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00002C40 File Offset: 0x00000E40
		protected DataSources DataSources
		{
			get
			{
				return this.m_reportObjectModel.DataSources;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00002C4D File Offset: 0x00000E4D
		protected Variables Variables
		{
			get
			{
				return this.m_reportObjectModel.Variables;
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00002C5A File Offset: 0x00000E5A
		protected bool InScope(string scope)
		{
			return this.m_reportObjectModel.InScope(scope);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00002C68 File Offset: 0x00000E68
		protected int Level()
		{
			return this.m_reportObjectModel.RecursiveLevel(null);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00002C76 File Offset: 0x00000E76
		protected int Level(string scope)
		{
			return this.m_reportObjectModel.RecursiveLevel(scope);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00002C84 File Offset: 0x00000E84
		protected object MinValue(params object[] arguments)
		{
			return this.m_reportObjectModel.MinValue(arguments);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00002C92 File Offset: 0x00000E92
		protected object MaxValue(params object[] arguments)
		{
			return this.m_reportObjectModel.MaxValue(arguments);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00002CA0 File Offset: 0x00000EA0
		protected string CreateDrillthroughContext()
		{
			return this.m_reportObjectModel.CreateDrillthroughContext();
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00002CAD File Offset: 0x00000EAD
		Parameters IReportObjectModelProxyForCustomCode.Parameters
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00002CB5 File Offset: 0x00000EB5
		Globals IReportObjectModelProxyForCustomCode.Globals
		{
			get
			{
				return this.Globals;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00002CBD File Offset: 0x00000EBD
		User IReportObjectModelProxyForCustomCode.User
		{
			get
			{
				return this.User;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00002CC5 File Offset: 0x00000EC5
		Variables IReportObjectModelProxyForCustomCode.Variables
		{
			get
			{
				return this.Variables;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00002CCD File Offset: 0x00000ECD
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x0400007B RID: 123
		private OnDemandObjectModel m_reportObjectModel;
	}
}
