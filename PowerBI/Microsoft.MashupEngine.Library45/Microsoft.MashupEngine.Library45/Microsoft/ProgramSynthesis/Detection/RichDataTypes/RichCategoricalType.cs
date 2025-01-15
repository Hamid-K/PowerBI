using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000A7C RID: 2684
	public class RichCategoricalType : IRichDataType, IEquatable<IRichDataType>
	{
		// Token: 0x060042A4 RID: 17060 RVA: 0x000D12DF File Offset: 0x000CF4DF
		public RichCategoricalType(int maxCategories = 1000, int minSamplesForCategorical = 50, double sampleCountMultiplier = 0.1)
		{
			this.MaxCategories = maxCategories;
			this.MinSamplesForCategorical = minSamplesForCategorical;
			this.SampleCountMultiplier = sampleCountMultiplier;
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060042A5 RID: 17061 RVA: 0x000D1320 File Offset: 0x000CF520
		public IReadOnlyList<string> Categories
		{
			get
			{
				IReadOnlyList<string> readOnlyList;
				if ((readOnlyList = this._categories) == null)
				{
					readOnlyList = (this._categories = this._categoricalValues.Keys.ToList<string>());
				}
				return readOnlyList;
			}
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x000D1350 File Offset: 0x000CF550
		public bool Equals(IRichDataType other)
		{
			if (other == this)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			RichCategoricalType richCategoricalType = other as RichCategoricalType;
			return richCategoricalType != null && richCategoricalType._categoricalValues.Keys.ConvertToHashSet<string>().SetEquals(this._categoricalValues.Keys);
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x000D1394 File Offset: 0x000CF594
		public Optional<string> Canonicalize(string value)
		{
			if (this.NormalizableStringsExpectedInData)
			{
				value = value.NormalizeAndTrim(NormalizationForm.FormC);
			}
			if (this.EmptyStringsExpectedInData && string.IsNullOrEmpty(value))
			{
				return string.Empty.Some<string>();
			}
			if (this._categoricalValues.ContainsKey(value))
			{
				return value.Some<string>();
			}
			return Optional<string>.Nothing;
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x000D13E7 File Offset: 0x000CF5E7
		public Optional<object> MaybeCastAsType(string value)
		{
			return this.Canonicalize(value).Cast<object>();
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x0001B224 File Offset: 0x00019424
		public DataKind BaseKind
		{
			get
			{
				return DataKind.Categorical;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060042AA RID: 17066 RVA: 0x0001B224 File Offset: 0x00019424
		public DataKind Kind
		{
			get
			{
				return DataKind.Categorical;
			}
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x000D13FA File Offset: 0x000CF5FA
		public bool IsValueOfType(string value)
		{
			return this._categoricalValues.ContainsKey(value);
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x000D1408 File Offset: 0x000CF608
		public bool AddSample(string value)
		{
			if (this.IsFinalized)
			{
				return false;
			}
			int num = this.SampleCount + 1;
			this.SampleCount = num;
			int num2;
			if (this._categoricalValues.TryGetValue(value, out num2))
			{
				this._categoricalValues[value] = num2 + 1;
				num = this.AcceptanceCount + 1;
				this.AcceptanceCount = num;
				return true;
			}
			if (this._categoricalValues.Count < this.MaxCategories)
			{
				this._categoricalValues[value] = 1;
				this._categories = null;
				num = this.AcceptanceCount + 1;
				this.AcceptanceCount = num;
				return true;
			}
			num = this.RejectionCount + 1;
			this.RejectionCount = num;
			return false;
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x000D14AC File Offset: 0x000CF6AC
		public void Finish(long _)
		{
			if (this.IsFinalized)
			{
				return;
			}
			this.IsFinalized = true;
			this.SuccessOnFinish = !this.EarlyFailure && this.RejectionCount <= 0 && this.SampleCount >= this.MinSamplesForCategorical && (double)this._categoricalValues.Count <= (double)this.SampleCount * this.SampleCountMultiplier;
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x000D1510 File Offset: 0x000CF710
		// (set) Token: 0x060042AF RID: 17071 RVA: 0x000D1518 File Offset: 0x000CF718
		public int RejectionCount { get; private set; }

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x000D1521 File Offset: 0x000CF721
		// (set) Token: 0x060042B1 RID: 17073 RVA: 0x000D1529 File Offset: 0x000CF729
		public int SampleCount { get; private set; }

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x000D1532 File Offset: 0x000CF732
		// (set) Token: 0x060042B3 RID: 17075 RVA: 0x000D153A File Offset: 0x000CF73A
		public int AcceptanceCount { get; private set; }

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x000D1543 File Offset: 0x000CF743
		public int NaValueCount { get; }

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060042B5 RID: 17077 RVA: 0x000D154B File Offset: 0x000CF74B
		// (set) Token: 0x060042B6 RID: 17078 RVA: 0x000D1553 File Offset: 0x000CF753
		public bool SuccessOnFinish { get; private set; }

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060042B7 RID: 17079 RVA: 0x000D155C File Offset: 0x000CF75C
		// (set) Token: 0x060042B8 RID: 17080 RVA: 0x000D1564 File Offset: 0x000CF764
		public bool EarlyFailure { get; private set; }

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x060042B9 RID: 17081 RVA: 0x000D156D File Offset: 0x000CF76D
		// (set) Token: 0x060042BA RID: 17082 RVA: 0x000D1575 File Offset: 0x000CF775
		public bool IsFinalized { get; private set; }

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x060042BB RID: 17083 RVA: 0x000D157E File Offset: 0x000CF77E
		// (set) Token: 0x060042BC RID: 17084 RVA: 0x000D1586 File Offset: 0x000CF786
		public bool EmptyStringsExpectedInData { get; set; }

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x060042BD RID: 17085 RVA: 0x000D158F File Offset: 0x000CF78F
		// (set) Token: 0x060042BE RID: 17086 RVA: 0x000D1597 File Offset: 0x000CF797
		public bool NormalizableStringsExpectedInData { get; set; }

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x060042BF RID: 17087 RVA: 0x000D15A0 File Offset: 0x000CF7A0
		// (set) Token: 0x060042C0 RID: 17088 RVA: 0x000D15A8 File Offset: 0x000CF7A8
		public bool NullsExpectedInData { get; set; }

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x000D15B1 File Offset: 0x000CF7B1
		private int MaxCategories { get; }

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060042C2 RID: 17090 RVA: 0x000D15B9 File Offset: 0x000CF7B9
		public long MinRequiredSamplesForSuccess
		{
			get
			{
				return (long)((double)this._categoricalValues.Count / this.SampleCountMultiplier);
			}
		}

		// Token: 0x04001E08 RID: 7688
		private readonly Dictionary<string, int> _categoricalValues = new Dictionary<string, int>();

		// Token: 0x04001E09 RID: 7689
		private IReadOnlyList<string> _categories;

		// Token: 0x04001E14 RID: 7700
		private const int DefaultMaxCategories = 1000;

		// Token: 0x04001E15 RID: 7701
		private const int DefaultMinSamplesForCategorical = 50;

		// Token: 0x04001E16 RID: 7702
		private const double DefaultSampleCountMultiplier = 0.1;

		// Token: 0x04001E18 RID: 7704
		private int MinSamplesForCategorical = 50;

		// Token: 0x04001E19 RID: 7705
		private double SampleCountMultiplier = 0.1;
	}
}
