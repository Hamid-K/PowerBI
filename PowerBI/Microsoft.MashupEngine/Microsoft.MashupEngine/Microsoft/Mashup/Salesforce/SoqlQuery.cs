using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001E7 RID: 487
	internal class SoqlQuery : DataSourceQuery
	{
		// Token: 0x060009AB RID: 2475 RVA: 0x00014094 File Offset: 0x00012294
		public SoqlQuery(SalesforceDataLoader dataLoader, SalesforceObjectDetail metadata)
		{
			this.dataLoader = dataLoader;
			this.metadata = metadata;
			this.range = RowRange.All;
			this.whereCondition = EmptyArray<SoqlExpression>.Instance;
			this.sortOrder = SoqlSortOrder.None;
			this.keyColumns = metadata.KeyColumnNames;
			this.columns = new SoqlColumns(metadata.Type);
			this.queryDomain = new SoqlQueryDomain(this.dataLoader.LoginUrl);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001410C File Offset: 0x0001230C
		public SoqlQuery(SoqlQuery baseQuery, SoqlColumns columns, RowRange range, SoqlExpression[] whereCondition, bool hasGroup, SoqlSortOrder sortOrder, Keys groupKeys = null)
		{
			this.dataLoader = baseQuery.dataLoader;
			this.metadata = baseQuery.metadata;
			this.columns = columns;
			this.range = range;
			this.whereCondition = whereCondition;
			this.hasGroup = hasGroup;
			this.sortOrder = sortOrder;
			if (baseQuery.keyColumns.All((string key) => columns.Names.Contains(key)))
			{
				this.keyColumns = baseQuery.keyColumns;
			}
			else
			{
				this.keyColumns = Keys.Empty;
			}
			if (this.keyColumns.Length == 0)
			{
				this.keyField = baseQuery.keyField;
			}
			else
			{
				this.keyField = this.metadata.Type.Fields.Keys.IndexOfKey(this.keyColumns[0]);
			}
			this.queryDomain = new SoqlQueryDomain(this.dataLoader.LoginUrl);
			this.groupKeys = groupKeys;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00014206 File Offset: 0x00012406
		public override Keys Columns
		{
			get
			{
				return this.columns.Names;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00014214 File Offset: 0x00012414
		public RowCount TakeCount
		{
			get
			{
				return this.range.TakeCount;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001422F File Offset: 0x0001242F
		public SalesforceObjectDetail Metadata
		{
			get
			{
				return this.metadata;
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00014237 File Offset: 0x00012437
		public override TypeValue GetColumnType(int index)
		{
			return this.columns.Types[index].Value.AsType;
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00014250 File Offset: 0x00012450
		public override IList<TableKey> TableKeys
		{
			get
			{
				if (this.tableKeys == null)
				{
					if (this.keyColumns == null)
					{
						this.tableKeys = EmptyArray<TableKey>.Instance;
					}
					else
					{
						int[] array = this.keyColumns.Select((string key) => this.Columns.IndexOfKey(key)).ToArray<int>();
						this.tableKeys = new TableKey[]
						{
							new TableKey(array, true)
						};
					}
				}
				return this.tableKeys;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000142B3 File Offset: 0x000124B3
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.queryDomain;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x000142BB File Offset: 0x000124BB
		public override IEngineHost EngineHost
		{
			get
			{
				return this.dataLoader.EngineHost;
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000142C8 File Offset: 0x000124C8
		public override Query Skip(RowCount count)
		{
			if (this.range.TakeCount.IsInfinite && !this.hasGroup)
			{
				return new SoqlQuery(this, this.columns, this.range.Skip(count), this.whereCondition, this.hasGroup, this.sortOrder, this.groupKeys);
			}
			return base.Skip(count);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00014330 File Offset: 0x00012530
		public override Query Take(RowCount count)
		{
			if (this.hasGroup)
			{
				bool flag = true;
				for (int i = 0; i < this.columns.Length; i++)
				{
					if (!this.columns.IsAggregate[i] && !this.columns.Expressions[i].IsConstant)
					{
						flag = false;
						break;
					}
				}
				if (flag || count.Value > 2000L)
				{
					return base.Take(count);
				}
			}
			return new SoqlQuery(this, this.columns, this.range.Take(count), this.whereCondition, this.hasGroup, this.sortOrder, this.groupKeys);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000143D5 File Offset: 0x000125D5
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			if (!this.hasGroup)
			{
				return new SoqlQuery(this, this.columns.SelectColumns(columnSelection), this.range, this.whereCondition, this.hasGroup, this.sortOrder, null);
			}
			return base.SelectColumns(columnSelection);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00014412 File Offset: 0x00012612
		public override Query RenameReorderColumns(ColumnSelection columnSelection)
		{
			if (!this.hasGroup)
			{
				return new SoqlQuery(this, this.columns.SelectColumns(columnSelection), this.range, this.whereCondition, this.hasGroup, this.sortOrder, null);
			}
			return base.RenameReorderColumns(columnSelection);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00014450 File Offset: 0x00012650
		public override Query SelectRows(FunctionValue condition)
		{
			if (!this.hasGroup && this.range.IsAll)
			{
				SoqlExpression soqlExpression = new SoqlExpression(this.metadata.Type, this.columns, TypeValue.Record, QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), condition));
				this.ValidateMemberOperation(soqlExpression, (SalesforceObjectField field) => field.Filterable);
				if (soqlExpression.Type.TypeKind == ValueKind.Logical)
				{
					SoqlExpression[] array = this.whereCondition.Add(soqlExpression);
					return new SoqlQuery(this, this.columns, this.range, array, this.hasGroup, this.sortOrder, null);
				}
			}
			return base.SelectRows(condition);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001450C File Offset: 0x0001270C
		public override Query AddColumns(ColumnsConstructor columnGenerator)
		{
			SoqlColumns soqlColumns;
			SoqlColumns soqlColumns2;
			if (!this.hasGroup && SoqlQuery.TryGetAddedColumns(this, columnGenerator, out soqlColumns) && this.columns.TryUnion(soqlColumns, out soqlColumns2))
			{
				return new SoqlQuery(this, soqlColumns2, this.range, this.whereCondition, this.hasGroup, this.sortOrder, null);
			}
			return base.AddColumns(columnGenerator);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00014564 File Offset: 0x00012764
		public override Query Distinct(TableDistinct distinctCriteria)
		{
			int[] array;
			if (this.range.IsAll && !this.hasGroup && this.sortOrder == SoqlSortOrder.None && this.Columns.Length == distinctCriteria.Distincts.Length && distinctCriteria.TryGetColumns(QueryTableValue.NewRowType(this), out array) && this.Columns.Length == array.Length)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = i;
				}
				TypeValue type = new QueryTableValue(this).Type;
				Grouping grouping = new Grouping(false, this.Columns, this.Columns, array, EmptyArray<ColumnConstructor>.Instance, true, null, type.AsTableType);
				return this.Group(grouping);
			}
			return base.Distinct(distinctCriteria);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00014628 File Offset: 0x00012828
		public override Query Sort(TableSortOrder tableSortOrder)
		{
			SoqlSortOrder soqlSortOrder;
			if (this.range.IsAll && this.sortOrder == SoqlSortOrder.None && !this.hasGroup && this.TryGetSortOrder(tableSortOrder, out soqlSortOrder))
			{
				return new SoqlQuery(this, this.columns, this.range, this.whereCondition, this.hasGroup, soqlSortOrder, this.groupKeys);
			}
			return base.Sort(tableSortOrder);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00014694 File Offset: 0x00012894
		public override Query Group(Grouping grouping)
		{
			SoqlColumns soqlColumns;
			if (this.range.IsAll && !this.hasGroup && this.sortOrder == SoqlSortOrder.None && this.TryGetGrouping(grouping, out soqlColumns))
			{
				SoqlColumns soqlColumns2 = this.columns.SelectColumns(grouping.KeyKeys).Add(soqlColumns);
				for (int i = 0; i < soqlColumns2.Length; i++)
				{
					if (!soqlColumns2.IsAggregate[i] && !soqlColumns2.Expressions[i].IsConstant && SalesforceTypes.IsScalar(soqlColumns2.Types[i].Value.AsType))
					{
						this.ValidateMemberOperation(soqlColumns2.Expressions[i], (SalesforceObjectField field) => field.Groupable);
					}
				}
				return new SoqlQuery(this, this.columns.SelectColumns(grouping.KeyKeys).Add(soqlColumns), this.range, this.whereCondition, true, this.sortOrder, grouping.KeyKeys);
			}
			return base.Group(grouping);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000147AB File Offset: 0x000129AB
		public override IEnumerable<IValueReference> GetRows()
		{
			return new SoqlQuery.RowEnumerable(this);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000147B4 File Offset: 0x000129B4
		private static bool TryGetAddedColumns(SoqlQuery query, ColumnsConstructor columnGenerator, out SoqlColumns addedColumns)
		{
			IList<QueryExpression> list = AddColumnsQuery.CreateQueryExpressions(columnGenerator.Function, QueryTableValue.NewRowType(query));
			if (list == null)
			{
				addedColumns = null;
				return false;
			}
			SoqlExpression[] array = new SoqlExpression[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SoqlExpression(query.metadata.Type, query.columns, TypeValue.Record, list[i]);
			}
			addedColumns = new SoqlColumns(columnGenerator.Names, array, columnGenerator.Types, false);
			return true;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00014830 File Offset: 0x00012A30
		private void ValidateMemberOperation(SoqlExpression expression, Func<SalesforceObjectField, bool> predicate)
		{
			SoqlQuery.ColumnAccessValidator.Validate(this.metadata, expression, predicate);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00014840 File Offset: 0x00012A40
		private bool TryGetGrouping(Grouping grouping, out SoqlColumns aggregates)
		{
			if (!grouping.Adjacent && grouping.Comparer == null)
			{
				if (!grouping.Constructors.Any((ColumnConstructor c) => !SalesforceTypes.IsScalar(c.Type.Value.AsType)))
				{
					KeysBuilder keysBuilder = default(KeysBuilder);
					SoqlExpression[] array = new SoqlExpression[grouping.Constructors.Length];
					IValueReference[] array2 = new IValueReference[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						ColumnConstructor columnConstructor = grouping.Constructors[i];
						keysBuilder.Add(columnConstructor.Name);
						array[i] = new SoqlExpression(this.metadata.Type, this.columns, TypeValue.Table, QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), columnConstructor.Function));
						array2[i] = columnConstructor.Type;
					}
					aggregates = new SoqlColumns(keysBuilder.ToKeys(), array, array2, true);
					return true;
				}
			}
			aggregates = null;
			return false;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00014920 File Offset: 0x00012B20
		private bool TryGetSortOrder(TableSortOrder sortOrder, out SoqlSortOrder soqlSortOrder)
		{
			QueryExpression[] array;
			bool[] array2;
			if (!SortQuery.TryGetSelectors(sortOrder, QueryTableValue.NewRowType(this), out array, out array2))
			{
				soqlSortOrder = null;
				return false;
			}
			SoqlExpression[] array3 = new SoqlExpression[array.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				if (array[i].Kind != QueryExpressionKind.ColumnAccess)
				{
					soqlSortOrder = null;
					return false;
				}
				array3[i] = this.columns.Expressions[((ColumnAccessQueryExpression)array[i]).Column];
			}
			soqlSortOrder = new SoqlSortOrder(array3, array2);
			return true;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00014998 File Offset: 0x00012B98
		private void CreateScript(SoqlWriter writer)
		{
			writer.Write(SoqlTokens.Select);
			writer.WriteSpace();
			Keys keys = this.metadata.Type.Fields.Keys;
			bool flag = true;
			bool flag2 = this.metadata.Name.Equals("Attachment");
			for (int i = 0; i < this.columns.Length; i++)
			{
				if (!this.columns.Expressions[i].IsConstant && (!flag2 || !this.columns.Names.ElementAt(i).Equals("Body")))
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						writer.Write(SoqlTokens.Comma);
					}
					this.WriteExpression(writer, this.columns.Expressions[i]);
				}
			}
			if (flag)
			{
				if (!this.hasGroup)
				{
					writer.WriteIdentifier(this.metadata.Type.Fields.Keys[this.keyField]);
				}
				else
				{
					for (int j = 0; j < this.columns.Length; j++)
					{
						if (!this.columns.IsAggregate[j] && !this.columns.Expressions[j].IsConstant)
						{
							this.WriteExpression(writer, this.columns.Expressions[j]);
							break;
						}
					}
				}
			}
			writer.WriteSpace();
			writer.Write(SoqlTokens.From);
			writer.WriteSpace();
			writer.WriteIdentifier(this.metadata.Name);
			for (int k = 0; k < this.whereCondition.Length; k++)
			{
				writer.WriteSpace();
				if (k == 0)
				{
					writer.Write(SoqlTokens.Where);
				}
				else
				{
					writer.Write(SoqlTokens.And);
				}
				writer.WriteSpace();
				this.WriteExpression(writer, this.whereCondition[k]);
			}
			if (this.hasGroup)
			{
				flag = true;
				for (int l = 0; l < this.columns.Length; l++)
				{
					if (!this.columns.IsAggregate[l] && !this.columns.Expressions[l].IsConstant)
					{
						if (flag)
						{
							writer.WriteSpace();
							writer.Write(SoqlTokens.Group);
							writer.WriteSpace();
							writer.Write(SoqlTokens.By);
							writer.WriteSpace();
							flag = false;
						}
						else
						{
							writer.Write(SoqlTokens.Comma);
						}
						this.WriteExpression(writer, this.columns.Expressions[l]);
					}
				}
			}
			if (this.sortOrder != SoqlSortOrder.None)
			{
				writer.WriteSpace();
				writer.Write(SoqlTokens.Order);
				writer.WriteSpace();
				writer.Write(SoqlTokens.By);
				writer.WriteSpace();
				for (int m = 0; m < this.sortOrder.Expressions.Length; m++)
				{
					if (m != 0)
					{
						writer.Write(SoqlTokens.Comma);
					}
					this.WriteExpression(writer, this.sortOrder.Expressions[m]);
					if (!this.sortOrder.Ascendings[m])
					{
						writer.WriteSpace();
						writer.Write(SoqlTokens.Desc);
					}
				}
			}
			if (!this.range.TakeCount.IsInfinite)
			{
				writer.WriteSpace();
				writer.Write(SoqlTokens.Limit);
				writer.WriteSpace();
				writer.Write(this.range.TakeCount.Value);
			}
			if (!this.range.SkipCount.IsZero)
			{
				writer.WriteSpace();
				writer.Write(SoqlTokens.Offset);
				writer.WriteSpace();
				writer.Write(this.range.SkipCount.Value);
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00014D40 File Offset: 0x00012F40
		private void WriteExpression(SoqlWriter writer, SoqlExpression condition)
		{
			string text = this.metadata.Type.Fields.Keys[this.keyField];
			new SoqlExpressionWriter(writer, this.metadata.Type.Fields.Keys, text).Write(condition);
		}

		// Token: 0x040005CF RID: 1487
		private const int SalesforcePageSize = 2000;

		// Token: 0x040005D0 RID: 1488
		private readonly SalesforceDataLoader dataLoader;

		// Token: 0x040005D1 RID: 1489
		private readonly SalesforceObjectDetail metadata;

		// Token: 0x040005D2 RID: 1490
		private readonly RowRange range;

		// Token: 0x040005D3 RID: 1491
		private readonly SoqlExpression[] whereCondition;

		// Token: 0x040005D4 RID: 1492
		private readonly SoqlColumns columns;

		// Token: 0x040005D5 RID: 1493
		private readonly bool hasGroup;

		// Token: 0x040005D6 RID: 1494
		private readonly SoqlSortOrder sortOrder;

		// Token: 0x040005D7 RID: 1495
		private readonly Keys keyColumns;

		// Token: 0x040005D8 RID: 1496
		private readonly int keyField;

		// Token: 0x040005D9 RID: 1497
		private readonly SoqlQueryDomain queryDomain;

		// Token: 0x040005DA RID: 1498
		private IList<TableKey> tableKeys;

		// Token: 0x040005DB RID: 1499
		private readonly Keys groupKeys;

		// Token: 0x020001E8 RID: 488
		private class RowEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x060009C5 RID: 2501 RVA: 0x00014D9E File Offset: 0x00012F9E
			public RowEnumerable(SoqlQuery query)
			{
				this.query = query;
			}

			// Token: 0x060009C6 RID: 2502 RVA: 0x00014DAD File Offset: 0x00012FAD
			public IEnumerator<IValueReference> GetEnumerator()
			{
				return new SoqlQuery.RowEnumerable.RowEnumerator(this.query);
			}

			// Token: 0x060009C7 RID: 2503 RVA: 0x00014DAD File Offset: 0x00012FAD
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SoqlQuery.RowEnumerable.RowEnumerator(this.query);
			}

			// Token: 0x040005DC RID: 1500
			private readonly SoqlQuery query;

			// Token: 0x020001E9 RID: 489
			private class RowEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x060009C8 RID: 2504 RVA: 0x00014DBC File Offset: 0x00012FBC
				public RowEnumerator(SoqlQuery query)
				{
					this.query = query;
					this.converters = new Func<JsonTokenizer, IValueReference>[this.query.Columns.Length];
					this.isConstant = new bool[this.query.Columns.Length];
					for (int i = 0; i < this.query.Columns.Length; i++)
					{
						Value constant;
						if (this.query.columns.Expressions[i].Expression.TryGetConstant(out constant))
						{
							this.converters[i] = (JsonTokenizer _) => constant;
							this.isConstant[i] = true;
						}
						else
						{
							this.converters[i] = SalesforceTypes.MakeJsonConverter(this.query.GetColumnType(i));
							this.isConstant[i] = false;
						}
					}
					bool flag = true;
					for (int j = 0; j < this.query.columns.Length; j++)
					{
						if (!this.query.columns.IsAggregate[j] && !this.query.columns.Expressions[j].IsConstant)
						{
							flag = false;
							break;
						}
					}
					this.multipartQuery = this.query.hasGroup && !flag && (this.query.TakeCount.IsInfinite || this.query.TakeCount.Value > 2000L);
					this.UpdateQueryText(null);
				}

				// Token: 0x060009C9 RID: 2505 RVA: 0x00014F40 File Offset: 0x00013140
				private BinaryQueryExpression BuildGroupCondition(int depth, RecordValue previousRecord)
				{
					if (depth == this.query.groupKeys.Length)
					{
						string text = this.query.groupKeys[depth - 1];
						return new BinaryQueryExpression(BinaryOperator2.GreaterThan, new ColumnAccessQueryExpression(this.query.columns.Names.IndexOfKey(text)), new ConstantQueryExpression(previousRecord[text]));
					}
					int num = this.query.columns.Names.IndexOfKey(this.query.groupKeys[depth - 1]);
					Value value = previousRecord[this.query.groupKeys[depth - 1]];
					return new BinaryQueryExpression(BinaryOperator2.Or, new BinaryQueryExpression(BinaryOperator2.GreaterThan, new ColumnAccessQueryExpression(num), new ConstantQueryExpression(value)), new BinaryQueryExpression(BinaryOperator2.And, new BinaryQueryExpression(BinaryOperator2.Equals, new ColumnAccessQueryExpression(num), new ConstantQueryExpression(value)), this.BuildGroupCondition(depth + 1, previousRecord)));
				}

				// Token: 0x060009CA RID: 2506 RVA: 0x00015024 File Offset: 0x00013224
				private void UpdateQueryText(IValueReference previousRow)
				{
					SoqlWriter soqlWriter = new SoqlWriter(null);
					SoqlQuery soqlQuery = this.query;
					if (this.multipartQuery)
					{
						if (previousRow != null)
						{
							BinaryQueryExpression binaryQueryExpression = this.BuildGroupCondition(1, previousRow.Value.AsRecord);
							SoqlExpression[] array = soqlQuery.whereCondition.Add(new SoqlExpression(soqlQuery.metadata.Type, soqlQuery.columns, TypeValue.Record, binaryQueryExpression));
							soqlQuery = new SoqlQuery(this.query, soqlQuery.columns, soqlQuery.range, array, true, soqlQuery.sortOrder, soqlQuery.groupKeys);
						}
						SoqlExpression[] array2 = new SoqlExpression[soqlQuery.groupKeys.Length];
						bool[] array3 = new bool[soqlQuery.groupKeys.Length];
						for (int i = 0; i < soqlQuery.groupKeys.Length; i++)
						{
							int num = soqlQuery.columns.Names.IndexOfKey(soqlQuery.groupKeys[i]);
							array2[i] = soqlQuery.columns.Expressions[num];
							array3[i] = true;
						}
						soqlQuery = new SoqlQuery(soqlQuery, soqlQuery.columns, soqlQuery.range.Take(new RowCount(2000L)), soqlQuery.whereCondition, true, new SoqlSortOrder(array2, array3), soqlQuery.groupKeys);
					}
					soqlQuery.CreateScript(soqlWriter);
					this.queryText = soqlWriter.ToText();
					string text = this.query.dataLoader.SoqlPath + Uri.EscapeDataString(this.queryText);
					this.request = SoqlQuery.RowEnumerable.SalesforceRequest.New(this, text, 0);
				}

				// Token: 0x170002D2 RID: 722
				// (get) Token: 0x060009CB RID: 2507 RVA: 0x000151A8 File Offset: 0x000133A8
				public IThreadPoolService ThreadPool
				{
					get
					{
						return this.query.dataLoader.ThreadPool;
					}
				}

				// Token: 0x170002D3 RID: 723
				// (get) Token: 0x060009CC RID: 2508 RVA: 0x000151BA File Offset: 0x000133BA
				public Keys Columns
				{
					get
					{
						return this.query.Columns;
					}
				}

				// Token: 0x170002D4 RID: 724
				// (get) Token: 0x060009CD RID: 2509 RVA: 0x000151C7 File Offset: 0x000133C7
				public Func<JsonTokenizer, IValueReference>[] Converters
				{
					get
					{
						return this.converters;
					}
				}

				// Token: 0x170002D5 RID: 725
				// (get) Token: 0x060009CE RID: 2510 RVA: 0x000151CF File Offset: 0x000133CF
				public bool[] IsConstant
				{
					get
					{
						return this.isConstant;
					}
				}

				// Token: 0x170002D6 RID: 726
				// (get) Token: 0x060009CF RID: 2511 RVA: 0x000151D7 File Offset: 0x000133D7
				public IValueReference Current
				{
					get
					{
						return this.currentRecord;
					}
				}

				// Token: 0x170002D7 RID: 727
				// (get) Token: 0x060009D0 RID: 2512 RVA: 0x000151D7 File Offset: 0x000133D7
				object IEnumerator.Current
				{
					get
					{
						return this.currentRecord;
					}
				}

				// Token: 0x060009D1 RID: 2513 RVA: 0x000151E0 File Offset: 0x000133E0
				public bool MoveNext()
				{
					bool flag;
					for (;;)
					{
						using (EngineContext.Leave())
						{
							if (this.request == null)
							{
								flag = false;
							}
							else
							{
								do
								{
									int num;
									this.currentRecord = this.request.GetNext(out this.request, out num);
									this.multipartContinue = num == 2000;
								}
								while (this.currentRecord == null && this.request != null);
								if (this.currentRecord != null)
								{
									this.previousRecord = this.currentRecord;
									flag = true;
								}
								else
								{
									if (this.multipartQuery && this.multipartContinue)
									{
										this.UpdateQueryText(this.previousRecord);
										continue;
									}
									flag = false;
								}
							}
						}
						break;
					}
					return flag;
				}

				// Token: 0x060009D2 RID: 2514 RVA: 0x000091AE File Offset: 0x000073AE
				public void Reset()
				{
					throw new NotImplementedException();
				}

				// Token: 0x060009D3 RID: 2515 RVA: 0x00015298 File Offset: 0x00013498
				public void Dispose()
				{
					if (this.request != null)
					{
						this.request.Dispose();
						this.request = null;
					}
				}

				// Token: 0x060009D4 RID: 2516 RVA: 0x000152B4 File Offset: 0x000134B4
				public JsonTokenizer FetchData(int page, string url)
				{
					string text = this.query.dataLoader.CreateCacheKey(new string[]
					{
						"Soql",
						this.queryText,
						page.ToString(CultureInfo.InvariantCulture)
					});
					return this.query.dataLoader.StreamJsonValueOutsideOfEngineContext(url, text);
				}

				// Token: 0x040005DD RID: 1501
				private readonly SoqlQuery query;

				// Token: 0x040005DE RID: 1502
				private readonly bool multipartQuery;

				// Token: 0x040005DF RID: 1503
				private readonly Func<JsonTokenizer, IValueReference>[] converters;

				// Token: 0x040005E0 RID: 1504
				private readonly bool[] isConstant;

				// Token: 0x040005E1 RID: 1505
				private string queryText;

				// Token: 0x040005E2 RID: 1506
				private IValueReference currentRecord;

				// Token: 0x040005E3 RID: 1507
				private IValueReference previousRecord;

				// Token: 0x040005E4 RID: 1508
				private SoqlQuery.RowEnumerable.SalesforceRequest request;

				// Token: 0x040005E5 RID: 1509
				private bool multipartContinue;
			}

			// Token: 0x020001EB RID: 491
			private class SalesforceRequest : IDisposable
			{
				// Token: 0x060009D7 RID: 2519 RVA: 0x00015312 File Offset: 0x00013512
				private SalesforceRequest(SoqlQuery.RowEnumerable.RowEnumerator parent, string url, int page)
				{
					this.lockObject = new object();
					this.parent = parent;
					this.url = url;
					this.page = page;
				}

				// Token: 0x060009D8 RID: 2520 RVA: 0x0001533C File Offset: 0x0001353C
				public static SoqlQuery.RowEnumerable.SalesforceRequest New(SoqlQuery.RowEnumerable.RowEnumerator parent, string url, int page)
				{
					SoqlQuery.RowEnumerable.SalesforceRequest salesforceRequest = new SoqlQuery.RowEnumerable.SalesforceRequest(parent, url, page);
					parent.ThreadPool.QueueUserWorkItem(new WaitCallback(SoqlQuery.RowEnumerable.SalesforceRequest.ExecuteInternal), salesforceRequest);
					return salesforceRequest;
				}

				// Token: 0x060009D9 RID: 2521 RVA: 0x0001536C File Offset: 0x0001356C
				public void Dispose()
				{
					SoqlQuery.RowEnumerable.SalesforceRequest salesforceRequest = null;
					object obj = this.lockObject;
					lock (obj)
					{
						if (this.next != null)
						{
							salesforceRequest = this.next;
							this.next = null;
						}
						this.disposed = true;
					}
					if (salesforceRequest != null)
					{
						salesforceRequest.Dispose();
					}
				}

				// Token: 0x060009DA RID: 2522 RVA: 0x000153D0 File Offset: 0x000135D0
				private void ExecuteWithExceptionHandling()
				{
					List<IValueReference> list = null;
					try
					{
						list = this.Execute();
					}
					catch (Exception ex)
					{
						if (!SafeExceptions.IsSafeException(ex))
						{
							throw;
						}
						this.exception = ex;
					}
					object obj = this.lockObject;
					lock (obj)
					{
						this.records = list ?? new List<IValueReference>(0);
					}
				}

				// Token: 0x060009DB RID: 2523 RVA: 0x00015448 File Offset: 0x00013648
				private List<IValueReference> Execute()
				{
					object obj = this.lockObject;
					lock (obj)
					{
						if (this.disposed)
						{
							return null;
						}
					}
					List<IValueReference> list2;
					using (JsonTokenizer jsonTokenizer = this.parent.FetchData(this.page, this.url))
					{
						if (jsonTokenizer.GetNextToken() != JsonToken.RecordStart)
						{
							this.ParseError();
						}
						if (jsonTokenizer.GetNextToken() != JsonToken.RecordKey)
						{
							this.ParseError();
						}
						if (jsonTokenizer.TokenTextMatches("query") && (jsonTokenizer.GetNextToken() != JsonToken.String || jsonTokenizer.GetNextToken() != JsonToken.RecordKey))
						{
							this.ParseError();
						}
						int num = 0;
						if (!jsonTokenizer.TokenTextMatches("totalSize") || jsonTokenizer.GetNextToken() != JsonToken.Number || !jsonTokenizer.TryParse(out num))
						{
							this.ParseError();
						}
						List<IValueReference> list = new List<IValueReference>(Math.Max(1, Math.Min(num, 1000)));
						JsonToken jsonToken = JsonToken.True;
						if (jsonTokenizer.GetNextToken() != JsonToken.RecordKey || !jsonTokenizer.TokenTextMatches("done") || ((jsonToken = jsonTokenizer.GetNextToken()) != JsonToken.True && jsonToken != JsonToken.False))
						{
							this.ParseError();
						}
						bool flag2 = jsonToken == JsonToken.True;
						if (!flag2 && (jsonTokenizer.GetNextToken() != JsonToken.RecordKey || !jsonTokenizer.TokenTextMatches("nextRecordsUrl") || jsonTokenizer.GetNextToken() != JsonToken.String))
						{
							this.ParseError();
						}
						if (!flag2)
						{
							obj = this.lockObject;
							lock (obj)
							{
								if (!this.disposed)
								{
									this.next = SoqlQuery.RowEnumerable.SalesforceRequest.New(this.parent, jsonTokenizer.GetTokenText(), this.page + 1);
								}
							}
						}
						if (jsonTokenizer.GetNextToken() != JsonToken.RecordKey || !jsonTokenizer.TokenTextMatches("records") || jsonTokenizer.GetNextToken() != JsonToken.ListStart)
						{
							this.ParseError();
						}
						Keys columns = this.parent.Columns;
						bool[] isConstant = this.parent.IsConstant;
						Func<JsonTokenizer, IValueReference>[] converters = this.parent.Converters;
						while (jsonTokenizer.GetNextToken() == JsonToken.RecordStart)
						{
							if (jsonTokenizer.GetNextToken() != JsonToken.RecordKey)
							{
								this.ParseError();
							}
							TextValue textValue = null;
							TextValue textValue2 = null;
							Value value = jsonTokenizer.ReadValue();
							if (value.IsRecord)
							{
								RecordValue asRecord = value.AsRecord;
								if (asRecord.Keys.Contains("url") && asRecord.Keys.Contains("type"))
								{
									Value value2 = asRecord["url"];
									Value value3 = asRecord["type"];
									if (value2.IsText && value3.IsText)
									{
										textValue = (TextValue)value2;
										textValue2 = (TextValue)value3;
									}
								}
							}
							IValueReference[] array = new IValueReference[columns.Length];
							for (int i = 0; i < columns.Length; i++)
							{
								if (isConstant[i])
								{
									array[i] = converters[i](null);
								}
								else if (textValue != null && textValue2 != null && textValue2.Equals(TextValue.New("Attachment")) && columns.ElementAt(i).Equals("Body"))
								{
									if (jsonTokenizer.PeekNextToken() == JsonToken.RecordKey)
									{
										if (jsonTokenizer.TokenTextMatches("Body"))
										{
											jsonTokenizer.GetNextToken();
											jsonTokenizer.SkipValue();
										}
									}
									else
									{
										this.ParseError();
									}
									array[i] = textValue.Concatenate(TextValue.New("/Body"));
								}
								else if (jsonTokenizer.GetNextToken() == JsonToken.RecordKey)
								{
									array[i] = converters[i](jsonTokenizer);
								}
								else
								{
									this.ParseError();
								}
							}
							if (jsonTokenizer.GetNextToken() != JsonToken.RecordEnd)
							{
								this.ParseError();
							}
							list.Add(RecordValue.New(columns, array));
						}
						list2 = list;
					}
					return list2;
				}

				// Token: 0x060009DC RID: 2524 RVA: 0x0001580C File Offset: 0x00013A0C
				public IValueReference GetNext(out SoqlQuery.RowEnumerable.SalesforceRequest nextResult, out int rowCount)
				{
					while (this.records == null)
					{
						SoqlQuery.RowEnumerable.SalesforceRequest.Sleep();
					}
					rowCount = this.records.Count;
					if (this.exception != null)
					{
						throw this.exception;
					}
					if (this.position < this.records.Count)
					{
						nextResult = this;
						List<IValueReference> list = this.records;
						int num = this.position;
						this.position = num + 1;
						return list[num];
					}
					nextResult = this.next;
					return null;
				}

				// Token: 0x060009DD RID: 2525 RVA: 0x00015880 File Offset: 0x00013A80
				private static void ExecuteInternal(object request)
				{
					((SoqlQuery.RowEnumerable.SalesforceRequest)request).ExecuteWithExceptionHandling();
				}

				// Token: 0x060009DE RID: 2526 RVA: 0x0001588D File Offset: 0x00013A8D
				private void ParseError()
				{
					throw new InvalidDataException();
				}

				// Token: 0x060009DF RID: 2527 RVA: 0x00015894 File Offset: 0x00013A94
				private static void Sleep()
				{
					Thread.MemoryBarrier();
					Thread.Sleep(10);
				}

				// Token: 0x040005E7 RID: 1511
				private readonly object lockObject;

				// Token: 0x040005E8 RID: 1512
				private readonly SoqlQuery.RowEnumerable.RowEnumerator parent;

				// Token: 0x040005E9 RID: 1513
				private readonly string url;

				// Token: 0x040005EA RID: 1514
				private readonly int page;

				// Token: 0x040005EB RID: 1515
				private List<IValueReference> records;

				// Token: 0x040005EC RID: 1516
				private SoqlQuery.RowEnumerable.SalesforceRequest next;

				// Token: 0x040005ED RID: 1517
				private int position;

				// Token: 0x040005EE RID: 1518
				private Exception exception;

				// Token: 0x040005EF RID: 1519
				private bool disposed;
			}
		}

		// Token: 0x020001EC RID: 492
		private class ColumnAccessValidator : QueryExpressionVisitor
		{
			// Token: 0x060009E0 RID: 2528 RVA: 0x000158A2 File Offset: 0x00013AA2
			private ColumnAccessValidator(SalesforceObjectDetail metadata, Func<SalesforceObjectField, bool> predicate)
			{
				this.metadata = metadata;
				this.predicate = predicate;
			}

			// Token: 0x060009E1 RID: 2529 RVA: 0x000158B8 File Offset: 0x00013AB8
			public static void Validate(SalesforceObjectDetail metadata, SoqlExpression expression, Func<SalesforceObjectField, bool> predicate)
			{
				new SoqlQuery.ColumnAccessValidator(metadata, predicate).Visit(expression.Expression);
			}

			// Token: 0x060009E2 RID: 2530 RVA: 0x000158CD File Offset: 0x00013ACD
			protected override QueryExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
			{
				if (!this.predicate(this.metadata.Fields[columnAccess.Column]))
				{
					throw new NotSupportedException();
				}
				return columnAccess;
			}

			// Token: 0x040005F0 RID: 1520
			private readonly SalesforceObjectDetail metadata;

			// Token: 0x040005F1 RID: 1521
			private readonly Func<SalesforceObjectField, bool> predicate;
		}
	}
}
