using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004FE RID: 1278
	[Serializable]
	internal sealed class ReportSection : Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IRIFReportScope, IInstancePath
	{
		// Token: 0x06004225 RID: 16933 RVA: 0x00116622 File Offset: 0x00114822
		internal ReportSection()
			: base(null)
		{
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x00116632 File Offset: 0x00114832
		internal ReportSection(int indexInCollection)
			: this()
		{
			this.m_publishingIndexInCollection = indexInCollection;
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x00116641 File Offset: 0x00114841
		internal ReportSection(int indexInCollection, Microsoft.ReportingServices.ReportIntermediateFormat.Report report, int id, int idForReportItems)
			: base(id, report)
		{
			this.m_publishingIndexInCollection = indexInCollection;
			this.m_height = "11in";
			this.m_width = "8.5in";
			this.m_reportItems = new Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection(idForReportItems, true);
		}

		// Token: 0x17001BD0 RID: 7120
		// (get) Token: 0x06004228 RID: 16936 RVA: 0x0011667D File Offset: 0x0011487D
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportSection;
			}
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x00116684 File Offset: 0x00114884
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			ReportItemExprHost reportItemExprHost = null;
			if (base.ExprHostID >= 0)
			{
				if (reportExprHost.ReportSectionHostsRemotable != null)
				{
					reportItemExprHost = reportExprHost.ReportSectionHostsRemotable[base.ExprHostID];
				}
				else if (base.ExprHostID == 0)
				{
					reportItemExprHost = reportExprHost;
				}
				else
				{
					Global.Tracer.Assert(false, "Missing ExprHost for Body. ExprHostID: {0}", new object[] { base.ExprHostID });
				}
				base.ReportItemSetExprHost(reportItemExprHost, reportObjectModel);
			}
			if (this.m_page != null)
			{
				this.m_page.SetExprHost(reportExprHost, reportObjectModel);
			}
		}

		// Token: 0x17001BD1 RID: 7121
		// (get) Token: 0x0600422A RID: 16938 RVA: 0x00116705 File Offset: 0x00114905
		// (set) Token: 0x0600422B RID: 16939 RVA: 0x0011670D File Offset: 0x0011490D
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
			set
			{
				this.m_reportItems = value;
			}
		}

		// Token: 0x17001BD2 RID: 7122
		// (get) Token: 0x0600422C RID: 16940 RVA: 0x00116716 File Offset: 0x00114916
		// (set) Token: 0x0600422D RID: 16941 RVA: 0x0011671E File Offset: 0x0011491E
		internal Page Page
		{
			get
			{
				return this.m_page;
			}
			set
			{
				this.m_page = value;
			}
		}

		// Token: 0x17001BD3 RID: 7123
		// (get) Token: 0x0600422E RID: 16942 RVA: 0x00116727 File Offset: 0x00114927
		internal double PageSectionWidth
		{
			get
			{
				return this.m_page.GetPageSectionWidth(this.m_widthValue);
			}
		}

		// Token: 0x17001BD4 RID: 7124
		// (get) Token: 0x0600422F RID: 16943 RVA: 0x0011673A File Offset: 0x0011493A
		internal override string DataElementNameDefault
		{
			get
			{
				return "ReportSection" + this.m_publishingIndexInCollection.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17001BD5 RID: 7125
		// (get) Token: 0x06004230 RID: 16944 RVA: 0x00116756 File Offset: 0x00114956
		internal override DataElementOutputTypes DataElementOutputDefault
		{
			get
			{
				return DataElementOutputTypes.ContentsOnly;
			}
		}

		// Token: 0x17001BD6 RID: 7126
		// (get) Token: 0x06004231 RID: 16945 RVA: 0x00116759 File Offset: 0x00114959
		// (set) Token: 0x06004232 RID: 16946 RVA: 0x00116761 File Offset: 0x00114961
		internal bool NeedsOverallTotalPages
		{
			get
			{
				return this.m_needsOverallTotalPages;
			}
			set
			{
				this.m_needsOverallTotalPages = value;
			}
		}

		// Token: 0x17001BD7 RID: 7127
		// (get) Token: 0x06004233 RID: 16947 RVA: 0x0011676A File Offset: 0x0011496A
		// (set) Token: 0x06004234 RID: 16948 RVA: 0x00116772 File Offset: 0x00114972
		internal bool NeedsPageBreakTotalPages
		{
			get
			{
				return this.m_needsOverallTotalPages;
			}
			set
			{
				this.m_needsOverallTotalPages = value;
			}
		}

		// Token: 0x17001BD8 RID: 7128
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x0011677B File Offset: 0x0011497B
		// (set) Token: 0x06004236 RID: 16950 RVA: 0x00116783 File Offset: 0x00114983
		internal bool NeedsReportItemsOnPage
		{
			get
			{
				return this.m_needsReportItemsOnPage;
			}
			set
			{
				this.m_needsReportItemsOnPage = value;
			}
		}

		// Token: 0x17001BD9 RID: 7129
		// (get) Token: 0x06004237 RID: 16951 RVA: 0x0011678C File Offset: 0x0011498C
		// (set) Token: 0x06004238 RID: 16952 RVA: 0x00116794 File Offset: 0x00114994
		internal bool LayoutDirection
		{
			get
			{
				return this.m_layoutDirection;
			}
			set
			{
				this.m_layoutDirection = value;
			}
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x001167A0 File Offset: 0x001149A0
		internal override bool Initialize(InitializationContext context)
		{
			context.Location = Microsoft.ReportingServices.ReportPublishing.LocationFlags.None;
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.DataElementNameDefault;
			context.RegisterReportSection(this);
			context.ExprHostBuilder.ReportSectionStart();
			base.Initialize(context);
			base.ExprHostID = context.ExprHostBuilder.ReportSectionEnd();
			this.m_page.Initialize(context);
			this.BodyInitialize(context);
			context.ValidateToggleItems();
			this.m_page.PageHeaderFooterInitialize(context);
			context.UnRegisterReportSection();
			return false;
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x0011682C File Offset: 0x00114A2C
		internal void BodyInitialize(InitializationContext context)
		{
			context.RegisterReportItems(this.m_reportItems);
			this.m_textboxesInScope = context.GetCurrentReferencableTextboxes();
			this.m_variablesInScope = context.GetCurrentReferencableVariables();
			this.m_reportItems.Initialize(context);
			Microsoft.ReportingServices.ReportIntermediateFormat.Report report = context.Report;
			if (report.HasUserSortFilter || report.HasSubReports)
			{
				context.InitializingUserSorts = true;
				this.m_reportItems.InitializeRVDirectionDependentItems(context);
				context.EventSourcesWithDetailSortExpressionInitialize(null);
				List<DataSource> dataSources = report.DataSources;
				if (dataSources != null)
				{
					for (int i = 0; i < dataSources.Count; i++)
					{
						List<Microsoft.ReportingServices.ReportIntermediateFormat.DataSet> dataSets = dataSources[i].DataSets;
						if (dataSets != null)
						{
							for (int j = 0; j < dataSets.Count; j++)
							{
								context.ProcessUserSortScopes(dataSets[j].Name);
							}
						}
					}
				}
				context.ProcessUserSortScopes("0_ReportScope");
			}
			if (report.HasPreviousAggregates)
			{
				this.m_reportItems.DetermineGroupingExprValueCount(context, 0);
			}
			context.UnRegisterReportItems(this.m_reportItems);
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x00116924 File Offset: 0x00114B24
		internal override void TraverseScopes(IRIFScopeVisitor visitor)
		{
			foreach (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem in this.m_reportItems)
			{
				reportItem.TraverseScopes(visitor);
			}
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x00116970 File Offset: 0x00114B70
		public bool TextboxInScope(int sequenceIndex)
		{
			return SequenceIndex.GetBit(this.m_textboxesInScope, sequenceIndex, true);
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x0011697F File Offset: 0x00114B7F
		public void AddInScopeTextBox(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textbox)
		{
			if (this.m_inScopeTextBoxes == null)
			{
				this.m_inScopeTextBoxes = new List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>();
			}
			this.m_inScopeTextBoxes.Add(textbox);
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x001169A0 File Offset: 0x00114BA0
		public void ResetTextBoxImpls(OnDemandProcessingContext context)
		{
			if (this.m_inScopeTextBoxes != null)
			{
				for (int i = 0; i < this.m_inScopeTextBoxes.Count; i++)
				{
					this.m_inScopeTextBoxes[i].ResetTextBoxImpl(context);
				}
			}
		}

		// Token: 0x17001BDA RID: 7130
		// (get) Token: 0x0600423F RID: 16959 RVA: 0x001169DD File Offset: 0x00114BDD
		// (set) Token: 0x06004240 RID: 16960 RVA: 0x001169E0 File Offset: 0x00114BE0
		public bool NeedToCacheDataRows
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x001169E2 File Offset: 0x00114BE2
		public bool VariableInScope(int sequenceIndex)
		{
			return SequenceIndex.GetBit(this.m_variablesInScope, sequenceIndex, true);
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x001169F1 File Offset: 0x00114BF1
		public void AddInScopeEventSource(IInScopeEventSource eventSource)
		{
			Global.Tracer.Assert(false, "Top level event sources should be registered on the Report, not ReportSection");
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x00116A03 File Offset: 0x00114C03
		internal void SetTextboxesInScope(byte[] items)
		{
			this.m_textboxesInScope = items;
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x00116A0C File Offset: 0x00114C0C
		internal void SetInScopeTextBoxes(List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox> items)
		{
			this.m_inScopeTextBoxes = items;
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x00116A18 File Offset: 0x00114C18
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ReportSection.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.InScopeTextBoxes)
				{
					if (memberName <= MemberName.LayoutDirection)
					{
						if (memberName == MemberName.ReportItems)
						{
							writer.Write(this.m_reportItems);
							continue;
						}
						if (memberName == MemberName.LayoutDirection)
						{
							writer.Write(this.m_layoutDirection);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Page)
						{
							writer.Write(this.m_page);
							continue;
						}
						if (memberName == MemberName.InScopeTextBoxes)
						{
							writer.WriteListOfReferences(this.m_inScopeTextBoxes);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.TextboxesInScope)
				{
					if (memberName == MemberName.VariablesInScope)
					{
						writer.Write(this.m_variablesInScope);
						continue;
					}
					if (memberName == MemberName.TextboxesInScope)
					{
						writer.Write(this.m_textboxesInScope);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.NeedsReportItemsOnPage)
					{
						writer.Write(this.m_needsReportItemsOnPage);
						continue;
					}
					if (memberName == MemberName.NeedsOverallTotalPages)
					{
						writer.Write(this.m_needsOverallTotalPages);
						continue;
					}
					if (memberName == MemberName.NeedsPageBreakTotalPages)
					{
						writer.Write(this.m_needsPageBreakTotalPages);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x00116B68 File Offset: 0x00114D68
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ReportSection.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName > MemberName.VariablesInScope)
				{
					if (memberName <= MemberName.NeedsTotalPages)
					{
						if (memberName == MemberName.TextboxesInScope)
						{
							this.m_textboxesInScope = reader.ReadByteArray();
							continue;
						}
						if (memberName != MemberName.NeedsTotalPages)
						{
							goto IL_013F;
						}
					}
					else
					{
						if (memberName == MemberName.NeedsReportItemsOnPage)
						{
							this.m_needsReportItemsOnPage = reader.ReadBoolean();
							continue;
						}
						if (memberName != MemberName.NeedsOverallTotalPages)
						{
							if (memberName != MemberName.NeedsPageBreakTotalPages)
							{
								goto IL_013F;
							}
							this.m_needsPageBreakTotalPages = reader.ReadBoolean();
							continue;
						}
					}
					this.m_needsOverallTotalPages = reader.ReadBoolean();
					continue;
				}
				if (memberName <= MemberName.LayoutDirection)
				{
					if (memberName == MemberName.ReportItems)
					{
						this.m_reportItems = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.LayoutDirection)
					{
						this.m_layoutDirection = reader.ReadBoolean();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Page)
					{
						this.m_page = (Page)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.InScopeTextBoxes)
					{
						this.m_inScopeTextBoxes = reader.ReadGenericListOfReferences<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>(this);
						continue;
					}
					if (memberName == MemberName.VariablesInScope)
					{
						this.m_variablesInScope = reader.ReadByteArray();
						continue;
					}
				}
				IL_013F:
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x00116CCC File Offset: 0x00114ECC
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ReportSection.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.InScopeTextBoxes)
					{
						if (this.m_inScopeTextBoxes == null)
						{
							this.m_inScopeTextBoxes = new List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>();
						}
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable;
						referenceableItems.TryGetValue(memberReference.RefID, out referenceable);
						Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textBox = (Microsoft.ReportingServices.ReportIntermediateFormat.TextBox)referenceable;
						this.m_inScopeTextBoxes.Add(textBox);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x00116D78 File Offset: 0x00114F78
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportSection;
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x00116D80 File Offset: 0x00114F80
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (ReportSection.m_Declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportSection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Page, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Page),
					new MemberInfo(MemberName.ReportItems, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemCollection),
					new MemberInfo(MemberName.TextboxesInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
					new MemberInfo(MemberName.VariablesInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
					new ReadOnlyMemberInfo(MemberName.NeedsTotalPages, Token.Boolean),
					new MemberInfo(MemberName.NeedsOverallTotalPages, Token.Boolean),
					new MemberInfo(MemberName.NeedsReportItemsOnPage, Token.Boolean),
					new MemberInfo(MemberName.InScopeTextBoxes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextBox),
					new MemberInfo(MemberName.NeedsPageBreakTotalPages, Token.Boolean),
					new MemberInfo(MemberName.LayoutDirection, Token.Boolean, Lifetime.AddedIn(200))
				});
			}
			return ReportSection.m_Declaration;
		}

		// Token: 0x04001E3C RID: 7740
		private Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection m_reportItems;

		// Token: 0x04001E3D RID: 7741
		private Page m_page;

		// Token: 0x04001E3E RID: 7742
		private byte[] m_textboxesInScope;

		// Token: 0x04001E3F RID: 7743
		private byte[] m_variablesInScope;

		// Token: 0x04001E40 RID: 7744
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox> m_inScopeTextBoxes;

		// Token: 0x04001E41 RID: 7745
		private bool m_needsOverallTotalPages;

		// Token: 0x04001E42 RID: 7746
		private bool m_needsPageBreakTotalPages;

		// Token: 0x04001E43 RID: 7747
		private bool m_needsReportItemsOnPage;

		// Token: 0x04001E44 RID: 7748
		private bool m_layoutDirection;

		// Token: 0x04001E45 RID: 7749
		[NonSerialized]
		private int m_publishingIndexInCollection = -1;

		// Token: 0x04001E46 RID: 7750
		[NonSerialized]
		internal const int UpgradedExprHostId = 0;

		// Token: 0x04001E47 RID: 7751
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ReportSection.GetDeclaration();
	}
}
