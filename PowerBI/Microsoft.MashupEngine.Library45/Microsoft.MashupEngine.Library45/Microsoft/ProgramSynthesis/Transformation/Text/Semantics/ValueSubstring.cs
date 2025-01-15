using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001CCA RID: 7370
	[DebuggerDisplay("{Start}-{End}: \"{Value}\"")]
	public class ValueSubstring : LearningCacheSubstring, IEquatable<ValueSubstring>, ITypedValue, ICachefulObject<ValueSubstring>, ICachefulObject
	{
		// Token: 0x170029DC RID: 10716
		// (get) Token: 0x0600F9F8 RID: 63992 RVA: 0x00352987 File Offset: 0x00350B87
		public IType Type { get; }

		// Token: 0x0600F9F9 RID: 63993 RVA: 0x0035298F File Offset: 0x00350B8F
		[JsonConstructor]
		private ValueSubstring(string s, uint start, uint end, IType type, StringLearningCache cache = null, ConcurrentLruCache<CustomExtractor, ConcurrentLruCache<string, Dictionary<Record<uint?, uint?>, Record<int, int>>>> externalExtractorCache = null)
			: base(s, start, end, cache ?? new StringLearningCache(s, Semantics.Tokens))
		{
			this.Type = type;
			this._externalExtractorCache = externalExtractorCache ?? new ConcurrentLruCache<CustomExtractor, ConcurrentLruCache<string, Dictionary<Record<uint?, uint?>, Record<int, int>>>>(32, IdentityEquality.Comparer, null, null);
		}

		// Token: 0x0600F9FA RID: 63994 RVA: 0x003529CD File Offset: 0x00350BCD
		private ValueSubstring(ValueSubstring other)
			: base(other)
		{
			this.Type = other.Type;
			this._externalExtractorCache = other._externalExtractorCache.DeepClone() as ConcurrentLruCache<CustomExtractor, ConcurrentLruCache<string, Dictionary<Record<uint?, uint?>, Record<int, int>>>>;
			this._hashCode = other._hashCode;
		}

		// Token: 0x0600F9FB RID: 63995 RVA: 0x00352A04 File Offset: 0x00350C04
		public ValueSubstring WithType(IType type)
		{
			if (type != this.Type)
			{
				return new ValueSubstring(base.Source, base.Start, base.End, type, base.Cache, null);
			}
			return this;
		}

		// Token: 0x0600F9FC RID: 63996 RVA: 0x00352A30 File Offset: 0x00350C30
		public bool Equals(ValueSubstring obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			uint length = base.Length;
			if (obj.Length != length)
			{
				return false;
			}
			string source = obj.Source;
			string source2 = base.Source;
			if (source == source2 && obj.Start == base.Start)
			{
				return true;
			}
			if (base.IsValueAlreadyComputed && obj.IsValueAlreadyComputed)
			{
				return base.Value.Equals(obj.Value);
			}
			if (base.IsValueAlreadyComputed)
			{
				return obj.Source.SubstringEquals(base.Value, (int)obj.Start);
			}
			if (obj.IsValueAlreadyComputed)
			{
				return base.Source.SubstringEquals(obj.Value, (int)base.Start);
			}
			int start = (int)base.Start;
			int start2 = (int)obj.Start;
			int num = 0;
			while ((long)num < (long)((ulong)length))
			{
				if (source2[start++] != source[start2++])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x0600F9FD RID: 63997 RVA: 0x00352B1C File Offset: 0x00350D1C
		public override int GetHashCode()
		{
			int? hashCode = this._hashCode;
			if (hashCode == null)
			{
				int? num = (this._hashCode = new int?(Substring.ValueEquality.GetHashCode(this)));
				return num.Value;
			}
			return hashCode.GetValueOrDefault();
		}

		// Token: 0x0600F9FE RID: 63998 RVA: 0x00352B62 File Offset: 0x00350D62
		ICachefulObject ICachefulObject.CloneWithCurrentCacheState()
		{
			return this.CloneWithCurrentCacheState();
		}

		// Token: 0x0600F9FF RID: 63999 RVA: 0x0007B50E File Offset: 0x0007970E
		public void ClearCaches()
		{
			base.Cache.ClearCaches();
		}

		// Token: 0x0600FA00 RID: 64000 RVA: 0x00352B6A File Offset: 0x00350D6A
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(base.GetType() != obj.GetType()) && this.Equals((ValueSubstring)obj)));
		}

		// Token: 0x0600FA01 RID: 64001 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(ValueSubstring left, ValueSubstring right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600FA02 RID: 64002 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(ValueSubstring left, ValueSubstring right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x0600FA03 RID: 64003 RVA: 0x00352B98 File Offset: 0x00350D98
		public static ValueSubstring Create(string s, uint? start = null, uint? end = null, IType type = null, IReadOnlyDictionary<string, Token> allowedTokens = null)
		{
			if (s == null)
			{
				return null;
			}
			StringLearningCache stringLearningCache = new StringLearningCache(s, allowedTokens ?? Semantics.Tokens);
			return new ValueSubstring(s, start.GetValueOrDefault(), end ?? ((uint)s.Length), type, stringLearningCache, null);
		}

		// Token: 0x0600FA04 RID: 64004 RVA: 0x00352BE8 File Offset: 0x00350DE8
		public ValueSubstring Slice(uint start, uint? end = null)
		{
			uint num = end ?? base.Info.End;
			if (start < base.Start)
			{
				throw new ArgumentOutOfRangeException("start", FormattableString.Invariant(FormattableStringFactory.Create("Starting slicing position {0} is less than the region start {1}", new object[] { start, base.Start })));
			}
			if (num > base.Info.End)
			{
				throw new ArgumentOutOfRangeException("end", FormattableString.Invariant(FormattableStringFactory.Create("Ending slicing position {0} exceeds the region end {1}", new object[] { end, base.End })));
			}
			return new ValueSubstring(base.Source, start, num, null, base.Cache, this._externalExtractorCache);
		}

		// Token: 0x0600FA05 RID: 64005 RVA: 0x00352CBC File Offset: 0x00350EBC
		public ValueSubstring SliceRelative(uint start, uint? end = null)
		{
			return this.Slice(base.Start + start, base.Start + end);
		}

		// Token: 0x0600FA06 RID: 64006 RVA: 0x00352D04 File Offset: 0x00350F04
		public ValueSubstring Concat(ValueSubstring other)
		{
			if (base.Source == other.Source && base.Info.End == other.Start)
			{
				return ValueSubstring.Create(base.Source, new uint?(base.Start), new uint?(other.Info.End), null, null);
			}
			return ValueSubstring.Create(base.Value + other.Value, null, null, null, null);
		}

		// Token: 0x0600FA07 RID: 64007 RVA: 0x00352D8B File Offset: 0x00350F8B
		public ValueSubstring CloneWithCurrentCacheState()
		{
			return new ValueSubstring(this);
		}

		// Token: 0x0600FA08 RID: 64008 RVA: 0x0001B9A2 File Offset: 0x00019BA2
		public override string ToString()
		{
			return base.Value;
		}

		// Token: 0x0600FA09 RID: 64009 RVA: 0x00352D93 File Offset: 0x00350F93
		public Optional<T> GetTypedValue<T>()
		{
			IType<T> type = this.Type as IType<T>;
			if (type == null)
			{
				return Optional<T>.Nothing;
			}
			return type.GetTypedValue(this);
		}

		// Token: 0x0600FA0A RID: 64010 RVA: 0x00352DB0 File Offset: 0x00350FB0
		public IEnumerable<ValueSubstring> GetAllSubstringsStartingAt(uint startPos, uint minimumLength)
		{
			if (startPos < base.Start)
			{
				yield break;
			}
			uint len = minimumLength;
			while (startPos + len <= base.End)
			{
				yield return this.Slice(startPos, new uint?(startPos + len));
				uint num = len + 1U;
				len = num;
			}
			yield break;
		}

		// Token: 0x0600FA0B RID: 64011 RVA: 0x00352DCE File Offset: 0x00350FCE
		public IEnumerable<ValueSubstring> GetAllSubstringsStartingAtRelative(uint startPos, uint minimumLength)
		{
			return this.GetAllSubstringsStartingAt(base.Start + startPos, minimumLength);
		}

		// Token: 0x0600FA0C RID: 64012 RVA: 0x00352DE0 File Offset: 0x00350FE0
		private Record<int, int>? UpdateExternalExtractorSubstringCache(ConcurrentLruCache<string, Dictionary<Record<uint?, uint?>, Record<int, int>>> substringCache, CustomExtractor extractor, Record<uint?, uint?> pp)
		{
			IReadOnlyList<Record<uint, uint>> readOnlyList = extractor.Extract(base.Source);
			int numMatches = readOnlyList.Count;
			Dictionary<Record<uint?, uint?>, Record<int, int>> d = new Dictionary<Record<uint?, uint?>, Record<int, int>>();
			readOnlyList.ForEach(delegate(Record<uint, uint> s, int i)
			{
				d[new Record<uint?, uint?>(new uint?(s.Item1), new uint?(s.Item2))] = new Record<int, int>(i, i - numMatches);
			});
			substringCache.Add(base.Source, d);
			Record<int, int> record;
			if (!d.TryGetValue(pp, out record))
			{
				return null;
			}
			return new Record<int, int>?(record);
		}

		// Token: 0x0600FA0D RID: 64013 RVA: 0x00352E5C File Offset: 0x0035105C
		private Record<int, int>? UpdateExternalExtractorCache(CustomExtractor extractor, Record<uint?, uint?> pp)
		{
			ConcurrentLruCache<string, Dictionary<Record<uint?, uint?>, Record<int, int>>> concurrentLruCache = new ConcurrentLruCache<string, Dictionary<Record<uint?, uint?>, Record<int, int>>>(32, EqualityComparer<string>.Default, null, null);
			this._externalExtractorCache.Add(extractor, concurrentLruCache);
			return this.UpdateExternalExtractorSubstringCache(concurrentLruCache, extractor, pp);
		}

		// Token: 0x0600FA0E RID: 64014 RVA: 0x00352E90 File Offset: 0x00351090
		internal Record<int, int>? CachedMatchIndicesAt(CustomExtractor extractor, Record<uint?, uint?> pp)
		{
			ConcurrentLruCache<string, Dictionary<Record<uint?, uint?>, Record<int, int>>> concurrentLruCache;
			if (!this._externalExtractorCache.Lookup(extractor, out concurrentLruCache) || concurrentLruCache == null)
			{
				return this.UpdateExternalExtractorCache(extractor, pp);
			}
			Dictionary<Record<uint?, uint?>, Record<int, int>> dictionary;
			if (!concurrentLruCache.Lookup(base.Source, out dictionary))
			{
				return this.UpdateExternalExtractorSubstringCache(concurrentLruCache, extractor, pp);
			}
			Record<int, int> record;
			if (!dictionary.TryGetValue(pp, out record))
			{
				return null;
			}
			return new Record<int, int>?(record);
		}

		// Token: 0x0600FA0F RID: 64015 RVA: 0x00352EF0 File Offset: 0x003510F0
		public XElement SerializeToXML(Dictionary<object, int> identityCache)
		{
			return new XElement("ValueSubstring", base.Source).WithAttribute("Start", base.Start).WithAttribute("End", base.End);
		}

		// Token: 0x0600FA10 RID: 64016 RVA: 0x00352F3C File Offset: 0x0035113C
		public static ValueSubstring DeserializeFromXML(XElement node, Dictionary<int, object> identityCache)
		{
			if (node.Name != "ValueSubstring")
			{
				throw new InvalidOperationException();
			}
			string value = node.Value;
			XAttribute xattribute = node.Attribute("Start");
			string text = ((xattribute != null) ? xattribute.Value : null);
			if (text == null)
			{
				throw new InvalidOperationException();
			}
			uint num = uint.Parse(text);
			XAttribute xattribute2 = node.Attribute("End");
			string text2 = ((xattribute2 != null) ? xattribute2.Value : null);
			if (text2 == null)
			{
				throw new InvalidOperationException();
			}
			uint num2 = uint.Parse(text2);
			return ValueSubstring.Create(value, new uint?(num), new uint?(num2), null, null);
		}

		// Token: 0x04005C96 RID: 23702
		private int? _hashCode;

		// Token: 0x04005C98 RID: 23704
		private const int MaxExtractorsInCache = 32;

		// Token: 0x04005C99 RID: 23705
		private const int MaxStringsPerExtractorInCache = 32;

		// Token: 0x04005C9A RID: 23706
		private readonly ConcurrentLruCache<CustomExtractor, ConcurrentLruCache<string, Dictionary<Record<uint?, uint?>, Record<int, int>>>> _externalExtractorCache;
	}
}
