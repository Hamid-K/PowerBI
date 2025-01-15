using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Specifications.Serialization;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Learning
{
	// Token: 0x020010D7 RID: 4311
	public class TextSubsequenceSpec : SubsequenceSpec
	{
		// Token: 0x0600816B RID: 33131 RVA: 0x001B162F File Offset: 0x001AF82F
		public TextSubsequenceSpec(IEnumerable<KeyValuePair<State, IEnumerable<object>>> examples, Dictionary<State, int[]> nodeIndexes, Dictionary<State, bool[]> softConstraints)
			: base(examples)
		{
			this.NodeIndexes = nodeIndexes;
			this.SoftConstraints = softConstraints;
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x0600816C RID: 33132 RVA: 0x001B1646 File Offset: 0x001AF846
		public IReadOnlyDictionary<State, int[]> NodeIndexes { get; }

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x0600816D RID: 33133 RVA: 0x001B164E File Offset: 0x001AF84E
		public IReadOnlyDictionary<State, bool[]> SoftConstraints { get; }

		// Token: 0x0600816E RID: 33134 RVA: 0x001B1656 File Offset: 0x001AF856
		public bool IsSoft()
		{
			return this.SoftConstraints.Any((KeyValuePair<State, bool[]> kvp) => kvp.Value.Any((bool c) => c));
		}

		// Token: 0x0600816F RID: 33135 RVA: 0x001B1684 File Offset: 0x001AF884
		public TextSubsequenceSpec GetHardSpec()
		{
			IEnumerable<KeyValuePair<State, IEnumerable<object>>> enumerable = base.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.Zip(this.SoftConstraints[kvp.Key], delegate(object s, bool c)
			{
				if (!c)
				{
					return s as string;
				}
				return string.Empty;
			}).ToArray<string>());
			Dictionary<State, int[]> dictionary = this.NodeIndexes.ToDictionary((KeyValuePair<State, int[]> kvp) => kvp.Key, (KeyValuePair<State, int[]> kvp) => kvp.Value.Zip(this.SoftConstraints[kvp.Key], delegate(int n, bool c)
			{
				if (!c)
				{
					return n;
				}
				return -1;
			}).ToArray<int>());
			Dictionary<State, bool[]> dictionary2 = this.SoftConstraints.ToDictionary((KeyValuePair<State, bool[]> kvp) => kvp.Key, (KeyValuePair<State, bool[]> kvp) => kvp.Value.Select((bool t) => false).ToArray<bool>());
			return new TextSubsequenceSpec(enumerable, dictionary, dictionary2);
		}

		// Token: 0x06008170 RID: 33136 RVA: 0x000373A6 File Offset: 0x000355A6
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			base.ThrowSerializationUnsupportedException();
			return null;
		}
	}
}
