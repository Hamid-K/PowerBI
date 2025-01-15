using System;
using System.Globalization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000656 RID: 1622
	public sealed class DataSetContext
	{
		// Token: 0x06005A45 RID: 23109 RVA: 0x00172654 File Offset: 0x00170854
		public DataSetContext(string targetChunkNameInSnapshot, string cachedDataChunkName, bool mustCreateDataChunk, IRowConsumer consumerRequest, ICatalogItemContext itemContext, RuntimeDataSourceInfoCollection dataSources, string requestUserName, DateTime executionTimeStamp, ParameterInfoCollection parameters, IChunkFactory createChunkFactory, ReportProcessing.ExecutionType interactiveExecution, CultureInfo culture, UserProfileState allowUserProfileState, UserProfileState initialUserProfileState, IProcessingDataExtensionConnection createDataExtensionInstanceFunction, CreateAndRegisterStream createStreamCallbackForScalability, ReportRuntimeSetup dataSetRuntimeSetup, IJobContext jobContext, IDataProtection dataProtection)
		{
			this.m_targetChunkNameInSnapshot = targetChunkNameInSnapshot;
			this.m_cachedDataChunkName = cachedDataChunkName;
			this.m_mustCreateDataChunk = mustCreateDataChunk;
			this.m_consumerRequest = consumerRequest;
			this.m_itemContext = itemContext;
			this.m_dataSources = dataSources;
			this.m_requestUserName = requestUserName;
			this.m_executionTimeStamp = executionTimeStamp;
			this.m_parameters = parameters;
			this.m_createChunkFactory = createChunkFactory;
			this.m_interactiveExecution = interactiveExecution;
			this.m_culture = culture;
			this.m_allowUserProfileState = allowUserProfileState;
			this.m_initialUserProfileState = initialUserProfileState;
			this.m_createDataExtensionInstanceFunction = createDataExtensionInstanceFunction;
			this.m_createStreamCallbackForScalability = createStreamCallbackForScalability;
			this.m_dataSetRuntimeSetup = dataSetRuntimeSetup;
			this.m_jobContext = jobContext;
			this.m_dataProtection = dataProtection;
		}

		// Token: 0x17002025 RID: 8229
		// (get) Token: 0x06005A46 RID: 23110 RVA: 0x001726FC File Offset: 0x001708FC
		// (set) Token: 0x06005A47 RID: 23111 RVA: 0x00172704 File Offset: 0x00170904
		public string TargetChunkNameInSnapshot
		{
			get
			{
				return this.m_targetChunkNameInSnapshot;
			}
			set
			{
				this.m_targetChunkNameInSnapshot = value;
			}
		}

		// Token: 0x17002026 RID: 8230
		// (get) Token: 0x06005A48 RID: 23112 RVA: 0x0017270D File Offset: 0x0017090D
		// (set) Token: 0x06005A49 RID: 23113 RVA: 0x00172715 File Offset: 0x00170915
		public string CachedDataChunkName
		{
			get
			{
				return this.m_cachedDataChunkName;
			}
			set
			{
				this.m_cachedDataChunkName = value;
			}
		}

		// Token: 0x17002027 RID: 8231
		// (get) Token: 0x06005A4A RID: 23114 RVA: 0x0017271E File Offset: 0x0017091E
		// (set) Token: 0x06005A4B RID: 23115 RVA: 0x00172726 File Offset: 0x00170926
		public bool MustCreateDataChunk
		{
			get
			{
				return this.m_mustCreateDataChunk;
			}
			set
			{
				this.m_mustCreateDataChunk = value;
			}
		}

		// Token: 0x17002028 RID: 8232
		// (get) Token: 0x06005A4C RID: 23116 RVA: 0x0017272F File Offset: 0x0017092F
		public IRowConsumer ConsumerRequest
		{
			get
			{
				return this.m_consumerRequest;
			}
		}

		// Token: 0x17002029 RID: 8233
		// (get) Token: 0x06005A4D RID: 23117 RVA: 0x00172737 File Offset: 0x00170937
		public ICatalogItemContext ItemContext
		{
			get
			{
				return this.m_itemContext;
			}
		}

		// Token: 0x1700202A RID: 8234
		// (get) Token: 0x06005A4E RID: 23118 RVA: 0x0017273F File Offset: 0x0017093F
		public RuntimeDataSourceInfoCollection DataSources
		{
			get
			{
				return this.m_dataSources;
			}
		}

		// Token: 0x1700202B RID: 8235
		// (get) Token: 0x06005A4F RID: 23119 RVA: 0x00172747 File Offset: 0x00170947
		public string RequestUserName
		{
			get
			{
				return this.m_requestUserName;
			}
		}

		// Token: 0x1700202C RID: 8236
		// (get) Token: 0x06005A50 RID: 23120 RVA: 0x0017274F File Offset: 0x0017094F
		public DateTime ExecutionTimeStamp
		{
			get
			{
				return this.m_executionTimeStamp;
			}
		}

		// Token: 0x1700202D RID: 8237
		// (get) Token: 0x06005A51 RID: 23121 RVA: 0x00172757 File Offset: 0x00170957
		public ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x1700202E RID: 8238
		// (get) Token: 0x06005A52 RID: 23122 RVA: 0x0017275F File Offset: 0x0017095F
		// (set) Token: 0x06005A53 RID: 23123 RVA: 0x00172767 File Offset: 0x00170967
		public IChunkFactory CreateChunkFactory
		{
			get
			{
				return this.m_createChunkFactory;
			}
			set
			{
				this.m_createChunkFactory = value;
			}
		}

		// Token: 0x1700202F RID: 8239
		// (get) Token: 0x06005A54 RID: 23124 RVA: 0x00172770 File Offset: 0x00170970
		public ReportProcessing.ExecutionType InteractiveExecution
		{
			get
			{
				return this.m_interactiveExecution;
			}
		}

		// Token: 0x17002030 RID: 8240
		// (get) Token: 0x06005A55 RID: 23125 RVA: 0x00172778 File Offset: 0x00170978
		public CultureInfo Culture
		{
			get
			{
				return this.m_culture;
			}
		}

		// Token: 0x17002031 RID: 8241
		// (get) Token: 0x06005A56 RID: 23126 RVA: 0x00172780 File Offset: 0x00170980
		public UserProfileState AllowUserProfileState
		{
			get
			{
				return this.m_allowUserProfileState;
			}
		}

		// Token: 0x17002032 RID: 8242
		// (get) Token: 0x06005A57 RID: 23127 RVA: 0x00172788 File Offset: 0x00170988
		public UserProfileState InitialUserProfileState
		{
			get
			{
				return this.m_initialUserProfileState;
			}
		}

		// Token: 0x17002033 RID: 8243
		// (get) Token: 0x06005A58 RID: 23128 RVA: 0x00172790 File Offset: 0x00170990
		public IProcessingDataExtensionConnection CreateAndSetupDataExtensionFunction
		{
			get
			{
				return this.m_createDataExtensionInstanceFunction;
			}
		}

		// Token: 0x17002034 RID: 8244
		// (get) Token: 0x06005A59 RID: 23129 RVA: 0x00172798 File Offset: 0x00170998
		public CreateAndRegisterStream CreateStreamCallbackForScalability
		{
			get
			{
				return this.m_createStreamCallbackForScalability;
			}
		}

		// Token: 0x17002035 RID: 8245
		// (get) Token: 0x06005A5A RID: 23130 RVA: 0x001727A0 File Offset: 0x001709A0
		public ReportRuntimeSetup DataSetRuntimeSetup
		{
			get
			{
				return this.m_dataSetRuntimeSetup;
			}
		}

		// Token: 0x17002036 RID: 8246
		// (get) Token: 0x06005A5B RID: 23131 RVA: 0x001727A8 File Offset: 0x001709A8
		public IJobContext JobContext
		{
			get
			{
				return this.m_jobContext;
			}
		}

		// Token: 0x17002037 RID: 8247
		// (get) Token: 0x06005A5C RID: 23132 RVA: 0x001727B0 File Offset: 0x001709B0
		public IDataProtection DataProtection
		{
			get
			{
				return this.m_dataProtection;
			}
		}

		// Token: 0x04002E79 RID: 11897
		private string m_targetChunkNameInSnapshot;

		// Token: 0x04002E7A RID: 11898
		private string m_cachedDataChunkName;

		// Token: 0x04002E7B RID: 11899
		private bool m_mustCreateDataChunk;

		// Token: 0x04002E7C RID: 11900
		private IRowConsumer m_consumerRequest;

		// Token: 0x04002E7D RID: 11901
		private ICatalogItemContext m_itemContext;

		// Token: 0x04002E7E RID: 11902
		private RuntimeDataSourceInfoCollection m_dataSources;

		// Token: 0x04002E7F RID: 11903
		private string m_requestUserName;

		// Token: 0x04002E80 RID: 11904
		private DateTime m_executionTimeStamp;

		// Token: 0x04002E81 RID: 11905
		private ParameterInfoCollection m_parameters;

		// Token: 0x04002E82 RID: 11906
		private IChunkFactory m_createChunkFactory;

		// Token: 0x04002E83 RID: 11907
		private ReportProcessing.ExecutionType m_interactiveExecution;

		// Token: 0x04002E84 RID: 11908
		private CultureInfo m_culture;

		// Token: 0x04002E85 RID: 11909
		private UserProfileState m_allowUserProfileState;

		// Token: 0x04002E86 RID: 11910
		private UserProfileState m_initialUserProfileState;

		// Token: 0x04002E87 RID: 11911
		private IProcessingDataExtensionConnection m_createDataExtensionInstanceFunction;

		// Token: 0x04002E88 RID: 11912
		private CreateAndRegisterStream m_createStreamCallbackForScalability;

		// Token: 0x04002E89 RID: 11913
		private ReportRuntimeSetup m_dataSetRuntimeSetup;

		// Token: 0x04002E8A RID: 11914
		private IJobContext m_jobContext;

		// Token: 0x04002E8B RID: 11915
		private IDataProtection m_dataProtection;
	}
}
