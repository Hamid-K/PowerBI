using System;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200168E RID: 5774
	internal struct PiiFree
	{
		// Token: 0x0600922F RID: 37423 RVA: 0x001E52AC File Offset: 0x001E34AC
		public static IPiiFree New(object value)
		{
			return new PiiFree.PiiFreeObject(value);
		}

		// Token: 0x06009230 RID: 37424 RVA: 0x001E52B9 File Offset: 0x001E34B9
		public static IPiiFree New(string value)
		{
			return new PiiFree.PiiFreeString(value);
		}

		// Token: 0x06009231 RID: 37425 RVA: 0x001E52C6 File Offset: 0x001E34C6
		public static IPiiFree New(int value)
		{
			return new PiiFree.PiiFreeInt(value);
		}

		// Token: 0x06009232 RID: 37426 RVA: 0x001E52D3 File Offset: 0x001E34D3
		public static IPiiFree New(long value)
		{
			return new PiiFree.PiiFreeLong(value);
		}

		// Token: 0x0200168F RID: 5775
		private struct PiiFreeObject : IPiiFree
		{
			// Token: 0x06009233 RID: 37427 RVA: 0x001E52E0 File Offset: 0x001E34E0
			public PiiFreeObject(object value)
			{
				this.value = value;
			}

			// Token: 0x06009234 RID: 37428 RVA: 0x001E52E9 File Offset: 0x001E34E9
			public static implicit operator string(PiiFree.PiiFreeObject piiFreeObject)
			{
				return piiFreeObject.ToString();
			}

			// Token: 0x06009235 RID: 37429 RVA: 0x001E52F8 File Offset: 0x001E34F8
			public override string ToString()
			{
				return this.value.ToString();
			}

			// Token: 0x17002662 RID: 9826
			// (get) Token: 0x06009236 RID: 37430 RVA: 0x001E5305 File Offset: 0x001E3505
			public Value Value
			{
				get
				{
					return MessageUtils.MarshalValue(this.value);
				}
			}

			// Token: 0x17002663 RID: 9827
			// (get) Token: 0x06009237 RID: 37431 RVA: 0x001E5312 File Offset: 0x001E3512
			public object ClrValue
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04004E74 RID: 20084
			private readonly object value;
		}

		// Token: 0x02001690 RID: 5776
		private struct PiiFreeString : IPiiFree
		{
			// Token: 0x06009238 RID: 37432 RVA: 0x001E531A File Offset: 0x001E351A
			public PiiFreeString(string value)
			{
				this.value = value;
			}

			// Token: 0x06009239 RID: 37433 RVA: 0x001E5323 File Offset: 0x001E3523
			public static implicit operator string(PiiFree.PiiFreeString piiFreeString)
			{
				return piiFreeString.ToString();
			}

			// Token: 0x0600923A RID: 37434 RVA: 0x001E5332 File Offset: 0x001E3532
			public override string ToString()
			{
				return this.value;
			}

			// Token: 0x17002664 RID: 9828
			// (get) Token: 0x0600923B RID: 37435 RVA: 0x001E533A File Offset: 0x001E353A
			public Value Value
			{
				get
				{
					return TextValue.New(this.value);
				}
			}

			// Token: 0x17002665 RID: 9829
			// (get) Token: 0x0600923C RID: 37436 RVA: 0x001E5332 File Offset: 0x001E3532
			public object ClrValue
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04004E75 RID: 20085
			private readonly string value;
		}

		// Token: 0x02001691 RID: 5777
		private struct PiiFreeInt : IPiiFree
		{
			// Token: 0x0600923D RID: 37437 RVA: 0x001E5347 File Offset: 0x001E3547
			public PiiFreeInt(int value)
			{
				this.value = value;
			}

			// Token: 0x0600923E RID: 37438 RVA: 0x001E5350 File Offset: 0x001E3550
			public static implicit operator string(PiiFree.PiiFreeInt piiFreeInt)
			{
				return piiFreeInt.ToString();
			}

			// Token: 0x0600923F RID: 37439 RVA: 0x001E5360 File Offset: 0x001E3560
			public override string ToString()
			{
				return this.value.ToString(CultureInfo.InvariantCulture);
			}

			// Token: 0x17002666 RID: 9830
			// (get) Token: 0x06009240 RID: 37440 RVA: 0x001E5380 File Offset: 0x001E3580
			public Value Value
			{
				get
				{
					return NumberValue.New(this.value);
				}
			}

			// Token: 0x17002667 RID: 9831
			// (get) Token: 0x06009241 RID: 37441 RVA: 0x001E538D File Offset: 0x001E358D
			public object ClrValue
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04004E76 RID: 20086
			private readonly int value;
		}

		// Token: 0x02001692 RID: 5778
		private struct PiiFreeLong : IPiiFree
		{
			// Token: 0x06009242 RID: 37442 RVA: 0x001E539A File Offset: 0x001E359A
			public PiiFreeLong(long value)
			{
				this.value = value;
			}

			// Token: 0x06009243 RID: 37443 RVA: 0x001E53A3 File Offset: 0x001E35A3
			public static implicit operator string(PiiFree.PiiFreeLong piiFreeLong)
			{
				return piiFreeLong.ToString();
			}

			// Token: 0x06009244 RID: 37444 RVA: 0x001E53B4 File Offset: 0x001E35B4
			public override string ToString()
			{
				return this.value.ToString(CultureInfo.InvariantCulture);
			}

			// Token: 0x17002668 RID: 9832
			// (get) Token: 0x06009245 RID: 37445 RVA: 0x001E53D4 File Offset: 0x001E35D4
			public Value Value
			{
				get
				{
					return NumberValue.New((double)this.value);
				}
			}

			// Token: 0x17002669 RID: 9833
			// (get) Token: 0x06009246 RID: 37446 RVA: 0x001E53E2 File Offset: 0x001E35E2
			public object ClrValue
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04004E77 RID: 20087
			private readonly long value;
		}
	}
}
