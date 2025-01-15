using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Learning
{
	// Token: 0x020010D9 RID: 4313
	public class TextTableSpec : Spec
	{
		// Token: 0x0600817E RID: 33150 RVA: 0x001B1885 File Offset: 0x001AFA85
		public TextTableSpec(IEnumerable<TextSubsequenceSpec> columnSpecs)
			: base(columnSpecs.SelectMany((TextSubsequenceSpec c) => c.ProvidedInputs).Distinct<State>(), true)
		{
			this.ColumnSpecs = columnSpecs.ToArray<TextSubsequenceSpec>();
		}

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x0600817F RID: 33151 RVA: 0x001B18C4 File Offset: 0x001AFAC4
		public TextSubsequenceSpec[] ColumnSpecs { get; }

		// Token: 0x06008180 RID: 33152 RVA: 0x001B18CC File Offset: 0x001AFACC
		public TextTableSpec GetHardSpec(bool[] softCols = null)
		{
			if (softCols != null)
			{
				return new TextTableSpec(softCols.Select(delegate(bool b, int i)
				{
					if (!b)
					{
						return this.ColumnSpecs[i].GetHardSpec();
					}
					return this.ColumnSpecs[i];
				}).ToArray<TextSubsequenceSpec>());
			}
			return new TextTableSpec(this.ColumnSpecs.Select((TextSubsequenceSpec s) => s.GetHardSpec()).ToArray<TextSubsequenceSpec>());
		}

		// Token: 0x06008181 RID: 33153 RVA: 0x001B192D File Offset: 0x001AFB2D
		public bool IsSoft()
		{
			return this.ColumnSpecs.Any((TextSubsequenceSpec s) => s.IsSoft());
		}

		// Token: 0x06008182 RID: 33154 RVA: 0x000170F6 File Offset: 0x000152F6
		protected override bool CorrectOnProvided(State state, object output)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008183 RID: 33155 RVA: 0x001B195C File Offset: 0x001AFB5C
		protected override bool EqualsOnInput(State state, Spec other)
		{
			TextTableSpec textTableSpec = other as TextTableSpec;
			if (textTableSpec == null)
			{
				return false;
			}
			for (int i = 0; i < this.ColumnSpecs.Length; i++)
			{
				TextSubsequenceSpec textSubsequenceSpec = this.ColumnSpecs[i];
				TextSubsequenceSpec textSubsequenceSpec2 = textTableSpec.ColumnSpecs[i];
				if (!textSubsequenceSpec.PositiveExamples[state].SequenceEqual(textSubsequenceSpec2.PositiveExamples[state]) || !textSubsequenceSpec.NegativeExamples[state].SetEquals(textSubsequenceSpec2.NegativeExamples[state]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008184 RID: 33156 RVA: 0x0003995D File Offset: 0x00037B5D
		protected override int GetHashCodeOnInput(State state)
		{
			return 397;
		}

		// Token: 0x06008185 RID: 33157 RVA: 0x00002188 File Offset: 0x00000388
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return null;
		}

		// Token: 0x06008186 RID: 33158 RVA: 0x001B19E4 File Offset: 0x001AFBE4
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			List<TextSubsequenceSpec> list = new List<TextSubsequenceSpec>();
			Func<KeyValuePair<State, IEnumerable<object>>, State> <>9__0;
			for (int i = 0; i < this.ColumnSpecs.Length; i++)
			{
				IEnumerable<KeyValuePair<State, IEnumerable<object>>> positiveExamples = this.ColumnSpecs[i].PositiveExamples;
				Func<KeyValuePair<State, IEnumerable<object>>, State> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (KeyValuePair<State, IEnumerable<object>> kvp) => transformer(kvp.Key));
				}
				TextSubsequenceSpec textSubsequenceSpec = new TextSubsequenceSpec(positiveExamples.ToDictionary(func, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value), this.ColumnSpecs[i].NodeIndexes.ToDictionary<State, int[]>(), this.ColumnSpecs[i].SoftConstraints.ToDictionary<State, bool[]>());
				list.Add(textSubsequenceSpec);
			}
			return new TextTableSpec(list);
		}

		// Token: 0x06008187 RID: 33159 RVA: 0x000373A6 File Offset: 0x000355A6
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			base.ThrowSerializationUnsupportedException();
			return null;
		}
	}
}
