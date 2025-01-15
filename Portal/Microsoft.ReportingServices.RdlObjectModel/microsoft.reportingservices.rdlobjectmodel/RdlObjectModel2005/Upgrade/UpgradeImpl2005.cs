using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2008.Upgrade;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlObjectModel2005.Upgrade
{
	// Token: 0x02000053 RID: 83
	internal class UpgradeImpl2005 : UpgradeImpl2008
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x00004C7C File Offset: 0x00002E7C
		internal void UpgradeMatrix(Matrix2005 matrix)
		{
			this.UpgradeReportItem(matrix);
			this.UpgradePageBreak(matrix);
			TablixBody tablixBody = matrix.TablixBody;
			matrix.RepeatRowHeaders = true;
			matrix.RepeatColumnHeaders = true;
			int count = matrix.ColumnGroupings.Count;
			int count2 = matrix.RowGroupings.Count;
			int i;
			if (matrix.Corner != null)
			{
				TablixCorner tablixCorner = new TablixCorner();
				matrix.TablixCorner = tablixCorner;
				for (i = 0; i < count; i++)
				{
					TablixCornerRow tablixCornerRow = new TablixCornerRow();
					tablixCorner.TablixCornerRows.Add(tablixCornerRow);
					for (int j = 0; j < count2; j++)
					{
						tablixCornerRow.Add(new TablixCornerCell());
					}
				}
				TablixCornerCell tablixCornerCell = tablixCorner.TablixCornerRows[0][0];
				tablixCornerCell.CellContents = new CellContents();
				tablixCornerCell.CellContents.RowSpan = count;
				tablixCornerCell.CellContents.ColSpan = count2;
				if (matrix.Corner.ReportItems.Count > 0)
				{
					tablixCornerCell.CellContents.ReportItem = matrix.Corner.ReportItems[0];
				}
			}
			IList<TablixMember> list = null;
			TablixMember tablixMember = null;
			TablixMember tablixMember2 = null;
			TablixMember tablixMember3 = null;
			ColumnGrouping2005 columnGrouping = null;
			RowGrouping2005 rowGrouping = null;
			int num = 1;
			int num2 = 1;
			foreach (ColumnGrouping2005 columnGrouping2 in matrix.ColumnGroupings)
			{
				if (list == null)
				{
					matrix.TablixColumnHierarchy = new TablixHierarchy();
					list = matrix.TablixColumnHierarchy.TablixMembers;
				}
				if (columnGrouping2.FixedHeader)
				{
					matrix.FixedColumnHeaders = true;
				}
				DynamicColumns2005 dynamicColumns = columnGrouping2.DynamicColumns;
				if (dynamicColumns != null)
				{
					TablixMember tablixMember4 = new TablixMember();
					list.Add(tablixMember4);
					list = tablixMember4.TablixMembers;
					tablixMember = tablixMember4;
					tablixMember3 = tablixMember4;
					tablixMember4.Group = dynamicColumns.Grouping;
					tablixMember4.SortExpressions = dynamicColumns.Sorting;
					tablixMember4.Visibility = dynamicColumns.Visibility;
					tablixMember4.DataElementName = dynamicColumns.Grouping.DataCollectionName;
					tablixMember4.DataElementOutput = dynamicColumns.Grouping.DataElementOutput;
					this.TransferGroupingCustomProperties(tablixMember4, new UpgradeImpl2005.GroupAccessor(UpgradeImpl2005.TablixMemberGroupAccessor), new UpgradeImpl2005.CustomPropertiesAccessor(UpgradeImpl2005.TablixMemberCustomPropertiesAccessor));
					TablixHeader tablixHeader = new TablixHeader();
					tablixMember4.TablixHeader = tablixHeader;
					tablixHeader.Size = columnGrouping2.Height;
					tablixHeader.CellContents = new CellContents();
					if (dynamicColumns.ReportItems.Count > 0)
					{
						tablixHeader.CellContents.ReportItem = dynamicColumns.ReportItems[0];
					}
				}
				else
				{
					if (columnGrouping2.StaticColumns.Count <= 0)
					{
						throw new ArgumentException("No DynamicColumns or StaticColumns.");
					}
					if (columnGrouping != null)
					{
						throw new ArgumentException("More than one ColumnGrouping with StaticColumns.");
					}
					columnGrouping = columnGrouping2;
					num = columnGrouping2.StaticColumns.Count;
					for (int j = 0; j < num; j++)
					{
						TablixMember tablixMember4 = new TablixMember();
						list.Add(tablixMember4);
						TablixHeader tablixHeader2 = new TablixHeader();
						tablixMember4.TablixHeader = tablixHeader2;
						tablixHeader2.Size = columnGrouping2.Height;
						tablixHeader2.CellContents = new CellContents();
						if (columnGrouping2.StaticColumns[j].ReportItems.Count > 0)
						{
							tablixHeader2.CellContents.ReportItem = columnGrouping2.StaticColumns[j].ReportItems[0];
						}
						int k;
						for (k = 0; k < matrix.MatrixRows.Count; k++)
						{
							MatrixRow2005 matrixRow = matrix.MatrixRows[k];
							if (matrixRow.MatrixCells.Count > j && matrixRow.MatrixCells[j].ReportItems.Count > 0 && matrixRow.MatrixCells[j].ReportItems[0].DataElementOutput != DataElementOutputTypes.NoOutput)
							{
								break;
							}
						}
						if (k == matrix.MatrixRows.Count)
						{
							tablixMember4.DataElementOutput = DataElementOutputTypes.NoOutput;
						}
					}
					tablixMember = list[0];
					list = tablixMember.TablixMembers;
				}
			}
			this.SetKeepTogether(tablixMember3);
			list = null;
			tablixMember3 = null;
			foreach (RowGrouping2005 rowGrouping2 in matrix.RowGroupings)
			{
				if (list == null)
				{
					matrix.TablixRowHierarchy = new TablixHierarchy();
					list = matrix.TablixRowHierarchy.TablixMembers;
				}
				if (rowGrouping2.FixedHeader)
				{
					matrix.FixedRowHeaders = true;
				}
				DynamicRows2005 dynamicRows = rowGrouping2.DynamicRows;
				if (dynamicRows != null)
				{
					TablixMember tablixMember5 = new TablixMember();
					list.Add(tablixMember5);
					list = tablixMember5.TablixMembers;
					tablixMember2 = tablixMember5;
					tablixMember3 = tablixMember5;
					tablixMember5.Group = dynamicRows.Grouping;
					tablixMember5.SortExpressions = dynamicRows.Sorting;
					tablixMember5.Visibility = dynamicRows.Visibility;
					tablixMember5.DataElementName = dynamicRows.Grouping.DataCollectionName;
					tablixMember5.DataElementOutput = dynamicRows.Grouping.DataElementOutput;
					this.TransferGroupingCustomProperties(tablixMember5, new UpgradeImpl2005.GroupAccessor(UpgradeImpl2005.TablixMemberGroupAccessor), new UpgradeImpl2005.CustomPropertiesAccessor(UpgradeImpl2005.TablixMemberCustomPropertiesAccessor));
					TablixHeader tablixHeader3 = new TablixHeader();
					tablixMember5.TablixHeader = tablixHeader3;
					tablixHeader3.Size = rowGrouping2.Width;
					tablixHeader3.CellContents = new CellContents();
					if (dynamicRows.ReportItems.Count > 0)
					{
						tablixHeader3.CellContents.ReportItem = dynamicRows.ReportItems[0];
					}
				}
				else
				{
					if (rowGrouping2.StaticRows.Count <= 0)
					{
						throw new ArgumentException("No DynamicRows or StaticRows.");
					}
					if (rowGrouping != null)
					{
						throw new ArgumentException("More than one RowGrouping with StaticRows.");
					}
					rowGrouping = rowGrouping2;
					num2 = rowGrouping2.StaticRows.Count;
					for (int j = 0; j < num2; j++)
					{
						TablixMember tablixMember5 = new TablixMember();
						list.Add(tablixMember5);
						TablixHeader tablixHeader4 = new TablixHeader();
						tablixMember5.TablixHeader = tablixHeader4;
						tablixHeader4.Size = rowGrouping2.Width;
						tablixHeader4.CellContents = new CellContents();
						if (rowGrouping2.StaticRows[j].ReportItems.Count > 0)
						{
							tablixHeader4.CellContents.ReportItem = rowGrouping2.StaticRows[j].ReportItems[0];
						}
						if (matrix.MatrixRows.Count > j)
						{
							MatrixRow2005 matrixRow2 = matrix.MatrixRows[j];
							int k = 0;
							while (k < matrixRow2.MatrixCells.Count && (matrixRow2.MatrixCells[k].ReportItems.Count <= 0 || matrixRow2.MatrixCells[k].ReportItems[0].DataElementOutput == DataElementOutputTypes.NoOutput))
							{
								k++;
							}
							if (k == matrixRow2.MatrixCells.Count)
							{
								tablixMember5.DataElementOutput = DataElementOutputTypes.NoOutput;
							}
						}
					}
					tablixMember2 = list[0];
					list = tablixMember2.TablixMembers;
				}
			}
			this.SetKeepTogether(tablixMember3);
			this.UpgradePageBreaks(matrix, false);
			if (matrix.MatrixColumns.Count != num)
			{
				throw new ArgumentException("Wrong number of MatrixColumns.");
			}
			if (matrix.MatrixRows.Count != num2)
			{
				throw new ArgumentException("Wrong number of MatrixRows.");
			}
			foreach (MatrixRow2005 matrixRow3 in matrix.MatrixRows)
			{
				TablixRow tablixRow = new TablixRow();
				tablixBody.TablixRows.Add(tablixRow);
				tablixRow.Height = matrixRow3.Height;
				if (matrixRow3.MatrixCells.Count != num)
				{
					throw new ArgumentException("Wrong number of MatrixCells.");
				}
				foreach (MatrixCell2005 matrixCell in matrixRow3.MatrixCells)
				{
					TablixCell tablixCell = new TablixCell();
					tablixRow.TablixCells.Add(tablixCell);
					tablixCell.DataElementName = matrix.CellDataElementName;
					tablixCell.DataElementOutput = matrix.CellDataElementOutput;
					tablixCell.CellContents = new CellContents();
					if (matrixCell.ReportItems.Count > 0)
					{
						tablixCell.CellContents.ReportItem = matrixCell.ReportItems[0];
					}
				}
			}
			List<int> list2 = new List<int>();
			int num3 = num2;
			i = matrix.RowGroupings.Count;
			while (--i >= 0)
			{
				RowGrouping2005 rowGrouping3 = matrix.RowGroupings[i];
				if (rowGrouping3 == rowGrouping)
				{
					num3 = 1;
					if (i < matrix.RowGroupings.Count - 1)
					{
						this.CloneTablixHierarchy(matrix, tablixMember2, true);
					}
				}
				else if (rowGrouping3.DynamicRows != null && rowGrouping3.DynamicRows.Subtotal != null)
				{
					this.CloneTablixSubtotal(matrix, tablixMember2, rowGrouping3.DynamicRows.Subtotal, num3, num2, true, list2);
				}
				if (i > 0)
				{
					tablixMember2 = (TablixMember)tablixMember2.Parent;
				}
			}
			num3 = num;
			i = matrix.ColumnGroupings.Count;
			while (--i >= 0)
			{
				ColumnGrouping2005 columnGrouping3 = matrix.ColumnGroupings[i];
				if (columnGrouping3.StaticColumns.Count > 0)
				{
					num3 = 1;
					if (i < matrix.ColumnGroupings.Count - 1)
					{
						this.CloneTablixHierarchy(matrix, tablixMember, false);
					}
				}
				else if (columnGrouping3.DynamicColumns != null && columnGrouping3.DynamicColumns.Subtotal != null)
				{
					this.CloneTablixSubtotal(matrix, tablixMember, columnGrouping3.DynamicColumns.Subtotal, num3, num, false, list2);
				}
				if (i > 0)
				{
					tablixMember = (TablixMember)tablixMember.Parent;
				}
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00005614 File Offset: 0x00003814
		private void SetKeepTogether(TablixMember innerMostDynamicMember)
		{
			if (innerMostDynamicMember != null)
			{
				innerMostDynamicMember.KeepTogether = true;
				if (innerMostDynamicMember.TablixMembers != null)
				{
					foreach (TablixMember tablixMember in innerMostDynamicMember.TablixMembers)
					{
						tablixMember.KeepTogether = true;
					}
				}
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00005674 File Offset: 0x00003874
		private void CloneTablixHierarchy(Tablix tablix, TablixMember staticMember, bool cloneRows)
		{
			if (staticMember.TablixMembers.Count == 0)
			{
				return;
			}
			TablixBody tablixBody = tablix.TablixBody;
			IList<TablixMember> siblingTablixMembers = this.GetSiblingTablixMembers(staticMember);
			int count = siblingTablixMembers.Count;
			int i = 1;
			while (i < count)
			{
				TablixMember tablixMember = siblingTablixMembers[i];
				UpgradeImpl2005.Cloner cloner = new UpgradeImpl2005.Cloner(this);
				tablixMember.TablixMembers = (IList<TablixMember>)cloner.Clone(staticMember.TablixMembers);
				cloner.FixReferences();
				int num;
				int num2;
				int num3;
				if (!cloneRows)
				{
					num = tablixBody.TablixColumns.Count / count;
					using (IEnumerator<TablixRow> enumerator = tablixBody.TablixRows.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TablixRow tablixRow = enumerator.Current;
							num2 = num;
							num3 = i * num;
							while (num2-- > 0)
							{
								cloner.FixReferences(tablixRow.TablixCells[num3].CellContents.ReportItem);
								num3++;
							}
						}
						goto IL_0118;
					}
					goto IL_00D4;
				}
				goto IL_00D4;
				IL_0118:
				i++;
				continue;
				IL_00D4:
				num = tablixBody.TablixRows.Count / count;
				num2 = num;
				num3 = i * num;
				while (num2-- > 0)
				{
					cloner.FixReferences(tablixBody.TablixRows[num3].TablixCells);
					num3++;
				}
				goto IL_0118;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000057B4 File Offset: 0x000039B4
		private IList<TablixMember> GetSiblingTablixMembers(TablixMember tablixMember)
		{
			if (!(tablixMember.Parent is TablixHierarchy))
			{
				return ((TablixMember)tablixMember.Parent).TablixMembers;
			}
			return ((TablixHierarchy)tablixMember.Parent).TablixMembers;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000057E4 File Offset: 0x000039E4
		private void CloneTablixSubtotal(Tablix tablix, TablixMember dynamicMember, Subtotal2005 subtotal, int outerStaticMembers, int originalCount, bool rowSubtotal, List<int> subTotalRows)
		{
			string text = tablix.Name;
			bool flag = true;
			for (TablixMember tablixMember = dynamicMember.Parent as TablixMember; tablixMember != null; tablixMember = tablixMember.Parent as TablixMember)
			{
				if (tablixMember.Group != null)
				{
					flag = false;
					text = tablixMember.Group.Name;
					break;
				}
			}
			UpgradeImpl2005.Cloner cloner = new UpgradeImpl2005.Cloner(this);
			this.ProcessClonedDynamicTablixMember(dynamicMember, cloner, text);
			TablixMember tablixMember2 = new TablixMember();
			if (flag)
			{
				tablixMember2.HideIfNoRows = true;
			}
			this.GetSiblingTablixMembers(dynamicMember).Insert((subtotal.Position != SubtotalPositions.Before) ? 1 : 0, tablixMember2);
			tablixMember2.DataElementName = subtotal.DataElementName;
			tablixMember2.DataElementOutput = subtotal.DataElementOutput;
			TablixHeader tablixHeader = new TablixHeader();
			tablixMember2.TablixHeader = tablixHeader;
			tablixHeader.Size = dynamicMember.TablixHeader.Size;
			tablixHeader.CellContents = new CellContents();
			tablixHeader.CellContents.ReportItem = subtotal.ReportItems[0];
			this.CloneSubtotalTablixMembers(cloner, tablixMember2, dynamicMember.TablixMembers, text);
			this.FixupMutualReferences(cloner.TextboxNameValueExprTable);
			TablixBody tablixBody = tablix.TablixBody;
			if (!rowSubtotal)
			{
				int num = originalCount / outerStaticMembers;
				int num2 = tablixBody.TablixColumns.Count / outerStaticMembers;
				for (int i = 0; i < outerStaticMembers; i++)
				{
					int num3 = i * (num + num2);
					int num4 = ((subtotal.Position == SubtotalPositions.Before) ? num3 : (num3 + num2));
					int j = 0;
					while (j < num)
					{
						for (int k = 0; k < tablixBody.TablixRows.Count; k++)
						{
							TablixRow tablixRow = tablixBody.TablixRows[k];
							TablixCell tablixCell = (TablixCell)cloner.Clone(tablixRow.TablixCells[num3]);
							if (!subTotalRows.Contains(k))
							{
								cloner.ApplySubTotalStyleOverrides(tablixCell.CellContents.ReportItem, subtotal.Style);
							}
							tablixRow.TablixCells.Insert(num4, tablixCell);
						}
						TablixColumn tablixColumn = (TablixColumn)cloner.Clone(tablixBody.TablixColumns[num3]);
						tablixBody.TablixColumns.Insert(num4, tablixColumn);
						if (num3 >= num4)
						{
							num3++;
						}
						j++;
						num3++;
						num4++;
					}
				}
			}
			else
			{
				int num = originalCount / outerStaticMembers;
				int num2 = tablixBody.TablixRows.Count / outerStaticMembers;
				for (int i = 0; i < outerStaticMembers; i++)
				{
					int num3 = i * (num + num2);
					int num4 = ((subtotal.Position == SubtotalPositions.Before) ? num3 : (num3 + num2));
					int j = 0;
					while (j < num)
					{
						TablixRow tablixRow2 = (TablixRow)cloner.Clone(tablixBody.TablixRows[num3]);
						foreach (TablixCell tablixCell2 in tablixRow2.TablixCells)
						{
							cloner.ApplySubTotalStyleOverrides(tablixCell2.CellContents.ReportItem, subtotal.Style);
						}
						tablixBody.TablixRows.Insert(num4, tablixRow2);
						subTotalRows.Add(num4);
						if (num3 >= num4)
						{
							num3++;
						}
						j++;
						num3++;
						num4++;
					}
				}
			}
			cloner.FixReferences();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00005B2C File Offset: 0x00003D2C
		private void CloneSubtotalTablixMembers(UpgradeImpl2005.Cloner cloner, TablixMember tablixMember, IList<TablixMember> tablixMembers, string parentScope)
		{
			if (tablixMembers.Count > 0)
			{
				TablixMember tablixMember2 = null;
				if (tablixMembers[0].Group != null)
				{
					tablixMember2 = tablixMembers[0];
				}
				else if (tablixMembers.Count > 1 && tablixMembers[1].Group != null)
				{
					tablixMember2 = tablixMembers[1];
				}
				if (tablixMember2 != null)
				{
					ReportSize size = tablixMember2.TablixHeader.Size;
					tablixMember.TablixHeader.Size += size;
					this.ProcessClonedDynamicTablixMember(tablixMember2, cloner, parentScope);
					this.CloneSubtotalTablixMembers(cloner, tablixMember, tablixMember2.TablixMembers, parentScope);
					return;
				}
				foreach (TablixMember tablixMember3 in tablixMembers)
				{
					TablixMember tablixMember4 = new TablixMember();
					tablixMember.TablixMembers.Add(tablixMember4);
					tablixMember4.Visibility = (Visibility)cloner.Clone(tablixMember3.Visibility);
					tablixMember4.TablixHeader = (TablixHeader)cloner.Clone(tablixMember3.TablixHeader);
					tablixMember4.DataElementName = tablixMember3.DataElementName;
					tablixMember4.DataElementOutput = tablixMember3.DataElementOutput;
					this.CloneSubtotalTablixMembers(cloner, tablixMember4, tablixMember3.TablixMembers, parentScope);
				}
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00005C64 File Offset: 0x00003E64
		private void ProcessClonedDynamicTablixMember(TablixMember dynamicMember, UpgradeImpl2005.Cloner cloner, string parentScope)
		{
			cloner.AddNameMapping(dynamicMember.Group.Name, parentScope);
			if (dynamicMember.TablixHeader.CellContents != null)
			{
				this.CollectInScopeTextboxValues(dynamicMember.TablixHeader.CellContents.ReportItem, cloner.TextboxNameValueExprTable);
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00005CA4 File Offset: 0x00003EA4
		private void CollectInScopeTextboxValues(ReportItem reportItem, Dictionary<string, string> nameValueExprTable)
		{
			if (reportItem == null)
			{
				return;
			}
			if (reportItem is Textbox)
			{
				Textbox textbox = (Textbox)reportItem;
				string text = textbox.Paragraphs[0].TextRuns[0].Value.Value;
				if (ReportExpression.IsExpressionString(text))
				{
					text = text.Substring(1);
				}
				this.ReplaceReportItemReferenceWithValue(text, nameValueExprTable);
				nameValueExprTable[textbox.Name] = text;
				return;
			}
			if (reportItem is Microsoft.ReportingServices.RdlObjectModel.Rectangle)
			{
				Microsoft.ReportingServices.RdlObjectModel.Rectangle rectangle = (Microsoft.ReportingServices.RdlObjectModel.Rectangle)reportItem;
				this.CollectInScopeTextboxValues(rectangle.ReportItems, nameValueExprTable);
				return;
			}
			if (reportItem is Tablix)
			{
				Tablix tablix = (Tablix)reportItem;
				if (tablix.TablixCorner != null)
				{
					IList<IList<TablixCornerCell>> tablixCornerRows = tablix.TablixCorner.TablixCornerRows;
					if (tablixCornerRows != null)
					{
						foreach (IList<TablixCornerCell> list in tablixCornerRows)
						{
							if (list != null)
							{
								foreach (TablixCornerCell tablixCornerCell in list)
								{
									if (tablixCornerCell != null && tablixCornerCell.CellContents != null)
									{
										this.CollectInScopeTextboxValues(tablixCornerCell.CellContents.ReportItem, nameValueExprTable);
									}
								}
							}
						}
					}
				}
				this.CollectInScopeTextboxValues(tablix.TablixColumnHierarchy, nameValueExprTable);
				this.CollectInScopeTextboxValues(tablix.TablixRowHierarchy, nameValueExprTable);
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00005E10 File Offset: 0x00004010
		private void CollectInScopeTextboxValues(TablixHierarchy hierarchy, Dictionary<string, string> nameValueExprTable)
		{
			if (hierarchy == null || hierarchy.TablixMembers == null)
			{
				return;
			}
			this.CollectInScopeTextboxValues(hierarchy.TablixMembers, nameValueExprTable);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00005E2C File Offset: 0x0000402C
		private void CollectInScopeTextboxValues(IList<TablixMember> tablixMembers, Dictionary<string, string> nameValueExprTable)
		{
			foreach (TablixMember tablixMember in tablixMembers)
			{
				if (tablixMember != null && tablixMember.Group == null)
				{
					if (tablixMember.TablixHeader != null && tablixMember.TablixHeader.CellContents != null)
					{
						this.CollectInScopeTextboxValues(tablixMember.TablixHeader.CellContents.ReportItem, nameValueExprTable);
					}
					this.CollectInScopeTextboxValues(tablixMember.TablixMembers, nameValueExprTable);
				}
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00005EB4 File Offset: 0x000040B4
		private void CollectInScopeTextboxValues(IList<ReportItem> reportItems, Dictionary<string, string> nameValueExprTable)
		{
			if (reportItems == null)
			{
				return;
			}
			foreach (ReportItem reportItem in reportItems)
			{
				this.CollectInScopeTextboxValues(reportItem, nameValueExprTable);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00005F04 File Offset: 0x00004104
		private string ReplaceReference(string expression, string oldValue, string newValue)
		{
			MatchCollection matchCollection = this.m_regexes.ReportItemName.Matches(expression);
			int num = 0;
			int newLength = newValue.Length;
			int oldLength = oldValue.Length;
			foreach (object obj in matchCollection)
			{
				global::System.Text.RegularExpressions.Group group = ((Match)obj).Groups["reportitemname"];
				if (group != null && group.Value.Equals(oldValue, StringComparison.OrdinalIgnoreCase))
				{
					expression = expression.Substring(0, num + group.Index) + newValue + expression.Substring(num + group.Index + oldLength);
					num += newLength - oldLength;
				}
			}
			expression = this.FixAggregateFunctions(expression, delegate(string expr, int currentOffset, string specialFunctionName, int specialFunctionPos, int argumentsPos, int scopePos, int scopeLength, ref int offset)
			{
				if (scopeLength != 0)
				{
					Match match = this.m_regexes.StringLiteralOnly.Match(expr, scopePos, scopeLength);
					if (match.Success && match.Groups["string"].Value.Equals(oldValue, StringComparison.OrdinalIgnoreCase))
					{
						scopePos = match.Groups["string"].Index;
						expr = expr.Substring(0, scopePos) + newValue + expr.Substring(scopePos + oldLength);
						offset += newLength - oldLength;
					}
				}
				return expr;
			});
			return expression;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00006020 File Offset: 0x00004220
		private string ReplaceReportItemReferenceWithValue(string expression, Dictionary<string, string> nameValueExprTable)
		{
			if (nameValueExprTable.Count == 0)
			{
				return expression;
			}
			MatchCollection matchCollection = this.m_regexes.ReportItemValueReference.Matches(expression);
			int num = 0;
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				global::System.Text.RegularExpressions.Group group = match.Groups["reportitemname"];
				string text;
				if (group != null && nameValueExprTable.TryGetValue(group.Value, out text))
				{
					text = "(" + text + ")";
					int length = text.Length;
					int length2 = match.Value.Length;
					expression = expression.Substring(0, num + match.Index) + text + expression.Substring(num + match.Index + length2);
					num += length - length2;
				}
			}
			return expression;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00006114 File Offset: 0x00004314
		private void FixupMutualReferences(Dictionary<string, string> nameValueExprTable)
		{
			if (nameValueExprTable.Count == 0)
			{
				return;
			}
			string[] array = new string[nameValueExprTable.Count];
			nameValueExprTable.Keys.CopyTo(array, 0);
			for (int i = 0; i < array.Length - 1; i++)
			{
				foreach (string text in array)
				{
					string text2 = this.ReplaceReportItemReferenceWithValue(nameValueExprTable[text], nameValueExprTable);
					nameValueExprTable[text] = text2;
				}
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00006180 File Offset: 0x00004380
		private int GetScopeArgumentIndex(string function)
		{
			string text = function.ToUpperInvariant();
			if (text == "RUNNINGVALUE")
			{
				return 2;
			}
			if (!(text == "ROWNUMBER") && !(text == "COUNTROWS"))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000061C4 File Offset: 0x000043C4
		private bool FindArgument(int currentPos, string expression, out int newPos, int argumentIndex, out int argumentPos, out int argumentLength)
		{
			int num = 1;
			int num2 = 0;
			argumentPos = currentPos;
			argumentLength = 0;
			while (0 < num && currentPos < expression.Length)
			{
				Match match = this.m_regexes.Arguments.Match(expression, currentPos);
				if (!match.Success)
				{
					currentPos = expression.Length;
				}
				else
				{
					string text = match.Result("${openParen}");
					string text2 = match.Result("${closeParen}");
					string text3 = match.Result("${comma}");
					if (text != null && text.Length != 0)
					{
						num++;
					}
					else if (text2 != null && text2.Length != 0)
					{
						num--;
						if (num == 0)
						{
							if (num2 == argumentIndex)
							{
								argumentLength = match.Index - argumentPos;
							}
							num2++;
						}
					}
					else if (text3 != null && text3.Length != 0 && 1 == num)
					{
						if (num2 == argumentIndex)
						{
							argumentLength = match.Index - argumentPos;
						}
						num2++;
						if (num2 == argumentIndex)
						{
							argumentPos = match.Index + 1;
						}
					}
					currentPos = match.Index + match.Length;
				}
			}
			newPos = currentPos;
			return argumentLength != 0;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000062CC File Offset: 0x000044CC
		public UpgradeImpl2005(bool throwUpgradeException)
			: this(throwUpgradeException, true, true)
		{
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000062D7 File Offset: 0x000044D7
		public UpgradeImpl2005(bool throwUpgradeException, bool upgradeDundasCRIToNative, bool renameInvalidDataSources)
		{
			this.m_throwUpgradeException = throwUpgradeException;
			this.m_upgradeDundasCRIToNative = upgradeDundasCRIToNative;
			this.m_renameInvalidDataSources = renameInvalidDataSources;
			this.m_regexes = ReportRegularExpressions.Value;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000630D File Offset: 0x0000450D
		internal override Type GetReportType()
		{
			return typeof(Report2005);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00006319 File Offset: 0x00004519
		protected override void InitUpgrade()
		{
			this.m_dataSourceNameTable = new Hashtable();
			this.m_dataSourceCaseSensitiveNameTable = new Hashtable();
			this.m_upgradeable = new List<IUpgradeable>();
			this.m_dataSources = new List<DataSource2005>();
			this.m_nameTable = new Hashtable();
			base.InitUpgrade();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00006358 File Offset: 0x00004558
		protected override void Upgrade(Report report)
		{
			if (this.m_dataSources != null)
			{
				foreach (DataSource2005 dataSource in this.m_dataSources)
				{
					dataSource.Upgrade(this);
				}
			}
			foreach (IUpgradeable upgradeable in this.m_upgradeable)
			{
				upgradeable.Upgrade(this);
				if (upgradeable is CustomReportItem2005)
				{
					CustomReportItem2005 customReportItem = (CustomReportItem2005)upgradeable;
					if (customReportItem.Type == "DundasChartControl" && this.m_upgradeDundasCRIToNative)
					{
						Chart chart = new Chart();
						this.UpgradeDundasCRIChart(customReportItem, chart);
						this.ChangeReportItem(customReportItem.Parent, customReportItem, chart);
					}
					else if (customReportItem.Type == "DundasGaugeControl" && this.m_upgradeDundasCRIToNative)
					{
						GaugePanel gaugePanel = new GaugePanel();
						this.UpgradeDundasCRIGaugePanel(customReportItem, gaugePanel);
						this.ChangeReportItem(customReportItem.Parent, customReportItem, gaugePanel);
					}
					else if (this.m_throwUpgradeException)
					{
						throw new CRI2005UpgradeException();
					}
				}
			}
			UpgradeImpl2005.AdjustBodyWhitespace((Report2005)report);
			base.Upgrade(report);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000064A4 File Offset: 0x000046A4
		protected override RdlSerializerSettings CreateReaderSettings()
		{
			return UpgradeSerializerSettings2005.CreateReaderSettings();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000064AB File Offset: 0x000046AB
		protected override RdlSerializerSettings CreateWriterSettings()
		{
			return UpgradeSerializerSettings2005.CreateWriterSettings();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000064B2 File Offset: 0x000046B2
		protected override void SetupReaderSettings(RdlSerializerSettings settings)
		{
			SerializerHost2005 serializerHost = (SerializerHost2005)settings.Host;
			serializerHost.Upgradeable = this.m_upgradeable;
			serializerHost.DataSources = this.m_dataSources;
			serializerHost.NameTable = this.m_nameTable;
			base.SetupReaderSettings(settings);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000064EC File Offset: 0x000046EC
		private void ChangeReportItem(object parentObject, object oldReportItem, object newReportItem)
		{
			TypeMapping typeMapping = TypeMapper.GetTypeMapping(parentObject.GetType());
			if (typeMapping is StructMapping)
			{
				foreach (MemberMapping memberMapping in ((StructMapping)typeMapping).Members)
				{
					object value = memberMapping.GetValue(parentObject);
					if (memberMapping.Type == typeof(RdlCollection<ReportItem>))
					{
						RdlCollection<ReportItem> rdlCollection = (RdlCollection<ReportItem>)value;
						int num = rdlCollection.IndexOf((ReportItem)oldReportItem);
						if (num != -1)
						{
							rdlCollection[num] = (ReportItem)newReportItem;
							break;
						}
					}
					else if (value == oldReportItem)
					{
						memberMapping.SetValue(parentObject, newReportItem);
						break;
					}
				}
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000065B0 File Offset: 0x000047B0
		private static void AdjustBodyWhitespace(Report2005 report)
		{
			if (report.Body.ReportItems != null && report.Body.ReportItems.Count != 0)
			{
				double num = 0.0;
				double num2 = 0.0;
				double num3 = report.Width.ToPixels();
				double num4 = report.Body.Height.ToPixels();
				foreach (ReportItem reportItem in report.Body.ReportItems)
				{
					if (reportItem.Width.IsEmpty)
					{
						num = num3;
					}
					else
					{
						num = Math.Max(num, reportItem.Left.ToPixels() + reportItem.Width.ToPixels());
					}
					if (reportItem.Height.IsEmpty)
					{
						num2 = num4;
					}
					else
					{
						num2 = Math.Max(num2, reportItem.Top.ToPixels() + reportItem.Height.ToPixels());
					}
				}
				num4 = Math.Min(num4, num2);
				report.Body.Height = ReportSize.FromPixels(num4, report.Body.Height.Type);
				double num5 = Math.Max(1.0, report.Page.PageWidth.ToPixels() - report.Page.LeftMargin.ToPixels() - report.Page.RightMargin.ToPixels());
				if (report.Page.Columns > 1)
				{
					num5 -= (double)(report.Page.Columns - 1) * report.Page.ColumnSpacing.ToPixels();
					num5 = Math.Max(1.0, num5 / (double)report.Page.Columns);
				}
				num3 = Math.Min(num3, num5 * Math.Ceiling(num / num5));
				report.Width = ReportSize.FromPixels(num3, report.Width.Type);
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000067E0 File Offset: 0x000049E0
		internal static Microsoft.ReportingServices.RdlObjectModel.Group TablixMemberGroupAccessor(object member)
		{
			return ((TablixMember)member).Group;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000067ED File Offset: 0x000049ED
		internal static IList<CustomProperty> TablixMemberCustomPropertiesAccessor(object member)
		{
			return ((TablixMember)member).CustomProperties;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000067FA File Offset: 0x000049FA
		internal static Microsoft.ReportingServices.RdlObjectModel.Group ChartMemberGroupAccessor(object member)
		{
			return ((ChartMember)member).Group;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00006807 File Offset: 0x00004A07
		internal static IList<CustomProperty> ChartMemberCustomPropertiesAccessor(object member)
		{
			return ((ChartMember)member).CustomProperties;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00006814 File Offset: 0x00004A14
		internal static Microsoft.ReportingServices.RdlObjectModel.Group DataMemberGroupAccessor(object member)
		{
			return ((DataMember)member).Group;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00006821 File Offset: 0x00004A21
		internal static IList<CustomProperty> DataMemberCustomPropertiesAccessor(object member)
		{
			return ((DataMember)member).CustomProperties;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000682E File Offset: 0x00004A2E
		internal static string SplitName(string name)
		{
			return Regex.Replace(name, "(\\p{Ll})(\\p{Lu})|_+", "$1 $2");
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00006840 File Offset: 0x00004A40
		internal void UpgradeReport(Report2005 report)
		{
			report.ConsumeContainerWhitespace = true;
			Body2005 body = report.Body as Body2005;
			if (body != null)
			{
				report.Page.Columns = body.Columns;
				report.Page.ColumnSpacing = body.ColumnSpacing;
			}
			Style style = body.Style;
			if (style != null && (style.Border == null || style.Border.Style == BorderStyles.None) && (style.TopBorder == null || style.TopBorder.Style == BorderStyles.None) && (style.BottomBorder == null || style.BottomBorder.Style == BorderStyles.None) && (style.LeftBorder == null || style.LeftBorder.Style == BorderStyles.None) && (style.RightBorder == null || style.RightBorder.Style == BorderStyles.None))
			{
				report.Page.Style = style;
				report.Body.Style = null;
			}
			foreach (ReportParameter reportParameter in report.ReportParameters)
			{
				ReportParameter2005 reportParameter2 = (ReportParameter2005)reportParameter;
				if (reportParameter2.Nullable && (reportParameter2.DefaultValue == null || (reportParameter2.DefaultValue.Values.Count == 0 && reportParameter2.DefaultValue.DataSetReference == null)))
				{
					if (reportParameter2.DefaultValue == null)
					{
						reportParameter2.DefaultValue = new DefaultValue();
					}
					reportParameter2.DefaultValue.Values.Add(null);
				}
				if (reportParameter2.Prompt != null && reportParameter2.Prompt.Value.Value == "")
				{
					reportParameter2.Hidden = true;
					reportParameter2.Prompt = new ReportExpression?(reportParameter2.Name);
				}
			}
			if (report.Page.InteractiveHeight == report.Page.PageHeight)
			{
				report.Page.InteractiveHeight = ReportSize.Empty;
			}
			if (report.Page.InteractiveWidth == report.Page.PageWidth)
			{
				report.Page.InteractiveWidth = ReportSize.Empty;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00006A84 File Offset: 0x00004C84
		internal void UpgradeReportItem(ReportItem item)
		{
			IReportItem2005 reportItem = (IReportItem2005)item;
			if (reportItem.Action != null)
			{
				item.ActionInfo = new ActionInfo();
				item.ActionInfo.Actions.Add(reportItem.Action);
			}
			this.UpgradeDataElementOutput(item);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00006AC8 File Offset: 0x00004CC8
		internal void UpgradeDataElementOutput(ReportItem reportItem)
		{
			if (reportItem.DataElementOutput == DataElementOutputTypes.Auto && reportItem.Visibility != null && reportItem.Visibility.Hidden.IsExpression)
			{
				reportItem.DataElementOutput = DataElementOutputTypes.NoOutput;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00006B04 File Offset: 0x00004D04
		internal void UpgradePageBreak(IPageBreakLocation2005 item)
		{
			if (item.PageBreak == null && (item.PageBreakAtStart || item.PageBreakAtEnd))
			{
				item.PageBreak = new PageBreak();
				item.PageBreak.BreakLocation = ((!item.PageBreakAtStart) ? BreakLocations.End : ((!item.PageBreakAtEnd) ? BreakLocations.Start : BreakLocations.StartAndEnd));
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00006B56 File Offset: 0x00004D56
		internal void UpgradeRectangle(Rectangle2005 rectangle)
		{
			if (rectangle.DataElementOutput == DataElementOutputTypes.Auto)
			{
				rectangle.DataElementOutput = DataElementOutputTypes.ContentsOnly;
			}
			this.UpgradeReportItem(rectangle);
			this.UpgradePageBreak(rectangle);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00006B75 File Offset: 0x00004D75
		internal void UpgradeCustomReportItem(CustomReportItem2005 cri)
		{
			this.UpgradeReportItem(cri);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00006B80 File Offset: 0x00004D80
		internal void UpgradeDataGrouping(DataGrouping2005 dataGrouping)
		{
			if (!dataGrouping.Static && dataGrouping.Group == null)
			{
				Microsoft.ReportingServices.RdlObjectModel.Group group = new Microsoft.ReportingServices.RdlObjectModel.Group();
				string parentReportItemName = this.GetParentReportItemName(dataGrouping);
				group.Name = this.UniqueName(parentReportItemName + "_Group", group);
				dataGrouping.Group = group;
				return;
			}
			this.TransferGroupingCustomProperties(dataGrouping, new UpgradeImpl2005.GroupAccessor(UpgradeImpl2005.DataMemberGroupAccessor), new UpgradeImpl2005.CustomPropertiesAccessor(UpgradeImpl2005.DataMemberCustomPropertiesAccessor));
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00006BEC File Offset: 0x00004DEC
		internal void UpgradeList(List2005 list)
		{
			this.UpgradeReportItem(list);
			this.UpgradePageBreak(list);
			list.TablixColumnHierarchy = new TablixHierarchy();
			TablixMember tablixMember = new TablixMember();
			list.TablixColumnHierarchy.TablixMembers.Add(tablixMember);
			TablixMember tablixMember2 = new TablixMember();
			list.TablixRowHierarchy = new TablixHierarchy();
			list.TablixRowHierarchy.TablixMembers.Add(tablixMember2);
			if (list.Grouping == null)
			{
				Microsoft.ReportingServices.RdlObjectModel.Group group = new Microsoft.ReportingServices.RdlObjectModel.Group();
				group = new Microsoft.ReportingServices.RdlObjectModel.Group();
				group.Name = this.UniqueName(list.Name + "_Details_Group", group);
				tablixMember2.Group = group;
				if (list.DataInstanceName == null)
				{
					tablixMember2.Group.DataElementName = "Item";
					tablixMember2.DataElementName = "Item_Collection";
				}
				else
				{
					tablixMember2.Group.DataElementName = list.DataInstanceName;
					tablixMember2.DataElementName = list.DataInstanceName + "_Collection";
				}
			}
			else
			{
				tablixMember2.Group = list.Grouping;
				Grouping2005 grouping = (Grouping2005)list.Grouping;
				this.UpgradePageBreaks(list, false);
			}
			tablixMember2.DataElementOutput = list.DataInstanceElementOutput;
			tablixMember2.KeepTogether = true;
			this.TransferGroupingCustomProperties(tablixMember2, new UpgradeImpl2005.GroupAccessor(UpgradeImpl2005.TablixMemberGroupAccessor), new UpgradeImpl2005.CustomPropertiesAccessor(UpgradeImpl2005.TablixMemberCustomPropertiesAccessor));
			TablixColumn tablixColumn = new TablixColumn();
			list.TablixBody.TablixColumns.Add(tablixColumn);
			tablixColumn.Width = this.GetReportItemWidth(list);
			TablixRow tablixRow = new TablixRow();
			list.TablixBody.TablixRows.Add(tablixRow);
			tablixRow.Height = this.GetReportItemHeight(list);
			TablixCell tablixCell = new TablixCell();
			tablixRow.TablixCells.Add(tablixCell);
			Microsoft.ReportingServices.RdlObjectModel.Rectangle rectangle = new Microsoft.ReportingServices.RdlObjectModel.Rectangle();
			tablixCell.CellContents = new CellContents();
			tablixCell.CellContents.ReportItem = rectangle;
			rectangle.KeepTogether = true;
			rectangle.Name = this.UniqueName(list.Name + "_Contents", rectangle);
			rectangle.ReportItems = list.ReportItems;
			bool flag = false;
			if (this.IsUpgradedListDetailMember(tablixMember2))
			{
				this.FixAggregateFunction(rectangle.ReportItems, list.Name, list.Name, false, ref flag);
			}
			else
			{
				this.FixAggregateFunction(rectangle.ReportItems);
			}
			if (list.Visibility != null)
			{
				string toggleItem = list.Visibility.ToggleItem;
				bool flag2 = false;
				if (toggleItem != null && this.m_nameTable.ContainsKey(toggleItem))
				{
					flag2 = true;
					if (tablixMember2.Group.Parent != null && this.TextBoxExistsInCollection(rectangle.ReportItems, toggleItem))
					{
						tablixMember2.Visibility = list.Visibility;
						list.Visibility = null;
					}
				}
				if (!flag2 && tablixMember2.Visibility == null)
				{
					tablixMember2.Visibility = new Visibility();
					tablixMember2.Visibility.Hidden = list.Visibility.Hidden;
					list.Visibility.Hidden = null;
				}
				if (this.IsUpgradedListDetailMember(tablixMember2))
				{
					this.FixAggregateFunction(tablixMember2.Visibility, list.Name, list.Name, false, ref flag);
				}
				else
				{
					this.FixAggregateFunction(tablixMember2.Visibility);
				}
			}
			if (list.Sorting != null && list.Sorting.Count != 0)
			{
				if (!flag || list.Grouping != null || this.SortingContainsAggregate(list.Sorting))
				{
					tablixMember2.SortExpressions = list.Sorting;
					return;
				}
				list.SortExpressions = list.Sorting;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00006F42 File Offset: 0x00005142
		private bool IsUpgradedListDetailMember(TablixMember rowMember)
		{
			return rowMember.Group.GroupExpressions == null || rowMember.Group.GroupExpressions.Count == 0;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00006F68 File Offset: 0x00005168
		private bool TextBoxExistsInCollection(IList<ReportItem> reportItems, string name)
		{
			if (reportItems != null)
			{
				foreach (ReportItem reportItem in reportItems)
				{
					if (reportItem is Microsoft.ReportingServices.RdlObjectModel.Rectangle)
					{
						if (this.TextBoxExistsInCollection(((Microsoft.ReportingServices.RdlObjectModel.Rectangle)reportItem).ReportItems, name))
						{
							return true;
						}
					}
					else if (reportItem is Matrix2005)
					{
						if (((Matrix2005)reportItem).Corner != null && this.TextBoxExistsInCollection(((Matrix2005)reportItem).Corner.ReportItems, name))
						{
							return true;
						}
					}
					else if (reportItem is Table2005)
					{
						Table2005 table = (Table2005)reportItem;
						if (table.Header != null && this.TextBoxExistsInCollection(table.Header.TableRows, name))
						{
							return true;
						}
						if (table.Footer != null && this.TextBoxExistsInCollection(table.Footer.TableRows, name))
						{
							return true;
						}
					}
					else if (reportItem is Textbox && reportItem.Name == name)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00007080 File Offset: 0x00005280
		private bool TextBoxExistsInCollection(IList<TableRow2005> rows, string name)
		{
			if (rows != null && rows.Count > 0)
			{
				foreach (TableRow2005 tableRow in rows)
				{
					IList<TableCell2005> tableCells = tableRow.TableCells;
					if (tableCells != null && tableCells.Count > 0)
					{
						foreach (TableCell2005 tableCell in tableCells)
						{
							if (this.TextBoxExistsInCollection(tableCell.ReportItems, name))
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00007128 File Offset: 0x00005328
		internal void UpgradeTable(Table2005 table)
		{
			this.UpgradeReportItem(table);
			this.UpgradePageBreak(table);
			int count = table.TableColumns.Count;
			table.TablixColumnHierarchy = new TablixHierarchy();
			for (int i = 0; i < count; i++)
			{
				TableColumn2005 tableColumn = table.TableColumns[i];
				TablixMember tablixMember = new TablixMember();
				tablixMember.Visibility = tableColumn.Visibility;
				tablixMember.FixedData = tableColumn.FixedHeader;
				table.TablixColumnHierarchy.TablixMembers.Add(tablixMember);
				TablixColumn tablixColumn = new TablixColumn();
				tablixColumn.Width = tableColumn.Width;
				table.TablixBody.TablixColumns.Add(tablixColumn);
			}
			table.TablixRowHierarchy = new TablixHierarchy();
			IList<TablixMember> list = table.TablixRowHierarchy.TablixMembers;
			int num = 0;
			int num2 = 0;
			bool flag = false;
			if (table.Header != null)
			{
				int i;
				for (i = 0; i < table.Header.TableRows.Count; i++)
				{
					TablixMember tablixMember = new TablixMember();
					list.Add(tablixMember);
					tablixMember.FixedData = table.Header.FixedHeader;
					tablixMember.KeepTogether = true;
					tablixMember.KeepWithGroup = KeepWithGroupTypes.After;
					if (table.Header.RepeatOnNewPage)
					{
						tablixMember.RepeatOnNewPage = true;
					}
					TablixRow tablixRow = this.UpgradeTableRow(table.Header.TableRows[i], table, i, tablixMember);
					this.FixAggregateFunction(tablixRow, ref flag);
				}
				num2 = (num = i);
			}
			if (table.Footer != null)
			{
				for (int i = 0; i < table.Footer.TableRows.Count; i++)
				{
					TablixMember tablixMember = new TablixMember();
					list.Add(tablixMember);
					tablixMember.KeepTogether = true;
					tablixMember.KeepWithGroup = KeepWithGroupTypes.Before;
					if (table.Footer.RepeatOnNewPage)
					{
						tablixMember.RepeatOnNewPage = true;
					}
					TablixRow tablixRow2 = this.UpgradeTableRow(table.Footer.TableRows[i], table, num2 + i, tablixMember);
					this.FixAggregateFunction(tablixRow2, ref flag);
				}
			}
			for (int i = 0; i < table.TableGroups.Count; i++)
			{
				TableGroup2005 tableGroup = table.TableGroups[i];
				TablixMember tablixMember = new TablixMember();
				list.Insert(num, tablixMember);
				tablixMember.Visibility = tableGroup.Visibility;
				tablixMember.Group = tableGroup.Grouping;
				tablixMember.SortExpressions = tableGroup.Sorting;
				this.TransferGroupingCustomProperties(tablixMember, new UpgradeImpl2005.GroupAccessor(UpgradeImpl2005.TablixMemberGroupAccessor), new UpgradeImpl2005.CustomPropertiesAccessor(UpgradeImpl2005.TablixMemberCustomPropertiesAccessor));
				list = tablixMember.TablixMembers;
				num = 0;
				if (tableGroup.Header != null)
				{
					int j;
					for (j = 0; j < tableGroup.Header.TableRows.Count; j++)
					{
						tablixMember = new TablixMember();
						list.Add(tablixMember);
						tablixMember.KeepTogether = true;
						tablixMember.KeepWithGroup = KeepWithGroupTypes.After;
						if (tableGroup.Header.RepeatOnNewPage)
						{
							tablixMember.RepeatOnNewPage = true;
						}
						TablixRow tablixRow3 = this.UpgradeTableRow(tableGroup.Header.TableRows[j], table, num2 + j, tablixMember);
						this.FixAggregateFunction(tablixRow3);
					}
					num = j;
					num2 += j;
				}
				if (tableGroup.Footer != null)
				{
					for (int j = 0; j < tableGroup.Footer.TableRows.Count; j++)
					{
						tablixMember = new TablixMember();
						list.Add(tablixMember);
						tablixMember.KeepTogether = true;
						tablixMember.KeepWithGroup = KeepWithGroupTypes.Before;
						if (tableGroup.Footer.RepeatOnNewPage)
						{
							tablixMember.RepeatOnNewPage = true;
						}
						TablixRow tablixRow4 = this.UpgradeTableRow(tableGroup.Footer.TableRows[j], table, num2 + j, tablixMember);
						this.FixAggregateFunction(tablixRow4);
					}
				}
				if (i == table.TableGroups.Count - 1 && tableGroup.Header == null && tableGroup.Footer == null && table.Details == null)
				{
					tablixMember = new TablixMember();
					list.Add(tablixMember);
					tablixMember.Visibility = new Visibility();
					tablixMember.Visibility.Hidden = true;
					TablixRow tablixRow5 = new TablixRow();
					table.TablixBody.TablixRows.Insert(num2, tablixRow5);
					for (int j = 0; j < count; j++)
					{
						TablixCell tablixCell = new TablixCell();
						tablixCell.CellContents = new CellContents();
						tablixRow5.TablixCells.Add(tablixCell);
					}
					num2++;
				}
			}
			Details2005 details = table.Details;
			if (details != null)
			{
				TablixMember tablixMember = new TablixMember();
				list.Insert(num, tablixMember);
				tablixMember.Visibility = details.Visibility;
				Microsoft.ReportingServices.RdlObjectModel.Group group = details.Grouping;
				if (group == null)
				{
					group = new Microsoft.ReportingServices.RdlObjectModel.Group();
					group.Name = this.UniqueName(table.Name + "_Details_Group", group);
				}
				tablixMember.Group = group;
				tablixMember.DataElementOutput = table.DetailDataElementOutput;
				if (table.DetailDataElementName == null)
				{
					tablixMember.Group.DataElementName = "Detail";
				}
				else
				{
					tablixMember.Group.DataElementName = table.DetailDataElementName;
				}
				if (table.DetailDataCollectionName == null)
				{
					tablixMember.DataElementName = tablixMember.Group.DataElementName + "_Collection";
				}
				else
				{
					tablixMember.DataElementName = table.DetailDataCollectionName;
				}
				this.TransferGroupingCustomProperties(tablixMember, new UpgradeImpl2005.GroupAccessor(UpgradeImpl2005.TablixMemberGroupAccessor), new UpgradeImpl2005.CustomPropertiesAccessor(UpgradeImpl2005.TablixMemberCustomPropertiesAccessor));
				for (int i = 0; i < details.TableRows.Count; i++)
				{
					TablixMember tablixMember2 = new TablixMember();
					tablixMember.TablixMembers.Add(tablixMember2);
					tablixMember.KeepTogether = true;
					TablixRow tablixRow6 = this.UpgradeTableRow(details.TableRows[i], table, num2 + i, tablixMember2);
					if (group.GroupExpressions.Count == 0)
					{
						string text = table.Name;
						if (table.TableGroups.Count > 0)
						{
							text = table.TableGroups[table.TableGroups.Count - 1].Grouping.Name;
						}
						this.FixAggregateFunction(tablixRow6, text, table.Name, false, ref flag);
						this.FixAggregateFunction(tablixMember2, text, table.Name, false, ref flag);
					}
				}
				if (details.Sorting != null && details.Sorting.Count != 0)
				{
					if (!flag || table.TableGroups.Count != 0 || this.SortingContainsAggregate(details.Sorting))
					{
						tablixMember.SortExpressions = details.Sorting;
					}
					else
					{
						table.SortExpressions = details.Sorting;
					}
				}
			}
			if (table.TablixBody.TablixRows.Count == 0)
			{
				if (table.TablixBody.TablixColumns.Count > 0)
				{
					TablixMember tablixMember = new TablixMember();
					table.TablixRowHierarchy.TablixMembers.Add(tablixMember);
					tablixMember.Visibility = new Visibility();
					tablixMember.Visibility.Hidden = true;
					TablixRow tablixRow7 = new TablixRow();
					table.TablixBody.TablixRows.Insert(num2, tablixRow7);
					for (int j = 0; j < count; j++)
					{
						TablixCell tablixCell2 = new TablixCell();
						tablixCell2.CellContents = new CellContents();
						tablixRow7.TablixCells.Add(tablixCell2);
					}
				}
				else
				{
					table.TablixBody = null;
				}
			}
			this.UpgradePageBreaks(table, true);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000077F4 File Offset: 0x000059F4
		private bool SortingContainsAggregate(IList<SortExpression> sortExpressions)
		{
			if (sortExpressions == null || sortExpressions.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < sortExpressions.Count; i++)
			{
				if (this.SortExpressionContainsAggregate(sortExpressions[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00007834 File Offset: 0x00005A34
		private bool SortExpressionContainsAggregate(SortExpression sortExpression)
		{
			return sortExpression != null && !(null == sortExpression.Value) && sortExpression.Value.Value != null && sortExpression.Value.IsExpression && this.ContainsRegexMatch(sortExpression.Value.Value, this.m_regexes.SpecialFunction, "sfname");
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00007898 File Offset: 0x00005A98
		private bool ContainsRegexMatch(string expression, Regex regex, string pattern)
		{
			if (string.IsNullOrEmpty(expression))
			{
				return false;
			}
			Match match;
			for (int i = 0; i < expression.Length; i = match.Index + match.Length)
			{
				match = regex.Match(expression, i);
				if (!match.Success)
				{
					return false;
				}
				if (match.Groups[pattern].Value.Length != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000078FC File Offset: 0x00005AFC
		private static bool IsToggleable(Visibility visibility)
		{
			return visibility != null && (visibility.ToggleItem != null || visibility.Hidden.IsExpression);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00007926 File Offset: 0x00005B26
		private void MergePageBreakLocation(BreakLocations breakLocation, PageBreak pageBreak)
		{
			if (breakLocation == BreakLocations.Start)
			{
				if (pageBreak.BreakLocation == BreakLocations.End || pageBreak.BreakLocation == BreakLocations.StartAndEnd)
				{
					pageBreak.BreakLocation = BreakLocations.StartAndEnd;
					return;
				}
			}
			else if (breakLocation == BreakLocations.End && (pageBreak.BreakLocation == BreakLocations.Start || pageBreak.BreakLocation == BreakLocations.StartAndEnd))
			{
				pageBreak.BreakLocation = BreakLocations.StartAndEnd;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00007964 File Offset: 0x00005B64
		private void UpgradePageBreaks(Tablix tablix, bool isTable)
		{
			if (tablix.TablixRowHierarchy != null)
			{
				IList<TablixMember> tablixMembers = tablix.TablixRowHierarchy.TablixMembers;
				if (tablixMembers != null && tablixMembers.Count > 0)
				{
					BreakLocations? breakLocations = this.UpgradePageBreaks(tablixMembers, UpgradeImpl2005.IsToggleable(tablix.Visibility), isTable);
					if (breakLocations != null)
					{
						if (tablix.PageBreak == null)
						{
							tablix.PageBreak = new PageBreak();
							tablix.PageBreak.BreakLocation = breakLocations.Value;
							return;
						}
						this.MergePageBreakLocation(breakLocations.Value, tablix.PageBreak);
					}
				}
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000079E8 File Offset: 0x00005BE8
		private BreakLocations? UpgradePageBreaks(IList<TablixMember> members, bool thisOrAnscestorHasToggle, bool isTable)
		{
			BreakLocations? breakLocations = null;
			bool flag = false;
			int num;
			if (isTable)
			{
				num = members.Count;
			}
			else
			{
				num = 1;
			}
			TablixMember tablixMember = null;
			for (int i = 0; i < num; i++)
			{
				TablixMember tablixMember2 = members[i];
				if (tablixMember2.Group != null)
				{
					tablixMember = tablixMember2;
					break;
				}
				if (isTable)
				{
					if (tablixMember2.RepeatOnNewPage)
					{
						flag = true;
					}
				}
				else
				{
					IList<TablixMember> tablixMembers = tablixMember2.TablixMembers;
					if (tablixMembers != null && tablixMembers.Count > 0)
					{
						breakLocations = this.UpgradePageBreaks(tablixMembers, thisOrAnscestorHasToggle, isTable);
					}
				}
			}
			if (tablixMember != null)
			{
				thisOrAnscestorHasToggle |= UpgradeImpl2005.IsToggleable(tablixMember.Visibility);
				IList<TablixMember> tablixMembers2 = tablixMember.TablixMembers;
				Microsoft.ReportingServices.RdlObjectModel.Group group = tablixMember.Group;
				PageBreak pageBreak = group.PageBreak;
				if (tablixMembers2 != null && tablixMembers2.Count > 0)
				{
					breakLocations = this.UpgradePageBreaks(tablixMembers2, thisOrAnscestorHasToggle, isTable);
					if (breakLocations != null)
					{
						if (pageBreak == null)
						{
							pageBreak = new PageBreak();
							pageBreak.BreakLocation = breakLocations.Value;
							group.PageBreak = pageBreak;
						}
						else
						{
							this.MergePageBreakLocation(breakLocations.Value, pageBreak);
						}
					}
				}
				if ((!isTable || flag) && pageBreak != null)
				{
					if (!thisOrAnscestorHasToggle)
					{
						breakLocations = new BreakLocations?(pageBreak.BreakLocation);
					}
					pageBreak.BreakLocation = BreakLocations.Between;
				}
			}
			return breakLocations;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00007B14 File Offset: 0x00005D14
		private ReportSize GetReportItemWidth(ReportItem reportItem)
		{
			ReportSize reportSize = reportItem.Width;
			ReportSize reportSize2 = default(ReportSize);
			IContainedObject containedObject = reportItem;
			while (reportSize.IsEmpty && containedObject != null)
			{
				if (containedObject is ReportItem)
				{
					reportSize = ((ReportItem)containedObject).Width;
					reportSize2 += ((ReportItem)containedObject).Left;
				}
				else
				{
					if (containedObject is Report)
					{
						reportSize = ((Report)containedObject).Width;
						break;
					}
					if (containedObject is TableCell2005)
					{
						TableCell2005 tableCell = (TableCell2005)containedObject;
						TableRow2005 tableRow = (TableRow2005)containedObject.Parent;
						Table2005 parentTable = this.GetParentTable(tableRow);
						int num = tableRow.TableCells.IndexOf((TableCell2005)containedObject);
						if (num < parentTable.TableColumns.Count)
						{
							reportSize = parentTable.TableColumns[num].Width;
						}
						int num2 = tableCell.ColSpan;
						while (--num2 > 0)
						{
							if (++num >= parentTable.TableColumns.Count)
							{
								break;
							}
							reportSize += parentTable.TableColumns[num].Width;
						}
						break;
					}
					if (containedObject is MatrixCell2005)
					{
						MatrixRow2005 matrixRow = (MatrixRow2005)containedObject.Parent;
						Matrix2005 matrix = (Matrix2005)matrixRow.Parent;
						int num3 = matrixRow.MatrixCells.IndexOf((MatrixCell2005)containedObject);
						if (num3 < matrix.MatrixColumns.Count)
						{
							reportSize = matrix.MatrixColumns[num3].Width;
							break;
						}
						break;
					}
				}
				containedObject = containedObject.Parent;
			}
			if (reportSize.IsEmpty)
			{
				reportSize = new ReportSize(0.0);
			}
			else
			{
				reportSize -= reportSize2;
			}
			return reportSize;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00007CB4 File Offset: 0x00005EB4
		private ReportSize GetReportItemHeight(ReportItem reportItem)
		{
			ReportSize reportSize = reportItem.Height;
			ReportSize reportSize2 = default(ReportSize);
			IContainedObject containedObject = reportItem;
			while (reportSize.IsEmpty && containedObject != null)
			{
				if (containedObject is ReportItem)
				{
					reportSize = ((ReportItem)containedObject).Height;
					if (reportSize.IsEmpty)
					{
						reportSize2 += ((ReportItem)containedObject).Top;
					}
				}
				else
				{
					if (containedObject is Body)
					{
						reportSize = ((Body)containedObject).Height;
						break;
					}
					if (containedObject is TableCell2005)
					{
						reportSize = ((TableRow2005)containedObject.Parent).Height;
						break;
					}
					if (containedObject is MatrixCell2005)
					{
						reportSize = ((MatrixRow2005)containedObject.Parent).Height;
						break;
					}
				}
				containedObject = containedObject.Parent;
			}
			if (reportSize.IsEmpty)
			{
				reportSize = new ReportSize(0.0);
			}
			else
			{
				reportSize -= reportSize2;
			}
			return reportSize;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00007D90 File Offset: 0x00005F90
		private Table2005 GetParentTable(TableRow2005 row)
		{
			for (IContainedObject containedObject = row.Parent; containedObject != null; containedObject = containedObject.Parent)
			{
				if (containedObject is Table2005)
				{
					return (Table2005)containedObject;
				}
			}
			return null;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00007DC0 File Offset: 0x00005FC0
		private void FixAggregateFunction(object obj)
		{
			bool flag = false;
			this.FixAggregateFunction(obj, null, null, true, ref flag);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00007DDB File Offset: 0x00005FDB
		private void FixAggregateFunction(object obj, ref bool containsPostSortAggregate)
		{
			this.FixAggregateFunction(obj, null, null, true, ref containsPostSortAggregate);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00007DE8 File Offset: 0x00005FE8
		private void FixAggregateFunction(object obj, string defaultScope, string dataRegion, bool fixPreviousAggregate, ref bool containsPostSortAggregate)
		{
			if (obj is IList)
			{
				foreach (object obj2 in ((IList)obj))
				{
					this.FixAggregateFunction(obj2, defaultScope, dataRegion, fixPreviousAggregate, ref containsPostSortAggregate);
				}
				return;
			}
			if (!(obj is ReportObject))
			{
				return;
			}
			foreach (MemberMapping memberMapping in ((StructMapping)TypeMapper.GetTypeMapping(obj.GetType())).Members)
			{
				object value = memberMapping.GetValue(obj);
				if (value != null)
				{
					if (typeof(IExpression).IsAssignableFrom(value.GetType()))
					{
						memberMapping.SetValue(obj, this.FixAggregateFunction((IExpression)value, defaultScope, dataRegion, fixPreviousAggregate, ref containsPostSortAggregate));
					}
					else
					{
						this.FixAggregateFunction(value, defaultScope, dataRegion, fixPreviousAggregate, ref containsPostSortAggregate);
					}
				}
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00007EF0 File Offset: 0x000060F0
		private IExpression FixAggregateFunction(IExpression value, string defaultScope, string dataRegion, bool fixPreviousAggregates, ref bool containsPostSortAggregate)
		{
			if (value != null && value.IsExpression && !string.IsNullOrEmpty(value.Expression))
			{
				string text = value.Expression;
				if (this.ContainsRegexMatch(text, this.m_regexes.PSAFunction, "psaname"))
				{
					containsPostSortAggregate = true;
				}
				if (this.m_regexes.SpecialFunction.IsMatch(text))
				{
					text = this.FixAggregateFunctions(text, delegate(string expr, int currentOffset, string specialFunctionName, int specialFunctionPos, int argumentsPos, int scopePos, int scopeLength, ref int offset)
					{
						if (!specialFunctionName.Equals("Previous", StringComparison.OrdinalIgnoreCase))
						{
							if (scopeLength == 0 && defaultScope != null)
							{
								string text2 = ((this.GetScopeArgumentIndex(specialFunctionName) > 0) ? ", " : "");
								expr = string.Concat(new string[]
								{
									expr.Substring(0, offset - 1),
									text2,
									"\"",
									defaultScope,
									"\")",
									expr.Substring(offset)
								});
								offset += defaultScope.Length + 4;
							}
						}
						else if (fixPreviousAggregates)
						{
							expr = this.FixPreviousAggregate(expr, currentOffset, specialFunctionPos, argumentsPos, ref offset);
						}
						return expr;
					});
					value = (IExpression)Activator.CreateInstance(value.GetType());
					value.Expression = text;
				}
			}
			return value;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00007F98 File Offset: 0x00006198
		private string FixPreviousAggregate(string expr, int currentOffset, int specialFunctionPos, int argumentsPos, ref int offset)
		{
			Match match = this.m_regexes.SpecialFunction.Match(expr, argumentsPos);
			if (match.Success)
			{
				global::System.Text.RegularExpressions.Group group = match.Groups["sfname"];
				if (group.Length > 0 && group.Index <= offset)
				{
					return expr;
				}
			}
			int num;
			int num2;
			int num3;
			if (this.FindArgument(currentOffset, expr, out num, 0, out num2, out num3))
			{
				Match match2 = this.m_regexes.FieldDetection.Match(expr, num2 - 1);
				if (match2.Success)
				{
					int num4 = 0;
					int num5 = num2;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(expr.Substring(0, num5));
					while (match2.Success && num5 < num2 + num3)
					{
						global::System.Text.RegularExpressions.Group group2 = match2.Groups["detected"];
						if (group2.Length > 0)
						{
							stringBuilder.Append(expr.Substring(num5, group2.Index - num5));
							int num6 = 0;
							int num7 = group2.Index;
							while (num7 < num2 + num3 && !this.IsNotPartOfReference(expr[num7]))
							{
								if (expr[num7] == '(')
								{
									num6++;
								}
								else if (expr[num7] == ')')
								{
									if (num6 == 0)
									{
										break;
									}
									num6--;
								}
								num7++;
							}
							stringBuilder.Append("Last(");
							stringBuilder.Append(expr.Substring(group2.Index, num7 - group2.Index));
							stringBuilder.Append(")");
							num5 = num7;
							num4++;
						}
						match2 = match2.NextMatch();
					}
					stringBuilder.Append(expr.Substring(num5));
					expr = stringBuilder.ToString();
					offset += num4 * 6;
				}
			}
			return expr;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00008158 File Offset: 0x00006358
		private bool IsNotPartOfReference(char c)
		{
			if (c <= '&')
			{
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					return false;
				default:
					if (c != ' ' && c != '&')
					{
						return false;
					}
					break;
				}
			}
			else if (c <= '>')
			{
				switch (c)
				{
				case '*':
				case '+':
				case '-':
				case '/':
					break;
				case ',':
				case '.':
					return false;
				default:
					switch (c)
					{
					case '<':
					case '=':
					case '>':
						break;
					default:
						return false;
					}
					break;
				}
			}
			else if (c != '\\' && c != '^')
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000081E0 File Offset: 0x000063E0
		private string FixAggregateFunctions(string expression, UpgradeImpl2005.AggregateFunctionFixup fixup)
		{
			int i = 0;
			while (i < expression.Length)
			{
				Match match = this.m_regexes.SpecialFunction.Match(expression, i);
				if (!match.Success)
				{
					break;
				}
				global::System.Text.RegularExpressions.Group group = match.Groups["sfname"];
				string value = group.Value;
				if (value.Length == 0)
				{
					i = match.Index + match.Length;
				}
				else
				{
					int index = group.Index;
					int num = index + group.Length;
					i = match.Index + match.Length;
					int scopeArgumentIndex = this.GetScopeArgumentIndex(value);
					int num2;
					int num3;
					this.FindArgument(i, expression, out i, scopeArgumentIndex, out num2, out num3);
					expression = fixup(expression, match.Index + match.Length, value, index, num, num2, num3, ref i);
				}
			}
			return expression;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000082AC File Offset: 0x000064AC
		private TablixRow UpgradeTableRow(TableRow2005 tableRow, Tablix tablix, int rowIndex, TablixMember tablixMember)
		{
			tablixMember.Visibility = tableRow.Visibility;
			TablixRow tablixRow = new TablixRow();
			tablix.TablixBody.TablixRows.Insert(rowIndex, tablixRow);
			tablixRow.Height = tableRow.Height;
			IList<TablixMember> tablixMembers = tablix.TablixColumnHierarchy.TablixMembers;
			int num = 0;
			foreach (TableCell2005 tableCell in tableRow.TableCells)
			{
				TablixCell tablixCell = new TablixCell();
				tablixRow.TablixCells.Add(tablixCell);
				tablixCell.CellContents = new CellContents();
				if (tableCell.ReportItems.Count > 0)
				{
					tablixCell.CellContents.ReportItem = tableCell.ReportItems[0];
				}
				if (tableCell.ColSpan > 1)
				{
					int num2 = num + tableCell.ColSpan - 1;
					bool flag = tablixMembers[num].FixedData || tablixMembers[num2].FixedData;
					tablixCell.CellContents.ColSpan = tableCell.ColSpan;
					for (int i = 0; i < tableCell.ColSpan; i++)
					{
						if (i > 0)
						{
							tablixRow.TablixCells.Add(new TablixCell());
						}
						if (flag)
						{
							tablixMembers[num].FixedData = true;
						}
						num++;
					}
				}
				else
				{
					num++;
				}
			}
			return tablixRow;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00008414 File Offset: 0x00006614
		internal void UpgradeChart(Chart2005 chart2005)
		{
			this.UpgradeReportItem(chart2005);
			this.UpgradePageBreak(chart2005);
			if (chart2005.CustomProperties == null)
			{
				chart2005.CustomProperties = new List<CustomProperty>();
			}
			CustomProperty customProperty = new CustomProperty();
			customProperty.Name = "__Upgraded2005__";
			customProperty.Value = "__Upgraded2005__";
			chart2005.CustomProperties.Add(customProperty);
			IList<ChartMember> list = null;
			ChartMember chartMember = null;
			foreach (SeriesGrouping2005 seriesGrouping in chart2005.SeriesGroupings)
			{
				if (list == null)
				{
					chart2005.ChartSeriesHierarchy = new ChartSeriesHierarchy();
					list = chart2005.ChartSeriesHierarchy.ChartMembers;
				}
				DynamicSeries2005 dynamicSeries = seriesGrouping.DynamicSeries;
				if (dynamicSeries != null)
				{
					ChartMember chartMember2 = new ChartMember();
					list.Add(chartMember2);
					list = chartMember2.ChartMembers;
					chartMember = chartMember2;
					chartMember2.Group = dynamicSeries.Grouping;
					chartMember2.SortExpressions = dynamicSeries.Sorting;
					chartMember2.Label = dynamicSeries.Label;
					chartMember2.PropertyStore.SetObject(4, dynamicSeries.LabelLocID);
					chartMember2.DataElementName = dynamicSeries.Grouping.DataCollectionName;
					chartMember2.DataElementOutput = dynamicSeries.Grouping.DataElementOutput;
					this.TransferGroupingCustomProperties(chartMember2, new UpgradeImpl2005.GroupAccessor(UpgradeImpl2005.ChartMemberGroupAccessor), new UpgradeImpl2005.CustomPropertiesAccessor(UpgradeImpl2005.ChartMemberCustomPropertiesAccessor));
				}
				else
				{
					foreach (StaticMember2005 staticMember in seriesGrouping.StaticSeries)
					{
						ChartMember chartMember2 = new ChartMember();
						list.Add(chartMember2);
						chartMember2.Label = staticMember.Label;
						chartMember2.PropertyStore.SetObject(4, staticMember.LabelLocID);
					}
					if (list.Count > 0)
					{
						chartMember = list[0];
						list = chartMember.ChartMembers;
					}
				}
			}
			list = null;
			foreach (CategoryGrouping2005 categoryGrouping in chart2005.CategoryGroupings)
			{
				if (list == null)
				{
					chart2005.ChartCategoryHierarchy = new ChartCategoryHierarchy();
					list = chart2005.ChartCategoryHierarchy.ChartMembers;
				}
				DynamicSeries2005 dynamicCategories = categoryGrouping.DynamicCategories;
				if (dynamicCategories != null)
				{
					ChartMember chartMember3 = new ChartMember();
					list.Add(chartMember3);
					list = chartMember3.ChartMembers;
					chartMember3.Group = dynamicCategories.Grouping;
					chartMember3.SortExpressions = dynamicCategories.Sorting;
					chartMember3.Label = dynamicCategories.Label;
					chartMember3.PropertyStore.SetObject(4, dynamicCategories.LabelLocID);
					chartMember3.DataElementName = dynamicCategories.Grouping.DataCollectionName;
					chartMember3.DataElementOutput = dynamicCategories.Grouping.DataElementOutput;
					this.TransferGroupingCustomProperties(chartMember3, new UpgradeImpl2005.GroupAccessor(UpgradeImpl2005.ChartMemberGroupAccessor), new UpgradeImpl2005.CustomPropertiesAccessor(UpgradeImpl2005.ChartMemberCustomPropertiesAccessor));
				}
				else
				{
					using (IEnumerator<StaticMember2005> enumerator2 = categoryGrouping.StaticCategories.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							StaticMember2005 staticMember2 = enumerator2.Current;
							ChartMember chartMember3 = new ChartMember();
							list.Add(chartMember3);
							chartMember3.Label = staticMember2.Label;
							chartMember3.PropertyStore.SetObject(4, staticMember2.LabelLocID);
						}
						break;
					}
				}
			}
			if (chart2005.Palette.Value == ChartPalettes.GrayScale)
			{
				chart2005.PaletteHatchBehavior = ChartPaletteHatchBehaviorTypes.Always;
			}
			if (chart2005.Action != null)
			{
				chart2005.ActionInfo = new ActionInfo();
				chart2005.ActionInfo.Actions.Add(chart2005.Action);
			}
			if (chart2005.NoRows != null)
			{
				chart2005.ChartNoDataMessage = new ChartTitle();
				chart2005.ChartNoDataMessage.Name = "NoDataMessageTitle";
				chart2005.ChartNoDataMessage.Caption = chart2005.NoRows.ToString();
			}
			ChartArea chartArea = new ChartArea();
			chart2005.ChartAreas.Add(chartArea);
			chartArea.Name = "Default";
			if (chart2005.ThreeDProperties != null)
			{
				chartArea.ChartThreeDProperties = new ChartThreeDProperties();
				chartArea.ChartThreeDProperties.Clustered = !chart2005.ThreeDProperties.Clustered.Value;
				chartArea.ChartThreeDProperties.DepthRatio = chart2005.ThreeDProperties.DepthRatio;
				chartArea.ChartThreeDProperties.Enabled = chart2005.ThreeDProperties.Enabled;
				chartArea.ChartThreeDProperties.GapDepth = chart2005.ThreeDProperties.GapDepth;
				chartArea.ChartThreeDProperties.Inclination = chart2005.ThreeDProperties.Rotation;
				chartArea.ChartThreeDProperties.Rotation = chart2005.ThreeDProperties.Inclination;
				chartArea.ChartThreeDProperties.Shading = chart2005.ThreeDProperties.Shading;
				ChartProjectionModes projectionMode = (ChartProjectionModes)chart2005.ThreeDProperties.ProjectionMode;
				chartArea.ChartThreeDProperties.ProjectionMode = projectionMode;
				if (projectionMode == ChartProjectionModes.Perspective)
				{
					chartArea.ChartThreeDProperties.Perspective = chart2005.ThreeDProperties.Perspective;
				}
				int num = (int)(30.0 * ((double)chart2005.ThreeDProperties.WallThickness / 100.0));
				chartArea.ChartThreeDProperties.WallThickness = ((num > 30 || num < 0) ? 7 : num);
			}
			if (chart2005.PlotArea != null)
			{
				chartArea.Style = chart2005.PlotArea.Style;
				this.FixYukonChartBorderWidth(chartArea.Style, false);
			}
			if (chart2005.Style != null && chart2005.Style.BackgroundImage != null)
			{
				if (chartArea.Style == null)
				{
					chartArea.Style = new Style();
				}
				chartArea.Style.BackgroundImage = chart2005.Style.BackgroundImage;
				chart2005.Style.BackgroundImage = null;
			}
			if (chart2005.Title != null && chart2005.Title.Caption.Value.Length > 0)
			{
				ChartTitle chartTitle = new ChartTitle();
				chartTitle.Name = "Default";
				chart2005.ChartTitles.Add(chartTitle);
				chartTitle.Caption = chart2005.Title.Caption;
				chartTitle.PropertyStore.SetObject(2, chart2005.Title.PropertyStore.GetObject(2));
				chartTitle.Style = chart2005.Title.Style;
			}
			ChartLegend chartLegend = new ChartLegend();
			chartLegend.AutoFitTextDisabled = true;
			chartLegend.Name = "Default";
			chart2005.ChartLegends.Add(chartLegend);
			chartLegend.Hidden = !chart2005.Legend.Visible;
			chartLegend.Style = this.FixYukonEmptyBorderStyle(chart2005.Legend.Style);
			this.FixYukonChartBorderWidth(chartLegend.Style, false);
			chartLegend.Position = chart2005.Legend.Position;
			chartLegend.Layout = new ReportExpression<ChartLegendLayouts>((ChartLegendLayouts)chart2005.Legend.Layout);
			if (chart2005.Legend.InsidePlotArea)
			{
				chartLegend.DockOutsideChartArea = !chart2005.Legend.InsidePlotArea;
				chartLegend.DockToChartArea = chartArea.Name;
			}
			if (chart2005.CategoryAxis != null)
			{
				chartArea.ChartCategoryAxes.Add(this.UpgradeChartAxis(chart2005.CategoryAxis.Axis, true, chart2005.Type));
				chartArea.ChartCategoryAxes[0].Name = "Primary";
			}
			if (chart2005.ValueAxis != null)
			{
				chartArea.ChartValueAxes.Add(this.UpgradeChartAxis(chart2005.ValueAxis.Axis, false, chart2005.Type));
				chartArea.ChartValueAxes[0].Name = "Primary";
			}
			if (chart2005.ChartData != null && chart2005.ChartData.Count > 0)
			{
				chart2005.ChartData = new ChartData();
				foreach (ChartSeries2005 chartSeries in chart2005.ChartData)
				{
					ChartSeries chartSeries2 = new ChartSeries();
					chartSeries2.CategoryAxisName = "Primary";
					chartSeries2.ValueAxisName = "Primary";
					this.SetChartTypes(chart2005, chartSeries.PlotType, chartSeries2);
					double num2 = 0.8;
					if (chart2005.PointWidth > 0)
					{
						num2 = Math.Min((double)chart2005.PointWidth / 100.0, 2.0);
					}
					else if (chart2005.Type == ChartTypes2005.Bar || chart2005.Type == ChartTypes2005.Column)
					{
						num2 = 0.6;
					}
					if (num2 != 0.8)
					{
						CustomProperty customProperty2 = new CustomProperty();
						customProperty2.Name = "PointWidth";
						customProperty2.Value = num2.ToString(CultureInfo.InvariantCulture.NumberFormat);
						chartSeries2.CustomProperties.Add(customProperty2);
					}
					if ((chart2005.Type == ChartTypes2005.Bar || chart2005.Type == ChartTypes2005.Column) && chart2005.ThreeDProperties != null && chart2005.ThreeDProperties.Enabled == true && chart2005.ThreeDProperties.DrawingStyle != DrawingStyleTypes2005.Cube)
					{
						CustomProperty customProperty3 = new CustomProperty();
						customProperty3.Name = "DrawingStyle";
						customProperty3.Value = chart2005.ThreeDProperties.DrawingStyle.ToString();
						chartSeries2.CustomProperties.Add(customProperty3);
					}
					chart2005.ChartData.ChartSeriesCollection.Add(chartSeries2);
					chartSeries2.Name = "Series" + chart2005.ChartData.ChartSeriesCollection.Count.ToString(CultureInfo.InvariantCulture.NumberFormat);
					foreach (DataPoint2005 dataPoint in chartSeries.DataPoints)
					{
						ChartDataPoint chartDataPoint = dataPoint;
						chartDataPoint.DataElementName = dataPoint.DataElementName;
						chartDataPoint.DataElementOutput = ((dataPoint.DataElementOutput == DataElementOutputTypes.Output) ? DataElementOutputTypes.ContentsOnly : dataPoint.DataElementOutput);
						if (dataPoint.Style != null)
						{
							chartDataPoint.Style = new EmptyColorStyle(dataPoint.Style.PropertyStore);
							if (chartDataPoint.Style.Border != null && chartDataPoint.Style.Border.Style == BorderStyles.None)
							{
								chartDataPoint.Style.Border.Style = BorderStyles.Solid;
							}
						}
						if (chartSeries2.Type == ChartTypes.Line)
						{
							if (chartDataPoint.Style == null)
							{
								chartDataPoint.Style = new EmptyColorStyle();
							}
							if (chartDataPoint.Style.Border == null)
							{
								chartDataPoint.Style.Border = new EmptyBorder();
							}
							if (!chartDataPoint.Style.Border.Width.IsExpression && chartDataPoint.Style.Border.Width.Value.IsEmpty)
							{
								chartDataPoint.Style.Border.Width = new ReportSize(2.25, SizeTypes.Point);
							}
							else
							{
								this.FixYukonChartBorderWidth(chartDataPoint.Style, false);
							}
							if (!chartDataPoint.Style.Border.Color.Value.IsEmpty || chartDataPoint.Style.Border.Color.IsExpression)
							{
								chartDataPoint.Style.Color = chartDataPoint.Style.Border.Color;
								chartDataPoint.Style.Border.Color = ReportColor.Empty;
							}
						}
						else
						{
							if (chartDataPoint.Style != null && (!chartDataPoint.Style.BackgroundColor.Value.IsEmpty || chartDataPoint.Style.BackgroundColor.IsExpression))
							{
								chartDataPoint.Style.Color = chartDataPoint.Style.BackgroundColor;
								chartDataPoint.Style.BackgroundColor = ReportColor.Empty;
							}
							this.FixYukonChartBorderWidth(chartDataPoint.Style, false);
						}
						if (chart2005.Type == ChartTypes2005.Pie || chart2005.Type == ChartTypes2005.Doughnut)
						{
							if (chartDataPoint.Style == null)
							{
								chartDataPoint.Style = new EmptyColorStyle();
							}
							if (chartDataPoint.Style.Border == null)
							{
								chartDataPoint.Style.Border = new EmptyBorder();
							}
							if (!chartDataPoint.Style.Border.Color.IsExpression && chartDataPoint.Style.Border.Color.Value.IsEmpty)
							{
								chartDataPoint.Style.Border.Color = new ReportColor(Color.Black);
							}
						}
						if (dataPoint.DataValues != null && dataPoint.DataValues.Count > 0)
						{
							chartDataPoint.ChartDataPointValues = new ChartDataPointValues();
							ChartTypes2005 type = chart2005.Type;
							if (type != ChartTypes2005.Scatter)
							{
								if (type != ChartTypes2005.Bubble)
								{
									if (type == ChartTypes2005.Stock)
									{
										string[] array;
										if (chart2005.Subtype == ChartSubtypes2005.HighLowClose)
										{
											array = UpgradeImpl2005.m_highLowCloseDataPointNames;
										}
										else
										{
											array = UpgradeImpl2005.m_openHighLowCloseDataPointNames;
										}
										this.SetChartDataPointNames(dataPoint, array);
									}
								}
								else
								{
									this.SetChartDataPointNames(dataPoint, UpgradeImpl2005.m_bubbleChartDataPointNames);
								}
							}
							else
							{
								this.SetChartDataPointNames(dataPoint, UpgradeImpl2005.m_scatterChartDataPointNames);
							}
							foreach (DataValue2005 dataValue in dataPoint.DataValues)
							{
								string name = dataValue.Name;
								if (!(name == "X"))
								{
									if (!(name == "High"))
									{
										if (!(name == "Low"))
										{
											if (!(name == "Open"))
											{
												if (!(name == "Close"))
												{
													if (!(name == "Size"))
													{
														chartDataPoint.ChartDataPointValues.Y = dataValue.Value;
													}
													else
													{
														chartDataPoint.ChartDataPointValues.Size = dataValue.Value;
													}
												}
												else
												{
													chartDataPoint.ChartDataPointValues.End = dataValue.Value;
												}
											}
											else
											{
												chartDataPoint.ChartDataPointValues.Start = dataValue.Value;
											}
										}
										else
										{
											chartDataPoint.ChartDataPointValues.Low = dataValue.Value;
										}
									}
									else
									{
										chartDataPoint.ChartDataPointValues.High = dataValue.Value;
									}
								}
								else
								{
									chartDataPoint.ChartDataPointValues.X = dataValue.Value;
								}
							}
						}
						if (dataPoint.Action != null)
						{
							dataPoint.ActionInfo = new ActionInfo();
							dataPoint.ActionInfo.Actions.Add(dataPoint.Action);
						}
						if (dataPoint.DataLabel != null)
						{
							ChartDataLabel chartDataLabel = new ChartDataLabel();
							chartDataPoint.ChartDataLabel = chartDataLabel;
							chartDataLabel.Visible = dataPoint.DataLabel.Visible;
							chartDataLabel.UseValueAsLabel = dataPoint.DataLabel.Visible && dataPoint.DataLabel.Value == null;
							chartDataLabel.Style = dataPoint.DataLabel.Style;
							chartDataLabel.Label = dataPoint.DataLabel.Value;
							chartDataLabel.PropertyStore.SetObject(2, dataPoint.DataLabel.ValueLocID);
							chartDataLabel.Rotation = dataPoint.DataLabel.Rotation;
							if (dataPoint.DataLabel.Position != ChartDataLabelPositions.Auto)
							{
								if (chartSeries2.ChartSmartLabel == null)
								{
									chartSeries2.ChartSmartLabel = new ChartSmartLabel();
								}
								chartSeries2.ChartSmartLabel.Disabled = true;
							}
							if ((chart2005.Type == ChartTypes2005.Pie || chart2005.Type == ChartTypes2005.Doughnut) && dataPoint.DataLabel.Position != ChartDataLabelPositions.Auto && dataPoint.DataLabel.Position != ChartDataLabelPositions.Center)
							{
								CustomProperty customProperty4 = new CustomProperty();
								customProperty4.Name = "PieLabelStyle";
								customProperty4.Value = "Outside";
								dataPoint.CustomProperties.Add(customProperty4);
							}
							else
							{
								chartDataLabel.Position = dataPoint.DataLabel.Position;
							}
						}
						if (dataPoint.Marker != null)
						{
							chartDataPoint.ChartMarker = new ChartMarker();
							chartDataPoint.ChartMarker.Type = dataPoint.Marker.Type;
							chartDataPoint.ChartMarker.Size = dataPoint.Marker.Size;
							if (dataPoint.Marker.Style != null)
							{
								chartDataPoint.ChartMarker.Style = new EmptyColorStyle(dataPoint.Marker.Style.PropertyStore);
								chartDataPoint.ChartMarker.Style.Color = chartDataPoint.ChartMarker.Style.BackgroundColor;
								chartDataPoint.ChartMarker.Style.BackgroundColor = ReportColor.Empty;
							}
						}
						if (chart2005.Type == ChartTypes2005.Bubble)
						{
							if (chartDataPoint.ChartMarker == null)
							{
								chartDataPoint.ChartMarker = new ChartMarker();
							}
							if (chartDataPoint.ChartMarker.Type == ChartMarkerTypes.None)
							{
								chartDataPoint.ChartMarker.Type = ChartMarkerTypes.Circle;
							}
						}
						if (chart2005.Palette.Value == ChartPalettes.GrayScale)
						{
							if (chartDataPoint.Style == null)
							{
								chartDataPoint.Style = new EmptyColorStyle();
							}
							else if (chartDataPoint.Style.Color.IsExpression || !chartDataPoint.Style.Color.Value.IsEmpty || chartDataPoint.Style.BackgroundGradientType.IsExpression || (chartDataPoint.Style.BackgroundGradientType.Value != BackgroundGradients.Default && chartDataPoint.Style.BackgroundGradientType.Value != BackgroundGradients.None))
							{
								chartDataPoint.Style.BackgroundHatchType = BackgroundHatchTypes.None;
							}
							if (chartDataPoint.Style.Border == null)
							{
								chartDataPoint.Style.Border = new EmptyBorder();
								chartDataPoint.Style.Border.Color = new ReportColor(Color.Black);
								chartDataPoint.Style.Border.Width = new ReportSize(0.75, SizeTypes.Point);
								chartDataPoint.Style.Border.Style = BorderStyles.Solid;
							}
							else if (!chartDataPoint.Style.Border.Color.IsExpression && chartDataPoint.Style.Border.Color.Value.IsEmpty)
							{
								chartDataPoint.Style.Border.Color = new ReportColor(Color.Black);
							}
						}
						chartSeries2.ChartDataPoints.Add(chartDataPoint);
					}
				}
			}
			if (chart2005.ChartCategoryHierarchy == null || chart2005.ChartCategoryHierarchy.ChartMembers == null || chart2005.ChartCategoryHierarchy.ChartMembers.Count == 0)
			{
				if (chart2005.ChartCategoryHierarchy == null)
				{
					chart2005.ChartCategoryHierarchy = new ChartCategoryHierarchy();
				}
				if (chart2005.ChartCategoryHierarchy.ChartMembers == null)
				{
					chart2005.ChartCategoryHierarchy.ChartMembers = new RdlCollection<ChartMember>();
				}
				if (chart2005.ChartData != null && chart2005.ChartData.ChartSeriesCollection != null && chart2005.ChartData.ChartSeriesCollection.Count > 0)
				{
					using (IEnumerator<ChartDataPoint> enumerator7 = chart2005.ChartData.ChartSeriesCollection[0].ChartDataPoints.GetEnumerator())
					{
						while (enumerator7.MoveNext())
						{
							ChartDataPoint chartDataPoint2 = enumerator7.Current;
							chart2005.ChartCategoryHierarchy.ChartMembers.Add(new ChartMember());
						}
						goto IL_13B7;
					}
				}
				chart2005.ChartCategoryHierarchy.ChartMembers.Add(new ChartMember());
			}
			IL_13B7:
			if (chart2005.ChartSeriesHierarchy == null || chart2005.ChartSeriesHierarchy.ChartMembers == null || chart2005.ChartSeriesHierarchy.ChartMembers.Count == 0)
			{
				if (chart2005.ChartSeriesHierarchy == null)
				{
					chart2005.ChartSeriesHierarchy = new ChartSeriesHierarchy();
				}
				if (chart2005.ChartSeriesHierarchy.ChartMembers == null)
				{
					chart2005.ChartSeriesHierarchy.ChartMembers = new RdlCollection<ChartMember>();
				}
				chart2005.ChartSeriesHierarchy.ChartMembers.Add(new ChartMember());
			}
			if (chart2005.ChartData == null || chart2005.ChartData.ChartSeriesCollection == null || chart2005.ChartData.ChartSeriesCollection.Count == 0)
			{
				if (chart2005.ChartData == null)
				{
					chart2005.ChartData = new ChartData();
				}
				if (chart2005.ChartData.ChartSeriesCollection == null)
				{
					chart2005.ChartData.ChartSeriesCollection = new RdlCollection<ChartSeries>();
				}
				ChartSeries chartSeries3 = new ChartSeries();
				chartSeries3.Name = "emptySeriesName";
				chartSeries3.ChartDataPoints.Add(new ChartDataPoint());
				chart2005.ChartData.ChartSeriesCollection.Add(chartSeries3);
			}
			int num3 = chart2005.SeriesGroupings.Count;
			while (--num3 >= 0)
			{
				if (chart2005.SeriesGroupings[num3].StaticSeries.Count > 0 && num3 < chart2005.SeriesGroupings.Count - 1)
				{
					this.CloneChartSeriesHierarchy(chart2005, chartMember);
				}
				if (num3 > 0)
				{
					chartMember = (ChartMember)chartMember.Parent;
				}
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000099F4 File Offset: 0x00007BF4
		private void CloneChartSeriesHierarchy(Chart chart, ChartMember staticMember)
		{
			if (staticMember.ChartMembers.Count == 0)
			{
				return;
			}
			ChartData chartData = chart.ChartData;
			IList<ChartMember> siblingChartMembers = this.GetSiblingChartMembers(staticMember);
			int count = siblingChartMembers.Count;
			for (int i = 1; i < count; i++)
			{
				ChartMember chartMember = siblingChartMembers[i];
				UpgradeImpl2005.Cloner cloner = new UpgradeImpl2005.Cloner(this);
				chartMember.ChartMembers = (IList<ChartMember>)cloner.Clone(staticMember.ChartMembers);
				cloner.FixReferences();
				int num = chartData.ChartSeriesCollection.Count / count;
				int num2 = num;
				int num3 = i * num;
				while (num2-- > 0)
				{
					cloner.FixReferences(chartData.ChartSeriesCollection[num3].ChartDataPoints);
					num3++;
				}
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00009AA4 File Offset: 0x00007CA4
		private IList<ChartMember> GetSiblingChartMembers(ChartMember chartMember)
		{
			if (chartMember.Parent is ChartSeriesHierarchy)
			{
				return ((ChartSeriesHierarchy)chartMember.Parent).ChartMembers;
			}
			if (chartMember.Parent is ChartCategoryHierarchy)
			{
				return ((ChartCategoryHierarchy)chartMember.Parent).ChartMembers;
			}
			return ((ChartMember)chartMember.Parent).ChartMembers;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00009B00 File Offset: 0x00007D00
		private ChartAxis UpgradeChartAxis(Axis2005 axis2005, bool categoryAxis, ChartTypes2005 charType)
		{
			ChartAxis chartAxis = new ChartAxis();
			chartAxis.HideLabels = !axis2005.Visible;
			chartAxis.Margin = new ReportExpression<ChartAxisMarginVisibleTypes>(axis2005.Margin.ToString(), CultureInfo.InvariantCulture);
			chartAxis.Reverse = axis2005.Reverse;
			chartAxis.CrossAt = axis2005.CrossAt;
			chartAxis.Interlaced = axis2005.Interlaced;
			chartAxis.Scalar = axis2005.Scalar;
			chartAxis.LogScale = axis2005.LogScale;
			chartAxis.PreventFontShrink = true;
			chartAxis.PreventFontGrow = true;
			chartAxis.Style = this.FixYukonEmptyBorderStyle(axis2005.Style);
			this.FixYukonChartBorderWidth(chartAxis.Style, true);
			if (!categoryAxis || chartAxis.Scalar)
			{
				chartAxis.Minimum = axis2005.Min;
				chartAxis.Maximum = axis2005.Max;
			}
			chartAxis.IncludeZero = false;
			double num = double.NaN;
			double num2 = double.NaN;
			if (axis2005.MajorGridLines != null)
			{
				chartAxis.ChartMajorGridLines = new ChartGridLines();
				chartAxis.ChartMajorGridLines.Enabled = new ReportExpression<ChartGridLinesEnabledTypes>(axis2005.MajorGridLines.ShowGridLines.ToString(), CultureInfo.InvariantCulture);
				chartAxis.ChartMajorGridLines.Style = this.FixYukonEmptyBorderStyle(axis2005.MajorGridLines.Style);
				this.FixYukonChartBorderWidth(chartAxis.ChartMajorGridLines.Style, true);
				if (axis2005.MajorInterval.IsExpression)
				{
					chartAxis.ChartMajorGridLines.Interval = new ReportExpression<double>(axis2005.MajorInterval.ToString(), CultureInfo.InvariantCulture);
				}
				else
				{
					num = this.ConvertToDouble(axis2005.MajorInterval.Value);
					if (num < 0.0)
					{
						num = double.NaN;
					}
					chartAxis.ChartMajorGridLines.Interval = num;
					if (!chartAxis.Scalar && chartAxis.Margin == ChartAxisMarginVisibleTypes.True)
					{
						chartAxis.ChartMajorGridLines.IntervalOffset = 1.0;
					}
				}
			}
			if (axis2005.MinorGridLines != null)
			{
				chartAxis.ChartMinorGridLines = new ChartGridLines();
				chartAxis.ChartMinorGridLines.Enabled = new ReportExpression<ChartGridLinesEnabledTypes>(axis2005.MinorGridLines.ShowGridLines.ToString(), CultureInfo.InvariantCulture);
				chartAxis.ChartMinorGridLines.Style = this.FixYukonEmptyBorderStyle(axis2005.MinorGridLines.Style);
				this.FixYukonChartBorderWidth(chartAxis.ChartMinorGridLines.Style, true);
				if (axis2005.MinorInterval.IsExpression)
				{
					chartAxis.ChartMinorGridLines.Interval = new ReportExpression<double>(axis2005.MinorInterval.ToString(), CultureInfo.InvariantCulture);
				}
				else
				{
					num2 = this.ConvertToDouble(axis2005.MinorInterval.Value);
					if (num2 < 0.0)
					{
						num2 = double.NaN;
					}
					chartAxis.ChartMinorGridLines.Interval = num2;
					if (!chartAxis.Scalar && chartAxis.Margin == ChartAxisMarginVisibleTypes.False)
					{
						chartAxis.ChartMinorGridLines.IntervalOffset = -1.0;
					}
				}
			}
			chartAxis.ChartMajorTickMarks = new ChartTickMarks();
			chartAxis.ChartMajorTickMarks.Type = new ReportExpression<ChartTickMarkTypes>(axis2005.MajorTickMarks.ToString(), CultureInfo.InvariantCulture);
			if (axis2005.MajorTickMarks != TickMarks2005.None)
			{
				if (chartAxis.ChartMajorGridLines != null)
				{
					chartAxis.ChartMajorTickMarks.Style = chartAxis.ChartMajorGridLines.Style;
					chartAxis.ChartMajorTickMarks.Interval = chartAxis.ChartMajorGridLines.Interval;
					chartAxis.ChartMajorTickMarks.IntervalOffset = chartAxis.ChartMajorGridLines.IntervalOffset;
				}
				chartAxis.ChartMajorTickMarks.Enabled = ChartTickMarksEnabledTypes.True;
			}
			chartAxis.ChartMinorTickMarks = new ChartTickMarks();
			chartAxis.ChartMinorTickMarks.Type = new ReportExpression<ChartTickMarkTypes>(axis2005.MinorTickMarks.ToString(), CultureInfo.InvariantCulture);
			if (axis2005.MinorTickMarks != TickMarks2005.None)
			{
				if (chartAxis.ChartMinorGridLines != null)
				{
					chartAxis.ChartMinorTickMarks.Style = chartAxis.ChartMinorGridLines.Style;
					chartAxis.ChartMinorTickMarks.Interval = chartAxis.ChartMinorGridLines.Interval;
					chartAxis.ChartMinorTickMarks.IntervalOffset = chartAxis.ChartMinorGridLines.IntervalOffset;
				}
				chartAxis.ChartMinorTickMarks.Enabled = ChartTickMarksEnabledTypes.True;
			}
			if (axis2005.Title != null && axis2005.Title.Caption != null)
			{
				ChartAxisTitle chartAxisTitle = new ChartAxisTitle();
				chartAxis.ChartAxisTitle = chartAxisTitle;
				chartAxisTitle.Caption = axis2005.Title.Caption;
				chartAxisTitle.PropertyStore.SetObject(1, axis2005.Title.PropertyStore.GetObject(2));
				chartAxisTitle.Position = new ReportExpression<ChartAxisTitlePositions>(axis2005.Title.Position.ToString(), CultureInfo.InvariantCulture);
				chartAxisTitle.Style = axis2005.Title.Style;
			}
			if (categoryAxis)
			{
				if (!chartAxis.Scalar)
				{
					if (!double.IsNaN(num))
					{
						chartAxis.Interval = (double.IsNaN(num2) ? num : Math.Min(num, num2));
					}
					else if (!double.IsNaN(num2))
					{
						chartAxis.Interval = num2;
					}
					else if (charType != ChartTypes2005.Bar)
					{
						chartAxis.Interval = 1.0;
					}
				}
				else
				{
					chartAxis.Interval = num;
				}
			}
			return chartAxis;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000A070 File Offset: 0x00008270
		private double ConvertToDouble(string value)
		{
			double naN = double.NaN;
			if (double.TryParse(value, out naN))
			{
				return naN;
			}
			return double.NaN;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000A09C File Offset: 0x0000829C
		private void SetChartDataPointNames(DataPoint2005 dataPoint, string[] names)
		{
			int num = Math.Min(dataPoint.DataValues.Count, names.Length);
			for (int i = 0; i < num; i++)
			{
				dataPoint.DataValues[i].Name = names[i];
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A0E0 File Offset: 0x000082E0
		private void SetChartTypes(Chart2005 oldChart, PlotTypes2005 plotType, ChartSeries newSeries)
		{
			if (plotType == PlotTypes2005.Line && oldChart.Type != ChartTypes2005.Line)
			{
				newSeries.Type = ChartTypes.Line;
				newSeries.Subtype = ChartSubtypes.Plain;
				return;
			}
			switch (oldChart.Type)
			{
			case ChartTypes2005.Column:
			case ChartTypes2005.Bar:
			case ChartTypes2005.Line:
			case ChartTypes2005.Area:
				newSeries.Type = new ReportExpression<ChartTypes>(oldChart.Type.ToString(), CultureInfo.InvariantCulture);
				newSeries.Subtype = new ReportExpression<ChartSubtypes>(oldChart.Subtype.ToString(), CultureInfo.InvariantCulture);
				return;
			case (ChartTypes2005)3:
				break;
			case ChartTypes2005.Scatter:
				if (oldChart.Subtype == ChartSubtypes2005.Plain)
				{
					newSeries.Type = ChartTypes.Scatter;
					newSeries.Subtype = ChartSubtypes.Plain;
					return;
				}
				newSeries.Type = ChartTypes.Line;
				if (oldChart.Subtype == ChartSubtypes2005.Line)
				{
					newSeries.Subtype = ChartSubtypes.Plain;
					return;
				}
				newSeries.Subtype = ChartSubtypes.Smooth;
				return;
			case ChartTypes2005.Pie:
				newSeries.Type = ChartTypes.Shape;
				if (oldChart.Subtype == ChartSubtypes2005.OpenHighLowClose)
				{
					newSeries.Subtype = ChartSubtypes.ExplodedPie;
					return;
				}
				newSeries.Subtype = ChartSubtypes.Pie;
				return;
			case ChartTypes2005.Bubble:
				newSeries.Type = ChartTypes.Scatter;
				newSeries.Subtype = ChartSubtypes.Bubble;
				return;
			case ChartTypes2005.Doughnut:
				newSeries.Type = ChartTypes.Shape;
				if (oldChart.Subtype == ChartSubtypes2005.OpenHighLowClose)
				{
					newSeries.Subtype = ChartSubtypes.ExplodedDoughnut;
					return;
				}
				newSeries.Subtype = ChartSubtypes.Doughnut;
				return;
			case ChartTypes2005.Stock:
				newSeries.Type = ChartTypes.Range;
				if (oldChart.Subtype == ChartSubtypes2005.Candlestick)
				{
					newSeries.Subtype = ChartSubtypes.Candlestick;
					return;
				}
				newSeries.Subtype = ChartSubtypes.Stock;
				break;
			default:
				return;
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000A290 File Offset: 0x00008490
		private void FixYukonChartBorderWidth(Style style, bool roundValue)
		{
			if (style == null)
			{
				return;
			}
			if (style.Border == null)
			{
				if (style is EmptyColorStyle)
				{
					style.Border = new EmptyBorder();
				}
				else
				{
					style.Border = new Border();
				}
			}
			if (!style.Border.Width.IsExpression)
			{
				double num = (roundValue ? Math.Max(Math.Round(style.Border.Width.Value.Value) * 0.75, 0.25) : Math.Max(style.Border.Width.Value.Value * 0.75, 0.376));
				style.Border.Width = new ReportSize(num, SizeTypes.Point);
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000A36C File Offset: 0x0000856C
		private Style FixYukonEmptyBorderStyle(Style2005 style2005)
		{
			Style style2006 = style2005;
			if (style2006 == null)
			{
				style2006 = new Style();
			}
			if (style2006.Border == null)
			{
				style2006.Border = new Border();
			}
			if (style2006.Border.Style == BorderStyles.Default)
			{
				style2006.Border.Style = BorderStyles.None;
			}
			return style2006;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000A3BC File Offset: 0x000085BC
		internal void UpgradeTextbox(Textbox2005 textbox)
		{
			this.UpgradeReportItem(textbox);
			textbox.KeepTogether = true;
			RdlCollection<Paragraph> rdlCollection = new RdlCollection<Paragraph>();
			Paragraph paragraph = new Paragraph();
			rdlCollection.Add(paragraph);
			textbox.Paragraphs = rdlCollection;
			RdlCollection<TextRun> rdlCollection2 = new RdlCollection<TextRun>();
			TextRun textRun = new TextRun();
			rdlCollection2.Add(textRun);
			paragraph.TextRuns = rdlCollection2;
			textRun.Value = textbox.Value;
			textRun.PropertyStore.SetObject(3, textbox.ValueLocID);
			Style style = textbox.Style;
			if (style != null)
			{
				Style style2 = this.CreateAndMoveStyleProperties(style, UpgradeImpl2005.m_TextRunAvailableStyles);
				if (style2 != null)
				{
					textRun.Style = style2;
				}
				style2 = this.CreateAndMoveStyleProperties(style, UpgradeImpl2005.m_ParagraphAvailableStyles, true, textbox.Name);
				if (style2 != null)
				{
					paragraph.Style = style2;
				}
			}
			textbox.Value = null;
			textbox.ValueLocID = null;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000A485 File Offset: 0x00008685
		private Style CreateAndMoveStyleProperties(Style srcStyle, Style.Definition.Properties[] availableStyles)
		{
			return this.CreateAndMoveStyleProperties(srcStyle, availableStyles, false, null);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000A494 File Offset: 0x00008694
		private Style CreateAndMoveStyleProperties(Style srcStyle, Style.Definition.Properties[] availableStyles, bool convertMeDotValue, string textboxName)
		{
			Style style = null;
			for (int i = 0; i < availableStyles.Length; i++)
			{
				int num = (int)availableStyles[i];
				if (srcStyle.PropertyStore.ContainsObject(num))
				{
					if (style == null)
					{
						style = new Style();
					}
					IExpression expression = (IExpression)srcStyle.PropertyStore.GetObject(num);
					if (convertMeDotValue && expression.IsExpression)
					{
						expression.Expression = this.ConvertMeDotValue(expression.ToString(), textboxName);
					}
					style.PropertyStore.SetObject(num, expression);
					Style.Definition.Properties properties = availableStyles[i];
					if (properties <= Style.Definition.Properties.FontSize)
					{
						if (properties == Style.Definition.Properties.FontFamily)
						{
							srcStyle.FontFamily = "Arial";
							goto IL_00DC;
						}
						if (properties == Style.Definition.Properties.FontSize)
						{
							srcStyle.FontSize = Constants.DefaultFontSize;
							goto IL_00DC;
						}
					}
					else
					{
						if (properties == Style.Definition.Properties.Color)
						{
							srcStyle.Color = Constants.DefaultColor;
							goto IL_00DC;
						}
						if (properties == Style.Definition.Properties.NumeralVariant)
						{
							srcStyle.NumeralVariant = 1;
							goto IL_00DC;
						}
					}
					srcStyle.PropertyStore.RemoveObject(num);
				}
				IL_00DC:;
			}
			return style;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000A58C File Offset: 0x0000878C
		private string ConvertMeDotValue(string expression, string textboxName)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			MatchCollection matchCollection = this.m_regexes.MeDotValueExpression.Matches(expression);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				global::System.Text.RegularExpressions.Group group = matchCollection[i].Groups["medotvalue"];
				if (group.Value != null && group.Value.Length > 0)
				{
					stringBuilder.Append(expression.Substring(num, group.Index - num));
					stringBuilder.Append("ReportItems!");
					stringBuilder.Append(textboxName);
					stringBuilder.Append(".Value");
					num = group.Index + group.Length;
				}
			}
			if (num == 0)
			{
				return expression;
			}
			if (num < expression.Length)
			{
				stringBuilder.Append(expression.Substring(num));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000A660 File Offset: 0x00008860
		internal void UpgradeQuery(Query2005 query)
		{
			query.DataSourceName = this.GetDataSourceName(query.DataSourceName);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000A674 File Offset: 0x00008874
		internal void UpgradeDataSource(DataSource2005 dataSource)
		{
			if (this.m_renameInvalidDataSources)
			{
				dataSource.Name = this.CreateUniqueDataSourceName(dataSource.Name);
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000A690 File Offset: 0x00008890
		internal void UpgradeSubreport(Subreport2005 subreport)
		{
			this.UpgradeReportItem(subreport);
			subreport.KeepTogether = true;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000A6A0 File Offset: 0x000088A0
		internal void UpgradeStyle(Style2005 style2005)
		{
			style2005.FontWeight = this.UpgradeStyleEnum<FontWeights, FontWeight2005>(style2005.FontWeight);
			if (!style2005.FontWeight.IsExpression)
			{
				switch (style2005.FontWeight.Value.Value)
				{
				case FontWeight2005.Lighter:
					style2005.FontWeight = FontWeights.Light;
					break;
				case FontWeight2005.Normal:
					style2005.FontWeight = FontWeights.Normal;
					break;
				case FontWeight2005.Bold:
					style2005.FontWeight = FontWeights.Bold;
					break;
				case FontWeight2005.Bolder:
					style2005.FontWeight = FontWeights.Bold;
					break;
				}
			}
			style2005.WritingMode = this.UpgradeStyleEnum<WritingModes, WritingMode2005>(style2005.WritingMode);
			style2005.Calendar = this.UpgradeStyleEnum<Calendars, Calendar2005>(style2005.Calendar);
			style2005.UnicodeBiDi = this.UpgradeStyleEnum<UnicodeBiDiTypes, UnicodeBiDi2005>(style2005.UnicodeBiDi);
			Border border = null;
			Border border2 = null;
			Border border3 = null;
			Border border4 = null;
			Border border5 = null;
			BorderColor2005 borderColor = style2005.BorderColor;
			if (borderColor != null)
			{
				this.SetBorderColor(borderColor.Default, ref border, Constants.DefaultBorderColor);
				this.SetBorderColor(borderColor.Top, ref border2, ReportColor.Empty);
				this.SetBorderColor(borderColor.Bottom, ref border3, ReportColor.Empty);
				this.SetBorderColor(borderColor.Left, ref border4, ReportColor.Empty);
				this.SetBorderColor(borderColor.Right, ref border5, ReportColor.Empty);
			}
			BorderStyle2005 borderStyle = style2005.BorderStyle;
			if (borderStyle != null)
			{
				this.SetBorderStyle(borderStyle.Default, ref border, BorderStyles2005.None);
				this.SetBorderStyle(borderStyle.Top, ref border2, BorderStyles2005.Default);
				this.SetBorderStyle(borderStyle.Bottom, ref border3, BorderStyles2005.Default);
				this.SetBorderStyle(borderStyle.Left, ref border4, BorderStyles2005.Default);
				this.SetBorderStyle(borderStyle.Right, ref border5, BorderStyles2005.Default);
			}
			BorderWidth2005 borderWidth = style2005.BorderWidth;
			if (borderWidth != null)
			{
				this.SetBorderWidth(borderWidth.Default, ref border, Constants.DefaultBorderWidth);
				this.SetBorderWidth(borderWidth.Top, ref border2, ReportSize.Empty);
				this.SetBorderWidth(borderWidth.Bottom, ref border3, ReportSize.Empty);
				this.SetBorderWidth(borderWidth.Left, ref border4, ReportSize.Empty);
				this.SetBorderWidth(borderWidth.Right, ref border5, ReportSize.Empty);
			}
			BackgroundImage2005 backgroundImage = style2005.BackgroundImage;
			if (backgroundImage != null)
			{
				BackgroundImage backgroundImage2 = backgroundImage;
				style2005.BackgroundImage = backgroundImage2;
				style2005.BackgroundImage = null;
				if (!backgroundImage.BackgroundRepeat.IsExpression)
				{
					if (style2005.Parent is Chart2005)
					{
						if (backgroundImage.BackgroundRepeat.Value != BackgroundRepeatTypes2005.NoRepeat)
						{
							backgroundImage2.BackgroundRepeat = BackgroundRepeatTypes.Repeat;
						}
					}
					else if (backgroundImage.BackgroundRepeat.Value == BackgroundRepeatTypes2005.NoRepeat)
					{
						backgroundImage2.BackgroundRepeat = BackgroundRepeatTypes.Clip;
					}
					else
					{
						backgroundImage2.BackgroundRepeat = (BackgroundRepeatTypes)backgroundImage.BackgroundRepeat.Value;
					}
				}
				else
				{
					backgroundImage2.BackgroundRepeat = new ReportExpression<BackgroundRepeatTypes>(backgroundImage.BackgroundRepeat.Expression, CultureInfo.InvariantCulture);
				}
			}
			if (border != null)
			{
				style2005.Border = border;
			}
			if (border2 != null)
			{
				style2005.TopBorder = border2;
			}
			if (border3 != null)
			{
				style2005.BottomBorder = border3;
			}
			if (border4 != null)
			{
				style2005.LeftBorder = border4;
			}
			if (border5 != null)
			{
				style2005.RightBorder = border5;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		internal void UpgradeEmptyColorStyle(EmptyColorStyle2005 emptyColorStyle2005)
		{
			if (emptyColorStyle2005.BorderColor == null)
			{
				emptyColorStyle2005.BorderColor = new EmptyBorderColor2005();
			}
			if (emptyColorStyle2005.BorderStyle == null)
			{
				emptyColorStyle2005.BorderStyle = new BorderStyle2005();
			}
			emptyColorStyle2005.FontWeight = this.UpgradeStyleEnum<FontWeights, FontWeight2005>(emptyColorStyle2005.FontWeight);
			if (!emptyColorStyle2005.FontWeight.IsExpression)
			{
				switch (emptyColorStyle2005.FontWeight.Value.Value)
				{
				case FontWeight2005.Lighter:
					emptyColorStyle2005.FontWeight = FontWeights.Light;
					break;
				case FontWeight2005.Normal:
					emptyColorStyle2005.FontWeight = FontWeights.Normal;
					break;
				case FontWeight2005.Bold:
					emptyColorStyle2005.FontWeight = FontWeights.Bold;
					break;
				case FontWeight2005.Bolder:
					emptyColorStyle2005.FontWeight = FontWeights.Bold;
					break;
				}
			}
			emptyColorStyle2005.WritingMode = this.UpgradeStyleEnum<WritingModes, WritingMode2005>(emptyColorStyle2005.WritingMode);
			emptyColorStyle2005.Calendar = this.UpgradeStyleEnum<Calendars, Calendar2005>(emptyColorStyle2005.Calendar);
			emptyColorStyle2005.UnicodeBiDi = this.UpgradeStyleEnum<UnicodeBiDiTypes, UnicodeBiDi2005>(emptyColorStyle2005.UnicodeBiDi);
			EmptyBorder emptyBorder = null;
			EmptyBorder emptyBorder2 = null;
			EmptyBorder emptyBorder3 = null;
			EmptyBorder emptyBorder4 = null;
			EmptyBorder emptyBorder5 = null;
			EmptyBorderColor2005 borderColor = emptyColorStyle2005.BorderColor;
			if (borderColor != null)
			{
				this.SetEmptyBorderColor(borderColor.Default, ref emptyBorder, Constants.DefaultEmptyColor);
				this.SetEmptyBorderColor(borderColor.Top, ref emptyBorder2, ReportColor.Empty);
				this.SetEmptyBorderColor(borderColor.Bottom, ref emptyBorder3, ReportColor.Empty);
				this.SetEmptyBorderColor(borderColor.Left, ref emptyBorder4, ReportColor.Empty);
				this.SetEmptyBorderColor(borderColor.Right, ref emptyBorder5, ReportColor.Empty);
			}
			BorderStyle2005 borderStyle = emptyColorStyle2005.BorderStyle;
			if (borderStyle != null)
			{
				this.SetEmptyBorderStyle(borderStyle.Default, ref emptyBorder, BorderStyles2005.Solid);
				this.SetEmptyBorderStyle(borderStyle.Top, ref emptyBorder2, BorderStyles2005.Default);
				this.SetEmptyBorderStyle(borderStyle.Bottom, ref emptyBorder3, BorderStyles2005.Default);
				this.SetEmptyBorderStyle(borderStyle.Left, ref emptyBorder4, BorderStyles2005.Default);
				this.SetEmptyBorderStyle(borderStyle.Right, ref emptyBorder5, BorderStyles2005.Default);
			}
			BorderWidth2005 borderWidth = emptyColorStyle2005.BorderWidth;
			if (borderWidth != null)
			{
				this.SetEmptyBorderWidth(borderWidth.Default, ref emptyBorder, Constants.DefaultBorderWidth);
				this.SetEmptyBorderWidth(borderWidth.Top, ref emptyBorder2, ReportSize.Empty);
				this.SetEmptyBorderWidth(borderWidth.Bottom, ref emptyBorder3, ReportSize.Empty);
				this.SetEmptyBorderWidth(borderWidth.Left, ref emptyBorder4, ReportSize.Empty);
				this.SetEmptyBorderWidth(borderWidth.Right, ref emptyBorder5, ReportSize.Empty);
			}
			emptyColorStyle2005.Border = emptyBorder;
			emptyColorStyle2005.TopBorder = emptyBorder2;
			emptyColorStyle2005.BottomBorder = emptyBorder3;
			emptyColorStyle2005.LeftBorder = emptyBorder4;
			emptyColorStyle2005.RightBorder = emptyBorder5;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000AC20 File Offset: 0x00008E20
		private ReportExpression<T> UpgradeStyleEnum<T, T2005>(ReportExpression<ReportEnum<T2005>> value2005) where T : struct where T2005 : struct, IConvertible
		{
			ReportExpression<T> reportExpression = default(ReportExpression<T>);
			if (value2005.IsExpression)
			{
				reportExpression.Expression = value2005.Expression;
			}
			else
			{
				Type typeFromHandle = typeof(T);
				T2005 value2006 = value2005.Value.Value;
				reportExpression.Value = (T)((object)Enum.ToObject(typeFromHandle, value2006.ToInt32(null)));
			}
			return reportExpression;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000AC88 File Offset: 0x00008E88
		private ReportExpression<T> UpgradeStyleEnum<T, T2005>(ReportExpression<T2005> value2005) where T : struct where T2005 : struct, IConvertible
		{
			ReportExpression<T> reportExpression = default(ReportExpression<T>);
			if (value2005.IsExpression)
			{
				reportExpression.Expression = value2005.Expression;
			}
			else
			{
				Type typeFromHandle = typeof(T);
				T2005 value2006 = value2005.Value;
				reportExpression.Value = (T)((object)Enum.ToObject(typeFromHandle, value2006.ToInt32(null)));
			}
			return reportExpression;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		private void SetBorderColor(ReportExpression<ReportColor> color, ref Border border, ReportColor defaultColor)
		{
			if (color != defaultColor)
			{
				if (border == null)
				{
					border = new Border();
				}
				border.Color = color;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000AD06 File Offset: 0x00008F06
		private void SetEmptyBorderColor(ReportExpression<ReportColor> color, ref EmptyBorder border, ReportColor defaultColor)
		{
			if (color != defaultColor)
			{
				if (border == null)
				{
					border = new EmptyBorder();
				}
				border.Color = color;
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000AD24 File Offset: 0x00008F24
		private void SetBorderStyle(ReportExpression<BorderStyles2005> style, ref Border border, BorderStyles2005 defaultStyle)
		{
			if (style != defaultStyle)
			{
				if (border == null)
				{
					border = new Border();
				}
				border.Style = this.UpgradeStyleEnum<BorderStyles, BorderStyles2005>(style);
				if (!border.Style.IsExpression)
				{
					BorderStyles2005 value = style.Value;
					if (value - BorderStyles2005.Groove <= 4)
					{
						border.Style = BorderStyles.Solid;
					}
				}
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000AD80 File Offset: 0x00008F80
		private void SetEmptyBorderStyle(ReportExpression<BorderStyles2005> style, ref EmptyBorder border, BorderStyles2005 defaultStyle)
		{
			if (style != defaultStyle)
			{
				if (border == null)
				{
					border = new EmptyBorder();
				}
				border.Style = this.UpgradeStyleEnum<BorderStyles, BorderStyles2005>(style);
				if (!border.Style.IsExpression)
				{
					BorderStyles2005 value = style.Value;
					if (value - BorderStyles2005.Groove <= 4)
					{
						border.Style = BorderStyles.Solid;
					}
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000ADDB File Offset: 0x00008FDB
		private void SetBorderWidth(ReportExpression<ReportSize> width, ref Border border, ReportSize defaultWidth)
		{
			if (width != defaultWidth)
			{
				if (border == null)
				{
					border = new Border();
				}
				border.Width = width;
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000ADF9 File Offset: 0x00008FF9
		private void SetEmptyBorderWidth(ReportExpression<ReportSize> width, ref EmptyBorder border, ReportSize defaultWidth)
		{
			if (width != defaultWidth)
			{
				if (border == null)
				{
					border = new EmptyBorder();
				}
				border.Width = width;
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000AE18 File Offset: 0x00009018
		private void TransferGroupingCustomProperties(object member, UpgradeImpl2005.GroupAccessor groupAccessor, UpgradeImpl2005.CustomPropertiesAccessor propertiesAccessor)
		{
			Grouping2005 grouping = groupAccessor(member) as Grouping2005;
			if (grouping != null)
			{
				IList<CustomProperty> list = propertiesAccessor(member);
				foreach (CustomProperty customProperty in grouping.CustomProperties)
				{
					list.Add(customProperty);
				}
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000AE80 File Offset: 0x00009080
		private string UniqueName(string baseName, object obj)
		{
			return this.UniqueName(baseName, obj, true);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000AE8C File Offset: 0x0000908C
		private string UniqueName(string baseName, object obj, bool allowBaseName)
		{
			string text = baseName;
			int num = ((!allowBaseName) ? 1 : 0);
			for (;;)
			{
				if (num > 0)
				{
					text = baseName + num.ToString();
				}
				if (!this.m_nameTable.ContainsKey(text))
				{
					break;
				}
				num++;
			}
			this.m_nameTable.Add(text, obj);
			return text;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000AED4 File Offset: 0x000090D4
		private string CreateUniqueDataSourceName(string oldName)
		{
			string text = oldName;
			if (!ReportRegularExpressions.Value.ClsIdentifierRegex.Match(oldName).Success)
			{
				text = Regex.Replace(oldName, "[^\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]", "_");
				if (!ReportRegularExpressions.Value.ClsIdentifierRegex.Match(text).Success)
				{
					text = "AutoGen_" + text;
				}
			}
			string text2 = text.ToUpperInvariant();
			string text3 = text;
			string text4 = text2;
			int num = 0;
			for (;;)
			{
				if (num > 0)
				{
					text3 = text + num.ToString();
					text4 = text2 + num.ToString();
				}
				if (!this.m_dataSourceNameTable.ContainsKey(text4))
				{
					break;
				}
				num++;
			}
			this.m_dataSourceNameTable.Add(text4, text3);
			if (!this.m_dataSourceCaseSensitiveNameTable.ContainsKey(oldName))
			{
				this.m_dataSourceCaseSensitiveNameTable.Add(oldName, text3);
			}
			return text3;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000AF9C File Offset: 0x0000919C
		private string GetDataSourceName(string dataSourceName)
		{
			if (this.m_dataSourceCaseSensitiveNameTable.ContainsKey(dataSourceName))
			{
				return (string)this.m_dataSourceCaseSensitiveNameTable[dataSourceName];
			}
			string text = dataSourceName.ToUpperInvariant();
			if (this.m_dataSourceNameTable.ContainsKey(text))
			{
				return (string)this.m_dataSourceNameTable[text];
			}
			return dataSourceName;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000AFF4 File Offset: 0x000091F4
		private string GetParentReportItemName(IContainedObject obj)
		{
			for (IContainedObject containedObject = obj.Parent; containedObject != null; containedObject = containedObject.Parent)
			{
				if (containedObject is ReportItem)
				{
					return ((ReportItem)containedObject).Name;
				}
			}
			return "";
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000B030 File Offset: 0x00009230
		private void UpgradeDundasCRIChart(CustomReportItem cri, Chart chart)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			OrderedDictionary orderedDictionary2 = new OrderedDictionary();
			chart.Name = cri.Name;
			chart.ActionInfo = cri.ActionInfo;
			chart.Bookmark = cri.Bookmark;
			chart.DataElementName = cri.DataElementName;
			chart.DataElementOutput = cri.DataElementOutput;
			chart.DocumentMapLabel = cri.DocumentMapLabel;
			chart.PropertyStore.SetObject(12, cri.PropertyStore.GetObject(12));
			chart.Height = cri.Height;
			chart.Left = cri.Left;
			chart.Parent = cri.Parent;
			chart.RepeatWith = cri.RepeatWith;
			chart.ToolTip = cri.ToolTip;
			chart.PropertyStore.SetObject(10, cri.PropertyStore.GetObject(10));
			chart.Top = cri.Top;
			chart.Visibility = cri.Visibility;
			chart.Width = cri.Width;
			chart.ZIndex = cri.ZIndex;
			if (cri.CustomData != null)
			{
				chart.DataSetName = cri.CustomData.DataSetName;
				chart.Filters = cri.CustomData.Filters;
			}
			Hashtable hashtable = new Hashtable();
			List<Hashtable> list = new List<Hashtable>();
			List<Hashtable> list2 = new List<Hashtable>();
			List<Hashtable> list3 = new List<Hashtable>();
			foreach (CustomProperty customProperty in cri.CustomProperties)
			{
				string text = customProperty.Name.Value;
				if (text.StartsWith("expression:", StringComparison.OrdinalIgnoreCase))
				{
					text = text.Substring("expression:".Length);
				}
				if (!this.AddToPropertyList(list, "Chart.Titles.", text, customProperty.Value) && !this.AddToPropertyList(list2, "Chart.Legends.", text, customProperty.Value) && !this.AddToPropertyList(list3, "Chart.ChartAreas.", text, customProperty.Value))
				{
					hashtable.Add(text, customProperty.Value);
				}
				if (text.StartsWith("CHART.ANNOTATIONS.", StringComparison.OrdinalIgnoreCase))
				{
					base.UpgradeResults.HasUnsupportedDundasChartFeatures = true;
				}
			}
			if (hashtable["CUSTOM_CODE_CS"] != null || hashtable["CUSTOM_CODE_VB"] != null || hashtable["CUSTOM_CODE_COMPILED_ASSEMBLY"] != null)
			{
				if (this.m_throwUpgradeException)
				{
					throw new CRI2005UpgradeException();
				}
				base.UpgradeResults.HasUnsupportedDundasChartFeatures = true;
			}
			StringCollection stringCollection = new StringCollection();
			foreach (Hashtable hashtable2 in list3)
			{
				ChartArea chartArea = new ChartArea();
				chart.ChartAreas.Add(chartArea);
				string text2 = this.ConvertDundasCRIStringProperty(hashtable2["ChartArea.Name"]);
				string text3 = this.CreateNewName(stringCollection, text2, "ChartArea");
				stringCollection.Add(text3);
				if (!orderedDictionary.Contains(text2))
				{
					orderedDictionary.Add(text2, text3);
				}
				chartArea.Name = text3;
			}
			int num = 0;
			foreach (Hashtable hashtable3 in list3)
			{
				ChartArea chartArea2 = chart.ChartAreas[num];
				num++;
				chartArea2.AlignWithChartArea = this.GetNewName(orderedDictionary, this.ConvertDundasCRIStringProperty(hashtable3["ChartArea.AlignWithChartArea"]));
				chartArea2.AlignOrientation = new ReportExpression<ChartAlignOrientations>(this.ConvertDundasCRIStringProperty(ChartAlignOrientations.Vertical.ToString(), hashtable3["ChartArea.AlignOrientation"]), CultureInfo.InvariantCulture);
				bool? flag = this.ConvertDundasCRIBoolProperty(hashtable3["EquallySizedAxesFont"]);
				if (flag != null)
				{
					chartArea2.EquallySizedAxesFont = flag.Value;
				}
				bool? flag2 = this.ConvertDundasCRIBoolProperty(hashtable3["ChartArea.Visible"]);
				if (flag2 != null)
				{
					chartArea2.Hidden = !flag2.Value;
				}
				string text4 = this.ConvertDundasCRIStringProperty(hashtable3["ChartArea.AlignType"]);
				if (text4 != string.Empty)
				{
					ChartAlignType chartAlignType = new ChartAlignType();
					text4 = " " + text4.Replace(',', ' ') + " ";
					chartAlignType.AxesView = text4.Contains("AxesView");
					chartAlignType.Cursor = text4.Contains("Cursor");
					chartAlignType.InnerPlotPosition = text4.Contains("PlotPosition");
					chartAlignType.Position = text4.Contains("Position");
				}
				chartArea2.Style = this.ConvertDundasCRIStyleProperty(null, hashtable3["ChartArea.BackColor"], hashtable3["ChartArea.BackGradientType"], hashtable3["ChartArea.BackGradientEndColor"], hashtable3["ChartArea.BackHatchStyle"], hashtable3["ChartArea.ShadowColor"], hashtable3["ChartArea.ShadowOffset"], hashtable3["ChartArea.BorderColor"], hashtable3["ChartArea.BorderStyle"], hashtable3["ChartArea.BorderWidth"], hashtable3["ChartArea.BackImage"], hashtable3["ChartArea.BackImageTranspColor"], hashtable3["ChartArea.BackImageAlign"], hashtable3["ChartArea.BackImageMode"], null, null, null, null, null);
				chartArea2.ChartElementPosition = this.ConvertDundasCRIChartElementPosition(hashtable3["ChartArea.Position.Y"], hashtable3["ChartArea.Position.X"], hashtable3["ChartArea.Position.Height"], hashtable3["ChartArea.Position.Width"]);
				chartArea2.ChartInnerPlotPosition = this.ConvertDundasCRIChartElementPosition(hashtable3["ChartArea.InnerPlotPosition.Y"], hashtable3["ChartArea.InnerPlotPosition.X"], hashtable3["ChartArea.InnerPlotPosition.Height"], hashtable3["ChartArea.InnerPlotPosition.Width"]);
				int num2 = 0;
				ChartThreeDProperties chartThreeDProperties = new ChartThreeDProperties();
				chartThreeDProperties.Perspective = this.ConvertDundasCRIIntegerReportExpressionProperty(hashtable3["ChartArea.Area3DStyle.Perspective"], ref num2);
				chartThreeDProperties.Rotation = this.ConvertDundasCRIIntegerReportExpressionProperty(chartThreeDProperties.Rotation, hashtable3["ChartArea.Area3DStyle.YAngle"], ref num2);
				chartThreeDProperties.Inclination = this.ConvertDundasCRIIntegerReportExpressionProperty(chartThreeDProperties.Inclination, hashtable3["ChartArea.Area3DStyle.XAngle"], ref num2);
				chartThreeDProperties.DepthRatio = this.ConvertDundasCRIIntegerReportExpressionProperty(chartThreeDProperties.DepthRatio, hashtable3["ChartArea.Area3DStyle.PointDepth"], ref num2);
				chartThreeDProperties.GapDepth = this.ConvertDundasCRIIntegerReportExpressionProperty(chartThreeDProperties.GapDepth, hashtable3["ChartArea.Area3DStyle.PointGapDepth"], ref num2);
				chartThreeDProperties.WallThickness = this.ConvertDundasCRIIntegerReportExpressionProperty(chartThreeDProperties.WallThickness, hashtable3["ChartArea.Area3DStyle.WallWidth"], ref num2);
				bool? flag3 = this.ConvertDundasCRIBoolProperty(hashtable3["ChartArea.Area3DStyle.Clustered"], ref num2);
				if (flag3 != null)
				{
					chartThreeDProperties.Clustered = !flag3.Value;
				}
				else
				{
					chartThreeDProperties.Clustered = true;
				}
				bool? flag4 = this.ConvertDundasCRIBoolProperty(hashtable3["ChartArea.Area3DStyle.Enable3D"], ref num2);
				if (flag4 != null)
				{
					chartThreeDProperties.Enabled = flag4.Value;
				}
				bool? flag5 = this.ConvertDundasCRIBoolProperty(hashtable3["ChartArea.Area3DStyle.RightAngleAxes"], ref num2);
				if (flag5 == null || flag5.Value)
				{
					chartThreeDProperties.ProjectionMode = ChartProjectionModes.Oblique;
				}
				else
				{
					chartThreeDProperties.ProjectionMode = ChartProjectionModes.Perspective;
				}
				string text5 = this.ConvertDundasCRIStringProperty(hashtable3["ChartArea.Area3DStyle.Light"], ref num2);
				if (text5 == "None")
				{
					chartThreeDProperties.Shading = ChartShadings.None;
				}
				else if (text5 == "Realistic")
				{
					chartThreeDProperties.Shading = ChartShadings.Real;
				}
				else
				{
					chartThreeDProperties.Shading = ChartShadings.Simple;
				}
				if (num2 > 0)
				{
					chartArea2.ChartThreeDProperties = chartThreeDProperties;
				}
				ChartAxis chartAxis = new ChartAxis();
				ChartAxis chartAxis2 = new ChartAxis();
				chartAxis.Name = (chartAxis2.Name = "Primary");
				chartAxis.Location = (chartAxis2.Location = ChartAxisLocations.Default);
				ChartAxis chartAxis3 = new ChartAxis();
				ChartAxis chartAxis4 = new ChartAxis();
				chartAxis3.Name = (chartAxis4.Name = "Secondary");
				chartAxis3.Location = (chartAxis4.Location = ChartAxisLocations.Opposite);
				chartArea2.ChartCategoryAxes.Add(chartAxis);
				chartArea2.ChartCategoryAxes.Add(chartAxis3);
				chartArea2.ChartValueAxes.Add(chartAxis2);
				chartArea2.ChartValueAxes.Add(chartAxis4);
				this.UpgradeDundasCRIChartAxis(chartAxis, hashtable3, "ChartArea.AxisX.");
				this.UpgradeDundasCRIChartAxis(chartAxis3, hashtable3, "ChartArea.AxisX2.");
				this.UpgradeDundasCRIChartAxis(chartAxis2, hashtable3, "ChartArea.AxisY.");
				this.UpgradeDundasCRIChartAxis(chartAxis4, hashtable3, "ChartArea.AxisY2.");
			}
			chart.ToolTip = this.ConvertDundasCRIStringProperty(hashtable["Chart.ToolTip"]);
			chart.Style = this.ConvertDundasCRIStyleProperty(null, this.ConvertDundasCRIColorProperty(Color.White.Name, hashtable["Chart.BackColor"]), hashtable["Chart.BackGradientType"], hashtable["Chart.BackGradientEndColor"], hashtable["Chart.BackHatchStyle"], null, null, hashtable["Chart.BorderLineColor"], hashtable["Chart.BorderLineStyle"] ?? BorderStyles.None, hashtable["Chart.BorderLineWidth"], hashtable["Chart.BackImage"], null, null, null, null, null, null, null, null);
			if (cri.Style != null && cri.Style.Language != null)
			{
				if (chart.Style == null)
				{
					chart.Style = new Style();
				}
				chart.Style.Language = cri.Style.Language;
			}
			string text6 = this.ConvertDundasCRIStringProperty(hashtable["Chart.Palette"]);
			if (text6 == string.Empty || text6 == "Dundas")
			{
				chart.Palette = ChartPalettes.BrightPastel;
			}
			else if (text6 != "None")
			{
				chart.Palette = new ReportExpression<ChartPalettes>(text6, CultureInfo.InvariantCulture);
			}
			string text7 = this.ConvertDundasCRIStringProperty(hashtable["Chart.PaletteCustomColors"]);
			if (text7 != string.Empty)
			{
				string[] array = text7.Split(new char[] { ';' });
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text8 = array2[i].Trim();
					chart.ChartCustomPaletteColors.Add(this.ConvertDundasCRIColorProperty(Color.Transparent.Name, string.IsNullOrEmpty(text8) ? null : text8.Trim()));
				}
				if (array.Length != 0)
				{
					chart.Palette = new ReportExpression<ChartPalettes>(ChartPalettes.Custom);
				}
			}
			int num3 = 0;
			ChartBorderSkin chartBorderSkin = new ChartBorderSkin();
			chartBorderSkin.ChartBorderSkinType = new ReportExpression<ChartBorderSkinTypes>(this.ConvertDundasCRIStringProperty(hashtable["Chart.BorderSkin.SkinStyle"], ref num3), CultureInfo.InvariantCulture);
			chartBorderSkin.Style = this.ConvertDundasCRIStyleProperty(this.ConvertDundasCRIColorProperty(Color.White.Name, hashtable["Chart.BorderSkin.PageColor"]), hashtable["Chart.BorderSkin.FrameBackColor"], hashtable["Chart.BorderSkin.FrameBackGradientType"], hashtable["Chart.BorderSkin.FrameBackGradientEndColor"], hashtable["Chart.BorderSkin.FrameBackHatchStyle"], null, null, hashtable["Chart.BorderSkin.FrameBorderColor"], hashtable["Chart.BorderSkin.FrameBorderStyle"], hashtable["Chart.BorderSkin.FrameBorderWidth"], null, null, null, null, null, null, null, null, null, ref num3);
			if (num3 > 0)
			{
				chart.ChartBorderSkin = chartBorderSkin;
			}
			StringCollection stringCollection2 = new StringCollection();
			foreach (Hashtable hashtable4 in list)
			{
				ChartTitle chartTitle = new ChartTitle();
				chart.ChartTitles.Add(chartTitle);
				string text9 = this.CreateNewName(stringCollection2, this.ConvertDundasCRIStringProperty(hashtable4["Title.Name"]), "Title");
				stringCollection2.Add(text9);
				chartTitle.DockToChartArea = this.GetNewName(orderedDictionary, this.ConvertDundasCRIStringProperty(hashtable4["Title.DockToChartArea"]));
				chartTitle.Name = text9;
				this.UpgradeDundasCRIChartTitle(chartTitle, hashtable4, "Title.");
			}
			ChartTitle chartTitle2 = new ChartTitle();
			if (this.UpgradeDundasCRIChartTitle(chartTitle2, hashtable, "Chart.NoDataMessage."))
			{
				chartTitle2.Name = "NoDataMessageTitle";
				chart.ChartNoDataMessage = chartTitle2;
			}
			StringCollection stringCollection3 = new StringCollection();
			foreach (Hashtable hashtable5 in list2)
			{
				ChartLegend chartLegend = new ChartLegend();
				chart.ChartLegends.Add(chartLegend);
				string text10 = this.ConvertDundasCRIStringProperty(hashtable5["Legend.Name"]);
				string text11 = this.CreateNewName(stringCollection3, text10, "Legend");
				stringCollection3.Add(text11);
				if (!orderedDictionary2.Contains(text10))
				{
					orderedDictionary2.Add(text10, text11);
				}
				chartLegend.DockToChartArea = this.GetNewName(orderedDictionary, this.ConvertDundasCRIStringProperty(hashtable5["Legend.DockToChartArea"]));
				chartLegend.Name = text11;
				this.UpgradeDundasCRIChartLegend(chartLegend, hashtable5, "Legend.");
			}
			if (cri.CustomData != null)
			{
				List<Hashtable> list4 = new List<Hashtable>();
				List<Hashtable> list5 = new List<Hashtable>();
				IList<DataMember> list6;
				IList<ChartMember> list7;
				if (cri.CustomData.DataColumnHierarchy != null)
				{
					list6 = cri.CustomData.DataColumnHierarchy.DataMembers;
					list7 = chart.ChartCategoryHierarchy.ChartMembers;
					while (list6 != null && list6.Count > 0)
					{
						foreach (DataMember dataMember in list6)
						{
							ChartMember chartMember = new ChartMember();
							list7.Add(chartMember);
							chartMember.Group = dataMember.Group;
							chartMember.SortExpressions = dataMember.SortExpressions;
							foreach (CustomProperty customProperty2 in dataMember.CustomProperties)
							{
								if (customProperty2.Name == "GroupLabel")
								{
									chartMember.Label = customProperty2.Value.ToString();
									break;
								}
							}
							list6 = dataMember.DataMembers;
							list7 = chartMember.ChartMembers;
						}
					}
				}
				if (cri.CustomData.DataRowHierarchy == null)
				{
					goto IL_1267;
				}
				list6 = cri.CustomData.DataRowHierarchy.DataMembers;
				list7 = chart.ChartSeriesHierarchy.ChartMembers;
				while (list6 != null)
				{
					bool flag6 = false;
					foreach (DataMember dataMember2 in list6)
					{
						if (dataMember2.DataMembers != null && dataMember2.DataMembers.Count > 0)
						{
							ChartMember chartMember2 = new ChartMember();
							list7.Add(chartMember2);
							chartMember2.Group = dataMember2.Group;
							chartMember2.SortExpressions = dataMember2.SortExpressions;
							foreach (CustomProperty customProperty3 in dataMember2.CustomProperties)
							{
								if (customProperty3.Name == "GroupLabel")
								{
									chartMember2.Label = customProperty3.Value.ToString();
									break;
								}
							}
							list6 = dataMember2.DataMembers;
							list7 = chartMember2.ChartMembers;
						}
						else
						{
							flag6 = true;
							Hashtable hashtable6 = new Hashtable(dataMember2.CustomProperties.Count);
							if (dataMember2.CustomProperties != null)
							{
								foreach (CustomProperty customProperty4 in dataMember2.CustomProperties)
								{
									hashtable6.Add(customProperty4.Name, customProperty4.Value);
								}
								list4.Add(hashtable6);
							}
							list7.Add(new ChartMember
							{
								Label = this.ConvertDundasCRIStringProperty(hashtable6["SeriesLabel"])
							});
						}
					}
					if (flag6)
					{
						list6 = null;
					}
				}
				if (cri.CustomData.DataRows != null)
				{
					foreach (IList<IList<DataValue>> list8 in cri.CustomData.DataRows)
					{
						foreach (IList<DataValue> list9 in list8)
						{
							Hashtable hashtable7 = new Hashtable(list9.Count);
							foreach (DataValue dataValue in list9)
							{
								if (dataValue.Name.Value.StartsWith("CUSTOMVALUE:", StringComparison.OrdinalIgnoreCase))
								{
									if (this.m_throwUpgradeException)
									{
										throw new CRI2005UpgradeException();
									}
									base.UpgradeResults.HasUnsupportedDundasChartFeatures = true;
								}
								hashtable7.Add(dataValue.Name, dataValue.Value);
							}
							list5.Add(hashtable7);
						}
					}
				}
				if (chart.ChartData == null)
				{
					chart.ChartData = new ChartData();
				}
				using (List<Hashtable>.Enumerator enumerator2 = list4.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Hashtable hashtable8 = enumerator2.Current;
						ChartSeries chartSeries = new ChartSeries();
						chart.ChartData.ChartSeriesCollection.Add(chartSeries);
						chartSeries.Name = "Series" + chart.ChartData.ChartSeriesCollection.Count.ToString(CultureInfo.InvariantCulture.NumberFormat);
						chartSeries.ChartAreaName = this.GetNewName(orderedDictionary, this.ConvertDundasCRIStringProperty(hashtable8["ChartArea"]));
						chartSeries.LegendName = this.GetNewName(orderedDictionary2, this.ConvertDundasCRIStringProperty(hashtable8["Legend"]));
						this.UpgradeDundasCRIChartSeries(chartSeries, chart.ChartData.ChartDerivedSeriesCollection, hashtable8, list5);
					}
					goto IL_1267;
				}
			}
			chart.ChartCategoryHierarchy.ChartMembers.Add(new ChartMember());
			chart.ChartSeriesHierarchy.ChartMembers.Add(new ChartMember());
			chart.ChartData = new ChartData();
			ChartSeries chartSeries2 = new ChartSeries();
			chartSeries2.Name = "emptySeriesName";
			chartSeries2.ChartDataPoints.Add(new ChartDataPoint());
			chart.ChartData.ChartSeriesCollection.Add(chartSeries2);
			IL_1267:
			this.FixChartAxisStriplineTitleAngle(chart);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000C400 File Offset: 0x0000A600
		private void UpgradeDundasCRIChartAxis(ChartAxis axis, Hashtable axisProperties, string propertyPrefix)
		{
			axis.Visible = new ReportExpression<ChartVisibleTypes>(this.ConvertDundasCRIStringProperty(axisProperties[propertyPrefix + "Enabled"]), CultureInfo.InvariantCulture);
			axis.Interval = this.ConvertDundasCRIDoubleReportExpressionProperty(axisProperties[propertyPrefix + "Interval"]);
			axis.IntervalType = new ReportExpression<ChartIntervalTypes>(this.ConvertDundasCRIStringProperty(ChartIntervalTypes.Auto.ToString(), axisProperties[propertyPrefix + "IntervalType"]), CultureInfo.InvariantCulture);
			axis.IntervalOffset = this.ConvertDundasCRIDoubleReportExpressionProperty(axisProperties[propertyPrefix + "IntervalOffset"]);
			axis.IntervalOffsetType = new ReportExpression<ChartIntervalOffsetTypes>(this.ConvertDundasCRIStringProperty(ChartIntervalOffsetTypes.Auto.ToString(), axisProperties[propertyPrefix + "IntervalOffsetType"]), CultureInfo.InvariantCulture);
			axis.CrossAt = this.ConvertDundasCRIStringProperty(axisProperties[propertyPrefix + "Crossing"]);
			axis.Arrows = new ReportExpression<ChartArrowsTypes>(this.ConvertDundasCRIStringProperty(axisProperties[propertyPrefix + "Arrows"]), CultureInfo.InvariantCulture);
			axis.Minimum = this.ConvertDundasCRIStringProperty(axisProperties[propertyPrefix + "Minimum"]);
			axis.Maximum = this.ConvertDundasCRIStringProperty(axisProperties[propertyPrefix + "Maximum"]);
			axis.LogBase = this.ConvertDundasCRIDoubleReportExpressionProperty(axis.LogBase, axisProperties[propertyPrefix + "LogarithmBase"]);
			axis.Angle = this.ConvertDundasCRIDoubleReportExpressionProperty(axisProperties[propertyPrefix + "LabelStyle.FontAngle"]);
			axis.LabelInterval = this.ConvertDundasCRIDoubleReportExpressionProperty(axisProperties[propertyPrefix + "LabelStyle.Interval"]);
			axis.LabelIntervalType = new ReportExpression<ChartIntervalTypes>(this.ConvertDundasCRIStringProperty(axisProperties[propertyPrefix + "LabelStyle.IntervalType"]), CultureInfo.InvariantCulture);
			axis.LabelIntervalOffset = this.ConvertDundasCRIDoubleReportExpressionProperty(axisProperties[propertyPrefix + "LabelStyle.IntervalOffset"]);
			axis.LabelIntervalOffsetType = new ReportExpression<ChartIntervalOffsetTypes>(this.ConvertDundasCRIStringProperty(axisProperties[propertyPrefix + "LabelStyle.IntervalOffsetType"]), CultureInfo.InvariantCulture);
			axis.MinFontSize = this.ConvertDundasCRIPointReportSizeProperty(axis.MinFontSize, axisProperties[propertyPrefix + "LabelsAutoFitMinFontSize"]);
			axis.MaxFontSize = this.ConvertDundasCRIPointReportSizeProperty(axis.MaxFontSize, axisProperties[propertyPrefix + "LabelsAutoFitMaxFontSize"]);
			axis.InterlacedColor = this.ConvertDundasCRIColorProperty(axis.InterlacedColor, axisProperties[propertyPrefix + "InterlacedColor"]);
			bool? flag = this.ConvertDundasCRIBoolProperty(axisProperties[propertyPrefix + "Reverse"]);
			if (flag != null)
			{
				axis.Reverse = flag.Value;
			}
			bool? flag2 = this.ConvertDundasCRIBoolProperty(axisProperties[propertyPrefix + "Interlaced"]);
			if (flag2 != null)
			{
				axis.Interlaced = flag2.Value;
			}
			bool? flag3 = this.ConvertDundasCRIBoolProperty(axisProperties[propertyPrefix + "Logarithmic"]);
			if (flag3 != null)
			{
				axis.LogScale = flag3.Value;
			}
			bool? flag4 = this.ConvertDundasCRIBoolProperty(axisProperties[propertyPrefix + "LabelStyle.Enabled"]);
			if (flag4 != null)
			{
				axis.HideLabels = !flag4.Value;
			}
			bool? flag5 = this.ConvertDundasCRIBoolProperty(axisProperties[propertyPrefix + "StartFromZero"]);
			if (flag5 != null)
			{
				axis.IncludeZero = flag5.Value;
			}
			bool? flag6 = this.ConvertDundasCRIBoolProperty(axisProperties[propertyPrefix + "LabelsAutoFit"]);
			if (flag6 != null)
			{
				axis.LabelsAutoFitDisabled = !flag6.Value;
			}
			bool? flag7 = this.ConvertDundasCRIBoolProperty(axisProperties[propertyPrefix + "LabelStyle.OffsetLabels"]);
			if (flag7 != null)
			{
				axis.OffsetLabels = flag7.Value;
			}
			bool? flag8 = this.ConvertDundasCRIBoolProperty(axisProperties[propertyPrefix + "LabelStyle.ShowEndLabels"]);
			if (flag8 != null)
			{
				axis.HideEndLabels = !flag8.Value;
			}
			string text = this.ConvertDundasCRIStringProperty(axisProperties[propertyPrefix + "LabelsAutoFitStyle"]);
			axis.PreventFontGrow = !(text == string.Empty) && !text.Contains("IncreaseFont");
			axis.PreventFontShrink = !(text == string.Empty) && !text.Contains("DecreaseFont");
			axis.PreventLabelOffset = !(text == string.Empty) && !text.Contains("OffsetLabels");
			axis.PreventWordWrap = !(text == string.Empty) && !text.Contains("WordWrap");
			if (text == string.Empty || text.Contains("LabelsAngleStep30"))
			{
				axis.AllowLabelRotation = ChartLabelRotationTypes.Rotate30;
			}
			else if (text.Contains("LabelsAngleStep45"))
			{
				axis.AllowLabelRotation = ChartLabelRotationTypes.Rotate45;
			}
			else if (text.Contains("LabelsAngleStep90"))
			{
				axis.AllowLabelRotation = ChartLabelRotationTypes.Rotate90;
			}
			bool? flag9 = this.ConvertDundasCRIBoolProperty(axisProperties[propertyPrefix + "MarksNextToAxis"]);
			if (flag9 != null)
			{
				axis.MarksAlwaysAtPlotEdge = !flag9.Value;
			}
			bool? flag10 = this.ConvertDundasCRIBoolProperty(axisProperties[propertyPrefix + "Margin"]);
			if (flag10 != null && !flag10.Value)
			{
				axis.Margin = ChartAxisMarginVisibleTypes.False;
			}
			else
			{
				axis.Margin = ChartAxisMarginVisibleTypes.True;
			}
			axis.Style = this.ConvertDundasCRIStyleProperty(axisProperties[propertyPrefix + "LabelStyle.FontColor"], null, null, null, null, null, null, axisProperties[propertyPrefix + "LineColor"], axisProperties[propertyPrefix + "LineStyle"], axisProperties[propertyPrefix + "LineWidth"], null, null, null, null, this.ConvertDundasCRIStringProperty("Microsoft Sans Serif, 8pt", axisProperties[propertyPrefix + "LabelStyle.Font"]), axisProperties[propertyPrefix + "LabelStyle.Format"], null, null, null);
			int num = 0;
			ChartAxisTitle chartAxisTitle = new ChartAxisTitle();
			chartAxisTitle.Caption = this.ConvertDundasCRIStringProperty(axisProperties[propertyPrefix + "Title"], ref num);
			chartAxisTitle.Position = new ReportExpression<ChartAxisTitlePositions>(this.ConvertDundasCRIStringProperty(axisProperties[propertyPrefix + "TitleAlignment"], ref num), CultureInfo.InvariantCulture);
			chartAxisTitle.Style = this.ConvertDundasCRIStyleProperty(axisProperties[propertyPrefix + "TitleColor"], null, null, null, null, null, null, null, null, null, null, null, null, null, this.ConvertDundasCRIStringProperty("Microsoft Sans Serif, 8pt", axisProperties[propertyPrefix + "TitleFont"]), null, null, null, null, ref num);
			if (num > 0)
			{
				axis.ChartAxisTitle = chartAxisTitle;
			}
			num = 0;
			ChartAxisScaleBreak chartAxisScaleBreak = new ChartAxisScaleBreak();
			bool? flag11 = this.ConvertDundasCRIBoolProperty(axisProperties[propertyPrefix + "ScaleBreakStyle.Enabled"], ref num);
			if (flag11 != null)
			{
				chartAxisScaleBreak.Enabled = flag11.Value;
			}
			chartAxisScaleBreak.BreakLineType = new ReportExpression<ChartBreakLineTypes>(this.ConvertDundasCRIStringProperty(axisProperties[propertyPrefix + "ScaleBreakStyle.BreakLineType"], ref num), CultureInfo.InvariantCulture);
			chartAxisScaleBreak.CollapsibleSpaceThreshold = this.ConvertDundasCRIIntegerReportExpressionProperty(chartAxisScaleBreak.CollapsibleSpaceThreshold, axisProperties[propertyPrefix + "ScaleBreakStyle.CollapsibleSpaceThreshold"], ref num);
			chartAxisScaleBreak.MaxNumberOfBreaks = this.ConvertDundasCRIIntegerReportExpressionProperty(chartAxisScaleBreak.MaxNumberOfBreaks, axisProperties[propertyPrefix + "ScaleBreakStyle.MaxNumberOfBreaks"], ref num);
			chartAxisScaleBreak.Spacing = this.ConvertDundasCRIDoubleReportExpressionProperty(chartAxisScaleBreak.Spacing, axisProperties[propertyPrefix + "ScaleBreakStyle.Spacing"], ref num);
			chartAxisScaleBreak.IncludeZero = new ReportExpression<ChartIncludeZeroTypes>(this.ConvertDundasCRIStringProperty(axisProperties[propertyPrefix + "ScaleBreakStyle.StartFromZero"], ref num), CultureInfo.InvariantCulture);
			chartAxisScaleBreak.Style = this.ConvertDundasCRIStyleProperty(null, null, null, null, null, null, null, axisProperties[propertyPrefix + "ScaleBreakStyle.LineColor"], axisProperties[propertyPrefix + "ScaleBreakStyle.LineStyle"], axisProperties[propertyPrefix + "ScaleBreakStyle.LineWidth"], null, null, null, null, null, null, null, null, null, ref num);
			if (num > 0)
			{
				axis.ChartAxisScaleBreak = chartAxisScaleBreak;
			}
			ChartTickMarks chartTickMarks = new ChartTickMarks();
			if (this.UpgradeDundasCRIChartTickMarks(chartTickMarks, axisProperties, propertyPrefix + "MajorTickMark.", true))
			{
				axis.ChartMajorTickMarks = chartTickMarks;
			}
			ChartTickMarks chartTickMarks2 = new ChartTickMarks();
			if (this.UpgradeDundasCRIChartTickMarks(chartTickMarks2, axisProperties, propertyPrefix + "MinorTickMark.", false))
			{
				axis.ChartMinorTickMarks = chartTickMarks2;
			}
			ChartGridLines chartGridLines = new ChartGridLines();
			if (this.UpgradeDundasCRIChartGridLines(chartGridLines, axisProperties, propertyPrefix + "MajorGrid.", true))
			{
				axis.ChartMajorGridLines = chartGridLines;
			}
			ChartGridLines chartGridLines2 = new ChartGridLines();
			if (this.UpgradeDundasCRIChartGridLines(chartGridLines2, axisProperties, propertyPrefix + "MinorGrid.", false))
			{
				axis.ChartMinorGridLines = chartGridLines2;
			}
			List<Hashtable> list = new List<Hashtable>();
			foreach (object obj in axisProperties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.AddToPropertyList(list, propertyPrefix + "StripLines.", dictionaryEntry.Key.ToString(), dictionaryEntry.Value.ToString());
			}
			foreach (Hashtable hashtable in list)
			{
				ChartStripLine chartStripLine = new ChartStripLine();
				chartStripLine.Title = this.ConvertDundasCRIStringProperty(hashtable["StripLine.Title"]);
				chartStripLine.Interval = this.ConvertDundasCRIDoubleReportExpressionProperty(hashtable["StripLine.Interval"]);
				chartStripLine.IntervalType = new ReportExpression<ChartIntervalTypes>(this.ConvertDundasCRIStringProperty(ChartIntervalTypes.Auto.ToString(), hashtable["StripLine.IntervalType"]), CultureInfo.InvariantCulture);
				chartStripLine.IntervalOffset = this.ConvertDundasCRIDoubleReportExpressionProperty(hashtable["StripLine.IntervalOffset"]);
				chartStripLine.IntervalOffsetType = new ReportExpression<ChartIntervalOffsetTypes>(this.ConvertDundasCRIStringProperty(ChartIntervalTypes.Auto.ToString(), hashtable["StripLine.IntervalOffsetType"]), CultureInfo.InvariantCulture);
				chartStripLine.StripWidth = this.ConvertDundasCRIDoubleReportExpressionProperty(hashtable["StripLine.StripWidth"]);
				chartStripLine.StripWidthType = new ReportExpression<ChartStripWidthTypes>(this.ConvertDundasCRIStringProperty(ChartIntervalTypes.Auto.ToString(), hashtable["StripLine.StripWidthType"]), CultureInfo.InvariantCulture);
				string text2 = this.ConvertDundasCRIStringProperty(hashtable["StripLine.TitleAngle"]);
				if (!(text2 == "90"))
				{
					if (!(text2 == "180"))
					{
						if (!(text2 == "270"))
						{
							chartStripLine.TextOrientation = TextOrientations.Horizontal;
						}
						else
						{
							chartStripLine.TextOrientation = TextOrientations.Rotated270;
						}
					}
					else
					{
						chartStripLine.TextOrientation = TextOrientations.Stacked;
					}
				}
				else
				{
					chartStripLine.TextOrientation = TextOrientations.Rotated90;
				}
				string text3 = this.ConvertDundasCRIStringProperty(hashtable["StripLine.Href"]);
				if (text3 != string.Empty)
				{
					chartStripLine.ActionInfo = new ActionInfo();
					Microsoft.ReportingServices.RdlObjectModel.Action action = new Microsoft.ReportingServices.RdlObjectModel.Action();
					action.Hyperlink = text3;
					chartStripLine.ActionInfo.Actions.Add(action);
				}
				Style style = new Style();
				int num2 = 0;
				string text4 = this.ConvertDundasCRIStringProperty(hashtable["StripLine.TitleAlignment"], ref num2);
				int num3 = 0;
				string text5 = this.ConvertDundasCRIStringProperty(hashtable["StripLine.TitleLineAlignment"], ref num3);
				style = this.ConvertDundasCRIStyleProperty(hashtable["StripLine.TitleColor"], hashtable["StripLine.BackColor"], hashtable["StripLine.BackGradientType"], hashtable["StripLine.BackGradientEndColor"], hashtable["StripLine.BackHatchStyle"], null, null, hashtable["StripLine.BorderColor"], hashtable["StripLine.BorderStyle"], hashtable["StripLine.BorderWidth"], hashtable["StripLine.BackImage"], hashtable["StripLine.BackImageTranspColor"], hashtable["StripLine.BackImageAlign"], hashtable["StripLine.BackImageMode"], hashtable["StripLine.TitleFont"] ?? "Microsoft Sans Serif, 8pt", null, null, (text4 == "Near") ? TextAlignments.Left.ToString() : ((text4 == "Center") ? TextAlignments.Center.ToString() : TextAlignments.Right.ToString()), (num3 == 0) ? null : ((text5 == "Center") ? VerticalAlignments.Middle.ToString() : ((text5 == "Far") ? VerticalAlignments.Bottom.ToString() : VerticalAlignments.Top.ToString())));
				chartStripLine.Style = style;
				axis.ChartStripLines.Add(chartStripLine);
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000D178 File Offset: 0x0000B378
		private void UpgradeDundasCRIChartSeries(ChartSeries series, IList<ChartDerivedSeries> derivedSeriesCollection, Hashtable seriesProperties, List<Hashtable> dataPointCustomProperties)
		{
			string text = this.ConvertDundasCRIStringProperty(seriesProperties["Type"]);
			this.SetChartSeriesType(series, text);
			this.ConvertDundasCRICustomProperties(series.CustomProperties, seriesProperties["CustomAttributes"]);
			ReportExpression reportExpression = null;
			foreach (CustomProperty customProperty in series.CustomProperties)
			{
				string value = customProperty.Name.Value;
				if (!(value == "ShowPieAsCollected"))
				{
					if (!(value == "CollectedPercentage"))
					{
						if (!(value == "CollectedSliceLabel"))
						{
							if (!(value == "CollectedSliceColor"))
							{
								if (!(value == "ShowCollectedLegend"))
								{
									if (value == "ShowCollectedPointLabels")
									{
										customProperty.Name = "CollectedChartShowLabels";
									}
								}
								else
								{
									customProperty.Name = "CollectedChartShowLegend";
								}
							}
							else
							{
								customProperty.Name = "CollectedColor";
							}
						}
						else
						{
							customProperty.Name = "CollectedLabel";
							reportExpression = customProperty.Value;
						}
					}
					else
					{
						customProperty.Name = "CollectedThreshold";
					}
				}
				else
				{
					customProperty.Name = "CollectedStyle";
					customProperty.Value = "CollectedPie";
				}
			}
			if (reportExpression != null)
			{
				CustomProperty customProperty2 = new CustomProperty();
				customProperty2.Name = "CollectedLegendText";
				customProperty2.Value = reportExpression;
				series.CustomProperties.Add(customProperty2);
			}
			series.ValueAxisName = this.ConvertDundasCRIStringProperty("Primary", seriesProperties["YAxisType"]);
			series.CategoryAxisName = this.ConvertDundasCRIStringProperty("Primary", seriesProperties["XAxisType"]);
			series.Style.ShadowOffset = this.ConvertDundasCRIPixelReportSizeProperty(seriesProperties["ShadowOffset"]);
			int num = 0;
			ChartItemInLegend chartItemInLegend = new ChartItemInLegend();
			chartItemInLegend.LegendText = this.ConvertDundasCRIStringProperty(seriesProperties["LegendText"], ref num);
			bool? flag = this.ConvertDundasCRIBoolProperty(seriesProperties["ShowInLegend"], ref num);
			if (flag != null)
			{
				chartItemInLegend.Hidden = !flag.Value;
			}
			if (num > 0)
			{
				series.ChartItemInLegend = chartItemInLegend;
			}
			num = 0;
			ChartSmartLabel chartSmartLabel = new ChartSmartLabel();
			chartSmartLabel.CalloutBackColor = this.ConvertDundasCRIColorProperty(chartSmartLabel.CalloutBackColor, seriesProperties["SmartLabels.CalloutBackColor"], ref num);
			chartSmartLabel.CalloutLineAnchor = new ReportExpression<ChartCalloutLineAnchorTypes>(this.ConvertDundasCRIStringProperty(seriesProperties["SmartLabels.CalloutLineAnchorCap"], ref num), CultureInfo.InvariantCulture);
			chartSmartLabel.CalloutLineColor = this.ConvertDundasCRIColorProperty(chartSmartLabel.CalloutLineColor, seriesProperties["SmartLabels.CalloutLineColor"], ref num);
			chartSmartLabel.CalloutLineStyle = new ReportExpression<ChartCalloutLineStyles>(this.ConvertDundasCRIStringProperty(seriesProperties["SmartLabels.CalloutLineStyle"], ref num), CultureInfo.InvariantCulture);
			chartSmartLabel.CalloutLineWidth = this.ConvertDundasCRIPixelReportSizeProperty(seriesProperties["SmartLabels.CalloutLineWidth"], ref num);
			chartSmartLabel.MaxMovingDistance = this.ConvertDundasCRIPixelReportSizeProperty(new double?(30.0), seriesProperties["SmartLabels.MaxMovingDistance"], ref num);
			chartSmartLabel.MinMovingDistance = this.ConvertDundasCRIPixelReportSizeProperty(seriesProperties["SmartLabels.MinMovingDistance"], ref num);
			string text2 = this.ConvertDundasCRIStringProperty(seriesProperties["SmartLabels.AllowOutsidePlotArea"], ref num);
			if (text2 == "Yes")
			{
				chartSmartLabel.AllowOutSidePlotArea = ChartAllowOutSidePlotAreaTypes.True;
			}
			else if (text2 == "No")
			{
				chartSmartLabel.AllowOutSidePlotArea = ChartAllowOutSidePlotAreaTypes.False;
			}
			else
			{
				chartSmartLabel.AllowOutSidePlotArea = ChartAllowOutSidePlotAreaTypes.Partial;
			}
			string text3 = this.ConvertDundasCRIStringProperty(seriesProperties["SmartLabels.CalloutStyle"], ref num);
			chartSmartLabel.CalloutStyle = new ReportExpression<ChartCalloutStyles>((text3 == "Underlined") ? ChartCalloutStyles.Underline.ToString() : text3, CultureInfo.InvariantCulture);
			bool? flag2 = this.ConvertDundasCRIBoolProperty(seriesProperties["SmartLabels.Enabled"], ref num);
			if (flag2 != null)
			{
				chartSmartLabel.Disabled = !flag2.Value;
			}
			bool? flag3 = this.ConvertDundasCRIBoolProperty(seriesProperties["SmartLabels.HideOverlapped"], ref num);
			if (flag3 != null)
			{
				chartSmartLabel.ShowOverlapped = !flag3.Value;
			}
			bool? flag4 = this.ConvertDundasCRIBoolProperty(seriesProperties["SmartLabels.MarkerOverlapping"], ref num);
			if (flag4 != null)
			{
				chartSmartLabel.MarkerOverlapping = flag4.Value;
			}
			string text4 = this.ConvertDundasCRIStringProperty(seriesProperties["SmartLabels.MovingDirection"], ref num);
			if (text4 != string.Empty)
			{
				ChartNoMoveDirections chartNoMoveDirections = new ChartNoMoveDirections();
				chartSmartLabel.ChartNoMoveDirections = chartNoMoveDirections;
				text4 = " " + text4.Replace(',', ' ') + " ";
				chartNoMoveDirections.Down = !text4.Contains(" Bottom ");
				chartNoMoveDirections.DownLeft = !text4.Contains(" BottomLeft ");
				chartNoMoveDirections.DownRight = !text4.Contains(" BottomRight ");
				chartNoMoveDirections.Left = !text4.Contains(" Left ");
				chartNoMoveDirections.Right = !text4.Contains(" Right ");
				chartNoMoveDirections.Up = !text4.Contains(" Top ");
				chartNoMoveDirections.UpLeft = !text4.Contains(" TopLeft ");
				chartNoMoveDirections.UpRight = !text4.Contains(" TopRight ");
			}
			if (num > 0)
			{
				series.ChartSmartLabel = chartSmartLabel;
			}
			num = 0;
			ChartEmptyPoints chartEmptyPoints = new ChartEmptyPoints();
			this.ConvertDundasCRICustomProperties(chartEmptyPoints.CustomProperties, seriesProperties["EmptyPointStyle.CustomAttributes"], ref num);
			chartEmptyPoints.AxisLabel = this.ConvertDundasCRIStringProperty(seriesProperties["EmptyPointStyle.AxisLabel"], ref num);
			chartEmptyPoints.ActionInfo = this.ConvertDundasCRIActionInfoProperty(seriesProperties["EmptyPointStyle.Href"], ref num);
			chartEmptyPoints.Style = this.ConvertDundasCRIEmptyColorStyleProperty(seriesProperties["EmptyPointStyle.Color"], null, seriesProperties["EmptyPointStyle.BackGradientType"], seriesProperties["EmptyPointStyle.BackGradientEndColor"], seriesProperties["EmptyPointStyle.BackHatchStyle"], seriesProperties["EmptyPointStyle.BorderColor"], seriesProperties["EmptyPointStyle.BorderStyle"], seriesProperties["EmptyPointStyle.BorderWidth"], seriesProperties["EmptyPointStyle.BackImage"], seriesProperties["EmptyPointStyle.BackImageTranspColor"], seriesProperties["EmptyPointStyle.BackImageAlign"], seriesProperties["EmptyPointStyle.BackImageMode"], null, null, ref num);
			int num2 = 0;
			ChartMarker chartMarker = new ChartMarker();
			chartMarker.Style = this.ConvertDundasCRIEmptyColorStyleProperty(seriesProperties["EmptyPointStyle.MarkerColor"], null, null, null, null, seriesProperties["EmptyPointStyle.MarkerBorderColor"], null, seriesProperties["EmptyPointStyle.MarkerBorderWidth"], seriesProperties["EmptyPointStyle.MarkerImage"], seriesProperties["EmptyPointStyle.MarkerImageTranspColor"], seriesProperties["EmptyPointStyle.MarkerImageAlign"], seriesProperties["EmptyPointStyle.MarkerImageMode"], null, null, ref num2);
			chartMarker.Type = new ReportExpression<ChartMarkerTypes>(this.ConvertDundasCRIStringProperty(seriesProperties["EmptyPointStyle.MarkerStyle"], ref num2), CultureInfo.InvariantCulture);
			chartMarker.Size = this.ConvertDundasCRIPixelReportSizeProperty(seriesProperties["EmptyPointStyle.MarkerSize"], ref num2);
			if (num2 > 0)
			{
				chartEmptyPoints.ChartMarker = chartMarker;
				num++;
			}
			int num3 = 0;
			ChartDataLabel chartDataLabel = new ChartDataLabel();
			string text5 = this.ConvertDundasCRIStringProperty(seriesProperties["EmptyPointStyle.Label"], ref num3);
			chartDataLabel.Label = text5;
			chartDataLabel.Visible = !string.IsNullOrEmpty(text5);
			chartDataLabel.Rotation = this.ConvertDundasCRIIntegerReportExpressionProperty(seriesProperties["EmptyPointStyle.FontAngle"], ref num3);
			chartDataLabel.ActionInfo = this.ConvertDundasCRIActionInfoProperty(seriesProperties["ChartEmptyPointstyle.LabelHref"], ref num3);
			chartDataLabel.Style = this.ConvertDundasCRIEmptyColorStyleProperty(seriesProperties["EmptyPointStyle.FontColor"], seriesProperties["EmptyPointStyle.LabelBackColor"], null, null, null, seriesProperties["EmptyPointStyle.LabelBorderColor"], seriesProperties["EmptyPointStyle.LabelBorderStyle"], seriesProperties["EmptyPointStyle.LabelBorderWidth"], null, null, null, null, seriesProperties["EmptyPointStyle.Font"] ?? "Microsoft Sans Serif, 8pt", null, ref num2);
			if (num3 > 0)
			{
				chartEmptyPoints.ChartDataLabel = chartDataLabel;
				num++;
			}
			if (num > 0)
			{
				series.ChartEmptyPoints = chartEmptyPoints;
			}
			foreach (object obj in seriesProperties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text6 = dictionaryEntry.Key.ToString();
				if (text6.Equals("ERRORFORMULA:BOXPLOT", StringComparison.OrdinalIgnoreCase) || text6.Equals("FINANCIALFORMULA:FORECASTING", StringComparison.OrdinalIgnoreCase))
				{
					if (this.m_throwUpgradeException)
					{
						throw new CRI2005UpgradeException();
					}
					base.UpgradeResults.HasUnsupportedDundasChartFeatures = true;
				}
				if (text6.StartsWith("FINANCIALFORMULA:", StringComparison.OrdinalIgnoreCase) || text6.StartsWith("STATISTICALFORMULA:", StringComparison.OrdinalIgnoreCase))
				{
					ChartDerivedSeries chartDerivedSeries = new ChartDerivedSeries();
					chartDerivedSeries.SourceChartSeriesName = series.Name;
					string text7 = (chartDerivedSeries.ChartSeries.Name = series.Name + "_Formula");
					int num4 = 1;
					bool flag5 = false;
					do
					{
						flag5 = false;
						using (IEnumerator<ChartDerivedSeries> enumerator3 = derivedSeriesCollection.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								if (enumerator3.Current.ChartSeries.Name == text7 + ((num4 > 1) ? num4.ToString(CultureInfo.InvariantCulture) : string.Empty))
								{
									flag5 = true;
									num4++;
									break;
								}
							}
						}
					}
					while (flag5);
					if (num4 > 1)
					{
						ChartSeries chartSeries = chartDerivedSeries.ChartSeries;
						chartSeries.Name += num4.ToString(CultureInfo.InvariantCulture);
					}
					string text8 = text6.Substring(text6.IndexOf(':') + 1);
					try
					{
						chartDerivedSeries.DerivedSeriesFormula = (ChartFormulas)Enum.Parse(typeof(ChartFormulas), text8);
					}
					catch
					{
						break;
					}
					string[] array = dictionaryEntry.Value.ToString().Split(new char[] { ';' });
					for (int i = 0; i < array.Length; i++)
					{
						string[] array2 = array[i].Split(new char[] { '=' });
						string text9 = ((array2.Length != 0) ? array2[0].ToUpperInvariant().Trim() : string.Empty);
						string text10 = ((array2.Length > 1) ? array2[1] : string.Empty);
						if (text9 == "SERIESTYPE")
						{
							this.SetChartSeriesType(chartDerivedSeries.ChartSeries, text10);
						}
						else if (text9 == "SHOWLEGEND")
						{
							bool flag6;
							if (bool.TryParse(text10, out flag6))
							{
								if (chartDerivedSeries.ChartSeries.ChartItemInLegend == null)
								{
									chartDerivedSeries.ChartSeries.ChartItemInLegend = new ChartItemInLegend();
								}
								chartDerivedSeries.ChartSeries.ChartItemInLegend.Hidden = !flag6;
							}
						}
						else if (text9 == "LEGENDTEXT")
						{
							if (chartDerivedSeries.ChartSeries.ChartItemInLegend == null)
							{
								chartDerivedSeries.ChartSeries.ChartItemInLegend = new ChartItemInLegend();
							}
							chartDerivedSeries.ChartSeries.ChartItemInLegend.LegendText = text10.Replace("_x003B_", ";").Replace("_x003D_", "=");
						}
						else if (text9 == "FORMULAPARAMETERS")
						{
							ChartFormulaParameter chartFormulaParameter = new ChartFormulaParameter();
							chartFormulaParameter.Name = "FormulaParameters";
							chartFormulaParameter.Value = text10;
							chartDerivedSeries.ChartFormulaParameters.Add(chartFormulaParameter);
						}
						else if (text9 == "NEWAREA")
						{
							bool flag7;
							if (bool.TryParse(text10, out flag7) && flag7)
							{
								chartDerivedSeries.ChartSeries.ChartAreaName = "#NewChartArea";
							}
						}
						else if (text9 == "STARTFROMFIRST")
						{
							ChartFormulaParameter chartFormulaParameter2 = new ChartFormulaParameter();
							chartFormulaParameter2.Name = "StartFromFirst";
							chartFormulaParameter2.Value = text10;
							chartDerivedSeries.ChartFormulaParameters.Add(chartFormulaParameter2);
						}
						else if (text9 == "OUTPUT")
						{
							ChartFormulaParameter chartFormulaParameter3 = new ChartFormulaParameter();
							chartFormulaParameter3.Name = "Output";
							chartFormulaParameter3.Value = text10.Replace("#OUTPUTSERIES", chartDerivedSeries.ChartSeries.Name);
							chartDerivedSeries.ChartFormulaParameters.Add(chartFormulaParameter3);
						}
						else if (text9 == "INPUT")
						{
							ChartFormulaParameter chartFormulaParameter4 = new ChartFormulaParameter();
							chartFormulaParameter4.Name = "Input";
							chartFormulaParameter4.Value = text10;
							chartDerivedSeries.ChartFormulaParameters.Add(chartFormulaParameter4);
						}
						else if (text9 == "SECONDARYAXIS")
						{
							if (this.m_throwUpgradeException)
							{
								throw new CRI2005UpgradeException();
							}
							base.UpgradeResults.HasUnsupportedDundasChartFeatures = true;
						}
					}
					derivedSeriesCollection.Add(chartDerivedSeries);
				}
			}
			string text11 = this.ConvertDundasCRIStringProperty(seriesProperties["ID"]);
			if (text11 != null)
			{
				foreach (Hashtable hashtable in dataPointCustomProperties)
				{
					if (this.ConvertDundasCRIStringProperty(hashtable["ID"]) == text11)
					{
						ChartDataPoint chartDataPoint = new ChartDataPoint();
						chartDataPoint.ChartDataPointValues = new ChartDataPointValues();
						series.ChartDataPoints.Add(chartDataPoint);
						this.ConvertDundasCRICustomProperties(chartDataPoint.CustomProperties, hashtable["CustomAttributes"]);
						chartDataPoint.AxisLabel = this.ConvertDundasCRIStringProperty(hashtable["AxisLabel"]);
						chartDataPoint.ChartDataPointValues.X = this.ConvertDundasCRIStringProperty(hashtable["XValue"]);
						if (series.Type.Value == ChartTypes.Range)
						{
							switch (series.Subtype.Value)
							{
							case ChartSubtypes.Candlestick:
							case ChartSubtypes.Stock:
								chartDataPoint.ChartDataPointValues.End = this.ConvertDundasCRIStringProperty(hashtable["Y3"]);
								chartDataPoint.ChartDataPointValues.Start = this.ConvertDundasCRIStringProperty(hashtable["Y2"]);
								chartDataPoint.ChartDataPointValues.Low = this.ConvertDundasCRIStringProperty(hashtable["Y1"]);
								chartDataPoint.ChartDataPointValues.High = this.ConvertDundasCRIStringProperty(hashtable["Y0"]);
								goto IL_10B8;
							case ChartSubtypes.Bar:
								chartDataPoint.ChartDataPointValues.End = this.ConvertDundasCRIStringProperty(hashtable["Y1"]);
								chartDataPoint.ChartDataPointValues.Start = this.ConvertDundasCRIStringProperty(hashtable["Y0"]);
								goto IL_10B8;
							case ChartSubtypes.BoxPlot:
								chartDataPoint.ChartDataPointValues.Median = this.ConvertDundasCRIStringProperty(hashtable["Y5"]);
								chartDataPoint.ChartDataPointValues.Mean = this.ConvertDundasCRIStringProperty(hashtable["Y4"]);
								chartDataPoint.ChartDataPointValues.End = this.ConvertDundasCRIStringProperty(hashtable["Y3"]);
								chartDataPoint.ChartDataPointValues.Start = this.ConvertDundasCRIStringProperty(hashtable["Y2"]);
								chartDataPoint.ChartDataPointValues.High = this.ConvertDundasCRIStringProperty(hashtable["Y1"]);
								chartDataPoint.ChartDataPointValues.Low = this.ConvertDundasCRIStringProperty(hashtable["Y0"]);
								goto IL_10B8;
							case ChartSubtypes.ErrorBar:
								chartDataPoint.ChartDataPointValues.High = this.ConvertDundasCRIStringProperty(hashtable["Y2"]);
								chartDataPoint.ChartDataPointValues.Low = this.ConvertDundasCRIStringProperty(hashtable["Y1"]);
								chartDataPoint.ChartDataPointValues.Y = this.ConvertDundasCRIStringProperty(hashtable["Y0"]);
								goto IL_10B8;
							}
							chartDataPoint.ChartDataPointValues.Low = this.ConvertDundasCRIStringProperty(hashtable["Y1"]);
							chartDataPoint.ChartDataPointValues.High = this.ConvertDundasCRIStringProperty(hashtable["Y0"]);
						}
						else
						{
							if (series.Subtype.Value == ChartSubtypes.Bubble)
							{
								chartDataPoint.ChartDataPointValues.Size = this.ConvertDundasCRIStringProperty(hashtable["Y1"]);
							}
							chartDataPoint.ChartDataPointValues.Y = this.ConvertDundasCRIStringProperty(hashtable["Y0"]);
						}
						IL_10B8:
						if (!text.StartsWith("fast", StringComparison.OrdinalIgnoreCase))
						{
							chartDataPoint.Style = this.ConvertDundasCRIEmptyColorStyleProperty(hashtable["Color"], null, hashtable["BackGradientType"], hashtable["BackGradientEndColor"], hashtable["BackHatchStyle"], hashtable["BorderColor"] ?? seriesProperties["BorderColor"], hashtable["BorderStyle"] ?? seriesProperties["BorderStyle"], hashtable["BorderWidth"] ?? seriesProperties["BorderWidth"], hashtable["BackImage"], hashtable["BackImageTranspColor"], hashtable["BackImageAlign"], hashtable["BackImageMode"], null, null, ref num);
						}
						else
						{
							chartDataPoint.Style = this.ConvertDundasCRIEmptyColorStyleProperty(hashtable["Color"], null, hashtable["BackGradientType"], hashtable["BackGradientEndColor"], hashtable["BackHatchStyle"], null, null, null, null, null, null, null, null, null, ref num);
						}
						num = 0;
						ChartMarker chartMarker2 = new ChartMarker();
						chartMarker2.Type = new ReportExpression<ChartMarkerTypes>(this.ConvertDundasCRIStringProperty(hashtable["MarkerStyle"], ref num), CultureInfo.InvariantCulture);
						chartMarker2.Size = this.ConvertDundasCRIPixelReportSizeProperty(hashtable["MarkerSize"] ?? seriesProperties["MarkerSize"], ref num);
						chartMarker2.Style = this.ConvertDundasCRIEmptyColorStyleProperty(hashtable["MarkerColor"], null, null, null, null, (!this.IsZero(hashtable["MarkerBorderWidth"])) ? hashtable["MarkerBorderColor"] : ReportColor.Empty, null, (!this.IsZero(hashtable["MarkerBorderWidth"])) ? hashtable["MarkerBorderWidth"] : null, hashtable["MarkerImage"], hashtable["MarkerImageTranspColor"], null, null, null, null, ref num);
						if (num > 0)
						{
							chartDataPoint.ChartMarker = chartMarker2;
						}
						num = 0;
						ChartDataLabel chartDataLabel2 = new ChartDataLabel();
						bool? flag8 = this.ConvertDundasCRIBoolProperty(seriesProperties["ShowLabelAsValue"], ref num);
						if (flag8 != null)
						{
							chartDataLabel2.UseValueAsLabel = flag8.Value;
						}
						string text12 = this.ConvertDundasCRIStringProperty(hashtable["Label"], ref num);
						chartDataLabel2.Label = text12;
						chartDataLabel2.Visible = !string.IsNullOrEmpty(text12) || chartDataLabel2.UseValueAsLabel.Value;
						chartDataLabel2.Rotation = this.ConvertDundasCRIIntegerReportExpressionProperty(hashtable["FontAngle"], ref num);
						chartDataLabel2.ActionInfo = this.ConvertDundasCRIActionInfoProperty(hashtable["LabelHref"], ref num);
						chartDataLabel2.Style = this.ConvertDundasCRIStyleProperty(hashtable["FontColor"], hashtable["LabelBackColor"], null, null, null, null, null, hashtable["LabelBorderColor"], hashtable["LabelBorderStyle"], hashtable["LabelBorderWidth"], null, null, null, null, hashtable["Font"] ?? "Microsoft Sans Serif, 8pt", hashtable["LabelFormat"], null, null, null, ref num);
						if (num > 0)
						{
							chartDataPoint.ChartDataLabel = chartDataLabel2;
						}
						chartDataPoint.ActionInfo = this.UpgradeDundasCRIChartActionInfo(hashtable) ?? this.ConvertDundasCRIActionInfoProperty(hashtable["Href"]);
						num = 0;
						ChartItemInLegend chartItemInLegend2 = new ChartItemInLegend();
						chartItemInLegend2.ActionInfo = this.ConvertDundasCRIActionInfoProperty(hashtable["LegendHref"], ref num);
						chartItemInLegend2.LegendText = this.ConvertDundasCRIStringProperty(hashtable["LegendText"], ref num);
						if (num > 0)
						{
							chartDataPoint.ChartItemInLegend = chartItemInLegend2;
						}
						string text13 = this.ConvertDundasCRIStringProperty(hashtable["MarkerBorderWidth"]);
						if (series.ChartEmptyPoints != null && series.ChartEmptyPoints.ChartMarker != null && text13 != string.Empty)
						{
							if (series.ChartEmptyPoints.ChartMarker.Style == null)
							{
								series.ChartEmptyPoints.ChartMarker.Style = new EmptyColorStyle();
							}
							if (series.ChartEmptyPoints.ChartMarker.Style.Border == null)
							{
								series.ChartEmptyPoints.ChartMarker.Style.Border = new EmptyBorder();
							}
							if (this.IsZero(text13))
							{
								series.ChartEmptyPoints.ChartMarker.Style.Border.Color = ReportColor.Empty;
							}
							else
							{
								series.ChartEmptyPoints.ChartMarker.Style.Border.Width = this.ConvertDundasCRIPixelReportSizeProperty(text13);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000E788 File Offset: 0x0000C988
		private bool UpgradeDundasCRIChartTitle(ChartTitle title, Hashtable titleProperties, string propertyPrefix)
		{
			int num = 0;
			title.Caption = this.ConvertDundasCRIStringProperty(titleProperties[propertyPrefix + "Text"], ref num);
			title.DockOffset = this.ConvertDundasCRIIntegerReportExpressionProperty(titleProperties[propertyPrefix + "DockOffset"], ref num);
			string text = this.ConvertDundasCRIStringProperty(titleProperties[propertyPrefix + "Docking"], ref num);
			string text2 = this.ConvertDundasCRIStringProperty(titleProperties[propertyPrefix + "Alignment"], ref num);
			title.Position = this.ConvertDundasCRIPosition(text, text2);
			bool? flag = this.ConvertDundasCRIBoolProperty(titleProperties[propertyPrefix + "DockInsideChartArea"], ref num);
			if (flag != null)
			{
				title.DockOutsideChartArea = !flag.Value;
			}
			bool? flag2 = this.ConvertDundasCRIBoolProperty(titleProperties[propertyPrefix + "Visible"], ref num);
			if (flag2 != null)
			{
				title.Hidden = !flag2.Value;
			}
			title.Style = this.ConvertDundasCRIStyleProperty(titleProperties[propertyPrefix + "Color"], titleProperties[propertyPrefix + "BackColor"], titleProperties[propertyPrefix + "BackGradientType"], titleProperties[propertyPrefix + "BackGradientEndColor"], titleProperties[propertyPrefix + "BackHatchStyle"], titleProperties[propertyPrefix + "ShadowColor"], titleProperties[propertyPrefix + "ShadowOffset"], titleProperties[propertyPrefix + "BorderColor"] ?? Color.Transparent, titleProperties[propertyPrefix + "BorderStyle"], titleProperties[propertyPrefix + "BorderWidth"], titleProperties[propertyPrefix + "BackImage"], titleProperties[propertyPrefix + "BackImageTranspColor"], titleProperties[propertyPrefix + "BackImageAlign"], titleProperties[propertyPrefix + "BackImageMode"], this.ConvertDundasCRIStringProperty("Microsoft Sans Serif, 8pt", titleProperties[propertyPrefix + "Font"]), null, titleProperties[propertyPrefix + "Style"], null, null, ref num);
			title.ChartElementPosition = this.ConvertDundasCRIChartElementPosition(titleProperties[propertyPrefix + "Position.Y"], titleProperties[propertyPrefix + "Position.X"], titleProperties[propertyPrefix + "Position.Height"], titleProperties[propertyPrefix + "Position.Width"], ref num);
			return num > 0;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000EA24 File Offset: 0x0000CC24
		private void UpgradeDundasCRIChartLegend(ChartLegend legend, Hashtable legendProperties, string propertyPrefix)
		{
			legend.HeaderSeparator = new ReportExpression<ChartHeaderSeparatorTypes>(this.ConvertDundasCRIStringProperty(legendProperties[propertyPrefix + "HeaderSeparator"]), CultureInfo.InvariantCulture);
			legend.InterlacedRowsColor = this.ConvertDundasCRIColorProperty(legend.InterlacedRowsColor, legendProperties[propertyPrefix + "InterlacedRowsColor"]);
			legend.Reversed = new ReportExpression<ChartLegendReversedTypes>(this.ConvertDundasCRIStringProperty(legendProperties[propertyPrefix + "Reversed"]), CultureInfo.InvariantCulture);
			legend.ColumnSeparator = new ReportExpression<ChartColumnSeparatorTypes>(this.ConvertDundasCRIStringProperty(legendProperties[propertyPrefix + "ItemColumnSeparator"]), CultureInfo.InvariantCulture);
			legend.ColumnSeparatorColor = this.ConvertDundasCRIColorProperty(legend.ColumnSeparatorColor, legendProperties[propertyPrefix + "ItemColumnSeparatorColor"]);
			legend.ColumnSpacing = this.ConvertDundasCRIIntegerReportExpressionProperty(legend.ColumnSpacing, legendProperties[propertyPrefix + "ItemColumnSpacing"]);
			legend.MaxAutoSize = this.ConvertDundasCRIIntegerReportExpressionProperty(legend.MaxAutoSize, legendProperties[propertyPrefix + "MaxAutoSize"]);
			legend.TextWrapThreshold = this.ConvertDundasCRIIntegerReportExpressionProperty(legend.TextWrapThreshold, legendProperties[propertyPrefix + "TextWrapThreshold"]);
			legend.MinFontSize = this.ConvertDundasCRIPointReportSizeProperty(legend.MinFontSize, legendProperties[propertyPrefix + "AutoFitMinFontSize"]);
			bool? flag = this.ConvertDundasCRIBoolProperty(legendProperties[propertyPrefix + "AutoFitText"]);
			if (flag != null)
			{
				legend.AutoFitTextDisabled = !flag.Value;
			}
			bool? flag2 = this.ConvertDundasCRIBoolProperty(legendProperties[propertyPrefix + "Enabled"]);
			if (flag2 != null)
			{
				legend.Hidden = !flag2.Value;
			}
			bool? flag3 = this.ConvertDundasCRIBoolProperty(legendProperties[propertyPrefix + "DockInsideChartArea"]);
			if (flag3 != null)
			{
				legend.DockOutsideChartArea = !flag3.Value;
			}
			bool? flag4 = this.ConvertDundasCRIBoolProperty(legendProperties[propertyPrefix + "InterlacedRows"]);
			if (flag4 != null)
			{
				legend.InterlacedRows = flag4.Value;
			}
			bool? flag5 = this.ConvertDundasCRIBoolProperty(legendProperties[propertyPrefix + "EquallySpacedItems"]);
			if (flag5 != null)
			{
				legend.EquallySpacedItems = flag5.Value;
			}
			legend.Style = this.ConvertDundasCRIStyleProperty(legendProperties[propertyPrefix + "FontColor"], legendProperties[propertyPrefix + "BackColor"], legendProperties[propertyPrefix + "BackGradientType"], legendProperties[propertyPrefix + "BackGradientEndColor"], legendProperties[propertyPrefix + "BackHatchStyle"], legendProperties[propertyPrefix + "ShadowColor"], legendProperties[propertyPrefix + "ShadowOffset"], legendProperties[propertyPrefix + "BorderColor"], legendProperties[propertyPrefix + "BorderStyle"] ?? BorderStyles.Solid, legendProperties[propertyPrefix + "BorderWidth"], legendProperties[propertyPrefix + "BackImage"], legendProperties[propertyPrefix + "BackImageTranspColor"], legendProperties[propertyPrefix + "BackImageAlign"], legendProperties[propertyPrefix + "BackImageMode"], this.ConvertDundasCRIStringProperty("Microsoft Sans Serif, 8pt", legendProperties[propertyPrefix + "Font"]), null, null, null, null);
			string text = this.ConvertDundasCRIStringProperty("Right", legendProperties[propertyPrefix + "Docking"]);
			string text2 = this.ConvertDundasCRIStringProperty("Near", legendProperties[propertyPrefix + "Alignment"]);
			if (text == "Top" || text == "Bottom")
			{
				text2 = text2.Replace("Near", "Left").Replace("Far", "Right");
			}
			else
			{
				text2 = text2.Replace("Near", "Top").Replace("Far", "Bottom");
			}
			legend.Position = new ReportExpression<ChartPositions>(text + text2, CultureInfo.InvariantCulture);
			string text3 = this.ConvertDundasCRIStringProperty(legendProperties[propertyPrefix + "LegendStyle"]);
			string text4 = this.ConvertDundasCRIStringProperty(legendProperties[propertyPrefix + "TableStyle"]);
			if (text3 != "" && text3 != "Table")
			{
				legend.Layout = new ReportExpression<ChartLegendLayouts>(text3, CultureInfo.InvariantCulture);
			}
			else if (text4 == "Wide")
			{
				legend.Layout = ChartLegendLayouts.WideTable;
			}
			else if (text4 == "Tall")
			{
				legend.Layout = ChartLegendLayouts.TallTable;
			}
			else
			{
				legend.Layout = ChartLegendLayouts.AutoTable;
			}
			legend.ChartElementPosition = this.ConvertDundasCRIChartElementPosition(legendProperties[propertyPrefix + "Position.Y"], legendProperties[propertyPrefix + "Position.X"], legendProperties[propertyPrefix + "Position.Height"], legendProperties[propertyPrefix + "Position.Width"]);
			int num = 0;
			ChartLegendTitle chartLegendTitle = new ChartLegendTitle();
			chartLegendTitle.Caption = this.ConvertDundasCRIStringProperty(legendProperties[propertyPrefix + "Title"], ref num);
			int num2 = 0;
			string text5 = this.ConvertDundasCRIStringProperty(legendProperties[propertyPrefix + "TitleAlignment"], ref num2);
			chartLegendTitle.Style = this.ConvertDundasCRIStyleProperty(legendProperties[propertyPrefix + "TitleColor"], legendProperties[propertyPrefix + "TitleBackColor"], null, null, null, null, null, legendProperties[propertyPrefix + "TitleSeparatorColor"], null, null, null, null, null, null, this.ConvertDundasCRIStringProperty("Microsoft Sans Serif, 8pt, style=Bold", legendProperties[propertyPrefix + "TitleFont"]), null, null, (num2 == 0) ? null : ((text5 == "Near") ? TextAlignments.Left.ToString() : ((text5 == "Far") ? TextAlignments.Right.ToString() : TextAlignments.Center.ToString())), null, ref num);
			try
			{
				chartLegendTitle.TitleSeparator = new ReportExpression<ChartTitleSeparatorTypes>(this.ConvertDundasCRIStringProperty(legendProperties[propertyPrefix + "TitleSeparator"], ref num), CultureInfo.InvariantCulture);
			}
			catch
			{
			}
			if (num > 0)
			{
				legend.ChartLegendTitle = chartLegendTitle;
			}
			foreach (object obj in legendProperties)
			{
				string text6 = ((DictionaryEntry)obj).Key.ToString();
				if (text6.StartsWith("LEGEND.CUSTOMITEMS.", StringComparison.OrdinalIgnoreCase) || text6.StartsWith("LEGEND.CELLCOLUMNS.", StringComparison.OrdinalIgnoreCase))
				{
					base.UpgradeResults.HasUnsupportedDundasChartFeatures = true;
				}
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000F12C File Offset: 0x0000D32C
		private bool UpgradeDundasCRIChartGridLines(ChartGridLines gridLines, Hashtable properties, string propertyPrefix, bool isMajor)
		{
			int num = 0;
			gridLines.Interval = this.ConvertDundasCRIDoubleReportExpressionProperty(properties[propertyPrefix + "Interval"], ref num);
			gridLines.IntervalType = new ReportExpression<ChartIntervalTypes>(this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "IntervalType"], ref num), CultureInfo.InvariantCulture);
			gridLines.IntervalOffset = this.ConvertDundasCRIDoubleReportExpressionProperty(double.NaN, properties[propertyPrefix + "IntervalOffset"], ref num);
			gridLines.IntervalOffsetType = new ReportExpression<ChartIntervalOffsetTypes>(this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "IntervalOffsetType"], ref num), CultureInfo.InvariantCulture);
			if (isMajor)
			{
				bool? flag = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "Enabled"], ref num);
				if (flag != null && !flag.Value)
				{
					gridLines.Enabled = ChartGridLinesEnabledTypes.False;
				}
				else
				{
					gridLines.Enabled = ChartGridLinesEnabledTypes.True;
				}
			}
			else
			{
				bool? flag2 = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "Disabled"], ref num);
				if (flag2 != null && !flag2.Value)
				{
					gridLines.Enabled = ChartGridLinesEnabledTypes.True;
				}
				else
				{
					gridLines.Enabled = ChartGridLinesEnabledTypes.False;
				}
			}
			gridLines.Style = this.ConvertDundasCRIStyleProperty(null, null, null, null, null, null, null, properties[propertyPrefix + "LineColor"], properties[propertyPrefix + "LineStyle"], properties[propertyPrefix + "LineWidth"], null, null, null, null, null, null, null, null, null, ref num);
			return num > 0;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
		private bool UpgradeDundasCRIChartTickMarks(ChartTickMarks tickMarks, Hashtable properties, string propertyPrefix, bool isMajor)
		{
			int num = 0;
			tickMarks.Type = new ReportExpression<ChartTickMarkTypes>(this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "Style"], ref num), CultureInfo.InvariantCulture);
			tickMarks.Length = this.ConvertDundasCRIDoubleReportExpressionProperty(tickMarks.Length, properties[propertyPrefix + "Size"], ref num);
			tickMarks.Interval = this.ConvertDundasCRIDoubleReportExpressionProperty(properties[propertyPrefix + "Interval"], ref num);
			tickMarks.IntervalType = new ReportExpression<ChartIntervalTypes>(this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "IntervalType"], ref num), CultureInfo.InvariantCulture);
			tickMarks.IntervalOffset = this.ConvertDundasCRIDoubleReportExpressionProperty(properties[propertyPrefix + "IntervalOffset"], ref num);
			tickMarks.IntervalOffsetType = new ReportExpression<ChartIntervalOffsetTypes>(this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "IntervalOffsetType"], ref num), CultureInfo.InvariantCulture);
			if (isMajor)
			{
				bool? flag = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "Enabled"], ref num);
				if (flag != null && !flag.Value)
				{
					tickMarks.Enabled = ChartTickMarksEnabledTypes.False;
				}
				else
				{
					tickMarks.Enabled = ChartTickMarksEnabledTypes.True;
				}
			}
			else
			{
				bool? flag2 = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "Disabled"], ref num);
				if (flag2 != null && !flag2.Value)
				{
					tickMarks.Enabled = ChartTickMarksEnabledTypes.True;
				}
				else
				{
					tickMarks.Enabled = ChartTickMarksEnabledTypes.False;
				}
			}
			tickMarks.Style = this.ConvertDundasCRIStyleProperty(null, null, null, null, null, null, null, properties[propertyPrefix + "LineColor"], properties[propertyPrefix + "LineStyle"], properties[propertyPrefix + "LineWidth"], null, null, null, null, null, null, null, null, null, ref num);
			return num > 0;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000F494 File Offset: 0x0000D694
		private ActionInfo UpgradeDundasCRIChartActionInfo(Hashtable properties)
		{
			return this.UpgradeDundasCRIActionInfo(properties, string.Empty, "Hyperlink");
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
		private ChartPositions ConvertDundasCRIPosition(string docking, string alignment)
		{
			if (!(docking == "Left"))
			{
				if (!(docking == "Right"))
				{
					if (!(docking == "Bottom"))
					{
						if (alignment.EndsWith("Left", StringComparison.Ordinal))
						{
							return ChartPositions.TopLeft;
						}
						if (alignment.EndsWith("Right", StringComparison.Ordinal))
						{
							return ChartPositions.TopRight;
						}
						return ChartPositions.TopCenter;
					}
					else
					{
						if (alignment.EndsWith("Left", StringComparison.Ordinal))
						{
							return ChartPositions.BottomLeft;
						}
						if (alignment.EndsWith("Right", StringComparison.Ordinal))
						{
							return ChartPositions.BottomRight;
						}
						return ChartPositions.BottomCenter;
					}
				}
				else
				{
					if (alignment.EndsWith("Left", StringComparison.Ordinal))
					{
						return ChartPositions.RightTop;
					}
					if (alignment.EndsWith("Right", StringComparison.Ordinal))
					{
						return ChartPositions.RightBottom;
					}
					return ChartPositions.RightCenter;
				}
			}
			else
			{
				if (alignment.EndsWith("Right", StringComparison.Ordinal))
				{
					return ChartPositions.LeftTop;
				}
				if (alignment.EndsWith("Left", StringComparison.Ordinal))
				{
					return ChartPositions.LeftBottom;
				}
				return ChartPositions.LeftCenter;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000F568 File Offset: 0x0000D768
		private void SetChartSeriesType(ChartSeries series, string dundasSeriesType)
		{
			if (dundasSeriesType != null)
			{
				switch (dundasSeriesType.Length)
				{
				case 3:
				{
					char c = dundasSeriesType[0];
					if (c != 'B')
					{
						if (c != 'P')
						{
							return;
						}
						if (!(dundasSeriesType == "Pie"))
						{
							return;
						}
						series.Type = ChartTypes.Shape;
						series.Subtype = ChartSubtypes.Pie;
						return;
					}
					else
					{
						if (!(dundasSeriesType == "Bar"))
						{
							return;
						}
						series.Type = ChartTypes.Bar;
						series.Subtype = ChartSubtypes.Plain;
						return;
					}
					break;
				}
				case 4:
				{
					char c = dundasSeriesType[0];
					if (c != 'A')
					{
						if (c != 'L')
						{
							return;
						}
						if (!(dundasSeriesType == "Line"))
						{
							return;
						}
					}
					else
					{
						if (!(dundasSeriesType == "Area"))
						{
							return;
						}
						series.Type = ChartTypes.Area;
						series.Subtype = ChartSubtypes.Plain;
						return;
					}
					break;
				}
				case 5:
				{
					char c = dundasSeriesType[2];
					if (c != 'd')
					{
						switch (c)
						{
						case 'i':
							if (!(dundasSeriesType == "Point"))
							{
								return;
							}
							goto IL_04D6;
						case 'j':
						case 'k':
						case 'm':
							return;
						case 'l':
							if (!(dundasSeriesType == "Polar"))
							{
								return;
							}
							series.Type = ChartTypes.Polar;
							series.Subtype = ChartSubtypes.Plain;
							return;
						case 'n':
							if (dundasSeriesType == "Range")
							{
								series.Type = ChartTypes.Range;
								series.Subtype = ChartSubtypes.Plain;
								return;
							}
							if (!(dundasSeriesType == "Gantt"))
							{
								return;
							}
							series.Type = ChartTypes.Range;
							series.Subtype = ChartSubtypes.Bar;
							return;
						case 'o':
							if (!(dundasSeriesType == "Stock"))
							{
								return;
							}
							series.Type = ChartTypes.Range;
							series.Subtype = ChartSubtypes.Stock;
							return;
						default:
							return;
						}
					}
					else
					{
						if (!(dundasSeriesType == "Radar"))
						{
							return;
						}
						series.Type = ChartTypes.Polar;
						series.Subtype = ChartSubtypes.Radar;
						return;
					}
					break;
				}
				case 6:
				{
					char c = dundasSeriesType[0];
					switch (c)
					{
					case 'B':
						if (!(dundasSeriesType == "Bubble"))
						{
							return;
						}
						series.Type = ChartTypes.Scatter;
						series.Subtype = ChartSubtypes.Bubble;
						return;
					case 'C':
						if (!(dundasSeriesType == "Column"))
						{
							return;
						}
						series.Type = ChartTypes.Column;
						series.Subtype = ChartSubtypes.Plain;
						return;
					case 'D':
					case 'E':
						return;
					case 'F':
						if (!(dundasSeriesType == "Funnel"))
						{
							return;
						}
						series.Type = ChartTypes.Shape;
						series.Subtype = ChartSubtypes.Funnel;
						return;
					default:
						if (c != 'S')
						{
							return;
						}
						if (!(dundasSeriesType == "Spline"))
						{
							return;
						}
						series.Type = ChartTypes.Line;
						series.Subtype = ChartSubtypes.Smooth;
						return;
					}
					break;
				}
				case 7:
				{
					char c = dundasSeriesType[0];
					if (c != 'B')
					{
						if (c != 'P')
						{
							return;
						}
						if (!(dundasSeriesType == "Pyramid"))
						{
							return;
						}
						series.Type = ChartTypes.Shape;
						series.Subtype = ChartSubtypes.Pyramid;
						return;
					}
					else
					{
						if (!(dundasSeriesType == "BoxPlot"))
						{
							return;
						}
						series.Type = ChartTypes.Range;
						series.Subtype = ChartSubtypes.BoxPlot;
						return;
					}
					break;
				}
				case 8:
				{
					char c = dundasSeriesType[0];
					switch (c)
					{
					case 'D':
						if (!(dundasSeriesType == "Doughnut"))
						{
							return;
						}
						series.Type = ChartTypes.Shape;
						series.Subtype = ChartSubtypes.Doughnut;
						return;
					case 'E':
						if (!(dundasSeriesType == "ErrorBar"))
						{
							return;
						}
						series.Type = ChartTypes.Range;
						series.Subtype = ChartSubtypes.ErrorBar;
						return;
					case 'F':
						if (!(dundasSeriesType == "FastLine"))
						{
							return;
						}
						break;
					default:
						if (c != 'S')
						{
							return;
						}
						if (!(dundasSeriesType == "StepLine"))
						{
							return;
						}
						series.Type = ChartTypes.Line;
						series.Subtype = ChartSubtypes.Stepped;
						return;
					}
					break;
				}
				case 9:
					if (!(dundasSeriesType == "FastPoint"))
					{
						return;
					}
					goto IL_04D6;
				case 10:
				{
					char c = dundasSeriesType[1];
					if (c != 'p')
					{
						if (c != 't')
						{
							return;
						}
						if (!(dundasSeriesType == "StackedBar"))
						{
							return;
						}
						series.Type = ChartTypes.Bar;
						series.Subtype = ChartSubtypes.Stacked;
						return;
					}
					else
					{
						if (!(dundasSeriesType == "SplineArea"))
						{
							return;
						}
						series.Type = ChartTypes.Area;
						series.Subtype = ChartSubtypes.Smooth;
						return;
					}
					break;
				}
				case 11:
					switch (dundasSeriesType[3])
					{
					case 'c':
						if (!(dundasSeriesType == "StackedArea"))
						{
							return;
						}
						series.Type = ChartTypes.Area;
						series.Subtype = ChartSubtypes.Stacked;
						return;
					case 'd':
						if (!(dundasSeriesType == "CandleStick"))
						{
							return;
						}
						series.Type = ChartTypes.Range;
						series.Subtype = ChartSubtypes.Candlestick;
						return;
					case 'e':
					case 'f':
					case 'h':
						return;
					case 'g':
						if (!(dundasSeriesType == "RangeColumn"))
						{
							return;
						}
						series.Type = ChartTypes.Range;
						series.Subtype = ChartSubtypes.Column;
						return;
					case 'i':
						if (!(dundasSeriesType == "SplineRange"))
						{
							return;
						}
						series.Type = ChartTypes.Range;
						series.Subtype = ChartSubtypes.Smooth;
						return;
					default:
						return;
					}
					break;
				case 12:
				case 15:
					return;
				case 13:
				{
					char c = dundasSeriesType[7];
					if (c != 'B')
					{
						if (c != 'C')
						{
							return;
						}
						if (!(dundasSeriesType == "StackedColumn"))
						{
							return;
						}
						series.Type = ChartTypes.Column;
						series.Subtype = ChartSubtypes.Stacked;
						return;
					}
					else
					{
						if (!(dundasSeriesType == "StackedBar100"))
						{
							return;
						}
						series.Type = ChartTypes.Bar;
						series.Subtype = ChartSubtypes.PercentStacked;
						return;
					}
					break;
				}
				case 14:
					if (!(dundasSeriesType == "StackedArea100"))
					{
						return;
					}
					series.Type = ChartTypes.Area;
					series.Subtype = ChartSubtypes.PercentStacked;
					return;
				case 16:
					if (!(dundasSeriesType == "StackedColumn100"))
					{
						return;
					}
					series.Type = ChartTypes.Column;
					series.Subtype = ChartSubtypes.PercentStacked;
					return;
				default:
					return;
				}
				series.Type = ChartTypes.Line;
				series.Subtype = ChartSubtypes.Plain;
				return;
				IL_04D6:
				series.Type = ChartTypes.Scatter;
				series.Subtype = ChartSubtypes.Plain;
				return;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
		private BackgroundImage ConvertDundasCRIChartBackgroundImageProperty(object imageReference, object transparentColor, object align, object mode, ref int counter)
		{
			int num = 0;
			BackgroundImage backgroundImage = new BackgroundImage();
			backgroundImage.Source = SourceType.External;
			backgroundImage.Value = this.ConvertDundasCRIStringProperty(imageReference, ref num);
			backgroundImage.TransparentColor = this.ConvertDundasCRIColorProperty(backgroundImage.TransparentColor, transparentColor, ref num);
			backgroundImage.Position = new ReportExpression<BackgroundPositions>(this.ConvertDundasCRIStringProperty(BackgroundPositions.TopLeft.ToString(), align, ref num), CultureInfo.InvariantCulture);
			string text = this.ConvertDundasCRIStringProperty(mode, ref num);
			if (!(text == "Tile") && !(text == "TileFlipX") && !(text == "TileFlipY") && !(text == "TileFlipXY"))
			{
				if (!(text == "Scaled"))
				{
					if (!(text == "Unscaled"))
					{
						if (text != string.Empty)
						{
							backgroundImage.BackgroundRepeat = new ReportExpression<BackgroundRepeatTypes>(text, CultureInfo.InvariantCulture);
						}
					}
					else
					{
						backgroundImage.BackgroundRepeat = BackgroundRepeatTypes.Clip;
					}
				}
				else
				{
					backgroundImage.BackgroundRepeat = BackgroundRepeatTypes.Fit;
				}
			}
			else
			{
				backgroundImage.BackgroundRepeat = BackgroundRepeatTypes.Repeat;
			}
			if (num > 0)
			{
				counter++;
				return backgroundImage;
			}
			return null;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000FD04 File Offset: 0x0000DF04
		private void FixChartAxisStriplineTitleAngle(Chart chart)
		{
			foreach (ChartArea chartArea in chart.ChartAreas)
			{
				bool flag = false;
				if (chart.ChartData != null && chart.ChartData.ChartSeriesCollection != null)
				{
					foreach (ChartSeries chartSeries in chart.ChartData.ChartSeriesCollection)
					{
						if (chartSeries.ChartAreaName == chartArea.Name && chartSeries.Type.Value == ChartTypes.Bar)
						{
							flag = true;
							break;
						}
					}
				}
				IList<ChartAxis> list = (flag ? chartArea.ChartValueAxes : chartArea.ChartCategoryAxes);
				IList<ChartAxis> list2 = (flag ? chartArea.ChartCategoryAxes : chartArea.ChartValueAxes);
				foreach (ChartAxis chartAxis in list)
				{
					if (chartAxis.ChartStripLines != null)
					{
						foreach (ChartStripLine chartStripLine in chartAxis.ChartStripLines)
						{
							if (!chartStripLine.TextOrientation.IsExpression)
							{
								TextOrientations value = chartStripLine.TextOrientation.Value;
								if (value - TextOrientations.Rotated90 > 1)
								{
									if (value != TextOrientations.Stacked)
									{
										chartStripLine.TextOrientation = TextOrientations.Auto;
									}
									else
									{
										chartStripLine.TextOrientation = TextOrientations.Rotated90;
									}
								}
								else
								{
									chartStripLine.TextOrientation = TextOrientations.Horizontal;
								}
							}
						}
					}
				}
				foreach (ChartAxis chartAxis2 in list2)
				{
					if (chartAxis2.ChartStripLines != null)
					{
						foreach (ChartStripLine chartStripLine2 in chartAxis2.ChartStripLines)
						{
							if (!chartStripLine2.TextOrientation.IsExpression)
							{
								TextOrientations value2 = chartStripLine2.TextOrientation.Value;
								if (value2 == TextOrientations.Horizontal || value2 == TextOrientations.Stacked)
								{
									chartStripLine2.TextOrientation = TextOrientations.Auto;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000FFDC File Offset: 0x0000E1DC
		private void UpgradeDundasCRIGaugePanel(CustomReportItem cri, GaugePanel gaugePanel)
		{
			gaugePanel.Name = cri.Name;
			gaugePanel.ActionInfo = cri.ActionInfo;
			gaugePanel.Bookmark = cri.Bookmark;
			gaugePanel.DataElementName = cri.DataElementName;
			gaugePanel.DataElementOutput = cri.DataElementOutput;
			gaugePanel.DocumentMapLabel = cri.DocumentMapLabel;
			gaugePanel.PropertyStore.SetObject(12, cri.PropertyStore.GetObject(12));
			gaugePanel.Height = cri.Height;
			gaugePanel.Left = cri.Left;
			gaugePanel.Parent = cri.Parent;
			gaugePanel.RepeatWith = cri.RepeatWith;
			gaugePanel.Style = cri.Style;
			gaugePanel.ToolTip = cri.ToolTip;
			gaugePanel.PropertyStore.SetObject(10, cri.PropertyStore.GetObject(10));
			gaugePanel.Top = cri.Top;
			gaugePanel.Visibility = cri.Visibility;
			gaugePanel.Width = cri.Width;
			gaugePanel.ZIndex = cri.ZIndex;
			if (cri.CustomData != null)
			{
				gaugePanel.DataSetName = cri.CustomData.DataSetName;
				gaugePanel.Filters = cri.CustomData.Filters;
			}
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			Hashtable hashtable3 = new Hashtable();
			List<Hashtable> list = new List<Hashtable>();
			List<Hashtable> list2 = new List<Hashtable>();
			List<Hashtable> list3 = new List<Hashtable>();
			foreach (CustomProperty customProperty in cri.CustomProperties)
			{
				string text = customProperty.Name.Value;
				if (text.StartsWith("expression:", StringComparison.OrdinalIgnoreCase))
				{
					text = text.Substring("expression:".Length);
				}
				if (!this.AddToPropertyList(list, "GaugeCore.Labels.", text, customProperty.Value) && !this.AddToPropertyList(list2, "GaugeCore.CircularGauges.", text, customProperty.Value) && !this.AddToPropertyList(list3, "GaugeCore.LinearGauges.", text, customProperty.Value))
				{
					hashtable.Add(text, customProperty.Value);
				}
				if (text.StartsWith("GAUGECORE.STATEINDICATORS.", StringComparison.OrdinalIgnoreCase) || text.StartsWith("GAUGECORE.NUMERICINDICATORS.", StringComparison.OrdinalIgnoreCase) || text.StartsWith("GAUGECORE.NAMEDIMAGES.", StringComparison.OrdinalIgnoreCase) || text.StartsWith("GAUGECORE.IMAGES.", StringComparison.OrdinalIgnoreCase))
				{
					base.UpgradeResults.HasUnsupportedDundasGaugeFeatures = true;
				}
			}
			if (hashtable["CUSTOM_CODE_CS"] != null || hashtable["CUSTOM_CODE_VB"] != null || hashtable["CUSTOM_CODE_COMPILED_ASSEMBLY"] != null)
			{
				if (this.m_throwUpgradeException)
				{
					throw new CRI2005UpgradeException();
				}
				base.UpgradeResults.HasUnsupportedDundasGaugeFeatures = true;
			}
			if (cri.CustomData != null && cri.CustomData.DataRowHierarchy != null && cri.CustomData.DataRowHierarchy.DataMembers != null && cri.CustomData.DataRowHierarchy.DataMembers.Count > 0)
			{
				foreach (CustomProperty customProperty2 in cri.CustomData.DataRowHierarchy.DataMembers[0].CustomProperties)
				{
					hashtable3.Add(customProperty2.Name, customProperty2.Value);
				}
			}
			if (cri.CustomData != null && cri.CustomData.DataRows != null && cri.CustomData.DataRows.Count > 0 && cri.CustomData.DataRows[0].Count > 0)
			{
				foreach (DataValue dataValue in cri.CustomData.DataRows[0][0])
				{
					hashtable2.Add(dataValue.Name, dataValue.Value);
				}
			}
			gaugePanel.ToolTip = this.ConvertDundasCRIStringProperty(hashtable["GaugeCore.ToolTip"]);
			gaugePanel.AntiAliasing = new ReportExpression<AntiAliasingTypes>(this.ConvertDundasCRIStringProperty(hashtable["GaugeCore.AntiAliasing"]), CultureInfo.InvariantCulture);
			gaugePanel.TextAntiAliasingQuality = new ReportExpression<TextAntiAliasingQualityTypes>(this.ConvertDundasCRIStringProperty(hashtable["GaugeCore.TextAntiAliasingQuality"]), CultureInfo.InvariantCulture);
			gaugePanel.ShadowIntensity = this.ConvertDundasCRIDoubleReportExpressionProperty(gaugePanel.ShadowIntensity, hashtable["GaugeCore.ShadowIntensity"]);
			bool? flag = this.ConvertDundasCRIBoolProperty(hashtable["GaugeCore.AutoLayout"]);
			if (flag != null)
			{
				gaugePanel.AutoLayout = flag.Value;
			}
			else
			{
				gaugePanel.AutoLayout = true;
			}
			gaugePanel.Style = this.ConvertDundasCRIStyleProperty(null, hashtable["GaugeCore.BackColor"] ?? Color.White, null, null, null, null, null, null, null, null, null, null, null);
			BackFrame backFrame = new BackFrame();
			if (this.UpgradeDundasCRIGaugeBackFrame(backFrame, hashtable, "GaugeCore.BackFrame."))
			{
				gaugePanel.BackFrame = backFrame;
			}
			foreach (Hashtable hashtable4 in list)
			{
				GaugeLabel gaugeLabel = new GaugeLabel();
				gaugePanel.GaugeLabels.Add(gaugeLabel);
				this.UpgradeDundasCRIGaugeLabel(gaugeLabel, hashtable4, "GaugeLabel.");
			}
			foreach (Hashtable hashtable5 in list2)
			{
				RadialGauge radialGauge = new RadialGauge();
				gaugePanel.RadialGauges.Add(radialGauge);
				this.UpgradeDundasCRIGaugeRadial(radialGauge, hashtable5, "CircularGauge.", hashtable3, hashtable2);
			}
			foreach (Hashtable hashtable6 in list3)
			{
				LinearGauge linearGauge = new LinearGauge();
				gaugePanel.LinearGauges.Add(linearGauge);
				this.UpgradeDundasCRIGaugeLinear(linearGauge, hashtable6, "LinearGauge.", hashtable3, hashtable2);
			}
			if (cri.CustomData != null && cri.CustomData.DataColumnHierarchy != null)
			{
				IList<DataMember> list4 = cri.CustomData.DataColumnHierarchy.DataMembers;
				GaugeMember gaugeMember = null;
				while (list4 != null && list4.Count > 0)
				{
					DataMember dataMember = list4[0];
					if (((DataGrouping2005)dataMember).Static)
					{
						break;
					}
					if (gaugeMember == null)
					{
						gaugeMember = (gaugePanel.GaugeMember = new GaugeMember());
					}
					else
					{
						gaugeMember = (gaugeMember.ChildGaugeMember = new GaugeMember());
					}
					gaugeMember.SortExpressions = dataMember.SortExpressions;
					if (dataMember.Group != null)
					{
						gaugeMember.Group = dataMember.Group;
					}
					list4 = dataMember.DataMembers;
				}
			}
			this.FixGaugeElementNames(gaugePanel);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000106C4 File Offset: 0x0000E8C4
		private bool UpgradeDundasCRIGaugeBackFrame(BackFrame backFrame, Hashtable backFrameProperties, string propertyPrefix)
		{
			int num = 0;
			string text = this.ConvertDundasCRIStringProperty(backFrameProperties[propertyPrefix + "FrameStyle"], ref num);
			if (string.IsNullOrEmpty(text))
			{
				backFrame.FrameStyle = new ReportExpression<FrameStyles>(this.ConvertDundasCRIStringProperty(backFrameProperties[propertyPrefix + "Style"], ref num), CultureInfo.InvariantCulture);
			}
			else
			{
				backFrame.FrameStyle = new ReportExpression<FrameStyles>(text, CultureInfo.InvariantCulture);
			}
			string text2 = this.ConvertDundasCRIStringProperty(backFrameProperties[propertyPrefix + "FrameShape"], ref num);
			if (string.IsNullOrEmpty(text2))
			{
				backFrame.FrameShape = new ReportExpression<FrameShapes>(this.ConvertDundasCRIStringProperty(backFrameProperties[propertyPrefix + "Shape"], ref num), CultureInfo.InvariantCulture);
			}
			else
			{
				backFrame.FrameShape = new ReportExpression<FrameShapes>(text2, CultureInfo.InvariantCulture);
			}
			backFrame.FrameWidth = this.ConvertDundasCRIDoubleReportExpressionProperty(backFrame.FrameWidth, backFrameProperties[propertyPrefix + "FrameWidth"], ref num);
			backFrame.GlassEffect = new ReportExpression<GlassEffects>(this.ConvertDundasCRIStringProperty(backFrameProperties[propertyPrefix + "GlassEffect"], ref num), CultureInfo.InvariantCulture);
			backFrame.Style = this.ConvertDundasCRIStyleProperty(null, backFrameProperties[propertyPrefix + "FrameColor"] ?? Color.Gainsboro, backFrameProperties[propertyPrefix + "FrameGradientType"] ?? BackgroundGradients.DiagonalLeft, backFrameProperties[propertyPrefix + "FrameGradientEndColor"] ?? Color.Gray, backFrameProperties[propertyPrefix + "FrameHatchStyle"], backFrameProperties[propertyPrefix + "ShadowOffset"], backFrameProperties[propertyPrefix + "BorderColor"], backFrameProperties[propertyPrefix + "BorderStyle"], backFrameProperties[propertyPrefix + "BorderWidth"], null, null, null, null, ref num);
			int num2 = 0;
			Style style = this.ConvertDundasCRIStyleProperty(null, backFrameProperties[propertyPrefix + "BackColor"] ?? Color.Silver, backFrameProperties[propertyPrefix + "BackGradientType"] ?? BackgroundGradients.DiagonalLeft, backFrameProperties[propertyPrefix + "BackGradientEndColor"] ?? Color.Gray, backFrameProperties[propertyPrefix + "BackHatchStyle"], null, null, null, null, null, null, null, null, ref num2);
			if (num2 > 0)
			{
				backFrame.FrameBackground = new FrameBackground();
				backFrame.FrameBackground.Style = style;
				num++;
			}
			return num > 0;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00010944 File Offset: 0x0000EB44
		private void UpgradeDundasCRIGaugeLabel(GaugeLabel label, Hashtable labelProperties, string propertyPrefix)
		{
			label.Name = this.ConvertDundasCRIStringProperty(labelProperties[propertyPrefix + "Name"]);
			label.ParentItem = this.ConvertDundasCRIStringProperty(labelProperties[propertyPrefix + "Parent"]);
			label.ZIndex = this.ConvertDundasCRIIntegerReportExpressionProperty(labelProperties[propertyPrefix + "ZOrder"]);
			label.Left = this.ConvertDundasCRIDoubleReportExpressionProperty(labelProperties[propertyPrefix + "Location.X"]);
			label.Top = this.ConvertDundasCRIDoubleReportExpressionProperty(labelProperties[propertyPrefix + "Location.Y"]);
			label.Width = this.ConvertDundasCRIDoubleReportExpressionProperty(labelProperties[propertyPrefix + "Size.Width"]);
			label.Height = this.ConvertDundasCRIDoubleReportExpressionProperty(labelProperties[propertyPrefix + "Size.Height"]);
			label.ResizeMode = new ReportExpression<ResizeModes>(this.ConvertDundasCRIStringProperty(labelProperties[propertyPrefix + "ResizeMode"]), CultureInfo.InvariantCulture);
			label.Text = this.ConvertDundasCRIStringProperty("Text", labelProperties[propertyPrefix + "Text"]);
			label.TextShadowOffset = this.ConvertDundasCRIPixelReportSizeProperty(labelProperties[propertyPrefix + "TextShadowOffset"]);
			label.Angle = this.ConvertDundasCRIDoubleReportExpressionProperty(labelProperties[propertyPrefix + "Angle"]);
			bool? flag = this.ConvertDundasCRIBoolProperty(labelProperties[propertyPrefix + "Visible"]);
			if (flag != null)
			{
				label.Hidden = !flag.Value;
			}
			if (this.ConvertDundasCRIStringProperty("Default", labelProperties[propertyPrefix + "FontUnit"]) == "Percent")
			{
				label.UseFontPercent = true;
			}
			string text = this.ConvertDundasCRIStringProperty(labelProperties[propertyPrefix + "TextAlignment"]);
			TextAlignments textAlignments = ((string.IsNullOrEmpty(text) || text.EndsWith("LEFT", StringComparison.OrdinalIgnoreCase)) ? TextAlignments.Left : (text.EndsWith("CENTER", StringComparison.OrdinalIgnoreCase) ? TextAlignments.Center : TextAlignments.Right));
			VerticalAlignments verticalAlignments = ((string.IsNullOrEmpty(text) || text.StartsWith("TOP", StringComparison.OrdinalIgnoreCase)) ? VerticalAlignments.Top : (text.StartsWith("MIDDLE", StringComparison.OrdinalIgnoreCase) ? VerticalAlignments.Middle : VerticalAlignments.Bottom));
			label.Style = this.ConvertDundasCRIStyleProperty(labelProperties[propertyPrefix + "TextColor"], labelProperties[propertyPrefix + "BackColor"], labelProperties[propertyPrefix + "BackGradientType"], labelProperties[propertyPrefix + "BackGradientEndColor"], labelProperties[propertyPrefix + "BackHatchStyle"], labelProperties[propertyPrefix + "BackShadowOffset"], labelProperties[propertyPrefix + "BorderColor"], labelProperties[propertyPrefix + "BorderStyle"], labelProperties[propertyPrefix + "BorderWidth"], labelProperties.ContainsKey(propertyPrefix + "Font") ? labelProperties[propertyPrefix + "Font"] : "Microsoft Sans Serif, 8.25pt", null, textAlignments, verticalAlignments);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010C5C File Offset: 0x0000EE5C
		private void UpgradeDundasCRIGauge(Gauge gauge, Hashtable gaugeProperties, string propertyPrefix, Hashtable formulaProperties, Hashtable dataValueProperties)
		{
			string text = this.ConvertDundasCRIStringProperty(gaugeProperties[propertyPrefix + "Name"]);
			gauge.Name = text;
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			string text2 = ((gauge is LinearGauge) ? "LinearGauge" : "CircularGauge") + ":" + text;
			foreach (object obj in formulaProperties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (dictionaryEntry.Key.ToString().StartsWith(text2, StringComparison.Ordinal))
				{
					hashtable.Add(dictionaryEntry.Key.ToString().Remove(0, text2.Length + 1), dictionaryEntry.Value);
				}
			}
			foreach (object obj2 in dataValueProperties)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				if (dictionaryEntry2.Key.ToString().StartsWith(text2, StringComparison.Ordinal))
				{
					hashtable2.Add(dictionaryEntry2.Key.ToString().Remove(0, text2.Length + 1), dictionaryEntry2.Value);
				}
			}
			gauge.ActionInfo = this.UpgradeDundasCRIGaugeActionInfo(formulaProperties, text2 + ":");
			gauge.ParentItem = this.ConvertDundasCRIStringProperty(gaugeProperties[propertyPrefix + "Parent"]);
			gauge.ZIndex = this.ConvertDundasCRIIntegerReportExpressionProperty(gaugeProperties[propertyPrefix + "ZOrder"]);
			gauge.Left = this.ConvertDundasCRIDoubleReportExpressionProperty(gaugeProperties[propertyPrefix + "Location.X"]);
			gauge.Top = this.ConvertDundasCRIDoubleReportExpressionProperty(gaugeProperties[propertyPrefix + "Location.Y"]);
			gauge.Width = this.ConvertDundasCRIDoubleReportExpressionProperty(gaugeProperties[propertyPrefix + "Size.Width"]);
			gauge.Height = this.ConvertDundasCRIDoubleReportExpressionProperty(gaugeProperties[propertyPrefix + "Size.Height"]);
			bool? flag = this.ConvertDundasCRIBoolProperty(gaugeProperties[propertyPrefix + "Visible"]);
			if (flag != null)
			{
				gauge.Hidden = !flag.Value;
			}
			bool? flag2 = this.ConvertDundasCRIBoolProperty(gaugeProperties[propertyPrefix + "ClipContent"]);
			if (flag2 != null)
			{
				gauge.ClipContent = flag2.Value;
			}
			else
			{
				gauge.ClipContent = true;
			}
			BackFrame backFrame = new BackFrame();
			if (this.UpgradeDundasCRIGaugeBackFrame(backFrame, gaugeProperties, propertyPrefix + "BackFrame."))
			{
				gauge.BackFrame = backFrame;
			}
			List<Hashtable> list = new List<Hashtable>();
			List<Hashtable> list2 = new List<Hashtable>();
			List<Hashtable> list3 = new List<Hashtable>();
			foreach (object obj3 in gaugeProperties)
			{
				DictionaryEntry dictionaryEntry3 = (DictionaryEntry)obj3;
				string text3 = dictionaryEntry3.Key.ToString();
				string text4 = dictionaryEntry3.Value.ToString();
				this.AddToPropertyList(list, propertyPrefix + "Scales.", text3, text4);
				this.AddToPropertyList(list2, propertyPrefix + "Ranges.", text3, text4);
				this.AddToPropertyList(list3, propertyPrefix + "Pointers.", text3, text4);
			}
			Hashtable hashtable3 = new Hashtable();
			foreach (Hashtable hashtable4 in list)
			{
				if (gauge is LinearGauge)
				{
					LinearScale linearScale = new LinearScale();
					this.UpgradeDundasCRIGaugeScaleLinear(linearScale, hashtable4, "LinearScale.", hashtable, hashtable2);
					gauge.GaugeScales.Add(linearScale);
					hashtable3.Add(linearScale.Name, linearScale);
				}
				else
				{
					RadialScale radialScale = new RadialScale();
					this.UpgradeDundasCRIGaugeScaleRadial(radialScale, hashtable4, "CircularScale.", hashtable, hashtable2);
					gauge.GaugeScales.Add(radialScale);
					hashtable3.Add(radialScale.Name, radialScale);
				}
			}
			string text5 = ((gauge.GaugeScales.Count > 0) ? gauge.GaugeScales[0].Name : "Default");
			foreach (Hashtable hashtable5 in list2)
			{
				ScaleRange scaleRange = new ScaleRange();
				string text6;
				if (gauge is LinearGauge)
				{
					this.UpgradeDundasCRIGaugeScaleRange(scaleRange, hashtable5, "LinearRange.", true, hashtable, hashtable2);
					text6 = this.ConvertDundasCRIStringProperty(text5, hashtable5["LinearRange.ScaleName"]);
				}
				else
				{
					this.UpgradeDundasCRIGaugeScaleRange(scaleRange, hashtable5, "CircularRange.", false, hashtable, hashtable2);
					text6 = this.ConvertDundasCRIStringProperty(text5, hashtable5["CircularRange.ScaleName"]);
				}
				if (hashtable3.Contains(text6))
				{
					((GaugeScale)hashtable3[text6]).ScaleRanges.Add(scaleRange);
				}
			}
			foreach (Hashtable hashtable6 in list3)
			{
				GaugePointer gaugePointer;
				string text7;
				if (gauge is LinearGauge)
				{
					gaugePointer = new LinearPointer();
					this.UpgradeDundasCRIGaugePointerLinear((LinearPointer)gaugePointer, hashtable6, "LinearPointer.", hashtable, hashtable2);
					text7 = this.ConvertDundasCRIStringProperty(text5, hashtable6["LinearPointer.ScaleName"]);
				}
				else
				{
					gaugePointer = new RadialPointer();
					this.UpgradeDundasCRIGaugePointerRadial((RadialPointer)gaugePointer, hashtable6, "CircularPointer.", hashtable, hashtable2);
					text7 = this.ConvertDundasCRIStringProperty(text5, hashtable6["CircularPointer.ScaleName"]);
				}
				if (hashtable3.Contains(text7))
				{
					((GaugeScale)hashtable3[text7]).GaugePointers.Add(gaugePointer);
				}
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00011274 File Offset: 0x0000F474
		private void UpgradeDundasCRIGaugeRadial(RadialGauge gauge, Hashtable gaugeProperties, string propertyPrefix, Hashtable formulaProperties, Hashtable dataValueProperties)
		{
			this.UpgradeDundasCRIGauge(gauge, gaugeProperties, propertyPrefix, formulaProperties, dataValueProperties);
			gauge.PivotX = this.ConvertDundasCRIDoubleReportExpressionProperty(gauge.PivotX, gaugeProperties[propertyPrefix + "PivotPoint.X"]);
			gauge.PivotY = this.ConvertDundasCRIDoubleReportExpressionProperty(gauge.PivotY, gaugeProperties[propertyPrefix + "PivotPoint.Y"]);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000112D4 File Offset: 0x0000F4D4
		private void UpgradeDundasCRIGaugeLinear(LinearGauge gauge, Hashtable gaugeProperties, string propertyPrefix, Hashtable formulaCustomProperties, Hashtable dataValueCustomProperties)
		{
			this.UpgradeDundasCRIGauge(gauge, gaugeProperties, propertyPrefix, formulaCustomProperties, dataValueCustomProperties);
			gauge.Orientation = new ReportExpression<Orientations>(this.ConvertDundasCRIStringProperty(gaugeProperties[propertyPrefix + "Orientation"]), CultureInfo.InvariantCulture);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001130C File Offset: 0x0000F50C
		private void UpgradeDundasCRIGaugeScale(GaugeScale scale, Hashtable scaleProperties, string propertyPrefix, Hashtable formulaProperties, Hashtable dataValueProperties)
		{
			string text = this.ConvertDundasCRIStringProperty(scaleProperties[propertyPrefix + "Name"]);
			scale.Name = text;
			string text2 = ((scale is LinearScale) ? "LinearScale" : "CircularScale") + ":" + text + ":";
			scale.MinimumValue = this.UpgradeDundasCRIGaugeInputValue(formulaProperties, dataValueProperties, text2 + "Minimum", scaleProperties, propertyPrefix + "Minimum");
			scale.MaximumValue = this.UpgradeDundasCRIGaugeInputValue(formulaProperties, dataValueProperties, text2 + "Maximum", scaleProperties, propertyPrefix + "Maximum");
			scale.Multiplier = this.ConvertDundasCRIDoubleReportExpressionProperty(scale.Multiplier, scaleProperties[propertyPrefix + "Multiplier"]);
			scale.Interval = this.ConvertDundasCRIDoubleReportExpressionProperty(scaleProperties[propertyPrefix + "Interval"]);
			scale.IntervalOffset = this.ConvertDundasCRIDoubleReportExpressionProperty(double.NaN, scaleProperties[propertyPrefix + "IntervalOffset"]);
			scale.LogarithmicBase = this.ConvertDundasCRIDoubleReportExpressionProperty(scale.LogarithmicBase, scaleProperties[propertyPrefix + "LogarithmicBase"]);
			scale.Width = this.ConvertDundasCRIDoubleReportExpressionProperty(scale.Width, scaleProperties[propertyPrefix + "Width"]);
			scale.Style = this.ConvertDundasCRIStyleProperty(null, scaleProperties[propertyPrefix + "FillColor"] ?? Color.CornflowerBlue, scaleProperties[propertyPrefix + "FillGradientType"], scaleProperties[propertyPrefix + "FillGradientEndColor"] ?? Color.White, scaleProperties[propertyPrefix + "FillHatchStyle"], scaleProperties[propertyPrefix + "ShadowOffset"] ?? 1, scaleProperties[propertyPrefix + "BorderColor"], scaleProperties[propertyPrefix + "BorderStyle"], scaleProperties[propertyPrefix + "BorderWidth"], null, null, null, null);
			bool? flag = this.ConvertDundasCRIBoolProperty(scaleProperties[propertyPrefix + "Visible"]);
			if (flag != null)
			{
				scale.Hidden = !flag.Value;
			}
			bool? flag2 = this.ConvertDundasCRIBoolProperty(scaleProperties[propertyPrefix + "TickMarksOnTop"]);
			if (flag2 != null)
			{
				scale.TickMarksOnTop = flag2.Value;
			}
			bool? flag3 = this.ConvertDundasCRIBoolProperty(scaleProperties[propertyPrefix + "Reversed"]);
			if (flag3 != null)
			{
				scale.Reversed = flag3.Value;
			}
			bool? flag4 = this.ConvertDundasCRIBoolProperty(scaleProperties[propertyPrefix + "Logarithmic"]);
			if (flag4 != null)
			{
				scale.Logarithmic = flag4.Value;
			}
			ScalePin scalePin = new ScalePin();
			if (this.UpgradeDundasCRIGaugeScalePin(scalePin, scaleProperties, propertyPrefix + "MinimumPin.", 6.0, 6.0))
			{
				scale.MinimumPin = scalePin;
			}
			ScalePin scalePin2 = new ScalePin();
			if (this.UpgradeDundasCRIGaugeScalePin(scalePin2, scaleProperties, propertyPrefix + "MaximumPin.", 6.0, 6.0))
			{
				scale.MaximumPin = scalePin2;
			}
			int num = 0;
			ScaleLabels scaleLabels = new ScaleLabels();
			scaleLabels.Placement = new ReportExpression<Placements>(this.ConvertDundasCRIStringProperty(Placements.Inside.ToString(), scaleProperties[propertyPrefix + "LabelStyle.Placement"], ref num), CultureInfo.InvariantCulture);
			scaleLabels.Interval = this.ConvertDundasCRIDoubleReportExpressionProperty(scaleProperties[propertyPrefix + "LabelStyle.Interval"], ref num);
			scaleLabels.IntervalOffset = this.ConvertDundasCRIDoubleReportExpressionProperty(double.NaN, scaleProperties[propertyPrefix + "LabelStyle.IntervalOffset"], ref num);
			scaleLabels.FontAngle = this.ConvertDundasCRIDoubleReportExpressionProperty(scaleProperties[propertyPrefix + "LabelStyle.FontAngle"], ref num);
			scaleLabels.DistanceFromScale = this.ConvertDundasCRIDoubleReportExpressionProperty(scaleLabels.DistanceFromScale, scaleProperties[propertyPrefix + "LabelStyle.DistanceFromScale"], ref num);
			bool? flag5 = this.ConvertDundasCRIBoolProperty(scaleProperties[propertyPrefix + "LabelStyle.Visible"], ref num);
			if (flag5 != null)
			{
				scaleLabels.Hidden = !flag5.Value;
			}
			bool? flag6 = this.ConvertDundasCRIBoolProperty(scaleProperties[propertyPrefix + "LabelStyle.AllowUpsideDown"], ref num);
			if (flag6 != null)
			{
				scaleLabels.AllowUpsideDown = flag6.Value;
			}
			bool? flag7 = this.ConvertDundasCRIBoolProperty(scaleProperties[propertyPrefix + "LabelStyle.ShowEndLabels"], ref num);
			if (flag7 != null)
			{
				scaleLabels.ShowEndLabels = flag7.Value;
			}
			else
			{
				scaleLabels.ShowEndLabels = true;
			}
			if (this.ConvertDundasCRIStringProperty("Percent", scaleProperties[propertyPrefix + "LabelStyle.FontUnit"], ref num) == "Percent")
			{
				scaleLabels.UseFontPercent = true;
			}
			scaleLabels.Style = this.ConvertDundasCRIStyleProperty(scaleProperties[propertyPrefix + "LabelStyle.TextColor"], null, null, null, null, null, null, null, null, scaleProperties.ContainsKey(propertyPrefix + "LabelStyle.Font") ? scaleProperties[propertyPrefix + "LabelStyle.Font"] : "Microsoft Sans Serif, 14pt", scaleProperties[propertyPrefix + "LabelStyle.FormatString"], null, null, ref num);
			if (num > 0)
			{
				scale.ScaleLabels = scaleLabels;
			}
			List<Hashtable> list = new List<Hashtable>();
			foreach (object obj in scaleProperties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.AddToPropertyList(list, propertyPrefix + "CustomLabels.", dictionaryEntry.Key.ToString(), dictionaryEntry.Value.ToString());
			}
			foreach (Hashtable hashtable in list)
			{
				CustomLabel customLabel = new CustomLabel();
				this.UpgradeDundasCRIGaugeCustomLabel(customLabel, hashtable, "CustomLabel.");
				scale.CustomLabels.Add(customLabel);
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00011984 File Offset: 0x0000FB84
		private void UpgradeDundasCRIGaugeScaleRadial(RadialScale scale, Hashtable scaleProperties, string propertyPrefix, Hashtable formulaProperties, Hashtable dataValueProperties)
		{
			this.UpgradeDundasCRIGaugeScale(scale, scaleProperties, propertyPrefix, formulaProperties, dataValueProperties);
			scale.Radius = this.ConvertDundasCRIDoubleReportExpressionProperty(scale.Radius, scaleProperties[propertyPrefix + "Radius"]);
			scale.StartAngle = this.ConvertDundasCRIDoubleReportExpressionProperty(scale.StartAngle, scaleProperties[propertyPrefix + "StartAngle"]);
			scale.SweepAngle = this.ConvertDundasCRIDoubleReportExpressionProperty(scale.SweepAngle, scaleProperties[propertyPrefix + "SweepAngle"]);
			bool? flag = this.ConvertDundasCRIBoolProperty(scaleProperties[propertyPrefix + "LabelStyle.RotateLabels"]);
			if (flag == null)
			{
				flag = new bool?(true);
			}
			if (flag.Value)
			{
				if (scale.ScaleLabels == null)
				{
					scale.ScaleLabels = new ScaleLabels();
				}
				scale.ScaleLabels.RotateLabels = true;
			}
			GaugeTickMarks gaugeTickMarks = new GaugeTickMarks();
			if (this.UpgradeDundasCRIGaugeTickMarks(gaugeTickMarks, scaleProperties, propertyPrefix + "MajorTickMark.", 8.0, 14.0, MarkerStyles.Trapezoid))
			{
				scale.GaugeMajorTickMarks = gaugeTickMarks;
			}
			GaugeTickMarks gaugeTickMarks2 = new GaugeTickMarks();
			if (this.UpgradeDundasCRIGaugeTickMarks(gaugeTickMarks2, scaleProperties, propertyPrefix + "MinorTickMark.", 3.0, 8.0, MarkerStyles.Rectangle))
			{
				scale.GaugeMinorTickMarks = gaugeTickMarks2;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00011ADC File Offset: 0x0000FCDC
		private void UpgradeDundasCRIGaugeScaleLinear(LinearScale scale, Hashtable scaleProperties, string propertyPrefix, Hashtable formulaProperties, Hashtable dataValueProperties)
		{
			this.UpgradeDundasCRIGaugeScale(scale, scaleProperties, propertyPrefix, formulaProperties, dataValueProperties);
			scale.StartMargin = this.ConvertDundasCRIDoubleReportExpressionProperty(scale.StartMargin, scaleProperties[propertyPrefix + "StartMargin"]);
			scale.EndMargin = this.ConvertDundasCRIDoubleReportExpressionProperty(scale.EndMargin, scaleProperties[propertyPrefix + "EndMargin"]);
			scale.Position = this.ConvertDundasCRIDoubleReportExpressionProperty(scale.Position, scaleProperties[propertyPrefix + "Position"]);
			GaugeTickMarks gaugeTickMarks = new GaugeTickMarks();
			if (this.UpgradeDundasCRIGaugeTickMarks(gaugeTickMarks, scaleProperties, propertyPrefix + "MajorTickMark.", 4.0, 15.0, MarkerStyles.Rectangle))
			{
				scale.GaugeMajorTickMarks = gaugeTickMarks;
			}
			GaugeTickMarks gaugeTickMarks2 = new GaugeTickMarks();
			if (this.UpgradeDundasCRIGaugeTickMarks(gaugeTickMarks2, scaleProperties, propertyPrefix + "MinorTickMark.", 3.0, 9.0, MarkerStyles.Rectangle))
			{
				scale.GaugeMinorTickMarks = gaugeTickMarks2;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		private void UpgradeDundasCRIGaugeScaleRange(ScaleRange range, Hashtable rangeProperties, string propertyPrefix, bool isLinear, Hashtable formulaProperties, Hashtable dataValueProperties)
		{
			string text = this.ConvertDundasCRIStringProperty(rangeProperties[propertyPrefix + "Name"]);
			range.Name = text;
			string text2 = (isLinear ? "LinearRange" : "CircularRange") + ":" + text + ":";
			range.StartValue = this.UpgradeDundasCRIGaugeInputValue(formulaProperties, dataValueProperties, text2 + "StartValue", rangeProperties, propertyPrefix + "StartValue");
			range.EndValue = this.UpgradeDundasCRIGaugeInputValue(formulaProperties, dataValueProperties, text2 + "EndValue", rangeProperties, propertyPrefix + "EndValue");
			range.StartWidth = this.ConvertDundasCRIDoubleReportExpressionProperty(isLinear ? 10.0 : 15.0, rangeProperties[propertyPrefix + "StartWidth"]);
			range.EndWidth = this.ConvertDundasCRIDoubleReportExpressionProperty(isLinear ? 10.0 : 30.0, rangeProperties[propertyPrefix + "EndWidth"]);
			range.DistanceFromScale = this.ConvertDundasCRIDoubleReportExpressionProperty(isLinear ? 10.0 : 30.0, rangeProperties[propertyPrefix + "DistanceFromScale"]);
			range.Placement = new ReportExpression<Placements>(this.ConvertDundasCRIStringProperty(isLinear ? Placements.Outside.ToString() : Placements.Inside.ToString(), rangeProperties[propertyPrefix + "Placement"]), CultureInfo.InvariantCulture);
			range.InRangeTickMarksColor = this.ConvertDundasCRIColorProperty(range.InRangeTickMarksColor, rangeProperties[propertyPrefix + "InRangeTickMarkColor"]);
			range.InRangeLabelColor = this.ConvertDundasCRIColorProperty(range.InRangeLabelColor, rangeProperties[propertyPrefix + "InRangeLabelColor"]);
			range.InRangeBarPointerColor = this.ConvertDundasCRIColorProperty(range.InRangeBarPointerColor, rangeProperties[propertyPrefix + "InRangeBarPointerColor"]);
			range.BackgroundGradientType = new ReportExpression<GaugeBackgroundGradients>(this.ConvertDundasCRIStringProperty(rangeProperties[propertyPrefix + "FillGradientType"]), CultureInfo.InvariantCulture);
			bool? flag = this.ConvertDundasCRIBoolProperty(rangeProperties[propertyPrefix + "Visible"]);
			if (flag != null)
			{
				range.Hidden = !flag.Value;
			}
			range.ActionInfo = this.ConvertDundasCRIActionInfoProperty(rangeProperties[propertyPrefix + "Href"]);
			range.Style = this.ConvertDundasCRIStyleProperty(null, rangeProperties[propertyPrefix + "FillColor"] ?? Color.Lime, null, rangeProperties[propertyPrefix + "FillGradientEndColor"] ?? Color.Red, rangeProperties[propertyPrefix + "FillHatchStyle"], rangeProperties[propertyPrefix + "ShadowOffset"], rangeProperties[propertyPrefix + "BorderColor"], rangeProperties[propertyPrefix + "BorderStyle"], rangeProperties[propertyPrefix + "BorderWidth"], null, null, null, null);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00011EFC File Offset: 0x000100FC
		private bool UpgradeDundasCRIGaugeTickMarkStyle(TickMarkStyle tickMarkStyle, Hashtable properties, string propertyPrefix, ReportExpression<double> defaultWidth, ReportExpression<double> defaultLength, MarkerStyles? defaultShape)
		{
			int num = 0;
			tickMarkStyle.Shape = new ReportExpression<MarkerStyles>(this.ConvertDundasCRIStringProperty((defaultShape != null) ? defaultShape.Value.ToString() : string.Empty, properties[propertyPrefix + "Shape"], ref num), CultureInfo.InvariantCulture);
			tickMarkStyle.Placement = new ReportExpression<Placements>(this.ConvertDundasCRIStringProperty(Placements.Cross.ToString(), properties[propertyPrefix + "Placement"], ref num), CultureInfo.InvariantCulture);
			tickMarkStyle.GradientDensity = this.ConvertDundasCRIDoubleReportExpressionProperty(tickMarkStyle.GradientDensity, properties[propertyPrefix + "GradientDensity"], ref num);
			tickMarkStyle.DistanceFromScale = this.ConvertDundasCRIDoubleReportExpressionProperty(properties[propertyPrefix + "DistanceFromScale"], ref num);
			tickMarkStyle.Width = this.ConvertDundasCRIDoubleReportExpressionProperty(defaultWidth, properties[propertyPrefix + "Width"], ref num);
			tickMarkStyle.Length = this.ConvertDundasCRIDoubleReportExpressionProperty(defaultLength, properties[propertyPrefix + "Length"], ref num);
			bool? flag = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "Visible"], ref num);
			if (flag != null)
			{
				tickMarkStyle.Hidden = !flag.Value;
			}
			bool? flag2 = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "EnableGradient"], ref num);
			if (flag2 != null)
			{
				tickMarkStyle.EnableGradient = flag2.Value;
			}
			else
			{
				tickMarkStyle.EnableGradient = true;
			}
			tickMarkStyle.Style = this.ConvertDundasCRIStyleProperty(null, properties[propertyPrefix + "FillColor"] ?? Color.WhiteSmoke, null, null, null, null, properties[propertyPrefix + "BorderColor"] ?? Color.DimGray, null, properties[propertyPrefix + "BorderWidth"], null, null, null, null, ref num);
			return num > 0;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00012100 File Offset: 0x00010300
		private bool UpgradeDundasCRIGaugeTickMarks(GaugeTickMarks tickMarks, Hashtable properties, string propertyPrefix, ReportExpression<double> defaultWidth, ReportExpression<double> defaultLength, MarkerStyles defaultShape)
		{
			int num = 0;
			if (this.UpgradeDundasCRIGaugeTickMarkStyle(tickMarks, properties, propertyPrefix, defaultWidth, defaultLength, new MarkerStyles?(defaultShape)))
			{
				num++;
			}
			tickMarks.Interval = this.ConvertDundasCRIDoubleReportExpressionProperty(properties[propertyPrefix + "Interval"], ref num);
			tickMarks.IntervalOffset = this.ConvertDundasCRIDoubleReportExpressionProperty(double.NaN, properties[propertyPrefix + "IntervalOffset"], ref num);
			return num > 0;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001217C File Offset: 0x0001037C
		private bool UpgradeDundasCRIGaugeScalePin(ScalePin pin, Hashtable properties, string propertyPrefix, ReportExpression<double> defaultWidth, ReportExpression<double> defaultLength)
		{
			int num = 0;
			if (this.UpgradeDundasCRIGaugeTickMarkStyle(pin, properties, propertyPrefix, defaultWidth, defaultLength, new MarkerStyles?(MarkerStyles.Circle)))
			{
				num++;
			}
			pin.Location = this.ConvertDundasCRIDoubleReportExpressionProperty(pin.Location, properties[propertyPrefix + "Location"], ref num);
			bool? flag = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "Enable"], ref num);
			if (flag != null)
			{
				pin.Enable = flag.Value;
			}
			int num2 = 0;
			PinLabel pinLabel = new PinLabel();
			pinLabel.Placement = new ReportExpression<Placements>(this.ConvertDundasCRIStringProperty(Placements.Inside.ToString(), properties[propertyPrefix + "LabelStyle.Placement"], ref num2), CultureInfo.InvariantCulture);
			pinLabel.Text = this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "LabelStyle.Text"], ref num2);
			pinLabel.FontAngle = this.ConvertDundasCRIDoubleReportExpressionProperty(properties[propertyPrefix + "LabelStyle.FontAngle"], ref num2);
			pinLabel.DistanceFromScale = this.ConvertDundasCRIDoubleReportExpressionProperty(pinLabel.DistanceFromScale, properties[propertyPrefix + "LabelStyle.DistanceFromScale"], ref num2);
			bool? flag2 = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "LabelStyle.RotateLabel"], ref num2);
			if (flag2 != null)
			{
				pinLabel.RotateLabel = flag2.Value;
			}
			bool? flag3 = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "LabelStyle.AllowUpsideDown"], ref num2);
			if (flag3 != null)
			{
				pinLabel.AllowUpsideDown = flag3.Value;
			}
			if (this.ConvertDundasCRIStringProperty("Percent", properties[propertyPrefix + "LabelStyle.FontUnit"], ref num2) == "Percent")
			{
				pinLabel.UseFontPercent = true;
			}
			pinLabel.Style = this.ConvertDundasCRIStyleProperty(properties[propertyPrefix + "LabelStyle.TextColor"], null, null, null, null, null, null, null, null, properties.ContainsKey(propertyPrefix + "LabelStyle.Font") ? properties[propertyPrefix + "LabelStyle.Font"] : "Microsoft Sans Serif, 12pt", null, null, null, ref num2);
			if (num2 > 0)
			{
				pin.PinLabel = pinLabel;
				num++;
			}
			return num > 0;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000123B4 File Offset: 0x000105B4
		private void UpgradeDundasCRIGaugeCustomLabel(CustomLabel customLabel, Hashtable properties, string propertyPrefix)
		{
			customLabel.Name = this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "Name"]);
			customLabel.Text = this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "Text"]);
			customLabel.Value = this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "Value"]);
			customLabel.Placement = new ReportExpression<Placements>(this.ConvertDundasCRIStringProperty(Placements.Inside.ToString(), properties[propertyPrefix + "Placement"]), CultureInfo.InvariantCulture);
			customLabel.FontAngle = this.ConvertDundasCRIDoubleReportExpressionProperty(properties[propertyPrefix + "FontAngle"]);
			customLabel.DistanceFromScale = this.ConvertDundasCRIDoubleReportExpressionProperty(properties[propertyPrefix + "DistanceFromScale"]);
			bool? flag = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "Visible"]);
			if (flag != null)
			{
				customLabel.Hidden = !flag.Value;
			}
			bool? flag2 = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "RotateLabel"]);
			if (flag2 != null)
			{
				customLabel.RotateLabel = flag2.Value;
			}
			bool? flag3 = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "AllowUpsideDown"]);
			if (flag3 != null)
			{
				customLabel.AllowUpsideDown = flag3.Value;
			}
			if (this.ConvertDundasCRIStringProperty("Percent", properties[propertyPrefix + "FontUnit"]) == "Percent")
			{
				customLabel.UseFontPercent = true;
			}
			customLabel.Style = this.ConvertDundasCRIStyleProperty(properties[propertyPrefix + "TextColor"], null, null, null, null, null, null, null, null, properties.ContainsKey(propertyPrefix + "Font") ? properties[propertyPrefix + "Font"] : "Microsoft Sans Serif, 14pt", null, null, null);
			TickMarkStyle tickMarkStyle = new TickMarkStyle();
			if (this.UpgradeDundasCRIGaugeTickMarkStyle(tickMarkStyle, properties, propertyPrefix + "TickMarkStyle.", 3.0, null, null))
			{
				customLabel.TickMarkStyle = tickMarkStyle;
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00012600 File Offset: 0x00010800
		private void UpgradeDundasCRIGaugePointer(GaugePointer pointer, Hashtable properties, string propertyPrefix, ReportExpression<double> defaultWidth, ReportExpression<double> defaultMarkerLength, Hashtable formulaProperties, Hashtable dataValueProperties)
		{
			string text = this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "Name"]);
			pointer.Name = text;
			string text2 = ((pointer is LinearPointer) ? "LinearPointer" : "CircularPointer") + ":" + text;
			pointer.GaugeInputValue = this.UpgradeDundasCRIGaugeInputValue(formulaProperties, dataValueProperties, text2, properties, propertyPrefix + "Value");
			pointer.BarStart = new ReportExpression<BarStartTypes>(this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "BarStart"]), CultureInfo.InvariantCulture);
			pointer.Width = this.ConvertDundasCRIDoubleReportExpressionProperty(defaultWidth, properties[propertyPrefix + "Width"]);
			pointer.MarkerLength = this.ConvertDundasCRIDoubleReportExpressionProperty(defaultMarkerLength, properties[propertyPrefix + "MarkerLength"]);
			pointer.DistanceFromScale = this.ConvertDundasCRIDoubleReportExpressionProperty(properties[propertyPrefix + "DistanceFromScale"]);
			bool? flag = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "Visible"]);
			if (flag != null)
			{
				pointer.Hidden = !flag.Value;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00012724 File Offset: 0x00010924
		private void UpgradeDundasCRIGaugePointerLinear(LinearPointer pointer, Hashtable properties, string propertyPrefix, Hashtable formulaProperties, Hashtable dataValueProperties)
		{
			this.UpgradeDundasCRIGaugePointer(pointer, properties, propertyPrefix, 20.0, 20.0, formulaProperties, dataValueProperties);
			pointer.MarkerStyle = new ReportExpression<MarkerStyles>(this.ConvertDundasCRIStringProperty(MarkerStyles.Triangle.ToString(), properties[propertyPrefix + "MarkerStyle"]), CultureInfo.InvariantCulture);
			pointer.Type = new ReportExpression<LinearPointerTypes>(this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "Type"]), CultureInfo.InvariantCulture);
			pointer.Placement = new ReportExpression<Placements>(this.ConvertDundasCRIStringProperty(Placements.Outside.ToString(), properties[propertyPrefix + "Placement"]), CultureInfo.InvariantCulture);
			int num = 0;
			Thermometer thermometer = new Thermometer();
			thermometer.ThermometerStyle = new ReportExpression<ThermometerStyles>(this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "ThermometerStyle"], ref num), CultureInfo.InvariantCulture);
			thermometer.BulbOffset = this.ConvertDundasCRIDoubleReportExpressionProperty(thermometer.BulbOffset, properties[propertyPrefix + "ThermometerBulbOffset"], ref num);
			thermometer.BulbSize = this.ConvertDundasCRIDoubleReportExpressionProperty(thermometer.BulbSize, properties[propertyPrefix + "ThermometerBulbSize"], ref num);
			thermometer.Style = this.ConvertDundasCRIStyleProperty(null, properties[propertyPrefix + "ThermometerBackColor"], properties[propertyPrefix + "ThermometerBackGradientType"], properties[propertyPrefix + "ThermometerBackGradientEndColor"], properties[propertyPrefix + "ThermometerBackHatchStyle"], null, null, null, null, null, null, null, null, ref num);
			pointer.Style = this.ConvertDundasCRIStyleProperty(null, properties[propertyPrefix + "FillColor"] ?? Color.White, properties[propertyPrefix + "FillGradientType"] ?? BackgroundGradients.DiagonalLeft, properties[propertyPrefix + "FillGradientEndColor"] ?? Color.Red, properties[propertyPrefix + "FillHatchStyle"], properties[propertyPrefix + "ShadowOffset"] ?? 2, properties[propertyPrefix + "BorderColor"], properties[propertyPrefix + "BorderStyle"] ?? BorderStyles.Solid, properties[propertyPrefix + "BorderWidth"], null, null, null, null);
			if (num > 0)
			{
				pointer.Thermometer = thermometer;
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000129A4 File Offset: 0x00010BA4
		private void UpgradeDundasCRIGaugePointerRadial(RadialPointer pointer, Hashtable properties, string propertyPrefix, Hashtable formulaProperties, Hashtable dataValueProperties)
		{
			this.UpgradeDundasCRIGaugePointer(pointer, properties, propertyPrefix, 15.0, 10.0, formulaProperties, dataValueProperties);
			pointer.MarkerStyle = new ReportExpression<MarkerStyles>(this.ConvertDundasCRIStringProperty(MarkerStyles.Diamond.ToString(), properties[propertyPrefix + "MarkerStyle"]), CultureInfo.InvariantCulture);
			pointer.Type = new ReportExpression<RadialPointerTypes>(this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "Type"]), CultureInfo.InvariantCulture);
			pointer.Placement = new ReportExpression<Placements>(this.ConvertDundasCRIStringProperty(Placements.Cross.ToString(), properties[propertyPrefix + "Placement"]), CultureInfo.InvariantCulture);
			pointer.NeedleStyle = this.ConvertDundasCRIGaugeNeedleStyles(this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "NeedleStyle"]));
			int num = 0;
			PointerCap pointerCap = new PointerCap();
			pointerCap.Width = this.ConvertDundasCRIDoubleReportExpressionProperty(pointerCap.Width, properties[propertyPrefix + "CapWidth"], ref num);
			pointerCap.CapStyle = this.ConvertDundasCRIGaugeCapStyle(this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "CapStyle"], ref num));
			bool? flag = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "CapVisible"], ref num);
			if (flag != null)
			{
				pointerCap.Hidden = !flag.Value;
			}
			bool? flag2 = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "CapOnTop"], ref num);
			if (flag2 != null)
			{
				pointerCap.OnTop = flag2.Value;
			}
			else
			{
				pointerCap.OnTop = true;
			}
			bool? flag3 = this.ConvertDundasCRIBoolProperty(properties[propertyPrefix + "CapReflection"], ref num);
			if (flag3 != null)
			{
				pointerCap.Reflection = flag3.Value;
			}
			pointerCap.Style = this.ConvertDundasCRIStyleProperty(null, properties[propertyPrefix + "CapFillColor"] ?? Color.Gainsboro, properties[propertyPrefix + "CapFillGradientType"] ?? BackgroundGradients.DiagonalLeft, properties[propertyPrefix + "CapFillGradientEndColor"] ?? Color.DimGray, properties[propertyPrefix + "CapFillHatchStyle"], null, null, null, null, null, null, null, null, ref num);
			pointer.Style = this.ConvertDundasCRIStyleProperty(null, properties[propertyPrefix + "FillColor"] ?? Color.White, properties[propertyPrefix + "FillGradientType"] ?? BackgroundGradients.LeftRight, properties[propertyPrefix + "FillGradientEndColor"] ?? Color.Red, properties[propertyPrefix + "FillHatchStyle"], properties[propertyPrefix + "ShadowOffset"] ?? 2, properties[propertyPrefix + "BorderColor"], properties[propertyPrefix + "BorderStyle"] ?? BorderStyles.Solid, properties[propertyPrefix + "BorderWidth"], null, null, null, null);
			if (num > 0)
			{
				pointer.PointerCap = pointerCap;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00012D00 File Offset: 0x00010F00
		private GaugeInputValue UpgradeDundasCRIGaugeInputValue(Hashtable formulaProperties, Hashtable dataValueProperties, string dataValuePropertyKey, Hashtable customProperties, string customPropertyKey)
		{
			GaugeInputValue gaugeInputValue = null;
			if (formulaProperties.Contains(dataValuePropertyKey))
			{
				if (gaugeInputValue == null)
				{
					gaugeInputValue = new GaugeInputValue();
				}
				string text = this.ConvertDundasCRIStringProperty(formulaProperties[dataValuePropertyKey]);
				int num = text.IndexOf('(');
				int num2 = text.LastIndexOf(')');
				string text2 = ((num > -1 && num < num2) ? text.Substring(num + 1, num2 - num - 1) : string.Empty);
				string text3 = ((num > -1) ? text.Remove(num) : text).ToUpperInvariant();
				if (text3 != null)
				{
					int length = text3.Length;
					switch (length)
					{
					case 6:
						if (text3 == "MEDIAN")
						{
							gaugeInputValue.Formula = FormulaTypes.Median;
						}
						break;
					case 7:
						break;
					case 8:
						if (text3 == "VARIANCE")
						{
							gaugeInputValue.Formula = FormulaTypes.Variance;
						}
						break;
					case 9:
						if (text3 == "OPENCLOSE")
						{
							gaugeInputValue.Formula = FormulaTypes.OpenClose;
						}
						break;
					case 10:
						if (text3 == "PERCENTILE")
						{
							gaugeInputValue.Formula = FormulaTypes.Percentile;
						}
						break;
					default:
						switch (length)
						{
						case 18:
						{
							char c = text3[16];
							if (c != 'A')
							{
								if (c == 'I')
								{
									if (text3 == "CALCULATEDVALUEMIN")
									{
										gaugeInputValue.Formula = FormulaTypes.Min;
									}
								}
							}
							else if (text3 == "CALCULATEDVALUEMAX")
							{
								gaugeInputValue.Formula = FormulaTypes.Max;
							}
							break;
						}
						case 19:
						case 20:
							break;
						case 21:
							if (text3 == "CALCULATEDVALUELINEAR")
							{
								gaugeInputValue.Formula = FormulaTypes.Linear;
							}
							break;
						case 22:
							if (text3 == "CALCULATEDVALUEAVERAGE")
							{
								gaugeInputValue.Formula = FormulaTypes.Average;
							}
							break;
						case 23:
							if (text3 == "CALCULATEDVALUEINTEGRAL")
							{
								gaugeInputValue.Formula = FormulaTypes.Integral;
							}
							break;
						default:
							if (length == 27)
							{
								if (text3 == "CALCULATEDVALUERATEOFCHANGE")
								{
									gaugeInputValue.Formula = FormulaTypes.RateOfChange;
								}
							}
							break;
						}
						break;
					}
				}
				if (!string.IsNullOrEmpty(text2))
				{
					string[] array = text2.Split(new char[] { ',' });
					if (gaugeInputValue.Formula == FormulaTypes.Percentile)
					{
						if (array.Length != 0)
						{
							gaugeInputValue.MinPercent = new ReportExpression<double>(array[0], CultureInfo.InvariantCulture);
						}
						if (array.Length > 1)
						{
							gaugeInputValue.MaxPercent = new ReportExpression<double>(array[1], CultureInfo.InvariantCulture);
						}
					}
					else if (gaugeInputValue.Formula == FormulaTypes.Linear)
					{
						if (array.Length != 0)
						{
							gaugeInputValue.Multiplier = new ReportExpression<double>(array[0], CultureInfo.InvariantCulture);
						}
						if (array.Length > 1)
						{
							gaugeInputValue.AddConstant = new ReportExpression<double>(array[1], CultureInfo.InvariantCulture);
						}
					}
				}
			}
			if (dataValueProperties.Contains(dataValuePropertyKey))
			{
				if (gaugeInputValue == null)
				{
					gaugeInputValue = new GaugeInputValue();
				}
				gaugeInputValue.Value = this.ConvertDundasCRIStringProperty(dataValueProperties[dataValuePropertyKey]);
			}
			else if (customProperties.Contains(customPropertyKey))
			{
				if (gaugeInputValue == null)
				{
					gaugeInputValue = new GaugeInputValue();
				}
				gaugeInputValue.Value = this.ConvertDundasCRIStringProperty(customProperties[customPropertyKey]);
			}
			return gaugeInputValue;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0001305A File Offset: 0x0001125A
		private ActionInfo UpgradeDundasCRIGaugeActionInfo(Hashtable formulaProperties, string propertyPrefix)
		{
			return this.UpgradeDundasCRIActionInfo(formulaProperties, propertyPrefix, "Href");
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001306C File Offset: 0x0001126C
		private ActionInfo UpgradeDundasCRIActionInfo(Hashtable properties, string propertyPrefix, string hyperLinkKey)
		{
			ActionInfo actionInfo = null;
			object obj = properties[propertyPrefix + hyperLinkKey];
			if (obj != null)
			{
				string text = this.ConvertDundasCRIStringProperty(properties[propertyPrefix + "MapAreaType"]);
				actionInfo = new ActionInfo();
				Microsoft.ReportingServices.RdlObjectModel.Action action = new Microsoft.ReportingServices.RdlObjectModel.Action();
				actionInfo.Actions.Add(action);
				if (!(text == "Url"))
				{
					if (!(text == "Bookmark"))
					{
						if (text == "Report")
						{
							action.Drillthrough = new Drillthrough();
							action.Drillthrough.ReportName = this.ConvertDundasCRIStringProperty(obj);
							string text2 = propertyPrefix + "REPORTPARAM:";
							foreach (object obj2 in properties)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
								if (dictionaryEntry.Key.ToString().StartsWith(text2, StringComparison.Ordinal))
								{
									Parameter parameter = new Parameter();
									parameter.Name = dictionaryEntry.Key.ToString().Remove(0, text2.Length);
									parameter.Value = dictionaryEntry.Value.ToString();
									action.Drillthrough.Parameters.Add(parameter);
								}
							}
						}
					}
					else
					{
						action.BookmarkLink = this.ConvertDundasCRIStringProperty(obj);
					}
				}
				else
				{
					action.Hyperlink = this.ConvertDundasCRIStringProperty(obj);
				}
			}
			return actionInfo;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000131F8 File Offset: 0x000113F8
		private void FixGaugeElementNames(GaugePanel gaugePanel)
		{
			StringCollection stringCollection = new StringCollection();
			OrderedDictionary orderedDictionary = new OrderedDictionary(gaugePanel.RadialGauges.Count);
			OrderedDictionary orderedDictionary2 = new OrderedDictionary(gaugePanel.LinearGauges.Count);
			OrderedDictionary orderedDictionary3 = new OrderedDictionary(gaugePanel.GaugeLabels.Count);
			foreach (RadialGauge radialGauge in gaugePanel.RadialGauges)
			{
				string text = this.CreateNewName(stringCollection, radialGauge.Name, "Default");
				stringCollection.Add(text);
				if (!orderedDictionary.Contains(radialGauge.Name))
				{
					orderedDictionary.Add(radialGauge.Name, text);
				}
				radialGauge.Name = text;
				this.FixGaugeSubElementNames(radialGauge);
			}
			stringCollection.Clear();
			foreach (LinearGauge linearGauge in gaugePanel.LinearGauges)
			{
				string text2 = this.CreateNewName(stringCollection, linearGauge.Name, "Default");
				stringCollection.Add(text2);
				if (!orderedDictionary2.Contains(linearGauge.Name))
				{
					orderedDictionary2.Add(linearGauge.Name, text2);
				}
				linearGauge.Name = text2;
				this.FixGaugeSubElementNames(linearGauge);
			}
			stringCollection.Clear();
			foreach (GaugeLabel gaugeLabel in gaugePanel.GaugeLabels)
			{
				string text3 = this.CreateNewName(stringCollection, gaugeLabel.Name, "Default");
				stringCollection.Add(text3);
				if (!orderedDictionary3.Contains(gaugeLabel.Name))
				{
					orderedDictionary3.Add(gaugeLabel.Name, text3);
				}
				gaugeLabel.Name = text3;
			}
			foreach (RadialGauge radialGauge2 in gaugePanel.RadialGauges)
			{
				this.FixGaugeElementParentItemNames(radialGauge2, orderedDictionary, orderedDictionary2, orderedDictionary3);
			}
			foreach (LinearGauge linearGauge2 in gaugePanel.LinearGauges)
			{
				this.FixGaugeElementParentItemNames(linearGauge2, orderedDictionary, orderedDictionary2, orderedDictionary3);
			}
			foreach (GaugeLabel gaugeLabel2 in gaugePanel.GaugeLabels)
			{
				this.FixGaugeElementParentItemNames(gaugeLabel2, orderedDictionary, orderedDictionary2, orderedDictionary3);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000134B0 File Offset: 0x000116B0
		private void FixGaugeSubElementNames(Gauge gauge)
		{
			StringCollection stringCollection = new StringCollection();
			foreach (GaugeScale gaugeScale in gauge.GaugeScales)
			{
				gaugeScale.Name = this.CreateNewName(stringCollection, gaugeScale.Name, "Default");
				stringCollection.Add(gaugeScale.Name);
				StringCollection stringCollection2 = new StringCollection();
				foreach (ScaleRange scaleRange in gaugeScale.ScaleRanges)
				{
					scaleRange.Name = this.CreateNewName(stringCollection2, scaleRange.Name, "Default");
					stringCollection2.Add(scaleRange.Name);
				}
				stringCollection2.Clear();
				foreach (GaugePointer gaugePointer in gaugeScale.GaugePointers)
				{
					gaugePointer.Name = this.CreateNewName(stringCollection2, gaugePointer.Name, "Default");
					stringCollection2.Add(gaugePointer.Name);
				}
				stringCollection2.Clear();
				foreach (CustomLabel customLabel in gaugeScale.CustomLabels)
				{
					customLabel.Name = this.CreateNewName(stringCollection2, customLabel.Name, "Default");
					stringCollection2.Add(customLabel.Name);
				}
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00013694 File Offset: 0x00011894
		private void FixGaugeElementParentItemNames(GaugePanelItem gaugeElement, OrderedDictionary radialGaugeNameMapping, OrderedDictionary linearGaugeNameMapping, OrderedDictionary gaugeLabelNameMapping)
		{
			string text = string.Empty;
			if (gaugeElement.ParentItem.StartsWith("CircularGauges.", StringComparison.Ordinal))
			{
				text = this.GetNewName(radialGaugeNameMapping, gaugeElement.ParentItem.Substring("CircularGauges.".Length));
				if (!string.IsNullOrEmpty(text))
				{
					text = "RadialGauges." + text;
				}
			}
			else if (gaugeElement.ParentItem.StartsWith("LinearGauges.", StringComparison.Ordinal))
			{
				text = this.GetNewName(linearGaugeNameMapping, gaugeElement.ParentItem.Substring("LinearGauges.".Length));
				if (!string.IsNullOrEmpty(text))
				{
					text = "LinearGauges." + text;
				}
			}
			else if (gaugeElement is GaugeLabel && gaugeElement.ParentItem.StartsWith("GaugeLabels.", StringComparison.Ordinal))
			{
				text = this.GetNewName(gaugeLabelNameMapping, gaugeElement.ParentItem.Substring("GaugeLabels.".Length));
				if (!string.IsNullOrEmpty(text))
				{
					text = "GaugeLabels." + text;
				}
			}
			gaugeElement.ParentItem = text;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00013790 File Offset: 0x00011990
		private CapStyles ConvertDundasCRIGaugeCapStyle(string capStyle)
		{
			if (capStyle == "CustomCap1")
			{
				return CapStyles.Rounded;
			}
			if (capStyle == "CustomCap2")
			{
				return CapStyles.RoundedLight;
			}
			if (capStyle == "CustomCap3")
			{
				return CapStyles.RoundedWithAdditionalTop;
			}
			if (capStyle == "CustomCap4")
			{
				return CapStyles.RoundedWithWideIndentation;
			}
			if (capStyle == "CustomCap5")
			{
				return CapStyles.FlattenedWithIndentation;
			}
			if (capStyle == "CustomCap6")
			{
				return CapStyles.FlattenedWithWideIndentation;
			}
			if (capStyle == "CustomCap7")
			{
				return CapStyles.RoundedGlossyWithIndentation;
			}
			if (capStyle == "CustomCap8")
			{
				return CapStyles.RoundedWithIndentation;
			}
			return CapStyles.RoundedDark;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00013818 File Offset: 0x00011A18
		private NeedleStyles ConvertDundasCRIGaugeNeedleStyles(string needleStyle)
		{
			if (needleStyle == "NeedleStyle2")
			{
				return NeedleStyles.Rectangular;
			}
			if (needleStyle == "NeedleStyle3")
			{
				return NeedleStyles.TaperedWithTail;
			}
			if (needleStyle == "NeedleStyle4")
			{
				return NeedleStyles.Tapered;
			}
			if (needleStyle == "NeedleStyle5")
			{
				return NeedleStyles.ArrowWithTail;
			}
			if (needleStyle == "NeedleStyle6")
			{
				return NeedleStyles.Arrow;
			}
			if (needleStyle == "NeedleStyle7")
			{
				return NeedleStyles.StealthArrowWithTail;
			}
			if (needleStyle == "NeedleStyle8")
			{
				return NeedleStyles.StealthArrow;
			}
			if (needleStyle == "NeedleStyle9")
			{
				return NeedleStyles.TaperedWithStealthArrow;
			}
			if (needleStyle == "NeedleStyle10")
			{
				return NeedleStyles.StealthArrowWithWideTail;
			}
			if (needleStyle == "NeedleStyle11")
			{
				return NeedleStyles.TaperedWithRoundedPoint;
			}
			return NeedleStyles.Triangular;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000138BE File Offset: 0x00011ABE
		private string GetNewName(OrderedDictionary oldAndNewNameMapping, string oldName)
		{
			if (oldAndNewNameMapping.Contains(oldName))
			{
				return oldAndNewNameMapping[oldName].ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000138DC File Offset: 0x00011ADC
		private string CreateNewName(StringCollection newNamesCollection, string oldName, string defaultNewName)
		{
			int num = 1;
			string text = ((oldName.Trim() == string.Empty) ? (defaultNewName + num.ToString(CultureInfo.InvariantCulture)) : StringUtil.GetClsCompliantIdentifier(oldName, "chart"));
			if (newNamesCollection.Contains(text))
			{
				while (newNamesCollection.Contains(text + "_" + num.ToString(CultureInfo.InvariantCulture)))
				{
					num++;
				}
				text = text + "_" + num.ToString(CultureInfo.InvariantCulture);
			}
			return text;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00013964 File Offset: 0x00011B64
		private Font FontFromString(string fontString)
		{
			string text = fontString;
			byte b = 1;
			bool flag = false;
			int num = fontString.IndexOf(", GdiCharSet=", StringComparison.Ordinal);
			if (num >= 0)
			{
				string text2 = fontString.Substring(num + 13);
				int num2 = text2.IndexOf(',');
				if (num2 >= 0)
				{
					text2 = text2.Substring(0, num2);
				}
				b = (byte)int.Parse(text2, CultureInfo.InvariantCulture);
				if (text.Length > num)
				{
					text = text.Substring(0, num);
				}
			}
			num = fontString.IndexOf(", GdiVerticalFont", StringComparison.Ordinal);
			if (num >= 0)
			{
				flag = true;
				if (text.Length > num)
				{
					text = text.Substring(0, num);
				}
			}
			Font font = (Font)new FontConverter().ConvertFromInvariantString(text);
			float num3 = font.SizeInPoints;
			num3 = Math.Min(Math.Max(font.SizeInPoints, (float)Constants.MinimumFontSize.ToPoints()), (float)Constants.MaximumFontSize.ToPoints());
			if (flag || b != 1 || num3 != font.SizeInPoints)
			{
				Font font2 = new Font(font.Name, num3, font.Style, GraphicsUnit.Point, b, flag);
				font.Dispose();
				return font2;
			}
			return font;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00013A78 File Offset: 0x00011C78
		private bool AddToPropertyList(List<Hashtable> propertyList, string counterPrefix, string key, ReportExpression value)
		{
			if (!key.StartsWith(counterPrefix, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (!counterPrefix.EndsWith(".", StringComparison.Ordinal))
			{
				counterPrefix += ".";
			}
			key = key.Substring(counterPrefix.Length);
			int num = key.IndexOf('.');
			int i = 0;
			if (!int.TryParse(key.Substring(0, num), out i))
			{
				return false;
			}
			key = key.Substring(num + 1);
			while (i >= propertyList.Count)
			{
				propertyList.Add(new Hashtable());
			}
			propertyList[i].Add(key, value);
			return true;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00013B10 File Offset: 0x00011D10
		private bool IsZero(object value)
		{
			double num;
			return value != null && (double.TryParse(value.ToString(), out num) && num == 0.0);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00013B40 File Offset: 0x00011D40
		private void ConvertDundasCRICustomProperties(IList<CustomProperty> customProperties, object property)
		{
			int num = 0;
			this.ConvertDundasCRICustomProperties(customProperties, property, ref num);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00013B5C File Offset: 0x00011D5C
		private void ConvertDundasCRICustomProperties(IList<CustomProperty> customProperties, object property, ref int counter)
		{
			if (property != null)
			{
				counter++;
				if (customProperties != null)
				{
					customProperties.Clear();
				}
				else
				{
					customProperties = new List<CustomProperty>();
				}
				foreach (string text in property.ToString().Replace("\\,", "\\x45").Replace("\\=", "\\x46")
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					int num = text.IndexOf('=');
					customProperties.Add(new CustomProperty
					{
						Name = text.Substring(0, num).Trim(),
						Value = text.Substring(num + 1).Replace("\\x45", ",").Replace("\\x46", "=")
							.Trim()
					});
				}
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00013C38 File Offset: 0x00011E38
		private ReportExpression<ReportColor> ConvertDundasCRIColorProperty(string defaultValue, object color)
		{
			return this.ConvertDundasCRIColorProperty(new ReportExpression<ReportColor>(defaultValue, CultureInfo.InvariantCulture), color);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00013C4C File Offset: 0x00011E4C
		private ReportExpression<ReportColor> ConvertDundasCRIColorProperty(ReportExpression<ReportColor> defaultValue, object color)
		{
			int num = 0;
			return this.ConvertDundasCRIColorProperty(defaultValue, color, ref num);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00013C68 File Offset: 0x00011E68
		private ReportExpression<ReportColor> ConvertDundasCRIColorProperty(ReportExpression<ReportColor> defaultValue, object color, ref int counter)
		{
			if (color != null)
			{
				counter++;
				if (color is ReportExpression<ReportColor>)
				{
					return (ReportExpression<ReportColor>)color;
				}
				ColorConverter colorConverter = new ColorConverter();
				try
				{
					Color color2 = ((color is Color) ? ((Color)color) : ((Color)colorConverter.ConvertFromInvariantString(color.ToString())));
					if (color2.IsSystemColor)
					{
						return new ReportExpression<ReportColor>(new ReportColor(Color.FromArgb(color2.ToArgb())));
					}
					return new ReportExpression<ReportColor>(new ReportColor(color2));
				}
				catch
				{
					try
					{
						return new ReportExpression<ReportColor>(color.ToString(), CultureInfo.InvariantCulture);
					}
					catch
					{
						return defaultValue;
					}
				}
				return defaultValue;
			}
			return defaultValue;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00013D20 File Offset: 0x00011F20
		private bool? ConvertDundasCRIBoolProperty(object property)
		{
			int num = 0;
			return this.ConvertDundasCRIBoolProperty(property, ref num);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00013D38 File Offset: 0x00011F38
		private bool? ConvertDundasCRIBoolProperty(object property, ref int counter)
		{
			bool flag;
			if (property != null && bool.TryParse(property.ToString(), out flag))
			{
				counter++;
				return new bool?(flag);
			}
			return null;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00013D70 File Offset: 0x00011F70
		private string ConvertDundasCRIStringProperty(object property)
		{
			int num = 0;
			return this.ConvertDundasCRIStringProperty(string.Empty, property, ref num);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00013D90 File Offset: 0x00011F90
		private string ConvertDundasCRIStringProperty(string defaultValue, object property)
		{
			int num = 0;
			return this.ConvertDundasCRIStringProperty(defaultValue, property, ref num);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00013DA9 File Offset: 0x00011FA9
		private string ConvertDundasCRIStringProperty(object property, ref int counter)
		{
			return this.ConvertDundasCRIStringProperty(string.Empty, property, ref counter);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00013DB8 File Offset: 0x00011FB8
		private string ConvertDundasCRIStringProperty(string defaultValue, object property, ref int counter)
		{
			if (property != null)
			{
				counter++;
				return property.ToString();
			}
			return defaultValue;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00013DCC File Offset: 0x00011FCC
		private ReportExpression<int> ConvertDundasCRIIntegerReportExpressionProperty(object property)
		{
			int num = 0;
			return this.ConvertDundasCRIIntegerReportExpressionProperty(null, property, ref num);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00013DF4 File Offset: 0x00011FF4
		private ReportExpression<int> ConvertDundasCRIIntegerReportExpressionProperty(object property, ref int counter)
		{
			return this.ConvertDundasCRIIntegerReportExpressionProperty(null, property, ref counter);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00013E18 File Offset: 0x00012018
		private ReportExpression<int> ConvertDundasCRIIntegerReportExpressionProperty(ReportExpression<int> defaultValue, object property)
		{
			int num = 0;
			return this.ConvertDundasCRIIntegerReportExpressionProperty(defaultValue, property, ref num);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00013E31 File Offset: 0x00012031
		private ReportExpression<int> ConvertDundasCRIIntegerReportExpressionProperty(ReportExpression<int> defaultValue, object property, ref int counter)
		{
			if (property != null)
			{
				counter++;
				return new ReportExpression<int>(property.ToString(), CultureInfo.InvariantCulture.NumberFormat);
			}
			return defaultValue;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00013E54 File Offset: 0x00012054
		private ReportExpression<double> ConvertDundasCRIDoubleReportExpressionProperty(object property)
		{
			int num = 0;
			return this.ConvertDundasCRIDoubleReportExpressionProperty(null, property, ref num);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00013E7C File Offset: 0x0001207C
		private ReportExpression<double> ConvertDundasCRIDoubleReportExpressionProperty(ReportExpression<double> defaultValue, object property)
		{
			int num = 0;
			return this.ConvertDundasCRIDoubleReportExpressionProperty(defaultValue, property, ref num);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00013E98 File Offset: 0x00012098
		private ReportExpression<double> ConvertDundasCRIDoubleReportExpressionProperty(object property, ref int counter)
		{
			return this.ConvertDundasCRIDoubleReportExpressionProperty(null, property, ref counter);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00013EBC File Offset: 0x000120BC
		private ReportExpression<double> ConvertDundasCRIDoubleReportExpressionProperty(ReportExpression<double> defaultValue, object property, ref int counter)
		{
			if (property == null)
			{
				return defaultValue;
			}
			string text = property.ToString();
			counter++;
			if (text == "Auto")
			{
				return new ReportExpression<double>(double.NaN);
			}
			return new ReportExpression<double>(property.ToString(), CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00013F0C File Offset: 0x0001210C
		private ReportExpression<ReportSize> ConvertDundasCRIPointReportSizeProperty(ReportExpression<ReportSize> defaultValue, object property, ref int counter)
		{
			if (property == null)
			{
				return defaultValue;
			}
			string text = property.ToString();
			counter++;
			if (property is ReportExpression && ((ReportExpression)property).IsExpression)
			{
				return new ReportExpression<ReportSize>(text, CultureInfo.InvariantCulture);
			}
			double num = 0.0;
			if (!double.TryParse(text, out num))
			{
				return defaultValue;
			}
			return new ReportExpression<ReportSize>(new ReportSize(num, SizeTypes.Point));
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00013F74 File Offset: 0x00012174
		private ReportExpression<ReportSize> ConvertDundasCRIPointReportSizeProperty(ReportExpression<ReportSize> defaultValue, object property)
		{
			int num = 0;
			return this.ConvertDundasCRIPointReportSizeProperty(defaultValue, property, ref num);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00013F90 File Offset: 0x00012190
		private ReportExpression<ReportSize> ConvertDundasCRIPixelReportSizeProperty(double? defaultValue, object property, ref int counter)
		{
			if (property != null)
			{
				string text = property.ToString();
				counter++;
				if (property is ReportExpression && ((ReportExpression)property).IsExpression)
				{
					text = text.Substring(text.IndexOf('=') + 1);
					return new ReportExpression<ReportSize>("=CStr(({0})*{1})&\"pt\"".Replace("{1}", 0.75.ToString(CultureInfo.InvariantCulture.NumberFormat)).Replace("{0}", text), CultureInfo.InvariantCulture);
				}
				double num = 0.0;
				if (double.TryParse(text, out num))
				{
					return new ReportExpression<ReportSize>(new ReportSize(num * 0.75, SizeTypes.Point));
				}
			}
			if (defaultValue == null)
			{
				return default(ReportExpression<ReportSize>);
			}
			return new ReportExpression<ReportSize>(new ReportSize(defaultValue.Value * 0.75, SizeTypes.Point));
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00014074 File Offset: 0x00012274
		private ReportExpression<ReportSize> ConvertDundasCRIPixelReportSizeProperty(object property, ref int counter)
		{
			return this.ConvertDundasCRIPixelReportSizeProperty(null, property, ref counter);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00014094 File Offset: 0x00012294
		private ReportExpression<ReportSize> ConvertDundasCRIPixelReportSizeProperty(object property)
		{
			int num = 0;
			return this.ConvertDundasCRIPixelReportSizeProperty(null, property, ref num);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000140B8 File Offset: 0x000122B8
		private ActionInfo ConvertDundasCRIActionInfoProperty(object hyperlink)
		{
			int num = 0;
			return this.ConvertDundasCRIActionInfoProperty(hyperlink, ref num);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000140D0 File Offset: 0x000122D0
		private ActionInfo ConvertDundasCRIActionInfoProperty(object hyperlink, ref int counter)
		{
			if (hyperlink != null && hyperlink.ToString() != string.Empty)
			{
				Microsoft.ReportingServices.RdlObjectModel.Action action = new Microsoft.ReportingServices.RdlObjectModel.Action();
				action.Hyperlink = hyperlink.ToString();
				ActionInfo actionInfo = new ActionInfo();
				actionInfo.Actions.Add(action);
				counter++;
				return actionInfo;
			}
			return null;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00014124 File Offset: 0x00012324
		private ChartElementPosition ConvertDundasCRIChartElementPosition(object top, object left, object height, object width)
		{
			int num = 0;
			return this.ConvertDundasCRIChartElementPosition(top, left, height, width, ref num);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00014140 File Offset: 0x00012340
		private ChartElementPosition ConvertDundasCRIChartElementPosition(object top, object left, object height, object width, ref int counter)
		{
			int num = 0;
			ChartElementPosition chartElementPosition = new ChartElementPosition();
			chartElementPosition.Top = this.ConvertDundasCRIDoubleReportExpressionProperty(top, ref num);
			chartElementPosition.Left = this.ConvertDundasCRIDoubleReportExpressionProperty(left, ref num);
			chartElementPosition.Height = this.ConvertDundasCRIDoubleReportExpressionProperty(height, ref num);
			chartElementPosition.Width = this.ConvertDundasCRIDoubleReportExpressionProperty(width, ref num);
			if (num > 0)
			{
				counter++;
				return chartElementPosition;
			}
			return null;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000141A4 File Offset: 0x000123A4
		private Style ConvertDundasCRIStyleProperty(object color, object backgroundColor, object backgroundGradientType, object backgroundGradientEndColor, object backgroundHatchType, object shadowOffset, object borderColor, object borderStyle, object borderWidth, object font, object format, object textAlign, object textVerticalAlign)
		{
			int num = 0;
			return (Style)this.ConvertDundasCRIStyleProperty(color, backgroundColor, backgroundGradientType, backgroundGradientEndColor, backgroundHatchType, null, shadowOffset, borderColor, borderStyle, borderWidth, null, null, null, null, font, format, null, textAlign, textVerticalAlign, ref num, new Style(), new Border());
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000141E8 File Offset: 0x000123E8
		private Style ConvertDundasCRIStyleProperty(object color, object backgroundColor, object backgroundGradientType, object backgroundGradientEndColor, object backgroundHatchType, object shadowOffset, object borderColor, object borderStyle, object borderWidth, object font, object format, object textAlign, object textVerticalAlign, ref int counter)
		{
			return (Style)this.ConvertDundasCRIStyleProperty(color, backgroundColor, backgroundGradientType, backgroundGradientEndColor, backgroundHatchType, null, shadowOffset, borderColor, borderStyle, borderWidth, null, null, null, null, font, format, null, textAlign, textVerticalAlign, ref counter, new Style(), new Border());
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001422C File Offset: 0x0001242C
		private Style ConvertDundasCRIStyleProperty(object color, object backgroundColor, object backgroundGradientType, object backgroundGradientEndColor, object backgroundHatchType, object shadowColor, object shadowOffset, object borderColor, object borderStyle, object borderWidth, object imageReference, object imageTransColor, object imageAlign, object imageMode, object font, object format, object textEffect, object textAlign, object textVerticalAlign)
		{
			int num = 0;
			return (Style)this.ConvertDundasCRIStyleProperty(color, backgroundColor, backgroundGradientType, backgroundGradientEndColor, backgroundHatchType, shadowColor, shadowOffset, borderColor, borderStyle, borderWidth, imageReference, imageTransColor, imageAlign, imageMode, font, format, textEffect, textAlign, textVerticalAlign, ref num, new Style(), new Border());
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00014278 File Offset: 0x00012478
		private Style ConvertDundasCRIStyleProperty(object color, object backgroundColor, object backgroundGradientType, object backgroundGradientEndColor, object backgroundHatchType, object shadowColor, object shadowOffset, object borderColor, object borderStyle, object borderWidth, object imageReference, object imageTransColor, object imageAlign, object imageMode, object font, object format, object textEffect, object textAlign, object textVerticalAlign, ref int counter)
		{
			return (Style)this.ConvertDundasCRIStyleProperty(color, backgroundColor, backgroundGradientType, backgroundGradientEndColor, backgroundHatchType, shadowColor, shadowOffset, borderColor, borderStyle, borderWidth, imageReference, imageTransColor, imageAlign, imageMode, font, format, textEffect, textAlign, textVerticalAlign, ref counter, new Style(), new Border());
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000142C0 File Offset: 0x000124C0
		private EmptyColorStyle ConvertDundasCRIEmptyColorStyleProperty(object color, object backgroundColor, object backgroundGradientType, object backgroundGradientEndColor, object backgroundHatchType, object borderColor, object borderStyle, object borderWidth, object imageReference, object imageTransColor, object imageAlign, object imageMode, object font, object format, ref int counter)
		{
			return (EmptyColorStyle)this.ConvertDundasCRIStyleProperty(color, backgroundColor, backgroundGradientType, backgroundGradientEndColor, backgroundHatchType, null, null, borderColor, borderStyle, borderWidth, imageReference, imageTransColor, imageAlign, imageMode, font, format, null, null, null, ref counter, new EmptyColorStyle(), new EmptyBorder());
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00014304 File Offset: 0x00012504
		private object ConvertDundasCRIStyleProperty(object color, object backgroundColor, object backgroundGradientType, object backgroundGradientEndColor, object backgroundHatchType, object shadowColor, object shadowOffset, object borderColor, object borderStyle, object borderWidth, object imageReference, object imageTransColor, object imageAlign, object imageMode, object font, object format, object textEffect, object textAlign, object textVerticalAlign, ref int counter, Style style, Border border)
		{
			int num = 0;
			style.Color = this.ConvertDundasCRIColorProperty(style.Color, color, ref num);
			style.BackgroundColor = this.ConvertDundasCRIColorProperty(style.BackgroundColor, backgroundColor, ref num);
			style.BackgroundGradientEndColor = this.ConvertDundasCRIColorProperty(style.BackgroundGradientEndColor, backgroundGradientEndColor, ref num);
			style.ShadowColor = this.ConvertDundasCRIColorProperty(style.ShadowColor, shadowColor, ref num);
			style.ShadowOffset = this.ConvertDundasCRIPixelReportSizeProperty(shadowOffset, ref num);
			style.Format = this.ConvertDundasCRIStringProperty(format, ref num);
			style.TextEffect = new ReportExpression<TextEffects>(this.ConvertDundasCRIStringProperty(textEffect, ref num), CultureInfo.InvariantCulture);
			try
			{
				style.BackgroundGradientType = new ReportExpression<BackgroundGradients>(this.ConvertDundasCRIStringProperty(backgroundGradientType, ref num), CultureInfo.InvariantCulture);
			}
			catch
			{
			}
			try
			{
				style.BackgroundHatchType = new ReportExpression<BackgroundHatchTypes>(this.ConvertDundasCRIStringProperty(backgroundHatchType, ref num), CultureInfo.InvariantCulture);
			}
			catch
			{
			}
			style.TextAlign = new ReportExpression<TextAlignments>(this.ConvertDundasCRIStringProperty(textAlign, ref num), CultureInfo.InvariantCulture);
			style.VerticalAlign = new ReportExpression<VerticalAlignments>(this.ConvertDundasCRIStringProperty(textVerticalAlign, ref num), CultureInfo.InvariantCulture);
			int num2 = 0;
			border.Color = this.ConvertDundasCRIColorProperty(border.Color, borderColor, ref num2);
			border.Width = this.ConvertDundasCRIPixelReportSizeProperty(new double?(1.0), borderWidth, ref num2);
			string text = this.ConvertDundasCRIStringProperty(BorderStyles.Solid.ToString(), borderStyle, ref num2);
			if (!(text == "NotSet"))
			{
				if (!(text == "Dash"))
				{
					if (!(text == "Dot"))
					{
						try
						{
							border.Style = new ReportExpression<BorderStyles>(text, CultureInfo.InvariantCulture);
						}
						catch
						{
							border.Style = BorderStyles.Solid;
						}
					}
					else
					{
						border.Style = BorderStyles.Dotted;
					}
				}
				else
				{
					border.Style = BorderStyles.Dashed;
				}
			}
			else
			{
				border.Style = BorderStyles.None;
			}
			if (num2 > 0)
			{
				if (borderWidth != null && !border.Width.IsExpression)
				{
					if (border.Width.Value < Constants.MinimumBorderWidth)
					{
						border.Width = Constants.DefaultBorderWidth;
						border.Style = BorderStyles.None;
					}
					else if (border.Width.Value > Constants.MaximumBorderWidth)
					{
						border.Width = Constants.MaximumBorderWidth;
					}
				}
				if (style is EmptyColorStyle)
				{
					((EmptyColorStyle)style).Border = (EmptyBorder)border;
				}
				else
				{
					style.Border = border;
				}
				num++;
			}
			style.BackgroundImage = this.ConvertDundasCRIChartBackgroundImageProperty(imageReference, imageTransColor, imageAlign, imageMode, ref num);
			string text2 = this.ConvertDundasCRIStringProperty(font, ref num);
			if (text2 != string.Empty)
			{
				Font font2 = this.FontFromString(text2);
				style.FontFamily = font2.FontFamily.Name;
				style.FontSize = new ReportSize((double)font2.Size, SizeTypes.Point);
				if (font2.Bold)
				{
					style.FontWeight = FontWeights.Bold;
				}
				if (font2.Italic)
				{
					style.FontStyle = FontStyles.Italic;
				}
				if (font2.Strikeout)
				{
					style.TextDecoration = TextDecorations.LineThrough;
				}
				else if (font2.Underline)
				{
					style.TextDecoration = TextDecorations.Underline;
				}
			}
			if (num > 0)
			{
				counter++;
				return style;
			}
			return null;
		}

		// Token: 0x04000078 RID: 120
		private Hashtable m_nameTable;

		// Token: 0x04000079 RID: 121
		private Hashtable m_dataSourceNameTable;

		// Token: 0x0400007A RID: 122
		private Hashtable m_dataSourceCaseSensitiveNameTable;

		// Token: 0x0400007B RID: 123
		private List<DataSource2005> m_dataSources;

		// Token: 0x0400007C RID: 124
		private List<IUpgradeable> m_upgradeable;

		// Token: 0x0400007D RID: 125
		private readonly ReportRegularExpressions m_regexes;

		// Token: 0x0400007E RID: 126
		private readonly bool m_throwUpgradeException = true;

		// Token: 0x0400007F RID: 127
		private readonly bool m_upgradeDundasCRIToNative;

		// Token: 0x04000080 RID: 128
		private readonly bool m_renameInvalidDataSources = true;

		// Token: 0x04000081 RID: 129
		private static readonly string[] m_scatterChartDataPointNames = new string[] { "X", "Y" };

		// Token: 0x04000082 RID: 130
		private static readonly string[] m_bubbleChartDataPointNames = new string[] { "X", "Y", "Size" };

		// Token: 0x04000083 RID: 131
		private static readonly string[] m_highLowCloseDataPointNames = new string[] { "High", "Low", "Close" };

		// Token: 0x04000084 RID: 132
		private static readonly string[] m_openHighLowCloseDataPointNames = new string[] { "High", "Low", "Open", "Close" };

		// Token: 0x04000085 RID: 133
		private const string DundasChartControl = "DundasChartControl";

		// Token: 0x04000086 RID: 134
		private const string DundasGaugeControl = "DundasGaugeControl";

		// Token: 0x04000087 RID: 135
		private const string UpgradedYukonChart = "__Upgraded2005__";

		// Token: 0x04000088 RID: 136
		private const double YukonDefaultPointWidth = 0.8;

		// Token: 0x04000089 RID: 137
		private const double YukonDefaultBarAndColumnPointWidth = 0.6;

		// Token: 0x0400008A RID: 138
		private const double YukonDefaultLineWidthInPoints = 2.25;

		// Token: 0x0400008B RID: 139
		private const double YukonDefaultBorderWidthInPoints = 0.75;

		// Token: 0x0400008C RID: 140
		private const double YukonBorderWidthFactor = 0.75;

		// Token: 0x0400008D RID: 141
		private const double KatmaiMinimumVisibleBorderWidth = 0.376;

		// Token: 0x0400008E RID: 142
		private const double KatmaiMinimumBorderWidth = 0.25;

		// Token: 0x0400008F RID: 143
		private static readonly Style.Definition.Properties[] m_ParagraphAvailableStyles = new Style.Definition.Properties[]
		{
			Style.Definition.Properties.TextAlign,
			Style.Definition.Properties.LineHeight
		};

		// Token: 0x04000090 RID: 144
		private static readonly Style.Definition.Properties[] m_TextRunAvailableStyles = new Style.Definition.Properties[]
		{
			Style.Definition.Properties.FontStyle,
			Style.Definition.Properties.FontFamily,
			Style.Definition.Properties.FontSize,
			Style.Definition.Properties.FontWeight,
			Style.Definition.Properties.Format,
			Style.Definition.Properties.TextDecoration,
			Style.Definition.Properties.Color,
			Style.Definition.Properties.Language,
			Style.Definition.Properties.Calendar,
			Style.Definition.Properties.NumeralLanguage,
			Style.Definition.Properties.NumeralVariant
		};

		// Token: 0x04000091 RID: 145
		private const double PointsPerPixel = 0.75;

		// Token: 0x04000092 RID: 146
		private const string DundasCRIExpressionPrefixLowerCase = "expression:";

		// Token: 0x04000093 RID: 147
		private const string DundasCRIDefaultFont = "Microsoft Sans Serif, 8pt";

		// Token: 0x04000094 RID: 148
		private const string DundasCRIDefaultBoldFont = "Microsoft Sans Serif, 8pt, style=Bold";

		// Token: 0x04000095 RID: 149
		private const string DundasCRIDefaultCollectedPieStyle = "CollectedPie";

		// Token: 0x04000096 RID: 150
		private const string DundasCRISizeExpressionWrapper = "=CStr(({0})*{1})&\"pt\"";

		// Token: 0x04000097 RID: 151
		private const string EmptySeriesName = "emptySeriesName";

		// Token: 0x04000098 RID: 152
		private const string EmptyNamePrefix = "chart";

		// Token: 0x04000099 RID: 153
		private const string ChartElementDefaultName = "Default";

		// Token: 0x0400009A RID: 154
		private const string ChartPrimaryAxisName = "Primary";

		// Token: 0x0400009B RID: 155
		private const string ChartSecondaryAxisName = "Secondary";

		// Token: 0x0400009C RID: 156
		private const string NewChartAreaName = "ChartArea";

		// Token: 0x0400009D RID: 157
		private const string NewChartSeriesName = "Series";

		// Token: 0x0400009E RID: 158
		private const string NewChartTitleName = "Title";

		// Token: 0x0400009F RID: 159
		private const string ChartNoDataMessageTitleName = "NoDataMessageTitle";

		// Token: 0x040000A0 RID: 160
		private const string NewChartLegendName = "Legend";

		// Token: 0x040000A1 RID: 161
		private const string ChartFormulaNamePostfix = "_Formula";

		// Token: 0x040000A2 RID: 162
		private const string NewChartAreaNameForFormulaSeries = "#NewChartArea";

		// Token: 0x040000A3 RID: 163
		private const string PointWidthAttributeName = "PointWidth";

		// Token: 0x040000A4 RID: 164
		private const string DrawingStyleAttributeName = "DrawingStyle";

		// Token: 0x040000A5 RID: 165
		private const string PieLabelStyleAttributeName = "PieLabelStyle";

		// Token: 0x040000A6 RID: 166
		private const string PieLabelStyleAttributeDefaultValueForYukon = "Outside";

		// Token: 0x040000A7 RID: 167
		private const double DefaultSmartLabelMaxMovingDistance = 30.0;

		// Token: 0x040000A8 RID: 168
		private const double DefaultBorderLineWidthInPixels = 1.0;

		// Token: 0x040000A9 RID: 169
		private const string GaugeElementDefaultName = "Default";

		// Token: 0x040000AA RID: 170
		private const string DefaultRadialGaugeCollectionPrefix = "RadialGauges.";

		// Token: 0x040000AB RID: 171
		private const string DefaultLinearGaugeCollectionPrefix = "LinearGauges.";

		// Token: 0x040000AC RID: 172
		private const string DefaultGaugeLabelCollectionPrefix = "GaugeLabels.";

		// Token: 0x040000AD RID: 173
		private const string GaugeFontUnitPercentValue = "Percent";

		// Token: 0x040000AE RID: 174
		private const string GaugeFontUnitDefaultValue = "Default";

		// Token: 0x040000AF RID: 175
		private const string DefaultDundasCircularGaugeCollectionPrefix = "CircularGauges.";

		// Token: 0x040000B0 RID: 176
		private const string DefaultDundasLinearGaugeCollectionPrefix = "LinearGauges.";

		// Token: 0x040000B1 RID: 177
		private const string DefaultDundasGaugeLabelCollectionPrefix = "GaugeLabels.";

		// Token: 0x040000B2 RID: 178
		private const string DefaultGaugeScaleLabelFont = "Microsoft Sans Serif, 14pt";

		// Token: 0x040000B3 RID: 179
		private const string DefaultGaugeScalePinFont = "Microsoft Sans Serif, 12pt";

		// Token: 0x040000B4 RID: 180
		private const string DefaultGaugeLabelFont = "Microsoft Sans Serif, 8.25pt";

		// Token: 0x040000B5 RID: 181
		private const string DefaultGaugeScaleName = "Default";

		// Token: 0x040000B6 RID: 182
		private const string DefaultGaugeTextPropertyValue = "Text";

		// Token: 0x040000B7 RID: 183
		private const double DefaultLinearScaleRangeStartWidth = 10.0;

		// Token: 0x040000B8 RID: 184
		private const double DefaultRadialScaleRangeStartWidth = 15.0;

		// Token: 0x040000B9 RID: 185
		private const double DefaultLinearScaleRangeEndWidth = 10.0;

		// Token: 0x040000BA RID: 186
		private const double DefaultRadialScaleRangeEndWidth = 30.0;

		// Token: 0x040000BB RID: 187
		private const double DefaultLinearScaleRangeDistanceFromScale = 10.0;

		// Token: 0x040000BC RID: 188
		private const double DefaultRadialScaleRangeDistanceFromScale = 30.0;

		// Token: 0x040000BD RID: 189
		private const double DefaultGaugeTickMarkWidth = 3.0;

		// Token: 0x040000BE RID: 190
		private const double DefaultLinearScaleMajorTickMarkWidth = 4.0;

		// Token: 0x040000BF RID: 191
		private const double DefaultLinearScaleMinorTickMarkWidth = 3.0;

		// Token: 0x040000C0 RID: 192
		private const double DefaultRadialScaleMajorTickMarkWidth = 8.0;

		// Token: 0x040000C1 RID: 193
		private const double DefaultRadialScaleMinorTickMarkWidth = 3.0;

		// Token: 0x040000C2 RID: 194
		private const double DefaultLinearScaleMajorTickMarkLength = 15.0;

		// Token: 0x040000C3 RID: 195
		private const double DefaultLinearScaleMinorTickMarkLength = 9.0;

		// Token: 0x040000C4 RID: 196
		private const double DefaultRadialScaleMajorTickMarkLength = 14.0;

		// Token: 0x040000C5 RID: 197
		private const double DefaultRadialScaleMinorTickMarkLength = 8.0;

		// Token: 0x040000C6 RID: 198
		private const double DefaultLinearGaugePointerWidth = 20.0;

		// Token: 0x040000C7 RID: 199
		private const double DefaultRadialGaugePointerWidth = 15.0;

		// Token: 0x040000C8 RID: 200
		private const double DefaultLinearGaugePointerMarkerLength = 20.0;

		// Token: 0x040000C9 RID: 201
		private const double DefaultRadialGaugePointerMarkerLength = 10.0;

		// Token: 0x040000CA RID: 202
		private const double DefaultDefaultScalePinWidth = 6.0;

		// Token: 0x040000CB RID: 203
		private const double DefaultDefaultScalePinLength = 6.0;

		// Token: 0x040000CC RID: 204
		private const int DefaultGaugeScaleShadowOffset = 1;

		// Token: 0x040000CD RID: 205
		private const int DefaultGaugePointerShadowOffset = 2;

		// Token: 0x02000320 RID: 800
		private class Cloner
		{
			// Token: 0x0600171C RID: 5916 RVA: 0x0003657A File Offset: 0x0003477A
			public Cloner(UpgradeImpl2005 upgrader)
			{
				this.m_upgrader = upgrader;
				this.m_nameTable = new Dictionary<string, string>();
				this.m_clonedObjects = new ArrayList();
				this.m_textboxNameValueExprTable = new Dictionary<string, string>();
			}

			// Token: 0x0600171D RID: 5917 RVA: 0x000365AC File Offset: 0x000347AC
			public object Clone(object obj)
			{
				if (obj is ReportObject)
				{
					StructMapping structMapping = (StructMapping)TypeMapper.GetTypeMapping(SerializerHost2005.GetSubstituteType(obj.GetType(), true));
					object obj2 = this.CloneStructure(obj, structMapping);
					this.m_clonedObjects.Add(obj2);
					return obj2;
				}
				if (obj is IList)
				{
					object obj2 = this.CloneList((IList)obj);
					this.m_clonedObjects.Add(obj2);
					return obj2;
				}
				return obj;
			}

			// Token: 0x0600171E RID: 5918 RVA: 0x00036618 File Offset: 0x00034818
			private object CloneStructure(object obj, StructMapping mapping)
			{
				Type type = mapping.Type;
				object obj2 = Activator.CreateInstance(type);
				foreach (MemberMapping memberMapping in mapping.Members)
				{
					object value = memberMapping.GetValue(obj);
					memberMapping.SetValue(obj2, this.Clone(value));
				}
				if (obj2 is IGlobalNamedObject)
				{
					string text;
					if (obj2 is Microsoft.ReportingServices.RdlObjectModel.Group)
					{
						text = this.m_upgrader.GetParentReportItemName((IContainedObject)obj) + "_Group";
					}
					else
					{
						text = type.Name;
						text = char.ToLower(text[0], CultureInfo.InvariantCulture).ToString() + text.Substring(1);
					}
					string text2 = this.m_upgrader.UniqueName(text, obj2, false);
					this.m_nameTable.Add(((IGlobalNamedObject)obj2).Name, text2);
					((IGlobalNamedObject)obj2).Name = text2;
				}
				return obj2;
			}

			// Token: 0x0600171F RID: 5919 RVA: 0x00036724 File Offset: 0x00034924
			private object CloneList(IList obj)
			{
				IList list = (IList)Activator.CreateInstance(obj.GetType());
				foreach (object obj2 in obj)
				{
					list.Add(this.Clone(obj2));
				}
				return list;
			}

			// Token: 0x06001720 RID: 5920 RVA: 0x0003678C File Offset: 0x0003498C
			public void FixReferences()
			{
				foreach (object obj in this.m_clonedObjects)
				{
					this.FixReferences(obj);
				}
			}

			// Token: 0x06001721 RID: 5921 RVA: 0x000367E0 File Offset: 0x000349E0
			public void FixReferences(object obj)
			{
				if (obj is IList)
				{
					foreach (object obj2 in ((IList)obj))
					{
						this.FixReferences(obj2);
					}
					return;
				}
				if (!(obj is ReportObject))
				{
					return;
				}
				Type type = obj.GetType();
				StructMapping structMapping = (StructMapping)TypeMapper.GetTypeMapping(type);
				if (typeof(ReportItem).IsAssignableFrom(type))
				{
					ReportItem reportItem = (ReportItem)obj;
					reportItem.RepeatWith = this.FixReference(reportItem.RepeatWith);
					if (type == typeof(Microsoft.ReportingServices.RdlObjectModel.Rectangle))
					{
						((Microsoft.ReportingServices.RdlObjectModel.Rectangle)reportItem).LinkToChild = this.FixReference(((Microsoft.ReportingServices.RdlObjectModel.Rectangle)reportItem).LinkToChild);
					}
					else if (type == typeof(Textbox))
					{
						((Textbox)obj).HideDuplicates = this.FixReference(((Textbox)obj).HideDuplicates);
					}
				}
				else if (type == typeof(Visibility))
				{
					((Visibility)obj).ToggleItem = this.FixReference(((Visibility)obj).ToggleItem);
				}
				else if (type == typeof(UserSort))
				{
					((UserSort)obj).SortExpressionScope = this.FixReference(((UserSort)obj).SortExpressionScope);
				}
				foreach (MemberMapping memberMapping in structMapping.Members)
				{
					object value = memberMapping.GetValue(obj);
					if (typeof(IExpression).IsAssignableFrom(memberMapping.Type))
					{
						memberMapping.SetValue(obj, this.FixReference((IExpression)value));
					}
					else
					{
						this.FixReferences(value);
					}
				}
			}

			// Token: 0x06001722 RID: 5922 RVA: 0x000369CC File Offset: 0x00034BCC
			private string FixReference(string value)
			{
				if (value != null && this.m_nameTable.ContainsKey(value))
				{
					return this.m_nameTable[value];
				}
				return value;
			}

			// Token: 0x06001723 RID: 5923 RVA: 0x000369F0 File Offset: 0x00034BF0
			private IExpression FixReference(IExpression value)
			{
				if (value != null && value.IsExpression)
				{
					string text = value.Expression;
					foreach (KeyValuePair<string, string> keyValuePair in this.m_nameTable)
					{
						text = this.m_upgrader.ReplaceReference(text, keyValuePair.Key, keyValuePair.Value);
					}
					text = this.m_upgrader.ReplaceReportItemReferenceWithValue(text, this.m_textboxNameValueExprTable);
					value = (IExpression)Activator.CreateInstance(value.GetType());
					value.Expression = text;
				}
				return value;
			}

			// Token: 0x06001724 RID: 5924 RVA: 0x00036A9C File Offset: 0x00034C9C
			public void AddNameMapping(string oldName, string newName)
			{
				this.m_nameTable.Add(oldName, newName);
			}

			// Token: 0x17000727 RID: 1831
			// (get) Token: 0x06001725 RID: 5925 RVA: 0x00036AAB File Offset: 0x00034CAB
			public Dictionary<string, string> TextboxNameValueExprTable
			{
				get
				{
					return this.m_textboxNameValueExprTable;
				}
			}

			// Token: 0x06001726 RID: 5926 RVA: 0x00036AB4 File Offset: 0x00034CB4
			public void ApplySubTotalStyleOverrides(ReportItem item, SubtotalStyle2005 style)
			{
				if (item == null || style == null)
				{
					return;
				}
				if (item is Textbox)
				{
					if (item.Style == null)
					{
						item.Style = new Style();
					}
					Textbox textbox = (Textbox)item;
					Style style2 = textbox.Style;
					Style style3 = null;
					Style style4 = null;
					if (textbox.Paragraphs.Count > 0)
					{
						Paragraph paragraph = textbox.Paragraphs[0];
						if (paragraph.Style == null)
						{
							style3 = (paragraph.Style = new Style());
						}
						else
						{
							style3 = paragraph.Style;
						}
						if (paragraph.TextRuns.Count > 0)
						{
							TextRun textRun = paragraph.TextRuns[0];
							if (textRun.Style == null)
							{
								style4 = (textRun.Style = new Style());
							}
							else
							{
								style4 = textRun.Style;
							}
						}
					}
					this.ApplySubTotalStyleOverrides(style, style2, style3, style4);
					return;
				}
				if (item.Style != null)
				{
					this.ApplySubTotalStyleOverrides(style, item.Style, item.Style, item.Style);
				}
			}

			// Token: 0x06001727 RID: 5927 RVA: 0x00036BA8 File Offset: 0x00034DA8
			private void ApplySubTotalStyleOverrides(SubtotalStyle2005 subTotalStyle, Style style1, Style style2, Style style3)
			{
				foreach (MemberMapping memberMapping in ((StructMapping)TypeMapper.GetTypeMapping(typeof(Style))).Members)
				{
					if (memberMapping.HasValue(subTotalStyle) && !subTotalStyle.IsPropertyDefinedOnInitialize(memberMapping.Name))
					{
						string name = memberMapping.Name;
						if (name != null)
						{
							switch (name.Length)
							{
							case 5:
								if (!(name == "Color"))
								{
									goto IL_0237;
								}
								goto IL_0222;
							case 6:
								if (!(name == "Format"))
								{
									goto IL_0237;
								}
								goto IL_0222;
							case 7:
							case 11:
							case 12:
							case 13:
								goto IL_0237;
							case 8:
							{
								char c = name[0];
								if (c != 'C')
								{
									if (c != 'F')
									{
										if (c != 'L')
										{
											goto IL_0237;
										}
										if (!(name == "Language"))
										{
											goto IL_0237;
										}
										goto IL_0222;
									}
									else
									{
										if (!(name == "FontSize"))
										{
											goto IL_0237;
										}
										goto IL_0222;
									}
								}
								else
								{
									if (!(name == "Calendar"))
									{
										goto IL_0237;
									}
									goto IL_0222;
								}
								break;
							}
							case 9:
							{
								char c = name[0];
								if (c != 'F')
								{
									if (c != 'T')
									{
										goto IL_0237;
									}
									if (!(name == "TextAlign"))
									{
										goto IL_0237;
									}
								}
								else
								{
									if (!(name == "FontStyle"))
									{
										goto IL_0237;
									}
									goto IL_0222;
								}
								break;
							}
							case 10:
							{
								char c = name[4];
								if (c != 'F')
								{
									if (c != 'H')
									{
										if (c != 'W')
										{
											goto IL_0237;
										}
										if (!(name == "FontWeight"))
										{
											goto IL_0237;
										}
										goto IL_0222;
									}
									else if (!(name == "LineHeight"))
									{
										goto IL_0237;
									}
								}
								else
								{
									if (!(name == "FontFamily"))
									{
										goto IL_0237;
									}
									goto IL_0222;
								}
								break;
							}
							case 14:
							{
								char c = name[0];
								if (c != 'N')
								{
									if (c != 'T')
									{
										goto IL_0237;
									}
									if (!(name == "TextDecoration"))
									{
										goto IL_0237;
									}
									goto IL_0222;
								}
								else
								{
									if (!(name == "NumeralVariant"))
									{
										goto IL_0237;
									}
									goto IL_0222;
								}
								break;
							}
							case 15:
								if (!(name == "NumeralLanguage"))
								{
									goto IL_0237;
								}
								goto IL_0222;
							default:
								goto IL_0237;
							}
							if (style2 != null)
							{
								memberMapping.SetValue(style2, memberMapping.GetValue(subTotalStyle));
								continue;
							}
							continue;
							IL_0222:
							if (style3 != null)
							{
								memberMapping.SetValue(style3, memberMapping.GetValue(subTotalStyle));
								continue;
							}
							continue;
						}
						IL_0237:
						memberMapping.SetValue(style1, memberMapping.GetValue(subTotalStyle));
					}
				}
			}

			// Token: 0x0400071A RID: 1818
			private readonly UpgradeImpl2005 m_upgrader;

			// Token: 0x0400071B RID: 1819
			private readonly Dictionary<string, string> m_nameTable;

			// Token: 0x0400071C RID: 1820
			private readonly ArrayList m_clonedObjects;

			// Token: 0x0400071D RID: 1821
			private readonly Dictionary<string, string> m_textboxNameValueExprTable;
		}

		// Token: 0x02000321 RID: 801
		// (Invoke) Token: 0x06001729 RID: 5929
		internal delegate Microsoft.ReportingServices.RdlObjectModel.Group GroupAccessor(object member);

		// Token: 0x02000322 RID: 802
		// (Invoke) Token: 0x0600172D RID: 5933
		internal delegate IList<CustomProperty> CustomPropertiesAccessor(object member);

		// Token: 0x02000323 RID: 803
		// (Invoke) Token: 0x06001731 RID: 5937
		private delegate string AggregateFunctionFixup(string expression, int currentOffset, string specialFunctionName, int specialFunctionPos, int argumentsPos, int scopePos, int scopeLength, ref int offset);
	}
}
