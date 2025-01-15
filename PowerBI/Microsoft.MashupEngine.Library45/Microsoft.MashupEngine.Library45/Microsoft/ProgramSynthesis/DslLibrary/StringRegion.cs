using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x02000805 RID: 2053
	[DebuggerDisplay("{Start}-{End} : {Value}")]
	[JsonConverter(typeof(StringRegionSerializer))]
	public class StringRegion : LearningCacheSubstring, IRegion<StringRegion>, IComparable<StringRegion>, IEquatable<StringRegion>, ICachefulObject<StringRegion>, ICachefulObject, ISubstring<StringRegion>, ISubstring
	{
		// Token: 0x06002BDF RID: 11231 RVA: 0x0007B244 File Offset: 0x00079444
		private StringRegion(string s, uint start, uint end, StringLearningCache cache)
			: base(s, start, end, cache)
		{
			this.StartPositionIsPrecise = true;
			this.EndPositionIsPrecise = true;
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x0007B25F File Offset: 0x0007945F
		public StringRegion(string s, IReadOnlyDictionary<string, Token> tokens)
			: base(s, tokens)
		{
			this.StartPositionIsPrecise = true;
			this.EndPositionIsPrecise = true;
			this.WholeRegion = this;
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x0007B27E File Offset: 0x0007947E
		public StringRegion(StringLearningCache cache)
			: this(cache.Content, 0U, (uint)cache.Content.Length, cache)
		{
			this.WholeRegion = this;
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x0007B2A0 File Offset: 0x000794A0
		private StringRegion(StringRegion other)
			: base(other)
		{
			this.StartPositionIsPrecise = other.StartPositionIsPrecise;
			this.EndPositionIsPrecise = other.EndPositionIsPrecise;
			this.WholeRegion = other.WholeRegion;
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x0007B2CD File Offset: 0x000794CD
		// (set) Token: 0x06002BE4 RID: 11236 RVA: 0x0007B2D5 File Offset: 0x000794D5
		public bool StartPositionIsPrecise { get; set; }

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002BE5 RID: 11237 RVA: 0x0007B2DE File Offset: 0x000794DE
		// (set) Token: 0x06002BE6 RID: 11238 RVA: 0x0007B2E6 File Offset: 0x000794E6
		public bool EndPositionIsPrecise { get; set; }

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x0007B2EF File Offset: 0x000794EF
		// (set) Token: 0x06002BE8 RID: 11240 RVA: 0x0007B2F7 File Offset: 0x000794F7
		public StringRegion WholeRegion { get; private set; }

		// Token: 0x06002BE9 RID: 11241 RVA: 0x0007B300 File Offset: 0x00079500
		public bool Equals(StringRegion other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && (base.Source == other.Source && base.Start == other.Start) && base.End == other.End));
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x0007B35C File Offset: 0x0007955C
		public int CompareTo(StringRegion other)
		{
			if (other == null)
			{
				return 1;
			}
			if (base.Start < other.Start)
			{
				return -1;
			}
			if (base.Start != other.Start)
			{
				return 1;
			}
			if (base.End < other.End)
			{
				return -1;
			}
			return (base.End != other.End) ? 1 : 0;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x0007B3B8 File Offset: 0x000795B8
		public bool IntersectNonEmpty(StringRegion region)
		{
			return !(base.Source != region.Source) && ((base.End > region.Start && base.Start < region.End) || (base.End == base.Start && region.Start <= base.Start && region.End >= base.End) || (region.End == region.Start && base.Start <= region.Start && base.End >= region.End));
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x0007B454 File Offset: 0x00079654
		public bool Contains(StringRegion other)
		{
			return !(other == null) && !(other.Source != base.Source) && base.Start <= other.Start && base.End >= other.End;
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x0007B4A0 File Offset: 0x000796A0
		public bool IsBefore(StringRegion other)
		{
			return base.End < other.End && base.Start < other.Start;
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x0007B4C0 File Offset: 0x000796C0
		public StringRegion ClipBefore(StringRegion other)
		{
			return this.Slice(base.Start, Math.Min(base.End, other.Start));
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x0007B4DF File Offset: 0x000796DF
		public override int GetHashCode()
		{
			return (((RuntimeHelpers.GetHashCode(base.Source) * 5458421) ^ (int)base.Start) * 5458421) ^ (int)base.End;
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x0007B506 File Offset: 0x00079706
		ICachefulObject ICachefulObject.CloneWithCurrentCacheState()
		{
			return this.CloneWithCurrentCacheState();
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x0007B50E File Offset: 0x0007970E
		public void ClearCaches()
		{
			base.Cache.ClearCaches();
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(StringRegion left, StringRegion right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(StringRegion left, StringRegion right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x0007B51B File Offset: 0x0007971B
		public StringRegion CloneWithCurrentCacheState()
		{
			return new StringRegion(this);
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x0001B9A2 File Offset: 0x00019BA2
		public override string ToString()
		{
			return base.Value;
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x0007B524 File Offset: 0x00079724
		public StringRegion Slice(uint start, uint end)
		{
			if (start < base.Start)
			{
				throw new ArgumentOutOfRangeException("start", FormattableString.Invariant(FormattableStringFactory.Create("Starting slicing position {0} is less than the region start {1}", new object[] { start, base.Start })));
			}
			if (end > base.End)
			{
				throw new ArgumentOutOfRangeException("end", FormattableString.Invariant(FormattableStringFactory.Create("Ending slicing position {0} exceeds the region end {1}", new object[] { end, base.End })));
			}
			return new StringRegion(base.Source, start, end, base.Cache)
			{
				WholeRegion = this.WholeRegion
			};
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x0007B5D0 File Offset: 0x000797D0
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((StringRegion)obj)));
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x0007B5FE File Offset: 0x000797FE
		public StringRegion AbsoluteSlice(uint start, uint end)
		{
			return this.Slice(start, end);
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x0007B608 File Offset: 0x00079808
		public StringRegion RelativeSlice(uint start, uint end)
		{
			return this.Slice(start + base.Start, end + base.Start);
		}
	}
}
