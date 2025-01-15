using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004F9 RID: 1273
	[Serializable]
	internal sealed class Report : Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IRIFReportScope, IInstancePath, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGloballyReferenceable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGlobalIDOwner, IExpressionHostAssemblyHolder
	{
		// Token: 0x060040BE RID: 16574 RVA: 0x00110CF0 File Offset: 0x0010EEF0
		internal Report()
			: base(null)
		{
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x00110D30 File Offset: 0x0010EF30
		internal Report(int id, int idForReportItems)
			: base(id, null)
		{
			this.m_reportVersion = Guid.NewGuid();
			this.m_height = "11in";
			this.m_width = "8.5in";
			this.m_dataSources = new List<DataSource>();
			this.m_exprCompiledCode = new byte[0];
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x00110DB2 File Offset: 0x0010EFB2
		internal Report(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x17001B41 RID: 6977
		// (get) Token: 0x060040C1 RID: 16577 RVA: 0x00110DF0 File Offset: 0x0010EFF0
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Report;
			}
		}

		// Token: 0x17001B42 RID: 6978
		// (get) Token: 0x060040C2 RID: 16578 RVA: 0x00110DF3 File Offset: 0x0010EFF3
		internal override string DataElementNameDefault
		{
			get
			{
				return "Report";
			}
		}

		// Token: 0x17001B43 RID: 6979
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x00110DFA File Offset: 0x0010EFFA
		// (set) Token: 0x060040C4 RID: 16580 RVA: 0x00110E02 File Offset: 0x0010F002
		internal bool ConsumeContainerWhitespace
		{
			get
			{
				return this.m_consumeContainerWhitespace;
			}
			set
			{
				this.m_consumeContainerWhitespace = value;
			}
		}

		// Token: 0x17001B44 RID: 6980
		// (get) Token: 0x060040C5 RID: 16581 RVA: 0x00110E0B File Offset: 0x0010F00B
		// (set) Token: 0x060040C6 RID: 16582 RVA: 0x00110E13 File Offset: 0x0010F013
		internal string Author
		{
			get
			{
				return this.m_author;
			}
			set
			{
				this.m_author = value;
			}
		}

		// Token: 0x17001B45 RID: 6981
		// (get) Token: 0x060040C7 RID: 16583 RVA: 0x00110E1C File Offset: 0x0010F01C
		// (set) Token: 0x060040C8 RID: 16584 RVA: 0x00110E24 File Offset: 0x0010F024
		internal string DefaultFontFamily
		{
			get
			{
				return this.m_defaultFontFamily;
			}
			set
			{
				this.m_defaultFontFamily = value;
			}
		}

		// Token: 0x17001B46 RID: 6982
		// (get) Token: 0x060040C9 RID: 16585 RVA: 0x00110E2D File Offset: 0x0010F02D
		// (set) Token: 0x060040CA RID: 16586 RVA: 0x00110E35 File Offset: 0x0010F035
		internal ExpressionInfo AutoRefreshExpression
		{
			get
			{
				return this.m_autoRefreshExpression;
			}
			set
			{
				this.m_autoRefreshExpression = value;
			}
		}

		// Token: 0x17001B47 RID: 6983
		// (get) Token: 0x060040CB RID: 16587 RVA: 0x00110E3E File Offset: 0x0010F03E
		// (set) Token: 0x060040CC RID: 16588 RVA: 0x00110E46 File Offset: 0x0010F046
		internal Dictionary<string, ImageInfo> EmbeddedImages
		{
			get
			{
				return this.m_embeddedImages;
			}
			set
			{
				this.m_embeddedImages = value;
			}
		}

		// Token: 0x17001B48 RID: 6984
		// (get) Token: 0x060040CD RID: 16589 RVA: 0x00110E4F File Offset: 0x0010F04F
		// (set) Token: 0x060040CE RID: 16590 RVA: 0x00110E57 File Offset: 0x0010F057
		internal List<DataSource> DataSources
		{
			get
			{
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x17001B49 RID: 6985
		// (get) Token: 0x060040CF RID: 16591 RVA: 0x00110E60 File Offset: 0x0010F060
		internal int DataSourceCount
		{
			get
			{
				if (this.m_dataSources != null)
				{
					return this.m_dataSources.Count;
				}
				return 0;
			}
		}

		// Token: 0x17001B4A RID: 6986
		// (get) Token: 0x060040D0 RID: 16592 RVA: 0x00110E77 File Offset: 0x0010F077
		internal int DataSetCount
		{
			get
			{
				if (this.MappingNameToDataSet != null)
				{
					return this.MappingNameToDataSet.Count;
				}
				return 0;
			}
		}

		// Token: 0x17001B4B RID: 6987
		// (get) Token: 0x060040D1 RID: 16593 RVA: 0x00110E8E File Offset: 0x0010F08E
		// (set) Token: 0x060040D2 RID: 16594 RVA: 0x00110E96 File Offset: 0x0010F096
		internal int DataPipelineCount
		{
			get
			{
				return this.m_dataPipelineCount;
			}
			set
			{
				this.m_dataPipelineCount = value;
			}
		}

		// Token: 0x17001B4C RID: 6988
		// (get) Token: 0x060040D3 RID: 16595 RVA: 0x00110E9F File Offset: 0x0010F09F
		// (set) Token: 0x060040D4 RID: 16596 RVA: 0x00110ED7 File Offset: 0x0010F0D7
		internal DataSource SharedDSContainer
		{
			get
			{
				if (this.m_sharedDSContainer == null && this.m_sharedDSContainerCollectionIndex >= 0 && this.m_dataSources != null)
				{
					this.m_sharedDSContainer = this.m_dataSources[this.m_sharedDSContainerCollectionIndex];
				}
				return this.m_sharedDSContainer;
			}
			set
			{
				this.m_sharedDSContainer = value;
			}
		}

		// Token: 0x17001B4D RID: 6989
		// (get) Token: 0x060040D5 RID: 16597 RVA: 0x00110EE0 File Offset: 0x0010F0E0
		// (set) Token: 0x060040D6 RID: 16598 RVA: 0x00110EE8 File Offset: 0x0010F0E8
		internal int SharedDSContainerCollectionIndex
		{
			get
			{
				return this.m_sharedDSContainerCollectionIndex;
			}
			set
			{
				this.m_sharedDSContainerCollectionIndex = value;
			}
		}

		// Token: 0x17001B4E RID: 6990
		// (get) Token: 0x060040D7 RID: 16599 RVA: 0x00110EF1 File Offset: 0x0010F0F1
		internal bool HasSharedDataSetReferences
		{
			get
			{
				return -1 != this.m_sharedDSContainerCollectionIndex;
			}
		}

		// Token: 0x17001B4F RID: 6991
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x00110EFF File Offset: 0x0010F0FF
		// (set) Token: 0x060040D9 RID: 16601 RVA: 0x00110F07 File Offset: 0x0010F107
		internal bool MergeOnePass
		{
			get
			{
				return this.m_mergeOnePass;
			}
			set
			{
				this.m_mergeOnePass = value;
			}
		}

		// Token: 0x17001B50 RID: 6992
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x00110F10 File Offset: 0x0010F110
		// (set) Token: 0x060040DB RID: 16603 RVA: 0x00110F18 File Offset: 0x0010F118
		internal bool SubReportMergeTransactions
		{
			get
			{
				return this.m_subReportMergeTransactions;
			}
			set
			{
				this.m_subReportMergeTransactions = value;
			}
		}

		// Token: 0x17001B51 RID: 6993
		// (get) Token: 0x060040DC RID: 16604 RVA: 0x00110F21 File Offset: 0x0010F121
		// (set) Token: 0x060040DD RID: 16605 RVA: 0x00110F33 File Offset: 0x0010F133
		internal bool NeedPostGroupProcessing
		{
			get
			{
				return this.m_needPostGroupProcessing || this.HasVariables;
			}
			set
			{
				this.m_needPostGroupProcessing = value;
			}
		}

		// Token: 0x17001B52 RID: 6994
		// (get) Token: 0x060040DE RID: 16606 RVA: 0x00110F3C File Offset: 0x0010F13C
		// (set) Token: 0x060040DF RID: 16607 RVA: 0x00110F44 File Offset: 0x0010F144
		internal bool HasPostSortAggregates
		{
			get
			{
				return this.m_hasPostSortAggregates;
			}
			set
			{
				this.m_hasPostSortAggregates = value;
			}
		}

		// Token: 0x17001B53 RID: 6995
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x00110F4D File Offset: 0x0010F14D
		// (set) Token: 0x060040E1 RID: 16609 RVA: 0x00110F55 File Offset: 0x0010F155
		internal bool HasAggregatesOfAggregates
		{
			get
			{
				return this.m_hasAggregatesOfAggregates;
			}
			set
			{
				this.m_hasAggregatesOfAggregates = value;
			}
		}

		// Token: 0x17001B54 RID: 6996
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x00110F5E File Offset: 0x0010F15E
		// (set) Token: 0x060040E3 RID: 16611 RVA: 0x00110F66 File Offset: 0x0010F166
		internal bool HasAggregatesOfAggregatesInUserSort
		{
			get
			{
				return this.m_hasAggregatesOfAggregatesInUserSort;
			}
			set
			{
				this.m_hasAggregatesOfAggregatesInUserSort = value;
			}
		}

		// Token: 0x17001B55 RID: 6997
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x00110F6F File Offset: 0x0010F16F
		// (set) Token: 0x060040E5 RID: 16613 RVA: 0x00110F77 File Offset: 0x0010F177
		internal bool HasReportItemReferences
		{
			get
			{
				return this.m_hasReportItemReferences;
			}
			set
			{
				this.m_hasReportItemReferences = value;
			}
		}

		// Token: 0x17001B56 RID: 6998
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x00110F80 File Offset: 0x0010F180
		// (set) Token: 0x060040E7 RID: 16615 RVA: 0x00110F88 File Offset: 0x0010F188
		internal int DataSetsNotOnlyUsedInParameters
		{
			get
			{
				return this.m_dataSetsNotOnlyUsedInParameters;
			}
			set
			{
				this.m_dataSetsNotOnlyUsedInParameters = value;
			}
		}

		// Token: 0x17001B57 RID: 6999
		// (get) Token: 0x060040E8 RID: 16616 RVA: 0x00110F91 File Offset: 0x0010F191
		// (set) Token: 0x060040E9 RID: 16617 RVA: 0x00110F99 File Offset: 0x0010F199
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Report.ShowHideTypes ShowHideType
		{
			get
			{
				return this.m_showHideType;
			}
			set
			{
				this.m_showHideType = value;
			}
		}

		// Token: 0x17001B58 RID: 7000
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x00110FA2 File Offset: 0x0010F1A2
		// (set) Token: 0x060040EB RID: 16619 RVA: 0x00110FAA File Offset: 0x0010F1AA
		internal bool ParametersNotUsedInQuery
		{
			get
			{
				return this.m_parametersNotUsedInQuery;
			}
			set
			{
				this.m_parametersNotUsedInQuery = value;
			}
		}

		// Token: 0x17001B59 RID: 7001
		// (get) Token: 0x060040EC RID: 16620 RVA: 0x00110FB3 File Offset: 0x0010F1B3
		// (set) Token: 0x060040ED RID: 16621 RVA: 0x00110FBB File Offset: 0x0010F1BB
		internal int LastID
		{
			get
			{
				return this.m_lastID;
			}
			set
			{
				this.m_lastID = value;
			}
		}

		// Token: 0x17001B5A RID: 7002
		// (get) Token: 0x060040EE RID: 16622 RVA: 0x00110FC4 File Offset: 0x0010F1C4
		// (set) Token: 0x060040EF RID: 16623 RVA: 0x00110FCC File Offset: 0x0010F1CC
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport> SubReports
		{
			get
			{
				return this.m_subReports;
			}
			set
			{
				this.m_subReports = value;
			}
		}

		// Token: 0x17001B5B RID: 7003
		// (get) Token: 0x060040F0 RID: 16624 RVA: 0x00110FD5 File Offset: 0x0010F1D5
		// (set) Token: 0x060040F1 RID: 16625 RVA: 0x00110FDD File Offset: 0x0010F1DD
		internal bool HasImageStreams
		{
			get
			{
				return this.m_hasImageStreams;
			}
			set
			{
				this.m_hasImageStreams = value;
			}
		}

		// Token: 0x17001B5C RID: 7004
		// (get) Token: 0x060040F2 RID: 16626 RVA: 0x00110FE6 File Offset: 0x0010F1E6
		// (set) Token: 0x060040F3 RID: 16627 RVA: 0x00110FEE File Offset: 0x0010F1EE
		internal bool HasLabels
		{
			get
			{
				return this.m_hasLabels;
			}
			set
			{
				this.m_hasLabels = value;
			}
		}

		// Token: 0x17001B5D RID: 7005
		// (get) Token: 0x060040F4 RID: 16628 RVA: 0x00110FF7 File Offset: 0x0010F1F7
		// (set) Token: 0x060040F5 RID: 16629 RVA: 0x00110FFF File Offset: 0x0010F1FF
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

		// Token: 0x17001B5E RID: 7006
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x00111008 File Offset: 0x0010F208
		// (set) Token: 0x060040F7 RID: 16631 RVA: 0x00111010 File Offset: 0x0010F210
		internal bool HasHeadersOrFooters
		{
			get
			{
				return this.m_hasHeadersOrFooters;
			}
			set
			{
				this.m_hasHeadersOrFooters = value;
			}
		}

		// Token: 0x17001B5F RID: 7007
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x00111019 File Offset: 0x0010F219
		// (set) Token: 0x060040F9 RID: 16633 RVA: 0x00111021 File Offset: 0x0010F221
		internal List<ParameterDef> Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x17001B60 RID: 7008
		// (get) Token: 0x060040FA RID: 16634 RVA: 0x0011102A File Offset: 0x0010F22A
		// (set) Token: 0x060040FB RID: 16635 RVA: 0x00111032 File Offset: 0x0010F232
		internal string OneDataSetName
		{
			get
			{
				return this.m_oneDataSetName;
			}
			set
			{
				this.m_oneDataSetName = value;
			}
		}

		// Token: 0x17001B61 RID: 7009
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x0011103B File Offset: 0x0010F23B
		// (set) Token: 0x060040FD RID: 16637 RVA: 0x00111043 File Offset: 0x0010F243
		internal bool HasSpecialRecursiveAggregates
		{
			get
			{
				return this.m_hasSpecialRecursiveAggregates;
			}
			set
			{
				this.m_hasSpecialRecursiveAggregates = value;
			}
		}

		// Token: 0x17001B62 RID: 7010
		// (get) Token: 0x060040FE RID: 16638 RVA: 0x0011104C File Offset: 0x0010F24C
		// (set) Token: 0x060040FF RID: 16639 RVA: 0x00111054 File Offset: 0x0010F254
		internal bool HasPreviousAggregates
		{
			get
			{
				return this.m_hasPreviousAggregates;
			}
			set
			{
				this.m_hasPreviousAggregates = value;
			}
		}

		// Token: 0x17001B63 RID: 7011
		// (get) Token: 0x06004100 RID: 16640 RVA: 0x0011105D File Offset: 0x0010F25D
		internal bool HasVariables
		{
			get
			{
				return this.m_variables != null || this.m_groupsWithVariables != null;
			}
		}

		// Token: 0x17001B64 RID: 7012
		// (get) Token: 0x06004101 RID: 16641 RVA: 0x00111072 File Offset: 0x0010F272
		// (set) Token: 0x06004102 RID: 16642 RVA: 0x0011107A File Offset: 0x0010F27A
		internal bool HasLookups
		{
			get
			{
				return this.m_hasLookups;
			}
			set
			{
				this.m_hasLookups = value;
			}
		}

		// Token: 0x17001B65 RID: 7013
		// (get) Token: 0x06004103 RID: 16643 RVA: 0x00111083 File Offset: 0x0010F283
		// (set) Token: 0x06004104 RID: 16644 RVA: 0x0011108B File Offset: 0x0010F28B
		internal ExpressionInfo Language
		{
			get
			{
				return this.m_language;
			}
			set
			{
				this.m_language = value;
			}
		}

		// Token: 0x17001B66 RID: 7014
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x00111094 File Offset: 0x0010F294
		internal ReportExprHost ReportExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001B67 RID: 7015
		// (get) Token: 0x06004106 RID: 16646 RVA: 0x0011109C File Offset: 0x0010F29C
		// (set) Token: 0x06004107 RID: 16647 RVA: 0x001110A4 File Offset: 0x0010F2A4
		internal string DataTransform
		{
			get
			{
				return this.m_dataTransform;
			}
			set
			{
				this.m_dataTransform = value;
			}
		}

		// Token: 0x17001B68 RID: 7016
		// (get) Token: 0x06004108 RID: 16648 RVA: 0x001110AD File Offset: 0x0010F2AD
		// (set) Token: 0x06004109 RID: 16649 RVA: 0x001110B5 File Offset: 0x0010F2B5
		internal string DataSchema
		{
			get
			{
				return this.m_dataSchema;
			}
			set
			{
				this.m_dataSchema = value;
			}
		}

		// Token: 0x17001B69 RID: 7017
		// (get) Token: 0x0600410A RID: 16650 RVA: 0x001110BE File Offset: 0x0010F2BE
		// (set) Token: 0x0600410B RID: 16651 RVA: 0x001110C6 File Offset: 0x0010F2C6
		internal bool DataElementStyleAttribute
		{
			get
			{
				return this.m_dataElementStyleAttribute;
			}
			set
			{
				this.m_dataElementStyleAttribute = value;
			}
		}

		// Token: 0x17001B6A RID: 7018
		// (get) Token: 0x0600410C RID: 16652 RVA: 0x001110CF File Offset: 0x0010F2CF
		// (set) Token: 0x0600410D RID: 16653 RVA: 0x001110D7 File Offset: 0x0010F2D7
		internal string Code
		{
			get
			{
				return this.m_code;
			}
			set
			{
				this.m_code = value;
			}
		}

		// Token: 0x17001B6B RID: 7019
		// (get) Token: 0x0600410E RID: 16654 RVA: 0x001110E0 File Offset: 0x0010F2E0
		// (set) Token: 0x0600410F RID: 16655 RVA: 0x001110E8 File Offset: 0x0010F2E8
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

		// Token: 0x17001B6C RID: 7020
		// (get) Token: 0x06004110 RID: 16656 RVA: 0x001110F1 File Offset: 0x0010F2F1
		// (set) Token: 0x06004111 RID: 16657 RVA: 0x001110F9 File Offset: 0x0010F2F9
		internal bool ReportOrDescendentHasUserSortFilter
		{
			get
			{
				return this.m_reportOrDescendentHasUserSortFilter;
			}
			set
			{
				this.m_reportOrDescendentHasUserSortFilter = value;
			}
		}

		// Token: 0x17001B6D RID: 7021
		// (get) Token: 0x06004112 RID: 16658 RVA: 0x00111102 File Offset: 0x0010F302
		// (set) Token: 0x06004113 RID: 16659 RVA: 0x0011110A File Offset: 0x0010F30A
		internal InScopeSortFilterHashtable NonDetailSortFiltersInScope
		{
			get
			{
				return this.m_nonDetailSortFiltersInScope;
			}
			set
			{
				this.m_nonDetailSortFiltersInScope = value;
			}
		}

		// Token: 0x17001B6E RID: 7022
		// (get) Token: 0x06004114 RID: 16660 RVA: 0x00111113 File Offset: 0x0010F313
		// (set) Token: 0x06004115 RID: 16661 RVA: 0x0011111B File Offset: 0x0010F31B
		internal InScopeSortFilterHashtable DetailSortFiltersInScope
		{
			get
			{
				return this.m_detailSortFiltersInScope;
			}
			set
			{
				this.m_detailSortFiltersInScope = value;
			}
		}

		// Token: 0x17001B6F RID: 7023
		// (get) Token: 0x06004116 RID: 16662 RVA: 0x00111124 File Offset: 0x0010F324
		// (set) Token: 0x06004117 RID: 16663 RVA: 0x0011112C File Offset: 0x0010F32C
		internal int LastAggregateID
		{
			get
			{
				return this.m_lastAggregateID;
			}
			set
			{
				this.m_lastAggregateID = value;
			}
		}

		// Token: 0x17001B70 RID: 7024
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x00111135 File Offset: 0x0010F335
		// (set) Token: 0x06004119 RID: 16665 RVA: 0x0011113D File Offset: 0x0010F33D
		internal int LastLookupID
		{
			get
			{
				return this.m_lastLookupID;
			}
			set
			{
				this.m_lastLookupID = value;
			}
		}

		// Token: 0x17001B71 RID: 7025
		// (get) Token: 0x0600411A RID: 16666 RVA: 0x00111146 File Offset: 0x0010F346
		// (set) Token: 0x0600411B RID: 16667 RVA: 0x0011114E File Offset: 0x0010F34E
		internal List<Variable> Variables
		{
			get
			{
				return this.m_variables;
			}
			set
			{
				this.m_variables = value;
			}
		}

		// Token: 0x17001B72 RID: 7026
		// (get) Token: 0x0600411C RID: 16668 RVA: 0x00111157 File Offset: 0x0010F357
		// (set) Token: 0x0600411D RID: 16669 RVA: 0x0011115F File Offset: 0x0010F35F
		internal bool DeferVariableEvaluation
		{
			get
			{
				return this.m_deferVariableEvaluation;
			}
			set
			{
				this.m_deferVariableEvaluation = value;
			}
		}

		// Token: 0x17001B73 RID: 7027
		// (get) Token: 0x0600411E RID: 16670 RVA: 0x00111168 File Offset: 0x0010F368
		internal bool HasSubReports
		{
			get
			{
				return this.m_subReports != null && this.m_subReports.Count > 0;
			}
		}

		// Token: 0x17001B74 RID: 7028
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x00111182 File Offset: 0x0010F382
		internal Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet> MappingNameToDataSet
		{
			get
			{
				if (this.m_mappingNameToDataSet == null)
				{
					this.GenerateDataSetMappings();
				}
				return this.m_mappingNameToDataSet;
			}
		}

		// Token: 0x17001B75 RID: 7029
		// (get) Token: 0x06004120 RID: 16672 RVA: 0x00111198 File Offset: 0x0010F398
		internal List<int> MappingDataSetIndexToDataSourceIndex
		{
			get
			{
				if (this.m_mappingDataSetIndexToDataSourceIndex == null)
				{
					this.GenerateDataSetMappings();
				}
				return this.m_mappingDataSetIndexToDataSourceIndex;
			}
		}

		// Token: 0x17001B76 RID: 7030
		// (get) Token: 0x06004121 RID: 16673 RVA: 0x001111AE File Offset: 0x0010F3AE
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.DataSet> MappingDataSetIndexToDataSet
		{
			get
			{
				if (this.m_mappingDataSetIndexToDataSet == null)
				{
					this.GenerateDataSetMappings();
				}
				return this.m_mappingDataSetIndexToDataSet;
			}
		}

		// Token: 0x17001B77 RID: 7031
		// (get) Token: 0x06004122 RID: 16674 RVA: 0x001111C4 File Offset: 0x0010F3C4
		// (set) Token: 0x06004123 RID: 16675 RVA: 0x001111CC File Offset: 0x0010F3CC
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion> TopLevelDataRegions
		{
			get
			{
				return this.m_topLevelDataRegions;
			}
			set
			{
				this.m_topLevelDataRegions = value;
			}
		}

		// Token: 0x17001B78 RID: 7032
		// (get) Token: 0x06004124 RID: 16676 RVA: 0x001111D5 File Offset: 0x0010F3D5
		// (set) Token: 0x06004125 RID: 16677 RVA: 0x001111DD File Offset: 0x0010F3DD
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataSet FirstDataSet
		{
			get
			{
				return this.m_firstDataSet;
			}
			set
			{
				this.m_firstDataSet = value;
			}
		}

		// Token: 0x17001B79 RID: 7033
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x001111E6 File Offset: 0x0010F3E6
		internal int FirstDataSetIndexToProcess
		{
			get
			{
				return this.m_firstDataSetIndexToProcess;
			}
		}

		// Token: 0x17001B7A RID: 7034
		// (get) Token: 0x06004127 RID: 16679 RVA: 0x001111EE File Offset: 0x0010F3EE
		internal List<IInScopeEventSource> InScopeEventSources
		{
			get
			{
				return this.m_inScopeEventSources;
			}
		}

		// Token: 0x17001B7B RID: 7035
		// (get) Token: 0x06004128 RID: 16680 RVA: 0x001111F6 File Offset: 0x0010F3F6
		internal List<IInScopeEventSource> EventSources
		{
			get
			{
				return this.m_eventSources;
			}
		}

		// Token: 0x17001B7C RID: 7036
		// (get) Token: 0x06004129 RID: 16681 RVA: 0x001111FE File Offset: 0x0010F3FE
		internal List<ReportHierarchyNode> GroupsWithVariables
		{
			get
			{
				return this.m_groupsWithVariables;
			}
		}

		// Token: 0x17001B7D RID: 7037
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x00111206 File Offset: 0x0010F406
		// (set) Token: 0x0600412B RID: 16683 RVA: 0x0011120E File Offset: 0x0010F40E
		internal List<ReportSection> ReportSections
		{
			get
			{
				return this.m_reportSections;
			}
			set
			{
				Global.Tracer.Assert(this.m_dataShapes == null, " Must not set report sections for a report with data shapes.");
				this.m_reportSections = value;
			}
		}

		// Token: 0x17001B7E RID: 7038
		// (get) Token: 0x0600412C RID: 16684 RVA: 0x0011122F File Offset: 0x0010F42F
		// (set) Token: 0x0600412D RID: 16685 RVA: 0x00111237 File Offset: 0x0010F437
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> DataShapes
		{
			get
			{
				return this.m_dataShapes;
			}
			set
			{
				Global.Tracer.Assert(this.m_reportSections == null, "Must not set data shapes for a report with sections.");
				this.m_dataShapes = value;
			}
		}

		// Token: 0x17001B7F RID: 7039
		// (get) Token: 0x0600412E RID: 16686 RVA: 0x00111258 File Offset: 0x0010F458
		// (set) Token: 0x0600412F RID: 16687 RVA: 0x00111260 File Offset: 0x0010F460
		internal ExpressionInfo InitialPageName
		{
			get
			{
				return this.m_initialPageName;
			}
			set
			{
				this.m_initialPageName = value;
			}
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x0011126C File Offset: 0x0010F46C
		internal override bool Initialize(InitializationContext context)
		{
			context.Location = Microsoft.ReportingServices.ReportPublishing.LocationFlags.None;
			context.ObjectType = this.ObjectType;
			context.ObjectName = null;
			if (this.m_variables != null && this.m_variables.Count != 0)
			{
				context.RegisterVariables(this.m_variables);
				context.ExprHostBuilder.VariableValuesStart();
				for (int i = 0; i < this.m_variables.Count; i++)
				{
					Variable variable = this.m_variables[i];
					variable.Initialize(context);
					context.ExprHostBuilder.VariableValueExpression(variable.Value);
				}
				context.ExprHostBuilder.VariableValuesEnd();
			}
			this.AllocateDatasetDependencyMatrix();
			base.Initialize(context);
			if (this.m_language != null)
			{
				this.m_language.Initialize("Language", context);
				context.ExprHostBuilder.ReportLanguage(this.m_language);
			}
			if (this.m_autoRefreshExpression != null)
			{
				this.m_autoRefreshExpression.Initialize("AutoRefresh", context);
				context.ExprHostBuilder.ReportAutoRefresh(this.m_autoRefreshExpression);
			}
			context.ReportDataElementStyleAttribute = this.m_dataElementStyleAttribute;
			if (this.m_dataSources != null)
			{
				for (int j = 0; j < this.m_dataSources.Count; j++)
				{
					Global.Tracer.Assert(this.m_dataSources[j] != null, "(null != m_dataSources[i])");
					this.m_dataSources[j].Initialize(context);
				}
			}
			this.m_variablesInScope = context.GetCurrentReferencableVariables();
			if (this.m_reportSections != null)
			{
				for (int k = 0; k < this.m_reportSections.Count; k++)
				{
					this.m_reportSections[k].Initialize(context);
				}
			}
			if (this.m_dataShapes != null)
			{
				for (int l = 0; l < this.m_dataShapes.Count; l++)
				{
					this.m_dataShapes[l].Initialize(context);
				}
			}
			if (context.ExprHostBuilder.CustomCode)
			{
				context.ExprHostBuilder.CustomCodeProxyStart();
				if (this.m_codeClasses != null && this.m_codeClasses.Count > 0)
				{
					for (int m = this.m_codeClasses.Count - 1; m >= 0; m--)
					{
						CodeClass codeClass = this.m_codeClasses[m];
						context.EnforceRdlSandboxContentRestrictions(codeClass);
						context.ExprHostBuilder.CustomCodeClassInstance(codeClass.ClassName, codeClass.InstanceName, m);
					}
				}
				if (this.m_code != null && this.m_code.Length > 0)
				{
					context.ExprHostBuilder.ReportCode(this.m_code);
				}
				context.ExprHostBuilder.CustomCodeProxyEnd();
			}
			if (this.m_initialPageName != null)
			{
				this.m_initialPageName.Initialize("InitialPageName", context);
				context.ExprHostBuilder.ReportInitialPageName(this.m_initialPageName);
			}
			if (this.m_variables != null)
			{
				foreach (Variable variable2 in this.m_variables)
				{
					context.UnregisterVariable(variable2);
				}
			}
			if (this.m_dataSources != null)
			{
				foreach (DataSource dataSource in this.m_dataSources)
				{
					dataSource.DetermineDecomposability(context);
				}
			}
			return false;
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x001115C0 File Offset: 0x0010F7C0
		internal void BindAndValidateDataSetDefaultRelationships(ErrorContext errorContext)
		{
			foreach (Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet in this.MappingDataSetIndexToDataSet)
			{
				if (dataSet != null)
				{
					dataSet.BindAndValidateDefaultRelationships(errorContext, this);
				}
			}
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x00111618 File Offset: 0x0010F818
		internal ScopeTree BuildScopeTree()
		{
			return ScopeTreeBuilder.BuildScopeTree(this);
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x00111620 File Offset: 0x0010F820
		internal override void TraverseScopes(IRIFScopeVisitor visitor)
		{
			if (this.m_reportSections != null)
			{
				foreach (ReportSection reportSection in this.m_reportSections)
				{
					reportSection.TraverseScopes(visitor);
				}
			}
			if (this.m_dataShapes != null)
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem in this.m_dataShapes)
				{
					((DataShape)reportItem).TraverseScopes(visitor);
				}
			}
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x001116C8 File Offset: 0x0010F8C8
		internal void UpdateTopLeftDataRegion(InitializationContext context, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion)
		{
			if (this.m_topLeftDataRegion == null || this.m_topLeftDataRegionAbsTop > context.CurrentAbsoluteTop || (0.0 == Math.Round(this.m_topLeftDataRegionAbsTop - context.CurrentAbsoluteTop, 10) && this.m_topLeftDataRegionAbsLeft > context.CurrentAbsoluteLeft))
			{
				this.m_topLeftDataRegion = dataRegion;
				this.m_topLeftDataRegionAbsTop = context.CurrentAbsoluteTop;
				this.m_topLeftDataRegionAbsLeft = context.CurrentAbsoluteLeft;
			}
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x0011173C File Offset: 0x0010F93C
		bool IRIFReportScope.TextboxInScope(int sequenceIndex)
		{
			return false;
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x0011173F File Offset: 0x0010F93F
		void IRIFReportScope.AddInScopeTextBox(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textbox)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x0011174C File Offset: 0x0010F94C
		void IRIFReportScope.ResetTextBoxImpls(OnDemandProcessingContext context)
		{
		}

		// Token: 0x17001B80 RID: 7040
		// (get) Token: 0x06004138 RID: 16696 RVA: 0x0011174E File Offset: 0x0010F94E
		// (set) Token: 0x06004139 RID: 16697 RVA: 0x00111751 File Offset: 0x0010F951
		bool IRIFReportScope.NeedToCacheDataRows
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x00111753 File Offset: 0x0010F953
		bool IRIFReportScope.VariableInScope(int sequenceIndex)
		{
			return SequenceIndex.GetBit(this.m_variablesInScope, sequenceIndex, true);
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x00111762 File Offset: 0x0010F962
		void IRIFReportScope.AddInScopeEventSource(IInScopeEventSource eventSource)
		{
			if (this.m_inScopeEventSources == null)
			{
				this.m_inScopeEventSources = new List<IInScopeEventSource>();
			}
			this.m_inScopeEventSources.Add(eventSource);
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x00111783 File Offset: 0x0010F983
		internal void AddEventSource(IInScopeEventSource eventSource)
		{
			if (this.m_eventSources == null)
			{
				this.m_eventSources = new List<IInScopeEventSource>();
			}
			this.m_eventSources.Add(eventSource);
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x001117A4 File Offset: 0x0010F9A4
		internal void AddGroupWithVariables(ReportHierarchyNode node)
		{
			if (this.m_groupsWithVariables == null)
			{
				this.m_groupsWithVariables = new List<ReportHierarchyNode>();
			}
			this.m_groupsWithVariables.Add(node);
		}

		// Token: 0x17001B81 RID: 7041
		// (get) Token: 0x0600413E RID: 16702 RVA: 0x001117C5 File Offset: 0x0010F9C5
		Microsoft.ReportingServices.ReportProcessing.ObjectType IExpressionHostAssemblyHolder.ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Report;
			}
		}

		// Token: 0x17001B82 RID: 7042
		// (get) Token: 0x0600413F RID: 16703 RVA: 0x001117C8 File Offset: 0x0010F9C8
		string IExpressionHostAssemblyHolder.ExprHostAssemblyName
		{
			get
			{
				return "expression_host_" + this.m_reportVersion.ToString().Replace("-", "");
			}
		}

		// Token: 0x17001B83 RID: 7043
		// (get) Token: 0x06004140 RID: 16704 RVA: 0x001117F4 File Offset: 0x0010F9F4
		// (set) Token: 0x06004141 RID: 16705 RVA: 0x001117FC File Offset: 0x0010F9FC
		List<string> IExpressionHostAssemblyHolder.CodeModules
		{
			get
			{
				return this.m_codeModules;
			}
			set
			{
				this.m_codeModules = value;
			}
		}

		// Token: 0x17001B84 RID: 7044
		// (get) Token: 0x06004142 RID: 16706 RVA: 0x00111805 File Offset: 0x0010FA05
		// (set) Token: 0x06004143 RID: 16707 RVA: 0x0011180D File Offset: 0x0010FA0D
		List<CodeClass> IExpressionHostAssemblyHolder.CodeClasses
		{
			get
			{
				return this.m_codeClasses;
			}
			set
			{
				this.m_codeClasses = value;
			}
		}

		// Token: 0x17001B85 RID: 7045
		// (get) Token: 0x06004144 RID: 16708 RVA: 0x00111816 File Offset: 0x0010FA16
		// (set) Token: 0x06004145 RID: 16709 RVA: 0x0011181E File Offset: 0x0010FA1E
		byte[] IExpressionHostAssemblyHolder.CompiledCode
		{
			get
			{
				return this.m_exprCompiledCode;
			}
			set
			{
				this.m_exprCompiledCode = value;
			}
		}

		// Token: 0x17001B86 RID: 7046
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x00111827 File Offset: 0x0010FA27
		// (set) Token: 0x06004147 RID: 16711 RVA: 0x0011182F File Offset: 0x0010FA2F
		bool IExpressionHostAssemblyHolder.CompiledCodeGeneratedWithRefusedPermissions
		{
			get
			{
				return this.m_exprCompiledCodeGeneratedWithRefusedPermissions;
			}
			set
			{
				this.m_exprCompiledCodeGeneratedWithRefusedPermissions = value;
			}
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x00111838 File Offset: 0x0010FA38
		private void AllocateDatasetDependencyMatrix()
		{
			if (this.MappingNameToDataSet != null)
			{
				int dataSetCount = this.DataSetCount;
				if (dataSetCount < 10000)
				{
					this.m_flattenedDatasetDependencyMatrix = new byte[(int)Math.Ceiling((double)(dataSetCount * dataSetCount) / 8.0)];
				}
			}
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x0011187C File Offset: 0x0010FA7C
		private void CalculateOffsetAndMask(int datasetIndex, int referencedDatasetIndex, out int byteOffset, out byte bitMask)
		{
			int dataSetCount = this.DataSetCount;
			byteOffset = dataSetCount * datasetIndex + referencedDatasetIndex;
			byte b = (byte)(byteOffset % 8);
			bitMask = (byte)(SequenceIndex.BitMask001 << (int)b);
			byteOffset >>= 3;
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x001118B4 File Offset: 0x0010FAB4
		internal void SetDatasetDependency(int datasetIndex, int referencedDatasetIndex, bool clearDependency)
		{
			if (this.m_flattenedDatasetDependencyMatrix == null)
			{
				return;
			}
			int num;
			byte b;
			this.CalculateOffsetAndMask(datasetIndex, referencedDatasetIndex, out num, out b);
			if (clearDependency)
			{
				b ^= SequenceIndex.BitMask255;
				byte[] flattenedDatasetDependencyMatrix = this.m_flattenedDatasetDependencyMatrix;
				int num2 = num;
				flattenedDatasetDependencyMatrix[num2] &= b;
				return;
			}
			byte[] flattenedDatasetDependencyMatrix2 = this.m_flattenedDatasetDependencyMatrix;
			int num3 = num;
			flattenedDatasetDependencyMatrix2[num3] |= b;
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x00111908 File Offset: 0x0010FB08
		internal bool HasDatasetDependency(int datasetIndex, int referencedDatasetIndex)
		{
			if (this.m_flattenedDatasetDependencyMatrix == null)
			{
				return false;
			}
			int num;
			byte b;
			this.CalculateOffsetAndMask(datasetIndex, referencedDatasetIndex, out num, out b);
			return (this.m_flattenedDatasetDependencyMatrix[num] & b) > 0;
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x00111938 File Offset: 0x0010FB38
		internal void ClearDatasetParameterOnlyDependencies(int datasetIndex)
		{
			if (this.m_flattenedDatasetDependencyMatrix == null)
			{
				return;
			}
			int dataSetCount = this.DataSetCount;
			for (int i = 0; i < dataSetCount; i++)
			{
				this.SetDatasetDependency(i, datasetIndex, true);
				this.SetDatasetDependency(datasetIndex, i, true);
			}
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x00111974 File Offset: 0x0010FB74
		internal int CalculateDatasetRootIndex(int suggestedRootIndex, bool[] exclusionList, int unprocessedDataSetCount)
		{
			if (this.m_flattenedDatasetDependencyMatrix == null)
			{
				return suggestedRootIndex;
			}
			int dataSetCount = this.DataSetCount;
			if (exclusionList == null)
			{
				exclusionList = new bool[dataSetCount];
				unprocessedDataSetCount = dataSetCount;
			}
			if (!exclusionList[suggestedRootIndex])
			{
				exclusionList[suggestedRootIndex] = true;
				unprocessedDataSetCount--;
			}
			int num = -1;
			while (++num < dataSetCount && unprocessedDataSetCount > 0)
			{
				if (!exclusionList[num] && this.HasDatasetDependency(suggestedRootIndex, num))
				{
					suggestedRootIndex = num;
					exclusionList[num] = true;
					unprocessedDataSetCount--;
					num = -1;
				}
			}
			return suggestedRootIndex;
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x001119DC File Offset: 0x0010FBDC
		internal void Phase4_DetermineFirstDatasetToProcess()
		{
			if (this.m_topLeftDataRegion == null)
			{
				this.m_firstDataSetIndexToProcess = 0;
				return;
			}
			int indexInCollection = this.m_topLeftDataRegion.GetDataSet(this).IndexInCollection;
			this.m_firstDataSetIndexToProcess = this.CalculateDatasetRootIndex(indexInCollection, null, -1);
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x00111A1C File Offset: 0x0010FC1C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Report, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ReportVersion, Token.Guid),
				new MemberInfo(MemberName.Author, Token.String),
				new MemberInfo(MemberName.AutoRefresh, Token.Int32),
				new MemberInfo(MemberName.EmbeddedImages, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringRIFObjectDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ImageInfo),
				new ReadOnlyMemberInfo(MemberName.Page, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Page),
				new ReadOnlyMemberInfo(MemberName.ReportItems, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemCollection),
				new MemberInfo(MemberName.DataSources, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSource),
				new ReadOnlyMemberInfo(MemberName.PageAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo),
				new MemberInfo(MemberName.CompiledCode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.MergeOnePass, Token.Boolean),
				new ReadOnlyMemberInfo(MemberName.PageMergeOnePass, Token.Boolean),
				new MemberInfo(MemberName.SubReportMergeTransactions, Token.Boolean),
				new MemberInfo(MemberName.NeedPostGroupProcessing, Token.Boolean),
				new MemberInfo(MemberName.HasPostSortAggregates, Token.Boolean),
				new MemberInfo(MemberName.HasReportItemReferences, Token.Boolean),
				new MemberInfo(MemberName.ShowHideType, Token.Enum),
				new MemberInfo(MemberName.LastID, Token.Int32),
				new MemberInfo(MemberName.SubReports, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReport),
				new MemberInfo(MemberName.HasImageStreams, Token.Boolean),
				new MemberInfo(MemberName.HasLabels, Token.Boolean),
				new MemberInfo(MemberName.HasBookmarks, Token.Boolean),
				new MemberInfo(MemberName.ParametersNotUsedInQuery, Token.Boolean),
				new MemberInfo(MemberName.Parameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterDef),
				new MemberInfo(MemberName.DataSetName, Token.String),
				new MemberInfo(MemberName.CodeModules, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String),
				new MemberInfo(MemberName.CodeClasses, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CodeClass),
				new MemberInfo(MemberName.HasSpecialRecursiveAggregates, Token.Boolean),
				new MemberInfo(MemberName.Language, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DataTransform, Token.String),
				new MemberInfo(MemberName.DataSchema, Token.String),
				new MemberInfo(MemberName.DataElementStyleAttribute, Token.Boolean),
				new MemberInfo(MemberName.Code, Token.String),
				new MemberInfo(MemberName.HasUserSortFilter, Token.Boolean),
				new MemberInfo(MemberName.CompiledCodeGeneratedWithRefusedPermissions, Token.Boolean),
				new MemberInfo(MemberName.NonDetailSortFiltersInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Int32PrimitiveListHashtable),
				new MemberInfo(MemberName.DetailSortFiltersInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Int32PrimitiveListHashtable),
				new MemberInfo(MemberName.Variables, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Variable),
				new MemberInfo(MemberName.DeferVariableEvaluation, Token.Boolean),
				new MemberInfo(MemberName.DataRegions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion),
				new MemberInfo(MemberName.FirstDataSet, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSet, Token.Reference),
				new MemberInfo(MemberName.TopLeftDataRegion, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion, Token.Reference),
				new MemberInfo(MemberName.DataSetsNotOnlyUsedInParameters, Token.Int32),
				new MemberInfo(MemberName.HasPreviousAggregates, Token.Boolean),
				new ReadOnlyMemberInfo(MemberName.InScopeTextBoxesInBody, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextBox),
				new ReadOnlyMemberInfo(MemberName.InScopeTextBoxesInPage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextBox),
				new MemberInfo(MemberName.InScopeEventSources, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IInScopeEventSource),
				new MemberInfo(MemberName.EventSources, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IInScopeEventSource),
				new MemberInfo(MemberName.GroupsWithVariables, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportHierarchyNode),
				new MemberInfo(MemberName.ConsumeContainerWhitespace, Token.Boolean),
				new MemberInfo(MemberName.FlattenedDatasetDependencyMatrix, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.FirstDataSetIndexToProcess, Token.Int32),
				new ReadOnlyMemberInfo(MemberName.TextboxesInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.VariablesInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.HasLookups, Token.Boolean),
				new MemberInfo(MemberName.ReportSections, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportSection),
				new MemberInfo(MemberName.HasHeadersOrFooters, Token.Boolean),
				new MemberInfo(MemberName.AutoRefreshExpression, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.InitialPageName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.HasAggregatesOfAggregates, Token.Boolean),
				new MemberInfo(MemberName.HasAggregatesOfAggregatesInUserSort, Token.Boolean),
				new MemberInfo(MemberName.SharedDSContainerCollectionIndex, Token.Int32),
				new MemberInfo(MemberName.DataPipelineCount, Token.Int32),
				new MemberInfo(MemberName.DefaultFontFamily, Token.String)
			});
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x00111F70 File Offset: 0x00110170
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Report.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.DeferVariableEvaluation)
				{
					if (memberName <= MemberName.ParametersNotUsedInQuery)
					{
						if (memberName <= MemberName.LastID)
						{
							if (memberName <= MemberName.Language)
							{
								if (memberName == MemberName.Variables)
								{
									writer.Write<Variable>(this.m_variables);
									continue;
								}
								if (memberName == MemberName.Language)
								{
									writer.Write(this.m_language);
									continue;
								}
							}
							else
							{
								if (memberName == MemberName.Parameters)
								{
									writer.Write<ParameterDef>(this.m_parameters);
									continue;
								}
								if (memberName == MemberName.LastID)
								{
									writer.Write(this.m_lastID);
									continue;
								}
							}
						}
						else if (memberName <= MemberName.ShowHideType)
						{
							switch (memberName)
							{
							case MemberName.Author:
								writer.Write(this.m_author);
								continue;
							case MemberName.AutoRefresh:
								writer.Write(this.m_autoRefresh);
								continue;
							case MemberName.EmbeddedImages:
								writer.WriteStringRIFObjectDictionary<ImageInfo>(this.m_embeddedImages);
								continue;
							case MemberName.PageHeader:
							case MemberName.PageFooter:
							case MemberName.ReportItems:
								break;
							case MemberName.DataSources:
								writer.Write<DataSource>(this.m_dataSources);
								continue;
							default:
								switch (memberName)
								{
								case MemberName.CodeModules:
									writer.WriteListOfPrimitives<string>(this.m_codeModules);
									continue;
								case MemberName.CodeClasses:
									writer.Write<CodeClass>(this.m_codeClasses);
									continue;
								case MemberName.CompiledCode:
									writer.Write(this.m_exprCompiledCode);
									continue;
								case MemberName.MergeOnePass:
									writer.Write(this.m_mergeOnePass);
									continue;
								case MemberName.SubReportMergeTransactions:
									writer.Write(this.m_subReportMergeTransactions);
									continue;
								case MemberName.NeedPostGroupProcessing:
									writer.Write(this.m_needPostGroupProcessing);
									continue;
								case MemberName.HasPostSortAggregates:
									writer.Write(this.m_hasPostSortAggregates);
									continue;
								case MemberName.HasReportItemReferences:
									writer.Write(this.m_hasReportItemReferences);
									continue;
								case MemberName.ShowHideType:
									writer.WriteEnum((int)this.m_showHideType);
									continue;
								}
								break;
							}
						}
						else
						{
							if (memberName == MemberName.DataSetName)
							{
								writer.Write(this.m_oneDataSetName);
								continue;
							}
							if (memberName == MemberName.DataRegions)
							{
								writer.WriteListOfReferences(this.m_topLevelDataRegions);
								continue;
							}
							switch (memberName)
							{
							case MemberName.SubReports:
								writer.WriteListOfReferences(this.m_subReports);
								continue;
							case MemberName.HasImageStreams:
								writer.Write(this.m_hasImageStreams);
								continue;
							case MemberName.HasBookmarks:
								writer.Write(this.m_hasBookmarks);
								continue;
							case MemberName.HasLabels:
								writer.Write(this.m_hasLabels);
								continue;
							case MemberName.ParametersNotUsedInQuery:
								writer.Write(this.m_parametersNotUsedInQuery);
								continue;
							}
						}
					}
					else if (memberName <= MemberName.ReportVersion)
					{
						if (memberName <= MemberName.HasSpecialRecursiveAggregates)
						{
							if (memberName == MemberName.Code)
							{
								writer.Write(this.m_code);
								continue;
							}
							if (memberName == MemberName.HasSpecialRecursiveAggregates)
							{
								writer.Write(this.m_hasSpecialRecursiveAggregates);
								continue;
							}
						}
						else
						{
							switch (memberName)
							{
							case MemberName.DataTransform:
								writer.Write(this.m_dataTransform);
								continue;
							case MemberName.DataSchema:
								writer.Write(this.m_dataSchema);
								continue;
							case MemberName.DataElementName:
								break;
							case MemberName.DataElementStyleAttribute:
								writer.Write(this.m_dataElementStyleAttribute);
								continue;
							default:
								if (memberName == MemberName.ReportVersion)
								{
									writer.Write(this.m_reportVersion);
									continue;
								}
								break;
							}
						}
					}
					else if (memberName <= MemberName.CompiledCodeGeneratedWithRefusedPermissions)
					{
						if (memberName == MemberName.HasUserSortFilter)
						{
							writer.Write(this.m_hasUserSortFilter);
							continue;
						}
						if (memberName == MemberName.CompiledCodeGeneratedWithRefusedPermissions)
						{
							writer.Write(this.m_exprCompiledCodeGeneratedWithRefusedPermissions);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.NonDetailSortFiltersInScope)
						{
							writer.WriteInt32PrimitiveListHashtable<int>(this.m_nonDetailSortFiltersInScope);
							continue;
						}
						if (memberName == MemberName.DetailSortFiltersInScope)
						{
							writer.WriteInt32PrimitiveListHashtable<int>(this.m_detailSortFiltersInScope);
							continue;
						}
						if (memberName == MemberName.DeferVariableEvaluation)
						{
							writer.Write(this.m_deferVariableEvaluation);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.FirstDataSetIndexToProcess)
				{
					if (memberName <= MemberName.HasPreviousAggregates)
					{
						if (memberName <= MemberName.TopLeftDataRegion)
						{
							if (memberName == MemberName.FirstDataSet)
							{
								writer.WriteReference(this.m_firstDataSet);
								continue;
							}
							if (memberName == MemberName.TopLeftDataRegion)
							{
								writer.WriteReference(this.m_topLeftDataRegion);
								continue;
							}
						}
						else
						{
							if (memberName == MemberName.DataSetsNotOnlyUsedInParameters)
							{
								writer.Write(this.m_dataSetsNotOnlyUsedInParameters);
								continue;
							}
							if (memberName == MemberName.HasPreviousAggregates)
							{
								writer.Write(this.m_hasPreviousAggregates);
								continue;
							}
						}
					}
					else if (memberName <= MemberName.EventSources)
					{
						if (memberName == MemberName.InScopeEventSources)
						{
							writer.WriteListOfReferences(this.m_inScopeEventSources);
							continue;
						}
						if (memberName == MemberName.EventSources)
						{
							writer.WriteListOfReferences(this.m_eventSources);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.GroupsWithVariables)
						{
							writer.WriteListOfReferences(this.m_groupsWithVariables);
							continue;
						}
						if (memberName == MemberName.ConsumeContainerWhitespace)
						{
							writer.Write(this.m_consumeContainerWhitespace);
							continue;
						}
						switch (memberName)
						{
						case MemberName.VariablesInScope:
							writer.Write(this.m_variablesInScope);
							continue;
						case MemberName.FlattenedDatasetDependencyMatrix:
							writer.Write(this.m_flattenedDatasetDependencyMatrix);
							continue;
						case MemberName.FirstDataSetIndexToProcess:
							writer.Write(this.m_firstDataSetIndexToProcess);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.InitialPageName)
				{
					if (memberName <= MemberName.ReportSections)
					{
						if (memberName == MemberName.HasLookups)
						{
							writer.Write(this.m_hasLookups);
							continue;
						}
						if (memberName == MemberName.ReportSections)
						{
							writer.Write<ReportSection>(this.m_reportSections);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.HasHeadersOrFooters)
						{
							writer.Write(this.m_hasHeadersOrFooters);
							continue;
						}
						if (memberName == MemberName.AutoRefreshExpression)
						{
							writer.Write(this.m_autoRefreshExpression);
							continue;
						}
						if (memberName == MemberName.InitialPageName)
						{
							writer.Write(this.m_initialPageName);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.HasAggregatesOfAggregatesInUserSort)
				{
					if (memberName == MemberName.HasAggregatesOfAggregates)
					{
						writer.Write(this.m_hasAggregatesOfAggregates);
						continue;
					}
					if (memberName == MemberName.HasAggregatesOfAggregatesInUserSort)
					{
						writer.Write(this.m_hasAggregatesOfAggregatesInUserSort);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.SharedDSContainerCollectionIndex)
					{
						writer.Write(this.m_sharedDSContainerCollectionIndex);
						continue;
					}
					if (memberName == MemberName.DataPipelineCount)
					{
						writer.Write(this.m_dataPipelineCount);
						continue;
					}
					if (memberName == MemberName.DefaultFontFamily)
					{
						writer.Write(this.m_defaultFontFamily);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x00112674 File Offset: 0x00110874
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Report.m_Declaration);
			ReportSection reportSection = null;
			byte[] array = null;
			List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox> list = null;
			List<DataAggregateInfo> list2 = null;
			if (reader.IntermediateFormatVersion.CompareTo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatVersion.BIRefresh) < 0)
			{
				reportSection = new ReportSection(0);
				reportSection.Name = "ReportSection0";
				reportSection.Width = base.Width;
				reportSection.WidthValue = base.WidthValue;
				reportSection.DataElementName = reportSection.DataElementNameDefault;
				reportSection.DataElementOutput = reportSection.DataElementOutputDefault;
				reportSection.ExprHostID = 0;
				reportSection.ParentInstancePath = this;
				reportSection.Height = base.Height;
				reportSection.HeightValue = base.HeightValue;
				reportSection.StyleClass = base.StyleClass;
				this.m_reportSections = new List<ReportSection>();
				this.m_reportSections.Add(reportSection);
			}
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.DeferVariableEvaluation)
				{
					if (memberName <= MemberName.ParametersNotUsedInQuery)
					{
						if (memberName <= MemberName.LastID)
						{
							if (memberName <= MemberName.Language)
							{
								if (memberName == MemberName.Variables)
								{
									this.m_variables = reader.ReadGenericListOfRIFObjects<Variable>();
									continue;
								}
								if (memberName == MemberName.Language)
								{
									this.m_language = (ExpressionInfo)reader.ReadRIFObject();
									continue;
								}
							}
							else
							{
								if (memberName == MemberName.Parameters)
								{
									this.m_parameters = reader.ReadGenericListOfRIFObjects<ParameterDef>();
									continue;
								}
								if (memberName == MemberName.LastID)
								{
									this.m_lastID = reader.ReadInt32();
									continue;
								}
							}
						}
						else if (memberName <= MemberName.ShowHideType)
						{
							switch (memberName)
							{
							case MemberName.Author:
								this.m_author = reader.ReadString();
								continue;
							case MemberName.AutoRefresh:
								this.m_autoRefresh = reader.ReadInt32();
								continue;
							case MemberName.EmbeddedImages:
								this.m_embeddedImages = reader.ReadStringRIFObjectDictionary<ImageInfo>();
								continue;
							case MemberName.PageHeader:
							case MemberName.PageFooter:
								break;
							case MemberName.ReportItems:
								reportSection.ReportItems = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection)reader.ReadRIFObject();
								continue;
							case MemberName.DataSources:
								this.m_dataSources = reader.ReadGenericListOfRIFObjects<DataSource>();
								continue;
							default:
								switch (memberName)
								{
								case MemberName.CodeModules:
									this.m_codeModules = reader.ReadListOfPrimitives<string>();
									continue;
								case MemberName.CodeClasses:
									this.m_codeClasses = reader.ReadGenericListOfRIFObjects<CodeClass>();
									continue;
								case MemberName.PageAggregates:
									list2 = reader.ReadGenericListOfRIFObjects<DataAggregateInfo>();
									continue;
								case MemberName.CompiledCode:
									this.m_exprCompiledCode = reader.ReadByteArray();
									continue;
								case MemberName.MergeOnePass:
									this.m_mergeOnePass = reader.ReadBoolean();
									continue;
								case MemberName.PageMergeOnePass:
									reportSection.NeedsReportItemsOnPage |= !reader.ReadBoolean();
									continue;
								case MemberName.SubReportMergeTransactions:
									this.m_subReportMergeTransactions = reader.ReadBoolean();
									continue;
								case MemberName.NeedPostGroupProcessing:
									this.m_needPostGroupProcessing = reader.ReadBoolean();
									continue;
								case MemberName.HasPostSortAggregates:
									this.m_hasPostSortAggregates = reader.ReadBoolean();
									continue;
								case MemberName.HasReportItemReferences:
									this.m_hasReportItemReferences = reader.ReadBoolean();
									continue;
								case MemberName.ShowHideType:
									this.m_showHideType = (Microsoft.ReportingServices.ReportIntermediateFormat.Report.ShowHideTypes)reader.ReadEnum();
									continue;
								}
								break;
							}
						}
						else
						{
							if (memberName == MemberName.DataSetName)
							{
								this.m_oneDataSetName = reader.ReadString();
								continue;
							}
							if (memberName == MemberName.DataRegions)
							{
								this.m_topLevelDataRegions = reader.ReadGenericListOfReferences<Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion>(this);
								continue;
							}
							switch (memberName)
							{
							case MemberName.SubReports:
								this.m_subReports = reader.ReadGenericListOfReferences<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport>(this);
								continue;
							case MemberName.HasImageStreams:
								this.m_hasImageStreams = reader.ReadBoolean();
								continue;
							case MemberName.HasBookmarks:
								this.m_hasBookmarks = reader.ReadBoolean();
								continue;
							case MemberName.HasLabels:
								this.m_hasLabels = reader.ReadBoolean();
								continue;
							case MemberName.ParametersNotUsedInQuery:
								this.m_parametersNotUsedInQuery = reader.ReadBoolean();
								continue;
							}
						}
					}
					else if (memberName <= MemberName.ReportVersion)
					{
						if (memberName <= MemberName.HasSpecialRecursiveAggregates)
						{
							if (memberName == MemberName.Code)
							{
								this.m_code = reader.ReadString();
								continue;
							}
							if (memberName == MemberName.HasSpecialRecursiveAggregates)
							{
								this.m_hasSpecialRecursiveAggregates = reader.ReadBoolean();
								continue;
							}
						}
						else
						{
							switch (memberName)
							{
							case MemberName.DataTransform:
								this.m_dataTransform = reader.ReadString();
								continue;
							case MemberName.DataSchema:
								this.m_dataSchema = reader.ReadString();
								continue;
							case MemberName.DataElementName:
								break;
							case MemberName.DataElementStyleAttribute:
								this.m_dataElementStyleAttribute = reader.ReadBoolean();
								continue;
							default:
								if (memberName != MemberName.Page)
								{
									if (memberName == MemberName.ReportVersion)
									{
										this.m_reportVersion = reader.ReadGuid();
										continue;
									}
								}
								else
								{
									Page page = (Page)reader.ReadRIFObject();
									reportSection.Page = page;
									reportSection.Page.ExprHostID = 0;
									bool flag = page.UpgradedSnapshotPageHeaderEvaluation || page.UpgradedSnapshotPageFooterEvaluation;
									reportSection.NeedsOverallTotalPages = flag;
									reportSection.NeedsReportItemsOnPage = flag;
									if (page.PageHeader != null || page.PageFooter != null)
									{
										this.m_hasHeadersOrFooters = true;
										continue;
									}
									continue;
								}
								break;
							}
						}
					}
					else if (memberName <= MemberName.CompiledCodeGeneratedWithRefusedPermissions)
					{
						if (memberName == MemberName.HasUserSortFilter)
						{
							this.m_hasUserSortFilter = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.CompiledCodeGeneratedWithRefusedPermissions)
						{
							this.m_exprCompiledCodeGeneratedWithRefusedPermissions = reader.ReadBoolean();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.NonDetailSortFiltersInScope)
						{
							this.m_nonDetailSortFiltersInScope = reader.ReadInt32PrimitiveListHashtable<InScopeSortFilterHashtable, int>();
							continue;
						}
						if (memberName == MemberName.DetailSortFiltersInScope)
						{
							this.m_detailSortFiltersInScope = reader.ReadInt32PrimitiveListHashtable<InScopeSortFilterHashtable, int>();
							continue;
						}
						if (memberName == MemberName.DeferVariableEvaluation)
						{
							this.m_deferVariableEvaluation = reader.ReadBoolean();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.TextboxesInScope)
				{
					if (memberName <= MemberName.HasPreviousAggregates)
					{
						if (memberName <= MemberName.TopLeftDataRegion)
						{
							if (memberName == MemberName.FirstDataSet)
							{
								this.m_firstDataSet = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.DataSet>(this);
								continue;
							}
							if (memberName == MemberName.TopLeftDataRegion)
							{
								this.m_topLeftDataRegion = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion>(this);
								continue;
							}
						}
						else
						{
							if (memberName == MemberName.DataSetsNotOnlyUsedInParameters)
							{
								this.m_dataSetsNotOnlyUsedInParameters = reader.ReadInt32();
								continue;
							}
							if (memberName == MemberName.HasPreviousAggregates)
							{
								this.m_hasPreviousAggregates = reader.ReadBoolean();
								continue;
							}
						}
					}
					else if (memberName <= MemberName.EventSources)
					{
						switch (memberName)
						{
						case MemberName.InScopeEventSources:
							this.m_inScopeEventSources = reader.ReadGenericListOfReferences<IInScopeEventSource>(this);
							continue;
						case MemberName.InScopeTextBoxesInPage:
							list = reader.ReadGenericListOfReferences<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>(this);
							continue;
						case MemberName.InScopeTextBoxesInBody:
							reportSection.SetInScopeTextBoxes(reader.ReadGenericListOfReferences<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>(this));
							continue;
						default:
							if (memberName == MemberName.EventSources)
							{
								this.m_eventSources = reader.ReadGenericListOfReferences<IInScopeEventSource>(this);
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.GroupsWithVariables)
						{
							this.m_groupsWithVariables = reader.ReadGenericListOfReferences<ReportHierarchyNode>(this);
							continue;
						}
						if (memberName == MemberName.ConsumeContainerWhitespace)
						{
							this.m_consumeContainerWhitespace = reader.ReadBoolean();
							continue;
						}
						switch (memberName)
						{
						case MemberName.VariablesInScope:
							this.m_variablesInScope = reader.ReadByteArray();
							continue;
						case MemberName.FlattenedDatasetDependencyMatrix:
							this.m_flattenedDatasetDependencyMatrix = reader.ReadByteArray();
							continue;
						case MemberName.FirstDataSetIndexToProcess:
							this.m_firstDataSetIndexToProcess = reader.ReadInt32();
							continue;
						case MemberName.TextboxesInScope:
						{
							byte[] array2 = reader.ReadByteArray();
							reportSection.SetTextboxesInScope(array2);
							array = null;
							continue;
						}
						}
					}
				}
				else if (memberName <= MemberName.InitialPageName)
				{
					if (memberName <= MemberName.ReportSections)
					{
						if (memberName == MemberName.HasLookups)
						{
							this.m_hasLookups = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.ReportSections)
						{
							this.m_reportSections = reader.ReadGenericListOfRIFObjects<ReportSection>();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.HasHeadersOrFooters)
						{
							this.m_hasHeadersOrFooters = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.AutoRefreshExpression)
						{
							this.m_autoRefreshExpression = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.InitialPageName)
						{
							this.m_initialPageName = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.HasAggregatesOfAggregatesInUserSort)
				{
					if (memberName == MemberName.HasAggregatesOfAggregates)
					{
						this.m_hasAggregatesOfAggregates = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.HasAggregatesOfAggregatesInUserSort)
					{
						this.m_hasAggregatesOfAggregatesInUserSort = reader.ReadBoolean();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.SharedDSContainerCollectionIndex)
					{
						this.m_sharedDSContainerCollectionIndex = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.DataPipelineCount)
					{
						this.m_dataPipelineCount = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.DefaultFontFamily)
					{
						this.m_defaultFontFamily = reader.ReadString();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
			if (reportSection != null)
			{
				IDOwner idowner = reportSection;
				int num = this.m_lastID + 1;
				this.m_lastID = num;
				idowner.ID = num;
				reportSection.GlobalID = reportSection.ReportItems.GlobalID * -1;
				reportSection.Page.SetTextboxesInScope(array);
				reportSection.Page.SetInScopeTextBoxes(list);
				reportSection.Page.PageAggregates = list2;
			}
			if (this.m_name == null)
			{
				this.m_name = "Report";
			}
			reader.ResolveReferences();
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x00112FF4 File Offset: 0x001111F4
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Microsoft.ReportingServices.ReportIntermediateFormat.Report.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName <= MemberName.FirstDataSet)
					{
						if (memberName == MemberName.DataRegions)
						{
							if (this.m_topLevelDataRegions == null)
							{
								this.m_topLevelDataRegions = new List<Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion>();
							}
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable);
							Global.Tracer.Assert(referenceable != null && ((Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem)referenceable).IsDataRegion && !this.m_topLevelDataRegions.Contains((Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)referenceable));
							this.m_topLevelDataRegions.Add((Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)referenceable);
							continue;
						}
						if (memberName == MemberName.SubReports)
						{
							if (this.m_subReports == null)
							{
								this.m_subReports = new List<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport>();
							}
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable2;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable2);
							Global.Tracer.Assert(referenceable2 != null && referenceable2 is Microsoft.ReportingServices.ReportIntermediateFormat.SubReport && !this.m_subReports.Contains((Microsoft.ReportingServices.ReportIntermediateFormat.SubReport)referenceable2));
							this.m_subReports.Add((Microsoft.ReportingServices.ReportIntermediateFormat.SubReport)referenceable2);
							continue;
						}
						if (memberName == MemberName.FirstDataSet)
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable3;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable3);
							Global.Tracer.Assert(referenceable3 != null && referenceable3 is Microsoft.ReportingServices.ReportIntermediateFormat.DataSet);
							this.m_firstDataSet = (Microsoft.ReportingServices.ReportIntermediateFormat.DataSet)referenceable3;
							continue;
						}
					}
					else if (memberName <= MemberName.InScopeTextBoxesInBody)
					{
						if (memberName == MemberName.TopLeftDataRegion)
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable4;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable4);
							Global.Tracer.Assert(referenceable4 != null && ((Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem)referenceable4).IsDataRegion);
							this.m_topLeftDataRegion = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)referenceable4;
							continue;
						}
						switch (memberName)
						{
						case MemberName.InScopeEventSources:
						{
							if (this.m_inScopeEventSources == null)
							{
								this.m_inScopeEventSources = new List<IInScopeEventSource>();
							}
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable5;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable5);
							IInScopeEventSource inScopeEventSource = (IInScopeEventSource)referenceable5;
							this.m_inScopeEventSources.Add(inScopeEventSource);
							continue;
						}
						case MemberName.InScopeTextBoxesInPage:
						{
							Global.Tracer.Assert(this.m_reportSections != null && this.m_reportSections.Count == 1, "Expected single section");
							ReportSection reportSection = this.m_reportSections[0];
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable6;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable6);
							Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textBox = (Microsoft.ReportingServices.ReportIntermediateFormat.TextBox)referenceable6;
							reportSection.Page.AddInScopeTextBox(textBox);
							continue;
						}
						case MemberName.InScopeTextBoxesInBody:
						{
							Global.Tracer.Assert(this.m_reportSections != null && this.m_reportSections.Count == 1, "Expected single section");
							ReportSection reportSection2 = this.m_reportSections[0];
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable7;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable7);
							Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textBox2 = (Microsoft.ReportingServices.ReportIntermediateFormat.TextBox)referenceable7;
							reportSection2.AddInScopeTextBox(textBox2);
							continue;
						}
						}
					}
					else
					{
						if (memberName == MemberName.EventSources)
						{
							if (this.m_eventSources == null)
							{
								this.m_eventSources = new List<IInScopeEventSource>();
							}
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable8;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable8);
							IInScopeEventSource inScopeEventSource2 = (IInScopeEventSource)referenceable8;
							this.m_eventSources.Add(inScopeEventSource2);
							continue;
						}
						if (memberName == MemberName.GroupsWithVariables)
						{
							if (this.m_groupsWithVariables == null)
							{
								this.m_groupsWithVariables = new List<ReportHierarchyNode>();
							}
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable9;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable9);
							ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)referenceable9;
							this.m_groupsWithVariables.Add(reportHierarchyNode);
							continue;
						}
					}
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x001133A0 File Offset: 0x001115A0
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Report;
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x001133A8 File Offset: 0x001115A8
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			this.m_exprHost = reportExprHost;
			base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
			if (reportExprHost.VariableValueHosts != null)
			{
				reportExprHost.VariableValueHosts.SetReportObjectModel(reportObjectModel);
			}
			for (int i = 0; i < this.m_reportSections.Count; i++)
			{
				this.m_reportSections[i].SetExprHost(reportExprHost, reportObjectModel);
			}
			if ((reportExprHost.LookupExprHostsRemotable != null || reportExprHost.LookupDestExprHostsRemotable != null || reportExprHost.DataSetHostsRemotable != null) && this.m_dataSources != null)
			{
				for (int j = 0; j < this.m_dataSources.Count; j++)
				{
					DataSource dataSource = this.m_dataSources[j];
					if (dataSource.DataSets != null)
					{
						for (int k = 0; k < dataSource.DataSets.Count; k++)
						{
							dataSource.DataSets[k].SetExprHost(reportExprHost, reportObjectModel);
						}
					}
				}
			}
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x0011347C File Offset: 0x0011167C
		internal int EvaluateAutoRefresh(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			if (this.m_autoRefreshExpression == null)
			{
				return Math.Max(0, this.m_autoRefresh);
			}
			if (this.m_autoRefresh < 0)
			{
				if (!this.m_autoRefreshExpression.IsExpression)
				{
					this.m_autoRefresh = this.m_autoRefreshExpression.IntValue;
				}
				else
				{
					context.SetupContext(this, romInstance);
					this.m_autoRefresh = Math.Max(0, context.ReportRuntime.EvaluateReportAutoRefreshExpression(this));
				}
			}
			return this.m_autoRefresh;
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x001134ED File Offset: 0x001116ED
		internal string EvaluateInitialPageName(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, instance);
			return context.ReportRuntime.EvaluateInitialPageNameExpression(this);
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x00113504 File Offset: 0x00111704
		internal void RegisterDataSetScopedAggregates(OnDemandProcessingContext odpContext)
		{
			int count = this.MappingDataSetIndexToDataSet.Count;
			for (int i = 0; i < count; i++)
			{
				odpContext.RuntimeInitializeAggregates<DataAggregateInfo>(this.MappingDataSetIndexToDataSet[i].Aggregates);
				odpContext.RuntimeInitializeAggregates<DataAggregateInfo>(this.MappingDataSetIndexToDataSet[i].PostSortAggregates);
			}
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x00113558 File Offset: 0x00111758
		private void GenerateDataSetMappings()
		{
			if (this.m_mappingNameToDataSet != null)
			{
				return;
			}
			this.m_mappingNameToDataSet = new Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet>();
			this.m_mappingDataSetIndexToDataSourceIndex = new List<int>();
			this.m_mappingDataSetIndexToDataSet = new List<Microsoft.ReportingServices.ReportIntermediateFormat.DataSet>();
			int num = ((this.m_dataSources == null) ? 0 : this.m_dataSources.Count);
			for (int i = 0; i < num; i++)
			{
				DataSource dataSource = this.m_dataSources[i];
				int num2 = ((dataSource.DataSets == null) ? 0 : dataSource.DataSets.Count);
				for (int j = 0; j < num2; j++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = dataSource.DataSets[j];
					this.AddDataSetMapping(i, dataSet);
				}
			}
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x00113600 File Offset: 0x00111800
		private void AddDataSetMapping(int dataSourceIndex, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
		{
			if (!this.m_mappingNameToDataSet.ContainsKey(dataSet.Name))
			{
				this.m_mappingNameToDataSet.Add(dataSet.Name, dataSet);
				for (int i = this.m_mappingDataSetIndexToDataSourceIndex.Count; i <= dataSet.IndexInCollection; i++)
				{
					this.m_mappingDataSetIndexToDataSourceIndex.Add(-1);
					this.m_mappingDataSetIndexToDataSet.Add(null);
				}
				this.m_mappingDataSetIndexToDataSourceIndex[dataSet.IndexInCollection] = dataSourceIndex;
				this.m_mappingDataSetIndexToDataSet[dataSet.IndexInCollection] = dataSet;
			}
		}

		// Token: 0x04001DAD RID: 7597
		private bool m_consumeContainerWhitespace;

		// Token: 0x04001DAE RID: 7598
		private Guid m_reportVersion = Guid.Empty;

		// Token: 0x04001DAF RID: 7599
		private string m_author;

		// Token: 0x04001DB0 RID: 7600
		private int m_autoRefresh = -1;

		// Token: 0x04001DB1 RID: 7601
		private Dictionary<string, ImageInfo> m_embeddedImages;

		// Token: 0x04001DB2 RID: 7602
		private List<DataSource> m_dataSources;

		// Token: 0x04001DB3 RID: 7603
		private List<Variable> m_variables;

		// Token: 0x04001DB4 RID: 7604
		private bool m_deferVariableEvaluation;

		// Token: 0x04001DB5 RID: 7605
		private byte[] m_exprCompiledCode;

		// Token: 0x04001DB6 RID: 7606
		private bool m_exprCompiledCodeGeneratedWithRefusedPermissions;

		// Token: 0x04001DB7 RID: 7607
		private bool m_mergeOnePass;

		// Token: 0x04001DB8 RID: 7608
		private bool m_subReportMergeTransactions;

		// Token: 0x04001DB9 RID: 7609
		private bool m_needPostGroupProcessing;

		// Token: 0x04001DBA RID: 7610
		private bool m_hasPostSortAggregates;

		// Token: 0x04001DBB RID: 7611
		private bool m_hasAggregatesOfAggregates;

		// Token: 0x04001DBC RID: 7612
		private bool m_hasAggregatesOfAggregatesInUserSort;

		// Token: 0x04001DBD RID: 7613
		private bool m_hasReportItemReferences;

		// Token: 0x04001DBE RID: 7614
		private Microsoft.ReportingServices.ReportIntermediateFormat.Report.ShowHideTypes m_showHideType;

		// Token: 0x04001DBF RID: 7615
		private int m_lastID;

		// Token: 0x04001DC0 RID: 7616
		[Reference]
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport> m_subReports;

		// Token: 0x04001DC1 RID: 7617
		private bool m_hasImageStreams;

		// Token: 0x04001DC2 RID: 7618
		private bool m_hasLabels;

		// Token: 0x04001DC3 RID: 7619
		private bool m_hasBookmarks;

		// Token: 0x04001DC4 RID: 7620
		private bool m_parametersNotUsedInQuery;

		// Token: 0x04001DC5 RID: 7621
		private List<ParameterDef> m_parameters;

		// Token: 0x04001DC6 RID: 7622
		private string m_oneDataSetName;

		// Token: 0x04001DC7 RID: 7623
		private List<string> m_codeModules;

		// Token: 0x04001DC8 RID: 7624
		private List<CodeClass> m_codeClasses;

		// Token: 0x04001DC9 RID: 7625
		private bool m_hasSpecialRecursiveAggregates;

		// Token: 0x04001DCA RID: 7626
		private ExpressionInfo m_language;

		// Token: 0x04001DCB RID: 7627
		private string m_dataTransform;

		// Token: 0x04001DCC RID: 7628
		private string m_dataSchema;

		// Token: 0x04001DCD RID: 7629
		private bool m_dataElementStyleAttribute = true;

		// Token: 0x04001DCE RID: 7630
		private string m_code;

		// Token: 0x04001DCF RID: 7631
		private bool m_hasUserSortFilter;

		// Token: 0x04001DD0 RID: 7632
		private bool m_hasHeadersOrFooters;

		// Token: 0x04001DD1 RID: 7633
		private bool m_hasPreviousAggregates;

		// Token: 0x04001DD2 RID: 7634
		private InScopeSortFilterHashtable m_nonDetailSortFiltersInScope;

		// Token: 0x04001DD3 RID: 7635
		private InScopeSortFilterHashtable m_detailSortFiltersInScope;

		// Token: 0x04001DD4 RID: 7636
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion> m_topLevelDataRegions;

		// Token: 0x04001DD5 RID: 7637
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_firstDataSet;

		// Token: 0x04001DD6 RID: 7638
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion m_topLeftDataRegion;

		// Token: 0x04001DD7 RID: 7639
		private int m_dataSetsNotOnlyUsedInParameters;

		// Token: 0x04001DD8 RID: 7640
		private List<IInScopeEventSource> m_inScopeEventSources;

		// Token: 0x04001DD9 RID: 7641
		private List<IInScopeEventSource> m_eventSources;

		// Token: 0x04001DDA RID: 7642
		private List<ReportHierarchyNode> m_groupsWithVariables;

		// Token: 0x04001DDB RID: 7643
		private byte[] m_flattenedDatasetDependencyMatrix;

		// Token: 0x04001DDC RID: 7644
		private int m_firstDataSetIndexToProcess = -1;

		// Token: 0x04001DDD RID: 7645
		private byte[] m_variablesInScope;

		// Token: 0x04001DDE RID: 7646
		private bool m_hasLookups;

		// Token: 0x04001DDF RID: 7647
		private List<ReportSection> m_reportSections;

		// Token: 0x04001DE0 RID: 7648
		[NonSerialized]
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> m_dataShapes;

		// Token: 0x04001DE1 RID: 7649
		private ExpressionInfo m_autoRefreshExpression;

		// Token: 0x04001DE2 RID: 7650
		private ExpressionInfo m_initialPageName;

		// Token: 0x04001DE3 RID: 7651
		private int m_sharedDSContainerCollectionIndex = -1;

		// Token: 0x04001DE4 RID: 7652
		private int m_dataPipelineCount;

		// Token: 0x04001DE5 RID: 7653
		private string m_defaultFontFamily;

		// Token: 0x04001DE6 RID: 7654
		[NonSerialized]
		private DataSource m_sharedDSContainer;

		// Token: 0x04001DE7 RID: 7655
		[NonSerialized]
		private int m_lastAggregateID = -1;

		// Token: 0x04001DE8 RID: 7656
		[NonSerialized]
		private int m_lastLookupID = -1;

		// Token: 0x04001DE9 RID: 7657
		[NonSerialized]
		private double m_topLeftDataRegionAbsTop;

		// Token: 0x04001DEA RID: 7658
		[NonSerialized]
		private double m_topLeftDataRegionAbsLeft;

		// Token: 0x04001DEB RID: 7659
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.Report.GetDeclaration();

		// Token: 0x04001DEC RID: 7660
		[NonSerialized]
		private ReportExprHost m_exprHost;

		// Token: 0x04001DED RID: 7661
		[NonSerialized]
		private Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet> m_mappingNameToDataSet;

		// Token: 0x04001DEE RID: 7662
		[NonSerialized]
		private List<int> m_mappingDataSetIndexToDataSourceIndex;

		// Token: 0x04001DEF RID: 7663
		[NonSerialized]
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.DataSet> m_mappingDataSetIndexToDataSet;

		// Token: 0x04001DF0 RID: 7664
		[NonSerialized]
		private bool m_reportOrDescendentHasUserSortFilter;

		// Token: 0x0200097D RID: 2429
		internal enum ShowHideTypes
		{
			// Token: 0x040040F7 RID: 16631
			None,
			// Token: 0x040040F8 RID: 16632
			Static,
			// Token: 0x040040F9 RID: 16633
			Interactive
		}
	}
}
