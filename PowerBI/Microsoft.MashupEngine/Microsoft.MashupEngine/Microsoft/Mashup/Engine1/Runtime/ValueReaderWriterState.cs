using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016AA RID: 5802
	public sealed class ValueReaderWriterState
	{
		// Token: 0x06009393 RID: 37779 RVA: 0x001E8347 File Offset: 0x001E6547
		public ValueReaderWriterState()
		{
			this.state = new Stack<long>();
			this.state.Push(0L);
		}

		// Token: 0x170026D7 RID: 9943
		// (get) Token: 0x06009394 RID: 37780 RVA: 0x001E8367 File Offset: 0x001E6567
		public long Continuation
		{
			get
			{
				return this.state.Peek();
			}
		}

		// Token: 0x06009395 RID: 37781 RVA: 0x001E8374 File Offset: 0x001E6574
		public void StartBinary(long start)
		{
			this.state.Push(start);
		}

		// Token: 0x06009396 RID: 37782 RVA: 0x001E8382 File Offset: 0x001E6582
		public long EndBinary()
		{
			return this.state.Pop();
		}

		// Token: 0x06009397 RID: 37783 RVA: 0x001E8374 File Offset: 0x001E6574
		public void StartList(long start)
		{
			this.state.Push(start);
		}

		// Token: 0x06009398 RID: 37784 RVA: 0x001E8382 File Offset: 0x001E6582
		public long EndList()
		{
			return this.state.Pop();
		}

		// Token: 0x06009399 RID: 37785 RVA: 0x001E8374 File Offset: 0x001E6574
		public void StartTable(long start, int columns)
		{
			this.state.Push(start);
		}

		// Token: 0x0600939A RID: 37786 RVA: 0x001E8382 File Offset: 0x001E6582
		public long EndTable()
		{
			return this.state.Pop();
		}

		// Token: 0x0600939B RID: 37787 RVA: 0x001E838F File Offset: 0x001E658F
		[Conditional("DEBUG")]
		private void InitChecker()
		{
			this.check = new Stack<ValueReaderWriterState.StateRecord>();
			this.check.Push(ValueReaderWriterState.StateRecord.Top);
		}

		// Token: 0x170026D8 RID: 9944
		// (get) Token: 0x0600939C RID: 37788 RVA: 0x001E83AC File Offset: 0x001E65AC
		private ValueReaderWriterState.StateRecord CurrentCheck
		{
			get
			{
				return this.check.Peek();
			}
		}

		// Token: 0x0600939D RID: 37789 RVA: 0x001E83BC File Offset: 0x001E65BC
		[Conditional("DEBUG")]
		private void Assert(ValueReaderWriterState.StateCode expected)
		{
			ValueReaderWriterState.StateCode stateCode = this.CurrentCheck.State;
			if ((stateCode & expected) != stateCode)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600939E RID: 37790 RVA: 0x001E83E1 File Offset: 0x001E65E1
		[Conditional("DEBUG")]
		private void AssertKind(ValueKind kind, ValueKind expected)
		{
			if (kind != expected)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600939F RID: 37791 RVA: 0x001E83ED File Offset: 0x001E65ED
		[Conditional("DEBUG")]
		private void PushCheck(ValueReaderWriterState.StateCode current, ValueReaderWriterState.StateRecord record)
		{
			this.check.Push(record);
		}

		// Token: 0x060093A0 RID: 37792 RVA: 0x001E83FB File Offset: 0x001E65FB
		[Conditional("DEBUG")]
		private void PopCheck(ValueReaderWriterState.StateCode current)
		{
			this.check.Pop();
			if (this.check.Count == 0)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060093A1 RID: 37793 RVA: 0x001E841C File Offset: 0x001E661C
		[Conditional("DEBUG")]
		private void NextStateCheck()
		{
			ValueReaderWriterState.StateRecord currentCheck = this.CurrentCheck;
			ValueReaderWriterState.StateCode stateCode;
			switch (this.CurrentCheck.State)
			{
			case ValueReaderWriterState.StateCode.ValueMeta:
				if ((currentCheck.Flags & ValueFlags.HasType) == ValueFlags.HasType)
				{
					stateCode = ValueReaderWriterState.StateCode.ValueType;
				}
				else
				{
					stateCode = ValueReaderWriterState.StateCode.Value;
				}
				break;
			case ValueReaderWriterState.StateCode.ValueType:
				stateCode = ValueReaderWriterState.StateCode.Value;
				break;
			case ValueReaderWriterState.StateCode.Value:
				stateCode = ValueReaderWriterState.StateCode.EndValue;
				break;
			default:
				return;
			}
			currentCheck.MoveState(stateCode);
		}

		// Token: 0x060093A2 RID: 37794 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void InBinary()
		{
		}

		// Token: 0x060093A3 RID: 37795 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void InList()
		{
		}

		// Token: 0x060093A4 RID: 37796 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void InTable()
		{
		}

		// Token: 0x060093A5 RID: 37797 RVA: 0x001E8474 File Offset: 0x001E6674
		[Conditional("DEBUG")]
		public void StartValue(ValueKind kind, ValueFlags flags)
		{
			ValueReaderWriterState.StateCode stateCode = this.CurrentCheck.State;
			if (stateCode != ValueReaderWriterState.StateCode.ValueMeta)
			{
			}
			this.CurrentCheck.Advance();
		}

		// Token: 0x060093A6 RID: 37798 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void EndValue()
		{
		}

		// Token: 0x060093A7 RID: 37799 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void Reference()
		{
		}

		// Token: 0x060093A8 RID: 37800 RVA: 0x001E84A0 File Offset: 0x001E66A0
		[Conditional("DEBUG")]
		public void PrimitiveValue(ValueKind kind)
		{
			ValueReaderWriterState.StateCode stateCode = this.CurrentCheck.State;
		}

		// Token: 0x060093A9 RID: 37801 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void NumberKind()
		{
		}

		// Token: 0x060093AA RID: 37802 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void BinaryValue()
		{
		}

		// Token: 0x060093AB RID: 37803 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void StartRecord(int length)
		{
		}

		// Token: 0x060093AC RID: 37804 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void EndRecord()
		{
		}

		// Token: 0x060093AD RID: 37805 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void StartRow()
		{
		}

		// Token: 0x060093AE RID: 37806 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void EndRow()
		{
		}

		// Token: 0x060093AF RID: 37807 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void StartType(ValueKind kind)
		{
		}

		// Token: 0x060093B0 RID: 37808 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void EndType()
		{
		}

		// Token: 0x060093B1 RID: 37809 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void StartRecordType()
		{
		}

		// Token: 0x060093B2 RID: 37810 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void EndRecordType()
		{
		}

		// Token: 0x060093B3 RID: 37811 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void TableType()
		{
		}

		// Token: 0x060093B4 RID: 37812 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void StartFunctionType()
		{
		}

		// Token: 0x060093B5 RID: 37813 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void EndFunctionType()
		{
		}

		// Token: 0x04004EC7 RID: 20167
		private Stack<long> state;

		// Token: 0x04004EC8 RID: 20168
		private Stack<ValueReaderWriterState.StateRecord> check;

		// Token: 0x020016AB RID: 5803
		private abstract class StateRecord
		{
			// Token: 0x060093B6 RID: 37814 RVA: 0x001E84B0 File Offset: 0x001E66B0
			public static ValueReaderWriterState.StateRecord NewValue(ValueKind kind, ValueFlags flags)
			{
				return new ValueReaderWriterState.StateRecord.ValueStateRecord(kind, flags);
			}

			// Token: 0x060093B7 RID: 37815 RVA: 0x001E84B9 File Offset: 0x001E66B9
			public static ValueReaderWriterState.StateRecord NewTaggedNumber()
			{
				return new ValueReaderWriterState.StateRecord.TaggedNumberStateRecord();
			}

			// Token: 0x060093B8 RID: 37816 RVA: 0x001E84C0 File Offset: 0x001E66C0
			public static ValueReaderWriterState.StateRecord NewBinary(long start)
			{
				return new ValueReaderWriterState.StateRecord.BinaryStateRecord(start);
			}

			// Token: 0x060093B9 RID: 37817 RVA: 0x001E84C8 File Offset: 0x001E66C8
			public static ValueReaderWriterState.StateRecord NewList(long start)
			{
				return new ValueReaderWriterState.StateRecord.ListStateRecord(start);
			}

			// Token: 0x060093BA RID: 37818 RVA: 0x001E84D0 File Offset: 0x001E66D0
			public static ValueReaderWriterState.StateRecord NewRecord(int fields)
			{
				return new ValueReaderWriterState.StateRecord.RecordStateRecord(fields);
			}

			// Token: 0x060093BB RID: 37819 RVA: 0x001E84D8 File Offset: 0x001E66D8
			public static ValueReaderWriterState.StateRecord NewTable(long start, int columns)
			{
				return new ValueReaderWriterState.StateRecord.TableStateRecord(start, columns);
			}

			// Token: 0x060093BC RID: 37820 RVA: 0x001E84E1 File Offset: 0x001E66E1
			public static ValueReaderWriterState.StateRecord NewRow(int columns)
			{
				return new ValueReaderWriterState.StateRecord.RowStateRecord(columns);
			}

			// Token: 0x060093BD RID: 37821 RVA: 0x001E84E9 File Offset: 0x001E66E9
			public static ValueReaderWriterState.StateRecord NewType(ValueKind kind)
			{
				return new ValueReaderWriterState.StateRecord.TypeStateRecord(kind);
			}

			// Token: 0x060093BE RID: 37822 RVA: 0x001E84F1 File Offset: 0x001E66F1
			public static ValueReaderWriterState.StateRecord NewRecordType()
			{
				return new ValueReaderWriterState.StateRecord.RecordTypeStateRecord();
			}

			// Token: 0x060093BF RID: 37823 RVA: 0x001E84F8 File Offset: 0x001E66F8
			public static ValueReaderWriterState.StateRecord NewFunctionType()
			{
				return new ValueReaderWriterState.StateRecord.FunctionTypeStateRecord();
			}

			// Token: 0x170026D9 RID: 9945
			// (get) Token: 0x060093C0 RID: 37824
			public abstract ValueReaderWriterState.StateCode State { get; }

			// Token: 0x170026DA RID: 9946
			// (get) Token: 0x060093C1 RID: 37825 RVA: 0x0000EE09 File Offset: 0x0000D009
			public virtual ValueKind Kind
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170026DB RID: 9947
			// (get) Token: 0x060093C2 RID: 37826 RVA: 0x0000EE09 File Offset: 0x0000D009
			public virtual ValueFlags Flags
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170026DC RID: 9948
			// (get) Token: 0x060093C3 RID: 37827 RVA: 0x0000EE09 File Offset: 0x0000D009
			public virtual long Start
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170026DD RID: 9949
			// (get) Token: 0x060093C4 RID: 37828 RVA: 0x0000EE09 File Offset: 0x0000D009
			public virtual int Fields
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170026DE RID: 9950
			// (get) Token: 0x060093C5 RID: 37829 RVA: 0x0000EE09 File Offset: 0x0000D009
			public virtual int Columns
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060093C6 RID: 37830 RVA: 0x0000EE09 File Offset: 0x0000D009
			public virtual void MoveState(ValueReaderWriterState.StateCode next)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060093C7 RID: 37831 RVA: 0x0000336E File Offset: 0x0000156E
			public virtual void Advance()
			{
			}

			// Token: 0x060093C8 RID: 37832 RVA: 0x00002105 File Offset: 0x00000305
			public virtual bool IsEnd(long position)
			{
				return false;
			}

			// Token: 0x04004EC9 RID: 20169
			public static ValueReaderWriterState.StateRecord Top = new ValueReaderWriterState.StateRecord.TopStateRecord();

			// Token: 0x020016AC RID: 5804
			private class TopStateRecord : ValueReaderWriterState.StateRecord
			{
				// Token: 0x170026DF RID: 9951
				// (get) Token: 0x060093CB RID: 37835 RVA: 0x00002105 File Offset: 0x00000305
				public override ValueReaderWriterState.StateCode State
				{
					get
					{
						return ValueReaderWriterState.StateCode.Top;
					}
				}
			}

			// Token: 0x020016AD RID: 5805
			private class ValueStateRecord : ValueReaderWriterState.StateRecord
			{
				// Token: 0x060093CD RID: 37837 RVA: 0x001E8513 File Offset: 0x001E6713
				public ValueStateRecord(ValueKind kind, ValueFlags flags)
				{
					if ((flags & ValueFlags.HasMeta) == ValueFlags.HasMeta)
					{
						this.state = ValueReaderWriterState.StateCode.ValueMeta;
					}
					else if ((flags & ValueFlags.HasType) == ValueFlags.HasType)
					{
						this.state = ValueReaderWriterState.StateCode.ValueType;
					}
					else
					{
						this.state = ValueReaderWriterState.StateCode.Value;
					}
					this.kind = kind;
					this.flags = flags;
				}

				// Token: 0x170026E0 RID: 9952
				// (get) Token: 0x060093CE RID: 37838 RVA: 0x001E8552 File Offset: 0x001E6752
				public override ValueKind Kind
				{
					get
					{
						return this.kind;
					}
				}

				// Token: 0x170026E1 RID: 9953
				// (get) Token: 0x060093CF RID: 37839 RVA: 0x001E855A File Offset: 0x001E675A
				public override ValueFlags Flags
				{
					get
					{
						return this.flags;
					}
				}

				// Token: 0x170026E2 RID: 9954
				// (get) Token: 0x060093D0 RID: 37840 RVA: 0x001E8562 File Offset: 0x001E6762
				public override ValueReaderWriterState.StateCode State
				{
					get
					{
						return this.state;
					}
				}

				// Token: 0x060093D1 RID: 37841 RVA: 0x001E856A File Offset: 0x001E676A
				public override void MoveState(ValueReaderWriterState.StateCode next)
				{
					this.state = next;
				}

				// Token: 0x04004ECA RID: 20170
				private ValueReaderWriterState.StateCode state;

				// Token: 0x04004ECB RID: 20171
				private ValueKind kind;

				// Token: 0x04004ECC RID: 20172
				private ValueFlags flags;
			}

			// Token: 0x020016AE RID: 5806
			private class TaggedNumberStateRecord : ValueReaderWriterState.StateRecord
			{
				// Token: 0x170026E3 RID: 9955
				// (get) Token: 0x060093D3 RID: 37843 RVA: 0x00075E2C File Offset: 0x0007402C
				public override ValueReaderWriterState.StateCode State
				{
					get
					{
						return ValueReaderWriterState.StateCode.TaggedNumber;
					}
				}
			}

			// Token: 0x020016AF RID: 5807
			private class BinaryStateRecord : ValueReaderWriterState.StateRecord
			{
				// Token: 0x060093D4 RID: 37844 RVA: 0x001E8573 File Offset: 0x001E6773
				public BinaryStateRecord(long start)
				{
					this.start = start;
				}

				// Token: 0x170026E4 RID: 9956
				// (get) Token: 0x060093D5 RID: 37845 RVA: 0x00002461 File Offset: 0x00000661
				public override ValueReaderWriterState.StateCode State
				{
					get
					{
						return ValueReaderWriterState.StateCode.Binary;
					}
				}

				// Token: 0x170026E5 RID: 9957
				// (get) Token: 0x060093D6 RID: 37846 RVA: 0x001E8582 File Offset: 0x001E6782
				public override long Start
				{
					get
					{
						return this.start;
					}
				}

				// Token: 0x060093D7 RID: 37847 RVA: 0x001E858A File Offset: 0x001E678A
				public override bool IsEnd(long position)
				{
					return position >= this.start;
				}

				// Token: 0x04004ECD RID: 20173
				private long start;
			}

			// Token: 0x020016B0 RID: 5808
			private class ListStateRecord : ValueReaderWriterState.StateRecord
			{
				// Token: 0x060093D8 RID: 37848 RVA: 0x001E8598 File Offset: 0x001E6798
				public ListStateRecord(long start)
				{
					this.start = start;
				}

				// Token: 0x170026E6 RID: 9958
				// (get) Token: 0x060093D9 RID: 37849 RVA: 0x00002475 File Offset: 0x00000675
				public override ValueReaderWriterState.StateCode State
				{
					get
					{
						return ValueReaderWriterState.StateCode.List;
					}
				}

				// Token: 0x170026E7 RID: 9959
				// (get) Token: 0x060093DA RID: 37850 RVA: 0x001E85A7 File Offset: 0x001E67A7
				public override long Start
				{
					get
					{
						return this.start;
					}
				}

				// Token: 0x060093DB RID: 37851 RVA: 0x001E85AF File Offset: 0x001E67AF
				public override bool IsEnd(long position)
				{
					return position >= this.start;
				}

				// Token: 0x04004ECE RID: 20174
				private long start;
			}

			// Token: 0x020016B1 RID: 5809
			private class RecordStateRecord : ValueReaderWriterState.StateRecord
			{
				// Token: 0x060093DC RID: 37852 RVA: 0x001E85BD File Offset: 0x001E67BD
				public RecordStateRecord(int fields)
				{
					this.fields = fields;
				}

				// Token: 0x170026E8 RID: 9960
				// (get) Token: 0x060093DD RID: 37853 RVA: 0x000024ED File Offset: 0x000006ED
				public override ValueReaderWriterState.StateCode State
				{
					get
					{
						return ValueReaderWriterState.StateCode.Record;
					}
				}

				// Token: 0x170026E9 RID: 9961
				// (get) Token: 0x060093DE RID: 37854 RVA: 0x001E85CC File Offset: 0x001E67CC
				public override int Fields
				{
					get
					{
						return this.fields;
					}
				}

				// Token: 0x060093DF RID: 37855 RVA: 0x001E85D4 File Offset: 0x001E67D4
				public override void Advance()
				{
					if (this.fields == 0)
					{
						throw new InvalidOperationException();
					}
					this.fields--;
				}

				// Token: 0x04004ECF RID: 20175
				private int fields;
			}

			// Token: 0x020016B2 RID: 5810
			private class TableStateRecord : ValueReaderWriterState.StateRecord
			{
				// Token: 0x060093E0 RID: 37856 RVA: 0x001E85F2 File Offset: 0x001E67F2
				public TableStateRecord(long start, int columns)
				{
					this.start = start;
					this.columns = columns;
				}

				// Token: 0x170026EA RID: 9962
				// (get) Token: 0x060093E1 RID: 37857 RVA: 0x00142610 File Offset: 0x00140810
				public override ValueReaderWriterState.StateCode State
				{
					get
					{
						return ValueReaderWriterState.StateCode.Table;
					}
				}

				// Token: 0x170026EB RID: 9963
				// (get) Token: 0x060093E2 RID: 37858 RVA: 0x001E8608 File Offset: 0x001E6808
				public override long Start
				{
					get
					{
						return this.start;
					}
				}

				// Token: 0x170026EC RID: 9964
				// (get) Token: 0x060093E3 RID: 37859 RVA: 0x001E8610 File Offset: 0x001E6810
				public override int Columns
				{
					get
					{
						return this.columns;
					}
				}

				// Token: 0x060093E4 RID: 37860 RVA: 0x001E8618 File Offset: 0x001E6818
				public override bool IsEnd(long position)
				{
					return position >= this.start;
				}

				// Token: 0x04004ED0 RID: 20176
				private long start;

				// Token: 0x04004ED1 RID: 20177
				private int columns;
			}

			// Token: 0x020016B3 RID: 5811
			private class RowStateRecord : ValueReaderWriterState.StateRecord.RecordStateRecord
			{
				// Token: 0x060093E5 RID: 37861 RVA: 0x001E8626 File Offset: 0x001E6826
				public RowStateRecord(int fields)
					: base(fields)
				{
				}

				// Token: 0x170026ED RID: 9965
				// (get) Token: 0x060093E6 RID: 37862 RVA: 0x0014025A File Offset: 0x0013E45A
				public override ValueReaderWriterState.StateCode State
				{
					get
					{
						return ValueReaderWriterState.StateCode.Row;
					}
				}
			}

			// Token: 0x020016B4 RID: 5812
			private class TypeStateRecord : ValueReaderWriterState.StateRecord
			{
				// Token: 0x060093E7 RID: 37863 RVA: 0x001E862F File Offset: 0x001E682F
				public TypeStateRecord(ValueKind kind)
				{
					this.kind = kind;
				}

				// Token: 0x170026EE RID: 9966
				// (get) Token: 0x060093E8 RID: 37864 RVA: 0x001E863E File Offset: 0x001E683E
				public override ValueKind Kind
				{
					get
					{
						return this.kind;
					}
				}

				// Token: 0x170026EF RID: 9967
				// (get) Token: 0x060093E9 RID: 37865 RVA: 0x0014213C File Offset: 0x0014033C
				public override ValueReaderWriterState.StateCode State
				{
					get
					{
						return ValueReaderWriterState.StateCode.Type;
					}
				}

				// Token: 0x04004ED2 RID: 20178
				private ValueKind kind;
			}

			// Token: 0x020016B5 RID: 5813
			private class RecordTypeStateRecord : ValueReaderWriterState.StateRecord
			{
				// Token: 0x170026F0 RID: 9968
				// (get) Token: 0x060093EB RID: 37867 RVA: 0x001422C0 File Offset: 0x001404C0
				public override ValueReaderWriterState.StateCode State
				{
					get
					{
						return ValueReaderWriterState.StateCode.RecordType;
					}
				}
			}

			// Token: 0x020016B6 RID: 5814
			private class FunctionTypeStateRecord : ValueReaderWriterState.StateRecord
			{
				// Token: 0x170026F1 RID: 9969
				// (get) Token: 0x060093ED RID: 37869 RVA: 0x0006808E File Offset: 0x0006628E
				public override ValueReaderWriterState.StateCode State
				{
					get
					{
						return ValueReaderWriterState.StateCode.FunctionType;
					}
				}
			}
		}

		// Token: 0x020016B7 RID: 5815
		private enum StateCode
		{
			// Token: 0x04004ED4 RID: 20180
			Top,
			// Token: 0x04004ED5 RID: 20181
			ValueMeta,
			// Token: 0x04004ED6 RID: 20182
			ValueType,
			// Token: 0x04004ED7 RID: 20183
			Value,
			// Token: 0x04004ED8 RID: 20184
			EndValue,
			// Token: 0x04004ED9 RID: 20185
			TaggedNumber,
			// Token: 0x04004EDA RID: 20186
			Binary,
			// Token: 0x04004EDB RID: 20187
			List,
			// Token: 0x04004EDC RID: 20188
			Record,
			// Token: 0x04004EDD RID: 20189
			Table,
			// Token: 0x04004EDE RID: 20190
			Row,
			// Token: 0x04004EDF RID: 20191
			Type,
			// Token: 0x04004EE0 RID: 20192
			RecordType,
			// Token: 0x04004EE1 RID: 20193
			TableType,
			// Token: 0x04004EE2 RID: 20194
			FunctionType
		}
	}
}
