using System;
using System.Diagnostics;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200169B RID: 5787
	public abstract class Value : IValue, IValueReference, IEquatable<Value>, IComparable<Value>
	{
		// Token: 0x17002678 RID: 9848
		// (get) Token: 0x06009268 RID: 37480 RVA: 0x001E578E File Offset: 0x001E398E
		public static Value Null
		{
			get
			{
				return NullValue.Instance;
			}
		}

		// Token: 0x17002679 RID: 9849
		// (get) Token: 0x06009269 RID: 37481
		public abstract bool IsDefaultType { get; }

		// Token: 0x1700267A RID: 9850
		// (get) Token: 0x0600926A RID: 37482
		public abstract TypeValue Type { get; }

		// Token: 0x1700267B RID: 9851
		// (get) Token: 0x0600926B RID: 37483
		public abstract ValueKind Kind { get; }

		// Token: 0x1700267C RID: 9852
		// (get) Token: 0x0600926C RID: 37484 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsNull
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700267D RID: 9853
		// (get) Token: 0x0600926D RID: 37485 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsLogical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700267E RID: 9854
		// (get) Token: 0x0600926E RID: 37486 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsNumber
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700267F RID: 9855
		// (get) Token: 0x0600926F RID: 37487 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsDate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002680 RID: 9856
		// (get) Token: 0x06009270 RID: 37488 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsDateTime
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002681 RID: 9857
		// (get) Token: 0x06009271 RID: 37489 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsDateTimeZone
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002682 RID: 9858
		// (get) Token: 0x06009272 RID: 37490 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsTime
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002683 RID: 9859
		// (get) Token: 0x06009273 RID: 37491 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsDuration
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002684 RID: 9860
		// (get) Token: 0x06009274 RID: 37492 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002685 RID: 9861
		// (get) Token: 0x06009275 RID: 37493 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsFunction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002686 RID: 9862
		// (get) Token: 0x06009276 RID: 37494 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsList
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002687 RID: 9863
		// (get) Token: 0x06009277 RID: 37495 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsTable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002688 RID: 9864
		// (get) Token: 0x06009278 RID: 37496 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002689 RID: 9865
		// (get) Token: 0x06009279 RID: 37497 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsBinary
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700268A RID: 9866
		// (get) Token: 0x0600927A RID: 37498 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700268B RID: 9867
		// (get) Token: 0x0600927B RID: 37499 RVA: 0x00002105 File Offset: 0x00000305
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual bool IsAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600927C RID: 37500 RVA: 0x001E5795 File Offset: 0x001E3995
		public bool Is(TypeValue type)
		{
			if (this.IsNull)
			{
				return type.IsNullable;
			}
			return type.TypeKind == ValueKind.Any || type.TypeKind == this.Kind;
		}

		// Token: 0x0600927D RID: 37501 RVA: 0x001E57BF File Offset: 0x001E39BF
		public Value As(TypeValue type)
		{
			if (this.Is(type))
			{
				return this;
			}
			throw ValueException.CastTypeMismatch(this, type);
		}

		// Token: 0x0600927E RID: 37502 RVA: 0x001E57D3 File Offset: 0x001E39D3
		public T As<T>(TypeValue type) where T : Value
		{
			T t;
			if ((t = this.As(type) as T) == null)
			{
				t = this.Cast<T>(type);
			}
			return t;
		}

		// Token: 0x0600927F RID: 37503 RVA: 0x001E57F6 File Offset: 0x001E39F6
		protected virtual T Cast<T>(TypeValue type) where T : Value
		{
			throw ValueException.CastTypeMismatch(this, type);
		}

		// Token: 0x06009280 RID: 37504 RVA: 0x00182EDC File Offset: 0x001810DC
		public virtual bool TryGetAs<T>(out T contract) where T : class
		{
			contract = this as T;
			return contract != null;
		}

		// Token: 0x06009281 RID: 37505 RVA: 0x001E5800 File Offset: 0x001E3A00
		public bool Is<T>() where T : class
		{
			T t;
			return this.TryGetAs<T>(out t);
		}

		// Token: 0x06009282 RID: 37506 RVA: 0x001E5815 File Offset: 0x001E3A15
		public virtual Value ReplaceType(TypeValue type)
		{
			if (type.TypeKind == ValueKind.Any)
			{
				throw ValueException.CastTypeMismatch(this, type);
			}
			return this.As(type).NewType(type);
		}

		// Token: 0x06009283 RID: 37507 RVA: 0x001E57F6 File Offset: 0x001E39F6
		public virtual Value NewType(TypeValue type)
		{
			throw ValueException.CastTypeMismatch(this, type);
		}

		// Token: 0x1700268C RID: 9868
		// (get) Token: 0x06009284 RID: 37508 RVA: 0x001E5835 File Offset: 0x001E3A35
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual LogicalValue AsLogical
		{
			get
			{
				return this.As<LogicalValue>(TypeValue.Logical);
			}
		}

		// Token: 0x1700268D RID: 9869
		// (get) Token: 0x06009285 RID: 37509 RVA: 0x001E5842 File Offset: 0x001E3A42
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual NumberValue AsNumber
		{
			get
			{
				return this.As<NumberValue>(TypeValue.Number);
			}
		}

		// Token: 0x1700268E RID: 9870
		// (get) Token: 0x06009286 RID: 37510 RVA: 0x001E584F File Offset: 0x001E3A4F
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual DateValue AsDate
		{
			get
			{
				return this.As<DateValue>(TypeValue.Date);
			}
		}

		// Token: 0x1700268F RID: 9871
		// (get) Token: 0x06009287 RID: 37511 RVA: 0x001E585C File Offset: 0x001E3A5C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual DateTimeValue AsDateTime
		{
			get
			{
				return this.As<DateTimeValue>(TypeValue.DateTime);
			}
		}

		// Token: 0x17002690 RID: 9872
		// (get) Token: 0x06009288 RID: 37512 RVA: 0x001E5869 File Offset: 0x001E3A69
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual DateTimeZoneValue AsDateTimeZone
		{
			get
			{
				return this.As<DateTimeZoneValue>(TypeValue.DateTimeZone);
			}
		}

		// Token: 0x17002691 RID: 9873
		// (get) Token: 0x06009289 RID: 37513 RVA: 0x001E5876 File Offset: 0x001E3A76
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual TimeValue AsTime
		{
			get
			{
				return this.As<TimeValue>(TypeValue.Time);
			}
		}

		// Token: 0x17002692 RID: 9874
		// (get) Token: 0x0600928A RID: 37514 RVA: 0x001E5883 File Offset: 0x001E3A83
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual DurationValue AsDuration
		{
			get
			{
				return this.As<DurationValue>(TypeValue.Duration);
			}
		}

		// Token: 0x17002693 RID: 9875
		// (get) Token: 0x0600928B RID: 37515 RVA: 0x001E5890 File Offset: 0x001E3A90
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual RecordValue AsRecord
		{
			get
			{
				return this.As<RecordValue>(TypeValue.Record);
			}
		}

		// Token: 0x17002694 RID: 9876
		// (get) Token: 0x0600928C RID: 37516 RVA: 0x001E589D File Offset: 0x001E3A9D
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual FunctionValue AsFunction
		{
			get
			{
				return this.As<FunctionValue>(TypeValue.Function);
			}
		}

		// Token: 0x17002695 RID: 9877
		// (get) Token: 0x0600928D RID: 37517 RVA: 0x001E58AA File Offset: 0x001E3AAA
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual ListValue AsList
		{
			get
			{
				return this.As<ListValue>(TypeValue.List);
			}
		}

		// Token: 0x17002696 RID: 9878
		// (get) Token: 0x0600928E RID: 37518 RVA: 0x001E58B7 File Offset: 0x001E3AB7
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual TableValue AsTable
		{
			get
			{
				return this.As<TableValue>(TypeValue.Table);
			}
		}

		// Token: 0x17002697 RID: 9879
		// (get) Token: 0x0600928F RID: 37519 RVA: 0x001E58C4 File Offset: 0x001E3AC4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual TextValue AsText
		{
			get
			{
				return this.As<TextValue>(TypeValue.Text);
			}
		}

		// Token: 0x17002698 RID: 9880
		// (get) Token: 0x06009290 RID: 37520 RVA: 0x001E58D1 File Offset: 0x001E3AD1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual BinaryValue AsBinary
		{
			get
			{
				return this.As<BinaryValue>(TypeValue.Binary);
			}
		}

		// Token: 0x17002699 RID: 9881
		// (get) Token: 0x06009291 RID: 37521 RVA: 0x001E58DE File Offset: 0x001E3ADE
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual TypeValue AsType
		{
			get
			{
				return this.As<TypeValue>(TypeValue._Type);
			}
		}

		// Token: 0x1700269A RID: 9882
		// (get) Token: 0x06009292 RID: 37522 RVA: 0x001E58EB File Offset: 0x001E3AEB
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual ActionValue AsAction
		{
			get
			{
				return this.As<ActionValue>(TypeValue.Action);
			}
		}

		// Token: 0x1700269B RID: 9883
		// (get) Token: 0x06009293 RID: 37523 RVA: 0x001E58F8 File Offset: 0x001E3AF8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool AsBoolean
		{
			get
			{
				return this.AsLogical.Boolean;
			}
		}

		// Token: 0x1700269C RID: 9884
		// (get) Token: 0x06009294 RID: 37524 RVA: 0x001E5905 File Offset: 0x001E3B05
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string AsString
		{
			get
			{
				return this.AsText.String;
			}
		}

		// Token: 0x1700269D RID: 9885
		// (get) Token: 0x06009295 RID: 37525 RVA: 0x001E5914 File Offset: 0x001E3B14
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public char AsCharacter
		{
			get
			{
				string asString = this.AsString;
				if (asString.Length == 1)
				{
					return asString[0];
				}
				throw ValueException.NewExpressionError<Message0>(Strings.ValueException_CharacterError, RecordValue.New(Keys.New("Value"), new Value[] { this }), null);
			}
		}

		// Token: 0x1700269E RID: 9886
		// (get) Token: 0x06009296 RID: 37526 RVA: 0x001E595D File Offset: 0x001E3B5D
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int AsInteger32
		{
			get
			{
				return this.AsNumber.AsInteger32;
			}
		}

		// Token: 0x1700269F RID: 9887
		// (get) Token: 0x06009297 RID: 37527 RVA: 0x001E596A File Offset: 0x001E3B6A
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public double AsScientific64
		{
			get
			{
				return this.AsNumber.AsDouble;
			}
		}

		// Token: 0x170026A0 RID: 9888
		// (get) Token: 0x06009298 RID: 37528 RVA: 0x001E5977 File Offset: 0x001E3B77
		public virtual RecordValue MetaValue
		{
			get
			{
				throw ValueException.UnaryOperatorTypeMismatch("meta", this);
			}
		}

		// Token: 0x06009299 RID: 37529 RVA: 0x001E5984 File Offset: 0x001E3B84
		public virtual Value NewMeta(RecordValue metaValue)
		{
			throw ValueException.BinaryOperatorTypeMismatch("meta", this, metaValue);
		}

		// Token: 0x170026A1 RID: 9889
		// (get) Token: 0x0600929A RID: 37530 RVA: 0x001E5992 File Offset: 0x001E3B92
		public Value SubtractMetaValue
		{
			get
			{
				return this.NewMeta(RecordValue.Empty);
			}
		}

		// Token: 0x0600929B RID: 37531 RVA: 0x001E599F File Offset: 0x001E3B9F
		public virtual bool TryGetMetaField(string identifier, out Value value)
		{
			return this.MetaValue.TryGetValue(identifier, out value);
		}

		// Token: 0x170026A2 RID: 9890
		// (get) Token: 0x0600929C RID: 37532 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual IExpression Expression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600929D RID: 37533 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetProcessor(out QueryProcessor processor)
		{
			processor = null;
			return false;
		}

		// Token: 0x0600929E RID: 37534 RVA: 0x001E59AE File Offset: 0x001E3BAE
		public bool LessThan(Value value)
		{
			return this.CompareTo(value) < 0;
		}

		// Token: 0x0600929F RID: 37535 RVA: 0x001E59BA File Offset: 0x001E3BBA
		public bool LessThanOrEqual(Value value)
		{
			return this.CompareTo(value) <= 0;
		}

		// Token: 0x060092A0 RID: 37536 RVA: 0x001E59C9 File Offset: 0x001E3BC9
		public bool GreaterThan(Value value)
		{
			return this.CompareTo(value) > 0;
		}

		// Token: 0x060092A1 RID: 37537 RVA: 0x001E59D5 File Offset: 0x001E3BD5
		public bool GreaterThanOrEqual(Value value)
		{
			return this.CompareTo(value) >= 0;
		}

		// Token: 0x060092A2 RID: 37538
		public abstract Value NullableGreaterThan(Value value);

		// Token: 0x060092A3 RID: 37539
		public abstract Value NullableLessThan(Value value);

		// Token: 0x060092A4 RID: 37540
		public abstract Value NullableGreaterThanOrEqual(Value value);

		// Token: 0x060092A5 RID: 37541
		public abstract Value NullableLessThanOrEqual(Value value);

		// Token: 0x060092A6 RID: 37542 RVA: 0x001E59E4 File Offset: 0x001E3BE4
		public sealed override bool Equals(object obj)
		{
			Value value = obj as Value;
			return value != null && this.Equals(value);
		}

		// Token: 0x060092A7 RID: 37543 RVA: 0x001E5A04 File Offset: 0x001E3C04
		public bool Equals(Value value)
		{
			return this.Equals(value, _ValueComparer.StrictDefault);
		}

		// Token: 0x060092A8 RID: 37544
		public abstract bool Equals(Value value, _ValueComparer comparer);

		// Token: 0x060092A9 RID: 37545 RVA: 0x001E5A12 File Offset: 0x001E3C12
		public override int GetHashCode()
		{
			return _ValueComparer.StrictDefault.GetHashCode(this);
		}

		// Token: 0x060092AA RID: 37546
		public abstract int GetHashCode(_ValueComparer comparer);

		// Token: 0x060092AB RID: 37547 RVA: 0x001E5A1F File Offset: 0x001E3C1F
		public int CompareTo(Value value)
		{
			return this.CompareTo(value, _ValueComparer.StrictDefault);
		}

		// Token: 0x060092AC RID: 37548 RVA: 0x001E5A2D File Offset: 0x001E3C2D
		public virtual int CompareTo(Value value, _ValueComparer comparer)
		{
			return comparer.CompareKinds(this, value);
		}

		// Token: 0x060092AD RID: 37549 RVA: 0x001E5A37 File Offset: 0x001E3C37
		public virtual Value Identity()
		{
			throw ValueException.UnaryOperatorTypeMismatch("+", this);
		}

		// Token: 0x060092AE RID: 37550 RVA: 0x001E5A44 File Offset: 0x001E3C44
		public virtual Value Negate()
		{
			throw ValueException.UnaryOperatorTypeMismatch("-", this);
		}

		// Token: 0x060092AF RID: 37551 RVA: 0x001E5A51 File Offset: 0x001E3C51
		public virtual Value Not()
		{
			throw ValueException.UnaryOperatorTypeMismatch("not", this);
		}

		// Token: 0x060092B0 RID: 37552 RVA: 0x001E5A5E File Offset: 0x001E3C5E
		public virtual Value BitwiseNot()
		{
			throw ValueException.UnaryOperatorTypeMismatch("bitwise not", this);
		}

		// Token: 0x060092B1 RID: 37553 RVA: 0x001E5A6B File Offset: 0x001E3C6B
		public virtual Value Add(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("+", this, value);
		}

		// Token: 0x060092B2 RID: 37554 RVA: 0x001E5A79 File Offset: 0x001E3C79
		public virtual Value Subtract(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("-", this, value);
		}

		// Token: 0x060092B3 RID: 37555 RVA: 0x001E5A87 File Offset: 0x001E3C87
		public virtual Value Multiply(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("*", this, value);
		}

		// Token: 0x060092B4 RID: 37556 RVA: 0x001E5A95 File Offset: 0x001E3C95
		public virtual Value Divide(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("/", this, value);
		}

		// Token: 0x060092B5 RID: 37557 RVA: 0x001E5AA3 File Offset: 0x001E3CA3
		public virtual Value Mod(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("modulus", this, value);
		}

		// Token: 0x060092B6 RID: 37558 RVA: 0x001E5AB1 File Offset: 0x001E3CB1
		public virtual Value IntegerDivide(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("integer divide", this, value);
		}

		// Token: 0x060092B7 RID: 37559 RVA: 0x001E5A6B File Offset: 0x001E3C6B
		public virtual Value Add(Value value, Precision precision)
		{
			throw ValueException.BinaryOperatorTypeMismatch("+", this, value);
		}

		// Token: 0x060092B8 RID: 37560 RVA: 0x001E5A79 File Offset: 0x001E3C79
		public virtual Value Subtract(Value value, Precision precision)
		{
			throw ValueException.BinaryOperatorTypeMismatch("-", this, value);
		}

		// Token: 0x060092B9 RID: 37561 RVA: 0x001E5A87 File Offset: 0x001E3C87
		public virtual Value Multiply(Value value, Precision precision)
		{
			throw ValueException.BinaryOperatorTypeMismatch("*", this, value);
		}

		// Token: 0x060092BA RID: 37562 RVA: 0x001E5A95 File Offset: 0x001E3C95
		public virtual Value Divide(Value value, Precision precision)
		{
			throw ValueException.BinaryOperatorTypeMismatch("/", this, value);
		}

		// Token: 0x060092BB RID: 37563 RVA: 0x001E5AA3 File Offset: 0x001E3CA3
		public virtual Value Mod(Value value, Precision precision)
		{
			throw ValueException.BinaryOperatorTypeMismatch("modulus", this, value);
		}

		// Token: 0x060092BC RID: 37564 RVA: 0x001E5AB1 File Offset: 0x001E3CB1
		public virtual Value IntegerDivide(Value value, Precision precision)
		{
			throw ValueException.BinaryOperatorTypeMismatch("integer divide", this, value);
		}

		// Token: 0x060092BD RID: 37565 RVA: 0x001E5ABF File Offset: 0x001E3CBF
		public virtual Value AddR(Int32NumberValue value, Precision precision)
		{
			throw ValueException.BinaryOperatorTypeMismatch("+", value, this);
		}

		// Token: 0x060092BE RID: 37566 RVA: 0x001E5ABF File Offset: 0x001E3CBF
		public virtual Value AddR(DoubleNumberValue value, Precision precision)
		{
			throw ValueException.BinaryOperatorTypeMismatch("+", value, this);
		}

		// Token: 0x060092BF RID: 37567 RVA: 0x001E5ACD File Offset: 0x001E3CCD
		public virtual Value SubtractR(Int32NumberValue value, Precision precision)
		{
			throw ValueException.BinaryOperatorTypeMismatch("-", value, this);
		}

		// Token: 0x060092C0 RID: 37568 RVA: 0x001E5ACD File Offset: 0x001E3CCD
		public virtual Value SubtractR(DoubleNumberValue value, Precision precision)
		{
			throw ValueException.BinaryOperatorTypeMismatch("-", value, this);
		}

		// Token: 0x060092C1 RID: 37569 RVA: 0x001E5ADB File Offset: 0x001E3CDB
		public virtual Value MultiplyR(Int32NumberValue value, Precision precision)
		{
			throw ValueException.BinaryOperatorTypeMismatch("*", value, this);
		}

		// Token: 0x060092C2 RID: 37570 RVA: 0x001E5ADB File Offset: 0x001E3CDB
		public virtual Value MultiplyR(DoubleNumberValue value, Precision precision)
		{
			throw ValueException.BinaryOperatorTypeMismatch("*", value, this);
		}

		// Token: 0x060092C3 RID: 37571 RVA: 0x001E5AE9 File Offset: 0x001E3CE9
		public virtual Value Concatenate(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("&", this, value);
		}

		// Token: 0x060092C4 RID: 37572 RVA: 0x001E5AF7 File Offset: 0x001E3CF7
		public virtual Value BitwiseAnd(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("bitwise and", this, value);
		}

		// Token: 0x060092C5 RID: 37573 RVA: 0x001E5B05 File Offset: 0x001E3D05
		public virtual Value BitwiseOr(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("bitwise or", this, value);
		}

		// Token: 0x060092C6 RID: 37574 RVA: 0x001E5B13 File Offset: 0x001E3D13
		public virtual Value BitwiseXor(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("bitwise exclusive or", this, value);
		}

		// Token: 0x060092C7 RID: 37575 RVA: 0x001E5B21 File Offset: 0x001E3D21
		public virtual Value ShiftLeft(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("shift left", this, value);
		}

		// Token: 0x060092C8 RID: 37576 RVA: 0x001E5B2F File Offset: 0x001E3D2F
		public virtual Value ShiftRight(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("shift right", this, value);
		}

		// Token: 0x060092C9 RID: 37577 RVA: 0x001E5B3D File Offset: 0x001E3D3D
		public virtual Accumulator GetAccumulator(Precision precision)
		{
			return new DefaultAccumulator(precision, precision.Subtract(this, this));
		}

		// Token: 0x170026A3 RID: 9891
		public abstract Value this[int index] { get; }

		// Token: 0x170026A4 RID: 9892
		public abstract Value this[Value key] { get; }

		// Token: 0x170026A5 RID: 9893
		public virtual Value this[string key]
		{
			get
			{
				return this[TextValue.New(key)];
			}
		}

		// Token: 0x060092CD RID: 37581
		public abstract bool TryGetValue(Value index, out Value value);

		// Token: 0x060092CE RID: 37582 RVA: 0x001E5B5B File Offset: 0x001E3D5B
		public virtual bool TryGetValue(string key, out Value value)
		{
			return this.TryGetValue(TextValue.New(key), out value);
		}

		// Token: 0x060092CF RID: 37583 RVA: 0x001E5B6A File Offset: 0x001E3D6A
		public virtual Value NativeQuery(TextValue query, Value parameters, Value options)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.NativeQuery_NotSupported, this, null);
		}

		// Token: 0x060092D0 RID: 37584 RVA: 0x001ABC3C File Offset: 0x001A9E3C
		public virtual ActionValue Replace(Value value)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.ValueNotUpdatable, this, null);
		}

		// Token: 0x060092D1 RID: 37585 RVA: 0x001E5B78 File Offset: 0x001E3D78
		public virtual ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.Action_NativeStatementsNotSupported, this, null);
		}

		// Token: 0x060092D2 RID: 37586 RVA: 0x000BF254 File Offset: 0x000BD454
		public virtual bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			result = null;
			return false;
		}

		// Token: 0x060092D3 RID: 37587 RVA: 0x001E5B88 File Offset: 0x001E3D88
		public static Value Invoke(FoldableFunctionValue function, Value[] arguments)
		{
			for (int i = 0; i < arguments.Length; i++)
			{
				Value value;
				if (arguments[i].TryInvokeAsArgument(function, arguments, i, out value))
				{
					return value;
				}
			}
			return function.Function.Invoke(arguments);
		}

		// Token: 0x060092D4 RID: 37588
		public abstract string ToSource();

		// Token: 0x060092D5 RID: 37589
		public abstract override string ToString();

		// Token: 0x060092D6 RID: 37590
		public abstract object ToOleDb(Type type);

		// Token: 0x060092D7 RID: 37591 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void TestConnection()
		{
		}

		// Token: 0x170026A6 RID: 9894
		// (get) Token: 0x060092D8 RID: 37592 RVA: 0x00002139 File Offset: 0x00000339
		bool IValueReference.Evaluated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170026A7 RID: 9895
		// (get) Token: 0x060092D9 RID: 37593 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		Value IValueReference.Value
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170026A8 RID: 9896
		// (get) Token: 0x060092DA RID: 37594 RVA: 0x001E5BC0 File Offset: 0x001E3DC0
		ITypeValue IValue.Type
		{
			get
			{
				return this.Type;
			}
		}

		// Token: 0x060092DB RID: 37595 RVA: 0x001E5BC8 File Offset: 0x001E3DC8
		IValue IValue.NewMeta(IRecordValue meta)
		{
			return this.NewMeta((RecordValue)meta);
		}

		// Token: 0x170026A9 RID: 9897
		// (get) Token: 0x060092DC RID: 37596 RVA: 0x001E5BD6 File Offset: 0x001E3DD6
		IRecordValue IValue.MetaValue
		{
			get
			{
				return this.MetaValue;
			}
		}

		// Token: 0x170026AA RID: 9898
		// (get) Token: 0x060092DD RID: 37597 RVA: 0x001B4C9C File Offset: 0x001B2E9C
		IValue IValue.SubtractMetaValue
		{
			get
			{
				return this.SubtractMetaValue;
			}
		}

		// Token: 0x060092DE RID: 37598 RVA: 0x001E5BE0 File Offset: 0x001E3DE0
		bool IValue.TryGetMetaField(string name, out IValue value)
		{
			Value value2;
			if (!this.TryGetMetaField(name, out value2))
			{
				value = null;
				return false;
			}
			value = value2;
			return true;
		}

		// Token: 0x170026AB RID: 9899
		// (get) Token: 0x060092DF RID: 37599 RVA: 0x001E5C01 File Offset: 0x001E3E01
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		ITypeValue IValue.AsType
		{
			get
			{
				return this.AsType;
			}
		}

		// Token: 0x170026AC RID: 9900
		// (get) Token: 0x060092E0 RID: 37600 RVA: 0x001E5C09 File Offset: 0x001E3E09
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		INumberValue IValue.AsNumber
		{
			get
			{
				return this.AsNumber;
			}
		}

		// Token: 0x170026AD RID: 9901
		// (get) Token: 0x060092E1 RID: 37601 RVA: 0x001E5C11 File Offset: 0x001E3E11
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		IDateValue IValue.AsDate
		{
			get
			{
				return this.AsDate;
			}
		}

		// Token: 0x170026AE RID: 9902
		// (get) Token: 0x060092E2 RID: 37602 RVA: 0x001E5C19 File Offset: 0x001E3E19
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		IDateTime IValue.AsDateTime
		{
			get
			{
				return this.AsDateTime;
			}
		}

		// Token: 0x170026AF RID: 9903
		// (get) Token: 0x060092E3 RID: 37603 RVA: 0x001E5C21 File Offset: 0x001E3E21
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		IDateTimeZone IValue.AsDateTimeZone
		{
			get
			{
				return this.AsDateTimeZone;
			}
		}

		// Token: 0x170026B0 RID: 9904
		// (get) Token: 0x060092E4 RID: 37604 RVA: 0x001E5C29 File Offset: 0x001E3E29
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		ITimeValue IValue.AsTime
		{
			get
			{
				return this.AsTime;
			}
		}

		// Token: 0x170026B1 RID: 9905
		// (get) Token: 0x060092E5 RID: 37605 RVA: 0x001E5C31 File Offset: 0x001E3E31
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		IDurationValue IValue.AsDuration
		{
			get
			{
				return this.AsDuration;
			}
		}

		// Token: 0x170026B2 RID: 9906
		// (get) Token: 0x060092E6 RID: 37606 RVA: 0x001E5C39 File Offset: 0x001E3E39
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		ITextValue IValue.AsText
		{
			get
			{
				return this.AsText;
			}
		}

		// Token: 0x170026B3 RID: 9907
		// (get) Token: 0x060092E7 RID: 37607 RVA: 0x001E5C41 File Offset: 0x001E3E41
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		IListValue IValue.AsList
		{
			get
			{
				return this.AsList;
			}
		}

		// Token: 0x170026B4 RID: 9908
		// (get) Token: 0x060092E8 RID: 37608 RVA: 0x001E5C49 File Offset: 0x001E3E49
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		ITableValue IValue.AsTable
		{
			get
			{
				return this.AsTable;
			}
		}

		// Token: 0x170026B5 RID: 9909
		// (get) Token: 0x060092E9 RID: 37609 RVA: 0x001E5C51 File Offset: 0x001E3E51
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		IBinaryValue IValue.AsBinary
		{
			get
			{
				return this.AsBinary;
			}
		}

		// Token: 0x170026B6 RID: 9910
		// (get) Token: 0x060092EA RID: 37610 RVA: 0x001E5C59 File Offset: 0x001E3E59
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		IRecordValue IValue.AsRecord
		{
			get
			{
				return this.AsRecord;
			}
		}

		// Token: 0x170026B7 RID: 9911
		// (get) Token: 0x060092EB RID: 37611 RVA: 0x001E5C61 File Offset: 0x001E3E61
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		IFunctionValue IValue.AsFunction
		{
			get
			{
				return this.AsFunction;
			}
		}

		// Token: 0x170026B8 RID: 9912
		// (get) Token: 0x060092EC RID: 37612 RVA: 0x001E5C69 File Offset: 0x001E3E69
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		IActionValue IValue.AsAction
		{
			get
			{
				return this.AsAction;
			}
		}

		// Token: 0x170026B9 RID: 9913
		// (get) Token: 0x060092ED RID: 37613 RVA: 0x001E5C71 File Offset: 0x001E3E71
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.AsBoolean
		{
			get
			{
				return this.AsBoolean;
			}
		}

		// Token: 0x170026BA RID: 9914
		// (get) Token: 0x060092EE RID: 37614 RVA: 0x001E5C79 File Offset: 0x001E3E79
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		string IValue.AsString
		{
			get
			{
				return this.AsString;
			}
		}

		// Token: 0x170026BB RID: 9915
		// (get) Token: 0x060092EF RID: 37615 RVA: 0x001E5C81 File Offset: 0x001E3E81
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsNumber
		{
			get
			{
				return this.IsNumber;
			}
		}

		// Token: 0x170026BC RID: 9916
		// (get) Token: 0x060092F0 RID: 37616 RVA: 0x001E5C89 File Offset: 0x001E3E89
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsText
		{
			get
			{
				return this.IsText;
			}
		}

		// Token: 0x170026BD RID: 9917
		// (get) Token: 0x060092F1 RID: 37617 RVA: 0x001E5C91 File Offset: 0x001E3E91
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsList
		{
			get
			{
				return this.IsList;
			}
		}

		// Token: 0x170026BE RID: 9918
		// (get) Token: 0x060092F2 RID: 37618 RVA: 0x001E5C99 File Offset: 0x001E3E99
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsTable
		{
			get
			{
				return this.IsTable;
			}
		}

		// Token: 0x170026BF RID: 9919
		// (get) Token: 0x060092F3 RID: 37619 RVA: 0x001E5CA1 File Offset: 0x001E3EA1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsBinary
		{
			get
			{
				return this.IsBinary;
			}
		}

		// Token: 0x170026C0 RID: 9920
		// (get) Token: 0x060092F4 RID: 37620 RVA: 0x001E5CA9 File Offset: 0x001E3EA9
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsDate
		{
			get
			{
				return this.IsDate;
			}
		}

		// Token: 0x170026C1 RID: 9921
		// (get) Token: 0x060092F5 RID: 37621 RVA: 0x001E5CB1 File Offset: 0x001E3EB1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsDateTime
		{
			get
			{
				return this.IsDateTime;
			}
		}

		// Token: 0x170026C2 RID: 9922
		// (get) Token: 0x060092F6 RID: 37622 RVA: 0x001E5CB9 File Offset: 0x001E3EB9
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsDateTimeZone
		{
			get
			{
				return this.IsDateTimeZone;
			}
		}

		// Token: 0x170026C3 RID: 9923
		// (get) Token: 0x060092F7 RID: 37623 RVA: 0x001E5CC1 File Offset: 0x001E3EC1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsTime
		{
			get
			{
				return this.IsTime;
			}
		}

		// Token: 0x170026C4 RID: 9924
		// (get) Token: 0x060092F8 RID: 37624 RVA: 0x001E5CC9 File Offset: 0x001E3EC9
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsNull
		{
			get
			{
				return this.IsNull;
			}
		}

		// Token: 0x170026C5 RID: 9925
		// (get) Token: 0x060092F9 RID: 37625 RVA: 0x001E5CD1 File Offset: 0x001E3ED1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsLogical
		{
			get
			{
				return this.IsLogical;
			}
		}

		// Token: 0x170026C6 RID: 9926
		// (get) Token: 0x060092FA RID: 37626 RVA: 0x001E5CD9 File Offset: 0x001E3ED9
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsRecord
		{
			get
			{
				return this.IsRecord;
			}
		}

		// Token: 0x170026C7 RID: 9927
		// (get) Token: 0x060092FB RID: 37627 RVA: 0x001E5CE1 File Offset: 0x001E3EE1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsFunction
		{
			get
			{
				return this.IsFunction;
			}
		}

		// Token: 0x170026C8 RID: 9928
		// (get) Token: 0x060092FC RID: 37628 RVA: 0x001E5CE9 File Offset: 0x001E3EE9
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsDuration
		{
			get
			{
				return this.IsDuration;
			}
		}

		// Token: 0x170026C9 RID: 9929
		// (get) Token: 0x060092FD RID: 37629 RVA: 0x001E5CF1 File Offset: 0x001E3EF1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsType
		{
			get
			{
				return this.IsType;
			}
		}

		// Token: 0x170026CA RID: 9930
		// (get) Token: 0x060092FE RID: 37630 RVA: 0x001E5CF9 File Offset: 0x001E3EF9
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IValue.IsAction
		{
			get
			{
				return this.IsAction;
			}
		}

		// Token: 0x060092FF RID: 37631 RVA: 0x001E5D01 File Offset: 0x001E3F01
		IValue IValue.Concatenate(IValue value)
		{
			return this.Concatenate((Value)value);
		}

		// Token: 0x06009300 RID: 37632 RVA: 0x001E5D0F File Offset: 0x001E3F0F
		IValue IValue.ReplaceType(ITypeValue value)
		{
			return this.ReplaceType((TypeValue)value);
		}

		// Token: 0x06009301 RID: 37633 RVA: 0x001E5D1D File Offset: 0x001E3F1D
		string IValue.ToSource()
		{
			return this.ToSource();
		}
	}
}
