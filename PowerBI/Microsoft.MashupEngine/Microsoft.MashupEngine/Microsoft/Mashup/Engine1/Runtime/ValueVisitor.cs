using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016C8 RID: 5832
	internal class ValueVisitor
	{
		// Token: 0x170026F8 RID: 9976
		// (get) Token: 0x06009453 RID: 37971 RVA: 0x001E9BEA File Offset: 0x001E7DEA
		protected int Depth
		{
			get
			{
				if (this.currentScope != null)
				{
					return this.currentScope.Depth;
				}
				return 0;
			}
		}

		// Token: 0x06009454 RID: 37972 RVA: 0x001E9C01 File Offset: 0x001E7E01
		public virtual void VisitValue(Value value)
		{
			this.VisitMetaValue(value.MetaValue);
			this.VisitNonMetaValue(value);
		}

		// Token: 0x06009455 RID: 37973 RVA: 0x001E9C18 File Offset: 0x001E7E18
		protected virtual void VisitPrimitiveValue(Value value)
		{
			switch (value.Kind)
			{
			case ValueKind.Null:
				this.VisitNull();
				return;
			case ValueKind.Time:
				this.VisitTime(value.AsTime);
				return;
			case ValueKind.Date:
				this.VisitDate(value.AsDate);
				return;
			case ValueKind.DateTime:
				this.VisitDateTime(value.AsDateTime);
				return;
			case ValueKind.DateTimeZone:
				this.VisitDateTimeZone(value.AsDateTimeZone);
				return;
			case ValueKind.Duration:
				this.VisitDuration(value.AsDuration);
				return;
			case ValueKind.Number:
				this.VisitNumber(value.AsNumber);
				return;
			case ValueKind.Logical:
				this.VisitLogical(value.AsLogical);
				return;
			case ValueKind.Text:
				this.VisitText(value.AsText);
				return;
			case ValueKind.Binary:
				this.VisitBinary(value.AsBinary);
				return;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06009456 RID: 37974 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitBinary(BinaryValue value)
		{
		}

		// Token: 0x06009457 RID: 37975 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitCycle(int depth, Value value)
		{
		}

		// Token: 0x06009458 RID: 37976 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitDate(DateValue value)
		{
		}

		// Token: 0x06009459 RID: 37977 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitDateTime(DateTimeValue value)
		{
		}

		// Token: 0x0600945A RID: 37978 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitDateTimeZone(DateTimeZoneValue value)
		{
		}

		// Token: 0x0600945B RID: 37979 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitDuration(DurationValue value)
		{
		}

		// Token: 0x0600945C RID: 37980 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitFunction(FunctionValue function)
		{
		}

		// Token: 0x0600945D RID: 37981 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitAction(ActionValue action)
		{
		}

		// Token: 0x0600945E RID: 37982 RVA: 0x001E9CE0 File Offset: 0x001E7EE0
		protected virtual void VisitList(ListValue list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.VisitListItem(i, list[i]);
			}
		}

		// Token: 0x0600945F RID: 37983 RVA: 0x001E9D0C File Offset: 0x001E7F0C
		protected virtual void VisitListItem(int index, Value value)
		{
			this.VisitValue(value);
		}

		// Token: 0x06009460 RID: 37984 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitLogical(LogicalValue value)
		{
		}

		// Token: 0x06009461 RID: 37985 RVA: 0x001E9D15 File Offset: 0x001E7F15
		protected virtual void VisitMetaValue(RecordValue metaValue)
		{
			this._VisitRecord(metaValue);
		}

		// Token: 0x06009462 RID: 37986 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitNull()
		{
		}

		// Token: 0x06009463 RID: 37987 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitNumber(NumberValue value)
		{
		}

		// Token: 0x06009464 RID: 37988 RVA: 0x001E9D20 File Offset: 0x001E7F20
		protected virtual void VisitRecord(RecordValue record)
		{
			for (int i = 0; i < record.Count; i++)
			{
				this.VisitRecordField(record.Keys[i], record[i]);
			}
		}

		// Token: 0x06009465 RID: 37989 RVA: 0x001E9D0C File Offset: 0x001E7F0C
		protected virtual void VisitRecordField(string name, Value value)
		{
			this.VisitValue(value);
		}

		// Token: 0x06009466 RID: 37990 RVA: 0x001E9D58 File Offset: 0x001E7F58
		protected virtual void VisitTable(TableValue table)
		{
			int num = 0;
			foreach (IValueReference valueReference in table)
			{
				this.VisitTableRow(num++, valueReference.Value.AsRecord);
			}
		}

		// Token: 0x06009467 RID: 37991 RVA: 0x001E9DB4 File Offset: 0x001E7FB4
		protected virtual void VisitTableRow(int rowNumber, RecordValue row)
		{
			for (int i = 0; i < row.Count; i++)
			{
				this.VisitTableValue(rowNumber, row.Keys[i], row[i]);
			}
		}

		// Token: 0x06009468 RID: 37992 RVA: 0x001E9DEC File Offset: 0x001E7FEC
		protected virtual void VisitTableValue(int rowNumber, string columnName, Value value)
		{
			this.VisitValue(value);
		}

		// Token: 0x06009469 RID: 37993 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitText(TextValue value)
		{
		}

		// Token: 0x0600946A RID: 37994 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitTime(TimeValue value)
		{
		}

		// Token: 0x0600946B RID: 37995 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VisitType(TypeValue type)
		{
		}

		// Token: 0x0600946C RID: 37996 RVA: 0x001E9DF8 File Offset: 0x001E7FF8
		protected void VisitNonMetaValue(Value value)
		{
			switch (value.Kind)
			{
			case ValueKind.List:
				this._VisitList(value.AsList);
				return;
			case ValueKind.Record:
				this._VisitRecord(value.AsRecord);
				return;
			case ValueKind.Table:
				this._VisitTable(value.AsTable);
				return;
			case ValueKind.Function:
				this.VisitFunction(value.AsFunction);
				return;
			case ValueKind.Type:
				this._VisitType(value.AsType);
				return;
			case ValueKind.Action:
				this.VisitAction(value.AsAction);
				return;
			default:
				this.VisitPrimitiveValue(value);
				return;
			}
		}

		// Token: 0x0600946D RID: 37997 RVA: 0x001E9E84 File Offset: 0x001E8084
		private void _VisitList(ListValue list)
		{
			using (ValueVisitor.Scope scope = new ValueVisitor.Scope(this, list))
			{
				if (!scope.Visited)
				{
					this.VisitList(list);
				}
			}
		}

		// Token: 0x0600946E RID: 37998 RVA: 0x001E9EC4 File Offset: 0x001E80C4
		private void _VisitRecord(RecordValue record)
		{
			using (ValueVisitor.Scope scope = new ValueVisitor.Scope(this, record))
			{
				if (!scope.Visited)
				{
					this.VisitRecord(record);
				}
			}
		}

		// Token: 0x0600946F RID: 37999 RVA: 0x001E9F04 File Offset: 0x001E8104
		private void _VisitTable(TableValue table)
		{
			using (ValueVisitor.Scope scope = new ValueVisitor.Scope(this, table))
			{
				if (!scope.Visited)
				{
					this.VisitTable(table);
				}
			}
		}

		// Token: 0x06009470 RID: 38000 RVA: 0x001E9F44 File Offset: 0x001E8144
		private void _VisitType(TypeValue type)
		{
			using (ValueVisitor.Scope scope = new ValueVisitor.Scope(this, type))
			{
				if (!scope.Visited)
				{
					this.VisitType(type);
				}
			}
		}

		// Token: 0x04004EF1 RID: 20209
		private ValueVisitor.Scope currentScope;

		// Token: 0x020016C9 RID: 5833
		private class Scope : IDisposable
		{
			// Token: 0x06009472 RID: 38002 RVA: 0x001E9F84 File Offset: 0x001E8184
			public Scope(ValueVisitor visitor, Value value)
			{
				this.visitor = visitor;
				this.value = value;
				this.previousScope = visitor.currentScope;
				this.depth = ((this.previousScope == null) ? 0 : (this.previousScope.depth + 1));
				visitor.currentScope = this;
				for (ValueVisitor.Scope scope = this.previousScope; scope != null; scope = scope.previousScope)
				{
					if (scope.value == value)
					{
						this.visited = true;
						visitor.VisitCycle(scope.depth, value);
						return;
					}
				}
			}

			// Token: 0x06009473 RID: 38003 RVA: 0x001EA007 File Offset: 0x001E8207
			void IDisposable.Dispose()
			{
				this.visitor.currentScope = this.previousScope;
				this.value = null;
			}

			// Token: 0x170026F9 RID: 9977
			// (get) Token: 0x06009474 RID: 38004 RVA: 0x001EA021 File Offset: 0x001E8221
			public bool Visited
			{
				get
				{
					return this.visited;
				}
			}

			// Token: 0x170026FA RID: 9978
			// (get) Token: 0x06009475 RID: 38005 RVA: 0x001EA029 File Offset: 0x001E8229
			public int Depth
			{
				get
				{
					return this.depth;
				}
			}

			// Token: 0x04004EF2 RID: 20210
			private ValueVisitor visitor;

			// Token: 0x04004EF3 RID: 20211
			private ValueVisitor.Scope previousScope;

			// Token: 0x04004EF4 RID: 20212
			private IValueReference value;

			// Token: 0x04004EF5 RID: 20213
			private int depth;

			// Token: 0x04004EF6 RID: 20214
			private bool visited;
		}
	}
}
