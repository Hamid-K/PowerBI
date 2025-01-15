using System;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003AD RID: 941
	internal sealed class ValidityState : IValidityState
	{
		// Token: 0x06001DA5 RID: 7589 RVA: 0x00055F59 File Offset: 0x00054159
		internal ValidityState()
		{
			this._err = ValidityState.ErrorType.None;
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x00055F68 File Offset: 0x00054168
		// (set) Token: 0x06001DA7 RID: 7591 RVA: 0x00055F75 File Offset: 0x00054175
		public bool IsValueMissing
		{
			get
			{
				return (this._err & ValidityState.ErrorType.ValueMissing) == ValidityState.ErrorType.ValueMissing;
			}
			set
			{
				this.Set(this.IsValueMissing, value, ValidityState.ErrorType.ValueMissing);
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x00055F85 File Offset: 0x00054185
		// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x00055F92 File Offset: 0x00054192
		public bool IsTypeMismatch
		{
			get
			{
				return (this._err & ValidityState.ErrorType.TypeMismatch) == ValidityState.ErrorType.TypeMismatch;
			}
			set
			{
				this.Set(this.IsTypeMismatch, value, ValidityState.ErrorType.TypeMismatch);
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x00055FA2 File Offset: 0x000541A2
		// (set) Token: 0x06001DAB RID: 7595 RVA: 0x00055FAF File Offset: 0x000541AF
		public bool IsPatternMismatch
		{
			get
			{
				return (this._err & ValidityState.ErrorType.PatternMismatch) == ValidityState.ErrorType.PatternMismatch;
			}
			set
			{
				this.Set(this.IsPatternMismatch, value, ValidityState.ErrorType.PatternMismatch);
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x00055FBF File Offset: 0x000541BF
		// (set) Token: 0x06001DAD RID: 7597 RVA: 0x00055FD4 File Offset: 0x000541D4
		public bool IsBadInput
		{
			get
			{
				return (this._err & ValidityState.ErrorType.BadInput) == ValidityState.ErrorType.BadInput;
			}
			set
			{
				this.Set(this.IsBadInput, value, ValidityState.ErrorType.BadInput);
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x00055FE8 File Offset: 0x000541E8
		// (set) Token: 0x06001DAF RID: 7599 RVA: 0x00055FF5 File Offset: 0x000541F5
		public bool IsTooLong
		{
			get
			{
				return (this._err & ValidityState.ErrorType.TooLong) == ValidityState.ErrorType.TooLong;
			}
			set
			{
				this.Set(this.IsTooLong, value, ValidityState.ErrorType.TooLong);
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00056005 File Offset: 0x00054205
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x00056014 File Offset: 0x00054214
		public bool IsTooShort
		{
			get
			{
				return (this._err & ValidityState.ErrorType.TooShort) == ValidityState.ErrorType.TooShort;
			}
			set
			{
				this.Set(this.IsTooShort, value, ValidityState.ErrorType.TooShort);
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x00056025 File Offset: 0x00054225
		// (set) Token: 0x06001DB3 RID: 7603 RVA: 0x00056034 File Offset: 0x00054234
		public bool IsRangeUnderflow
		{
			get
			{
				return (this._err & ValidityState.ErrorType.RangeUnderflow) == ValidityState.ErrorType.RangeUnderflow;
			}
			set
			{
				this.Set(this.IsRangeUnderflow, value, ValidityState.ErrorType.RangeUnderflow);
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x00056045 File Offset: 0x00054245
		// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x00056054 File Offset: 0x00054254
		public bool IsRangeOverflow
		{
			get
			{
				return (this._err & ValidityState.ErrorType.RangeOverflow) == ValidityState.ErrorType.RangeOverflow;
			}
			set
			{
				this.Set(this.IsRangeOverflow, value, ValidityState.ErrorType.RangeOverflow);
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x00056065 File Offset: 0x00054265
		// (set) Token: 0x06001DB7 RID: 7607 RVA: 0x0005607A File Offset: 0x0005427A
		public bool IsStepMismatch
		{
			get
			{
				return (this._err & ValidityState.ErrorType.StepMismatch) == ValidityState.ErrorType.StepMismatch;
			}
			set
			{
				this.Set(this.IsStepMismatch, value, ValidityState.ErrorType.StepMismatch);
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x0005608E File Offset: 0x0005428E
		// (set) Token: 0x06001DB9 RID: 7609 RVA: 0x000560A3 File Offset: 0x000542A3
		public bool IsCustomError
		{
			get
			{
				return (this._err & ValidityState.ErrorType.Custom) == ValidityState.ErrorType.Custom;
			}
			set
			{
				this.Set(this.IsCustomError, value, ValidityState.ErrorType.Custom);
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x000560B7 File Offset: 0x000542B7
		public bool IsValid
		{
			get
			{
				return this._err == ValidityState.ErrorType.None;
			}
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000560C2 File Offset: 0x000542C2
		public void Reset()
		{
			this._err = ValidityState.ErrorType.None;
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x000560CB File Offset: 0x000542CB
		private void Set(bool oldValue, bool newValue, ValidityState.ErrorType err)
		{
			if (newValue != oldValue)
			{
				this._err ^= err;
			}
		}

		// Token: 0x04000D0D RID: 3341
		private ValidityState.ErrorType _err;

		// Token: 0x02000529 RID: 1321
		[Flags]
		private enum ErrorType : ushort
		{
			// Token: 0x040012A3 RID: 4771
			None = 0,
			// Token: 0x040012A4 RID: 4772
			ValueMissing = 1,
			// Token: 0x040012A5 RID: 4773
			TypeMismatch = 2,
			// Token: 0x040012A6 RID: 4774
			PatternMismatch = 4,
			// Token: 0x040012A7 RID: 4775
			TooLong = 8,
			// Token: 0x040012A8 RID: 4776
			TooShort = 16,
			// Token: 0x040012A9 RID: 4777
			RangeUnderflow = 32,
			// Token: 0x040012AA RID: 4778
			RangeOverflow = 64,
			// Token: 0x040012AB RID: 4779
			StepMismatch = 128,
			// Token: 0x040012AC RID: 4780
			BadInput = 256,
			// Token: 0x040012AD RID: 4781
			Custom = 512
		}
	}
}
