using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.InfoNav.Common;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000AB RID: 171
	internal sealed class ReportParser
	{
		// Token: 0x0600034C RID: 844 RVA: 0x0000D422 File Offset: 0x0000B622
		internal ReportParser()
		{
			this._unknownElements = new List<string>();
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000D435 File Offset: 0x0000B635
		public List<string> UnknownElements
		{
			get
			{
				return this._unknownElements;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000D43D File Offset: 0x0000B63D
		public RdmReport Parse(Stream reportStream)
		{
			return new ReportParser().InternalParse(reportStream);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000D44C File Offset: 0x0000B64C
		internal List<SortExpression> ParseSortExpressions(ReportParsingDiagnosticContext diagnosticContext, XmlReader sortExpressionsReader)
		{
			sortExpressionsReader.Read();
			List<SortExpression> list = new List<SortExpression>();
			while (!sortExpressionsReader.EOF)
			{
				if (sortExpressionsReader.IsStartElement())
				{
					if (sortExpressionsReader.LocalName == "SortExpression")
					{
						using (XmlReader xmlReader = sortExpressionsReader.ReadSubtreeAndMoveToContent())
						{
							list.Add(this.ParseSortExpression(diagnosticContext, xmlReader));
							goto IL_0052;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, sortExpressionsReader);
				}
				IL_0052:
				sortExpressionsReader.Skip();
			}
			return list;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000D4CC File Offset: 0x0000B6CC
		internal List<GroupExpression> ParseGroupExpressions(ReportParsingDiagnosticContext diagnosticContext, XmlReader groupExpressionsReader)
		{
			groupExpressionsReader.Read();
			List<GroupExpression> list = new List<GroupExpression>();
			while (!groupExpressionsReader.EOF)
			{
				if (groupExpressionsReader.IsStartElement())
				{
					if (groupExpressionsReader.LocalName == "GroupExpression")
					{
						list.Add(new GroupExpression(this.ParseExpressionValueNode(groupExpressionsReader)));
						continue;
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, groupExpressionsReader);
				}
				groupExpressionsReader.Skip();
			}
			return list;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000D530 File Offset: 0x0000B730
		internal Group ParseGroup(ReportParsingDiagnosticContext diagnosticContext, XmlReader groupMemberReader)
		{
			groupMemberReader.Read();
			List<GroupExpression> list = new List<GroupExpression>();
			string text = null;
			while (!groupMemberReader.EOF)
			{
				if (groupMemberReader.IsStartElement())
				{
					string localName = groupMemberReader.LocalName;
					if (!(localName == "GroupExpressions"))
					{
						if (!(localName == "DataSetName"))
						{
							if (!(localName == "NaturalGroup"))
							{
								this.AddErrorIfInRequiredNamespace(diagnosticContext, groupMemberReader);
								goto IL_0076;
							}
							goto IL_0076;
						}
					}
					else
					{
						using (XmlReader xmlReader = groupMemberReader.ReadSubtreeAndMoveToContent())
						{
							list = this.ParseGroupExpressions(diagnosticContext, xmlReader);
							goto IL_0076;
						}
					}
					text = groupMemberReader.ReadElementContentAsString();
					continue;
				}
				IL_0076:
				groupMemberReader.Skip();
			}
			return new Group(list, text);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		internal Tuple<string, bool> ParseChartTitle(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartTitleReader)
		{
			chartTitleReader.Read();
			bool flag = false;
			string text = string.Empty;
			while (!chartTitleReader.EOF)
			{
				if (chartTitleReader.IsStartElement())
				{
					string localName = chartTitleReader.LocalName;
					if (localName == "Hidden")
					{
						flag = chartTitleReader.ReadElementContentAsString().ToLowerInvariant() == "true";
						continue;
					}
					if (!(localName == "Caption"))
					{
						if (!(localName == "Style"))
						{
							this.AddErrorIfInRequiredNamespace(diagnosticContext, chartTitleReader);
						}
					}
					else
					{
						string text2 = chartTitleReader.ReadElementContentAsString();
						if (!string.IsNullOrEmpty(text2))
						{
							text = text2;
							continue;
						}
						continue;
					}
				}
				chartTitleReader.Skip();
			}
			return new Tuple<string, bool>(text, flag);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000D678 File Offset: 0x0000B878
		internal Tuple<string, bool> ParseChartTitles(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartTitlesReader)
		{
			chartTitlesReader.Read();
			Tuple<string, bool> tuple = new Tuple<string, bool>(string.Empty, false);
			while (!chartTitlesReader.EOF)
			{
				if (chartTitlesReader.IsStartElement())
				{
					if (chartTitlesReader.LocalName == "ChartTitle")
					{
						using (XmlReader xmlReader = chartTitlesReader.ReadSubtreeAndMoveToContent())
						{
							tuple = this.ParseChartTitle(diagnosticContext, xmlReader);
							goto IL_0083;
						}
					}
					if (chartTitlesReader.LocalName == "Caption")
					{
						string text = chartTitlesReader.ReadElementContentAsString();
						if (!string.IsNullOrEmpty(text))
						{
							tuple = new Tuple<string, bool>(text, tuple.Item2);
							continue;
						}
						continue;
					}
					else
					{
						this.AddErrorIfInRequiredNamespace(diagnosticContext, chartTitlesReader);
					}
				}
				IL_0083:
				chartTitlesReader.Skip();
			}
			return tuple;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000D728 File Offset: 0x0000B928
		internal ChartCategoryHierarchy ParseChartCategoryHierarchy(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartCategoryHierarchyReader)
		{
			chartCategoryHierarchyReader.Read();
			List<ChartMember> list = new List<ChartMember>();
			bool flag = false;
			while (!chartCategoryHierarchyReader.EOF)
			{
				if (chartCategoryHierarchyReader.IsStartElement())
				{
					string localName = chartCategoryHierarchyReader.LocalName;
					if (!(localName == "ChartMembers"))
					{
						if (!(localName == "EnableDrilldown"))
						{
							this.AddErrorIfInRequiredNamespace(diagnosticContext, chartCategoryHierarchyReader);
							goto IL_0078;
						}
					}
					else
					{
						using (XmlReader xmlReader = chartCategoryHierarchyReader.ReadSubtreeAndMoveToContent())
						{
							list = this.ParseChartMembers(diagnosticContext, xmlReader);
							goto IL_0078;
						}
					}
					flag = chartCategoryHierarchyReader.ReadElementContentAsString().ToLowerInvariant() == "true";
					continue;
				}
				IL_0078:
				chartCategoryHierarchyReader.Skip();
			}
			if (list.IsEmpty<ChartMember>())
			{
				list.Add(new ChartMember(null, null, null, null));
			}
			return new ChartCategoryHierarchy(list, flag);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		internal ChartMember ParseChartMember(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartMemberReader)
		{
			chartMemberReader.Read();
			Group group = null;
			List<SortExpression> list = null;
			Label label = null;
			List<ChartMember> list2 = null;
			while (!chartMemberReader.EOF)
			{
				if (chartMemberReader.IsStartElement())
				{
					string localName = chartMemberReader.LocalName;
					if (localName == "Group")
					{
						using (XmlReader xmlReader = chartMemberReader.ReadSubtreeAndMoveToContent())
						{
							group = this.ParseGroup(diagnosticContext, xmlReader);
							goto IL_00D8;
						}
						goto IL_0081;
					}
					if (localName == "SortExpressions")
					{
						goto IL_0081;
					}
					if (!(localName == "Label"))
					{
						if (localName == "ChartMembers")
						{
							using (XmlReader xmlReader2 = chartMemberReader.ReadSubtreeAndMoveToContent())
							{
								list2 = this.ParseChartMembers(diagnosticContext, xmlReader2);
								goto IL_00D8;
							}
						}
						this.AddErrorIfInRequiredNamespace(diagnosticContext, chartMemberReader);
						goto IL_00D8;
					}
					IL_00A1:
					label = new Label(this.ParseExpressionValueNode(chartMemberReader));
					continue;
					IL_0081:
					using (XmlReader xmlReader3 = chartMemberReader.ReadSubtreeAndMoveToContent())
					{
						list = this.ParseSortExpressions(diagnosticContext, xmlReader3);
						goto IL_00D8;
					}
					goto IL_00A1;
				}
				IL_00D8:
				chartMemberReader.Skip();
			}
			return new ChartMember(group, list, label, list2);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000D914 File Offset: 0x0000BB14
		internal ChartDataPoint ParseChartDataPoint(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartDataPointReader)
		{
			chartDataPointReader.Read();
			Expression expression = null;
			Expression expression2 = null;
			Expression expression3 = null;
			while (!chartDataPointReader.EOF)
			{
				if (chartDataPointReader.IsStartElement())
				{
					if (chartDataPointReader.LocalName == "ChartDataPointValues")
					{
						XmlReader xmlReader = chartDataPointReader.ReadSubtreeAndMoveToContent();
						xmlReader.Read();
						while (!xmlReader.EOF)
						{
							if (xmlReader.IsStartElement())
							{
								string localName = xmlReader.LocalName;
								if (localName == "X")
								{
									expression = this.ParseExpressionValueNode(xmlReader);
									continue;
								}
								if (localName == "Y")
								{
									expression2 = this.ParseExpressionValueNode(xmlReader);
									continue;
								}
								if (localName == "Size")
								{
									expression3 = this.ParseExpressionValueNode(xmlReader);
									continue;
								}
								if (!(localName == "HighlightX") && !(localName == "HighlightY") && !(localName == "HighlightSize"))
								{
									this.AddErrorIfInRequiredNamespace(diagnosticContext, xmlReader);
								}
							}
							xmlReader.Skip();
						}
					}
					else
					{
						this.AddErrorIfInRequiredNamespace(diagnosticContext, chartDataPointReader);
					}
					chartDataPointReader.Skip();
				}
				chartDataPointReader.Skip();
			}
			return new ChartDataPoint(expression, expression2, expression3);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000DA30 File Offset: 0x0000BC30
		internal List<ChartDataPoint> ParseChartDataPoints(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartDataPointsReader)
		{
			chartDataPointsReader.Read();
			List<ChartDataPoint> list = new List<ChartDataPoint>();
			while (!chartDataPointsReader.EOF)
			{
				if (chartDataPointsReader.IsStartElement())
				{
					if (chartDataPointsReader.LocalName == "ChartDataPoint")
					{
						using (XmlReader xmlReader = chartDataPointsReader.ReadSubtreeAndMoveToContent())
						{
							list.Add(this.ParseChartDataPoint(diagnosticContext, xmlReader));
							goto IL_0052;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, chartDataPointsReader);
				}
				IL_0052:
				chartDataPointsReader.Skip();
			}
			return list;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
		internal ChartDataLabelPositions ParseChartDataLabelsPositions(string chartDataLabelsPositionsValue)
		{
			if (chartDataLabelsPositionsValue == "Center")
			{
				return ChartDataLabelPositions.Center;
			}
			if (chartDataLabelsPositionsValue == "Top")
			{
				return ChartDataLabelPositions.Top;
			}
			if (chartDataLabelsPositionsValue == "Bottom")
			{
				return ChartDataLabelPositions.Bottom;
			}
			if (chartDataLabelsPositionsValue == "Left")
			{
				return ChartDataLabelPositions.Left;
			}
			if (!(chartDataLabelsPositionsValue == "Right"))
			{
				return ChartDataLabelPositions.Auto;
			}
			return ChartDataLabelPositions.Right;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		internal ChartSeries ParseChartSeries(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartSeriesReader)
		{
			chartSeriesReader.Read();
			string text = "Category";
			string text2 = "Plain";
			MapBackdropType mapBackdropType = MapBackdropType.Road;
			List<ChartDataPoint> list = new List<ChartDataPoint>();
			bool flag = false;
			ChartDataLabelPositions chartDataLabelPositions = ChartDataLabelPositions.Auto;
			while (!chartSeriesReader.EOF)
			{
				if (chartSeriesReader.IsStartElement() && !chartSeriesReader.IsEmptyElement)
				{
					string localName = chartSeriesReader.LocalName;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num <= 2473184578U)
					{
						if (num <= 1533637176U)
						{
							if (num != 1416096886U)
							{
								if (num != 1533637176U)
								{
									goto IL_01E0;
								}
								if (!(localName == "ChartDataPoints"))
								{
									goto IL_01E0;
								}
								using (XmlReader xmlReader = chartSeriesReader.ReadSubtreeAndMoveToContent())
								{
									list = this.ParseChartDataPoints(diagnosticContext, xmlReader);
									goto IL_01E8;
								}
								goto IL_01AA;
							}
							else
							{
								if (!(localName == "Style"))
								{
									goto IL_01E0;
								}
								goto IL_01E8;
							}
						}
						else if (num != 2136791899U)
						{
							if (num != 2473184578U)
							{
								goto IL_01E0;
							}
							if (!(localName == "ChartItemInLegend"))
							{
								goto IL_01E0;
							}
							goto IL_01E8;
						}
						else if (!(localName == "MapBackdropType"))
						{
							goto IL_01E0;
						}
					}
					else if (num <= 3160848053U)
					{
						if (num != 2881822685U)
						{
							if (num != 3160848053U)
							{
								goto IL_01E0;
							}
							if (!(localName == "SubtypeOverride"))
							{
								goto IL_01E0;
							}
							text2 = chartSeriesReader.ReadElementContentAsString();
							continue;
						}
						else
						{
							if (!(localName == "Subtype"))
							{
								goto IL_01E0;
							}
							text2 = chartSeriesReader.ReadElementContentAsString();
							continue;
						}
					}
					else if (num != 3224080635U)
					{
						if (num != 3512062061U)
						{
							goto IL_01E0;
						}
						if (!(localName == "Type"))
						{
							goto IL_01E0;
						}
						text = chartSeriesReader.ReadElementContentAsString();
						continue;
					}
					else
					{
						if (!(localName == "ChartDataLabel"))
						{
							goto IL_01E0;
						}
						goto IL_01AA;
					}
					IL_01CD:
					string text3 = chartSeriesReader.ReadElementContentAsString();
					mapBackdropType = this.ParseMapBackdropType(text3);
					continue;
					IL_01AA:
					using (XmlReader xmlReader2 = chartSeriesReader.ReadSubtreeAndMoveToContent())
					{
						this.ParseChartDataLabel(diagnosticContext, xmlReader2, ref flag, ref chartDataLabelPositions);
						goto IL_01E8;
					}
					goto IL_01CD;
					IL_01E0:
					this.AddErrorIfInRequiredNamespace(diagnosticContext, chartSeriesReader);
				}
				IL_01E8:
				chartSeriesReader.Skip();
			}
			return new ChartSeries(text, text2, list, flag, chartDataLabelPositions, mapBackdropType);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		internal List<ChartSeries> ParseChartSeriesCollection(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartSeriesCollectionReader)
		{
			chartSeriesCollectionReader.Read();
			List<ChartSeries> list = new List<ChartSeries>();
			while (!chartSeriesCollectionReader.EOF)
			{
				if (chartSeriesCollectionReader.IsStartElement())
				{
					if (chartSeriesCollectionReader.LocalName == "ChartSeries")
					{
						using (XmlReader xmlReader = chartSeriesCollectionReader.ReadSubtreeAndMoveToContent())
						{
							list.Add(this.ParseChartSeries(diagnosticContext, xmlReader));
							goto IL_0052;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, chartSeriesCollectionReader);
				}
				IL_0052:
				chartSeriesCollectionReader.Skip();
			}
			return list;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		internal ChartData ParseChartData(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartDataReader)
		{
			chartDataReader.Read();
			ChartData chartData = null;
			while (!chartDataReader.EOF)
			{
				if (chartDataReader.IsStartElement())
				{
					if (chartDataReader.LocalName == "ChartSeriesCollection")
					{
						using (XmlReader xmlReader = chartDataReader.ReadSubtreeAndMoveToContent())
						{
							chartData = new ChartData(this.ParseChartSeriesCollection(diagnosticContext, xmlReader));
							goto IL_004E;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, chartDataReader);
				}
				IL_004E:
				chartDataReader.Skip();
			}
			return chartData;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000DE38 File Offset: 0x0000C038
		internal Tuple<ChartPositions, bool> ParseLegendPosition(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartLegendsReader)
		{
			chartLegendsReader.Read();
			ChartPositions chartPositions = ChartPositions.RightTop;
			bool flag = false;
			while (!chartLegendsReader.EOF)
			{
				if (chartLegendsReader.IsStartElement())
				{
					if (chartLegendsReader.LocalName == "ChartLegend")
					{
						XmlReader xmlReader = chartLegendsReader.ReadSubtreeAndMoveToContent();
						xmlReader.Read();
						while (!xmlReader.EOF)
						{
							if (xmlReader.IsStartElement())
							{
								string localName = xmlReader.LocalName;
								uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
								if (num <= 1994351710U)
								{
									if (num <= 1079472612U)
									{
										if (num <= 503284309U)
										{
											if (num != 140829383U)
											{
												if (num == 503284309U)
												{
													if (localName == "ColumnSeparatorColor")
													{
														goto IL_03EA;
													}
												}
											}
											else if (localName == "HeaderSeparator")
											{
												goto IL_03EA;
											}
										}
										else if (num != 912440303U)
										{
											if (num != 1009647193U)
											{
												if (num == 1079472612U)
												{
													if (localName == "ColumnSpacing")
													{
														goto IL_03EA;
													}
												}
											}
											else if (localName == "Hidden")
											{
												flag = xmlReader.ReadElementContentAsString() == "true";
												continue;
											}
										}
										else if (localName == "InterlacedRows")
										{
											goto IL_03EA;
										}
									}
									else if (num <= 1416096886U)
									{
										if (num != 1081930338U)
										{
											if (num == 1416096886U)
											{
												if (localName == "Style")
												{
													goto IL_03EA;
												}
											}
										}
										else if (localName == "ColumnSeparator")
										{
											goto IL_03EA;
										}
									}
									else if (num != 1695374293U)
									{
										if (num != 1761394610U)
										{
											if (num == 1994351710U)
											{
												if (localName == "InterlacedRowsColor")
												{
													goto IL_03EA;
												}
											}
										}
										else if (localName == "ChartElementPosition")
										{
											goto IL_03EA;
										}
									}
									else if (localName == "TextWrapThreshold")
									{
										goto IL_03EA;
									}
								}
								else if (num <= 2804195342U)
								{
									if (num <= 2163889536U)
									{
										if (num != 2009302710U)
										{
											if (num == 2163889536U)
											{
												if (localName == "DockToChartArea")
												{
													goto IL_03EA;
												}
											}
										}
										else if (localName == "HeaderSeparatorColor")
										{
											goto IL_03EA;
										}
									}
									else if (num != 2651735519U)
									{
										if (num != 2773068316U)
										{
											if (num == 2804195342U)
											{
												if (localName == "DockOutsideChartArea")
												{
													goto IL_03EA;
												}
											}
										}
										else if (localName == "ChartLegendTitle")
										{
											goto IL_03EA;
										}
									}
									else if (localName == "MinFontSize")
									{
										goto IL_03EA;
									}
								}
								else if (num <= 3172871006U)
								{
									if (num != 2979015451U)
									{
										if (num != 3019262994U)
										{
											if (num == 3172871006U)
											{
												if (localName == "AutoFitTextDisabled")
												{
													goto IL_03EA;
												}
											}
										}
										else if (localName == "EquallySpacedItems")
										{
											goto IL_03EA;
										}
									}
									else if (localName == "MaxAutoSize")
									{
										goto IL_03EA;
									}
								}
								else if (num != 3471203795U)
								{
									if (num != 3799987242U)
									{
										if (num == 4152902175U)
										{
											if (localName == "Layout")
											{
												goto IL_03EA;
											}
										}
									}
									else if (localName == "Position")
									{
										string text = xmlReader.ReadElementContentAsString();
										if (text == "LeftTop")
										{
											chartPositions = ChartPositions.LeftTop;
											continue;
										}
										if (text == "TopCenter")
										{
											chartPositions = ChartPositions.TopCenter;
											continue;
										}
										if (text == "BottomCenter")
										{
											chartPositions = ChartPositions.BottomCenter;
											continue;
										}
										diagnosticContext.AddError("InvalidLegendPosition", new string[0]);
										continue;
									}
								}
								else if (localName == "Reversed")
								{
									goto IL_03EA;
								}
								this.AddErrorIfInRequiredNamespace(diagnosticContext, xmlReader);
							}
							IL_03EA:
							xmlReader.Skip();
						}
					}
					else
					{
						this.AddErrorIfInRequiredNamespace(diagnosticContext, chartLegendsReader);
					}
				}
				chartLegendsReader.Skip();
			}
			return new Tuple<ChartPositions, bool>(chartPositions, flag);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000E264 File Offset: 0x0000C464
		internal bool ParseScalarXAxis(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartAreasReader)
		{
			chartAreasReader.Read();
			bool flag = false;
			while (!chartAreasReader.EOF)
			{
				if (chartAreasReader.IsStartElement())
				{
					if (chartAreasReader.LocalName == "ChartArea")
					{
						using (XmlReader xmlReader = chartAreasReader.ReadSubtreeAndMoveToContent())
						{
							xmlReader.Read();
							while (!xmlReader.EOF)
							{
								if (xmlReader.IsStartElement())
								{
									string localName = xmlReader.LocalName;
									uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
									if (num <= 1596743757U)
									{
										if (num <= 615752251U)
										{
											if (num != 72343694U)
											{
												if (num != 218211160U)
												{
													if (num == 615752251U)
													{
														if (localName == "AlignWithChartArea")
														{
															goto IL_022B;
														}
													}
												}
												else if (localName == "SynchronizeSeries")
												{
													goto IL_022B;
												}
											}
											else if (localName == "ChartThreeDProperties")
											{
												goto IL_022B;
											}
										}
										else if (num != 1009647193U)
										{
											if (num != 1416096886U)
											{
												if (num == 1596743757U)
												{
													if (localName == "ChartInnerPlotPosition")
													{
														goto IL_022B;
													}
												}
											}
											else if (localName == "Style")
											{
												goto IL_022B;
											}
										}
										else if (localName == "Hidden")
										{
											goto IL_022B;
										}
									}
									else if (num <= 2366092014U)
									{
										if (num != 1761394610U)
										{
											if (num != 2278405877U)
											{
												if (num == 2366092014U)
												{
													if (localName == "ChartCategoryAxes")
													{
														using (XmlReader xmlReader2 = chartAreasReader.ReadSubtreeAndMoveToContent())
														{
															flag = this.ParseChartCategoryAxes(diagnosticContext, xmlReader2);
															goto IL_022B;
														}
													}
												}
											}
											else if (localName == "ChartValueAxes")
											{
												goto IL_022B;
											}
										}
										else if (localName == "ChartElementPosition")
										{
											goto IL_022B;
										}
									}
									else if (num <= 2783297167U)
									{
										if (num != 2605952120U)
										{
											if (num == 2783297167U)
											{
												if (localName == "EquallySizedAxesFont")
												{
													goto IL_022B;
												}
											}
										}
										else if (localName == "ChartAlignType")
										{
											goto IL_022B;
										}
									}
									else if (num != 3378399954U)
									{
										if (num == 3845009440U)
										{
											if (localName == "AlignOrientation")
											{
												goto IL_022B;
											}
										}
									}
									else if (localName == "SyncSeriesDataSetName")
									{
										goto IL_022B;
									}
									this.AddErrorIfInRequiredNamespace(diagnosticContext, chartAreasReader);
								}
								IL_022B:
								xmlReader.Skip();
							}
							goto IL_0250;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, chartAreasReader);
				}
				IL_0250:
				chartAreasReader.Skip();
			}
			return flag;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000E508 File Offset: 0x0000C708
		internal bool ParseScalarXAxisFromAxis(ReportParsingDiagnosticContext diagnosticContext, XmlReader axisReader)
		{
			axisReader.Read();
			bool flag = false;
			while (!axisReader.EOF)
			{
				if (axisReader.IsStartElement())
				{
					string localName = axisReader.LocalName;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num <= 1845915779U)
					{
						if (num <= 968404409U)
						{
							if (num <= 448136978U)
							{
								if (num <= 353630597U)
								{
									if (num != 203348700U)
									{
										if (num == 353630597U)
										{
											if (localName == "ChartMinorTickMarks")
											{
												goto IL_06C8;
											}
										}
									}
									else if (localName == "LogBase")
									{
										goto IL_06C8;
									}
								}
								else if (num != 438536855U)
								{
									if (num != 442354411U)
									{
										if (num == 448136978U)
										{
											if (localName == "HideLabels")
											{
												goto IL_06C8;
											}
										}
									}
									else if (localName == "ChartMajorGridLines")
									{
										goto IL_06C8;
									}
								}
								else if (localName == "Minimum")
								{
									goto IL_06C8;
								}
							}
							else if (num <= 707708114U)
							{
								if (num != 584794397U)
								{
									if (num != 705498355U)
									{
										if (num == 707708114U)
										{
											if (localName == "PreventLabelOffset")
											{
												goto IL_06C8;
											}
										}
									}
									else if (localName == "ChartAxisScaleBreak")
									{
										goto IL_06C8;
									}
								}
								else if (localName == "MaxFontSize")
								{
									goto IL_06C8;
								}
							}
							else if (num != 764796912U)
							{
								if (num != 921558380U)
								{
									if (num == 968404409U)
									{
										if (localName == "OffsetLabels")
										{
											goto IL_06C8;
										}
									}
								}
								else if (localName == "SynchronizeAxis")
								{
									goto IL_06C8;
								}
							}
							else if (localName == "IntervalType")
							{
								goto IL_06C8;
							}
						}
						else if (num <= 1495943489U)
						{
							if (num <= 1363604334U)
							{
								if (num != 969184184U)
								{
									if (num != 1143351963U)
									{
										if (num == 1363604334U)
										{
											if (localName == "AllowLabelRotation")
											{
												goto IL_06C8;
											}
										}
									}
									else if (localName == "LabelIntervalOffsetType")
									{
										goto IL_06C8;
									}
								}
								else if (localName == "Angle")
								{
									goto IL_06C8;
								}
							}
							else if (num != 1375394469U)
							{
								if (num != 1416096886U)
								{
									if (num == 1495943489U)
									{
										if (localName == "Visible")
										{
											goto IL_06C8;
										}
									}
								}
								else if (localName == "Style")
								{
									goto IL_06C8;
								}
							}
							else if (localName == "Reverse")
							{
								goto IL_06C8;
							}
						}
						else if (num <= 1654011843U)
						{
							if (num != 1539345862U)
							{
								if (num != 1559159053U)
								{
									if (num == 1654011843U)
									{
										if (localName == "VariableAutoInterval")
										{
											goto IL_06C8;
										}
									}
								}
								else if (localName == "InterlacedColor")
								{
									goto IL_06C8;
								}
							}
							else if (localName == "Location")
							{
								goto IL_06C8;
							}
						}
						else if (num != 1676895877U)
						{
							if (num != 1705362465U)
							{
								if (num == 1845915779U)
								{
									if (localName == "MarksAlwaysAtPlotEdge")
									{
										goto IL_06C8;
									}
								}
							}
							else if (localName == "HideEndLabels")
							{
								goto IL_06C8;
							}
						}
						else if (localName == "LogScale")
						{
							goto IL_06C8;
						}
					}
					else if (num <= 3083591375U)
					{
						if (num <= 2558333763U)
						{
							if (num <= 2433977907U)
							{
								if (num != 2249212663U)
								{
									if (num != 2425558298U)
									{
										if (num == 2433977907U)
										{
											if (localName == "IntervalOffsetType")
											{
												goto IL_06C8;
											}
										}
									}
									else if (localName == "Interlaced")
									{
										goto IL_06C8;
									}
								}
								else if (localName == "ChartMinorGridLines")
								{
									goto IL_06C8;
								}
							}
							else if (num != 2477637195U)
							{
								if (num != 2510511374U)
								{
									if (num == 2558333763U)
									{
										if (localName == "LabelIntervalOffset")
										{
											goto IL_06C8;
										}
									}
								}
								else if (localName == "CrossAt")
								{
									goto IL_06C8;
								}
							}
							else if (localName == "PreventFontGrow")
							{
								goto IL_06C8;
							}
						}
						else if (num <= 2657713792U)
						{
							if (num != 2614232489U)
							{
								if (num != 2651735519U)
								{
									if (num == 2657713792U)
									{
										if (localName == "LabelInterval")
										{
											goto IL_06C8;
										}
									}
								}
								else if (localName == "MinFontSize")
								{
									goto IL_06C8;
								}
							}
							else if (localName == "IncludeZero")
							{
								goto IL_06C8;
							}
						}
						else if (num != 2696749277U)
						{
							if (num != 2701668641U)
							{
								if (num == 3083591375U)
								{
									if (localName == "Arrows")
									{
										goto IL_06C8;
									}
								}
							}
							else if (localName == "ChartMajorTickMarks")
							{
								goto IL_06C8;
							}
						}
						else if (localName == "PreventWordWrap")
						{
							goto IL_06C8;
						}
					}
					else if (num <= 3783658427U)
					{
						if (num <= 3301734811U)
						{
							if (num != 3112849672U)
							{
								if (num != 3277277396U)
								{
									if (num == 3301734811U)
									{
										if (localName == "Margin")
										{
											goto IL_06C8;
										}
									}
								}
								else if (localName == "LabelsAutoFitDisabled")
								{
									goto IL_06C8;
								}
							}
							else if (localName == "LabelIntervalType")
							{
								goto IL_06C8;
							}
						}
						else if (num != 3364876465U)
						{
							if (num != 3684776344U)
							{
								if (num == 3783658427U)
								{
									if (localName == "CustomProperties")
									{
										goto IL_06C8;
									}
								}
							}
							else if (localName == "Interval")
							{
								goto IL_06C8;
							}
						}
						else if (localName == "SyncDataSetName")
						{
							goto IL_06C8;
						}
					}
					else if (num <= 3901846177U)
					{
						if (num != 3788247937U)
						{
							if (num != 3801439777U)
							{
								if (num == 3901846177U)
								{
									if (localName == "ScaleTypeSelection")
									{
										goto IL_06C8;
									}
								}
							}
							else if (localName == "Maximum")
							{
								goto IL_06C8;
							}
						}
						else if (localName == "PreventFontShrink")
						{
							goto IL_06C8;
						}
					}
					else if (num != 4091998080U)
					{
						if (num != 4110370301U)
						{
							if (num == 4240779403U)
							{
								if (localName == "IntervalOffset")
								{
									goto IL_06C8;
								}
							}
						}
						else if (localName == "Scalar")
						{
							flag = axisReader.ReadElementContentAsString().ToLowerInvariant() == "true";
							continue;
						}
					}
					else if (localName == "ChartAxisTitle")
					{
						goto IL_06C8;
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, axisReader);
				}
				IL_06C8:
				axisReader.Skip();
			}
			return flag;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		internal bool InRequiredNamespace(XmlReader xmlNode)
		{
			string namespaceURI = xmlNode.NamespaceURI;
			return !string.IsNullOrEmpty(xmlNode.Name.ToString()) && (namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition" || namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition" || namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition");
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000EC40 File Offset: 0x0000CE40
		internal void ParseReportSections(ReportParsingDiagnosticContext diagnosticContext, XmlReader reportSectionsReader, List<ReportSection> reportSections)
		{
			reportSectionsReader.Read();
			Contract.Check(reportSections != null, "Expecting reportSections to not be null");
			while (!reportSectionsReader.EOF)
			{
				if (reportSectionsReader.IsStartElement())
				{
					if (reportSectionsReader.LocalName == "ReportSection")
					{
						using (XmlReader xmlReader = reportSectionsReader.ReadSubtreeAndMoveToContent())
						{
							reportSections.Add(this.ParseReportSection(diagnosticContext, xmlReader));
							goto IL_005A;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, reportSectionsReader);
				}
				IL_005A:
				reportSectionsReader.Skip();
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		internal BackgroundImage ParseBackgroundImage(XmlReader backgroundImageReader, ReportParsingDiagnosticContext diagnosticContext)
		{
			backgroundImageReader.Read();
			ReportImageSource? reportImageSource = null;
			string text = string.Empty;
			string text2 = string.Empty;
			double num = 50.0;
			BackgroundImageRepeat backgroundImageRepeat = BackgroundImageRepeat.FitProportional;
			while (!backgroundImageReader.EOF)
			{
				if (backgroundImageReader.IsStartElement())
				{
					string localName = backgroundImageReader.LocalName;
					uint num2 = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num2 <= 2360978711U)
					{
						if (num2 != 1430321685U)
						{
							if (num2 != 1642243064U)
							{
								if (num2 == 2360978711U)
								{
									if (localName == "Transparency")
									{
										num = double.Parse(backgroundImageReader.ReadElementContentAsString(), CultureInfo.InvariantCulture);
										continue;
									}
								}
							}
							else if (localName == "Source")
							{
								reportImageSource = this.ParseImageSource(backgroundImageReader.ReadSubtreeAndMoveToContent(), diagnosticContext);
								goto IL_019E;
							}
						}
						else if (localName == "MIMEType")
						{
							text2 = backgroundImageReader.ReadElementContentAsString();
							continue;
						}
					}
					else if (num2 <= 3799987242U)
					{
						if (num2 != 3511155050U)
						{
							if (num2 == 3799987242U)
							{
								if (localName == "Position")
								{
									goto IL_019E;
								}
							}
						}
						else if (localName == "Value")
						{
							text = backgroundImageReader.ReadElementContentAsString();
							continue;
						}
					}
					else if (num2 != 3944519906U)
					{
						if (num2 == 4023266649U)
						{
							if (localName == "EmbeddingMode")
							{
								goto IL_019E;
							}
						}
					}
					else if (localName == "BackgroundRepeat")
					{
						using (XmlReader xmlReader = backgroundImageReader.ReadSubtreeAndMoveToContent())
						{
							BackgroundImageRepeat? backgroundImageRepeat2 = this.ParseBackgroundImageRepeat(xmlReader, diagnosticContext);
							if (backgroundImageRepeat2 != null)
							{
								backgroundImageRepeat = backgroundImageRepeat2.Value;
							}
							goto IL_019E;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, backgroundImageReader);
				}
				IL_019E:
				backgroundImageReader.Skip();
			}
			return new BackgroundImage(reportImageSource, text, text2, backgroundImageRepeat, num);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		internal bool TryParseSizingProperty(ReportParsingDiagnosticContext diagnosticContext, XmlReader childOfDataRegionNode, ReportItemProperties reportItemProperties)
		{
			if (childOfDataRegionNode == null)
			{
				return false;
			}
			string localName = childOfDataRegionNode.LocalName;
			if (localName == "Size")
			{
				reportItemProperties.Size = this.ParseSize(diagnosticContext, childOfDataRegionNode);
				return true;
			}
			if (localName == "Top")
			{
				reportItemProperties.Top = ReportSize.ParseReportSize(childOfDataRegionNode);
				return true;
			}
			if (localName == "Left")
			{
				reportItemProperties.Left = ReportSize.ParseReportSize(childOfDataRegionNode);
				return true;
			}
			if (!(localName == "ZIndex"))
			{
				return localName == "Width" || localName == "Height";
			}
			string text = childOfDataRegionNode.ReadElementContentAsString();
			reportItemProperties.ZIndex = (string.IsNullOrEmpty(text) ? 0 : int.Parse(text));
			return true;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000EF5C File Offset: 0x0000D15C
		private void ParseDataRegionProperty(XmlReader dataRegionReader, ReportParsingDiagnosticContext diagnosticContext, ReportItemProperties reportItemProperties)
		{
			if (dataRegionReader != null && !this.TryParseSizingProperty(diagnosticContext, dataRegionReader, reportItemProperties))
			{
				string localName = dataRegionReader.LocalName;
				if (localName == "Style")
				{
					reportItemProperties.Style = XNode.ReadFrom(dataRegionReader) as XElement;
					return;
				}
				if (!(localName == "ActionInfo"))
				{
					this.AddErrorIfInRequiredNamespace(diagnosticContext, dataRegionReader);
				}
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
		internal FailedReportItem CreateFailedReportItem(ReportParsingDiagnosticContext diagnosticContext)
		{
			string empty = string.Empty;
			ReportItemProperties reportItemProperties = new ReportItemProperties();
			return new FailedReportItem(empty, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, diagnosticContext);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		internal FailedReportItem CreateFailedReportItem(XmlReader reportItemReader, ReportParsingDiagnosticContext diagnosticContext, ReportItemProperties reportItemProperties = null, string name = null)
		{
			if (name == null)
			{
				name = reportItemReader.GetAttribute("Name");
			}
			reportItemReader.Read();
			if (reportItemProperties == null)
			{
				reportItemProperties = new ReportItemProperties();
				while (!reportItemReader.EOF)
				{
					if (reportItemReader.IsStartElement())
					{
						using (XmlReader xmlReader = reportItemReader.ReadSubtreeAndMoveToContent())
						{
							this.TryParseSizingProperty(diagnosticContext, xmlReader, reportItemProperties);
						}
					}
					reportItemReader.Skip();
				}
			}
			return new FailedReportItem(name, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, diagnosticContext);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000F06C File Offset: 0x0000D26C
		internal ReportItemRect ParseReportItemRect(ReportItemProperties reportItemProperties)
		{
			Size size = reportItemProperties.Size;
			if (size == null)
			{
				size = new Size(new ReportSize(0), new ReportSize(0), new ReportSize(0), new ReportSize(0), new ReportSize(0), new ReportSize(0));
			}
			return new ReportItemRect(reportItemProperties.Left, reportItemProperties.Top, size);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		internal ReportItem ParseCellContents(XmlReader cellContentsReader)
		{
			if (cellContentsReader == null)
			{
				ReportParsingDiagnosticContext reportParsingDiagnosticContext = new ReportParsingDiagnosticContext();
				reportParsingDiagnosticContext.AddError("MissingCellContents", new string[0]);
				return this.CreateFailedReportItem(reportParsingDiagnosticContext);
			}
			cellContentsReader.Read();
			ReportItem reportItem = null;
			while (!cellContentsReader.EOF)
			{
				if (cellContentsReader.IsStartElement() && !string.IsNullOrEmpty(cellContentsReader.LocalName))
				{
					using (XmlReader xmlReader = cellContentsReader.ReadSubtreeAndMoveToContent())
					{
						reportItem = this.ParseReportItem(xmlReader);
					}
				}
				cellContentsReader.Skip();
			}
			if (reportItem == null)
			{
				ReportParsingDiagnosticContext reportParsingDiagnosticContext2 = new ReportParsingDiagnosticContext();
				reportParsingDiagnosticContext2.AddError("MissingChildNode", new string[0]);
				reportItem = this.CreateFailedReportItem(reportParsingDiagnosticContext2);
			}
			return reportItem;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000F170 File Offset: 0x0000D370
		private TablixCell ParseTablixCell(ReportParsingDiagnosticContext diagnosticContext, XmlReader tablixCellsReader)
		{
			tablixCellsReader.Read();
			string text = null;
			ReportItem reportItem = null;
			while (!tablixCellsReader.EOF)
			{
				if (tablixCellsReader.IsStartElement())
				{
					string localName = tablixCellsReader.LocalName;
					if (localName == "DataSetName")
					{
						text = tablixCellsReader.ReadElementContentAsString();
						continue;
					}
					if (!(localName == "CellContents"))
					{
						if (localName == "Relationships")
						{
							goto IL_0071;
						}
					}
					else
					{
						using (XmlReader xmlReader = tablixCellsReader.ReadSubtreeAndMoveToContent())
						{
							reportItem = this.ParseCellContents(xmlReader);
							goto IL_0071;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, tablixCellsReader);
				}
				IL_0071:
				tablixCellsReader.Skip();
			}
			return new TablixCell(reportItem, text);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000F214 File Offset: 0x0000D414
		private void ParseTablixCells(ReportParsingDiagnosticContext diagnosticContext, XmlReader tablixCellsReader, List<TablixCell> cells)
		{
			tablixCellsReader.Read();
			while (!tablixCellsReader.EOF)
			{
				if (tablixCellsReader.IsStartElement())
				{
					if (tablixCellsReader.LocalName == "TablixCell")
					{
						using (XmlReader xmlReader = tablixCellsReader.ReadSubtreeAndMoveToContent())
						{
							cells.Add(this.ParseTablixCell(diagnosticContext, xmlReader));
							goto IL_004C;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, tablixCellsReader);
				}
				IL_004C:
				tablixCellsReader.Skip();
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000F28C File Offset: 0x0000D48C
		internal TablixRow ParseTablixRow(ReportParsingDiagnosticContext diagnosticContext, XmlReader tablixRowReader)
		{
			ReportSize reportSize = new ReportSize(0);
			List<TablixCell> list = new List<TablixCell>();
			tablixRowReader.Read();
			while (!tablixRowReader.EOF)
			{
				if (tablixRowReader.IsStartElement())
				{
					string localName = tablixRowReader.LocalName;
					if (!(localName == "Height"))
					{
						if (localName == "TablixCells")
						{
							using (XmlReader xmlReader = tablixRowReader.ReadSubtreeAndMoveToContent())
							{
								this.ParseTablixCells(diagnosticContext, xmlReader, list);
								goto IL_0077;
							}
						}
						this.AddErrorIfInRequiredNamespace(diagnosticContext, tablixRowReader);
					}
					else
					{
						ReportSize reportSize2 = ReportSize.ParseReportSize(tablixRowReader);
						if (reportSize2 != null)
						{
							reportSize = reportSize2;
							continue;
						}
						continue;
					}
				}
				IL_0077:
				tablixRowReader.Skip();
			}
			return new TablixRow(reportSize, list);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000F338 File Offset: 0x0000D538
		internal List<TablixRow> ParseTablixRows(ReportParsingDiagnosticContext diagnosticContext, XmlReader tablixRowsReader)
		{
			List<TablixRow> list = new List<TablixRow>();
			tablixRowsReader.Read();
			while (!tablixRowsReader.EOF)
			{
				if (tablixRowsReader.LocalName == "TablixRow")
				{
					using (XmlReader xmlReader = tablixRowsReader.ReadSubtreeAndMoveToContent())
					{
						list.Add(this.ParseTablixRow(diagnosticContext, xmlReader));
						goto IL_004A;
					}
					goto IL_0042;
				}
				goto IL_0042;
				IL_004A:
				tablixRowsReader.Skip();
				continue;
				IL_0042:
				this.AddErrorIfInRequiredNamespace(diagnosticContext, tablixRowsReader);
				goto IL_004A;
			}
			return list;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		internal List<ReportSize> ParseTablixColumns(ReportParsingDiagnosticContext diagnosticContext, XmlReader tablixColumnsReader)
		{
			List<ReportSize> list = new List<ReportSize>();
			tablixColumnsReader.Read();
			while (!tablixColumnsReader.EOF)
			{
				if (tablixColumnsReader.IsStartElement())
				{
					if (tablixColumnsReader.LocalName == "TablixColumn")
					{
						using (XmlReader xmlReader = tablixColumnsReader.ReadSubtreeAndMoveToContent())
						{
							xmlReader.Read();
							ReportSize reportSize = new ReportSize(0);
							while (!xmlReader.EOF)
							{
								if (xmlReader.IsStartElement())
								{
									if (xmlReader.LocalName == "Width")
									{
										reportSize = ReportSize.ParseReportSize(xmlReader);
										continue;
									}
									this.AddErrorIfInRequiredNamespace(diagnosticContext, xmlReader);
								}
								xmlReader.Skip();
							}
							list.Add(reportSize);
							goto IL_0097;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, tablixColumnsReader);
				}
				IL_0097:
				tablixColumnsReader.Skip();
			}
			return list;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000F478 File Offset: 0x0000D678
		internal TablixBody ParseTablixBody(ReportParsingDiagnosticContext diagnosticContext, XmlReader tablixBodyReader)
		{
			tablixBodyReader.Read();
			List<ReportSize> list = null;
			List<TablixRow> list2 = null;
			while (!tablixBodyReader.EOF)
			{
				if (tablixBodyReader.IsStartElement())
				{
					string localName = tablixBodyReader.LocalName;
					if (!(localName == "TablixColumns"))
					{
						if (!(localName == "TablixRows"))
						{
							goto IL_0074;
						}
					}
					else
					{
						using (XmlReader xmlReader = tablixBodyReader.ReadSubtreeAndMoveToContent())
						{
							list = this.ParseTablixColumns(diagnosticContext, xmlReader);
							goto IL_007C;
						}
					}
					using (XmlReader xmlReader2 = tablixBodyReader.ReadSubtreeAndMoveToContent())
					{
						list2 = this.ParseTablixRows(diagnosticContext, xmlReader2);
						goto IL_007C;
					}
					IL_0074:
					this.AddErrorIfInRequiredNamespace(diagnosticContext, tablixBodyReader);
				}
				IL_007C:
				tablixBodyReader.Skip();
			}
			Contract.Check(list != null && list2 != null, "TablixBody expects Rows and Columns");
			return new TablixBody(list, list2);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000F548 File Offset: 0x0000D748
		internal TablixHeader ParseTablixHeader(ReportParsingDiagnosticContext diagnosticContext, XmlReader tablixHeaderReader)
		{
			tablixHeaderReader.Read();
			ReportItem reportItem = null;
			ReportSize reportSize = null;
			while (!tablixHeaderReader.EOF)
			{
				if (tablixHeaderReader.IsStartElement())
				{
					string localName = tablixHeaderReader.LocalName;
					if (!(localName == "CellContents"))
					{
						if (!(localName == "Size"))
						{
							if (!(localName == "SizeBasedOnChildren"))
							{
								this.AddErrorIfInRequiredNamespace(diagnosticContext, tablixHeaderReader);
								goto IL_0071;
							}
							goto IL_0071;
						}
					}
					else
					{
						using (XmlReader xmlReader = tablixHeaderReader.ReadSubtreeAndMoveToContent())
						{
							reportItem = this.ParseCellContents(xmlReader);
							goto IL_0071;
						}
					}
					reportSize = ReportSize.ParseReportSize(tablixHeaderReader);
					continue;
				}
				IL_0071:
				tablixHeaderReader.Skip();
			}
			return new TablixHeader(reportItem, reportSize);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000F5EC File Offset: 0x0000D7EC
		internal TablixMember ParseTablixMember(ReportParsingDiagnosticContext diagnosticContext, XmlReader tablixMemberReader)
		{
			Group group = null;
			List<SortExpression> list = null;
			TablixHeader tablixHeader = null;
			List<TablixMember> list2 = new List<TablixMember>();
			bool flag = false;
			tablixMemberReader.Read();
			while (!tablixMemberReader.EOF)
			{
				if (tablixMemberReader.IsStartElement())
				{
					string localName = tablixMemberReader.LocalName;
					if (localName == "Group")
					{
						using (XmlReader xmlReader = tablixMemberReader.ReadSubtreeAndMoveToContent())
						{
							group = this.ParseGroup(diagnosticContext, xmlReader);
							goto IL_017C;
						}
						goto IL_009F;
					}
					if (localName == "SortExpressions")
					{
						goto IL_009F;
					}
					if (localName == "TablixHeader")
					{
						goto IL_00C2;
					}
					if (localName == "TablixMembers")
					{
						goto IL_00E5;
					}
					if (!(localName == "Subtotal"))
					{
						this.AddErrorIfInRequiredNamespace(diagnosticContext, tablixMemberReader);
						goto IL_017C;
					}
					IL_0160:
					flag = tablixMemberReader.ReadElementContentAsString() == "true";
					continue;
					IL_00E5:
					using (XmlReader xmlReader2 = tablixMemberReader.ReadSubtreeAndMoveToContent())
					{
						xmlReader2.Read();
						while (!xmlReader2.EOF)
						{
							if (xmlReader2.IsStartElement())
							{
								if (xmlReader2.LocalName == "TablixMember")
								{
									using (XmlReader xmlReader3 = xmlReader2.ReadSubtreeAndMoveToContent())
									{
										list2.Add(this.ParseTablixMember(diagnosticContext, xmlReader3));
										goto IL_0142;
									}
								}
								this.AddErrorIfInRequiredNamespace(diagnosticContext, xmlReader2);
							}
							IL_0142:
							xmlReader2.Skip();
						}
						goto IL_017C;
					}
					goto IL_0160;
					IL_009F:
					using (XmlReader xmlReader4 = tablixMemberReader.ReadSubtreeAndMoveToContent())
					{
						list = this.ParseSortExpressions(diagnosticContext, xmlReader4);
						goto IL_017C;
					}
					IL_00C2:
					using (XmlReader xmlReader5 = tablixMemberReader.ReadSubtreeAndMoveToContent())
					{
						tablixHeader = this.ParseTablixHeader(diagnosticContext, xmlReader5);
						goto IL_017C;
					}
					goto IL_00E5;
				}
				IL_017C:
				tablixMemberReader.Skip();
			}
			return new TablixMember(group, list, tablixHeader, list2, flag);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000F7D4 File Offset: 0x0000D9D4
		internal TablixHierarchy ParseTablixHierarchy(ReportParsingDiagnosticContext diagnosticContext, XmlReader tablixHierarchyReader)
		{
			tablixHierarchyReader.Read();
			List<TablixMember> list = new List<TablixMember>();
			bool flag = false;
			bool flag2 = false;
			while (!tablixHierarchyReader.EOF)
			{
				if (tablixHierarchyReader.IsStartElement())
				{
					string localName = tablixHierarchyReader.LocalName;
					if (!(localName == "TablixMembers"))
					{
						if (!(localName == "Subtotals"))
						{
							if (!(localName == "EnableDrilldown"))
							{
								this.AddErrorIfInRequiredNamespace(diagnosticContext, tablixHierarchyReader);
								goto IL_0103;
							}
							flag2 = tablixHierarchyReader.ReadElementContentAsString() == "true";
							continue;
						}
					}
					else
					{
						using (XmlReader xmlReader = tablixHierarchyReader.ReadSubtreeAndMoveToContent())
						{
							xmlReader.Read();
							while (!xmlReader.EOF)
							{
								if (xmlReader.IsStartElement())
								{
									if (xmlReader.LocalName == "TablixMember")
									{
										using (XmlReader xmlReader2 = xmlReader.ReadSubtreeAndMoveToContent())
										{
											list.Add(this.ParseTablixMember(diagnosticContext, xmlReader2));
											goto IL_00B7;
										}
									}
									this.AddErrorIfInRequiredNamespace(diagnosticContext, xmlReader);
								}
								IL_00B7:
								xmlReader.Skip();
							}
							goto IL_0103;
						}
					}
					flag = tablixHierarchyReader.ReadElementContentAsString() == "true";
					continue;
				}
				IL_0103:
				tablixHierarchyReader.Skip();
			}
			return new TablixHierarchy(list, flag, flag2);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000F91C File Offset: 0x0000DB1C
		internal LabelData ParseLabelData(ReportParsingDiagnosticContext diagnosticContext, XmlReader labelDataReader)
		{
			labelDataReader.Read();
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			while (!labelDataReader.EOF)
			{
				if (labelDataReader.IsStartElement())
				{
					string localName = labelDataReader.LocalName;
					if (localName == "DataSetName")
					{
						text2 = labelDataReader.ReadElementContentAsString();
						continue;
					}
					if (localName == "Label")
					{
						text = labelDataReader.ReadElementContentAsString();
						continue;
					}
					if (localName == "Key")
					{
						text3 = labelDataReader.ReadElementContentAsString();
						continue;
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, labelDataReader);
				}
				labelDataReader.Skip();
			}
			return new LabelData(text3, text, text2);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000F9B8 File Offset: 0x0000DBB8
		internal Slider ParseSlider(ReportParsingDiagnosticContext diagnosticContext, XmlReader sliderReader)
		{
			sliderReader.Read();
			LabelData labelData = null;
			while (!sliderReader.EOF)
			{
				if (sliderReader.IsStartElement())
				{
					if (sliderReader.LocalName == "LabelData")
					{
						using (XmlReader xmlReader = sliderReader.ReadSubtreeAndMoveToContent())
						{
							labelData = this.ParseLabelData(diagnosticContext, xmlReader);
							goto IL_0049;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, sliderReader);
				}
				IL_0049:
				sliderReader.Skip();
			}
			return new Slider(labelData);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000FA34 File Offset: 0x0000DC34
		internal Navigation ParseNavigation(ReportParsingDiagnosticContext diagnosticContext, XmlReader navigationReader, NavigationType navigationType)
		{
			Slider slider = null;
			navigationReader.Read();
			while (!navigationReader.EOF)
			{
				if (navigationReader.IsStartElement())
				{
					string localName = navigationReader.LocalName;
					if (!(localName == "Slider"))
					{
						if (localName == "NavigationItem")
						{
							goto IL_005A;
						}
					}
					else
					{
						using (XmlReader xmlReader = navigationReader.ReadSubtreeAndMoveToContent())
						{
							slider = this.ParseSlider(diagnosticContext, xmlReader);
							goto IL_005A;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, navigationReader);
				}
				IL_005A:
				navigationReader.Skip();
			}
			return new Navigation(navigationType, slider);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000FAC0 File Offset: 0x0000DCC0
		internal BandLayoutOptions ParseBandLayoutOptions(ReportParsingDiagnosticContext diagnosticContext, XmlReader bandLayoutOptionsReader)
		{
			Navigation navigation = null;
			int num = 0;
			int num2 = 0;
			bool flag = true;
			bandLayoutOptionsReader.Read();
			while (!bandLayoutOptionsReader.EOF)
			{
				if (bandLayoutOptionsReader.IsStartElement())
				{
					string localName = bandLayoutOptionsReader.LocalName;
					if (localName == "PlayAxis")
					{
						using (XmlReader xmlReader = bandLayoutOptionsReader.ReadSubtreeAndMoveToContent())
						{
							navigation = this.ParseNavigation(diagnosticContext, xmlReader, NavigationType.PlayAxis);
							goto IL_0113;
						}
						goto IL_00A4;
					}
					if (localName == "Tabstrip")
					{
						goto IL_00A4;
					}
					if (localName == "Coverflow")
					{
						goto IL_00C5;
					}
					if (!(localName == "RowCount"))
					{
						if (localName == "ColumnCount")
						{
							num2 = bandLayoutOptionsReader.ReadElementContentAsInt();
							continue;
						}
						if (!(localName == "AutomaticColumnAndRowCount"))
						{
							this.AddErrorIfInRequiredNamespace(diagnosticContext, bandLayoutOptionsReader);
							goto IL_0113;
						}
						flag = bandLayoutOptionsReader.ReadElementContentAsString() == "true";
						continue;
					}
					IL_00E6:
					num = bandLayoutOptionsReader.ReadElementContentAsInt();
					continue;
					IL_00A4:
					using (XmlReader xmlReader2 = bandLayoutOptionsReader.ReadSubtreeAndMoveToContent())
					{
						navigation = this.ParseNavigation(diagnosticContext, xmlReader2, NavigationType.Tabstrip);
						goto IL_0113;
					}
					IL_00C5:
					using (XmlReader xmlReader3 = bandLayoutOptionsReader.ReadSubtreeAndMoveToContent())
					{
						navigation = this.ParseNavigation(diagnosticContext, xmlReader3, NavigationType.Coverflow);
						goto IL_0113;
					}
					goto IL_00E6;
				}
				IL_0113:
				bandLayoutOptionsReader.Skip();
			}
			return new BandLayoutOptions(navigation, num, num2, flag);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000FC24 File Offset: 0x0000DE24
		internal ReportItem ParseTablix(XmlReader reportItemReader)
		{
			string text = string.Empty;
			string text2 = this.ParseReportObjectName(reportItemReader);
			reportItemReader.Read();
			TablixHierarchy tablixHierarchy = null;
			TablixHierarchy tablixHierarchy2 = null;
			TablixBody tablixBody = null;
			bool flag = false;
			string text3 = string.Empty;
			string text4 = "Matrix";
			IFilterCondition<IRdmQueryExpression> filterCondition = null;
			BandLayoutOptions bandLayoutOptions = null;
			ReportParsingDiagnosticContext reportParsingDiagnosticContext = new ReportParsingDiagnosticContext();
			int num = 0;
			ReportItemProperties reportItemProperties = new ReportItemProperties();
			try
			{
				while (!reportItemReader.EOF)
				{
					if (reportItemReader.IsStartElement())
					{
						string localName = reportItemReader.LocalName;
						uint num2 = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
						if (num2 > 1829703532U)
						{
							if (num2 <= 3210053446U)
							{
								if (num2 <= 2613749273U)
								{
									if (num2 != 1848122508U)
									{
										if (num2 != 2043379210U)
										{
											if (num2 != 2613749273U)
											{
												goto IL_0502;
											}
											if (!(localName == "TablixBody"))
											{
												goto IL_0502;
											}
											using (XmlReader xmlReader = reportItemReader.ReadSubtreeAndMoveToContent())
											{
												tablixBody = this.ParseTablixBody(reportParsingDiagnosticContext, xmlReader);
												goto IL_0524;
											}
										}
										else
										{
											if (!(localName == "RepeatColumnHeaders"))
											{
												goto IL_0502;
											}
											goto IL_0524;
										}
									}
									else
									{
										if (!(localName == "CanScroll"))
										{
											goto IL_0502;
										}
										goto IL_0524;
									}
								}
								else if (num2 != 2974894206U)
								{
									if (num2 != 3039688044U)
									{
										if (num2 != 3210053446U)
										{
											goto IL_0502;
										}
										if (!(localName == "TablixColumnHierarchy"))
										{
											goto IL_0502;
										}
									}
									else
									{
										if (!(localName == "RepeatRowHeaders"))
										{
											goto IL_0502;
										}
										goto IL_0524;
									}
								}
								else
								{
									if (!(localName == "LeftMargin"))
									{
										goto IL_0502;
									}
									goto IL_0524;
								}
								using (XmlReader xmlReader2 = reportItemReader.ReadSubtreeAndMoveToContent())
								{
									tablixHierarchy = this.ParseTablixHierarchy(reportParsingDiagnosticContext, xmlReader2);
									goto IL_0524;
								}
								goto IL_044E;
							}
							if (num2 <= 3505350856U)
							{
								if (num2 != 3280635411U)
								{
									if (num2 != 3343458690U)
									{
										if (num2 != 3505350856U)
										{
											goto IL_0502;
										}
										if (!(localName == "DataSetName"))
										{
											goto IL_0502;
										}
										text = reportItemReader.ReadElementContentAsString();
										continue;
									}
									else
									{
										if (!(localName == "TopMargin"))
										{
											goto IL_0502;
										}
										goto IL_0524;
									}
								}
								else if (!(localName == "IsCallout"))
								{
									goto IL_0502;
								}
							}
							else if (num2 != 3512062061U)
							{
								if (num2 != 3991678602U)
								{
									if (num2 != 4119883099U)
									{
										goto IL_0502;
									}
									if (!(localName == "RightMargin"))
									{
										goto IL_0502;
									}
									goto IL_0524;
								}
								else
								{
									if (!(localName == "TablixRowHierarchy"))
									{
										goto IL_0502;
									}
									goto IL_044E;
								}
							}
							else
							{
								if (!(localName == "Type"))
								{
									goto IL_0502;
								}
								string text5 = reportItemReader.ReadElementContentAsString();
								if (!string.IsNullOrEmpty(text5))
								{
									text4 = text5;
									continue;
								}
								continue;
							}
							IL_0472:
							flag = reportItemReader.ReadElementContentAsString() == "true";
							continue;
							IL_044E:
							using (XmlReader xmlReader3 = reportItemReader.ReadSubtreeAndMoveToContent())
							{
								tablixHierarchy2 = this.ParseTablixHierarchy(reportParsingDiagnosticContext, xmlReader3);
								goto IL_0524;
							}
							goto IL_0472;
						}
						if (num2 > 647081108U)
						{
							if (num2 <= 1321656236U)
							{
								if (num2 != 928931998U)
								{
									if (num2 != 1320388628U)
									{
										if (num2 != 1321656236U)
										{
											goto IL_0502;
										}
										if (!(localName == "TablixCorner"))
										{
											goto IL_0502;
										}
										goto IL_0524;
									}
									else
									{
										if (!(localName == "ReportSlicerState"))
										{
											goto IL_0502;
										}
										XElement xelement = (XNode.ReadFrom(reportItemReader) as XElement).ElementByLocalName("CompoundFilterCondition");
										if (xelement != null)
										{
											filterCondition = ReportStateParser.ParseCompoundFilterCondition(xelement);
											continue;
										}
										continue;
									}
								}
								else
								{
									if (!(localName == "BandLayoutOptions"))
									{
										goto IL_0502;
									}
									using (XmlReader xmlReader4 = reportItemReader.ReadSubtreeAndMoveToContent())
									{
										bandLayoutOptions = this.ParseBandLayoutOptions(reportParsingDiagnosticContext, xmlReader4);
										goto IL_0524;
									}
								}
							}
							else if (num2 != 1326047838U)
							{
								if (num2 != 1792547896U)
								{
									if (num2 != 1829703532U)
									{
										goto IL_0502;
									}
									if (!(localName == "BottomMargin"))
									{
										goto IL_0502;
									}
									goto IL_0524;
								}
								else
								{
									if (!(localName == "LayoutDirection"))
									{
										goto IL_0502;
									}
									goto IL_0524;
								}
							}
							else if (!(localName == "FontSizeOffset"))
							{
								goto IL_0502;
							}
							num = reportItemReader.ReadElementContentAsInt();
							continue;
						}
						if (num2 <= 162835443U)
						{
							if (num2 != 87033402U)
							{
								if (num2 != 160701591U)
								{
									if (num2 == 162835443U)
									{
										if (localName == "GroupsBeforeRowHeader")
										{
											goto IL_0524;
										}
									}
								}
								else if (localName == "FixedRowHeaders")
								{
									goto IL_0524;
								}
							}
							else if (localName == "KeepTogether")
							{
								goto IL_0524;
							}
						}
						else if (num2 != 278515219U)
						{
							if (num2 != 635319263U)
							{
								if (num2 == 647081108U)
								{
									if (localName == "CalloutStyle")
									{
										text3 = reportItemReader.ReadElementContentAsString();
										continue;
									}
								}
							}
							else if (localName == "OmitBorderOnPageBreak")
							{
								goto IL_0524;
							}
						}
						else if (localName == "FixedColumnHeaders")
						{
							goto IL_0524;
						}
						IL_0502:
						using (XmlReader xmlReader5 = reportItemReader.ReadSubtreeAndMoveToContent())
						{
							this.ParseDataRegionProperty(xmlReader5, reportParsingDiagnosticContext, reportItemProperties);
						}
					}
					IL_0524:
					reportItemReader.Skip();
				}
			}
			catch (Exception)
			{
				reportParsingDiagnosticContext.AddError("InvalidReportItem", new string[0]);
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if (reportParsingDiagnosticContext.HasError)
			{
				return new FailedReportItem(text2, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, reportParsingDiagnosticContext);
			}
			return new Tablix(text2, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, reportParsingDiagnosticContext, text, tablixHierarchy, tablixHierarchy2, tablixBody, text4, flag, text3, filterCondition, bandLayoutOptions, num);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00010264 File Offset: 0x0000E464
		internal RdmTextRun ParseTextRun(ReportParsingDiagnosticContext diagnosticContext, XmlReader textRunReader, ref string customFormatString, ref FormatType formatType)
		{
			string text = string.Empty;
			Style style = new Style();
			textRunReader.Read();
			while (!textRunReader.EOF)
			{
				if (textRunReader.IsStartElement())
				{
					string localName = textRunReader.LocalName;
					if (localName == "Value")
					{
						text = textRunReader.ReadElementContentAsString();
						continue;
					}
					if (!(localName == "Style"))
					{
						if (localName == "ActionInfo" || localName == "Label" || localName == "ToolTip" || localName == "MarkupType")
						{
							goto IL_0108;
						}
					}
					else
					{
						XElement xelement = XNode.ReadFrom(textRunReader) as XElement;
						XElement xelement2 = xelement.ElementByLocalName("FormatType");
						if (xelement2 != null && xelement2.Value == "Manual")
						{
							formatType = FormatType.Manual;
						}
						XElement xelement3 = xelement.ElementByLocalName("Format");
						if (xelement3 != null)
						{
							customFormatString = xelement3.Value;
						}
						using (XmlReader xmlReader = xelement.CreateReader())
						{
							xmlReader.MoveToContent();
							style = this.ParseStyle(diagnosticContext, xmlReader);
							goto IL_0108;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, textRunReader);
				}
				IL_0108:
				textRunReader.Skip();
			}
			return new RdmTextRun(text, style);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000103A4 File Offset: 0x0000E5A4
		internal void ParseParagraphs(ReportParsingDiagnosticContext diagnosticContext, XmlReader paragraphsReader, List<RdmParagraph> paragraphs, ref string customFormatString, ref FormatType formatType)
		{
			paragraphsReader.Read();
			while (!paragraphsReader.EOF)
			{
				if (paragraphsReader.IsStartElement())
				{
					if (paragraphsReader.LocalName == "Paragraph")
					{
						List<RdmTextRun> list = new List<RdmTextRun>();
						Style style = new Style();
						using (XmlReader xmlReader = paragraphsReader.ReadSubtreeAndMoveToContent())
						{
							xmlReader.Read();
							while (!xmlReader.EOF)
							{
								if (xmlReader.IsStartElement())
								{
									string localName = xmlReader.LocalName;
									if (!(localName == "TextRuns"))
									{
										if (!(localName == "Style"))
										{
											goto IL_0121;
										}
									}
									else
									{
										using (XmlReader xmlReader2 = xmlReader.ReadSubtreeAndMoveToContent())
										{
											xmlReader2.Read();
											while (!xmlReader2.EOF)
											{
												if (xmlReader2.IsStartElement())
												{
													if (xmlReader2.LocalName == "TextRun")
													{
														using (XmlReader xmlReader3 = xmlReader2.ReadSubtreeAndMoveToContent())
														{
															list.Add(this.ParseTextRun(diagnosticContext, xmlReader3, ref customFormatString, ref formatType));
															goto IL_00E3;
														}
													}
													this.AddErrorIfInRequiredNamespace(diagnosticContext, xmlReader2);
												}
												IL_00E3:
												xmlReader2.Skip();
											}
											goto IL_0129;
										}
									}
									using (XmlReader xmlReader4 = xmlReader.ReadSubtreeAndMoveToContent())
									{
										style = this.ParseStyle(diagnosticContext, xmlReader4);
										goto IL_0129;
									}
									IL_0121:
									this.AddErrorIfInRequiredNamespace(diagnosticContext, xmlReader);
								}
								IL_0129:
								xmlReader.Skip();
							}
							Style style2 = new Style(new Dictionary<string, string> { { "Wrapped", "true" } });
							RdmParagraph rdmParagraph = new RdmParagraph(list, style.Combine(style2));
							paragraphs.Add(rdmParagraph);
							goto IL_0180;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, paragraphsReader);
				}
				IL_0180:
				paragraphsReader.Skip();
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000105A8 File Offset: 0x0000E7A8
		private ReportItem ParseTextbox(XmlReader textboxReader)
		{
			string text = this.ParseReportObjectName(textboxReader);
			List<RdmParagraph> list = new List<RdmParagraph>();
			Style style = new Style();
			FormatType formatType = FormatType.Auto;
			string empty = string.Empty;
			ReportParsingDiagnosticContext reportParsingDiagnosticContext = new ReportParsingDiagnosticContext();
			ReportItemProperties reportItemProperties = new ReportItemProperties();
			textboxReader.Read();
			while (!textboxReader.EOF)
			{
				if (textboxReader.IsStartElement())
				{
					string localName = textboxReader.LocalName;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num <= 1874641522U)
					{
						if (num <= 445182469U)
						{
							if (num != 87033402U)
							{
								if (num == 445182469U)
								{
									if (localName == "CanScrollVertically")
									{
										goto IL_0218;
									}
								}
							}
							else if (localName == "KeepTogether")
							{
								goto IL_0218;
							}
						}
						else if (num != 1284758864U)
						{
							if (num != 1567648135U)
							{
								if (num == 1874641522U)
								{
									if (localName == "UserSort")
									{
										goto IL_0218;
									}
								}
							}
							else if (localName == "HideDuplicates")
							{
								goto IL_0218;
							}
						}
						else if (localName == "CanShrink")
						{
							goto IL_0218;
						}
					}
					else if (num <= 3190832328U)
					{
						if (num != 1972810922U)
						{
							if (num != 2826696094U)
							{
								if (num == 3190832328U)
								{
									if (localName == "DataElementStyle")
									{
										goto IL_0218;
									}
								}
							}
							else if (localName == "Paragraphs")
							{
								using (XmlReader xmlReader = textboxReader.ReadSubtreeAndMoveToContent())
								{
									this.ParseParagraphs(reportParsingDiagnosticContext, xmlReader, list, ref empty, ref formatType);
									goto IL_0218;
								}
							}
						}
						else if (localName == "UserHasSwitchedFont")
						{
							goto IL_0218;
						}
					}
					else if (num != 3352889914U)
					{
						if (num != 3605113402U)
						{
							if (num == 3989628511U)
							{
								if (localName == "WatermarkTextbox")
								{
									goto IL_0218;
								}
							}
						}
						else if (localName == "ToggleImage")
						{
							goto IL_0218;
						}
					}
					else if (localName == "CanGrow")
					{
						goto IL_0218;
					}
					using (XmlReader xmlReader2 = textboxReader.ReadSubtreeAndMoveToContent())
					{
						this.ParseDataRegionProperty(xmlReader2, reportParsingDiagnosticContext, reportItemProperties);
					}
				}
				IL_0218:
				textboxReader.Skip();
			}
			ReportItemRect reportItemRect = this.ParseReportItemRect(reportItemProperties);
			if (reportParsingDiagnosticContext.HasError)
			{
				return new FailedReportItem(text, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, reportParsingDiagnosticContext);
			}
			if (reportItemProperties.Style != null)
			{
				XmlReader xmlReader3 = reportItemProperties.Style.CreateReader();
				xmlReader3.MoveToContent();
				style = this.ParseStyle(reportParsingDiagnosticContext, xmlReader3);
			}
			return new Textbox(text, reportItemRect, reportItemProperties.ZIndex, reportParsingDiagnosticContext, list, style, formatType, empty);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00010868 File Offset: 0x0000EA68
		private Tuple<string, string> ParseImageLookup(XmlReader imageLookupReader, ReportParsingDiagnosticContext diagnosticContext)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			imageLookupReader.Read();
			while (!imageLookupReader.EOF)
			{
				if (imageLookupReader.IsStartElement())
				{
					string localName = imageLookupReader.LocalName;
					if (localName == "FieldName")
					{
						text = ExpressionBuilder.Build(imageLookupReader.ReadElementContentAsString());
						continue;
					}
					if (localName == "DataSetName")
					{
						text2 = imageLookupReader.ReadElementContentAsString();
						continue;
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, imageLookupReader);
				}
				imageLookupReader.Skip();
			}
			return new Tuple<string, string>(text, text2);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000108EC File Offset: 0x0000EAEC
		private ReportImageSizing? ParseImageSizing(XmlReader sizingReader, ReportParsingDiagnosticContext diagnosticContext)
		{
			string text = sizingReader.ReadElementContentAsString();
			if (text != null)
			{
				if (text == "AutoSize")
				{
					return new ReportImageSizing?(ReportImageSizing.AutoSize);
				}
				if (text == "Fit")
				{
					return new ReportImageSizing?(ReportImageSizing.Fit);
				}
				if (text == "FitProportional")
				{
					return new ReportImageSizing?(ReportImageSizing.FitProportional);
				}
				if (text == "Clip")
				{
					return new ReportImageSizing?(ReportImageSizing.Clip);
				}
				diagnosticContext.AddError("InvalidReportImageSizing", new string[0]);
			}
			else
			{
				diagnosticContext.AddError("UnexpectedRdlContent", new string[0]);
			}
			return null;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00010984 File Offset: 0x0000EB84
		internal ReportItem ParseImage(XmlReader imageReader)
		{
			string text = this.ParseReportObjectName(imageReader);
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string text6 = null;
			ReportParsingDiagnosticContext reportParsingDiagnosticContext = new ReportParsingDiagnosticContext();
			ReportItemProperties reportItemProperties = new ReportItemProperties();
			ReportImageSource? reportImageSource = null;
			ReportImageSizing? reportImageSizing = null;
			string text7 = null;
			string text8 = null;
			imageReader.Read();
			while (!imageReader.EOF)
			{
				if (imageReader.IsStartElement())
				{
					string localName = imageReader.LocalName;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num <= 2759779951U)
					{
						if (num <= 1642243064U)
						{
							if (num != 1430321685U)
							{
								if (num != 1642243064U)
								{
									goto IL_0237;
								}
								if (!(localName == "Source"))
								{
									goto IL_0237;
								}
								using (XmlReader xmlReader = imageReader.ReadSubtreeAndMoveToContent())
								{
									reportImageSource = this.ParseImageSource(xmlReader, reportParsingDiagnosticContext);
									goto IL_0259;
								}
							}
							else if (!(localName == "MIMEType"))
							{
								goto IL_0237;
							}
							text3 = imageReader.ReadElementContentAsString();
							continue;
						}
						if (num != 2055080892U)
						{
							if (num != 2759779951U)
							{
								goto IL_0237;
							}
							if (!(localName == "Sizing"))
							{
								goto IL_0237;
							}
							using (XmlReader xmlReader2 = imageReader.ReadSubtreeAndMoveToContent())
							{
								reportImageSizing = this.ParseImageSizing(xmlReader2, reportParsingDiagnosticContext);
								goto IL_0259;
							}
						}
						else
						{
							if (!(localName == "ImageCacheScope"))
							{
								goto IL_0237;
							}
							text4 = imageReader.ReadElementContentAsString();
							continue;
						}
					}
					else if (num <= 3511155050U)
					{
						if (num != 2865558684U)
						{
							if (num != 3511155050U)
							{
								goto IL_0237;
							}
							if (!(localName == "Value"))
							{
								goto IL_0237;
							}
							text2 = imageReader.ReadElementContentAsString();
							continue;
						}
						else if (!(localName == "ImageLookup"))
						{
							goto IL_0237;
						}
					}
					else if (num != 4023266649U)
					{
						if (num != 4169356339U)
						{
							goto IL_0237;
						}
						if (!(localName == "Tag"))
						{
							goto IL_0237;
						}
						text5 = imageReader.ReadElementContentAsString();
						continue;
					}
					else
					{
						if (!(localName == "EmbeddingMode"))
						{
							goto IL_0237;
						}
						goto IL_0259;
					}
					using (XmlReader xmlReader3 = imageReader.ReadSubtreeAndMoveToContent())
					{
						Tuple<string, string> tuple = this.ParseImageLookup(xmlReader3, reportParsingDiagnosticContext);
						text7 = tuple.Item1;
						text8 = tuple.Item2;
						goto IL_0259;
					}
					IL_0237:
					using (XmlReader xmlReader4 = imageReader.ReadSubtreeAndMoveToContent())
					{
						this.ParseDataRegionProperty(xmlReader4, reportParsingDiagnosticContext, reportItemProperties);
					}
				}
				IL_0259:
				imageReader.Skip();
			}
			ImageLookup imageLookup = null;
			if (!string.IsNullOrEmpty(text7))
			{
				if (!string.IsNullOrEmpty(text2))
				{
					reportParsingDiagnosticContext.AddError("UnexpectedRdlContent", new string[0]);
				}
				imageLookup = new ImageLookup(reportImageSource, text7, text3, text8);
			}
			if (reportParsingDiagnosticContext.HasError)
			{
				return new FailedReportItem(text, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, reportParsingDiagnosticContext);
			}
			return new Image(text, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, reportParsingDiagnosticContext, reportImageSource, text2, text3, reportImageSizing, text5, text4, text6, imageLookup);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00010CAC File Offset: 0x0000EEAC
		private ReportItem ParseRectangle(XmlReader rectangleReader)
		{
			List<ReportItem> list = null;
			ReportParsingDiagnosticContext reportParsingDiagnosticContext = new ReportParsingDiagnosticContext();
			ReportItemProperties reportItemProperties = new ReportItemProperties();
			string text = this.ParseReportObjectName(rectangleReader);
			new ReportSize(0);
			rectangleReader.Read();
			while (!rectangleReader.EOF)
			{
				if (rectangleReader.IsStartElement())
				{
					string localName = rectangleReader.LocalName;
					if (!(localName == "ReportItems"))
					{
						if (localName == "Style")
						{
							goto IL_009D;
						}
					}
					else
					{
						using (XmlReader xmlReader = rectangleReader.ReadSubtreeAndMoveToContent())
						{
							list = this.ParseReportItems(xmlReader);
							goto IL_009D;
						}
					}
					using (XmlReader xmlReader2 = rectangleReader.ReadSubtreeAndMoveToContent())
					{
						if (!this.TryParseSizingProperty(reportParsingDiagnosticContext, xmlReader2, reportItemProperties))
						{
							this.AddErrorIfInRequiredNamespace(reportParsingDiagnosticContext, rectangleReader);
						}
					}
				}
				IL_009D:
				rectangleReader.Skip();
			}
			if (reportParsingDiagnosticContext.HasError)
			{
				return new FailedReportItem(text, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, reportParsingDiagnosticContext);
			}
			return new Rectangle(text, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, reportParsingDiagnosticContext, list);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00010DB8 File Offset: 0x0000EFB8
		private string ParseGaugeInputValueChild(ReportParsingDiagnosticContext diagnosticContext, XmlReader gaugeInputValueChild)
		{
			string empty = string.Empty;
			string localName = gaugeInputValueChild.LocalName;
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
			if (num <= 1681574380U)
			{
				if (num <= 1271803764U)
				{
					if (num != 484520218U)
					{
						if (num == 1271803764U)
						{
							if (localName == "DataElementName")
							{
								return empty;
							}
						}
					}
					else if (localName == "DataElementOutput")
					{
						return empty;
					}
				}
				else if (num != 1314593792U)
				{
					if (num == 1681574380U)
					{
						if (localName == "MinPercent")
						{
							return empty;
						}
					}
				}
				else if (localName == "AddConstant")
				{
					return empty;
				}
			}
			else if (num <= 2855835645U)
			{
				if (num != 2732365255U)
				{
					if (num == 2855835645U)
					{
						if (localName == "Formula")
						{
							return empty;
						}
					}
				}
				else if (localName == "PropertyCount")
				{
					return empty;
				}
			}
			else if (num != 2890758342U)
			{
				if (num != 3093150726U)
				{
					if (num == 3511155050U)
					{
						if (localName == "Value")
						{
							XElement xelement = XNode.ReadFrom(gaugeInputValueChild) as XElement;
							string value = xelement.Value;
							if (!string.IsNullOrEmpty(value))
							{
								return value;
							}
							this.AddErrorIfInRequiredNamespace(diagnosticContext, xelement.CreateReader());
							return empty;
						}
					}
				}
				else if (localName == "Multiplier")
				{
					return empty;
				}
			}
			else if (localName == "MaxPercent")
			{
				return empty;
			}
			this.AddErrorIfInRequiredNamespace(diagnosticContext, gaugeInputValueChild);
			return empty;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00010F30 File Offset: 0x0000F130
		internal void ParseStateIndicators(ReportParsingDiagnosticContext diagnosticContext, XmlReader stateIndicatorsReader, List<StateIndicator> stateIndicators)
		{
			string text = string.Empty;
			stateIndicatorsReader.Read();
			while (!stateIndicatorsReader.EOF)
			{
				if (stateIndicatorsReader.IsStartElement())
				{
					if (stateIndicatorsReader.LocalName == "StateIndicator")
					{
						using (XmlReader xmlReader = stateIndicatorsReader.ReadSubtreeAndMoveToContent())
						{
							xmlReader.Read();
							while (!xmlReader.EOF)
							{
								if (xmlReader.IsStartElement())
								{
									string localName = xmlReader.LocalName;
									uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
									if (num <= 1738891501U)
									{
										if (num <= 905944531U)
										{
											if (num != 168126768U)
											{
												if (num != 581018668U)
												{
													if (num == 905944531U)
													{
														if (localName == "IndicatorStyle")
														{
															goto IL_02BF;
														}
													}
												}
												else if (localName == "MinimumValue")
												{
													goto IL_02BF;
												}
											}
											else if (localName == "IndicatorStates")
											{
												goto IL_02BF;
											}
										}
										else if (num <= 1037600958U)
										{
											if (num != 969184184U)
											{
												if (num == 1037600958U)
												{
													if (localName == "ResizeMode")
													{
														goto IL_02BF;
													}
												}
											}
											else if (localName == "Angle")
											{
												goto IL_02BF;
											}
										}
										else if (num != 1125255534U)
										{
											if (num == 1738891501U)
											{
												if (localName == "GaugeInputValue")
												{
													using (XmlReader xmlReader2 = xmlReader.ReadSubtreeAndMoveToContent())
													{
														xmlReader2.Read();
														while (!xmlReader2.EOF)
														{
															if (xmlReader2.IsStartElement())
															{
																using (XmlReader xmlReader3 = xmlReader2.ReadSubtreeAndMoveToContent())
																{
																	text = this.ParseGaugeInputValueChild(diagnosticContext, xmlReader3);
																}
															}
															xmlReader2.Skip();
														}
														goto IL_02BF;
													}
												}
											}
										}
										else if (localName == "MaximumValue")
										{
											goto IL_02BF;
										}
									}
									else if (num <= 2399689012U)
									{
										if (num != 2167979815U)
										{
											if (num != 2172343663U)
											{
												if (num == 2399689012U)
												{
													if (localName == "TransformationScope")
													{
														goto IL_02BF;
													}
												}
											}
											else if (localName == "IconsSet")
											{
												goto IL_02BF;
											}
										}
										else if (localName == "StateDataElementOutput")
										{
											goto IL_02BF;
										}
									}
									else if (num <= 3605928275U)
									{
										if (num != 3209189450U)
										{
											if (num == 3605928275U)
											{
												if (localName == "IndicatorImage")
												{
													goto IL_02BF;
												}
											}
										}
										else if (localName == "ScaleFactor")
										{
											goto IL_02BF;
										}
									}
									else if (num != 3610219384U)
									{
										if (num == 3723981677U)
										{
											if (localName == "StateDataElementName")
											{
												goto IL_02BF;
											}
										}
									}
									else if (localName == "TransformationType")
									{
										goto IL_02BF;
									}
									this.AddErrorIfInRequiredNamespace(diagnosticContext, xmlReader);
								}
								IL_02BF:
								xmlReader.Skip();
							}
							StateIndicator stateIndicator = new StateIndicator(text);
							stateIndicators.Add(stateIndicator);
							goto IL_02F2;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, stateIndicatorsReader);
				}
				IL_02F2:
				stateIndicatorsReader.Skip();
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001128C File Offset: 0x0000F48C
		internal ReportItem ParseGaugePanel(XmlReader gaugePanelReader)
		{
			string text = this.ParseReportObjectName(gaugePanelReader);
			List<StateIndicator> list = new List<StateIndicator>();
			ReportParsingDiagnosticContext reportParsingDiagnosticContext = new ReportParsingDiagnosticContext();
			ReportItemProperties reportItemProperties = new ReportItemProperties();
			gaugePanelReader.Read();
			while (!gaugePanelReader.EOF)
			{
				if (gaugePanelReader.IsStartElement())
				{
					string localName = gaugePanelReader.LocalName;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num <= 2305863664U)
					{
						if (num <= 1350819706U)
						{
							if (num != 190151126U)
							{
								if (num != 407595678U)
								{
									if (num == 1350819706U)
									{
										if (localName == "GaugeImages")
										{
											goto IL_0285;
										}
									}
								}
								else if (localName == "NumericIndicators")
								{
									goto IL_0285;
								}
							}
							else if (localName == "ShadowIntensity")
							{
								goto IL_0285;
							}
						}
						else if (num <= 1562243321U)
						{
							if (num != 1416096886U)
							{
								if (num == 1562243321U)
								{
									if (localName == "TopImage")
									{
										goto IL_0285;
									}
								}
							}
							else if (localName == "Style")
							{
								goto IL_0285;
							}
						}
						else if (num != 1779946768U)
						{
							if (num == 2305863664U)
							{
								if (localName == "StateIndicators")
								{
									using (XmlReader xmlReader = gaugePanelReader.ReadSubtreeAndMoveToContent())
									{
										this.ParseStateIndicators(reportParsingDiagnosticContext, xmlReader, list);
										goto IL_0285;
									}
								}
							}
						}
						else if (localName == "AutoLayout")
						{
							goto IL_0285;
						}
					}
					else if (num <= 2853230279U)
					{
						if (num != 2473729136U)
						{
							if (num != 2485958535U)
							{
								if (num == 2853230279U)
								{
									if (localName == "TextAntiAliasingQuality")
									{
										goto IL_0285;
									}
								}
							}
							else if (localName == "GaugeLabels")
							{
								goto IL_0285;
							}
						}
						else if (localName == "LinearGauges")
						{
							goto IL_0285;
						}
					}
					else if (num <= 3383441858U)
					{
						if (num != 3290611874U)
						{
							if (num == 3383441858U)
							{
								if (localName == "GaugeMember")
								{
									goto IL_0285;
								}
							}
						}
						else if (localName == "RadialGauges")
						{
							goto IL_0285;
						}
					}
					else if (num != 3425124841U)
					{
						if (num == 3979303763U)
						{
							if (localName == "BackFrame")
							{
								goto IL_0285;
							}
						}
					}
					else if (localName == "AntiAliasing")
					{
						goto IL_0285;
					}
					using (XmlReader xmlReader2 = gaugePanelReader.ReadSubtreeAndMoveToContent())
					{
						this.ParseDataRegionProperty(xmlReader2, reportParsingDiagnosticContext, reportItemProperties);
					}
				}
				IL_0285:
				gaugePanelReader.Skip();
			}
			if (reportParsingDiagnosticContext.HasError)
			{
				return new FailedReportItem(text, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, reportParsingDiagnosticContext);
			}
			return new GaugePanel(text, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, reportParsingDiagnosticContext, list);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00011580 File Offset: 0x0000F780
		private ReportItem ParseChart(XmlReader chartReader)
		{
			string attribute = chartReader.GetAttribute("Name");
			chartReader.Read();
			Tuple<string, bool> tuple = new Tuple<string, bool>(string.Empty, false);
			string text = string.Empty;
			ChartCategoryHierarchy chartCategoryHierarchy = null;
			ChartSeriesHierarchy chartSeriesHierarchy = null;
			ChartData chartData = null;
			Tuple<ChartPositions, bool> tuple2 = new Tuple<ChartPositions, bool>(ChartPositions.RightTop, false);
			bool flag = false;
			ReportParsingDiagnosticContext reportParsingDiagnosticContext = new ReportParsingDiagnosticContext();
			int num = 0;
			bool flag2 = false;
			int num2 = 0;
			ReportItemProperties reportItemProperties = new ReportItemProperties();
			while (!chartReader.EOF)
			{
				if (chartReader.IsStartElement())
				{
					string localName = chartReader.LocalName;
					uint num3 = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num3 <= 2016693039U)
					{
						if (num3 <= 768445829U)
						{
							if (num3 > 124593386U)
							{
								if (num3 != 317674626U)
								{
									if (num3 != 768445829U)
									{
										goto IL_042E;
									}
									if (!(localName == "ChartSeriesHierarchy"))
									{
										goto IL_042E;
									}
								}
								else
								{
									if (!(localName == "ChartCategoryHierarchy"))
									{
										goto IL_042E;
									}
									using (XmlReader xmlReader = chartReader.ReadSubtreeAndMoveToContent())
									{
										chartCategoryHierarchy = this.ParseChartCategoryHierarchy(reportParsingDiagnosticContext, xmlReader);
										goto IL_0450;
									}
								}
								using (XmlReader xmlReader2 = chartReader.ReadSubtreeAndMoveToContent())
								{
									chartSeriesHierarchy = this.ParseChartSeriesHierarchy(reportParsingDiagnosticContext, xmlReader2);
									goto IL_0450;
								}
								goto IL_0360;
							}
							if (num3 != 2912339U)
							{
								if (num3 != 124593386U)
								{
									goto IL_042E;
								}
								if (!(localName == "DynamicWidth"))
								{
									goto IL_042E;
								}
								goto IL_0450;
							}
							else
							{
								if (!(localName == "ChartData"))
								{
									goto IL_042E;
								}
								using (XmlReader xmlReader3 = chartReader.ReadSubtreeAndMoveToContent())
								{
									chartData = this.ParseChartData(reportParsingDiagnosticContext, xmlReader3);
									goto IL_0450;
								}
								goto IL_03B5;
							}
						}
						else if (num3 <= 1303642257U)
						{
							if (num3 != 909965311U)
							{
								if (num3 != 1303642257U)
								{
									goto IL_042E;
								}
								if (!(localName == "ExperimentalFeaturesEnabled"))
								{
									goto IL_042E;
								}
								flag2 = chartReader.ReadElementContentAsString() == "true";
								continue;
							}
							else
							{
								if (!(localName == "ChartAreas"))
								{
									goto IL_042E;
								}
								goto IL_03DA;
							}
						}
						else if (num3 != 1326047838U)
						{
							if (num3 != 2016693039U)
							{
								goto IL_042E;
							}
							if (!(localName == "ChartCustomPaletteColors"))
							{
								goto IL_042E;
							}
							goto IL_0450;
						}
						else
						{
							if (!(localName == "FontSizeOffset"))
							{
								goto IL_042E;
							}
							goto IL_03FC;
						}
					}
					else if (num3 <= 2917912727U)
					{
						if (num3 <= 2114461760U)
						{
							if (num3 != 2090686075U)
							{
								if (num3 != 2114461760U)
								{
									goto IL_042E;
								}
								if (!(localName == "Palette"))
								{
									goto IL_042E;
								}
								goto IL_0450;
							}
							else
							{
								if (!(localName == "ChartLegends"))
								{
									goto IL_042E;
								}
								goto IL_03B5;
							}
						}
						else if (num3 != 2210097776U)
						{
							if (num3 != 2917912727U)
							{
								goto IL_042E;
							}
							if (!(localName == "NumberOfVisibleForecastPoints"))
							{
								goto IL_042E;
							}
							num2 = int.Parse(chartReader.ReadElementContentAsString());
							continue;
						}
						else
						{
							if (!(localName == "PaletteHatchBehavior"))
							{
								goto IL_042E;
							}
							goto IL_0450;
						}
					}
					else if (num3 <= 3066425391U)
					{
						if (num3 != 2935689264U)
						{
							if (num3 != 3066425391U)
							{
								goto IL_042E;
							}
							if (!(localName == "ChartNoDataMessage"))
							{
								goto IL_042E;
							}
							goto IL_0450;
						}
						else
						{
							if (!(localName == "ChartBorderSkin"))
							{
								goto IL_042E;
							}
							goto IL_0450;
						}
					}
					else if (num3 != 3505350856U)
					{
						if (num3 != 3901189736U)
						{
							if (num3 != 4048011013U)
							{
								goto IL_042E;
							}
							if (!(localName == "DynamicHeight"))
							{
								goto IL_042E;
							}
							goto IL_0450;
						}
						else
						{
							if (!(localName == "ChartTitles"))
							{
								goto IL_042E;
							}
							goto IL_0360;
						}
					}
					else if (!(localName == "DataSetName"))
					{
						goto IL_042E;
					}
					IL_0384:
					text = chartReader.ReadElementContentAsString();
					continue;
					IL_03FC:
					num = int.Parse(chartReader.ReadElementContentAsString());
					continue;
					IL_03DA:
					using (XmlReader xmlReader4 = chartReader.ReadSubtreeAndMoveToContent())
					{
						flag = this.ParseScalarXAxis(reportParsingDiagnosticContext, xmlReader4);
						goto IL_0450;
					}
					goto IL_03FC;
					IL_0360:
					using (XmlReader xmlReader5 = chartReader.ReadSubtreeAndMoveToContent())
					{
						tuple = this.ParseChartTitles(reportParsingDiagnosticContext, xmlReader5);
						goto IL_0450;
					}
					goto IL_0384;
					IL_03B5:
					using (XmlReader xmlReader6 = chartReader.ReadSubtreeAndMoveToContent())
					{
						tuple2 = this.ParseLegendPosition(reportParsingDiagnosticContext, xmlReader6);
						goto IL_0450;
					}
					goto IL_03DA;
					IL_042E:
					using (XmlReader xmlReader7 = chartReader.ReadSubtreeAndMoveToContent())
					{
						this.ParseDataRegionProperty(xmlReader7, reportParsingDiagnosticContext, reportItemProperties);
					}
				}
				IL_0450:
				chartReader.Skip();
			}
			if (reportParsingDiagnosticContext.HasError)
			{
				return new FailedReportItem(attribute, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, reportParsingDiagnosticContext);
			}
			return new Chart(attribute, this.ParseReportItemRect(reportItemProperties), reportItemProperties.ZIndex, reportParsingDiagnosticContext, tuple.Item1, tuple.Item2, text, chartCategoryHierarchy, chartSeriesHierarchy, chartData, tuple2.Item1, tuple2.Item2, flag, num, flag2, num2);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		private ReportImageSource? ParseImageSource(XmlReader sourceNode, ReportParsingDiagnosticContext diagnosticContext)
		{
			string text = sourceNode.ReadElementContentAsString();
			if (text != null)
			{
				if (text == "External")
				{
					return new ReportImageSource?(ReportImageSource.External);
				}
				if (text == "Embedded")
				{
					return new ReportImageSource?(ReportImageSource.Embedded);
				}
				if (text == "Database")
				{
					return new ReportImageSource?(ReportImageSource.Database);
				}
				diagnosticContext.AddError("InvalidReportImageSource", new string[0]);
			}
			else
			{
				diagnosticContext.AddError("UnexpectedRdlContent", new string[0]);
			}
			return null;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011B2C File Offset: 0x0000FD2C
		private bool TryParsePoints(string stringValue, out double points)
		{
			int num = stringValue.IndexOf("pt", StringComparison.Ordinal);
			if (num == -1)
			{
				points = 0.0;
				return false;
			}
			string text = stringValue.Substring(0, num);
			points = double.Parse(text);
			return true;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00011B6C File Offset: 0x0000FD6C
		private BackgroundImageRepeat? ParseBackgroundImageRepeat(XmlReader repeatNode, ReportParsingDiagnosticContext diagnosticContext)
		{
			string text = repeatNode.ReadElementContentAsString();
			if (string.IsNullOrEmpty(text))
			{
				diagnosticContext.AddError("UnexpectedRdlContent", new string[0]);
				return null;
			}
			if (text == "FitProportional")
			{
				return new BackgroundImageRepeat?(BackgroundImageRepeat.FitProportional);
			}
			if (text == "Fit")
			{
				return new BackgroundImageRepeat?(BackgroundImageRepeat.Fit);
			}
			if (text == "Repeat")
			{
				return new BackgroundImageRepeat?(BackgroundImageRepeat.Repeat);
			}
			if (!(text == "Clip"))
			{
				diagnosticContext.AddError("InvalidBackgroundImageRepeat", new string[0]);
				return null;
			}
			return new BackgroundImageRepeat?(BackgroundImageRepeat.Clip);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011C14 File Offset: 0x0000FE14
		internal Style ParseStyle(ReportParsingDiagnosticContext diagnosticContext, XmlReader styleReader)
		{
			styleReader.Read();
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string text6 = null;
			string text7 = null;
			string text8 = null;
			while (!styleReader.EOF)
			{
				if (styleReader.IsStartElement())
				{
					string localName = styleReader.LocalName;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num <= 2390696461U)
					{
						if (num <= 1049977134U)
						{
							if (num <= 648063511U)
							{
								if (num <= 212692823U)
								{
									if (num != 47094389U)
									{
										if (num == 212692823U)
										{
											if (localName == "Border")
											{
												goto IL_061F;
											}
										}
									}
									else if (localName == "BackgroundGradientEndColor")
									{
										goto IL_061F;
									}
								}
								else if (num != 436398163U)
								{
									if (num == 648063511U)
									{
										if (localName == "RightBorder")
										{
											goto IL_061F;
										}
									}
								}
								else if (localName == "NumeralLanguage")
								{
									goto IL_061F;
								}
							}
							else if (num <= 877888800U)
							{
								if (num != 777437884U)
								{
									if (num == 877888800U)
									{
										if (localName == "TextDecoration")
										{
											text5 = styleReader.ReadElementContentAsString();
											continue;
										}
									}
								}
								else if (localName == "PaddingRight")
								{
									goto IL_061F;
								}
							}
							else if (num != 940492723U)
							{
								if (num == 1049977134U)
								{
									if (localName == "LeftBorder")
									{
										goto IL_061F;
									}
								}
							}
							else if (localName == "PaddingLeft")
							{
								goto IL_061F;
							}
						}
						else if (num <= 1354541024U)
						{
							if (num <= 1124145470U)
							{
								if (num != 1079093535U)
								{
									if (num == 1124145470U)
									{
										if (localName == "ShadowOffset")
										{
											goto IL_061F;
										}
									}
								}
								else if (localName == "TextAlign")
								{
									text = styleReader.ReadElementContentAsString();
									continue;
								}
							}
							else if (num != 1340770632U)
							{
								if (num == 1354541024U)
								{
									if (localName == "VerticalAlign")
									{
										text2 = styleReader.ReadElementContentAsString();
										continue;
									}
								}
							}
							else if (localName == "WritingMode")
							{
								goto IL_061F;
							}
						}
						else if (num <= 1644100618U)
						{
							if (num != 1356396181U)
							{
								if (num == 1644100618U)
								{
									if (localName == "Direction")
									{
										goto IL_061F;
									}
								}
							}
							else if (localName == "BackgroundGradientType")
							{
								goto IL_061F;
							}
						}
						else if (num != 1740465153U)
						{
							if (num != 1813401013U)
							{
								if (num == 2390696461U)
								{
									if (localName == "PaddingBottom")
									{
										goto IL_061F;
									}
								}
							}
							else if (localName == "FontStyle")
							{
								text6 = styleReader.ReadElementContentAsString();
								continue;
							}
						}
						else if (localName == "PaddingTop")
						{
							goto IL_061F;
						}
					}
					else if (num <= 3239417872U)
					{
						if (num <= 2724873441U)
						{
							if (num <= 2591284123U)
							{
								if (num != 2551695987U)
								{
									if (num == 2591284123U)
									{
										if (localName == "Language")
										{
											goto IL_061F;
										}
									}
								}
								else if (localName == "TextEffect")
								{
									goto IL_061F;
								}
							}
							else if (num != 2656199999U)
							{
								if (num == 2724873441U)
								{
									if (localName == "FontSize")
									{
										text3 = styleReader.ReadElementContentAsString();
										continue;
									}
								}
							}
							else if (localName == "Calendar")
							{
								goto IL_061F;
							}
						}
						else if (num <= 2890288545U)
						{
							if (num != 2733805326U)
							{
								if (num == 2890288545U)
								{
									if (localName == "BackgroundHatchType")
									{
										goto IL_061F;
									}
								}
							}
							else if (localName == "NumeralVariant")
							{
								goto IL_061F;
							}
						}
						else if (num != 3037753912U)
						{
							if (num == 3239417872U)
							{
								if (localName == "BottomBorder")
								{
									goto IL_061F;
								}
							}
						}
						else if (localName == "LineHeight")
						{
							goto IL_061F;
						}
					}
					else if (num <= 3853794552U)
					{
						if (num <= 3511250224U)
						{
							if (num != 3496045264U)
							{
								if (num == 3511250224U)
								{
									if (localName == "BackgroundColor")
									{
										goto IL_061F;
									}
								}
							}
							else if (localName == "FontWeight")
							{
								text4 = styleReader.ReadElementContentAsString();
								continue;
							}
						}
						else if (num != 3770400898U)
						{
							if (num == 3853794552U)
							{
								if (localName == "Color")
								{
									goto IL_061F;
								}
							}
						}
						else if (localName == "BackgroundImage")
						{
							goto IL_061F;
						}
					}
					else if (num <= 4061492226U)
					{
						if (num != 4015975666U)
						{
							if (num == 4061492226U)
							{
								if (localName == "FormatType")
								{
									goto IL_061F;
								}
							}
						}
						else if (localName == "TopBorder")
						{
							goto IL_061F;
						}
					}
					else if (num != 4130445440U)
					{
						if (num != 4210400900U)
						{
							if (num == 4289789810U)
							{
								if (localName == "Format")
								{
									goto IL_061F;
								}
							}
						}
						else if (localName == "ShadowColor")
						{
							goto IL_061F;
						}
					}
					else if (localName == "FontFamily")
					{
						string namespaceURI = styleReader.NamespaceURI;
						if ("http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition" == namespaceURI)
						{
							text7 = styleReader.ReadElementContentAsString();
							continue;
						}
						if ("http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition" == namespaceURI)
						{
							text8 = styleReader.ReadElementContentAsString();
							continue;
						}
						this.AddErrorIfInRequiredNamespace(diagnosticContext, styleReader);
						goto IL_061F;
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, styleReader);
				}
				IL_061F:
				styleReader.Skip();
			}
			if (text7 == null && text8 != null)
			{
				text7 = text8;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(text))
			{
				dictionary["TextAlignment"] = text;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				dictionary["TextVerticalAlignment"] = text2;
			}
			double num2;
			if (!string.IsNullOrEmpty(text3) && this.TryParsePoints(text3, out num2))
			{
				dictionary["FontSize"] = new FontSize((float)num2, FontSizeUnit.Points).ToString();
			}
			if (!string.IsNullOrEmpty(text4))
			{
				dictionary["FontWeight"] = text4;
			}
			if (!string.IsNullOrEmpty(text5))
			{
				dictionary["TextDecoration"] = text5;
			}
			if (!string.IsNullOrEmpty(text6))
			{
				dictionary["FontStyle"] = text6;
			}
			if (!string.IsNullOrEmpty(text7))
			{
				dictionary["FontFamily"] = text7;
			}
			return new Style(dictionary);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001231C File Offset: 0x0001051C
		internal ReportSection ParseReportSection(ReportParsingDiagnosticContext diagnosticContext, XmlReader sectionReader)
		{
			string attribute = sectionReader.GetAttribute("Name", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition");
			sectionReader.Read();
			Style style = null;
			List<ReportItem> list = new List<ReportItem>();
			ReportSize reportSize = new ReportSize();
			ReportSize reportSize2 = new ReportSize();
			BackgroundImage backgroundImage = null;
			string text = string.Empty;
			while (!sectionReader.EOF)
			{
				if (sectionReader.IsStartElement())
				{
					string localName = sectionReader.LocalName;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num > 994238399U)
					{
						if (num <= 1170697275U)
						{
							if (num != 1116344469U)
							{
								if (num != 1170697275U)
								{
									goto IL_01AC;
								}
								if (!(localName == "DataSourceName"))
								{
									goto IL_01AC;
								}
							}
							else
							{
								if (!(localName == "Body"))
								{
									goto IL_01AC;
								}
								using (XmlReader xmlReader = sectionReader.ReadSubtreeAndMoveToContent())
								{
									list = this.ParseReportSectionBody(diagnosticContext, xmlReader, reportSize, style);
									goto IL_01B4;
								}
								goto IL_016B;
							}
						}
						else if (num != 1271803764U)
						{
							if (num != 3896349078U)
							{
								goto IL_01AC;
							}
							if (!(localName == "Page"))
							{
								goto IL_01AC;
							}
							using (XmlReader xmlReader2 = sectionReader.ReadSubtreeAndMoveToContent())
							{
								this.ParsePage(diagnosticContext, xmlReader2);
								goto IL_01B4;
							}
						}
						else
						{
							if (!(localName == "DataElementName"))
							{
								goto IL_01AC;
							}
							goto IL_01B4;
						}
						text = sectionReader.ReadElementContentAsString();
						continue;
					}
					if (num != 405389359U)
					{
						if (num != 484520218U)
						{
							if (num != 994238399U)
							{
								goto IL_01AC;
							}
							if (!(localName == "Width"))
							{
								goto IL_01AC;
							}
						}
						else
						{
							if (!(localName == "DataElementOutput"))
							{
								goto IL_01AC;
							}
							goto IL_01B4;
						}
					}
					else
					{
						if (!(localName == "PreviewImageRelationshipId"))
						{
							goto IL_01AC;
						}
						goto IL_01B4;
					}
					IL_016B:
					ReportSize reportSize3 = ReportSize.CreateReportSize(sectionReader.ReadElementContentAsString());
					if (reportSize3 != null)
					{
						reportSize2 = reportSize3;
						continue;
					}
					continue;
					IL_01AC:
					this.AddErrorIfInRequiredNamespace(diagnosticContext, sectionReader);
				}
				IL_01B4:
				sectionReader.Skip();
			}
			return new ReportSection(attribute, text, list, reportSize2, reportSize, style, backgroundImage);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001251C File Offset: 0x0001071C
		internal ReportItem ParseReportItem(XmlReader reportItemReader)
		{
			string localName = reportItemReader.LocalName;
			if (localName == "Chart")
			{
				return this.ParseChart(reportItemReader);
			}
			if (localName == "Tablix")
			{
				return this.ParseTablix(reportItemReader);
			}
			if (localName == "Textbox")
			{
				return this.ParseTextbox(reportItemReader);
			}
			if (localName == "Image")
			{
				return this.ParseImage(reportItemReader);
			}
			if (localName == "Rectangle")
			{
				return this.ParseRectangle(reportItemReader);
			}
			if (!(localName == "GaugePanel"))
			{
				ReportParsingDiagnosticContext reportParsingDiagnosticContext = new ReportParsingDiagnosticContext();
				reportParsingDiagnosticContext.AddError("InvalidReportItemType", new string[0]);
				return this.CreateFailedReportItem(reportItemReader, reportParsingDiagnosticContext, null, localName);
			}
			return this.ParseGaugePanel(reportItemReader);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000125D4 File Offset: 0x000107D4
		internal void AddErrorIfInRequiredNamespace(ReportParsingDiagnosticContext diagnosticContext, XmlReader xmlNode)
		{
			if (xmlNode.IsStartElement() && !string.IsNullOrEmpty(xmlNode.LocalName))
			{
				if (this.InRequiredNamespace(xmlNode))
				{
					Contract.Check(diagnosticContext != null, "Expecting a diagnostic context");
					diagnosticContext.AddError("UnexpectedRdlContent", new string[0]);
				}
				if (this.InRequiredNamespace(xmlNode) || xmlNode.NamespaceURI == "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")
				{
					this._unknownElements.Add(string.Concat(new string[] { "{namespace:", xmlNode.NamespaceURI, ";name:", xmlNode.Name, "}" }));
				}
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00012680 File Offset: 0x00010880
		internal DataSource ParseDataSource(ReportParsingDiagnosticContext diagnosticContext, XmlReader dataSourceReader)
		{
			string text = this.ParseReportObjectName(dataSourceReader);
			dataSourceReader.Read();
			string text2 = string.Empty;
			while (!dataSourceReader.EOF)
			{
				if (dataSourceReader.IsStartElement())
				{
					if (dataSourceReader.LocalName == "DataSourceReference")
					{
						text2 = dataSourceReader.ReadElementContentAsString();
						continue;
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, dataSourceReader);
				}
				dataSourceReader.Read();
			}
			return new DataSource(text, text2);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000126E8 File Offset: 0x000108E8
		internal void ParseDataSources(ReportParsingDiagnosticContext diagnosticContext, XmlReader dataSourcesReader, List<DataSource> dataSources)
		{
			Contract.Check(dataSources != null, "Expecting dataSources to not be null");
			dataSourcesReader.Read();
			while (!dataSourcesReader.EOF)
			{
				if (dataSourcesReader.IsStartElement())
				{
					if (dataSourcesReader.LocalName == "DataSource")
					{
						using (dataSourcesReader.ReadSubtreeAndMoveToContent())
						{
							dataSources.Add(this.ParseDataSource(diagnosticContext, dataSourcesReader));
							goto IL_005A;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, dataSourcesReader);
				}
				IL_005A:
				dataSourcesReader.Skip();
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00012770 File Offset: 0x00010970
		internal Tuple<Query, QueryDesignState> ParseQuery(ReportParsingDiagnosticContext diagnosticContext, XmlReader queryReader)
		{
			queryReader.Read();
			string text = null;
			QueryDesignState queryDesignState = null;
			while (!queryReader.EOF)
			{
				if (queryReader.IsStartElement())
				{
					string localName = queryReader.LocalName;
					if (localName == "DataSourceName")
					{
						text = queryReader.ReadElementContentAsString();
						continue;
					}
					if (!(localName == "DesignerState"))
					{
						if (localName == "CommandText")
						{
							goto IL_007A;
						}
					}
					else
					{
						using (XmlReader xmlReader = queryReader.ReadSubtreeAndMoveToContent())
						{
							queryDesignState = QueryDesignStateParser.ParseDesignerState(XNode.ReadFrom(xmlReader) as XElement);
							goto IL_007A;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, queryReader);
				}
				IL_007A:
				queryReader.Skip();
			}
			Contract.Check(text != null, "Expect dataSourceName to not be null");
			return new Tuple<Query, QueryDesignState>(new Query(text), queryDesignState);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00012830 File Offset: 0x00010A30
		internal Field ParseField(ReportParsingDiagnosticContext diagnosticContext, XElement fieldElement, QueryDesignState queryDesignState)
		{
			string value = fieldElement.AttributeByLocalName("Name").Value;
			IRdmQueryExpression rdmQueryExpression = null;
			bool? flag = null;
			string[] validFieldChildren = new string[] { "QueryReference", "DataField", "AggregateIndicatorField", "Value" };
			XElement xelement = fieldElement.ElementByLocalName("QueryReference");
			IEnumerable<XElement> enumerable = fieldElement.Elements();
			Func<XElement, bool> <>9__0;
			Func<XElement, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (XElement child) => !validFieldChildren.Contains(child.Name.LocalName));
			}
			foreach (XElement xelement2 in enumerable.Where(func))
			{
				this.AddErrorIfInRequiredNamespace(diagnosticContext, xelement2.CreateReader());
			}
			if (xelement != null)
			{
				string[] validQueryRefChildren = new string[] { "Expression", "OuterJoin", "CalculateInMeasureContext" };
				XElement xelement3 = xelement.ElementByLocalName("Expression");
				if (xelement3 != null)
				{
					XElement xelement4 = xelement3.ElementByLocalName("QueryExpression");
					if (xelement4 != null)
					{
						try
						{
							rdmQueryExpression = RdmQueryExpressionParser.ParseQueryExpression(xelement4);
						}
						catch (ArgumentException)
						{
							rdmQueryExpression = null;
						}
					}
					foreach (XElement xelement5 in from child in xelement3.Elements()
						where child.Name.LocalName != "QueryExpression"
						select child)
					{
						this.AddErrorIfInRequiredNamespace(diagnosticContext, xelement5.CreateReader());
					}
				}
				XElement xelement6 = xelement.ElementByLocalName("OuterJoin");
				if (xelement6 != null)
				{
					flag = new bool?(xelement6.Value == "true");
				}
				IEnumerable<XElement> enumerable2 = xelement.Elements();
				Func<XElement, bool> func2;
				Func<XElement, bool> <>9__2;
				if ((func2 = <>9__2) == null)
				{
					func2 = (<>9__2 = (XElement child) => !validQueryRefChildren.Contains(child.Name.LocalName));
				}
				using (IEnumerator<XElement> enumerator = enumerable2.Where(func2).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						XElement xelement7 = enumerator.Current;
						this.AddErrorIfInRequiredNamespace(diagnosticContext, xelement7.CreateReader());
					}
					goto IL_0224;
				}
			}
			if (queryDesignState != null)
			{
				rdmQueryExpression = queryDesignState.FindByName(value);
			}
			IL_0224:
			return new Field(value, rdmQueryExpression, flag);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00012AA0 File Offset: 0x00010CA0
		internal List<Field> ParseFields(ReportParsingDiagnosticContext diagnosticContext, XElement fieldsElement, QueryDesignState queryDesignState)
		{
			List<Field> list = (from field in fieldsElement.ElementsByLocalName("Field")
				select this.ParseField(diagnosticContext, field, queryDesignState)).ToList<Field>();
			foreach (XElement xelement in from child in fieldsElement.Elements()
				where child.Name.LocalName != "Field"
				select child)
			{
				this.AddErrorIfInRequiredNamespace(diagnosticContext, xelement.CreateReader());
			}
			return list;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00012B5C File Offset: 0x00010D5C
		internal DataSet ParseDataSet(ReportParsingDiagnosticContext diagnosticContext, XmlReader dataSetReader)
		{
			string text = this.ParseReportObjectName(dataSetReader);
			dataSetReader.Read();
			Tuple<Query, QueryDesignState> tuple = new Tuple<Query, QueryDesignState>(null, null);
			List<Field> list = new List<Field>();
			XElement xelement = null;
			while (!dataSetReader.EOF)
			{
				if (dataSetReader.IsStartElement())
				{
					string localName = dataSetReader.LocalName;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num <= 2025479575U)
					{
						if (num <= 160276536U)
						{
							if (num != 157909182U)
							{
								if (num != 160276536U)
								{
									goto IL_01C7;
								}
								if (!(localName == "CaseSensitivity"))
								{
									goto IL_01C7;
								}
								goto IL_01CF;
							}
							else
							{
								if (!(localName == "NullsAsBlanks"))
								{
									goto IL_01C7;
								}
								goto IL_01CF;
							}
						}
						else if (num != 423706510U)
						{
							if (num != 856210844U)
							{
								if (num != 2025479575U)
								{
									goto IL_01C7;
								}
								if (!(localName == "InterpretSubtotalsAsDetails"))
								{
									goto IL_01C7;
								}
								goto IL_01CF;
							}
							else if (!(localName == "Fields"))
							{
								goto IL_01C7;
							}
						}
						else
						{
							if (!(localName == "CollationCulture"))
							{
								goto IL_01C7;
							}
							goto IL_01CF;
						}
					}
					else if (num <= 2671338447U)
					{
						if (num != 2659837827U)
						{
							if (num != 2671338447U)
							{
								goto IL_01C7;
							}
							if (!(localName == "KanatypeSensitivity"))
							{
								goto IL_01C7;
							}
							goto IL_01CF;
						}
						else
						{
							if (!(localName == "Query"))
							{
								goto IL_01C7;
							}
							using (XmlReader xmlReader = dataSetReader.ReadSubtreeAndMoveToContent())
							{
								tuple = this.ParseQuery(diagnosticContext, xmlReader);
								goto IL_01CF;
							}
						}
					}
					else if (num != 2673203917U)
					{
						if (num != 2683052490U)
						{
							if (num != 3238662882U)
							{
								goto IL_01C7;
							}
							if (!(localName == "AccentSensitivity"))
							{
								goto IL_01C7;
							}
							goto IL_01CF;
						}
						else
						{
							if (!(localName == "WidthSensitivity"))
							{
								goto IL_01C7;
							}
							goto IL_01CF;
						}
					}
					else
					{
						if (!(localName == "DefaultRelationships"))
						{
							goto IL_01C7;
						}
						goto IL_01CF;
					}
					xelement = XNode.ReadFrom(dataSetReader) as XElement;
					goto IL_01CF;
					IL_01C7:
					this.AddErrorIfInRequiredNamespace(diagnosticContext, dataSetReader);
				}
				IL_01CF:
				dataSetReader.Skip();
			}
			if (xelement != null)
			{
				list = this.ParseFields(diagnosticContext, xelement, tuple.Item2);
			}
			return new DataSet(text, tuple.Item1, list);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00012D78 File Offset: 0x00010F78
		internal void ParseDataSets(ReportParsingDiagnosticContext diagnosticContext, XmlReader dataSetsReader, List<DataSet> dataSets)
		{
			dataSetsReader.Read();
			while (!dataSetsReader.EOF)
			{
				if (dataSetsReader.IsStartElement())
				{
					if (dataSetsReader.LocalName == "DataSet")
					{
						using (XmlReader xmlReader = dataSetsReader.ReadSubtreeAndMoveToContent())
						{
							dataSets.Add(this.ParseDataSet(diagnosticContext, xmlReader));
							goto IL_004C;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, dataSetsReader);
				}
				IL_004C:
				dataSetsReader.Skip();
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00012DF0 File Offset: 0x00010FF0
		internal MapBackdropType ParseMapBackdropType(string backdropString)
		{
			MapBackdropType mapBackdropType = MapBackdropType.Road;
			backdropString = backdropString.ToLowerInvariant();
			if (backdropString == "road")
			{
				mapBackdropType = MapBackdropType.Road;
			}
			else if (backdropString == "grayscaleroad")
			{
				mapBackdropType = MapBackdropType.GrayscaleRoad;
			}
			else if (backdropString == "invertedgrayscaleroad")
			{
				mapBackdropType = MapBackdropType.InvertedGrayscaleRoad;
			}
			else if (backdropString == "aerial")
			{
				mapBackdropType = MapBackdropType.Aerial;
			}
			else if (backdropString == "grayscaleaerial")
			{
				mapBackdropType = MapBackdropType.GrayscaleAerial;
			}
			else if (backdropString == "default")
			{
				mapBackdropType = MapBackdropType.Road;
			}
			else
			{
				Contract.Check(false, "Invalid map backdrop revieved.");
			}
			return mapBackdropType;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00012E7C File Offset: 0x0001107C
		private List<ReportItem> ParseReportSectionBody(ReportParsingDiagnosticContext diagnosticContext, XmlReader sectionBodyReader, ReportSize height, Style style)
		{
			List<ReportItem> list = new List<ReportItem>();
			sectionBodyReader.Read();
			while (!sectionBodyReader.EOF)
			{
				if (sectionBodyReader.IsStartElement())
				{
					string localName = sectionBodyReader.LocalName;
					if (!(localName == "ReportItems"))
					{
						if (!(localName == "Height"))
						{
							if (localName == "Style")
							{
								using (sectionBodyReader.ReadSubtreeAndMoveToContent())
								{
									style = this.ParseStyle(diagnosticContext, sectionBodyReader);
									goto IL_00A4;
								}
							}
							this.AddErrorIfInRequiredNamespace(diagnosticContext, sectionBodyReader);
							goto IL_00A4;
						}
					}
					else
					{
						using (XmlReader xmlReader2 = sectionBodyReader.ReadSubtreeAndMoveToContent())
						{
							list = this.ParseReportItems(xmlReader2);
							goto IL_00A4;
						}
					}
					ReportSize reportSize = ReportSize.CreateReportSize(sectionBodyReader.ReadElementContentAsString());
					if (reportSize != null)
					{
						continue;
					}
					continue;
				}
				IL_00A4:
				sectionBodyReader.Skip();
			}
			return list;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00012F5C File Offset: 0x0001115C
		private BackgroundImage ParsePage(ReportParsingDiagnosticContext diagnosticContext, XmlReader pageReader)
		{
			pageReader.Read();
			BackgroundImage backgroundImage = null;
			while (pageReader.EOF)
			{
				if (pageReader.IsStartElement())
				{
					string localName = pageReader.LocalName;
					if (!(localName == "Style"))
					{
						if (localName == "LeftMargin" || localName == "RightMargin" || localName == "TopMargin" || localName == "BottomMargin")
						{
							goto IL_0081;
						}
					}
					else
					{
						using (XmlReader xmlReader = pageReader.ReadSubtreeAndMoveToContent())
						{
							backgroundImage = this.ParsePageStyle(diagnosticContext, xmlReader);
							goto IL_0081;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, pageReader);
				}
				IL_0081:
				pageReader.Skip();
			}
			return backgroundImage;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001300C File Offset: 0x0001120C
		private BackgroundImage ParsePageStyle(ReportParsingDiagnosticContext diagnosticContext, XmlReader pageStyleReader)
		{
			pageStyleReader.Read();
			BackgroundImage backgroundImage = null;
			while (!pageStyleReader.EOF)
			{
				if (pageStyleReader.IsStartElement())
				{
					string localName = pageStyleReader.LocalName;
					if (!(localName == "BackgroundImage"))
					{
						if (localName == "BackgroundColor")
						{
							goto IL_005A;
						}
					}
					else
					{
						using (XmlReader xmlReader = pageStyleReader.ReadSubtreeAndMoveToContent())
						{
							backgroundImage = this.ParseBackgroundImage(xmlReader, diagnosticContext);
							goto IL_005A;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, pageStyleReader);
				}
				IL_005A:
				pageStyleReader.Skip();
			}
			return backgroundImage;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00013094 File Offset: 0x00011294
		private List<ReportItem> ParseReportItems(XmlReader reportItemsReader)
		{
			reportItemsReader.Read();
			List<ReportItem> list = new List<ReportItem>();
			while (!reportItemsReader.EOF)
			{
				if (reportItemsReader.IsStartElement() && !string.IsNullOrEmpty(reportItemsReader.LocalName))
				{
					using (XmlReader xmlReader = reportItemsReader.ReadSubtreeAndMoveToContent())
					{
						list.Add(this.ParseReportItem(xmlReader));
					}
				}
				reportItemsReader.Skip();
			}
			return list;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00013104 File Offset: 0x00011304
		private RdmReport InternalParse(Stream reportStream)
		{
			ReportParsingDiagnosticContext reportParsingDiagnosticContext = new ReportParsingDiagnosticContext();
			List<DataSet> list = new List<DataSet>();
			List<DataSource> list2 = new List<DataSource>();
			List<ReportSection> list3 = new List<ReportSection>();
			XmlReaderSettings xmlReaderSettings = XmlUtil.CreateSafeXmlReaderSettings();
			xmlReaderSettings.IgnoreWhitespace = true;
			using (XmlReader xmlReader = XmlReader.Create(reportStream, xmlReaderSettings))
			{
				xmlReader.MoveToContent();
				xmlReader.Read();
				while (!xmlReader.EOF)
				{
					if (xmlReader.IsStartElement())
					{
						string localName = xmlReader.LocalName;
						if (!(localName == "DataSources"))
						{
							if (!(localName == "DataSets"))
							{
								if (localName == "ReportSections")
								{
									goto IL_00F6;
								}
								if (!(localName == "AutoRefresh") && !(localName == "SavePreviewImages") && !(localName == "Compatibility"))
								{
									goto IL_0117;
								}
								goto IL_0117;
							}
						}
						else
						{
							using (XmlReader xmlReader2 = xmlReader.ReadSubtreeAndMoveToContent())
							{
								this.ParseDataSources(reportParsingDiagnosticContext, xmlReader2, list2);
								goto IL_0117;
							}
						}
						using (XmlReader xmlReader3 = xmlReader.ReadSubtreeAndMoveToContent())
						{
							this.ParseDataSets(reportParsingDiagnosticContext, xmlReader3, list);
							goto IL_0117;
						}
						IL_00F6:
						using (XmlReader xmlReader4 = xmlReader.ReadSubtreeAndMoveToContent())
						{
							this.ParseReportSections(reportParsingDiagnosticContext, xmlReader4, list3);
						}
					}
					IL_0117:
					xmlReader.Skip();
				}
			}
			return new RdmReport(list3, list, list2);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00013288 File Offset: 0x00011488
		private string ParseReportObjectName(XmlReader node)
		{
			return node.GetAttribute("Name");
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00013298 File Offset: 0x00011498
		private SortExpression ParseSortExpression(ReportParsingDiagnosticContext diagnosticContext, XmlReader sortExpressionReader)
		{
			sortExpressionReader.Read();
			Expression expression = null;
			bool flag = false;
			SortDirection sortDirection = SortDirection.Ascending;
			while (!sortExpressionReader.EOF)
			{
				if (sortExpressionReader.IsStartElement())
				{
					string localName = sortExpressionReader.LocalName;
					if (localName == "Value")
					{
						expression = this.ParseExpressionValueNode(sortExpressionReader);
						continue;
					}
					if (localName == "AutoGenerated")
					{
						flag = sortExpressionReader.ReadElementContentAsString().ToLowerInvariant() == "true";
						continue;
					}
					if (!(localName == "Direction"))
					{
						if (!(localName == "NaturalSort") && !(localName == "DeferredSort"))
						{
							this.AddErrorIfInRequiredNamespace(diagnosticContext, sortExpressionReader);
						}
					}
					else
					{
						sortDirection = SortDirection.Descending;
					}
				}
				sortExpressionReader.Skip();
			}
			return new SortExpression(expression, flag, sortDirection);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00013350 File Offset: 0x00011550
		private Expression ParseExpressionValueNode(XmlReader nodeWithExpressionReader)
		{
			if (nodeWithExpressionReader == null)
			{
				return null;
			}
			string text = nodeWithExpressionReader.ReadElementContentAsString();
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new Expression(text);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001337C File Offset: 0x0001157C
		private List<ChartMember> ParseChartMembers(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartMembersReader)
		{
			chartMembersReader.Read();
			List<ChartMember> list = new List<ChartMember>();
			while (!chartMembersReader.EOF)
			{
				if (chartMembersReader.IsStartElement())
				{
					if (chartMembersReader.LocalName == "ChartMember")
					{
						using (XmlReader xmlReader = chartMembersReader.ReadSubtreeAndMoveToContent())
						{
							ChartMember chartMember = this.ParseChartMember(diagnosticContext, xmlReader);
							list.Add(chartMember);
							goto IL_0054;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, chartMembersReader);
				}
				IL_0054:
				chartMembersReader.Skip();
			}
			return list;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000133FC File Offset: 0x000115FC
		private ChartSeriesHierarchy ParseChartSeriesHierarchy(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartSeriesHierarchyReader)
		{
			chartSeriesHierarchyReader.Read();
			List<ChartMember> list = new List<ChartMember>();
			while (!chartSeriesHierarchyReader.EOF)
			{
				if (chartSeriesHierarchyReader.IsStartElement())
				{
					if (chartSeriesHierarchyReader.LocalName == "ChartMembers")
					{
						using (XmlReader xmlReader = chartSeriesHierarchyReader.ReadSubtreeAndMoveToContent())
						{
							list = this.ParseChartMembers(diagnosticContext, xmlReader);
							break;
						}
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, chartSeriesHierarchyReader);
				}
				chartSeriesHierarchyReader.Skip();
			}
			if (list.IsEmpty<ChartMember>())
			{
				list.Add(new ChartMember(null, null, null, null));
			}
			return new ChartSeriesHierarchy(list);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00013494 File Offset: 0x00011694
		private void ParseChartDataLabel(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartDataLabelReader, ref bool isLabelsVisible, ref ChartDataLabelPositions labelsPosition)
		{
			chartDataLabelReader.Read();
			isLabelsVisible = false;
			labelsPosition = ChartDataLabelPositions.Auto;
			while (!chartDataLabelReader.EOF)
			{
				if (chartDataLabelReader.IsStartElement())
				{
					if (chartDataLabelReader.LocalName == "Visible")
					{
						isLabelsVisible = chartDataLabelReader.ReadElementContentAsString().ToLowerInvariant() == "true";
						continue;
					}
					if (chartDataLabelReader.LocalName == "Position")
					{
						string text = chartDataLabelReader.ReadElementContentAsString();
						labelsPosition = ((!string.IsNullOrEmpty(text)) ? this.ParseChartDataLabelsPositions(text) : labelsPosition);
						continue;
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, chartDataLabelReader);
				}
				chartDataLabelReader.Skip();
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001352C File Offset: 0x0001172C
		private bool ParseChartCategoryAxes(ReportParsingDiagnosticContext diagnosticContext, XmlReader chartCategoryAxes)
		{
			chartCategoryAxes.Read();
			bool flag = false;
			bool flag2 = false;
			while (!chartCategoryAxes.EOF)
			{
				if (!chartCategoryAxes.IsStartElement("ChartAxis"))
				{
					goto IL_0052;
				}
				if (chartCategoryAxes.GetAttribute("Name") == "Primary" && !flag)
				{
					flag = true;
					using (XmlReader xmlReader = chartCategoryAxes.ReadSubtreeAndMoveToContent())
					{
						flag2 = this.ParseScalarXAxisFromAxis(diagnosticContext, xmlReader);
						goto IL_0062;
					}
					goto IL_0052;
				}
				IL_0062:
				chartCategoryAxes.Skip();
				continue;
				IL_0052:
				if (chartCategoryAxes.IsStartElement())
				{
					this.AddErrorIfInRequiredNamespace(diagnosticContext, chartCategoryAxes);
					goto IL_0062;
				}
				goto IL_0062;
			}
			return flag2;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000135BC File Offset: 0x000117BC
		private Size ParseSize(ReportParsingDiagnosticContext diagnosticContext, XmlReader sizeReader)
		{
			sizeReader.Read();
			ReportSize reportSize = new ReportSize();
			ReportSize reportSize2 = new ReportSize();
			ReportSize reportSize3 = new ReportSize();
			ReportSize reportSize4 = new ReportSize();
			ReportSize reportSize5 = new ReportSize();
			ReportSize reportSize6 = new ReportSize();
			while (!sizeReader.EOF)
			{
				if (sizeReader.IsStartElement())
				{
					string localName = sizeReader.LocalName;
					if (localName == "Width")
					{
						reportSize = ReportSize.ParseReportSize(sizeReader);
						continue;
					}
					if (localName == "MinWidth")
					{
						reportSize2 = ReportSize.ParseReportSize(sizeReader);
						continue;
					}
					if (localName == "MaxWidth")
					{
						reportSize3 = ReportSize.ParseReportSize(sizeReader);
						continue;
					}
					if (localName == "Height")
					{
						reportSize4 = ReportSize.ParseReportSize(sizeReader);
						continue;
					}
					if (localName == "MinHeight")
					{
						reportSize5 = ReportSize.ParseReportSize(sizeReader);
						continue;
					}
					if (localName == "MaxHeight")
					{
						reportSize6 = ReportSize.ParseReportSize(sizeReader);
						continue;
					}
					this.AddErrorIfInRequiredNamespace(diagnosticContext, sizeReader);
				}
				sizeReader.Skip();
			}
			return new Size(reportSize, reportSize2, reportSize3, reportSize4, reportSize5, reportSize6);
		}

		// Token: 0x04000231 RID: 561
		private List<string> _unknownElements;
	}
}
