using System;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005AA RID: 1450
	[Serializable]
	public sealed class DataSetInfo
	{
		// Token: 0x0600521F RID: 21023 RVA: 0x0015A750 File Offset: 0x00158950
		public DataSetInfo(Guid id, Guid linkId, string name, string absolutePath, byte[] secDesc, Guid compiledDefinitionId, string parameters)
		{
			this.m_id = id;
			this.m_linkSharedDataSetId = linkId;
			this.m_dataSetName = name;
			this.m_absolutePath = absolutePath;
			this.m_secDesc = secDesc;
			this.m_compiledDefinitionId = compiledDefinitionId;
			this.m_referenceValid = this.m_linkSharedDataSetId != Guid.Empty;
			this.m_parameters = parameters;
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x0015A7DC File Offset: 0x001589DC
		public DataSetInfo(string reportDataSetName, string absolutePath, Guid linkId)
		{
			this.m_id = Guid.NewGuid();
			this.m_dataSetName = reportDataSetName;
			this.m_absolutePath = absolutePath;
			this.m_linkSharedDataSetId = linkId;
			this.m_referenceValid = this.m_linkSharedDataSetId != Guid.Empty;
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x0015A854 File Offset: 0x00158A54
		public DataSetInfo(string reportDataSetName, string absolutePath)
		{
			this.m_id = Guid.NewGuid();
			this.m_dataSetName = reportDataSetName;
			this.m_absolutePath = absolutePath;
			this.m_linkSharedDataSetId = Guid.Empty;
			this.m_referenceValid = false;
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0015A8C0 File Offset: 0x00158AC0
		public DataSetInfo(string reportDataSetName, string absolutePath, byte[] definition)
		{
			if (definition == null)
			{
				throw new ArgumentNullException("definition");
			}
			this.m_definition = definition;
			this.m_id = Guid.NewGuid();
			this.m_dataSetName = reportDataSetName;
			this.m_absolutePath = absolutePath;
			this.m_linkSharedDataSetId = Guid.Empty;
			this.m_referenceValid = true;
		}

		// Token: 0x17001E95 RID: 7829
		// (get) Token: 0x06005223 RID: 21027 RVA: 0x0015A93F File Offset: 0x00158B3F
		// (set) Token: 0x06005224 RID: 21028 RVA: 0x0015A947 File Offset: 0x00158B47
		public Guid ID
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x17001E96 RID: 7830
		// (get) Token: 0x06005225 RID: 21029 RVA: 0x0015A950 File Offset: 0x00158B50
		// (set) Token: 0x06005226 RID: 21030 RVA: 0x0015A958 File Offset: 0x00158B58
		public Guid LinkedSharedDataSetID
		{
			get
			{
				return this.m_linkSharedDataSetId;
			}
			set
			{
				this.m_linkSharedDataSetId = value;
			}
		}

		// Token: 0x17001E97 RID: 7831
		// (get) Token: 0x06005227 RID: 21031 RVA: 0x0015A961 File Offset: 0x00158B61
		// (set) Token: 0x06005228 RID: 21032 RVA: 0x0015A969 File Offset: 0x00158B69
		public string AbsolutePath
		{
			get
			{
				return this.m_absolutePath;
			}
			set
			{
				this.m_absolutePath = value;
			}
		}

		// Token: 0x17001E98 RID: 7832
		// (get) Token: 0x06005229 RID: 21033 RVA: 0x0015A972 File Offset: 0x00158B72
		// (set) Token: 0x0600522A RID: 21034 RVA: 0x0015A97A File Offset: 0x00158B7A
		public string DataSetName
		{
			get
			{
				return this.m_dataSetName;
			}
			set
			{
				this.m_dataSetName = value;
			}
		}

		// Token: 0x17001E99 RID: 7833
		// (get) Token: 0x0600522B RID: 21035 RVA: 0x0015A983 File Offset: 0x00158B83
		public byte[] SecurityDescriptor
		{
			get
			{
				return this.m_secDesc;
			}
		}

		// Token: 0x17001E9A RID: 7834
		// (get) Token: 0x0600522C RID: 21036 RVA: 0x0015A98B File Offset: 0x00158B8B
		public Guid CompiledDefinitionId
		{
			get
			{
				return this.m_compiledDefinitionId;
			}
		}

		// Token: 0x17001E9B RID: 7835
		// (get) Token: 0x0600522D RID: 21037 RVA: 0x0015A993 File Offset: 0x00158B93
		// (set) Token: 0x0600522E RID: 21038 RVA: 0x0015A99B File Offset: 0x00158B9B
		public Guid DataSourceId
		{
			get
			{
				return this.m_dataSourceId;
			}
			set
			{
				this.m_dataSourceId = value;
			}
		}

		// Token: 0x17001E9C RID: 7836
		// (get) Token: 0x0600522F RID: 21039 RVA: 0x0015A9A4 File Offset: 0x00158BA4
		public byte[] Definition
		{
			get
			{
				return this.m_definition;
			}
		}

		// Token: 0x17001E9D RID: 7837
		// (get) Token: 0x06005230 RID: 21040 RVA: 0x0015A9AC File Offset: 0x00158BAC
		public string ParametersXml
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x0015A9B4 File Offset: 0x00158BB4
		public bool IsValidReference()
		{
			return this.m_referenceValid;
		}

		// Token: 0x0400297F RID: 10623
		private Guid m_id = Guid.Empty;

		// Token: 0x04002980 RID: 10624
		private Guid m_linkSharedDataSetId = Guid.Empty;

		// Token: 0x04002981 RID: 10625
		private string m_absolutePath;

		// Token: 0x04002982 RID: 10626
		private string m_dataSetName;

		// Token: 0x04002983 RID: 10627
		private byte[] m_secDesc;

		// Token: 0x04002984 RID: 10628
		private Guid m_compiledDefinitionId = Guid.Empty;

		// Token: 0x04002985 RID: 10629
		private Guid m_dataSourceId = Guid.Empty;

		// Token: 0x04002986 RID: 10630
		private string m_parameters;

		// Token: 0x04002987 RID: 10631
		private bool m_referenceValid;

		// Token: 0x04002988 RID: 10632
		private readonly byte[] m_definition;
	}
}
