using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200004C RID: 76
	internal sealed class ChartMember : Group
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x00013DB8 File Offset: 0x00011FB8
		internal ChartMember(Chart owner, ChartMember parent, ChartHeading headingDef, ChartHeadingInstance headingInstance, int index)
			: base(owner, headingDef.Grouping, headingDef.Visibility)
		{
			this.m_parent = parent;
			this.m_headingDef = headingDef;
			this.m_headingInstance = headingInstance;
			this.m_index = index;
			if (this.m_headingInstance != null)
			{
				this.m_uniqueName = this.m_headingInstance.UniqueName;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00013E18 File Offset: 0x00012018
		public override string ID
		{
			get
			{
				if (this.m_headingDef.Grouping == null && this.m_headingDef.IDs != null)
				{
					return this.m_headingDef.IDs[this.m_index].ToString(CultureInfo.InvariantCulture);
				}
				return this.m_headingDef.ID.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00013E7B File Offset: 0x0001207B
		internal override TextBox ToggleParent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00013E7E File Offset: 0x0001207E
		public override SharedHiddenState SharedHidden
		{
			get
			{
				if (this.IsStatic)
				{
					return SharedHiddenState.Never;
				}
				return Visibility.GetSharedHidden(this.m_visibilityDef);
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00013E95 File Offset: 0x00012095
		public override bool IsToggleChild
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00013E98 File Offset: 0x00012098
		public override bool Hidden
		{
			get
			{
				if (this.m_headingInstance == null)
				{
					return RenderingContext.GetDefinitionHidden(this.m_headingDef.Visibility);
				}
				return this.m_headingDef.Visibility != null && this.m_headingDef.Visibility.Toggle != null && base.OwnerDataRegion.RenderingContext.IsItemHidden(this.m_headingInstance.UniqueName, false);
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00013F00 File Offset: 0x00012100
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				CustomPropertyCollection customPropertyCollection = this.m_customProperties;
				if (this.m_customProperties == null)
				{
					if (this.m_headingDef.Grouping == null || this.m_headingDef.Grouping.CustomProperties == null)
					{
						return null;
					}
					if (this.m_headingInstance == null)
					{
						customPropertyCollection = new CustomPropertyCollection(this.m_headingDef.Grouping.CustomProperties, null);
					}
					else
					{
						customPropertyCollection = new CustomPropertyCollection(this.m_headingDef.Grouping.CustomProperties, this.InstanceInfo.CustomPropertyInstances);
					}
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_customProperties = customPropertyCollection;
					}
				}
				return customPropertyCollection;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00013F99 File Offset: 0x00012199
		public override string Label
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00013F9C File Offset: 0x0001219C
		public object MemberLabel
		{
			get
			{
				object obj;
				if (this.IsFakedStatic)
				{
					obj = null;
				}
				else if (this.m_headingInstance == null)
				{
					if (this.m_headingDef.Labels != null && this.m_headingDef.Labels[this.m_index] != null && ExpressionInfo.Types.Constant == this.m_headingDef.Labels[this.m_index].Type)
					{
						obj = this.m_headingDef.Labels[this.m_index].Value;
					}
					else
					{
						obj = null;
					}
				}
				else if (this.m_headingDef.ChartGroupExpression)
				{
					obj = this.InstanceInfo.GroupExpressionValue;
				}
				else
				{
					obj = this.InstanceInfo.HeadingLabel;
				}
				return obj;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001404D File Offset: 0x0001224D
		public ChartMember Parent
		{
			get
			{
				return this.m_parent;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00014055 File Offset: 0x00012255
		public bool IsInnerMostMember
		{
			get
			{
				return this.m_headingDef.SubHeading == null;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00014068 File Offset: 0x00012268
		public ChartMemberCollection Children
		{
			get
			{
				ChartHeading subHeading = this.m_headingDef.SubHeading;
				if (subHeading == null)
				{
					return null;
				}
				ChartMemberCollection chartMemberCollection = this.m_children;
				if (this.m_children == null)
				{
					ChartHeadingInstanceList chartHeadingInstanceList = null;
					if (this.m_headingInstance != null)
					{
						chartHeadingInstanceList = this.m_headingInstance.SubHeadingInstances;
					}
					chartMemberCollection = new ChartMemberCollection((Chart)base.OwnerDataRegion, this, subHeading, chartHeadingInstanceList);
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_children = chartMemberCollection;
					}
				}
				return chartMemberCollection;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x000140D9 File Offset: 0x000122D9
		public int MemberDataPointIndex
		{
			get
			{
				if (this.m_headingInstance != null)
				{
					return this.InstanceInfo.HeadingCellIndex;
				}
				if (this.m_headingDef.Grouping == null)
				{
					return this.m_index;
				}
				return 0;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00014104 File Offset: 0x00012304
		internal int CachedMemberDataPointIndex
		{
			get
			{
				if (this.m_cachedMemberDataPointIndex < 0)
				{
					this.m_cachedMemberDataPointIndex = this.MemberDataPointIndex;
				}
				return this.m_cachedMemberDataPointIndex;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00014121 File Offset: 0x00012321
		public int MemberHeadingSpan
		{
			get
			{
				if (this.m_headingInstance == null)
				{
					return 1;
				}
				return this.InstanceInfo.HeadingSpan;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00014138 File Offset: 0x00012338
		private bool IsFakedStatic
		{
			get
			{
				return this.m_headingDef.Grouping == null && this.m_headingDef.Labels == null;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00014157 File Offset: 0x00012357
		public bool IsStatic
		{
			get
			{
				return this.m_headingDef.Grouping == null;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0001416C File Offset: 0x0001236C
		public ChartMember.SortOrders SortOrder
		{
			get
			{
				ChartMember.SortOrders sortOrders = ChartMember.SortOrders.None;
				if (!this.IsStatic)
				{
					BoolList boolList;
					if (this.m_headingDef.Sorting != null)
					{
						boolList = this.m_headingDef.Sorting.SortDirections;
					}
					else
					{
						boolList = this.m_headingDef.Grouping.SortDirections;
					}
					if (boolList != null && 0 < boolList.Count)
					{
						if (boolList[0])
						{
							sortOrders = ChartMember.SortOrders.Ascending;
						}
						else
						{
							sortOrders = ChartMember.SortOrders.Descending;
						}
					}
				}
				return sortOrders;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x000141D0 File Offset: 0x000123D0
		public override string DataElementName
		{
			get
			{
				if (!this.IsStatic)
				{
					return base.DataElementName;
				}
				if (this.m_headingInstance != null && this.InstanceInfo.HeadingLabel != null)
				{
					return DataTypeUtility.ConvertToInvariantString(this.InstanceInfo.HeadingLabel);
				}
				if (!this.m_headingDef.IsColumn)
				{
					return "Series" + this.m_index.ToString(CultureInfo.InvariantCulture);
				}
				return "Category" + this.m_index.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00014254 File Offset: 0x00012454
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.IsStatic)
				{
					return this.DataElementOutputForStatic(null);
				}
				return base.DataElementOutput;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001426C File Offset: 0x0001246C
		internal ExpressionInfo LabelDefinition
		{
			get
			{
				if (!this.IsFakedStatic && this.m_headingDef.Labels != null)
				{
					return this.m_headingDef.Labels[this.m_index];
				}
				return null;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001429C File Offset: 0x0001249C
		internal object LabelValue
		{
			get
			{
				if (this.IsFakedStatic || this.m_headingDef.Labels == null)
				{
					return null;
				}
				if (this.m_headingInstance != null)
				{
					return this.InstanceInfo.HeadingLabel;
				}
				if (this.m_headingDef.Labels != null && this.m_headingDef.Labels[this.m_index] != null && ExpressionInfo.Types.Constant == this.m_headingDef.Labels[this.m_index].Type)
				{
					return this.m_headingDef.Labels[this.m_index].Value;
				}
				return null;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00014334 File Offset: 0x00012534
		internal ChartHeadingInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_headingInstance == null)
				{
					return null;
				}
				if (this.m_headingInstanceInfo == null)
				{
					this.m_headingInstanceInfo = this.m_headingInstance.GetInstanceInfo(base.OwnerDataRegion.RenderingContext.ChunkManager);
				}
				return this.m_headingInstanceInfo;
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00014370 File Offset: 0x00012570
		public DataElementOutputTypes DataElementOutputForStatic(ChartMember staticHeading)
		{
			if (!this.IsStatic)
			{
				return this.DataElementOutput;
			}
			if (staticHeading != null && (!staticHeading.IsStatic || staticHeading.Parent == this.Parent))
			{
				staticHeading = null;
			}
			if (staticHeading != null)
			{
				int num;
				int num2;
				if (this.m_headingDef.IsColumn)
				{
					num = staticHeading.m_index;
					num2 = this.m_index;
				}
				else
				{
					num = this.m_index;
					num2 = staticHeading.m_index;
				}
				return this.GetDataElementOutputTypeFromDataPoint(num, num2);
			}
			Chart chart = (Chart)base.OwnerDataRegion.ReportItemDef;
			if (chart.PivotStaticColumns == null || chart.PivotStaticRows == null)
			{
				return this.GetDataElementOutputTypeFromDataPoint(0, this.m_index);
			}
			Global.Tracer.Assert(chart.PivotStaticColumns != null && chart.PivotStaticRows != null);
			return this.GetDataElementOutputTypeForSeriesCategory(this.m_index);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00014438 File Offset: 0x00012638
		internal bool IsPlotTypeLine()
		{
			if (this.m_headingInstance == null)
			{
				return false;
			}
			if (0 <= this.InstanceInfo.StaticGroupingIndex)
			{
				Global.Tracer.Assert(this.m_headingDef != null);
				if (this.m_headingDef.PlotTypesLine != null)
				{
					return this.m_headingDef.PlotTypesLine[this.InstanceInfo.StaticGroupingIndex];
				}
			}
			return false;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001449A File Offset: 0x0001269A
		private DataElementOutputTypes GetDataElementOutputTypeFromDataPoint(int seriesIndex, int categoryIndex)
		{
			return ((Chart)base.OwnerDataRegion.ReportItemDef).GetDataPoint(seriesIndex, categoryIndex).DataElementOutput;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000144B8 File Offset: 0x000126B8
		private DataElementOutputTypes GetDataElementOutputTypeForSeriesCategory(int index)
		{
			Chart chart = (Chart)base.OwnerDataRegion.ReportItemDef;
			int num;
			int num2;
			int num3;
			if (this.m_headingDef.IsColumn)
			{
				num = 0;
				num2 = index;
				num3 = chart.StaticSeriesCount;
			}
			else
			{
				num = index;
				num2 = 0;
				num3 = chart.StaticCategoryCount;
			}
			while (chart.GetDataPoint(num, num2).DataElementOutput == DataElementOutputTypes.NoOutput)
			{
				if (this.m_headingDef.IsColumn)
				{
					num++;
					if (num < num3)
					{
						continue;
					}
				}
				else
				{
					num2++;
					if (num2 < num3)
					{
						continue;
					}
				}
				return DataElementOutputTypes.NoOutput;
			}
			return DataElementOutputTypes.Output;
		}

		// Token: 0x0400016C RID: 364
		private ChartHeading m_headingDef;

		// Token: 0x0400016D RID: 365
		private ChartHeadingInstance m_headingInstance;

		// Token: 0x0400016E RID: 366
		private ChartHeadingInstanceInfo m_headingInstanceInfo;

		// Token: 0x0400016F RID: 367
		private ChartMemberCollection m_children;

		// Token: 0x04000170 RID: 368
		private ChartMember m_parent;

		// Token: 0x04000171 RID: 369
		private int m_index;

		// Token: 0x04000172 RID: 370
		private int m_cachedMemberDataPointIndex = -1;

		// Token: 0x02000911 RID: 2321
		public enum SortOrders
		{
			// Token: 0x04003EF4 RID: 16116
			None,
			// Token: 0x04003EF5 RID: 16117
			Ascending,
			// Token: 0x04003EF6 RID: 16118
			Descending
		}
	}
}
