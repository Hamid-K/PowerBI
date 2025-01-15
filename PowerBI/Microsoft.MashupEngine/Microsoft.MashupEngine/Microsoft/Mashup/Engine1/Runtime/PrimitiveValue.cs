using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015D5 RID: 5589
	public abstract class PrimitiveValue : Value
	{
		// Token: 0x170024DA RID: 9434
		// (get) Token: 0x06008C8B RID: 35979 RVA: 0x00002139 File Offset: 0x00000339
		public sealed override bool IsDefaultType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008C8C RID: 35980 RVA: 0x001D7C4C File Offset: 0x001D5E4C
		public override bool TryGetValue(Value index, out Value value)
		{
			throw ValueException.CastTypeMismatch(this, TypeValue.List);
		}

		// Token: 0x170024DB RID: 9435
		public override Value this[int index]
		{
			get
			{
				throw ValueException.ElementAccessTypeMismatch(this, NumberValue.New(index));
			}
		}

		// Token: 0x170024DC RID: 9436
		public override Value this[Value key]
		{
			get
			{
				throw ValueException.ElementAccessByKeyTypeMismatch(this, key);
			}
		}

		// Token: 0x06008C8F RID: 35983 RVA: 0x001D7C70 File Offset: 0x001D5E70
		public override Value ReplaceType(TypeValue type)
		{
			if ((this.Kind != ValueKind.Null && type.IsNullable) || (this.Kind == ValueKind.Null && type.TypeKind != ValueKind.Null))
			{
				throw ValueException.CastTypeMismatch(this, type);
			}
			return base.ReplaceType(type);
		}

		// Token: 0x06008C90 RID: 35984 RVA: 0x001D7CA1 File Offset: 0x001D5EA1
		public override Value NullableGreaterThan(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return LogicalValue.New(value.LessThan(this));
		}

		// Token: 0x06008C91 RID: 35985 RVA: 0x001D7CBD File Offset: 0x001D5EBD
		public override Value NullableLessThan(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return LogicalValue.New(value.GreaterThan(this));
		}

		// Token: 0x06008C92 RID: 35986 RVA: 0x001D7CD9 File Offset: 0x001D5ED9
		public override Value NullableGreaterThanOrEqual(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return LogicalValue.New(value.LessThanOrEqual(this));
		}

		// Token: 0x06008C93 RID: 35987 RVA: 0x001D7CF5 File Offset: 0x001D5EF5
		public override Value NullableLessThanOrEqual(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			return LogicalValue.New(value.GreaterThanOrEqual(this));
		}
	}
}
