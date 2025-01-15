using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001CC8 RID: 7368
	public class StringPrefixSet : IEquatable<StringPrefixSet>
	{
		// Token: 0x0600F9DD RID: 63965 RVA: 0x0035257C File Offset: 0x0035077C
		public StringPrefixSet(string prefix)
		{
			this.Prefix = ValueSubstring.Create(prefix, null, null, null, null);
		}

		// Token: 0x0600F9DE RID: 63966 RVA: 0x003525AF File Offset: 0x003507AF
		public StringPrefixSet(ValueSubstring prefix)
		{
			this.Prefix = prefix;
		}

		// Token: 0x170029D7 RID: 10711
		// (get) Token: 0x0600F9DF RID: 63967 RVA: 0x003525BE File Offset: 0x003507BE
		public ValueSubstring Prefix { get; }

		// Token: 0x0600F9E0 RID: 63968 RVA: 0x003525C6 File Offset: 0x003507C6
		public bool Contains(string str)
		{
			return str.StartsWith(this.Prefix.Value, StringComparison.Ordinal);
		}

		// Token: 0x0600F9E1 RID: 63969 RVA: 0x003525DA File Offset: 0x003507DA
		public bool Contains(StringPrefixSet other)
		{
			return other.Prefix.StartsWith(this.Prefix);
		}

		// Token: 0x0600F9E2 RID: 63970 RVA: 0x003525ED File Offset: 0x003507ED
		public bool IsPrefixOfSomeStringInSet(string str)
		{
			return str.StartsWith(this.Prefix.Value, StringComparison.Ordinal) || this.Prefix.Value.StartsWith(str, StringComparison.Ordinal);
		}

		// Token: 0x0600F9E3 RID: 63971 RVA: 0x00352617 File Offset: 0x00350817
		public bool Contains(ValueSubstring vs)
		{
			return vs.StartsWith(this.Prefix);
		}

		// Token: 0x170029D8 RID: 10712
		// (get) Token: 0x0600F9E4 RID: 63972 RVA: 0x00352625 File Offset: 0x00350825
		public static StringPrefixSet AllStrings { get; } = new StringPrefixSet(string.Empty);

		// Token: 0x0600F9E5 RID: 63973 RVA: 0x0035262C File Offset: 0x0035082C
		public bool Equals(StringPrefixSet other)
		{
			return other != null && (other == this || other.Prefix.Equals(this.Prefix));
		}

		// Token: 0x0600F9E6 RID: 63974 RVA: 0x0035264C File Offset: 0x0035084C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			StringPrefixSet stringPrefixSet = obj as StringPrefixSet;
			return stringPrefixSet != null && this.Equals(stringPrefixSet);
		}

		// Token: 0x0600F9E7 RID: 63975 RVA: 0x00352671 File Offset: 0x00350871
		public override int GetHashCode()
		{
			return (this.Prefix.GetHashCode() * 20107) ^ base.GetType().GetHashCode();
		}

		// Token: 0x0600F9E8 RID: 63976 RVA: 0x00352690 File Offset: 0x00350890
		private ValueSubstring Slice(int len)
		{
			return this.Prefix.Slice(this.Prefix.Start, new uint?((uint)((ulong)this.Prefix.Start + (ulong)((long)len))));
		}

		// Token: 0x170029D9 RID: 10713
		// (get) Token: 0x0600F9E9 RID: 63977 RVA: 0x003526C0 File Offset: 0x003508C0
		public IReadOnlyList<ValueSubstring> AllPrefixesOfPrefix
		{
			get
			{
				IReadOnlyList<ValueSubstring> readOnlyList;
				if ((readOnlyList = this._concreteFiniteLengthPrefixes) == null)
				{
					readOnlyList = (this._concreteFiniteLengthPrefixes = Enumerable.Range(1, (int)this.Prefix.Length).Select(new Func<int, ValueSubstring>(this.Slice)).ToList<ValueSubstring>());
				}
				return readOnlyList;
			}
		}

		// Token: 0x0600F9EA RID: 63978 RVA: 0x00352708 File Offset: 0x00350908
		public StringPrefixSet PrefixSetAfter(ValueSubstring matchedPrefixOfPrefix)
		{
			if (matchedPrefixOfPrefix.StartsWith(this.Prefix))
			{
				return StringPrefixSet.AllStrings;
			}
			if (this.Prefix.StartsWith(matchedPrefixOfPrefix))
			{
				return new StringPrefixSet(this.Prefix.SliceRelative(matchedPrefixOfPrefix.Length, null));
			}
			throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("{0} is not a valid prefix for the prefix set.", new object[] { "matchedPrefixOfPrefix" })));
		}

		// Token: 0x0600F9EB RID: 63979 RVA: 0x00352779 File Offset: 0x00350979
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}...", new object[] { this.Prefix }));
		}

		// Token: 0x0600F9EC RID: 63980 RVA: 0x0035279C File Offset: 0x0035099C
		public XElement SerializeToXML(Dictionary<object, int> idCache)
		{
			int num;
			if (idCache.TryGetValue(this, out num))
			{
				return new XElement("Reference", num);
			}
			return new XElement("StringPrefixSet", this.Prefix.SerializeToXML(idCache));
		}

		// Token: 0x0600F9ED RID: 63981 RVA: 0x003527E8 File Offset: 0x003509E8
		public static StringPrefixSet DeserializeFromXML(XElement node, Dictionary<int, object> idCache)
		{
			if (node.Name == "Reference")
			{
				return (StringPrefixSet)idCache[int.Parse(node.Value)];
			}
			return new StringPrefixSet(ValueSubstring.DeserializeFromXML(node.Elements().First<XElement>(), idCache));
		}

		// Token: 0x04005C91 RID: 23697
		private IReadOnlyList<ValueSubstring> _concreteFiniteLengthPrefixes;
	}
}
