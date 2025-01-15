using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.ReportProcessing.Upgrade
{
	// Token: 0x0200078B RID: 1931
	internal sealed class Upgrader
	{
		// Token: 0x06006BA3 RID: 27555 RVA: 0x001B5E34 File Offset: 0x001B4034
		internal static void UpgradeToCurrent(Report report)
		{
			if (report.IntermediateFormatVersion.IsRS2000_Beta2_orOlder)
			{
				new Upgrader.UpgraderForV1Beta2().Upgrade(report);
			}
		}

		// Token: 0x06006BA4 RID: 27556 RVA: 0x001B5E4E File Offset: 0x001B404E
		internal static bool UpgradeToCurrent(ReportSnapshot reportSnapshot, ChunkManager.RenderingChunkManager chunkManager, ReportProcessing.CreateReportChunk createChunkCallback)
		{
			if (reportSnapshot.Report.IntermediateFormatVersion.IsRS2000_Beta2_orOlder)
			{
				new Upgrader.UpgraderForV1Beta2().Upgrade(reportSnapshot, chunkManager, createChunkCallback);
				return true;
			}
			return false;
		}

		// Token: 0x06006BA5 RID: 27557 RVA: 0x001B5E72 File Offset: 0x001B4072
		internal static void UpgradeDatasetIDs(Report report)
		{
			if (!report.IntermediateFormatVersion.Is_WithUserSort)
			{
				new Upgrader.DataSetUpgrader().Upgrade(report);
			}
		}

		// Token: 0x06006BA6 RID: 27558 RVA: 0x001B5E8C File Offset: 0x001B408C
		internal static bool CreateBookmarkDrillthroughChunks(ReportSnapshot reportSnapshot, ChunkManager.RenderingChunkManager chunkManager, ChunkManager.UpgradeManager upgradeManager)
		{
			if (!reportSnapshot.Report.IntermediateFormatVersion.IsRS2005_WithSpecialChunkSplit)
			{
				new Upgrader.BookmarkDrillthroughUpgrader().Upgrade(reportSnapshot, chunkManager, upgradeManager);
				return true;
			}
			return false;
		}

		// Token: 0x06006BA7 RID: 27559 RVA: 0x001B5EB0 File Offset: 0x001B40B0
		internal static void UpgradeToPageSectionsChunk(ReportSnapshot reportSnapshot, ICatalogItemContext reportContext, ChunkManager.RenderingChunkManager chunkManager, ReportProcessing.CreateReportChunk createChunkCallback, IGetResource getResourceCallback, RenderingContext renderingContext, IDataProtection dataProtection)
		{
			if (reportSnapshot.PageSectionOffsets == null && (reportSnapshot.Report.PageHeaderEvaluation || reportSnapshot.Report.PageFooterEvaluation) && !chunkManager.PageSectionChunkExists())
			{
				new Upgrader.PageSectionsGenerator().Upgrade(reportSnapshot, reportContext, chunkManager, createChunkCallback, getResourceCallback, renderingContext, dataProtection);
			}
		}

		// Token: 0x06006BA8 RID: 27560 RVA: 0x001B5EF0 File Offset: 0x001B40F0
		private static void SetRowPages(ref int[] rowPages, int start, int span, int pageNumber)
		{
			Global.Tracer.Assert(rowPages != null && start >= 0 && span > 0 && start + span <= rowPages.Length, "(null != rowPages && start >= 0 && span > 0 && start + span <= rowPages.Length)");
			for (int i = 0; i < span; i++)
			{
				rowPages[start + i] = pageNumber;
			}
		}

		// Token: 0x02000CE7 RID: 3303
		private sealed class DataSetUpgrader
		{
			// Token: 0x06008D6F RID: 36207 RVA: 0x0023F608 File Offset: 0x0023D808
			internal void Upgrade(Report report)
			{
				List<DataSet> list = new List<DataSet>();
				int num = 0;
				this.DetermineCurrentMaxID(report, ref list, ref num);
				for (int i = 0; i < list.Count; i++)
				{
					num++;
					list[i].ID = num;
				}
				report.LastID = num;
			}

			// Token: 0x06008D70 RID: 36208 RVA: 0x0023F654 File Offset: 0x0023D854
			private void DetermineCurrentMaxID(Report report, ref List<DataSet> datasets, ref int maxID)
			{
				if (report == null)
				{
					return;
				}
				maxID += report.LastID;
				if (report.DataSources != null)
				{
					for (int i = 0; i < report.DataSources.Count; i++)
					{
						DataSource dataSource = report.DataSources[i];
						if (dataSource.DataSets != null)
						{
							for (int j = 0; j < dataSource.DataSets.Count; j++)
							{
								DataSet dataSet = dataSource.DataSets[j];
								if (dataSet.ID <= 0)
								{
									datasets.Add(dataSet);
								}
							}
						}
					}
				}
				if (report.SubReports != null)
				{
					for (int k = 0; k < report.SubReports.Count; k++)
					{
						if (report.SubReports[k].Report != null)
						{
							this.DetermineCurrentMaxID(report.SubReports[k].Report, ref datasets, ref maxID);
						}
					}
				}
			}
		}

		// Token: 0x02000CE8 RID: 3304
		private sealed class BookmarkDrillthroughUpgrader
		{
			// Token: 0x06008D72 RID: 36210 RVA: 0x0023F730 File Offset: 0x0023D930
			internal void Upgrade(ReportSnapshot reportSnapshot, ChunkManager.RenderingChunkManager chunkManager, ChunkManager.UpgradeManager upgradeManager)
			{
				Global.Tracer.Assert(chunkManager != null && upgradeManager != null && reportSnapshot != null, "(null != chunkManager && null != upgradeManager && null != reportSnapshot)");
				this.m_chunkManager = chunkManager;
				this.m_upgradeManager = upgradeManager;
				this.m_bookmarks = new BookmarksHashtable();
				this.m_drillthroughs = new ReportDrillthroughInfo();
				this.ProcessReport(reportSnapshot.Report, reportSnapshot.ReportInstance, 0, reportSnapshot.ReportInstance.NumberOfPages);
				bool flag = this.m_bookmarks.Count != 0;
				if (reportSnapshot.HasBookmarks)
				{
					Global.Tracer.Assert(flag, "(hasBookmarks)");
					reportSnapshot.BookmarksInfo = this.m_bookmarks;
				}
				else if (flag)
				{
					reportSnapshot.HasBookmarks = true;
					reportSnapshot.BookmarksInfo = this.m_bookmarks;
				}
				else
				{
					reportSnapshot.HasBookmarks = false;
					reportSnapshot.BookmarksInfo = null;
				}
				if (this.m_drillthroughs.Count == 0)
				{
					reportSnapshot.DrillthroughInfo = null;
					return;
				}
				reportSnapshot.DrillthroughInfo = this.m_drillthroughs;
			}

			// Token: 0x06008D73 RID: 36211 RVA: 0x0023F818 File Offset: 0x0023DA18
			private void ProcessReport(Report report, ReportInstance reportInstance, int startPage, int endPage)
			{
				long chunkOffset = reportInstance.ChunkOffset;
				ReportItemInstanceInfo instanceInfo = reportInstance.GetInstanceInfo(this.m_chunkManager);
				this.m_upgradeManager.AddInstance(instanceInfo, reportInstance, chunkOffset);
				this.ProcessReportItemColInstance(reportInstance.ReportItemColInstance, startPage, endPage);
			}

			// Token: 0x06008D74 RID: 36212 RVA: 0x0023F858 File Offset: 0x0023DA58
			private void ProcessReportItemColInstance(ReportItemColInstance reportItemColInstance, int startPage, int endPage)
			{
				if (reportItemColInstance != null)
				{
					long chunkOffset = reportItemColInstance.ChunkOffset;
					ReportItemColInstanceInfo instanceInfo = reportItemColInstance.GetInstanceInfo(this.m_chunkManager, false);
					this.m_upgradeManager.AddInstance(instanceInfo, reportItemColInstance, chunkOffset);
					reportItemColInstance.ChildrenNonComputedUniqueNames = instanceInfo.ChildrenNonComputedUniqueNames;
					Global.Tracer.Assert(reportItemColInstance.ReportItemColDef != null, "(null != reportItemColInstance.ReportItemColDef)");
					ReportItemIndexerList sortedReportItems = reportItemColInstance.ReportItemColDef.SortedReportItems;
					if (sortedReportItems != null)
					{
						int count = sortedReportItems.Count;
						for (int i = 0; i < count; i++)
						{
							int num = startPage;
							int num2 = endPage;
							if (reportItemColInstance.ChildrenStartAndEndPages != null)
							{
								if (0 <= reportItemColInstance.ChildrenStartAndEndPages[i].StartPage)
								{
									num = reportItemColInstance.ChildrenStartAndEndPages[i].StartPage;
								}
								if (0 <= reportItemColInstance.ChildrenStartAndEndPages[i].EndPage)
								{
									num2 = reportItemColInstance.ChildrenStartAndEndPages[i].EndPage;
								}
							}
							if (sortedReportItems[i].IsComputed)
							{
								this.ProcessReportItemInstance(reportItemColInstance[sortedReportItems[i].Index], num, num2);
							}
							else
							{
								Global.Tracer.Assert(reportItemColInstance.ChildrenNonComputedUniqueNames != null && sortedReportItems[i].Index < reportItemColInstance.ChildrenNonComputedUniqueNames.Length);
								this.ProcessReportItem(reportItemColInstance.ReportItemColDef[i], reportItemColInstance.ChildrenNonComputedUniqueNames[sortedReportItems[i].Index], num);
							}
						}
					}
				}
			}

			// Token: 0x06008D75 RID: 36213 RVA: 0x0023F9D4 File Offset: 0x0023DBD4
			private void ProcessReportItem(ReportItem reportItem, NonComputedUniqueNames uniqueName, int startPage)
			{
				if (reportItem.Bookmark != null && reportItem.Bookmark.Value != null)
				{
					this.m_bookmarks.Add(reportItem.Bookmark.Value, startPage, uniqueName.UniqueName.ToString(CultureInfo.InvariantCulture));
				}
				Rectangle rectangle = reportItem as Rectangle;
				if (rectangle != null && rectangle.ReportItems != null && rectangle.ReportItems.Count != 0)
				{
					Global.Tracer.Assert(uniqueName.ChildrenUniqueNames != null && rectangle.ReportItems.Count == uniqueName.ChildrenUniqueNames.Length);
					for (int i = 0; i < rectangle.ReportItems.Count; i++)
					{
						this.ProcessReportItem(rectangle.ReportItems[i], uniqueName.ChildrenUniqueNames[i], startPage);
					}
					return;
				}
				Image image = reportItem as Image;
				if (image != null && image.Action != null)
				{
					this.ProcessNonComputedAction(image.Action, uniqueName.UniqueName);
					return;
				}
				TextBox textBox = reportItem as TextBox;
				if (textBox != null && textBox.Action != null)
				{
					this.ProcessNonComputedAction(textBox.Action, uniqueName.UniqueName);
					return;
				}
			}

			// Token: 0x06008D76 RID: 36214 RVA: 0x0023FAEB File Offset: 0x0023DCEB
			private void ProcessAction(Action action, ActionInstance actionInstance, int uniqueName)
			{
				if (action == null)
				{
					return;
				}
				if (actionInstance != null)
				{
					this.ProcessComputedAction(action, actionInstance, uniqueName);
					return;
				}
				this.ProcessNonComputedAction(action, uniqueName);
			}

			// Token: 0x06008D77 RID: 36215 RVA: 0x0023FB08 File Offset: 0x0023DD08
			private void ProcessComputedAction(Action action, ActionInstance actionInstance, int uniqueName)
			{
				if (action == null || actionInstance == null)
				{
					return;
				}
				int count = action.ActionItems.Count;
				for (int i = 0; i < count; i++)
				{
					int computedIndex = action.ActionItems[i].ComputedIndex;
					if (computedIndex >= 0)
					{
						Global.Tracer.Assert(computedIndex < action.ComputedActionItemsCount && computedIndex < actionInstance.ActionItemsValues.Count);
						this.ProcessComputedActionItem(action.ActionItems[i], actionInstance.ActionItemsValues[computedIndex], uniqueName, i);
					}
					else
					{
						this.ProcessNonComputedActionItem(action.ActionItems[i], uniqueName, i);
					}
				}
			}

			// Token: 0x06008D78 RID: 36216 RVA: 0x0023FBA4 File Offset: 0x0023DDA4
			private void ProcessNonComputedAction(Action action, int uniqueName)
			{
				if (action == null || action.ActionItems == null)
				{
					return;
				}
				int count = action.ActionItems.Count;
				for (int i = 0; i < count; i++)
				{
					this.ProcessNonComputedActionItem(action.ActionItems[i], uniqueName, i);
				}
			}

			// Token: 0x06008D79 RID: 36217 RVA: 0x0023FBEC File Offset: 0x0023DDEC
			private void ProcessNonComputedActionItem(ActionItem actionItem, int uniqueName, int index)
			{
				if (actionItem.DrillthroughReportName != null)
				{
					Global.Tracer.Assert(actionItem.DrillthroughReportName.Type == ExpressionInfo.Types.Constant, "(actionItem.DrillthroughReportName.Type == ExpressionInfo.Types.Constant)");
					DrillthroughParameters drillthroughParameters = null;
					if (actionItem.DrillthroughParameters != null)
					{
						int i = 0;
						while (i < actionItem.DrillthroughParameters.Count)
						{
							ParameterValue parameterValue = actionItem.DrillthroughParameters[i];
							if (parameterValue.Omit == null)
							{
								goto IL_007F;
							}
							Global.Tracer.Assert(parameterValue.Omit.Type == ExpressionInfo.Types.Constant, "(paramValue.Omit.Type == ExpressionInfo.Types.Constant)");
							if (!parameterValue.Omit.BoolValue)
							{
								goto IL_007F;
							}
							IL_00BC:
							i++;
							continue;
							IL_007F:
							Global.Tracer.Assert(parameterValue.Value.Type == ExpressionInfo.Types.Constant, "(paramValue.Value.Type == ExpressionInfo.Types.Constant)");
							if (drillthroughParameters == null)
							{
								drillthroughParameters = new DrillthroughParameters();
							}
							drillthroughParameters.Add(parameterValue.Name, parameterValue.Value.Value);
							goto IL_00BC;
						}
					}
					DrillthroughInformation drillthroughInformation = new DrillthroughInformation(actionItem.DrillthroughReportName.Value, drillthroughParameters, null);
					string text = uniqueName.ToString(CultureInfo.InvariantCulture) + ":" + index.ToString(CultureInfo.InvariantCulture);
					this.m_drillthroughs.AddDrillthrough(text, drillthroughInformation);
				}
			}

			// Token: 0x06008D7A RID: 36218 RVA: 0x0023FD10 File Offset: 0x0023DF10
			private void ProcessComputedActionItem(ActionItem actionItem, ActionItemInstance actionInstance, int uniqueName, int index)
			{
				Global.Tracer.Assert(actionItem != null, "(null != actionItem)");
				if (actionItem.DrillthroughReportName == null)
				{
					return;
				}
				string text;
				if (ExpressionInfo.Types.Constant == actionItem.DrillthroughReportName.Type)
				{
					text = actionItem.DrillthroughReportName.Value;
				}
				else
				{
					Global.Tracer.Assert(actionInstance != null, "(null != actionInstance)");
					text = actionInstance.DrillthroughReportName;
				}
				if (text == null)
				{
					return;
				}
				DrillthroughParameters drillthroughParameters = null;
				if (actionItem.DrillthroughParameters != null)
				{
					int count = actionItem.DrillthroughParameters.Count;
					Global.Tracer.Assert(count == actionInstance.DrillthroughParametersOmits.Count && count == actionInstance.DrillthroughParametersValues.Length && count == actionItem.DrillthroughParameters.Count);
					for (int i = 0; i < count; i++)
					{
						if (!actionInstance.DrillthroughParametersOmits[i])
						{
							if (drillthroughParameters == null)
							{
								drillthroughParameters = new DrillthroughParameters();
							}
							drillthroughParameters.Add(actionItem.DrillthroughParameters[i].Name, actionInstance.DrillthroughParametersValues[i]);
						}
					}
				}
				DrillthroughInformation drillthroughInformation = new DrillthroughInformation(text, drillthroughParameters, null);
				string text2 = uniqueName.ToString(CultureInfo.InvariantCulture) + ":" + index.ToString(CultureInfo.InvariantCulture);
				this.m_drillthroughs.AddDrillthrough(text2, drillthroughInformation);
			}

			// Token: 0x06008D7B RID: 36219 RVA: 0x0023FE4C File Offset: 0x0023E04C
			private void ProcessReportItemInstance(ReportItemInstance reportItemInstance, int startPage, int endPage)
			{
				Global.Tracer.Assert(reportItemInstance != null, "(null != reportItemInstance)");
				ReportItem reportItemDef = reportItemInstance.ReportItemDef;
				long chunkOffset = reportItemInstance.ChunkOffset;
				ReportItemInstanceInfo instanceInfo = reportItemInstance.GetInstanceInfo(this.m_chunkManager);
				if (reportItemDef is TextBox)
				{
					bool flag;
					SimpleTextBoxInstanceInfo simpleTextBoxInstanceInfo = ((TextBoxInstance)reportItemInstance).UpgradeToSimpleTextbox(instanceInfo as TextBoxInstanceInfo, out flag);
					if (flag)
					{
						this.m_upgradeManager.AddInstance(simpleTextBoxInstanceInfo, reportItemInstance, chunkOffset);
						return;
					}
				}
				this.m_upgradeManager.AddInstance(instanceInfo, reportItemInstance, chunkOffset);
				if (instanceInfo.Bookmark != null)
				{
					this.m_bookmarks.Add(instanceInfo.Bookmark, startPage, reportItemInstance.UniqueName.ToString(CultureInfo.InvariantCulture));
				}
				if (reportItemDef is Rectangle)
				{
					this.ProcessReportItemColInstance(((RectangleInstance)reportItemInstance).ReportItemColInstance, startPage, endPage);
					return;
				}
				if (reportItemDef is Image)
				{
					Image image = reportItemDef as Image;
					if (image.Action != null)
					{
						this.ProcessAction(image.Action, ((ImageInstanceInfo)instanceInfo).Action, reportItemInstance.UniqueName);
						return;
					}
				}
				else if (reportItemDef is TextBox)
				{
					TextBox textBox = reportItemDef as TextBox;
					if (textBox.Action != null)
					{
						this.ProcessAction(textBox.Action, ((TextBoxInstanceInfo)instanceInfo).Action, reportItemInstance.UniqueName);
						return;
					}
				}
				else if (reportItemDef is SubReport)
				{
					SubReport subReport = (SubReport)reportItemDef;
					SubReportInstance subReportInstance = (SubReportInstance)reportItemInstance;
					if (subReportInstance.ReportInstance != null)
					{
						this.ProcessReport(subReport.Report, subReportInstance.ReportInstance, startPage, endPage);
						return;
					}
				}
				else if (reportItemDef is DataRegion)
				{
					if (reportItemDef is List)
					{
						this.ProcessList((List)reportItemDef, (ListInstance)reportItemInstance, startPage);
						return;
					}
					if (reportItemDef is Matrix)
					{
						this.ProcessMatrix((Matrix)reportItemDef, (MatrixInstance)reportItemInstance, startPage, endPage);
						return;
					}
					if (reportItemDef is Table)
					{
						this.ProcessTable((Table)reportItemDef, (TableInstance)reportItemInstance, startPage, endPage);
						return;
					}
					if (reportItemDef is Chart)
					{
						this.ProcessChart((Chart)reportItemDef, (ChartInstance)reportItemInstance, startPage);
						return;
					}
					if (reportItemDef is CustomReportItem)
					{
						this.ProcessCustomReportItem((CustomReportItem)reportItemDef, (CustomReportItemInstance)reportItemInstance, startPage, endPage);
						return;
					}
					if (!(reportItemDef is OWCChart) && !(reportItemDef is ActiveXControl))
					{
						CheckBox checkBox = reportItemDef as CheckBox;
					}
				}
			}

			// Token: 0x06008D7C RID: 36220 RVA: 0x00240074 File Offset: 0x0023E274
			private void ProcessCustomReportItem(CustomReportItem criDef, CustomReportItemInstance criInstance, int startPage, int endPage)
			{
				ReportItem reportItem = null;
				Global.Tracer.Assert(criDef.AltReportItem != null, "(null != criDef.AltReportItem)");
				if (criDef.RenderReportItem != null && 1 == criDef.RenderReportItem.Count)
				{
					reportItem = criDef.RenderReportItem[0];
				}
				else if (criDef.AltReportItem != null && 1 == criDef.AltReportItem.Count)
				{
					Global.Tracer.Assert(criDef.RenderReportItem == null, "(null == criDef.RenderReportItem)");
					reportItem = criDef.AltReportItem[0];
				}
				if (reportItem != null && criInstance.AltReportItemColInstance != null)
				{
					Global.Tracer.Assert(criInstance.AltReportItemColInstance.ReportItemInstances != null && 1 == criInstance.AltReportItemColInstance.ReportItemInstances.Count);
					this.ProcessReportItemInstance(criInstance.AltReportItemColInstance.ReportItemInstances[0], startPage, endPage);
				}
			}

			// Token: 0x06008D7D RID: 36221 RVA: 0x0024014D File Offset: 0x0023E34D
			private void ProcessChart(Chart chartDef, ChartInstance chartInstance, int startPage)
			{
				this.ProcessChartHeadings(chartInstance.RowInstances);
				this.ProcessChartHeadings(chartInstance.ColumnInstances);
				this.ProcessChartDataPoints(chartDef, chartInstance, startPage);
			}

			// Token: 0x06008D7E RID: 36222 RVA: 0x00240170 File Offset: 0x0023E370
			private void ProcessChartHeadings(ChartHeadingInstanceList headings)
			{
				if (headings != null)
				{
					for (int i = 0; i < headings.Count; i++)
					{
						ChartHeadingInstance chartHeadingInstance = headings[i];
						Global.Tracer.Assert(chartHeadingInstance != null, "(null != headingInstance)");
						long chunkOffset = chartHeadingInstance.ChunkOffset;
						ChartHeadingInstanceInfo instanceInfo = chartHeadingInstance.GetInstanceInfo(this.m_chunkManager);
						this.m_upgradeManager.AddInstance(instanceInfo, chartHeadingInstance, chunkOffset);
						if (chartHeadingInstance.SubHeadingInstances != null)
						{
							this.ProcessChartHeadings(chartHeadingInstance.SubHeadingInstances);
						}
					}
				}
			}

			// Token: 0x06008D7F RID: 36223 RVA: 0x002401E4 File Offset: 0x0023E3E4
			private void ProcessChartDataPoints(Chart chartDef, ChartInstance chartInstance, int startPage)
			{
				if (chartInstance.DataPoints != null)
				{
					for (int i = 0; i < chartInstance.DataPointSeriesCount; i++)
					{
						for (int j = 0; j < chartInstance.DataPointCategoryCount; j++)
						{
							ChartDataPointInstance chartDataPointInstance = chartInstance.DataPoints[i][j];
							Global.Tracer.Assert(chartDataPointInstance != null, "(null != instance)");
							long chunkOffset = chartDataPointInstance.ChunkOffset;
							ChartDataPointInstanceInfo instanceInfo = chartDataPointInstance.GetInstanceInfo(this.m_chunkManager, chartDef.ChartDataPoints);
							this.m_upgradeManager.AddInstance(instanceInfo, chartDataPointInstance, chunkOffset);
							if (instanceInfo.Action != null)
							{
								Global.Tracer.Assert(instanceInfo.DataPointIndex < chartDef.ChartDataPoints.Count, "(instanceInfo.DataPointIndex < chartDef.ChartDataPoints.Count)");
								Action action = chartDef.ChartDataPoints[instanceInfo.DataPointIndex].Action;
								Global.Tracer.Assert(action != null, "(null != actionDef)");
								this.ProcessAction(action, instanceInfo.Action, chartDataPointInstance.UniqueName);
							}
						}
					}
				}
			}

			// Token: 0x06008D80 RID: 36224 RVA: 0x002402E8 File Offset: 0x0023E4E8
			private void ProcessList(List listDef, ListInstance listInstance, int startPage)
			{
				if (listInstance.ListContents != null)
				{
					if (listDef.Grouping != null)
					{
						Global.Tracer.Assert(listInstance.ListContents.Count == listInstance.ChildrenStartAndEndPages.Count, "(listInstance.ListContents.Count == listInstance.ChildrenStartAndEndPages.Count)");
						for (int i = 0; i < listInstance.ChildrenStartAndEndPages.Count; i++)
						{
							ListContentInstance listContentInstance = listInstance.ListContents[i];
							long chunkOffset = listContentInstance.ChunkOffset;
							ListContentInstanceInfo instanceInfo = listContentInstance.GetInstanceInfo(this.m_chunkManager);
							this.m_upgradeManager.AddInstance(instanceInfo, listContentInstance, chunkOffset);
							RenderingPagesRanges renderingPagesRanges = listInstance.ChildrenStartAndEndPages[i];
							this.ProcessReportItemColInstance(listContentInstance.ReportItemColInstance, renderingPagesRanges.StartPage, renderingPagesRanges.EndPage);
						}
						return;
					}
					if (listInstance.ChildrenStartAndEndPages == null)
					{
						this.ProcessListContents(listDef, listInstance, startPage, 0, listInstance.ListContents.Count - 1);
						return;
					}
					for (int j = 0; j < listInstance.ChildrenStartAndEndPages.Count; j++)
					{
						RenderingPagesRanges renderingPagesRanges2 = listInstance.ChildrenStartAndEndPages[j];
						this.ProcessListContents(listDef, listInstance, startPage + j, renderingPagesRanges2.StartRow, renderingPagesRanges2.StartRow + renderingPagesRanges2.NumberOfDetails - 1);
					}
				}
			}

			// Token: 0x06008D81 RID: 36225 RVA: 0x00240410 File Offset: 0x0023E610
			private void ProcessListContents(List listDef, ListInstance listInstance, int page, int startRow, int endRow)
			{
				for (int i = startRow; i <= endRow; i++)
				{
					ListContentInstance listContentInstance = listInstance.ListContents[i];
					long chunkOffset = listContentInstance.ChunkOffset;
					ListContentInstanceInfo instanceInfo = listContentInstance.GetInstanceInfo(this.m_chunkManager);
					this.m_upgradeManager.AddInstance(instanceInfo, listContentInstance, chunkOffset);
					this.ProcessReportItemColInstance(listContentInstance.ReportItemColInstance, page, page);
				}
			}

			// Token: 0x06008D82 RID: 36226 RVA: 0x00240468 File Offset: 0x0023E668
			private void ProcessTable(Table tableDef, TableInstance tableInstance, int startPage, int endPage)
			{
				Global.Tracer.Assert(tableInstance != null, "(null != tableInstance)");
				this.ProcessTableRowInstances(tableInstance.HeaderRowInstances, startPage);
				this.ProcessTableRowInstances(tableInstance.FooterRowInstances, endPage);
				if (tableInstance.TableGroupInstances != null)
				{
					this.ProcessTableGroupInstances(tableInstance.TableGroupInstances, startPage, tableInstance.ChildrenStartAndEndPages);
					return;
				}
				this.ProcessTableDetailInstances(tableInstance.TableDetailInstances, startPage, tableInstance.ChildrenStartAndEndPages);
			}

			// Token: 0x06008D83 RID: 36227 RVA: 0x002404D4 File Offset: 0x0023E6D4
			private void ProcessTableRowInstances(TableRowInstance[] rowInstances, int startPage)
			{
				if (rowInstances != null)
				{
					foreach (TableRowInstance tableRowInstance in rowInstances)
					{
						Global.Tracer.Assert(tableRowInstance != null, "(null != rowInstance)");
						long chunkOffset = tableRowInstance.ChunkOffset;
						TableRowInstanceInfo instanceInfo = tableRowInstance.GetInstanceInfo(this.m_chunkManager);
						this.m_upgradeManager.AddInstance(instanceInfo, tableRowInstance, chunkOffset);
						this.ProcessReportItemColInstance(tableRowInstance.TableRowReportItemColInstance, startPage, startPage);
					}
				}
			}

			// Token: 0x06008D84 RID: 36228 RVA: 0x0024053C File Offset: 0x0023E73C
			private void ProcessTableDetailInstances(TableDetailInstanceList detailInstances, int startPage, RenderingPagesRangesList instancePages)
			{
				if (detailInstances != null)
				{
					int count = instancePages.Count;
					int num = 0;
					for (int i = 0; i < count; i++)
					{
						RenderingPagesRanges renderingPagesRanges = instancePages[i];
						for (int j = 0; j < renderingPagesRanges.NumberOfDetails; j++)
						{
							TableDetailInstance tableDetailInstance = detailInstances[num + j];
							Global.Tracer.Assert(tableDetailInstance != null && tableDetailInstance.TableDetailDef != null);
							long chunkOffset = tableDetailInstance.ChunkOffset;
							TableDetailInstanceInfo instanceInfo = tableDetailInstance.GetInstanceInfo(this.m_chunkManager);
							this.m_upgradeManager.AddInstance(instanceInfo, tableDetailInstance, chunkOffset);
							this.ProcessTableRowInstances(tableDetailInstance.DetailRowInstances, startPage + i);
						}
						num += renderingPagesRanges.NumberOfDetails;
					}
				}
			}

			// Token: 0x06008D85 RID: 36229 RVA: 0x002405F4 File Offset: 0x0023E7F4
			private void ProcessTableGroupInstances(TableGroupInstanceList groupInstances, int startPage, RenderingPagesRangesList groupInstancePages)
			{
				if (groupInstances != null)
				{
					for (int i = 0; i < groupInstances.Count; i++)
					{
						TableGroupInstance tableGroupInstance = groupInstances[i];
						Global.Tracer.Assert(tableGroupInstance != null, "(null != groupInstance)");
						long chunkOffset = tableGroupInstance.ChunkOffset;
						TableGroupInstanceInfo instanceInfo = tableGroupInstance.GetInstanceInfo(this.m_chunkManager);
						this.m_upgradeManager.AddInstance(instanceInfo, tableGroupInstance, chunkOffset);
						RenderingPagesRanges renderingPagesRanges = groupInstancePages[i];
						this.ProcessTableRowInstances(tableGroupInstance.HeaderRowInstances, renderingPagesRanges.StartPage);
						this.ProcessTableRowInstances(tableGroupInstance.FooterRowInstances, renderingPagesRanges.EndPage);
						if (tableGroupInstance.SubGroupInstances != null)
						{
							this.ProcessTableGroupInstances(tableGroupInstance.SubGroupInstances, renderingPagesRanges.StartPage, tableGroupInstance.ChildrenStartAndEndPages);
						}
						else
						{
							this.ProcessTableDetailInstances(tableGroupInstance.TableDetailInstances, renderingPagesRanges.StartPage, tableGroupInstance.ChildrenStartAndEndPages);
						}
					}
				}
			}

			// Token: 0x06008D86 RID: 36230 RVA: 0x002406C8 File Offset: 0x0023E8C8
			private void ProcessMatrix(Matrix matrixDef, MatrixInstance matrixInstance, int startPage, int endPage)
			{
				if (matrixInstance.CornerContent != null)
				{
					this.ProcessReportItemInstance(matrixInstance.CornerContent, startPage, startPage);
				}
				int[] array = new int[matrixInstance.CellRowCount];
				this.ProcessMatrixHeadings(matrixInstance.RowInstances, matrixInstance.ChildrenStartAndEndPages, ref array, startPage, endPage);
				this.ProcessMatrixHeadings(matrixInstance.ColumnInstances, null, ref array, startPage, endPage);
				this.ProcessMatrixCells(matrixInstance, array);
			}

			// Token: 0x06008D87 RID: 36231 RVA: 0x00240728 File Offset: 0x0023E928
			private void ProcessMatrixCells(MatrixInstance matrixInstance, int[] rowPages)
			{
				if (matrixInstance.Cells != null)
				{
					for (int i = 0; i < matrixInstance.CellRowCount; i++)
					{
						for (int j = 0; j < matrixInstance.CellColumnCount; j++)
						{
							MatrixCellInstance matrixCellInstance = matrixInstance.Cells[i][j];
							Global.Tracer.Assert(matrixCellInstance != null, "(null != cellInstance)");
							long chunkOffset = matrixCellInstance.ChunkOffset;
							MatrixCellInstanceInfo instanceInfo = matrixCellInstance.GetInstanceInfo(this.m_chunkManager);
							this.m_upgradeManager.AddInstance(instanceInfo, matrixCellInstance, chunkOffset);
							if (matrixCellInstance.Content != null)
							{
								this.ProcessReportItemInstance(matrixCellInstance.Content, rowPages[i], rowPages[i]);
							}
							else
							{
								ReportItem cellReportItem = matrixInstance.MatrixDef.GetCellReportItem(instanceInfo.RowIndex, instanceInfo.ColumnIndex);
								if (cellReportItem != null)
								{
									this.ProcessReportItem(cellReportItem, instanceInfo.ContentUniqueNames, rowPages[i]);
								}
							}
						}
					}
				}
			}

			// Token: 0x06008D88 RID: 36232 RVA: 0x00240808 File Offset: 0x0023EA08
			private void ProcessMatrixHeadings(MatrixHeadingInstanceList headings, RenderingPagesRangesList pagesList, ref int[] rowPages, int startPage, int endPage)
			{
				if (headings != null)
				{
					for (int i = 0; i < headings.Count; i++)
					{
						MatrixHeadingInstance matrixHeadingInstance = headings[i];
						Global.Tracer.Assert(matrixHeadingInstance != null, "(null != headingInstance)");
						long chunkOffset = matrixHeadingInstance.ChunkOffset;
						MatrixHeading matrixHeadingDef = matrixHeadingInstance.MatrixHeadingDef;
						MatrixHeadingInstanceInfo instanceInfo = matrixHeadingInstance.GetInstanceInfo(this.m_chunkManager);
						this.m_upgradeManager.AddInstance(instanceInfo, matrixHeadingInstance, chunkOffset);
						if (pagesList != null && pagesList.Count != 0)
						{
							Global.Tracer.Assert(headings.Count == pagesList.Count, "(headings.Count == pagesList.Count)");
							startPage = pagesList[i].StartPage;
							endPage = pagesList[i].EndPage;
						}
						if (!matrixHeadingDef.IsColumn)
						{
							Upgrader.SetRowPages(ref rowPages, instanceInfo.HeadingCellIndex, instanceInfo.HeadingSpan, startPage);
						}
						if (matrixHeadingInstance.Content != null)
						{
							this.ProcessReportItemInstance(matrixHeadingInstance.Content, startPage, endPage);
						}
						else
						{
							ReportItem reportItem = ((matrixHeadingInstance.IsSubtotal && matrixHeadingDef.Subtotal != null) ? matrixHeadingDef.Subtotal.ReportItem : matrixHeadingDef.ReportItem);
							if (reportItem != null)
							{
								this.ProcessReportItem(reportItem, instanceInfo.ContentUniqueNames, startPage);
							}
						}
						if (matrixHeadingInstance.SubHeadingInstances != null)
						{
							this.ProcessMatrixHeadings(matrixHeadingInstance.SubHeadingInstances, matrixHeadingInstance.ChildrenStartAndEndPages, ref rowPages, startPage, endPage);
						}
					}
				}
			}

			// Token: 0x04004F64 RID: 20324
			private ChunkManager.RenderingChunkManager m_chunkManager;

			// Token: 0x04004F65 RID: 20325
			private ChunkManager.UpgradeManager m_upgradeManager;

			// Token: 0x04004F66 RID: 20326
			private BookmarksHashtable m_bookmarks;

			// Token: 0x04004F67 RID: 20327
			private ReportDrillthroughInfo m_drillthroughs;
		}

		// Token: 0x02000CE9 RID: 3305
		private sealed class PageSectionsGenerator
		{
			// Token: 0x06008D8A RID: 36234 RVA: 0x00240960 File Offset: 0x0023EB60
			internal void Upgrade(ReportSnapshot reportSnapshot, ICatalogItemContext reportContext, ChunkManager.RenderingChunkManager chunkManager, ReportProcessing.CreateReportChunk createChunkCallback, IGetResource getResourceCallback, RenderingContext renderingContext, IDataProtection dataProtection)
			{
				this.m_chunkManager = chunkManager;
				this.m_createChunkCallback = createChunkCallback;
				Report report = reportSnapshot.Report;
				this.m_pageSectionContext = new ReportProcessing.PageSectionContext(true, report != null && report.MergeOnePass);
				ReportInstance reportInstance = reportSnapshot.ReportInstance;
				this.ProcessReport(report, reportInstance);
				this.ProcessPageHeaderFooter(report, reportInstance, reportSnapshot, reportContext, createChunkCallback, getResourceCallback, renderingContext, dataProtection);
			}

			// Token: 0x06008D8B RID: 36235 RVA: 0x002409BC File Offset: 0x0023EBBC
			private void ProcessPageHeaderFooter(Report report, ReportInstance reportInstance, ReportSnapshot reportSnapshot, ICatalogItemContext reportContext, ReportProcessing.CreateReportChunk createChunkCallback, IGetResource getResourceCallback, RenderingContext renderingContext, IDataProtection dataProtection)
			{
				ChunkManager.ProcessingChunkManager processingChunkManager = new ChunkManager.ProcessingChunkManager(createChunkCallback, false);
				ReportProcessing.PageMergeInteractive pageMergeInteractive = new ReportProcessing.PageMergeInteractive();
				ProcessingErrorContext processingErrorContext = new ProcessingErrorContext();
				UserProfileState allowUserProfileState = renderingContext.AllowUserProfileState;
				ReportRuntimeSetup reportRuntimeSetup = renderingContext.ReportRuntimeSetup;
				ReportDrillthroughInfo reportDrillthroughInfo = null;
				pageMergeInteractive.Process(this.m_pageSectionContext.PageTextboxes, reportSnapshot, reportContext, this.m_reportInstanceInfo.ReportName, reportSnapshot.Parameters, processingChunkManager, createChunkCallback, getResourceCallback, processingErrorContext, allowUserProfileState, reportRuntimeSetup, 0, dataProtection, ref reportDrillthroughInfo);
				processingChunkManager.PageSectionFlush(reportSnapshot);
				processingChunkManager.Close();
			}

			// Token: 0x06008D8C RID: 36236 RVA: 0x00240A30 File Offset: 0x0023EC30
			private void ProcessReport(Report report, ReportInstance reportInstance)
			{
				this.m_reportInstanceInfo = reportInstance.GetInstanceInfo(this.m_chunkManager) as ReportInstanceInfo;
				if (Visibility.IsVisible(report, reportInstance, this.m_reportInstanceInfo))
				{
					this.ProcessReportItemColInstance(reportInstance.ReportItemColInstance, report.StartPage, reportInstance.NumberOfPages - 1);
				}
			}

			// Token: 0x06008D8D RID: 36237 RVA: 0x00240A7D File Offset: 0x0023EC7D
			private void ProcessReportItemColInstance(ReportItemColInstance reportItemColInstance, int startPage, int endPage)
			{
				this.ProcessReportItemColInstance(reportItemColInstance, startPage, endPage, null, null);
			}

			// Token: 0x06008D8E RID: 36238 RVA: 0x00240A8C File Offset: 0x0023EC8C
			private void ProcessReportItemColInstance(ReportItemColInstance reportItemColInstance, int startPage, int endPage, bool[] tableColumnsVisible, IntList colSpans)
			{
				if (reportItemColInstance != null)
				{
					ReportItemColInstanceInfo instanceInfo = reportItemColInstance.GetInstanceInfo(this.m_chunkManager, false);
					reportItemColInstance.ChildrenNonComputedUniqueNames = instanceInfo.ChildrenNonComputedUniqueNames;
					Global.Tracer.Assert(reportItemColInstance.ReportItemColDef != null, "(null != reportItemColInstance.ReportItemColDef)");
					ReportItemIndexerList sortedReportItems = reportItemColInstance.ReportItemColDef.SortedReportItems;
					if (sortedReportItems != null)
					{
						int count = sortedReportItems.Count;
						List<DataRegion> list = new List<DataRegion>();
						int num = 0;
						int i = 0;
						while (i < count)
						{
							if (colSpans == null)
							{
								goto IL_00AC;
							}
							Global.Tracer.Assert(tableColumnsVisible != null && colSpans.Count <= tableColumnsVisible.Length);
							bool flag = Visibility.IsTableCellVisible(tableColumnsVisible, num, colSpans[i]);
							num += colSpans[i];
							if (flag)
							{
								goto IL_00AC;
							}
							IL_0201:
							i++;
							continue;
							IL_00AC:
							int num2 = startPage;
							int num3 = endPage;
							if (reportItemColInstance.ChildrenStartAndEndPages != null)
							{
								if (0 <= reportItemColInstance.ChildrenStartAndEndPages[i].StartPage)
								{
									num2 = reportItemColInstance.ChildrenStartAndEndPages[i].StartPage;
								}
								if (0 <= reportItemColInstance.ChildrenStartAndEndPages[i].EndPage)
								{
									num3 = reportItemColInstance.ChildrenStartAndEndPages[i].EndPage;
								}
							}
							ReportItem reportItem;
							if (sortedReportItems[i].IsComputed)
							{
								this.ProcessReportItemInstance(reportItemColInstance[sortedReportItems[i].Index], num2, num3, list);
								reportItem = reportItemColInstance[sortedReportItems[i].Index].ReportItemDef;
							}
							else
							{
								Global.Tracer.Assert(reportItemColInstance.ChildrenNonComputedUniqueNames != null && sortedReportItems[i].Index < reportItemColInstance.ChildrenNonComputedUniqueNames.Length);
								this.ProcessReportItem(reportItemColInstance.ReportItemColDef[i], reportItemColInstance.ChildrenNonComputedUniqueNames[sortedReportItems[i].Index], num2);
								reportItem = reportItemColInstance.ReportItemColDef[i];
							}
							Global.Tracer.Assert(reportItem != null, "(null != currentReportItem)");
							if (reportItem.RepeatedSiblingTextboxes != null)
							{
								this.m_pageSectionContext.PageTextboxes.IntegrateRepeatingTextboxValues(reportItem.RepeatedSiblingTextboxes, num2, num3);
								goto IL_0201;
							}
							goto IL_0201;
						}
						for (int j = 0; j < list.Count; j++)
						{
							DataRegion dataRegion = list[j];
							Global.Tracer.Assert(dataRegion.RepeatSiblings != null, "(null != dataRegion.RepeatSiblings)");
							for (int k = 0; k < dataRegion.RepeatSiblings.Count; k++)
							{
								ReportItem reportItem2 = reportItemColInstance.ReportItemColDef[dataRegion.RepeatSiblings[k]];
								Global.Tracer.Assert(reportItem2 != null, "(null != sibling)");
								this.m_pageSectionContext.PageTextboxes.IntegrateRepeatingTextboxValues(reportItem2.RepeatedSiblingTextboxes, dataRegion.StartPage, dataRegion.EndPage);
							}
						}
					}
				}
			}

			// Token: 0x06008D8F RID: 36239 RVA: 0x00240D58 File Offset: 0x0023EF58
			private void ProcessReportItem(ReportItem reportItem, NonComputedUniqueNames uniqueName, int startPage)
			{
				if (!Visibility.IsVisible(reportItem))
				{
					return;
				}
				if (reportItem.RepeatedSibling)
				{
					this.m_pageSectionContext.EnterRepeatingItem();
				}
				Rectangle rectangle = reportItem as Rectangle;
				if (rectangle != null && rectangle.ReportItems != null && rectangle.ReportItems.Count != 0)
				{
					Global.Tracer.Assert(uniqueName.ChildrenUniqueNames != null && rectangle.ReportItems.Count == uniqueName.ChildrenUniqueNames.Length);
					for (int i = 0; i < rectangle.ReportItems.Count; i++)
					{
						this.ProcessReportItem(rectangle.ReportItems[i], uniqueName.ChildrenUniqueNames[i], startPage);
					}
				}
				else if (reportItem is TextBox)
				{
					this.ProcessTextbox(reportItem as TextBox, null, null, null, uniqueName.UniqueName, startPage);
				}
				if (reportItem.RepeatedSibling)
				{
					reportItem.RepeatedSiblingTextboxes = this.m_pageSectionContext.ExitRepeatingItem();
				}
			}

			// Token: 0x06008D90 RID: 36240 RVA: 0x00240E34 File Offset: 0x0023F034
			private void ProcessTextbox(TextBox textbox, TextBoxInstance textboxInstance, TextBoxInstanceInfo textboxInstanceInfo, SimpleTextBoxInstanceInfo simpleTextboxInstanceInfo, int uniqueName, int startPage)
			{
				if (textbox == null)
				{
					return;
				}
				if (!this.m_pageSectionContext.IsParentVisible())
				{
					return;
				}
				if (!Visibility.IsVisible(textbox, textboxInstance, textboxInstanceInfo))
				{
					return;
				}
				if (!this.m_pageSectionContext.InMatrixCell)
				{
					bool inRepeatingItem = this.m_pageSectionContext.InRepeatingItem;
				}
				if (textboxInstance != null)
				{
					if (textboxInstanceInfo != null)
					{
						this.m_pageSectionContext.PageTextboxes.AddTextboxValue(startPage, textbox.Name, textboxInstanceInfo.OriginalValue);
						return;
					}
					if (simpleTextboxInstanceInfo != null)
					{
						this.m_pageSectionContext.PageTextboxes.AddTextboxValue(startPage, textbox.Name, simpleTextboxInstanceInfo.OriginalValue);
						return;
					}
				}
				else
				{
					Global.Tracer.Assert(textbox.Value != null && ExpressionInfo.Types.Constant == textbox.Value.Type);
					this.m_pageSectionContext.PageTextboxes.AddTextboxValue(startPage, textbox.Name, textbox.Value.Value);
				}
			}

			// Token: 0x06008D91 RID: 36241 RVA: 0x00240F08 File Offset: 0x0023F108
			private void ProcessReportItemInstance(ReportItemInstance reportItemInstance, int startPage, int endPage)
			{
				List<DataRegion> list = new List<DataRegion>();
				this.ProcessReportItemInstance(reportItemInstance, startPage, endPage, list);
				Global.Tracer.Assert(list.Count == 0, "(0 == dataRegionsWithRepeatSiblings.Count)");
			}

			// Token: 0x06008D92 RID: 36242 RVA: 0x00240F40 File Offset: 0x0023F140
			private void ProcessReportItemInstance(ReportItemInstance reportItemInstance, int startPage, int endPage, List<DataRegion> dataRegionsWithRepeatSiblings)
			{
				Global.Tracer.Assert(reportItemInstance != null, "(null != reportItemInstance)");
				ReportItem reportItemDef = reportItemInstance.ReportItemDef;
				TextBox textBox = reportItemDef as TextBox;
				TextBoxInstance textBoxInstance = reportItemInstance as TextBoxInstance;
				ReportItemInstanceInfo reportItemInstanceInfo = null;
				SimpleTextBoxInstanceInfo simpleTextBoxInstanceInfo = null;
				if (textBox != null && textBox.IsSimpleTextBox() && textBoxInstance != null)
				{
					simpleTextBoxInstanceInfo = textBoxInstance.GetSimpleInstanceInfo(this.m_chunkManager, false);
				}
				else
				{
					reportItemInstanceInfo = reportItemInstance.GetInstanceInfo(this.m_chunkManager);
				}
				if (!Visibility.IsVisible(reportItemDef, reportItemInstance, reportItemInstanceInfo))
				{
					return;
				}
				if (reportItemDef.RepeatedSibling)
				{
					this.m_pageSectionContext.EnterRepeatingItem();
				}
				if (textBox != null)
				{
					this.ProcessTextbox(textBox, textBoxInstance, reportItemInstanceInfo as TextBoxInstanceInfo, simpleTextBoxInstanceInfo, textBoxInstance.UniqueName, startPage);
				}
				else if (reportItemDef is Rectangle)
				{
					this.ProcessReportItemColInstance(((RectangleInstance)reportItemInstance).ReportItemColInstance, startPage, endPage);
				}
				else if (!(reportItemDef is SubReport) && reportItemDef is DataRegion)
				{
					DataRegion dataRegion = reportItemDef as DataRegion;
					if (dataRegion.RepeatSiblings != null)
					{
						dataRegion.StartPage = startPage;
						dataRegion.EndPage = endPage;
						dataRegionsWithRepeatSiblings.Add(reportItemDef as DataRegion);
					}
					if (reportItemDef is List)
					{
						this.ProcessList((List)reportItemDef, (ListInstance)reportItemInstance, startPage);
					}
					else if (reportItemDef is Matrix)
					{
						this.ProcessMatrix((Matrix)reportItemDef, (MatrixInstance)reportItemInstance, (MatrixInstanceInfo)reportItemInstanceInfo, startPage, endPage);
					}
					else if (reportItemDef is Table)
					{
						this.ProcessTable((Table)reportItemDef, (TableInstance)reportItemInstance, (TableInstanceInfo)reportItemInstanceInfo, startPage, endPage);
					}
				}
				if (reportItemDef.RepeatedSibling)
				{
					reportItemDef.RepeatedSiblingTextboxes = this.m_pageSectionContext.ExitRepeatingItem();
				}
			}

			// Token: 0x06008D93 RID: 36243 RVA: 0x002410C4 File Offset: 0x0023F2C4
			private void ProcessList(List listDef, ListInstance listInstance, int startPage)
			{
				if (listInstance.ListContents != null)
				{
					if (listDef.Grouping != null)
					{
						Global.Tracer.Assert(listInstance.ListContents.Count == listInstance.ChildrenStartAndEndPages.Count, "(listInstance.ListContents.Count == listInstance.ChildrenStartAndEndPages.Count)");
						for (int i = 0; i < listInstance.ChildrenStartAndEndPages.Count; i++)
						{
							ListContentInstance listContentInstance = listInstance.ListContents[i];
							ListContentInstanceInfo instanceInfo = listContentInstance.GetInstanceInfo(this.m_chunkManager);
							RenderingPagesRanges renderingPagesRanges = listInstance.ChildrenStartAndEndPages[i];
							if (Visibility.IsVisible(listDef.Visibility, instanceInfo.StartHidden))
							{
								this.ProcessReportItemColInstance(listContentInstance.ReportItemColInstance, renderingPagesRanges.StartPage, renderingPagesRanges.EndPage);
							}
						}
						return;
					}
					if (listInstance.ChildrenStartAndEndPages == null)
					{
						this.ProcessListContents(listDef, listInstance, startPage, 0, listInstance.ListContents.Count - 1);
						return;
					}
					for (int j = 0; j < listInstance.ChildrenStartAndEndPages.Count; j++)
					{
						RenderingPagesRanges renderingPagesRanges2 = listInstance.ChildrenStartAndEndPages[j];
						this.ProcessListContents(listDef, listInstance, startPage + j, renderingPagesRanges2.StartRow, renderingPagesRanges2.StartRow + renderingPagesRanges2.NumberOfDetails - 1);
					}
				}
			}

			// Token: 0x06008D94 RID: 36244 RVA: 0x002411E8 File Offset: 0x0023F3E8
			private void ProcessListContents(List listDef, ListInstance listInstance, int page, int startRow, int endRow)
			{
				for (int i = startRow; i <= endRow; i++)
				{
					ListContentInstance listContentInstance = listInstance.ListContents[i];
					ListContentInstanceInfo instanceInfo = listContentInstance.GetInstanceInfo(this.m_chunkManager);
					if (Visibility.IsVisible(listDef.Visibility, instanceInfo.StartHidden))
					{
						this.ProcessReportItemColInstance(listContentInstance.ReportItemColInstance, page, page);
					}
				}
			}

			// Token: 0x06008D95 RID: 36245 RVA: 0x00241240 File Offset: 0x0023F440
			private void ProcessTable(Table tableDef, TableInstance tableInstance, TableInstanceInfo tableInstanceInfo, int startPage, int endPage)
			{
				Global.Tracer.Assert(tableInstance != null && tableDef.TableColumns != null, "(null != tableInstance && null != tableDef.TableColumns)");
				bool[] array = new bool[tableDef.TableColumns.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Visibility.IsVisible(tableDef.TableColumns[i].Visibility, tableInstanceInfo.ColumnInstances[i].StartHidden);
				}
				this.ProcessTableRowInstances(tableInstance.HeaderRowInstances, array, startPage, tableDef.HeaderRepeatOnNewPage, endPage);
				if (tableInstance.TableGroupInstances != null)
				{
					this.ProcessTableGroupInstances(tableInstance.TableGroupInstances, array, startPage, tableInstance.ChildrenStartAndEndPages);
				}
				else
				{
					this.ProcessTableDetailInstances(tableInstance.TableDetailInstances, array, startPage, tableInstance.ChildrenStartAndEndPages);
				}
				this.ProcessTableRowInstances(tableInstance.FooterRowInstances, array, endPage, tableDef.FooterRepeatOnNewPage, startPage);
			}

			// Token: 0x06008D96 RID: 36246 RVA: 0x00241314 File Offset: 0x0023F514
			private void ProcessTableRowInstances(TableRowInstance[] rowInstances, bool[] tableColumnsVisible, int startPage, bool repeat, int endPage)
			{
				if (!repeat)
				{
					this.ProcessTableRowInstances(rowInstances, tableColumnsVisible, startPage);
					return;
				}
				int num = Math.Min(startPage, endPage);
				int num2 = Math.Max(startPage, endPage);
				Global.Tracer.Assert(0 <= num && 0 <= num2, "(0 <= minPage && 0 <= maxPage)");
				for (int i = num; i <= num2; i++)
				{
					this.ProcessTableRowInstances(rowInstances, tableColumnsVisible, i);
				}
			}

			// Token: 0x06008D97 RID: 36247 RVA: 0x00241374 File Offset: 0x0023F574
			private void ProcessTableRowInstances(TableRowInstance[] rowInstances, bool[] tableColumnsVisible, int startPage)
			{
				if (rowInstances != null)
				{
					foreach (TableRowInstance tableRowInstance in rowInstances)
					{
						Global.Tracer.Assert(tableRowInstance != null && tableRowInstance.TableRowDef != null);
						TableRowInstanceInfo instanceInfo = tableRowInstance.GetInstanceInfo(this.m_chunkManager);
						if (Visibility.IsVisible(tableRowInstance.TableRowDef.Visibility, instanceInfo.StartHidden))
						{
							this.ProcessReportItemColInstance(tableRowInstance.TableRowReportItemColInstance, startPage, startPage, tableColumnsVisible, tableRowInstance.TableRowDef.ColSpans);
						}
					}
				}
			}

			// Token: 0x06008D98 RID: 36248 RVA: 0x002413F0 File Offset: 0x0023F5F0
			private void ProcessTableDetailInstances(TableDetailInstanceList detailInstances, bool[] tableColumnsVisible, int startPage, RenderingPagesRangesList instancePages)
			{
				if (detailInstances != null)
				{
					Global.Tracer.Assert(instancePages != null, "(null != instancePages)");
					int count = instancePages.Count;
					int num = 0;
					for (int i = 0; i < count; i++)
					{
						RenderingPagesRanges renderingPagesRanges = instancePages[i];
						for (int j = 0; j < renderingPagesRanges.NumberOfDetails; j++)
						{
							TableDetailInstance tableDetailInstance = detailInstances[num + j];
							Global.Tracer.Assert(tableDetailInstance != null && tableDetailInstance.TableDetailDef != null);
							TableDetailInstanceInfo instanceInfo = tableDetailInstance.GetInstanceInfo(this.m_chunkManager);
							if (Visibility.IsVisible(tableDetailInstance.TableDetailDef.Visibility, instanceInfo.StartHidden))
							{
								this.ProcessTableRowInstances(tableDetailInstance.DetailRowInstances, tableColumnsVisible, startPage + i);
							}
						}
						num += renderingPagesRanges.NumberOfDetails;
					}
				}
			}

			// Token: 0x06008D99 RID: 36249 RVA: 0x002414C0 File Offset: 0x0023F6C0
			private void ProcessTableGroupInstances(TableGroupInstanceList groupInstances, bool[] tableColumnsVisible, int startPage, RenderingPagesRangesList groupInstancePages)
			{
				if (groupInstances != null)
				{
					int count = groupInstances.Count;
					Global.Tracer.Assert(groupInstancePages != null && count == groupInstancePages.Count, "(null != groupInstancePages && instanceCount == groupInstancePages.Count)");
					for (int i = 0; i < count; i++)
					{
						TableGroupInstance tableGroupInstance = groupInstances[i];
						Global.Tracer.Assert(tableGroupInstance != null && tableGroupInstance.TableGroupDef != null);
						TableGroupInstanceInfo instanceInfo = tableGroupInstance.GetInstanceInfo(this.m_chunkManager);
						RenderingPagesRanges renderingPagesRanges = groupInstancePages[i];
						if (Visibility.IsVisible(tableGroupInstance.TableGroupDef.Visibility, instanceInfo.StartHidden))
						{
							this.ProcessTableRowInstances(tableGroupInstance.HeaderRowInstances, tableColumnsVisible, renderingPagesRanges.StartPage, tableGroupInstance.TableGroupDef.HeaderRepeatOnNewPage, renderingPagesRanges.EndPage);
							if (tableGroupInstance.SubGroupInstances != null)
							{
								this.ProcessTableGroupInstances(tableGroupInstance.SubGroupInstances, tableColumnsVisible, renderingPagesRanges.StartPage, tableGroupInstance.ChildrenStartAndEndPages);
							}
							else
							{
								this.ProcessTableDetailInstances(tableGroupInstance.TableDetailInstances, tableColumnsVisible, renderingPagesRanges.StartPage, tableGroupInstance.ChildrenStartAndEndPages);
							}
							this.ProcessTableRowInstances(tableGroupInstance.FooterRowInstances, tableColumnsVisible, renderingPagesRanges.EndPage, tableGroupInstance.TableGroupDef.FooterRepeatOnNewPage, renderingPagesRanges.StartPage);
						}
					}
				}
			}

			// Token: 0x06008D9A RID: 36250 RVA: 0x002415EC File Offset: 0x0023F7EC
			private void ProcessMatrix(Matrix matrixDef, MatrixInstance matrixInstance, MatrixInstanceInfo matrixInstanceInfo, int startPage, int endPage)
			{
				ReportProcessing.PageSectionContext pageSectionContext = this.m_pageSectionContext;
				this.m_pageSectionContext = new ReportProcessing.PageSectionContext(pageSectionContext);
				ReportProcessing.PageTextboxes pageTextboxes = null;
				if (matrixInstance.CornerContent != null)
				{
					this.m_pageSectionContext.EnterRepeatingItem();
					this.ProcessReportItemInstance(matrixInstance.CornerContent, startPage, endPage);
					pageTextboxes = this.m_pageSectionContext.ExitRepeatingItem();
				}
				matrixDef.InitializePageSectionProcessing();
				bool[] array = new bool[matrixInstance.CellRowCount];
				int[] array2 = new int[matrixInstance.CellRowCount];
				bool[] array3 = new bool[matrixInstance.CellColumnCount];
				this.ProcessMatrixHeadings(matrixInstance, matrixInstance.ColumnInstances, null, ref array3, ref array2, startPage, endPage);
				this.ProcessMatrixHeadings(matrixInstance, matrixInstance.RowInstances, matrixInstance.ChildrenStartAndEndPages, ref array, ref array2, startPage, endPage);
				this.ProcessMatrixCells(matrixInstance, array, array2, array3);
				this.m_pageSectionContext = pageSectionContext;
				this.m_pageSectionContext.PageTextboxes.IntegrateRepeatingTextboxValues(pageTextboxes, startPage, endPage);
				this.m_pageSectionContext.PageTextboxes.IntegrateRepeatingTextboxValues(matrixInstance.MatrixDef.ColumnHeaderPageTextboxes, startPage, endPage);
				this.m_pageSectionContext.PageTextboxes.IntegrateNonRepeatingTextboxValues(matrixInstance.MatrixDef.RowHeaderPageTextboxes);
				this.m_pageSectionContext.PageTextboxes.IntegrateNonRepeatingTextboxValues(matrixInstance.MatrixDef.CellPageTextboxes);
			}

			// Token: 0x06008D9B RID: 36251 RVA: 0x00241718 File Offset: 0x0023F918
			private void ProcessMatrixCells(MatrixInstance matrixInstance, bool[] rowCanGetReferenced, int[] rowPages, bool[] colCanGetReferenced)
			{
				if (matrixInstance.Cells != null)
				{
					this.m_pageSectionContext.EnterRepeatingItem();
					this.m_pageSectionContext.InMatrixCell = true;
					for (int i = 0; i < matrixInstance.CellRowCount; i++)
					{
						if (rowCanGetReferenced[i])
						{
							for (int j = 0; j < matrixInstance.CellColumnCount; j++)
							{
								if (colCanGetReferenced[j])
								{
									MatrixCellInstance matrixCellInstance = matrixInstance.Cells[i][j];
									Global.Tracer.Assert(matrixCellInstance != null, "(null != cellInstance)");
									MatrixCellInstanceInfo instanceInfo = matrixCellInstance.GetInstanceInfo(this.m_chunkManager);
									if (matrixCellInstance.Content != null)
									{
										this.ProcessReportItemInstance(matrixCellInstance.Content, rowPages[i], rowPages[i]);
									}
									else
									{
										ReportItem cellReportItem = matrixInstance.MatrixDef.GetCellReportItem(instanceInfo.RowIndex, instanceInfo.ColumnIndex);
										if (cellReportItem != null)
										{
											this.ProcessReportItem(cellReportItem, instanceInfo.ContentUniqueNames, rowPages[i]);
										}
									}
								}
							}
						}
					}
					this.m_pageSectionContext.InMatrixCell = false;
					matrixInstance.MatrixDef.CellPageTextboxes.IntegrateNonRepeatingTextboxValues(this.m_pageSectionContext.ExitRepeatingItem());
				}
			}

			// Token: 0x06008D9C RID: 36252 RVA: 0x00241828 File Offset: 0x0023FA28
			private static void SetCellVisiblity(ref bool[] cellVisiblity, int start, int span, bool visible)
			{
				Global.Tracer.Assert(cellVisiblity != null && start >= 0 && span > 0 && start + span <= cellVisiblity.Length, "(null != cellVisiblity && start >= 0 && span > 0 && start + span <= cellVisiblity.Length)");
				for (int i = 0; i < span; i++)
				{
					cellVisiblity[start + i] = visible;
				}
			}

			// Token: 0x06008D9D RID: 36253 RVA: 0x00241874 File Offset: 0x0023FA74
			private void ProcessMatrixHeadings(MatrixInstance matrixInstance, MatrixHeadingInstanceList headings, RenderingPagesRangesList pagesList, ref bool[] cellsCanGetReferenced, ref int[] rowPages, int startPage, int endPage)
			{
				if (headings != null)
				{
					for (int i = 0; i < headings.Count; i++)
					{
						MatrixHeadingInstance matrixHeadingInstance = headings[i];
						MatrixHeading matrixHeadingDef = matrixHeadingInstance.MatrixHeadingDef;
						MatrixHeadingInstanceInfo instanceInfo = matrixHeadingInstance.GetInstanceInfo(this.m_chunkManager);
						this.m_pageSectionContext.EnterMatrixHeadingScope(Visibility.IsVisible(matrixHeadingDef.Visibility, instanceInfo.StartHidden), matrixHeadingDef.IsColumn);
						if (pagesList != null && pagesList.Count != 0)
						{
							Global.Tracer.Assert(headings.Count == pagesList.Count, "(headings.Count == pagesList.Count)");
							startPage = pagesList[i].StartPage;
							endPage = pagesList[i].EndPage;
						}
						if (!matrixHeadingDef.IsColumn)
						{
							Upgrader.SetRowPages(ref rowPages, instanceInfo.HeadingCellIndex, instanceInfo.HeadingSpan, startPage);
						}
						this.m_pageSectionContext.EnterRepeatingItem();
						if (matrixHeadingInstance.Content != null)
						{
							this.ProcessReportItemInstance(matrixHeadingInstance.Content, startPage, endPage);
						}
						else
						{
							ReportItem reportItem = ((matrixHeadingInstance.IsSubtotal && matrixHeadingDef.Subtotal != null) ? matrixHeadingDef.Subtotal.ReportItem : matrixHeadingDef.ReportItem);
							if (reportItem != null)
							{
								this.ProcessReportItem(reportItem, instanceInfo.ContentUniqueNames, startPage);
							}
						}
						if (matrixHeadingDef.IsColumn)
						{
							matrixInstance.MatrixDef.ColumnHeaderPageTextboxes.IntegrateNonRepeatingTextboxValues(this.m_pageSectionContext.ExitRepeatingItem());
						}
						else
						{
							matrixInstance.MatrixDef.RowHeaderPageTextboxes.IntegrateRepeatingTextboxValues(this.m_pageSectionContext.ExitRepeatingItem(), startPage, endPage);
						}
						if (matrixHeadingInstance.IsSubtotal)
						{
							this.m_pageSectionContext.EnterMatrixSubtotalScope(matrixHeadingDef.IsColumn);
						}
						Upgrader.PageSectionsGenerator.SetCellVisiblity(ref cellsCanGetReferenced, instanceInfo.HeadingCellIndex, instanceInfo.HeadingSpan, this.m_pageSectionContext.IsParentVisible());
						if (matrixHeadingInstance.SubHeadingInstances != null)
						{
							this.ProcessMatrixHeadings(matrixInstance, matrixHeadingInstance.SubHeadingInstances, matrixHeadingInstance.ChildrenStartAndEndPages, ref cellsCanGetReferenced, ref rowPages, startPage, endPage);
						}
						if (matrixHeadingInstance.IsSubtotal)
						{
							this.m_pageSectionContext.ExitMatrixHeadingScope(matrixHeadingDef.IsColumn);
						}
						this.m_pageSectionContext.ExitMatrixHeadingScope(matrixHeadingDef.IsColumn);
					}
				}
			}

			// Token: 0x04004F68 RID: 20328
			private ChunkManager.RenderingChunkManager m_chunkManager;

			// Token: 0x04004F69 RID: 20329
			private ReportProcessing.CreateReportChunk m_createChunkCallback;

			// Token: 0x04004F6A RID: 20330
			private ReportProcessing.PageSectionContext m_pageSectionContext;

			// Token: 0x04004F6B RID: 20331
			private ReportInstanceInfo m_reportInstanceInfo;
		}

		// Token: 0x02000CEA RID: 3306
		private sealed class UpgraderForV1Beta2
		{
			// Token: 0x06008D9F RID: 36255 RVA: 0x00241A73 File Offset: 0x0023FC73
			internal void Upgrade(Report report)
			{
				ReportPublishing.CalculateChildrenPostions(report);
				ReportPublishing.CalculateChildrenDependencies(report);
			}

			// Token: 0x06008DA0 RID: 36256 RVA: 0x00241A84 File Offset: 0x0023FC84
			internal void Upgrade(ReportSnapshot reportSnapshot, ChunkManager.RenderingChunkManager chunkManager, ReportProcessing.CreateReportChunk createChunkCallback)
			{
				Report report = reportSnapshot.Report;
				ReportInstance reportInstance = reportSnapshot.ReportInstance;
				this.Upgrade(report);
				this.m_pagination = new ReportProcessing.Pagination(report.InteractiveHeightValue);
				this.m_navigationInfo = new ReportProcessing.NavigationInfo();
				this.m_onePass = report.MergeOnePass;
				this.m_chunkManager = chunkManager;
				this.m_hasDocMap = reportSnapshot.HasDocumentMap;
				this.ProcessReport(report, reportInstance);
			}

			// Token: 0x06008DA1 RID: 36257 RVA: 0x00241AE9 File Offset: 0x0023FCE9
			private void ProcessReport(Report report, ReportInstance reportInstance)
			{
				this.m_pagination.SetReportItemStartPage(report, false);
				this.ProcessReportItemColInstance(reportInstance.ReportItemColInstance);
				this.m_pagination.ProcessEndPage(reportInstance, report, false, false);
				reportInstance.NumberOfPages = report.EndPage + 1;
			}

			// Token: 0x06008DA2 RID: 36258 RVA: 0x00241B24 File Offset: 0x0023FD24
			private void ProcessReportItemColInstance(ReportItemColInstance collectionInstance)
			{
				if (collectionInstance == null)
				{
					return;
				}
				ReportItemCollection reportItemColDef = collectionInstance.ReportItemColDef;
				if (reportItemColDef == null || reportItemColDef.Count < 1)
				{
					return;
				}
				ReportItemColInstanceInfo instanceInfo = collectionInstance.GetInstanceInfo(this.m_chunkManager, false);
				collectionInstance.ChildrenNonComputedUniqueNames = instanceInfo.ChildrenNonComputedUniqueNames;
				ReportItem parent = reportItemColDef[0].Parent;
				int num = parent.StartPage;
				bool flag = false;
				if (parent is Report || parent is List || parent is Rectangle || parent is SubReport || parent is CustomReportItem)
				{
					flag = true;
					collectionInstance.ChildrenStartAndEndPages = new RenderingPagesRangesList();
				}
				int i = 0;
				int num2 = 0;
				while (i < reportItemColDef.Count)
				{
					bool flag2;
					int num3;
					ReportItem reportItem;
					reportItemColDef.GetReportItem(i, out flag2, out num3, out reportItem);
					if (flag2)
					{
						reportItem = reportItemColDef.ComputedReportItems[num3];
						this.ProcessReportItemInstance(collectionInstance.ReportItemInstances[num2]);
					}
					else
					{
						collectionInstance.SetPaginationForNonComputedChild(this.m_pagination, reportItem, parent);
						reportItem.ProcessNavigationAction(this.m_navigationInfo, collectionInstance.ChildrenNonComputedUniqueNames[num3], reportItem.StartPage);
					}
					num = Math.Max(num, reportItem.EndPage);
					if (flag)
					{
						RenderingPagesRanges renderingPagesRanges = default(RenderingPagesRanges);
						renderingPagesRanges.StartPage = reportItem.StartPage;
						renderingPagesRanges.EndPage = reportItem.EndPage;
						collectionInstance.ChildrenStartAndEndPages.Add(renderingPagesRanges);
					}
					i++;
				}
				if (num > parent.EndPage)
				{
					parent.EndPage = num;
					this.m_pagination.SetCurrentPageHeight(parent, 1.0);
				}
			}

			// Token: 0x06008DA3 RID: 36259 RVA: 0x00241C9C File Offset: 0x0023FE9C
			private void ProcessReportItemInstance(ReportItemInstance reportItemInstance)
			{
				ReportItem reportItemDef = reportItemInstance.ReportItemDef;
				bool flag = reportItemDef is SubReport || reportItemDef is Rectangle || reportItemDef is DataRegion;
				this.m_pagination.EnterIgnorePageBreak(reportItemDef.Visibility, false);
				ReportItemInstanceInfo instanceInfo = reportItemInstance.GetInstanceInfo(this.m_chunkManager);
				reportItemDef.StartHidden = instanceInfo.StartHidden;
				this.m_pagination.EnterIgnoreHeight(reportItemDef.StartHidden);
				string label = instanceInfo.Label;
				if (reportItemDef is Rectangle)
				{
					Rectangle rectangle = (Rectangle)reportItemDef;
					RectangleInstance rectangleInstance = (RectangleInstance)reportItemInstance;
					bool flag2 = this.m_pagination.CalculateSoftPageBreak(rectangle, 0.0, (double)rectangle.DistanceBeforeTop, false);
					this.m_pagination.SetReportItemStartPage(rectangle, flag2);
					if (label != null)
					{
						this.m_navigationInfo.EnterDocumentMapChildren();
					}
					this.ProcessReportItemColInstance(rectangleInstance.ReportItemColInstance);
					this.m_pagination.ProcessEndPage(rectangleInstance, reportItemDef, rectangle.PageBreakAtEnd || rectangle.PageBreakAtStart, false);
				}
				else if (reportItemDef is DataRegion)
				{
					DataRegion dataRegion = (DataRegion)reportItemDef;
					bool flag3 = this.m_pagination.CalculateSoftPageBreak(reportItemDef, 0.0, (double)dataRegion.DistanceBeforeTop, false);
					this.m_pagination.SetReportItemStartPage(reportItemDef, flag3);
					if (reportItemDef is List)
					{
						ListInstance listInstance = (ListInstance)reportItemInstance;
						List list = (List)reportItemDef;
						if (-1 == list.ContentStartPage)
						{
							list.ContentStartPage = list.StartPage;
						}
						if (label != null)
						{
							this.m_navigationInfo.EnterDocumentMapChildren();
						}
						this.ProcessList(list, listInstance);
						this.m_pagination.ProcessListRenderingPages(listInstance, list);
					}
					else if (reportItemDef is Matrix)
					{
						if (label != null)
						{
							this.m_navigationInfo.EnterDocumentMapChildren();
						}
						this.ProcessMatrix((Matrix)reportItemDef, (MatrixInstance)reportItemInstance);
					}
					else if (reportItemDef is Chart)
					{
						if (label != null)
						{
							this.m_navigationInfo.EnterDocumentMapChildren();
						}
						ChartInstance chartInstance = (ChartInstance)reportItemInstance;
						this.m_pagination.ProcessEndPage(chartInstance, reportItemDef, ((Chart)reportItemDef).PageBreakAtEnd, false);
					}
					else if (reportItemDef is Table)
					{
						if (label != null)
						{
							this.m_navigationInfo.EnterDocumentMapChildren();
						}
						TableInstance tableInstance = (TableInstance)reportItemInstance;
						Table table = (Table)reportItemDef;
						this.ProcessTable(table, tableInstance);
						this.m_pagination.ProcessTableRenderingPages(tableInstance, (Table)reportItemDef);
					}
					else if (reportItemDef is OWCChart)
					{
						if (label != null)
						{
							this.m_navigationInfo.EnterDocumentMapChildren();
						}
						this.m_pagination.ProcessEndPage((OWCChartInstance)reportItemInstance, reportItemDef, ((OWCChart)reportItemDef).PageBreakAtEnd, false);
					}
				}
				else
				{
					if (reportItemDef.Parent != null && (reportItemDef.Parent is Rectangle || reportItemDef.Parent is Report || reportItemDef.Parent is List))
					{
						bool flag4 = this.m_pagination.CalculateSoftPageBreak(reportItemDef, 0.0, (double)reportItemDef.DistanceBeforeTop, false, false);
						this.m_pagination.SetReportItemStartPage(reportItemDef, flag4);
					}
					if (reportItemDef is SubReport)
					{
						if (label != null)
						{
							this.m_navigationInfo.EnterDocumentMapChildren();
						}
						SubReport subReport = (SubReport)reportItemDef;
						SubReportInstance subReportInstance = (SubReportInstance)reportItemInstance;
						if (subReportInstance.ReportInstance != null)
						{
							this.ProcessReport(subReport.Report, subReportInstance.ReportInstance);
						}
						this.m_pagination.ProcessEndPage(subReportInstance, subReport, false, false);
					}
				}
				if (label != null)
				{
					this.m_navigationInfo.AddToDocumentMap(reportItemInstance.GetDocumentMapUniqueName(), flag, reportItemDef.StartPage, label);
				}
				if (reportItemDef.Parent != null)
				{
					if (reportItemDef.EndPage > reportItemDef.Parent.EndPage)
					{
						reportItemDef.Parent.EndPage = reportItemDef.EndPage;
						reportItemDef.Parent.BottomInEndPage = reportItemDef.BottomInEndPage;
						if (reportItemDef.Parent is List)
						{
							((List)reportItemDef.Parent).ContentStartPage = reportItemDef.EndPage;
						}
					}
					else if (reportItemDef.EndPage == reportItemDef.Parent.EndPage)
					{
						reportItemDef.Parent.BottomInEndPage = Math.Max(reportItemDef.Parent.BottomInEndPage, reportItemDef.BottomInEndPage);
					}
				}
				this.m_pagination.LeaveIgnorePageBreak(reportItemDef.Visibility, false);
			}

			// Token: 0x06008DA4 RID: 36260 RVA: 0x002420A0 File Offset: 0x002402A0
			private void ProcessList(List listDef, ListInstance listInstance)
			{
				listInstance.ChildrenStartAndEndPages = new RenderingPagesRangesList();
				this.m_pagination.EnterIgnorePageBreak(listDef.Visibility, false);
				if (listDef.Grouping != null)
				{
					this.ProcessListGroupContents(listDef, listInstance);
				}
				else
				{
					this.ProcessListDetailContents(listDef, listInstance);
				}
				this.m_pagination.LeaveIgnorePageBreak(listDef.Visibility, false);
			}

			// Token: 0x06008DA5 RID: 36261 RVA: 0x002420F8 File Offset: 0x002402F8
			private void ProcessListGroupContents(List listDef, ListInstance listInstance)
			{
				for (int i = 0; i < listInstance.ListContents.Count; i++)
				{
					ListContentInstance listContentInstance = listInstance.ListContents[i];
					ListContentInstanceInfo instanceInfo = listContentInstance.GetInstanceInfo(this.m_chunkManager);
					listDef.StartHidden = instanceInfo.StartHidden;
					this.m_pagination.EnterIgnoreHeight(listDef.StartHidden);
					string label = instanceInfo.Label;
					if (i > 0)
					{
						bool flag = this.m_pagination.CalculateSoftPageBreak(null, 0.0, 0.0, false, listDef.Grouping.PageBreakAtStart);
						if (!this.m_pagination.IgnorePageBreak && (this.m_pagination.CanMoveToNextPage(listDef.Grouping.PageBreakAtStart) || flag))
						{
							int contentStartPage = listDef.ContentStartPage;
							listDef.ContentStartPage = contentStartPage + 1;
							this.m_pagination.SetCurrentPageHeight(listDef, 0.0);
						}
					}
					RenderingPagesRanges renderingPagesRanges = default(RenderingPagesRanges);
					renderingPagesRanges.StartPage = listDef.ContentStartPage;
					if (listDef.Grouping.GroupLabel != null && label != null)
					{
						this.m_navigationInfo.EnterDocumentMapChildren();
					}
					int startPage = listDef.StartPage;
					listDef.StartPage = listDef.ContentStartPage;
					this.ProcessReportItemColInstance(listContentInstance.ReportItemColInstance);
					this.m_pagination.ProcessEndGroupPage(listDef.IsListMostInner ? listDef.HeightValue : 0.0, listDef.Grouping.PageBreakAtEnd, listDef, true, listDef.StartHidden);
					listDef.ContentStartPage = listDef.EndPage;
					listDef.StartPage = startPage;
					if (this.m_pagination.ShouldItemMoveToChildStartPage(listDef))
					{
						renderingPagesRanges.StartPage = listContentInstance.ReportItemColInstance.ChildrenStartAndEndPages[0].StartPage;
					}
					renderingPagesRanges.EndPage = listDef.EndPage;
					listInstance.ChildrenStartAndEndPages.Add(renderingPagesRanges);
					if (listDef.Grouping.GroupLabel != null && label != null)
					{
						this.m_navigationInfo.AddToDocumentMap(listContentInstance.UniqueName, true, startPage, label);
					}
				}
			}

			// Token: 0x06008DA6 RID: 36262 RVA: 0x002422F0 File Offset: 0x002404F0
			private void ProcessListDetailContents(List listDef, ListInstance listInstance)
			{
				double heightValue = listDef.HeightValue;
				for (int i = 0; i < listInstance.ListContents.Count; i++)
				{
					ListContentInstance listContentInstance = listInstance.ListContents[i];
					ListContentInstanceInfo instanceInfo = listContentInstance.GetInstanceInfo(this.m_chunkManager);
					listDef.StartHidden = instanceInfo.StartHidden;
					this.m_pagination.EnterIgnoreHeight(listDef.StartHidden);
					string label = instanceInfo.Label;
					if (!this.m_pagination.IgnoreHeight)
					{
						this.m_pagination.AddToCurrentPageHeight(listDef, heightValue);
					}
					if (!this.m_pagination.IgnorePageBreak && this.m_pagination.CurrentPageHeight >= this.m_pagination.PageHeight && listInstance.NumberOfContentsOnThisPage > 0)
					{
						RenderingPagesRanges renderingPagesRanges = default(RenderingPagesRanges);
						renderingPagesRanges.StartRow = i + 1 - listInstance.NumberOfContentsOnThisPage;
						renderingPagesRanges.NumberOfDetails = listInstance.NumberOfContentsOnThisPage;
						this.m_pagination.SetCurrentPageHeight(listDef, 0.0);
						int num = listDef.ContentStartPage;
						listDef.ContentStartPage = num + 1;
						listDef.BottomInEndPage = 0.0;
						listInstance.ChildrenStartAndEndPages.Add(renderingPagesRanges);
						listInstance.NumberOfContentsOnThisPage = 1;
					}
					else
					{
						int num = listInstance.NumberOfContentsOnThisPage;
						listInstance.NumberOfContentsOnThisPage = num + 1;
					}
					int startPage = listDef.StartPage;
					listDef.StartPage = listDef.ContentStartPage;
					this.m_pagination.EnterIgnorePageBreak(null, true);
					this.m_pagination.EnterIgnoreHeight(true);
					this.ProcessReportItemColInstance(listContentInstance.ReportItemColInstance);
					this.m_pagination.LeaveIgnoreHeight(true);
					this.m_pagination.LeaveIgnorePageBreak(null, true);
					this.m_pagination.ProcessEndGroupPage(0.0, false, listDef, listInstance.NumberOfContentsOnThisPage > 0, listDef.StartHidden);
					listDef.StartPage = startPage;
				}
				listDef.EndPage = Math.Max(listDef.ContentStartPage, listDef.EndPage);
			}

			// Token: 0x06008DA7 RID: 36263 RVA: 0x002424CC File Offset: 0x002406CC
			private void ProcessTable(Table tableDef, TableInstance tableInstance)
			{
				tableInstance.ChildrenStartAndEndPages = new RenderingPagesRangesList();
				tableInstance.CurrentPage = tableDef.StartPage;
				tableDef.CurrentPage = tableDef.StartPage;
				this.m_pagination.InitProcessTableRenderingPages(tableInstance, tableDef);
				if (tableInstance.HeaderRowInstances != null)
				{
					for (int i = 0; i < tableInstance.HeaderRowInstances.Length; i++)
					{
						this.ProcessReportItemColInstance(tableInstance.HeaderRowInstances[i].TableRowReportItemColInstance);
					}
				}
				if (tableInstance.FooterRowInstances != null)
				{
					for (int j = 0; j < tableInstance.FooterRowInstances.Length; j++)
					{
						this.ProcessReportItemColInstance(tableInstance.FooterRowInstances[j].TableRowReportItemColInstance);
					}
				}
				if (tableInstance.TableGroupInstances != null)
				{
					this.ProcessTableGroups(tableDef, tableInstance, tableInstance.TableGroupInstances, tableInstance.ChildrenStartAndEndPages);
					return;
				}
				if (tableInstance.TableDetailInstances != null)
				{
					int num = this.ProcessTableDetails(tableDef, tableInstance, tableDef.TableDetail, tableInstance.TableDetailInstances, tableInstance.ChildrenStartAndEndPages);
					tableInstance.NumberOfChildrenOnThisPage = num;
					if (num > 0)
					{
						RenderingPagesRanges renderingPagesRanges = default(RenderingPagesRanges);
						renderingPagesRanges.StartRow = tableInstance.TableDetailInstances.Count - num;
						renderingPagesRanges.NumberOfDetails = num;
						tableInstance.ChildrenStartAndEndPages.Add(renderingPagesRanges);
					}
				}
			}

			// Token: 0x06008DA8 RID: 36264 RVA: 0x002425E8 File Offset: 0x002407E8
			private void ProcessTableGroups(Table tableDef, TableInstance tableInstance, TableGroupInstanceList tableGroupInstances, RenderingPagesRangesList pagesList)
			{
				for (int i = 0; i < tableGroupInstances.Count; i++)
				{
					TableGroupInstance tableGroupInstance = tableGroupInstances[i];
					TableGroup tableGroupDef = tableGroupInstance.TableGroupDef;
					tableGroupInstance.ChildrenStartAndEndPages = new RenderingPagesRangesList();
					TableGroupInstanceInfo instanceInfo = tableGroupInstance.GetInstanceInfo(this.m_chunkManager);
					tableGroupDef.StartHidden = instanceInfo.StartHidden;
					this.m_pagination.EnterIgnoreHeight(tableGroupDef.StartHidden);
					string label = instanceInfo.Label;
					if (label != null)
					{
						this.m_navigationInfo.EnterDocumentMapChildren();
					}
					RenderingPagesRanges renderingPagesRanges = default(RenderingPagesRanges);
					this.m_pagination.InitProcessingTableGroup(tableInstance, tableDef, tableGroupInstance, tableGroupDef, ref renderingPagesRanges, i == 0);
					if (tableGroupInstance.HeaderRowInstances != null)
					{
						for (int j = 0; j < tableGroupInstance.HeaderRowInstances.Length; j++)
						{
							this.ProcessReportItemColInstance(tableGroupInstance.HeaderRowInstances[j].TableRowReportItemColInstance);
						}
					}
					if (tableGroupInstance.FooterRowInstances != null)
					{
						for (int k = 0; k < tableGroupInstance.FooterRowInstances.Length; k++)
						{
							this.ProcessReportItemColInstance(tableGroupInstance.FooterRowInstances[k].TableRowReportItemColInstance);
						}
					}
					if (tableGroupInstance.SubGroupInstances != null)
					{
						this.ProcessTableGroups(tableDef, tableInstance, tableGroupInstance.SubGroupInstances, tableGroupInstance.ChildrenStartAndEndPages);
					}
					else if (tableGroupInstance.TableDetailInstances != null)
					{
						int num = this.ProcessTableDetails(tableDef, tableInstance, tableDef.TableDetail, tableGroupInstance.TableDetailInstances, tableGroupInstance.ChildrenStartAndEndPages);
						tableGroupInstance.NumberOfChildrenOnThisPage = num;
						if (num > 0)
						{
							RenderingPagesRanges renderingPagesRanges2 = default(RenderingPagesRanges);
							renderingPagesRanges2.StartRow = tableGroupInstance.TableDetailInstances.Count - num;
							renderingPagesRanges2.NumberOfDetails = num;
							tableGroupInstance.ChildrenStartAndEndPages.Add(renderingPagesRanges2);
						}
					}
					double footerHeightValue = tableGroupDef.FooterHeightValue;
					tableGroupDef.EndPage = tableInstance.CurrentPage;
					this.m_pagination.ProcessEndGroupPage(footerHeightValue, tableGroupDef.PropagatedPageBreakAtEnd || tableGroupDef.Grouping.PageBreakAtEnd, tableDef, tableGroupInstance.NumberOfChildrenOnThisPage > 0, tableGroupDef.StartHidden);
					renderingPagesRanges.EndPage = tableGroupDef.EndPage;
					pagesList.Add(renderingPagesRanges);
					this.m_pagination.LeaveIgnorePageBreak(tableGroupDef.Visibility, false);
				}
			}

			// Token: 0x06008DA9 RID: 36265 RVA: 0x002427E8 File Offset: 0x002409E8
			private int ProcessTableDetails(Table tableDef, TableInstance tableInstance, TableDetail detailDef, TableDetailInstanceList detailInstances, RenderingPagesRangesList pagesList)
			{
				TableRowList detailRows = detailDef.DetailRows;
				double num = -1.0;
				this.m_pagination.EnterIgnorePageBreak(detailRows[0].Visibility, false);
				int num2 = 0;
				for (int i = 0; i < detailInstances.Count; i++)
				{
					TableDetailInstance tableDetailInstance = detailInstances[i];
					TableDetailInstanceInfo instanceInfo = tableDetailInstance.GetInstanceInfo(this.m_chunkManager);
					detailDef.StartHidden = instanceInfo.StartHidden;
					this.m_pagination.EnterIgnoreHeight(detailDef.StartHidden);
					this.m_pagination.ProcessTableDetails(tableDef, tableDetailInstance, detailInstances, ref num, detailRows, pagesList, ref num2);
					tableInstance.CurrentPage = tableDef.CurrentPage;
					tableInstance.NumberOfChildrenOnThisPage = num2;
					this.m_pagination.EnterIgnorePageBreak(null, true);
					this.m_pagination.EnterIgnoreHeight(true);
					if (tableDetailInstance.DetailRowInstances != null)
					{
						for (int j = 0; j < tableDetailInstance.DetailRowInstances.Length; j++)
						{
							this.ProcessReportItemColInstance(tableDetailInstance.DetailRowInstances[i].TableRowReportItemColInstance);
						}
					}
					this.m_pagination.LeaveIgnorePageBreak(null, true);
					this.m_pagination.LeaveIgnoreHeight(true);
					this.m_pagination.LeaveIgnoreHeight(detailDef.StartHidden);
				}
				this.m_pagination.LeaveIgnorePageBreak(detailRows[0].Visibility, false);
				return num2;
			}

			// Token: 0x06008DAA RID: 36266 RVA: 0x0024292C File Offset: 0x00240B2C
			private void ProcessMatrix(Matrix matrixDef, MatrixInstance matrixInstance)
			{
				matrixInstance.ChildrenStartAndEndPages = new RenderingPagesRangesList();
				this.m_pagination.EnterIgnorePageBreak(matrixDef.Visibility, false);
				matrixDef.CurrentPage = matrixDef.StartPage;
				((IPageItem)matrixInstance).StartPage = matrixDef.StartPage;
				MatrixInstanceInfo matrixInstanceInfo = (MatrixInstanceInfo)matrixInstance.GetInstanceInfo(this.m_chunkManager);
				matrixDef.StartHidden = matrixInstanceInfo.StartHidden;
				this.m_pagination.EnterIgnoreHeight(matrixDef.StartHidden);
				if (matrixInstance.CornerContent != null)
				{
					this.ProcessReportItemInstance(matrixInstance.CornerContent);
				}
				else
				{
					ReportItem cornerReportItem = matrixDef.CornerReportItem;
					if (cornerReportItem != null)
					{
						NonComputedUniqueNames cornerNonComputedNames = matrixInstanceInfo.CornerNonComputedNames;
						cornerReportItem.ProcessNavigationAction(this.m_navigationInfo, cornerNonComputedNames, matrixDef.CurrentPage);
					}
				}
				this.ProcessMatrixColumnHeadings(matrixDef, matrixInstance, matrixInstance.ColumnInstances);
				this.ProcessMatrixRowHeadings(matrixDef, matrixInstance, matrixInstance.RowInstances, matrixInstance.ChildrenStartAndEndPages, 0, 0, 0);
				int count = matrixInstance.ChildrenStartAndEndPages.Count;
				if (count > 0)
				{
					matrixDef.EndPage = matrixInstance.ChildrenStartAndEndPages[count - 1].EndPage;
				}
				else
				{
					matrixDef.EndPage = ((IPageItem)matrixInstance).StartPage;
				}
				this.m_pagination.ProcessEndPage(matrixInstance, matrixDef, matrixDef.PageBreakAtEnd || matrixDef.PropagatedPageBreakAtEnd, matrixInstance.NumberOfChildrenOnThisPage > 0);
			}

			// Token: 0x06008DAB RID: 36267 RVA: 0x00242A60 File Offset: 0x00240C60
			private void ProcessMatrixColumnHeadings(Matrix matrixDef, MatrixInstance matrixInstance, MatrixHeadingInstanceList headingInstances)
			{
				for (int i = 0; i < headingInstances.Count; i++)
				{
					MatrixHeadingInstance matrixHeadingInstance = headingInstances[i];
					MatrixHeading matrixHeadingDef = matrixHeadingInstance.MatrixHeadingDef;
					MatrixHeadingInstanceInfo instanceInfo = matrixHeadingInstance.GetInstanceInfo(this.m_chunkManager);
					this.ProcessMatrixHeadingContent(matrixDef, matrixHeadingDef, matrixHeadingInstance, instanceInfo);
					if (matrixHeadingInstance.SubHeadingInstances != null)
					{
						this.ProcessMatrixColumnHeadings(matrixDef, matrixInstance, matrixHeadingInstance.SubHeadingInstances);
					}
					if (instanceInfo.Label != null)
					{
						this.m_navigationInfo.AddToDocumentMap(matrixHeadingInstance.UniqueName, true, matrixDef.CurrentPage, instanceInfo.Label);
					}
				}
			}

			// Token: 0x06008DAC RID: 36268 RVA: 0x00242AE0 File Offset: 0x00240CE0
			private void ProcessMatrixRowHeadings(Matrix matrixDef, MatrixInstance matrixInstance, MatrixHeadingInstanceList headingInstances, RenderingPagesRangesList pagesList, int rowDefIndex, int headingCellIndex, int rowIndex)
			{
				if (headingInstances == null)
				{
					if (!this.m_pagination.IgnoreHeight)
					{
						this.m_pagination.AddToCurrentPageHeight(matrixDef, matrixDef.MatrixRows[rowDefIndex].HeightValue);
					}
					if (!this.m_pagination.IgnorePageBreak && this.m_pagination.CurrentPageHeight >= this.m_pagination.PageHeight && matrixInstance.RowInstances.Count > 1)
					{
						this.m_pagination.SetCurrentPageHeight(matrixDef, 0.0);
						int num = matrixInstance.ExtraPagesFilled;
						matrixInstance.ExtraPagesFilled = num + 1;
						num = matrixDef.CurrentPage;
						matrixDef.CurrentPage = num + 1;
						matrixInstance.NumberOfChildrenOnThisPage = 0;
					}
					else
					{
						int num = matrixInstance.NumberOfChildrenOnThisPage;
						matrixInstance.NumberOfChildrenOnThisPage = num + 1;
					}
					for (int i = 0; i < matrixInstance.CellColumnCount; i++)
					{
						MatrixCellInstance matrixCellInstance = matrixInstance.Cells[rowIndex][i];
						ReportItemInstance content = matrixCellInstance.Content;
						if (content != null)
						{
							this.ProcessReportItemInstance(content);
						}
						else
						{
							MatrixCellInstanceInfo instanceInfo = matrixCellInstance.GetInstanceInfo(this.m_chunkManager);
							ReportItem cellReportItem = matrixDef.GetCellReportItem(instanceInfo.RowIndex, instanceInfo.ColumnIndex);
							if (cellReportItem != null)
							{
								NonComputedUniqueNames contentUniqueNames = instanceInfo.ContentUniqueNames;
								cellReportItem.ProcessNavigationAction(this.m_navigationInfo, contentUniqueNames, matrixDef.CurrentPage);
							}
						}
					}
					return;
				}
				for (int j = 0; j < headingInstances.Count; j++)
				{
					MatrixHeadingInstance matrixHeadingInstance = headingInstances[j];
					MatrixHeading matrixHeadingDef = matrixHeadingInstance.MatrixHeadingDef;
					matrixHeadingInstance.ChildrenStartAndEndPages = new RenderingPagesRangesList();
					MatrixHeadingInstanceInfo instanceInfo2 = matrixHeadingInstance.GetInstanceInfo(this.m_chunkManager);
					matrixHeadingDef.StartHidden = instanceInfo2.StartHidden;
					this.m_pagination.EnterIgnoreHeight(matrixHeadingDef.StartHidden);
					int num2;
					if (matrixHeadingInstance.IsSubtotal || matrixHeadingDef.Grouping == null)
					{
						num2 = this.ProcessMatrixRowSubtotalOrStaticHeading(matrixDef, matrixInstance, matrixHeadingDef, matrixHeadingInstance, instanceInfo2, pagesList, matrixHeadingInstance.IsSubtotal ? rowDefIndex : j, instanceInfo2.HeadingCellIndex, j);
					}
					else
					{
						num2 = this.ProcessMatrixDynamicRowHeading(matrixDef, matrixInstance, matrixHeadingDef, matrixHeadingInstance, instanceInfo2, j == 0, pagesList, rowDefIndex, instanceInfo2.HeadingCellIndex, j);
					}
					if (instanceInfo2.Label != null)
					{
						this.m_navigationInfo.AddToDocumentMap(matrixHeadingInstance.UniqueName, true, num2, instanceInfo2.Label);
					}
				}
			}

			// Token: 0x06008DAD RID: 36269 RVA: 0x00242D0C File Offset: 0x00240F0C
			private void ProcessMatrixHeadingContent(Matrix matrixDef, MatrixHeading headingDef, MatrixHeadingInstance headingInstance, MatrixHeadingInstanceInfo headingInstanceInfo)
			{
				if (headingInstanceInfo.Label != null)
				{
					this.m_navigationInfo.EnterDocumentMapChildren();
				}
				if (headingInstance.Content != null)
				{
					this.ProcessReportItemInstance(headingInstance.Content);
					return;
				}
				ReportItem reportItem = headingDef.ReportItem;
				if (reportItem != null)
				{
					NonComputedUniqueNames contentUniqueNames = headingInstanceInfo.ContentUniqueNames;
					reportItem.ProcessNavigationAction(this.m_navigationInfo, contentUniqueNames, matrixDef.CurrentPage);
				}
			}

			// Token: 0x06008DAE RID: 36270 RVA: 0x00242D68 File Offset: 0x00240F68
			private int ProcessMatrixRowSubtotalOrStaticHeading(Matrix matrixDef, MatrixInstance matrixInstance, MatrixHeading headingDef, MatrixHeadingInstance headingInstance, MatrixHeadingInstanceInfo headingInstanceInfo, RenderingPagesRangesList pagesList, int rowDefIndex, int headingCellIndex, int rowIndex)
			{
				RenderingPagesRanges renderingPagesRanges = default(RenderingPagesRanges);
				bool isSubtotal = headingInstance.IsSubtotal;
				this.m_pagination.EnterIgnorePageBreak(headingDef.Visibility, isSubtotal);
				renderingPagesRanges.StartPage = ((Matrix)matrixInstance.ReportItemDef).CurrentPage;
				this.ProcessMatrixHeadingContent(matrixDef, headingDef, headingInstance, headingInstanceInfo);
				this.ProcessMatrixRowHeadings(matrixDef, matrixInstance, headingInstance.SubHeadingInstances, headingInstance.ChildrenStartAndEndPages, rowDefIndex, headingCellIndex, rowIndex);
				this.m_pagination.LeaveIgnorePageBreak(headingDef.Visibility, isSubtotal);
				renderingPagesRanges.EndPage = ((Matrix)matrixInstance.ReportItemDef).CurrentPage;
				if (headingInstance.ChildrenStartAndEndPages == null || headingInstance.ChildrenStartAndEndPages.Count < 1)
				{
					renderingPagesRanges.EndPage = renderingPagesRanges.StartPage;
				}
				pagesList.Add(renderingPagesRanges);
				return renderingPagesRanges.StartPage;
			}

			// Token: 0x06008DAF RID: 36271 RVA: 0x00242E3C File Offset: 0x0024103C
			private int ProcessMatrixDynamicRowHeading(Matrix matrixDef, MatrixInstance matrixInstance, MatrixHeading headingDef, MatrixHeadingInstance headingInstance, MatrixHeadingInstanceInfo headingInstanceInfo, bool firstHeading, RenderingPagesRangesList pagesList, int rowDefIndex, int headingCellIndex, int rowIndex)
			{
				RenderingPagesRanges renderingPagesRanges = default(RenderingPagesRanges);
				this.m_pagination.EnterIgnorePageBreak(headingDef.Visibility, false);
				if (!this.m_pagination.IgnorePageBreak && !firstHeading && headingDef.Grouping.PageBreakAtStart && matrixInstance.NumberOfChildrenOnThisPage > 0)
				{
					this.m_pagination.SetCurrentPageHeight(matrixInstance.ReportItemDef, 0.0);
					int num = matrixInstance.ExtraPagesFilled;
					matrixInstance.ExtraPagesFilled = num + 1;
					num = matrixDef.CurrentPage;
					matrixDef.CurrentPage = num + 1;
					matrixInstance.NumberOfChildrenOnThisPage = 0;
				}
				renderingPagesRanges.StartPage = matrixDef.CurrentPage;
				this.ProcessMatrixHeadingContent(matrixDef, headingDef, headingInstance, headingInstanceInfo);
				this.ProcessMatrixRowHeadings(matrixDef, matrixInstance, headingInstance.SubHeadingInstances, headingInstance.ChildrenStartAndEndPages, rowDefIndex, headingCellIndex, rowIndex);
				renderingPagesRanges.EndPage = ((Matrix)matrixInstance.ReportItemDef).CurrentPage;
				if (headingInstance.SubHeadingInstances == null || headingInstance.SubHeadingInstances.Count < 1)
				{
					renderingPagesRanges.EndPage = renderingPagesRanges.StartPage;
				}
				else
				{
					renderingPagesRanges.EndPage = headingInstance.ChildrenStartAndEndPages[headingInstance.ChildrenStartAndEndPages.Count - 1].EndPage;
				}
				if (!this.m_pagination.IgnorePageBreak && matrixInstance.NumberOfChildrenOnThisPage > 0 && this.m_pagination.CanMoveToNextPage(headingDef.Grouping.PageBreakAtEnd))
				{
					this.m_pagination.SetCurrentPageHeight(matrixDef, 0.0);
					int num = matrixInstance.ExtraPagesFilled;
					matrixInstance.ExtraPagesFilled = num + 1;
					num = matrixDef.CurrentPage;
					matrixDef.CurrentPage = num + 1;
					matrixInstance.NumberOfChildrenOnThisPage = 0;
				}
				pagesList.Add(renderingPagesRanges);
				this.m_pagination.LeaveIgnoreHeight(headingDef.StartHidden);
				this.m_pagination.LeaveIgnorePageBreak(headingDef.Visibility, false);
				return renderingPagesRanges.StartPage;
			}

			// Token: 0x04004F6C RID: 20332
			private ReportProcessing.Pagination m_pagination;

			// Token: 0x04004F6D RID: 20333
			private ReportProcessing.NavigationInfo m_navigationInfo;

			// Token: 0x04004F6E RID: 20334
			private bool m_onePass;

			// Token: 0x04004F6F RID: 20335
			private ChunkManager.RenderingChunkManager m_chunkManager;

			// Token: 0x04004F70 RID: 20336
			private bool m_hasDocMap;
		}
	}
}
