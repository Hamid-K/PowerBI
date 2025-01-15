using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000A7A RID: 2682
	public class RichBooleanType : IRichDataType, IEquatable<IRichDataType>
	{
		// Token: 0x0600427B RID: 17019 RVA: 0x000D0E00 File Offset: 0x000CF000
		public bool Equals(IRichDataType other)
		{
			RichBooleanType richBooleanType = other as RichBooleanType;
			return richBooleanType != null && this._exampleTrueValues.SetEquals(richBooleanType._exampleTrueValues) && this._exampleFalseValues.SetEquals(richBooleanType._exampleFalseValues) && object.Equals(this.SingleNonWhitespaceNaValue, richBooleanType.SingleNonWhitespaceNaValue);
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x000D0E50 File Offset: 0x000CF050
		public IEnumerable<string> ExampleTrueValues
		{
			get
			{
				return this._exampleTrueValues;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600427D RID: 17021 RVA: 0x000D0E58 File Offset: 0x000CF058
		public IEnumerable<string> ExampleFalseValues
		{
			get
			{
				return this._exampleFalseValues;
			}
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x000D0E60 File Offset: 0x000CF060
		public Optional<string> Canonicalize(string value)
		{
			if (value == null)
			{
				return Optional<string>.Nothing;
			}
			if (this.NormalizableStringsExpectedInData)
			{
				value = value.NormalizeAndTrim(NormalizationForm.FormC);
			}
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty.Some<string>();
			}
			bool flag;
			if (RichBooleanType.ConversionDictionary.TryGetValue(value.ToLower(), out flag))
			{
				return (flag ? RichBooleanType.TrueString : RichBooleanType.FalseString).Some<string>();
			}
			return Optional<string>.Nothing;
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000D0EC8 File Offset: 0x000CF0C8
		public Optional<object> MaybeCastAsType(string value)
		{
			return this.Canonicalize(value).Select(delegate(string str)
			{
				bool flag;
				if (!bool.TryParse(str, out flag))
				{
					return null;
				}
				return flag;
			});
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06004280 RID: 17024 RVA: 0x000D0EF5 File Offset: 0x000CF0F5
		// (set) Token: 0x06004281 RID: 17025 RVA: 0x000D0EFD File Offset: 0x000CF0FD
		public string SingleNonWhitespaceNaValue { get; private set; }

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06004282 RID: 17026 RVA: 0x0001B1B1 File Offset: 0x000193B1
		public DataKind BaseKind
		{
			get
			{
				return DataKind.Boolean;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x0001B1B1 File Offset: 0x000193B1
		public DataKind Kind
		{
			get
			{
				return DataKind.Boolean;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x000D0F06 File Offset: 0x000CF106
		// (set) Token: 0x06004285 RID: 17029 RVA: 0x000D0F0E File Offset: 0x000CF10E
		public bool EmptyStringsExpectedInData { get; set; }

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06004286 RID: 17030 RVA: 0x000D0F17 File Offset: 0x000CF117
		// (set) Token: 0x06004287 RID: 17031 RVA: 0x000D0F1F File Offset: 0x000CF11F
		public bool NormalizableStringsExpectedInData { get; set; }

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06004288 RID: 17032 RVA: 0x000D0F28 File Offset: 0x000CF128
		// (set) Token: 0x06004289 RID: 17033 RVA: 0x000D0F30 File Offset: 0x000CF130
		public bool NullsExpectedInData { get; set; }

		// Token: 0x0600428A RID: 17034 RVA: 0x000D0F39 File Offset: 0x000CF139
		public bool IsValueOfType(string value)
		{
			return RichBooleanType.ConversionDictionary.ContainsKey(value.ToLower());
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x000D0F4C File Offset: 0x000CF14C
		public bool AddSample(string value)
		{
			if (this.IsFinalized)
			{
				return false;
			}
			int num;
			if (string.IsNullOrWhiteSpace(value))
			{
				num = this.NaValueCount + 1;
				this.NaValueCount = num;
				return true;
			}
			num = this.SampleCount + 1;
			this.SampleCount = num;
			Optional<string> optional = this.Canonicalize(value);
			if (!optional.HasValue)
			{
				if (this.SingleNonWhitespaceNaValue == null)
				{
					if (!value.Any((char c, int _) => char.IsDigit(c)))
					{
						goto IL_0082;
					}
				}
				if (!(this.SingleNonWhitespaceNaValue == value))
				{
					num = this.RejectionCount + 1;
					this.RejectionCount = num;
					return false;
				}
				IL_0082:
				this.SingleNonWhitespaceNaValue = value;
				num = this.NaValueCount + 1;
				this.NaValueCount = num;
				return true;
			}
			if (optional.Equals(RichBooleanType.TrueString.Some<string>()))
			{
				this._exampleTrueValues.Add(value);
			}
			else
			{
				this._exampleFalseValues.Add(value);
			}
			num = this.AcceptanceCount + 1;
			this.AcceptanceCount = num;
			return true;
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x000D1048 File Offset: 0x000CF248
		public void Finish(long numSamples)
		{
			this.IsFinalized = true;
			this.SuccessOnFinish = !this.EarlyFailure && this.RejectionCount == 0 && this.AcceptanceCount > 0 && (double)this.NaValueCount < 0.5 * (double)numSamples && this._exampleTrueValues.Count > 0 && this._exampleFalseValues.Count > 0;
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x0600428D RID: 17037 RVA: 0x000D10B2 File Offset: 0x000CF2B2
		// (set) Token: 0x0600428E RID: 17038 RVA: 0x000D10BA File Offset: 0x000CF2BA
		public int RejectionCount { get; private set; }

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600428F RID: 17039 RVA: 0x000D10C3 File Offset: 0x000CF2C3
		// (set) Token: 0x06004290 RID: 17040 RVA: 0x000D10CB File Offset: 0x000CF2CB
		public int SampleCount { get; private set; }

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x000D10D4 File Offset: 0x000CF2D4
		// (set) Token: 0x06004292 RID: 17042 RVA: 0x000D10DC File Offset: 0x000CF2DC
		public int AcceptanceCount { get; private set; }

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06004293 RID: 17043 RVA: 0x000D10E5 File Offset: 0x000CF2E5
		// (set) Token: 0x06004294 RID: 17044 RVA: 0x000D10ED File Offset: 0x000CF2ED
		public int NaValueCount { get; private set; }

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x000D10F6 File Offset: 0x000CF2F6
		// (set) Token: 0x06004296 RID: 17046 RVA: 0x000D10FE File Offset: 0x000CF2FE
		public bool SuccessOnFinish { get; private set; }

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06004297 RID: 17047 RVA: 0x000D1107 File Offset: 0x000CF307
		// (set) Token: 0x06004298 RID: 17048 RVA: 0x000D110F File Offset: 0x000CF30F
		public bool EarlyFailure { get; private set; }

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06004299 RID: 17049 RVA: 0x000D1118 File Offset: 0x000CF318
		// (set) Token: 0x0600429A RID: 17050 RVA: 0x000D1120 File Offset: 0x000CF320
		public bool IsFinalized { get; private set; }

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x000D1129 File Offset: 0x000CF329
		private static Dictionary<string, bool> ConversionDictionary
		{
			get
			{
				return RichBooleanType.ConversionDictionaryLazy.Value;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x0600429C RID: 17052 RVA: 0x000D1135 File Offset: 0x000CF335
		public long MinRequiredSamplesForSuccess
		{
			get
			{
				return (long)(this.NaValueCount * 2);
			}
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x000D1140 File Offset: 0x000CF340
		private static Dictionary<string, bool> BuildConversionDictionary()
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			dictionary["t"] = true;
			dictionary["f"] = false;
			dictionary["true"] = true;
			dictionary["false"] = false;
			dictionary["y"] = true;
			dictionary["n"] = false;
			dictionary["yes"] = true;
			dictionary["no"] = false;
			dictionary["1"] = true;
			dictionary["0"] = false;
			dictionary["\"t\""] = true;
			dictionary["\"f\""] = false;
			dictionary["\"true\""] = true;
			dictionary["\"false\""] = false;
			dictionary["\"y\""] = true;
			dictionary["\"n\""] = false;
			dictionary["\"yes\""] = true;
			dictionary["\"no\""] = false;
			dictionary["\"1\""] = true;
			dictionary["\"0\""] = false;
			return dictionary;
		}

		// Token: 0x04001DF5 RID: 7669
		private static readonly string TrueString = true.ToString(CultureInfo.InvariantCulture);

		// Token: 0x04001DF6 RID: 7670
		private static readonly string FalseString = false.ToString(CultureInfo.InvariantCulture);

		// Token: 0x04001DF7 RID: 7671
		private readonly HashSet<string> _exampleTrueValues = new HashSet<string>();

		// Token: 0x04001DF8 RID: 7672
		private readonly HashSet<string> _exampleFalseValues = new HashSet<string>();

		// Token: 0x04001E04 RID: 7684
		private static readonly Lazy<Dictionary<string, bool>> ConversionDictionaryLazy = new Lazy<Dictionary<string, bool>>(new Func<Dictionary<string, bool>>(RichBooleanType.BuildConversionDictionary));
	}
}
