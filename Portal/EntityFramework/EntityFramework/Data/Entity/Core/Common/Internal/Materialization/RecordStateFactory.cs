using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x0200063C RID: 1596
	internal class RecordStateFactory
	{
		// Token: 0x06004CC4 RID: 19652 RVA: 0x0010F1E0 File Offset: 0x0010D3E0
		public RecordStateFactory(int stateSlotNumber, int columnCount, RecordStateFactory[] nestedRecordStateFactories, DataRecordInfo dataRecordInfo, Expression<Func<Shaper, bool>> gatherData, string[] propertyNames, TypeUsage[] typeUsages, bool[] isColumnNested)
		{
			this.StateSlotNumber = stateSlotNumber;
			this.ColumnCount = columnCount;
			this.NestedRecordStateFactories = new ReadOnlyCollection<RecordStateFactory>(nestedRecordStateFactories);
			this.DataRecordInfo = dataRecordInfo;
			this.GatherData = gatherData.Compile();
			this.Description = gatherData.ToString();
			this.ColumnNames = new ReadOnlyCollection<string>(propertyNames);
			this.TypeUsages = new ReadOnlyCollection<TypeUsage>(typeUsages);
			this.FieldNameLookup = new FieldNameLookup(this.ColumnNames);
			if (isColumnNested == null)
			{
				isColumnNested = new bool[columnCount];
				int i = 0;
				while (i < columnCount)
				{
					BuiltInTypeKind builtInTypeKind = typeUsages[i].EdmType.BuiltInTypeKind;
					if (builtInTypeKind <= BuiltInTypeKind.ComplexType)
					{
						if (builtInTypeKind != BuiltInTypeKind.CollectionType && builtInTypeKind != BuiltInTypeKind.ComplexType)
						{
							goto IL_00B2;
						}
						goto IL_00A4;
					}
					else
					{
						if (builtInTypeKind == BuiltInTypeKind.EntityType || builtInTypeKind == BuiltInTypeKind.RowType)
						{
							goto IL_00A4;
						}
						goto IL_00B2;
					}
					IL_00B7:
					i++;
					continue;
					IL_00A4:
					isColumnNested[i] = true;
					this.HasNestedColumns = true;
					goto IL_00B7;
					IL_00B2:
					isColumnNested[i] = false;
					goto IL_00B7;
				}
			}
			this.IsColumnNested = new ReadOnlyCollection<bool>(isColumnNested);
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x0010F2BC File Offset: 0x0010D4BC
		public RecordStateFactory(int stateSlotNumber, int columnCount, RecordStateFactory[] nestedRecordStateFactories, DataRecordInfo dataRecordInfo, Expression gatherData, string[] propertyNames, TypeUsage[] typeUsages)
			: this(stateSlotNumber, columnCount, nestedRecordStateFactories, dataRecordInfo, CodeGenEmitter.BuildShaperLambda<bool>(gatherData), propertyNames, typeUsages, null)
		{
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x0010F2E0 File Offset: 0x0010D4E0
		internal RecordState Create(CoordinatorFactory coordinatorFactory)
		{
			return new RecordState(this, coordinatorFactory);
		}

		// Token: 0x04001B3F RID: 6975
		internal readonly int StateSlotNumber;

		// Token: 0x04001B40 RID: 6976
		internal readonly int ColumnCount;

		// Token: 0x04001B41 RID: 6977
		internal readonly DataRecordInfo DataRecordInfo;

		// Token: 0x04001B42 RID: 6978
		internal readonly Func<Shaper, bool> GatherData;

		// Token: 0x04001B43 RID: 6979
		internal readonly ReadOnlyCollection<RecordStateFactory> NestedRecordStateFactories;

		// Token: 0x04001B44 RID: 6980
		internal readonly ReadOnlyCollection<string> ColumnNames;

		// Token: 0x04001B45 RID: 6981
		internal readonly ReadOnlyCollection<TypeUsage> TypeUsages;

		// Token: 0x04001B46 RID: 6982
		internal readonly ReadOnlyCollection<bool> IsColumnNested;

		// Token: 0x04001B47 RID: 6983
		internal readonly bool HasNestedColumns;

		// Token: 0x04001B48 RID: 6984
		internal readonly FieldNameLookup FieldNameLookup;

		// Token: 0x04001B49 RID: 6985
		private readonly string Description;
	}
}
