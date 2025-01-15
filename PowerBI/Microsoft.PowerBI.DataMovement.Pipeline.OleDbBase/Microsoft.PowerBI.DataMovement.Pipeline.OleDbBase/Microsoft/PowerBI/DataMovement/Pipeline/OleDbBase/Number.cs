using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200004E RID: 78
	public struct Number
	{
		// Token: 0x0600028C RID: 652 RVA: 0x000087C4 File Offset: 0x000069C4
		public unsafe Number(double value)
		{
			Number.NumberBuffer numberBuffer;
			*(double*)(&numberBuffer) = value;
			this.kind = NumberKind.Double;
			this.buffer = numberBuffer;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000087E4 File Offset: 0x000069E4
		public unsafe Number(decimal value)
		{
			Number.NumberBuffer numberBuffer;
			*(decimal*)(&numberBuffer) = value;
			this.kind = NumberKind.Decimal;
			this.buffer = numberBuffer;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00008808 File Offset: 0x00006A08
		public unsafe Number(NUMERIC value)
		{
			Number.NumberBuffer numberBuffer;
			*(NUMERIC*)(&numberBuffer) = value;
			this.kind = NumberKind.Numeric;
			this.buffer = numberBuffer;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000882C File Offset: 0x00006A2C
		public NumberKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00008834 File Offset: 0x00006A34
		public double ToDouble()
		{
			switch (this.kind)
			{
			case NumberKind.Decimal:
				return (double)this.GetDecimal();
			case NumberKind.Double:
				return this.GetDouble();
			case NumberKind.Numeric:
				throw new NotImplementedException();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000887C File Offset: 0x00006A7C
		public decimal ToDecimal()
		{
			switch (this.kind)
			{
			case NumberKind.Decimal:
				return this.GetDecimal();
			case NumberKind.Double:
				return (decimal)this.GetDouble();
			case NumberKind.Numeric:
				return Number.GetDecimal(this.GetNumeric());
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000088C8 File Offset: 0x00006AC8
		public NUMERIC ToNumeric()
		{
			NumberKind numberKind = this.kind;
			if (numberKind <= NumberKind.Double)
			{
				throw new NotImplementedException();
			}
			if (numberKind != NumberKind.Numeric)
			{
				throw new InvalidOperationException();
			}
			return this.GetNumeric();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000088F8 File Offset: 0x00006AF8
		private unsafe double GetDouble()
		{
			Number.NumberBuffer numberBuffer = this.buffer;
			return *(double*)(&numberBuffer);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00008910 File Offset: 0x00006B10
		private unsafe decimal GetDecimal()
		{
			Number.NumberBuffer numberBuffer = this.buffer;
			return *(decimal*)(&numberBuffer);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000892C File Offset: 0x00006B2C
		private unsafe NUMERIC GetNumeric()
		{
			Number.NumberBuffer numberBuffer = this.buffer;
			return *(NUMERIC*)(&numberBuffer);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00008948 File Offset: 0x00006B48
		private unsafe static decimal GetDecimal(NUMERIC numeric)
		{
			DBLENGTH dblength;
			decimal num;
			DBSTATUS dbstatus;
			DataConvert.GetInstance().DataConvert(DBTYPE.NUMERIC, DBTYPE.DECIMAL, DbLength.Numeric, out dblength, (void*)(&numeric), (void*)(&num), DbLength.Decimal, DBSTATUS.S_OK, out dbstatus, Math.Min(numeric.Precision, 29), numeric.Scale, DBDATACONVERT.DEFAULT);
			((VARIANT*)(&num))->Type = VARTYPE.EMPTY;
			return num;
		}

		// Token: 0x04000098 RID: 152
		private NumberKind kind;

		// Token: 0x04000099 RID: 153
		private Number.NumberBuffer buffer;

		// Token: 0x020000EF RID: 239
		internal struct NumberBuffer
		{
			// Token: 0x04000409 RID: 1033
			private ulong u0;

			// Token: 0x0400040A RID: 1034
			private ulong u1;

			// Token: 0x0400040B RID: 1035
			private ulong u2;
		}
	}
}
