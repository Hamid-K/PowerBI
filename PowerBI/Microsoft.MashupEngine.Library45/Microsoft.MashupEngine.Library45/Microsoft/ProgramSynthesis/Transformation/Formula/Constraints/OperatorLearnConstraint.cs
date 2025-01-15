using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019D5 RID: 6613
	public abstract class OperatorLearnConstraint : FormulaConstraint
	{
		// Token: 0x0600D7D0 RID: 55248 RVA: 0x002DD894 File Offset: 0x002DBA94
		protected OperatorLearnConstraint()
		{
		}

		// Token: 0x0600D7D1 RID: 55249 RVA: 0x002DD968 File Offset: 0x002DBB68
		protected OperatorLearnConstraint(OperatorLearnConstraint other)
		{
			this.ArithmeticMinExampleCount = other.ArithmeticMinExampleCount;
			this.ArithmeticStrategy = other.ArithmeticStrategy;
			this.DateTimeSources = other.DateTimeSources;
			this.EnableArithmetic = other.EnableArithmetic;
			this.EnableArithmeticConstants = other.EnableArithmeticConstants;
			this.EnableConcat = other.EnableConcat;
			this.EnableDateTimePart = other.EnableDateTimePart;
			this.EnableDefaultColumnNamePriority = other.EnableDefaultColumnNamePriority;
			this.EnableFromDateTimePart = other.EnableFromDateTimePart;
			this.EnableFromNumberStr = other.EnableFromNumberStr;
			this.EnableLearningShortCircuit = other.EnableLearningShortCircuit;
			this.EnableLength = other.EnableLength;
			this.EnableMatchNames = other.EnableMatchNames;
			this.EnableMatchUnicode = other.EnableMatchUnicode;
			this.EnableNegativePosition = other.EnableNegativePosition;
			this.EnableParseDateTimePartial = other.EnableParseDateTimePartial;
			this.EnableProperCase = other.EnableProperCase;
			this.EnableReplace = other.EnableReplace;
			this.EnableRoundDateTime = other.EnableRoundDateTime;
			this.EnableRoundNumber = other.EnableRoundNumber;
			this.EnableSlice = other.EnableSlice;
			this.EnableSliceBetween = other.EnableSliceBetween;
			this.EnableSplit = other.EnableSplit;
			this.EnableTimePart = other.EnableTimePart;
			this.EnableTrim = other.EnableTrim;
			this.NumberFormatMaxLeadingDigits = other.NumberFormatMaxLeadingDigits;
			this.NumberRoundMinExampleCount = other.NumberRoundMinExampleCount;
			this.NumberSources = other.NumberSources;
		}

		// Token: 0x170023ED RID: 9197
		// (get) Token: 0x0600D7D2 RID: 55250 RVA: 0x002DDB8A File Offset: 0x002DBD8A
		// (set) Token: 0x0600D7D3 RID: 55251 RVA: 0x002DDB92 File Offset: 0x002DBD92
		public int ArithmeticMinExampleCount { get; set; } = 2;

		// Token: 0x170023EE RID: 9198
		// (get) Token: 0x0600D7D4 RID: 55252 RVA: 0x002DDB9B File Offset: 0x002DBD9B
		// (set) Token: 0x0600D7D5 RID: 55253 RVA: 0x002DDBA3 File Offset: 0x002DBDA3
		public ArithmeticStrategy ArithmeticStrategy { get; set; }

		// Token: 0x170023EF RID: 9199
		// (get) Token: 0x0600D7D6 RID: 55254 RVA: 0x002DDBAC File Offset: 0x002DBDAC
		// (set) Token: 0x0600D7D7 RID: 55255 RVA: 0x002DDBB4 File Offset: 0x002DBDB4
		public DateTimeSourceKind DateTimeSources { get; set; } = DateTimeSourceKind.All;

		// Token: 0x170023F0 RID: 9200
		// (get) Token: 0x0600D7D8 RID: 55256 RVA: 0x002DDBBD File Offset: 0x002DBDBD
		// (set) Token: 0x0600D7D9 RID: 55257 RVA: 0x002DDBC5 File Offset: 0x002DBDC5
		public bool EnableArithmetic { get; set; } = true;

		// Token: 0x170023F1 RID: 9201
		// (get) Token: 0x0600D7DA RID: 55258 RVA: 0x002DDBCE File Offset: 0x002DBDCE
		// (set) Token: 0x0600D7DB RID: 55259 RVA: 0x002DDBD6 File Offset: 0x002DBDD6
		public bool EnableArithmeticConstants { get; set; } = true;

		// Token: 0x170023F2 RID: 9202
		// (get) Token: 0x0600D7DC RID: 55260 RVA: 0x002DDBDF File Offset: 0x002DBDDF
		// (set) Token: 0x0600D7DD RID: 55261 RVA: 0x002DDBE7 File Offset: 0x002DBDE7
		public bool EnableConcat { get; set; } = true;

		// Token: 0x170023F3 RID: 9203
		// (get) Token: 0x0600D7DE RID: 55262 RVA: 0x002DDBF0 File Offset: 0x002DBDF0
		// (set) Token: 0x0600D7DF RID: 55263 RVA: 0x002DDBF8 File Offset: 0x002DBDF8
		public bool EnableDateTimePart { get; set; } = true;

		// Token: 0x170023F4 RID: 9204
		// (get) Token: 0x0600D7E0 RID: 55264 RVA: 0x002DDC01 File Offset: 0x002DBE01
		// (set) Token: 0x0600D7E1 RID: 55265 RVA: 0x002DDC09 File Offset: 0x002DBE09
		public bool EnableDefaultColumnNamePriority { get; set; } = true;

		// Token: 0x170023F5 RID: 9205
		// (get) Token: 0x0600D7E2 RID: 55266 RVA: 0x002DDC12 File Offset: 0x002DBE12
		// (set) Token: 0x0600D7E3 RID: 55267 RVA: 0x002DDC1A File Offset: 0x002DBE1A
		public bool EnableFromDateTimePart { get; set; } = true;

		// Token: 0x170023F6 RID: 9206
		// (get) Token: 0x0600D7E4 RID: 55268 RVA: 0x002DDC23 File Offset: 0x002DBE23
		// (set) Token: 0x0600D7E5 RID: 55269 RVA: 0x002DDC2B File Offset: 0x002DBE2B
		public bool EnableFromNumberStr { get; set; }

		// Token: 0x170023F7 RID: 9207
		// (get) Token: 0x0600D7E6 RID: 55270 RVA: 0x002DDC34 File Offset: 0x002DBE34
		// (set) Token: 0x0600D7E7 RID: 55271 RVA: 0x002DDC3C File Offset: 0x002DBE3C
		public bool EnableLearningShortCircuit { get; set; } = true;

		// Token: 0x170023F8 RID: 9208
		// (get) Token: 0x0600D7E8 RID: 55272 RVA: 0x002DDC45 File Offset: 0x002DBE45
		// (set) Token: 0x0600D7E9 RID: 55273 RVA: 0x002DDC4D File Offset: 0x002DBE4D
		public bool EnableLength { get; set; } = true;

		// Token: 0x170023F9 RID: 9209
		// (get) Token: 0x0600D7EA RID: 55274 RVA: 0x002DDC56 File Offset: 0x002DBE56
		public bool EnableMatch
		{
			get
			{
				return this.EnableMatchNames > MatchName.None;
			}
		}

		// Token: 0x170023FA RID: 9210
		// (get) Token: 0x0600D7EB RID: 55275 RVA: 0x002DDC62 File Offset: 0x002DBE62
		// (set) Token: 0x0600D7EC RID: 55276 RVA: 0x002DDC6A File Offset: 0x002DBE6A
		public MatchName EnableMatchNames { get; set; } = MatchName.All;

		// Token: 0x170023FB RID: 9211
		// (get) Token: 0x0600D7ED RID: 55277 RVA: 0x002DDC73 File Offset: 0x002DBE73
		// (set) Token: 0x0600D7EE RID: 55278 RVA: 0x002DDC7B File Offset: 0x002DBE7B
		public bool EnableMatchUnicode { get; set; } = true;

		// Token: 0x170023FC RID: 9212
		// (get) Token: 0x0600D7EF RID: 55279 RVA: 0x002DDC84 File Offset: 0x002DBE84
		// (set) Token: 0x0600D7F0 RID: 55280 RVA: 0x002DDC8C File Offset: 0x002DBE8C
		public bool EnableNegativePosition { get; set; } = true;

		// Token: 0x170023FD RID: 9213
		// (get) Token: 0x0600D7F1 RID: 55281 RVA: 0x002DDC95 File Offset: 0x002DBE95
		// (set) Token: 0x0600D7F2 RID: 55282 RVA: 0x002DDC9D File Offset: 0x002DBE9D
		public bool EnableParseDateTimePartial { get; set; } = true;

		// Token: 0x170023FE RID: 9214
		// (get) Token: 0x0600D7F3 RID: 55283 RVA: 0x002DDCA6 File Offset: 0x002DBEA6
		// (set) Token: 0x0600D7F4 RID: 55284 RVA: 0x002DDCAE File Offset: 0x002DBEAE
		public bool EnableProperCase { get; set; } = true;

		// Token: 0x170023FF RID: 9215
		// (get) Token: 0x0600D7F5 RID: 55285 RVA: 0x002DDCB7 File Offset: 0x002DBEB7
		// (set) Token: 0x0600D7F6 RID: 55286 RVA: 0x002DDCBF File Offset: 0x002DBEBF
		public bool EnableReplace { get; set; } = true;

		// Token: 0x17002400 RID: 9216
		// (get) Token: 0x0600D7F7 RID: 55287 RVA: 0x002DDCC8 File Offset: 0x002DBEC8
		// (set) Token: 0x0600D7F8 RID: 55288 RVA: 0x002DDCD0 File Offset: 0x002DBED0
		public bool EnableRoundDateTime { get; set; } = true;

		// Token: 0x17002401 RID: 9217
		// (get) Token: 0x0600D7F9 RID: 55289 RVA: 0x002DDCD9 File Offset: 0x002DBED9
		// (set) Token: 0x0600D7FA RID: 55290 RVA: 0x002DDCE1 File Offset: 0x002DBEE1
		public bool EnableRoundNumber { get; set; } = true;

		// Token: 0x17002402 RID: 9218
		// (get) Token: 0x0600D7FB RID: 55291 RVA: 0x002DDCEA File Offset: 0x002DBEEA
		// (set) Token: 0x0600D7FC RID: 55292 RVA: 0x002DDCF2 File Offset: 0x002DBEF2
		public bool EnableSlice { get; set; } = true;

		// Token: 0x17002403 RID: 9219
		// (get) Token: 0x0600D7FD RID: 55293 RVA: 0x002DDCFB File Offset: 0x002DBEFB
		// (set) Token: 0x0600D7FE RID: 55294 RVA: 0x002DDD03 File Offset: 0x002DBF03
		public bool EnableSliceBetween { get; set; } = true;

		// Token: 0x17002404 RID: 9220
		// (get) Token: 0x0600D7FF RID: 55295 RVA: 0x002DDD0C File Offset: 0x002DBF0C
		// (set) Token: 0x0600D800 RID: 55296 RVA: 0x002DDD14 File Offset: 0x002DBF14
		public bool EnableSplit { get; set; } = true;

		// Token: 0x17002405 RID: 9221
		// (get) Token: 0x0600D801 RID: 55297 RVA: 0x002DDD1D File Offset: 0x002DBF1D
		// (set) Token: 0x0600D802 RID: 55298 RVA: 0x002DDD25 File Offset: 0x002DBF25
		public bool EnableTimePart { get; set; } = true;

		// Token: 0x17002406 RID: 9222
		// (get) Token: 0x0600D803 RID: 55299 RVA: 0x002DDD2E File Offset: 0x002DBF2E
		// (set) Token: 0x0600D804 RID: 55300 RVA: 0x002DDD36 File Offset: 0x002DBF36
		public bool EnableTrim { get; set; } = true;

		// Token: 0x17002407 RID: 9223
		// (get) Token: 0x0600D805 RID: 55301 RVA: 0x002DDD3F File Offset: 0x002DBF3F
		// (set) Token: 0x0600D806 RID: 55302 RVA: 0x002DDD48 File Offset: 0x002DBF48
		public int NumberFormatMaxLeadingDigits
		{
			get
			{
				return this._numberFormatMaxLeadingDigits;
			}
			set
			{
				bool flag = value < 1 || value > 8;
				if (flag)
				{
					throw new InvalidOperationException(string.Format("Invalid {0}: {1}. Choose a number between 1 and 8.", "NumberFormatMaxLeadingDigits", value));
				}
				this._numberFormatMaxLeadingDigits = value;
			}
		}

		// Token: 0x17002408 RID: 9224
		// (get) Token: 0x0600D807 RID: 55303 RVA: 0x002DDD88 File Offset: 0x002DBF88
		// (set) Token: 0x0600D808 RID: 55304 RVA: 0x002DDD90 File Offset: 0x002DBF90
		public int NumberRoundMinExampleCount { get; set; } = 1;

		// Token: 0x17002409 RID: 9225
		// (get) Token: 0x0600D809 RID: 55305 RVA: 0x002DDD99 File Offset: 0x002DBF99
		// (set) Token: 0x0600D80A RID: 55306 RVA: 0x002DDDA1 File Offset: 0x002DBFA1
		public NumberSourceKind NumberSources { get; set; } = NumberSourceKind.All;

		// Token: 0x0600D80B RID: 55307 RVA: 0x002DDDAC File Offset: 0x002DBFAC
		public virtual void SetOptions(LearnOptions options)
		{
			options.ArithmeticStrategy = this.ArithmeticStrategy;
			options.ArithmeticMinExampleCount = this.ArithmeticMinExampleCount;
			options.DateTimeSources = this.DateTimeSources;
			options.EnableArithmetic = this.EnableArithmetic;
			options.EnableArithmeticConstants = this.EnableArithmeticConstants;
			options.EnableConcat = this.EnableConcat;
			options.EnableDateTimePart = this.EnableDateTimePart;
			options.EnableDefaultColumnNamePriority = this.EnableDefaultColumnNamePriority;
			options.EnableFromDateTimePart = this.EnableFromDateTimePart;
			options.EnableFromNumberStr = this.EnableFromNumberStr;
			options.EnableLearningShortCircuit = this.EnableLearningShortCircuit;
			options.EnableLength = this.EnableLength;
			options.EnableMatchNames = this.EnableMatchNames;
			options.EnableMatchUnicode = this.EnableMatchUnicode;
			options.EnableNegativePosition = this.EnableNegativePosition;
			options.EnableParseDateTimePartial = this.EnableParseDateTimePartial;
			options.EnableProperCase = this.EnableProperCase;
			options.EnableReplace = this.EnableReplace;
			options.EnableRoundDateTime = this.EnableRoundDateTime;
			options.EnableRoundNumber = this.EnableRoundNumber;
			options.EnableSlice = this.EnableSlice;
			options.EnableSliceBetween = this.EnableSliceBetween;
			options.EnableSplit = this.EnableSplit;
			options.EnableTimePart = this.EnableTimePart;
			options.EnableTrim = this.EnableTrim;
			options.NumberFormatMaxLeadingDigits = this.NumberFormatMaxLeadingDigits;
			options.NumberRoundMinExampleCount = this.NumberRoundMinExampleCount;
			options.NumberSources = this.NumberSources;
			options.LearnConfidence = LearnConfidenceBehavior.None;
			options.MinLearnConfidence = 1.0;
		}

		// Token: 0x0600D80C RID: 55308 RVA: 0x002DDF20 File Offset: 0x002DC120
		internal override string ToEqualString()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				" :: ",
				string.Format("{0}={1}; ", "ArithmeticStrategy", this.ArithmeticStrategy),
				string.Format("{0}={1}; ", "ArithmeticMinExampleCount", this.ArithmeticMinExampleCount),
				string.Format("{0}={1}; ", "DateTimeSources", this.DateTimeSources),
				string.Format("{0}={1}; ", "EnableArithmetic", this.EnableArithmetic),
				string.Format("{0}={1}; ", "EnableArithmeticConstants", this.EnableArithmeticConstants),
				string.Format("{0}={1}; ", "EnableConcat", this.EnableConcat),
				string.Format("{0}={1}; ", "EnableDateTimePart", this.EnableDateTimePart),
				string.Format("{0}={1}; ", "EnableDefaultColumnNamePriority", this.EnableDefaultColumnNamePriority),
				string.Format("{0}={1}; ", "EnableFromDateTimePart", this.EnableFromDateTimePart),
				string.Format("{0}={1}; ", "EnableFromNumberStr", this.EnableFromNumberStr),
				string.Format("{0}={1}; ", "EnableLearningShortCircuit", this.EnableLearningShortCircuit),
				string.Format("{0}={1}; ", "EnableLength", this.EnableLength),
				string.Format("{0}={1}; ", "EnableMatchNames", this.EnableMatchNames),
				string.Format("{0}={1}; ", "EnableMatchUnicode", this.EnableMatchUnicode),
				string.Format("{0}={1}; ", "EnableNegativePosition", this.EnableNegativePosition),
				string.Format("{0}={1}; ", "EnableParseDateTimePartial", this.EnableParseDateTimePartial),
				string.Format("{0}={1}; ", "EnableProperCase", this.EnableProperCase),
				string.Format("{0}={1}; ", "EnableReplace", this.EnableReplace),
				string.Format("{0}={1}; ", "EnableRoundDateTime", this.EnableRoundDateTime),
				string.Format("{0}={1}; ", "EnableRoundNumber", this.EnableRoundNumber),
				string.Format("{0}={1}; ", "EnableSlice", this.EnableSlice),
				string.Format("{0}={1}; ", "EnableSliceBetween", this.EnableSliceBetween),
				string.Format("{0}={1}; ", "EnableSplit", this.EnableSplit),
				string.Format("{0}={1}; ", "EnableTimePart", this.EnableTimePart),
				string.Format("{0}={1}; ", "EnableTrim", this.EnableTrim),
				string.Format("{0}={1}; ", "NumberFormatMaxLeadingDigits", this.NumberFormatMaxLeadingDigits),
				string.Format("{0}={1}; ", "NumberRoundMinExampleCount", this.NumberRoundMinExampleCount),
				string.Format("{0}={1}; ", "NumberSources", this.NumberSources)
			});
		}

		// Token: 0x040052DC RID: 21212
		private int _numberFormatMaxLeadingDigits = 1;
	}
}
