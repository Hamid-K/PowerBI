using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000500 RID: 1280
	[Serializable]
	internal sealed class ReportSnapshot : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600424B RID: 16971 RVA: 0x00116EA0 File Offset: 0x001150A0
		internal ReportSnapshot(Report report, string reportName, ParameterInfoCollection parameters, string requestUserName, DateTime executionTime, string reportServerUrl, string reportFolder, string language)
		{
			this.m_report = report;
			this.m_reportName = reportName;
			this.m_parameters = parameters;
			this.m_requestUserName = requestUserName;
			this.m_executionTime = executionTime;
			this.m_reportServerUrl = reportServerUrl;
			this.m_reportFolder = reportFolder;
			this.m_language = language;
			this.m_hasDocumentMap = report.HasLabels;
			this.m_definitionHasDocumentMap = new bool?(report.HasLabels);
			this.m_hasBookmarks = report.HasBookmarks;
			this.m_cachedDataChanged = true;
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x00116F2C File Offset: 0x0011512C
		internal ReportSnapshot()
		{
			this.m_executionTime = DateTime.Now;
			this.m_cachedDataChanged = false;
		}

		// Token: 0x17001BDB RID: 7131
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x00116F52 File Offset: 0x00115152
		// (set) Token: 0x0600424E RID: 16974 RVA: 0x00116F5A File Offset: 0x0011515A
		internal Report Report
		{
			get
			{
				return this.m_report;
			}
			set
			{
				this.m_report = value;
			}
		}

		// Token: 0x17001BDC RID: 7132
		// (get) Token: 0x0600424F RID: 16975 RVA: 0x00116F63 File Offset: 0x00115163
		// (set) Token: 0x06004250 RID: 16976 RVA: 0x00116F6B File Offset: 0x0011516B
		internal bool HasDocumentMap
		{
			get
			{
				return this.m_hasDocumentMap;
			}
			set
			{
				this.m_hasDocumentMap = value;
				this.m_cachedDataChanged = true;
			}
		}

		// Token: 0x17001BDD RID: 7133
		// (get) Token: 0x06004251 RID: 16977 RVA: 0x00116F7B File Offset: 0x0011517B
		private bool DocumentMapHasRenderFormatDependency
		{
			get
			{
				return this.m_documentMapRenderFormat != null;
			}
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x00116F86 File Offset: 0x00115186
		internal bool CanUseExistingDocumentMapChunk(ICatalogItemContext reportContext)
		{
			return this.HasDocumentMap && (!this.DocumentMapHasRenderFormatDependency || RenderFormatImpl.IsRenderFormat(ReportSnapshot.NormalizeRenderFormatForDocumentMap(reportContext), this.m_documentMapRenderFormat));
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x00116FB0 File Offset: 0x001151B0
		private static string NormalizeRenderFormatForDocumentMap(ICatalogItemContext reportContext)
		{
			bool flag;
			string text = RenderFormatImpl.NormalizeRenderFormat(reportContext, out flag);
			if (flag)
			{
				text = "RPL";
			}
			return text;
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x00116FD0 File Offset: 0x001151D0
		internal void SetRenderFormatDependencyInDocumentMap(OnDemandProcessingContext odpContext)
		{
			this.m_cachedDataChanged = true;
			this.m_documentMapRenderFormat = ReportSnapshot.NormalizeRenderFormatForDocumentMap(odpContext.TopLevelContext.ReportContext);
		}

		// Token: 0x17001BDE RID: 7134
		// (get) Token: 0x06004255 RID: 16981 RVA: 0x00116FEF File Offset: 0x001151EF
		// (set) Token: 0x06004256 RID: 16982 RVA: 0x0011701F File Offset: 0x0011521F
		internal bool DefinitionTreeHasDocumentMap
		{
			get
			{
				if (this.m_definitionHasDocumentMap == null)
				{
					this.m_definitionHasDocumentMap = new bool?(this.m_report.HasLabels);
				}
				return this.m_definitionHasDocumentMap.Value;
			}
			set
			{
				this.m_definitionHasDocumentMap = new bool?(value);
			}
		}

		// Token: 0x17001BDF RID: 7135
		// (get) Token: 0x06004257 RID: 16983 RVA: 0x0011702D File Offset: 0x0011522D
		// (set) Token: 0x06004258 RID: 16984 RVA: 0x00117035 File Offset: 0x00115235
		internal bool HasBookmarks
		{
			get
			{
				return this.m_hasBookmarks;
			}
			set
			{
				this.m_hasBookmarks = value;
			}
		}

		// Token: 0x17001BE0 RID: 7136
		// (get) Token: 0x06004259 RID: 16985 RVA: 0x0011703E File Offset: 0x0011523E
		// (set) Token: 0x0600425A RID: 16986 RVA: 0x00117046 File Offset: 0x00115246
		internal bool HasShowHide
		{
			get
			{
				return this.m_hasShowHide;
			}
			set
			{
				this.m_hasShowHide = value;
			}
		}

		// Token: 0x17001BE1 RID: 7137
		// (get) Token: 0x0600425B RID: 16987 RVA: 0x0011704F File Offset: 0x0011524F
		// (set) Token: 0x0600425C RID: 16988 RVA: 0x00117057 File Offset: 0x00115257
		internal bool HasUserSortFilter
		{
			get
			{
				return this.m_hasUserSortFilter;
			}
			set
			{
				this.m_hasUserSortFilter = value;
			}
		}

		// Token: 0x17001BE2 RID: 7138
		// (get) Token: 0x0600425D RID: 16989 RVA: 0x00117060 File Offset: 0x00115260
		internal string RequestUserName
		{
			get
			{
				return this.m_requestUserName;
			}
		}

		// Token: 0x17001BE3 RID: 7139
		// (get) Token: 0x0600425E RID: 16990 RVA: 0x00117068 File Offset: 0x00115268
		internal DateTime ExecutionTime
		{
			get
			{
				return this.m_executionTime;
			}
		}

		// Token: 0x17001BE4 RID: 7140
		// (get) Token: 0x0600425F RID: 16991 RVA: 0x00117070 File Offset: 0x00115270
		internal string ReportServerUrl
		{
			get
			{
				return this.m_reportServerUrl;
			}
		}

		// Token: 0x17001BE5 RID: 7141
		// (get) Token: 0x06004260 RID: 16992 RVA: 0x00117078 File Offset: 0x00115278
		internal string ReportFolder
		{
			get
			{
				return this.m_reportFolder;
			}
		}

		// Token: 0x17001BE6 RID: 7142
		// (get) Token: 0x06004261 RID: 16993 RVA: 0x00117080 File Offset: 0x00115280
		internal string Language
		{
			get
			{
				return this.m_language;
			}
		}

		// Token: 0x17001BE7 RID: 7143
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x00117088 File Offset: 0x00115288
		// (set) Token: 0x06004263 RID: 16995 RVA: 0x00117090 File Offset: 0x00115290
		internal ProcessingMessageList Warnings
		{
			get
			{
				return this.m_processingMessages;
			}
			set
			{
				this.m_processingMessages = value;
			}
		}

		// Token: 0x17001BE8 RID: 7144
		// (get) Token: 0x06004264 RID: 16996 RVA: 0x00117099 File Offset: 0x00115299
		// (set) Token: 0x06004265 RID: 16997 RVA: 0x001170A1 File Offset: 0x001152A1
		internal ReportInstance ReportInstance
		{
			get
			{
				return this.m_reportInstance;
			}
			set
			{
				this.m_reportInstance = value;
			}
		}

		// Token: 0x17001BE9 RID: 7145
		// (get) Token: 0x06004266 RID: 16998 RVA: 0x001170AA File Offset: 0x001152AA
		internal bool CachedDataChanged
		{
			get
			{
				return this.m_cachedDataChanged;
			}
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x001170B2 File Offset: 0x001152B2
		internal void AddImageChunkName(string definitionKey, string name)
		{
			this.m_cachedDataChanged = true;
			if (this.m_cachedDatabaseImages == null)
			{
				this.m_cachedDatabaseImages = new Dictionary<string, string>(EqualityComparers.StringComparerInstance);
			}
			this.m_cachedDatabaseImages.Add(definitionKey, name);
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x001170E0 File Offset: 0x001152E0
		internal bool TryGetImageChunkName(string definitionKey, out string name)
		{
			if (this.m_cachedDatabaseImages != null)
			{
				return this.m_cachedDatabaseImages.TryGetValue(definitionKey, out name);
			}
			name = null;
			return false;
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x001170FC File Offset: 0x001152FC
		internal void AddGeneratedReportItemChunkName(string definitionKey, string name)
		{
			this.m_cachedDataChanged = true;
			if (this.m_cachedGeneratedReportItems == null)
			{
				this.m_cachedGeneratedReportItems = new Dictionary<string, string>(EqualityComparers.StringComparerInstance);
			}
			this.m_cachedGeneratedReportItems.Add(definitionKey, name);
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x0011712A File Offset: 0x0011532A
		internal bool TryGetGeneratedReportItemChunkName(string definitionKey, out string name)
		{
			if (this.m_cachedGeneratedReportItems != null)
			{
				return this.m_cachedGeneratedReportItems.TryGetValue(definitionKey, out name);
			}
			name = null;
			return false;
		}

		// Token: 0x17001BEA RID: 7146
		// (get) Token: 0x0600426B RID: 17003 RVA: 0x00117146 File Offset: 0x00115346
		internal ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x17001BEB RID: 7147
		// (get) Token: 0x0600426C RID: 17004 RVA: 0x0011714E File Offset: 0x0011534E
		internal Dictionary<string, List<string>> AggregateFieldReferences
		{
			get
			{
				if (this.m_aggregateFieldReferences == null)
				{
					this.m_aggregateFieldReferences = new Dictionary<string, List<string>>();
				}
				return this.m_aggregateFieldReferences;
			}
		}

		// Token: 0x17001BEC RID: 7148
		// (get) Token: 0x0600426D RID: 17005 RVA: 0x00117169 File Offset: 0x00115369
		// (set) Token: 0x0600426E RID: 17006 RVA: 0x00117171 File Offset: 0x00115371
		internal SortFilterEventInfoMap SortFilterEventInfo
		{
			get
			{
				return this.m_sortFilterEventInfo;
			}
			set
			{
				this.m_sortFilterEventInfo = value;
			}
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x0011717C File Offset: 0x0011537C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ReportSnapshot.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.HasDocumentMap)
				{
					if (memberName <= MemberName.Report)
					{
						if (memberName == MemberName.Language)
						{
							writer.Write(this.m_language);
							continue;
						}
						if (memberName == MemberName.Parameters)
						{
							writer.Write(null);
							continue;
						}
						if (memberName == MemberName.Report)
						{
							Global.Tracer.Assert(this.m_report != null);
							writer.WriteReference(this.m_report);
							continue;
						}
					}
					else if (memberName <= MemberName.HasBookmarks)
					{
						if (memberName == MemberName.RequestUserName)
						{
							writer.Write(this.m_requestUserName);
							continue;
						}
						if (memberName == MemberName.HasBookmarks)
						{
							writer.Write(this.m_hasBookmarks);
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.ExecutionTime:
							writer.Write(this.m_executionTime);
							continue;
						case MemberName.ReportServerUrl:
							writer.Write(this.m_reportServerUrl);
							continue;
						case MemberName.ReportFolder:
							writer.Write(this.m_reportFolder);
							continue;
						case MemberName.Formula:
							break;
						case MemberName.ProcessingMessages:
							writer.Write(this.m_processingMessages);
							continue;
						default:
							if (memberName == MemberName.HasDocumentMap)
							{
								writer.Write(this.m_hasDocumentMap);
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.GeneratedReportItemChunkNames)
				{
					if (memberName <= MemberName.HasUserSortFilter)
					{
						if (memberName == MemberName.HasShowHide)
						{
							writer.Write(this.m_hasShowHide);
							continue;
						}
						if (memberName == MemberName.HasUserSortFilter)
						{
							writer.Write(this.m_hasUserSortFilter);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ImageChunkNames)
						{
							writer.WriteStringStringHashtable(this.m_cachedDatabaseImages);
							continue;
						}
						if (memberName == MemberName.GeneratedReportItemChunkNames)
						{
							writer.WriteStringStringHashtable(this.m_cachedGeneratedReportItems);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.SnapshotParameters)
				{
					if (memberName == MemberName.AggregateFieldReferences)
					{
						writer.WriteStringListOfStringDictionary(this.m_aggregateFieldReferences);
						continue;
					}
					if (memberName == MemberName.SnapshotParameters)
					{
						writer.Write(this.m_parameters);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DefinitionHasDocumentMap)
					{
						writer.Write(this.m_definitionHasDocumentMap);
						continue;
					}
					if (memberName == MemberName.DocumentMapRenderFormat)
					{
						writer.Write(this.m_documentMapRenderFormat);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x00117404 File Offset: 0x00115604
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			ParameterInfoCollection parameterInfoCollection = null;
			ParameterInfoCollection parameterInfoCollection2 = null;
			reader.RegisterDeclaration(ReportSnapshot.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.HasDocumentMap)
				{
					if (memberName <= MemberName.Report)
					{
						if (memberName == MemberName.Language)
						{
							this.m_language = reader.ReadString();
							continue;
						}
						if (memberName == MemberName.Parameters)
						{
							parameterInfoCollection = new ParameterInfoCollection();
							reader.ReadListOfRIFObjects(parameterInfoCollection);
							continue;
						}
						if (memberName == MemberName.Report)
						{
							this.m_report = reader.ReadReference<Report>(this);
							continue;
						}
					}
					else if (memberName <= MemberName.HasBookmarks)
					{
						if (memberName == MemberName.RequestUserName)
						{
							this.m_requestUserName = reader.ReadString();
							continue;
						}
						if (memberName == MemberName.HasBookmarks)
						{
							this.m_hasBookmarks = reader.ReadBoolean();
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.ExecutionTime:
							this.m_executionTime = reader.ReadDateTime();
							continue;
						case MemberName.ReportServerUrl:
							this.m_reportServerUrl = reader.ReadString();
							continue;
						case MemberName.ReportFolder:
							this.m_reportFolder = reader.ReadString();
							continue;
						case MemberName.Formula:
							break;
						case MemberName.ProcessingMessages:
							this.m_processingMessages = reader.ReadListOfRIFObjects<ProcessingMessageList>();
							continue;
						default:
							if (memberName == MemberName.HasDocumentMap)
							{
								this.m_hasDocumentMap = reader.ReadBoolean();
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.GeneratedReportItemChunkNames)
				{
					if (memberName <= MemberName.HasUserSortFilter)
					{
						if (memberName == MemberName.HasShowHide)
						{
							this.m_hasShowHide = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.HasUserSortFilter)
						{
							this.m_hasUserSortFilter = reader.ReadBoolean();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ImageChunkNames)
						{
							this.m_cachedDatabaseImages = reader.ReadStringStringHashtable<Dictionary<string, string>>();
							continue;
						}
						if (memberName == MemberName.GeneratedReportItemChunkNames)
						{
							this.m_cachedGeneratedReportItems = reader.ReadStringStringHashtable<Dictionary<string, string>>();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.SnapshotParameters)
				{
					if (memberName == MemberName.AggregateFieldReferences)
					{
						this.m_aggregateFieldReferences = reader.ReadStringListOfStringDictionary();
						continue;
					}
					if (memberName == MemberName.SnapshotParameters)
					{
						parameterInfoCollection2 = (ParameterInfoCollection)reader.ReadRIFObject();
						continue;
					}
				}
				else if (memberName != MemberName.DefinitionHasDocumentMap)
				{
					if (memberName == MemberName.DocumentMapRenderFormat)
					{
						this.m_documentMapRenderFormat = reader.ReadString();
						continue;
					}
				}
				else
				{
					object obj = reader.ReadVariant();
					if (obj != null)
					{
						this.m_definitionHasDocumentMap = new bool?((bool)obj);
						continue;
					}
					continue;
				}
				Global.Tracer.Assert(false);
			}
			this.m_parameters = parameterInfoCollection;
			if ((parameterInfoCollection == null || parameterInfoCollection.Count == 0) && parameterInfoCollection2 != null)
			{
				this.m_parameters = parameterInfoCollection2;
			}
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x001176B0 File Offset: 0x001158B0
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ReportSnapshot.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.Report)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_report = (Report)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x00117754 File Offset: 0x00115954
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportSnapshot;
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x0011775C File Offset: 0x0011595C
		[SkipMemberStaticValidation(MemberName.Parameters)]
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportSnapshot, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ExecutionTime, Token.DateTime),
				new MemberInfo(MemberName.Report, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Report, Token.Reference),
				new MemberInfo(MemberName.HasDocumentMap, Token.Boolean),
				new MemberInfo(MemberName.HasShowHide, Token.Boolean),
				new MemberInfo(MemberName.HasBookmarks, Token.Boolean),
				new MemberInfo(MemberName.RequestUserName, Token.String),
				new MemberInfo(MemberName.ReportServerUrl, Token.String),
				new MemberInfo(MemberName.ReportFolder, Token.String),
				new MemberInfo(MemberName.Language, Token.String),
				new MemberInfo(MemberName.Parameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInfo),
				new MemberInfo(MemberName.ImageChunkNames, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringStringHashtable),
				new MemberInfo(MemberName.HasUserSortFilter, Token.Boolean),
				new MemberInfo(MemberName.GeneratedReportItemChunkNames, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringStringHashtable),
				new MemberInfo(MemberName.AggregateFieldReferences, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringListOfStringDictionary, Token.String, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList),
				new MemberInfo(MemberName.SnapshotParameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInfoCollection),
				new MemberInfo(MemberName.ProcessingMessages, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ProcessingMessage),
				new MemberInfo(MemberName.DefinitionHasDocumentMap, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Nullable, Token.Boolean),
				new MemberInfo(MemberName.DocumentMapRenderFormat, Token.String)
			});
		}

		// Token: 0x04001E4B RID: 7755
		private DateTime m_executionTime;

		// Token: 0x04001E4C RID: 7756
		private Report m_report;

		// Token: 0x04001E4D RID: 7757
		private bool m_hasDocumentMap;

		// Token: 0x04001E4E RID: 7758
		private bool? m_definitionHasDocumentMap = new bool?(false);

		// Token: 0x04001E4F RID: 7759
		private string m_documentMapRenderFormat;

		// Token: 0x04001E50 RID: 7760
		private bool m_hasShowHide;

		// Token: 0x04001E51 RID: 7761
		private bool m_hasBookmarks;

		// Token: 0x04001E52 RID: 7762
		private string m_requestUserName;

		// Token: 0x04001E53 RID: 7763
		private string m_reportServerUrl;

		// Token: 0x04001E54 RID: 7764
		private string m_reportFolder;

		// Token: 0x04001E55 RID: 7765
		private string m_language;

		// Token: 0x04001E56 RID: 7766
		private ProcessingMessageList m_processingMessages;

		// Token: 0x04001E57 RID: 7767
		private Dictionary<string, string> m_cachedDatabaseImages;

		// Token: 0x04001E58 RID: 7768
		private Dictionary<string, string> m_cachedGeneratedReportItems;

		// Token: 0x04001E59 RID: 7769
		private ParameterInfoCollection m_parameters;

		// Token: 0x04001E5A RID: 7770
		private bool m_hasUserSortFilter;

		// Token: 0x04001E5B RID: 7771
		private Dictionary<string, List<string>> m_aggregateFieldReferences;

		// Token: 0x04001E5C RID: 7772
		[NonSerialized]
		private bool m_cachedDataChanged;

		// Token: 0x04001E5D RID: 7773
		[NonSerialized]
		private ReportInstance m_reportInstance;

		// Token: 0x04001E5E RID: 7774
		[NonSerialized]
		private SortFilterEventInfoMap m_sortFilterEventInfo;

		// Token: 0x04001E5F RID: 7775
		[NonSerialized]
		private string m_reportName;

		// Token: 0x04001E60 RID: 7776
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ReportSnapshot.GetDeclaration();
	}
}
