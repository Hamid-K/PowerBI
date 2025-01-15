using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Learning
{
	// Token: 0x020010AB RID: 4267
	public class MultiPageTextTableSpec : Spec
	{
		// Token: 0x06008078 RID: 32888 RVA: 0x001ADFF0 File Offset: 0x001AC1F0
		public MultiPageTextTableSpec(List<Tuple<State, TextTableSpec>> tableSpecs)
			: base(tableSpecs.Select((Tuple<State, TextTableSpec> t) => t.Item1).ToArray<State>(), true)
		{
			this.TableSpecs = tableSpecs;
			IEnumerable<int> enumerable = (from t in tableSpecs.Where(delegate(Tuple<State, TextTableSpec> t)
				{
					TextTableSpec item = t.Item2;
					return ((item != null) ? item.ColumnSpecs : null) != null;
				})
				select t.Item2.ColumnSpecs.Count<TextSubsequenceSpec>()).Distinct<int>();
			if (enumerable.Skip(1).Any<int>() || !enumerable.Any<int>())
			{
				throw new Exception("Multipage table spec requires same number of columns on all pages");
			}
			this.NumColumns = enumerable.First<int>();
		}

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x06008079 RID: 32889 RVA: 0x001AE0B1 File Offset: 0x001AC2B1
		public List<Tuple<State, TextTableSpec>> TableSpecs { get; }

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x0600807A RID: 32890 RVA: 0x001AE0B9 File Offset: 0x001AC2B9
		public int NumColumns { get; }

		// Token: 0x0600807B RID: 32891 RVA: 0x000170F6 File Offset: 0x000152F6
		protected override bool CorrectOnProvided(State state, object output)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600807C RID: 32892 RVA: 0x001AE0C4 File Offset: 0x001AC2C4
		protected override bool EqualsOnInput(State state, Spec other)
		{
			MultiPageTextTableSpec multiPageTextTableSpec = other as MultiPageTextTableSpec;
			if (multiPageTextTableSpec == null)
			{
				return false;
			}
			Tuple<State, TextTableSpec> tuple = multiPageTextTableSpec.TableSpecs.FirstOrDefault((Tuple<State, TextTableSpec> t) => t.Item1 == state);
			Tuple<State, TextTableSpec> tuple2 = this.TableSpecs.FirstOrDefault((Tuple<State, TextTableSpec> t) => t.Item1 == state);
			if (tuple == null)
			{
				return tuple2 == null;
			}
			if (tuple.Item2 == null)
			{
				return tuple2 != null && tuple2.Item2 == null;
			}
			return tuple.Item2.Equals((tuple2 != null) ? tuple2.Item2 : null);
		}

		// Token: 0x0600807D RID: 32893 RVA: 0x0003995D File Offset: 0x00037B5D
		protected override int GetHashCodeOnInput(State state)
		{
			return 397;
		}

		// Token: 0x0600807E RID: 32894 RVA: 0x00002188 File Offset: 0x00000388
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			return null;
		}

		// Token: 0x0600807F RID: 32895 RVA: 0x001AE160 File Offset: 0x001AC360
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			List<Tuple<State, TextTableSpec>> list = new List<Tuple<State, TextTableSpec>>();
			Func<KeyValuePair<State, IEnumerable<object>>, State> <>9__0;
			foreach (Tuple<State, TextTableSpec> tuple in this.TableSpecs)
			{
				State item = tuple.Item1;
				TextTableSpec item2 = tuple.Item2;
				List<TextSubsequenceSpec> list2 = new List<TextSubsequenceSpec>();
				for (int i = 0; i < item2.ColumnSpecs.Length; i++)
				{
					IEnumerable<KeyValuePair<State, IEnumerable<object>>> positiveExamples = item2.ColumnSpecs[i].PositiveExamples;
					Func<KeyValuePair<State, IEnumerable<object>>, State> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (KeyValuePair<State, IEnumerable<object>> x) => transformer(x.Key));
					}
					TextSubsequenceSpec textSubsequenceSpec = new TextSubsequenceSpec(positiveExamples.ToDictionary(func, (KeyValuePair<State, IEnumerable<object>> x) => x.Value), item2.ColumnSpecs[i].NodeIndexes.ToDictionary<State, int[]>(), item2.ColumnSpecs[i].SoftConstraints.ToDictionary<State, bool[]>());
					list2.Add(textSubsequenceSpec);
				}
				list.Add(Tuple.Create<State, TextTableSpec>(tuple.Item1, new TextTableSpec(list2)));
			}
			return new MultiPageTextTableSpec(list);
		}

		// Token: 0x06008080 RID: 32896 RVA: 0x000373A6 File Offset: 0x000355A6
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			base.ThrowSerializationUnsupportedException();
			return null;
		}
	}
}
