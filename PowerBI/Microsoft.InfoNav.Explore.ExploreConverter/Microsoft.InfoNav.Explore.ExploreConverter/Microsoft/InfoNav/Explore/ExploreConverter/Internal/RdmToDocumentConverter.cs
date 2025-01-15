using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200005A RID: 90
	internal static class RdmToDocumentConverter
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x0000A138 File Offset: 0x00008338
		internal static PVDocumentRoot Convert(ReportArchive reportArchive)
		{
			RdmReport reportDefinition = reportArchive.ReportDefinition;
			PVVisual pvvisual = new PVVisual
			{
				Name = "DocumentRoot",
				Frame = new PVFrame
				{
					Height = 0m,
					Width = 0m,
					Position = new PVPosition
					{
						X = 0m,
						Y = 0m
					}
				},
				DataContext = new DataContext
				{
					Buckets = new List<Bucket>(),
					Type = "DataContext"
				},
				LayoutContext = new LayoutContext
				{
					Theme = "Theme1",
					Type = "Root"
				},
				Visuals = new List<PVVisual>(),
				Properties = new List<PVProperty>()
			};
			PVDocumentRoot pvdocumentRoot = new PVDocumentRoot
			{
				Document = new PVDocument
				{
					Context = new PVDocumentContext
					{
						ImageResourceMap = reportArchive.ImageResourceMap.ConvertToDocumentImageResourceMap()
					},
					RootVisual = pvvisual
				}
			};
			ReportState reportState = reportArchive.ReportState;
			if (reportState != null)
			{
				string theme = reportState.Theme;
				ReportSection reportSection = reportDefinition.ReportSections[0];
				if (theme != null)
				{
					pvvisual.LayoutContext = new LayoutContext
					{
						Theme = theme,
						Type = "Root"
					};
				}
			}
			PVVisual pvvisual2 = new PVVisual
			{
				Name = "default",
				ZIndex = 0,
				Frame = new PVFrame
				{
					Height = 0m,
					Width = 0m,
					Position = new PVPosition
					{
						X = 0m,
						Y = 0m
					}
				},
				DataContext = new DataContext
				{
					Buckets = new List<Bucket>(),
					Type = "DataContext"
				},
				LayoutContext = new LayoutContext(),
				Visuals = new List<PVVisual>(),
				Properties = new List<PVProperty>()
			};
			pvvisual.AddVisual(pvvisual2, false);
			ReportDeserializationContext reportDeserializationContext = new ReportDeserializationContext(pvdocumentRoot, reportDefinition, new Dictionary<PVVisual, Tuple<ReportItem, IRdlReportItemConverter>>());
			RdmToDocumentConverter.ConvertReportDefinition(pvdocumentRoot, reportDefinition, reportDeserializationContext, reportState);
			if (reportState != null)
			{
				RdmToDocumentConverter.ConvertReportState(pvdocumentRoot.Document, reportDefinition, reportState);
			}
			return pvdocumentRoot;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000A33C File Offset: 0x0000853C
		private static void ConvertReportDefinition(PVDocumentRoot documentRoot, RdmReport report, IReportDeserializationContext ctx, ReportState reportState)
		{
			PVVisual rootVisual = documentRoot.Document.RootVisual;
			int num = 0;
			ReportSectionState reportSectionState = null;
			if (reportState != null)
			{
				reportSectionState = reportState.ReportSections[num];
			}
			foreach (ReportSection reportSection in report.ReportSections)
			{
				PVVisual pvvisual = new PVVisual
				{
					Name = reportSection.Name,
					DataContext = new DataContext
					{
						Buckets = new List<Bucket>(),
						Type = "DataContext"
					},
					LayoutContext = new LayoutContext(),
					Visuals = new List<PVVisual>(),
					Properties = new List<PVProperty>()
				};
				pvvisual.Type = "Slide";
				pvvisual.Frame = new PVFrame
				{
					Position = new PVPosition
					{
						X = 0m,
						Y = 0m
					},
					Height = (decimal)reportSection.Height.PixelUnitsInFloat,
					Width = (decimal)reportSection.Width.PixelUnitsInFloat
				};
				BackgroundImage backgroundImage = reportSection.BackgroundImage;
				LayoutContext layoutContext;
				if (backgroundImage != null)
				{
					double num2 = backgroundImage.Transparency / 100.0;
					layoutContext = new LayoutContext
					{
						Type = "CanvasVisual"
					};
				}
				else
				{
					layoutContext = new LayoutContext
					{
						Type = "CanvasVisual"
					};
				}
				PVVisual pvvisual2 = new PVVisual
				{
					Name = reportSection.Name + "_Canvas",
					Frame = new PVFrame
					{
						Position = new PVPosition
						{
							X = 0m,
							Y = 0m
						},
						Height = (decimal)reportSection.Height.PixelUnitsInFloat,
						Width = (decimal)reportSection.Width.PixelUnitsInFloat
					},
					Visuals = new List<PVVisual>(),
					Properties = new List<PVProperty>(),
					DataContext = new DataContext
					{
						Buckets = new List<Bucket>(),
						Type = "DataContext"
					},
					LayoutContext = new LayoutContext()
				};
				pvvisual2.Type = "CanvasVisual";
				pvvisual2.LayoutContext = layoutContext;
				pvvisual.AddVisual(pvvisual2, false);
				List<PVVisual> list = new List<PVVisual>();
				if (reportState != null && num <= reportState.ReportSections.Count)
				{
					ctx.RegisterReportSectionState(reportSectionState);
				}
				using (List<ReportItem>.Enumerator enumerator2 = reportSection.ReportItems.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ReportItem reportItem = enumerator2.Current;
						ReportItemState reportItemState = reportState.ReportSections.Select((ReportSectionState s) => s.FindReportItem(reportItem.Name)).FirstOrDefault((ReportItemState s) => s != null);
						PVVisual pvvisual3 = RdmToDocumentConverter.CreateVisual(ctx, reportItem, reportItemState, 0.0m);
						pvvisual2.AddVisual(pvvisual3, false);
						PVProperty pvproperty = pvvisual3.Properties.FirstOrDefault((PVProperty prop) => prop.Name == "IsFailed");
						if (pvproperty == null || pvproperty.Value.BooleanValue)
						{
							list.Add(pvvisual3);
						}
					}
				}
				foreach (PVVisual pvvisual4 in list)
				{
					Tuple<ReportItem, IRdlReportItemConverter> creationContext = ctx.GetCreationContext(pvvisual4);
					ReportItem item = creationContext.Item1;
					IRdlReportItemConverter item2 = creationContext.Item2;
					if (item2 != null)
					{
						item2.SetProperties(pvvisual4);
						item2.SetOutputValues(ctx, pvvisual4, item);
					}
				}
				rootVisual.Visuals[0].AddVisual(pvvisual, false);
				if (reportState != null && ++num < reportState.ReportSections.Count)
				{
					ctx.UnregisterReportSectionState();
					reportSectionState = reportState.ReportSections[num];
				}
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A770 File Offset: 0x00008970
		internal static PVVisual CreateVisual(IReportDeserializationContext ctx, ReportItem reportItem, ReportItemState reportItemState, decimal verticalOffset = 0.0m)
		{
			Contract.Check(reportItem != null, "Expect reportItem to not be null");
			string name = reportItem.Name;
			string typeFromRdmReportItem = RdmToDocumentConverter.GetTypeFromRdmReportItem(reportItem);
			IRdlReportItemConverter rdlReportItemConverter = null;
			ReportParsingDiagnosticContext diagnosticContext = reportItem.DiagnosticContext;
			PVFrame pvframe = RdmToDocumentConverter.CreateFrame(reportItem);
			pvframe.Position.Y += verticalOffset;
			PVVisual pvvisual;
			if (!string.IsNullOrEmpty(typeFromRdmReportItem))
			{
				if (typeFromRdmReportItem == "Unknown")
				{
					pvvisual = RdmToDocumentConverter.CreateFailedVisual(name, pvframe, "Unknown");
				}
				else
				{
					rdlReportItemConverter = RdlReportItemConverterFactory.CreateRdlReportItemConverter(typeFromRdmReportItem);
					if (rdlReportItemConverter != null && rdlReportItemConverter.IsEnabled())
					{
						PVVisual pvvisual2 = new PVVisual
						{
							Name = name,
							Type = typeFromRdmReportItem,
							Frame = pvframe,
							ZIndex = reportItem.ZIndex,
							Visuals = new List<PVVisual>(),
							LayoutContext = new LayoutContext(),
							DataContext = new DataContext
							{
								Buckets = new List<Bucket>(),
								Type = "DataContext"
							},
							Properties = new List<PVProperty>()
						};
						bool flag = false;
						try
						{
							rdlReportItemConverter.Load(ctx, reportItem, pvvisual2);
							flag = true;
						}
						catch (InvalidOperationException)
						{
							diagnosticContext.AddError("ConverterFailed", new string[0]);
						}
						if (flag)
						{
							List<Filter> list = new List<Filter>();
							if (reportItemState != null)
							{
								foreach (Filter filter in reportItemState.Filters)
								{
									if (filter.IsDrilldownFilter)
									{
										list.Add(filter);
									}
								}
							}
							rdlReportItemConverter.SetDrill(pvvisual2, list);
						}
						if (reportItem.DiagnosticContext.HasError)
						{
							pvvisual = RdmToDocumentConverter.CreateFailedVisual(name, pvframe, typeFromRdmReportItem);
						}
						else
						{
							pvvisual = pvvisual2;
						}
					}
					else
					{
						diagnosticContext.AddError("MissingConverter", new string[0]);
						pvvisual = RdmToDocumentConverter.CreateFailedVisual(name, pvframe, typeFromRdmReportItem);
					}
				}
			}
			else
			{
				diagnosticContext.AddError("UnknownVisualType", new string[0]);
				pvvisual = RdmToDocumentConverter.CreateFailedVisual(name, pvframe, "Unknown");
			}
			ctx.Register(pvvisual, new Tuple<ReportItem, IRdlReportItemConverter>(reportItem, rdlReportItemConverter));
			return pvvisual;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A980 File Offset: 0x00008B80
		internal static string GetTypeFromRdmReportItem(ReportItem reportItem)
		{
			string rdlTagName = reportItem.RdlTagName;
			if (rdlTagName == "Chart")
			{
				Chart chart = reportItem as Chart;
				switch (ChartRdlReportItemConverter.ConvertChartType(chart.Type, chart.Subtype))
				{
				case ChartLayoutType.Bar:
				case ChartLayoutType.BarPercentStacked:
				case ChartLayoutType.BarStacked:
				case ChartLayoutType.Column:
				case ChartLayoutType.ColumnPercentStacked:
				case ChartLayoutType.ColumnStacked:
					return "CategoryChart";
				case ChartLayoutType.DecisionTree:
					return "DecisionTree";
				case ChartLayoutType.Funnel:
					return "Funnel";
				case ChartLayoutType.Line:
					return "Line";
				case ChartLayoutType.Map:
					return "Map";
				case ChartLayoutType.Pie:
					return "Pie";
				case ChartLayoutType.Scatter:
					return "Scatter";
				case ChartLayoutType.Sunburst:
					return "Sunburst";
				case ChartLayoutType.TreeMap:
					return "TreeMap";
				}
				throw new InvalidOperationException("Unknown ChartType");
			}
			if (rdlTagName == "Tablix")
			{
				Tablix tablix = reportItem as Tablix;
				string subType = tablix.SubType;
				if ("Matrix" == subType)
				{
					BandLayoutOptions bandLayoutOptions = tablix.BandLayoutOptions;
					if (bandLayoutOptions != null)
					{
						Navigation navigation = bandLayoutOptions.Navigation;
						if (navigation != null)
						{
							if (navigation.NavigationType == NavigationType.PlayAxis)
							{
								return "Play";
							}
							return "Band";
						}
					}
					return "Matrix";
				}
				if ("Table" == subType)
				{
					if (tablix.IsCallout)
					{
						return "Card";
					}
					return "Table";
				}
				else
				{
					if ("Filter" == subType)
					{
						return "Slicer";
					}
					if ("SmallMultiple" == subType)
					{
						return "SmallMultiple";
					}
					return "Unknown";
				}
			}
			else
			{
				if (rdlTagName == "Textbox")
				{
					return "Textbox";
				}
				if (rdlTagName == "Image")
				{
					return "Image";
				}
				return "Unknown";
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000AB2C File Offset: 0x00008D2C
		private static void ConvertReportState(PVDocument document, RdmReport report, ReportState reportState)
		{
			PVVisual rootVisual = document.RootVisual;
			if (rootVisual.Visuals[0].Visuals.Count > reportState.ReportSections.Count && reportState.ReportSections.Count > 0)
			{
				throw new InvalidOperationException("Expect Report.Sections to be the same count as the ReportState.ReportSections");
			}
			int num = 0;
			List<PVVisual> visuals = rootVisual.Visuals[0].Visuals;
			PVVisual pvvisual = visuals[0];
			foreach (ReportSectionState reportSectionState in reportState.ReportSections)
			{
				ReportSection reportSection = report.ReportSections[num];
				if (pvvisual != null && pvvisual.Name == reportSection.Name)
				{
					string text = "Filter_" + num.ToString();
					PVVisual pvvisual2 = new PVVisual
					{
						Name = text,
						Properties = new List<PVProperty>(),
						Visuals = new List<PVVisual>()
					};
					pvvisual2.Type = "FilterVisual";
					PVVisual pvvisual3 = pvvisual;
					PVVisual pvvisual4 = pvvisual3.Visuals[0];
					pvvisual3.AddVisual(pvvisual2, false);
					int num2 = 0;
					foreach (Filter filter in reportSectionState.Filters)
					{
						if (RdmToDocumentConverter.AddNewFilterVisualToDocument(text + "_" + num2.ToString(), filter, document, pvvisual4) != null)
						{
							num2++;
						}
					}
					foreach (PVVisual pvvisual5 in pvvisual4.Visuals)
					{
						RdmToDocumentConverter.ConvertReportStateForVisual(document, reportSectionState, pvvisual5);
					}
					if (num + 1 < visuals.Count)
					{
						pvvisual = visuals[num + 1];
					}
					else
					{
						pvvisual = null;
					}
				}
				num++;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000AD54 File Offset: 0x00008F54
		private static void ConvertReportStateForVisual(PVDocument document, ReportSectionState sectionState, PVVisual visual)
		{
			bool flag = true;
			string name = visual.Name;
			ReportItemState reportItemState = sectionState.FindReportItem(name);
			if (visual.Type == "CanvasVisual")
			{
				flag = false;
			}
			bool flag2 = false;
			if (flag)
			{
				IRdlReportItemConverter rdlReportItemConverter = RdlReportItemConverterFactory.CreateRdlReportItemConverter(visual.Type);
				if (rdlReportItemConverter != null && rdlReportItemConverter.SupportsTargetedScopeFilters(visual))
				{
					flag2 = true;
				}
			}
			if (flag)
			{
				string text = "FilterVisual_" + visual.Name;
				PVVisual pvvisual = new PVVisual
				{
					Name = text,
					Properties = new List<PVProperty>(),
					Visuals = new List<PVVisual>()
				};
				pvvisual.Type = "FilterVisual";
				visual.AddVisual(pvvisual, false);
			}
			foreach (PVVisual pvvisual2 in visual.Visuals)
			{
				if (pvvisual2.Type != "FilterVisual")
				{
					RdmToDocumentConverter.ConvertReportStateForVisual(document, sectionState, pvvisual2);
				}
			}
			if (flag)
			{
				int num = 0;
				if (reportItemState != null && !reportItemState.DiagnosticContext.HasError)
				{
					foreach (Filter filter in reportItemState.Filters)
					{
						string text2 = "FilterCard_";
						if (filter.IsMeasureFilter)
						{
							if (!flag2)
							{
								continue;
							}
							text2 = "MeasureFilter_";
						}
						text2 = text2 + name + "_" + num.ToString(CultureInfo.InvariantCulture);
						if (RdmToDocumentConverter.AddNewFilterVisualToDocument(text2, filter, document, visual) != null)
						{
							num++;
						}
					}
				}
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000AEF8 File Offset: 0x000090F8
		private static string GetFilterVisualTypeFromDataType(string dataType)
		{
			if ("Decimal" == dataType || "Int64" == dataType || "Double" == dataType || "DateTime" == dataType)
			{
				return "AdvancedFilterCard";
			}
			return "CategoryFilterCard";
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000AF44 File Offset: 0x00009144
		private static string GetVisualType(Filter filter)
		{
			switch (filter.Mode)
			{
			case FilterMode.Cleared:
			{
				string type = filter.Type;
				if (type != null)
				{
					return RdmToDocumentConverter.GetFilterVisualTypeFromDataType(type);
				}
				return null;
			}
			case FilterMode.List:
				return "CategoryFilterCard";
			case FilterMode.Advanced:
				return "AdvancedFilterCard";
			case FilterMode.Range:
				if (!filter.IsMeasureFilter)
				{
					return "RangeFilterCard";
				}
				return "MeasureRangeFilterCard";
			default:
				return null;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000AFA4 File Offset: 0x000091A4
		private static PVVisual AddNewFilterVisualToDocument(string name, Filter filter, PVDocument document, PVVisual bindingVisual)
		{
			PVVisual pvvisual = new PVVisual
			{
				Name = name,
				Properties = new List<PVProperty>(),
				Visuals = new List<PVVisual>()
			};
			string visualType = RdmToDocumentConverter.GetVisualType(filter);
			if (visualType == null)
			{
				return null;
			}
			RdlReportItemConverterFactory.CreateRdlReportItemConverter(bindingVisual.Type).GetParentForFilter(bindingVisual, filter).AddVisual(pvvisual, false);
			PVDocumentContext context = document.Context;
			pvvisual.Type = visualType;
			pvvisual.DataContext = new DataContext
			{
				Formula = QueryExpressionAnalyzer.CreateFormula(filter.Operand)
			};
			CompoundFilterCondition<IRdmQueryExpression> filterCondition = filter.FilterCondition;
			if (filterCondition != null)
			{
				bool flag = pvvisual.ParentVisual.ParentVisual.Type != "Slide";
				FormulaEdmReferenceKind? edmReferenceKind = pvvisual.DataContext.Formula.EdmReferenceKind;
				FormulaEdmReferenceKind formulaEdmReferenceKind = FormulaEdmReferenceKind.MeasureProperty;
				bool flag2 = ((edmReferenceKind.GetValueOrDefault() == formulaEdmReferenceKind) & (edmReferenceKind != null)) || pvvisual.DataContext.Formula.Function != null;
				if (flag && flag2)
				{
					pvvisual.Properties.Add(new PVProperty
					{
						Name = "ScopedFilterOutput",
						Value = new CustomPVProperties
						{
							FilterValue = FilterConverter.ConvertFilter(FormulaFilterConditionTransform.GetResult(filterCondition))
						}
					});
				}
				else
				{
					RdmToDocumentConverter.SetFilterOutputProperty(pvvisual, filterCondition);
				}
			}
			return pvvisual;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000B0D0 File Offset: 0x000092D0
		private static PVFrame CreateFrame(ReportItem reportItem)
		{
			decimal num = ((reportItem.Rect.Top != null) ? reportItem.Rect.Top.PixelUnits : 0m);
			decimal num2 = ((reportItem.Rect.Left != null) ? reportItem.Rect.Left.PixelUnits : 0m);
			decimal num3 = reportItem.Rect.Size.Width.PixelUnits;
			decimal num4 = reportItem.Rect.Size.Height.PixelUnits;
			return new PVFrame
			{
				Height = num4,
				Width = num3,
				Position = new PVPosition
				{
					X = num2,
					Y = num
				}
			};
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000B194 File Offset: 0x00009394
		private static PVVisual CreateFailedVisual(string name, PVFrame frame, string type)
		{
			return new PVVisual
			{
				Name = name,
				Type = type,
				Properties = new List<PVProperty>
				{
					new PVProperty
					{
						Name = "IsFailed",
						Value = new CustomPVProperties
						{
							BooleanValue = true
						}
					}
				},
				ZIndex = 0,
				DataContext = new DataContext
				{
					Buckets = new List<Bucket>(),
					Type = "DataContext"
				},
				LayoutContext = new LayoutContext(),
				Visuals = new List<PVVisual>(),
				Frame = frame
			};
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000B22C File Offset: 0x0000942C
		public static void SetFilterOutputProperty(PVVisual visual, IFilterCondition<IRdmQueryExpression> filterCondition)
		{
			IFilterCondition<Formula> result = FormulaFilterConditionTransform.GetResult(filterCondition);
			Contract.Check(result != null, "If the slicer has a filter condition we should be able to convert it to formula filter condition.");
			RdmToDocumentConverter.SetFilterOutputProperty(visual, result);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000B258 File Offset: 0x00009458
		public static void SetFilterOutputProperty(PVVisual visual, IFilterCondition<Formula> formulaFilterCondition)
		{
			PVFilter pvfilter = FilterConverter.ConvertFilter(formulaFilterCondition);
			visual.Properties.Add(new PVProperty
			{
				Name = "FilterOutput",
				Value = new CustomPVProperties
				{
					FilterValue = pvfilter
				}
			});
		}
	}
}
