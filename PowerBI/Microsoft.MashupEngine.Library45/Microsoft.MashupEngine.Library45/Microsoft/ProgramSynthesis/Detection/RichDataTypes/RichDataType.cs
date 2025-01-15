using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000A7F RID: 2687
	public abstract class RichDataType<TSyntacticType> : IRichDataType, IEquatable<IRichDataType>, IEquatable<RichDataType<TSyntacticType>> where TSyntacticType : SyntacticType
	{
		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x060042D8 RID: 17112 RVA: 0x000D15CF File Offset: 0x000CF7CF
		// (set) Token: 0x060042D9 RID: 17113 RVA: 0x000D15D7 File Offset: 0x000CF7D7
		protected List<SyntacticTypeOptionSet<TSyntacticType>> TypeClusters { get; set; }

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x060042DA RID: 17114 RVA: 0x000D15E0 File Offset: 0x000CF7E0
		public IReadOnlyList<IEnumerable<TSyntacticType>> SyntacticClusters
		{
			get
			{
				return this.TypeClusters;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060042DB RID: 17115 RVA: 0x000D15E8 File Offset: 0x000CF7E8
		// (set) Token: 0x060042DC RID: 17116 RVA: 0x000D15F0 File Offset: 0x000CF7F0
		public bool IsFinalized { get; private set; }

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x060042DD RID: 17117 RVA: 0x000D15F9 File Offset: 0x000CF7F9
		// (set) Token: 0x060042DE RID: 17118 RVA: 0x000D1601 File Offset: 0x000CF801
		public bool SuccessOnFinish { get; protected set; }

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x000D160A File Offset: 0x000CF80A
		// (set) Token: 0x060042E0 RID: 17120 RVA: 0x000D1612 File Offset: 0x000CF812
		public bool EarlyFailure { get; protected set; }

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x060042E1 RID: 17121 RVA: 0x000D161B File Offset: 0x000CF81B
		// (set) Token: 0x060042E2 RID: 17122 RVA: 0x000D1623 File Offset: 0x000CF823
		public bool EmptyStringsExpectedInData { get; set; }

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x000D162C File Offset: 0x000CF82C
		// (set) Token: 0x060042E4 RID: 17124 RVA: 0x000D1634 File Offset: 0x000CF834
		public bool NormalizableStringsExpectedInData { get; set; }

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x060042E5 RID: 17125 RVA: 0x000D163D File Offset: 0x000CF83D
		// (set) Token: 0x060042E6 RID: 17126 RVA: 0x000D1645 File Offset: 0x000CF845
		public bool NullsExpectedInData { get; set; }

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x000D164E File Offset: 0x000CF84E
		public DataKind BaseKind { get; }

		// Token: 0x060042E8 RID: 17128 RVA: 0x000D1656 File Offset: 0x000CF856
		public void FixOneInterpretation()
		{
			this.TypeClusters = this.TypeClusters.Select((SyntacticTypeOptionSet<TSyntacticType> t) => SyntacticTypeOptionSet.From<TSyntacticType>(t.First<TSyntacticType>())).ToList<SyntacticTypeOptionSet<TSyntacticType>>();
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x060042E9 RID: 17129 RVA: 0x000D168D File Offset: 0x000CF88D
		public IReadOnlyList<TSyntacticType> PickOneInterpretation
		{
			get
			{
				return this.TypeClusters.Select((SyntacticTypeOptionSet<TSyntacticType> t) => t.First<TSyntacticType>()).ToList<TSyntacticType>();
			}
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x000D16BE File Offset: 0x000CF8BE
		protected RichDataType(DataKind kind)
		{
			this.BaseKind = kind;
			this.TypeClusters = new List<SyntacticTypeOptionSet<TSyntacticType>>();
			this.NaValueSet = new HashSet<string>();
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x060042EB RID: 17131 RVA: 0x000D16E3 File Offset: 0x000CF8E3
		public virtual DataKind Kind
		{
			get
			{
				return this.BaseKind;
			}
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x000D16EC File Offset: 0x000CF8EC
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
			if (this.NaValueSet.Contains(value))
			{
				return Optional<string>.Nothing;
			}
			if (string.IsNullOrEmpty(value))
			{
				return Optional<string>.Nothing;
			}
			return this.TypeClusters.SelectMany((SyntacticTypeOptionSet<TSyntacticType> t) => t).FirstValue((TSyntacticType type) => type.Canonicalize(value));
		}

		// Token: 0x060042ED RID: 17133
		public abstract Optional<object> MaybeCastAsType(string value);

		// Token: 0x060042EE RID: 17134 RVA: 0x000D1798 File Offset: 0x000CF998
		public bool IsValueOfType(string value)
		{
			return this.TypeClusters.SelectMany((SyntacticTypeOptionSet<TSyntacticType> t) => t).Any((TSyntacticType t) => t.IsValid(value));
		}

		// Token: 0x060042EF RID: 17135
		public abstract bool Equals(RichDataType<TSyntacticType> other);

		// Token: 0x060042F0 RID: 17136 RVA: 0x000D17F0 File Offset: 0x000CF9F0
		public bool AddSample(string value)
		{
			if (this.IsFinalized)
			{
				return false;
			}
			int num = this.SampleCount + 1;
			this.SampleCount = num;
			SyntacticTypeOptionSet<TSyntacticType> syntacticTypeOptionSet = this.TypeClusters.FirstOrDefault((SyntacticTypeOptionSet<TSyntacticType> c) => c.MatchesAll(value));
			if (syntacticTypeOptionSet != null)
			{
				this.NotifySample(value);
				syntacticTypeOptionSet.AddExample(value);
				TSyntacticType tsyntacticType = syntacticTypeOptionSet.OnlyOrDefault<TSyntacticType>();
				if (tsyntacticType != null && tsyntacticType.IsNaValue)
				{
					num = this.NaValueCount + 1;
					this.NaValueCount = num;
				}
				else
				{
					num = this.AcceptanceCount + 1;
					this.AcceptanceCount = num;
				}
				return true;
			}
			foreach (SyntacticTypeOptionSet<TSyntacticType> syntacticTypeOptionSet2 in this.TypeClusters)
			{
				if (syntacticTypeOptionSet2.Refine(value))
				{
					syntacticTypeOptionSet2.AddExample(value);
					this.NotifySample(value);
					num = this.AcceptanceCount + 1;
					this.AcceptanceCount = num;
					return true;
				}
			}
			SyntacticTypeOptionSet<TSyntacticType> syntacticTypeOptionSet3 = this.ProcessSample(value);
			if (syntacticTypeOptionSet3 == null || !syntacticTypeOptionSet3.Any<TSyntacticType>())
			{
				num = this.RejectionCount + 1;
				this.RejectionCount = num;
				return false;
			}
			TSyntacticType tsyntacticType2 = syntacticTypeOptionSet3.OnlyOrDefault<TSyntacticType>();
			if (tsyntacticType2 != null && tsyntacticType2.IsNaValue)
			{
				this.NaValueSet.Add(value);
				num = this.NaValueCount + 1;
				this.NaValueCount = num;
				if (this.SampleCount > 100 && (double)this.NaValueCount > 0.9 * (double)this.SampleCount && (double)this.NaValueSet.Count > 0.5 * (double)this.SampleCount)
				{
					this.EarlyFailure = true;
				}
			}
			else
			{
				num = this.AcceptanceCount + 1;
				this.AcceptanceCount = num;
			}
			syntacticTypeOptionSet3.AddExample(value);
			this.TypeClusters.Add(syntacticTypeOptionSet3);
			return true;
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x060042F1 RID: 17137 RVA: 0x000D19F8 File Offset: 0x000CFBF8
		// (set) Token: 0x060042F2 RID: 17138 RVA: 0x000D1A00 File Offset: 0x000CFC00
		public int RejectionCount { get; private set; }

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x060042F3 RID: 17139 RVA: 0x000D1A09 File Offset: 0x000CFC09
		// (set) Token: 0x060042F4 RID: 17140 RVA: 0x000D1A11 File Offset: 0x000CFC11
		public int SampleCount { get; private set; }

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x060042F5 RID: 17141 RVA: 0x000D1A1A File Offset: 0x000CFC1A
		// (set) Token: 0x060042F6 RID: 17142 RVA: 0x000D1A22 File Offset: 0x000CFC22
		public int AcceptanceCount { get; private set; }

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x060042F7 RID: 17143 RVA: 0x000D1A2B File Offset: 0x000CFC2B
		// (set) Token: 0x060042F8 RID: 17144 RVA: 0x000D1A33 File Offset: 0x000CFC33
		public int NaValueCount { get; private set; }

		// Token: 0x060042F9 RID: 17145
		protected abstract SyntacticTypeOptionSet<TSyntacticType> ProcessSample(string sample);

		// Token: 0x060042FA RID: 17146 RVA: 0x0000CC37 File Offset: 0x0000AE37
		protected virtual void NotifySample(string sample)
		{
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x000D1A3C File Offset: 0x000CFC3C
		public void Finish(long numSamples)
		{
			if (this.IsFinalized)
			{
				return;
			}
			if (this.EarlyFailure)
			{
				this.SuccessOnFinish = false;
			}
			else
			{
				this.FinishImpl(numSamples);
			}
			this.IsFinalized = true;
		}

		// Token: 0x060042FC RID: 17148
		protected abstract void FinishImpl(long numSamples);

		// Token: 0x060042FD RID: 17149 RVA: 0x000D1A66 File Offset: 0x000CFC66
		public bool Equals(IRichDataType other)
		{
			return other == this || (other != null && this.Equals(other as RichDataType<TSyntacticType>));
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x060042FE RID: 17150 RVA: 0x000D1A7F File Offset: 0x000CFC7F
		protected HashSet<string> NaValueSet { get; }

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x060042FF RID: 17151 RVA: 0x000D1A87 File Offset: 0x000CFC87
		public IEnumerable<string> NaValues
		{
			get
			{
				return this.NaValueSet;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06004300 RID: 17152
		public abstract long MinRequiredSamplesForSuccess { get; }
	}
}
