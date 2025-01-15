using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CEE RID: 3310
	[NullableContext(1)]
	[Nullable(0)]
	internal class MultiPageTable : IPdfTable
	{
		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x060054EF RID: 21743 RVA: 0x0010ABFF File Offset: 0x00108DFF
		public int StartingPageIndex { get; }

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x060054F0 RID: 21744 RVA: 0x0010AC07 File Offset: 0x00108E07
		public int EndingPageIndex { get; }

		// Token: 0x060054F1 RID: 21745 RVA: 0x0010AC0F File Offset: 0x00108E0F
		[return: Nullable(new byte[] { 1, 2 })]
		public string[,] GetTextTable()
		{
			return this.CombinedTable.ToTextTable<ICell>();
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x060054F2 RID: 21746 RVA: 0x0010AC1C File Offset: 0x00108E1C
		// (set) Token: 0x060054F3 RID: 21747 RVA: 0x0010AC24 File Offset: 0x00108E24
		[Nullable(2)]
		public TableIdentity TableIdentity
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			internal set;
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x060054F4 RID: 21748 RVA: 0x0010AC2D File Offset: 0x00108E2D
		[Nullable(2)]
		public string DisplayName
		{
			[NullableContext(2)]
			get
			{
				TableIdentity tableIdentity = this.TableIdentity;
				if (tableIdentity == null)
				{
					return null;
				}
				return tableIdentity.Identifier;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x060054F5 RID: 21749 RVA: 0x0001AFD7 File Offset: 0x000191D7
		public TableKind Kind
		{
			get
			{
				return TableKind.Inferred;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x060054F6 RID: 21750 RVA: 0x0010AC40 File Offset: 0x00108E40
		public int Width
		{
			get
			{
				return this.CombinedTable.Width;
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x060054F7 RID: 21751 RVA: 0x0010AC5C File Offset: 0x00108E5C
		public int Height
		{
			get
			{
				return this.CombinedTable.Height;
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x060054F8 RID: 21752 RVA: 0x0010AC77 File Offset: 0x00108E77
		public int? RecognizedHeaderRowCount
		{
			get
			{
				return new int?(this._recognizedHeaderRowCount);
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x060054F9 RID: 21753 RVA: 0x0010AC84 File Offset: 0x00108E84
		internal IReadOnlyList<IProsePdfTable<ICell>> TableComponents { get; }

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x060054FA RID: 21754 RVA: 0x0010AC8C File Offset: 0x00108E8C
		[Nullable(new byte[] { 0, 2 })]
		internal RectangularArray<ICell> CombinedTable
		{
			[return: Nullable(new byte[] { 0, 2 })]
			get;
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x0010AC94 File Offset: 0x00108E94
		public MultiPageTable(IEnumerable<IProsePdfTable<ICell>> tableComponents, int headerRowCount)
		{
			this.TableComponents = tableComponents.ToList<IProsePdfTable<ICell>>();
			this.StartingPageIndex = this.TableComponents.Min((IProsePdfTable<ICell> table) => table.StartingPageIndex);
			this.EndingPageIndex = this.TableComponents.Max((IProsePdfTable<ICell> table) => table.EndingPageIndex);
			this._recognizedHeaderRowCount = headerRowCount;
			this.CombinedTable = this.TableComponents.Select((IProsePdfTable<ICell> pdfTable) => pdfTable.Table).Aggregate(new Func<RectangularArray<ICell>, RectangularArray<ICell>, RectangularArray<ICell>>(this.JoinTablesRemovingRepeatHeaders));
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x0010AD5C File Offset: 0x00108F5C
		[return: Nullable(new byte[] { 0, 2 })]
		private RectangularArray<ICell> JoinTablesRemovingRepeatHeaders([Nullable(new byte[] { 0, 2 })] RectangularArray<ICell> topTable, [Nullable(new byte[] { 0, 2 })] RectangularArray<ICell> bottomTable)
		{
			IEnumerable<IEnumerable<ICell>> enumerable = topTable.Rows().Take(this._recognizedHeaderRowCount);
			IEnumerable<IEnumerable<ICell>> enumerable2 = bottomTable.Rows().Take(this._recognizedHeaderRowCount);
			IReadOnlyList<bool> readOnlyList = this.BuildNonEmptyHeaderLookup(enumerable);
			IReadOnlyList<bool> readOnlyList2 = this.BuildNonEmptyHeaderLookup(enumerable2);
			MultiPageTable.<>c__DisplayClass29_0 CS$<>8__locals1;
			CS$<>8__locals1.topTableHeaderColumns = MultiPageTable.<JoinTablesRemovingRepeatHeaders>g__RowsToColumnStrings|29_0(enumerable);
			CS$<>8__locals1.bottomTableHeaderColumns = MultiPageTable.<JoinTablesRemovingRepeatHeaders>g__RowsToColumnStrings|29_0(enumerable2);
			int[] array = new int[topTable.Width];
			int[] array2 = new int[bottomTable.Width];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (num2 < readOnlyList.Count || num3 < readOnlyList2.Count)
			{
				Optional<bool> optional = readOnlyList.MaybeElementAt(num2);
				Optional<bool> optional2 = readOnlyList2.MaybeElementAt(num3);
				if (optional == optional2)
				{
					if ((!optional.OrElse(true) && !optional2.OrElse(true)) || MultiPageTable.<JoinTablesRemovingRepeatHeaders>g__HeadersMatch|29_1(num2, num3, ref CS$<>8__locals1))
					{
						array[num2++] = num;
						array2[num3++] = num;
					}
					else
					{
						bool flag = false;
						int num4 = 1;
						while (num2 + num4 < readOnlyList.Count && num3 + num4 < readOnlyList2.Count)
						{
							if (MultiPageTable.<JoinTablesRemovingRepeatHeaders>g__HeadersMatch|29_1(num2, num3 + num4, ref CS$<>8__locals1))
							{
								flag = true;
								for (int i = 0; i < num4; i++)
								{
									array2[num3++] = num++;
								}
								break;
							}
							if (MultiPageTable.<JoinTablesRemovingRepeatHeaders>g__HeadersMatch|29_1(num2 + num4, num3, ref CS$<>8__locals1))
							{
								flag = true;
								for (int j = 0; j < num4; j++)
								{
									array[num2++] = num++;
								}
								break;
							}
							num4++;
						}
						if (flag)
						{
							array[num2++] = num;
							array2[num3++] = num;
						}
						else
						{
							array[num2++] = num++;
							array2[num3++] = num;
						}
					}
				}
				else if (!optional.OrElse(true))
				{
					array[num2++] = num;
				}
				else if (!optional2.OrElse(true))
				{
					array2[num3++] = num;
				}
				else if (optional.HasValue)
				{
					array[num2++] = num;
				}
				else if (optional2.HasValue)
				{
					array2[num3++] = num;
				}
				else
				{
					array[num2++] = num;
					array2[num3++] = num;
				}
				num++;
			}
			int num5 = num;
			RectangularArray<ICell> rectangularArray = bottomTable.Section(0, this._recognizedHeaderRowCount, bottomTable.Width, bottomTable.Height);
			int num6 = topTable.Height + rectangularArray.Height;
			RectangularArray<ICell> rectangularArray2 = new RectangularArray<ICell>(num5, num6);
			this.FillValues(topTable, rectangularArray2, array, 0);
			this.FillValues(rectangularArray, rectangularArray2, array2, topTable.Height);
			return rectangularArray2;
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x0010B015 File Offset: 0x00109215
		private IReadOnlyList<bool> BuildNonEmptyHeaderLookup([Nullable(new byte[] { 1, 1, 2 })] IEnumerable<IEnumerable<ICell>> headerRows)
		{
			return (from column in headerRows.Transpose<ICell>()
				select column.Any((ICell x) => x != null)).ToList<bool>();
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x0010B048 File Offset: 0x00109248
		private void FillValues([Nullable(new byte[] { 0, 2 })] RectangularArray<ICell> tableComponent, [Nullable(new byte[] { 0, 2 })] RectangularArray<ICell> destinationTable, IReadOnlyList<int> xIndexLookup, int startY)
		{
			for (int i = 0; i < tableComponent.Width; i++)
			{
				int num = xIndexLookup[i];
				for (int j = 0; j < tableComponent.Height; j++)
				{
					destinationTable[num, j + startY] = tableComponent[i, j];
				}
			}
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x0010B096 File Offset: 0x00109296
		[CompilerGenerated]
		[return: Nullable(new byte[] { 1, 1, 2 })]
		internal static IReadOnlyList<IReadOnlyList<string>> <JoinTablesRemovingRepeatHeaders>g__RowsToColumnStrings|29_0([Nullable(new byte[] { 1, 1, 2 })] IEnumerable<IEnumerable<ICell>> rows)
		{
			return (from col in rows.Transpose<ICell>()
				select col.Select(delegate(ICell cell)
				{
					if (cell == null)
					{
						return null;
					}
					return cell.Content;
				}).ToList<string>()).ToList<List<string>>();
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x0010B0C8 File Offset: 0x001092C8
		[CompilerGenerated]
		internal static bool <JoinTablesRemovingRepeatHeaders>g__HeadersMatch|29_1(int topIndex, int bottomIndex, ref MultiPageTable.<>c__DisplayClass29_0 A_2)
		{
			if (topIndex < A_2.topTableHeaderColumns.Count && bottomIndex < A_2.bottomTableHeaderColumns.Count)
			{
				return A_2.topTableHeaderColumns[topIndex].Zip(A_2.bottomTableHeaderColumns[bottomIndex], (string a, string b) => a != null && b != null && object.Equals(a, b)).Any((bool b) => b);
			}
			return false;
		}

		// Token: 0x04002674 RID: 9844
		private readonly int _recognizedHeaderRowCount;
	}
}
