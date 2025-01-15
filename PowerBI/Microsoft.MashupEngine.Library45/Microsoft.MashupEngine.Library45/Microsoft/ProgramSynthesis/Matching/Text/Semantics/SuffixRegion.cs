using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Caching;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x02001223 RID: 4643
	[DebuggerDisplay("\"{Source}\"[{Start}:] with {PrefixMatchToken}")]
	public class SuffixRegion : IEquatable<SuffixRegion>, ICachefulObject<SuffixRegion>, ICachefulObject
	{
		// Token: 0x06008BCB RID: 35787 RVA: 0x001D4741 File Offset: 0x001D2941
		public SuffixRegion(string s, uint start = 0U)
			: this(new UnboundedCache<uint, UnboundedCache<IToken, uint>>(null, null, (UnboundedCache<IToken, uint> c) => c.DeepClone()), s, start)
		{
		}

		// Token: 0x06008BCC RID: 35788 RVA: 0x001D4771 File Offset: 0x001D2971
		private SuffixRegion(UnboundedCache<uint, UnboundedCache<IToken, uint>> cache, string s, uint start = 0U)
		{
			this._lazyMatchCache = cache;
			this.Source = s;
			this.Start = start;
			this._value = null;
			this.PrefixMatchToken = null;
		}

		// Token: 0x06008BCD RID: 35789 RVA: 0x001D479C File Offset: 0x001D299C
		private SuffixRegion(SuffixRegion other)
		{
			this._lazyMatchCache = other._lazyMatchCache.DeepClone();
			this.Source = other.Source;
			this.Start = other.Start;
			this._value = other._value;
			this._hashCode = other._hashCode;
			this.PrefixMatchToken = other.PrefixMatchToken;
		}

		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x06008BCE RID: 35790 RVA: 0x001D47FC File Offset: 0x001D29FC
		public int Length
		{
			get
			{
				return (int)((long)this.Source.Length - (long)((ulong)this.Start));
			}
		}

		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x06008BCF RID: 35791 RVA: 0x001D4813 File Offset: 0x001D2A13
		// (set) Token: 0x06008BD0 RID: 35792 RVA: 0x001D481B File Offset: 0x001D2A1B
		public IToken PrefixMatchToken { get; private set; }

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x06008BD1 RID: 35793 RVA: 0x001D4824 File Offset: 0x001D2A24
		public uint Start { get; }

		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x06008BD2 RID: 35794 RVA: 0x001D482C File Offset: 0x001D2A2C
		public string Source { get; }

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x06008BD3 RID: 35795 RVA: 0x001D4834 File Offset: 0x001D2A34
		public string Value
		{
			get
			{
				string text;
				if ((text = this._value) == null)
				{
					string source = this.Source;
					text = (this._value = ((source != null) ? source.Substring(Convert.ToInt32(this.Start)) : null));
				}
				return text;
			}
		}

		// Token: 0x170017F1 RID: 6129
		public char this[int i]
		{
			get
			{
				return this.Source[i + (int)this.Start];
			}
		}

		// Token: 0x06008BD5 RID: 35797 RVA: 0x001D4886 File Offset: 0x001D2A86
		public bool Equals(SuffixRegion other)
		{
			return this == other || (other != null && this.Source == other.Source && this.Start == other.Start);
		}

		// Token: 0x06008BD6 RID: 35798 RVA: 0x001D48B8 File Offset: 0x001D2AB8
		public uint PrefixMatchLength(IToken token)
		{
			if (token == null || this.Source == null || (ulong)this.Start == (ulong)((long)this.Source.Length))
			{
				return 0U;
			}
			UnboundedCache<IToken, uint> unboundedCache = this._lazyMatchCache.LookupOrCreateValue(this.Start);
			uint num;
			if (unboundedCache.Lookup(token, out num))
			{
				return num;
			}
			CharClassToken charClassToken = token as CharClassToken;
			if (charClassToken == null)
			{
				num = token.PrefixMatchLength(this.Value);
				unboundedCache.Add(token, num);
				return num;
			}
			CharClassToken unrestrictedToken = charClassToken.UnrestrictedToken;
			if (!unboundedCache.Lookup(unrestrictedToken, out num))
			{
				num = unrestrictedToken.PrefixMatchLength(this.Value);
				for (uint num2 = 0U; num2 <= num; num2 += 1U)
				{
					uint num3;
					this._lazyMatchCache.LookupOrCreateValue(this.Start + num2).Replace(unrestrictedToken, num - num2, out num3);
				}
			}
			if (charClassToken.RequiredLength == null)
			{
				return num;
			}
			uint num4 = ((charClassToken.RequiredLength.Value == num) ? num : 0U);
			unboundedCache.Add(charClassToken, num4);
			return num4;
		}

		// Token: 0x06008BD7 RID: 35799 RVA: 0x001D49B4 File Offset: 0x001D2BB4
		public SuffixRegion UnmatchedSuffix(IToken token)
		{
			uint num = this.PrefixMatchLength(token);
			if (num >= 1U)
			{
				return new SuffixRegion(this._lazyMatchCache, this.Source, this.Start + num)
				{
					PrefixMatchToken = token
				};
			}
			return null;
		}

		// Token: 0x06008BD8 RID: 35800 RVA: 0x001D49EF File Offset: 0x001D2BEF
		public override string ToString()
		{
			if (this.Source != null)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}[{1}:]", new object[] { this.Source, this.Start }));
			}
			return "<NULL>";
		}

		// Token: 0x06008BD9 RID: 35801 RVA: 0x001D4A2B File Offset: 0x001D2C2B
		public override bool Equals(object other)
		{
			return this == other || (other != null && !(base.GetType() != other.GetType()) && this.Equals((SuffixRegion)other));
		}

		// Token: 0x06008BDA RID: 35802 RVA: 0x001D4A5C File Offset: 0x001D2C5C
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			string source = this.Source;
			this._hashCode = new int?((((source != null) ? new int?(source.GetHashCode() * 3873349) : null) ^ this.Start.GetHashCode()).GetValueOrDefault());
			return this._hashCode.Value;
		}

		// Token: 0x06008BDB RID: 35803 RVA: 0x001D4AF7 File Offset: 0x001D2CF7
		public SuffixRegion CloneWithCurrentCacheState()
		{
			return new SuffixRegion(this);
		}

		// Token: 0x06008BDC RID: 35804 RVA: 0x001D4AFF File Offset: 0x001D2CFF
		public void ClearCaches()
		{
			this._lazyMatchCache.Clear();
		}

		// Token: 0x06008BDD RID: 35805 RVA: 0x001D4B0C File Offset: 0x001D2D0C
		ICachefulObject ICachefulObject.CloneWithCurrentCacheState()
		{
			return this.CloneWithCurrentCacheState();
		}

		// Token: 0x06008BDE RID: 35806 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(SuffixRegion left, SuffixRegion right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06008BDF RID: 35807 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(SuffixRegion left, SuffixRegion right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x04003926 RID: 14630
		private readonly UnboundedCache<uint, UnboundedCache<IToken, uint>> _lazyMatchCache;

		// Token: 0x04003927 RID: 14631
		private int? _hashCode;

		// Token: 0x04003928 RID: 14632
		private string _value;
	}
}
