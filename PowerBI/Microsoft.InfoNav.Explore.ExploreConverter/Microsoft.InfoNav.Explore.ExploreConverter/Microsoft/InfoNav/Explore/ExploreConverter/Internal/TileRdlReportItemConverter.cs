using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000043 RID: 67
	internal sealed class TileRdlReportItemConverter : ContainerRdlReportItemConverter
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00009764 File Offset: 0x00007964
		internal TileRdlReportItemConverter()
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000976C File Offset: 0x0000796C
		public override void Load(IReportDeserializationContext ctx, ReportItem reportItem, PVVisual visual)
		{
			Tablix tablix = reportItem as Tablix;
			Contract.Check(tablix != null, "Expect reportItem to be Tablix");
			string text = visual.Name + "canvas";
			string text2 = visual.Name + "slicer";
			visual.Type = "Subview";
			visual.DataContext = null;
			PVVisual pvvisual = new PVVisual
			{
				Name = text,
				Type = "CanvasVisual",
				DataContext = new DataContext
				{
					Buckets = new List<Bucket>(),
					Type = "DataContext"
				},
				Visuals = new List<PVVisual>(),
				LayoutContext = new LayoutContext(),
				Properties = new List<PVProperty>()
			};
			visual.AddVisual(pvvisual, false);
			using (new DataSetScope(ctx, tablix))
			{
				PVVisual pvvisual2 = this.CreateSlicerTablix(text2, ctx, reportItem);
				pvvisual.AddVisual(pvvisual2, false);
				this.LoadDataContext(ctx, tablix, pvvisual);
				int num = -1;
				foreach (PVVisual pvvisual3 in pvvisual.Visuals)
				{
					if (pvvisual3 != pvvisual2)
					{
						num = Math.Max(num, pvvisual3.ZIndex);
					}
				}
				if (num >= pvvisual2.ZIndex)
				{
					pvvisual2.ZIndex = num + 1;
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000098D4 File Offset: 0x00007AD4
		public override void SetOutputValues(IReportDeserializationContext ctx, PVVisual visual, ReportItem reportItem)
		{
			List<PVVisual> visuals = this.GetCanvasVisual(visual).Visuals;
			int i = 1;
			Contract.Check(visuals[0].Type == "Slicer", "First visual must be the tile slicer.");
			while (i < visuals.Count)
			{
				PVVisual pvvisual = visuals[i];
				Tuple<ReportItem, IRdlReportItemConverter> creationContext = ctx.GetCreationContext(pvvisual);
				IRdlReportItemConverter item = creationContext.Item2;
				if (item != null)
				{
					item.SetOutputValues(ctx, pvvisual, creationContext.Item1);
				}
				i++;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000994C File Offset: 0x00007B4C
		public override void LoadDataContext(IReportDeserializationContext ctx, Tablix tablix, PVVisual visual)
		{
			Bucket bucket = new Bucket
			{
				BucketItems = new List<BucketItem>(),
				Properties = new List<BucketProperty>()
			};
			Bucket bucket2 = new Bucket
			{
				BucketItems = new List<BucketItem>(),
				Properties = new List<BucketProperty>()
			};
			base.RowIndex = 0;
			base.ColumnIndex = 0;
			Contract.Check(tablix.RowHierarchy.Members.Count == 1, "the row size of a Tile should be 1");
			Dictionary<ReportImageSource, int> dictionary = new Dictionary<ReportImageSource, int>();
			foreach (TablixMember tablixMember in tablix.ColumnHierarchy.Members)
			{
				base.ConvertTablixColumnHierarchy(ctx, tablix, visual, tablixMember, bucket2, bucket, dictionary);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009A18 File Offset: 0x00007C18
		public override void ConvertTablixBodyCell(IReportDeserializationContext ctx, TablixCell cell, Tablix tablix, PVVisual visual, Bucket valuesBucket)
		{
			Contract.Check(valuesBucket != null, "Expecting valuesBucket to not be null");
			ReportItem reportItem = cell.ReportItem;
			Contract.Check(reportItem.RdlTagName == "Rectangle", "Tile must contain a Rectangle.");
			Rectangle rectangle = reportItem as Rectangle;
			Contract.Check(rectangle != null, "Rectangle should not be null");
			List<ReportItem> reportItems = rectangle.ReportItems;
			decimal y = this.GetSubviewLayout(tablix).Item2.Position.Y;
			foreach (ReportItem reportItem2 in rectangle.ReportItems)
			{
				PVVisual pvvisual = RdmToDocumentConverter.CreateVisual(ctx, reportItem2, base.GetReportItemState(ctx, reportItem2), y);
				visual.AddVisual(pvvisual, false);
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009AE0 File Offset: 0x00007CE0
		internal PVVisual CreateSlicerTablix(string name, IReportDeserializationContext ctx, ReportItem reportItem)
		{
			PVVisual pvvisual = new PVVisual();
			pvvisual.Name = name;
			pvvisual.DataContext = new DataContext
			{
				Buckets = new List<Bucket>(),
				Type = "DataContext"
			};
			pvvisual.Visuals = new List<PVVisual>();
			pvvisual.LayoutContext = new LayoutContext();
			pvvisual.Properties = new List<PVProperty>();
			Tablix tablix = reportItem as Tablix;
			Contract.Check(tablix != null, "Expect reportItem to be Tablix");
			LabelData labelData = this.GetLabelData(tablix);
			Contract.Check(labelData != null, "Expect labelData to not be null");
			Tuple<PVFrame, PVFrame> subviewLayout = this.GetSubviewLayout(tablix);
			Contract.Check(subviewLayout != null, "Expect subviewLayout to not be null");
			Image slicerImage = this.GetSlicerImage(tablix);
			pvvisual.Type = "Slicer";
			pvvisual.Frame = subviewLayout.Item1;
			Tuple<Formula, Formula> slicerValueFormula = this.GetSlicerValueFormula(ctx, labelData, slicerImage);
			Contract.Check(slicerValueFormula != null, "Expect slicerInfo to not be null");
			DataContext dataContext = pvvisual.DataContext;
			Bucket bucket = new Bucket
			{
				Name = "Values",
				BucketItems = new List<BucketItem>(),
				Properties = new List<BucketProperty>()
			};
			BucketItem bucketItem = new BucketItem
			{
				Formula = slicerValueFormula.Item1
			};
			bucket.BucketItems.Add(bucketItem);
			if (slicerValueFormula.Item2 != null)
			{
				BucketItem bucketItem2 = new BucketItem
				{
					Formula = slicerValueFormula.Item2
				};
				bucket.BucketItems.Add(bucketItem2);
			}
			dataContext.Buckets.Add(bucket);
			return pvvisual;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009C3C File Offset: 0x00007E3C
		internal LayoutContext CreateSlicerLayoutContext(Tablix tablixTileReportItem)
		{
			List<PVColumnInfo> list = new List<PVColumnInfo>();
			string empty = string.Empty;
			list.Add(new PVColumnInfo
			{
				CustomFormatString = empty
			});
			return new LayoutContext
			{
				Columns = list
			};
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009C74 File Offset: 0x00007E74
		internal Tuple<Formula, Formula> GetSlicerValueFormula(IReportDeserializationContext ctx, LabelData labelData, Image image)
		{
			Formula formula = null;
			string text = null;
			string labelDataField = labelData.LabelDataField;
			if (image != null)
			{
				Expression valueAsExpression = image.Source.ValueAsExpression;
				if (valueAsExpression != null)
				{
					text = valueAsExpression.FieldName;
					if (text != null && labelDataField != null)
					{
						formula = base.ParseFormula(ctx, labelDataField);
					}
				}
			}
			if (text == null)
			{
				text = labelDataField;
			}
			if (text == null)
			{
				text = labelData.KeyField;
			}
			Contract.Check(text != null, "Expect fieldName to not be null");
			return new Tuple<Formula, Formula>(base.ParseFormula(ctx, text), formula);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009CE0 File Offset: 0x00007EE0
		internal LabelData GetLabelData(Tablix tablix)
		{
			BandLayoutOptions bandLayoutOptions = tablix.BandLayoutOptions;
			Contract.Check(bandLayoutOptions != null, "Expect layoutOptions to not be null");
			Navigation navigation = bandLayoutOptions.Navigation;
			Contract.Check(navigation != null, "Expect supportsSlider to not be null");
			Slider slider = navigation.Slider;
			Contract.Check(slider != null, "Expect slider to not be null");
			return slider.LabelData;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009D2C File Offset: 0x00007F2C
		internal Image GetSlicerImage(Tablix tablix)
		{
			List<TablixMember> members = tablix.RowHierarchy.Members;
			Contract.Check(members.Count == 1, "Slicer must have one and only one row member.");
			TablixHeader tablixHeader = members[0].TablixHeader;
			if (tablixHeader != null)
			{
				ReportItem cellContents = tablixHeader.CellContents;
				if (cellContents != null)
				{
					Image image = cellContents as Image;
					if (image != null)
					{
						return image;
					}
				}
			}
			return null;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009D80 File Offset: 0x00007F80
		internal Tuple<PVFrame, PVFrame> GetSubviewLayout(Tablix tablix)
		{
			bool flag = this.GetSlicerImage(tablix) != null;
			SubviewSlicerDockStyle subviewSlicerDockStyle = SubviewSlicerDockStyle.Undefined;
			NavigationType navigationType = tablix.BandLayoutOptions.Navigation.NavigationType;
			if (navigationType != NavigationType.Tabstrip)
			{
				if (navigationType == NavigationType.Coverflow)
				{
					subviewSlicerDockStyle = SubviewSlicerDockStyle.Bottom;
				}
			}
			else
			{
				subviewSlicerDockStyle = SubviewSlicerDockStyle.Top;
			}
			Contract.Check(SubviewSlicerDockStyle.Undefined != subviewSlicerDockStyle, "Unrecognized navigation type in Tile");
			Tuple<PVFrame, PVFrame> tuple = null;
			float num = this.GetSlicerHeight(subviewSlicerDockStyle, flag);
			float pixelUnitsInFloat = tablix.Rect.Size.Width.PixelUnitsInFloat;
			float pixelUnitsInFloat2 = tablix.Rect.Size.Height.PixelUnitsInFloat;
			if (num > pixelUnitsInFloat2)
			{
				num = pixelUnitsInFloat2;
			}
			float num2 = 10f;
			if (subviewSlicerDockStyle == SubviewSlicerDockStyle.Top)
			{
				PVFrame pvframe = new PVFrame();
				pvframe.Position = new PVPosition
				{
					X = 0m,
					Y = (decimal)num2
				};
				pvframe.Height = (decimal)num;
				pvframe.Width = (decimal)pixelUnitsInFloat;
				PVFrame pvframe2 = new PVFrame
				{
					Position = new PVPosition
					{
						X = 0m,
						Y = (decimal)(num2 + num)
					},
					Height = (decimal)(pixelUnitsInFloat2 - num),
					Width = (decimal)pixelUnitsInFloat
				};
				tuple = new Tuple<PVFrame, PVFrame>(pvframe, pvframe2);
			}
			else if (subviewSlicerDockStyle == SubviewSlicerDockStyle.Bottom)
			{
				PVFrame pvframe3 = new PVFrame();
				pvframe3.Position = new PVPosition
				{
					X = 0m,
					Y = (decimal)(pixelUnitsInFloat2 - num)
				};
				pvframe3.Height = (decimal)num;
				pvframe3.Width = (decimal)pixelUnitsInFloat;
				PVFrame pvframe4 = new PVFrame
				{
					Position = new PVPosition
					{
						X = 0m,
						Y = 0m
					},
					Height = (decimal)(pixelUnitsInFloat2 - num),
					Width = (decimal)pixelUnitsInFloat
				};
				tuple = new Tuple<PVFrame, PVFrame>(pvframe3, pvframe4);
			}
			return tuple;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009F4C File Offset: 0x0000814C
		internal float GetSlicerHeight(SubviewSlicerDockStyle slicerStyle, bool hasImage)
		{
			if (slicerStyle == SubviewSlicerDockStyle.Undefined)
			{
				return 0f;
			}
			float num = 40f;
			float num2 = 68f;
			float num3 = 25f;
			if (!hasImage)
			{
				return num3 + num;
			}
			return num2;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00009F80 File Offset: 0x00008180
		internal PVVisual GetCanvasVisual(PVVisual parent)
		{
			List<PVVisual> list = parent.Visuals.Where((PVVisual visual) => visual.Type == "CanvasVisual").ToList<PVVisual>();
			Contract.Check(list.Count == 1, "Wrong number of canvas visuals");
			return list[0];
		}
	}
}
