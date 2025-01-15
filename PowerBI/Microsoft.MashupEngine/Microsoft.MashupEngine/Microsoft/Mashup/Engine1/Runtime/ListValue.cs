using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200156E RID: 5486
	public abstract class ListValue : StructureValue, IListValue, IValue, IEnumerable<IValueReference>, IEnumerable
	{
		// Token: 0x170023E7 RID: 9191
		// (get) Token: 0x0600886A RID: 34922 RVA: 0x001CEC19 File Offset: 0x001CCE19
		public static long MaxCount
		{
			get
			{
				return 4503599627370496L;
			}
		}

		// Token: 0x0600886B RID: 34923 RVA: 0x001CEC24 File Offset: 0x001CCE24
		public static ListValue New(IEnumerable<IValueReference> enumerable)
		{
			return new ListValue.EnumerableListValue(enumerable);
		}

		// Token: 0x0600886C RID: 34924 RVA: 0x001CEC2C File Offset: 0x001CCE2C
		public static ListValue New(int length, Func<int, Value> getValue)
		{
			return new ListValue.DemandArrayListValue(length, getValue);
		}

		// Token: 0x0600886D RID: 34925 RVA: 0x001CEC35 File Offset: 0x001CCE35
		public static ListValue New(params IValueReference[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length == 0)
			{
				return ListValue.Empty;
			}
			return new ListValue.ReferenceArrayArrayListValue(values);
		}

		// Token: 0x0600886E RID: 34926 RVA: 0x001CEC55 File Offset: 0x001CCE55
		public static ListValue New(params Value[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length == 0)
			{
				return ListValue.Empty;
			}
			return new ListValue.ArrayArrayListValue(values);
		}

		// Token: 0x0600886F RID: 34927 RVA: 0x001CEC78 File Offset: 0x001CCE78
		public static ListValue New(string[] value)
		{
			Value[] array = new Value[value.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = TextValue.New(value[i]);
			}
			return ListValue.New(array);
		}

		// Token: 0x170023E8 RID: 9192
		// (get) Token: 0x06008870 RID: 34928
		public abstract long LargeCount { get; }

		// Token: 0x170023E9 RID: 9193
		// (get) Token: 0x06008871 RID: 34929
		public abstract int Count { get; }

		// Token: 0x170023EA RID: 9194
		// (get) Token: 0x06008872 RID: 34930 RVA: 0x001CECB0 File Offset: 0x001CCEB0
		public bool IsEmpty
		{
			get
			{
				if (this.IsBuffered)
				{
					return this.Count == 0;
				}
				bool flag;
				using (IEnumerator<IValueReference> enumerator = this.GetEnumerator())
				{
					flag = !enumerator.MoveNext();
				}
				return flag;
			}
		}

		// Token: 0x06008873 RID: 34931 RVA: 0x001CED00 File Offset: 0x001CCF00
		public sealed override string ToSource()
		{
			return "{...}";
		}

		// Token: 0x06008874 RID: 34932 RVA: 0x001CED07 File Offset: 0x001CCF07
		public sealed override string ToString()
		{
			return "List";
		}

		// Token: 0x06008875 RID: 34933 RVA: 0x001CED0E File Offset: 0x001CCF0E
		public sealed override object ToOleDb(Type type)
		{
			return ValueMarshaller.ToOleDbString("[List]", this, type);
		}

		// Token: 0x06008876 RID: 34934 RVA: 0x001CED1C File Offset: 0x001CCF1C
		public override void TestConnection()
		{
			using (IEnumerator<IValueReference> enumerator = this.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					enumerator.Current.Value.TestConnection();
				}
			}
		}

		// Token: 0x170023EB RID: 9195
		// (get) Token: 0x06008877 RID: 34935 RVA: 0x001CED68 File Offset: 0x001CCF68
		public override TypeValue Type
		{
			get
			{
				return TypeValue.List;
			}
		}

		// Token: 0x06008878 RID: 34936 RVA: 0x001CED6F File Offset: 0x001CCF6F
		public override Value NewType(TypeValue type)
		{
			return new ListValue.CastListValue(this, type.AsListType);
		}

		// Token: 0x170023EC RID: 9196
		// (get) Token: 0x06008879 RID: 34937 RVA: 0x0014025A File Offset: 0x0013E45A
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.List;
			}
		}

		// Token: 0x170023ED RID: 9197
		// (get) Token: 0x0600887A RID: 34938 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsList
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170023EE RID: 9198
		// (get) Token: 0x0600887B RID: 34939 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override ListValue AsList
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170023EF RID: 9199
		// (get) Token: 0x0600887C RID: 34940
		public abstract bool IsBuffered { get; }

		// Token: 0x170023F0 RID: 9200
		// (get) Token: 0x0600887D RID: 34941 RVA: 0x001CED7D File Offset: 0x001CCF7D
		public override bool IsDefaultType
		{
			get
			{
				return this.Type.AsListType.ItemType.Equals(TypeValue.Any);
			}
		}

		// Token: 0x0600887E RID: 34942
		public abstract IEnumerator<IValueReference> GetEnumerator();

		// Token: 0x0600887F RID: 34943 RVA: 0x001CED99 File Offset: 0x001CCF99
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170023F1 RID: 9201
		public override Value this[Value key]
		{
			get
			{
				return this[key.AsInteger32];
			}
		}

		// Token: 0x170023F2 RID: 9202
		public override Value this[string key]
		{
			get
			{
				throw ValueException.ElementAccessByKeyTypeMismatch(this, TextValue.New(key));
			}
		}

		// Token: 0x170023F3 RID: 9203
		public override Value this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw ValueException.StructureIndexCannotBeNegative(index, this);
				}
				foreach (IValueReference valueReference in this)
				{
					if (index == 0)
					{
						return valueReference.Value;
					}
					index--;
				}
				throw ValueException.InsufficientElements(this);
			}
		}

		// Token: 0x06008883 RID: 34947
		public abstract IValueReference GetReference(int index);

		// Token: 0x06008884 RID: 34948 RVA: 0x001CEE28 File Offset: 0x001CD028
		public override bool TryGetValue(Value indexValue, out Value value)
		{
			int num = indexValue.AsInteger32;
			if (num < 0)
			{
				throw ValueException.StructureIndexCannotBeNegative(num, this);
			}
			foreach (IValueReference valueReference in this)
			{
				if (num == 0)
				{
					value = valueReference.Value;
					return true;
				}
				num--;
			}
			value = Value.Null;
			return false;
		}

		// Token: 0x06008885 RID: 34949 RVA: 0x001CEDAF File Offset: 0x001CCFAF
		public override bool TryGetValue(string key, out Value value)
		{
			throw ValueException.ElementAccessByKeyTypeMismatch(this, TextValue.New(key));
		}

		// Token: 0x06008886 RID: 34950 RVA: 0x001CEE9C File Offset: 0x001CD09C
		public virtual Value[] ToArray()
		{
			List<Value> list = new List<Value>();
			foreach (IValueReference valueReference in this)
			{
				list.Add(valueReference.Value);
			}
			return list.ToArray();
		}

		// Token: 0x06008887 RID: 34951 RVA: 0x001CEEF8 File Offset: 0x001CD0F8
		public virtual RecordValue ToRecord(Keys keys)
		{
			List<IValueReference> list = new List<IValueReference>();
			foreach (IValueReference valueReference in this)
			{
				list.Add(valueReference);
			}
			return RecordValue.New(keys, list.ToArray());
		}

		// Token: 0x06008888 RID: 34952 RVA: 0x001CEF54 File Offset: 0x001CD154
		public virtual RecordValue ToRecord(RecordTypeValue type)
		{
			List<IValueReference> list = new List<IValueReference>();
			foreach (IValueReference valueReference in this)
			{
				list.Add(valueReference);
			}
			return RecordValue.New(type, list.ToArray());
		}

		// Token: 0x06008889 RID: 34953 RVA: 0x001CEFB0 File Offset: 0x001CD1B0
		public override Value Concatenate(Value value)
		{
			if (value.IsList)
			{
				return ListValue.Combine(ListValue.New(new Value[] { this, value }));
			}
			return base.Concatenate(value);
		}

		// Token: 0x0600888A RID: 34954 RVA: 0x001CEFDA File Offset: 0x001CD1DA
		public static ListValue Combine(ListValue lists)
		{
			return new ListValue.CombineListValue(lists);
		}

		// Token: 0x0600888B RID: 34955 RVA: 0x001CEFE4 File Offset: 0x001CD1E4
		public sealed override bool Equals(Value value, _ValueComparer comparer)
		{
			if (!value.IsList)
			{
				return false;
			}
			bool flag3;
			using (IEnumerator<IValueReference> enumerator = this.GetEnumerator())
			{
				using (IEnumerator<IValueReference> enumerator2 = value.AsList.GetEnumerator())
				{
					for (;;)
					{
						bool flag = enumerator.MoveNext();
						bool flag2 = enumerator2.MoveNext();
						if (!flag && !flag2)
						{
							break;
						}
						if (!flag || !flag2)
						{
							goto IL_003C;
						}
						if (!enumerator.Current.Value.Equals(enumerator2.Current.Value, comparer))
						{
							goto Block_8;
						}
					}
					return true;
					IL_003C:
					return false;
					Block_8:
					flag3 = false;
				}
			}
			return flag3;
		}

		// Token: 0x0600888C RID: 34956 RVA: 0x001CF088 File Offset: 0x001CD288
		public sealed override int GetHashCode(_ValueComparer comparer)
		{
			HashBuilder hashBuilder = default(HashBuilder);
			foreach (IValueReference valueReference in this)
			{
				hashBuilder.Add(valueReference.Value.GetHashCode(comparer));
			}
			return hashBuilder.ToHash();
		}

		// Token: 0x0600888D RID: 34957 RVA: 0x001CF0EC File Offset: 0x001CD2EC
		public virtual BinaryValue ToBinary()
		{
			return new ListValue.ListBinaryValue(this);
		}

		// Token: 0x0600888E RID: 34958 RVA: 0x001CF0F4 File Offset: 0x001CD2F4
		public TableValue ToTable()
		{
			TableValue tableValue;
			using (IEnumerator<IValueReference> enumerator = this.GetEnumerator())
			{
				Keys keys;
				if (enumerator.MoveNext())
				{
					keys = enumerator.Current.Value.AsRecord.Keys;
				}
				else
				{
					keys = Keys.Empty;
				}
				tableValue = this.ToTable(keys);
			}
			return tableValue;
		}

		// Token: 0x0600888F RID: 34959 RVA: 0x001CF154 File Offset: 0x001CD354
		public TableValue ToTable(Keys columns)
		{
			return this.ToTable(TableTypeValue.New(RecordTypeValue.New(columns)));
		}

		// Token: 0x06008890 RID: 34960 RVA: 0x001CF167 File Offset: 0x001CD367
		public TableValue ToTable(TableTypeValue type)
		{
			return new ListTableValue(this, type);
		}

		// Token: 0x06008891 RID: 34961 RVA: 0x001CF170 File Offset: 0x001CD370
		public bool TryGetStringList(int maxCount, out IList<string> strings)
		{
			strings = new List<string>(maxCount);
			foreach (IValueReference valueReference in this)
			{
				bool flag = false;
				try
				{
					if (strings.Count < maxCount && valueReference.Value.IsText)
					{
						strings.Add(valueReference.Value.AsText.String);
						flag = true;
					}
				}
				catch (ValueException)
				{
				}
				if (!flag)
				{
					strings = null;
					return false;
				}
			}
			return true;
		}

		// Token: 0x170023F4 RID: 9204
		// (get) Token: 0x06008892 RID: 34962 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x06008893 RID: 34963 RVA: 0x001CF20C File Offset: 0x001CD40C
		public override Value NewMeta(RecordValue metaValue)
		{
			if (!metaValue.IsEmpty)
			{
				return new ListValue.MetaListValue(this, metaValue);
			}
			return this;
		}

		// Token: 0x170023F5 RID: 9205
		// (get) Token: 0x06008894 RID: 34964 RVA: 0x001CF21F File Offset: 0x001CD41F
		int IListValue.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170023F6 RID: 9206
		IValue IListValue.this[int index]
		{
			get
			{
				return this[index];
			}
		}

		// Token: 0x06008896 RID: 34966 RVA: 0x001CF227 File Offset: 0x001CD427
		IEnumerator<IValueReference2> IListValue.GetEnumerator()
		{
			return new ListValue.ValueReference2Enumerator(this.GetEnumerator());
		}

		// Token: 0x06008897 RID: 34967 RVA: 0x001CF234 File Offset: 0x001CD434
		IEnumerable<IValueReference2> IListValue.GetEnumerable()
		{
			return this.Select((IValueReference item) => new ValueReference2(item));
		}

		// Token: 0x04004B93 RID: 19347
		private const string placeholder = "[List]";

		// Token: 0x04004B94 RID: 19348
		public static readonly TextValue Placeholder = TextValue.New("[List]");

		// Token: 0x04004B95 RID: 19349
		public static readonly ListValue Empty = new ListValue.ArrayArrayListValue(new Value[0]);

		// Token: 0x0200156F RID: 5487
		private class ListBinaryValue : StreamedBinaryValue
		{
			// Token: 0x0600889A RID: 34970 RVA: 0x001CF27C File Offset: 0x001CD47C
			public ListBinaryValue(ListValue list)
			{
				this.list = list;
			}

			// Token: 0x0600889B RID: 34971 RVA: 0x001CF28B File Offset: 0x001CD48B
			public override bool TryGetLength(out long length)
			{
				if (this.list.IsBuffered)
				{
					length = this.list.LargeCount;
					return true;
				}
				length = 0L;
				return false;
			}

			// Token: 0x0600889C RID: 34972 RVA: 0x001CF2AE File Offset: 0x001CD4AE
			public override ListValue ToList()
			{
				return this.list;
			}

			// Token: 0x0600889D RID: 34973 RVA: 0x001CF2B6 File Offset: 0x001CD4B6
			public override Stream Open()
			{
				return new EnumeratorStream(this.list.GetEnumerator());
			}

			// Token: 0x04004B96 RID: 19350
			private readonly ListValue list;
		}

		// Token: 0x02001570 RID: 5488
		private class ArrayArrayListValue : ArrayListValue
		{
			// Token: 0x0600889E RID: 34974 RVA: 0x001CF2C8 File Offset: 0x001CD4C8
			public ArrayArrayListValue(Value[] values)
			{
				this.values = values;
			}

			// Token: 0x170023F7 RID: 9207
			// (get) Token: 0x0600889F RID: 34975 RVA: 0x001CF2D7 File Offset: 0x001CD4D7
			public override int Count
			{
				get
				{
					return this.values.Length;
				}
			}

			// Token: 0x060088A0 RID: 34976 RVA: 0x001CF2E1 File Offset: 0x001CD4E1
			public override IValueReference GetReference(int index)
			{
				if (index < 0 || index >= this.values.Length)
				{
					throw ValueException.ListIndexOutOfRange(index, this);
				}
				return this.values[index];
			}

			// Token: 0x170023F8 RID: 9208
			public override Value this[int index]
			{
				get
				{
					if (index < 0 || index >= this.values.Length)
					{
						throw ValueException.ListIndexOutOfRange(index, this);
					}
					return this.values[index];
				}
			}

			// Token: 0x060088A2 RID: 34978 RVA: 0x001CF302 File Offset: 0x001CD502
			public override Value[] ToArray()
			{
				return this.values;
			}

			// Token: 0x060088A3 RID: 34979 RVA: 0x001CF30A File Offset: 0x001CD50A
			public override RecordValue ToRecord(Keys keys)
			{
				return RecordValue.New(keys, this.values);
			}

			// Token: 0x060088A4 RID: 34980 RVA: 0x001CF318 File Offset: 0x001CD518
			public override RecordValue ToRecord(RecordTypeValue type)
			{
				return RecordValue.New(type, this.values);
			}

			// Token: 0x04004B97 RID: 19351
			private readonly Value[] values;
		}

		// Token: 0x02001571 RID: 5489
		private class ReferenceArrayArrayListValue : ArrayListValue
		{
			// Token: 0x060088A5 RID: 34981 RVA: 0x001CF326 File Offset: 0x001CD526
			public ReferenceArrayArrayListValue(IValueReference[] values)
			{
				this.values = values;
			}

			// Token: 0x170023F9 RID: 9209
			// (get) Token: 0x060088A6 RID: 34982 RVA: 0x001CF335 File Offset: 0x001CD535
			public override int Count
			{
				get
				{
					return this.values.Length;
				}
			}

			// Token: 0x060088A7 RID: 34983 RVA: 0x001CF33F File Offset: 0x001CD53F
			public override IValueReference GetReference(int index)
			{
				if (index < 0 || index >= this.values.Length)
				{
					throw ValueException.ListIndexOutOfRange(index, this);
				}
				return this.values[index];
			}

			// Token: 0x170023FA RID: 9210
			public override Value this[int index]
			{
				get
				{
					if (index < 0 || index >= this.values.Length)
					{
						throw ValueException.ListIndexOutOfRange(index, this);
					}
					return this.values[index].Value;
				}
			}

			// Token: 0x060088A9 RID: 34985 RVA: 0x001CF388 File Offset: 0x001CD588
			public override Value[] ToArray()
			{
				Value[] array = new Value[this.values.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.values[i].Value;
				}
				return array;
			}

			// Token: 0x060088AA RID: 34986 RVA: 0x001CF3C2 File Offset: 0x001CD5C2
			public override RecordValue ToRecord(Keys keys)
			{
				return RecordValue.New(keys, this.values);
			}

			// Token: 0x060088AB RID: 34987 RVA: 0x001CF3D0 File Offset: 0x001CD5D0
			public override RecordValue ToRecord(RecordTypeValue type)
			{
				return RecordValue.New(type, this.values);
			}

			// Token: 0x04004B98 RID: 19352
			private readonly IValueReference[] values;
		}

		// Token: 0x02001572 RID: 5490
		private class DemandArrayListValue : ArrayListValue
		{
			// Token: 0x060088AC RID: 34988 RVA: 0x001CF3DE File Offset: 0x001CD5DE
			public DemandArrayListValue(int length, Func<int, Value> getValue)
			{
				this.values = new Value[length];
				this.getValue = getValue;
			}

			// Token: 0x170023FB RID: 9211
			// (get) Token: 0x060088AD RID: 34989 RVA: 0x001CF3F9 File Offset: 0x001CD5F9
			public override int Count
			{
				get
				{
					return this.values.Length;
				}
			}

			// Token: 0x170023FC RID: 9212
			public override Value this[int index]
			{
				get
				{
					if (index >= this.values.Length)
					{
						throw ValueException.ListIndexOutOfRange(index, this);
					}
					Value value = this.values[index];
					if (value == null)
					{
						value = this.getValue(index);
						this.values[index] = value;
					}
					return value;
				}
			}

			// Token: 0x04004B99 RID: 19353
			private readonly Func<int, Value> getValue;

			// Token: 0x04004B9A RID: 19354
			private readonly Value[] values;
		}

		// Token: 0x02001573 RID: 5491
		private class MetaListValue : ListValue
		{
			// Token: 0x060088AF RID: 34991 RVA: 0x001CF447 File Offset: 0x001CD647
			public MetaListValue(ListValue list, RecordValue metaValue)
			{
				this.list = list;
				this.metaValue = metaValue;
			}

			// Token: 0x060088B0 RID: 34992 RVA: 0x001CF45D File Offset: 0x001CD65D
			public override bool TryGetProcessor(out QueryProcessor processor)
			{
				return this.list.TryGetProcessor(out processor);
			}

			// Token: 0x170023FD RID: 9213
			// (get) Token: 0x060088B1 RID: 34993 RVA: 0x001CF46B File Offset: 0x001CD66B
			public override TypeValue Type
			{
				get
				{
					return this.list.Type;
				}
			}

			// Token: 0x060088B2 RID: 34994 RVA: 0x001CF478 File Offset: 0x001CD678
			public override Value NewType(TypeValue type)
			{
				return this.list.NewType(type).NewMeta(this.metaValue);
			}

			// Token: 0x170023FE RID: 9214
			// (get) Token: 0x060088B3 RID: 34995 RVA: 0x001CF491 File Offset: 0x001CD691
			public override RecordValue MetaValue
			{
				get
				{
					return this.metaValue;
				}
			}

			// Token: 0x060088B4 RID: 34996 RVA: 0x001CF499 File Offset: 0x001CD699
			public override Value NewMeta(RecordValue metaValue)
			{
				if (!metaValue.IsEmpty)
				{
					return new ListValue.MetaListValue(this.list, metaValue);
				}
				return this.list;
			}

			// Token: 0x170023FF RID: 9215
			// (get) Token: 0x060088B5 RID: 34997 RVA: 0x001CF4B6 File Offset: 0x001CD6B6
			public override bool IsBuffered
			{
				get
				{
					return this.list.IsBuffered;
				}
			}

			// Token: 0x17002400 RID: 9216
			// (get) Token: 0x060088B6 RID: 34998 RVA: 0x001CF4C3 File Offset: 0x001CD6C3
			public override int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			// Token: 0x17002401 RID: 9217
			// (get) Token: 0x060088B7 RID: 34999 RVA: 0x001CF4D0 File Offset: 0x001CD6D0
			public override long LargeCount
			{
				get
				{
					return this.list.LargeCount;
				}
			}

			// Token: 0x060088B8 RID: 35000 RVA: 0x001CF4DD File Offset: 0x001CD6DD
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			// Token: 0x17002402 RID: 9218
			public override Value this[int key]
			{
				get
				{
					return this.list[key];
				}
			}

			// Token: 0x17002403 RID: 9219
			public override Value this[Value key]
			{
				get
				{
					return this.list[key];
				}
			}

			// Token: 0x060088BB RID: 35003 RVA: 0x001CF506 File Offset: 0x001CD706
			public override bool TryGetValue(Value key, out Value value)
			{
				return this.list.TryGetValue(key, out value);
			}

			// Token: 0x060088BC RID: 35004 RVA: 0x001CF515 File Offset: 0x001CD715
			public override IValueReference GetReference(int index)
			{
				return this.list.GetReference(index);
			}

			// Token: 0x04004B9B RID: 19355
			private readonly ListValue list;

			// Token: 0x04004B9C RID: 19356
			private readonly RecordValue metaValue;
		}

		// Token: 0x02001574 RID: 5492
		private class CastListValue : ListValue
		{
			// Token: 0x060088BD RID: 35005 RVA: 0x001CF523 File Offset: 0x001CD723
			public CastListValue(ListValue list, ListTypeValue type)
			{
				this.list = list;
				this.type = type;
			}

			// Token: 0x060088BE RID: 35006 RVA: 0x001CF539 File Offset: 0x001CD739
			public override bool TryGetProcessor(out QueryProcessor processor)
			{
				return this.list.TryGetProcessor(out processor);
			}

			// Token: 0x17002404 RID: 9220
			// (get) Token: 0x060088BF RID: 35007 RVA: 0x001CF547 File Offset: 0x001CD747
			public override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17002405 RID: 9221
			// (get) Token: 0x060088C0 RID: 35008 RVA: 0x001CF54F File Offset: 0x001CD74F
			public override RecordValue MetaValue
			{
				get
				{
					return this.list.MetaValue;
				}
			}

			// Token: 0x17002406 RID: 9222
			// (get) Token: 0x060088C1 RID: 35009 RVA: 0x001CF55C File Offset: 0x001CD75C
			public override int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			// Token: 0x17002407 RID: 9223
			// (get) Token: 0x060088C2 RID: 35010 RVA: 0x001CF569 File Offset: 0x001CD769
			public override long LargeCount
			{
				get
				{
					return this.list.LargeCount;
				}
			}

			// Token: 0x17002408 RID: 9224
			// (get) Token: 0x060088C3 RID: 35011 RVA: 0x001CF576 File Offset: 0x001CD776
			public override bool IsBuffered
			{
				get
				{
					return this.list.IsBuffered;
				}
			}

			// Token: 0x17002409 RID: 9225
			public override Value this[int index]
			{
				get
				{
					return this.list[index];
				}
			}

			// Token: 0x060088C5 RID: 35013 RVA: 0x001CF591 File Offset: 0x001CD791
			public override IValueReference GetReference(int index)
			{
				return this.list.GetReference(index);
			}

			// Token: 0x060088C6 RID: 35014 RVA: 0x001CF59F File Offset: 0x001CD79F
			public override bool TryGetValue(Value key, out Value value)
			{
				return this.list.TryGetValue(key, out value);
			}

			// Token: 0x060088C7 RID: 35015 RVA: 0x001CF5AE File Offset: 0x001CD7AE
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			// Token: 0x04004B9D RID: 19357
			private readonly ListValue list;

			// Token: 0x04004B9E RID: 19358
			private readonly ListTypeValue type;
		}

		// Token: 0x02001575 RID: 5493
		private class EnumerableListValue : StreamedListValue
		{
			// Token: 0x060088C8 RID: 35016 RVA: 0x001CF5BB File Offset: 0x001CD7BB
			public EnumerableListValue(IEnumerable<IValueReference> enumerable)
			{
				this.enumerable = enumerable;
			}

			// Token: 0x060088C9 RID: 35017 RVA: 0x001CF5CA File Offset: 0x001CD7CA
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.enumerable.GetEnumerator();
			}

			// Token: 0x04004B9F RID: 19359
			private readonly IEnumerable<IValueReference> enumerable;
		}

		// Token: 0x02001576 RID: 5494
		private class CombineListValue : StreamedListValue
		{
			// Token: 0x060088CA RID: 35018 RVA: 0x001CF5D7 File Offset: 0x001CD7D7
			public CombineListValue(ListValue lists)
			{
				this.lists = lists;
			}

			// Token: 0x1700240A RID: 9226
			// (get) Token: 0x060088CB RID: 35019 RVA: 0x001CF5E8 File Offset: 0x001CD7E8
			public override TypeValue Type
			{
				get
				{
					if (this.type == null)
					{
						TypeValue typeValue = TypeValue.None;
						foreach (IValueReference valueReference in this.lists)
						{
							typeValue = TypeAlgebra.Union(typeValue, valueReference.Value.AsList.Type.AsListType.ItemType);
						}
						this.type = ListTypeValue.New(typeValue);
					}
					return this.type;
				}
			}

			// Token: 0x060088CC RID: 35020 RVA: 0x001CF670 File Offset: 0x001CD870
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return new ListValue.CombineListValue.CombineEnumerator(this.lists.GetEnumerator());
			}

			// Token: 0x04004BA0 RID: 19360
			private readonly ListValue lists;

			// Token: 0x04004BA1 RID: 19361
			private TypeValue type;

			// Token: 0x02001577 RID: 5495
			private class CombineEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x060088CD RID: 35021 RVA: 0x001CF682 File Offset: 0x001CD882
				public CombineEnumerator(IEnumerator<IValueReference> listEnumerator)
				{
					this.listEnumerator = listEnumerator;
					this.enumerator = ListValue.Empty.GetEnumerator();
				}

				// Token: 0x1700240B RID: 9227
				// (get) Token: 0x060088CE RID: 35022 RVA: 0x001CF6A1 File Offset: 0x001CD8A1
				public IValueReference Current
				{
					get
					{
						return this.enumerator.Current;
					}
				}

				// Token: 0x1700240C RID: 9228
				// (get) Token: 0x060088CF RID: 35023 RVA: 0x001CF6AE File Offset: 0x001CD8AE
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060088D0 RID: 35024 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x060088D1 RID: 35025 RVA: 0x001CF6B6 File Offset: 0x001CD8B6
				public void Dispose()
				{
					this.listEnumerator.Dispose();
					this.enumerator.Dispose();
				}

				// Token: 0x060088D2 RID: 35026 RVA: 0x001CF6D0 File Offset: 0x001CD8D0
				public bool MoveNext()
				{
					while (!this.enumerator.MoveNext())
					{
						if (!this.listEnumerator.MoveNext())
						{
							return false;
						}
						this.enumerator.Dispose();
						this.enumerator = this.listEnumerator.Current.Value.AsList.GetEnumerator();
					}
					return true;
				}

				// Token: 0x04004BA2 RID: 19362
				private readonly IEnumerator<IValueReference> listEnumerator;

				// Token: 0x04004BA3 RID: 19363
				private IEnumerator<IValueReference> enumerator;
			}
		}

		// Token: 0x02001578 RID: 5496
		private class ValueReference2Enumerator : IEnumerator<IValueReference2>, IDisposable, IEnumerator
		{
			// Token: 0x060088D3 RID: 35027 RVA: 0x001CF727 File Offset: 0x001CD927
			public ValueReference2Enumerator(IEnumerator<IValueReference> enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x1700240D RID: 9229
			// (get) Token: 0x060088D4 RID: 35028 RVA: 0x001CF736 File Offset: 0x001CD936
			public IValueReference2 Current
			{
				get
				{
					return new ValueReference2(this.enumerator.Current);
				}
			}

			// Token: 0x060088D5 RID: 35029 RVA: 0x001CF748 File Offset: 0x001CD948
			public void Dispose()
			{
				this.enumerator.Dispose();
			}

			// Token: 0x1700240E RID: 9230
			// (get) Token: 0x060088D6 RID: 35030 RVA: 0x001CF755 File Offset: 0x001CD955
			object IEnumerator.Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x060088D7 RID: 35031 RVA: 0x001CF762 File Offset: 0x001CD962
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x060088D8 RID: 35032 RVA: 0x001CF76F File Offset: 0x001CD96F
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x04004BA4 RID: 19364
			private readonly IEnumerator<IValueReference> enumerator;
		}
	}
}
