using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006F9 RID: 1785
	[Serializable]
	internal sealed class TableDetail : IDOwner, IRunningValueHolder
	{
		// Token: 0x0600633B RID: 25403 RVA: 0x0018A868 File Offset: 0x00188A68
		internal TableDetail()
		{
		}

		// Token: 0x0600633C RID: 25404 RVA: 0x0018A870 File Offset: 0x00188A70
		internal TableDetail(int id)
			: base(id)
		{
			this.m_runningValues = new RunningValueInfoList();
		}

		// Token: 0x17002315 RID: 8981
		// (get) Token: 0x0600633D RID: 25405 RVA: 0x0018A884 File Offset: 0x00188A84
		// (set) Token: 0x0600633E RID: 25406 RVA: 0x0018A88C File Offset: 0x00188A8C
		internal TableRowList DetailRows
		{
			get
			{
				return this.m_detailRows;
			}
			set
			{
				this.m_detailRows = value;
			}
		}

		// Token: 0x17002316 RID: 8982
		// (get) Token: 0x0600633F RID: 25407 RVA: 0x0018A895 File Offset: 0x00188A95
		// (set) Token: 0x06006340 RID: 25408 RVA: 0x0018A89D File Offset: 0x00188A9D
		internal Sorting Sorting
		{
			get
			{
				return this.m_sorting;
			}
			set
			{
				this.m_sorting = value;
			}
		}

		// Token: 0x17002317 RID: 8983
		// (get) Token: 0x06006341 RID: 25409 RVA: 0x0018A8A6 File Offset: 0x00188AA6
		// (set) Token: 0x06006342 RID: 25410 RVA: 0x0018A8AE File Offset: 0x00188AAE
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

		// Token: 0x17002318 RID: 8984
		// (get) Token: 0x06006343 RID: 25411 RVA: 0x0018A8B7 File Offset: 0x00188AB7
		// (set) Token: 0x06006344 RID: 25412 RVA: 0x0018A8BF File Offset: 0x00188ABF
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

		// Token: 0x17002319 RID: 8985
		// (get) Token: 0x06006345 RID: 25413 RVA: 0x0018A8C8 File Offset: 0x00188AC8
		// (set) Token: 0x06006346 RID: 25414 RVA: 0x0018A8D0 File Offset: 0x00188AD0
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

		// Token: 0x1700231A RID: 8986
		// (get) Token: 0x06006347 RID: 25415 RVA: 0x0018A8D9 File Offset: 0x00188AD9
		// (set) Token: 0x06006348 RID: 25416 RVA: 0x0018A8E1 File Offset: 0x00188AE1
		internal bool SimpleDetailRows
		{
			get
			{
				return this.m_simpleDetailRows;
			}
			set
			{
				this.m_simpleDetailRows = value;
			}
		}

		// Token: 0x1700231B RID: 8987
		// (get) Token: 0x06006349 RID: 25417 RVA: 0x0018A8EA File Offset: 0x00188AEA
		internal TableGroupExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x1700231C RID: 8988
		// (get) Token: 0x0600634A RID: 25418 RVA: 0x0018A8F2 File Offset: 0x00188AF2
		// (set) Token: 0x0600634B RID: 25419 RVA: 0x0018A8FA File Offset: 0x00188AFA
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

		// Token: 0x0600634C RID: 25420 RVA: 0x0018A904 File Offset: 0x00188B04
		internal void Initialize(int numberOfColumns, InitializationContext context, ref double tableHeight, bool[] tableColumnVisibility)
		{
			context.Location |= LocationFlags.InDetail;
			context.DetailObjectType = ObjectType.Table;
			context.ExprHostBuilder.TableGroupStart("TableDetails");
			context.RegisterRunningValues(this.m_runningValues);
			if (this.m_sorting != null)
			{
				this.m_sorting.Initialize(context);
			}
			if (this.m_detailRows != null)
			{
				this.m_detailRows.Register(context);
			}
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, true, false);
			}
			this.InitializeDetailRows(numberOfColumns, context, ref tableHeight, tableColumnVisibility);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			if (this.m_detailRows != null)
			{
				this.m_detailRows.UnRegister(context);
			}
			context.UnRegisterRunningValues(this.m_runningValues);
			this.m_hasExprHost = context.ExprHostBuilder.TableGroupEnd();
		}

		// Token: 0x0600634D RID: 25421 RVA: 0x0018A9DC File Offset: 0x00188BDC
		private void InitializeDetailRows(int numberOfColumns, InitializationContext context, ref double tableHeight, bool[] tableColumnVisibility)
		{
			if (this.m_detailRows != null)
			{
				if (!context.MergeOnePass || 1 >= context.DataRegionCount)
				{
					this.m_simpleDetailRows = true;
				}
				context.ExprHostBuilder.TableRowVisibilityHiddenExpressionsStart();
				for (int i = 0; i < this.m_detailRows.Count; i++)
				{
					Global.Tracer.Assert(this.m_detailRows[i] != null);
					if (!this.m_detailRows[i].Initialize(true, numberOfColumns, context, ref tableHeight, tableColumnVisibility))
					{
						this.m_simpleDetailRows = false;
					}
				}
				context.ExprHostBuilder.TableRowVisibilityHiddenExpressionsEnd();
			}
		}

		// Token: 0x0600634E RID: 25422 RVA: 0x0018AA78 File Offset: 0x00188C78
		internal void RegisterReceiver(InitializationContext context)
		{
			if (this.m_detailRows != null)
			{
				this.m_detailRows.Register(context);
				if (this.m_visibility != null)
				{
					this.m_visibility.Initialize(context, true, false);
				}
				for (int i = 0; i < this.m_detailRows.Count; i++)
				{
					Global.Tracer.Assert(this.m_detailRows[i] != null);
					this.m_detailRows[i].RegisterReceiver(context);
				}
				if (this.m_visibility != null)
				{
					this.m_visibility.UnRegisterReceiver(context);
				}
				this.m_detailRows.UnRegister(context);
			}
		}

		// Token: 0x0600634F RID: 25423 RVA: 0x0018AB13 File Offset: 0x00188D13
		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x06006350 RID: 25424 RVA: 0x0018AB1B File Offset: 0x00188D1B
		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_runningValues != null);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		// Token: 0x06006351 RID: 25425 RVA: 0x0018AB44 File Offset: 0x00188D44
		internal void SetExprHost(TableGroupExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null && this.m_hasExprHost);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_exprHost.TableRowVisibilityHiddenExpressions != null)
			{
				this.m_exprHost.TableRowVisibilityHiddenExpressions.SetReportObjectModel(reportObjectModel);
			}
			if (this.m_exprHost.SortingHost != null)
			{
				Global.Tracer.Assert(this.m_sorting != null);
				this.m_sorting.SetExprHost(exprHost.SortingHost, reportObjectModel);
			}
		}

		// Token: 0x06006352 RID: 25426 RVA: 0x0018ABD0 File Offset: 0x00188DD0
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.DetailRows, ObjectType.TableRowList),
				new MemberInfo(MemberName.Sorting, ObjectType.Sorting),
				new MemberInfo(MemberName.Visibility, ObjectType.Visibility),
				new MemberInfo(MemberName.RunningValues, ObjectType.RunningValueInfoList),
				new MemberInfo(MemberName.HasExprHost, Token.Boolean),
				new MemberInfo(MemberName.SimpleDetailRows, Token.Boolean)
			});
		}

		// Token: 0x040031F1 RID: 12785
		private TableRowList m_detailRows;

		// Token: 0x040031F2 RID: 12786
		private Sorting m_sorting;

		// Token: 0x040031F3 RID: 12787
		private Visibility m_visibility;

		// Token: 0x040031F4 RID: 12788
		private RunningValueInfoList m_runningValues;

		// Token: 0x040031F5 RID: 12789
		private bool m_hasExprHost;

		// Token: 0x040031F6 RID: 12790
		private bool m_simpleDetailRows;

		// Token: 0x040031F7 RID: 12791
		[NonSerialized]
		private TableGroupExprHost m_exprHost;

		// Token: 0x040031F8 RID: 12792
		[NonSerialized]
		private bool m_startHidden;
	}
}
