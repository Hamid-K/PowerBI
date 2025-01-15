using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006F8 RID: 1784
	[Serializable]
	internal sealed class TableGroup : ReportHierarchyNode, IRunningValueHolder, IPageBreakItem
	{
		// Token: 0x0600630C RID: 25356 RVA: 0x0018A0FA File Offset: 0x001882FA
		internal TableGroup()
		{
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x0018A110 File Offset: 0x00188310
		internal TableGroup(int id, Table tableDef)
			: base(id, tableDef)
		{
			this.m_runningValues = new RunningValueInfoList();
		}

		// Token: 0x17002304 RID: 8964
		// (get) Token: 0x0600630E RID: 25358 RVA: 0x0018A133 File Offset: 0x00188333
		// (set) Token: 0x0600630F RID: 25359 RVA: 0x0018A140 File Offset: 0x00188340
		internal TableGroup SubGroup
		{
			get
			{
				return (TableGroup)this.m_innerHierarchy;
			}
			set
			{
				this.m_innerHierarchy = value;
			}
		}

		// Token: 0x17002305 RID: 8965
		// (get) Token: 0x06006310 RID: 25360 RVA: 0x0018A149 File Offset: 0x00188349
		// (set) Token: 0x06006311 RID: 25361 RVA: 0x0018A151 File Offset: 0x00188351
		internal TableRowList HeaderRows
		{
			get
			{
				return this.m_headerRows;
			}
			set
			{
				this.m_headerRows = value;
			}
		}

		// Token: 0x17002306 RID: 8966
		// (get) Token: 0x06006312 RID: 25362 RVA: 0x0018A15A File Offset: 0x0018835A
		// (set) Token: 0x06006313 RID: 25363 RVA: 0x0018A162 File Offset: 0x00188362
		internal bool HeaderRepeatOnNewPage
		{
			get
			{
				return this.m_headerRepeatOnNewPage;
			}
			set
			{
				this.m_headerRepeatOnNewPage = value;
			}
		}

		// Token: 0x17002307 RID: 8967
		// (get) Token: 0x06006314 RID: 25364 RVA: 0x0018A16B File Offset: 0x0018836B
		// (set) Token: 0x06006315 RID: 25365 RVA: 0x0018A173 File Offset: 0x00188373
		internal TableRowList FooterRows
		{
			get
			{
				return this.m_footerRows;
			}
			set
			{
				this.m_footerRows = value;
			}
		}

		// Token: 0x17002308 RID: 8968
		// (get) Token: 0x06006316 RID: 25366 RVA: 0x0018A17C File Offset: 0x0018837C
		// (set) Token: 0x06006317 RID: 25367 RVA: 0x0018A184 File Offset: 0x00188384
		internal bool FooterRepeatOnNewPage
		{
			get
			{
				return this.m_footerRepeatOnNewPage;
			}
			set
			{
				this.m_footerRepeatOnNewPage = value;
			}
		}

		// Token: 0x17002309 RID: 8969
		// (get) Token: 0x06006318 RID: 25368 RVA: 0x0018A18D File Offset: 0x0018838D
		// (set) Token: 0x06006319 RID: 25369 RVA: 0x0018A195 File Offset: 0x00188395
		internal Visibility Visibility
		{
			get
			{
				return this.m_visibility;
			}
			set
			{
				this.m_visibility = value;
			}
		}

		// Token: 0x1700230A RID: 8970
		// (get) Token: 0x0600631A RID: 25370 RVA: 0x0018A19E File Offset: 0x0018839E
		// (set) Token: 0x0600631B RID: 25371 RVA: 0x0018A1A6 File Offset: 0x001883A6
		internal bool PropagatedPageBreakAtStart
		{
			get
			{
				return this.m_propagatedPageBreakAtStart;
			}
			set
			{
				this.m_propagatedPageBreakAtStart = value;
			}
		}

		// Token: 0x1700230B RID: 8971
		// (get) Token: 0x0600631C RID: 25372 RVA: 0x0018A1AF File Offset: 0x001883AF
		// (set) Token: 0x0600631D RID: 25373 RVA: 0x0018A1B7 File Offset: 0x001883B7
		internal bool PropagatedPageBreakAtEnd
		{
			get
			{
				return this.m_propagatedPageBreakAtEnd;
			}
			set
			{
				this.m_propagatedPageBreakAtEnd = value;
			}
		}

		// Token: 0x1700230C RID: 8972
		// (get) Token: 0x0600631E RID: 25374 RVA: 0x0018A1C0 File Offset: 0x001883C0
		// (set) Token: 0x0600631F RID: 25375 RVA: 0x0018A1C8 File Offset: 0x001883C8
		internal RunningValueInfoList RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
			set
			{
				this.m_runningValues = value;
			}
		}

		// Token: 0x1700230D RID: 8973
		// (get) Token: 0x06006320 RID: 25376 RVA: 0x0018A1D1 File Offset: 0x001883D1
		// (set) Token: 0x06006321 RID: 25377 RVA: 0x0018A1D9 File Offset: 0x001883D9
		internal string RenderingModelID
		{
			get
			{
				return this.m_renderingModelID;
			}
			set
			{
				this.m_renderingModelID = value;
			}
		}

		// Token: 0x1700230E RID: 8974
		// (get) Token: 0x06006322 RID: 25378 RVA: 0x0018A1E2 File Offset: 0x001883E2
		// (set) Token: 0x06006323 RID: 25379 RVA: 0x0018A1EA File Offset: 0x001883EA
		internal bool HasExprHost
		{
			get
			{
				return this.m_hasExprHost;
			}
			set
			{
				this.m_hasExprHost = value;
			}
		}

		// Token: 0x1700230F RID: 8975
		// (get) Token: 0x06006324 RID: 25380 RVA: 0x0018A1F3 File Offset: 0x001883F3
		internal TableGroupExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17002310 RID: 8976
		// (get) Token: 0x06006325 RID: 25381 RVA: 0x0018A1FB File Offset: 0x001883FB
		// (set) Token: 0x06006326 RID: 25382 RVA: 0x0018A203 File Offset: 0x00188403
		internal int StartPage
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		// Token: 0x17002311 RID: 8977
		// (get) Token: 0x06006327 RID: 25383 RVA: 0x0018A20C File Offset: 0x0018840C
		// (set) Token: 0x06006328 RID: 25384 RVA: 0x0018A214 File Offset: 0x00188414
		internal int EndPage
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		// Token: 0x17002312 RID: 8978
		// (get) Token: 0x06006329 RID: 25385 RVA: 0x0018A21D File Offset: 0x0018841D
		// (set) Token: 0x0600632A RID: 25386 RVA: 0x0018A225 File Offset: 0x00188425
		internal bool StartHidden
		{
			get
			{
				return this.m_startHidden;
			}
			set
			{
				this.m_startHidden = value;
			}
		}

		// Token: 0x17002313 RID: 8979
		// (get) Token: 0x0600632B RID: 25387 RVA: 0x0018A22E File Offset: 0x0018842E
		internal double HeaderHeightValue
		{
			get
			{
				if (this.m_headerRows != null)
				{
					return this.m_headerRows.GetHeightValue();
				}
				return 0.0;
			}
		}

		// Token: 0x17002314 RID: 8980
		// (get) Token: 0x0600632C RID: 25388 RVA: 0x0018A24D File Offset: 0x0018844D
		internal double FooterHeightValue
		{
			get
			{
				if (this.m_footerRows != null)
				{
					return this.m_footerRows.GetHeightValue();
				}
				return 0.0;
			}
		}

		// Token: 0x0600632D RID: 25389 RVA: 0x0018A26C File Offset: 0x0018846C
		internal void Initialize(int numberOfColumns, TableDetail tableDetail, TableGroup detailGroup, InitializationContext context, ref double tableHeight, bool[] tableColumnVisibility)
		{
			Global.Tracer.Assert(this.m_grouping != null);
			context.Location |= LocationFlags.InGrouping;
			context.ExprHostBuilder.TableGroupStart(this.m_grouping.Name);
			context.RegisterGroupingScope(this.m_grouping.Name, this.m_grouping.SimpleGroupExpressions, this.m_grouping.Aggregates, this.m_grouping.PostSortAggregates, this.m_grouping.RecursiveAggregates, this.m_grouping);
			base.Initialize(context);
			context.RegisterRunningValues(this.m_runningValues);
			this.RegisterHeaderAndFooter(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, true, false);
			}
			this.InitializeHeaderAndFooter(numberOfColumns, context, ref tableHeight, tableColumnVisibility);
			this.InitializeSubGroupsOrDetail(numberOfColumns, tableDetail, detailGroup, context, ref tableHeight, tableColumnVisibility);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			this.UnRegisterHeaderAndFooter(context);
			context.UnRegisterRunningValues(this.m_runningValues);
			context.UnRegisterGroupingScope(this.m_grouping.Name);
			this.m_hasExprHost = context.ExprHostBuilder.TableGroupEnd();
		}

		// Token: 0x0600632E RID: 25390 RVA: 0x0018A398 File Offset: 0x00188598
		internal void RegisterReceiver(InitializationContext context, TableDetail tableDetail)
		{
			this.RegisterHeaderAndFooter(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.RegisterReceiver(context, true);
			}
			this.RegisterHeaderAndFooterReceiver(context);
			this.RegisterSubGroupsOrDetailReceiver(context, tableDetail);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			this.UnRegisterHeaderAndFooter(context);
		}

		// Token: 0x0600632F RID: 25391 RVA: 0x0018A3EC File Offset: 0x001885EC
		private void RegisterHeaderAndFooter(InitializationContext context)
		{
			if (this.m_headerRows != null)
			{
				this.m_headerRows.Register(context);
			}
			if (this.m_footerRows != null)
			{
				this.m_footerRows.Register(context);
			}
		}

		// Token: 0x06006330 RID: 25392 RVA: 0x0018A416 File Offset: 0x00188616
		private void UnRegisterHeaderAndFooter(InitializationContext context)
		{
			if (this.m_footerRows != null)
			{
				this.m_footerRows.UnRegister(context);
			}
			if (this.m_headerRows != null)
			{
				this.m_headerRows.UnRegister(context);
			}
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x0018A440 File Offset: 0x00188640
		private void InitializeHeaderAndFooter(int numberOfColumns, InitializationContext context, ref double tableHeight, bool[] tableColumnVisibility)
		{
			context.ExprHostBuilder.TableRowVisibilityHiddenExpressionsStart();
			if (this.m_headerRows != null)
			{
				for (int i = 0; i < this.m_headerRows.Count; i++)
				{
					Global.Tracer.Assert(this.m_headerRows[i] != null);
					this.m_headerRows[i].Initialize(true, numberOfColumns, context, ref tableHeight, tableColumnVisibility);
				}
			}
			if (this.m_footerRows != null)
			{
				for (int j = 0; j < this.m_footerRows.Count; j++)
				{
					Global.Tracer.Assert(this.m_footerRows[j] != null);
					this.m_footerRows[j].Initialize(true, numberOfColumns, context, ref tableHeight, tableColumnVisibility);
				}
			}
			context.ExprHostBuilder.TableRowVisibilityHiddenExpressionsEnd();
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x0018A504 File Offset: 0x00188704
		private void RegisterHeaderAndFooterReceiver(InitializationContext context)
		{
			if (this.m_headerRows != null)
			{
				for (int i = 0; i < this.m_headerRows.Count; i++)
				{
					Global.Tracer.Assert(this.m_headerRows[i] != null);
					this.m_headerRows[i].RegisterReceiver(context);
				}
			}
			if (this.m_footerRows != null)
			{
				for (int j = 0; j < this.m_footerRows.Count; j++)
				{
					Global.Tracer.Assert(this.m_footerRows[j] != null);
					this.m_footerRows[j].RegisterReceiver(context);
				}
			}
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x0018A5A4 File Offset: 0x001887A4
		private void InitializeSubGroupsOrDetail(int numberOfColumns, TableDetail tableDetail, TableGroup detailGroup, InitializationContext context, ref double tableHeight, bool[] tableColumnVisibility)
		{
			if (detailGroup != null && this.SubGroup == null)
			{
				this.SubGroup = detailGroup;
				detailGroup = null;
			}
			if (this.SubGroup != null)
			{
				this.SubGroup.Initialize(numberOfColumns, tableDetail, detailGroup, context, ref tableHeight, tableColumnVisibility);
				return;
			}
			if (tableDetail != null)
			{
				tableDetail.Initialize(numberOfColumns, context, ref tableHeight, tableColumnVisibility);
			}
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x0018A5F3 File Offset: 0x001887F3
		private void RegisterSubGroupsOrDetailReceiver(InitializationContext context, TableDetail tableDetail)
		{
			if (this.SubGroup != null)
			{
				this.SubGroup.RegisterReceiver(context, tableDetail);
				return;
			}
			if (tableDetail != null)
			{
				tableDetail.RegisterReceiver(context);
			}
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x0018A618 File Offset: 0x00188818
		internal void CalculatePropagatedFlags(out bool groupPageBreakAtStart, out bool groupPageBreakAtEnd)
		{
			if (this.SubGroup == null)
			{
				groupPageBreakAtStart = this.m_grouping.PageBreakAtStart;
				groupPageBreakAtEnd = this.m_grouping.PageBreakAtEnd;
				return;
			}
			this.SubGroup.CalculatePropagatedFlags(out groupPageBreakAtStart, out groupPageBreakAtEnd);
			groupPageBreakAtStart = groupPageBreakAtStart || this.m_grouping.PageBreakAtStart;
			groupPageBreakAtEnd = groupPageBreakAtEnd || this.m_grouping.PageBreakAtEnd;
			bool flag = true;
			if (this.SubGroup.HeaderRows != null)
			{
				flag = this.SubGroup.HeaderRepeatOnNewPage;
			}
			this.m_propagatedPageBreakAtStart = this.SubGroup.Grouping.PageBreakAtStart || (this.SubGroup.PropagatedPageBreakAtStart && flag);
			flag = true;
			if (this.SubGroup.FooterRows != null)
			{
				flag = this.SubGroup.FooterRepeatOnNewPage;
			}
			this.m_propagatedPageBreakAtEnd = this.SubGroup.Grouping.PageBreakAtEnd || (this.SubGroup.PropagatedPageBreakAtEnd && flag);
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x0018A703 File Offset: 0x00188903
		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x0018A70B File Offset: 0x0018890B
		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_runningValues != null);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x0018A734 File Offset: 0x00188934
		bool IPageBreakItem.IgnorePageBreaks()
		{
			return base.IgnorePageBreaks(this.m_visibility);
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x0018A744 File Offset: 0x00188944
		internal void SetExprHost(TableGroupExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null && this.m_hasExprHost);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			base.ReportHierarchyNodeSetExprHost(this.m_exprHost, reportObjectModel);
			if (this.m_exprHost.TableRowVisibilityHiddenExpressions != null)
			{
				this.m_exprHost.TableRowVisibilityHiddenExpressions.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x0600633A RID: 25402 RVA: 0x0018A7A8 File Offset: 0x001889A8
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportHierarchyNode, new MemberInfoList
			{
				new MemberInfo(MemberName.HeaderRows, ObjectType.TableRowList),
				new MemberInfo(MemberName.HeaderRepeatOnNewPage, Token.Boolean),
				new MemberInfo(MemberName.FooterRows, ObjectType.TableRowList),
				new MemberInfo(MemberName.FooterRepeatOnNewPage, Token.Boolean),
				new MemberInfo(MemberName.Visibility, ObjectType.Visibility),
				new MemberInfo(MemberName.PropagatedPageBreakAtStart, Token.Boolean),
				new MemberInfo(MemberName.PropagatedPageBreakAtEnd, Token.Boolean),
				new MemberInfo(MemberName.RunningValues, ObjectType.RunningValueInfoList),
				new MemberInfo(MemberName.HasExprHost, Token.Boolean)
			});
		}

		// Token: 0x040031E3 RID: 12771
		private TableRowList m_headerRows;

		// Token: 0x040031E4 RID: 12772
		private bool m_headerRepeatOnNewPage;

		// Token: 0x040031E5 RID: 12773
		private TableRowList m_footerRows;

		// Token: 0x040031E6 RID: 12774
		private bool m_footerRepeatOnNewPage;

		// Token: 0x040031E7 RID: 12775
		private Visibility m_visibility;

		// Token: 0x040031E8 RID: 12776
		private bool m_propagatedPageBreakAtStart;

		// Token: 0x040031E9 RID: 12777
		private bool m_propagatedPageBreakAtEnd;

		// Token: 0x040031EA RID: 12778
		private RunningValueInfoList m_runningValues;

		// Token: 0x040031EB RID: 12779
		private bool m_hasExprHost;

		// Token: 0x040031EC RID: 12780
		[NonSerialized]
		private TableGroupExprHost m_exprHost;

		// Token: 0x040031ED RID: 12781
		[NonSerialized]
		private bool m_startHidden;

		// Token: 0x040031EE RID: 12782
		[NonSerialized]
		private string m_renderingModelID;

		// Token: 0x040031EF RID: 12783
		[NonSerialized]
		private int m_startPage = -1;

		// Token: 0x040031F0 RID: 12784
		[NonSerialized]
		private int m_endPage = -1;
	}
}
