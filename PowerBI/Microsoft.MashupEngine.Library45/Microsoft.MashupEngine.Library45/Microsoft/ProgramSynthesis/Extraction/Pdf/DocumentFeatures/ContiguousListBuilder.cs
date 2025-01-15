using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.Logging;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CAC RID: 3244
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ContiguousListBuilder<TCell> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x06005347 RID: 21319 RVA: 0x00106E9C File Offset: 0x0010509C
		public static AxisAlignedList<ContiguousList<TCell>> Build(QuadTree<TCell, PixelUnit> cells, AxisAlignedList<Alignment<TCell>> alignments)
		{
			if (cells.IsEmpty())
			{
				Logger.Instance.Debug("No cells for contiguous list recognition", null, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\ContiguousListBuilder.cs", 19);
				return AxisAlignedList<ContiguousList<TCell>>.Empty;
			}
			if (alignments.IsEmpty())
			{
				Logger.Instance.Debug("No alignments for contiguous list recognition", null, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\ContiguousListBuilder.cs", 24);
				return AxisAlignedList<ContiguousList<TCell>>.Empty;
			}
			if (cells.Any((TCell cell) => cell.ContiguousLists.Vertical.Any<ContiguousList<TCell>>() || cell.ContiguousLists.Horizontal.Any<ContiguousList<TCell>>()))
			{
				throw new ArgumentException("cells", "These cells have already been built into contiguous lists.");
			}
			IStopwatchWrapper stopwatchWrapper = Logger.Instance.InfoTiming("Recognize Contiguous Lists", "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\ContiguousListBuilder.cs", 32);
			AxisAlignedList<ContiguousList<TCell>> axisAlignedList = new AxisAlignedList<ContiguousList<TCell>>(delegate(Axis axis)
			{
				ContiguousListBuilder<TCell>.<>c__DisplayClass0_1 CS$<>8__locals2 = new ContiguousListBuilder<TCell>.<>c__DisplayClass0_1();
				CS$<>8__locals2.axis = axis;
				CS$<>8__locals2.lists = new List<ContiguousList<TCell>>();
				using (IEnumerator<Alignment<TCell>> enumerator3 = alignments[CS$<>8__locals2.axis].GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						ContiguousListBuilder<TCell>.<>c__DisplayClass0_2 CS$<>8__locals3 = new ContiguousListBuilder<TCell>.<>c__DisplayClass0_2();
						CS$<>8__locals3.CS$<>8__locals1 = CS$<>8__locals2;
						CS$<>8__locals3.alignment = enumerator3.Current;
						Axis axis2 = CS$<>8__locals3.CS$<>8__locals1.axis.Perpendicular();
						IReadOnlyList<TCell> readOnlyList = (from cell in cells.OverlappingElements(CS$<>8__locals3.alignment.Range, axis2)
							where !CS$<>8__locals3.alignment.Contains(cell)
							select cell).ToList<TCell>();
						int num = 0;
						Bounds<PixelUnit> bounds = CS$<>8__locals3.alignment.Cells[num].PixelBounds;
						for (int i = 0; i < CS$<>8__locals3.alignment.Cells.Count; i++)
						{
							TCell tcell2 = CS$<>8__locals3.alignment.Cells[i];
							Bounds<PixelUnit> newBounds = Bounds<PixelUnit>.Join(new Bounds<PixelUnit>[] { bounds, tcell2.PixelBounds });
							IEnumerable<TCell> enumerable = readOnlyList.Where((TCell cell) => cell.PixelBounds.Overlaps(newBounds));
							Func<TCell, int> func;
							if ((func = CS$<>8__locals3.CS$<>8__locals1.<>9__7) == null)
							{
								func = (CS$<>8__locals3.CS$<>8__locals1.<>9__7 = (TCell cell) => cell.PixelBounds.BoundInDirection(CS$<>8__locals3.CS$<>8__locals1.axis.IncreasingDirection()));
							}
							List<TCell> list2 = enumerable.OrderByDescending(func).ToList<TCell>();
							if (list2.Any<TCell>())
							{
								CS$<>8__locals3.<Build>g__AddContiguousList|5(num, i);
							}
							bounds = newBounds;
							foreach (TCell tcell3 in ((IEnumerable<TCell>)list2))
							{
								if (tcell3.PixelBounds.Overlaps(bounds))
								{
									TCell tcell4 = default(TCell);
									int j;
									for (j = i; j >= num; j--)
									{
										tcell4 = CS$<>8__locals3.alignment.Cells[j];
										if (tcell3.PixelBounds.Overlaps(tcell4.PixelBounds, axis2))
										{
											break;
										}
									}
									if (tcell3.IsAfter(tcell4, CS$<>8__locals3.CS$<>8__locals1.axis))
									{
										num = j + 1;
									}
									else
									{
										for (int k = num; k <= i; k++)
										{
											if (CS$<>8__locals3.alignment.Cells[k].IsAfter(tcell3, CS$<>8__locals3.CS$<>8__locals1.axis))
											{
												num = k;
												break;
											}
										}
									}
									int num2 = i + 1 - num;
									IEnumerable<TCell> enumerable2 = CS$<>8__locals3.alignment.Cells.Skip(num).Take(num2);
									if (num2 > 0)
									{
										bounds = Bounds<PixelUnit>.Join(enumerable2.Select((TCell cell) => cell.PixelBounds));
									}
									if (num2 < 2)
									{
										break;
									}
								}
							}
						}
						CS$<>8__locals3.<Build>g__AddContiguousList|5(num, CS$<>8__locals3.alignment.Cells.Count);
					}
				}
				return (from list in CS$<>8__locals2.lists.SortAndRemoveSubordinates(CompareByCellCountDescending<TCell>.Instance, (ContiguousList<TCell> a, ContiguousList<TCell> b) => b.IsCellSubsetOf(a))
					orderby list.PixelBounds.BoundInDirection(CS$<>8__locals2.axis.DecreasingDirection())
					select list).ToList<ContiguousList<TCell>>();
			});
			foreach (ContiguousList<TCell> contiguousList in axisAlignedList)
			{
				foreach (TCell tcell in contiguousList.Cells)
				{
					tcell.AddToContiguousList(contiguousList);
				}
			}
			stopwatchWrapper.Stop();
			Logger.Instance.Debug("Contiguous Lists", axisAlignedList, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\ContiguousListBuilder.cs", 126);
			return axisAlignedList;
		}
	}
}
