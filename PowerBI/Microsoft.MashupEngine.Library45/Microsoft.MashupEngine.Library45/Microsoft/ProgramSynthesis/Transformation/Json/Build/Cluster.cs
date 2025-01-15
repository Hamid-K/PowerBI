using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build
{
	// Token: 0x02001A18 RID: 6680
	public static class Cluster
	{
		// Token: 0x0600DB2D RID: 56109 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x0600DB2E RID: 56110 RVA: 0x002ECAEF File Offset: 0x002EACEF
		public static IEnumerable<KeyValuePair<Optional<JPath>, ProgramSetBuilder<path>>> ClusterOnInput(this ProgramSetBuilder<path> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<JPath>, ProgramSetBuilder<path>>(Cluster.CastValue<JPath>(kvp.Key), ProgramSetBuilder<path>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB2F RID: 56111 RVA: 0x002ECB21 File Offset: 0x002EAD21
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<str>>> ClusterOnInput(this ProgramSetBuilder<str> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<str>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<str>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB30 RID: 56112 RVA: 0x002ECB53 File Offset: 0x002EAD53
		public static IEnumerable<KeyValuePair<Optional<JTokenType>, ProgramSetBuilder<t>>> ClusterOnInput(this ProgramSetBuilder<t> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<JTokenType>, ProgramSetBuilder<t>>(Cluster.CastValue<JTokenType>(kvp.Key), ProgramSetBuilder<t>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB31 RID: 56113 RVA: 0x002ECB85 File Offset: 0x002EAD85
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>, ProgramSetBuilder<output>>> ClusterOnInput(this ProgramSetBuilder<output> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>, ProgramSetBuilder<output>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>(kvp.Key), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB32 RID: 56114 RVA: 0x002ECBB7 File Offset: 0x002EADB7
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>, ProgramSetBuilder<value>>> ClusterOnInput(this ProgramSetBuilder<value> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>, ProgramSetBuilder<value>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>(kvp.Key), ProgramSetBuilder<value>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB33 RID: 56115 RVA: 0x002ECBE9 File Offset: 0x002EADE9
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>, ProgramSetBuilder<@object>>> ClusterOnInput(this ProgramSetBuilder<@object> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>, ProgramSetBuilder<@object>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>(kvp.Key), ProgramSetBuilder<@object>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB34 RID: 56116 RVA: 0x002ECC1B File Offset: 0x002EAE1B
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>, ProgramSetBuilder<array>>> ClusterOnInput(this ProgramSetBuilder<array> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>, ProgramSetBuilder<array>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>(kvp.Key), ProgramSetBuilder<array>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB35 RID: 56117 RVA: 0x002ECC4D File Offset: 0x002EAE4D
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>, ProgramSetBuilder<property>>> ClusterOnInput(this ProgramSetBuilder<property> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>, ProgramSetBuilder<property>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>(kvp.Key), ProgramSetBuilder<property>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB36 RID: 56118 RVA: 0x002ECC7F File Offset: 0x002EAE7F
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>, ProgramSetBuilder<elements>>> ClusterOnInput(this ProgramSetBuilder<elements> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>, ProgramSetBuilder<elements>>(Cluster.CastValue<IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>(kvp.Key), ProgramSetBuilder<elements>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB37 RID: 56119 RVA: 0x002ECCB1 File Offset: 0x002EAEB1
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<key>>> ClusterOnInput(this ProgramSetBuilder<key> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<key>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<key>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB38 RID: 56120 RVA: 0x002ECCE3 File Offset: 0x002EAEE3
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<selectKey>>> ClusterOnInput(this ProgramSetBuilder<selectKey> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<selectKey>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<selectKey>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB39 RID: 56121 RVA: 0x002ECD15 File Offset: 0x002EAF15
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>, ProgramSetBuilder<selectOrTransformValue>>> ClusterOnInput(this ProgramSetBuilder<selectOrTransformValue> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>, ProgramSetBuilder<selectOrTransformValue>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>(kvp.Key), ProgramSetBuilder<selectOrTransformValue>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB3A RID: 56122 RVA: 0x002ECD47 File Offset: 0x002EAF47
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>, ProgramSetBuilder<selectValue>>> ClusterOnInput(this ProgramSetBuilder<selectValue> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>, ProgramSetBuilder<selectValue>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>(kvp.Key), ProgramSetBuilder<selectValue>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB3B RID: 56123 RVA: 0x002ECD79 File Offset: 0x002EAF79
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>, ProgramSetBuilder<transformValue>>> ClusterOnInput(this ProgramSetBuilder<transformValue> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>, ProgramSetBuilder<transformValue>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>(kvp.Key), ProgramSetBuilder<transformValue>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB3C RID: 56124 RVA: 0x002ECDAB File Offset: 0x002EAFAB
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<transformLet>>> ClusterOnInput(this ProgramSetBuilder<transformLet> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<transformLet>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<transformLet>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB3D RID: 56125 RVA: 0x002ECDDD File Offset: 0x002EAFDD
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<transformString>>> ClusterOnInput(this ProgramSetBuilder<transformString> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<transformString>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<transformString>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB3E RID: 56126 RVA: 0x002ECE0F File Offset: 0x002EB00F
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>, ProgramSetBuilder<selectArray>>> ClusterOnInput(this ProgramSetBuilder<selectArray> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>, ProgramSetBuilder<selectArray>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>(kvp.Key), ProgramSetBuilder<selectArray>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB3F RID: 56127 RVA: 0x002ECE41 File Offset: 0x002EB041
		public static IEnumerable<KeyValuePair<Optional<IRow>, ProgramSetBuilder<_LetB0>>> ClusterOnInput(this ProgramSetBuilder<_LetB0> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IRow>, ProgramSetBuilder<_LetB0>>(Cluster.CastValue<IRow>(kvp.Key), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600DB40 RID: 56128 RVA: 0x002ECE73 File Offset: 0x002EB073
		public static IEnumerable<KeyValuePair<Optional<JPath>[], ProgramSetBuilder<path>>> ClusterOnInputTuple(this ProgramSetBuilder<path> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<JPath>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<JPath>>(Cluster.CastValue<JPath>));
				}
				return new KeyValuePair<Optional<JPath>[], ProgramSetBuilder<path>>(key.Select(func).ToArray<Optional<JPath>>(), ProgramSetBuilder<path>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB41 RID: 56129 RVA: 0x002ECEA5 File Offset: 0x002EB0A5
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<str>>> ClusterOnInputTuple(this ProgramSetBuilder<str> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<str>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<str>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB42 RID: 56130 RVA: 0x002ECED7 File Offset: 0x002EB0D7
		public static IEnumerable<KeyValuePair<Optional<JTokenType>[], ProgramSetBuilder<t>>> ClusterOnInputTuple(this ProgramSetBuilder<t> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<JTokenType>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<JTokenType>>(Cluster.CastValue<JTokenType>));
				}
				return new KeyValuePair<Optional<JTokenType>[], ProgramSetBuilder<t>>(key.Select(func).ToArray<Optional<JTokenType>>(), ProgramSetBuilder<t>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB43 RID: 56131 RVA: 0x002ECF09 File Offset: 0x002EB109
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>[], ProgramSetBuilder<output>>> ClusterOnInputTuple(this ProgramSetBuilder<output> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>));
				}
				return new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>[], ProgramSetBuilder<output>>(key.Select(func).ToArray<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>(), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB44 RID: 56132 RVA: 0x002ECF3B File Offset: 0x002EB13B
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>[], ProgramSetBuilder<value>>> ClusterOnInputTuple(this ProgramSetBuilder<value> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>));
				}
				return new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>[], ProgramSetBuilder<value>>(key.Select(func).ToArray<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>(), ProgramSetBuilder<value>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB45 RID: 56133 RVA: 0x002ECF6D File Offset: 0x002EB16D
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>[], ProgramSetBuilder<@object>>> ClusterOnInputTuple(this ProgramSetBuilder<@object> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>));
				}
				return new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>[], ProgramSetBuilder<@object>>(key.Select(func).ToArray<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>>(), ProgramSetBuilder<@object>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB46 RID: 56134 RVA: 0x002ECF9F File Offset: 0x002EB19F
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>[], ProgramSetBuilder<array>>> ClusterOnInputTuple(this ProgramSetBuilder<array> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>));
				}
				return new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>[], ProgramSetBuilder<array>>(key.Select(func).ToArray<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>>(), ProgramSetBuilder<array>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB47 RID: 56135 RVA: 0x002ECFD1 File Offset: 0x002EB1D1
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>[], ProgramSetBuilder<property>>> ClusterOnInputTuple(this ProgramSetBuilder<property> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>));
				}
				return new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>[], ProgramSetBuilder<property>>(key.Select(func).ToArray<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>>(), ProgramSetBuilder<property>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB48 RID: 56136 RVA: 0x002ED003 File Offset: 0x002EB203
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>[], ProgramSetBuilder<elements>>> ClusterOnInputTuple(this ProgramSetBuilder<elements> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>>(Cluster.CastValue<IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>));
				}
				return new KeyValuePair<Optional<IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>[], ProgramSetBuilder<elements>>(key.Select(func).ToArray<Optional<IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>>(), ProgramSetBuilder<elements>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB49 RID: 56137 RVA: 0x002ED035 File Offset: 0x002EB235
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<key>>> ClusterOnInputTuple(this ProgramSetBuilder<key> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<key>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<key>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB4A RID: 56138 RVA: 0x002ED067 File Offset: 0x002EB267
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<selectKey>>> ClusterOnInputTuple(this ProgramSetBuilder<selectKey> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<selectKey>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<selectKey>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB4B RID: 56139 RVA: 0x002ED099 File Offset: 0x002EB299
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>[], ProgramSetBuilder<selectOrTransformValue>>> ClusterOnInputTuple(this ProgramSetBuilder<selectOrTransformValue> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>));
				}
				return new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>[], ProgramSetBuilder<selectOrTransformValue>>(key.Select(func).ToArray<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>(), ProgramSetBuilder<selectOrTransformValue>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB4C RID: 56140 RVA: 0x002ED0CB File Offset: 0x002EB2CB
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>[], ProgramSetBuilder<selectValue>>> ClusterOnInputTuple(this ProgramSetBuilder<selectValue> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>));
				}
				return new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>[], ProgramSetBuilder<selectValue>>(key.Select(func).ToArray<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>(), ProgramSetBuilder<selectValue>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB4D RID: 56141 RVA: 0x002ED0FD File Offset: 0x002EB2FD
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>[], ProgramSetBuilder<transformValue>>> ClusterOnInputTuple(this ProgramSetBuilder<transformValue> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>));
				}
				return new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>[], ProgramSetBuilder<transformValue>>(key.Select(func).ToArray<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>(), ProgramSetBuilder<transformValue>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB4E RID: 56142 RVA: 0x002ED12F File Offset: 0x002EB32F
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<transformLet>>> ClusterOnInputTuple(this ProgramSetBuilder<transformLet> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<transformLet>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<transformLet>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB4F RID: 56143 RVA: 0x002ED161 File Offset: 0x002EB361
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<transformString>>> ClusterOnInputTuple(this ProgramSetBuilder<transformString> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<transformString>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<transformString>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB50 RID: 56144 RVA: 0x002ED193 File Offset: 0x002EB393
		public static IEnumerable<KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>[], ProgramSetBuilder<selectArray>>> ClusterOnInputTuple(this ProgramSetBuilder<selectArray> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>>(Cluster.CastValue<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>));
				}
				return new KeyValuePair<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>[], ProgramSetBuilder<selectArray>>(key.Select(func).ToArray<Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>>(), ProgramSetBuilder<selectArray>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600DB51 RID: 56145 RVA: 0x002ED1C5 File Offset: 0x002EB3C5
		public static IEnumerable<KeyValuePair<Optional<IRow>[], ProgramSetBuilder<_LetB0>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB0> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IRow>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<IRow>>(Cluster.CastValue<IRow>));
				}
				return new KeyValuePair<Optional<IRow>[], ProgramSetBuilder<_LetB0>>(key.Select(func).ToArray<Optional<IRow>>(), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02001A19 RID: 6681
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040053DD RID: 21469
			public static Func<object, Optional<JPath>> <0>__CastValue;

			// Token: 0x040053DE RID: 21470
			public static Func<object, Optional<string>> <1>__CastValue;

			// Token: 0x040053DF RID: 21471
			public static Func<object, Optional<JTokenType>> <2>__CastValue;

			// Token: 0x040053E0 RID: 21472
			public static Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>> <3>__CastValue;

			// Token: 0x040053E1 RID: 21473
			public static Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>> <4>__CastValue;

			// Token: 0x040053E2 RID: 21474
			public static Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>> <5>__CastValue;

			// Token: 0x040053E3 RID: 21475
			public static Func<object, Optional<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>> <6>__CastValue;

			// Token: 0x040053E4 RID: 21476
			public static Func<object, Optional<IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>>> <7>__CastValue;

			// Token: 0x040053E5 RID: 21477
			public static Func<object, Optional<ValueSubstring>> <8>__CastValue;

			// Token: 0x040053E6 RID: 21478
			public static Func<object, Optional<IRow>> <9>__CastValue;
		}
	}
}
