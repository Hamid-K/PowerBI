using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001593 RID: 5523
	public class NullValue : PrimitiveValue
	{
		// Token: 0x060089D8 RID: 35288 RVA: 0x001D1797 File Offset: 0x001CF997
		private static NullValue New(RecordValue meta, TypeValue type)
		{
			if (meta.IsEmpty && type.Equals(TypeValue.Null))
			{
				return NullValue.Instance;
			}
			return new NullValue(meta, type);
		}

		// Token: 0x060089D9 RID: 35289 RVA: 0x001D17BB File Offset: 0x001CF9BB
		protected NullValue(RecordValue meta, TypeValue type)
		{
			this.meta = meta;
			this.type = type;
		}

		// Token: 0x060089DA RID: 35290 RVA: 0x001D17D1 File Offset: 0x001CF9D1
		public sealed override string ToSource()
		{
			return "null";
		}

		// Token: 0x060089DB RID: 35291 RVA: 0x001D17D1 File Offset: 0x001CF9D1
		public sealed override string ToString()
		{
			return "null";
		}

		// Token: 0x060089DC RID: 35292 RVA: 0x001D17D8 File Offset: 0x001CF9D8
		public override object ToOleDb(Type type)
		{
			return DBNull.Value;
		}

		// Token: 0x17002479 RID: 9337
		// (get) Token: 0x060089DD RID: 35293 RVA: 0x001D17DF File Offset: 0x001CF9DF
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060089DE RID: 35294 RVA: 0x001D17E7 File Offset: 0x001CF9E7
		public override Value NewType(TypeValue type)
		{
			if (type.TypeKind != ValueKind.Null)
			{
				return this;
			}
			return NullValue.New(this.meta, type);
		}

		// Token: 0x1700247A RID: 9338
		// (get) Token: 0x060089DF RID: 35295 RVA: 0x00002105 File Offset: 0x00000305
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Null;
			}
		}

		// Token: 0x1700247B RID: 9339
		// (get) Token: 0x060089E0 RID: 35296 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060089E1 RID: 35297 RVA: 0x001D17FF File Offset: 0x001CF9FF
		public override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsNull;
		}

		// Token: 0x060089E2 RID: 35298 RVA: 0x00002105 File Offset: 0x00000305
		public override int GetHashCode(_ValueComparer comparer)
		{
			return 0;
		}

		// Token: 0x060089E3 RID: 35299 RVA: 0x001D1807 File Offset: 0x001CFA07
		public override int CompareTo(Value value, _ValueComparer comparer)
		{
			if (value.IsNull)
			{
				return 0;
			}
			return base.CompareTo(value, comparer);
		}

		// Token: 0x060089E4 RID: 35300 RVA: 0x00019E42 File Offset: 0x00018042
		public override Value Identity()
		{
			return Value.Null;
		}

		// Token: 0x060089E5 RID: 35301 RVA: 0x00019E42 File Offset: 0x00018042
		public override Value Negate()
		{
			return Value.Null;
		}

		// Token: 0x060089E6 RID: 35302 RVA: 0x001D181B File Offset: 0x001CFA1B
		public override Value Add(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.Add(this);
		}

		// Token: 0x060089E7 RID: 35303 RVA: 0x001D1832 File Offset: 0x001CFA32
		public override Value Subtract(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.Subtract(this);
		}

		// Token: 0x060089E8 RID: 35304 RVA: 0x001D1849 File Offset: 0x001CFA49
		public override Value Multiply(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.Multiply(this);
		}

		// Token: 0x060089E9 RID: 35305 RVA: 0x001D1860 File Offset: 0x001CFA60
		public override Value Divide(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.Divide(this);
		}

		// Token: 0x060089EA RID: 35306 RVA: 0x001D1877 File Offset: 0x001CFA77
		public override Value IntegerDivide(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.IntegerDivide(this);
		}

		// Token: 0x060089EB RID: 35307 RVA: 0x001D188E File Offset: 0x001CFA8E
		public override Value Mod(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.Mod(this);
		}

		// Token: 0x060089EC RID: 35308 RVA: 0x00019E42 File Offset: 0x00018042
		public override Value BitwiseNot()
		{
			return Value.Null;
		}

		// Token: 0x060089ED RID: 35309 RVA: 0x001D18A5 File Offset: 0x001CFAA5
		public override Value BitwiseOr(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.BitwiseOr(this);
		}

		// Token: 0x060089EE RID: 35310 RVA: 0x001D18BC File Offset: 0x001CFABC
		public override Value BitwiseAnd(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.BitwiseAnd(this);
		}

		// Token: 0x060089EF RID: 35311 RVA: 0x001D18D3 File Offset: 0x001CFAD3
		public override Value BitwiseXor(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.BitwiseXor(this);
		}

		// Token: 0x060089F0 RID: 35312 RVA: 0x001D18EA File Offset: 0x001CFAEA
		public override Value ShiftLeft(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.ShiftLeft(this);
		}

		// Token: 0x060089F1 RID: 35313 RVA: 0x001D1901 File Offset: 0x001CFB01
		public override Value ShiftRight(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.ShiftRight(this);
		}

		// Token: 0x060089F2 RID: 35314 RVA: 0x001D1918 File Offset: 0x001CFB18
		public override Value Concatenate(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.Concatenate(this);
		}

		// Token: 0x060089F3 RID: 35315 RVA: 0x00019E42 File Offset: 0x00018042
		public override Value Add(Value value, Precision precision)
		{
			return Value.Null;
		}

		// Token: 0x060089F4 RID: 35316 RVA: 0x00019E42 File Offset: 0x00018042
		public override Value Subtract(Value value, Precision precision)
		{
			return Value.Null;
		}

		// Token: 0x060089F5 RID: 35317 RVA: 0x00019E42 File Offset: 0x00018042
		public override Value Multiply(Value value, Precision precision)
		{
			return Value.Null;
		}

		// Token: 0x060089F6 RID: 35318 RVA: 0x00019E42 File Offset: 0x00018042
		public override Value Divide(Value value, Precision precision)
		{
			return Value.Null;
		}

		// Token: 0x060089F7 RID: 35319 RVA: 0x001D192F File Offset: 0x001CFB2F
		public sealed override Value NullableGreaterThan(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.NullableLessThan(this);
		}

		// Token: 0x060089F8 RID: 35320 RVA: 0x001D1946 File Offset: 0x001CFB46
		public sealed override Value NullableLessThan(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.NullableGreaterThan(this);
		}

		// Token: 0x060089F9 RID: 35321 RVA: 0x001D195D File Offset: 0x001CFB5D
		public sealed override Value NullableGreaterThanOrEqual(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.NullableLessThanOrEqual(this);
		}

		// Token: 0x060089FA RID: 35322 RVA: 0x001D1974 File Offset: 0x001CFB74
		public sealed override Value NullableLessThanOrEqual(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return value.NullableGreaterThanOrEqual(this);
		}

		// Token: 0x060089FB RID: 35323 RVA: 0x00019E42 File Offset: 0x00018042
		public override Value AddR(Int32NumberValue value, Precision precision)
		{
			return Value.Null;
		}

		// Token: 0x060089FC RID: 35324 RVA: 0x00019E42 File Offset: 0x00018042
		public override Value AddR(DoubleNumberValue value, Precision precision)
		{
			return Value.Null;
		}

		// Token: 0x060089FD RID: 35325 RVA: 0x00019E42 File Offset: 0x00018042
		public override Value SubtractR(Int32NumberValue value, Precision precision)
		{
			return Value.Null;
		}

		// Token: 0x060089FE RID: 35326 RVA: 0x00019E42 File Offset: 0x00018042
		public override Value SubtractR(DoubleNumberValue value, Precision precision)
		{
			return Value.Null;
		}

		// Token: 0x060089FF RID: 35327 RVA: 0x00019E42 File Offset: 0x00018042
		public override Value Not()
		{
			return Value.Null;
		}

		// Token: 0x1700247C RID: 9340
		// (get) Token: 0x06008A00 RID: 35328 RVA: 0x001D198B File Offset: 0x001CFB8B
		public override RecordValue MetaValue
		{
			get
			{
				return this.meta;
			}
		}

		// Token: 0x06008A01 RID: 35329 RVA: 0x001D1993 File Offset: 0x001CFB93
		public override Value NewMeta(RecordValue metaValue)
		{
			return NullValue.New(metaValue, this.type);
		}

		// Token: 0x04004BFE RID: 19454
		public static readonly NullValue Instance = new NullValue(RecordValue.Empty, TypeValue.Null);

		// Token: 0x04004BFF RID: 19455
		private readonly RecordValue meta;

		// Token: 0x04004C00 RID: 19456
		private readonly TypeValue type;
	}
}
