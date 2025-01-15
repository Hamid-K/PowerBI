using System;
using System.ComponentModel;
using Microsoft.InfoNav.Data.PrimitiveValues;

namespace Microsoft.InfoNav
{
	// Token: 0x0200005A RID: 90
	[ImmutableObject(true)]
	public abstract class PrimitiveValue : IEquatable<PrimitiveValue>
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000169 RID: 361
		public abstract ConceptualPrimitiveType Type { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000030AA File Offset: 0x000012AA
		public static PrimitiveValue Null
		{
			get
			{
				return NullPrimitiveValue.Instance;
			}
		}

		// Token: 0x0600016B RID: 363
		public abstract bool Equals(PrimitiveValue other);

		// Token: 0x0600016C RID: 364
		public abstract object GetValueAsObject();

		// Token: 0x0600016D RID: 365 RVA: 0x000030B1 File Offset: 0x000012B1
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PrimitiveValue);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000030BF File Offset: 0x000012BF
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000030C2 File Offset: 0x000012C2
		public override string ToString()
		{
			return PrimitiveValueEncoding.ToTypeEncodedString(this);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000030CA File Offset: 0x000012CA
		public static implicit operator PrimitiveValue(string value)
		{
			return new TextPrimitiveValue(value);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000030D2 File Offset: 0x000012D2
		public static implicit operator PrimitiveValue(decimal value)
		{
			return new DecimalPrimitiveValue(value);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000030DA File Offset: 0x000012DA
		public static implicit operator PrimitiveValue(double value)
		{
			return new DoublePrimitiveValue(value);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000030E2 File Offset: 0x000012E2
		public static implicit operator PrimitiveValue(long value)
		{
			return new IntegerPrimitiveValue(value);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000030EA File Offset: 0x000012EA
		public static implicit operator PrimitiveValue(bool value)
		{
			if (!value)
			{
				return BooleanPrimitiveValue.False;
			}
			return BooleanPrimitiveValue.True;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000030FA File Offset: 0x000012FA
		public static implicit operator PrimitiveValue(DateTime value)
		{
			return new DateTimePrimitiveValue(value);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00003102 File Offset: 0x00001302
		public static implicit operator PrimitiveValue(byte[] value)
		{
			return new BinaryPrimitiveValue(value);
		}
	}
}
