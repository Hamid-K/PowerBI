using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.ReportingServices.ProcessingRenderingCommon;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000086 RID: 134
	public abstract class RequestContextBase
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000BF15 File Offset: 0x0000A115
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000BF1D File Offset: 0x0000A11D
		public TimeSpan TotalDatabaseTime
		{
			get
			{
				return this.m_totalDatabaseTime;
			}
			set
			{
				this.m_totalDatabaseTime = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000BF26 File Offset: 0x0000A126
		public Timer RequestTimer
		{
			get
			{
				return this.m_requestTimer;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000BF2E File Offset: 0x0000A12E
		public IServiceInstanceContext ServiceInstanceContext
		{
			get
			{
				return this.m_serviceInstanceContext;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000BF36 File Offset: 0x0000A136
		public virtual string ApplicationPath
		{
			get
			{
				return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000BF47 File Offset: 0x0000A147
		public virtual string ClientIPAddress
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000BF4A File Offset: 0x0000A14A
		public virtual string MapPath(string path)
		{
			return Path.Combine(this.ApplicationPath, path);
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000BF58 File Offset: 0x0000A158
		public virtual string DataSourceMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000BF5B File Offset: 0x0000A15B
		public virtual string UserId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000BF5E File Offset: 0x0000A15E
		public virtual string PowerBIGroupObjectId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000BF61 File Offset: 0x0000A161
		public virtual ReadOnlyCollection<PvDataSource> PvDataSources
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000BF64 File Offset: 0x0000A164
		public virtual string DatabaseFullName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000BF67 File Offset: 0x0000A167
		public virtual PvDataSource InternalDataSource
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000BF6A File Offset: 0x0000A16A
		public virtual bool HasInternalDataSource
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000BF6D File Offset: 0x0000A16D
		public virtual bool IsDataSourceMapDecoded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000BF70 File Offset: 0x0000A170
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000BF78 File Offset: 0x0000A178
		public IResourceTicket ResourceTicket { get; protected set; }

		// Token: 0x060003D6 RID: 982 RVA: 0x0000BF81 File Offset: 0x0000A181
		public virtual void TraceCustomerContentByHost(string customerContent)
		{
		}

		// Token: 0x040001FC RID: 508
		public static readonly string RequestContextSlotName = "ReportServerRequestContext";

		// Token: 0x040001FD RID: 509
		private TimeSpan m_totalDatabaseTime;

		// Token: 0x040001FE RID: 510
		private readonly Timer m_requestTimer = new Timer();

		// Token: 0x040001FF RID: 511
		protected IServiceInstanceContext m_serviceInstanceContext;
	}
}
