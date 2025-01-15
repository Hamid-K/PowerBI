using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200003A RID: 58
	internal class MatrixRdlReportItemConverterBase : BaseRdlReportItemConverter
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00008064 File Offset: 0x00006264
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000806C File Offset: 0x0000626C
		protected int RowIndex { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00008075 File Offset: 0x00006275
		// (set) Token: 0x06000197 RID: 407 RVA: 0x0000807D File Offset: 0x0000627D
		protected int ColumnIndex { get; set; }

		// Token: 0x06000198 RID: 408 RVA: 0x00008088 File Offset: 0x00006288
		public void ConvertTablixRowHierarchy(IReportDeserializationContext ctx, Tablix tablix, PVVisual visual, TablixMember currentNode, Bucket valuesBucket, Bucket rowBucket, Bucket columnBucket, Dictionary<ReportImageSource, int> imageCounterMap)
		{
			using (new DataSetScope(ctx, currentNode.Group))
			{
				this.ConvertTablixHierarchyMemberHeader(ctx, ctx.GetCurrentDataSet(), currentNode, rowBucket, imageCounterMap);
				List<TablixMember> members = currentNode.Members;
				foreach (TablixMember tablixMember in members)
				{
					this.ConvertTablixRowHierarchy(ctx, tablix, visual, tablixMember, valuesBucket, rowBucket, columnBucket, imageCounterMap);
				}
				if (members.Count == 0)
				{
					if (!currentNode.IsSubtotal)
					{
						this.ColumnIndex = 0;
						foreach (TablixMember tablixMember2 in tablix.ColumnHierarchy.Members)
						{
							this.ConvertTablixColumnHierarchy(ctx, tablix, visual, tablixMember2, valuesBucket, columnBucket, imageCounterMap);
						}
					}
					int rowIndex = this.RowIndex;
					this.RowIndex = rowIndex + 1;
				}
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000081A0 File Offset: 0x000063A0
		public void ConvertTablixColumnHierarchy(IReportDeserializationContext ctx, Tablix tablix, PVVisual visual, TablixMember currentNode, Bucket valuesBucket, Bucket columnBucket, Dictionary<ReportImageSource, int> imageCounterMap)
		{
			DataSet dataSetByName = ctx.GetDataSetByName(tablix.DataSetName);
			this.ConvertTablixHierarchyMemberHeader(ctx, dataSetByName, currentNode, columnBucket, imageCounterMap);
			List<TablixMember> members = currentNode.Members;
			foreach (TablixMember tablixMember in members)
			{
				this.ConvertTablixColumnHierarchy(ctx, tablix, visual, tablixMember, valuesBucket, columnBucket, imageCounterMap);
			}
			if (members.Count == 0)
			{
				if (!currentNode.IsSubtotal)
				{
					TablixRow tablixRow = tablix.Body.TablixRows[this.RowIndex];
					if (tablixRow.TablixCells.Count > this.ColumnIndex)
					{
						TablixCell tablixCell = tablixRow.TablixCells[this.ColumnIndex];
						this.ConvertTablixBodyCell(ctx, tablixCell, tablix, visual, valuesBucket);
					}
				}
				int columnIndex = this.ColumnIndex;
				this.ColumnIndex = columnIndex + 1;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000828C File Offset: 0x0000648C
		public virtual void ConvertTablixBodyCell(IReportDeserializationContext ctx, TablixCell cell, Tablix tablix, PVVisual visual, Bucket valuesBucket)
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008290 File Offset: 0x00006490
		public void ConvertTablixHierarchyMemberHeader(IReportDeserializationContext ctx, DataSet dataSet, TablixMember currentNode, Bucket hierarchyBucket, Dictionary<ReportImageSource, int> imageCounterMap)
		{
			TablixHeader tablixHeader = currentNode.TablixHeader;
			if (tablixHeader != null)
			{
				ReportItem cellContents = tablixHeader.CellContents;
				if (cellContents != null)
				{
					if (cellContents is Textbox)
					{
						Expression firstValue = (cellContents as Textbox).FirstValue;
						base.AddToBucketIfNotNull(hierarchyBucket, dataSet, firstValue);
						return;
					}
					if (cellContents is Image)
					{
						Image image = cellContents as Image;
						Expression valueAsExpression = image.Source.ValueAsExpression;
						string dataSetName = image.DataSetName;
						if (!string.IsNullOrEmpty(dataSetName))
						{
							base.AddToBucketIfNotNull(hierarchyBucket, ctx.GetDataSetByName(dataSetName), valueAsExpression);
						}
						else
						{
							base.AddToBucketIfNotNull(hierarchyBucket, dataSet, valueAsExpression);
						}
						ReportImageSource? source = image.Source.Source;
						if (source != null)
						{
							ReportImageSource value = source.Value;
							int num = imageCounterMap[value];
							imageCounterMap[value] = num + 1;
						}
					}
				}
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008354 File Offset: 0x00006554
		public void LoadSortsFromMembers(List<TablixMember> members, IReportDeserializationContext ctx, DataSet defaultDataSet, Dictionary<Formula, SortDirection> sorts)
		{
			foreach (TablixMember tablixMember in members)
			{
				DataSet dataSet = defaultDataSet;
				Group group = tablixMember.Group;
				if (group != null && group.DataSetName != null)
				{
					using (new DataSetScope(ctx, group))
					{
						dataSet = ctx.GetCurrentDataSet();
					}
				}
				Dictionary<Formula, SortDirection> dictionary = this.ToSorts(dataSet, tablixMember.SortExpressions);
				base.AppendUniqueSorts(sorts, dictionary);
				this.LoadSortsFromMembers(tablixMember.Members, ctx, defaultDataSet, sorts);
			}
		}
	}
}
