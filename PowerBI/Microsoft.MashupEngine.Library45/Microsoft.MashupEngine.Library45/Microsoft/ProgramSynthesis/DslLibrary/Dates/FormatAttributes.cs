using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000865 RID: 2149
	public class FormatAttributes : IEquatable<FormatAttributes>
	{
		// Token: 0x06002EBE RID: 11966 RVA: 0x000867B2 File Offset: 0x000849B2
		public FormatAttributes(IReadOnlyDictionary<string, string> attributes)
			: this(attributes, true)
		{
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000867BC File Offset: 0x000849BC
		internal FormatAttributes(IReadOnlyDictionary<string, string> attributes, bool checkUnhandled)
		{
			if (!attributes.Any<KeyValuePair<string, string>>())
			{
				throw new ArgumentException("Must have one or more attributes. Use null instead to represent no attributes.", "attributes");
			}
			this.Attributes = attributes.ToImmutableDictionary<string, string>();
			this.UnhandledAttributes = (checkUnhandled ? attributes.Keys.ConvertToHashSet<string>() : null);
			this.FormatSuffix = "{" + string.Join(";", this.Attributes.OrderBy((KeyValuePair<string, string> a) => a.Key, StringComparer.Ordinal).Select(delegate(KeyValuePair<string, string> kv)
			{
				if (kv.Value != null)
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}={1}", new object[]
					{
						kv.Key,
						kv.Value.ToLiteral(null)
					}));
				}
				return kv.Key;
			})) + "}";
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002EC0 RID: 11968 RVA: 0x0008687C File Offset: 0x00084A7C
		public ImmutableDictionary<string, string> Attributes { get; }

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x00086884 File Offset: 0x00084A84
		// (set) Token: 0x06002EC2 RID: 11970 RVA: 0x0008688C File Offset: 0x00084A8C
		private HashSet<string> UnhandledAttributes { get; set; }

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x00086895 File Offset: 0x00084A95
		internal bool HasUnhandledAttributes
		{
			get
			{
				return this.UnhandledAttributes != null && this.UnhandledAttributes.Count > 0;
			}
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000868AF File Offset: 0x00084AAF
		internal void MarkAttributeAsHandled(string attribute)
		{
			if (this.UnhandledAttributes == null || !this.UnhandledAttributes.Contains(attribute))
			{
				return;
			}
			this.UnhandledAttributes.Remove(attribute);
			if (this.UnhandledAttributes.Count == 0)
			{
				this.UnhandledAttributes = null;
			}
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000868E9 File Offset: 0x00084AE9
		public bool Equals(FormatAttributes other)
		{
			return this == other || (other != null && this.Attributes.DictionaryEquals(other.Attributes, null));
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x00086908 File Offset: 0x00084B08
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((FormatAttributes)obj)));
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x00086936 File Offset: 0x00084B36
		public override int GetHashCode()
		{
			return this.Attributes.OrderIndependentKeyValueHashCode<string, string>();
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x00086943 File Offset: 0x00084B43
		public FormatAttributes CloneWithAllAttributesUnhandled()
		{
			return new FormatAttributes(this.Attributes);
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x00086950 File Offset: 0x00084B50
		public string FormatSuffix { get; }

		// Token: 0x06002ECA RID: 11978 RVA: 0x00086958 File Offset: 0x00084B58
		public bool TryMerge(FormatAttributes other, out FormatAttributes result)
		{
			if (other == null)
			{
				result = this;
				return true;
			}
			result = null;
			if (!this.Attributes.All(delegate(KeyValuePair<string, string> kvp)
			{
				string text;
				return !other.Attributes.TryGetValue(kvp.Key, out text) || text == kvp.Value;
			}))
			{
				return false;
			}
			if (!other.Attributes.All(delegate(KeyValuePair<string, string> kvp)
			{
				string text2;
				return !this.Attributes.TryGetValue(kvp.Key, out text2) || text2 == kvp.Value;
			}))
			{
				return false;
			}
			result = new FormatAttributes((from kvp in this.Attributes.Union(other.Attributes)
				group kvp by kvp.Key).ToDictionary((IGrouping<string, KeyValuePair<string, string>> g) => g.Key, (IGrouping<string, KeyValuePair<string, string>> g) => g.First<KeyValuePair<string, string>>().Value));
			return true;
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x00086A49 File Offset: 0x00084C49
		public override string ToString()
		{
			return this.FormatSuffix;
		}
	}
}
