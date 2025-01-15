using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000AAB RID: 2731
	public class RichStringType : IRichDataType, IEquatable<IRichDataType>
	{
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004493 RID: 17555 RVA: 0x000D745F File Offset: 0x000D565F
		// (set) Token: 0x06004494 RID: 17556 RVA: 0x000D7467 File Offset: 0x000D5667
		public uint MinLength { get; private set; } = uint.MaxValue;

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06004495 RID: 17557 RVA: 0x000D7470 File Offset: 0x000D5670
		// (set) Token: 0x06004496 RID: 17558 RVA: 0x000D7478 File Offset: 0x000D5678
		public uint MaxLength { get; private set; }

		// Token: 0x06004497 RID: 17559 RVA: 0x000D7484 File Offset: 0x000D5684
		public bool Equals(IRichDataType other)
		{
			if (this == other)
			{
				return true;
			}
			if (other == null)
			{
				return true;
			}
			RichStringType richStringType = other as RichStringType;
			return richStringType != null && richStringType.MaxLength == this.MaxLength && richStringType.MinLength == this.MinLength;
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x000D74C6 File Offset: 0x000D56C6
		public Optional<string> Canonicalize(string value)
		{
			return value.Some<string>();
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x000D74CE File Offset: 0x000D56CE
		public Optional<object> MaybeCastAsType(string value)
		{
			return value.Some<object>();
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x0001B291 File Offset: 0x00019491
		public DataKind BaseKind
		{
			get
			{
				return DataKind.String;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x0600449B RID: 17563 RVA: 0x000D74D6 File Offset: 0x000D56D6
		public DataKind Kind
		{
			get
			{
				return this.BaseKind;
			}
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool IsValueOfType(string value)
		{
			return true;
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x000D74E0 File Offset: 0x000D56E0
		public bool AddSample(string value)
		{
			if (this.IsFinalized)
			{
				return true;
			}
			int num = this.SampleCount + 1;
			this.SampleCount = num;
			this.MinLength = Math.Min(this.MinLength, (uint)value.Length);
			this.MaxLength = Math.Max(this.MaxLength, (uint)value.Length);
			return true;
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x000D7536 File Offset: 0x000D5736
		public void Finish(long _)
		{
			this.IsFinalized = true;
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x0600449F RID: 17567 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int RejectionCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060044A0 RID: 17568 RVA: 0x000D753F File Offset: 0x000D573F
		// (set) Token: 0x060044A1 RID: 17569 RVA: 0x000D7547 File Offset: 0x000D5747
		public int SampleCount { get; private set; }

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060044A2 RID: 17570 RVA: 0x000D7550 File Offset: 0x000D5750
		public int AcceptanceCount
		{
			get
			{
				return this.SampleCount;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int NaValueCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool SuccessOnFinish
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060044A5 RID: 17573 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public bool EarlyFailure
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060044A6 RID: 17574 RVA: 0x000D7558 File Offset: 0x000D5758
		// (set) Token: 0x060044A7 RID: 17575 RVA: 0x000D7560 File Offset: 0x000D5760
		public bool IsFinalized { get; private set; }

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x060044A8 RID: 17576 RVA: 0x000D7569 File Offset: 0x000D5769
		// (set) Token: 0x060044A9 RID: 17577 RVA: 0x000D7571 File Offset: 0x000D5771
		public bool EmptyStringsExpectedInData { get; set; }

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x000D757A File Offset: 0x000D577A
		// (set) Token: 0x060044AB RID: 17579 RVA: 0x000D7582 File Offset: 0x000D5782
		public bool NormalizableStringsExpectedInData { get; set; }

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060044AC RID: 17580 RVA: 0x000D758B File Offset: 0x000D578B
		// (set) Token: 0x060044AD RID: 17581 RVA: 0x000D7593 File Offset: 0x000D5793
		public bool NullsExpectedInData { get; set; }

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x000D759C File Offset: 0x000D579C
		public long MinRequiredSamplesForSuccess
		{
			get
			{
				return 0L;
			}
		}
	}
}
