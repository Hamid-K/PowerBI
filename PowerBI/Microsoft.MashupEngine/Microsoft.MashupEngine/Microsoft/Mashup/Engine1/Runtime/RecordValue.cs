using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015EB RID: 5611
	public abstract class RecordValue : StructureValue, IRecordValue, IValue
	{
		// Token: 0x06008D0F RID: 36111 RVA: 0x001D89E9 File Offset: 0x001D6BE9
		public static RecordValue New(RecordTypeValue type, params Value[] values)
		{
			return RecordValue.New(type.Fields.Keys, type, values);
		}

		// Token: 0x06008D10 RID: 36112 RVA: 0x001D89FD File Offset: 0x001D6BFD
		public static RecordValue New(Keys keys, params Value[] values)
		{
			if (keys.Length == 0)
			{
				return RecordValue.Empty;
			}
			return RecordValue.New(keys, null, values);
		}

		// Token: 0x06008D11 RID: 36113 RVA: 0x001D8A15 File Offset: 0x001D6C15
		private static RecordValue New(Keys keys, RecordTypeValue type, params Value[] values)
		{
			if (keys.Length != values.Length)
			{
				throw ValueException.NewExpressionError<Message2>(Strings.RecordValue_New_MismatchedKeysAndValues(keys.Length, values.Length), ListValue.New(values), null);
			}
			return new RecordValue.ArrayRecordValue(keys, type, values);
		}

		// Token: 0x06008D12 RID: 36114 RVA: 0x001D8A4F File Offset: 0x001D6C4F
		public static RecordValue New(Keys keys, params IValueReference[] values)
		{
			if (keys.Length == 0)
			{
				return RecordValue.Empty;
			}
			return RecordValue.New(keys, null, values);
		}

		// Token: 0x06008D13 RID: 36115 RVA: 0x001D8A67 File Offset: 0x001D6C67
		public static RecordValue New(RecordTypeValue type, params IValueReference[] values)
		{
			return RecordValue.New(type.Fields.Keys, type, values);
		}

		// Token: 0x06008D14 RID: 36116 RVA: 0x001D8A7B File Offset: 0x001D6C7B
		private static RecordValue New(Keys keys, RecordTypeValue type, params IValueReference[] values)
		{
			if (keys.Length != values.Length)
			{
				throw ValueException.NewExpressionError<Message2>(Strings.RecordValue_New_MismatchedKeysAndValues(keys.Length, values.Length), ListValue.New(values), null);
			}
			return new RecordValue.ReferenceArrayRecordValue(keys, type, values);
		}

		// Token: 0x06008D15 RID: 36117 RVA: 0x001D8AB5 File Offset: 0x001D6CB5
		public static RecordValue New(RecordTypeValue type, Func<int, Value> getValue)
		{
			return RecordValue.New(type.Fields.Keys, type, getValue);
		}

		// Token: 0x06008D16 RID: 36118 RVA: 0x001D8AC9 File Offset: 0x001D6CC9
		public static RecordValue New(Keys keys, Func<int, Value> getValue)
		{
			if (keys.Length == 0)
			{
				return RecordValue.Empty;
			}
			return RecordValue.New(keys, null, getValue);
		}

		// Token: 0x06008D17 RID: 36119 RVA: 0x001D8AE1 File Offset: 0x001D6CE1
		private static RecordValue New(Keys keys, RecordTypeValue type, Func<int, Value> getValue)
		{
			return new RecordValue.DemandRecordValue(keys, type, getValue);
		}

		// Token: 0x06008D18 RID: 36120 RVA: 0x001D8AEC File Offset: 0x001D6CEC
		public static RecordValue New(params NamedValue[] namedValues)
		{
			KeysBuilder keysBuilder = new KeysBuilder(namedValues.Length);
			Value[] array = new Value[namedValues.Length];
			for (int i = 0; i < namedValues.Length; i++)
			{
				keysBuilder.Add(namedValues[i].Key);
				array[i] = namedValues[i].Value;
			}
			return RecordValue.New(keysBuilder.ToKeys(), array);
		}

		// Token: 0x06008D19 RID: 36121 RVA: 0x001D8B49 File Offset: 0x001D6D49
		public sealed override string ToSource()
		{
			return "[...]";
		}

		// Token: 0x06008D1A RID: 36122 RVA: 0x001D8B50 File Offset: 0x001D6D50
		public sealed override string ToString()
		{
			return "Record";
		}

		// Token: 0x06008D1B RID: 36123 RVA: 0x001D8B57 File Offset: 0x001D6D57
		public sealed override object ToOleDb(Type type)
		{
			return ValueMarshaller.ToOleDbString("[Record]", this, type);
		}

		// Token: 0x06008D1C RID: 36124 RVA: 0x001D8B65 File Offset: 0x001D6D65
		public override void TestConnection()
		{
			if (this.Keys.Length > 0)
			{
				this.Item0.TestConnection();
			}
		}

		// Token: 0x17002507 RID: 9479
		// (get) Token: 0x06008D1D RID: 36125 RVA: 0x001D8B80 File Offset: 0x001D6D80
		public int Count
		{
			get
			{
				return this.Keys.Length;
			}
		}

		// Token: 0x17002508 RID: 9480
		// (get) Token: 0x06008D1E RID: 36126 RVA: 0x001D8B8D File Offset: 0x001D6D8D
		public virtual bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x17002509 RID: 9481
		// (get) Token: 0x06008D1F RID: 36127 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsRecord
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700250A RID: 9482
		// (get) Token: 0x06008D20 RID: 36128 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override RecordValue AsRecord
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700250B RID: 9483
		// (get) Token: 0x06008D21 RID: 36129 RVA: 0x0014213C File Offset: 0x0014033C
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Record;
			}
		}

		// Token: 0x1700250C RID: 9484
		// (get) Token: 0x06008D22 RID: 36130
		public abstract override TypeValue Type { get; }

		// Token: 0x06008D23 RID: 36131 RVA: 0x001D8B98 File Offset: 0x001D6D98
		public override Value NewType(TypeValue type)
		{
			RecordTypeValue asRecordType = type.AsRecordType;
			if (asRecordType.Open || asRecordType.Fields.Keys.Length != this.Keys.Length)
			{
				throw ValueException.CastTypeMismatch(this, type);
			}
			for (int i = 0; i < asRecordType.Fields.Keys.Length; i++)
			{
				if (asRecordType.Fields[i]["Optional"].AsBoolean)
				{
					throw ValueException.CastTypeMismatch(this, type);
				}
			}
			return new RecordValue.CastRecordValue(this, asRecordType);
		}

		// Token: 0x1700250D RID: 9485
		// (get) Token: 0x06008D24 RID: 36132 RVA: 0x001D8C20 File Offset: 0x001D6E20
		public override bool IsDefaultType
		{
			get
			{
				RecordValue fields = this.Type.AsRecordType.Fields;
				for (int i = 0; i < fields.Keys.Length; i++)
				{
					if (!fields[i]["Type"].Equals(TypeValue.Any))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x1700250E RID: 9486
		// (get) Token: 0x06008D25 RID: 36133
		public abstract Keys Keys { get; }

		// Token: 0x1700250F RID: 9487
		public override Value this[Value key]
		{
			get
			{
				if (key.IsList)
				{
					return Library.Record.SelectFields.Invoke(this, key);
				}
				return this[this.IndexOf(key)];
			}
		}

		// Token: 0x06008D27 RID: 36135 RVA: 0x001D8C98 File Offset: 0x001D6E98
		public virtual IValueReference GetReference(int index)
		{
			if (index >= this.Keys.Length)
			{
				throw ValueException.RecordIndexOutOfRange(index, this);
			}
			return new RecordValue.RecordValueReference(this, index);
		}

		// Token: 0x17002510 RID: 9488
		public override Value this[string field]
		{
			get
			{
				return this[this.IndexOf(field)];
			}
		}

		// Token: 0x06008D29 RID: 36137 RVA: 0x001D8CC6 File Offset: 0x001D6EC6
		public virtual ValueException MissingField(string fieldName)
		{
			return ValueException.MissingField(fieldName, this);
		}

		// Token: 0x06008D2A RID: 36138 RVA: 0x001D8CD0 File Offset: 0x001D6ED0
		private int IndexOf(Value key)
		{
			int num;
			if (!this.Keys.TryGetKeyIndex(key.AsString, out num))
			{
				throw this.MissingField(key.AsString);
			}
			return num;
		}

		// Token: 0x06008D2B RID: 36139 RVA: 0x001D8D00 File Offset: 0x001D6F00
		public int IndexOf(string field)
		{
			int num;
			if (!this.Keys.TryGetKeyIndex(field, out num))
			{
				throw this.MissingField(field);
			}
			return num;
		}

		// Token: 0x06008D2C RID: 36140 RVA: 0x001D8D26 File Offset: 0x001D6F26
		public override bool TryGetValue(Value index, out Value value)
		{
			return this.TryGetValue(index.AsString, out value);
		}

		// Token: 0x06008D2D RID: 36141 RVA: 0x001D8D38 File Offset: 0x001D6F38
		public override bool TryGetValue(string key, out Value value)
		{
			int num;
			if (this.Keys.TryGetKeyIndex(key, out num))
			{
				value = this[num];
				return true;
			}
			value = Value.Null;
			return false;
		}

		// Token: 0x06008D2E RID: 36142 RVA: 0x001D8D68 File Offset: 0x001D6F68
		public override Value Concatenate(Value value)
		{
			if (!value.IsRecord)
			{
				return base.Concatenate(value);
			}
			if (this.IsEmpty)
			{
				return value.SubtractMetaValue;
			}
			if (value.AsRecord.IsEmpty)
			{
				return base.SubtractMetaValue;
			}
			return RecordValue.Combine(ListValue.New(new Value[] { this, value }));
		}

		// Token: 0x06008D2F RID: 36143 RVA: 0x001D8DC0 File Offset: 0x001D6FC0
		public static RecordValue Combine(ListValue values)
		{
			Dictionary<string, Tuple<RecordValue, int>> dictionary = new Dictionary<string, Tuple<RecordValue, int>>();
			foreach (IValueReference valueReference in values)
			{
				RecordValue asRecord = valueReference.Value.AsRecord;
				Keys keys = asRecord.Keys;
				for (int i = 0; i < keys.Length; i++)
				{
					dictionary[keys[i]] = Tuple.Create<RecordValue, int>(asRecord, i);
				}
			}
			KeysBuilder keysBuilder = new KeysBuilder(dictionary.Count);
			Value[] array = new Value[dictionary.Count];
			IValueReference[] array2 = new IValueReference[dictionary.Count];
			int num = 0;
			foreach (KeyValuePair<string, Tuple<RecordValue, int>> keyValuePair in dictionary)
			{
				RecordValue item = keyValuePair.Value.Item1;
				int item2 = keyValuePair.Value.Item2;
				keysBuilder.Add(keyValuePair.Key);
				array[num] = item.Type.AsRecordType.Fields[item2];
				array2[num] = item.GetReference(item2);
				num++;
			}
			return RecordValue.New(RecordTypeValue.New(RecordValue.New(keysBuilder.ToKeys(), array), false), array2);
		}

		// Token: 0x06008D30 RID: 36144 RVA: 0x001D8F20 File Offset: 0x001D7120
		public sealed override bool Equals(Value value, _ValueComparer comparer)
		{
			if (!value.IsRecord)
			{
				return false;
			}
			Keys keys = this.Keys;
			Keys keys2 = value.AsRecord.Keys;
			if (keys.Length != keys2.Length)
			{
				return false;
			}
			if (keys.Equals(keys2))
			{
				for (int i = 0; i < keys.Length; i++)
				{
					if (!this[i].Equals(value[i], comparer))
					{
						return false;
					}
				}
			}
			else
			{
				for (int j = 0; j < keys.Length; j++)
				{
					int num;
					if (!keys2.TryGetKeyIndex(keys[j], out num))
					{
						return false;
					}
					if (!this[j].Equals(value[num], comparer))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06008D31 RID: 36145 RVA: 0x001D8FCC File Offset: 0x001D71CC
		public Value KeyValue(int index)
		{
			if (index < 0 || index >= this.Keys.Length)
			{
				throw ValueException.RecordIndexOutOfRange(index, this);
			}
			return RecordValue.New(RecordValue.NameValueRecordKeys, new Value[]
			{
				TextValue.New(this.Keys[index]),
				this[index]
			});
		}

		// Token: 0x06008D32 RID: 36146 RVA: 0x001D9024 File Offset: 0x001D7224
		public sealed override int GetHashCode(_ValueComparer comparer)
		{
			int num = this.Keys.Length;
			for (int i = 0; i < this.Keys.Length; i++)
			{
				int hashCode = this.Keys[i].GetHashCode();
				int hashCode2 = this[i].GetHashCode(comparer);
				int num2 = hashCode * 6883 + hashCode2 * 7853;
				num += num2;
			}
			return num;
		}

		// Token: 0x17002511 RID: 9489
		// (get) Token: 0x06008D33 RID: 36147 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x06008D34 RID: 36148 RVA: 0x001D9086 File Offset: 0x001D7286
		public override Value NewMeta(RecordValue metaValue)
		{
			if (!metaValue.IsEmpty)
			{
				return new RecordValue.MetaRecordValue(this, metaValue);
			}
			return this;
		}

		// Token: 0x06008D35 RID: 36149 RVA: 0x001D9099 File Offset: 0x001D7299
		IValueReference2 IRecordValue.GetReference(int index)
		{
			return new ValueReference2(this.GetReference(index));
		}

		// Token: 0x17002512 RID: 9490
		// (get) Token: 0x06008D36 RID: 36150 RVA: 0x001D90A7 File Offset: 0x001D72A7
		IKeys IRecordValue.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17002513 RID: 9491
		IValue IRecordValue.this[int index]
		{
			get
			{
				return this[index];
			}
		}

		// Token: 0x17002514 RID: 9492
		IValue IRecordValue.this[string index]
		{
			get
			{
				return this[index];
			}
		}

		// Token: 0x06008D39 RID: 36153 RVA: 0x001D90B8 File Offset: 0x001D72B8
		bool IRecordValue.TryGetValue(string name, out IValue value)
		{
			Value value2;
			if (!this.TryGetValue(name, out value2))
			{
				value = null;
				return false;
			}
			value = value2;
			return true;
		}

		// Token: 0x04004CE4 RID: 19684
		private const string placeholder = "[Record]";

		// Token: 0x04004CE5 RID: 19685
		public static readonly TextValue Placeholder = TextValue.New("[Record]");

		// Token: 0x04004CE6 RID: 19686
		public static readonly RecordValue Empty = new RecordValue.ArrayRecordValue(Keys.Empty, null, new Value[0]);

		// Token: 0x04004CE7 RID: 19687
		public static readonly Keys NameValueRecordKeys = Keys.New("Name", "Value");

		// Token: 0x020015EC RID: 5612
		private class ArrayRecordValue : RecordValue
		{
			// Token: 0x06008D3C RID: 36156 RVA: 0x001D9114 File Offset: 0x001D7314
			public ArrayRecordValue(Keys fields, RecordTypeValue type, Value[] values)
			{
				this.fields = fields;
				this.type = type;
				this.values = values;
			}

			// Token: 0x17002515 RID: 9493
			// (get) Token: 0x06008D3D RID: 36157 RVA: 0x001D9131 File Offset: 0x001D7331
			public override Keys Keys
			{
				get
				{
					return this.fields;
				}
			}

			// Token: 0x17002516 RID: 9494
			// (get) Token: 0x06008D3E RID: 36158 RVA: 0x001D9139 File Offset: 0x001D7339
			public override TypeValue Type
			{
				get
				{
					if (this.type == null)
					{
						this.type = RecordTypeValue.New(this.fields);
					}
					return this.type;
				}
			}

			// Token: 0x06008D3F RID: 36159 RVA: 0x001AC6DA File Offset: 0x001AA8DA
			public sealed override IValueReference GetReference(int index)
			{
				return this[index];
			}

			// Token: 0x17002517 RID: 9495
			public sealed override Value this[int index]
			{
				get
				{
					if (index >= this.values.Length)
					{
						throw ValueException.RecordIndexOutOfRange(index, this);
					}
					return this.values[index];
				}
			}

			// Token: 0x04004CE8 RID: 19688
			private RecordTypeValue type;

			// Token: 0x04004CE9 RID: 19689
			private readonly Keys fields;

			// Token: 0x04004CEA RID: 19690
			private readonly Value[] values;
		}

		// Token: 0x020015ED RID: 5613
		private class ReferenceArrayRecordValue : RecordValue
		{
			// Token: 0x06008D41 RID: 36161 RVA: 0x001D9177 File Offset: 0x001D7377
			public ReferenceArrayRecordValue(Keys keys, RecordTypeValue type, IValueReference[] references)
			{
				this.keys = keys;
				this.type = type;
				this.references = references;
			}

			// Token: 0x17002518 RID: 9496
			// (get) Token: 0x06008D42 RID: 36162 RVA: 0x001D9194 File Offset: 0x001D7394
			public sealed override Keys Keys
			{
				get
				{
					return this.keys;
				}
			}

			// Token: 0x17002519 RID: 9497
			// (get) Token: 0x06008D43 RID: 36163 RVA: 0x001D919C File Offset: 0x001D739C
			public override TypeValue Type
			{
				get
				{
					if (this.type == null)
					{
						this.type = RecordTypeValue.New(this.keys);
					}
					return this.type;
				}
			}

			// Token: 0x1700251A RID: 9498
			public sealed override Value this[int index]
			{
				get
				{
					IValueReference reference = this.GetReference(index);
					if (!reference.Evaluated && !(reference is ExceptionValueReference))
					{
						try
						{
							this.references[index] = reference.Value;
						}
						catch (ValueException ex)
						{
							this.references[index] = new ExceptionValueReference(ex);
							throw;
						}
					}
					return reference.Value;
				}
			}

			// Token: 0x06008D45 RID: 36165 RVA: 0x001D921C File Offset: 0x001D741C
			public sealed override IValueReference GetReference(int index)
			{
				if (index >= this.references.Length)
				{
					throw ValueException.RecordIndexOutOfRange(index, this);
				}
				return this.references[index];
			}

			// Token: 0x04004CEB RID: 19691
			private RecordTypeValue type;

			// Token: 0x04004CEC RID: 19692
			private readonly Keys keys;

			// Token: 0x04004CED RID: 19693
			private readonly IValueReference[] references;
		}

		// Token: 0x020015EE RID: 5614
		private class DemandRecordValue : RecordValue
		{
			// Token: 0x06008D46 RID: 36166 RVA: 0x001D9239 File Offset: 0x001D7439
			public DemandRecordValue(Keys keys, RecordTypeValue type, Func<int, Value> getValue)
			{
				this.keys = keys;
				this.type = type;
				this.getValue = getValue;
			}

			// Token: 0x1700251B RID: 9499
			// (get) Token: 0x06008D47 RID: 36167 RVA: 0x001D9256 File Offset: 0x001D7456
			public override Keys Keys
			{
				get
				{
					return this.keys;
				}
			}

			// Token: 0x1700251C RID: 9500
			// (get) Token: 0x06008D48 RID: 36168 RVA: 0x001D925E File Offset: 0x001D745E
			public override TypeValue Type
			{
				get
				{
					if (this.type == null)
					{
						this.type = RecordTypeValue.New(this.keys);
					}
					return this.type;
				}
			}

			// Token: 0x06008D49 RID: 36169 RVA: 0x001D927F File Offset: 0x001D747F
			public override IValueReference GetReference(int index)
			{
				if (index >= this.keys.Length)
				{
					throw ValueException.RecordIndexOutOfRange(index, this);
				}
				if (this.values == null || this.values[index] == null)
				{
					return base.GetReference(index);
				}
				return this.values[index];
			}

			// Token: 0x1700251D RID: 9501
			public override Value this[int index]
			{
				get
				{
					if (index >= this.keys.Length)
					{
						throw ValueException.RecordIndexOutOfRange(index, this);
					}
					Value[] array = this.values;
					if (array == null)
					{
						array = new Value[this.keys.Length];
						this.values = array;
					}
					Value value = array[index];
					if (value == RecordValue.DemandRecordValue.recursionGuard)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.ValueException_CyclicReference, null, null);
					}
					if (value == null)
					{
						if (this.exceptions != null && this.exceptions[index] != null)
						{
							throw this.exceptions[index];
						}
						try
						{
							this.values[index] = RecordValue.DemandRecordValue.recursionGuard;
							value = this.getValue(index);
						}
						catch (ValueException ex)
						{
							if (this.exceptions == null)
							{
								this.exceptions = new ValueException[this.keys.Length];
							}
							this.exceptions[index] = ex;
							this.values[index] = null;
							throw;
						}
						catch
						{
							this.values[index] = null;
							throw;
						}
						this.values[index] = value;
					}
					return value;
				}
			}

			// Token: 0x04004CEE RID: 19694
			private static readonly Value recursionGuard = new RecordValue.DemandRecordValue.UniqueValue();

			// Token: 0x04004CEF RID: 19695
			private RecordTypeValue type;

			// Token: 0x04004CF0 RID: 19696
			private readonly Keys keys;

			// Token: 0x04004CF1 RID: 19697
			private readonly Func<int, Value> getValue;

			// Token: 0x04004CF2 RID: 19698
			private Value[] values;

			// Token: 0x04004CF3 RID: 19699
			private ValueException[] exceptions;

			// Token: 0x020015EF RID: 5615
			private class UniqueValue : NativeFunctionValue0
			{
				// Token: 0x06008D4C RID: 36172 RVA: 0x0000EE09 File Offset: 0x0000D009
				public override Value Invoke()
				{
					throw new InvalidOperationException();
				}
			}
		}

		// Token: 0x020015F0 RID: 5616
		private class MetaRecordValue : RecordValue
		{
			// Token: 0x06008D4E RID: 36174 RVA: 0x001D93D4 File Offset: 0x001D75D4
			public MetaRecordValue(RecordValue record, RecordValue metaValue)
			{
				this.record = record;
				this.metaValue = metaValue;
			}

			// Token: 0x1700251E RID: 9502
			// (get) Token: 0x06008D4F RID: 36175 RVA: 0x001D93EA File Offset: 0x001D75EA
			public override TypeValue Type
			{
				get
				{
					return this.record.Type;
				}
			}

			// Token: 0x06008D50 RID: 36176 RVA: 0x001D93F7 File Offset: 0x001D75F7
			public override Value NewType(TypeValue type)
			{
				return this.record.NewType(type).NewMeta(this.metaValue);
			}

			// Token: 0x1700251F RID: 9503
			// (get) Token: 0x06008D51 RID: 36177 RVA: 0x001D9410 File Offset: 0x001D7610
			public override RecordValue MetaValue
			{
				get
				{
					return this.metaValue;
				}
			}

			// Token: 0x06008D52 RID: 36178 RVA: 0x001D9418 File Offset: 0x001D7618
			public override Value NewMeta(RecordValue metaValue)
			{
				if (!metaValue.IsEmpty)
				{
					return new RecordValue.MetaRecordValue(this.record, metaValue);
				}
				return this.record;
			}

			// Token: 0x17002520 RID: 9504
			// (get) Token: 0x06008D53 RID: 36179 RVA: 0x001D9435 File Offset: 0x001D7635
			public override Keys Keys
			{
				get
				{
					return this.record.Keys;
				}
			}

			// Token: 0x17002521 RID: 9505
			public override Value this[int key]
			{
				get
				{
					return this.record[key];
				}
			}

			// Token: 0x17002522 RID: 9506
			public override Value this[string key]
			{
				get
				{
					return this.record[key];
				}
			}

			// Token: 0x17002523 RID: 9507
			public override Value this[Value key]
			{
				get
				{
					return this.record[key];
				}
			}

			// Token: 0x06008D57 RID: 36183 RVA: 0x001D946C File Offset: 0x001D766C
			public override IValueReference GetReference(int index)
			{
				return this.record.GetReference(index);
			}

			// Token: 0x04004CF4 RID: 19700
			private readonly RecordValue record;

			// Token: 0x04004CF5 RID: 19701
			private readonly RecordValue metaValue;
		}

		// Token: 0x020015F1 RID: 5617
		private class CastRecordValue : RecordValue
		{
			// Token: 0x06008D58 RID: 36184 RVA: 0x001D947A File Offset: 0x001D767A
			public CastRecordValue(RecordValue record, RecordTypeValue type)
			{
				this.record = record;
				this.type = type;
			}

			// Token: 0x17002524 RID: 9508
			// (get) Token: 0x06008D59 RID: 36185 RVA: 0x001D9490 File Offset: 0x001D7690
			public override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17002525 RID: 9509
			// (get) Token: 0x06008D5A RID: 36186 RVA: 0x001D9498 File Offset: 0x001D7698
			public override Keys Keys
			{
				get
				{
					return this.type.Fields.Keys;
				}
			}

			// Token: 0x17002526 RID: 9510
			// (get) Token: 0x06008D5B RID: 36187 RVA: 0x001D94AA File Offset: 0x001D76AA
			public override RecordValue MetaValue
			{
				get
				{
					return this.record.MetaValue;
				}
			}

			// Token: 0x06008D5C RID: 36188 RVA: 0x001D94B7 File Offset: 0x001D76B7
			public override IValueReference GetReference(int index)
			{
				return this.record.GetReference(index);
			}

			// Token: 0x17002527 RID: 9511
			public override Value this[int index]
			{
				get
				{
					return this.record[index];
				}
			}

			// Token: 0x04004CF6 RID: 19702
			private readonly RecordValue record;

			// Token: 0x04004CF7 RID: 19703
			private readonly RecordTypeValue type;
		}

		// Token: 0x020015F2 RID: 5618
		private class RecordValueReference : IValueReference
		{
			// Token: 0x06008D5E RID: 36190 RVA: 0x001D94D3 File Offset: 0x001D76D3
			public RecordValueReference(RecordValue map, int index)
			{
				this.map = map;
				this.index = index;
			}

			// Token: 0x17002528 RID: 9512
			// (get) Token: 0x06008D5F RID: 36191 RVA: 0x001D94E9 File Offset: 0x001D76E9
			public bool Evaluated
			{
				get
				{
					return this.value != null;
				}
			}

			// Token: 0x17002529 RID: 9513
			// (get) Token: 0x06008D60 RID: 36192 RVA: 0x001D94F4 File Offset: 0x001D76F4
			public Value Value
			{
				get
				{
					if (this.value == null)
					{
						this.value = this.map[this.index];
						this.map = null;
					}
					return this.value;
				}
			}

			// Token: 0x04004CF8 RID: 19704
			private RecordValue map;

			// Token: 0x04004CF9 RID: 19705
			private readonly int index;

			// Token: 0x04004CFA RID: 19706
			private Value value;
		}
	}
}
