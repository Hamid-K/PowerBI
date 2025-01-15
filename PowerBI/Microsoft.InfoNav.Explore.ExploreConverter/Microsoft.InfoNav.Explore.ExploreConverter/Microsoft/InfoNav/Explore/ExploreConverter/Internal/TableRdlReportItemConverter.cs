using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000041 RID: 65
	internal class TableRdlReportItemConverter : BaseRdlReportItemConverter
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00008F48 File Offset: 0x00007148
		public override bool SupportsTargetedScopeFilters(PVVisual visual)
		{
			Bucket bucket2 = visual.DataContext.Buckets.FirstOrDefault((Bucket bucket) => bucket.Name == "Values");
			if (bucket2 == null)
			{
				return false;
			}
			List<BucketItem> bucketItems = bucket2.BucketItems;
			bool flag = false;
			foreach (BucketItem bucketItem in bucketItems)
			{
				if (bucketItem.Formula.Function == null)
				{
					FormulaEdmReferenceKind? edmReferenceKind = bucketItem.Formula.EdmReferenceKind;
					FormulaEdmReferenceKind formulaEdmReferenceKind = FormulaEdmReferenceKind.MeasureProperty;
					if (!((edmReferenceKind.GetValueOrDefault() == formulaEdmReferenceKind) & (edmReferenceKind != null)))
					{
						continue;
					}
				}
				flag = true;
				break;
			}
			return flag;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009004 File Offset: 0x00007204
		public override void Load(IReportDeserializationContext ctx, ReportItem reportItem, PVVisual visual)
		{
			Tablix tablix = reportItem as Tablix;
			Contract.Check(tablix != null, "Expecting reportItem to be Tablix");
			this.LoadDataContext(ctx, tablix, visual.DataContext);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009034 File Offset: 0x00007234
		public List<PVColumnInfo> GetColumnsInfo(Tablix tablix)
		{
			List<PVColumnInfo> list = new List<PVColumnInfo>();
			int num = 0;
			int num2 = this.FindBodyRowIndex(tablix);
			TablixRow tablixRow = tablix.Body.TablixRows[num2];
			foreach (TablixMember tablixMember in tablix.ColumnHierarchy.Members)
			{
				string text = null;
				if (num < tablixRow.TablixCells.Count)
				{
					TablixCell tablixCell = tablixRow.TablixCells[num];
					if (tablixCell != null)
					{
						Textbox textbox = tablixCell.ReportItem as Textbox;
						if (textbox != null && textbox.FormatType == FormatType.Manual)
						{
							string customFormatString = textbox.CustomFormatString;
							if (customFormatString != null)
							{
								text = customFormatString;
							}
						}
					}
				}
				list.Add(new PVColumnInfo
				{
					CustomFormatString = text
				});
				num++;
			}
			return list;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009110 File Offset: 0x00007310
		public int FindBodyRowIndex(Tablix tablix)
		{
			List<TablixMember> members = tablix.RowHierarchy.Members;
			if (tablix.RowHierarchy.IsSubtotals)
			{
				int num = tablix.Body.TablixRows.Count;
				int num2 = 0;
				using (List<TablixMember>.Enumerator enumerator = members.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsSubtotal)
						{
							num = num2;
							break;
						}
						num2++;
					}
				}
				Contract.Check(num < tablix.Body.TablixRows.Count, "Invalid Tablix.  Cannot find bodyrow.");
				return num;
			}
			Contract.Check(members.Count == 1, "Expect 1 rowMember");
			Contract.Check(tablix.Body.TablixRows.Count == 1, "Expect 1 bodyRow");
			return 0;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000091E4 File Offset: 0x000073E4
		public void LoadDataContext(IReportDeserializationContext ctx, Tablix tablix, DataContext dataContext)
		{
			int num = 0;
			RdmToDocumentConverter.GetTypeFromRdmReportItem(tablix);
			Dictionary<ReportImageSource?, int> dictionary = new Dictionary<ReportImageSource?, int>();
			dictionary.Add(new ReportImageSource?(ReportImageSource.Database), 0);
			dictionary.Add(new ReportImageSource?(ReportImageSource.Embedded), 0);
			dictionary.Add(new ReportImageSource?(ReportImageSource.External), 0);
			using (new DataSetScope(ctx, tablix))
			{
				List<TablixMember> members = tablix.RowHierarchy.Members;
				bool isSubtotals = tablix.RowHierarchy.IsSubtotals;
				int num2 = this.FindBodyRowIndex(tablix);
				Group group = members[num2].Group;
				using (new DataSetScope(ctx, group))
				{
					List<TablixCell> tablixCells = tablix.Body.TablixRows[num2].TablixCells;
					Contract.Check(tablixCells.Count == tablix.ColumnHierarchy.Members.Count, "Expect the number of cells to match the root members");
					Bucket bucket = new Bucket
					{
						Name = "Values",
						BucketItems = new List<BucketItem>(),
						Properties = new List<BucketProperty>()
					};
					if (isSubtotals)
					{
						bucket.Properties.Add(new BucketProperty
						{
							Name = "Subtotal",
							Value = true
						});
					}
					foreach (TablixCell tablixCell in tablixCells)
					{
						if (tablixCell != null)
						{
							ReportItem reportItem = tablixCell.ReportItem;
							if (reportItem.RdlTagName == "Failed")
							{
								tablix.DiagnosticContext.AddError("TablixContainsFailedReportItem", new string[0]);
							}
							else
							{
								base.AddToBucketIfNotNull(ctx, reportItem, bucket);
								if (reportItem.RdlTagName == "Image")
								{
									ReportImageSource? source = (reportItem as Image).Source.Source;
									Dictionary<ReportImageSource?, int> dictionary2 = dictionary;
									ReportImageSource? reportImageSource = source;
									int num3 = dictionary2[reportImageSource];
									dictionary2[reportImageSource] = num3 + 1;
								}
								if (reportItem.RdlTagName == "GaugePanel")
								{
									num++;
								}
							}
						}
					}
					dataContext.Buckets.Add(bucket);
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009430 File Offset: 0x00007630
		public override void SetOutputValues(IReportDeserializationContext ctx, PVVisual visual, ReportItem reportItem)
		{
			Tablix tablix = reportItem as Tablix;
			using (new DataSetScope(ctx, tablix))
			{
				DataSet currentDataSet = ctx.GetCurrentDataSet();
				TablixHierarchy rowHierarchy = tablix.RowHierarchy;
				if (rowHierarchy != null)
				{
					Dictionary<Formula, SortDirection> dictionary = new Dictionary<Formula, SortDirection>();
					foreach (TablixMember tablixMember in rowHierarchy.Members)
					{
						DataSet dataSet = currentDataSet;
						Group group = tablixMember.Group;
						if (group != null && !string.IsNullOrEmpty(group.DataSetName))
						{
							using (new DataSetScope(ctx, group))
							{
								dataSet = ctx.GetCurrentDataSet();
							}
						}
						Dictionary<Formula, SortDirection> dictionary2 = this.ToSorts(dataSet, tablixMember.SortExpressions);
						base.AppendUniqueSorts(dictionary, dictionary2);
					}
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
		}
	}
}
