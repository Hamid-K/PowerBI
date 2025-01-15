using System;
using System.Globalization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200060A RID: 1546
	internal abstract class StreamingOdpProcessingContextBase
	{
		// Token: 0x06005538 RID: 21816 RVA: 0x00167678 File Offset: 0x00165878
		internal StreamingOdpProcessingContextBase(string requestUserName, IGetResource getResourceFunction, CultureInfo userLanguage, IProcessingDataExtensionConnection createDataExtensionInstanceFunction, ReportRuntimeSetup reportRuntimeSetup, CreateAndRegisterStream createStreamCallback, IJobContext jobContext, IDataProtection dataProtection, IConfiguration configuration, ServerDataSourceSettings serverDataSourceSettings, DateTime executionTimeStamp)
		{
			this.m_requestUserName = requestUserName;
			this.m_getResourceFunction = getResourceFunction;
			this.m_userLanguage = userLanguage;
			this.m_createDataExtensionInstanceFunction = createDataExtensionInstanceFunction;
			this.m_reportRuntimeSetup = reportRuntimeSetup;
			this.m_createStreamCallback = createStreamCallback;
			this.m_jobContext = jobContext;
			this.m_dataProtection = dataProtection;
			this.m_configuration = configuration;
			this.m_serverDataSourceSettings = serverDataSourceSettings;
			this.m_executionTimeStamp = executionTimeStamp;
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x001676E0 File Offset: 0x001658E0
		internal virtual IAbortHelper GetAbortHelper()
		{
			if (this.m_jobContext != null)
			{
				return this.m_jobContext.GetAbortHelper();
			}
			return null;
		}

		// Token: 0x17001F32 RID: 7986
		// (get) Token: 0x0600553A RID: 21818 RVA: 0x001676F7 File Offset: 0x001658F7
		public string RequestUserName
		{
			get
			{
				return this.m_requestUserName;
			}
		}

		// Token: 0x17001F33 RID: 7987
		// (get) Token: 0x0600553B RID: 21819 RVA: 0x001676FF File Offset: 0x001658FF
		public IGetResource GetResourceCallback
		{
			get
			{
				return this.m_getResourceFunction;
			}
		}

		// Token: 0x17001F34 RID: 7988
		// (get) Token: 0x0600553C RID: 21820 RVA: 0x00167707 File Offset: 0x00165907
		public CultureInfo UserLanguage
		{
			get
			{
				return this.m_userLanguage;
			}
		}

		// Token: 0x17001F35 RID: 7989
		// (get) Token: 0x0600553D RID: 21821 RVA: 0x0016770F File Offset: 0x0016590F
		public ReportRuntimeSetup ReportRuntimeSetup
		{
			get
			{
				return this.m_reportRuntimeSetup;
			}
		}

		// Token: 0x17001F36 RID: 7990
		// (get) Token: 0x0600553E RID: 21822 RVA: 0x00167717 File Offset: 0x00165917
		public CreateAndRegisterStream CreateStreamCallback
		{
			get
			{
				return this.m_createStreamCallback;
			}
		}

		// Token: 0x17001F37 RID: 7991
		// (get) Token: 0x0600553F RID: 21823 RVA: 0x0016771F File Offset: 0x0016591F
		public IJobContext JobContext
		{
			get
			{
				return this.m_jobContext;
			}
		}

		// Token: 0x17001F38 RID: 7992
		// (get) Token: 0x06005540 RID: 21824 RVA: 0x00167727 File Offset: 0x00165927
		public IDataProtection DataProtection
		{
			get
			{
				return this.m_dataProtection;
			}
		}

		// Token: 0x17001F39 RID: 7993
		// (get) Token: 0x06005541 RID: 21825 RVA: 0x0016772F File Offset: 0x0016592F
		public IProcessingDataExtensionConnection CreateDataExtensionInstanceFunction
		{
			get
			{
				return this.m_createDataExtensionInstanceFunction;
			}
		}

		// Token: 0x17001F3A RID: 7994
		// (get) Token: 0x06005542 RID: 21826 RVA: 0x00167737 File Offset: 0x00165937
		public IConfiguration Configuration
		{
			get
			{
				return this.m_configuration;
			}
		}

		// Token: 0x17001F3B RID: 7995
		// (get) Token: 0x06005543 RID: 21827 RVA: 0x0016773F File Offset: 0x0016593F
		public ServerDataSourceSettings ServerDataSourceSettings
		{
			get
			{
				return this.m_serverDataSourceSettings;
			}
		}

		// Token: 0x17001F3C RID: 7996
		// (get) Token: 0x06005544 RID: 21828 RVA: 0x00167747 File Offset: 0x00165947
		public DateTime ExecutionTimeStamp
		{
			get
			{
				return this.m_executionTimeStamp;
			}
		}

		// Token: 0x04002D1A RID: 11546
		private readonly IProcessingDataExtensionConnection m_createDataExtensionInstanceFunction;

		// Token: 0x04002D1B RID: 11547
		private readonly string m_requestUserName;

		// Token: 0x04002D1C RID: 11548
		private readonly IGetResource m_getResourceFunction;

		// Token: 0x04002D1D RID: 11549
		private readonly CultureInfo m_userLanguage;

		// Token: 0x04002D1E RID: 11550
		private readonly ReportRuntimeSetup m_reportRuntimeSetup;

		// Token: 0x04002D1F RID: 11551
		private readonly CreateAndRegisterStream m_createStreamCallback;

		// Token: 0x04002D20 RID: 11552
		private readonly IJobContext m_jobContext;

		// Token: 0x04002D21 RID: 11553
		private readonly IDataProtection m_dataProtection;

		// Token: 0x04002D22 RID: 11554
		private readonly IConfiguration m_configuration;

		// Token: 0x04002D23 RID: 11555
		private readonly DateTime m_executionTimeStamp;

		// Token: 0x04002D24 RID: 11556
		private readonly ServerDataSourceSettings m_serverDataSourceSettings;
	}
}
