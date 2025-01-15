using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion
{
	// Token: 0x0200024B RID: 587
	[JsonObject(MemberSerialization.OptIn)]
	public class Suggestion : ISuggestion, IEquatable<ISuggestion>
	{
		// Token: 0x06000C84 RID: 3204 RVA: 0x000256C0 File Offset: 0x000238C0
		public Suggestion(IAutoCompleter source, string prefixString, uint matchOffset, string completionSuffix, IReadOnlyDictionary<string, object> metadata, string suggestEventId)
		{
			this.Source = source;
			this.PrefixString = prefixString;
			this.MatchOffset = matchOffset;
			this.CompletionSuffix = completionSuffix;
			this.Metadata = metadata;
			this.SuggestEventId = suggestEventId;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x000256F5 File Offset: 0x000238F5
		public string SuggestEventId { get; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x000256FD File Offset: 0x000238FD
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x00025705 File Offset: 0x00023905
		public IAutoCompleter Source { get; private set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0002570E File Offset: 0x0002390E
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x00025716 File Offset: 0x00023916
		[JsonProperty]
		public string PrefixString { get; private set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0002571F File Offset: 0x0002391F
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x00025727 File Offset: 0x00023927
		[JsonProperty]
		public uint MatchOffset { get; private set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00025730 File Offset: 0x00023930
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x00025738 File Offset: 0x00023938
		[JsonProperty]
		public string CompletionSuffix { get; private set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00025741 File Offset: 0x00023941
		public IReadOnlyDictionary<string, object> Metadata { get; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0002574C File Offset: 0x0002394C
		public string CompleteValue
		{
			get
			{
				string text;
				if ((text = this._value) == null)
				{
					text = (this._value = this.PrefixString.Substring(0, (int)this.MatchOffset) + this.CompletionSuffix);
				}
				return text;
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002578C File Offset: 0x0002398C
		public bool Equals(ISuggestion other)
		{
			return other != null && (other == this || (!(base.GetType() != other.GetType()) && (other.PrefixString == this.PrefixString && other.MatchOffset == this.MatchOffset && other.CompletionSuffix == this.CompletionSuffix) && other.Metadata == this.Metadata));
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x000257FD File Offset: 0x000239FD
		public override bool Equals(object other)
		{
			return other != null && (other == this || (!(base.GetType() != other.GetType()) && this.Equals(other as Suggestion)));
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002582C File Offset: 0x00023A2C
		public override int GetHashCode()
		{
			return (((this.PrefixString.GetHashCode() * 10477) ^ this.MatchOffset.GetHashCode()) * 11383) ^ this.CompletionSuffix.GetHashCode();
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002586B File Offset: 0x00023A6B
		public override string ToString()
		{
			return this.CompleteValue;
		}

		// Token: 0x04000632 RID: 1586
		public const string ProvenancePropertyName = "Provenance";

		// Token: 0x04000633 RID: 1587
		private string _value;
	}
}
