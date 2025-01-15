using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Language;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200134F RID: 4943
	public sealed class IntEnumTypeValue<T> : TypeValue
	{
		// Token: 0x06008208 RID: 33288 RVA: 0x001B9CD7 File Offset: 0x001B7ED7
		public IntEnumTypeValue(string name)
		{
			this.name = name;
			this.type = TypeValue.Number;
			this.numberToValue = new Dictionary<NumberValue, T>();
			this.nameToNumber = new Dictionary<string, NumberValue>();
		}

		// Token: 0x06008209 RID: 33289 RVA: 0x001B9D08 File Offset: 0x001B7F08
		private IntEnumTypeValue(IntEnumTypeValue<T> type, RecordValue newMeta)
		{
			this.name = type.name;
			this.type = TypeValue.Number;
			this.numberToValue = type.numberToValue;
			this.nameToNumber = type.nameToNumber;
			this.meta = this.MetaValue.Concatenate(newMeta).AsRecord;
		}

		// Token: 0x0600820A RID: 33290 RVA: 0x001B9D64 File Offset: 0x001B7F64
		public NumberValue NewEnumValue(string name, int intValue, T value, string caption = null)
		{
			if (this.meta != null)
			{
				throw new InvalidOperationException("Enum type " + this.name + " is in use and cannot be modified.");
			}
			RecordBuilder recordBuilder = new RecordBuilder(2);
			recordBuilder.Add("Documentation.Name", TextValue.New(name), TypeValue.Text);
			if (caption != null)
			{
				recordBuilder.Add("Documentation.Caption", TextValue.New(caption), TypeValue.Text);
			}
			string @string = FunctionDescriptionStrings.ResourceManager.GetString(name.Replace(".", "_"));
			if (@string != null)
			{
				recordBuilder.Add("Documentation.Description", TextValue.New(@string), TypeValue.Text);
			}
			NumberValue numberValue = NumberValue.New(intValue);
			numberValue = numberValue.NewMeta(recordBuilder.ToRecord()).AsNumber;
			if (!this.numberToValue.ContainsKey(numberValue))
			{
				this.numberToValue.Add(numberValue, value);
			}
			this.nameToNumber.Add(name, numberValue);
			return numberValue;
		}

		// Token: 0x1700231F RID: 8991
		// (get) Token: 0x0600820B RID: 33291 RVA: 0x001B9E48 File Offset: 0x001B8048
		public override RecordValue MetaValue
		{
			get
			{
				if (this.meta == null)
				{
					NumberValue[] array = new NumberValue[this.nameToNumber.Count];
					int num = 0;
					foreach (NumberValue numberValue in this.nameToNumber.Values.OrderBy((NumberValue value) => value.AsInteger32))
					{
						array[num] = numberValue;
						num++;
					}
					this.meta = ValueHelper.AdjustEnumTypeMetavalues<NumberValue>(LibraryDescriptions.NewEnumType(this.name), array).MetaValue;
				}
				return this.meta;
			}
		}

		// Token: 0x17002320 RID: 8992
		// (get) Token: 0x0600820C RID: 33292 RVA: 0x001B9F00 File Offset: 0x001B8100
		public override ValueKind TypeKind
		{
			get
			{
				return this.type.TypeKind;
			}
		}

		// Token: 0x17002321 RID: 8993
		// (get) Token: 0x0600820D RID: 33293 RVA: 0x001B9F0D File Offset: 0x001B810D
		public override bool IsNullable
		{
			get
			{
				return this.type.IsNullable;
			}
		}

		// Token: 0x17002322 RID: 8994
		// (get) Token: 0x0600820E RID: 33294 RVA: 0x001B9F1A File Offset: 0x001B811A
		public override TypeValue NonNullable
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17002323 RID: 8995
		// (get) Token: 0x0600820F RID: 33295 RVA: 0x001B9F22 File Offset: 0x001B8122
		public override TypeValue Nullable
		{
			get
			{
				if (this.nullableType == null)
				{
					this.nullableType = this.type.Nullable.NewMeta(this.MetaValue).AsType;
				}
				return this.nullableType;
			}
		}

		// Token: 0x17002324 RID: 8996
		// (get) Token: 0x06008210 RID: 33296 RVA: 0x001B9F53 File Offset: 0x001B8153
		public override object TypeIdentity
		{
			get
			{
				return this.type.TypeIdentity;
			}
		}

		// Token: 0x06008211 RID: 33297 RVA: 0x001B9F60 File Offset: 0x001B8160
		public override Value NewMeta(RecordValue metaValue)
		{
			return new IntEnumTypeValue<T>(this, metaValue);
		}

		// Token: 0x06008212 RID: 33298 RVA: 0x001B9F69 File Offset: 0x001B8169
		public override bool IsCompatibleWith(TypeValue other)
		{
			return this.type.IsCompatibleWith(other);
		}

		// Token: 0x06008213 RID: 33299 RVA: 0x001B9F78 File Offset: 0x001B8178
		public NumberValue GetEnum(T value)
		{
			NumberValue numberValue;
			if (this.TryLookupEnum(value, out numberValue))
			{
				return numberValue;
			}
			throw ValueException.NewExpressionError<Message2>(Strings.InvalidEnumValue(this.name, string.Join(", ", this.nameToNumber.Keys.ToArray<string>())), TextValue.NewOrNull(value.ToString()), null);
		}

		// Token: 0x06008214 RID: 33300 RVA: 0x001B9FD0 File Offset: 0x001B81D0
		public T GetValue(NumberValue enumValue)
		{
			T t;
			if (this.numberToValue.TryGetValue(enumValue, out t))
			{
				return t;
			}
			throw this.InvalidEnumValue(enumValue);
		}

		// Token: 0x06008215 RID: 33301 RVA: 0x001B9FF6 File Offset: 0x001B81F6
		public bool TryGetValue(Value enumValue, out T value)
		{
			if (enumValue.IsNumber && this.numberToValue.TryGetValue(enumValue.AsNumber, out value))
			{
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x06008216 RID: 33302 RVA: 0x001BA020 File Offset: 0x001B8220
		public string GetName(NumberValue enumValue)
		{
			KeyValuePair<string, NumberValue> keyValuePair = this.nameToNumber.FirstOrDefault((KeyValuePair<string, NumberValue> kvp) => kvp.Value.AsInteger32 == enumValue.AsInteger32);
			if (!keyValuePair.Equals(default(KeyValuePair<string, NumberValue>)))
			{
				return keyValuePair.Key;
			}
			throw this.InvalidEnumValue(enumValue);
		}

		// Token: 0x06008217 RID: 33303 RVA: 0x001BA084 File Offset: 0x001B8284
		public bool TryLookupEnum(T value, out NumberValue enumValue)
		{
			if (typeof(T).IsValueType)
			{
				enumValue = this.numberToValue.FirstOrDefault(delegate(KeyValuePair<NumberValue, T> kvp)
				{
					ref T ptr = ref value;
					if (default(T) == null)
					{
						T value2 = value;
						ptr = ref value2;
					}
					return ptr.Equals(kvp.Value);
				}).Key;
			}
			else
			{
				enumValue = this.numberToValue.FirstOrDefault((KeyValuePair<NumberValue, T> kvp) => value == kvp.Value).Key;
			}
			return enumValue != null;
		}

		// Token: 0x06008218 RID: 33304 RVA: 0x001BA0F8 File Offset: 0x001B82F8
		private ValueException InvalidEnumValue(NumberValue enumValue)
		{
			return ValueException.NewExpressionError<Message2>(Strings.InvalidEnumValue(this.name, string.Join(", ", this.nameToNumber.Keys.ToArray<string>())), enumValue, null);
		}

		// Token: 0x040046D1 RID: 18129
		private readonly string name;

		// Token: 0x040046D2 RID: 18130
		private readonly TypeValue type;

		// Token: 0x040046D3 RID: 18131
		private readonly Dictionary<NumberValue, T> numberToValue;

		// Token: 0x040046D4 RID: 18132
		private readonly Dictionary<string, NumberValue> nameToNumber;

		// Token: 0x040046D5 RID: 18133
		private RecordValue meta;

		// Token: 0x040046D6 RID: 18134
		private TypeValue nullableType;
	}
}
