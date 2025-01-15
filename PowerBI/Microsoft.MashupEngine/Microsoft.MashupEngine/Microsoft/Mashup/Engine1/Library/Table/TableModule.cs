using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Lines;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Table
{
	// Token: 0x020002DD RID: 733
	public sealed class TableModule : Module
	{
		// Token: 0x06001D0C RID: 7436 RVA: 0x00048068 File Offset: 0x00046268
		static TableModule()
		{
			InliningVisitor.RegisterSimpleFunction(TableModule.ItemExpression.ItemFunction);
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x00048074 File Offset: 0x00046274
		public override string Name
		{
			get
			{
				return "TableModule";
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0004807B File Offset: 0x0004627B
		public override Keys ExportKeys
		{
			get
			{
				if (TableModule.exportKeys == null)
				{
					TableModule.exportKeys = Keys.New(79, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "Table.Type";
						case 1:
							return "Table.ColumnNames";
						case 2:
							return "Tables.GetRelationships";
						case 3:
							return "Table.FromColumns";
						case 4:
							return "Table.FromPartitions";
						case 5:
							return "Table.PartitionValues";
						case 6:
							return "Table.FromRows";
						case 7:
							return "Table.PromoteHeaders";
						case 8:
							return "Table.RowCount";
						case 9:
							return "Table.ApproximateRowCount";
						case 10:
							return "Table.ToRecords";
						case 11:
							return "Table.FromRecords";
						case 12:
							return "Table.Keys";
						case 13:
							return "Table.AddKey";
						case 14:
							return "Table.ReplaceKeys";
						case 15:
							return "Type.TableColumn";
						case 16:
							return "Type.TableRow";
						case 17:
							return "Type.TableKeys";
						case 18:
							return "Type.AddTableKey";
						case 19:
							return "Type.ReplaceTableKeys";
						case 20:
							return "Table.Column";
						case 21:
							return "Table.SelectColumns";
						case 22:
							return "Table.SelectRows";
						case 23:
							return "Table.RenameColumns";
						case 24:
							return "Table.TransformColumnNames";
						case 25:
							return "Table.ReorderColumns";
						case 26:
							return "Table.Skip";
						case 27:
							return "Table.First";
						case 28:
							return "Table.FirstN";
						case 29:
							return "Table.RemoveColumns";
						case 30:
							return "Table.Group";
						case 31:
							return "Table.Join";
						case 32:
							return "Table.AddJoinColumn";
						case 33:
							return "Table.NestedJoin";
						case 34:
							return "Table.AddIndexColumn";
						case 35:
							return "Table.FillDown";
						case 36:
							return "Table.TransformColumns";
						case 37:
							return "Table.TransformColumnTypes";
						case 38:
							return "Table.Sort";
						case 39:
							return "Table.Distinct";
						case 40:
							return "Table.ExpandRecordColumn";
						case 41:
							return "Table.AggregateTableColumn";
						case 42:
							return "Table.SingleRow";
						case 43:
							return "Table.Combine";
						case 44:
							return "Table.Pivot";
						case 45:
							return "Table.Unpivot";
						case 46:
							return "Table.UnpivotOtherColumns";
						case 47:
							return "Table.SelectRowsWithErrors";
						case 48:
							return "Table.RemoveRowsWithErrors";
						case 49:
							return "Table.ReplaceErrorValues";
						case 50:
							return "Table.ReplaceRelationshipIdentity";
						case 51:
							return "Table.FilterWithDataTable";
						case 52:
							return "Table.Split";
						case 53:
							return "Table.SplitAt";
						case 54:
							return "Table.Buffer";
						case 55:
							return "Table.StopFolding";
						case 56:
							return "Function.InvokeWithErrorContext";
						case 57:
							return "Table.WithErrorContext";
						case 58:
							return "Action.WithErrorContext";
						case 59:
							return TableModule.JoinAlgorithmEnum.Type.GetName();
						case 60:
							return TableModule.JoinAlgorithmEnum.Dynamic.GetName();
						case 61:
							return TableModule.JoinAlgorithmEnum.PairwiseHash.GetName();
						case 62:
							return TableModule.JoinAlgorithmEnum.SortMerge.GetName();
						case 63:
							return TableModule.JoinAlgorithmEnum.LeftHash.GetName();
						case 64:
							return TableModule.JoinAlgorithmEnum.RightHash.GetName();
						case 65:
							return TableModule.JoinAlgorithmEnum.LeftIndex.GetName();
						case 66:
							return TableModule.JoinAlgorithmEnum.RightIndex.GetName();
						case 67:
							return TableModule.JoinSide.Type.GetName();
						case 68:
							return TableModule.JoinSide.Left.GetName();
						case 69:
							return TableModule.JoinSide.Right.GetName();
						case 70:
							return "RowExpression.From";
						case 71:
							return "RowExpression.Row";
						case 72:
							return "RowExpression.Column";
						case 73:
							return "ItemExpression.From";
						case 74:
							return "ItemExpression.Item";
						case 75:
							return TableModule.RankKindEnum.Type.GetName();
						case 76:
							return TableModule.RankKindEnum.Competition.GetName();
						case 77:
							return TableModule.RankKindEnum.Dense.GetName();
						case 78:
							return TableModule.RankKindEnum.Ordinal.GetName();
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
					});
				}
				return TableModule.exportKeys;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x000480B4 File Offset: 0x000462B4
		public override Keys SectionKeys
		{
			get
			{
				if (TableModule.sectionKeys == null)
				{
					TableModule.sectionKeys = Keys.New(8, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "Value.Expression";
						case 1:
							return "Type.ForTable";
						case 2:
							return "Function.PreserveTableLineage";
						case 3:
							return "Table.AddColumns";
						case 4:
							return "Table.ExpandListColumn";
						case 5:
							return "Table.SortDescending";
						case 6:
							return "Table.FromHandlers";
						case 7:
							return "Table.AddAccumulatedColumn";
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
					});
				}
				return TableModule.sectionKeys;
			}
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000480EC File Offset: 0x000462EC
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return TypeValue.Table;
				case 1:
					return TableModule.Table.ColumnNames;
				case 2:
					return TableModule.Tables.GetRelationships;
				case 3:
					return TableModule.Table.FromColumns;
				case 4:
					return TableModule.Table.FromPartitions;
				case 5:
					return TableModule.Table.PartitionValues;
				case 6:
					return TableModule.Table.FromRows;
				case 7:
					return FoldableFunctionValue.New(new TableModule.Table.PromoteHeadersFunctionValue(hostEnvironment));
				case 8:
					return TableModule.Table.RowCount;
				case 9:
					return TableModule.Table.ApproximateRowCount;
				case 10:
					return TableModule.Table.ToRecords;
				case 11:
					return TableModule.Table.FromRecords;
				case 12:
					return TableModule.Table.Keys;
				case 13:
					return TableModule.Table.AddKey;
				case 14:
					return TableModule.Table.ReplaceKeys;
				case 15:
					return TableModule._Type.TableColumn;
				case 16:
					return TableModule._Type.TableRow;
				case 17:
					return TableModule._Type.TableKeys;
				case 18:
					return TableModule._Type.AddTableKey;
				case 19:
					return TableModule._Type.ReplaceTableKeys;
				case 20:
					return TableModule.Table.Column;
				case 21:
					return TableModule.Table.SelectColumns;
				case 22:
					return TableModule.Table.SelectRows;
				case 23:
					return TableModule.Table.RenameColumns;
				case 24:
					return TableModule.Table.TransformColumnNames;
				case 25:
					return TableModule.Table.ReorderColumns;
				case 26:
					return TableModule.Table.Skip;
				case 27:
					return TableModule.Table.First;
				case 28:
					return TableModule.Table.FirstN;
				case 29:
					return TableModule.Table.RemoveColumns;
				case 30:
					return TableModule.Table.Group;
				case 31:
					return TableModule.Table.Join;
				case 32:
					return TableModule.Table.AddJoinColumn;
				case 33:
					return TableModule.Table.NestedJoin;
				case 34:
					return TableModule.Table.AddIndexColumn;
				case 35:
					return TableModule.Table.FillDown;
				case 36:
					return TableModule.Table.TransformColumns;
				case 37:
					return new TableModule.Table.TransformColumnTypesFunctionValue(hostEnvironment);
				case 38:
					return TableModule.Table.Sort;
				case 39:
					return TableModule.Table.Distinct;
				case 40:
					return TableModule.Table.ExpandRecordColumn;
				case 41:
					return TableModule.Table.AggregateTableColumn;
				case 42:
					return TableModule.Table.SingleRow;
				case 43:
					return TableModule.Table.Combine;
				case 44:
					return TableModule.Table.Pivot;
				case 45:
					return TableModule.Table.Unpivot;
				case 46:
					return TableModule.Table.UnpivotOtherColumns;
				case 47:
					return TableModule.Table.SelectRowsWithErrors;
				case 48:
					return TableModule.Table.RemoveRowsWithErrors;
				case 49:
					return TableModule.Table.ReplaceErrorValues;
				case 50:
					return TableModule.Table.ReplaceRelationshipIdentity;
				case 51:
					return new TableModule.Table.FilterWithDataTableFunctionValue(hostEnvironment);
				case 52:
					return TableModule.Table.Split;
				case 53:
					return new TableModule.Table.SplitAtFunctionValue(hostEnvironment);
				case 54:
					return TableModule.Table.Buffer;
				case 55:
					return TableModule.Table.StopFolding;
				case 56:
					return TableModule.Table.Function_InvokeWithErrorContext;
				case 57:
					return TableModule.Table.Table_WithErrorContext;
				case 58:
					return TableModule.Table.Action_WithErrorContext;
				case 59:
					return TableModule.JoinAlgorithmEnum.Type;
				case 60:
					return TableModule.JoinAlgorithmEnum.Dynamic;
				case 61:
					return TableModule.JoinAlgorithmEnum.PairwiseHash;
				case 62:
					return TableModule.JoinAlgorithmEnum.SortMerge;
				case 63:
					return TableModule.JoinAlgorithmEnum.LeftHash;
				case 64:
					return TableModule.JoinAlgorithmEnum.RightHash;
				case 65:
					return TableModule.JoinAlgorithmEnum.LeftIndex;
				case 66:
					return TableModule.JoinAlgorithmEnum.RightIndex;
				case 67:
					return TableModule.JoinSide.Type;
				case 68:
					return TableModule.JoinSide.Left;
				case 69:
					return TableModule.JoinSide.Right;
				case 70:
					return TableModule.ItemExpression.From;
				case 71:
					return TableModule.ItemExpression.Item;
				case 72:
					return TableModule.RowExpression.Column;
				case 73:
					return TableModule.ItemExpression.From;
				case 74:
					return TableModule.ItemExpression.Item;
				case 75:
					return TableModule.RankKindEnum.Type;
				case 76:
					return TableModule.RankKindEnum.Competition;
				case 77:
					return TableModule.RankKindEnum.Dense;
				case 78:
					return TableModule.RankKindEnum.Ordinal;
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			});
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00048120 File Offset: 0x00046320
		protected override RecordValue GetSectionExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.SectionKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return Library._Value.Expression;
				case 1:
					return TableModule.Type.ForTable;
				case 2:
					return Library.Function.PreserveTableLineage;
				case 3:
					return TableModule.Table.AddColumns;
				case 4:
					return TableModule.Table.ExpandListColumn;
				case 5:
					return TableModule.Table.SortDescending;
				case 6:
					return new TableModule.Table.FromHandlersFunctionValue(hostEnvironment);
				case 7:
					return TableModule.Table.AddAccumulatedColumn;
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			});
		}

		// Token: 0x040009DA RID: 2522
		private const string ItemExpression_Item = "ItemExpression.Item";

		// Token: 0x040009DB RID: 2523
		private static Keys exportKeys;

		// Token: 0x040009DC RID: 2524
		private static Keys sectionKeys;

		// Token: 0x020002DE RID: 734
		private enum Exports
		{
			// Token: 0x040009DE RID: 2526
			Type,
			// Token: 0x040009DF RID: 2527
			ColumnNames,
			// Token: 0x040009E0 RID: 2528
			GetRelationships,
			// Token: 0x040009E1 RID: 2529
			FromColumns,
			// Token: 0x040009E2 RID: 2530
			FromPartitions,
			// Token: 0x040009E3 RID: 2531
			PartitionValues,
			// Token: 0x040009E4 RID: 2532
			FromRows,
			// Token: 0x040009E5 RID: 2533
			PromoteHeaders,
			// Token: 0x040009E6 RID: 2534
			RowCount,
			// Token: 0x040009E7 RID: 2535
			ApproximateRowCount,
			// Token: 0x040009E8 RID: 2536
			ToRecords,
			// Token: 0x040009E9 RID: 2537
			FromRecords,
			// Token: 0x040009EA RID: 2538
			Keys,
			// Token: 0x040009EB RID: 2539
			AddKey,
			// Token: 0x040009EC RID: 2540
			ReplaceKeys,
			// Token: 0x040009ED RID: 2541
			TypeTableColumn,
			// Token: 0x040009EE RID: 2542
			TypeTableRow,
			// Token: 0x040009EF RID: 2543
			TypeTableKeys,
			// Token: 0x040009F0 RID: 2544
			TypeAddTableKey,
			// Token: 0x040009F1 RID: 2545
			TypeReplaceTableKeys,
			// Token: 0x040009F2 RID: 2546
			Column,
			// Token: 0x040009F3 RID: 2547
			SelectColumns,
			// Token: 0x040009F4 RID: 2548
			SelectRows,
			// Token: 0x040009F5 RID: 2549
			RenameColumns,
			// Token: 0x040009F6 RID: 2550
			TransformColumnNames,
			// Token: 0x040009F7 RID: 2551
			ReorderColumns,
			// Token: 0x040009F8 RID: 2552
			TableSkip,
			// Token: 0x040009F9 RID: 2553
			TableFirst,
			// Token: 0x040009FA RID: 2554
			TableFirstN,
			// Token: 0x040009FB RID: 2555
			RemoveColumns,
			// Token: 0x040009FC RID: 2556
			Group,
			// Token: 0x040009FD RID: 2557
			Join,
			// Token: 0x040009FE RID: 2558
			AddJoinColumn,
			// Token: 0x040009FF RID: 2559
			NestedJoin,
			// Token: 0x04000A00 RID: 2560
			AddIndexColumn,
			// Token: 0x04000A01 RID: 2561
			FillDown,
			// Token: 0x04000A02 RID: 2562
			TransformColumns,
			// Token: 0x04000A03 RID: 2563
			TransformColumnTypes,
			// Token: 0x04000A04 RID: 2564
			Sort,
			// Token: 0x04000A05 RID: 2565
			Distinct,
			// Token: 0x04000A06 RID: 2566
			ExpandRecordColumn,
			// Token: 0x04000A07 RID: 2567
			AggregateTableColumn,
			// Token: 0x04000A08 RID: 2568
			SingleRow,
			// Token: 0x04000A09 RID: 2569
			Combine,
			// Token: 0x04000A0A RID: 2570
			Pivot,
			// Token: 0x04000A0B RID: 2571
			Unpivot,
			// Token: 0x04000A0C RID: 2572
			UnpivotOtherColumns,
			// Token: 0x04000A0D RID: 2573
			SelectRowsWithErrors,
			// Token: 0x04000A0E RID: 2574
			RemoveRowsWithErrors,
			// Token: 0x04000A0F RID: 2575
			ReplaceErrorValues,
			// Token: 0x04000A10 RID: 2576
			ReplaceRelationshipIdentity,
			// Token: 0x04000A11 RID: 2577
			FilterWithDataTable,
			// Token: 0x04000A12 RID: 2578
			Split,
			// Token: 0x04000A13 RID: 2579
			SplitAt,
			// Token: 0x04000A14 RID: 2580
			Buffer,
			// Token: 0x04000A15 RID: 2581
			StopFolding,
			// Token: 0x04000A16 RID: 2582
			Function_InvokeWithErrorContext,
			// Token: 0x04000A17 RID: 2583
			Table_WithErrorContext,
			// Token: 0x04000A18 RID: 2584
			Action_WithErrorContext,
			// Token: 0x04000A19 RID: 2585
			JoinAlgorithm_Type,
			// Token: 0x04000A1A RID: 2586
			JoinAlgorithm_Dynamic,
			// Token: 0x04000A1B RID: 2587
			JoinAlgorithm_PairwiseHash,
			// Token: 0x04000A1C RID: 2588
			JoinAlgorithm_SortMerge,
			// Token: 0x04000A1D RID: 2589
			JoinAlgorithm_LeftHash,
			// Token: 0x04000A1E RID: 2590
			JoinAlgorithm_RightHash,
			// Token: 0x04000A1F RID: 2591
			JoinAlgorithm_LeftIndex,
			// Token: 0x04000A20 RID: 2592
			JoinAlgorithm_RightIndex,
			// Token: 0x04000A21 RID: 2593
			JoinSide_Type,
			// Token: 0x04000A22 RID: 2594
			JoinSide_Left,
			// Token: 0x04000A23 RID: 2595
			JoinSide_Right,
			// Token: 0x04000A24 RID: 2596
			RowExpression_From,
			// Token: 0x04000A25 RID: 2597
			RowExpression_Row,
			// Token: 0x04000A26 RID: 2598
			RowExpression_Column,
			// Token: 0x04000A27 RID: 2599
			ItemExpression_From,
			// Token: 0x04000A28 RID: 2600
			ItemExpression_Item,
			// Token: 0x04000A29 RID: 2601
			RankKind_Type,
			// Token: 0x04000A2A RID: 2602
			RankKind_Competition,
			// Token: 0x04000A2B RID: 2603
			RankKind_Dense,
			// Token: 0x04000A2C RID: 2604
			RankKind_Ordinal,
			// Token: 0x04000A2D RID: 2605
			Count
		}

		// Token: 0x020002DF RID: 735
		private enum SectionExports
		{
			// Token: 0x04000A2F RID: 2607
			Value_Expression,
			// Token: 0x04000A30 RID: 2608
			Type_ForTable,
			// Token: 0x04000A31 RID: 2609
			Function_PreserveTableLineage,
			// Token: 0x04000A32 RID: 2610
			AddColumns,
			// Token: 0x04000A33 RID: 2611
			ExpandListColumn,
			// Token: 0x04000A34 RID: 2612
			SortDescending,
			// Token: 0x04000A35 RID: 2613
			FromHandlers,
			// Token: 0x04000A36 RID: 2614
			AddAccumulatedColumn,
			// Token: 0x04000A37 RID: 2615
			Count
		}

		// Token: 0x020002E0 RID: 736
		private static class Column
		{
			// Token: 0x04000A38 RID: 2616
			public static readonly FunctionValue IsColumn = new TableModule.Column.IsColumnFunctionValue();

			// Token: 0x020002E1 RID: 737
			public static class Part
			{
				// Token: 0x04000A39 RID: 2617
				public const int Name = 0;

				// Token: 0x04000A3A RID: 2618
				public const int Values = 1;
			}

			// Token: 0x020002E2 RID: 738
			private sealed class IsColumnFunctionValue : NativeFunctionValue1<LogicalValue, Value>
			{
				// Token: 0x06001D14 RID: 7444 RVA: 0x0004815D File Offset: 0x0004635D
				public IsColumnFunctionValue()
					: base(TypeValue.Logical, "value", TypeValue.Any)
				{
				}

				// Token: 0x06001D15 RID: 7445 RVA: 0x00048174 File Offset: 0x00046374
				public override LogicalValue TypedInvoke(Value value)
				{
					return LogicalValue.New(value.IsList && value.AsList.Count == 2 && value.AsList[0].IsText && value.AsList[1].IsList);
				}
			}
		}

		// Token: 0x020002E3 RID: 739
		public static class JoinAlgorithmEnum
		{
			// Token: 0x04000A3B RID: 2619
			public static readonly IntEnumTypeValue<JoinAlgorithm> Type = new IntEnumTypeValue<JoinAlgorithm>("JoinAlgorithm.Type");

			// Token: 0x04000A3C RID: 2620
			public static readonly NumberValue Dynamic = TableModule.JoinAlgorithmEnum.Type.NewEnumValue("JoinAlgorithm.Dynamic", 0, JoinAlgorithm.Dynamic, null);

			// Token: 0x04000A3D RID: 2621
			public static readonly NumberValue PairwiseHash = TableModule.JoinAlgorithmEnum.Type.NewEnumValue("JoinAlgorithm.PairwiseHash", 1, JoinAlgorithm.PairwiseHash, null);

			// Token: 0x04000A3E RID: 2622
			public static readonly NumberValue SortMerge = TableModule.JoinAlgorithmEnum.Type.NewEnumValue("JoinAlgorithm.SortMerge", 2, JoinAlgorithm.SortMerge, null);

			// Token: 0x04000A3F RID: 2623
			public static readonly NumberValue LeftHash = TableModule.JoinAlgorithmEnum.Type.NewEnumValue("JoinAlgorithm.LeftHash", 3, JoinAlgorithm.LeftHash, null);

			// Token: 0x04000A40 RID: 2624
			public static readonly NumberValue RightHash = TableModule.JoinAlgorithmEnum.Type.NewEnumValue("JoinAlgorithm.RightHash", 4, JoinAlgorithm.RightHash, null);

			// Token: 0x04000A41 RID: 2625
			public static readonly NumberValue LeftIndex = TableModule.JoinAlgorithmEnum.Type.NewEnumValue("JoinAlgorithm.LeftIndex", 5, JoinAlgorithm.LeftIndex, null);

			// Token: 0x04000A42 RID: 2626
			public static readonly NumberValue RightIndex = TableModule.JoinAlgorithmEnum.Type.NewEnumValue("JoinAlgorithm.RightIndex", 6, JoinAlgorithm.RightIndex, null);
		}

		// Token: 0x020002E4 RID: 740
		public static class JoinSide
		{
			// Token: 0x04000A43 RID: 2627
			public static readonly IntEnumTypeValue<NumberValue> Type = new IntEnumTypeValue<NumberValue>("JoinSide.Type");

			// Token: 0x04000A44 RID: 2628
			public static readonly NumberValue Left = TableModule.JoinSide.Type.NewEnumValue("JoinSide.Left", 0, NumberValue.Zero, null);

			// Token: 0x04000A45 RID: 2629
			public static readonly NumberValue Right = TableModule.JoinSide.Type.NewEnumValue("JoinSide.Right", 1, NumberValue.One, null);
		}

		// Token: 0x020002E5 RID: 741
		public static class RankKindEnum
		{
			// Token: 0x04000A46 RID: 2630
			public static readonly IntEnumTypeValue<NumberValue> Type = new IntEnumTypeValue<NumberValue>("RankKind.Type");

			// Token: 0x04000A47 RID: 2631
			public static readonly NumberValue Competition = TableModule.RankKindEnum.Type.NewEnumValue("RankKind.Competition", 0, NumberValue.Zero, null);

			// Token: 0x04000A48 RID: 2632
			public static readonly NumberValue Dense = TableModule.RankKindEnum.Type.NewEnumValue("RankKind.Dense", 1, NumberValue.One, null);

			// Token: 0x04000A49 RID: 2633
			public static readonly NumberValue Ordinal = TableModule.RankKindEnum.Type.NewEnumValue("RankKind.Ordinal", 2, NumberValue.New(2), null);
		}

		// Token: 0x020002E6 RID: 742
		public static class _Type
		{
			// Token: 0x04000A4A RID: 2634
			public static readonly FunctionValue TableRow = new TableModule._Type.TableRowFunctionValue();

			// Token: 0x04000A4B RID: 2635
			public static readonly FunctionValue TableKeys = new TableModule._Type.TableKeysFunctionValue();

			// Token: 0x04000A4C RID: 2636
			public static readonly FunctionValue AddTableKey = new TableModule._Type.AddTableKeyFunctionValue();

			// Token: 0x04000A4D RID: 2637
			public static readonly FunctionValue ReplaceTableKeys = new TableModule._Type.ReplaceTableKeysFunctionValue();

			// Token: 0x04000A4E RID: 2638
			public static readonly FunctionValue TableColumn = new TableModule._Type.TableColumnFunctionValue();

			// Token: 0x020002E7 RID: 743
			private sealed class TableColumnFunctionValue : NativeFunctionValue2<TypeValue, TypeValue, TextValue>
			{
				// Token: 0x06001D1A RID: 7450 RVA: 0x00048396 File Offset: 0x00046596
				public TableColumnFunctionValue()
					: base(TypeValue._Type, "tableType", TypeValue._Type, "column", TypeValue.Text)
				{
				}

				// Token: 0x06001D1B RID: 7451 RVA: 0x000483B8 File Offset: 0x000465B8
				public override TypeValue TypedInvoke(TypeValue tableType, TextValue columnName)
				{
					RecordValue fields = tableType.AsTableType.ItemType.AsRecordType.Fields;
					int num;
					if (!fields.Keys.TryGetKeyIndex(columnName.AsString, out num))
					{
						throw ValueException.TableColumnNotFound(columnName.AsString);
					}
					return fields[num].AsRecord["Type"].AsType;
				}
			}

			// Token: 0x020002E8 RID: 744
			private sealed class TableRowFunctionValue : NativeFunctionValue1<TypeValue, TypeValue>
			{
				// Token: 0x06001D1C RID: 7452 RVA: 0x00048415 File Offset: 0x00046615
				public TableRowFunctionValue()
					: base(TypeValue._Type, "table", TypeValue._Type)
				{
				}

				// Token: 0x06001D1D RID: 7453 RVA: 0x0004842C File Offset: 0x0004662C
				public override TypeValue TypedInvoke(TypeValue tableType)
				{
					return tableType.AsTableType.ItemType;
				}
			}

			// Token: 0x020002E9 RID: 745
			private class TableKeysFunctionValue : NativeFunctionValue1<ListValue, TypeValue>
			{
				// Token: 0x06001D1E RID: 7454 RVA: 0x00048439 File Offset: 0x00046639
				public TableKeysFunctionValue()
					: base(TypeValue.List, "tableType", TypeValue._Type)
				{
				}

				// Token: 0x06001D1F RID: 7455 RVA: 0x00048450 File Offset: 0x00046650
				public override ListValue TypedInvoke(TypeValue type)
				{
					TableTypeValue asTableType = type.AsTableType;
					return TableTypeMetadata.GetTableKeys(asTableType.TableKeys, asTableType.ItemType.Fields.Keys);
				}
			}

			// Token: 0x020002EA RID: 746
			private class AddTableKeyFunctionValue : NativeFunctionValue3<TypeValue, TypeValue, ListValue, LogicalValue>
			{
				// Token: 0x06001D20 RID: 7456 RVA: 0x0004847F File Offset: 0x0004667F
				public AddTableKeyFunctionValue()
					: base(TypeValue._Type, "table", TypeValue._Type, "columns", ListTypeValue.Text, "isPrimary", TypeValue.Logical)
				{
				}

				// Token: 0x06001D21 RID: 7457 RVA: 0x000484AC File Offset: 0x000466AC
				public override TypeValue TypedInvoke(TypeValue type, ListValue columns, LogicalValue isPrimary)
				{
					TableTypeValue asTableType = type.AsTableType;
					TableKey tableKey = new TableKey(TableValue.GetColumns(asTableType.ItemType.Fields.Keys, columns), isPrimary.AsBoolean);
					IList<TableKey> tableKeys = asTableType.TableKeys;
					if (isPrimary.Boolean)
					{
						if (tableKeys.Any((TableKey o) => o.Primary))
						{
							throw ValueException.NewExpressionError<Message0>(Strings.TableAddKey_PrimaryKeyAlreadyExists, asTableType, null);
						}
					}
					TableKey[] array = new TableKey[tableKeys.Count + 1];
					for (int i = 0; i < tableKeys.Count; i++)
					{
						array[i] = tableKeys[i];
					}
					array[tableKeys.Count] = tableKey;
					return asTableType.ReplaceTableKeys(array).NewMeta(type.MetaValue).AsType;
				}
			}

			// Token: 0x020002EC RID: 748
			private class ReplaceTableKeysFunctionValue : NativeFunctionValue2<TypeValue, TypeValue, ListValue>
			{
				// Token: 0x06001D25 RID: 7461 RVA: 0x00048588 File Offset: 0x00046788
				public ReplaceTableKeysFunctionValue()
					: base(TypeValue._Type, "tableType", TypeValue._Type, "keys", TypeValue.List)
				{
				}

				// Token: 0x06001D26 RID: 7462 RVA: 0x000485A9 File Offset: 0x000467A9
				public override TypeValue TypedInvoke(TypeValue type, ListValue keys)
				{
					TableTypeValue asTableType = type.AsTableType;
					return asTableType.ReplaceTableKeys(TableTypeMetadata.GetTableKeys(asTableType.ItemType.Fields.Keys, keys));
				}
			}
		}

		// Token: 0x020002ED RID: 749
		public static class Tables
		{
			// Token: 0x04000A51 RID: 2641
			public static readonly FunctionValue GetRelationships = new TableModule.Tables.GetRelationshipsFunctionValue();

			// Token: 0x020002EE RID: 750
			private sealed class GetRelationshipsFunctionValue : NativeFunctionValue2<TableValue, TableValue, Value>
			{
				// Token: 0x06001D28 RID: 7464 RVA: 0x000485D8 File Offset: 0x000467D8
				public GetRelationshipsFunctionValue()
					: base(TypeValue.Table, 1, "tables", TypeValue.Table, "dataColumn", NullableTypeValue.Text)
				{
				}

				// Token: 0x06001D29 RID: 7465 RVA: 0x000485FC File Offset: 0x000467FC
				public override TableValue TypedInvoke(TableValue tables, Value dataColumn)
				{
					if (dataColumn.IsNull && !tables.Type.MetaValue.TryGetValue("NavigationTable.DataColumn", out dataColumn))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.InvalidArgument, TextValue.New("dataColumn"), null);
					}
					int num;
					if (!tables.Columns.TryGetKeyIndex(dataColumn.AsString, out num))
					{
						throw ValueException.NewExpressionError<Message1>(Strings.ValueException_MissingColumn(dataColumn.AsString), dataColumn, null);
					}
					TableKey tableKey = tables.TableKeys.FirstOrDefault((TableKey k) => k.Primary) ?? tables.TableKeys.FirstOrDefault<TableKey>();
					if (tableKey == null)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Table_RelationshipsWithoutKey, Value.Null, null);
					}
					string[] array = new string[tableKey.Columns.Length];
					int num2 = 0;
					foreach (int num3 in tableKey.Columns)
					{
						array[num2] = tables.Type.AsTableType.ItemType.Fields.Keys[num3];
						num2++;
					}
					Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>();
					List<TableModule.Tables.GetRelationshipsFunctionValue.TableRelationships> list = new List<TableModule.Tables.GetRelationshipsFunctionValue.TableRelationships>();
					int num4 = 0;
					foreach (IValueReference valueReference in tables)
					{
						if (valueReference.Value.AsRecord[num].IsTable)
						{
							TableValue asTable = valueReference.Value.AsRecord[num].AsTable;
							list.Add(TableModule.Tables.GetRelationshipsFunctionValue.TableRelationships.Load(num4, valueReference, asTable, tableKey.Columns, dictionary));
							num4++;
						}
					}
					Dictionary<RecordValue, List<RecordValue>> dictionary2 = new Dictionary<RecordValue, List<RecordValue>>();
					for (int j = 0; j < list.Count; j++)
					{
						list[j].ProcessRelationships(list, num, Keys.New(array), tableKey.Columns, dictionary, dictionary2);
					}
					List<RecordValue> list2 = new List<RecordValue>();
					foreach (KeyValuePair<RecordValue, List<RecordValue>> keyValuePair in dictionary2)
					{
						list2.AddRange(keyValuePair.Value.Distinct<RecordValue>());
					}
					FunctionValue fromRecords = TableModule.Table.FromRecords;
					Value[] array2 = list2.ToArray();
					return fromRecords.Invoke(ListValue.New(array2), TableTypeValue.New(TableModule.Tables.GetRelationshipsFunctionValue.ResultKeys, null)).AsTable;
				}

				// Token: 0x04000A52 RID: 2642
				private static readonly Keys ResultKeys = Keys.New("Table", "Columns", "OtherTable", "OtherColumns");

				// Token: 0x04000A53 RID: 2643
				private const string DataColumn = "dataColumn";

				// Token: 0x04000A54 RID: 2644
				private const string NavigationTableDataColumn = "NavigationTable.DataColumn";

				// Token: 0x020002EF RID: 751
				private class TableRelationships
				{
					// Token: 0x06001D2B RID: 7467 RVA: 0x00048888 File Offset: 0x00046A88
					private TableRelationships(TableValue tableValue, Value[] keys, ColumnIdentity[] columnIdentities, List<string[]> columnsOfRelationships, IList<Relationship> relationships)
					{
						this.tableValue = tableValue;
						this.tableKeys = keys;
						this.columnIdentities = columnIdentities;
						this.columnsOfRelationships = columnsOfRelationships;
						this.relationships = relationships;
					}

					// Token: 0x06001D2C RID: 7468 RVA: 0x000488B8 File Offset: 0x00046AB8
					public static TableModule.Tables.GetRelationshipsFunctionValue.TableRelationships Load(int rowIndex, IValueReference row, TableValue table, int[] tablesKeysColumns, Dictionary<string, List<int>> columnIdentityMapTableRelationships)
					{
						Value[] array = new Value[tablesKeysColumns.Length];
						int num = 0;
						foreach (int num2 in tablesKeysColumns)
						{
							array[num] = row.Value.AsRecord[num2];
							num++;
						}
						List<string[]> list = new List<string[]>();
						foreach (Relationship relationship in table.Relationships)
						{
							string[] array2 = new string[relationship.LeftKeyColumns.Length];
							num = 0;
							foreach (int num3 in relationship.LeftKeyColumns)
							{
								array2[num] = table.Columns[num3];
								num++;
							}
							list.Add(array2);
						}
						TableModule.Tables.GetRelationshipsFunctionValue.TableRelationships tableRelationships = new TableModule.Tables.GetRelationshipsFunctionValue.TableRelationships(table, array, table.ColumnIdentities, list, table.Relationships);
						if (table.ColumnIdentities != null)
						{
							foreach (ColumnIdentity columnIdentity in table.ColumnIdentities)
							{
								if (columnIdentity != null)
								{
									List<int> list2;
									if (!columnIdentityMapTableRelationships.TryGetValue(columnIdentity.Identity, out list2))
									{
										list2 = new List<int>();
										columnIdentityMapTableRelationships.Add(columnIdentity.Identity, list2);
									}
									list2.Add(rowIndex);
								}
							}
						}
						return tableRelationships;
					}

					// Token: 0x06001D2D RID: 7469 RVA: 0x00048A10 File Offset: 0x00046C10
					public void ProcessRelationships(List<TableModule.Tables.GetRelationshipsFunctionValue.TableRelationships> tablesRelationships, int dataColumnIndex, Keys tablesKeyValues, int[] tablesKeysColumns, Dictionary<string, List<int>> columnIdentityMapTableRelationships, Dictionary<RecordValue, List<RecordValue>> tableKeyMapResultRecords)
					{
						for (int i = 0; i < this.relationships.Count; i++)
						{
							Relationship relationship = this.relationships[i];
							string[] array = this.columnsOfRelationships[i];
							List<TableModule.Tables.GetRelationshipsFunctionValue.TableRelationships> list = new List<TableModule.Tables.GetRelationshipsFunctionValue.TableRelationships>();
							foreach (ColumnIdentity columnIdentity in relationship.RightKeyColumns)
							{
								List<int> list2;
								if (columnIdentity != null && columnIdentityMapTableRelationships.TryGetValue(columnIdentity.Identity, out list2))
								{
									foreach (int num in list2)
									{
										TableModule.Tables.GetRelationshipsFunctionValue.TableRelationships tableRelationships = tablesRelationships[num];
										List<string> list3 = new List<string>();
										foreach (ColumnIdentity columnIdentity2 in tableRelationships.columnIdentities)
										{
											if (columnIdentity2 != null)
											{
												list3.Add(columnIdentity2.Identity);
											}
										}
										List<string> list4 = new List<string>();
										foreach (ColumnIdentity columnIdentity3 in relationship.RightKeyColumns)
										{
											if (columnIdentity3 != null)
											{
												list4.Add(columnIdentity3.Identity);
											}
										}
										if (!list4.Except(list3).Any<string>())
										{
											bool flag = false;
											using (List<TableModule.Tables.GetRelationshipsFunctionValue.TableRelationships>.Enumerator enumerator2 = list.GetEnumerator())
											{
												while (enumerator2.MoveNext())
												{
													if (enumerator2.Current == list)
													{
														flag = true;
														break;
													}
												}
											}
											if (!flag)
											{
												list.Add(tableRelationships);
											}
										}
									}
								}
							}
							foreach (TableModule.Tables.GetRelationshipsFunctionValue.TableRelationships tableRelationships2 in list)
							{
								List<string> list5 = new List<string>();
								foreach (ColumnIdentity columnIdentity4 in relationship.RightKeyColumns)
								{
									for (int l = 0; l < tableRelationships2.columnIdentities.Length; l++)
									{
										if (tableRelationships2.columnIdentities[l] != null && tableRelationships2.columnIdentities[l].Identity.Equals(columnIdentity4.Identity))
										{
											list5.Add(tableRelationships2.tableValue.Columns[l]);
											break;
										}
									}
								}
								RecordValue recordValue = RecordValue.New(tablesKeyValues, this.tableKeys);
								RecordValue recordValue2 = RecordValue.New(tablesKeyValues, tableRelationships2.tableKeys);
								List<RecordValue> list6;
								if (!tableKeyMapResultRecords.TryGetValue(recordValue, out list6))
								{
									list6 = new List<RecordValue>();
									tableKeyMapResultRecords.Add(recordValue, list6);
								}
								list6.Add(RecordValue.New(TableModule.Tables.GetRelationshipsFunctionValue.ResultKeys, new Value[]
								{
									recordValue,
									ListValue.New(array),
									recordValue2,
									ListValue.New(list5.ToArray())
								}));
								if (!tableKeyMapResultRecords.TryGetValue(recordValue2, out list6))
								{
									list6 = new List<RecordValue>();
									tableKeyMapResultRecords.Add(recordValue2, list6);
								}
								list6.Add(RecordValue.New(TableModule.Tables.GetRelationshipsFunctionValue.ResultKeys, new Value[]
								{
									recordValue2,
									ListValue.New(list5.ToArray()),
									recordValue,
									ListValue.New(array)
								}));
							}
						}
					}

					// Token: 0x04000A55 RID: 2645
					private readonly TableValue tableValue;

					// Token: 0x04000A56 RID: 2646
					private readonly Value[] tableKeys;

					// Token: 0x04000A57 RID: 2647
					private readonly ColumnIdentity[] columnIdentities;

					// Token: 0x04000A58 RID: 2648
					private readonly List<string[]> columnsOfRelationships;

					// Token: 0x04000A59 RID: 2649
					private readonly IList<Relationship> relationships;
				}
			}
		}

		// Token: 0x020002F1 RID: 753
		public static class Table
		{
			// Token: 0x04000A5C RID: 2652
			public static readonly FunctionValue ColumnNames = new TableModule.Table.ColumnNamesFunctionValue();

			// Token: 0x04000A5D RID: 2653
			public static readonly FunctionValue FromColumns = new TableModule.Table.FromColumnsFunctionValue();

			// Token: 0x04000A5E RID: 2654
			public static readonly FunctionValue FromRows = new TableModule.Table.FromRowsFunctionValue();

			// Token: 0x04000A5F RID: 2655
			public static readonly FunctionValue RowCount = new TableModule.Table.RowCountFunctionValue();

			// Token: 0x04000A60 RID: 2656
			public static readonly FunctionValue ApproximateRowCount = FoldableFunctionValue.New(new TableModule.Table.ApproximateRowCountFunctionValue());

			// Token: 0x04000A61 RID: 2657
			public static readonly FunctionValue ToRecords = new TableModule.Table.ToRecordsFunctionValue();

			// Token: 0x04000A62 RID: 2658
			public static readonly FunctionValue FromRecords = new TableModule.Table.FromRecordsFunctionValue();

			// Token: 0x04000A63 RID: 2659
			public static readonly FunctionValue Keys = new TableModule.Table.KeysFunctionValue();

			// Token: 0x04000A64 RID: 2660
			public static readonly FunctionValue AddKey = new TableModule.Table.AddKeyFunctionValue();

			// Token: 0x04000A65 RID: 2661
			public static readonly FunctionValue ReplaceKeys = new TableModule.Table.ReplaceKeysFunctionValue();

			// Token: 0x04000A66 RID: 2662
			public static readonly FunctionValue Column = new TableModule.Table.ColumnFunctionValue();

			// Token: 0x04000A67 RID: 2663
			public static readonly FunctionValue SelectColumns = new TableModule.Table.SelectColumnsFunctionValue();

			// Token: 0x04000A68 RID: 2664
			public static readonly FunctionValue SelectRows = new TableModule.Table.SelectRowsFunctionValue();

			// Token: 0x04000A69 RID: 2665
			public static readonly FunctionValue AddColumns = new TableModule.Table.AddColumnsFunctionValue();

			// Token: 0x04000A6A RID: 2666
			public static readonly FunctionValue TransformColumnNames = new TableModule.Table.TransformColumnNamesFunctionValue();

			// Token: 0x04000A6B RID: 2667
			public static readonly FunctionValue RenameColumns = new TableModule.Table.RenameColumnsFunctionValue();

			// Token: 0x04000A6C RID: 2668
			public static readonly FunctionValue ReorderColumns = new TableModule.Table.ReorderColumnsFunctionValue();

			// Token: 0x04000A6D RID: 2669
			public static readonly FunctionValue Skip = new TableModule.Table.SkipFunctionValue();

			// Token: 0x04000A6E RID: 2670
			public static readonly FunctionValue First = new TableModule.Table.FirstFunctionValue();

			// Token: 0x04000A6F RID: 2671
			public static readonly FunctionValue FirstN = new TableModule.Table.FirstNFunctionValue();

			// Token: 0x04000A70 RID: 2672
			public static readonly FunctionValue RemoveColumns = new TableModule.Table.RemoveColumnsFunctionValue();

			// Token: 0x04000A71 RID: 2673
			public static readonly FunctionValue Group = new TableModule.Table.GroupFunctionValue();

			// Token: 0x04000A72 RID: 2674
			public static readonly FunctionValue Join = new TableModule.Table.JoinFunctionValue();

			// Token: 0x04000A73 RID: 2675
			public static readonly FunctionValue AddJoinColumn = new TableModule.Table.AddJoinColumnFunctionValue();

			// Token: 0x04000A74 RID: 2676
			public static readonly FunctionValue NestedJoin = new TableModule.Table.NestedJoinFunctionValue();

			// Token: 0x04000A75 RID: 2677
			public static readonly FunctionValue AddIndexColumn = FoldableFunctionValue.New(new TableModule.Table.AddIndexColumnFunctionValue());

			// Token: 0x04000A76 RID: 2678
			public static readonly FunctionValue FillDown = new TableModule.Table.FillDownFunctionValue();

			// Token: 0x04000A77 RID: 2679
			public static readonly FunctionValue TransformColumns = new TableModule.Table.TransformColumnsFunctionValue();

			// Token: 0x04000A78 RID: 2680
			public static readonly FunctionValue Distinct = new TableModule.Table.DistinctFunctionValue();

			// Token: 0x04000A79 RID: 2681
			public static readonly FunctionValue Sort = new TableModule.Table.SortFunctionValue();

			// Token: 0x04000A7A RID: 2682
			public static readonly FunctionValue SortDescending = new TableModule.Table.SortDescendingFunctionValue();

			// Token: 0x04000A7B RID: 2683
			public static readonly FunctionValue ExpandListColumn = new TableModule.Table.ExpandListColumnFunctionValue();

			// Token: 0x04000A7C RID: 2684
			public static readonly FunctionValue ExpandRecordColumn = new TableModule.Table.ExpandRecordColumnFunctionValue();

			// Token: 0x04000A7D RID: 2685
			public static readonly FunctionValue ForceColumns = new TableModule.Table.ForceColumnsFunctionValue();

			// Token: 0x04000A7E RID: 2686
			public static readonly FunctionValue AggregateTableColumn = new TableModule.Table.AggregateTableColumnFunctionValue();

			// Token: 0x04000A7F RID: 2687
			public static readonly FunctionValue SingleRow = new TableModule.Table.SingleRowFunctionValue();

			// Token: 0x04000A80 RID: 2688
			public static readonly FunctionValue Combine = new TableModule.Table.CombineFunctionValue();

			// Token: 0x04000A81 RID: 2689
			public static readonly FunctionValue UnpivotOtherColumns = new TableModule.Table.UnpivotOtherColumnsFunctionValue();

			// Token: 0x04000A82 RID: 2690
			public static readonly FunctionValue Unpivot = new TableModule.Table.UnpivotFunctionValue();

			// Token: 0x04000A83 RID: 2691
			public static readonly FunctionValue Pivot = new TableModule.Table.PivotFunctionValue();

			// Token: 0x04000A84 RID: 2692
			public static readonly FunctionValue SelectRowsWithErrors = new TableModule.Table.SelectRowsWithErrorsFunctionValue();

			// Token: 0x04000A85 RID: 2693
			public static readonly FunctionValue RemoveRowsWithErrors = new TableModule.Table.RemoveRowsWithErrorsFunctionValue();

			// Token: 0x04000A86 RID: 2694
			public static readonly FunctionValue Literal = new TableModule.Table.LiteralFunctionValue();

			// Token: 0x04000A87 RID: 2695
			public static readonly FunctionValue FromPartitions = new TableModule.Table.FromPartitionsFunctionValue();

			// Token: 0x04000A88 RID: 2696
			public static readonly FunctionValue PartitionValues = new TableModule.Table.PartitionValuesFunctionValue();

			// Token: 0x04000A89 RID: 2697
			public static readonly FunctionValue ReplaceRelationshipIdentity = new TableModule.Table.ReplaceRelationshipIdentityFunctionValue();

			// Token: 0x04000A8A RID: 2698
			public static readonly FunctionValue ReplaceErrorValues = new TableModule.Table.ReplaceErrorValuesFunctionValue();

			// Token: 0x04000A8B RID: 2699
			public static readonly FunctionValue Split = new TableModule.Table.SplitFunctionValue();

			// Token: 0x04000A8C RID: 2700
			public static readonly FunctionValue Buffer = new TableModule.Table.BufferFunctionValue();

			// Token: 0x04000A8D RID: 2701
			public static readonly FunctionValue StopFolding = new TableModule.Table.StopFoldingFunctionValue();

			// Token: 0x04000A8E RID: 2702
			public static readonly FunctionValue AddAccumulatedColumn = new TableModule.Table.AddAccumulatedColumnFunctionValue();

			// Token: 0x04000A8F RID: 2703
			public static readonly FunctionValue Function_InvokeWithErrorContext = new TableModule.Table.Function_InvokeWithErrorContextFunctionValue();

			// Token: 0x04000A90 RID: 2704
			public static readonly FunctionValue Table_WithErrorContext = new TableModule.Table.Table_WithErrorContextFunctionValue();

			// Token: 0x04000A91 RID: 2705
			public static readonly FunctionValue Action_WithErrorContext = new TableModule.Table.Action_WithErrorContextFunctionValue();

			// Token: 0x020002F2 RID: 754
			private class ColumnFunctionValue : NativeFunctionValue2<ListValue, TableValue, TextValue>, IInvocationRewriter
			{
				// Token: 0x06001D32 RID: 7474 RVA: 0x00048FB3 File Offset: 0x000471B3
				public ColumnFunctionValue()
					: base(TypeValue.List, "table", TypeValue.Table, "column", TypeValue.Text)
				{
				}

				// Token: 0x06001D33 RID: 7475 RVA: 0x00048FD4 File Offset: 0x000471D4
				public override ListValue TypedInvoke(TableValue table, TextValue column)
				{
					return table.SelectColumns(ListValue.New(new Value[] { column }), Value.Null)[column.AsString].AsList;
				}

				// Token: 0x06001D34 RID: 7476 RVA: 0x00049000 File Offset: 0x00047200
				public bool TryRewriteInvocation(IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression)
				{
					Value value;
					if (invocation.Arguments.Count == 2 && invocation.Arguments[1].TryGetConstant(out value) && value.IsText)
					{
						expression = new RequiredFieldAccessExpressionSyntaxNode(invocation.Arguments[0], Identifier.New(value.AsString));
						return true;
					}
					expression = null;
					return false;
				}
			}

			// Token: 0x020002F3 RID: 755
			private class SelectColumnsFunctionValue : NativeFunctionValue3<TableValue, TableValue, Value, Value>
			{
				// Token: 0x06001D35 RID: 7477 RVA: 0x0004905C File Offset: 0x0004725C
				public SelectColumnsFunctionValue()
					: base(TypeValue.Table, 2, "table", TypeValue.Table, "columns", TypeValue.Any, "missingField", Library.MissingField.Type.Nullable)
				{
				}

				// Token: 0x06001D36 RID: 7478 RVA: 0x00049098 File Offset: 0x00047298
				public override TableValue TypedInvoke(TableValue table, Value columns, Value missingField)
				{
					return table.SelectColumns(columns, missingField);
				}
			}

			// Token: 0x020002F4 RID: 756
			private class SelectRowsFunctionValue : NativeFunctionValue2<TableValue, TableValue, FunctionValue>
			{
				// Token: 0x06001D37 RID: 7479 RVA: 0x000490A2 File Offset: 0x000472A2
				public SelectRowsFunctionValue()
					: base(TypeValue.Table, "table", TypeValue.Table, "condition", TypeValue.Function)
				{
				}

				// Token: 0x06001D38 RID: 7480 RVA: 0x000490C3 File Offset: 0x000472C3
				public override TableValue TypedInvoke(TableValue table, FunctionValue condition)
				{
					return table.SelectRows(condition);
				}
			}

			// Token: 0x020002F5 RID: 757
			private class AddColumnsFunctionValue : NativeFunctionValue4<TableValue, TableValue, ListValue, FunctionValue, Value>
			{
				// Token: 0x06001D39 RID: 7481 RVA: 0x000490CC File Offset: 0x000472CC
				public AddColumnsFunctionValue()
					: base(TypeValue.Table, 3, "table", TypeValue.Table, "newColumnNames", TypeValue.List, "columnGenerator", TypeValue.Function, "columnTypes", TypeValue.List.Nullable)
				{
				}

				// Token: 0x06001D3A RID: 7482 RVA: 0x00049112 File Offset: 0x00047312
				public override TableValue TypedInvoke(TableValue table, ListValue newColumnNames, FunctionValue columnGenerator, Value columnTypes)
				{
					return table.AddColumns(newColumnNames, columnGenerator, columnTypes);
				}
			}

			// Token: 0x020002F6 RID: 758
			private class RenameColumnsFunctionValue : NativeFunctionValue3<TableValue, TableValue, ListValue, Value>
			{
				// Token: 0x06001D3B RID: 7483 RVA: 0x00049120 File Offset: 0x00047320
				public RenameColumnsFunctionValue()
					: base(TypeValue.Table, 2, "table", TypeValue.Table, "renames", TypeValue.List, "missingField", Library.MissingField.Type.Nullable)
				{
				}

				// Token: 0x06001D3C RID: 7484 RVA: 0x0004915C File Offset: 0x0004735C
				public override TableValue TypedInvoke(TableValue table, ListValue renames, Value missingField)
				{
					return table.RenameColumns(renames, missingField);
				}
			}

			// Token: 0x020002F7 RID: 759
			private class TransformColumnNamesFunctionValue : NativeFunctionValue3<TableValue, TableValue, FunctionValue, Value>
			{
				// Token: 0x06001D3D RID: 7485 RVA: 0x00049168 File Offset: 0x00047368
				public TransformColumnNamesFunctionValue()
					: base(TypeValue.Table, 2, "table", TypeValue.Table, "nameGenerator", TypeValue.Function, "options", TypeValue.Record.Nullable)
				{
				}

				// Token: 0x06001D3E RID: 7486 RVA: 0x000491C8 File Offset: 0x000473C8
				public override TableValue TypedInvoke(TableValue table, FunctionValue nameGenerator, Value optionsValue)
				{
					int num;
					StringComparer stringComparer;
					TableModule.Table.TransformColumnNamesFunctionValue.GetOptions(optionsValue.IsNull ? RecordValue.Empty : Options.ValidateOptions(optionsValue.AsRecord, this.validOptionKeys, "Table.TransformColumnNames", null), out num, out stringComparer);
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					HashSet<string> hashSet = new HashSet<string>(stringComparer);
					int length = table.Columns.Length;
					string[] array = new string[length];
					for (int i = 0; i < length; i++)
					{
						string text = table.Columns[i];
						string text2 = TableModule.Table.TransformColumnNamesFunctionValue.TruncateColumnName(nameGenerator.Invoke(TextValue.New(text)).AsString, num);
						if (text2 == text && !hashSet.Contains(text))
						{
							dictionary.Add(text, text2);
							hashSet.Add(text2);
						}
						else
						{
							array[i] = text2;
						}
					}
					for (int j = 0; j < length; j++)
					{
						if (array[j] != null)
						{
							string text3 = table.Columns[j];
							string text4;
							if (!TableModule.Table.TransformColumnNamesFunctionValue.TryGetUniqueNewName(num, hashSet, array[j], out text4))
							{
								throw ValueException.NewExpressionError<Message0>(Strings.FailedToCreateUniqueColumnName, TextValue.New(array[j]), null);
							}
							dictionary.Add(text3, text4);
							hashSet.Add(text4);
						}
					}
					List<IValueReference> list = new List<IValueReference>();
					foreach (KeyValuePair<string, string> keyValuePair in dictionary)
					{
						if (keyValuePair.Key != keyValuePair.Value)
						{
							list.Add(ListValue.New(new Value[]
							{
								TextValue.New(keyValuePair.Key),
								TextValue.New(keyValuePair.Value)
							}));
						}
					}
					if (list.Count == 0)
					{
						return table;
					}
					return table.RenameColumns(ListValue.New(list), MissingFieldMode.Error);
				}

				// Token: 0x06001D3F RID: 7487 RVA: 0x00049394 File Offset: 0x00047594
				private static string TruncateColumnName(string columnName, int maxLength)
				{
					if (columnName.Length > maxLength)
					{
						columnName = columnName.Substring(0, maxLength);
					}
					return columnName;
				}

				// Token: 0x06001D40 RID: 7488 RVA: 0x000493AC File Offset: 0x000475AC
				private static bool TryGetUniqueNewName(int maxLength, HashSet<string> takenNewNames, string newName, out string uniqueNewName)
				{
					uniqueNewName = TableModule.Table.TransformColumnNamesFunctionValue.TruncateColumnName(newName, maxLength);
					if (takenNewNames.Contains(uniqueNewName))
					{
						for (int i = 1; i <= maxLength; i++)
						{
							int num = (int)Math.Pow(10.0, (double)i);
							string text = TableModule.Table.TransformColumnNamesFunctionValue.TruncateColumnName(newName, maxLength - i);
							for (int j = 1; j < num; j++)
							{
								string text2 = text + j.ToString();
								if (!takenNewNames.Contains(text2))
								{
									uniqueNewName = text2;
									return true;
								}
							}
						}
						return false;
					}
					return true;
				}

				// Token: 0x06001D41 RID: 7489 RVA: 0x00049424 File Offset: 0x00047624
				private static void GetOptions(RecordValue options, out int maxLength, out StringComparer stringComparer)
				{
					maxLength = int.MaxValue;
					stringComparer = StringComparer.Ordinal;
					Value value;
					if (options.TryGetValue("MaxLength", out value))
					{
						maxLength = value.AsNumber.AsInteger32;
						if (maxLength < 0)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.MaxColumnNameLengthCantBeNegative, NumberValue.New(maxLength), null);
						}
					}
					if (options.TryGetValue("Comparer", out value) && value != null)
					{
						CultureInfo cultureInfo = null;
						bool flag = false;
						if (!value.AsFunction.TryGetCultureCase(out cultureInfo, out flag))
						{
							throw ValueException.NewExpressionError<Message0>(Strings.CultureUnawareComparer, value, null);
						}
						if (cultureInfo == null)
						{
							stringComparer = (flag ? StringComparer.OrdinalIgnoreCase : stringComparer);
							return;
						}
						stringComparer = StringComparer.Create(cultureInfo, flag);
					}
				}

				// Token: 0x04000A92 RID: 2706
				public const string ComparerKey = "Comparer";

				// Token: 0x04000A93 RID: 2707
				public const string MaxLengthKey = "MaxLength";

				// Token: 0x04000A94 RID: 2708
				public readonly HashSet<string> validOptionKeys = new HashSet<string>(new string[] { "Comparer", "MaxLength" });
			}

			// Token: 0x020002F8 RID: 760
			private class ReorderColumnsFunctionValue : NativeFunctionValue3<TableValue, TableValue, ListValue, Value>
			{
				// Token: 0x06001D42 RID: 7490 RVA: 0x000494C4 File Offset: 0x000476C4
				public ReorderColumnsFunctionValue()
					: base(TypeValue.Table, 2, "table", TypeValue.Table, "columnOrder", TypeValue.List, "missingField", Library.MissingField.Type.Nullable)
				{
				}

				// Token: 0x06001D43 RID: 7491 RVA: 0x00049500 File Offset: 0x00047700
				public override TableValue TypedInvoke(TableValue table, ListValue columnOrder, Value missingField)
				{
					return table.ReorderColumns(columnOrder, missingField);
				}
			}

			// Token: 0x020002F9 RID: 761
			private class SkipFunctionValue : NativeFunctionValue2<Value, TableValue, Value>
			{
				// Token: 0x06001D44 RID: 7492 RVA: 0x0004950A File Offset: 0x0004770A
				public SkipFunctionValue()
					: base(TypeValue.Table, 1, "table", TypeValue.Table, "countOrCondition", TypeValue.Any)
				{
				}

				// Token: 0x06001D45 RID: 7493 RVA: 0x0004952C File Offset: 0x0004772C
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					TypeValue type = environment.GetType(arguments[0]);
					if (!type.IsTableType)
					{
						return TypeValue.Table;
					}
					return type;
				}

				// Token: 0x06001D46 RID: 7494 RVA: 0x00049556 File Offset: 0x00047756
				public override Value TypedInvoke(TableValue table, Value countOrCondition)
				{
					return table.Skip(countOrCondition);
				}
			}

			// Token: 0x020002FA RID: 762
			private class FirstFunctionValue : NativeFunctionValue2<Value, TableValue, Value>, IAccumulableFunction
			{
				// Token: 0x06001D47 RID: 7495 RVA: 0x0004955F File Offset: 0x0004775F
				public FirstFunctionValue()
					: base(TypeValue.Any, 1, "table", TypeValue.Table, "default", TypeValue.Any)
				{
				}

				// Token: 0x17000D61 RID: 3425
				// (get) Token: 0x06001D48 RID: 7496 RVA: 0x00049581 File Offset: 0x00047781
				public string EnumerableParameter
				{
					get
					{
						return "table";
					}
				}

				// Token: 0x06001D49 RID: 7497 RVA: 0x00049588 File Offset: 0x00047788
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					TypeValue type = environment.GetType(arguments[0]);
					if (!type.IsTableType)
					{
						return TypeValue.Record;
					}
					return type.AsTableType.ItemType;
				}

				// Token: 0x06001D4A RID: 7498 RVA: 0x000495BC File Offset: 0x000477BC
				public override Value TypedInvoke(TableValue table, Value defaultValue)
				{
					Value value;
					using (IEnumerator<IValueReference> enumerator = table.Take(NumberValue.One).GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							value = enumerator.Current.Value;
						}
						else
						{
							value = defaultValue;
						}
					}
					return value;
				}

				// Token: 0x06001D4B RID: 7499 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x06001D4C RID: 7500 RVA: 0x00049616 File Offset: 0x00047816
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new TableModule.Table.FirstFunctionValue.FirstAccumulable(arguments["default"]);
				}

				// Token: 0x04000A95 RID: 2709
				private const string enumerableParameter = "table";

				// Token: 0x020002FB RID: 763
				private sealed class FirstAccumulable : IAccumulable
				{
					// Token: 0x06001D4D RID: 7501 RVA: 0x00049628 File Offset: 0x00047828
					public FirstAccumulable(Value defaultValue)
					{
						this.defaultValue = defaultValue;
					}

					// Token: 0x06001D4E RID: 7502 RVA: 0x00049637 File Offset: 0x00047837
					public IAccumulator CreateAccumulator()
					{
						return new TableModule.Table.FirstFunctionValue.FirstAccumulable.FirstAccumulator(this);
					}

					// Token: 0x04000A96 RID: 2710
					private readonly Value defaultValue;

					// Token: 0x020002FC RID: 764
					private sealed class FirstAccumulator : IAccumulator
					{
						// Token: 0x06001D4F RID: 7503 RVA: 0x0004963F File Offset: 0x0004783F
						public FirstAccumulator(TableModule.Table.FirstFunctionValue.FirstAccumulable accumulable)
						{
							this.defaultValue = accumulable.defaultValue;
						}

						// Token: 0x17000D62 RID: 3426
						// (get) Token: 0x06001D50 RID: 7504 RVA: 0x00049653 File Offset: 0x00047853
						public IValueReference Current
						{
							get
							{
								return this.first ?? this.defaultValue;
							}
						}

						// Token: 0x06001D51 RID: 7505 RVA: 0x00049665 File Offset: 0x00047865
						public void AccumulateNext(IValueReference next)
						{
							if (this.first == null)
							{
								this.first = next;
							}
						}

						// Token: 0x04000A97 RID: 2711
						private readonly Value defaultValue;

						// Token: 0x04000A98 RID: 2712
						private IValueReference first;
					}
				}
			}

			// Token: 0x020002FD RID: 765
			private class FirstNFunctionValue : NativeFunctionValue2<Value, TableValue, Value>
			{
				// Token: 0x06001D52 RID: 7506 RVA: 0x00049676 File Offset: 0x00047876
				public FirstNFunctionValue()
					: base(TypeValue.Table, "table", TypeValue.Table, "countOrCondition", TypeValue.Any)
				{
				}

				// Token: 0x06001D53 RID: 7507 RVA: 0x00049698 File Offset: 0x00047898
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					TypeValue type = environment.GetType(arguments[0]);
					if (!type.IsTableType)
					{
						return TypeValue.Table;
					}
					return type;
				}

				// Token: 0x06001D54 RID: 7508 RVA: 0x000496C2 File Offset: 0x000478C2
				public override Value TypedInvoke(TableValue table, Value countOrCondition)
				{
					return table.Take(countOrCondition);
				}
			}

			// Token: 0x020002FE RID: 766
			public class PromoteHeadersFunctionValue : CultureSpecificFunctionValue2<TableValue, TableValue, Value>
			{
				// Token: 0x06001D55 RID: 7509 RVA: 0x000496CC File Offset: 0x000478CC
				public PromoteHeadersFunctionValue(IEngineHost host)
					: base(host, null, TypeValue.Table, 1, "table", TypeValue.Table, "options", TypeValue.Record.Nullable)
				{
					this.lifetimeService = host.QueryService<ILifetimeService>();
				}

				// Token: 0x06001D56 RID: 7510 RVA: 0x0004970C File Offset: 0x0004790C
				public override TableValue TypedInvoke(TableValue table, Value options)
				{
					bool flag;
					ICulture culture;
					this.GetOptions(options, out flag, out culture);
					TableModule.Table.PromoteHeadersFunctionValue.PromotedHeadersTableValue promotedHeadersTableValue = new TableModule.Table.PromoteHeadersFunctionValue.PromotedHeadersTableValue(table, this.lifetimeService, flag, culture);
					if (!promotedHeadersTableValue.IsEmpty)
					{
						return ValueServices.AddShouldInferTableTypeMeta(promotedHeadersTableValue);
					}
					return promotedHeadersTableValue;
				}

				// Token: 0x06001D57 RID: 7511 RVA: 0x00049744 File Offset: 0x00047944
				public void GetOptions(Value options, out bool promoteAllScalars, out ICulture culture)
				{
					promoteAllScalars = false;
					culture = base.GetCulture(TextValue.New(CultureInfo.InvariantCulture.Name));
					if (!options.IsNull)
					{
						RecordValue asRecord = options.AsRecord;
						Value value;
						promoteAllScalars = (asRecord.TryGetValue("PromoteAllScalars", out value) ? value.AsBoolean : promoteAllScalars);
						if (asRecord.TryGetValue("Culture", out value))
						{
							culture = base.GetCulture(value.AsText);
							return;
						}
						culture = (promoteAllScalars ? base.GetCulture(Value.Null) : culture);
					}
				}

				// Token: 0x04000A99 RID: 2713
				private const string CultureKey = "Culture";

				// Token: 0x04000A9A RID: 2714
				private const string PromoteAllScalarsKey = "PromoteAllScalars";

				// Token: 0x04000A9B RID: 2715
				private readonly ILifetimeService lifetimeService;

				// Token: 0x020002FF RID: 767
				private sealed class PromotedHeadersTableValue : TableValue, IDisposable
				{
					// Token: 0x06001D58 RID: 7512 RVA: 0x000497C8 File Offset: 0x000479C8
					public PromotedHeadersTableValue(TableValue table, ILifetimeService lifetimeService, bool promoteAllScalars, ICulture culture)
					{
						this.table = table;
						this.lifetimeService = lifetimeService;
						IEnumerator<IValueReference> enumerator = ((lifetimeService == null) ? table.Take(Microsoft.Mashup.Engine1.Runtime.RowCount.One) : table).GetEnumerator();
						try
						{
							if (!enumerator.MoveNext())
							{
								this.type = this.table.Type;
								this.empty = true;
							}
							else
							{
								Keys headers = TableModule.Table.PromoteHeadersFunctionValue.PromotedHeadersTableValue.GetHeaders(enumerator.Current.Value.AsRecord, promoteAllScalars, culture);
								this.columnSelection = new ColumnSelection(headers);
								this.type = table.SelectColumns(this.columnSelection).Type;
								if (lifetimeService != null)
								{
									this.rows = enumerator;
									this.lifetimeService.Register(this);
									enumerator = null;
								}
							}
						}
						finally
						{
							if (enumerator != null)
							{
								enumerator.Dispose();
							}
						}
					}

					// Token: 0x17000D63 RID: 3427
					// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00049894 File Offset: 0x00047A94
					public override TypeValue Type
					{
						get
						{
							return this.type;
						}
					}

					// Token: 0x17000D64 RID: 3428
					// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0004989C File Offset: 0x00047A9C
					public bool IsEmpty
					{
						get
						{
							return this.empty;
						}
					}

					// Token: 0x06001D5B RID: 7515 RVA: 0x000498A4 File Offset: 0x00047AA4
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						if (this.empty)
						{
							return EmptyArray<IValueReference>.Instance.GetEnumerator();
						}
						if (this.rows != null)
						{
							IEnumerator<IValueReference> enumerator = this.rows;
							this.rows = null;
							return ProjectColumnsQuery.Project(enumerator, this.columnSelection);
						}
						return this.table.Skip(Microsoft.Mashup.Engine1.Runtime.RowCount.One).SelectColumns(this.columnSelection).GetEnumerator();
					}

					// Token: 0x06001D5C RID: 7516 RVA: 0x00049905 File Offset: 0x00047B05
					public void Dispose()
					{
						if (this.rows != null)
						{
							this.rows.Dispose();
							this.rows = null;
						}
						ILifetimeService lifetimeService = this.lifetimeService;
						if (lifetimeService == null)
						{
							return;
						}
						lifetimeService.Unregister(this);
					}

					// Token: 0x06001D5D RID: 7517 RVA: 0x00049934 File Offset: 0x00047B34
					private static Keys GetHeaders(RecordValue record, bool promoteAllScalars, ICulture culture)
					{
						KeysBuilder keysBuilder = new KeysBuilder(record.Keys.Length);
						int num = 1;
						for (int i = 0; i < record.Keys.Length; i++)
						{
							string columnName = TableModule.Table.PromoteHeadersFunctionValue.PromotedHeadersTableValue.GetColumnName(record, i, promoteAllScalars, culture);
							string text = columnName;
							while (!keysBuilder.Union(text))
							{
								text = columnName + "_" + num.ToString(CultureInfo.InvariantCulture);
								num++;
							}
						}
						return keysBuilder.ToKeys();
					}

					// Token: 0x06001D5E RID: 7518 RVA: 0x000499AC File Offset: 0x00047BAC
					private static string GetColumnName(RecordValue record, int index, bool promoteAllScalars, ICulture culture)
					{
						Value value = record[index];
						string text = "Column" + (index + 1).ToString(CultureInfo.InvariantCulture);
						if (promoteAllScalars || value.IsText || value.IsNumber)
						{
							return TableModule.Table.PromoteHeadersFunctionValue.PromotedHeadersTableValue.ConvertToText(value, culture, text);
						}
						return text;
					}

					// Token: 0x06001D5F RID: 7519 RVA: 0x000499FC File Offset: 0x00047BFC
					private static string ConvertToText(Value value, ICulture culture, string defaultName)
					{
						Value value2;
						if (((Library.Text.FromFunctionValue)CultureSpecificFunction.TextFrom).TryTextFromValue(value, culture, out value2) && !value2.IsNull)
						{
							return value2.AsString;
						}
						return defaultName;
					}

					// Token: 0x04000A9C RID: 2716
					private readonly TableValue table;

					// Token: 0x04000A9D RID: 2717
					private readonly ILifetimeService lifetimeService;

					// Token: 0x04000A9E RID: 2718
					private readonly TypeValue type;

					// Token: 0x04000A9F RID: 2719
					private readonly ColumnSelection columnSelection;

					// Token: 0x04000AA0 RID: 2720
					private readonly bool empty;

					// Token: 0x04000AA1 RID: 2721
					private IEnumerator<IValueReference> rows;
				}
			}

			// Token: 0x02000300 RID: 768
			private class RowCountFunctionValue : NativeFunctionValue1<NumberValue, TableValue>, IAccumulableFunction
			{
				// Token: 0x06001D60 RID: 7520 RVA: 0x00049A2E File Offset: 0x00047C2E
				public RowCountFunctionValue()
					: base(TypeValue.Number, "table", TypeValue.Table)
				{
				}

				// Token: 0x17000D65 RID: 3429
				// (get) Token: 0x06001D61 RID: 7521 RVA: 0x00049581 File Offset: 0x00047781
				public string EnumerableParameter
				{
					get
					{
						return "table";
					}
				}

				// Token: 0x06001D62 RID: 7522 RVA: 0x00049A45 File Offset: 0x00047C45
				public override NumberValue TypedInvoke(TableValue table)
				{
					return NumberValue.New(table.LargeCount);
				}

				// Token: 0x06001D63 RID: 7523 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x06001D64 RID: 7524 RVA: 0x00049A52 File Offset: 0x00047C52
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new TableModule.Table.RowCountFunctionValue.RowCountAccumulable();
				}

				// Token: 0x04000AA2 RID: 2722
				private const string enumerableParameter = "table";

				// Token: 0x02000301 RID: 769
				private sealed class RowCountAccumulable : IAccumulable
				{
					// Token: 0x06001D65 RID: 7525 RVA: 0x00049A59 File Offset: 0x00047C59
					public IAccumulator CreateAccumulator()
					{
						return new TableModule.Table.RowCountFunctionValue.RowCountAccumulable.RowCountAccumulator();
					}

					// Token: 0x02000302 RID: 770
					private sealed class RowCountAccumulator : IAccumulator
					{
						// Token: 0x17000D66 RID: 3430
						// (get) Token: 0x06001D67 RID: 7527 RVA: 0x00049A60 File Offset: 0x00047C60
						public IValueReference Current
						{
							get
							{
								return NumberValue.New(this.count);
							}
						}

						// Token: 0x06001D68 RID: 7528 RVA: 0x00049A6D File Offset: 0x00047C6D
						public void AccumulateNext(IValueReference next)
						{
							this.count += 1L;
						}

						// Token: 0x04000AA3 RID: 2723
						private long count;
					}
				}
			}

			// Token: 0x02000303 RID: 771
			private class ApproximateRowCountFunctionValue : NativeFunctionValue1<NumberValue, TableValue>
			{
				// Token: 0x06001D6A RID: 7530 RVA: 0x00049A2E File Offset: 0x00047C2E
				public ApproximateRowCountFunctionValue()
					: base(TypeValue.Number, "table", TypeValue.Table)
				{
				}

				// Token: 0x06001D6B RID: 7531 RVA: 0x00049A7E File Offset: 0x00047C7E
				public override NumberValue TypedInvoke(TableValue table)
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.ValueException_ApproximateRowCount_Unsupported, TextValue.Empty, null);
				}
			}

			// Token: 0x02000304 RID: 772
			private class ToRecordsFunctionValue : NativeFunctionValue1<ListValue, TableValue>, IAccumulableChainingFunction
			{
				// Token: 0x06001D6C RID: 7532 RVA: 0x00049A90 File Offset: 0x00047C90
				public ToRecordsFunctionValue()
					: base(TypeValue.List, "table", TypeValue.Table)
				{
				}

				// Token: 0x17000D67 RID: 3431
				// (get) Token: 0x06001D6D RID: 7533 RVA: 0x00049581 File Offset: 0x00047781
				public string EnumerableParameter
				{
					get
					{
						return "table";
					}
				}

				// Token: 0x06001D6E RID: 7534 RVA: 0x00049AA7 File Offset: 0x00047CA7
				public override ListValue TypedInvoke(TableValue table)
				{
					return table.ToRecords();
				}

				// Token: 0x06001D6F RID: 7535 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableChainingFunction(out IAccumulableChainingFunction accumulableChainingFunction)
				{
					accumulableChainingFunction = this;
					return true;
				}

				// Token: 0x06001D70 RID: 7536 RVA: 0x00049AAF File Offset: 0x00047CAF
				public IAccumulable CreateAccumulable(RecordValue arguments, IAccumulable accumulable)
				{
					return new TableModule.Table.ToRecordsFunctionValue.ToRecordsAccumulable(accumulable);
				}

				// Token: 0x04000AA4 RID: 2724
				private const string enumerableParameter = "table";

				// Token: 0x02000305 RID: 773
				private sealed class ToRecordsAccumulable : IAccumulable
				{
					// Token: 0x06001D71 RID: 7537 RVA: 0x00049AB7 File Offset: 0x00047CB7
					public ToRecordsAccumulable(IAccumulable accumulable)
					{
						this.accumulable = accumulable;
					}

					// Token: 0x06001D72 RID: 7538 RVA: 0x00049AC6 File Offset: 0x00047CC6
					public IAccumulator CreateAccumulator()
					{
						return new TableModule.Table.ToRecordsFunctionValue.ToRecordsAccumulable.ToRecordsAccumulator(this);
					}

					// Token: 0x04000AA5 RID: 2725
					private readonly IAccumulable accumulable;

					// Token: 0x02000306 RID: 774
					private sealed class ToRecordsAccumulator : TransformingAccumulator
					{
						// Token: 0x06001D73 RID: 7539 RVA: 0x00049ACE File Offset: 0x00047CCE
						public ToRecordsAccumulator(TableModule.Table.ToRecordsFunctionValue.ToRecordsAccumulable accumulable)
							: base(accumulable.accumulable.CreateAccumulator())
						{
						}

						// Token: 0x06001D74 RID: 7540 RVA: 0x0000A6A5 File Offset: 0x000088A5
						protected override IValueReference Transform(IValueReference valueReference)
						{
							return valueReference;
						}
					}
				}
			}

			// Token: 0x02000307 RID: 775
			private class FromRecordsFunctionValue : NativeFunctionValue3<TableValue, ListValue, Value, Value>
			{
				// Token: 0x06001D75 RID: 7541 RVA: 0x00049AE4 File Offset: 0x00047CE4
				public FromRecordsFunctionValue()
					: base(TypeValue.Table, 1, "records", TypeValue.List, "columns", TypeValue.Any, "missingField", Library.MissingField.Type.Nullable)
				{
				}

				// Token: 0x06001D76 RID: 7542 RVA: 0x00049B20 File Offset: 0x00047D20
				public override TableValue TypedInvoke(ListValue records, Value columns, Value missingField)
				{
					if (columns.IsNull)
					{
						using (IEnumerator<IValueReference> enumerator = records.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								columns = Library.Record.FieldNames.Invoke(enumerator.Current.Value);
							}
							else
							{
								columns = ListValue.Empty;
							}
						}
					}
					TableTypeValue tableTypeValue = TableTypeValue.FromValue(columns, null);
					MissingFieldMode missingFieldMode = RecordTypeAlgebra.GetMissingFieldMode(missingField);
					if (missingFieldMode != MissingFieldMode.Error)
					{
						if (missingFieldMode != MissingFieldMode.UseNull)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.InvalidMissingFieldMode_ErrorOrUseNull, missingField, null);
						}
						RecordValue fields = tableTypeValue.ItemType.Fields;
						for (int i = 0; i < fields.Keys.Length; i++)
						{
							if (!fields[i].AsRecord["Type"].AsType.IsNullable)
							{
								throw ValueException.NewExpressionError<Message0>(Strings.TableFromRecordsColumnTypesMustBeNullable, null, null);
							}
						}
					}
					FunctionValue functionValue = new TableModule.Table.FromRecordsFunctionValue.ToListFunctionValue(tableTypeValue.ItemType.Fields.Keys, missingField);
					return LinesModule.Table.FromList.Invoke(records, functionValue, tableTypeValue).AsTable;
				}

				// Token: 0x02000308 RID: 776
				private class ToListFunctionValue : NativeFunctionValue1
				{
					// Token: 0x06001D77 RID: 7543 RVA: 0x00049C28 File Offset: 0x00047E28
					public ToListFunctionValue(Keys keys, Value missingField)
					{
						Value[] array = new Value[keys.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = TextValue.New(keys[i]);
						}
						this.columns = ListValue.New(array);
						this.missingField = missingField;
					}

					// Token: 0x06001D78 RID: 7544 RVA: 0x00049C77 File Offset: 0x00047E77
					public override Value Invoke(Value record)
					{
						return Library.Record.FieldValues.Invoke(Library.Record.SelectFields.Invoke(record, this.columns, this.missingField));
					}

					// Token: 0x04000AA6 RID: 2726
					private readonly ListValue columns;

					// Token: 0x04000AA7 RID: 2727
					private readonly Value missingField;
				}
			}

			// Token: 0x02000309 RID: 777
			private class KeysFunctionValue : NativeFunctionValue1<ListValue, TableValue>
			{
				// Token: 0x06001D79 RID: 7545 RVA: 0x00049A90 File Offset: 0x00047C90
				public KeysFunctionValue()
					: base(TypeValue.List, "table", TypeValue.Table)
				{
				}

				// Token: 0x06001D7A RID: 7546 RVA: 0x00049C9A File Offset: 0x00047E9A
				public override ListValue TypedInvoke(TableValue table)
				{
					return TableModule._Type.TableKeys.Invoke(table.Type).AsList;
				}
			}

			// Token: 0x0200030A RID: 778
			private sealed class AddKeyFunctionValue : NativeFunctionValue3<TableValue, TableValue, ListValue, LogicalValue>
			{
				// Token: 0x06001D7B RID: 7547 RVA: 0x00049CB1 File Offset: 0x00047EB1
				public AddKeyFunctionValue()
					: base(TypeValue.Table, "table", TypeValue.Table, "columns", ListTypeValue.Text, "isPrimary", TypeValue.Logical)
				{
				}

				// Token: 0x06001D7C RID: 7548 RVA: 0x00049CDC File Offset: 0x00047EDC
				public override TableValue TypedInvoke(TableValue table, ListValue columns, LogicalValue isPrimary)
				{
					TypeValue asType = TableModule._Type.AddTableKey.Invoke(table.Type.AsTableType, columns, isPrimary).AsType;
					return table.NewType(asType).AsTable;
				}
			}

			// Token: 0x0200030B RID: 779
			private class ReplaceKeysFunctionValue : NativeFunctionValue2<TableValue, TableValue, ListValue>
			{
				// Token: 0x06001D7D RID: 7549 RVA: 0x00049D12 File Offset: 0x00047F12
				public ReplaceKeysFunctionValue()
					: base(TypeValue.Table, "table", TypeValue.Table, "keys", TypeValue.List)
				{
				}

				// Token: 0x06001D7E RID: 7550 RVA: 0x00049D34 File Offset: 0x00047F34
				public override TableValue TypedInvoke(TableValue table, ListValue keys)
				{
					TypeValue asType = TableModule._Type.ReplaceTableKeys.Invoke(table.Type.AsTableType, keys).AsType;
					return table.NewType(asType).AsTable;
				}
			}

			// Token: 0x0200030C RID: 780
			public sealed class FromHandlersFunctionValue : NativeFunctionValue1<TableValue, RecordValue>
			{
				// Token: 0x06001D7F RID: 7551 RVA: 0x00049D69 File Offset: 0x00047F69
				public FromHandlersFunctionValue(IEngineHost engineHost)
					: base(TypeValue.Table, "handlers", TypeValue.Record)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x06001D80 RID: 7552 RVA: 0x00049D88 File Offset: 0x00047F88
				public override TableValue TypedInvoke(RecordValue handlers)
				{
					TableModule.Table.FromHandlersFunctionValue.HandlersQuery handlersQuery = new TableModule.Table.FromHandlersFunctionValue.HandlersQuery(this.engineHost, handlers);
					return new QueryTableValue(new TableModule.Table.FromHandlersFunctionValue.HandlersOptimizableQuery(handlersQuery), handlersQuery.Type);
				}

				// Token: 0x04000AA8 RID: 2728
				private readonly IEngineHost engineHost;

				// Token: 0x0200030D RID: 781
				private sealed class HandlersQuery : FilteredDataSourceQuery
				{
					// Token: 0x06001D81 RID: 7553 RVA: 0x00049DB3 File Offset: 0x00047FB3
					public HandlersQuery(IEngineHost engineHost, RecordValue handlers)
					{
						this.engineHost = engineHost;
						this.handlers = handlers;
					}

					// Token: 0x17000D68 RID: 3432
					// (get) Token: 0x06001D82 RID: 7554 RVA: 0x00049DC9 File Offset: 0x00047FC9
					public TableTypeValue Type
					{
						get
						{
							if (this.type == null)
							{
								this.type = this.GetHandler("GetType").Invoke().AsType.AsTableType;
							}
							return this.type;
						}
					}

					// Token: 0x17000D69 RID: 3433
					// (get) Token: 0x06001D83 RID: 7555 RVA: 0x00049DF9 File Offset: 0x00047FF9
					public override IEngineHost EngineHost
					{
						get
						{
							return this.engineHost;
						}
					}

					// Token: 0x17000D6A RID: 3434
					// (get) Token: 0x06001D84 RID: 7556 RVA: 0x00049E01 File Offset: 0x00048001
					public override Keys Columns
					{
						get
						{
							return this.Type.ItemType.Fields.Keys;
						}
					}

					// Token: 0x06001D85 RID: 7557 RVA: 0x00049E18 File Offset: 0x00048018
					public override TypeValue GetColumnType(int column)
					{
						return this.Type.ItemType.Fields[column]["Type"].AsType;
					}

					// Token: 0x17000D6B RID: 3435
					// (get) Token: 0x06001D86 RID: 7558 RVA: 0x00049E3F File Offset: 0x0004803F
					public override IList<TableKey> TableKeys
					{
						get
						{
							return this.Type.TableKeys;
						}
					}

					// Token: 0x17000D6C RID: 3436
					// (get) Token: 0x06001D87 RID: 7559 RVA: 0x00049E4C File Offset: 0x0004804C
					public override IList<ComputedColumn> ComputedColumns
					{
						get
						{
							return new ComputedColumn[0];
						}
					}

					// Token: 0x17000D6D RID: 3437
					// (get) Token: 0x06001D88 RID: 7560 RVA: 0x00049E54 File Offset: 0x00048054
					public override TableSortOrder SortOrder
					{
						get
						{
							return TableSortOrder.None;
						}
					}

					// Token: 0x17000D6E RID: 3438
					// (get) Token: 0x06001D89 RID: 7561 RVA: 0x00049E5B File Offset: 0x0004805B
					public override IQueryDomain QueryDomain
					{
						get
						{
							return TableModule.Table.FromHandlersFunctionValue.HandlersQuery.HandlersQueryDomain.Instance;
						}
					}

					// Token: 0x17000D6F RID: 3439
					// (get) Token: 0x06001D8A RID: 7562 RVA: 0x00049E64 File Offset: 0x00048064
					public IExpression Expression
					{
						get
						{
							if (this.expression == null)
							{
								try
								{
									FunctionValue functionValue;
									if (this.TryGetHandler("GetExpression", out functionValue))
									{
										Value value = functionValue.Invoke();
										if (!value.IsNull)
										{
											this.expression = MAstToExpressionVisitor.ToExpression(value.AsRecord);
										}
									}
								}
								catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
								{
									this.LogValueExceptionsOnHandlerFailures(ex, "Expression");
								}
							}
							return this.expression;
						}
					}

					// Token: 0x06001D8B RID: 7563 RVA: 0x00049EEC File Offset: 0x000480EC
					public override bool TryGetExpression(out IExpression expression)
					{
						if (base.TryGetExpression(out expression))
						{
							return true;
						}
						expression = this.Expression;
						return expression != null;
					}

					// Token: 0x17000D70 RID: 3440
					// (get) Token: 0x06001D8C RID: 7564 RVA: 0x00049F08 File Offset: 0x00048108
					public override RowCount RowCount
					{
						get
						{
							ValueException ex = null;
							try
							{
								FunctionValue functionValue;
								if (this.TryGetHandler("GetRowCount", out functionValue))
								{
									return new RowCount(functionValue.Invoke().AsNumber.AsInteger64);
								}
							}
							catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
							{
								ex = ex2;
							}
							this.ReportFoldingFailure("RowCount", ex);
							return base.RowCount;
						}
					}

					// Token: 0x06001D8D RID: 7565 RVA: 0x00049F84 File Offset: 0x00048184
					public override IEnumerable<IValueReference> GetRows()
					{
						return this.GetTable();
					}

					// Token: 0x06001D8E RID: 7566 RVA: 0x00049F8C File Offset: 0x0004818C
					public override bool TryGetReader(out IPageReader reader)
					{
						reader = this.GetTable().GetReader();
						return true;
					}

					// Token: 0x06001D8F RID: 7567 RVA: 0x00049F9C File Offset: 0x0004819C
					public override void TestConnection()
					{
						FunctionValue functionValue;
						if (this.TryGetHandler("OnTestConnection", out functionValue))
						{
							Value value = functionValue.Invoke();
							if (!value.AsBoolean)
							{
								throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_TestConnectionFailed, value, null);
							}
						}
						else
						{
							this.ReportFoldingFailure("TestConnection", null);
							base.TestConnection();
						}
					}

					// Token: 0x06001D90 RID: 7568 RVA: 0x00049FE8 File Offset: 0x000481E8
					public override Query RenameReorderColumns(ColumnSelection columnSelection)
					{
						ColumnSelection.SelectMap selectMap = columnSelection.CreateSelectMap(this.Columns);
						ArrayBuilder<Value> arrayBuilder = default(ArrayBuilder<Value>);
						for (int i = 0; i < this.Columns.Length; i++)
						{
							string text = columnSelection.Keys[selectMap.MapColumn(i)];
							string text2 = this.Columns[i];
							if (text != text2)
							{
								arrayBuilder.Add(RecordValue.New(TableModule.Table.FromHandlersFunctionValue.HandlersQuery.OldNameNewNameKeys, new Value[]
								{
									TextValue.New(text2),
									TextValue.New(text)
								}));
							}
						}
						Query query;
						if (arrayBuilder.Count > 0)
						{
							ValueException ex = null;
							try
							{
								FunctionValue functionValue;
								if (this.TryGetHandler("OnRenameColumns", out functionValue))
								{
									query = functionValue.Invoke(ListValue.New(arrayBuilder.ToArray())).AsTable.Query;
									if (!columnSelection.Ordered)
									{
										query = query.RenameReorderColumns(columnSelection);
									}
									return query;
								}
							}
							catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
							{
								ex = ex2;
							}
							this.ReportFoldingFailure("RenameReorderColumns", ex);
						}
						else if (!columnSelection.Ordered && this.TryTableSelectColumns(columnSelection.Keys, out query))
						{
							return query;
						}
						return base.RenameReorderColumns(columnSelection);
					}

					// Token: 0x06001D91 RID: 7569 RVA: 0x0004A130 File Offset: 0x00048330
					public override Query SelectColumns(ColumnSelection columnSelection)
					{
						if (columnSelection.Keys.Length < this.Columns.Length)
						{
							Query query;
							if (this.TryTableSelectColumns(columnSelection.Keys, out query))
							{
								return query;
							}
							this.ReportFoldingFailure("SelectColumns", null);
						}
						return base.SelectColumns(columnSelection);
					}

					// Token: 0x06001D92 RID: 7570 RVA: 0x0004A17C File Offset: 0x0004837C
					private bool TryTableSelectColumns(Keys keys, out Query result)
					{
						FunctionValue functionValue;
						if (this.TryGetHandler("OnSelectColumns", out functionValue))
						{
							ValueException ex2;
							try
							{
								Value[] array = new Value[keys.Length];
								for (int i = 0; i < array.Length; i++)
								{
									array[i] = TextValue.New(keys[i]);
								}
								TableValue asTable = functionValue.Invoke(ListValue.New(array)).AsTable;
								result = asTable.Query;
								return true;
							}
							catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
							{
								ex2 = ex;
							}
							this.ReportFoldingFailure("TryTableSelectColumns", ex2);
						}
						result = null;
						return false;
					}

					// Token: 0x06001D93 RID: 7571 RVA: 0x0004A22C File Offset: 0x0004842C
					public override Query SelectRows(FunctionValue selector)
					{
						ValueException ex = null;
						try
						{
							FunctionValue functionValue;
							FunctionValue functionValue2;
							if (this.TryGetRowFunction(selector, out functionValue) && this.TryGetHandler("OnSelectRows", out functionValue2))
							{
								return functionValue2.Invoke(functionValue).AsTable.Query;
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("SelectRows", ex);
						return base.SelectRows(selector);
					}

					// Token: 0x06001D94 RID: 7572 RVA: 0x0004A2B4 File Offset: 0x000484B4
					public override Query AddColumns(ColumnsConstructor columnGenerator)
					{
						ValueException ex = null;
						try
						{
							IList<QueryExpression> list = AddColumnsQuery.CreateQueryExpressions(columnGenerator.Function, QueryTableValue.NewRowType(this));
							if (list != null)
							{
								Value[] array = new Value[list.Count];
								for (int i = 0; i < array.Length; i++)
								{
									TextValue textValue = TextValue.New(columnGenerator.Names[i]);
									IValueReference valueReference = columnGenerator.Types[i] ?? TypeValue.Any;
									FunctionValue functionValue;
									if (!this.TryGetRowFunction(list[i], out functionValue))
									{
										array = null;
										break;
									}
									array[i] = RecordValue.New(TableModule.Table.FromHandlersFunctionValue.HandlersQuery.NameFunctionTypeKeys, new IValueReference[] { textValue, functionValue, valueReference });
								}
								FunctionValue functionValue2;
								if (array != null && this.TryGetHandler("OnAddColumns", out functionValue2))
								{
									return functionValue2.Invoke(ListValue.New(array)).AsTable.Query;
								}
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("AddColumns", ex);
						return base.AddColumns(columnGenerator);
					}

					// Token: 0x06001D95 RID: 7573 RVA: 0x0004A3D4 File Offset: 0x000485D4
					public override Query Skip(RowCount count)
					{
						if (count.IsZero)
						{
							return this;
						}
						ValueException ex = null;
						try
						{
							FunctionValue functionValue;
							if (!count.IsInfinite && this.TryGetHandler("OnSkip", out functionValue))
							{
								NumberValue numberValue = NumberValue.New(count.Value);
								return functionValue.Invoke(numberValue).AsTable.Query;
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("Skip", ex);
						return base.Skip(count);
					}

					// Token: 0x06001D96 RID: 7574 RVA: 0x0004A474 File Offset: 0x00048674
					public override Query Take(RowCount count)
					{
						if (count.IsInfinite)
						{
							return this;
						}
						ValueException ex = null;
						try
						{
							FunctionValue functionValue;
							if (this.TryGetHandler("OnTake", out functionValue))
							{
								NumberValue numberValue = NumberValue.New(count.Value);
								return functionValue.Invoke(numberValue).AsTable.Query;
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("Take", ex);
						return base.Take(count);
					}

					// Token: 0x06001D97 RID: 7575 RVA: 0x0004A508 File Offset: 0x00048708
					public override Query Sort(TableSortOrder sortOrder)
					{
						Query query = SortQuery.New(sortOrder, RowCount.Infinite, this);
						SortQuery sortQuery = query as SortQuery;
						if (sortQuery == null || this != sortQuery.InnerQuery)
						{
							return query;
						}
						sortOrder = sortQuery.SortOrder;
						ValueException ex = null;
						try
						{
							QueryExpression[] array;
							bool[] array2;
							if (!sortOrder.IsEmpty && SortQuery.TryGetSelectors(sortOrder, QueryTableValue.NewRowType(this), out array, out array2))
							{
								Value[] array3 = new Value[array.Length];
								for (int i = 0; i < array3.Length; i++)
								{
									int num;
									if (!array[i].TryGetColumnAccess(out num))
									{
										array3 = null;
										break;
									}
									NumberValue numberValue = (array2[i] ? Library.Order.Ascending : Library.Order.Descending);
									array3[i] = RecordValue.New(TableModule.Table.FromHandlersFunctionValue.HandlersQuery.NameAndOrderKeys, new Value[]
									{
										TextValue.New(this.Columns[num]),
										numberValue
									});
								}
								FunctionValue functionValue;
								if (array3 != null && this.TryGetHandler("OnSort", out functionValue))
								{
									return functionValue.Invoke(ListValue.New(array3)).AsTable.Query;
								}
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("Sort", ex);
						return base.Sort(sortOrder);
					}

					// Token: 0x06001D98 RID: 7576 RVA: 0x0004A650 File Offset: 0x00048850
					public override Query Group(Grouping grouping)
					{
						ValueException ex = null;
						try
						{
							if (grouping.Comparer == null)
							{
								Value[] array = new Value[grouping.KeyKeys.Length];
								for (int i = 0; i < array.Length; i++)
								{
									array[i] = TextValue.New(grouping.KeyKeys[i]);
								}
								Value[] array2 = new Value[grouping.Constructors.Length];
								for (int j = 0; j < array2.Length; j++)
								{
									ColumnConstructor columnConstructor = grouping.Constructors[j];
									TextValue textValue = TextValue.New(columnConstructor.Name);
									FunctionValue functionValue;
									if (!this.TryGetRowFunction(columnConstructor.Function, out functionValue))
									{
										array2 = null;
										break;
									}
									array2[j] = RecordValue.New(TableModule.Table.FromHandlersFunctionValue.HandlersQuery.NameFunctionTypeKeys, new IValueReference[] { textValue, functionValue, columnConstructor.Type });
								}
								FunctionValue functionValue2;
								if (array2 != null && this.TryGetHandler("OnGroup", out functionValue2))
								{
									return functionValue2.Invoke(ListValue.New(array), ListValue.New(array2)).AsTable.Query;
								}
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("Group", ex);
						return base.Group(grouping);
					}

					// Token: 0x06001D99 RID: 7577 RVA: 0x0004A798 File Offset: 0x00048998
					public override bool TryJoinAsLeft(RowCount take, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
					{
						ValueException ex = null;
						try
						{
							FunctionValue functionValue;
							if (this.TryGetHandler("OnJoin", out functionValue))
							{
								Value[] array = new Value[leftKeyColumns.Length];
								for (int i = 0; i < array.Length; i++)
								{
									Value value = null;
									if (keyEqualityComparers != null)
									{
										value = keyEqualityComparers[i];
									}
									array[i] = RecordValue.New(TableModule.Table.FromHandlersFunctionValue.HandlersQuery.JoinKeysKeys, new Value[]
									{
										TextValue.New(this.Columns[leftKeyColumns[i]]),
										TextValue.New(rightQuery.Columns[rightKeyColumns[i]]),
										value ?? Library._Value.Equals
									});
								}
								TableValue tableValue = functionValue.Invoke(TableModule.JoinSide.Left, new QueryTableValue(this), new QueryTableValue(rightQuery), ListValue.New(array).ToTable(TableModule.Table.FromHandlersFunctionValue.HandlersQuery.JoinKeysType), TableTypeAlgebra.GetValue(joinKind)).AsTable;
								tableValue = tableValue.Take(take);
								query = tableValue.Query;
								return true;
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("TryJoinAsLeft", ex);
						query = null;
						return false;
					}

					// Token: 0x06001D9A RID: 7578 RVA: 0x0004A8C8 File Offset: 0x00048AC8
					public override bool TryJoinAsRight(RowCount take, Query leftQuery, int[] leftKeyColumns, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
					{
						ValueException ex = null;
						try
						{
							FunctionValue functionValue;
							if (this.TryGetHandler("OnJoin", out functionValue))
							{
								Value[] array = new Value[leftKeyColumns.Length];
								for (int i = 0; i < array.Length; i++)
								{
									Value value = null;
									if (keyEqualityComparers != null)
									{
										value = keyEqualityComparers[i];
									}
									array[i] = RecordValue.New(TableModule.Table.FromHandlersFunctionValue.HandlersQuery.JoinKeysKeys, new Value[]
									{
										TextValue.New(leftQuery.Columns[leftKeyColumns[i]]),
										TextValue.New(this.Columns[rightKeyColumns[i]]),
										value ?? Library._Value.Equals
									});
								}
								TableValue tableValue = functionValue.Invoke(TableModule.JoinSide.Right, new QueryTableValue(leftQuery), new QueryTableValue(this), ListValue.New(array).ToTable(TableModule.Table.FromHandlersFunctionValue.HandlersQuery.JoinKeysType), TableTypeAlgebra.GetValue(joinKind)).AsTable;
								tableValue = tableValue.Take(take);
								query = tableValue.Query;
								return true;
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("TryJoinAsRight", ex);
						query = null;
						return false;
					}

					// Token: 0x06001D9B RID: 7579 RVA: 0x0004A9F8 File Offset: 0x00048BF8
					public override bool TryCombineAsItem(Query[] queries, int index, TypeValue[] types, TableSortOrder sortOrder, int disjointColumn, out Query query)
					{
						ValueException ex = null;
						try
						{
							FunctionValue functionValue;
							if (sortOrder == null && disjointColumn == -1 && this.TryGetHandler("OnCombine", out functionValue))
							{
								TableValue[] array = new TableValue[queries.Length];
								for (int i = 0; i < queries.Length; i++)
								{
									array[i] = new QueryTableValue(queries[i]);
								}
								FunctionValue functionValue2 = functionValue;
								Value[] array2 = array;
								TableValue asTable = functionValue2.Invoke(ListValue.New(array2), NumberValue.New(index)).AsTable;
								query = asTable.Query;
								return true;
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("TryCombineAsItem", ex);
						query = null;
						return false;
					}

					// Token: 0x06001D9C RID: 7580 RVA: 0x0004AABC File Offset: 0x00048CBC
					public override Query Pivot(TableTypeValue inputType, TableTypeValue outputType, string[] pivotValues, string attributeColumn, string valueColumn, FunctionValue aggregateFunction)
					{
						ValueException ex = null;
						try
						{
							FunctionValue functionValue;
							if (this.TryGetHandler("OnPivot", out functionValue))
							{
								return functionValue.Invoke(ListValue.New(pivotValues), TextValue.New(attributeColumn), TextValue.New(valueColumn), aggregateFunction).AsTable.Query;
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("Pivot", ex);
						return base.Pivot(inputType, outputType, pivotValues, attributeColumn, valueColumn, aggregateFunction);
					}

					// Token: 0x06001D9D RID: 7581 RVA: 0x0004AB54 File Offset: 0x00048D54
					public override Query Unpivot(TableTypeValue inputType, TableTypeValue outputType, string[] pivotValues, string attributeColumn, string valueColumn)
					{
						ValueException ex = null;
						try
						{
							FunctionValue functionValue;
							if (this.TryGetHandler("OnUnpivot", out functionValue))
							{
								return functionValue.Invoke(ListValue.New(pivotValues), TextValue.New(attributeColumn), TextValue.New(valueColumn)).AsTable.Query;
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("Unpivot", ex);
						return base.Unpivot(inputType, outputType, pivotValues, attributeColumn, valueColumn);
					}

					// Token: 0x06001D9E RID: 7582 RVA: 0x0004ABE8 File Offset: 0x00048DE8
					public override Query Distinct(TableDistinct distinctCriteria)
					{
						ValueException ex = null;
						try
						{
							Value[] array = new Value[distinctCriteria.Distincts.Length];
							for (int i = 0; i < array.Length; i++)
							{
								Distinct distinct = distinctCriteria.Distincts[i];
								QueryExpression queryExpression = null;
								if (distinct.Selector != null)
								{
									queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), distinct.Selector);
								}
								int num;
								if (queryExpression == null || distinct.Comparer != null || !queryExpression.TryGetColumnAccess(out num))
								{
									array = null;
									break;
								}
								array[i] = TextValue.New(this.Columns[num]);
							}
							FunctionValue functionValue;
							if (array != null && this.TryGetHandler("OnDistinct", out functionValue))
							{
								return functionValue.Invoke(ListValue.New(array)).AsTable.Query;
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("Distinct", ex);
						return base.Distinct(distinctCriteria);
					}

					// Token: 0x06001D9F RID: 7583 RVA: 0x0004ACEC File Offset: 0x00048EEC
					public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
					{
						ValueException ex = null;
						try
						{
							FunctionValue functionValue;
							if (this.TryGetHandler("OnInvoke", out functionValue))
							{
								result = functionValue.Invoke(function, ListValue.New(arguments), NumberValue.New(index));
								return true;
							}
						}
						catch (ValueException ex2) when (!ViewExceptions.IsMarked(ex2))
						{
							ex = ex2;
						}
						this.ReportFoldingFailure("TryInvokeAsArgument", ex);
						result = null;
						return false;
					}

					// Token: 0x06001DA0 RID: 7584 RVA: 0x0004AD6C File Offset: 0x00048F6C
					public override TableValue DeltaSince(Value tag)
					{
						FunctionValue functionValue;
						if (this.TryGetHandler("OnDeltaSince", out functionValue) && functionValue.Type.AsFunctionType.Parameters.Count == 1)
						{
							try
							{
								return functionValue.Invoke(tag).AsTable;
							}
							catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
							{
								this.LogValueExceptionsOnHandlerFailures(ex, "DeltaSince");
							}
						}
						return base.DeltaSince(tag);
					}

					// Token: 0x06001DA1 RID: 7585 RVA: 0x0004ADF4 File Offset: 0x00048FF4
					public override Value NativeQuery(TextValue query, Value parameters, Value options)
					{
						FunctionValue functionValue;
						if (this.TryGetHandler("OnNativeQuery", out functionValue) && functionValue.Type.AsFunctionType.Parameters.Count == 3)
						{
							try
							{
								return functionValue.Invoke(query, parameters, options);
							}
							catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
							{
								this.LogValueExceptionsOnHandlerFailures(ex, "NativeQuery");
							}
						}
						return base.NativeQuery(query, parameters, options);
					}

					// Token: 0x06001DA2 RID: 7586 RVA: 0x0004AE7C File Offset: 0x0004907C
					public override ActionValue InsertRows(Query rowsToInsert)
					{
						try
						{
							FunctionValue functionValue;
							if (this.TryGetHandler("OnInsertRows", out functionValue))
							{
								return functionValue.Invoke(TableModule.Table.FromHandlersFunctionValue.HandlersQuery.GetTable(rowsToInsert)).AsAction;
							}
						}
						catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
						{
							this.LogValueExceptionsOnHandlerFailures(ex, "InsertRows");
						}
						return base.InsertRows(rowsToInsert);
					}

					// Token: 0x06001DA3 RID: 7587 RVA: 0x0004AEF4 File Offset: 0x000490F4
					public override ActionValue UpdateRows(ColumnUpdates columnUpdates)
					{
						FunctionValue functionValue;
						if (this.TryGetHandler("OnUpdateRows", out functionValue) && functionValue.Type.AsFunctionType.Min == 1)
						{
							try
							{
								return functionValue.Invoke(this.GetColumnUpdates(columnUpdates)).AsAction;
							}
							catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
							{
								this.LogValueExceptionsOnHandlerFailures(ex, "UpdateRows");
								return base.UpdateRows(columnUpdates, ConstantFunctionValue.EachTrue);
							}
						}
						return base.UpdateRows(columnUpdates);
					}

					// Token: 0x06001DA4 RID: 7588 RVA: 0x0004AF88 File Offset: 0x00049188
					public override ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector)
					{
						try
						{
							FunctionValue functionValue;
							FunctionValue functionValue2;
							if (this.TryGetRowFunction(selector, out functionValue) && this.TryGetHandler("OnUpdateRows", out functionValue2) && functionValue2.Type.AsFunctionType.Max >= 2)
							{
								return functionValue2.Invoke(this.GetColumnUpdates(columnUpdates), functionValue).AsAction;
							}
						}
						catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
						{
							this.LogValueExceptionsOnHandlerFailures(ex, "UpdateRows");
						}
						return base.UpdateRows(columnUpdates, selector);
					}

					// Token: 0x06001DA5 RID: 7589 RVA: 0x0004B020 File Offset: 0x00049220
					public override ActionValue DeleteRows()
					{
						FunctionValue functionValue;
						if (this.TryGetHandler("OnDeleteRows", out functionValue) && functionValue.Type.AsFunctionType.Min == 0)
						{
							try
							{
								return functionValue.Invoke().AsAction;
							}
							catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
							{
								this.LogValueExceptionsOnHandlerFailures(ex, "DeleteRows");
								return base.DeleteRows(ConstantFunctionValue.EachTrue);
							}
						}
						return base.DeleteRows();
					}

					// Token: 0x06001DA6 RID: 7590 RVA: 0x0004B0AC File Offset: 0x000492AC
					public override ActionValue DeleteRows(FunctionValue selector)
					{
						try
						{
							FunctionValue functionValue;
							FunctionValue functionValue2;
							if (this.TryGetRowFunction(selector, out functionValue) && this.TryGetHandler("OnDeleteRows", out functionValue2) && functionValue2.Type.AsFunctionType.Max >= 1)
							{
								return functionValue2.Invoke(functionValue).AsAction;
							}
						}
						catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
						{
							this.LogValueExceptionsOnHandlerFailures(ex, "DeleteRows");
						}
						return base.DeleteRows(selector);
					}

					// Token: 0x06001DA7 RID: 7591 RVA: 0x0004B13C File Offset: 0x0004933C
					public override ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
					{
						FunctionValue functionValue;
						if (this.TryGetHandler("OnNativeStatement", out functionValue) && functionValue.Type.AsFunctionType.Parameters.Count == 3)
						{
							try
							{
								return functionValue.Invoke(statement, parameters, options).AsAction;
							}
							catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
							{
								this.LogValueExceptionsOnHandlerFailures(ex, "NativeStatement");
							}
						}
						return base.NativeStatement(statement, parameters, options);
					}

					// Token: 0x06001DA8 RID: 7592 RVA: 0x0004B1C8 File Offset: 0x000493C8
					private bool TryGetHandler(string key, out FunctionValue handler)
					{
						Value value;
						if (this.handlers.TryGetValue(key, out value))
						{
							handler = value.AsFunction;
							return true;
						}
						handler = null;
						return false;
					}

					// Token: 0x06001DA9 RID: 7593 RVA: 0x0004B1F3 File Offset: 0x000493F3
					private FunctionValue GetHandler(string key)
					{
						return this.handlers[key].AsFunction;
					}

					// Token: 0x06001DAA RID: 7594 RVA: 0x0004B208 File Offset: 0x00049408
					private TableValue GetTable()
					{
						if (this.table == null)
						{
							FunctionValue handler = this.GetHandler("GetRows");
							this.table = handler.Invoke().NewType(this.Type).AsTable;
						}
						return this.table;
					}

					// Token: 0x06001DAB RID: 7595 RVA: 0x0004B24C File Offset: 0x0004944C
					private bool TryGetRowFunction(FunctionValue function, out FunctionValue rowFunction)
					{
						QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), function);
						return this.TryGetRowFunction(queryExpression, out rowFunction);
					}

					// Token: 0x06001DAC RID: 7596 RVA: 0x0004B270 File Offset: 0x00049470
					private bool TryGetRowFunction(QueryExpression expression, out FunctionValue rowFunction)
					{
						if (expression.AllAccess(ArgumentAccess.Safe, (int c) => true))
						{
							rowFunction = QueryExpressionAssembler.Assemble(this.Columns, expression);
							rowFunction = BinaryOperator.AddMeta.Invoke(rowFunction, TableModule.ItemExpression.IsNormalizedMetadata).AsFunction;
							return true;
						}
						rowFunction = null;
						return false;
					}

					// Token: 0x06001DAD RID: 7597 RVA: 0x0004B2D8 File Offset: 0x000494D8
					private ListValue GetColumnUpdates(ColumnUpdates columnUpdates)
					{
						List<IValueReference> list = new List<IValueReference>(columnUpdates.Updates.Count);
						foreach (KeyValuePair<int, FunctionValue> keyValuePair in columnUpdates.Updates)
						{
							TextValue textValue = TextValue.New(this.Columns[keyValuePair.Key]);
							FunctionValue functionValue;
							if (!this.TryGetRowFunction(keyValuePair.Value, out functionValue))
							{
								list = null;
								break;
							}
							list.Add(RecordValue.New(TableModule.Table.FromHandlersFunctionValue.HandlersQuery.NameFunctionKeys, new Value[] { textValue, functionValue }));
						}
						return ListValue.New(list);
					}

					// Token: 0x06001DAE RID: 7598 RVA: 0x0004B384 File Offset: 0x00049584
					private void ReportFoldingFailure(string caller, ValueException e = null)
					{
						using (IHostTrace hostTrace = TracingService.CreatePerformanceTrace(this.engineHost, "HandlersQuery/ReportFoldingFailure/" + caller, TraceEventType.Information, null))
						{
							if (e != null)
							{
								hostTrace.Add(e, true);
							}
							if (this.engineHost.QueryService<IFoldingFailureService>().ThrowOnFoldingFailure)
							{
								throw ValueException.NewExpressionError<Message0>(Strings.FoldingFailure, null, null);
							}
						}
					}

					// Token: 0x06001DAF RID: 7599 RVA: 0x0004B3F0 File Offset: 0x000495F0
					private void LogValueExceptionsOnHandlerFailures(ValueException ve, string caller)
					{
						using (IHostTrace hostTrace = TracingService.CreatePerformanceTrace(this.engineHost, "HandlersQuery/ReportHandlerFailure/" + caller, TraceEventType.Information, null))
						{
							hostTrace.Add(ve, true);
						}
					}

					// Token: 0x06001DB0 RID: 7600 RVA: 0x0004B43C File Offset: 0x0004963C
					private static TableValue GetTable(Query query)
					{
						TableQuery tableQuery = query as TableQuery;
						if (tableQuery != null)
						{
							return tableQuery.Table;
						}
						return new QueryTableValue(query);
					}

					// Token: 0x04000AA9 RID: 2729
					private const string GetExpressionKey = "GetExpression";

					// Token: 0x04000AAA RID: 2730
					private const string GetRowCountKey = "GetRowCount";

					// Token: 0x04000AAB RID: 2731
					private const string GetRowsKey = "GetRows";

					// Token: 0x04000AAC RID: 2732
					private const string GetTypeKey = "GetType";

					// Token: 0x04000AAD RID: 2733
					private const string OnAddColumnsKey = "OnAddColumns";

					// Token: 0x04000AAE RID: 2734
					private const string OnCombineKey = "OnCombine";

					// Token: 0x04000AAF RID: 2735
					private const string OnDistinctKey = "OnDistinct";

					// Token: 0x04000AB0 RID: 2736
					private const string OnGroupKey = "OnGroup";

					// Token: 0x04000AB1 RID: 2737
					private const string OnInvokeKey = "OnInvoke";

					// Token: 0x04000AB2 RID: 2738
					private const string OnJoinKey = "OnJoin";

					// Token: 0x04000AB3 RID: 2739
					private const string OnPivotKey = "OnPivot";

					// Token: 0x04000AB4 RID: 2740
					private const string OnRenameColumnsKey = "OnRenameColumns";

					// Token: 0x04000AB5 RID: 2741
					private const string OnSelectColumnsKey = "OnSelectColumns";

					// Token: 0x04000AB6 RID: 2742
					private const string OnSelectRowsKey = "OnSelectRows";

					// Token: 0x04000AB7 RID: 2743
					private const string OnSkipKey = "OnSkip";

					// Token: 0x04000AB8 RID: 2744
					private const string OnSortKey = "OnSort";

					// Token: 0x04000AB9 RID: 2745
					private const string OnTakeKey = "OnTake";

					// Token: 0x04000ABA RID: 2746
					private const string OnUnpivotKey = "OnUnpivot";

					// Token: 0x04000ABB RID: 2747
					private const string OnDeltaSinceKey = "OnDeltaSince";

					// Token: 0x04000ABC RID: 2748
					private const string OnNativeQueryKey = "OnNativeQuery";

					// Token: 0x04000ABD RID: 2749
					private const string OnTestConnectionKey = "OnTestConnection";

					// Token: 0x04000ABE RID: 2750
					private const string OnInsertRowsKey = "OnInsertRows";

					// Token: 0x04000ABF RID: 2751
					private const string OnUpdateRowsKey = "OnUpdateRows";

					// Token: 0x04000AC0 RID: 2752
					private const string OnDeleteRowsKey = "OnDeleteRows";

					// Token: 0x04000AC1 RID: 2753
					private const string OnNativeStatementKey = "OnNativeStatement";

					// Token: 0x04000AC2 RID: 2754
					private static readonly Keys NameFunctionKeys = Microsoft.Mashup.Engine1.Runtime.Keys.New("Name", "Function");

					// Token: 0x04000AC3 RID: 2755
					private static readonly Keys NameFunctionTypeKeys = Microsoft.Mashup.Engine1.Runtime.Keys.New("Name", "Function", "Type");

					// Token: 0x04000AC4 RID: 2756
					private static readonly Keys NameAndOrderKeys = Microsoft.Mashup.Engine1.Runtime.Keys.New("Name", "Order");

					// Token: 0x04000AC5 RID: 2757
					private static readonly Keys OldNameNewNameKeys = Microsoft.Mashup.Engine1.Runtime.Keys.New("OldName", "NewName");

					// Token: 0x04000AC6 RID: 2758
					private static readonly Keys JoinKeysKeys = Microsoft.Mashup.Engine1.Runtime.Keys.New("Left", "Right", "EqualityComparer");

					// Token: 0x04000AC7 RID: 2759
					private static readonly TableTypeValue JoinKeysType = TableTypeValue.New(TableModule.Table.FromHandlersFunctionValue.HandlersQuery.JoinKeysKeys, null);

					// Token: 0x04000AC8 RID: 2760
					private readonly IEngineHost engineHost;

					// Token: 0x04000AC9 RID: 2761
					private readonly RecordValue handlers;

					// Token: 0x04000ACA RID: 2762
					private TableTypeValue type;

					// Token: 0x04000ACB RID: 2763
					private TableValue table;

					// Token: 0x04000ACC RID: 2764
					private IExpression expression;

					// Token: 0x0200030E RID: 782
					private sealed class HandlersQueryDomain : INativeQueryDomain, IQueryDomain
					{
						// Token: 0x06001DB2 RID: 7602 RVA: 0x000020FD File Offset: 0x000002FD
						private HandlersQueryDomain()
						{
						}

						// Token: 0x06001DB3 RID: 7603 RVA: 0x0004B4EB File Offset: 0x000496EB
						public bool IsCompatibleWith(IQueryDomain domain)
						{
							return domain == TableModule.Table.FromHandlersFunctionValue.HandlersQuery.HandlersQueryDomain.Instance;
						}

						// Token: 0x17000D71 RID: 3441
						// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x00002105 File Offset: 0x00000305
						public bool CanIndex
						{
							get
							{
								return false;
							}
						}

						// Token: 0x06001DB5 RID: 7605 RVA: 0x0000A6A5 File Offset: 0x000088A5
						public Query Optimize(Query query)
						{
							return query;
						}

						// Token: 0x06001DB6 RID: 7606 RVA: 0x0004B4F8 File Offset: 0x000496F8
						public bool TryGetNativeQuery(Query query, out IResource resource, out Value nativeQuery, out RecordValue options)
						{
							TableModule.Table.FromHandlersFunctionValue.HandlersQuery handlersQuery = query as TableModule.Table.FromHandlersFunctionValue.HandlersQuery;
							if (handlersQuery != null)
							{
								IExpression expression;
								if (!TableModule.Table.FromHandlersFunctionValue.HandlersQuery.HandlersQueryDomain.TryGetValueNativeQuery(handlersQuery.Expression, out expression, out nativeQuery))
								{
									expression = handlersQuery.Expression;
									nativeQuery = Value.Null;
								}
								string text;
								if (TableModule.Table.FromHandlersFunctionValue.HandlersQuery.HandlersQueryDomain.TryGetResourceAccess(expression, out resource, out text, out options) && ((nativeQuery != null && nativeQuery.IsNull) || text == null))
								{
									if (text != null)
									{
										nativeQuery = TextValue.New(text);
									}
									return true;
								}
							}
							resource = null;
							nativeQuery = null;
							options = null;
							return false;
						}

						// Token: 0x06001DB7 RID: 7607 RVA: 0x0004B564 File Offset: 0x00049764
						private static bool TryGetResourceAccess(IExpression expression, out IResource resource, out string nativeQuery, out RecordValue options)
						{
							IInvocationExpression invocationExpression = expression as IInvocationExpression;
							Value value;
							IDataSourceLocation dataSourceLocation;
							Keys keys;
							if (invocationExpression != null && invocationExpression.Function.TryGetConstant(out value) && value.IsFunction && value.AsFunction.TryGetLocation(invocationExpression, out dataSourceLocation, out options, out keys) && dataSourceLocation.TryGetResource(out resource))
							{
								nativeQuery = dataSourceLocation.Query;
								return true;
							}
							resource = null;
							nativeQuery = null;
							options = null;
							return false;
						}

						// Token: 0x06001DB8 RID: 7608 RVA: 0x0004B5C4 File Offset: 0x000497C4
						private static bool TryGetValueNativeQuery(IExpression expression, out IExpression target, out Value nativeQuery)
						{
							IList<IExpression> list;
							if (expression.TryGetInvocation(Library._Value.NativeQuery, 2, out list))
							{
								target = list[0];
								ExpressionKind kind = list[1].Kind;
								if (kind != ExpressionKind.Constant)
								{
									if (kind == ExpressionKind.NotImplemented)
									{
										nativeQuery = null;
										return true;
									}
								}
								else
								{
									IConstantExpression constantExpression = (IConstantExpression)list[1];
									if (constantExpression.Value.IsText)
									{
										nativeQuery = constantExpression.Value.AsText;
										return true;
									}
								}
							}
							target = null;
							nativeQuery = null;
							return false;
						}

						// Token: 0x04000ACD RID: 2765
						public static readonly TableModule.Table.FromHandlersFunctionValue.HandlersQuery.HandlersQueryDomain Instance = new TableModule.Table.FromHandlersFunctionValue.HandlersQuery.HandlersQueryDomain();
					}
				}

				// Token: 0x02000310 RID: 784
				private sealed class HandlersOptimizableQuery : OptimizableQuery
				{
					// Token: 0x06001DBD RID: 7613 RVA: 0x0004B64D File Offset: 0x0004984D
					public HandlersOptimizableQuery(Query query)
						: base(query)
					{
					}

					// Token: 0x17000D72 RID: 3442
					// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0004B656 File Offset: 0x00049856
					public override IQueryDomain QueryDomain
					{
						get
						{
							return TableModule.Table.FromHandlersFunctionValue.HandlersOptimizableQuery.HandlersOptimizingQueryDomain.Instance;
						}
					}

					// Token: 0x02000311 RID: 785
					private sealed class HandlersOptimizingQueryDomain : IQueryDomain
					{
						// Token: 0x06001DBF RID: 7615 RVA: 0x000020FD File Offset: 0x000002FD
						private HandlersOptimizingQueryDomain()
						{
						}

						// Token: 0x06001DC0 RID: 7616 RVA: 0x00002139 File Offset: 0x00000339
						public bool IsCompatibleWith(IQueryDomain other)
						{
							return true;
						}

						// Token: 0x17000D73 RID: 3443
						// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x00002105 File Offset: 0x00000305
						public bool CanIndex
						{
							get
							{
								return false;
							}
						}

						// Token: 0x06001DC2 RID: 7618 RVA: 0x0004B65D File Offset: 0x0004985D
						public Query Optimize(Query query)
						{
							query = TableModule.Table.FromHandlersFunctionValue.HandlersOptimizableQuery.HandlersOptimizingQueryDomain.ExpandRewriter.Instance.VisitQuery(query);
							query = TableModule.Table.FromHandlersFunctionValue.HandlersOptimizableQuery.HandlersOptimizingQueryDomain.HandlersOptimizingQueryVisitor.Instance.VisitQuery(query);
							query = query.QueryDomain.Optimize(query);
							return query;
						}

						// Token: 0x04000AD0 RID: 2768
						public static readonly IQueryDomain Instance = new TableModule.Table.FromHandlersFunctionValue.HandlersOptimizableQuery.HandlersOptimizingQueryDomain();

						// Token: 0x02000312 RID: 786
						private sealed class HandlersOptimizingQueryVisitor : FoldingQueryVisitor
						{
							// Token: 0x06001DC4 RID: 7620 RVA: 0x0004B694 File Offset: 0x00049894
							protected override Query VisitDataSource(DataSourceQuery query)
							{
								TableModule.Table.FromHandlersFunctionValue.HandlersOptimizableQuery handlersOptimizableQuery = query as TableModule.Table.FromHandlersFunctionValue.HandlersOptimizableQuery;
								if (handlersOptimizableQuery != null)
								{
									return handlersOptimizableQuery.Query;
								}
								return query;
							}

							// Token: 0x04000AD1 RID: 2769
							public static readonly QueryVisitor Instance = new TableModule.Table.FromHandlersFunctionValue.HandlersOptimizableQuery.HandlersOptimizingQueryDomain.HandlersOptimizingQueryVisitor();
						}

						// Token: 0x02000313 RID: 787
						private sealed class ExpandRewriter : FoldingQueryVisitor
						{
							// Token: 0x06001DC7 RID: 7623 RVA: 0x0004B6C8 File Offset: 0x000498C8
							protected override Query VisitExpandRecordColumn(ExpandRecordColumnQuery query)
							{
								Query query2 = this.VisitQuery(query.InnerQuery);
								return new TableModule.Table.FromHandlersFunctionValue.HandlersOptimizableQuery.HandlersOptimizingQueryDomain.HandlersExpandRecordColumnQuery(query.ColumnToExpand, query.FieldsToProject, query.NewColumns, null, query2);
							}

							// Token: 0x04000AD2 RID: 2770
							public static readonly QueryVisitor Instance = new TableModule.Table.FromHandlersFunctionValue.HandlersOptimizableQuery.HandlersOptimizingQueryDomain.ExpandRewriter();
						}

						// Token: 0x02000314 RID: 788
						private sealed class HandlersExpandRecordColumnQuery : ExpandRecordColumnQuery
						{
							// Token: 0x06001DCA RID: 7626 RVA: 0x0004B707 File Offset: 0x00049907
							public HandlersExpandRecordColumnQuery(int columnToExpand, Keys fieldsToProject, Keys newColumns, TypeValue[] projectedTypes, Query innerQuery)
								: base(columnToExpand, fieldsToProject, newColumns, projectedTypes, innerQuery)
							{
							}

							// Token: 0x06001DCB RID: 7627 RVA: 0x0004B716 File Offset: 0x00049916
							protected override bool TryGetInnerQueryExpression(RecordTypeValue expandedRecordType, QueryExpression expr, out QueryExpression newExpr)
							{
								return base.TryGetInnerQueryExpression(expandedRecordType, expr, this.ApplyBefore, out newExpr);
							}

							// Token: 0x17000D74 RID: 3444
							// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0004B727 File Offset: 0x00049927
							private Func<QueryExpression, bool> ApplyBefore
							{
								get
								{
									return (QueryExpression node) => node.AllAccess(ArgumentAccess.Safe, (int column) => true);
								}
							}
						}
					}
				}
			}

			// Token: 0x02000316 RID: 790
			private class RemoveColumnsFunctionValue : NativeFunctionValue3<TableValue, TableValue, Value, Value>
			{
				// Token: 0x06001DD1 RID: 7633 RVA: 0x0004B780 File Offset: 0x00049980
				public RemoveColumnsFunctionValue()
					: base(TypeValue.Table, 2, "table", TypeValue.Table, "columns", TypeValue.Any, "missingField", Library.MissingField.Type.Nullable)
				{
				}

				// Token: 0x06001DD2 RID: 7634 RVA: 0x0004B7BC File Offset: 0x000499BC
				public override TableValue TypedInvoke(TableValue table, Value columns, Value missingField)
				{
					return table.RemoveColumns(columns, missingField);
				}
			}

			// Token: 0x02000317 RID: 791
			private class GroupFunctionValue : NativeFunctionValue5<TableValue, TableValue, Value, ListValue, Value, Value>
			{
				// Token: 0x06001DD3 RID: 7635 RVA: 0x0004B7C8 File Offset: 0x000499C8
				public GroupFunctionValue()
					: base(TypeValue.Table, 3, "table", TypeValue.Table, "key", TypeValue.Any, "aggregatedColumns", TypeValue.List, "groupKind", Library.GroupKind.Type.Nullable, "comparer", NullableTypeValue.Function)
				{
				}

				// Token: 0x06001DD4 RID: 7636 RVA: 0x0004B818 File Offset: 0x00049A18
				public override TableValue TypedInvoke(TableValue table, Value key, ListValue aggregatedColumns, Value groupKind, Value comparer)
				{
					return table.Group(key, aggregatedColumns, groupKind, comparer);
				}
			}

			// Token: 0x02000318 RID: 792
			private class JoinFunctionValue : NativeFunctionValueN<TableValue>
			{
				// Token: 0x06001DD5 RID: 7637 RVA: 0x0004B828 File Offset: 0x00049A28
				public JoinFunctionValue()
					: base(TypeValue.Table, 4, new string[] { "table1", "key1", "table2", "key2", "joinKind", "joinAlgorithm", "keyEqualityComparers" }, new TypeValue[]
					{
						TypeValue.Table,
						TypeValue.Any,
						TypeValue.Table,
						TypeValue.Any,
						Library.JoinKind.Type.Nullable,
						TableModule.JoinAlgorithmEnum.Type.Nullable,
						TypeValue.List.Nullable
					})
				{
				}

				// Token: 0x06001DD6 RID: 7638 RVA: 0x0004B8CC File Offset: 0x00049ACC
				public static JoinAlgorithm GetAlgorithm(Value joinAlgorithm)
				{
					if (joinAlgorithm.Equals(TableModule.JoinAlgorithmEnum.Dynamic))
					{
						return JoinAlgorithm.Dynamic;
					}
					if (joinAlgorithm.Equals(TableModule.JoinAlgorithmEnum.PairwiseHash))
					{
						return JoinAlgorithm.PairwiseHash;
					}
					if (joinAlgorithm.Equals(TableModule.JoinAlgorithmEnum.SortMerge))
					{
						return JoinAlgorithm.SortMerge;
					}
					if (joinAlgorithm.Equals(TableModule.JoinAlgorithmEnum.LeftHash))
					{
						return JoinAlgorithm.LeftHash;
					}
					if (joinAlgorithm.Equals(TableModule.JoinAlgorithmEnum.RightHash))
					{
						return JoinAlgorithm.RightHash;
					}
					if (joinAlgorithm.Equals(TableModule.JoinAlgorithmEnum.LeftIndex))
					{
						return JoinAlgorithm.LeftIndex;
					}
					if (joinAlgorithm.Equals(TableModule.JoinAlgorithmEnum.RightIndex))
					{
						return JoinAlgorithm.RightIndex;
					}
					throw ValueException.NewExpressionError<Message0>(Strings.InvalidJoinAlgorithm, joinAlgorithm, null);
				}

				// Token: 0x06001DD7 RID: 7639 RVA: 0x0004B96C File Offset: 0x00049B6C
				protected override TableValue TypedInvokeN(Value[] arguments)
				{
					TableTypeAlgebra.JoinKind joinKind = (arguments[4].IsNull ? TableTypeAlgebra.JoinKind.Inner : TableTypeAlgebra.GetJoinKind(arguments[4]));
					JoinAlgorithm joinAlgorithm = (arguments[5].IsNull ? JoinAlgorithm.Dynamic : TableModule.Table.JoinFunctionValue.GetAlgorithm(arguments[5]));
					if (!joinAlgorithm.Supports(joinKind))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.InvalidJoinAlgorithmForJoinKind, arguments[5], null);
					}
					return TableValue.Join(arguments[0].AsTable, arguments[1], arguments[2].AsTable, arguments[3], arguments[4], joinAlgorithm, arguments[6]);
				}
			}

			// Token: 0x02000319 RID: 793
			private class AddJoinColumnFunctionValue : NativeFunctionValue5<TableValue, TableValue, Value, Value, Value, TextValue>
			{
				// Token: 0x06001DD8 RID: 7640 RVA: 0x0004B9E4 File Offset: 0x00049BE4
				public AddJoinColumnFunctionValue()
					: base(TypeValue.Table, "table1", TypeValue.Table, "key1", TypeValue.Any, "table2", TypeValue.Any, "key2", TypeValue.Any, "newColumnName", TypeValue.Text)
				{
				}

				// Token: 0x06001DD9 RID: 7641 RVA: 0x0004BA2E File Offset: 0x00049C2E
				public override TableValue TypedInvoke(TableValue table1, Value key1, Value table2, Value key2, TextValue newColumnName)
				{
					return table1.NestedJoin(key1, table2, key2, Value.Null, newColumnName, Value.Null);
				}
			}

			// Token: 0x0200031A RID: 794
			private class NestedJoinFunctionValue : NativeFunctionValueN<TableValue>
			{
				// Token: 0x06001DDA RID: 7642 RVA: 0x0004BA48 File Offset: 0x00049C48
				public NestedJoinFunctionValue()
					: base(TypeValue.Table, 5, new string[] { "table1", "key1", "table2", "key2", "newColumnName", "joinKind", "keyEqualityComparers" }, new TypeValue[]
					{
						TypeValue.Table,
						TypeValue.Any,
						TypeValue.Any,
						TypeValue.Any,
						TypeValue.Text,
						Library.JoinKind.Type.Nullable,
						TypeValue.List.Nullable
					})
				{
				}

				// Token: 0x06001DDB RID: 7643 RVA: 0x0004BAE7 File Offset: 0x00049CE7
				protected override TableValue TypedInvokeN(Value[] arguments)
				{
					return arguments[0].AsTable.NestedJoin(arguments[1], arguments[2], arguments[3], arguments[5], arguments[4].AsText, arguments[6]);
				}
			}

			// Token: 0x0200031B RID: 795
			private class AddIndexColumnFunctionValue : NativeFunctionValue5<TableValue, TableValue, TextValue, Value, Value, Value>
			{
				// Token: 0x06001DDC RID: 7644 RVA: 0x0004BB10 File Offset: 0x00049D10
				public AddIndexColumnFunctionValue()
					: base(TypeValue.Table, 2, "table", TypeValue.Table, "newColumnName", TypeValue.Text, "initialValue", NullableTypeValue.Number, "increment", NullableTypeValue.Number, "columnType", NullableTypeValue._Type)
				{
				}

				// Token: 0x06001DDD RID: 7645 RVA: 0x0004BB5C File Offset: 0x00049D5C
				public override TableValue TypedInvoke(TableValue table, TextValue newColumnName, Value initialValue, Value increment, Value columnType)
				{
					if (initialValue.IsNull)
					{
						initialValue = NumberValue.Zero;
					}
					if (increment.IsNull)
					{
						increment = NumberValue.One;
					}
					if (columnType.IsNull)
					{
						columnType = TypeValue.Number;
					}
					TableTypeValue asTableType = table.Type.AsTableType;
					RecordTypeValue itemType = asTableType.ItemType;
					Value[] array = new Value[itemType.Fields.Keys.Length + 1];
					KeysBuilder keysBuilder = new KeysBuilder(array.Length);
					for (int i = 0; i < array.Length - 1; i++)
					{
						keysBuilder.Add(itemType.Fields.Keys[i]);
						array[i] = itemType.Fields[i];
					}
					keysBuilder.Add(newColumnName.AsString);
					array[array.Length - 1] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						columnType,
						LogicalValue.False
					});
					Keys keys = keysBuilder.ToKeys();
					TableTypeValue tableTypeValue = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(keys, array)), asTableType.TableKeys);
					return new TableModule.Table.AddIndexColumnFunctionValue.AddIndexColumnTableValue(table, keys, newColumnName.AsString, initialValue.AsNumber, increment.AsNumber, columnType.AsType, tableTypeValue);
				}

				// Token: 0x0200031C RID: 796
				private sealed class AddIndexColumnTableValue : OptimizableTableValue
				{
					// Token: 0x06001DDE RID: 7646 RVA: 0x0004BC83 File Offset: 0x00049E83
					public AddIndexColumnTableValue(TableValue table, Keys columns, string newColumnName, NumberValue initialValue, NumberValue increment, TypeValue columnType, TableTypeValue type)
					{
						this.table = table;
						this.columns = columns;
						this.newColumnName = newColumnName;
						this.initialValue = initialValue;
						this.increment = increment;
						this.columnType = columnType;
						this.type = type;
					}

					// Token: 0x17000D75 RID: 3445
					// (get) Token: 0x06001DDF RID: 7647 RVA: 0x0004BCC0 File Offset: 0x00049EC0
					public override TypeValue Type
					{
						get
						{
							return this.type;
						}
					}

					// Token: 0x17000D76 RID: 3446
					// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x0004BCC8 File Offset: 0x00049EC8
					public override Keys Columns
					{
						get
						{
							return this.columns;
						}
					}

					// Token: 0x17000D77 RID: 3447
					// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0004BCD0 File Offset: 0x00049ED0
					public override TableSortOrder SortOrder
					{
						get
						{
							return this.table.SortOrder;
						}
					}

					// Token: 0x17000D78 RID: 3448
					// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x0004BCE0 File Offset: 0x00049EE0
					public override IExpression Expression
					{
						get
						{
							if (this.expression == null)
							{
								this.expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.AddIndexColumn), new IExpression[]
								{
									this.table.Expression,
									new ConstantExpressionSyntaxNode(TextValue.New(this.newColumnName)),
									new ConstantExpressionSyntaxNode(this.initialValue),
									new ConstantExpressionSyntaxNode(this.increment),
									new ConstantExpressionSyntaxNode(this.columnType)
								});
							}
							return this.expression;
						}
					}

					// Token: 0x06001DE3 RID: 7651 RVA: 0x0004BD61 File Offset: 0x00049F61
					public override TableValue Optimize()
					{
						return new OptimizedTableValue(new TableModule.Table.AddIndexColumnFunctionValue.AddIndexColumnTableValue(this.table.Optimize(), this.columns, this.newColumnName, this.initialValue, this.increment, this.columnType, this.type));
					}

					// Token: 0x06001DE4 RID: 7652 RVA: 0x0004BD9C File Offset: 0x00049F9C
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new TableModule.Table.AddIndexColumnFunctionValue.AddIndexColumnTableValue.AddIndexColumnEnumerator(this.table, this.columns, this.initialValue, this.increment);
					}

					// Token: 0x04000AD6 RID: 2774
					private readonly TableValue table;

					// Token: 0x04000AD7 RID: 2775
					private readonly Keys columns;

					// Token: 0x04000AD8 RID: 2776
					private readonly string newColumnName;

					// Token: 0x04000AD9 RID: 2777
					private readonly NumberValue initialValue;

					// Token: 0x04000ADA RID: 2778
					private readonly NumberValue increment;

					// Token: 0x04000ADB RID: 2779
					private readonly TypeValue columnType;

					// Token: 0x04000ADC RID: 2780
					private readonly TableTypeValue type;

					// Token: 0x04000ADD RID: 2781
					private IExpression expression;

					// Token: 0x0200031D RID: 797
					private class AddIndexColumnEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06001DE5 RID: 7653 RVA: 0x0004BDBC File Offset: 0x00049FBC
						public AddIndexColumnEnumerator(TableValue table, Keys columns, NumberValue initialValue, NumberValue increment)
						{
							this.enumerator = table.GetEnumerator();
							this.columns = columns;
							this.value = initialValue;
							this.increment = increment;
							this.newColumnCount = this.columns.Length;
							this.addedColumn = this.newColumnCount - 1;
						}

						// Token: 0x17000D79 RID: 3449
						// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0004BE10 File Offset: 0x0004A010
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06001DE7 RID: 7655 RVA: 0x0004BE18 File Offset: 0x0004A018
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x06001DE8 RID: 7656 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x17000D7A RID: 3450
						// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x0004BE28 File Offset: 0x0004A028
						public IValueReference Current
						{
							get
							{
								if (this.current == null)
								{
									IValueReference valueReference = this.enumerator.Current;
									IValueReference[] array = new IValueReference[this.newColumnCount];
									for (int i = 0; i < this.addedColumn; i++)
									{
										try
										{
											array[i] = valueReference.Value[i];
										}
										catch (ValueException ex)
										{
											array[i] = new ExceptionValueReference(ex);
										}
									}
									array[this.addedColumn] = this.value;
									this.current = RecordValue.New(this.columns, array);
								}
								return this.current;
							}
						}

						// Token: 0x06001DEA RID: 7658 RVA: 0x0004BEBC File Offset: 0x0004A0BC
						public bool MoveNext()
						{
							this.current = null;
							if (this.enumerator.MoveNext())
							{
								if (!this.haveMoved)
								{
									this.haveMoved = true;
								}
								else
								{
									this.value = this.value.Add(this.increment).AsNumber;
								}
								return true;
							}
							return false;
						}

						// Token: 0x04000ADE RID: 2782
						private IEnumerator<IValueReference> enumerator;

						// Token: 0x04000ADF RID: 2783
						private Keys columns;

						// Token: 0x04000AE0 RID: 2784
						private NumberValue value;

						// Token: 0x04000AE1 RID: 2785
						private NumberValue increment;

						// Token: 0x04000AE2 RID: 2786
						private RecordValue current;

						// Token: 0x04000AE3 RID: 2787
						private bool haveMoved;

						// Token: 0x04000AE4 RID: 2788
						private int addedColumn;

						// Token: 0x04000AE5 RID: 2789
						private int newColumnCount;
					}
				}
			}

			// Token: 0x0200031E RID: 798
			private sealed class FillDownFunctionValue : NativeFunctionValue2<TableValue, TableValue, ListValue>
			{
				// Token: 0x06001DEB RID: 7659 RVA: 0x0004BF0D File Offset: 0x0004A10D
				public FillDownFunctionValue()
					: base(TypeValue.Table, 2, "table", TypeValue.Table, "columns", TypeValue.List)
				{
				}

				// Token: 0x06001DEC RID: 7660 RVA: 0x0004BF30 File Offset: 0x0004A130
				public override TableValue TypedInvoke(TableValue tableValue, ListValue columns)
				{
					if (columns.Count == 0)
					{
						return tableValue;
					}
					int[] columns2 = TableValue.GetColumns(tableValue.Columns, columns);
					return new TableModule.Table.FillDownFunctionValue.FillDownTableValue(tableValue, columns2);
				}

				// Token: 0x0200031F RID: 799
				private class FillDownTableValue : OptimizableTableValue
				{
					// Token: 0x06001DED RID: 7661 RVA: 0x0004BF5C File Offset: 0x0004A15C
					public FillDownTableValue(TableValue table, int[] fillDownColumnIndices)
					{
						this.table = table;
						this.fillDownColumnIndices = fillDownColumnIndices;
						this.isFillDownColumn = new bool[table.Columns.Length];
						foreach (int num in fillDownColumnIndices)
						{
							this.isFillDownColumn[num] = true;
						}
					}

					// Token: 0x17000D7B RID: 3451
					// (get) Token: 0x06001DEE RID: 7662 RVA: 0x0004BFB0 File Offset: 0x0004A1B0
					public override Keys Columns
					{
						get
						{
							return this.table.Columns;
						}
					}

					// Token: 0x17000D7C RID: 3452
					// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0004BFBD File Offset: 0x0004A1BD
					public override TypeValue Type
					{
						get
						{
							return this.table.Type;
						}
					}

					// Token: 0x17000D7D RID: 3453
					// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x0004BFCA File Offset: 0x0004A1CA
					public override TableSortOrder SortOrder
					{
						get
						{
							return this.table.SortOrder;
						}
					}

					// Token: 0x17000D7E RID: 3454
					// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x0004BFD8 File Offset: 0x0004A1D8
					public override IExpression Expression
					{
						get
						{
							if (this.expression == null)
							{
								IExpression expression = new ConstantExpressionSyntaxNode(TableModule.Table.FillDown);
								IExpression[] array = new IExpression[2];
								array[0] = this.table.Expression;
								int num = 1;
								Value[] array2 = this.fillDownColumnIndices.Select((int i) => TextValue.New(this.table.Columns[i])).ToArray<TextValue>();
								array[num] = new ConstantExpressionSyntaxNode(ListValue.New(array2));
								this.expression = new InvocationExpressionSyntaxNodeN(expression, array);
							}
							return this.expression;
						}
					}

					// Token: 0x06001DF2 RID: 7666 RVA: 0x0004C047 File Offset: 0x0004A247
					public override TableValue Optimize()
					{
						return new OptimizedTableValue(new TableModule.Table.FillDownFunctionValue.FillDownTableValue(this.table.Optimize(), this.fillDownColumnIndices));
					}

					// Token: 0x06001DF3 RID: 7667 RVA: 0x0004C064 File Offset: 0x0004A264
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new TableModule.Table.FillDownFunctionValue.FillDownTableValue.FillDownEnumerator(this.table, this.isFillDownColumn);
					}

					// Token: 0x04000AE6 RID: 2790
					private readonly TableValue table;

					// Token: 0x04000AE7 RID: 2791
					private readonly int[] fillDownColumnIndices;

					// Token: 0x04000AE8 RID: 2792
					private readonly bool[] isFillDownColumn;

					// Token: 0x04000AE9 RID: 2793
					private IExpression expression;

					// Token: 0x02000320 RID: 800
					private class FillDownEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06001DF5 RID: 7669 RVA: 0x0004C08F File Offset: 0x0004A28F
						public FillDownEnumerator(TableValue table, bool[] isFillDownColumn)
						{
							this.isFillDownColumn = isFillDownColumn;
							this.enumerator = table.GetEnumerator();
							this.columns = table.Columns;
							this.fillDownColumnValue = new IValueReference[table.Columns.Length];
						}

						// Token: 0x17000D7F RID: 3455
						// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x0004C0CC File Offset: 0x0004A2CC
						public IValueReference Current
						{
							get
							{
								return this.current;
							}
						}

						// Token: 0x17000D80 RID: 3456
						// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x0004C0D4 File Offset: 0x0004A2D4
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06001DF8 RID: 7672 RVA: 0x0004C0DC File Offset: 0x0004A2DC
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x06001DF9 RID: 7673 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06001DFA RID: 7674 RVA: 0x0004C0EC File Offset: 0x0004A2EC
						public bool MoveNext()
						{
							this.current = null;
							bool flag = this.enumerator.MoveNext();
							if (flag)
							{
								this.current = this.enumerator.Current;
								RecordValue recordValue = null;
								try
								{
									recordValue = this.enumerator.Current.Value.AsRecord;
								}
								catch (ValueException ex)
								{
									ExceptionValueReference exceptionValueReference = new ExceptionValueReference(ex);
									this.current = exceptionValueReference;
									for (int i = 0; i < this.isFillDownColumn.Length; i++)
									{
										if (this.isFillDownColumn[i])
										{
											this.fillDownColumnValue[i] = exceptionValueReference;
										}
									}
								}
								if (recordValue != null)
								{
									this.current = this.CreateNewFilledRecordValue(recordValue);
								}
							}
							return flag;
						}

						// Token: 0x06001DFB RID: 7675 RVA: 0x0004C190 File Offset: 0x0004A390
						private RecordValue CreateNewFilledRecordValue(RecordValue recordValue)
						{
							IValueReference[] array = new IValueReference[this.columns.Length];
							for (int i = 0; i < this.columns.Length; i++)
							{
								array[i] = recordValue.GetReference(i);
								if (this.isFillDownColumn[i])
								{
									bool flag = false;
									try
									{
										flag = recordValue[i].IsNull;
									}
									catch (ValueException)
									{
									}
									if (!flag)
									{
										this.fillDownColumnValue[i] = array[i];
									}
									else if (this.fillDownColumnValue[i] != null)
									{
										array[i] = this.fillDownColumnValue[i];
									}
								}
							}
							return RecordValue.New(this.columns, array);
						}

						// Token: 0x04000AEA RID: 2794
						private IEnumerator<IValueReference> enumerator;

						// Token: 0x04000AEB RID: 2795
						private readonly Keys columns;

						// Token: 0x04000AEC RID: 2796
						private readonly bool[] isFillDownColumn;

						// Token: 0x04000AED RID: 2797
						private IValueReference[] fillDownColumnValue;

						// Token: 0x04000AEE RID: 2798
						private IValueReference current;
					}
				}
			}

			// Token: 0x02000321 RID: 801
			private class TransformColumnsFunctionValue : NativeFunctionValue4<TableValue, TableValue, ListValue, Value, Value>
			{
				// Token: 0x06001DFC RID: 7676 RVA: 0x0004C230 File Offset: 0x0004A430
				public TransformColumnsFunctionValue()
					: base(TypeValue.Table, 2, "table", TypeValue.Table, "transformOperations", TypeValue.List, "defaultTransformation", NullableTypeValue.Function, "missingField", Library.MissingField.Type.Nullable)
				{
				}

				// Token: 0x06001DFD RID: 7677 RVA: 0x0004C276 File Offset: 0x0004A476
				public override TableValue TypedInvoke(TableValue table, ListValue transformOperations, Value defaultTransformation, Value missingField)
				{
					return table.TransformColumns(transformOperations, defaultTransformation, missingField);
				}
			}

			// Token: 0x02000322 RID: 802
			public class TransformColumnTypesFunctionValue : CultureSpecificFunctionValue3<TableValue, TableValue, ListValue, Value>
			{
				// Token: 0x06001DFE RID: 7678 RVA: 0x0004C284 File Offset: 0x0004A484
				public TransformColumnTypesFunctionValue(IEngineHost host)
					: base(host, null, TypeValue.Table, 2, "table", TypeValue.Table, "typeTransformations", TypeValue.List, "culture", NullableTypeValue.Text)
				{
					this.engineHost = host;
				}

				// Token: 0x06001DFF RID: 7679 RVA: 0x0004C2C4 File Offset: 0x0004A4C4
				public override TableValue TypedInvoke(TableValue table, ListValue typeTransformations, Value culture)
				{
					ICulture culture2 = (culture.IsNull ? null : base.GetCulture(culture));
					return table.TransformColumnTypes(this.engineHost, typeTransformations, culture2);
				}

				// Token: 0x04000AEF RID: 2799
				private readonly IEngineHost engineHost;
			}

			// Token: 0x02000323 RID: 803
			private class SortFunctionValue : NativeFunctionValue2<TableValue, TableValue, Value>
			{
				// Token: 0x06001E00 RID: 7680 RVA: 0x0004C2F2 File Offset: 0x0004A4F2
				public SortFunctionValue()
					: base(TypeValue.Table, "table", TypeValue.Table, "comparisonCriteria", TypeValue.Any)
				{
				}

				// Token: 0x06001E01 RID: 7681 RVA: 0x0004C313 File Offset: 0x0004A513
				public override TableValue TypedInvoke(TableValue table, Value comparisonCriteria)
				{
					return table.Sort(comparisonCriteria);
				}
			}

			// Token: 0x02000324 RID: 804
			private class SortDescendingFunctionValue : NativeFunctionValue2<TableValue, TableValue, Value>
			{
				// Token: 0x06001E02 RID: 7682 RVA: 0x0004C2F2 File Offset: 0x0004A4F2
				public SortDescendingFunctionValue()
					: base(TypeValue.Table, "table", TypeValue.Table, "comparisonCriteria", TypeValue.Any)
				{
				}

				// Token: 0x06001E03 RID: 7683 RVA: 0x0004C31C File Offset: 0x0004A51C
				public override TableValue TypedInvoke(TableValue table, Value comparisonCriteria)
				{
					return table.SortDescending(comparisonCriteria);
				}
			}

			// Token: 0x02000325 RID: 805
			private class DistinctFunctionValue : NativeFunctionValue2<TableValue, TableValue, Value>
			{
				// Token: 0x06001E04 RID: 7684 RVA: 0x0004C325 File Offset: 0x0004A525
				public DistinctFunctionValue()
					: base(TypeValue.Table, 1, "table", TypeValue.Table, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x06001E05 RID: 7685 RVA: 0x0004C348 File Offset: 0x0004A548
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					TypeValue type = environment.GetType(arguments[0]);
					if (!type.IsTableType)
					{
						return TypeValue.Table;
					}
					return type;
				}

				// Token: 0x06001E06 RID: 7686 RVA: 0x0004C372 File Offset: 0x0004A572
				public override TableValue TypedInvoke(TableValue table, Value equationCriteria)
				{
					return table.Distinct(equationCriteria);
				}
			}

			// Token: 0x02000326 RID: 806
			private class ExpandListColumnFunctionValue : NativeFunctionValue3<TableValue, TableValue, TextValue, Value>
			{
				// Token: 0x06001E07 RID: 7687 RVA: 0x0004C37C File Offset: 0x0004A57C
				public ExpandListColumnFunctionValue()
					: base(TypeValue.Table, 2, "table", TypeValue.Table, "columnName", TypeValue.Text, "singleOrDefault", NullableTypeValue.Logical)
				{
				}

				// Token: 0x06001E08 RID: 7688 RVA: 0x0004C3B3 File Offset: 0x0004A5B3
				public override TableValue TypedInvoke(TableValue table, TextValue columnName, Value singleOrDefault)
				{
					return table.ExpandListColumn(columnName, singleOrDefault);
				}
			}

			// Token: 0x02000327 RID: 807
			private class ExpandRecordColumnFunctionValue : NativeFunctionValue4<TableValue, TableValue, TextValue, ListValue, Value>
			{
				// Token: 0x06001E09 RID: 7689 RVA: 0x0004C3C0 File Offset: 0x0004A5C0
				public ExpandRecordColumnFunctionValue()
					: base(TypeValue.Table, 3, "table", TypeValue.Table, "column", TypeValue.Text, "fieldNames", TypeValue.List, "newColumnNames", TypeValue.List.Nullable)
				{
				}

				// Token: 0x06001E0A RID: 7690 RVA: 0x0004C406 File Offset: 0x0004A606
				public override TableValue TypedInvoke(TableValue table, TextValue columnName, ListValue fieldNames, Value newColumnNames)
				{
					return table.ExpandRecordColumn(columnName, fieldNames, newColumnNames);
				}
			}

			// Token: 0x02000328 RID: 808
			private class SingleRowFunctionValue : NativeFunctionValue1<RecordValue, TableValue>
			{
				// Token: 0x06001E0B RID: 7691 RVA: 0x0004C412 File Offset: 0x0004A612
				public SingleRowFunctionValue()
					: base(TypeValue.Record, 1, "table", TypeValue.Table)
				{
				}

				// Token: 0x06001E0C RID: 7692 RVA: 0x0004C42A File Offset: 0x0004A62A
				public override RecordValue TypedInvoke(TableValue table)
				{
					return Library.List.Single.Invoke(table.ToRecords()).AsRecord;
				}
			}

			// Token: 0x02000329 RID: 809
			private class CombineFunctionValue : NativeFunctionValue2<TableValue, ListValue, Value>
			{
				// Token: 0x06001E0D RID: 7693 RVA: 0x0004C441 File Offset: 0x0004A641
				public CombineFunctionValue()
					: base(TypeValue.Table, 1, "tables", TypeValue.List, "columns", TypeValue.Any)
				{
				}

				// Token: 0x06001E0E RID: 7694 RVA: 0x0004C464 File Offset: 0x0004A664
				public override TableValue TypedInvoke(ListValue tables, Value columns)
				{
					TableTypeValue tableTypeValue = (columns.IsNull ? null : TableTypeValue.FromValue(columns, null));
					return TableValue.Combine(tables, tableTypeValue);
				}
			}

			// Token: 0x0200032A RID: 810
			private class UnpivotFunctionValue : NativeFunctionValue4<TableValue, TableValue, ListValue, TextValue, TextValue>
			{
				// Token: 0x06001E0F RID: 7695 RVA: 0x0004C48C File Offset: 0x0004A68C
				public UnpivotFunctionValue()
					: base(TypeValue.Table, "table", TypeValue.Table, "pivotColumns", ListTypeValue.Text, "attributeColumn", TypeValue.Text, "valueColumn", TypeValue.Text)
				{
				}

				// Token: 0x06001E10 RID: 7696 RVA: 0x0004C4CC File Offset: 0x0004A6CC
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					TypeValue type = environment.GetType(arguments[0]);
					IList<string> list;
					string text;
					string text2;
					if (type.IsTableType && arguments[1].TryGetStringList(256, out list) && arguments[2].TryGetStringConstant(out text) && arguments[3].TryGetStringConstant(out text2))
					{
						return TableValue.ConstructUnpivotTableType(new HashSet<string>(list), text, text2, type.AsTableType);
					}
					return TypeValue.Table;
				}

				// Token: 0x06001E11 RID: 7697 RVA: 0x0004C53D File Offset: 0x0004A73D
				public override TableValue TypedInvoke(TableValue table, ListValue toPivot, TextValue attributeColumn, TextValue valueColumn)
				{
					return table.Unpivot(toPivot, attributeColumn, valueColumn, false);
				}
			}

			// Token: 0x0200032B RID: 811
			private class UnpivotOtherColumnsFunctionValue : NativeFunctionValue4<TableValue, TableValue, ListValue, TextValue, TextValue>
			{
				// Token: 0x06001E12 RID: 7698 RVA: 0x0004C54C File Offset: 0x0004A74C
				public UnpivotOtherColumnsFunctionValue()
					: base(TypeValue.Table, "table", TypeValue.Table, "pivotColumns", ListTypeValue.Text, "attributeColumn", TypeValue.Text, "valueColumn", TypeValue.Text)
				{
				}

				// Token: 0x06001E13 RID: 7699 RVA: 0x0004C58C File Offset: 0x0004A78C
				public override TableValue TypedInvoke(TableValue table, ListValue toPivot, TextValue attributeColumn, TextValue valueColumn)
				{
					return table.Unpivot(toPivot, attributeColumn, valueColumn, true);
				}
			}

			// Token: 0x0200032C RID: 812
			private class PivotFunctionValue : NativeFunctionValue5<TableValue, TableValue, ListValue, TextValue, TextValue, Value>
			{
				// Token: 0x06001E14 RID: 7700 RVA: 0x0004C59C File Offset: 0x0004A79C
				public PivotFunctionValue()
					: base(TypeValue.Table, 4, "table", TypeValue.Table, "pivotValues", ListTypeValue.Text, "attributeColumn", TypeValue.Text, "valueColumn", TypeValue.Text, "aggregationFunction", NullableTypeValue.Function)
				{
				}

				// Token: 0x06001E15 RID: 7701 RVA: 0x0004C5E8 File Offset: 0x0004A7E8
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					TypeValue type = environment.GetType(arguments[0]);
					IList<string> list;
					string text;
					string text2;
					if (type.IsTableType && arguments[1].TryGetStringList(256, out list) && arguments[2].TryGetStringConstant(out text) && arguments[3].TryGetStringConstant(out text2))
					{
						return TableValue.ConstructPivotTableType(list, text, text2, type.AsTableType);
					}
					return TypeValue.Table;
				}

				// Token: 0x06001E16 RID: 7702 RVA: 0x0004C654 File Offset: 0x0004A854
				public override TableValue TypedInvoke(TableValue table, ListValue pivotValues, TextValue attributeColumn, TextValue valueColumn, Value aggregationFunction)
				{
					return table.Pivot(pivotValues, attributeColumn, valueColumn, aggregationFunction);
				}
			}

			// Token: 0x0200032D RID: 813
			private class LiteralFunctionValue : NativeFunctionValue2
			{
				// Token: 0x06001E17 RID: 7703 RVA: 0x0004C662 File Offset: 0x0004A862
				public LiteralFunctionValue()
					: base("columns", "rows")
				{
				}

				// Token: 0x06001E18 RID: 7704 RVA: 0x0004C674 File Offset: 0x0004A874
				public override Value Invoke(Value columns, Value rows)
				{
					return TableModule.Table.FromRows.Invoke(rows, columns);
				}

				// Token: 0x06001E19 RID: 7705 RVA: 0x0004C682 File Offset: 0x0004A882
				public override string ToSource()
				{
					return "#table";
				}
			}

			// Token: 0x0200032E RID: 814
			private class SelectRowsWithErrorsFunctionValue : NativeFunctionValue2<TableValue, TableValue, Value>, IAccumulableChainingFunction
			{
				// Token: 0x06001E1A RID: 7706 RVA: 0x0004C689 File Offset: 0x0004A889
				public SelectRowsWithErrorsFunctionValue()
					: base(TypeValue.Table, 1, "table", TypeValue.Table, "columns", TypeValue.List.Nullable)
				{
				}

				// Token: 0x17000D81 RID: 3457
				// (get) Token: 0x06001E1B RID: 7707 RVA: 0x00049581 File Offset: 0x00047781
				public string EnumerableParameter
				{
					get
					{
						return "table";
					}
				}

				// Token: 0x06001E1C RID: 7708 RVA: 0x0004C6B0 File Offset: 0x0004A8B0
				public override TableValue TypedInvoke(TableValue table, Value columns)
				{
					return new TableModule.Table.ErrorSelectingTableValue(table, columns, true);
				}

				// Token: 0x06001E1D RID: 7709 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableChainingFunction(out IAccumulableChainingFunction accumulableChainingFunction)
				{
					accumulableChainingFunction = this;
					return true;
				}

				// Token: 0x06001E1E RID: 7710 RVA: 0x0004C6BA File Offset: 0x0004A8BA
				public IAccumulable CreateAccumulable(RecordValue arguments, IAccumulable accumulable)
				{
					return new TableModule.Table.SelectRowsWithErrorsFunctionValue.SelectRowsWithErrorsAccumulable(accumulable, arguments[this.EnumerableParameter].Type.AsTableType, arguments["columns"]);
				}

				// Token: 0x04000AF0 RID: 2800
				private const string enumerableParameter = "table";

				// Token: 0x0200032F RID: 815
				private sealed class SelectRowsWithErrorsAccumulable : IAccumulable
				{
					// Token: 0x06001E1F RID: 7711 RVA: 0x0004C6E3 File Offset: 0x0004A8E3
					public SelectRowsWithErrorsAccumulable(IAccumulable accumulable, TableTypeValue tableType, Value columns)
					{
						this.accumulable = accumulable;
						this.columns = TableModule.Table.ErrorSelectingTableValue.GetIndicesFromColumnNames(columns, tableType.ItemType.FieldKeys);
					}

					// Token: 0x06001E20 RID: 7712 RVA: 0x0004C709 File Offset: 0x0004A909
					public IAccumulator CreateAccumulator()
					{
						return new TableModule.Table.SelectRowsWithErrorsFunctionValue.SelectRowsWithErrorsAccumulable.SelectRowsWithErrorsAccumulator(this);
					}

					// Token: 0x04000AF1 RID: 2801
					private readonly IAccumulable accumulable;

					// Token: 0x04000AF2 RID: 2802
					private readonly int[] columns;

					// Token: 0x02000330 RID: 816
					private sealed class SelectRowsWithErrorsAccumulator : SelectingAccumulator
					{
						// Token: 0x06001E21 RID: 7713 RVA: 0x0004C711 File Offset: 0x0004A911
						public SelectRowsWithErrorsAccumulator(TableModule.Table.SelectRowsWithErrorsFunctionValue.SelectRowsWithErrorsAccumulable accumulable)
							: base(accumulable.accumulable.CreateAccumulator())
						{
							this.columns = accumulable.columns;
						}

						// Token: 0x06001E22 RID: 7714 RVA: 0x0004C730 File Offset: 0x0004A930
						protected override bool Select(IValueReference valueReference)
						{
							RecordValue asRecord = valueReference.Value.AsRecord;
							bool flag;
							try
							{
								if (this.columns == null)
								{
									for (int i = 0; i < asRecord.Keys.Length; i++)
									{
										Value value = asRecord[i];
									}
								}
								else
								{
									for (int j = 0; j < this.columns.Length; j++)
									{
										Value value2 = asRecord[this.columns[j]];
									}
								}
								flag = false;
							}
							catch (ValueException)
							{
								flag = true;
							}
							return flag;
						}

						// Token: 0x04000AF3 RID: 2803
						private readonly int[] columns;
					}
				}
			}

			// Token: 0x02000331 RID: 817
			private class ReplaceErrorValuesFunctionValue : NativeFunctionValue2<TableValue, TableValue, ListValue>
			{
				// Token: 0x06001E23 RID: 7715 RVA: 0x0004C7B0 File Offset: 0x0004A9B0
				public ReplaceErrorValuesFunctionValue()
					: base(TypeValue.Table, 2, "table", TypeValue.Table, "errorReplacement", TypeValue.List)
				{
				}

				// Token: 0x06001E24 RID: 7716 RVA: 0x0004C7D4 File Offset: 0x0004A9D4
				public override TableValue TypedInvoke(TableValue table, ListValue errorReplacement)
				{
					if (errorReplacement.Count > 0 && errorReplacement.Item0.IsText)
					{
						errorReplacement = ListValue.New(new IValueReference[] { errorReplacement });
					}
					Dictionary<int, Value> dictionary = new Dictionary<int, Value>();
					string[] array = new string[errorReplacement.Count];
					Value[] array2 = new Value[errorReplacement.Count];
					int num = 0;
					foreach (IValueReference valueReference in errorReplacement)
					{
						int num2;
						if (!table.Columns.TryGetKeyIndex(valueReference.Value.AsList.Item0.AsString, out num2))
						{
							throw ValueException.TableColumnNotFound(valueReference.Value.AsList.Item0.AsString);
						}
						if (dictionary.ContainsKey(num2))
						{
							throw ValueException.NewExpressionError<Message1>(Strings.Table_ReplaceError_ColumnAlreadyExists(valueReference.Value.AsList.Item0.AsString), null, null);
						}
						dictionary.Add(num2, valueReference.Value.AsList.Item1);
						array[num] = valueReference.Value.AsList.Item0.AsString;
						array2[num] = table.Type.AsTableType.ItemType.Fields[num2].AsRecord.Item0.AsType.Nullable;
						num++;
					}
					FunctionValue functionValue = new TableModule.Table.ReplaceErrorFunctionValue(dictionary);
					Keys keys = Microsoft.Mashup.Engine1.Runtime.Keys.New(array);
					FunctionValue functionValue2 = functionValue;
					IValueReference[] array3 = array2;
					ColumnsConstructor columnsConstructor = new ColumnsConstructor(keys, functionValue2, array3);
					return table.ReplaceColumns(columnsConstructor);
				}
			}

			// Token: 0x02000332 RID: 818
			private class ReplaceErrorFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06001E25 RID: 7717 RVA: 0x0004C964 File Offset: 0x0004AB64
				public ReplaceErrorFunctionValue(Dictionary<int, Value> columnsErrorReplacements)
					: base(TypeValue.Any, 1, "row", TypeValue.Any)
				{
					this.columnsErrorReplacements = columnsErrorReplacements;
				}

				// Token: 0x06001E26 RID: 7718 RVA: 0x0004C984 File Offset: 0x0004AB84
				public override Value TypedInvoke(Value row)
				{
					IValueReference[] array = new IValueReference[this.columnsErrorReplacements.Count];
					int num = 0;
					foreach (KeyValuePair<int, Value> keyValuePair in this.columnsErrorReplacements)
					{
						try
						{
							array[num] = row[keyValuePair.Key];
						}
						catch (ValueException)
						{
							array[num] = keyValuePair.Value;
						}
						finally
						{
							num++;
						}
					}
					return ListValue.New(array);
				}

				// Token: 0x04000AF4 RID: 2804
				private readonly Dictionary<int, Value> columnsErrorReplacements;
			}

			// Token: 0x02000333 RID: 819
			private class RemoveRowsWithErrorsFunctionValue : NativeFunctionValue2<TableValue, TableValue, Value>
			{
				// Token: 0x06001E27 RID: 7719 RVA: 0x0004C689 File Offset: 0x0004A889
				public RemoveRowsWithErrorsFunctionValue()
					: base(TypeValue.Table, 1, "table", TypeValue.Table, "columns", TypeValue.List.Nullable)
				{
				}

				// Token: 0x06001E28 RID: 7720 RVA: 0x0004CA28 File Offset: 0x0004AC28
				public override TableValue TypedInvoke(TableValue table, Value columns)
				{
					return new TableModule.Table.ErrorSelectingTableValue(table, columns, false);
				}
			}

			// Token: 0x02000334 RID: 820
			private class ErrorSelectingTableValue : OptimizableTableValue
			{
				// Token: 0x06001E29 RID: 7721 RVA: 0x0004CA32 File Offset: 0x0004AC32
				private ErrorSelectingTableValue(TableValue table, int[] columns, bool selectErrors)
				{
					this.table = table;
					this.columns = columns;
					this.selectErrors = selectErrors;
				}

				// Token: 0x06001E2A RID: 7722 RVA: 0x0004CA4F File Offset: 0x0004AC4F
				public ErrorSelectingTableValue(TableValue table, Value columns, bool includeErrors)
					: this(table, TableModule.Table.ErrorSelectingTableValue.GetIndicesFromColumnNames(columns, table.Columns), includeErrors)
				{
				}

				// Token: 0x06001E2B RID: 7723 RVA: 0x0004CA65 File Offset: 0x0004AC65
				public static int[] GetIndicesFromColumnNames(Value columns, Keys defaultKeys)
				{
					if (columns.IsNull)
					{
						return null;
					}
					return TableValue.GetColumns(defaultKeys, columns);
				}

				// Token: 0x06001E2C RID: 7724 RVA: 0x0004CA78 File Offset: 0x0004AC78
				private TableValue Select(TableValue table)
				{
					return new TableModule.Table.ErrorSelectingTableValue(table, this.columns, this.selectErrors);
				}

				// Token: 0x17000D82 RID: 3458
				// (get) Token: 0x06001E2D RID: 7725 RVA: 0x0004CA8C File Offset: 0x0004AC8C
				public override TypeValue Type
				{
					get
					{
						return this.table.Type;
					}
				}

				// Token: 0x17000D83 RID: 3459
				// (get) Token: 0x06001E2E RID: 7726 RVA: 0x0004CA99 File Offset: 0x0004AC99
				public override TableSortOrder SortOrder
				{
					get
					{
						return this.table.SortOrder;
					}
				}

				// Token: 0x17000D84 RID: 3460
				// (get) Token: 0x06001E2F RID: 7727 RVA: 0x0004CAA8 File Offset: 0x0004ACA8
				public override IExpression Expression
				{
					get
					{
						if (this.expression == null)
						{
							List<IExpression> list = new List<IExpression> { this.table.Expression };
							if (this.columns != null)
							{
								List<IExpression> list2 = list;
								Value[] array = this.columns.Select((int i) => TextValue.New(this.table.Columns[i])).ToArray<TextValue>();
								list2.Add(new ConstantExpressionSyntaxNode(ListValue.New(array)));
							}
							this.expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(this.selectErrors ? TableModule.Table.SelectRowsWithErrors : TableModule.Table.RemoveRowsWithErrors), list.ToArray());
						}
						return this.expression;
					}
				}

				// Token: 0x06001E30 RID: 7728 RVA: 0x0004CB3A File Offset: 0x0004AD3A
				public override TableValue Optimize()
				{
					return new OptimizedTableValue(new TableModule.Table.ErrorSelectingTableValue(this.table.Optimize(), this.columns, this.selectErrors));
				}

				// Token: 0x06001E31 RID: 7729 RVA: 0x0004CB5D File Offset: 0x0004AD5D
				public override IEnumerator<IValueReference> GetEnumerator()
				{
					return new TableModule.Table.ErrorSelectingTableValue.ErrorFilteringEnumerator(this.table.GetEnumerator(), this.columns, this.selectErrors);
				}

				// Token: 0x04000AF5 RID: 2805
				private readonly TableValue table;

				// Token: 0x04000AF6 RID: 2806
				private readonly int[] columns;

				// Token: 0x04000AF7 RID: 2807
				private readonly bool selectErrors;

				// Token: 0x04000AF8 RID: 2808
				private IExpression expression;

				// Token: 0x02000335 RID: 821
				private class ErrorFilteringEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
				{
					// Token: 0x06001E33 RID: 7731 RVA: 0x0004CB93 File Offset: 0x0004AD93
					public ErrorFilteringEnumerator(IEnumerator<IValueReference> inner, int[] columns, bool selectErrors)
					{
						this.inner = inner;
						this.columns = columns;
						this.selectErrors = selectErrors;
					}

					// Token: 0x17000D85 RID: 3461
					// (get) Token: 0x06001E34 RID: 7732 RVA: 0x0004CBB0 File Offset: 0x0004ADB0
					public IValueReference Current
					{
						get
						{
							return this.current;
						}
					}

					// Token: 0x06001E35 RID: 7733 RVA: 0x0004CBB8 File Offset: 0x0004ADB8
					public void Dispose()
					{
						this.inner.Dispose();
					}

					// Token: 0x17000D86 RID: 3462
					// (get) Token: 0x06001E36 RID: 7734 RVA: 0x0004CBB0 File Offset: 0x0004ADB0
					object IEnumerator.Current
					{
						get
						{
							return this.current;
						}
					}

					// Token: 0x06001E37 RID: 7735 RVA: 0x0004CBC8 File Offset: 0x0004ADC8
					public bool MoveNext()
					{
						while (this.inner.MoveNext())
						{
							IValueReference valueReference = this.inner.Current;
							try
							{
								RecordValue asRecord = valueReference.Value.AsRecord;
								if (this.columns == null)
								{
									for (int i = 0; i < asRecord.Keys.Length; i++)
									{
										Value value = asRecord[i];
									}
								}
								else
								{
									for (int j = 0; j < this.columns.Length; j++)
									{
										Value value2 = asRecord[this.columns[j]];
									}
								}
								if (!this.selectErrors)
								{
									this.current = valueReference;
									return true;
								}
							}
							catch (ValueException)
							{
								if (this.selectErrors)
								{
									this.current = valueReference;
									return true;
								}
							}
						}
						this.current = null;
						return false;
					}

					// Token: 0x06001E38 RID: 7736 RVA: 0x0000EE09 File Offset: 0x0000D009
					public void Reset()
					{
						throw new InvalidOperationException();
					}

					// Token: 0x04000AF9 RID: 2809
					private readonly IEnumerator<IValueReference> inner;

					// Token: 0x04000AFA RID: 2810
					private readonly int[] columns;

					// Token: 0x04000AFB RID: 2811
					private readonly bool selectErrors;

					// Token: 0x04000AFC RID: 2812
					private IValueReference current;
				}
			}

			// Token: 0x02000336 RID: 822
			private sealed class ForceColumnsFunctionValue : NativeFunctionValue2
			{
				// Token: 0x06001E3A RID: 7738 RVA: 0x0000A6A5 File Offset: 0x000088A5
				public override Value Invoke(Value table, Value linksSelector)
				{
					return table;
				}
			}

			// Token: 0x02000337 RID: 823
			public class AggregateTableColumnFunctionValue : NativeFunctionValue3<TableValue, TableValue, TextValue, ListValue>
			{
				// Token: 0x06001E3B RID: 7739 RVA: 0x0004CC94 File Offset: 0x0004AE94
				public AggregateTableColumnFunctionValue()
					: base(TypeValue.Table, 3, "table", TypeValue.Table, "column", TypeValue.Text, "aggregations", TypeValue.List)
				{
				}

				// Token: 0x06001E3C RID: 7740 RVA: 0x0004CCCC File Offset: 0x0004AECC
				public override TableValue TypedInvoke(TableValue table, TextValue column, ListValue aggregations)
				{
					Keys columns = table.Columns;
					int num;
					if (!table.Columns.TryGetKeyIndex(column.AsString, out num))
					{
						throw ValueException.TableColumnNotFound(column.AsString);
					}
					int[] array;
					bool flag;
					table = GroupHelpers.EnsureTableKey(table, out array, out flag);
					KeysBuilder keysBuilder = default(KeysBuilder);
					for (int i = 0; i < aggregations.Count; i++)
					{
						keysBuilder.Union(aggregations[i].AsList.Item0.AsString);
					}
					Keys keys = keysBuilder.ToKeys();
					table = table.SelectColumns(new ColumnSelection(JoinQuery.EnsureUniqueKeys(table.Columns, keys)));
					Compiler compiler = new Compiler(CompileOptions.None);
					ListValue[] array2 = new ListValue[aggregations.Count];
					for (int j = 0; j < aggregations.Count; j++)
					{
						ListValue asList = aggregations[j].AsList;
						TypeValue typeValue = asList.Item1.Type.AsFunctionType.ReturnType;
						IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(asList.Item1), new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), asList.Item0.AsString)));
						Value value = compiler.ToFunction(functionExpression);
						if (asList.Count > 3)
						{
							typeValue = asList.Item3.AsType;
						}
						array2[j] = ListValue.New(new Value[] { asList.Item2, value, typeValue });
					}
					Value[] array3 = array2;
					ListValue listValue = ListValue.New(array3);
					table = table.ExpandListColumn(num, false).ExpandRecordColumn(num, keys, keys);
					array = JoinQuery.AdjustColumns(array, num, keys.Length - 1);
					ColumnSelection columnSelection = new ColumnSelection(table.Columns).Remove(num, keys.Length);
					table = GroupHelpers.GroupWithPassthrough(table, array, columnSelection, listValue);
					ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
					for (int k = 0; k < columns.Length; k++)
					{
						string text = columns[k];
						if (k == num)
						{
							for (int l = 0; l < aggregations.Count; l++)
							{
								string asString = aggregations[l].AsList.Item2.AsString;
								columnSelectionBuilder.Add(asString, columnSelection.Keys.Length + l);
							}
						}
						else
						{
							int num2 = ((k > num) ? (-1) : 0);
							columnSelectionBuilder.Add(text, k + num2);
						}
					}
					return table.SelectColumns(columnSelectionBuilder.ToColumnSelection());
				}
			}

			// Token: 0x02000338 RID: 824
			private sealed class ColumnNamesFunctionValue : NativeFunctionValue1<ListValue, TableValue>
			{
				// Token: 0x06001E3D RID: 7741 RVA: 0x00049A90 File Offset: 0x00047C90
				public ColumnNamesFunctionValue()
					: base(TypeValue.List, "table", TypeValue.Table)
				{
				}

				// Token: 0x06001E3E RID: 7742 RVA: 0x0004CF3C File Offset: 0x0004B13C
				public override ListValue TypedInvoke(TableValue table)
				{
					RecordTypeValue itemType = table.Type.AsTableType.ItemType;
					return Library.Record.FieldNames.Invoke(itemType.Fields).AsList;
				}
			}

			// Token: 0x02000339 RID: 825
			private sealed class FromColumnsFunctionValue : NativeFunctionValue2<TableValue, ListValue, Value>
			{
				// Token: 0x06001E3F RID: 7743 RVA: 0x0004CF6F File Offset: 0x0004B16F
				public FromColumnsFunctionValue()
					: base(TypeValue.Table, 1, "lists", TypeValue.List, "columns", TypeValue.Any)
				{
				}

				// Token: 0x06001E40 RID: 7744 RVA: 0x0004CF94 File Offset: 0x0004B194
				public override TableValue TypedInvoke(ListValue lists, Value columns)
				{
					Value[] array = lists.ToArray();
					TableTypeValue tableTypeValue = TableTypeValue.FromValue(columns.IsNull ? NumberValue.New(array.Length) : columns, null);
					if (tableTypeValue.ItemType.Fields.Keys.Length != array.Length)
					{
						throw ValueException.NewExpressionError<Message2>(Strings.Table_FromColumns_ColumnsAndColumnNamesCountMismatch(lists.Count, tableTypeValue.ItemType.Fields.Keys.Length), columns, null);
					}
					if (!columns.IsType)
					{
						Value[] array2 = new Value[tableTypeValue.ItemType.Fields.Keys.Length];
						for (int i = 0; i < array2.Length; i++)
						{
							Value asList = array[i].AsList;
							array2[i] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
							{
								asList.Type.AsListType.ItemType.Nullable,
								LogicalValue.False
							});
						}
						tableTypeValue = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(tableTypeValue.ItemType.Fields.Keys, array2.ToArray<Value>())));
					}
					return new TableModule.Table.FromColumnsFunctionValue.FromColumnsTableValue(array, tableTypeValue);
				}

				// Token: 0x0200033A RID: 826
				private class FromColumnsTableValue : TableValue
				{
					// Token: 0x06001E41 RID: 7745 RVA: 0x0004D0AD File Offset: 0x0004B2AD
					public FromColumnsTableValue(Value[] columns, TableTypeValue tableType)
					{
						this.columns = columns;
						this.type = tableType;
					}

					// Token: 0x17000D87 RID: 3463
					// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0004D0C3 File Offset: 0x0004B2C3
					public override TypeValue Type
					{
						get
						{
							return this.type;
						}
					}

					// Token: 0x06001E43 RID: 7747 RVA: 0x0004D0CC File Offset: 0x0004B2CC
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						IEnumerator<IValueReference>[] array = new IEnumerator<IValueReference>[this.columns.Length];
						IEnumerator<IValueReference> enumerator;
						try
						{
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = this.columns[i].AsList.GetEnumerator();
							}
							enumerator = new TableModule.Table.FromColumnsFunctionValue.FromColumnsTableValue.TableFromColumnsEnumerator(array, this.type.ItemType.Fields.Keys);
						}
						catch
						{
							TableModule.Table.FromColumnsFunctionValue.FromColumnsTableValue.Dispose(array);
							throw;
						}
						return enumerator;
					}

					// Token: 0x06001E44 RID: 7748 RVA: 0x0004D144 File Offset: 0x0004B344
					private static void Dispose(IEnumerator<IValueReference>[] columnEnumerators)
					{
						for (int i = 0; i < columnEnumerators.Length; i++)
						{
							IEnumerator<IValueReference> enumerator = columnEnumerators[i];
							if (enumerator != null)
							{
								try
								{
									enumerator.Dispose();
								}
								catch (RuntimeException)
								{
								}
								columnEnumerators[i] = null;
							}
						}
					}

					// Token: 0x04000AFD RID: 2813
					private readonly Value[] columns;

					// Token: 0x04000AFE RID: 2814
					private readonly TableTypeValue type;

					// Token: 0x0200033B RID: 827
					private class TableFromColumnsEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06001E45 RID: 7749 RVA: 0x0004D188 File Offset: 0x0004B388
						public TableFromColumnsEnumerator(IEnumerator<IValueReference>[] columns, Keys columnNames)
						{
							this.columns = columns;
							this.columnNames = columnNames;
						}

						// Token: 0x17000D88 RID: 3464
						// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0004D1A0 File Offset: 0x0004B3A0
						public IValueReference Current
						{
							get
							{
								if (this.current == null)
								{
									IValueReference[] array = new IValueReference[this.columns.Length];
									for (int i = 0; i < array.Length; i++)
									{
										IValueReference[] array2 = array;
										int num = i;
										IValueReference valueReference;
										if (this.columns[i] != null)
										{
											valueReference = this.columns[i].Current;
										}
										else
										{
											IValueReference @null = Value.Null;
											valueReference = @null;
										}
										array2[num] = valueReference;
									}
									this.current = RecordValue.New(this.columnNames, array);
								}
								return this.current;
							}
						}

						// Token: 0x17000D89 RID: 3465
						// (get) Token: 0x06001E47 RID: 7751 RVA: 0x0004D20C File Offset: 0x0004B40C
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06001E48 RID: 7752 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06001E49 RID: 7753 RVA: 0x0004D214 File Offset: 0x0004B414
						public void Dispose()
						{
							TableModule.Table.FromColumnsFunctionValue.FromColumnsTableValue.Dispose(this.columns);
						}

						// Token: 0x06001E4A RID: 7754 RVA: 0x0004D224 File Offset: 0x0004B424
						public bool MoveNext()
						{
							this.current = null;
							bool flag = false;
							for (int i = 0; i < this.columns.Length; i++)
							{
								if (this.columns[i] != null)
								{
									bool flag2 = this.columns[i].MoveNext();
									if (!flag2)
									{
										this.columns[i].Dispose();
										this.columns[i] = null;
									}
									flag = flag || flag2;
								}
							}
							return flag;
						}

						// Token: 0x04000AFF RID: 2815
						private readonly IEnumerator<IValueReference>[] columns;

						// Token: 0x04000B00 RID: 2816
						private readonly Keys columnNames;

						// Token: 0x04000B01 RID: 2817
						private IValueReference current;
					}
				}
			}

			// Token: 0x0200033C RID: 828
			private sealed class FromRowsFunctionValue : NativeFunctionValue2<TableValue, ListValue, Value>
			{
				// Token: 0x06001E4B RID: 7755 RVA: 0x0004D283 File Offset: 0x0004B483
				public FromRowsFunctionValue()
					: base(TypeValue.Table, 1, "rows", TypeValue.List, "columns", TypeValue.Any)
				{
				}

				// Token: 0x06001E4C RID: 7756 RVA: 0x0004D2A8 File Offset: 0x0004B4A8
				public override TableValue TypedInvoke(ListValue rows, Value columns)
				{
					if (columns.IsNull)
					{
						using (IEnumerator<IValueReference> enumerator = rows.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								columns = NumberValue.New(enumerator.Current.Value.AsList.Count);
							}
							else
							{
								columns = NumberValue.Zero;
							}
						}
					}
					TableTypeValue tableTypeValue = TableTypeValue.FromValue(columns, null);
					return LanguageLibrary.List.Transform.Invoke(rows, new TableModule.Table.FromRowsFunctionValue.CreateRowFunctionValue(tableTypeValue.ItemType.Fields.Keys)).AsList.ToTable(tableTypeValue);
				}

				// Token: 0x0200033D RID: 829
				private class CreateRowFunctionValue : NativeFunctionValue1<RecordValue, ListValue>
				{
					// Token: 0x06001E4D RID: 7757 RVA: 0x0004D340 File Offset: 0x0004B540
					public CreateRowFunctionValue(Keys keys)
						: base(TypeValue.Record, "row", TypeValue.List)
					{
						this.keys = keys;
					}

					// Token: 0x06001E4E RID: 7758 RVA: 0x0004D360 File Offset: 0x0004B560
					public override RecordValue TypedInvoke(ListValue row)
					{
						if (row.Count != this.keys.Length)
						{
							throw ValueException.NewExpressionError<Message2>(Strings.RecordValue_New_MismatchedKeysAndValues(this.keys.Length, row.Count), row, null);
						}
						return RecordValue.New(this.keys, (int index) => row[index]);
					}

					// Token: 0x04000B02 RID: 2818
					private readonly Keys keys;
				}
			}

			// Token: 0x0200033F RID: 831
			private sealed class FromPartitionsFunctionValue : NativeFunctionValue3<TableValue, TextValue, ListValue, Value>
			{
				// Token: 0x06001E51 RID: 7761 RVA: 0x0004D3EC File Offset: 0x0004B5EC
				public FromPartitionsFunctionValue()
					: base(TypeValue.Table, 2, "partitionColumn", TypeValue.Text, "partitions", TypeValue.List, "partitionColumnType", TypeValue._Type.Nullable)
				{
				}

				// Token: 0x06001E52 RID: 7762 RVA: 0x0004D428 File Offset: 0x0004B628
				public override TableValue TypedInvoke(TextValue partitionColumn, ListValue partitions, Value partitionColumnType)
				{
					return TableValue.FromPartitions(partitionColumn, partitions, partitionColumnType.IsNull ? TypeValue.Any : partitionColumnType.AsType);
				}
			}

			// Token: 0x02000340 RID: 832
			private sealed class PartitionValuesFunctionValue : NativeFunctionValue1<TableValue, TableValue>
			{
				// Token: 0x06001E53 RID: 7763 RVA: 0x0004D446 File Offset: 0x0004B646
				public PartitionValuesFunctionValue()
					: base(TypeValue.Table, 1, "table", TypeValue.Table)
				{
				}

				// Token: 0x06001E54 RID: 7764 RVA: 0x0004D45E File Offset: 0x0004B65E
				public override TableValue TypedInvoke(TableValue tableValue)
				{
					return tableValue.GetPartitionTable();
				}
			}

			// Token: 0x02000341 RID: 833
			private sealed class ReplaceRelationshipIdentityFunctionValue : NativeFunctionValue2<Value, Value, TextValue>
			{
				// Token: 0x06001E55 RID: 7765 RVA: 0x0004D466 File Offset: 0x0004B666
				public ReplaceRelationshipIdentityFunctionValue()
					: base(TypeValue.Any, "value", TypeValue.Any, "identity", TypeValue.Text)
				{
				}

				// Token: 0x06001E56 RID: 7766 RVA: 0x0004D487 File Offset: 0x0004B687
				public override Value TypedInvoke(Value value, TextValue identity)
				{
					if (value.IsTable)
					{
						value = value.AsTable.ReplaceRelationshipIdentity(identity.AsString);
					}
					return value;
				}
			}

			// Token: 0x02000342 RID: 834
			public sealed class FilterWithDataTableFunctionValue : NativeFunctionValue2<TableValue, TableValue, TextValue>
			{
				// Token: 0x06001E57 RID: 7767 RVA: 0x0004D4A5 File Offset: 0x0004B6A5
				public FilterWithDataTableFunctionValue(IEngineHost engineHost)
					: base(TypeValue.Any, "table", TypeValue.Table, "dataTableIdentifier", TypeValue.Text)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x06001E58 RID: 7768 RVA: 0x0004D4D0 File Offset: 0x0004B6D0
				public override TableValue TypedInvoke(TableValue table, TextValue dataTableIdentifier)
				{
					object obj;
					if (this.engineHost.QueryService<IVariableService>().TryGetValue(dataTableIdentifier.String, out obj))
					{
						DataTable dataTable = obj as DataTable;
						if (dataTable != null)
						{
							return (TableValue)((IEngine)Engine.Instance).FilterTable(table, dataTable).AsTable;
						}
					}
					throw ValueException.NewExpressionError<Message1>(Strings.VariableNotFound(dataTableIdentifier.String), null, null);
				}

				// Token: 0x04000B04 RID: 2820
				private readonly IEngineHost engineHost;
			}

			// Token: 0x02000343 RID: 835
			private sealed class SplitFunctionValue : NativeFunctionValue2<ListValue, TableValue, NumberValue>
			{
				// Token: 0x06001E59 RID: 7769 RVA: 0x0004D52A File Offset: 0x0004B72A
				public SplitFunctionValue()
					: base(TypeValue.List, "table", TypeValue.Table, "pageSize", TypeValue.Int32)
				{
				}

				// Token: 0x06001E5A RID: 7770 RVA: 0x0004D54C File Offset: 0x0004B74C
				public override ListValue TypedInvoke(TableValue table, NumberValue pageSize)
				{
					int asInteger = pageSize.AsInteger32;
					if (asInteger < 1)
					{
						throw ValueException.ArgumentOutOfRange("pageSize", pageSize);
					}
					return new PagedListValue(new Func<PagedListValue.GetCurrentEnumerator, RowCount, RowCount, Value>(new TableModule.Table.SplitFunctionValue.PageSource(table).GetPage), table, new RowCount((long)asInteger));
				}

				// Token: 0x02000344 RID: 836
				private sealed class PageSource
				{
					// Token: 0x06001E5B RID: 7771 RVA: 0x0004D58E File Offset: 0x0004B78E
					public PageSource(TableValue table)
					{
						this.table = table;
					}

					// Token: 0x06001E5C RID: 7772 RVA: 0x0004D59D File Offset: 0x0004B79D
					public Value GetPage(PagedListValue.GetCurrentEnumerator getCurrentEnumerator, RowCount offset, RowCount pageSize)
					{
						return new TableModule.Table.SplitFunctionValue.PageSource.Page(this.table, getCurrentEnumerator, offset, pageSize);
					}

					// Token: 0x04000B05 RID: 2821
					private readonly TableValue table;

					// Token: 0x02000345 RID: 837
					private sealed class Page : TableValue
					{
						// Token: 0x06001E5D RID: 7773 RVA: 0x0004D5AD File Offset: 0x0004B7AD
						public Page(TableValue table, PagedListValue.GetCurrentEnumerator getCurrentEnumerator, RowCount offset, RowCount pageSize)
						{
							this.table = table;
							this.getCurrentEnumerator = getCurrentEnumerator;
							this.offset = offset;
							this.pageSize = pageSize;
						}

						// Token: 0x17000D8A RID: 3466
						// (get) Token: 0x06001E5E RID: 7774 RVA: 0x0004D5D2 File Offset: 0x0004B7D2
						public override TypeValue Type
						{
							get
							{
								return this.table.Type;
							}
						}

						// Token: 0x06001E5F RID: 7775 RVA: 0x0004D5E0 File Offset: 0x0004B7E0
						public override IEnumerator<IValueReference> GetEnumerator()
						{
							IEnumerator<IValueReference> enumerator = this.getCurrentEnumerator();
							if (enumerator != null)
							{
								return enumerator;
							}
							return this.table.Skip(this.offset).Take(this.pageSize).GetEnumerator();
						}

						// Token: 0x04000B06 RID: 2822
						private readonly TableValue table;

						// Token: 0x04000B07 RID: 2823
						private readonly PagedListValue.GetCurrentEnumerator getCurrentEnumerator;

						// Token: 0x04000B08 RID: 2824
						private readonly RowCount offset;

						// Token: 0x04000B09 RID: 2825
						private readonly RowCount pageSize;
					}
				}
			}

			// Token: 0x02000346 RID: 838
			public class SplitAtFunctionValue : NativeFunctionValue2<ListValue, TableValue, NumberValue>
			{
				// Token: 0x06001E60 RID: 7776 RVA: 0x0004D61F File Offset: 0x0004B81F
				public SplitAtFunctionValue(IEngineHost engineHost)
					: base(TypeValue.List, "table", TypeValue.Table, "count", TypeValue.Int64)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x06001E61 RID: 7777 RVA: 0x0004D648 File Offset: 0x0004B848
				public override ListValue TypedInvoke(TableValue table, NumberValue count)
				{
					TableModule.Table.SplitAtFunctionValue.SharedEnumerator<IValueReference> sharedEnumerator = new TableModule.Table.SplitAtFunctionValue.SharedEnumerator<IValueReference>(this.engineHost, new Func<IEnumerator<IValueReference>>(table.GetEnumerator));
					return ListValue.New(new Value[]
					{
						new TableModule.Table.SplitAtFunctionValue.EnumeratorTableValue(table.Type.AsTableType, () => sharedEnumerator.GetEnumerator(0, (int)count.AsInteger64)),
						new TableModule.Table.SplitAtFunctionValue.EnumeratorTableValue(table.Type.AsTableType, () => sharedEnumerator.GetEnumerator((int)count.AsInteger64, int.MaxValue))
					});
				}

				// Token: 0x04000B0A RID: 2826
				private readonly IEngineHost engineHost;

				// Token: 0x02000347 RID: 839
				private class EnumeratorTableValue : TableValue
				{
					// Token: 0x06001E62 RID: 7778 RVA: 0x0004D6C9 File Offset: 0x0004B8C9
					public EnumeratorTableValue(TableTypeValue type, Func<IEnumerator<IValueReference>> getEnumerator)
					{
						this.type = type;
						this.getEnumerator = getEnumerator;
					}

					// Token: 0x17000D8B RID: 3467
					// (get) Token: 0x06001E63 RID: 7779 RVA: 0x0004D6DF File Offset: 0x0004B8DF
					public override TypeValue Type
					{
						get
						{
							return this.type;
						}
					}

					// Token: 0x06001E64 RID: 7780 RVA: 0x0004D6E7 File Offset: 0x0004B8E7
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return this.getEnumerator();
					}

					// Token: 0x04000B0B RID: 2827
					private readonly TableTypeValue type;

					// Token: 0x04000B0C RID: 2828
					private readonly Func<IEnumerator<IValueReference>> getEnumerator;
				}

				// Token: 0x02000348 RID: 840
				private class SharedEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
				{
					// Token: 0x06001E65 RID: 7781 RVA: 0x0004D6F4 File Offset: 0x0004B8F4
					public SharedEnumerator(IEngineHost engineHost, Func<IEnumerator<T>> getEnumerator)
					{
						this.lifetimeService = engineHost.QueryService<ILifetimeService>();
						ILifetimeService lifetimeService = this.lifetimeService;
						if (lifetimeService != null)
						{
							lifetimeService.Register(this);
						}
						this.getEnumerator = getEnumerator;
					}

					// Token: 0x17000D8C RID: 3468
					// (get) Token: 0x06001E66 RID: 7782 RVA: 0x0004D721 File Offset: 0x0004B921
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x17000D8D RID: 3469
					// (get) Token: 0x06001E67 RID: 7783 RVA: 0x0004D72E File Offset: 0x0004B92E
					public T Current
					{
						get
						{
							return this.enumerator.Current;
						}
					}

					// Token: 0x06001E68 RID: 7784 RVA: 0x000033E7 File Offset: 0x000015E7
					public void Reset()
					{
						throw new NotSupportedException();
					}

					// Token: 0x06001E69 RID: 7785 RVA: 0x0004D73B File Offset: 0x0004B93B
					public bool MoveNext()
					{
						if (this.enumerator.MoveNext())
						{
							this.position++;
							return true;
						}
						return false;
					}

					// Token: 0x06001E6A RID: 7786 RVA: 0x0004D75C File Offset: 0x0004B95C
					public void Dispose()
					{
						if (this.subEnumerator != null)
						{
							throw new InvalidOperationException();
						}
						if (this.enumerator != null)
						{
							this.enumerator.Dispose();
							this.enumerator = null;
							this.position = -1;
						}
						ILifetimeService lifetimeService = this.lifetimeService;
						if (lifetimeService == null)
						{
							return;
						}
						lifetimeService.Unregister(this);
					}

					// Token: 0x06001E6B RID: 7787 RVA: 0x0004D7AC File Offset: 0x0004B9AC
					public IEnumerator<T> GetEnumerator(int position, int count)
					{
						if (this.position == -1)
						{
							throw new InvalidOperationException();
						}
						if (this.subEnumerator == null && this.position <= position)
						{
							if (this.enumerator == null)
							{
								this.enumerator = this.getEnumerator();
							}
							for (int i = this.position; i < position; i++)
							{
								this.MoveNext();
							}
							this.subEnumerator = new TableModule.Table.SplitAtFunctionValue.SharedEnumerator<T>.SubEnumerator(this, count);
							return this.subEnumerator;
						}
						IEnumerator<T> enumerator;
						if (count != 0)
						{
							enumerator = this.getEnumerator();
						}
						else
						{
							IEnumerator<T> enumerator2 = new List<T>().GetEnumerator();
							enumerator = enumerator2;
						}
						IEnumerator<T> enumerator3 = enumerator;
						for (int j = 0; j < position; j++)
						{
							enumerator3.MoveNext();
						}
						return new TableModule.Table.SplitAtFunctionValue.SharedEnumerator<T>.LimitedEnumerator(enumerator3, count);
					}

					// Token: 0x06001E6C RID: 7788 RVA: 0x0004D85A File Offset: 0x0004BA5A
					private void Dispose(TableModule.Table.SplitAtFunctionValue.SharedEnumerator<T>.SubEnumerator subEnumerator)
					{
						if (this.subEnumerator != subEnumerator)
						{
							throw new InvalidOperationException();
						}
						this.subEnumerator = null;
					}

					// Token: 0x04000B0D RID: 2829
					private const int disposed = -1;

					// Token: 0x04000B0E RID: 2830
					private readonly ILifetimeService lifetimeService;

					// Token: 0x04000B0F RID: 2831
					private readonly Func<IEnumerator<T>> getEnumerator;

					// Token: 0x04000B10 RID: 2832
					private IEnumerator<T> enumerator;

					// Token: 0x04000B11 RID: 2833
					private IEnumerator<T> subEnumerator;

					// Token: 0x04000B12 RID: 2834
					private int position;

					// Token: 0x02000349 RID: 841
					private class SubEnumerator : TableModule.Table.SplitAtFunctionValue.SharedEnumerator<T>.LimitedEnumerator
					{
						// Token: 0x06001E6D RID: 7789 RVA: 0x0004D872 File Offset: 0x0004BA72
						public SubEnumerator(TableModule.Table.SplitAtFunctionValue.SharedEnumerator<T> sharedEnumerator, int count)
							: base(sharedEnumerator, count)
						{
						}

						// Token: 0x06001E6E RID: 7790 RVA: 0x0004D87C File Offset: 0x0004BA7C
						public override void Dispose()
						{
							TableModule.Table.SplitAtFunctionValue.SharedEnumerator<T> sharedEnumerator = (TableModule.Table.SplitAtFunctionValue.SharedEnumerator<T>)this.enumerator;
							if (sharedEnumerator != null)
							{
								sharedEnumerator.Dispose(this);
								this.enumerator = null;
							}
							base.Dispose();
						}
					}

					// Token: 0x0200034A RID: 842
					private class LimitedEnumerator : IEnumerator<T>, IDisposable, IEnumerator
					{
						// Token: 0x06001E6F RID: 7791 RVA: 0x0004D8AC File Offset: 0x0004BAAC
						public LimitedEnumerator(IEnumerator<T> enumerator, int count)
						{
							this.enumerator = enumerator;
							this.count = count;
						}

						// Token: 0x17000D8E RID: 3470
						// (get) Token: 0x06001E70 RID: 7792 RVA: 0x0004D8C2 File Offset: 0x0004BAC2
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x17000D8F RID: 3471
						// (get) Token: 0x06001E71 RID: 7793 RVA: 0x0004D8CF File Offset: 0x0004BACF
						public T Current
						{
							get
							{
								return this.enumerator.Current;
							}
						}

						// Token: 0x06001E72 RID: 7794 RVA: 0x000033E7 File Offset: 0x000015E7
						public void Reset()
						{
							throw new NotSupportedException();
						}

						// Token: 0x06001E73 RID: 7795 RVA: 0x0004D8DC File Offset: 0x0004BADC
						public bool MoveNext()
						{
							if (this.enumerator != null && this.count > 0 && this.enumerator.MoveNext())
							{
								this.count--;
								return true;
							}
							return false;
						}

						// Token: 0x06001E74 RID: 7796 RVA: 0x0004D90D File Offset: 0x0004BB0D
						public virtual void Dispose()
						{
							this.count = -1;
							if (this.enumerator != null)
							{
								this.enumerator.Dispose();
								this.enumerator = null;
							}
						}

						// Token: 0x04000B13 RID: 2835
						protected IEnumerator<T> enumerator;

						// Token: 0x04000B14 RID: 2836
						private int count;
					}
				}
			}

			// Token: 0x0200034C RID: 844
			private sealed class BufferFunctionValue : NativeFunctionValue2<TableValue, TableValue, Value>
			{
				// Token: 0x06001E78 RID: 7800 RVA: 0x0004D968 File Offset: 0x0004BB68
				public BufferFunctionValue()
					: base(TypeValue.Table, 1, "table", TypeValue.Table, "options", TableModule.Table.BufferFunctionValue.optionType.Nullable)
				{
				}

				// Token: 0x06001E79 RID: 7801 RVA: 0x0004D990 File Offset: 0x0004BB90
				public override TableValue TypedInvoke(TableValue table, Value options)
				{
					Value value;
					Library.BufferMode bufferMode;
					if (TableModule.Table.BufferFunctionValue.optionRecord.CreateOptions("Table.Buffer", options).TryGetValue("BufferMode", out value))
					{
						bufferMode = Library.BufferModeEnum.Type.GetValue(value.AsNumber);
					}
					else
					{
						bufferMode = Library.BufferMode.Eager;
					}
					return table.Buffer(bufferMode);
				}

				// Token: 0x04000B17 RID: 2839
				private static readonly OptionRecordDefinition optionRecord = new OptionRecordDefinition(new OptionItem[]
				{
					new OptionItem("BufferMode", Library.BufferModeEnum.Type.Nullable, Value.Null, OptionItemOption.None, null, null)
				});

				// Token: 0x04000B18 RID: 2840
				private static readonly TypeValue optionType = TableModule.Table.BufferFunctionValue.optionRecord.CreateRecordType();
			}

			// Token: 0x0200034D RID: 845
			private sealed class StopFoldingFunctionValue : NativeFunctionValue1<TableValue, TableValue>
			{
				// Token: 0x06001E7B RID: 7803 RVA: 0x0004DA23 File Offset: 0x0004BC23
				public StopFoldingFunctionValue()
					: base(TypeValue.Table, "table", TypeValue.Table)
				{
				}

				// Token: 0x06001E7C RID: 7804 RVA: 0x0004DA3A File Offset: 0x0004BC3A
				public override TableValue TypedInvoke(TableValue table)
				{
					return new TableModule.Table.StopFoldingFunctionValue.StopFoldingTableValue(table);
				}

				// Token: 0x0200034E RID: 846
				private sealed class StopFoldingTableValue : OptimizableTableValue
				{
					// Token: 0x06001E7D RID: 7805 RVA: 0x0004DA42 File Offset: 0x0004BC42
					public StopFoldingTableValue(TableValue table)
					{
						this.table = table;
					}

					// Token: 0x17000D90 RID: 3472
					// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0004DA51 File Offset: 0x0004BC51
					public override TypeValue Type
					{
						get
						{
							return this.table.Type;
						}
					}

					// Token: 0x17000D91 RID: 3473
					// (get) Token: 0x06001E7F RID: 7807 RVA: 0x0004DA5E File Offset: 0x0004BC5E
					public override TableSortOrder SortOrder
					{
						get
						{
							return this.table.SortOrder;
						}
					}

					// Token: 0x17000D92 RID: 3474
					// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0004DA6B File Offset: 0x0004BC6B
					public override IExpression Expression
					{
						get
						{
							if (this.table is IOptimizedValue)
							{
								return new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(TableModule.Table.StopFolding), this.table.Expression);
							}
							return base.Expression;
						}
					}

					// Token: 0x06001E81 RID: 7809 RVA: 0x0004DA9B File Offset: 0x0004BC9B
					public override TableValue Optimize()
					{
						return new OptimizedTableValue(new TableModule.Table.StopFoldingFunctionValue.StopFoldingTableValue(this.table.Optimize()));
					}

					// Token: 0x06001E82 RID: 7810 RVA: 0x0004DAB2 File Offset: 0x0004BCB2
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return this.table.GetEnumerator();
					}

					// Token: 0x04000B19 RID: 2841
					private readonly TableValue table;
				}
			}

			// Token: 0x0200034F RID: 847
			private static class ErrorContext
			{
				// Token: 0x06001E83 RID: 7811 RVA: 0x0004DAC0 File Offset: 0x0004BCC0
				public static void SetErrorContext(RuntimeException exception, string context)
				{
					if (!exception.Data.Contains("Microsoft.Data.Mashup.Error.Context"))
					{
						ValueException ex = exception as ValueException;
						Value value;
						Value value2;
						if (ex != null && ex.Value.TryGetValue(ErrorRecord.DetailKey, out value) && value.IsRecord && value.TryGetMetaField("Microsoft.Data.Mashup.Error.Context", out value2) && value2.IsText)
						{
							context = value2.AsString;
						}
						exception.Data.Add("Microsoft.Data.Mashup.Error.Context", context);
					}
				}
			}

			// Token: 0x02000350 RID: 848
			private sealed class Function_InvokeWithErrorContextFunctionValue : NativeFunctionValue2<Value, FunctionValue, TextValue>
			{
				// Token: 0x06001E84 RID: 7812 RVA: 0x0004DB35 File Offset: 0x0004BD35
				public Function_InvokeWithErrorContextFunctionValue()
					: base(TypeValue.Any, "function", TypeValue.Function, "context", TypeValue.Text)
				{
				}

				// Token: 0x06001E85 RID: 7813 RVA: 0x0004DB58 File Offset: 0x0004BD58
				public override Value TypedInvoke(FunctionValue function, TextValue context)
				{
					Value value;
					try
					{
						value = function.Invoke();
					}
					catch (RuntimeException ex)
					{
						TableModule.Table.ErrorContext.SetErrorContext(ex, context.AsString);
						throw;
					}
					return value;
				}
			}

			// Token: 0x02000351 RID: 849
			private sealed class Action_WithErrorContextFunctionValue : NativeFunctionValue2<ActionValue, ActionValue, TextValue>
			{
				// Token: 0x06001E86 RID: 7814 RVA: 0x0004DB8C File Offset: 0x0004BD8C
				public Action_WithErrorContextFunctionValue()
					: base(TypeValue.Action, "action", TypeValue.Action, "context", TypeValue.Text)
				{
				}

				// Token: 0x06001E87 RID: 7815 RVA: 0x0004DBB0 File Offset: 0x0004BDB0
				public override ActionValue TypedInvoke(ActionValue action, TextValue context)
				{
					string contextString = context.AsString;
					return new TableModule.Table.Action_WithErrorContextFunctionValue.WithErrorContextActionValue(action, delegate(RuntimeException exception)
					{
						TableModule.Table.ErrorContext.SetErrorContext(exception, contextString);
					});
				}

				// Token: 0x02000352 RID: 850
				private sealed class WithErrorContextActionValue : ActionValue
				{
					// Token: 0x06001E88 RID: 7816 RVA: 0x0004DBE1 File Offset: 0x0004BDE1
					public WithErrorContextActionValue(ActionValue action, Action<RuntimeException> handler)
					{
						this.action = action;
						this.handler = handler;
					}

					// Token: 0x06001E89 RID: 7817 RVA: 0x0004DBF7 File Offset: 0x0004BDF7
					private ActionValue Wrap(ActionValue action)
					{
						return new TableModule.Table.Action_WithErrorContextFunctionValue.WithErrorContextActionValue(action, this.handler);
					}

					// Token: 0x17000D93 RID: 3475
					// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0004DC05 File Offset: 0x0004BE05
					public override TypeValue Type
					{
						get
						{
							return this.action.Type;
						}
					}

					// Token: 0x17000D94 RID: 3476
					// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0004DC12 File Offset: 0x0004BE12
					public override RecordValue MetaValue
					{
						get
						{
							return this.action.MetaValue;
						}
					}

					// Token: 0x17000D95 RID: 3477
					// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0004DC1F File Offset: 0x0004BE1F
					public override IExpression Expression
					{
						get
						{
							return this.action.Expression;
						}
					}

					// Token: 0x17000D96 RID: 3478
					// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0004DC2C File Offset: 0x0004BE2C
					public override object ActionIdentity
					{
						get
						{
							return this.action.ActionIdentity;
						}
					}

					// Token: 0x17000D97 RID: 3479
					// (get) Token: 0x06001E8E RID: 7822 RVA: 0x0004DC39 File Offset: 0x0004BE39
					public override bool CanBind
					{
						get
						{
							return this.action.CanBind;
						}
					}

					// Token: 0x06001E8F RID: 7823 RVA: 0x0004DC48 File Offset: 0x0004BE48
					public override bool TryBind(FunctionValue binding, out ActionValue action)
					{
						try
						{
							if (!this.action.TryBind(binding, out action))
							{
								action = null;
								return false;
							}
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
						action = this.Wrap(action);
						return true;
					}

					// Token: 0x06001E90 RID: 7824 RVA: 0x0004DC9C File Offset: 0x0004BE9C
					public override ActionValue ExecuteBindings()
					{
						ActionValue actionValue;
						try
						{
							actionValue = this.action.ExecuteBindings();
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
						return this.Wrap(actionValue);
					}

					// Token: 0x06001E91 RID: 7825 RVA: 0x0004DCE0 File Offset: 0x0004BEE0
					public override Value Execute()
					{
						Value value;
						try
						{
							value = this.action.Execute();
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
						return value;
					}

					// Token: 0x06001E92 RID: 7826 RVA: 0x0004DD1C File Offset: 0x0004BF1C
					public override ActionValue Optimize()
					{
						ActionValue actionValue;
						try
						{
							actionValue = this.action.Optimize();
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
						return this.Wrap(actionValue);
					}

					// Token: 0x04000B1A RID: 2842
					private readonly ActionValue action;

					// Token: 0x04000B1B RID: 2843
					private readonly Action<RuntimeException> handler;
				}
			}

			// Token: 0x02000354 RID: 852
			private sealed class Table_WithErrorContextFunctionValue : NativeFunctionValue2<Value, Value, TextValue>
			{
				// Token: 0x06001E95 RID: 7829 RVA: 0x0004DD6E File Offset: 0x0004BF6E
				public Table_WithErrorContextFunctionValue()
					: base(TypeValue.Any, "value", TypeValue.Any, "context", TypeValue.Text)
				{
				}

				// Token: 0x06001E96 RID: 7830 RVA: 0x0004DD90 File Offset: 0x0004BF90
				public override Value TypedInvoke(Value value, TextValue context)
				{
					string contextString = context.AsString;
					return TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(value, delegate(RuntimeException exception)
					{
						TableModule.Table.ErrorContext.SetErrorContext(exception, contextString);
					});
				}

				// Token: 0x06001E97 RID: 7831 RVA: 0x0004DDC4 File Offset: 0x0004BFC4
				private static Value Wrap(Value value, Action<RuntimeException> handler)
				{
					switch (value.Kind)
					{
					case ValueKind.List:
						return new TableModule.Table.Table_WithErrorContextFunctionValue.WithErrorContextListValue(value.AsList, handler);
					case ValueKind.Record:
						return new TableModule.Table.Table_WithErrorContextFunctionValue.WithErrorContextRecordValue(value.AsRecord, handler);
					case ValueKind.Table:
						return new TableModule.Table.Table_WithErrorContextFunctionValue.WithErrorContextTableValue(value.AsTable, handler);
					default:
						return value;
					}
				}

				// Token: 0x06001E98 RID: 7832 RVA: 0x0004DE17 File Offset: 0x0004C017
				private static IValueReference Wrap(IValueReference valueReference, Action<RuntimeException> handler)
				{
					return new TableModule.Table.Table_WithErrorContextFunctionValue.WithErrorContextValueReference(valueReference, handler);
				}

				// Token: 0x02000355 RID: 853
				private sealed class WithErrorContextValueReference : IValueReference
				{
					// Token: 0x06001E99 RID: 7833 RVA: 0x0004DE20 File Offset: 0x0004C020
					public WithErrorContextValueReference(IValueReference current, Action<RuntimeException> handler)
					{
						this.current = current;
						this.handler = handler;
					}

					// Token: 0x17000D98 RID: 3480
					// (get) Token: 0x06001E9A RID: 7834 RVA: 0x0004DE36 File Offset: 0x0004C036
					public bool Evaluated
					{
						get
						{
							return this.handler == null;
						}
					}

					// Token: 0x17000D99 RID: 3481
					// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0004DE44 File Offset: 0x0004C044
					public Value Value
					{
						get
						{
							if (this.handler != null)
							{
								try
								{
									Value value = ((IValueReference)this.current).Value;
									this.current = TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(value, this.handler);
									this.handler = null;
								}
								catch (RuntimeException ex)
								{
									this.handler(ex);
									throw;
								}
							}
							return (Value)this.current;
						}
					}

					// Token: 0x04000B1D RID: 2845
					private Action<RuntimeException> handler;

					// Token: 0x04000B1E RID: 2846
					private object current;
				}

				// Token: 0x02000356 RID: 854
				private sealed class WithErrorContextTableValue : TableValue
				{
					// Token: 0x06001E9C RID: 7836 RVA: 0x0004DEB0 File Offset: 0x0004C0B0
					public WithErrorContextTableValue(TableValue table, Action<RuntimeException> handler)
					{
						this.table = table;
						this.handler = handler;
					}

					// Token: 0x17000D9A RID: 3482
					// (get) Token: 0x06001E9D RID: 7837 RVA: 0x0004DEC8 File Offset: 0x0004C0C8
					public override TypeValue Type
					{
						get
						{
							TypeValue type;
							try
							{
								type = this.table.Type;
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return type;
						}
					}

					// Token: 0x06001E9E RID: 7838 RVA: 0x0004DF04 File Offset: 0x0004C104
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						IEnumerator<IValueReference> enumerator;
						try
						{
							enumerator = new TableModule.Table.Table_WithErrorContextFunctionValue.WithErrorContextTableValue.Enumerator(this.table.GetEnumerator(), this.handler);
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
						return enumerator;
					}

					// Token: 0x06001E9F RID: 7839 RVA: 0x0004DF4C File Offset: 0x0004C14C
					public override IPageReader GetReader()
					{
						IPageReader pageReader;
						try
						{
							pageReader = new TableModule.Table.Table_WithErrorContextFunctionValue.WithErrorContextTableValue.PageReader(this.table.GetReader(), this.handler);
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
						return pageReader;
					}

					// Token: 0x04000B1F RID: 2847
					private readonly TableValue table;

					// Token: 0x04000B20 RID: 2848
					private readonly Action<RuntimeException> handler;

					// Token: 0x02000357 RID: 855
					private class PageReader : DelegatingPageReader
					{
						// Token: 0x06001EA0 RID: 7840 RVA: 0x0004DF94 File Offset: 0x0004C194
						public PageReader(IPageReader reader, Action<RuntimeException> handler)
							: base(reader)
						{
							this.handler = handler;
						}

						// Token: 0x17000D9B RID: 3483
						// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x0004DFA4 File Offset: 0x0004C1A4
						public override TableSchema Schema
						{
							get
							{
								TableSchema schema;
								try
								{
									schema = base.Schema;
								}
								catch (RuntimeException ex)
								{
									this.handler(ex);
									throw;
								}
								return schema;
							}
						}

						// Token: 0x17000D9C RID: 3484
						// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0004DFDC File Offset: 0x0004C1DC
						public override int MaxPageRowCount
						{
							get
							{
								int maxPageRowCount;
								try
								{
									maxPageRowCount = base.MaxPageRowCount;
								}
								catch (RuntimeException ex)
								{
									this.handler(ex);
									throw;
								}
								return maxPageRowCount;
							}
						}

						// Token: 0x06001EA3 RID: 7843 RVA: 0x0004E014 File Offset: 0x0004C214
						public override IPage CreatePage()
						{
							IPage page;
							try
							{
								page = base.CreatePage();
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return new TableModule.Table.Table_WithErrorContextFunctionValue.WithErrorContextTableValue.PageReader.ErrorHandlingPage(page, this.handler);
						}

						// Token: 0x06001EA4 RID: 7844 RVA: 0x0004E058 File Offset: 0x0004C258
						public override void Read(IPage page)
						{
							TableModule.Table.Table_WithErrorContextFunctionValue.WithErrorContextTableValue.PageReader.ErrorHandlingPage errorHandlingPage = (TableModule.Table.Table_WithErrorContextFunctionValue.WithErrorContextTableValue.PageReader.ErrorHandlingPage)page;
							try
							{
								base.Read(errorHandlingPage.InnerPage);
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							errorHandlingPage.SetPageException(this.HandleSerializedException(errorHandlingPage.InnerPage.PageException));
							Dictionary<int, IExceptionRow> dictionary = new Dictionary<int, IExceptionRow>(errorHandlingPage.InnerPage.ExceptionRows.Count);
							foreach (KeyValuePair<int, IExceptionRow> keyValuePair in errorHandlingPage.InnerPage.ExceptionRows.OrderBy((KeyValuePair<int, IExceptionRow> er) => er.Key))
							{
								Dictionary<int, ISerializedException> dictionary2 = new Dictionary<int, ISerializedException>(keyValuePair.Value.Exceptions.Count);
								foreach (KeyValuePair<int, ISerializedException> keyValuePair2 in keyValuePair.Value.Exceptions.OrderBy((KeyValuePair<int, ISerializedException> e) => e.Key))
								{
									dictionary2[keyValuePair2.Key] = this.HandleSerializedException(keyValuePair2.Value);
								}
								dictionary.Add(keyValuePair.Key, new ExceptionRow(dictionary2));
							}
							errorHandlingPage.SetExceptionRows(dictionary);
						}

						// Token: 0x06001EA5 RID: 7845 RVA: 0x0004E1E0 File Offset: 0x0004C3E0
						public override IPageReader NextResult()
						{
							IPageReader pageReader2;
							try
							{
								IPageReader pageReader = base.NextResult();
								if (pageReader == null)
								{
									pageReader2 = null;
								}
								else
								{
									pageReader2 = new TableModule.Table.Table_WithErrorContextFunctionValue.WithErrorContextTableValue.PageReader(pageReader, this.handler);
								}
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return pageReader2;
						}

						// Token: 0x06001EA6 RID: 7846 RVA: 0x0004E22C File Offset: 0x0004C42C
						public override void Dispose()
						{
							try
							{
								base.Dispose();
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
						}

						// Token: 0x06001EA7 RID: 7847 RVA: 0x0004E260 File Offset: 0x0004C460
						private ISerializedException HandleSerializedException(ISerializedException serializedException)
						{
							if (serializedException != null)
							{
								RuntimeException ex = PageExceptionSerializer.GetExceptionFromProperties(serializedException) as RuntimeException;
								if (ex != null)
								{
									this.handler(ex);
									ISerializedException ex2;
									if (PageExceptionSerializer.TryGetPropertiesFromException(ex, out ex2))
									{
										serializedException = ex2;
									}
								}
							}
							return serializedException;
						}

						// Token: 0x04000B21 RID: 2849
						private readonly Action<RuntimeException> handler;

						// Token: 0x02000358 RID: 856
						private class ErrorHandlingPage : VirtualPage
						{
							// Token: 0x06001EA8 RID: 7848 RVA: 0x0004E299 File Offset: 0x0004C499
							public ErrorHandlingPage(IPage page, Action<RuntimeException> handler)
							{
								this.page = page;
								this.handler = handler;
							}

							// Token: 0x17000D9D RID: 3485
							// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x0004E2AF File Offset: 0x0004C4AF
							protected override IPage Page
							{
								get
								{
									return this.page;
								}
							}

							// Token: 0x17000D9E RID: 3486
							// (get) Token: 0x06001EAA RID: 7850 RVA: 0x0004E2AF File Offset: 0x0004C4AF
							public IPage InnerPage
							{
								get
								{
									return this.page;
								}
							}

							// Token: 0x17000D9F RID: 3487
							// (get) Token: 0x06001EAB RID: 7851 RVA: 0x0004E2B7 File Offset: 0x0004C4B7
							public override IDictionary<int, IExceptionRow> ExceptionRows
							{
								get
								{
									return this.exceptionRows;
								}
							}

							// Token: 0x17000DA0 RID: 3488
							// (get) Token: 0x06001EAC RID: 7852 RVA: 0x0004E2BF File Offset: 0x0004C4BF
							public override ISerializedException PageException
							{
								get
								{
									return this.pageException;
								}
							}

							// Token: 0x06001EAD RID: 7853 RVA: 0x0004E2C7 File Offset: 0x0004C4C7
							public void SetExceptionRows(IDictionary<int, IExceptionRow> exceptionRows)
							{
								this.exceptionRows = exceptionRows;
							}

							// Token: 0x06001EAE RID: 7854 RVA: 0x0004E2D0 File Offset: 0x0004C4D0
							public void SetPageException(ISerializedException pageException)
							{
								this.pageException = pageException;
							}

							// Token: 0x04000B22 RID: 2850
							private readonly IPage page;

							// Token: 0x04000B23 RID: 2851
							private readonly Action<RuntimeException> handler;

							// Token: 0x04000B24 RID: 2852
							private IDictionary<int, IExceptionRow> exceptionRows;

							// Token: 0x04000B25 RID: 2853
							private ISerializedException pageException;
						}
					}

					// Token: 0x0200035A RID: 858
					private class Enumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06001EB3 RID: 7859 RVA: 0x0004E2F7 File Offset: 0x0004C4F7
						public Enumerator(IEnumerator<IValueReference> enumerator, Action<RuntimeException> handler)
						{
							this.enumerator = enumerator;
							this.handler = handler;
						}

						// Token: 0x06001EB4 RID: 7860 RVA: 0x0004E30D File Offset: 0x0004C50D
						public void Reset()
						{
							this.enumerator.Reset();
						}

						// Token: 0x06001EB5 RID: 7861 RVA: 0x0004E31C File Offset: 0x0004C51C
						public bool MoveNext()
						{
							bool flag;
							try
							{
								flag = this.enumerator.MoveNext();
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return flag;
						}

						// Token: 0x17000DA1 RID: 3489
						// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x0004E358 File Offset: 0x0004C558
						public IValueReference Current
						{
							get
							{
								IValueReference valueReference;
								try
								{
									RecordValue asRecord = this.enumerator.Current.Value.AsRecord;
									int length = asRecord.Keys.Length;
									for (int i = 0; i < length; i++)
									{
										ValueKind kind = asRecord[i].Kind;
										if (kind - ValueKind.List <= 2)
										{
											return TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(asRecord, this.handler);
										}
									}
									valueReference = asRecord;
								}
								catch (RuntimeException ex)
								{
									this.handler(ex);
									throw;
								}
								return valueReference;
							}
						}

						// Token: 0x17000DA2 RID: 3490
						// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x0004E3E0 File Offset: 0x0004C5E0
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06001EB8 RID: 7864 RVA: 0x0004E3E8 File Offset: 0x0004C5E8
						public void Dispose()
						{
							try
							{
								if (this.enumerator != null)
								{
									this.enumerator.Dispose();
									this.enumerator = null;
									this.handler = null;
								}
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
						}

						// Token: 0x04000B29 RID: 2857
						private IEnumerator<IValueReference> enumerator;

						// Token: 0x04000B2A RID: 2858
						private Action<RuntimeException> handler;
					}
				}

				// Token: 0x0200035B RID: 859
				private sealed class WithErrorContextRecordValue : RecordValue
				{
					// Token: 0x06001EB9 RID: 7865 RVA: 0x0004E438 File Offset: 0x0004C638
					public WithErrorContextRecordValue(RecordValue record, Action<RuntimeException> handler)
					{
						this.record = record;
						this.handler = handler;
					}

					// Token: 0x17000DA3 RID: 3491
					// (get) Token: 0x06001EBA RID: 7866 RVA: 0x0004E450 File Offset: 0x0004C650
					public override bool IsEmpty
					{
						get
						{
							bool isEmpty;
							try
							{
								isEmpty = this.record.IsEmpty;
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return isEmpty;
						}
					}

					// Token: 0x17000DA4 RID: 3492
					// (get) Token: 0x06001EBB RID: 7867 RVA: 0x0004E48C File Offset: 0x0004C68C
					public override TypeValue Type
					{
						get
						{
							TypeValue type;
							try
							{
								type = this.record.Type;
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return type;
						}
					}

					// Token: 0x17000DA5 RID: 3493
					// (get) Token: 0x06001EBC RID: 7868 RVA: 0x0004E4C8 File Offset: 0x0004C6C8
					public override Keys Keys
					{
						get
						{
							Keys keys;
							try
							{
								keys = this.record.Keys;
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return keys;
						}
					}

					// Token: 0x06001EBD RID: 7869 RVA: 0x0004E504 File Offset: 0x0004C704
					public override void TestConnection()
					{
						try
						{
							this.record.TestConnection();
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
					}

					// Token: 0x17000DA6 RID: 3494
					public override Value this[Value key]
					{
						get
						{
							Value value;
							try
							{
								value = TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(this.record[key], this.handler);
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return value;
						}
					}

					// Token: 0x17000DA7 RID: 3495
					public override Value this[string field]
					{
						get
						{
							Value value;
							try
							{
								value = TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(this.record[field], this.handler);
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return value;
						}
					}

					// Token: 0x17000DA8 RID: 3496
					public override Value this[int index]
					{
						get
						{
							Value value;
							try
							{
								value = TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(this.record[index], this.handler);
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return value;
						}
					}

					// Token: 0x06001EC1 RID: 7873 RVA: 0x0004E618 File Offset: 0x0004C818
					public override bool TryGetValue(Value index, out Value value)
					{
						bool flag;
						try
						{
							if (this.record.TryGetValue(index, out value))
							{
								value = TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(value, this.handler);
								flag = true;
							}
							else
							{
								value = Value.Null;
								flag = false;
							}
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
						return flag;
					}

					// Token: 0x06001EC2 RID: 7874 RVA: 0x0004E674 File Offset: 0x0004C874
					public override bool TryGetValue(string key, out Value value)
					{
						bool flag;
						try
						{
							if (this.record.TryGetValue(key, out value))
							{
								value = TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(value, this.handler);
								flag = true;
							}
							else
							{
								value = Value.Null;
								flag = false;
							}
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
						return flag;
					}

					// Token: 0x06001EC3 RID: 7875 RVA: 0x0004E6D0 File Offset: 0x0004C8D0
					public override IValueReference GetReference(int index)
					{
						IValueReference valueReference;
						try
						{
							valueReference = TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(this.record.GetReference(index), this.handler);
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
						return valueReference;
					}

					// Token: 0x04000B2B RID: 2859
					private readonly RecordValue record;

					// Token: 0x04000B2C RID: 2860
					private readonly Action<RuntimeException> handler;
				}

				// Token: 0x0200035C RID: 860
				private sealed class WithErrorContextListValue : ListValue
				{
					// Token: 0x06001EC4 RID: 7876 RVA: 0x0004E718 File Offset: 0x0004C918
					public WithErrorContextListValue(ListValue list, Action<RuntimeException> handler)
					{
						this.list = list;
						this.handler = handler;
					}

					// Token: 0x17000DA9 RID: 3497
					// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x0004E730 File Offset: 0x0004C930
					public override TypeValue Type
					{
						get
						{
							TypeValue type;
							try
							{
								type = this.list.Type;
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return type;
						}
					}

					// Token: 0x17000DAA RID: 3498
					// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x0004E76C File Offset: 0x0004C96C
					public override long LargeCount
					{
						get
						{
							long largeCount;
							try
							{
								largeCount = this.list.LargeCount;
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return largeCount;
						}
					}

					// Token: 0x17000DAB RID: 3499
					// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x0004E7A8 File Offset: 0x0004C9A8
					public override int Count
					{
						get
						{
							int count;
							try
							{
								count = this.list.Count;
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return count;
						}
					}

					// Token: 0x17000DAC RID: 3500
					// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x0004E7E4 File Offset: 0x0004C9E4
					public override bool IsBuffered
					{
						get
						{
							bool isBuffered;
							try
							{
								isBuffered = this.list.IsBuffered;
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return isBuffered;
						}
					}

					// Token: 0x06001EC9 RID: 7881 RVA: 0x0004E820 File Offset: 0x0004CA20
					public override void TestConnection()
					{
						try
						{
							this.list.TestConnection();
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
					}

					// Token: 0x06001ECA RID: 7882 RVA: 0x0004E85C File Offset: 0x0004CA5C
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						IEnumerator<IValueReference> enumerator;
						try
						{
							enumerator = new TableModule.Table.Table_WithErrorContextFunctionValue.WithErrorContextListValue.Enumerator(this.list.GetEnumerator(), this.handler);
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
						return enumerator;
					}

					// Token: 0x17000DAD RID: 3501
					public override Value this[Value key]
					{
						get
						{
							Value value;
							try
							{
								value = TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(this.list[key], this.handler);
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return value;
						}
					}

					// Token: 0x17000DAE RID: 3502
					public override Value this[int index]
					{
						get
						{
							Value value;
							try
							{
								value = TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(this.list[index], this.handler);
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return value;
						}
					}

					// Token: 0x06001ECD RID: 7885 RVA: 0x0004E934 File Offset: 0x0004CB34
					public override IValueReference GetReference(int index)
					{
						IValueReference valueReference;
						try
						{
							valueReference = TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(this.list.GetReference(index), this.handler);
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
						return valueReference;
					}

					// Token: 0x06001ECE RID: 7886 RVA: 0x0004E97C File Offset: 0x0004CB7C
					public override bool TryGetValue(Value indexValue, out Value value)
					{
						bool flag;
						try
						{
							if (this.list.TryGetValue(indexValue, out value))
							{
								value = TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(value, this.handler);
								flag = true;
							}
							else
							{
								value = Value.Null;
								flag = false;
							}
						}
						catch (RuntimeException ex)
						{
							this.handler(ex);
							throw;
						}
						return flag;
					}

					// Token: 0x04000B2D RID: 2861
					private readonly ListValue list;

					// Token: 0x04000B2E RID: 2862
					private readonly Action<RuntimeException> handler;

					// Token: 0x0200035D RID: 861
					private class Enumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06001ECF RID: 7887 RVA: 0x0004E9D8 File Offset: 0x0004CBD8
						public Enumerator(IEnumerator<IValueReference> enumerator, Action<RuntimeException> handler)
						{
							this.enumerator = enumerator;
							this.handler = handler;
						}

						// Token: 0x06001ED0 RID: 7888 RVA: 0x0004E9EE File Offset: 0x0004CBEE
						public void Reset()
						{
							this.enumerator.Reset();
						}

						// Token: 0x06001ED1 RID: 7889 RVA: 0x0004E9FC File Offset: 0x0004CBFC
						public bool MoveNext()
						{
							bool flag;
							try
							{
								flag = this.enumerator.MoveNext();
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
							return flag;
						}

						// Token: 0x17000DAF RID: 3503
						// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x0004EA38 File Offset: 0x0004CC38
						public IValueReference Current
						{
							get
							{
								IValueReference valueReference;
								try
								{
									valueReference = TableModule.Table.Table_WithErrorContextFunctionValue.Wrap(this.enumerator.Current, this.handler);
								}
								catch (RuntimeException ex)
								{
									this.handler(ex);
									throw;
								}
								return valueReference;
							}
						}

						// Token: 0x17000DB0 RID: 3504
						// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x0004EA80 File Offset: 0x0004CC80
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06001ED4 RID: 7892 RVA: 0x0004EA88 File Offset: 0x0004CC88
						public void Dispose()
						{
							try
							{
								if (this.enumerator != null)
								{
									this.enumerator.Dispose();
									this.enumerator = null;
									this.handler = null;
								}
							}
							catch (RuntimeException ex)
							{
								this.handler(ex);
								throw;
							}
						}

						// Token: 0x04000B2F RID: 2863
						private IEnumerator<IValueReference> enumerator;

						// Token: 0x04000B30 RID: 2864
						private Action<RuntimeException> handler;
					}
				}
			}

			// Token: 0x0200035F RID: 863
			private sealed class AddAccumulatedColumnFunctionValue : NativeFunctionValueN<TableValue>
			{
				// Token: 0x06001ED7 RID: 7895 RVA: 0x0004EAE8 File Offset: 0x0004CCE8
				public AddAccumulatedColumnFunctionValue()
					: base(TypeValue.Table, 5, new string[] { "table", "newColumnName", "seed", "accumulator", "selector", "columnType" }, new TypeValue[]
					{
						TypeValue.Table,
						TypeValue.Text,
						TypeValue.Any,
						TypeValue.Function,
						TypeValue.Function,
						NullableTypeValue._Type
					})
				{
				}

				// Token: 0x06001ED8 RID: 7896 RVA: 0x0004EB70 File Offset: 0x0004CD70
				protected override TableValue TypedInvokeN(Value[] args)
				{
					return new TableModule.Table.AddAccumulatedColumnFunctionValue.AddAccumulatedColumnTableValue(args[0].AsTable, args[1].AsText, args[2], args[3].AsFunction, args[4].AsFunction, (args.Length > 5 && !args[5].IsNull) ? args[5].AsType : TypeValue.Any);
				}

				// Token: 0x02000360 RID: 864
				private class AddAccumulatedColumnTableValue : TableValue
				{
					// Token: 0x06001ED9 RID: 7897 RVA: 0x0004EBC4 File Offset: 0x0004CDC4
					public AddAccumulatedColumnTableValue(TableValue table, TextValue newColumnName, Value seed, FunctionValue accumulator, FunctionValue selector, TypeValue columnType)
					{
						this.table = table;
						this.seed = seed;
						this.accumulator = accumulator;
						this.selector = selector;
						TableTypeValue asTableType = table.Type.AsTableType;
						RecordTypeValue itemType = asTableType.ItemType;
						Value[] array = new Value[itemType.Fields.Keys.Length + 1];
						KeysBuilder keysBuilder = new KeysBuilder(array.Length);
						for (int i = 0; i < array.Length - 1; i++)
						{
							keysBuilder.Add(itemType.Fields.Keys[i]);
							array[i] = itemType.Fields[i];
						}
						keysBuilder.Add(newColumnName.String);
						array[array.Length - 1] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							columnType,
							LogicalValue.False
						});
						this.type = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(keysBuilder.ToKeys(), array)), asTableType.TableKeys);
					}

					// Token: 0x06001EDA RID: 7898 RVA: 0x0004ECBC File Offset: 0x0004CEBC
					private AddAccumulatedColumnTableValue(TableValue table, Value seed, FunctionValue accumulator, FunctionValue selector, TableTypeValue type)
					{
						this.table = table;
						this.seed = seed;
						this.accumulator = accumulator;
						this.selector = selector;
						this.type = type;
					}

					// Token: 0x17000DB1 RID: 3505
					// (get) Token: 0x06001EDB RID: 7899 RVA: 0x0004ECE9 File Offset: 0x0004CEE9
					public override TypeValue Type
					{
						get
						{
							return this.type;
						}
					}

					// Token: 0x06001EDC RID: 7900 RVA: 0x0004ECF1 File Offset: 0x0004CEF1
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new TableModule.Table.AddAccumulatedColumnFunctionValue.AddAccumulatedColumnTableValue.AddAccumulatedColumnEnumerator(this.table, this.type.ItemType.Fields.Keys, this.seed, this.accumulator, this.selector);
					}

					// Token: 0x06001EDD RID: 7901 RVA: 0x0004ED25 File Offset: 0x0004CF25
					public override TableValue Take(RowCount count)
					{
						return new TableModule.Table.AddAccumulatedColumnFunctionValue.AddAccumulatedColumnTableValue(this.table.Take(count), this.seed, this.accumulator, this.selector, this.type);
					}

					// Token: 0x04000B32 RID: 2866
					private readonly TableValue table;

					// Token: 0x04000B33 RID: 2867
					private readonly Value seed;

					// Token: 0x04000B34 RID: 2868
					private readonly FunctionValue accumulator;

					// Token: 0x04000B35 RID: 2869
					private readonly FunctionValue selector;

					// Token: 0x04000B36 RID: 2870
					private readonly TableTypeValue type;

					// Token: 0x02000361 RID: 865
					private sealed class AddAccumulatedColumnEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06001EDE RID: 7902 RVA: 0x0004ED50 File Offset: 0x0004CF50
						public AddAccumulatedColumnEnumerator(TableValue table, Keys columns, Value seed, FunctionValue accumulator, FunctionValue selector)
						{
							this.columns = columns;
							this.accumulator = accumulator;
							this.selector = selector;
							this.enumerator = table.GetEnumerator();
							this.state = seed;
						}

						// Token: 0x17000DB2 RID: 3506
						// (get) Token: 0x06001EDF RID: 7903 RVA: 0x0004ED82 File Offset: 0x0004CF82
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06001EE0 RID: 7904 RVA: 0x0004ED8A File Offset: 0x0004CF8A
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x06001EE1 RID: 7905 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x17000DB3 RID: 3507
						// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x0004ED98 File Offset: 0x0004CF98
						public IValueReference Current
						{
							get
							{
								if (this.current == null)
								{
									IValueReference[] array = new IValueReference[this.columns.Length];
									int num = this.columns.Length - 1;
									for (int i = 0; i < this.columns.Length; i++)
									{
										try
										{
											array[i] = ((i == num) ? this.selector.Invoke(this.state) : this.enumerator.Current.Value[i]);
										}
										catch (ValueException ex)
										{
											array[i] = new ExceptionValueReference(ex);
										}
									}
									this.current = RecordValue.New(this.columns, array);
								}
								return this.current;
							}
						}

						// Token: 0x06001EE3 RID: 7907 RVA: 0x0004EE4C File Offset: 0x0004D04C
						public bool MoveNext()
						{
							this.current = null;
							if (this.enumerator.MoveNext())
							{
								this.state = this.accumulator.Invoke(this.state, this.enumerator.Current.Value);
								return true;
							}
							return false;
						}

						// Token: 0x04000B37 RID: 2871
						private readonly Keys columns;

						// Token: 0x04000B38 RID: 2872
						private readonly FunctionValue accumulator;

						// Token: 0x04000B39 RID: 2873
						private readonly FunctionValue selector;

						// Token: 0x04000B3A RID: 2874
						private IEnumerator<IValueReference> enumerator;

						// Token: 0x04000B3B RID: 2875
						private Value state;

						// Token: 0x04000B3C RID: 2876
						private Value current;
					}
				}
			}
		}

		// Token: 0x02000362 RID: 866
		public static class Type
		{
			// Token: 0x04000B3D RID: 2877
			public static readonly FunctionValue ForTable = new TableModule.Type.ForTableFunctionValue();

			// Token: 0x02000363 RID: 867
			private sealed class ForTableFunctionValue : NativeFunctionValue2<TypeValue, TypeValue, Value>
			{
				// Token: 0x06001EE5 RID: 7909 RVA: 0x0004EE98 File Offset: 0x0004D098
				public ForTableFunctionValue()
					: base(TypeValue._Type, 1, "rowType", TypeValue._Type, "tableKeys", NullableTypeValue.List)
				{
				}

				// Token: 0x06001EE6 RID: 7910 RVA: 0x0004EEBC File Offset: 0x0004D0BC
				public override TypeValue TypedInvoke(TypeValue rowType, Value keys)
				{
					if (RecordTypeAlgebra.IsClosedRecordWithRequiredFieldsOnly(rowType))
					{
						RecordTypeValue asRecordType = rowType.AsRecordType;
						IList<TableKey> list = null;
						if (!keys.IsNull)
						{
							list = TableTypeMetadata.GetTableKeys(asRecordType.Fields.Keys, keys.AsList);
						}
						return TableTypeValue.New(asRecordType, list);
					}
					if (rowType == RecordTypeValue.Any && (keys.IsNull || keys.AsList.IsEmpty))
					{
						return TableTypeValue.Any;
					}
					throw ValueException.NewExpressionError<Message0>(Strings.ValueException_RowTypeExpected, rowType, null);
				}
			}
		}

		// Token: 0x02000364 RID: 868
		public static class RowExpression
		{
			// Token: 0x04000B3E RID: 2878
			public static readonly FunctionValue Column = new TableModule.RowExpression.ColumnFunctionValue();

			// Token: 0x02000365 RID: 869
			private sealed class ColumnFunctionValue : NativeFunctionValue1<RecordValue, TextValue>
			{
				// Token: 0x06001EE8 RID: 7912 RVA: 0x0004EF3C File Offset: 0x0004D13C
				public ColumnFunctionValue()
					: base(TypeValue.Record, "columnName", TypeValue.Text)
				{
				}

				// Token: 0x06001EE9 RID: 7913 RVA: 0x0004EF53 File Offset: 0x0004D153
				public override RecordValue TypedInvoke(TextValue columnName)
				{
					return RecordValue.New(SimplifiedMAst.FieldAccessKeys, new Value[]
					{
						MAst.FieldAccessKind,
						columnName,
						TableModule.ItemExpression.Item
					});
				}
			}
		}

		// Token: 0x02000366 RID: 870
		public static class ItemExpression
		{
			// Token: 0x04000B3F RID: 2879
			public const string IsNormalizedKey = "ItemExpression.IsNormalized";

			// Token: 0x04000B40 RID: 2880
			public static readonly RecordValue IsNormalizedMetadata = RecordValue.New(Keys.New("ItemExpression.IsNormalized"), new Value[] { LogicalValue.True });

			// Token: 0x04000B41 RID: 2881
			public static readonly FunctionValue From = new TableModule.ItemExpression.FromFunctionValue();

			// Token: 0x04000B42 RID: 2882
			public static readonly FunctionValue ItemFunction = new TableModule.ItemExpression.OpaqueFunctionValue("ItemExpression.Item");

			// Token: 0x04000B43 RID: 2883
			public static readonly RecordValue Item = RecordValue.New(MAst.InvocationKeys, new Value[]
			{
				MAst.InvocationKind,
				RecordValue.New(MAst.ConstantKeys, new Value[]
				{
					MAst.ConstantKind,
					TableModule.ItemExpression.ItemFunction
				}),
				ListValue.Empty
			});

			// Token: 0x02000367 RID: 871
			private sealed class FromFunctionValue : NativeFunctionValue1<RecordValue, FunctionValue>
			{
				// Token: 0x06001EEB RID: 7915 RVA: 0x0004F00C File Offset: 0x0004D20C
				public FromFunctionValue()
					: base(TypeValue.Record, "function", TypeValue.Function)
				{
				}

				// Token: 0x06001EEC RID: 7916 RVA: 0x0004F024 File Offset: 0x0004D224
				public override RecordValue TypedInvoke(FunctionValue function)
				{
					if (function.Expression != null)
					{
						IExpression expression = new InvocationExpressionSyntaxNode1(function.Expression, new InvocationExpressionSyntaxNode0(new ConstantExpressionSyntaxNode(TableModule.ItemExpression.ItemFunction)));
						expression = InliningVisitor.Inline(Engine.Instance, expression, int.MaxValue);
						Value value;
						if (!function.TryGetMetaField("ItemExpression.IsNormalized", out value) || !value.IsLogical || !value.AsBoolean)
						{
							expression = NormalizationVisitor.Normalize(expression, true);
						}
						expression = TableModule.ItemExpression.FromFunctionValue.LambdaToFunctionVisitor.Visit(expression);
						try
						{
							return ExpressionToSimplifiedMAstVisitor.ToMAst(expression);
						}
						catch (NotSupportedException)
						{
						}
					}
					throw ValueException.NewExpressionError<Message0>(Strings.Expression_CantGetExpression, function, null);
				}

				// Token: 0x02000368 RID: 872
				private sealed class LambdaToFunctionVisitor : LogicalAstVisitor2<int>
				{
					// Token: 0x06001EED RID: 7917 RVA: 0x0004F0C0 File Offset: 0x0004D2C0
					public static IExpression Visit(IExpression expression)
					{
						return new TableModule.ItemExpression.FromFunctionValue.LambdaToFunctionVisitor().VisitExpression(expression);
					}

					// Token: 0x06001EEE RID: 7918 RVA: 0x0004F0CD File Offset: 0x0004D2CD
					private LambdaToFunctionVisitor()
					{
					}

					// Token: 0x06001EEF RID: 7919 RVA: 0x0004F0D8 File Offset: 0x0004D2D8
					protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
					{
						int num;
						if (base.Environment.TryGetValue(identifier.Name, identifier.IsInclusive, out num))
						{
							this.minRefDepth = Math.Min(num, this.minRefDepth);
						}
						return identifier;
					}

					// Token: 0x06001EF0 RID: 7920 RVA: 0x0004F114 File Offset: 0x0004D314
					protected override IExpression VisitFunction(IFunctionExpression function)
					{
						this.lambdaDepth++;
						int num = this.minRefDepth;
						this.minRefDepth = this.lambdaDepth;
						try
						{
							function = base.VisitFunction(function, this.CreateBindings(function.FunctionType.Parameters.Count));
							if (this.minRefDepth >= this.lambdaDepth)
							{
								return new ConstantExpressionSyntaxNode(new Compiler(CompileOptions.None).ToFunction(function));
							}
						}
						finally
						{
							this.minRefDepth = Math.Min(this.minRefDepth, num);
							this.lambdaDepth--;
						}
						return function;
					}

					// Token: 0x06001EF1 RID: 7921 RVA: 0x0004F1BC File Offset: 0x0004D3BC
					protected override IExpression VisitLet(ILetExpression let)
					{
						return base.VisitLet(let, this.CreateBindings(let.Variables.Count));
					}

					// Token: 0x06001EF2 RID: 7922 RVA: 0x0004F1D6 File Offset: 0x0004D3D6
					protected override IExpression VisitRecord(IRecordExpression record)
					{
						return base.VisitRecord(record, this.lambdaDepth, this.CreateBindings(record.Members.Count));
					}

					// Token: 0x06001EF3 RID: 7923 RVA: 0x0004F1F6 File Offset: 0x0004D3F6
					protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
					{
						return base.VisitTryCatchExceptionCase(tryCatchExceptionCase, this.lambdaDepth);
					}

					// Token: 0x06001EF4 RID: 7924 RVA: 0x0004F205 File Offset: 0x0004D405
					protected override ISection VisitModule(ISection module)
					{
						return base.VisitModule(module, this.CreateBindings(module.Members.Count));
					}

					// Token: 0x06001EF5 RID: 7925 RVA: 0x0004F220 File Offset: 0x0004D420
					private int[] CreateBindings(int count)
					{
						int[] array = new int[count];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = this.lambdaDepth;
						}
						return array;
					}

					// Token: 0x04000B44 RID: 2884
					private int lambdaDepth;

					// Token: 0x04000B45 RID: 2885
					private int minRefDepth;
				}
			}

			// Token: 0x02000369 RID: 873
			private sealed class OpaqueFunctionValue : NativeFunctionValue0<Value>, IOpaqueFunctionValue, IFunctionValue, IValue
			{
				// Token: 0x06001EF6 RID: 7926 RVA: 0x0004F24C File Offset: 0x0004D44C
				public OpaqueFunctionValue(string identifier)
					: base(TypeValue.Any)
				{
					this.expression = new InvocationExpressionSyntaxNode0(new RequiredFieldAccessExpressionSyntaxNode(new RequiredFieldAccessExpressionSyntaxNode(new LibraryIdentifierExpression(identifier), Identifier.New("Function")), Identifier.New("Value")));
				}

				// Token: 0x17000DB4 RID: 3508
				// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x0004F288 File Offset: 0x0004D488
				public override IExpression Expression
				{
					get
					{
						return this.expression;
					}
				}

				// Token: 0x06001EF8 RID: 7928 RVA: 0x0004F290 File Offset: 0x0004D490
				public override Value TypedInvoke()
				{
					throw ValueException.NewExpressionError<Message0>(Strings.FunctionCannotBeInvoked, this, null);
				}

				// Token: 0x04000B46 RID: 2886
				private readonly IExpression expression;
			}
		}
	}
}
