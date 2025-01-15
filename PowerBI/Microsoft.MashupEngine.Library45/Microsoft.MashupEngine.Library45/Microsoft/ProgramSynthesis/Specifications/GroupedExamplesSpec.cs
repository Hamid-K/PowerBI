using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x0200034E RID: 846
	public class GroupedExamplesSpec : ExampleSpec
	{
		// Token: 0x060012AE RID: 4782 RVA: 0x00036C34 File Offset: 0x00034E34
		public GroupedExamplesSpec(IReadOnlyDictionary<State, KeyValuePair<object, uint>> examplesWithCounts)
			: base(examplesWithCounts.ToDictionary((KeyValuePair<State, KeyValuePair<object, uint>> pair) => pair.Key, (KeyValuePair<State, KeyValuePair<object, uint>> pair) => pair.Value.Key))
		{
			this.ExamplesWithCounts = examplesWithCounts;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00036C92 File Offset: 0x00034E92
		public GroupedExamplesSpec(IEnumerable<KeyValuePair<State, KeyValuePair<object, uint>>> examplesWithCounts)
			: this(examplesWithCounts.ToDictionary<State, KeyValuePair<object, uint>>())
		{
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00036CA0 File Offset: 0x00034EA0
		public IReadOnlyDictionary<State, KeyValuePair<object, uint>> ExamplesWithCounts { get; }

		// Token: 0x060012B1 RID: 4785 RVA: 0x00036CA8 File Offset: 0x00034EA8
		public static GroupedExamplesSpec FromExamples(IEnumerable<KeyValuePair<State, object>> examples, IEqualityComparer<KeyValuePair<State, object>> equalityComparer = null)
		{
			equalityComparer = equalityComparer ?? ValueEquality<KeyValuePair<State, object>>.Instance;
			return new GroupedExamplesSpec(examples.GroupBy((KeyValuePair<State, object> e) => e, equalityComparer).ToList<IGrouping<KeyValuePair<State, object>, KeyValuePair<State, object>>>().ToDictionary((IGrouping<KeyValuePair<State, object>, KeyValuePair<State, object>> pairs) => pairs.Key.Key, (IGrouping<KeyValuePair<State, object>, KeyValuePair<State, object>> pairs) => new KeyValuePair<object, uint>(pairs.Key.Value, Convert.ToUInt32(pairs.Count<KeyValuePair<State, object>>())), ValueEquality<State>.Instance));
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00036D3C File Offset: 0x00034F3C
		protected override bool EqualsOnInput(State state, Spec other)
		{
			if (base.EqualsOnInput(state, other))
			{
				GroupedExamplesSpec groupedExamplesSpec = other as GroupedExamplesSpec;
				return groupedExamplesSpec == null || groupedExamplesSpec.ExamplesWithCounts[state].Equals(this.ExamplesWithCounts[state]);
			}
			return false;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00036D8C File Offset: 0x00034F8C
		public override string ToString()
		{
			return this.ExamplesWithCounts.Select((KeyValuePair<State, KeyValuePair<object, uint>> kvp) => FormattableString.Invariant(FormattableStringFactory.Create("{0} ({1}) -> {2}", new object[]
			{
				kvp.Key,
				kvp.Value.Value,
				kvp.Value.Key.ToLiteral(null)
			}))).DumpCollection(ObjectFormatting.ToString, "[", "]", ", ", null);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00036DDC File Offset: 0x00034FDC
		protected internal override Spec NullToBottom()
		{
			if (!base.DisjunctiveExamples.Values.Any((IEnumerable<object> d) => d.Contains(null)))
			{
				return this;
			}
			return new GroupedExamplesSpec(this.ExamplesWithCounts.ToDictionary((KeyValuePair<State, KeyValuePair<object, uint>> kvp) => kvp.Key, (KeyValuePair<State, KeyValuePair<object, uint>> kvp) => new KeyValuePair<object, uint>(kvp.Value.Key.NullToBottom(), kvp.Value.Value)));
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00036E6C File Offset: 0x0003506C
		protected internal override Spec BottomToNull()
		{
			if (!base.DisjunctiveExamples.Values.Any((IEnumerable<object> d) => d.Contains(Bottom.Value)))
			{
				return this;
			}
			return new GroupedExamplesSpec(this.ExamplesWithCounts.ToDictionary((KeyValuePair<State, KeyValuePair<object, uint>> kvp) => kvp.Key, (KeyValuePair<State, KeyValuePair<object, uint>> kvp) => new KeyValuePair<object, uint>(kvp.Value.Key.BottomToNull(), kvp.Value.Value)));
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00036EFC File Offset: 0x000350FC
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new GroupedExamplesSpec(this.ExamplesWithCounts.ToDictionary((KeyValuePair<State, KeyValuePair<object, uint>> kvp) => transformer(kvp.Key), (KeyValuePair<State, KeyValuePair<object, uint>> kvp) => kvp.Value));
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00036F54 File Offset: 0x00035154
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			return new XElement("GroupedExamplesSpec", this.ExamplesWithCounts.Select((KeyValuePair<State, KeyValuePair<object, uint>> kvp) => new XElement("Example", new object[]
			{
				kvp.Key.SerializeToXML(identityCache, context),
				context.SerializeObject(kvp.Value.Key, identityCache),
				kvp.Value.Value
			})));
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00036F9C File Offset: 0x0003519C
		internal new static GroupedExamplesSpec DeserializeFromXML(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext context)
		{
			if (node.Name != "GroupedExamplesSpec")
			{
				throw new InvalidOperationException();
			}
			return new GroupedExamplesSpec(node.Elements().Select(delegate(XElement ex)
			{
				List<XElement> list = ex.Elements().ToList<XElement>();
				State state = State.DeserializeFromXML(list[0], context, identityCache);
				object obj = context.DeserializeObject(list[1], identityCache);
				uint num = uint.Parse(list[2].Value);
				return new KeyValuePair<State, KeyValuePair<object, uint>>(state, new KeyValuePair<object, uint>(obj, num));
			}));
		}
	}
}
