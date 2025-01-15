using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Utils.IObservable;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CF1 RID: 3313
	[NullableContext(1)]
	[Nullable(0)]
	internal static class MultiPageTableBuilder
	{
		// Token: 0x0600550C RID: 21772 RVA: 0x0010B1E8 File Offset: 0x001093E8
		public static IObservable<IPdfTable> MergeCrossPageTables(IObservable<DependencyGraph> graphs)
		{
			return Observable.Create<IPdfTable>(delegate([Nullable(new byte[] { 0, 1 })] IObserver<IPdfTable> observer)
			{
				List<PotentialMultiPageTableComponent> matchingComponents = new List<PotentialMultiPageTableComponent>();
				int headerRowCount = 0;
				IConnectableObservable<List<IPdfTable>> connectableObservable = graphs.Select(delegate(DependencyGraph graph)
				{
					QuadTree<ICell, PixelUnit> cells = graph.BuildCells();
					IReadOnlyList<PotentialMultiPageTableComponent> readOnlyList = (from table in graph.BuildSimpleTables()
						select new PotentialMultiPageTableComponent(table, graph.GetPageBounds(), cells) into table
						orderby table.PixelBounds.Top
						select table).ToList<PotentialMultiPageTableComponent>();
					List<IPdfTable> list = new List<IPdfTable>();
					int num = graph.GetPageBounds().Height();
					PotentialMultiPageTableComponent continuedTable = null;
					PotentialMultiPageTableComponent possibleContinuableTable = MultiPageTableBuilder.ContinuableTable(num, readOnlyList);
					if (matchingComponents.Any<PotentialMultiPageTableComponent>())
					{
						PotentialMultiPageTableComponent potentialMultiPageTableComponent = MultiPageTableBuilder.ContinuedTable(num, readOnlyList);
						int? num2 = MultiPageTableBuilder.TablesMatch(matchingComponents, potentialMultiPageTableComponent, graph.GetPageBounds());
						if (num2 != null)
						{
							continuedTable = potentialMultiPageTableComponent;
							matchingComponents.Add(potentialMultiPageTableComponent);
							headerRowCount = num2.Value;
						}
						if (possibleContinuableTable == null || possibleContinuableTable != continuedTable)
						{
							list.Add(MultiPageTableBuilder.MergeTableComponents(matchingComponents, headerRowCount));
							matchingComponents.Clear();
						}
					}
					if (matchingComponents.IsEmpty<PotentialMultiPageTableComponent>() && possibleContinuableTable != null)
					{
						matchingComponents.Add(possibleContinuableTable);
					}
					list.AddRange(from table in readOnlyList
						where table != continuedTable && table != possibleContinuableTable
						select table.TableComponent);
					return list;
				}).Publish<List<IPdfTable>>();
				CompositeDisposable compositeDisposable = new CompositeDisposable();
				CompositeDisposable compositeDisposable2 = compositeDisposable;
				IObservable<IPdfTable>[] array = new IObservable<IPdfTable>[2];
				array[0] = connectableObservable.SelectMany((List<IPdfTable> completedTables) => completedTables);
				Func<IPdfTable> <>9__8;
				array[1] = connectableObservable.Last<List<IPdfTable>>().Select(delegate(List<IPdfTable> _)
				{
					bool flag = matchingComponents.Any<PotentialMultiPageTableComponent>();
					Func<IPdfTable> func;
					if ((func = <>9__8) == null)
					{
						func = (<>9__8 = () => MultiPageTableBuilder.MergeTableComponents(matchingComponents, headerRowCount));
					}
					return flag.Then(func);
				}).SelectValues<IPdfTable>();
				compositeDisposable2.Add(array.Merge<IPdfTable>().Subscribe(observer));
				compositeDisposable.Add(connectableObservable.Connect());
				return compositeDisposable;
			});
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x0010B208 File Offset: 0x00109408
		private static IPdfTable MergeTableComponents(IReadOnlyList<PotentialMultiPageTableComponent> components, int headerRowCount)
		{
			IReadOnlyList<IProsePdfTable<ICell>> componentTables = components.Select((PotentialMultiPageTableComponent component) => component.TableComponent).ToList<IProsePdfTable<ICell>>();
			return componentTables.MaybeOnly<IProsePdfTable<ICell>>().Cast<IPdfTable>().OrElseCompute(() => new MultiPageTable(componentTables, headerRowCount));
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x0010B278 File Offset: 0x00109478
		private static PotentialMultiPageTableComponent ContinuedTable(int pageHeight, IReadOnlyList<PotentialMultiPageTableComponent> topSortedPageTables)
		{
			return topSortedPageTables.FirstOrDefault((PotentialMultiPageTableComponent tableComponent) => tableComponent.PixelBounds.Top < pageHeight / 4);
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x0010B2A4 File Offset: 0x001094A4
		private static PotentialMultiPageTableComponent ContinuableTable(int pageHeight, IReadOnlyList<PotentialMultiPageTableComponent> pageSimpleTables)
		{
			return pageSimpleTables.OrderByDescending((PotentialMultiPageTableComponent tableComponent) => tableComponent.PixelBounds.Bottom).FirstOrDefault((PotentialMultiPageTableComponent tableComponent) => tableComponent.PixelBounds.Bottom > pageHeight * 4 / 5);
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x0010B2F4 File Offset: 0x001094F4
		private static int? TablesMatch(IReadOnlyList<PotentialMultiPageTableComponent> currentComponents, PotentialMultiPageTableComponent possibleMatchingComponent, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds)
		{
			if (possibleMatchingComponent == null)
			{
				return null;
			}
			IReadOnlyList<IReadOnlyList<Record<IReadOnlyList<string>, IReadOnlyList<string>>>> readOnlyList = currentComponents.Select(delegate(PotentialMultiPageTableComponent currentComponent)
			{
				RectangularArray<ICell> rectangularArray = currentComponent.TableComponent.Table;
				RectangularArray<string> rectangularArray2 = rectangularArray.Select<string>(delegate([Nullable(2)] ICell cell)
				{
					if (cell == null)
					{
						return null;
					}
					return cell.Content;
				});
				rectangularArray = possibleMatchingComponent.TableComponent.Table;
				return MultiPageTableBuilder.RectangularArrayHelpers.RepeatedRowsIgnoringNullFromTop<string>(rectangularArray2, rectangularArray.Select<string>(delegate([Nullable(2)] ICell cell)
				{
					if (cell == null)
					{
						return null;
					}
					return cell.Content;
				})).ToList<Record<IReadOnlyList<string>, IReadOnlyList<string>>>();
			}).ToList<IReadOnlyList<Record<IReadOnlyList<string>, IReadOnlyList<string>>>>();
			int num = readOnlyList.Min((IReadOnlyList<Record<IReadOnlyList<string>, IReadOnlyList<string>>> headers) => headers.Count);
			if (num == 0)
			{
				return null;
			}
			PotentialMultiPageTableComponent potentialMultiPageTableComponent = currentComponents.Last<PotentialMultiPageTableComponent>();
			int num2 = (int)Math.Floor((double)((float)Math.Min(pageBounds.Height(), pageBounds.Width()) * 0.125f));
			if (potentialMultiPageTableComponent.PixelBounds.Bottom > pageBounds.Height() - num2)
			{
				return new int?(num);
			}
			int count = readOnlyList.Last<IReadOnlyList<Record<IReadOnlyList<string>, IReadOnlyList<string>>>>().Count;
			if (possibleMatchingComponent.TableComponent.Table.Height == count)
			{
				return new int?(num);
			}
			if (possibleMatchingComponent.TableComponent.Table.Row(count).Max(delegate(ICell cell)
			{
				if (cell == null)
				{
					return 0;
				}
				return cell.PixelBounds.Height();
			}) + num2 > potentialMultiPageTableComponent.GapPixelsBelow)
			{
				return new int?(num);
			}
			return null;
		}

		// Token: 0x04002683 RID: 9859
		private const float ContentMarginProportion = 0.125f;

		// Token: 0x02000CF2 RID: 3314
		[NullableContext(0)]
		private static class RectangularArrayHelpers
		{
			// Token: 0x06005511 RID: 21777 RVA: 0x0010B445 File Offset: 0x00109645
			[NullableContext(1)]
			[return: Nullable(new byte[] { 1, 0, 1, 2, 1, 2 })]
			public static IEnumerable<Record<IReadOnlyList<TValue>, IReadOnlyList<TValue>>> RepeatedRowsIgnoringNullFromTop<TValue>([Nullable(new byte[] { 0, 2 })] RectangularArray<TValue> first, [Nullable(new byte[] { 0, 2 })] RectangularArray<TValue> second) where TValue : class
			{
				return MultiPageTableBuilder.RectangularArrayHelpers.PairwiseRowLists<TValue>(first, second).TakeWhile((Record<IReadOnlyList<TValue>, IReadOnlyList<TValue>> record) => record.Item1.Collect<TValue>().SequenceEqual(record.Item2.Collect<TValue>()));
			}

			// Token: 0x06005512 RID: 21778 RVA: 0x0010B474 File Offset: 0x00109674
			[NullableContext(2)]
			[return: Nullable(new byte[] { 1, 0, 1, 1, 1, 1 })]
			private static IEnumerable<Record<IReadOnlyList<TValue>, IReadOnlyList<TValue>>> PairwiseRowLists<TValue>([Nullable(new byte[] { 0, 1 })] RectangularArray<TValue> first, [Nullable(new byte[] { 0, 1 })] RectangularArray<TValue> second)
			{
				IEnumerable<IEnumerable<TValue>> enumerable = first.Rows();
				Func<IEnumerable<TValue>, IReadOnlyList<TValue>> func;
				if ((func = MultiPageTableBuilder.RectangularArrayHelpers.<>c__1<TValue>.<>9__1_0) == null)
				{
					Func<IEnumerable<TValue>, IReadOnlyList<TValue>> func2 = (MultiPageTableBuilder.RectangularArrayHelpers.<>c__1<TValue>.<>9__1_0 = ([Nullable(new byte[] { 1, 0 })] IEnumerable<TValue> row) => row.ToList<TValue>());
					func = func2;
				}
				IEnumerable<IReadOnlyList<TValue>> enumerable2 = enumerable.Select(func);
				IEnumerable<IEnumerable<TValue>> enumerable3 = second.Rows();
				Func<IEnumerable<TValue>, IReadOnlyList<TValue>> func3;
				if ((func3 = MultiPageTableBuilder.RectangularArrayHelpers.<>c__1<TValue>.<>9__1_1) == null)
				{
					Func<IEnumerable<TValue>, IReadOnlyList<TValue>> func2 = (MultiPageTableBuilder.RectangularArrayHelpers.<>c__1<TValue>.<>9__1_1 = ([Nullable(new byte[] { 1, 0 })] IEnumerable<TValue> row) => row.ToList<TValue>());
					func3 = func2;
				}
				return enumerable2.ZipWith(enumerable3.Select(func3));
			}
		}
	}
}
