using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200161E RID: 5662
	public abstract class StructureValue : Value
	{
		// Token: 0x17002554 RID: 9556
		// (get) Token: 0x06008E85 RID: 36485 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsDefaultType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008E86 RID: 36486 RVA: 0x001D7C4C File Offset: 0x001D5E4C
		public override bool TryGetValue(Value index, out Value value)
		{
			throw ValueException.CastTypeMismatch(this, TypeValue.List);
		}

		// Token: 0x17002555 RID: 9557
		public override Value this[Value key]
		{
			get
			{
				throw ValueException.ElementAccessByKeyTypeMismatch(this, key);
			}
		}

		// Token: 0x17002556 RID: 9558
		public override Value this[int index]
		{
			get
			{
				throw ValueException.ElementAccessTypeMismatch(this, NumberValue.New(index));
			}
		}

		// Token: 0x17002557 RID: 9559
		// (get) Token: 0x06008E89 RID: 36489 RVA: 0x001DB8D6 File Offset: 0x001D9AD6
		public virtual Value Item0
		{
			get
			{
				return this[0];
			}
		}

		// Token: 0x17002558 RID: 9560
		// (get) Token: 0x06008E8A RID: 36490 RVA: 0x001DB8DF File Offset: 0x001D9ADF
		public virtual Value Item1
		{
			get
			{
				return this[1];
			}
		}

		// Token: 0x17002559 RID: 9561
		// (get) Token: 0x06008E8B RID: 36491 RVA: 0x001DB8E8 File Offset: 0x001D9AE8
		public virtual Value Item2
		{
			get
			{
				return this[2];
			}
		}

		// Token: 0x1700255A RID: 9562
		// (get) Token: 0x06008E8C RID: 36492 RVA: 0x001DB8F1 File Offset: 0x001D9AF1
		public virtual Value Item3
		{
			get
			{
				return this[3];
			}
		}

		// Token: 0x1700255B RID: 9563
		// (get) Token: 0x06008E8D RID: 36493 RVA: 0x001DB8FA File Offset: 0x001D9AFA
		public virtual Value Item4
		{
			get
			{
				return this[4];
			}
		}

		// Token: 0x1700255C RID: 9564
		// (get) Token: 0x06008E8E RID: 36494 RVA: 0x001DB903 File Offset: 0x001D9B03
		public virtual Value Item5
		{
			get
			{
				return this[5];
			}
		}

		// Token: 0x1700255D RID: 9565
		// (get) Token: 0x06008E8F RID: 36495 RVA: 0x001DB90C File Offset: 0x001D9B0C
		public virtual Value Item6
		{
			get
			{
				return this[6];
			}
		}

		// Token: 0x1700255E RID: 9566
		// (get) Token: 0x06008E90 RID: 36496 RVA: 0x001DB915 File Offset: 0x001D9B15
		public virtual Value Item7
		{
			get
			{
				return this[7];
			}
		}

		// Token: 0x06008E91 RID: 36497 RVA: 0x001DB91E File Offset: 0x001D9B1E
		public sealed override Value NullableGreaterThan(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch(">", this, value);
		}

		// Token: 0x06008E92 RID: 36498 RVA: 0x001DB92C File Offset: 0x001D9B2C
		public sealed override Value NullableLessThan(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("<", this, value);
		}

		// Token: 0x06008E93 RID: 36499 RVA: 0x001DB93A File Offset: 0x001D9B3A
		public sealed override Value NullableGreaterThanOrEqual(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch(">=", this, value);
		}

		// Token: 0x06008E94 RID: 36500 RVA: 0x001DB948 File Offset: 0x001D9B48
		public sealed override Value NullableLessThanOrEqual(Value value)
		{
			throw ValueException.BinaryOperatorTypeMismatch("<=", this, value);
		}

		// Token: 0x06008E95 RID: 36501 RVA: 0x001DB92C File Offset: 0x001D9B2C
		public sealed override int CompareTo(Value value, _ValueComparer comparer)
		{
			throw ValueException.BinaryOperatorTypeMismatch("<", this, value);
		}

		// Token: 0x06008E96 RID: 36502
		public abstract override void TestConnection();
	}
}
