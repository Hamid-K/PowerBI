using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200051A RID: 1306
	[Serializable]
	internal class SubReport : Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IIndexedInCollection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGloballyReferenceable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGlobalIDOwner
	{
		// Token: 0x0600457A RID: 17786 RVA: 0x00123152 File Offset: 0x00121352
		internal SubReport(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x00123169 File Offset: 0x00121369
		internal SubReport(int id, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(id, parent)
		{
			this.m_parameters = new List<ParameterValue>();
		}

		// Token: 0x17001D14 RID: 7444
		// (get) Token: 0x0600457C RID: 17788 RVA: 0x0012318C File Offset: 0x0012138C
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Subreport;
			}
		}

		// Token: 0x17001D15 RID: 7445
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x0012318F File Offset: 0x0012138F
		// (set) Token: 0x0600457E RID: 17790 RVA: 0x00123197 File Offset: 0x00121397
		internal string OriginalCatalogPath
		{
			get
			{
				return this.m_reportPath;
			}
			set
			{
				this.m_reportPath = value;
			}
		}

		// Token: 0x17001D16 RID: 7446
		// (get) Token: 0x0600457F RID: 17791 RVA: 0x001231A0 File Offset: 0x001213A0
		// (set) Token: 0x06004580 RID: 17792 RVA: 0x001231A8 File Offset: 0x001213A8
		internal List<ParameterValue> Parameters
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

		// Token: 0x17001D17 RID: 7447
		// (get) Token: 0x06004581 RID: 17793 RVA: 0x001231B1 File Offset: 0x001213B1
		// (set) Token: 0x06004582 RID: 17794 RVA: 0x001231B9 File Offset: 0x001213B9
		internal ExpressionInfo NoRowsMessage
		{
			get
			{
				return this.m_noRowsMessage;
			}
			set
			{
				this.m_noRowsMessage = value;
			}
		}

		// Token: 0x17001D18 RID: 7448
		// (get) Token: 0x06004583 RID: 17795 RVA: 0x001231C2 File Offset: 0x001213C2
		// (set) Token: 0x06004584 RID: 17796 RVA: 0x001231CA File Offset: 0x001213CA
		internal bool MergeTransactions
		{
			get
			{
				return this.m_mergeTransactions;
			}
			set
			{
				this.m_mergeTransactions = value;
			}
		}

		// Token: 0x17001D19 RID: 7449
		// (get) Token: 0x06004585 RID: 17797 RVA: 0x001231D3 File Offset: 0x001213D3
		// (set) Token: 0x06004586 RID: 17798 RVA: 0x001231DB File Offset: 0x001213DB
		internal GroupingList ContainingScopes
		{
			get
			{
				return this.m_containingScopes;
			}
			set
			{
				this.m_containingScopes = value;
			}
		}

		// Token: 0x17001D1A RID: 7450
		// (get) Token: 0x06004587 RID: 17799 RVA: 0x001231E4 File Offset: 0x001213E4
		// (set) Token: 0x06004588 RID: 17800 RVA: 0x001231EC File Offset: 0x001213EC
		internal Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status RetrievalStatus
		{
			get
			{
				return this.m_status;
			}
			set
			{
				this.m_status = value;
			}
		}

		// Token: 0x17001D1B RID: 7451
		// (get) Token: 0x06004589 RID: 17801 RVA: 0x001231F5 File Offset: 0x001213F5
		// (set) Token: 0x0600458A RID: 17802 RVA: 0x001231FD File Offset: 0x001213FD
		internal string ReportName
		{
			get
			{
				return this.m_reportName;
			}
			set
			{
				this.m_reportName = value;
			}
		}

		// Token: 0x17001D1C RID: 7452
		// (get) Token: 0x0600458B RID: 17803 RVA: 0x00123206 File Offset: 0x00121406
		// (set) Token: 0x0600458C RID: 17804 RVA: 0x0012320E File Offset: 0x0012140E
		internal string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x17001D1D RID: 7453
		// (get) Token: 0x0600458D RID: 17805 RVA: 0x00123217 File Offset: 0x00121417
		// (set) Token: 0x0600458E RID: 17806 RVA: 0x0012321F File Offset: 0x0012141F
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Report Report
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

		// Token: 0x17001D1E RID: 7454
		// (get) Token: 0x0600458F RID: 17807 RVA: 0x00123228 File Offset: 0x00121428
		// (set) Token: 0x06004590 RID: 17808 RVA: 0x00123230 File Offset: 0x00121430
		internal ICatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
			set
			{
				this.m_reportContext = value;
			}
		}

		// Token: 0x17001D1F RID: 7455
		// (get) Token: 0x06004591 RID: 17809 RVA: 0x00123239 File Offset: 0x00121439
		// (set) Token: 0x06004592 RID: 17810 RVA: 0x00123241 File Offset: 0x00121441
		internal ParameterInfoCollection ParametersFromCatalog
		{
			get
			{
				return this.m_parametersFromCatalog;
			}
			set
			{
				this.m_parametersFromCatalog = value;
			}
		}

		// Token: 0x17001D20 RID: 7456
		// (get) Token: 0x06004593 RID: 17811 RVA: 0x0012324A File Offset: 0x0012144A
		internal SubreportExprHost SubReportExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001D21 RID: 7457
		// (get) Token: 0x06004594 RID: 17812 RVA: 0x00123252 File Offset: 0x00121452
		internal bool IsTablixCellScope
		{
			get
			{
				return this.m_isTablixCellScope;
			}
		}

		// Token: 0x17001D22 RID: 7458
		// (get) Token: 0x06004595 RID: 17813 RVA: 0x0012325A File Offset: 0x0012145A
		// (set) Token: 0x06004596 RID: 17814 RVA: 0x00123262 File Offset: 0x00121462
		internal bool IsDetailScope
		{
			get
			{
				return this.m_isDetailScope;
			}
			set
			{
				this.m_isDetailScope = value;
			}
		}

		// Token: 0x17001D23 RID: 7459
		// (get) Token: 0x06004597 RID: 17815 RVA: 0x0012326B File Offset: 0x0012146B
		// (set) Token: 0x06004598 RID: 17816 RVA: 0x00123273 File Offset: 0x00121473
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport> DetailScopeSubReports
		{
			get
			{
				return this.m_detailScopeSubReports;
			}
			set
			{
				this.m_detailScopeSubReports = value;
			}
		}

		// Token: 0x17001D24 RID: 7460
		// (get) Token: 0x06004599 RID: 17817 RVA: 0x0012327C File Offset: 0x0012147C
		// (set) Token: 0x0600459A RID: 17818 RVA: 0x00123284 File Offset: 0x00121484
		internal SubReportInfo OdpSubReportInfo
		{
			get
			{
				return this.m_odpSubReportInfo;
			}
			set
			{
				this.m_odpSubReportInfo = value;
			}
		}

		// Token: 0x17001D25 RID: 7461
		// (get) Token: 0x0600459B RID: 17819 RVA: 0x0012328D File Offset: 0x0012148D
		// (set) Token: 0x0600459C RID: 17820 RVA: 0x00123295 File Offset: 0x00121495
		internal bool KeepTogether
		{
			get
			{
				return this.m_keepTogether;
			}
			set
			{
				this.m_keepTogether = value;
			}
		}

		// Token: 0x17001D26 RID: 7462
		// (get) Token: 0x0600459D RID: 17821 RVA: 0x0012329E File Offset: 0x0012149E
		// (set) Token: 0x0600459E RID: 17822 RVA: 0x001232A6 File Offset: 0x001214A6
		internal bool OmitBorderOnPageBreak
		{
			get
			{
				return this.m_omitBorderOnPageBreak;
			}
			set
			{
				this.m_omitBorderOnPageBreak = value;
			}
		}

		// Token: 0x17001D27 RID: 7463
		// (get) Token: 0x0600459F RID: 17823 RVA: 0x001232AF File Offset: 0x001214AF
		// (set) Token: 0x060045A0 RID: 17824 RVA: 0x001232B7 File Offset: 0x001214B7
		internal OnDemandProcessingContext OdpContext
		{
			get
			{
				return this.m_odpContext;
			}
			set
			{
				this.m_odpContext = value;
			}
		}

		// Token: 0x17001D28 RID: 7464
		// (get) Token: 0x060045A1 RID: 17825 RVA: 0x001232C0 File Offset: 0x001214C0
		// (set) Token: 0x060045A2 RID: 17826 RVA: 0x001232C8 File Offset: 0x001214C8
		internal bool ExceededMaxLevel
		{
			get
			{
				return this.m_exceededMaxLevel;
			}
			set
			{
				this.m_exceededMaxLevel = value;
			}
		}

		// Token: 0x17001D29 RID: 7465
		// (get) Token: 0x060045A3 RID: 17827 RVA: 0x001232D1 File Offset: 0x001214D1
		internal bool InDataRegion
		{
			get
			{
				return (this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) > (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0;
			}
		}

		// Token: 0x17001D2A RID: 7466
		// (get) Token: 0x060045A4 RID: 17828 RVA: 0x001232DE File Offset: 0x001214DE
		// (set) Token: 0x060045A5 RID: 17829 RVA: 0x001232E6 File Offset: 0x001214E6
		public int IndexInCollection
		{
			get
			{
				return this.m_indexInCollection;
			}
			set
			{
				this.m_indexInCollection = value;
			}
		}

		// Token: 0x17001D2B RID: 7467
		// (get) Token: 0x060045A6 RID: 17830 RVA: 0x001232EF File Offset: 0x001214EF
		public IndexedInCollectionType IndexedInCollectionType
		{
			get
			{
				return IndexedInCollectionType.SubReport;
			}
		}

		// Token: 0x17001D2C RID: 7468
		// (get) Token: 0x060045A7 RID: 17831 RVA: 0x001232F2 File Offset: 0x001214F2
		// (set) Token: 0x060045A8 RID: 17832 RVA: 0x001232FA File Offset: 0x001214FA
		internal IReference<Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance> CurrentSubReportInstance
		{
			get
			{
				return this.m_currentSubReportInstance;
			}
			set
			{
				this.m_currentSubReportInstance = value;
			}
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x00123303 File Offset: 0x00121503
		protected override InstancePathItem CreateInstancePathItem()
		{
			return new InstancePathItem(InstancePathItemType.SubReport, this.IndexInCollection);
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x00123311 File Offset: 0x00121511
		internal ReportSection GetContainingSection(OnDemandProcessingContext parentReportOdpContext)
		{
			if (this.m_containingSection == null)
			{
				this.m_containingSection = parentReportOdpContext.ReportDefinition.ReportSections[0];
			}
			return this.m_containingSection;
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x00123338 File Offset: 0x00121538
		internal void SetContainingSection(ReportSection section)
		{
			this.m_containingSection = section;
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x00123344 File Offset: 0x00121544
		internal override bool Initialize(InitializationContext context)
		{
			this.m_location = context.Location;
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			if (this.InDataRegion)
			{
				context.SetDataSetHasSubReports();
				if ((context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegionCellTopLevelItem) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					this.m_isTablixCellScope = context.IsDataRegionScopedCell;
				}
				if ((Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 < (context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail))
				{
					this.m_isDetailScope = true;
					context.SetDataSetDetailUserSortFilter();
				}
			}
			context.SetIndexInCollection(this);
			context.ExprHostBuilder.SubreportStart(this.m_name);
			base.Initialize(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context);
			}
			if (this.m_parameters != null)
			{
				for (int i = 0; i < this.m_parameters.Count; i++)
				{
					ParameterValue parameterValue = this.m_parameters[i];
					context.ExprHostBuilder.SubreportParameterStart();
					parameterValue.Initialize("SubreportParameter(" + parameterValue.Name + ")", context, false);
					parameterValue.ExprHostID = context.ExprHostBuilder.SubreportParameterEnd();
				}
			}
			if (this.m_noRowsMessage != null)
			{
				this.m_noRowsMessage.Initialize("NoRows", context);
				context.ExprHostBuilder.GenericNoRows(this.m_noRowsMessage);
			}
			base.ExprHostID = context.ExprHostBuilder.SubreportEnd();
			return false;
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x00123498 File Offset: 0x00121698
		internal override void InitializeRVDirectionDependentItems(InitializationContext context)
		{
			this.m_containingScopes = context.GetContainingScopes();
			for (int i = 0; i < this.m_containingScopes.Count; i++)
			{
				this.m_containingScopes[i].SaveGroupExprValues = true;
			}
			if (this.m_isDetailScope)
			{
				this.m_containingScopes.Add(null);
			}
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x001234F0 File Offset: 0x001216F0
		internal void UpdateSubReportScopes(Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.UserSortFilterContext context)
		{
			if (this.m_containingScopes != null && 0 < this.m_containingScopes.Count && this.m_containingScopes.LastEntry == null)
			{
				if (context.DetailScopeSubReports != null)
				{
					this.m_detailScopeSubReports = context.CloneDetailScopeSubReports();
				}
				else
				{
					this.m_detailScopeSubReports = new List<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport>();
				}
				this.m_detailScopeSubReports.Add(this);
			}
			else
			{
				this.m_detailScopeSubReports = context.DetailScopeSubReports;
			}
			if (context.ContainingScopes != null)
			{
				if (this.m_containingScopes != null && 0 < this.m_containingScopes.Count)
				{
					this.m_containingScopes.InsertRange(0, context.ContainingScopes);
				}
				else
				{
					this.m_containingScopes = context.ContainingScopes;
				}
			}
			if (this.m_report != null && this.m_report.EventSources != null)
			{
				int count = this.m_report.EventSources.Count;
				for (int i = 0; i < count; i++)
				{
					IInScopeEventSource inScopeEventSource = this.m_report.EventSources[i];
					if (inScopeEventSource.UserSort != null)
					{
						inScopeEventSource.UserSort.DetailScopeSubReports = this.m_detailScopeSubReports;
					}
					if (this.m_containingScopes != null && 0 < this.m_containingScopes.Count)
					{
						if (inScopeEventSource.ContainingScopes != null && 0 < inScopeEventSource.ContainingScopes.Count)
						{
							inScopeEventSource.ContainingScopes.InsertRange(0, this.m_containingScopes);
						}
						else
						{
							inScopeEventSource.IsSubReportTopLevelScope = true;
							inScopeEventSource.ContainingScopes = this.m_containingScopes;
						}
					}
				}
			}
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x00123658 File Offset: 0x00121858
		internal void UpdateSubReportEventSourceGlobalDataSetIds(SubReportInfo subReportInfo)
		{
			this.m_odpSubReportInfo = subReportInfo;
			if (this.m_report != null && this.m_report.EventSources != null)
			{
				int count = this.m_report.EventSources.Count;
				for (int i = 0; i < count; i++)
				{
					IInScopeEventSource inScopeEventSource = this.m_report.EventSources[i];
					if (inScopeEventSource.UserSort != null && -1 != subReportInfo.UserSortDataSetGlobalId)
					{
						inScopeEventSource.UserSort.SubReportDataSetGlobalId = subReportInfo.UserSortDataSetGlobalId;
					}
				}
			}
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x001236D4 File Offset: 0x001218D4
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReport, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Parameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterValue),
				new MemberInfo(MemberName.NoRowsMessage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MergeTransactions, Token.Boolean),
				new MemberInfo(MemberName.ContainingScopes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Grouping),
				new MemberInfo(MemberName.IsTablixCellScope, Token.Boolean),
				new MemberInfo(MemberName.ReportName, Token.String),
				new MemberInfo(MemberName.OmitBorderOnPageBreak, Token.Boolean),
				new MemberInfo(MemberName.KeepTogether, Token.Boolean),
				new MemberInfo(MemberName.Location, Token.Enum),
				new MemberInfo(MemberName.IndexInCollection, Token.Int32),
				new MemberInfo(MemberName.ContainingSection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportSection, Token.Reference)
			});
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x001237DC File Offset: 0x001219DC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Location)
				{
					if (memberName <= MemberName.MergeTransactions)
					{
						if (memberName == MemberName.Parameters)
						{
							writer.Write<ParameterValue>(this.m_parameters);
							continue;
						}
						if (memberName == MemberName.MergeTransactions)
						{
							writer.Write(this.m_mergeTransactions);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ReportName)
						{
							writer.Write(this.m_reportName);
							continue;
						}
						if (memberName == MemberName.KeepTogether)
						{
							writer.Write(this.m_keepTogether);
							continue;
						}
						if (memberName == MemberName.Location)
						{
							writer.WriteEnum((int)this.m_location);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.NoRowsMessage)
				{
					if (memberName == MemberName.ContainingScopes)
					{
						writer.WriteListOfReferences(this.m_containingScopes);
						continue;
					}
					if (memberName == MemberName.IsTablixCellScope)
					{
						writer.Write(this.m_isTablixCellScope);
						continue;
					}
					if (memberName == MemberName.NoRowsMessage)
					{
						writer.Write(this.m_noRowsMessage);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.OmitBorderOnPageBreak)
					{
						writer.Write(this.m_omitBorderOnPageBreak);
						continue;
					}
					if (memberName == MemberName.IndexInCollection)
					{
						writer.Write(this.m_indexInCollection);
						continue;
					}
					if (memberName == MemberName.ContainingSection)
					{
						writer.WriteReference(this.m_containingSection);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x00123968 File Offset: 0x00121B68
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Location)
				{
					if (memberName <= MemberName.MergeTransactions)
					{
						if (memberName == MemberName.Parameters)
						{
							this.m_parameters = reader.ReadGenericListOfRIFObjects<ParameterValue>();
							continue;
						}
						if (memberName == MemberName.MergeTransactions)
						{
							this.m_mergeTransactions = reader.ReadBoolean();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ReportName)
						{
							this.m_reportName = reader.ReadString();
							continue;
						}
						if (memberName == MemberName.KeepTogether)
						{
							this.m_keepTogether = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.Location)
						{
							this.m_location = (Microsoft.ReportingServices.ReportPublishing.LocationFlags)reader.ReadEnum();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.NoRowsMessage)
				{
					if (memberName != MemberName.ContainingScopes)
					{
						if (memberName == MemberName.IsTablixCellScope)
						{
							this.m_isTablixCellScope = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.NoRowsMessage)
						{
							this.m_noRowsMessage = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						if (reader.ReadListOfReferencesNoResolution(this) == 0)
						{
							this.m_containingScopes = new GroupingList();
							continue;
						}
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.OmitBorderOnPageBreak)
					{
						this.m_omitBorderOnPageBreak = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.IndexInCollection)
					{
						this.m_indexInCollection = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.ContainingSection)
					{
						this.m_containingSection = reader.ReadReference<ReportSection>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x00123B0C File Offset: 0x00121D0C
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.ContainingScopes)
					{
						if (memberName != MemberName.ContainingSection)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable);
							this.m_containingSection = (ReportSection)referenceable;
						}
					}
					else
					{
						if (this.m_containingScopes == null)
						{
							this.m_containingScopes = new GroupingList();
						}
						if (memberReference.RefID != -2)
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							Global.Tracer.Assert(referenceableItems[memberReference.RefID] is Grouping);
							Global.Tracer.Assert(!this.m_containingScopes.Contains((Grouping)referenceableItems[memberReference.RefID]));
							this.m_containingScopes.Add((Grouping)referenceableItems[memberReference.RefID]);
						}
						else
						{
							this.m_containingScopes.Add(null);
						}
					}
				}
			}
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x00123C6C File Offset: 0x00121E6C
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReport;
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x00123C74 File Offset: 0x00121E74
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport = (Microsoft.ReportingServices.ReportIntermediateFormat.SubReport)base.PublishClone(context);
			context.AddSubReport(subReport);
			if (this.m_reportPath != null)
			{
				subReport.m_reportPath = (string)this.m_reportPath.Clone();
			}
			if (this.m_parameters != null)
			{
				subReport.m_parameters = new List<ParameterValue>(this.m_parameters.Count);
				foreach (ParameterValue parameterValue in this.m_parameters)
				{
					subReport.m_parameters.Add((ParameterValue)parameterValue.PublishClone(context));
				}
			}
			if (this.m_noRowsMessage != null)
			{
				subReport.m_noRowsMessage = (ExpressionInfo)this.m_noRowsMessage.PublishClone(context);
			}
			return subReport;
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x00123D48 File Offset: 0x00121F48
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_exprHost = reportExprHost.SubreportHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_exprHost.ParameterHostsRemotable != null)
				{
					Global.Tracer.Assert(this.m_parameters != null, "(m_parameters != null)");
					for (int i = this.m_parameters.Count - 1; i >= 0; i--)
					{
						this.m_parameters[i].SetExprHost(this.m_exprHost.ParameterHostsRemotable, reportObjectModel);
					}
				}
			}
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x00123DF8 File Offset: 0x00121FF8
		internal string EvaulateNoRowMessage(IReportScopeInstance subReportInstance, OnDemandProcessingContext odpContext)
		{
			odpContext.SetupContext(this, subReportInstance);
			return odpContext.ReportRuntime.EvaluateSubReportNoRowsExpression(this, "Subreport", "NoRowsMessage");
		}

		// Token: 0x04001F4C RID: 8012
		internal const uint MaxSubReportLevel = 20U;

		// Token: 0x04001F4D RID: 8013
		private string m_reportName;

		// Token: 0x04001F4E RID: 8014
		private List<ParameterValue> m_parameters;

		// Token: 0x04001F4F RID: 8015
		private ExpressionInfo m_noRowsMessage;

		// Token: 0x04001F50 RID: 8016
		private bool m_mergeTransactions;

		// Token: 0x04001F51 RID: 8017
		[Reference]
		private GroupingList m_containingScopes;

		// Token: 0x04001F52 RID: 8018
		private bool m_omitBorderOnPageBreak;

		// Token: 0x04001F53 RID: 8019
		private bool m_keepTogether;

		// Token: 0x04001F54 RID: 8020
		private bool m_isTablixCellScope;

		// Token: 0x04001F55 RID: 8021
		private Microsoft.ReportingServices.ReportPublishing.LocationFlags m_location = Microsoft.ReportingServices.ReportPublishing.LocationFlags.None;

		// Token: 0x04001F56 RID: 8022
		private int m_indexInCollection = -1;

		// Token: 0x04001F57 RID: 8023
		[Reference]
		private ReportSection m_containingSection;

		// Token: 0x04001F58 RID: 8024
		[NonSerialized]
		private bool m_isDetailScope;

		// Token: 0x04001F59 RID: 8025
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.GetDeclaration();

		// Token: 0x04001F5A RID: 8026
		[NonSerialized]
		private ParameterInfoCollection m_parametersFromCatalog;

		// Token: 0x04001F5B RID: 8027
		[NonSerialized]
		private Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status m_status;

		// Token: 0x04001F5C RID: 8028
		[NonSerialized]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Report m_report;

		// Token: 0x04001F5D RID: 8029
		[NonSerialized]
		private string m_description;

		// Token: 0x04001F5E RID: 8030
		[NonSerialized]
		private string m_reportPath;

		// Token: 0x04001F5F RID: 8031
		[NonSerialized]
		private SubreportExprHost m_exprHost;

		// Token: 0x04001F60 RID: 8032
		[NonSerialized]
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport> m_detailScopeSubReports;

		// Token: 0x04001F61 RID: 8033
		[NonSerialized]
		private SubReportInfo m_odpSubReportInfo;

		// Token: 0x04001F62 RID: 8034
		[NonSerialized]
		private ICatalogItemContext m_reportContext;

		// Token: 0x04001F63 RID: 8035
		[NonSerialized]
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x04001F64 RID: 8036
		[NonSerialized]
		private bool m_exceededMaxLevel;

		// Token: 0x04001F65 RID: 8037
		[NonSerialized]
		private IReference<Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance> m_currentSubReportInstance;

		// Token: 0x02000983 RID: 2435
		internal enum Status
		{
			// Token: 0x04004146 RID: 16710
			NotRetrieved,
			// Token: 0x04004147 RID: 16711
			DataRetrieveFailed,
			// Token: 0x04004148 RID: 16712
			DefinitionRetrieveFailed,
			// Token: 0x04004149 RID: 16713
			PreFetched,
			// Token: 0x0400414A RID: 16714
			DefinitionRetrieved,
			// Token: 0x0400414B RID: 16715
			DataRetrieved,
			// Token: 0x0400414C RID: 16716
			DataNotRetrieved,
			// Token: 0x0400414D RID: 16717
			ParametersNotSpecified
		}
	}
}
