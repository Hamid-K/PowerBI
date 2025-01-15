using System;
using System.Collections;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.RdlExpressions;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000638 RID: 1592
	[Serializable]
	public sealed class PublishingResult
	{
		// Token: 0x0600570E RID: 22286 RVA: 0x0016EFE0 File Offset: 0x0016D1E0
		public PublishingResult(string reportDescription, string reportLanguage, ParameterInfoCollection parameters, DataSourceInfoCollection dataSources, EmbeddedDataSetInfoCollection embeddedDataSets, DataSetInfoCollection sharedDataSetReferences, ProcessingMessageList warnings, UserLocationFlags userReferenceLocation, double pageHeight, double pageWidth, double topMargin, double bottomMargin, double leftMargin, double rightMargin, ArrayList dataSetsName, bool hasExternalImages, bool hasHyperlinks, ReportProcessingFlags reportProcessingFlags, byte[] dataSetsHash, int? customCodeSize = null)
			: this(reportDescription, reportLanguage, parameters, dataSources, embeddedDataSets, sharedDataSetReferences, warnings, userReferenceLocation, pageHeight, pageWidth, topMargin, bottomMargin, leftMargin, rightMargin, dataSetsName, hasExternalImages, hasHyperlinks, reportProcessingFlags, dataSetsHash, null, null, null, null, false, false, null, customCodeSize)
		{
		}

		// Token: 0x0600570F RID: 22287 RVA: 0x0016F020 File Offset: 0x0016D220
		public PublishingResult(string reportDescription, string reportLanguage, ParameterInfoCollection parameters, DataSourceInfoCollection dataSources, EmbeddedDataSetInfoCollection embeddedDataSets, DataSetInfoCollection sharedDataSetReferences, ProcessingMessageList warnings, UserLocationFlags userReferenceLocation, double pageHeight, double pageWidth, double topMargin, double bottomMargin, double leftMargin, double rightMargin, ArrayList dataSetsName, bool hasExternalImages, bool hasHyperlinks, ReportProcessingFlags reportProcessingFlags, byte[] dataSetsHash, ExpressionUsage expressionUsage, string webAuthoringVersion, string defaultView, AuthoringMetadata authoringMetadata, bool hasSubReports, bool hasDrillthroughs, string rdlReportLanguage, int? customCodeSize = null)
		{
			this.m_reportDescription = reportDescription;
			this.m_reportLanguage = reportLanguage;
			this.m_parameters = parameters;
			this.m_dataSources = dataSources;
			this.m_embeddedDataSets = embeddedDataSets;
			this.m_sharedDataSets = sharedDataSetReferences;
			this.m_warnings = warnings;
			this.m_userReferenceLocation = userReferenceLocation;
			this.m_hasExternalImages = hasExternalImages;
			this.m_hasHyperlinks = hasHyperlinks;
			this.m_reportProcessingFlags = reportProcessingFlags;
			this.m_dataSetsHash = dataSetsHash;
			this.m_pageProperties = new PageProperties(pageHeight, pageWidth, topMargin, bottomMargin, leftMargin, rightMargin);
			if (dataSetsName != null && dataSetsName.Count > 0)
			{
				this.m_dataSetsName = (string[])dataSetsName.ToArray(typeof(string));
			}
			this.m_customCodeSize = customCodeSize;
			this.m_expressionUsage = expressionUsage;
			this.m_webAuthoringVersion = webAuthoringVersion;
			this.m_defaultView = defaultView;
			this.m_authoringMetadata = authoringMetadata;
			this.m_hasSubReports = hasSubReports;
			this.m_hasDrillthroughs = hasDrillthroughs;
			this.m_rdlReportLanguage = rdlReportLanguage;
		}

		// Token: 0x17001FB9 RID: 8121
		// (get) Token: 0x06005710 RID: 22288 RVA: 0x0016F111 File Offset: 0x0016D311
		public string ReportDescription
		{
			get
			{
				return this.m_reportDescription;
			}
		}

		// Token: 0x17001FBA RID: 8122
		// (get) Token: 0x06005711 RID: 22289 RVA: 0x0016F119 File Offset: 0x0016D319
		public string ReportLanguage
		{
			get
			{
				return this.m_reportLanguage;
			}
		}

		// Token: 0x17001FBB RID: 8123
		// (get) Token: 0x06005712 RID: 22290 RVA: 0x0016F121 File Offset: 0x0016D321
		public string ReportDefinitionReportLanguage
		{
			get
			{
				return this.m_rdlReportLanguage;
			}
		}

		// Token: 0x17001FBC RID: 8124
		// (get) Token: 0x06005713 RID: 22291 RVA: 0x0016F129 File Offset: 0x0016D329
		public bool HasUserProfileQueryDependencies
		{
			get
			{
				return (this.m_userReferenceLocation & UserLocationFlags.ReportQueries) != (UserLocationFlags)0;
			}
		}

		// Token: 0x17001FBD RID: 8125
		// (get) Token: 0x06005714 RID: 22292 RVA: 0x0016F138 File Offset: 0x0016D338
		public bool HasUserProfileReportDependencies
		{
			get
			{
				return (this.m_userReferenceLocation & UserLocationFlags.ReportBody) != (UserLocationFlags)0 || (this.m_userReferenceLocation & UserLocationFlags.ReportPageSection) != (UserLocationFlags)0;
			}
		}

		// Token: 0x17001FBE RID: 8126
		// (get) Token: 0x06005715 RID: 22293 RVA: 0x0016F151 File Offset: 0x0016D351
		public ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x17001FBF RID: 8127
		// (get) Token: 0x06005716 RID: 22294 RVA: 0x0016F159 File Offset: 0x0016D359
		public DataSourceInfoCollection DataSources
		{
			get
			{
				return this.m_dataSources;
			}
		}

		// Token: 0x17001FC0 RID: 8128
		// (get) Token: 0x06005717 RID: 22295 RVA: 0x0016F161 File Offset: 0x0016D361
		public ProcessingMessageList Warnings
		{
			get
			{
				return this.m_warnings;
			}
		}

		// Token: 0x17001FC1 RID: 8129
		// (get) Token: 0x06005718 RID: 22296 RVA: 0x0016F169 File Offset: 0x0016D369
		public PageProperties PageProperties
		{
			get
			{
				return this.m_pageProperties;
			}
		}

		// Token: 0x17001FC2 RID: 8130
		// (get) Token: 0x06005719 RID: 22297 RVA: 0x0016F171 File Offset: 0x0016D371
		public string[] DataSetsName
		{
			get
			{
				return this.m_dataSetsName;
			}
		}

		// Token: 0x17001FC3 RID: 8131
		// (get) Token: 0x0600571A RID: 22298 RVA: 0x0016F179 File Offset: 0x0016D379
		public bool HasExternalImages
		{
			get
			{
				return this.m_hasExternalImages;
			}
		}

		// Token: 0x17001FC4 RID: 8132
		// (get) Token: 0x0600571B RID: 22299 RVA: 0x0016F181 File Offset: 0x0016D381
		public bool HasHyperlinks
		{
			get
			{
				return this.m_hasHyperlinks;
			}
		}

		// Token: 0x17001FC5 RID: 8133
		// (get) Token: 0x0600571C RID: 22300 RVA: 0x0016F189 File Offset: 0x0016D389
		public ReportProcessingFlags ReportProcessingFlags
		{
			get
			{
				return this.m_reportProcessingFlags;
			}
		}

		// Token: 0x17001FC6 RID: 8134
		// (get) Token: 0x0600571D RID: 22301 RVA: 0x0016F191 File Offset: 0x0016D391
		public byte[] DataSetsHash
		{
			get
			{
				return this.m_dataSetsHash;
			}
		}

		// Token: 0x17001FC7 RID: 8135
		// (get) Token: 0x0600571E RID: 22302 RVA: 0x0016F199 File Offset: 0x0016D399
		public EmbeddedDataSetInfoCollection EmbeddedDataSets
		{
			get
			{
				return this.m_embeddedDataSets;
			}
		}

		// Token: 0x17001FC8 RID: 8136
		// (get) Token: 0x0600571F RID: 22303 RVA: 0x0016F1A1 File Offset: 0x0016D3A1
		public DataSetInfoCollection SharedDataSets
		{
			get
			{
				return this.m_sharedDataSets;
			}
		}

		// Token: 0x17001FC9 RID: 8137
		// (get) Token: 0x06005720 RID: 22304 RVA: 0x0016F1A9 File Offset: 0x0016D3A9
		public int? CustomCodeSize
		{
			get
			{
				return this.m_customCodeSize;
			}
		}

		// Token: 0x17001FCA RID: 8138
		// (get) Token: 0x06005721 RID: 22305 RVA: 0x0016F1B1 File Offset: 0x0016D3B1
		public ExpressionUsage ExpressionUsage
		{
			get
			{
				return this.m_expressionUsage;
			}
		}

		// Token: 0x17001FCB RID: 8139
		// (get) Token: 0x06005722 RID: 22306 RVA: 0x0016F1B9 File Offset: 0x0016D3B9
		public string WebAuthoringVersion
		{
			get
			{
				return this.m_webAuthoringVersion;
			}
		}

		// Token: 0x17001FCC RID: 8140
		// (get) Token: 0x06005723 RID: 22307 RVA: 0x0016F1C1 File Offset: 0x0016D3C1
		public string DefaultView
		{
			get
			{
				return this.m_defaultView;
			}
		}

		// Token: 0x17001FCD RID: 8141
		// (get) Token: 0x06005724 RID: 22308 RVA: 0x0016F1C9 File Offset: 0x0016D3C9
		public AuthoringMetadata AuthoringMetadata
		{
			get
			{
				return this.m_authoringMetadata;
			}
		}

		// Token: 0x17001FCE RID: 8142
		// (get) Token: 0x06005725 RID: 22309 RVA: 0x0016F1D1 File Offset: 0x0016D3D1
		public bool HasSubReports
		{
			get
			{
				return this.m_hasSubReports;
			}
		}

		// Token: 0x17001FCF RID: 8143
		// (get) Token: 0x06005726 RID: 22310 RVA: 0x0016F1D9 File Offset: 0x0016D3D9
		public bool HasDrillthroughs
		{
			get
			{
				return this.m_hasDrillthroughs;
			}
		}

		// Token: 0x04002DFD RID: 11773
		private string m_reportDescription;

		// Token: 0x04002DFE RID: 11774
		private string m_reportLanguage;

		// Token: 0x04002DFF RID: 11775
		private string m_rdlReportLanguage;

		// Token: 0x04002E00 RID: 11776
		private ParameterInfoCollection m_parameters;

		// Token: 0x04002E01 RID: 11777
		private DataSourceInfoCollection m_dataSources;

		// Token: 0x04002E02 RID: 11778
		private EmbeddedDataSetInfoCollection m_embeddedDataSets;

		// Token: 0x04002E03 RID: 11779
		private ProcessingMessageList m_warnings;

		// Token: 0x04002E04 RID: 11780
		private UserLocationFlags m_userReferenceLocation;

		// Token: 0x04002E05 RID: 11781
		private PageProperties m_pageProperties;

		// Token: 0x04002E06 RID: 11782
		private string[] m_dataSetsName;

		// Token: 0x04002E07 RID: 11783
		private bool m_hasExternalImages;

		// Token: 0x04002E08 RID: 11784
		private bool m_hasHyperlinks;

		// Token: 0x04002E09 RID: 11785
		private ReportProcessingFlags m_reportProcessingFlags;

		// Token: 0x04002E0A RID: 11786
		private byte[] m_dataSetsHash;

		// Token: 0x04002E0B RID: 11787
		private DataSetInfoCollection m_sharedDataSets;

		// Token: 0x04002E0C RID: 11788
		private int? m_customCodeSize;

		// Token: 0x04002E0D RID: 11789
		private ExpressionUsage m_expressionUsage;

		// Token: 0x04002E0E RID: 11790
		private string m_webAuthoringVersion;

		// Token: 0x04002E0F RID: 11791
		private string m_defaultView;

		// Token: 0x04002E10 RID: 11792
		private AuthoringMetadata m_authoringMetadata;

		// Token: 0x04002E11 RID: 11793
		private bool m_hasSubReports;

		// Token: 0x04002E12 RID: 11794
		private bool m_hasDrillthroughs;
	}
}
