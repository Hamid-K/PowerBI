using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.PrimitiveValues;
using Microsoft.InfoNav.Explore.VisualContracts;
using Microsoft.PowerBI.ExplorationContracts;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000028 RID: 40
	internal static class PVDocumentToExplorationConverter
	{
		// Token: 0x06000100 RID: 256 RVA: 0x0000405C File Offset: 0x0000225C
		internal static Exploration Convert(PVDocument document, ExploreConverterContext converterContext)
		{
			List<Section> list = new List<Section>();
			PVVisual rootVisual = document.RootVisual;
			string text = null;
			if (rootVisual != null)
			{
				Theme theme = null;
				if (rootVisual.LayoutContext != null)
				{
					text = rootVisual.LayoutContext.Theme;
					if (text != null)
					{
						theme = PVDocumentToExplorationConverter.GetThemeManager().GetTheme(text);
					}
				}
				if (rootVisual.Visuals != null && rootVisual.Visuals.Count > 0)
				{
					PVDocumentContext context = document.Context;
					List<PVVisual> visuals = rootVisual.Visuals[0].Visuals;
					for (int i = 0; i < visuals.Count; i++)
					{
						PVVisual pvvisual = visuals[i];
						Section section = PVDocumentToExplorationConverter.ConvertPage(context, pvvisual, i, converterContext, theme);
						list.Add(section);
					}
				}
			}
			return new Exploration
			{
				Sections = list,
				Theme = text
			};
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004120 File Offset: 0x00002320
		internal static Section ConvertPage(PVDocumentContext context, PVVisual slideVisual, int pageIndex, ExploreConverterContext converterContext, Theme theme = null)
		{
			List<VisualContainer> list = new List<VisualContainer>();
			List<PVVisual> list2 = slideVisual.Visuals[0].Visuals ?? new List<PVVisual>();
			PVFrame frame = slideVisual.Frame;
			for (int i = 0; i < list2.Count; i++)
			{
				VisualContainer visualContainer = PVDocumentToExplorationConverter.ConvertVisualToContainer(context, list2[i], converterContext.ConceptualSchema, frame, theme);
				list.Add(visualContainer);
			}
			string text = null;
			if (slideVisual.Visuals.Count > 1)
			{
				text = JsonUtils.ToJsonStringOrNull(PVDocumentToExplorationConverter.ConvertFilters(slideVisual.Visuals[1], null, converterContext.ConceptualSchema));
			}
			string text2 = null;
			if (!string.IsNullOrEmpty(slideVisual.Name))
			{
				converterContext.SectionDisplayText.TryGetValue(slideVisual.Name, out text2);
			}
			return new Section
			{
				Name = slideVisual.Name,
				DisplayName = text2,
				VisualContainers = list,
				Filters = text,
				Ordinal = pageIndex
			};
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000420C File Offset: 0x0000240C
		internal static bool HasValuesBucketWithOneItem(DataContext dataContext)
		{
			Bucket bucket = PVDocumentToExplorationConverter.GetBucket(dataContext, "Values");
			return bucket != null && bucket.BucketItems.Count == 1;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004238 File Offset: 0x00002438
		internal static string GetVisualType(string visualType, string chartLayoutType, LayoutContext layoutContext, DataContext dataContext)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(visualType);
			if (num <= 2550723911U)
			{
				if (num <= 1077553687U)
				{
					if (num <= 638284327U)
					{
						if (num != 139802751U)
						{
							if (num == 638284327U)
							{
								if (visualType == "CategoryChart")
								{
									num = global::<PrivateImplementationDetails>.ComputeStringHash(chartLayoutType);
									if (num <= 873976187U)
									{
										if (num != 441069013U)
										{
											if (num != 583084615U)
											{
												if (num == 873976187U)
												{
													if (chartLayoutType == "BarStacked")
													{
														return "barChart";
													}
												}
											}
											else if (chartLayoutType == "Column")
											{
												return "clusteredColumnChart";
											}
										}
										else if (chartLayoutType == "ColumnPercentStacked")
										{
											return "hundredPercentStackedColumnChart";
										}
									}
									else if (num <= 2887154416U)
									{
										if (num != 1485970596U)
										{
											if (num == 2887154416U)
											{
												if (chartLayoutType == "ColumnStacked")
												{
													return "columnChart";
												}
											}
										}
										else if (chartLayoutType == "BarPercentStacked")
										{
											return "hundredPercentStackedBarChart";
										}
									}
									else if (num != 3211044737U)
									{
										if (num == 3686563258U)
										{
											if (chartLayoutType == "Bar")
											{
												return "clusteredBarChart";
											}
										}
									}
									else if (chartLayoutType == "TreeMap")
									{
										return "treemap";
									}
								}
							}
						}
						else if (visualType == "Card")
						{
							if (layoutContext != null && string.Equals(layoutContext.CardStyles, "Callout", StringComparison.OrdinalIgnoreCase) && PVDocumentToExplorationConverter.HasValuesBucketWithOneItem(dataContext))
							{
								return "card";
							}
							return "multiRowCard";
						}
					}
					else if (num != 699101059U)
					{
						if (num == 1077553687U)
						{
							if (visualType == "ComboChart")
							{
								if (chartLayoutType == "ColumnStacked")
								{
									return "lineStackedColumnComboChart";
								}
								return "lineClusteredColumnComboChart";
							}
						}
					}
					else if (visualType == "Play")
					{
						return "play";
					}
				}
				else if (num <= 1342272825U)
				{
					if (num != 1151856721U)
					{
						if (num == 1342272825U)
						{
							if (visualType == "Slicer")
							{
								return "slicer";
							}
						}
					}
					else if (visualType == "Map")
					{
						return "map";
					}
				}
				else if (num != 1356523456U)
				{
					if (num != 1494001562U)
					{
						if (num == 2550723911U)
						{
							if (visualType == "Line")
							{
								return "lineChart";
							}
						}
					}
					else if (visualType == "Image")
					{
						return "image";
					}
				}
				else if (visualType == "SmallMultiple")
				{
					return "smallMultiple";
				}
			}
			else if (num <= 3607948159U)
			{
				if (num <= 3211044737U)
				{
					if (num != 3096863483U)
					{
						if (num == 3211044737U)
						{
							if (visualType == "TreeMap")
							{
								return "treemap";
							}
						}
					}
					else if (visualType == "Textbox")
					{
						return "textbox";
					}
				}
				else if (num != 3293395595U)
				{
					if (num != 3477574229U)
					{
						if (num == 3607948159U)
						{
							if (visualType == "Table")
							{
								return "table";
							}
						}
					}
					else if (visualType == "Pie")
					{
						return "pieChart";
					}
				}
				else if (visualType == "ChoroplethMap")
				{
					return "filledMap";
				}
			}
			else if (num <= 3876551184U)
			{
				if (num != 3783502617U)
				{
					if (num == 3876551184U)
					{
						if (visualType == "Gauge")
						{
							return "gauge";
						}
					}
				}
				else if (visualType == "Funnel")
				{
					return "funnel";
				}
			}
			else if (num != 3922821388U)
			{
				if (num != 4094025000U)
				{
					if (num == 4217405153U)
					{
						if (visualType == "Scatter")
						{
							return "scatterChart";
						}
					}
				}
				else if (visualType == "Subview")
				{
					return "subview";
				}
			}
			else if (visualType == "Matrix")
			{
				return "matrix";
			}
			return null;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000046B4 File Offset: 0x000028B4
		internal static string ConvertFontStyle(string fontStyle)
		{
			if (fontStyle == null)
			{
				return null;
			}
			string text = fontStyle.ToLowerInvariant();
			if (text == "italic")
			{
				return "italic";
			}
			if (!(text == "normal"))
			{
				return null;
			}
			return "normal";
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000046F8 File Offset: 0x000028F8
		internal static string ConvertFontWeight(string fontWeight)
		{
			if (fontWeight == null)
			{
				return null;
			}
			string text = fontWeight.ToLowerInvariant();
			if (text == "bold")
			{
				return "bold";
			}
			if (text == "light")
			{
				return "300";
			}
			if (!(text == "normal"))
			{
				return null;
			}
			return "normal";
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004750 File Offset: 0x00002950
		internal static string ConvertTextDecoration(string textDecoration)
		{
			if (textDecoration == null)
			{
				return null;
			}
			string text = textDecoration.ToLowerInvariant();
			if (text == "underline")
			{
				return "underline";
			}
			if (!(text == "normal"))
			{
				return null;
			}
			return "normal";
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004794 File Offset: 0x00002994
		internal static TextStyle ConvertTextStyle(PVTextStyle inputTextStyle)
		{
			if (inputTextStyle == null)
			{
				return null;
			}
			return new TextStyle
			{
				FontFamily = inputTextStyle.FontFamily,
				FontSize = inputTextStyle.FontSize,
				FontStyle = PVDocumentToExplorationConverter.ConvertFontStyle(inputTextStyle.FontStyle),
				FontWeight = PVDocumentToExplorationConverter.ConvertFontWeight(inputTextStyle.FontWeight),
				TextDecoration = PVDocumentToExplorationConverter.ConvertTextDecoration(inputTextStyle.TextDecoration)
			};
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000047F8 File Offset: 0x000029F8
		internal static List<TextRun> ConvertTextRuns(List<PVTextRun> inputTextRuns)
		{
			if (inputTextRuns == null)
			{
				return null;
			}
			List<TextRun> list = new List<TextRun>(inputTextRuns.Count);
			int i = 0;
			int count = inputTextRuns.Count;
			while (i < count)
			{
				PVTextRun pvtextRun = inputTextRuns[i];
				list.Add(new TextRun
				{
					Value = pvtextRun.Value,
					TextStyle = PVDocumentToExplorationConverter.ConvertTextStyle(pvtextRun.TextStyle)
				});
				i++;
			}
			return list;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000485C File Offset: 0x00002A5C
		internal static List<Paragraph> ConvertParagraphs(List<PVParagraph> inputParagraphs)
		{
			if (inputParagraphs == null)
			{
				return null;
			}
			List<Paragraph> list = new List<Paragraph>(inputParagraphs.Count);
			int i = 0;
			int count = inputParagraphs.Count;
			while (i < count)
			{
				PVParagraph pvparagraph = inputParagraphs[i];
				list.Add(new Paragraph
				{
					TextRuns = PVDocumentToExplorationConverter.ConvertTextRuns(pvparagraph.TextRuns),
					HorizontalTextAlignment = ((pvparagraph.HorizontalTextAlignment != null) ? pvparagraph.HorizontalTextAlignment.ToLowerInvariant() : null)
				});
				i++;
			}
			return list;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000048D0 File Offset: 0x00002AD0
		internal static string LookupResource(PVDocumentContext context, string resourceId)
		{
			if (resourceId == null || context == null || context.ImageResourceMap == null)
			{
				return null;
			}
			PVResourceEntry pvresourceEntry = context.ImageResourceMap.FirstOrDefault((PVResourceEntry w) => w.ResourceId == resourceId);
			if (pvresourceEntry == null)
			{
				return null;
			}
			return pvresourceEntry.ImageBytes;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004924 File Offset: 0x00002B24
		internal static string WrapDataUri(string base64EncodedString)
		{
			if (base64EncodedString == null)
			{
				return null;
			}
			if (base64EncodedString.StartsWith("base64'"))
			{
				base64EncodedString = base64EncodedString.Substring("base64'".Length);
			}
			if (base64EncodedString.EndsWith("'", StringComparison.Ordinal))
			{
				base64EncodedString = base64EncodedString.Substring(0, base64EncodedString.Length - 1);
			}
			return "data:image/png;base64," + base64EncodedString;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004980 File Offset: 0x00002B80
		internal static DataViewGeneralPropertyDefinition ConvertTextboxProperties(PVDocumentContext context, LayoutContext layoutContext)
		{
			if (layoutContext == null)
			{
				return null;
			}
			if (layoutContext.Paragraphs != null || layoutContext.ResourceId != null || layoutContext.VerticalAlignment != null)
			{
				return new DataViewGeneralPropertyDefinition
				{
					Paragraphs = PVDocumentToExplorationConverter.ConvertParagraphs(layoutContext.Paragraphs),
					ImageUrl = PVDocumentToExplorationConverter.WrapDataUri(PVDocumentToExplorationConverter.LookupResource(context, layoutContext.ResourceId))
				};
			}
			return null;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000049E0 File Offset: 0x00002BE0
		internal static DataViewGeneralPropertyDefinition ConvertTableProperties(DataContext dataContext)
		{
			if (dataContext == null)
			{
				return null;
			}
			Bucket bucket = dataContext.Buckets.FirstOrDefault((Bucket b) => b.Name == "Values");
			if (bucket == null)
			{
				return null;
			}
			bool? flag = PVDocumentToExplorationConverter.HasSubtotals(bucket);
			DataViewGeneralPropertyDefinition dataViewGeneralPropertyDefinition = new DataViewGeneralPropertyDefinition();
			bool? flag2 = flag;
			bool flag3 = true;
			dataViewGeneralPropertyDefinition.Totals = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
			return dataViewGeneralPropertyDefinition;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004A50 File Offset: 0x00002C50
		internal static DataViewGeneralPropertyDefinition ConvertMatrixProperties(DataContext dataContext)
		{
			if (dataContext == null)
			{
				return null;
			}
			DataViewGeneralPropertyDefinition dataViewGeneralPropertyDefinition = null;
			Bucket bucket = dataContext.Buckets.FirstOrDefault((Bucket b) => b.Name == "RowHierarchy");
			if (bucket != null)
			{
				bool? flag = PVDocumentToExplorationConverter.HasSubtotals(bucket);
				DataViewGeneralPropertyDefinition dataViewGeneralPropertyDefinition2 = new DataViewGeneralPropertyDefinition();
				bool? flag2 = flag;
				bool flag3 = true;
				dataViewGeneralPropertyDefinition2.RowSubtotals = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
				dataViewGeneralPropertyDefinition = dataViewGeneralPropertyDefinition2;
			}
			Bucket bucket2 = dataContext.Buckets.FirstOrDefault((Bucket b) => b.Name == "ColumnHierarchy");
			if (bucket2 != null)
			{
				if (dataViewGeneralPropertyDefinition == null)
				{
					dataViewGeneralPropertyDefinition = new DataViewGeneralPropertyDefinition();
				}
				bool? flag4 = PVDocumentToExplorationConverter.HasSubtotals(bucket2);
				DataViewGeneralPropertyDefinition dataViewGeneralPropertyDefinition3 = dataViewGeneralPropertyDefinition;
				bool? flag2 = flag4;
				bool flag3 = true;
				dataViewGeneralPropertyDefinition3.ColumnSubtotals = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
			}
			return dataViewGeneralPropertyDefinition;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004B28 File Offset: 0x00002D28
		private static bool? HasSubtotals(Bucket bucket)
		{
			if (bucket.Properties == null)
			{
				return null;
			}
			BucketProperty bucketProperty = bucket.Properties.FirstOrDefault((BucketProperty p) => p.Name == "Subtotal");
			if (bucketProperty == null)
			{
				return null;
			}
			return new bool?(bucketProperty.Value);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004B8C File Offset: 0x00002D8C
		internal static Bucket GetBucket(DataContext dataContext, string name)
		{
			if (dataContext == null || dataContext.Buckets == null)
			{
				return null;
			}
			return dataContext.Buckets.FirstOrDefault((Bucket b) => string.Equals(b.Name, name, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004BCA File Offset: 0x00002DCA
		internal static Bucket GetCategoryBucket(DataContext dataContext)
		{
			return PVDocumentToExplorationConverter.GetBucket(dataContext, "Category");
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004BD8 File Offset: 0x00002DD8
		internal static DataViewAxisPropertyDefinition ConvertAxisProperties(DataContext dataContext)
		{
			Bucket categoryBucket = PVDocumentToExplorationConverter.GetCategoryBucket(dataContext);
			if (categoryBucket == null)
			{
				return null;
			}
			BucketProperty bucketProperty = null;
			if (categoryBucket.Properties != null)
			{
				bucketProperty = categoryBucket.Properties.FirstOrDefault((BucketProperty p) => string.Equals(p.Name, "ScalarAxis", StringComparison.OrdinalIgnoreCase));
			}
			return new DataViewAxisPropertyDefinition
			{
				AxisType = ((bucketProperty != null && bucketProperty.Value) ? "Scalar" : "Categorical")
			};
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004C50 File Offset: 0x00002E50
		internal static string ConvertVrmLegendPositionToDirection(string vrmLegendPosition)
		{
			if (vrmLegendPosition == "TopCenter")
			{
				return "Top";
			}
			if (vrmLegendPosition == "LeftTop")
			{
				return "Left";
			}
			if (vrmLegendPosition == "BottomCenter")
			{
				return "Bottom";
			}
			if (!(vrmLegendPosition == "RightTop"))
			{
				return null;
			}
			return "Right";
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004CAC File Offset: 0x00002EAC
		internal static List<DataViewLegendObjectDefinition> ConvertLegendProperties(LayoutContext layoutContext, bool hideLegend)
		{
			if (layoutContext == null)
			{
				return null;
			}
			if (layoutContext.IsLegendHidden != null || layoutContext.LegendPosition != null || hideLegend)
			{
				string text = PVDocumentToExplorationConverter.ConvertVrmLegendPositionToDirection(layoutContext.LegendPosition);
				bool? flag = null;
				if (hideLegend)
				{
					flag = new bool?(false);
				}
				else if (layoutContext.IsLegendHidden != null)
				{
					flag = new bool?(!layoutContext.IsLegendHidden.Value);
				}
				return new List<DataViewLegendObjectDefinition>
				{
					new DataViewLegendObjectDefinition
					{
						Properties = new DataViewLegendPropertyDefinition
						{
							Show = DataViewObjectPropertyDefinition.ToPropertyDefinition(flag),
							Position = text
						}
					}
				};
			}
			return null;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004D5C File Offset: 0x00002F5C
		internal static List<DataViewLabelsObjectDefinition> CreateLabelPropertiesWithValue(bool? value)
		{
			if (value == null)
			{
				return null;
			}
			return new List<DataViewLabelsObjectDefinition>
			{
				new DataViewLabelsObjectDefinition
				{
					Properties = new DataViewLabelsPropertyDefinition
					{
						Show = DataViewObjectPropertyDefinition.ToPropertyDefinition(new bool?(value.Value))
					}
				}
			};
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004D9B File Offset: 0x00002F9B
		internal static List<DataViewLabelsObjectDefinition> ConvertLabelsProperties(string visualType, LayoutContext layoutContext)
		{
			if (visualType == "pieChart")
			{
				return PVDocumentToExplorationConverter.CreateLabelPropertiesWithValue(new bool?(false));
			}
			if (layoutContext != null)
			{
				return PVDocumentToExplorationConverter.CreateLabelPropertiesWithValue(layoutContext.AreLabelsVisible);
			}
			return null;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004DC8 File Offset: 0x00002FC8
		internal static List<DataViewDataPointObjectDefinition> ConvertDataPointObjects(string visualType, DataContext dataContext, Theme theme)
		{
			if (visualType == "filledMap")
			{
				Bucket bucket = PVDocumentToExplorationConverter.GetBucket(dataContext, "Size");
				ColorRange colorRange = null;
				if (theme != null)
				{
					colorRange = ColorHelper.CalculateFilledMapColors(theme.Accent1);
				}
				if (bucket != null && bucket.BucketItems != null && bucket.BucketItems.Count > 0)
				{
					return new List<DataViewDataPointObjectDefinition>
					{
						new DataViewDataPointObjectDefinition
						{
							Properties = new DataViewDataPointPropertyDefinition
							{
								FillRule = new DataViewObjectPropertyDefinition
								{
									LinearGradient2 = new LinearGradient2
									{
										Max = new RuleColorStop
										{
											Color = ((colorRange != null) ? colorRange.Colors[1] : "#01B8AA")
										},
										Min = new RuleColorStop
										{
											Color = ((colorRange != null) ? colorRange.Colors[0] : "#ccf1ee")
										}
									}
								}
							}
						}
					};
				}
			}
			return null;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004EAF File Offset: 0x000030AF
		internal static List<DataViewLabelsObjectDefinition> ConvertCategoryLabels(string visualType)
		{
			if (visualType == "pieChart")
			{
				return PVDocumentToExplorationConverter.CreateLabelPropertiesWithValue(new bool?(false));
			}
			if (!(visualType == "scatterChart"))
			{
				return null;
			}
			return PVDocumentToExplorationConverter.CreateLabelPropertiesWithValue(new bool?(true));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004EE8 File Offset: 0x000030E8
		internal static DataViewObjectDefinitions ConvertObjects(PVDocumentContext context, string visualType, LayoutContext layoutContext, DataContext dataContext, List<PVProperty> powerViewProperties, QueryDefinition prototypeQuery, IConceptualSchema conceptualSchema, IEnumerable<ProjectionInfo> projections, bool hideLegend = false, Theme theme = null)
		{
			DataViewGeneralPropertyDefinition dataViewGeneralPropertyDefinition = null;
			if (!(visualType == "matrix"))
			{
				if (!(visualType == "table"))
				{
					if (!(visualType == "slicer"))
					{
						if (visualType == "image" || visualType == "textbox")
						{
							dataViewGeneralPropertyDefinition = PVDocumentToExplorationConverter.ConvertTextboxProperties(context, layoutContext);
						}
					}
					else
					{
						dataViewGeneralPropertyDefinition = PVDocumentToExplorationConverter.ConvertSlicerProperties(powerViewProperties, prototypeQuery, conceptualSchema);
					}
				}
				else
				{
					dataViewGeneralPropertyDefinition = PVDocumentToExplorationConverter.ConvertTableProperties(dataContext);
				}
			}
			else
			{
				dataViewGeneralPropertyDefinition = PVDocumentToExplorationConverter.ConvertMatrixProperties(dataContext);
			}
			List<DataViewGeneralObjectDefinition> list = null;
			if (dataViewGeneralPropertyDefinition != null)
			{
				list = new List<DataViewGeneralObjectDefinition>
				{
					new DataViewGeneralObjectDefinition
					{
						Properties = dataViewGeneralPropertyDefinition
					}
				};
			}
			PVDocumentToExplorationConverter.ConvertCustomFormatStrings(layoutContext, visualType, projections, ref list);
			List<DataViewAxisObjectDefinition> list2 = null;
			DataViewAxisPropertyDefinition dataViewAxisPropertyDefinition = PVDocumentToExplorationConverter.ConvertAxisProperties(dataContext);
			if (dataViewAxisPropertyDefinition != null)
			{
				list2 = new List<DataViewAxisObjectDefinition>
				{
					new DataViewAxisObjectDefinition
					{
						Properties = dataViewAxisPropertyDefinition
					}
				};
			}
			List<DataViewLegendObjectDefinition> list3 = PVDocumentToExplorationConverter.ConvertLegendProperties(layoutContext, hideLegend);
			List<DataViewLabelsObjectDefinition> list4 = PVDocumentToExplorationConverter.ConvertLabelsProperties(visualType, layoutContext);
			List<DataViewDataPointObjectDefinition> list5 = PVDocumentToExplorationConverter.ConvertDataPointObjects(visualType, dataContext, theme);
			List<DataViewLabelsObjectDefinition> list6 = PVDocumentToExplorationConverter.ConvertCategoryLabels(visualType);
			if (list == null && list2 == null && list3 == null && list4 == null && list5 == null && list6 == null)
			{
				return null;
			}
			return new DataViewObjectDefinitions
			{
				General = list,
				CategoryAxis = list2,
				Legend = list3,
				Labels = list4,
				CategoryLabels = list6,
				DataPoint = list5
			};
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005020 File Offset: 0x00003220
		private static DataViewGeneralPropertyDefinition ConvertSlicerProperties(List<PVProperty> powerViewProperties, QueryDefinition prototypeQuery, IConceptualSchema conceptualSchema)
		{
			if (prototypeQuery == null)
			{
				return null;
			}
			if (powerViewProperties == null || powerViewProperties.Count == 0)
			{
				return null;
			}
			PVProperty pvproperty = powerViewProperties.FirstOrDefault((PVProperty prop) => prop.Name == "FilterOutput");
			if (pvproperty == null)
			{
				return null;
			}
			List<EntitySource> list = new List<EntitySource>();
			FilterFromManager filterFromManager;
			PVDocumentToExplorationConverter.ConvertedFormulaInfo convertedFormulaInfo;
			FilterDefinition filterDefinition = PVDocumentToExplorationConverter.ConvertFilterDefinition(pvproperty.Value.FilterValue, prototypeQuery.From.AsReadOnly(), list, conceptualSchema, out filterFromManager, out convertedFormulaInfo);
			if (filterDefinition == null)
			{
				return null;
			}
			return new DataViewGeneralPropertyDefinition
			{
				Filter = filterDefinition
			};
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000050B8 File Offset: 0x000032B8
		private static void ConvertCustomFormatStrings(LayoutContext layoutContext, string visualType, IEnumerable<ProjectionInfo> projections, ref List<DataViewGeneralObjectDefinition> general)
		{
			if (layoutContext == null)
			{
				return;
			}
			if (layoutContext.Columns == null || layoutContext.Columns.Count == 0)
			{
				return;
			}
			IEnumerable<ProjectionInfo> enumerable = null;
			if (visualType == "table" || visualType == "matrix")
			{
				enumerable = projections.Where((ProjectionInfo p) => p.Role == "Values");
			}
			if (enumerable != null && layoutContext.Columns.Count == enumerable.Count<ProjectionInfo>())
			{
				int num = 0;
				foreach (ProjectionInfo projectionInfo in enumerable)
				{
					string customFormatString = layoutContext.Columns[num++].CustomFormatString;
					if (!string.IsNullOrEmpty(customFormatString))
					{
						if (general == null)
						{
							general = new List<DataViewGeneralObjectDefinition>();
						}
						general.Add(new DataViewGeneralObjectDefinition
						{
							Selector = new Selector
							{
								Metadata = projectionInfo.QueryRef
							},
							Properties = new DataViewGeneralPropertyDefinition
							{
								FormatString = customFormatString
							}
						});
					}
				}
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000051DC File Offset: 0x000033DC
		internal static bool FixScatterBubbleColoring(PVVisual visual, QueryDefinitionBuilder qd, List<ProjectionInfo> projections, ref List<string> showAllRoles)
		{
			if (visual.Type != "Scatter")
			{
				return false;
			}
			bool flag = string.Equals(visual.Type, "matrix", StringComparison.OrdinalIgnoreCase);
			DataContext dataContext = visual.DataContext;
			if (dataContext == null)
			{
				return false;
			}
			Bucket categoryBucket = PVDocumentToExplorationConverter.GetCategoryBucket(dataContext);
			Bucket bucket = PVDocumentToExplorationConverter.GetBucket(dataContext, "Series");
			Bucket bucket2 = PVDocumentToExplorationConverter.GetBucket(dataContext, "Size");
			if (categoryBucket != null && categoryBucket.BucketItems != null && categoryBucket.BucketItems.Count > 0 && bucket != null && (bucket.BucketItems == null || bucket.BucketItems.Count == 0) && bucket2 != null && bucket2.BucketItems != null && bucket2.BucketItems.Count == 1)
			{
				BucketItem bucketItem = categoryBucket.BucketItems[0];
				PVDocumentToExplorationConverter.AddBucketItem("Series", bucketItem, qd, projections, flag, ref showAllRoles);
				return true;
			}
			return false;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000052AC File Offset: 0x000034AC
		internal static VisualContainer ConvertVisualToContainer(PVDocumentContext context, PVVisual visual, IConceptualSchema conceptualSchema, PVFrame slideFrame, Theme theme = null)
		{
			VisualContainer visualContainer = new VisualContainer();
			if (visual.Frame != null)
			{
				decimal num = visual.Frame.Position.X;
				decimal num2 = visual.Frame.Position.Y;
				decimal num3 = visual.Frame.Width;
				decimal num4 = visual.Frame.Height;
				if (slideFrame != null && slideFrame.Width == 1024m && slideFrame.Height == 768m)
				{
					num3 = 0.9375m * num3;
					num = 0.9375m * num;
					num4 = 0.9375m * num4;
					num2 = 0.9375m * num2;
				}
				visualContainer.X = num;
				visualContainer.Y = num2;
				visualContainer.Width = num3;
				visualContainer.Height = num4;
			}
			visualContainer.Z = visual.ZIndex;
			VisualContainerConfig visualContainerConfig = new VisualContainerConfig();
			LayoutContext layoutContext = visual.LayoutContext;
			DataContext dataContext = visual.DataContext;
			string text = ((layoutContext != null) ? layoutContext.ChartLayoutType : null);
			string visualType = PVDocumentToExplorationConverter.GetVisualType(visual.Type, text, layoutContext, dataContext);
			if (visualType == null)
			{
				visualContainerConfig.SingleVisual = new SingleVisualConfig
				{
					Title = PVDocumentToExplorationConverter.GetVisualTitle(layoutContext),
					VisualType = "error"
				};
			}
			else
			{
				List<ProjectionInfo> list = new List<ProjectionInfo>();
				List<string> list2 = null;
				QueryDefinition queryDefinition = null;
				bool flag = false;
				if (dataContext != null)
				{
					QueryDefinitionBuilder queryDefinitionBuilder = new QueryDefinitionBuilder(conceptualSchema);
					PVDocumentToExplorationConverter.AddBuckets(visual, queryDefinitionBuilder, list, ref list2);
					PVDocumentToExplorationConverter.AddComboChartBuckets(visual, queryDefinitionBuilder, list, ref list2);
					PVDocumentToExplorationConverter.AddSort(visual, queryDefinitionBuilder);
					flag = PVDocumentToExplorationConverter.FixScatterBubbleColoring(visual, queryDefinitionBuilder, list, ref list2);
					queryDefinition = queryDefinitionBuilder.ToQueryDefinition();
					visualContainer.Filters = JsonUtils.ToJsonStringOrNull(PVDocumentToExplorationConverter.ConvertVisualFilters(visual, queryDefinition, conceptualSchema));
				}
				visualContainerConfig.SingleVisual = new SingleVisualConfig
				{
					Title = PVDocumentToExplorationConverter.GetVisualTitle(layoutContext),
					VisualType = visualType,
					Projections = list,
					ShowAllRoles = list2,
					PrototypeQuery = queryDefinition,
					Objects = PVDocumentToExplorationConverter.ConvertObjects(context, visualType, visual.LayoutContext, visual.DataContext, visual.Properties, queryDefinition, conceptualSchema, list, flag, theme)
				};
			}
			visualContainer.Config = JsonUtils.ToJsonString(visualContainerConfig);
			return visualContainer;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000054FC File Offset: 0x000036FC
		internal static VisualTitle GetVisualTitle(LayoutContext layoutContext)
		{
			if (layoutContext != null && layoutContext.IsChartTitleHidden != null)
			{
				return new VisualTitle
				{
					Show = !layoutContext.IsChartTitleHidden.Value
				};
			}
			return null;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000553C File Offset: 0x0000373C
		private static QueryPropertyExpression CreatePropertyExpression(IFromManager from, string entityName, string propertyName, out string rewrittenEntityName, out string rewrittenPropertyName, bool populateEntity = false)
		{
			EntitySource orAddEntitySource = from.GetOrAddEntitySource(entityName);
			rewrittenEntityName = orAddEntitySource.Entity ?? orAddEntitySource.EntitySet;
			bool flag;
			rewrittenPropertyName = from.RewritePropertyName(orAddEntitySource, propertyName, out flag);
			QuerySourceRefExpression querySourceRefExpression = new QuerySourceRefExpression
			{
				Source = orAddEntitySource.Name
			};
			if (populateEntity)
			{
				querySourceRefExpression.Entity = orAddEntitySource.Entity;
			}
			if (flag)
			{
				return new QueryMeasureExpression
				{
					Expression = querySourceRefExpression,
					Property = rewrittenPropertyName
				};
			}
			return new QueryColumnExpression
			{
				Expression = querySourceRefExpression,
				Property = rewrittenPropertyName
			};
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000055CC File Offset: 0x000037CC
		public static QueryAggregateFunction ConvertFormulaFunctionToQueryAggregateFunction(string formulaFunction)
		{
			if (formulaFunction == "Average")
			{
				return QueryAggregateFunction.Avg;
			}
			if (formulaFunction == "Count")
			{
				return QueryAggregateFunction.CountNonNull;
			}
			if (formulaFunction == "DistinctCount")
			{
				return QueryAggregateFunction.Count;
			}
			if (formulaFunction == "Min")
			{
				return QueryAggregateFunction.Min;
			}
			if (formulaFunction == "Max")
			{
				return QueryAggregateFunction.Max;
			}
			if (!(formulaFunction == "Sum"))
			{
				throw new ArgumentException("Invalid AggregateFunction", "formulaFunction");
			}
			return QueryAggregateFunction.Sum;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005644 File Offset: 0x00003844
		private static QueryHierarchyLevelExpression CreateHierarchyLevelExpression(IFromManager from, string entityName, string hierarchyName, string levelName, out string rewrittenEntityName, out string rewrittenLevelName, bool populateEntity = false)
		{
			QueryHierarchyExpression queryHierarchyExpression = PVDocumentToExplorationConverter.CreateHierarchyExpression(from, entityName, hierarchyName, levelName, out rewrittenEntityName, populateEntity);
			rewrittenLevelName = from.RewriteHierarchyLevelName(from.GetOrAddEntitySource(entityName), hierarchyName, levelName);
			return new QueryHierarchyLevelExpression
			{
				Expression = queryHierarchyExpression,
				Level = rewrittenLevelName
			};
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000568C File Offset: 0x0000388C
		private static QueryHierarchyExpression CreateHierarchyExpression(IFromManager from, string entityName, string hierarchyName, string propertyName, out string rewrittenEntityName, bool populateEntity = false)
		{
			EntitySource orAddEntitySource = from.GetOrAddEntitySource(entityName);
			rewrittenEntityName = orAddEntitySource.Entity ?? orAddEntitySource.EntitySet;
			QuerySourceRefExpression querySourceRefExpression = new QuerySourceRefExpression
			{
				Source = orAddEntitySource.Name
			};
			if (populateEntity)
			{
				querySourceRefExpression.Entity = orAddEntitySource.Entity;
			}
			string text = from.RewriteHierarchyName(orAddEntitySource, hierarchyName);
			return new QueryHierarchyExpression
			{
				Expression = querySourceRefExpression,
				Hierarchy = text
			};
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000056F8 File Offset: 0x000038F8
		private static PVDocumentToExplorationConverter.ConvertedFormulaInfo CreateAggregationExpression(IFromManager from, string formulaFunction, List<Formula> arguments, bool populateEntity = false)
		{
			if (arguments == null || arguments.Count != 1)
			{
				throw new ArgumentException("Invalid Arguments for an Aggregate", "arguments");
			}
			PVDocumentToExplorationConverter.ConvertedFormulaInfo convertedFormulaInfo = PVDocumentToExplorationConverter.FormulaToQueryExpression(from, arguments[0], populateEntity);
			return new PVDocumentToExplorationConverter.ConvertedFormulaInfo
			{
				QueryExpression = new QueryAggregationExpression
				{
					Function = PVDocumentToExplorationConverter.ConvertFormulaFunctionToQueryAggregateFunction(formulaFunction),
					Expression = convertedFormulaInfo.QueryExpression
				},
				EntityName = convertedFormulaInfo.EntityName,
				FieldName = convertedFormulaInfo.FieldName
			};
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005778 File Offset: 0x00003978
		internal static PVDocumentToExplorationConverter.ConvertedFormulaInfo FormulaToQueryExpression(IFromManager from, Formula formula, bool isStandaloneExpression = false)
		{
			if (formula.QualifiedName != null)
			{
				List<string> qualifiedName = formula.QualifiedName;
				if (qualifiedName.Count < 2)
				{
					throw new ArgumentException("Invalid QualifiedName", "formula.QualifiedName");
				}
				if (qualifiedName.Count == 2)
				{
					string text = qualifiedName[0];
					string text2 = qualifiedName[1];
					string text3;
					string text4;
					QueryPropertyExpression queryPropertyExpression = PVDocumentToExplorationConverter.CreatePropertyExpression(from, text, text2, out text3, out text4, isStandaloneExpression);
					return new PVDocumentToExplorationConverter.ConvertedFormulaInfo
					{
						QueryExpression = queryPropertyExpression,
						FieldName = text4,
						EntityName = text3
					};
				}
				if (qualifiedName.Count == 3)
				{
					string text5 = qualifiedName[0];
					string text6 = qualifiedName[1];
					string text7 = qualifiedName[2];
					string text8;
					string text9;
					QueryHierarchyLevelExpression queryHierarchyLevelExpression = PVDocumentToExplorationConverter.CreateHierarchyLevelExpression(from, text5, text6, text7, out text8, out text9, isStandaloneExpression);
					return new PVDocumentToExplorationConverter.ConvertedFormulaInfo
					{
						QueryExpression = queryHierarchyLevelExpression,
						FieldName = null,
						EntityName = null
					};
				}
			}
			else if (formula.Function != null)
			{
				return PVDocumentToExplorationConverter.CreateAggregationExpression(from, formula.Function, formula.Arguments, isStandaloneExpression);
			}
			string text10 = "Invalid formula:";
			string function = formula.Function;
			string text11 = ",";
			List<string> qualifiedName2 = formula.QualifiedName;
			throw new ArgumentException(text10 + function + text11 + ((qualifiedName2 != null) ? qualifiedName2.ToString() : null), "formula");
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005898 File Offset: 0x00003A98
		internal static void AddBucketItem(string roleName, BucketItem bucketItem, QueryDefinitionBuilder qd, List<ProjectionInfo> projections, bool isMatrixVisual, ref List<string> showAllRoles)
		{
			if (bucketItem == null || bucketItem.Formula == null)
			{
				return;
			}
			Formula formula = bucketItem.Formula;
			PVDocumentToExplorationConverter.ConvertedFormulaInfo convertedFormulaInfo = PVDocumentToExplorationConverter.FormulaToQueryExpression(qd.GetFromClause(), formula, false);
			if (convertedFormulaInfo != null)
			{
				string text;
				if (!qd.TryGetSelectName(convertedFormulaInfo.QueryExpression, out text))
				{
					text = qd.AddSelect(convertedFormulaInfo.QueryExpression);
				}
				ProjectionInfo projectionInfo = new ProjectionInfo
				{
					QueryRef = text,
					Role = roleName
				};
				if (!isMatrixVisual)
				{
					ProjectionInfo projectionInfo2 = projectionInfo;
					bool? isDrilledItem = bucketItem.IsDrilledItem;
					bool flag = true;
					projectionInfo2.Active = (isDrilledItem.GetValueOrDefault() == flag) & (isDrilledItem != null);
				}
				if (bucketItem.ShowItemsWithNoData != null && bucketItem.ShowItemsWithNoData.Value)
				{
					if (showAllRoles == null)
					{
						showAllRoles = new List<string>();
					}
					if (!showAllRoles.Contains(roleName))
					{
						showAllRoles.Add(roleName);
					}
				}
				projections.Add(projectionInfo);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000597C File Offset: 0x00003B7C
		private static string MapBucketName(string bucketName, string visualName)
		{
			if (bucketName == null)
			{
				return bucketName;
			}
			string text;
			if (string.Equals(visualName, "treemap", StringComparison.OrdinalIgnoreCase))
			{
				text = bucketName.ToUpperInvariant();
				if (text == "CATEGORY")
				{
					return "Group";
				}
				if (text == "SERIES")
				{
					return "Details";
				}
				if (text == "Y")
				{
					return "Values";
				}
			}
			text = bucketName.ToUpperInvariant();
			if (text == "ROWHIERARCHY")
			{
				return "Rows";
			}
			if (!(text == "COLUMNHIERARCHY"))
			{
				return bucketName;
			}
			return "Columns";
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005A10 File Offset: 0x00003C10
		internal static void AddBuckets(PVVisual visualJson, QueryDefinitionBuilder qd, List<ProjectionInfo> projections, ref List<string> showAllRoles)
		{
			List<Bucket> buckets = visualJson.DataContext.Buckets;
			if (buckets == null)
			{
				return;
			}
			for (int i = 0; i < buckets.Count; i++)
			{
				Bucket bucket = buckets[i];
				if (bucket != null)
				{
					List<BucketItem> bucketItems = bucket.BucketItems;
					if (!bucketItems.IsNullOrEmpty<BucketItem>())
					{
						string text = PVDocumentToExplorationConverter.MapBucketName(bucket.Name, visualJson.Type);
						text == "Category";
						bool flag = string.Equals(visualJson.Type, "matrix", StringComparison.OrdinalIgnoreCase);
						for (int j = 0; j < bucketItems.Count; j++)
						{
							BucketItem bucketItem = bucketItems[j];
							PVDocumentToExplorationConverter.AddBucketItem(text, bucketItem, qd, projections, flag, ref showAllRoles);
						}
					}
				}
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005ABC File Offset: 0x00003CBC
		internal static void AddComboChartBuckets(PVVisual visualJson, QueryDefinitionBuilder qd, List<ProjectionInfo> projections, ref List<string> showAllRoles)
		{
			if (visualJson.Type == "ComboChart")
			{
				PVVisual pvvisual = visualJson.Visuals.FirstOrDefault((PVVisual v) => v.Type == "ComboChart");
				if (pvvisual != null)
				{
					Bucket bucket = pvvisual.DataContext.Buckets.FirstOrDefault((Bucket b) => b.Name == "Y");
					if (bucket != null && bucket.BucketItems != null && bucket.BucketItems.Count > 0)
					{
						List<BucketItem> bucketItems = bucket.BucketItems;
						bool flag = string.Equals(visualJson.Type, "matrix", StringComparison.OrdinalIgnoreCase);
						for (int i = 0; i < bucketItems.Count; i++)
						{
							PVDocumentToExplorationConverter.AddBucketItem("Y2", bucketItems[i], qd, projections, flag, ref showAllRoles);
						}
					}
				}
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005B9C File Offset: 0x00003D9C
		internal static void AddSort(PVVisual visualJson, QueryDefinitionBuilder qd)
		{
			PVProperty pvproperty;
			if (visualJson.Properties == null)
			{
				pvproperty = null;
			}
			else
			{
				pvproperty = visualJson.Properties.FirstOrDefault((PVProperty prop) => prop.Name == "SelectedSort");
			}
			PVProperty pvproperty2 = pvproperty;
			if (pvproperty2 != null && pvproperty2.Value != null)
			{
				foreach (PVPropertyValue pvpropertyValue in pvproperty2.Value.SortValue)
				{
					QuerySortDirection querySortDirection = ((pvpropertyValue.Direction == "Descending") ? QuerySortDirection.Descending : QuerySortDirection.Ascending);
					PVDocumentToExplorationConverter.ConvertedFormulaInfo convertedFormulaInfo = PVDocumentToExplorationConverter.FormulaToQueryExpression(qd.GetFromClause(), pvpropertyValue.Formula, false);
					qd.AddOrderBy(new QuerySortClause
					{
						Direction = querySortDirection,
						Expression = convertedFormulaInfo.QueryExpression
					});
				}
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005C84 File Offset: 0x00003E84
		internal static List<FilterContainer> ConvertVisualFilters(PVVisual visualJson, QueryDefinition visualQueryDefinition, IConceptualSchema conceptualSchema)
		{
			PVVisual pvvisual;
			if (visualJson.Visuals == null)
			{
				pvvisual = null;
			}
			else
			{
				pvvisual = visualJson.Visuals.FirstOrDefault((PVVisual v) => v.Type == "FilterVisual");
			}
			PVVisual pvvisual2 = pvvisual;
			if (pvvisual2 == null || pvvisual2.Visuals == null)
			{
				return null;
			}
			return PVDocumentToExplorationConverter.ConvertFilters(pvvisual2, visualQueryDefinition, conceptualSchema);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005CDC File Offset: 0x00003EDC
		internal static FilterContainer ConvertPVVisualToFilterContainer(PVFilter filterVisual, ReadOnlyCollection<EntitySource> visualQueryEntitySources, List<EntitySource> filterEntitySources, IConceptualSchema conceptualSchema, Formula dataContextFormula)
		{
			FilterFromManager filterFromManager;
			PVDocumentToExplorationConverter.ConvertedFormulaInfo convertedFormulaInfo;
			FilterDefinition filterDefinition = PVDocumentToExplorationConverter.ConvertFilterDefinition(filterVisual, visualQueryEntitySources, filterEntitySources, conceptualSchema, out filterFromManager, out convertedFormulaInfo);
			if (convertedFormulaInfo == null)
			{
				return null;
			}
			string text = convertedFormulaInfo.EntityName;
			string text2 = convertedFormulaInfo.FieldName;
			QueryExpressionContainer queryExpressionContainer;
			if (dataContextFormula != null)
			{
				PVDocumentToExplorationConverter.ConvertedFormulaInfo convertedFormulaInfo2 = PVDocumentToExplorationConverter.FormulaToQueryExpression(filterFromManager, dataContextFormula, false);
				text = convertedFormulaInfo2.EntityName;
				text2 = convertedFormulaInfo2.FieldName;
				queryExpressionContainer = new QueryExpressionContainer(PVDocumentToExplorationConverter.FormulaToQueryExpression(filterFromManager, dataContextFormula, true).QueryExpression, null, null);
			}
			else
			{
				queryExpressionContainer = PVDocumentToExplorationConverter.CreatePropertyExpression(filterFromManager, text, text2, out text, out text2, true);
			}
			return new FilterContainer
			{
				Field = new SQFieldDef
				{
					EntityName = text,
					FieldName = text2
				},
				Filter = filterDefinition,
				Expression = queryExpressionContainer
			};
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005D88 File Offset: 0x00003F88
		private static FilterDefinition ConvertFilterDefinition(PVFilter filterVisual, ReadOnlyCollection<EntitySource> visualQueryEntitySources, List<EntitySource> filterEntitySources, IConceptualSchema conceptualSchema, out FilterFromManager fromManager, out PVDocumentToExplorationConverter.ConvertedFormulaInfo filterItem)
		{
			FilterDefinition filterDefinition = new FilterDefinition
			{
				Version = new int?(2),
				From = new List<EntitySource>(),
				Where = new List<QueryFilter>()
			};
			fromManager = new FilterFromManager(visualQueryEntitySources, filterEntitySources, filterDefinition.From, conceptualSchema);
			filterItem = PVDocumentToExplorationConverter.ConvertFilter(filterVisual, filterDefinition, fromManager);
			if (filterItem != null)
			{
				filterDefinition.Where.Add(new QueryFilter
				{
					Condition = filterItem.QueryExpression
				});
			}
			return filterDefinition;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005E04 File Offset: 0x00004004
		internal static string GetFilterContainerTypeFromVisual(PVVisual filterVisual)
		{
			string type = filterVisual.Type;
			if (type == "CategoryFilterCard")
			{
				return FilterType.Categorical;
			}
			if (!(type == "AdvancedFilterCard") && !(type == "MeasureRangeFilterCard"))
			{
				return null;
			}
			return FilterType.Advanced;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005E50 File Offset: 0x00004050
		internal static List<FilterContainer> ConvertFilters(PVVisual filterHost, QueryDefinition visualQueryDefinition, IConceptualSchema conceptualSchema)
		{
			if (filterHost.Visuals == null)
			{
				return null;
			}
			List<FilterContainer> list = new List<FilterContainer>();
			ReadOnlyCollection<EntitySource> readOnlyCollection = ((visualQueryDefinition != null) ? visualQueryDefinition.From.AsReadOnly() : null);
			List<EntitySource> list2 = new List<EntitySource>();
			for (int i = 0; i < filterHost.Visuals.Count; i++)
			{
				PVVisual pvvisual = filterHost.Visuals[i];
				Formula formula = null;
				if (pvvisual != null && pvvisual.DataContext != null)
				{
					formula = pvvisual.DataContext.Formula;
				}
				FilterContainer filterContainer = null;
				PVProperty pvproperty;
				if (pvvisual.Properties == null)
				{
					pvproperty = null;
				}
				else
				{
					pvproperty = pvvisual.Properties.FirstOrDefault((PVProperty prop) => prop.Name == "FilterOutput" || prop.Name == "ScopedFilterOutput");
				}
				PVProperty pvproperty2 = pvproperty;
				if (pvproperty2 != null)
				{
					filterContainer = PVDocumentToExplorationConverter.ConvertPVVisualToFilterContainer(pvproperty2.Value.FilterValue, readOnlyCollection, list2, conceptualSchema, formula);
				}
				else
				{
					List<EntitySource> list3 = new List<EntitySource>();
					PVDocumentToExplorationConverter.ConvertedFormulaInfo convertedFormulaInfo = PVDocumentToExplorationConverter.FormulaToQueryExpression(new FilterFromManager(readOnlyCollection, list2, list3, conceptualSchema), formula, true);
					QueryExpressionContainer queryExpressionContainer = new QueryExpressionContainer(convertedFormulaInfo.QueryExpression, null, null);
					if (convertedFormulaInfo != null)
					{
						filterContainer = new FilterContainer
						{
							Field = new SQFieldDef
							{
								EntityName = convertedFormulaInfo.EntityName,
								FieldName = convertedFormulaInfo.FieldName
							},
							Expression = queryExpressionContainer,
							Filter = new FilterDefinition
							{
								From = list3
							}
						};
					}
				}
				if (filterContainer != null)
				{
					filterContainer.Type = PVDocumentToExplorationConverter.GetFilterContainerTypeFromVisual(pvvisual);
					list.Add(filterContainer);
				}
			}
			return list;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005FC0 File Offset: 0x000041C0
		public static QueryExpression ConvertFilterValue(FilterValue filterValue, string filterOperator)
		{
			switch (filterValue.Type)
			{
			case FilterValueType.String:
				return new QueryLiteralExpression
				{
					Value = PrimitiveValueEncoding.ToTypeEncodedString(filterValue.StringValue)
				};
			case FilterValueType.Number:
				return new QueryLiteralExpression
				{
					Value = PrimitiveValueEncoding.ToTypeEncodedString(filterValue.NumberValue)
				};
			case FilterValueType.NumberODataEncoded:
				return new QueryLiteralExpression
				{
					Value = filterValue.NumberODataEncodedValue
				};
			case FilterValueType.Bool:
				return new QueryLiteralExpression
				{
					Value = PrimitiveValueEncoding.ToTypeEncodedString(filterValue.BoolValue)
				};
			case FilterValueType.Null:
				return new QueryLiteralExpression
				{
					Value = PrimitiveValueEncoding.ToTypeEncodedString(PrimitiveValue.Null)
				};
			case FilterValueType.DateTime:
			{
				QueryLiteralExpression queryLiteralExpression = new QueryLiteralExpression
				{
					Value = PrimitiveValueEncoding.ToTypeEncodedString(filterValue.DateTimeValue)
				};
				if (string.Equals(filterOperator, "DateTimeEqualToSecond", StringComparison.OrdinalIgnoreCase))
				{
					return new QueryDateSpanExpression
					{
						Expression = queryLiteralExpression,
						TimeUnit = TimeUnit.Second
					};
				}
				return queryLiteralExpression;
			}
			default:
				return null;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000060BC File Offset: 0x000042BC
		internal static QueryComparisonKind ConvertOperatorToQueryComparisonKind(string operatorText)
		{
			string text = operatorText.ToLowerInvariant();
			if (text == "equal")
			{
				return QueryComparisonKind.Equal;
			}
			if (text == "greaterthan")
			{
				return QueryComparisonKind.GreaterThan;
			}
			if (text == "greaterthanorequal")
			{
				return QueryComparisonKind.GreaterThanOrEqual;
			}
			if (text == "lessthan")
			{
				return QueryComparisonKind.LessThan;
			}
			if (!(text == "lessthanorequal"))
			{
				return QueryComparisonKind.Equal;
			}
			return QueryComparisonKind.LessThanOrEqual;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006120 File Offset: 0x00004320
		private static PVDocumentToExplorationConverter.ConvertedFormulaInfo ConvertFilterCompound(PVFilter filter, FilterDefinition fd, IFromManager from)
		{
			if (filter.FilterConditions == null || filter.FilterConditions.Count == 0)
			{
				return null;
			}
			bool flag = filter.Operator == "NotAny" || filter.Operator == "NotAll";
			PVDocumentToExplorationConverter.ConvertedFormulaInfo convertedFormulaInfo = PVDocumentToExplorationConverter.ConvertFilter(filter.FilterConditions[0], fd, from);
			string entityName = convertedFormulaInfo.EntityName;
			QueryExpression queryExpression = convertedFormulaInfo.QueryExpression;
			for (int i = 1; i < filter.FilterConditions.Count; i++)
			{
				if (filter.Operator == "Any" || filter.Operator == "NotAny")
				{
					queryExpression = new QueryOrExpression
					{
						Left = queryExpression,
						Right = PVDocumentToExplorationConverter.ConvertFilter(filter.FilterConditions[i], fd, from).QueryExpression
					};
				}
				else if (filter.Operator == "All" || filter.Operator == "NotAll")
				{
					queryExpression = new QueryAndExpression
					{
						Left = queryExpression,
						Right = PVDocumentToExplorationConverter.ConvertFilter(filter.FilterConditions[i], fd, from).QueryExpression
					};
				}
			}
			if (flag)
			{
				queryExpression = new QueryNotExpression
				{
					Expression = queryExpression
				};
			}
			return new PVDocumentToExplorationConverter.ConvertedFormulaInfo
			{
				EntityName = convertedFormulaInfo.EntityName,
				FieldName = convertedFormulaInfo.FieldName,
				QueryExpression = queryExpression
			};
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006298 File Offset: 0x00004498
		public static PVDocumentToExplorationConverter.ConvertedFormulaInfo ConvertFilterBinary(PVFilter filter, FilterDefinition fd, IFromManager from)
		{
			PVDocumentToExplorationConverter.ConvertedFormulaInfo convertedFormulaInfo = PVDocumentToExplorationConverter.FormulaToQueryExpression(from, filter.LeftExpression, false);
			QueryExpression queryExpression = PVDocumentToExplorationConverter.ConvertFilterValue(filter.RightExpression, filter.Operator);
			QueryExpression queryExpression2;
			if (filter.Operator == "Contains")
			{
				queryExpression2 = new QueryContainsExpression
				{
					Left = convertedFormulaInfo.QueryExpression,
					Right = queryExpression
				};
			}
			else
			{
				queryExpression2 = new QueryComparisonExpression
				{
					ComparisonKind = PVDocumentToExplorationConverter.ConvertOperatorToQueryComparisonKind(filter.Operator),
					Left = convertedFormulaInfo.QueryExpression,
					Right = queryExpression
				};
			}
			if (filter.Not)
			{
				queryExpression2 = new QueryNotExpression
				{
					Expression = queryExpression2
				};
			}
			return new PVDocumentToExplorationConverter.ConvertedFormulaInfo
			{
				EntityName = convertedFormulaInfo.EntityName,
				FieldName = convertedFormulaInfo.FieldName,
				QueryExpression = queryExpression2
			};
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000636E File Offset: 0x0000456E
		private static PVDocumentToExplorationConverter.ConvertedFormulaInfo ConvertFilter(PVFilter filter, FilterDefinition fd, IFromManager from)
		{
			if (filter.Type == "Compound")
			{
				return PVDocumentToExplorationConverter.ConvertFilterCompound(filter, fd, from);
			}
			if (filter.Type == "Binary")
			{
				return PVDocumentToExplorationConverter.ConvertFilterBinary(filter, fd, from);
			}
			return null;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000063A7 File Offset: 0x000045A7
		private static ThemeManager GetThemeManager()
		{
			if (PVDocumentToExplorationConverter.themeManager == null)
			{
				Interlocked.Exchange<ThemeManager>(ref PVDocumentToExplorationConverter.themeManager, new ThemeManager());
			}
			return PVDocumentToExplorationConverter.themeManager;
		}

		// Token: 0x040000A9 RID: 169
		internal const string VisualTypeComboChart = "ComboChart";

		// Token: 0x040000AA RID: 170
		internal const string CategoryBucketName = "Category";

		// Token: 0x040000AB RID: 171
		internal const string SeriesBucketName = "Series";

		// Token: 0x040000AC RID: 172
		internal const string SizeBucketName = "Size";

		// Token: 0x040000AD RID: 173
		internal const string RowHierarchyBucketName = "RowHierarchy";

		// Token: 0x040000AE RID: 174
		internal const string ColumnHierarchyBucketName = "ColumnHierarchy";

		// Token: 0x040000AF RID: 175
		internal const string ValuesBucketName = "Values";

		// Token: 0x040000B0 RID: 176
		internal const string ExplorationVisualTypePieChart = "pieChart";

		// Token: 0x040000B1 RID: 177
		internal const string ExplorationVisualTypeFilledMap = "filledMap";

		// Token: 0x040000B2 RID: 178
		internal const string ExplorationVisualTypeScatterChart = "scatterChart";

		// Token: 0x040000B3 RID: 179
		internal const string ExplorationVisualTypeMatrix = "matrix";

		// Token: 0x040000B4 RID: 180
		internal const string ThemeNew1GradientColor1 = "#ccf1ee";

		// Token: 0x040000B5 RID: 181
		internal const string ThemeNew1GradientColor2 = "#01B8AA";

		// Token: 0x040000B6 RID: 182
		private static ThemeManager themeManager;

		// Token: 0x020000C3 RID: 195
		internal sealed class ConvertedFormulaInfo
		{
			// Token: 0x17000146 RID: 326
			// (get) Token: 0x0600040C RID: 1036 RVA: 0x000149A9 File Offset: 0x00012BA9
			// (set) Token: 0x0600040D RID: 1037 RVA: 0x000149B1 File Offset: 0x00012BB1
			internal QueryExpression QueryExpression { get; set; }

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x0600040E RID: 1038 RVA: 0x000149BA File Offset: 0x00012BBA
			// (set) Token: 0x0600040F RID: 1039 RVA: 0x000149C2 File Offset: 0x00012BC2
			internal string EntityName { get; set; }

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000410 RID: 1040 RVA: 0x000149CB File Offset: 0x00012BCB
			// (set) Token: 0x06000411 RID: 1041 RVA: 0x000149D3 File Offset: 0x00012BD3
			internal string FieldName { get; set; }
		}
	}
}
