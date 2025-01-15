using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006F3 RID: 1779
	[Serializable]
	internal sealed class MatrixHeading : PivotHeading, IPageBreakItem
	{
		// Token: 0x06006283 RID: 25219 RVA: 0x00188C37 File Offset: 0x00186E37
		internal MatrixHeading()
		{
		}

		// Token: 0x06006284 RID: 25220 RVA: 0x00188C46 File Offset: 0x00186E46
		internal MatrixHeading(int id, int idForReportItems, Matrix matrixDef)
			: base(id, matrixDef)
		{
			this.m_reportItems = new ReportItemCollection(idForReportItems, false);
		}

		// Token: 0x170022D0 RID: 8912
		// (get) Token: 0x06006285 RID: 25221 RVA: 0x00188C64 File Offset: 0x00186E64
		// (set) Token: 0x06006286 RID: 25222 RVA: 0x00188C71 File Offset: 0x00186E71
		internal new MatrixHeading SubHeading
		{
			get
			{
				return (MatrixHeading)this.m_innerHierarchy;
			}
			set
			{
				this.m_innerHierarchy = value;
			}
		}

		// Token: 0x170022D1 RID: 8913
		// (get) Token: 0x06006287 RID: 25223 RVA: 0x00188C7A File Offset: 0x00186E7A
		// (set) Token: 0x06006288 RID: 25224 RVA: 0x00188C82 File Offset: 0x00186E82
		internal string Size
		{
			get
			{
				return this.m_size;
			}
			set
			{
				this.m_size = value;
			}
		}

		// Token: 0x170022D2 RID: 8914
		// (get) Token: 0x06006289 RID: 25225 RVA: 0x00188C8B File Offset: 0x00186E8B
		// (set) Token: 0x0600628A RID: 25226 RVA: 0x00188C93 File Offset: 0x00186E93
		internal double SizeValue
		{
			get
			{
				return this.m_sizeValue;
			}
			set
			{
				this.m_sizeValue = value;
			}
		}

		// Token: 0x170022D3 RID: 8915
		// (get) Token: 0x0600628B RID: 25227 RVA: 0x00188C9C File Offset: 0x00186E9C
		// (set) Token: 0x0600628C RID: 25228 RVA: 0x00188CA4 File Offset: 0x00186EA4
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

		// Token: 0x170022D4 RID: 8916
		// (get) Token: 0x0600628D RID: 25229 RVA: 0x00188CAD File Offset: 0x00186EAD
		internal Microsoft.ReportingServices.ReportProcessing.ReportItem ReportItem
		{
			get
			{
				if (this.m_reportItems != null && 0 < this.m_reportItems.Count)
				{
					return this.m_reportItems[0];
				}
				return null;
			}
		}

		// Token: 0x170022D5 RID: 8917
		// (get) Token: 0x0600628E RID: 25230 RVA: 0x00188CD3 File Offset: 0x00186ED3
		// (set) Token: 0x0600628F RID: 25231 RVA: 0x00188CDB File Offset: 0x00186EDB
		internal ReportItemCollection ReportItems
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

		// Token: 0x170022D6 RID: 8918
		// (get) Token: 0x06006290 RID: 25232 RVA: 0x00188CE4 File Offset: 0x00186EE4
		// (set) Token: 0x06006291 RID: 25233 RVA: 0x00188CEC File Offset: 0x00186EEC
		internal bool OwcGroupExpression
		{
			get
			{
				return this.m_owcGroupExpression;
			}
			set
			{
				this.m_owcGroupExpression = value;
			}
		}

		// Token: 0x170022D7 RID: 8919
		// (get) Token: 0x06006292 RID: 25234 RVA: 0x00188CF5 File Offset: 0x00186EF5
		// (set) Token: 0x06006293 RID: 25235 RVA: 0x00188CFD File Offset: 0x00186EFD
		internal bool InFirstPage
		{
			get
			{
				return this.m_inFirstPage;
			}
			set
			{
				this.m_inFirstPage = value;
			}
		}

		// Token: 0x170022D8 RID: 8920
		// (get) Token: 0x06006294 RID: 25236 RVA: 0x00188D06 File Offset: 0x00186F06
		// (set) Token: 0x06006295 RID: 25237 RVA: 0x00188D0E File Offset: 0x00186F0E
		internal BoolList FirstHeadingInstances
		{
			get
			{
				return this.m_firstHeadingInstances;
			}
			set
			{
				this.m_firstHeadingInstances = value;
			}
		}

		// Token: 0x170022D9 RID: 8921
		// (get) Token: 0x06006296 RID: 25238 RVA: 0x00188D17 File Offset: 0x00186F17
		// (set) Token: 0x06006297 RID: 25239 RVA: 0x00188D1F File Offset: 0x00186F1F
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

		// Token: 0x170022DA RID: 8922
		// (get) Token: 0x06006298 RID: 25240 RVA: 0x00188D28 File Offset: 0x00186F28
		// (set) Token: 0x06006299 RID: 25241 RVA: 0x00188D30 File Offset: 0x00186F30
		internal string[] RenderingModelIDs
		{
			get
			{
				return this.m_renderingModelIDs;
			}
			set
			{
				this.m_renderingModelIDs = value;
			}
		}

		// Token: 0x170022DB RID: 8923
		// (get) Token: 0x0600629A RID: 25242 RVA: 0x00188D39 File Offset: 0x00186F39
		// (set) Token: 0x0600629B RID: 25243 RVA: 0x00188D41 File Offset: 0x00186F41
		internal ReportSize SizeForRendering
		{
			get
			{
				return this.m_sizeForRendering;
			}
			set
			{
				this.m_sizeForRendering = value;
			}
		}

		// Token: 0x170022DC RID: 8924
		// (get) Token: 0x0600629C RID: 25244 RVA: 0x00188D4A File Offset: 0x00186F4A
		internal MatrixDynamicGroupExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170022DD RID: 8925
		// (get) Token: 0x0600629D RID: 25245 RVA: 0x00188D52 File Offset: 0x00186F52
		// (set) Token: 0x0600629E RID: 25246 RVA: 0x00188D5A File Offset: 0x00186F5A
		internal bool InOutermostSubtotalCell
		{
			get
			{
				return this.m_inOutermostSubtotalCell;
			}
			set
			{
				this.m_inOutermostSubtotalCell = value;
			}
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x00188D64 File Offset: 0x00186F64
		internal int DynamicInitialize(bool column, int level, InitializationContext context, ref double cornerSize)
		{
			this.m_level = level;
			this.m_isColumn = column;
			this.m_sizeValue = context.ValidateSize(ref this.m_size, column ? "Height" : "Width");
			cornerSize = Math.Round(cornerSize + this.m_sizeValue, Validator.DecimalPrecision);
			if (this.m_grouping == null)
			{
				if (this.SubHeading != null)
				{
					context.RegisterReportItems(this.m_reportItems);
					this.SubHeading.DynamicInitialize(column, ++level, context, ref cornerSize);
					context.UnRegisterReportItems(this.m_reportItems);
				}
				return 1;
			}
			context.ExprHostBuilder.MatrixDynamicGroupStart(this.m_grouping.Name);
			if (this.m_subtotal != null)
			{
				this.m_subtotal.RegisterReportItems(context);
				this.m_subtotal.Initialize(context);
			}
			context.Location |= LocationFlags.InGrouping;
			context.RegisterGroupingScope(this.m_grouping.Name, this.m_grouping.SimpleGroupExpressions, this.m_grouping.Aggregates, this.m_grouping.PostSortAggregates, this.m_grouping.RecursiveAggregates, this.m_grouping, true);
			ObjectType objectType = context.ObjectType;
			string objectName = context.ObjectName;
			context.ObjectType = ObjectType.Grouping;
			context.ObjectName = this.m_grouping.Name;
			base.Initialize(context);
			context.RegisterReportItems(this.m_reportItems);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, true, false);
			}
			if (this.SubHeading != null)
			{
				this.m_subtotalSpan = this.SubHeading.DynamicInitialize(column, ++level, context, ref cornerSize);
			}
			else
			{
				this.m_subtotalSpan = 1;
			}
			this.m_reportItems.Initialize(context, true);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			context.UnRegisterReportItems(this.m_reportItems);
			context.ObjectType = objectType;
			context.ObjectName = objectName;
			context.UnRegisterGroupingScope(this.m_grouping.Name, true);
			if (this.m_subtotal != null)
			{
				this.m_subtotal.UnregisterReportItems(context);
			}
			this.m_hasExprHost = context.ExprHostBuilder.MatrixDynamicGroupEnd(column);
			return this.m_subtotalSpan + 1;
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x00188F88 File Offset: 0x00187188
		internal void DynamicRegisterReceiver(InitializationContext context)
		{
			if (this.m_grouping == null)
			{
				if (this.SubHeading != null)
				{
					context.RegisterReportItems(this.m_reportItems);
					this.SubHeading.DynamicRegisterReceiver(context);
					context.UnRegisterReportItems(this.m_reportItems);
					return;
				}
			}
			else
			{
				if (this.m_subtotal != null)
				{
					this.m_subtotal.RegisterReceiver(context);
				}
				context.RegisterReportItems(this.m_reportItems);
				if (this.m_visibility != null)
				{
					this.m_visibility.RegisterReceiver(context, true);
				}
				if (this.SubHeading != null)
				{
					this.SubHeading.DynamicRegisterReceiver(context);
				}
				this.m_reportItems.RegisterReceiver(context);
				if (this.m_visibility != null)
				{
					this.m_visibility.UnRegisterReceiver(context);
				}
				context.UnRegisterReportItems(this.m_reportItems);
			}
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x00189048 File Offset: 0x00187248
		internal int StaticInitialize(InitializationContext context)
		{
			if (this.m_grouping != null)
			{
				int num = 1;
				if (this.SubHeading != null)
				{
					context.Location |= LocationFlags.InGrouping;
					context.RegisterGroupingScope(this.m_grouping.Name, this.m_grouping.SimpleGroupExpressions, this.m_aggregates, this.m_postSortAggregates, this.m_recursiveAggregates, this.m_grouping, true);
					context.RegisterReportItems(this.m_reportItems);
					num = this.SubHeading.StaticInitialize(context);
					context.UnRegisterReportItems(this.m_reportItems);
					context.UnRegisterGroupingScope(this.m_grouping.Name, true);
				}
				return num + 1;
			}
			context.RegisterReportItems(this.m_reportItems);
			if (this.SubHeading != null)
			{
				this.m_subtotalSpan = this.SubHeading.StaticInitialize(context);
			}
			else
			{
				this.m_subtotalSpan = 1;
			}
			this.m_reportItems.Initialize(context, true);
			context.UnRegisterReportItems(this.m_reportItems);
			return 0;
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x00189140 File Offset: 0x00187340
		internal void StaticRegisterReceiver(InitializationContext context)
		{
			if (this.m_grouping != null)
			{
				if (this.SubHeading != null)
				{
					context.RegisterReportItems(this.m_reportItems);
					this.SubHeading.StaticRegisterReceiver(context);
					context.UnRegisterReportItems(this.m_reportItems);
					return;
				}
			}
			else
			{
				context.RegisterReportItems(this.m_reportItems);
				if (this.SubHeading != null)
				{
					this.SubHeading.StaticRegisterReceiver(context);
				}
				this.m_reportItems.RegisterReceiver(context);
				context.UnRegisterReportItems(this.m_reportItems);
			}
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x001891BE File Offset: 0x001873BE
		bool IPageBreakItem.IgnorePageBreaks()
		{
			return base.IgnorePageBreaks(this.m_visibility);
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x001891CC File Offset: 0x001873CC
		internal void SetExprHost(MatrixDynamicGroupExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null && base.HasExprHost);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			base.ReportHierarchyNodeSetExprHost(this.m_exprHost, reportObjectModel);
		}

		// Token: 0x060062A5 RID: 25253 RVA: 0x00189208 File Offset: 0x00187408
		internal Microsoft.ReportingServices.ReportProcessing.ReportItem GetContent(out bool computed)
		{
			Microsoft.ReportingServices.ReportProcessing.ReportItem reportItem = null;
			computed = false;
			if (this.m_reportItems != null && 0 < this.m_reportItems.Count)
			{
				int num;
				this.m_reportItems.GetReportItem(0, out computed, out num, out reportItem);
			}
			return reportItem;
		}

		// Token: 0x060062A6 RID: 25254 RVA: 0x00189244 File Offset: 0x00187444
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.PivotHeading, new MemberInfoList
			{
				new MemberInfo(MemberName.Size, Token.String),
				new MemberInfo(MemberName.SizeValue, Token.Double),
				new MemberInfo(MemberName.ReportItems, ObjectType.ReportItemCollection),
				new MemberInfo(MemberName.OwcGroupExpression, Token.Boolean)
			});
		}

		// Token: 0x040031B5 RID: 12725
		private string m_size;

		// Token: 0x040031B6 RID: 12726
		private double m_sizeValue;

		// Token: 0x040031B7 RID: 12727
		private ReportItemCollection m_reportItems;

		// Token: 0x040031B8 RID: 12728
		private bool m_owcGroupExpression;

		// Token: 0x040031B9 RID: 12729
		[NonSerialized]
		private bool m_inFirstPage = true;

		// Token: 0x040031BA RID: 12730
		[NonSerialized]
		private BoolList m_firstHeadingInstances;

		// Token: 0x040031BB RID: 12731
		[NonSerialized]
		private MatrixDynamicGroupExprHost m_exprHost;

		// Token: 0x040031BC RID: 12732
		[NonSerialized]
		private bool m_startHidden;

		// Token: 0x040031BD RID: 12733
		[NonSerialized]
		private bool m_inOutermostSubtotalCell;

		// Token: 0x040031BE RID: 12734
		[NonSerialized]
		private string m_renderingModelID;

		// Token: 0x040031BF RID: 12735
		[NonSerialized]
		private string[] m_renderingModelIDs;

		// Token: 0x040031C0 RID: 12736
		[NonSerialized]
		private ReportSize m_sizeForRendering;
	}
}
