using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000C0 RID: 192
	public sealed class NameGenerator
	{
		// Token: 0x06000B17 RID: 2839 RVA: 0x00024D06 File Offset: 0x00022F06
		public NameGenerator()
			: this(null, null)
		{
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00024D10 File Offset: 0x00022F10
		public NameGenerator(IEqualityComparer<object> keyComparer, IEqualityComparer<string> nameComparer)
		{
			this.m_keysToNames = new Dictionary<object, string>(keyComparer);
			this.m_names = new Bag<string>(nameComparer ?? StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00024D4F File Offset: 0x00022F4F
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x00024D57 File Offset: 0x00022F57
		public bool ClsCompliant
		{
			get
			{
				return this.m_clsCompliant;
			}
			set
			{
				if (value != this.m_clsCompliant && this.m_names.Count > 0)
				{
					throw new InvalidOperationException();
				}
				this.m_clsCompliant = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00024D7D File Offset: 0x00022F7D
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x00024D85 File Offset: 0x00022F85
		public string DefaultName
		{
			get
			{
				return this.m_defaultName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException();
				}
				if (value != this.m_defaultName && this.m_names.Count > 0)
				{
					throw new InvalidOperationException();
				}
				this.m_defaultName = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00024DBE File Offset: 0x00022FBE
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00024DC6 File Offset: 0x00022FC6
		public int MaxLength
		{
			get
			{
				return this.m_maxLength;
			}
			set
			{
				if (value < 12)
				{
					throw new ArgumentException();
				}
				if (value != this.m_maxLength && this.m_names.Count > 0)
				{
					throw new InvalidOperationException();
				}
				this.m_maxLength = value;
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00024DF7 File Offset: 0x00022FF7
		public void AddExistingName(string name)
		{
			this.m_names.Add(name, true);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00024E06 File Offset: 0x00023006
		public string CreateName(string candidate)
		{
			return this.CreateName(candidate, null);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00024E10 File Offset: 0x00023010
		public string CreateName(string candidate, object key)
		{
			string text;
			if (key != null && this.m_keysToNames.TryGetValue(key, out text))
			{
				return text;
			}
			candidate = this.GetValidCandidate(candidate);
			if (this.m_names.Contains(candidate))
			{
				if (candidate != this.m_defaultName && char.IsDigit(candidate, candidate.Length - 1))
				{
					candidate += "_";
				}
				do
				{
					candidate = StringManipulation.IncrementDigitSuffix(candidate);
					candidate = this.GetValidCandidate(candidate);
				}
				while (this.m_names.Contains(candidate));
			}
			if (key != null)
			{
				this.m_keysToNames.Add(key, candidate);
			}
			this.m_names.Add(candidate);
			return candidate;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00024EB0 File Offset: 0x000230B0
		private string GetValidCandidate(string candidate)
		{
			candidate = (string.IsNullOrEmpty(candidate) ? this.m_defaultName : candidate);
			if (this.m_clsCompliant)
			{
				candidate = StringUtil.GetClsCompliantIdentifier(candidate, this.m_defaultName);
			}
			return StringManipulation.TrimToMaxLength(candidate, this.m_maxLength);
		}

		// Token: 0x04000492 RID: 1170
		private readonly Dictionary<object, string> m_keysToNames;

		// Token: 0x04000493 RID: 1171
		private readonly Bag<string> m_names;

		// Token: 0x04000494 RID: 1172
		private bool m_clsCompliant;

		// Token: 0x04000495 RID: 1173
		private string m_defaultName = "Item";

		// Token: 0x04000496 RID: 1174
		private int m_maxLength = int.MaxValue;
	}
}
