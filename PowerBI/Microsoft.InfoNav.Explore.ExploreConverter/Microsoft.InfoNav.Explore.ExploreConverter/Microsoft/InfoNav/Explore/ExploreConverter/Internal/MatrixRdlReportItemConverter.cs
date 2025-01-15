using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000039 RID: 57
	internal sealed class MatrixRdlReportItemConverter : MatrixRdlReportItemConverterBase
	{
		// Token: 0x0600018B RID: 395 RVA: 0x000079FC File Offset: 0x00005BFC
		public override bool SupportsTargetedScopeFilters(PVVisual visual)
		{
			return visual.DataContext.Buckets.Where((Bucket bucket) => bucket.Name == "RowHierarchy").Any((Bucket bucket) => bucket.BucketItems.Count > 0);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007A5C File Offset: 0x00005C5C
		public override void SetOutputValues(IReportDeserializationContext ctx, PVVisual visual, ReportItem reportItem)
		{
			Tablix tablix = reportItem as Tablix;
			using (new DataSetScope(ctx, tablix))
			{
				DataSet currentDataSet = ctx.GetCurrentDataSet();
				TablixHierarchy rowHierarchy = tablix.RowHierarchy;
				Contract.Check(rowHierarchy != null, "RowHierarchy of Matrix must not be null");
				Dictionary<Formula, SortDirection> dictionary = new Dictionary<Formula, SortDirection>();
				base.LoadSortsFromMembers(rowHierarchy.Members, ctx, currentDataSet, dictionary);
				if (dictionary.Count > 0)
				{
					List<PVProperty> properties = visual.Properties;
					PVProperty pvproperty = new PVProperty();
					pvproperty.Name = "SelectedSort";
					CustomPVProperties customPVProperties = new CustomPVProperties();
					customPVProperties.SortValue = (from kvp in dictionary.ToList<KeyValuePair<Formula, SortDirection>>()
						select new PVPropertyValue
						{
							Direction = kvp.Value.ToString(),
							Formula = kvp.Key
						}).ToList<PVPropertyValue>();
					pvproperty.Value = customPVProperties;
					properties.Add(pvproperty);
				}
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007B30 File Offset: 0x00005D30
		public override void Load(IReportDeserializationContext ctx, ReportItem reportItem, PVVisual visual)
		{
			Tablix tablix = reportItem as Tablix;
			Contract.Check(tablix != null, "Expect reportItem to be Tablix");
			LayoutContext layoutContext = this.CreateLayoutContext(tablix);
			visual.LayoutContext = layoutContext;
			this.LoadDataContext(ctx, tablix, visual);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007B6C File Offset: 0x00005D6C
		public override void ConvertTablixBodyCell(IReportDeserializationContext ctx, TablixCell cell, Tablix tablix, PVVisual visual, Bucket valuesBucket)
		{
			ReportItem reportItem = cell.ReportItem;
			if (reportItem == null)
			{
				return;
			}
			base.AddToBucketIfNotNull(ctx, reportItem, valuesBucket);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007B90 File Offset: 0x00005D90
		internal void LoadDataContext(IReportDeserializationContext ctx, Tablix tablix, PVVisual visual)
		{
			DataContext dataContext = visual.DataContext;
			using (new DataSetScope(ctx, tablix))
			{
				Bucket bucket = new Bucket
				{
					Name = "RowHierarchy",
					BucketItems = new List<BucketItem>(),
					Properties = new List<BucketProperty>()
				};
				dataContext.Buckets.Add(bucket);
				if (tablix.RowHierarchy.IsSubtotals)
				{
					bucket.Properties.Add(new BucketProperty
					{
						Name = "Subtotal",
						Value = true
					});
				}
				if (tablix.RowHierarchy.EnableDrilling)
				{
					bucket.Properties.Add(new BucketProperty
					{
						Name = "EnableDrilling",
						Value = true
					});
				}
				Bucket bucket2 = new Bucket
				{
					Name = "ColumnHierarchy",
					BucketItems = new List<BucketItem>(),
					Properties = new List<BucketProperty>()
				};
				dataContext.Buckets.Add(bucket2);
				if (tablix.ColumnHierarchy.IsSubtotals)
				{
					bucket2.Properties.Add(new BucketProperty
					{
						Name = "Subtotal",
						Value = true
					});
				}
				if (tablix.ColumnHierarchy.EnableDrilling)
				{
					bucket2.Properties.Add(new BucketProperty
					{
						Name = "EnableDrilling",
						Value = true
					});
				}
				Bucket bucket3 = new Bucket
				{
					Name = "Values",
					BucketItems = new List<BucketItem>(),
					Properties = new List<BucketProperty>()
				};
				TablixBody body = tablix.Body;
				Dictionary<ReportImageSource, int> dictionary = new Dictionary<ReportImageSource, int>();
				base.RowIndex = 0;
				base.ColumnIndex = 0;
				foreach (TablixMember tablixMember in tablix.RowHierarchy.Members)
				{
					base.ConvertTablixRowHierarchy(ctx, tablix, visual, tablixMember, bucket3, bucket, bucket2, dictionary);
				}
				dataContext.Buckets.Add(bucket3);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007DA8 File Offset: 0x00005FA8
		public LayoutContext CreateLayoutContext(Tablix reportItem)
		{
			List<PVColumnInfo> list = new List<PVColumnInfo>();
			int num = 0;
			List<TablixRow> tablixRows = reportItem.Body.TablixRows;
			foreach (ReportSize reportSize in reportItem.Body.TablixColumns)
			{
				int pixelUnits = reportSize.PixelUnits;
				string text = string.Empty;
				if (tablixRows[0].TablixCells.Count > num)
				{
					Textbox textbox = tablixRows[0].TablixCells[num].ReportItem as Textbox;
					if (textbox != null && textbox.FormatType == FormatType.Manual)
					{
						text = textbox.CustomFormatString;
					}
				}
				list.Add(new PVColumnInfo
				{
					CustomFormatString = text
				});
				num++;
			}
			List<float> list2 = new List<float>();
			foreach (TablixRow tablixRow in reportItem.Body.TablixRows)
			{
				float pixelUnitsInFloat = tablixRow.Height.PixelUnitsInFloat;
				list2.Add(pixelUnitsInFloat);
			}
			float pixelUnitsInFloat2 = reportItem.Rect.Size.MinWidth.PixelUnitsInFloat;
			float pixelUnitsInFloat3 = reportItem.Rect.Size.MaxWidth.PixelUnitsInFloat;
			float pixelUnitsInFloat4 = reportItem.Rect.Size.MinHeight.PixelUnitsInFloat;
			float pixelUnitsInFloat5 = reportItem.Rect.Size.MaxHeight.PixelUnitsInFloat;
			return new LayoutContext
			{
				Columns = list
			};
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007F38 File Offset: 0x00006138
		internal List<TablixHeaderInfo> GetHierarchyHeaderSizes(TablixHierarchy hierarchy)
		{
			List<TablixHeaderInfo> list = new List<TablixHeaderInfo>();
			if (hierarchy != null)
			{
				foreach (TablixMember tablixMember in hierarchy.Members)
				{
					list.Add(this.GetMemberHeaderSizes(tablixMember));
				}
			}
			return list;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007F9C File Offset: 0x0000619C
		internal TablixHeaderInfo GetMemberHeaderSizes(TablixMember member)
		{
			TablixHeaderInfo tablixHeaderInfo = null;
			TablixHeader tablixHeader = member.TablixHeader;
			if (tablixHeader != null)
			{
				Size size = tablixHeader.CellContents.Rect.Size;
				tablixHeaderInfo = new TablixHeaderInfo(size.Width.PixelUnitsInFloat, size.MinWidth.PixelUnitsInFloat, size.MaxWidth.PixelUnitsInFloat);
				foreach (TablixMember tablixMember in member.Members)
				{
					tablixHeaderInfo.ChildHeaderInfos.Add(this.GetMemberHeaderSizes(tablixMember));
				}
			}
			if (tablixHeaderInfo == null)
			{
				tablixHeaderInfo = new TablixHeaderInfo(0f, 0f, 0f);
			}
			return tablixHeaderInfo;
		}
	}
}
