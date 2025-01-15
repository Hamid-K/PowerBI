using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017D8 RID: 6104
	public abstract class JoinAlgorithm
	{
		// Token: 0x06009A49 RID: 39497
		public abstract bool Supports(TableTypeAlgebra.JoinKind joinKind);

		// Token: 0x06009A4A RID: 39498
		public abstract IEnumerable<IValueReference> Join(JoinAlgorithm.JoinParameters parameters);

		// Token: 0x06009A4B RID: 39499 RVA: 0x001FEC94 File Offset: 0x001FCE94
		private static Value GetKey(RecordValue row, int[] columns)
		{
			Value[] array = new Value[columns.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = row[columns[i]];
			}
			return ListValue.New(array);
		}

		// Token: 0x06009A4C RID: 39500 RVA: 0x001FECCA File Offset: 0x001FCECA
		private static RecordValue GetRow(JoinAlgorithm.JoinSide side, RecordValue record, RecordValue otherRecord, Keys joinKeys, JoinColumn[] joinColumns)
		{
			if (side == JoinAlgorithm.JoinSide.Left)
			{
				return JoinAlgorithm.GetRow(record, otherRecord, joinKeys, joinColumns);
			}
			return JoinAlgorithm.GetRow(otherRecord, record, joinKeys, joinColumns);
		}

		// Token: 0x06009A4D RID: 39501 RVA: 0x001FECE8 File Offset: 0x001FCEE8
		public static RecordValue GetRow(RecordValue left, RecordValue right, Keys joinKeys, JoinColumn[] joinColumns)
		{
			IValueReference[] array = new IValueReference[joinColumns.Length];
			for (int i = 0; i < joinColumns.Length; i++)
			{
				JoinColumn joinColumn = joinColumns[i];
				if (joinColumn.Left)
				{
					IValueReference[] array2 = array;
					int num = i;
					IValueReference valueReference2;
					if (left == null)
					{
						IValueReference valueReference = Value.Null;
						valueReference2 = valueReference;
					}
					else
					{
						valueReference2 = left.GetReference(joinColumn.LeftColumn);
					}
					array2[num] = valueReference2;
				}
				else
				{
					IValueReference[] array3 = array;
					int num2 = i;
					IValueReference valueReference3;
					if (right == null)
					{
						IValueReference valueReference = Value.Null;
						valueReference3 = valueReference;
					}
					else
					{
						valueReference3 = right.GetReference(joinColumn.RightColumn);
					}
					array3[num2] = valueReference3;
				}
			}
			return RecordValue.New(joinKeys, array);
		}

		// Token: 0x04005172 RID: 20850
		public static readonly JoinAlgorithm Dynamic = new JoinAlgorithm.DynamicJoinAlgorithm();

		// Token: 0x04005173 RID: 20851
		public static readonly JoinAlgorithm PairwiseHash = new JoinAlgorithm.PairwiseHashJoinAlgorithm();

		// Token: 0x04005174 RID: 20852
		public static readonly JoinAlgorithm SortMerge = new JoinAlgorithm.SortMergeJoinAlgorithm();

		// Token: 0x04005175 RID: 20853
		public static readonly JoinAlgorithm LeftHash = new JoinAlgorithm.HashJoinAlgorithm(JoinAlgorithm.JoinSide.Left);

		// Token: 0x04005176 RID: 20854
		public static readonly JoinAlgorithm RightHash = new JoinAlgorithm.HashJoinAlgorithm(JoinAlgorithm.JoinSide.Right);

		// Token: 0x04005177 RID: 20855
		public static readonly JoinAlgorithm LeftIndex = new JoinAlgorithm.IndexJoinAlgorithm(JoinAlgorithm.JoinSide.Left);

		// Token: 0x04005178 RID: 20856
		public static readonly JoinAlgorithm RightIndex = new JoinAlgorithm.IndexJoinAlgorithm(JoinAlgorithm.JoinSide.Right);

		// Token: 0x04005179 RID: 20857
		public static readonly JoinAlgorithm LeftAnti = new JoinAlgorithm.AntiSemiJoinAlgorithm(JoinAlgorithm.JoinSide.Left, false);

		// Token: 0x0400517A RID: 20858
		public static readonly JoinAlgorithm RightAnti = new JoinAlgorithm.AntiSemiJoinAlgorithm(JoinAlgorithm.JoinSide.Right, false);

		// Token: 0x0400517B RID: 20859
		public static readonly JoinAlgorithm LeftSemi = new JoinAlgorithm.AntiSemiJoinAlgorithm(JoinAlgorithm.JoinSide.Left, true);

		// Token: 0x0400517C RID: 20860
		public static readonly JoinAlgorithm RightSemi = new JoinAlgorithm.AntiSemiJoinAlgorithm(JoinAlgorithm.JoinSide.Right, true);

		// Token: 0x020017D9 RID: 6105
		private class DynamicJoinAlgorithm : JoinAlgorithm
		{
			// Token: 0x06009A50 RID: 39504 RVA: 0x00002139 File Offset: 0x00000339
			public override bool Supports(TableTypeAlgebra.JoinKind joinKind)
			{
				return true;
			}

			// Token: 0x06009A51 RID: 39505 RVA: 0x001FEDE8 File Offset: 0x001FCFE8
			public override IEnumerable<IValueReference> Join(JoinAlgorithm.JoinParameters parameters)
			{
				switch (parameters.JoinKind)
				{
				case TableTypeAlgebra.JoinKind.LeftAnti:
					return JoinAlgorithm.LeftAnti.Join(parameters);
				case TableTypeAlgebra.JoinKind.RightAnti:
					return JoinAlgorithm.RightAnti.Join(parameters);
				case TableTypeAlgebra.JoinKind.LeftSemi:
					return JoinAlgorithm.LeftSemi.Join(parameters);
				case TableTypeAlgebra.JoinKind.RightSemi:
					return JoinAlgorithm.RightSemi.Join(parameters);
				default:
					return new JoinAlgorithm.DynamicJoinAlgorithm.DynamicJoinEnumerable(parameters);
				}
			}

			// Token: 0x020017DA RID: 6106
			private class DynamicJoinEnumerable : IEnumerable<IValueReference>, IEnumerable
			{
				// Token: 0x06009A53 RID: 39507 RVA: 0x001FEE54 File Offset: 0x001FD054
				public DynamicJoinEnumerable(JoinAlgorithm.JoinParameters parameters)
				{
					this.parameters = parameters;
				}

				// Token: 0x06009A54 RID: 39508 RVA: 0x001FEE63 File Offset: 0x001FD063
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06009A55 RID: 39509 RVA: 0x001FEE6C File Offset: 0x001FD06C
				public IEnumerator<IValueReference> GetEnumerator()
				{
					JoinAlgorithm.JoinPrologue joinPrologue = JoinAlgorithm.JoinPrologue.Read(this.parameters, true, true);
					if (joinPrologue.EnumeratorSide != JoinAlgorithm.JoinSide.None && JoinAlgorithm.DynamicJoinAlgorithm.DynamicJoinEnumerable.CanIndex(this.parameters, joinPrologue.EnumeratorSide) && JoinAlgorithm.DynamicJoinAlgorithm.DynamicJoinEnumerable.ShouldIndex(joinPrologue))
					{
						return JoinAlgorithm.IndexJoinAlgorithm.Join(this.parameters, joinPrologue);
					}
					return JoinAlgorithm.HashJoinAlgorithm.Join(joinPrologue, this.parameters.JoinKeys, this.parameters.JoinColumns, this.parameters.JoinKind);
				}

				// Token: 0x06009A56 RID: 39510 RVA: 0x001FEEE0 File Offset: 0x001FD0E0
				private static bool CanIndex(JoinAlgorithm.JoinParameters joinParameters, JoinAlgorithm.JoinSide enumeratorSide)
				{
					Query query = joinParameters.Query(enumeratorSide);
					if (!query.QueryDomain.CanIndex || !JoinAlgorithm.DynamicJoinAlgorithm.DynamicJoinEnumerable.QueryFolds(query, joinParameters.KeyColumns(enumeratorSide)))
					{
						return false;
					}
					switch (joinParameters.JoinKind)
					{
					case TableTypeAlgebra.JoinKind.Inner:
						return true;
					case TableTypeAlgebra.JoinKind.LeftOuter:
						return enumeratorSide == JoinAlgorithm.JoinSide.Right;
					case TableTypeAlgebra.JoinKind.FullOuter:
						return false;
					case TableTypeAlgebra.JoinKind.RightOuter:
						return enumeratorSide == JoinAlgorithm.JoinSide.Left;
					default:
						throw new InvalidOperationException();
					}
				}

				// Token: 0x06009A57 RID: 39511 RVA: 0x001FEF48 File Offset: 0x001FD148
				private static bool ShouldIndex(JoinAlgorithm.JoinPrologue prologue)
				{
					long num = 0L;
					long num2 = 0L;
					long num3 = 0L;
					long num4 = 0L;
					foreach (KeyValuePair<Value, JoinAlgorithm.JoinRecord> keyValuePair in prologue.Dictionary)
					{
						num3 += (long)keyValuePair.Value.RightCount;
						num4 += (long)keyValuePair.Value.LeftCount;
						if (keyValuePair.Value.RightExists && keyValuePair.Value.LeftExists)
						{
							num += (long)keyValuePair.Value.RightCount;
							num2 += (long)keyValuePair.Value.LeftCount;
						}
					}
					if (prologue.EnumeratorSide == JoinAlgorithm.JoinSide.Left)
					{
						return JoinAlgorithm.DynamicJoinAlgorithm.DynamicJoinEnumerable.ShouldJoinHeuristic(num4, num2, num3);
					}
					return JoinAlgorithm.DynamicJoinAlgorithm.DynamicJoinEnumerable.ShouldJoinHeuristic(num3, num, num4);
				}

				// Token: 0x06009A58 RID: 39512 RVA: 0x001FF018 File Offset: 0x001FD218
				private static bool ShouldJoinHeuristic(long enumeratorSideTotal, long enumeratorSideMatch, long otherTotal)
				{
					return (otherTotal > 0L && otherTotal < 100L) || (otherTotal <= 200L && 50L * enumeratorSideMatch < enumeratorSideTotal);
				}

				// Token: 0x06009A59 RID: 39513 RVA: 0x001FF03C File Offset: 0x001FD23C
				private static bool QueryFolds(Query query, int[] keyColumns)
				{
					FunctionValue functionValue = JoinAlgorithm.IndexJoinAlgorithm.CreateSelector(query, keyColumns, EmptyArray<Value>.Instance);
					query = query.SelectRows(functionValue);
					query = query.QueryDomain.Optimize(query);
					return JoinAlgorithm.DynamicJoinAlgorithm.DynamicJoinEnumerable.QueryFolds(query);
				}

				// Token: 0x06009A5A RID: 39514 RVA: 0x001FF074 File Offset: 0x001FD274
				private static bool QueryFolds(Query query)
				{
					QueryKind kind = query.Kind;
					return kind == QueryKind.DataSource || (kind == QueryKind.ProjectColumns && JoinAlgorithm.DynamicJoinAlgorithm.DynamicJoinEnumerable.QueryFolds(((ProjectColumnsQuery)query).InnerQuery));
				}

				// Token: 0x0400517D RID: 20861
				private readonly JoinAlgorithm.JoinParameters parameters;
			}
		}

		// Token: 0x020017DB RID: 6107
		private class PairwiseHashJoinAlgorithm : JoinAlgorithm
		{
			// Token: 0x06009A5B RID: 39515 RVA: 0x001FF0A5 File Offset: 0x001FD2A5
			public override bool Supports(TableTypeAlgebra.JoinKind joinKind)
			{
				return joinKind <= TableTypeAlgebra.JoinKind.RightOuter;
			}

			// Token: 0x06009A5C RID: 39516 RVA: 0x001FF0AE File Offset: 0x001FD2AE
			public override IEnumerable<IValueReference> Join(JoinAlgorithm.JoinParameters parameters)
			{
				return new JoinAlgorithm.PairwiseHashJoinAlgorithm.HashJoinEnumerable(parameters);
			}

			// Token: 0x020017DC RID: 6108
			private class HashJoinEnumerable : IEnumerable<IValueReference>, IEnumerable
			{
				// Token: 0x06009A5E RID: 39518 RVA: 0x001FF0B6 File Offset: 0x001FD2B6
				public HashJoinEnumerable(JoinAlgorithm.JoinParameters parameters)
				{
					this.parameters = parameters;
				}

				// Token: 0x06009A5F RID: 39519 RVA: 0x001FF0C5 File Offset: 0x001FD2C5
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06009A60 RID: 39520 RVA: 0x001FF0CD File Offset: 0x001FD2CD
				public IEnumerator<IValueReference> GetEnumerator()
				{
					return JoinAlgorithm.HashJoinAlgorithm.Join(JoinAlgorithm.JoinPrologue.Read(this.parameters, true, true), this.parameters.JoinKeys, this.parameters.JoinColumns, this.parameters.JoinKind);
				}

				// Token: 0x0400517E RID: 20862
				private readonly JoinAlgorithm.JoinParameters parameters;
			}
		}

		// Token: 0x020017DD RID: 6109
		private class HashJoinAlgorithm : JoinAlgorithm
		{
			// Token: 0x06009A61 RID: 39521 RVA: 0x001FF102 File Offset: 0x001FD302
			public HashJoinAlgorithm(JoinAlgorithm.JoinSide hashSide)
			{
				this.hashSide = hashSide;
			}

			// Token: 0x06009A62 RID: 39522 RVA: 0x001FF0A5 File Offset: 0x001FD2A5
			public override bool Supports(TableTypeAlgebra.JoinKind joinKind)
			{
				return joinKind <= TableTypeAlgebra.JoinKind.RightOuter;
			}

			// Token: 0x06009A63 RID: 39523 RVA: 0x001FF111 File Offset: 0x001FD311
			public override IEnumerable<IValueReference> Join(JoinAlgorithm.JoinParameters parameters)
			{
				return new JoinAlgorithm.HashJoinAlgorithm.HashJoinEnumerable(this.hashSide, parameters);
			}

			// Token: 0x06009A64 RID: 39524 RVA: 0x001FF120 File Offset: 0x001FD320
			public static IEnumerator<IValueReference> Join(JoinAlgorithm.JoinPrologue prologue, Keys joinKeys, JoinColumn[] joinColumns, TableTypeAlgebra.JoinKind joinKind)
			{
				bool flag = joinKind == TableTypeAlgebra.JoinKind.FullOuter || joinKind == TableTypeAlgebra.JoinKind.LeftOuter;
				bool flag2 = joinKind == TableTypeAlgebra.JoinKind.FullOuter || joinKind == TableTypeAlgebra.JoinKind.RightOuter;
				bool rightNullWhenEmpty = flag && prologue.EnumeratorSide == JoinAlgorithm.JoinSide.Left;
				bool leftNullWhenEmpty = flag2 && prologue.EnumeratorSide == JoinAlgorithm.JoinSide.Right;
				IEnumerator<IValueReference> enumerator = prologue.Records.SelectMany((JoinAlgorithm.JoinRecord record) => record.LeftRows(leftNullWhenEmpty).SelectMany((RecordValue leftRow) => from rightRow in record.RightRows(rightNullWhenEmpty)
					select JoinAlgorithm.GetRow(leftRow, rightRow, joinKeys, joinColumns))).GetEnumerator();
				if (prologue.Enumerator != null)
				{
					bool flag3 = ((prologue.EnumeratorSide == JoinAlgorithm.JoinSide.Left) ? flag : flag2);
					enumerator = new ConcatEnumerator<IValueReference>(enumerator, new JoinAlgorithm.HashJoinAlgorithm.HashJoinEnumerator(prologue, joinKeys, joinColumns, flag3));
				}
				if (flag && prologue.EnumeratorSide != JoinAlgorithm.JoinSide.Left)
				{
					Func<RecordValue, IValueReference> <>9__5;
					IEnumerable<IValueReference> enumerable = prologue.Records.Where((JoinAlgorithm.JoinRecord record) => !record.RightExists).SelectMany(delegate(JoinAlgorithm.JoinRecord record)
					{
						IEnumerable<RecordValue> enumerable3 = record.LeftRows(false);
						Func<RecordValue, IValueReference> func;
						if ((func = <>9__5) == null)
						{
							func = (<>9__5 = (RecordValue leftRow) => JoinAlgorithm.GetRow(leftRow, null, joinKeys, joinColumns));
						}
						return enumerable3.Select(func);
					});
					enumerator = new ConcatEnumerator<IValueReference>(enumerator, enumerable.GetEnumerator());
				}
				if (flag2 && prologue.EnumeratorSide != JoinAlgorithm.JoinSide.Right)
				{
					Func<RecordValue, IValueReference> <>9__8;
					IEnumerable<IValueReference> enumerable2 = prologue.Records.Where((JoinAlgorithm.JoinRecord record) => !record.LeftExists).SelectMany(delegate(JoinAlgorithm.JoinRecord record)
					{
						IEnumerable<RecordValue> enumerable4 = record.RightRows(false);
						Func<RecordValue, IValueReference> func2;
						if ((func2 = <>9__8) == null)
						{
							func2 = (<>9__8 = (RecordValue rightRow) => JoinAlgorithm.GetRow(null, rightRow, joinKeys, joinColumns));
						}
						return enumerable4.Select(func2);
					});
					enumerator = new ConcatEnumerator<IValueReference>(enumerator, enumerable2.GetEnumerator());
				}
				return enumerator;
			}

			// Token: 0x0400517F RID: 20863
			private readonly JoinAlgorithm.JoinSide hashSide;

			// Token: 0x020017DE RID: 6110
			private class HashJoinEnumerable : IEnumerable<IValueReference>, IEnumerable
			{
				// Token: 0x06009A65 RID: 39525 RVA: 0x001FF280 File Offset: 0x001FD480
				public HashJoinEnumerable(JoinAlgorithm.JoinSide hashSide, JoinAlgorithm.JoinParameters parameters)
				{
					this.hashSide = hashSide;
					this.parameters = parameters;
				}

				// Token: 0x06009A66 RID: 39526 RVA: 0x001FF296 File Offset: 0x001FD496
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06009A67 RID: 39527 RVA: 0x001FF2A0 File Offset: 0x001FD4A0
				public IEnumerator<IValueReference> GetEnumerator()
				{
					return JoinAlgorithm.HashJoinAlgorithm.Join(JoinAlgorithm.JoinPrologue.Read(this.parameters, this.hashSide == JoinAlgorithm.JoinSide.Left, this.hashSide == JoinAlgorithm.JoinSide.Right), this.parameters.JoinKeys, this.parameters.JoinColumns, this.parameters.JoinKind);
				}

				// Token: 0x04005180 RID: 20864
				private readonly JoinAlgorithm.JoinSide hashSide;

				// Token: 0x04005181 RID: 20865
				private readonly JoinAlgorithm.JoinParameters parameters;
			}

			// Token: 0x020017DF RID: 6111
			private class HashJoinEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06009A68 RID: 39528 RVA: 0x001FF2F0 File Offset: 0x001FD4F0
				public HashJoinEnumerator(JoinAlgorithm.JoinPrologue prologue, Keys joinKeys, JoinColumn[] joinColumns, bool streamedIncludeOuter)
				{
					this.prologue = prologue;
					this.dictionary = prologue.Dictionary;
					this.joinKeys = joinKeys;
					this.joinColumns = joinColumns;
					this.streamedSide = prologue.EnumeratorSide;
					this.streamedEnumerator = prologue.Enumerator;
					this.streamedKeys = prologue.EnumeratorKeys;
					this.streamedIncludeOuter = streamedIncludeOuter;
					this.bufferedSide = ((this.streamedSide == JoinAlgorithm.JoinSide.Left) ? JoinAlgorithm.JoinSide.Right : JoinAlgorithm.JoinSide.Left);
					this.bufferedEnumerator = EmptyEnumerator<RecordValue>.Instance;
				}

				// Token: 0x06009A69 RID: 39529 RVA: 0x001FF36E File Offset: 0x001FD56E
				public void Dispose()
				{
					this.prologue.Dispose();
				}

				// Token: 0x170027B6 RID: 10166
				// (get) Token: 0x06009A6A RID: 39530 RVA: 0x001FF37B File Offset: 0x001FD57B
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06009A6B RID: 39531 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x170027B7 RID: 10167
				// (get) Token: 0x06009A6C RID: 39532 RVA: 0x001FF384 File Offset: 0x001FD584
				public IValueReference Current
				{
					get
					{
						if (this.current == null)
						{
							if (this.streamedSide == JoinAlgorithm.JoinSide.Left)
							{
								this.current = JoinAlgorithm.GetRow(this.streamedRow, this.bufferedRow, this.joinKeys, this.joinColumns);
							}
							else
							{
								this.current = JoinAlgorithm.GetRow(this.bufferedRow, this.streamedRow, this.joinKeys, this.joinColumns);
							}
						}
						return this.current;
					}
				}

				// Token: 0x06009A6D RID: 39533 RVA: 0x001FF3F0 File Offset: 0x001FD5F0
				public bool MoveNext()
				{
					this.current = null;
					if (this.bufferedEnumerator.MoveNext())
					{
						this.bufferedRow = this.bufferedEnumerator.Current;
						return true;
					}
					while (this.streamedEnumerator.MoveNext())
					{
						this.streamedRow = this.streamedEnumerator.Current.Value.AsRecord;
						Value key = JoinAlgorithm.GetKey(this.streamedRow, this.streamedKeys);
						JoinAlgorithm.JoinRecord joinRecord;
						if (this.dictionary.TryGetValue(key, out joinRecord))
						{
							joinRecord.VisitRow(this.streamedSide);
						}
						else
						{
							joinRecord = JoinAlgorithm.HashJoinAlgorithm.HashJoinEnumerator.emptyRecord;
						}
						this.bufferedEnumerator = joinRecord.Rows(this.bufferedSide).GetEnumerator();
						if (this.bufferedEnumerator.MoveNext())
						{
							this.bufferedRow = this.bufferedEnumerator.Current;
							return true;
						}
						if (this.streamedIncludeOuter)
						{
							this.bufferedRow = null;
							return true;
						}
					}
					return false;
				}

				// Token: 0x04005182 RID: 20866
				private static readonly JoinAlgorithm.JoinRecord emptyRecord = new JoinAlgorithm.JoinRecord();

				// Token: 0x04005183 RID: 20867
				private readonly Dictionary<Value, JoinAlgorithm.JoinRecord> dictionary;

				// Token: 0x04005184 RID: 20868
				private readonly Keys joinKeys;

				// Token: 0x04005185 RID: 20869
				private readonly JoinColumn[] joinColumns;

				// Token: 0x04005186 RID: 20870
				private readonly JoinAlgorithm.JoinSide streamedSide;

				// Token: 0x04005187 RID: 20871
				private readonly IEnumerator<IValueReference> streamedEnumerator;

				// Token: 0x04005188 RID: 20872
				private readonly int[] streamedKeys;

				// Token: 0x04005189 RID: 20873
				private readonly bool streamedIncludeOuter;

				// Token: 0x0400518A RID: 20874
				private readonly JoinAlgorithm.JoinSide bufferedSide;

				// Token: 0x0400518B RID: 20875
				private IEnumerator<RecordValue> bufferedEnumerator;

				// Token: 0x0400518C RID: 20876
				private RecordValue streamedRow;

				// Token: 0x0400518D RID: 20877
				private RecordValue bufferedRow;

				// Token: 0x0400518E RID: 20878
				private RecordValue current;

				// Token: 0x0400518F RID: 20879
				private readonly JoinAlgorithm.JoinPrologue prologue;
			}
		}

		// Token: 0x020017E4 RID: 6116
		private class IndexJoinAlgorithm : JoinAlgorithm
		{
			// Token: 0x06009A7D RID: 39549 RVA: 0x001FF65A File Offset: 0x001FD85A
			public IndexJoinAlgorithm(JoinAlgorithm.JoinSide indexSide)
			{
				this.indexSide = indexSide;
			}

			// Token: 0x06009A7E RID: 39550 RVA: 0x001FF669 File Offset: 0x001FD869
			public override bool Supports(TableTypeAlgebra.JoinKind joinKind)
			{
				switch (joinKind)
				{
				case TableTypeAlgebra.JoinKind.Inner:
					return true;
				case TableTypeAlgebra.JoinKind.LeftOuter:
					return this.indexSide == JoinAlgorithm.JoinSide.Left;
				case TableTypeAlgebra.JoinKind.RightOuter:
					return this.indexSide == JoinAlgorithm.JoinSide.Right;
				}
				return false;
			}

			// Token: 0x06009A7F RID: 39551 RVA: 0x001FF69A File Offset: 0x001FD89A
			public override IEnumerable<IValueReference> Join(JoinAlgorithm.JoinParameters parameters)
			{
				return new JoinAlgorithm.IndexJoinAlgorithm.IndexJoinEnumerable(this.indexSide, parameters);
			}

			// Token: 0x06009A80 RID: 39552 RVA: 0x001FF6A8 File Offset: 0x001FD8A8
			public static IEnumerator<IValueReference> Join(JoinAlgorithm.JoinParameters parameters, JoinAlgorithm.JoinPrologue prologue)
			{
				return new JoinAlgorithm.IndexJoinAlgorithm.IndexJoinEnumerator(parameters, prologue);
			}

			// Token: 0x06009A81 RID: 39553 RVA: 0x001FF6B4 File Offset: 0x001FD8B4
			public static FunctionValue CreateSelector(Query query, int[] keyColumns, Value[] keyValues)
			{
				IExpression expression = ConstantExpressionSyntaxNode.True;
				for (int i = 0; i < keyValues.Length; i++)
				{
					ListValue asList = keyValues[i].AsList;
					IExpression expression2 = ConstantExpressionSyntaxNode.True;
					for (int j = 0; j < keyColumns.Length; j++)
					{
						IExpression expression3 = new EqualsBinaryExpressionSyntaxNode(new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), Identifier.New(query.Columns[keyColumns[j]])), new ConstantExpressionSyntaxNode(asList[j]));
						IExpression expression5;
						if (j != 0)
						{
							IExpression expression4 = new AndBinaryExpressionSyntaxNode(expression2, expression3);
							expression5 = expression4;
						}
						else
						{
							expression5 = expression3;
						}
						expression2 = expression5;
					}
					IExpression expression6;
					if (i != 0)
					{
						IExpression expression4 = new OrBinaryExpressionSyntaxNode(expression, expression2);
						expression6 = expression4;
					}
					else
					{
						expression6 = expression2;
					}
					expression = expression6;
				}
				return new JoinAlgorithm.IndexJoinAlgorithm.SelectorFunctionValue(new FunctionExpressionSyntaxNode(JoinAlgorithm.IndexJoinAlgorithm.eachFunctionType, expression), keyColumns, keyValues);
			}

			// Token: 0x0400519D RID: 20893
			private const int batchSize = 25;

			// Token: 0x0400519E RID: 20894
			private readonly JoinAlgorithm.JoinSide indexSide;

			// Token: 0x0400519F RID: 20895
			private static IFunctionTypeExpression eachFunctionType = new FunctionTypeSyntaxNode(null, new IParameter[]
			{
				new ParameterSyntaxNode(Identifier.Underscore, null)
			}, 1);

			// Token: 0x020017E5 RID: 6117
			private class IndexJoinEnumerable : IEnumerable<IValueReference>, IEnumerable
			{
				// Token: 0x06009A83 RID: 39555 RVA: 0x001FF78E File Offset: 0x001FD98E
				public IndexJoinEnumerable(JoinAlgorithm.JoinSide indexSide, JoinAlgorithm.JoinParameters parameters)
				{
					this.indexSide = indexSide;
					this.parameters = parameters;
				}

				// Token: 0x06009A84 RID: 39556 RVA: 0x001FF7A4 File Offset: 0x001FD9A4
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06009A85 RID: 39557 RVA: 0x001FF7AC File Offset: 0x001FD9AC
				public IEnumerator<IValueReference> GetEnumerator()
				{
					JoinAlgorithm.JoinPrologue joinPrologue = JoinAlgorithm.JoinPrologue.Read(this.parameters, this.indexSide == JoinAlgorithm.JoinSide.Left, this.indexSide == JoinAlgorithm.JoinSide.Right);
					return JoinAlgorithm.IndexJoinAlgorithm.Join(this.parameters, joinPrologue);
				}

				// Token: 0x040051A0 RID: 20896
				private readonly JoinAlgorithm.JoinSide indexSide;

				// Token: 0x040051A1 RID: 20897
				private readonly JoinAlgorithm.JoinParameters parameters;
			}

			// Token: 0x020017E6 RID: 6118
			private class IndexJoinEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06009A86 RID: 39558 RVA: 0x001FF7E4 File Offset: 0x001FD9E4
				public IndexJoinEnumerator(JoinAlgorithm.JoinParameters parameters, JoinAlgorithm.JoinPrologue prologue)
				{
					this.parameters = parameters;
					this.streamedSide = prologue.EnumeratorSide;
					this.bufferedSide = ((this.streamedSide == JoinAlgorithm.JoinSide.Left) ? JoinAlgorithm.JoinSide.Right : JoinAlgorithm.JoinSide.Left);
					this.take = parameters.Take;
					this.bufferedKeys = new HashSet<Value>();
					this.bufferedRows = new List<RecordValue>();
					this.dictionaryEnumerator = prologue.Dictionary.GetEnumerator();
					this.prologue = prologue;
					this.batchEnumerator = EmptyEnumerator<IValueReference>.Instance;
				}

				// Token: 0x06009A87 RID: 39559 RVA: 0x001FF867 File Offset: 0x001FDA67
				public void Dispose()
				{
					this.prologue.Dispose();
					this.batchEnumerator.Dispose();
				}

				// Token: 0x170027B8 RID: 10168
				// (get) Token: 0x06009A88 RID: 39560 RVA: 0x001FF87F File Offset: 0x001FDA7F
				object IEnumerator.Current
				{
					get
					{
						return this.batchEnumerator.Current;
					}
				}

				// Token: 0x06009A89 RID: 39561 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x170027B9 RID: 10169
				// (get) Token: 0x06009A8A RID: 39562 RVA: 0x001FF87F File Offset: 0x001FDA7F
				public IValueReference Current
				{
					get
					{
						return this.batchEnumerator.Current;
					}
				}

				// Token: 0x06009A8B RID: 39563 RVA: 0x001FF88C File Offset: 0x001FDA8C
				public bool MoveNext()
				{
					while (!this.take.IsZero)
					{
						if (this.batchEnumerator.MoveNext())
						{
							this.take = RowCount.op_Decrement(this.take);
							return true;
						}
						if (this.dictionaryEnumerator == null)
						{
							return false;
						}
						while (this.bufferedKeys.Count < 25)
						{
							if (!this.dictionaryEnumerator.MoveNext())
							{
								this.dictionaryEnumerator = null;
								break;
							}
							KeyValuePair<Value, JoinAlgorithm.JoinRecord> keyValuePair = this.dictionaryEnumerator.Current;
							if (keyValuePair.Value.RowExists(this.bufferedSide))
							{
								HashSet<Value> hashSet = this.bufferedKeys;
								keyValuePair = this.dictionaryEnumerator.Current;
								hashSet.Add(keyValuePair.Key);
								List<RecordValue> list = this.bufferedRows;
								keyValuePair = this.dictionaryEnumerator.Current;
								list.AddRange(keyValuePair.Value.Rows(this.bufferedSide));
							}
						}
						TypeValue typeValue;
						if (this.streamedSide == JoinAlgorithm.JoinSide.Left)
						{
							typeValue = new QueryTableValue(this.parameters.RightQuery).Type;
						}
						else
						{
							typeValue = new QueryTableValue(this.parameters.LeftQuery).Type;
						}
						Value[] array = this.bufferedRows.ToArray();
						TableValue tableValue = ListValue.New(array).ToTable(typeValue.AsTableType);
						Query query;
						Query query2;
						if (this.streamedSide == JoinAlgorithm.JoinSide.Left)
						{
							FunctionValue functionValue = JoinAlgorithm.IndexJoinAlgorithm.CreateSelector(this.parameters.LeftQuery, this.parameters.LeftKeyColumns, this.bufferedKeys.ToArray<Value>());
							query = this.parameters.LeftQuery.SelectRows(functionValue);
							query2 = tableValue.Query;
						}
						else
						{
							FunctionValue functionValue2 = JoinAlgorithm.IndexJoinAlgorithm.CreateSelector(this.parameters.RightQuery, this.parameters.RightKeyColumns, this.bufferedKeys.ToArray<Value>());
							query = tableValue.Query;
							query2 = this.parameters.RightQuery.SelectRows(functionValue2);
						}
						TableValue tableValue2 = new QueryTableValue(new JoinQuery(this.take, query, this.parameters.LeftKeyColumns, query2, this.parameters.RightKeyColumns, this.parameters.JoinKind, this.parameters.JoinKeys, this.parameters.JoinColumns, JoinAlgorithm.PairwiseHash, null));
						this.bufferedKeys.Clear();
						this.bufferedRows.Clear();
						this.batchEnumerator.Dispose();
						this.batchEnumerator = tableValue2.GetEnumerator();
					}
					return false;
				}

				// Token: 0x040051A2 RID: 20898
				private readonly JoinAlgorithm.JoinParameters parameters;

				// Token: 0x040051A3 RID: 20899
				private readonly JoinAlgorithm.JoinSide streamedSide;

				// Token: 0x040051A4 RID: 20900
				private readonly JoinAlgorithm.JoinSide bufferedSide;

				// Token: 0x040051A5 RID: 20901
				private RowCount take;

				// Token: 0x040051A6 RID: 20902
				private readonly HashSet<Value> bufferedKeys;

				// Token: 0x040051A7 RID: 20903
				private readonly List<RecordValue> bufferedRows;

				// Token: 0x040051A8 RID: 20904
				private IEnumerator<KeyValuePair<Value, JoinAlgorithm.JoinRecord>> dictionaryEnumerator;

				// Token: 0x040051A9 RID: 20905
				private IEnumerator<IValueReference> batchEnumerator;

				// Token: 0x040051AA RID: 20906
				private readonly JoinAlgorithm.JoinPrologue prologue;
			}

			// Token: 0x020017E7 RID: 6119
			private class SelectorFunctionValue : NativeFunctionValue1
			{
				// Token: 0x06009A8C RID: 39564 RVA: 0x001FFAD1 File Offset: 0x001FDCD1
				public SelectorFunctionValue(IFunctionExpression expression, int[] keyColumns, Value[] keyValues)
				{
					this.expression = expression;
					this.keyColumns = keyColumns;
					this.keyValues = keyValues;
				}

				// Token: 0x170027BA RID: 10170
				// (get) Token: 0x06009A8D RID: 39565 RVA: 0x001FFAEE File Offset: 0x001FDCEE
				public override IExpression Expression
				{
					get
					{
						return this.expression;
					}
				}

				// Token: 0x06009A8E RID: 39566 RVA: 0x001FFAF8 File Offset: 0x001FDCF8
				private static bool Matches(ListValue key, int[] keyColumns, Value row)
				{
					for (int i = 0; i < keyColumns.Length; i++)
					{
						if (!key[i].Equals(row[keyColumns[i]]))
						{
							return false;
						}
					}
					return true;
				}

				// Token: 0x06009A8F RID: 39567 RVA: 0x001FFB30 File Offset: 0x001FDD30
				public override Value Invoke(Value row)
				{
					for (int i = 0; i < this.keyValues.Length; i++)
					{
						if (JoinAlgorithm.IndexJoinAlgorithm.SelectorFunctionValue.Matches(this.keyValues[i].AsList, this.keyColumns, row))
						{
							return LogicalValue.True;
						}
					}
					return LogicalValue.False;
				}

				// Token: 0x040051AB RID: 20907
				private readonly IFunctionExpression expression;

				// Token: 0x040051AC RID: 20908
				private readonly int[] keyColumns;

				// Token: 0x040051AD RID: 20909
				private readonly Value[] keyValues;
			}
		}

		// Token: 0x020017E8 RID: 6120
		private class SortMergeJoinAlgorithm : JoinAlgorithm
		{
			// Token: 0x06009A90 RID: 39568 RVA: 0x001FF0A5 File Offset: 0x001FD2A5
			public override bool Supports(TableTypeAlgebra.JoinKind joinKind)
			{
				return joinKind <= TableTypeAlgebra.JoinKind.RightOuter;
			}

			// Token: 0x06009A91 RID: 39569 RVA: 0x001FFB76 File Offset: 0x001FDD76
			public override IEnumerable<IValueReference> Join(JoinAlgorithm.JoinParameters parameters)
			{
				return JoinAlgorithm.SortMergeJoinAlgorithm.CreateSortedMergeJoinResult(parameters.Take, parameters.LeftQuery, parameters.LeftKeyColumns, parameters.RightQuery, parameters.RightKeyColumns, parameters.JoinKind, parameters.JoinKeys, parameters.JoinColumns);
			}

			// Token: 0x06009A92 RID: 39570 RVA: 0x001FFBB0 File Offset: 0x001FDDB0
			private static int CompareKeys(Value key1, Value key2)
			{
				ListValue asList = key1.AsList;
				ListValue asList2 = key2.AsList;
				for (int i = 0; i < asList.Count; i++)
				{
					int num = asList[i].CompareTo(asList2[i]);
					if (num != 0)
					{
						return num;
					}
				}
				return 0;
			}

			// Token: 0x06009A93 RID: 39571 RVA: 0x001FFBF8 File Offset: 0x001FDDF8
			private static Value BufferNextKey(ref IEnumerator<IValueReference> enumerator, int[] columns, List<IValueReference> buffer)
			{
				Value value = null;
				if (enumerator != null)
				{
					IValueReference valueReference = enumerator.Current;
					value = JoinAlgorithm.GetKey(valueReference.Value.AsRecord, columns);
					buffer.Add(valueReference);
					while (enumerator.MoveNext())
					{
						valueReference = enumerator.Current;
						if (JoinAlgorithm.SortMergeJoinAlgorithm.CompareKeys(JoinAlgorithm.GetKey(valueReference.Value.AsRecord, columns), value) != 0)
						{
							return value;
						}
						buffer.Add(valueReference);
					}
					enumerator = null;
				}
				return value;
			}

			// Token: 0x06009A94 RID: 39572 RVA: 0x001FFC68 File Offset: 0x001FDE68
			private static IEnumerable<IValueReference> CreateSortedMergeJoinResult(RowCount take, Query leftQuery, int[] leftKeys, Query rightQuery, int[] rightKeys, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns)
			{
				IEnumerable<IValueReference> rows = leftQuery.GetRows();
				IEnumerable<IValueReference> rows2 = rightQuery.GetRows();
				bool includeLeftOuter = joinKind == TableTypeAlgebra.JoinKind.FullOuter || joinKind == TableTypeAlgebra.JoinKind.LeftOuter;
				bool includeRightOuter = joinKind == TableTypeAlgebra.JoinKind.FullOuter || joinKind == TableTypeAlgebra.JoinKind.RightOuter;
				using (IEnumerator<IValueReference> leftEnumerator = rows.GetEnumerator())
				{
					using (IEnumerator<IValueReference> rightEnumerator = rows2.GetEnumerator())
					{
						IEnumerator<IValueReference> leftEnumerator2 = leftEnumerator;
						IEnumerator<IValueReference> rightEnumerator2 = rightEnumerator;
						if (!leftEnumerator2.MoveNext())
						{
							leftEnumerator2 = null;
						}
						if (!rightEnumerator2.MoveNext())
						{
							rightEnumerator2 = null;
						}
						List<IValueReference> buffered = new List<IValueReference>();
						List<IValueReference> buffered2 = new List<IValueReference>();
						Value key = JoinAlgorithm.SortMergeJoinAlgorithm.BufferNextKey(ref leftEnumerator2, leftKeys, buffered);
						Value key2 = JoinAlgorithm.SortMergeJoinAlgorithm.BufferNextKey(ref rightEnumerator2, rightKeys, buffered2);
						while ((key != null || includeRightOuter) && (key2 != null || includeLeftOuter) && (key != null || key2 != null))
						{
							int num = ((key == null) ? 1 : ((key2 == null) ? (-1) : JoinAlgorithm.SortMergeJoinAlgorithm.CompareKeys(key, key2)));
							if (num == 0)
							{
								TableTypeValue tableTypeValue = TableTypeValue.New(RecordTypeValue.New(leftQuery.Columns));
								TableTypeValue tableTypeValue2 = TableTypeValue.New(RecordTypeValue.New(rightQuery.Columns));
								IEnumerable<IValueReference> enumerable = JoinAlgorithm.PairwiseHash.Join(new JoinAlgorithm.JoinParameters(take, ListValue.New(buffered.ToArray()).ToTable(tableTypeValue).Query, leftKeys, ListValue.New(buffered2.ToArray()).ToTable(tableTypeValue2).Query, rightKeys, TableTypeAlgebra.JoinKind.Inner, joinKeys, joinColumns));
								foreach (IValueReference valueReference in enumerable)
								{
									RowCount rowCount = take;
									take = RowCount.op_Decrement(rowCount);
									yield return valueReference;
								}
								IEnumerator<IValueReference> enumerator = null;
								buffered = new List<IValueReference>();
								buffered2 = new List<IValueReference>();
								key = JoinAlgorithm.SortMergeJoinAlgorithm.BufferNextKey(ref leftEnumerator2, leftKeys, buffered);
								key2 = JoinAlgorithm.SortMergeJoinAlgorithm.BufferNextKey(ref rightEnumerator2, rightKeys, buffered2);
							}
							else if (num < 0)
							{
								if (includeLeftOuter)
								{
									foreach (IValueReference valueReference2 in buffered)
									{
										Value row = JoinAlgorithm.GetRow(valueReference2.Value.AsRecord, null, joinKeys, joinColumns);
										RowCount rowCount = take;
										take = RowCount.op_Decrement(rowCount);
										yield return row;
									}
									List<IValueReference>.Enumerator enumerator2 = default(List<IValueReference>.Enumerator);
								}
								buffered = new List<IValueReference>();
								key = JoinAlgorithm.SortMergeJoinAlgorithm.BufferNextKey(ref leftEnumerator2, leftKeys, buffered);
							}
							else if (num > 0)
							{
								if (includeRightOuter)
								{
									foreach (IValueReference valueReference3 in buffered2)
									{
										Value row2 = JoinAlgorithm.GetRow(null, valueReference3.Value.AsRecord, joinKeys, joinColumns);
										RowCount rowCount = take;
										take = RowCount.op_Decrement(rowCount);
										yield return row2;
									}
									List<IValueReference>.Enumerator enumerator2 = default(List<IValueReference>.Enumerator);
								}
								buffered2 = new List<IValueReference>();
								key2 = JoinAlgorithm.SortMergeJoinAlgorithm.BufferNextKey(ref rightEnumerator2, rightKeys, buffered2);
							}
						}
						leftEnumerator2 = null;
						rightEnumerator2 = null;
						buffered = null;
						buffered2 = null;
						key = null;
						key2 = null;
					}
					IEnumerator<IValueReference> rightEnumerator = null;
				}
				IEnumerator<IValueReference> leftEnumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x020017EA RID: 6122
		private class AntiSemiJoinAlgorithm : JoinAlgorithm
		{
			// Token: 0x06009AA3 RID: 39587 RVA: 0x002003E7 File Offset: 0x001FE5E7
			public AntiSemiJoinAlgorithm(JoinAlgorithm.JoinSide selectSide, bool isSemijoin)
			{
				this.selectSide = selectSide;
				this.isSemijoin = isSemijoin;
			}

			// Token: 0x06009AA4 RID: 39588 RVA: 0x002003FD File Offset: 0x001FE5FD
			public override bool Supports(TableTypeAlgebra.JoinKind joinKind)
			{
				switch (joinKind)
				{
				case TableTypeAlgebra.JoinKind.LeftAnti:
				case TableTypeAlgebra.JoinKind.LeftSemi:
					return this.selectSide == JoinAlgorithm.JoinSide.Left;
				case TableTypeAlgebra.JoinKind.RightAnti:
				case TableTypeAlgebra.JoinKind.RightSemi:
					return this.selectSide == JoinAlgorithm.JoinSide.Right;
				default:
					return false;
				}
			}

			// Token: 0x06009AA5 RID: 39589 RVA: 0x0020042E File Offset: 0x001FE62E
			public override IEnumerable<IValueReference> Join(JoinAlgorithm.JoinParameters parameters)
			{
				return new JoinAlgorithm.AntiSemiJoinAlgorithm.AntiSemiJoinEnumerable(this.selectSide, parameters, this.isSemijoin);
			}

			// Token: 0x040051CD RID: 20941
			private readonly JoinAlgorithm.JoinSide selectSide;

			// Token: 0x040051CE RID: 20942
			private readonly bool isSemijoin;

			// Token: 0x020017EB RID: 6123
			private class AntiSemiJoinEnumerable : IEnumerable<IValueReference>, IEnumerable
			{
				// Token: 0x06009AA6 RID: 39590 RVA: 0x00200442 File Offset: 0x001FE642
				public AntiSemiJoinEnumerable(JoinAlgorithm.JoinSide selectSide, JoinAlgorithm.JoinParameters parameters, bool isSemijoin)
				{
					this.selectSide = selectSide;
					this.parameters = parameters;
					this.isSemijoin = isSemijoin;
				}

				// Token: 0x06009AA7 RID: 39591 RVA: 0x0020045F File Offset: 0x001FE65F
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06009AA8 RID: 39592 RVA: 0x00200468 File Offset: 0x001FE668
				public IEnumerator<IValueReference> GetEnumerator()
				{
					JoinAlgorithm.JoinSide joinSide = ((this.selectSide == JoinAlgorithm.JoinSide.Left) ? JoinAlgorithm.JoinSide.Right : JoinAlgorithm.JoinSide.Left);
					Query query = this.parameters.Query(joinSide);
					int[] array = this.parameters.KeyColumns(joinSide);
					HashSet<Value> hashset = new HashSet<Value>();
					foreach (IValueReference valueReference in query.GetRows())
					{
						hashset.Add(JoinAlgorithm.GetKey(valueReference.Value.AsRecord, array));
					}
					Query query2 = this.parameters.Query(this.selectSide);
					int[] selectKeyColumns = this.parameters.KeyColumns(this.selectSide);
					return (from record in query2.GetRows()
						where hashset.Contains(JoinAlgorithm.GetKey(record.Value.AsRecord, selectKeyColumns)) == this.isSemijoin
						select JoinAlgorithm.GetRow(this.selectSide, record.Value.AsRecord, null, this.parameters.JoinKeys, this.parameters.JoinColumns)).GetEnumerator();
				}

				// Token: 0x040051CF RID: 20943
				private readonly JoinAlgorithm.JoinSide selectSide;

				// Token: 0x040051D0 RID: 20944
				private readonly JoinAlgorithm.JoinParameters parameters;

				// Token: 0x040051D1 RID: 20945
				private readonly bool isSemijoin;
			}
		}

		// Token: 0x020017ED RID: 6125
		public enum JoinSide
		{
			// Token: 0x040051D6 RID: 20950
			Right,
			// Token: 0x040051D7 RID: 20951
			Left,
			// Token: 0x040051D8 RID: 20952
			None
		}

		// Token: 0x020017EE RID: 6126
		private class JoinPrologue : IDisposable
		{
			// Token: 0x06009AAC RID: 39596 RVA: 0x002005CE File Offset: 0x001FE7CE
			private JoinPrologue(Dictionary<Value, JoinAlgorithm.JoinRecord> dictionary, List<JoinAlgorithm.JoinRecord> records, JoinAlgorithm.JoinSide enumeratorSide, int[] enumeratorKeys, IEnumerator<IValueReference> enumerator)
			{
				this.dictionary = dictionary;
				this.records = records;
				this.enumerator = enumerator;
				this.enumeratorKeys = enumeratorKeys;
				this.enumeratorSide = enumeratorSide;
			}

			// Token: 0x170027BD RID: 10173
			// (get) Token: 0x06009AAD RID: 39597 RVA: 0x002005FB File Offset: 0x001FE7FB
			public Dictionary<Value, JoinAlgorithm.JoinRecord> Dictionary
			{
				get
				{
					return this.dictionary;
				}
			}

			// Token: 0x170027BE RID: 10174
			// (get) Token: 0x06009AAE RID: 39598 RVA: 0x00200603 File Offset: 0x001FE803
			public List<JoinAlgorithm.JoinRecord> Records
			{
				get
				{
					return this.records;
				}
			}

			// Token: 0x170027BF RID: 10175
			// (get) Token: 0x06009AAF RID: 39599 RVA: 0x0020060B File Offset: 0x001FE80B
			public JoinAlgorithm.JoinSide EnumeratorSide
			{
				get
				{
					return this.enumeratorSide;
				}
			}

			// Token: 0x170027C0 RID: 10176
			// (get) Token: 0x06009AB0 RID: 39600 RVA: 0x00200613 File Offset: 0x001FE813
			public int[] EnumeratorKeys
			{
				get
				{
					return this.enumeratorKeys;
				}
			}

			// Token: 0x170027C1 RID: 10177
			// (get) Token: 0x06009AB1 RID: 39601 RVA: 0x0020061B File Offset: 0x001FE81B
			public IEnumerator<IValueReference> Enumerator
			{
				get
				{
					return this.enumerator;
				}
			}

			// Token: 0x06009AB2 RID: 39602 RVA: 0x00200624 File Offset: 0x001FE824
			public static JoinAlgorithm.JoinPrologue Read(JoinAlgorithm.JoinParameters parameters, bool readLeft, bool readRight)
			{
				RowCount take = parameters.Take;
				IEnumerable<IValueReference> rows = parameters.LeftQuery.GetRows();
				int[] leftKeyColumns = parameters.LeftKeyColumns;
				IEnumerable<IValueReference> rows2 = parameters.RightQuery.GetRows();
				int[] rightKeyColumns = parameters.RightKeyColumns;
				TableTypeAlgebra.JoinKind joinKind = parameters.JoinKind;
				Dictionary<Value, JoinAlgorithm.JoinRecord> dictionary = new Dictionary<Value, JoinAlgorithm.JoinRecord>();
				IEnumerator<IValueReference> enumerator = rows.GetEnumerator();
				IEnumerator<IValueReference> enumerator2 = rows2.GetEnumerator();
				long num = 0L;
				long num2 = (take.IsInfinite ? long.MaxValue : take.Value);
				List<JoinAlgorithm.JoinRecord> list = new List<JoinAlgorithm.JoinRecord>();
				bool? flag = null;
				bool? flag2 = null;
				if (readLeft)
				{
					flag = new bool?(false);
				}
				if (readRight)
				{
					flag2 = new bool?(false);
				}
				while (enumerator != null && enumerator2 != null && num < num2)
				{
					if (readLeft)
					{
						if (enumerator.MoveNext())
						{
							RecordValue asRecord = enumerator.Current.Value.AsRecord;
							Value key = JoinAlgorithm.GetKey(asRecord, leftKeyColumns);
							JoinAlgorithm.JoinRecord joinRecord;
							if (!dictionary.TryGetValue(key, out joinRecord))
							{
								joinRecord = new JoinAlgorithm.JoinRecord();
								dictionary.Add(key, joinRecord);
								list.Add(joinRecord);
							}
							joinRecord.AddLeftRow(asRecord);
							num += (long)joinRecord.RightCount;
							flag = new bool?(true);
						}
						else
						{
							enumerator.Dispose();
							enumerator = null;
						}
					}
					if (readRight)
					{
						if (enumerator2.MoveNext())
						{
							RecordValue asRecord2 = enumerator2.Current.Value.AsRecord;
							Value key2 = JoinAlgorithm.GetKey(asRecord2, rightKeyColumns);
							JoinAlgorithm.JoinRecord joinRecord2;
							if (!dictionary.TryGetValue(key2, out joinRecord2))
							{
								joinRecord2 = new JoinAlgorithm.JoinRecord();
								dictionary.Add(key2, joinRecord2);
								list.Add(joinRecord2);
							}
							joinRecord2.AddRightRow(asRecord2);
							num += (long)joinRecord2.LeftCount;
							flag2 = new bool?(true);
						}
						else
						{
							enumerator2.Dispose();
							enumerator2 = null;
						}
					}
				}
				bool flag3 = joinKind == TableTypeAlgebra.JoinKind.FullOuter || joinKind == TableTypeAlgebra.JoinKind.LeftOuter;
				bool flag4 = joinKind == TableTypeAlgebra.JoinKind.FullOuter || joinKind == TableTypeAlgebra.JoinKind.RightOuter;
				bool? flag5 = flag;
				bool flag6 = false;
				if (!((flag5.GetValueOrDefault() == flag6) & (flag5 != null)) || flag4)
				{
					flag5 = flag2;
					flag6 = false;
					if (!((flag5.GetValueOrDefault() == flag6) & (flag5 != null)) || flag3)
					{
						if (enumerator != null && enumerator2 != null)
						{
							enumerator.Dispose();
							enumerator2.Dispose();
							return new JoinAlgorithm.JoinPrologue(dictionary, list, JoinAlgorithm.JoinSide.None, null, null);
						}
						if (enumerator != null)
						{
							return new JoinAlgorithm.JoinPrologue(dictionary, list, JoinAlgorithm.JoinSide.Left, leftKeyColumns, enumerator);
						}
						return new JoinAlgorithm.JoinPrologue(dictionary, list, JoinAlgorithm.JoinSide.Right, rightKeyColumns, enumerator2);
					}
				}
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
				return new JoinAlgorithm.JoinPrologue(dictionary, list, JoinAlgorithm.JoinSide.None, null, null);
			}

			// Token: 0x06009AB3 RID: 39603 RVA: 0x0020089B File Offset: 0x001FEA9B
			public void Dispose()
			{
				if (this.enumerator != null)
				{
					this.enumerator.Dispose();
				}
			}

			// Token: 0x040051D9 RID: 20953
			private readonly Dictionary<Value, JoinAlgorithm.JoinRecord> dictionary;

			// Token: 0x040051DA RID: 20954
			private readonly List<JoinAlgorithm.JoinRecord> records;

			// Token: 0x040051DB RID: 20955
			private readonly IEnumerator<IValueReference> enumerator;

			// Token: 0x040051DC RID: 20956
			private readonly int[] enumeratorKeys;

			// Token: 0x040051DD RID: 20957
			private readonly JoinAlgorithm.JoinSide enumeratorSide;
		}

		// Token: 0x020017EF RID: 6127
		public class JoinParameters
		{
			// Token: 0x06009AB4 RID: 39604 RVA: 0x002008B0 File Offset: 0x001FEAB0
			public JoinParameters(RowCount take, Query leftQuery, int[] leftKeys, Query rightQuery, int[] rightKeys, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns)
			{
				this.take = take;
				this.leftQuery = leftQuery;
				this.leftKeys = leftKeys;
				this.rightQuery = rightQuery;
				this.rightKeys = rightKeys;
				this.joinKind = joinKind;
				this.joinKeys = joinKeys;
				this.joinColumns = joinColumns;
			}

			// Token: 0x170027C2 RID: 10178
			// (get) Token: 0x06009AB5 RID: 39605 RVA: 0x00200900 File Offset: 0x001FEB00
			public RowCount Take
			{
				get
				{
					return this.take;
				}
			}

			// Token: 0x170027C3 RID: 10179
			// (get) Token: 0x06009AB6 RID: 39606 RVA: 0x00200908 File Offset: 0x001FEB08
			public Query LeftQuery
			{
				get
				{
					return this.leftQuery;
				}
			}

			// Token: 0x170027C4 RID: 10180
			// (get) Token: 0x06009AB7 RID: 39607 RVA: 0x00200910 File Offset: 0x001FEB10
			public int[] LeftKeyColumns
			{
				get
				{
					return this.leftKeys;
				}
			}

			// Token: 0x170027C5 RID: 10181
			// (get) Token: 0x06009AB8 RID: 39608 RVA: 0x00200918 File Offset: 0x001FEB18
			public Query RightQuery
			{
				get
				{
					return this.rightQuery;
				}
			}

			// Token: 0x170027C6 RID: 10182
			// (get) Token: 0x06009AB9 RID: 39609 RVA: 0x00200920 File Offset: 0x001FEB20
			public int[] RightKeyColumns
			{
				get
				{
					return this.rightKeys;
				}
			}

			// Token: 0x170027C7 RID: 10183
			// (get) Token: 0x06009ABA RID: 39610 RVA: 0x00200928 File Offset: 0x001FEB28
			public TableTypeAlgebra.JoinKind JoinKind
			{
				get
				{
					return this.joinKind;
				}
			}

			// Token: 0x170027C8 RID: 10184
			// (get) Token: 0x06009ABB RID: 39611 RVA: 0x00200930 File Offset: 0x001FEB30
			public Keys JoinKeys
			{
				get
				{
					return this.joinKeys;
				}
			}

			// Token: 0x170027C9 RID: 10185
			// (get) Token: 0x06009ABC RID: 39612 RVA: 0x00200938 File Offset: 0x001FEB38
			public JoinColumn[] JoinColumns
			{
				get
				{
					return this.joinColumns;
				}
			}

			// Token: 0x06009ABD RID: 39613 RVA: 0x00200940 File Offset: 0x001FEB40
			public int[] KeyColumns(JoinAlgorithm.JoinSide joinSide)
			{
				if (joinSide != JoinAlgorithm.JoinSide.Left)
				{
					return this.rightKeys;
				}
				return this.leftKeys;
			}

			// Token: 0x06009ABE RID: 39614 RVA: 0x00200953 File Offset: 0x001FEB53
			public Query Query(JoinAlgorithm.JoinSide joinSide)
			{
				if (joinSide != JoinAlgorithm.JoinSide.Left)
				{
					return this.rightQuery;
				}
				return this.leftQuery;
			}

			// Token: 0x040051DE RID: 20958
			private readonly RowCount take;

			// Token: 0x040051DF RID: 20959
			private readonly Query leftQuery;

			// Token: 0x040051E0 RID: 20960
			private readonly int[] leftKeys;

			// Token: 0x040051E1 RID: 20961
			private readonly Query rightQuery;

			// Token: 0x040051E2 RID: 20962
			private readonly int[] rightKeys;

			// Token: 0x040051E3 RID: 20963
			private readonly TableTypeAlgebra.JoinKind joinKind;

			// Token: 0x040051E4 RID: 20964
			private readonly Keys joinKeys;

			// Token: 0x040051E5 RID: 20965
			private readonly JoinColumn[] joinColumns;
		}

		// Token: 0x020017F0 RID: 6128
		private class JoinRecord
		{
			// Token: 0x170027CA RID: 10186
			// (get) Token: 0x06009ABF RID: 39615 RVA: 0x00200966 File Offset: 0x001FEB66
			public bool LeftExists
			{
				get
				{
					return this.leftExists;
				}
			}

			// Token: 0x170027CB RID: 10187
			// (get) Token: 0x06009AC0 RID: 39616 RVA: 0x0020096E File Offset: 0x001FEB6E
			public int LeftCount
			{
				get
				{
					if (this.left == null)
					{
						return 0;
					}
					return this.left.Count;
				}
			}

			// Token: 0x06009AC1 RID: 39617 RVA: 0x00200985 File Offset: 0x001FEB85
			public IEnumerable<RecordValue> LeftRows(bool nullWhenEmpty)
			{
				if (this.left != null)
				{
					return this.left;
				}
				if (nullWhenEmpty)
				{
					return JoinAlgorithm.JoinRecord.NullEnumerable<RecordValue>.Instance;
				}
				return EmptyEnumerable<RecordValue>.Instance;
			}

			// Token: 0x170027CC RID: 10188
			// (get) Token: 0x06009AC2 RID: 39618 RVA: 0x002009A4 File Offset: 0x001FEBA4
			public bool RightExists
			{
				get
				{
					return this.rightExists;
				}
			}

			// Token: 0x170027CD RID: 10189
			// (get) Token: 0x06009AC3 RID: 39619 RVA: 0x002009AC File Offset: 0x001FEBAC
			public int RightCount
			{
				get
				{
					if (this.right == null)
					{
						return 0;
					}
					return this.right.Count;
				}
			}

			// Token: 0x06009AC4 RID: 39620 RVA: 0x002009C3 File Offset: 0x001FEBC3
			public IEnumerable<RecordValue> RightRows(bool nullWhenEmpty)
			{
				if (this.right != null)
				{
					return this.right;
				}
				if (nullWhenEmpty)
				{
					return JoinAlgorithm.JoinRecord.NullEnumerable<RecordValue>.Instance;
				}
				return EmptyEnumerable<RecordValue>.Instance;
			}

			// Token: 0x06009AC5 RID: 39621 RVA: 0x002009E2 File Offset: 0x001FEBE2
			public void AddLeftRow(RecordValue row)
			{
				this.leftExists = true;
				if (this.left == null)
				{
					this.left = new List<RecordValue>();
				}
				this.left.Add(row);
			}

			// Token: 0x06009AC6 RID: 39622 RVA: 0x00200A0A File Offset: 0x001FEC0A
			public void AddRightRow(RecordValue row)
			{
				this.rightExists = true;
				if (this.right == null)
				{
					this.right = new List<RecordValue>();
				}
				this.right.Add(row);
			}

			// Token: 0x06009AC7 RID: 39623 RVA: 0x00200A32 File Offset: 0x001FEC32
			public void VisitRow(JoinAlgorithm.JoinSide joinSide)
			{
				if (joinSide == JoinAlgorithm.JoinSide.Left)
				{
					this.leftExists = true;
					return;
				}
				this.rightExists = true;
			}

			// Token: 0x06009AC8 RID: 39624 RVA: 0x00200A47 File Offset: 0x001FEC47
			public bool RowExists(JoinAlgorithm.JoinSide joinSide)
			{
				if (joinSide != JoinAlgorithm.JoinSide.Left)
				{
					return this.rightExists;
				}
				return this.leftExists;
			}

			// Token: 0x06009AC9 RID: 39625 RVA: 0x00200A5A File Offset: 0x001FEC5A
			public IEnumerable<RecordValue> Rows(JoinAlgorithm.JoinSide joinSide)
			{
				if (joinSide != JoinAlgorithm.JoinSide.Left)
				{
					return this.RightRows(false);
				}
				return this.LeftRows(false);
			}

			// Token: 0x040051E6 RID: 20966
			private List<RecordValue> left;

			// Token: 0x040051E7 RID: 20967
			private List<RecordValue> right;

			// Token: 0x040051E8 RID: 20968
			private bool leftExists;

			// Token: 0x040051E9 RID: 20969
			private bool rightExists;

			// Token: 0x020017F1 RID: 6129
			private class NullEnumerable<T> : IEnumerable<T>, IEnumerable where T : class
			{
				// Token: 0x06009ACB RID: 39627 RVA: 0x00200A6F File Offset: 0x001FEC6F
				public IEnumerator<T> GetEnumerator()
				{
					return new JoinAlgorithm.JoinRecord.NullEnumerable<T>.NullEnumerator();
				}

				// Token: 0x06009ACC RID: 39628 RVA: 0x0000EDE9 File Offset: 0x0000CFE9
				IEnumerator IEnumerable.GetEnumerator()
				{
					return EmptyEnumerator<T>.Instance;
				}

				// Token: 0x040051EA RID: 20970
				public static readonly JoinAlgorithm.JoinRecord.NullEnumerable<T> Instance = new JoinAlgorithm.JoinRecord.NullEnumerable<T>();

				// Token: 0x020017F2 RID: 6130
				private class NullEnumerator : IEnumerator<T>, IDisposable, IEnumerator
				{
					// Token: 0x06009ACF RID: 39631 RVA: 0x00200A82 File Offset: 0x001FEC82
					public NullEnumerator()
					{
						this.moved = false;
					}

					// Token: 0x06009AD0 RID: 39632 RVA: 0x0000336E File Offset: 0x0000156E
					public void Dispose()
					{
					}

					// Token: 0x06009AD1 RID: 39633 RVA: 0x0000EE09 File Offset: 0x0000D009
					public void Reset()
					{
						throw new InvalidOperationException();
					}

					// Token: 0x170027CE RID: 10190
					// (get) Token: 0x06009AD2 RID: 39634 RVA: 0x00200A91 File Offset: 0x001FEC91
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x170027CF RID: 10191
					// (get) Token: 0x06009AD3 RID: 39635 RVA: 0x00200AA0 File Offset: 0x001FECA0
					public T Current
					{
						get
						{
							return default(T);
						}
					}

					// Token: 0x06009AD4 RID: 39636 RVA: 0x00200AB6 File Offset: 0x001FECB6
					public bool MoveNext()
					{
						if (this.moved)
						{
							return false;
						}
						this.moved = true;
						return true;
					}

					// Token: 0x040051EB RID: 20971
					private bool moved;
				}
			}
		}
	}
}
