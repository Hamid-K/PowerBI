using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.SchemaParser;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000CB RID: 203
	public static class SchemaProgramLearnerExtensions
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x0000FC74 File Offset: 0x0000DE74
		public static IEnumerable<TSchemaProgram> LearnSchema<TSchemaProgram, TSequenceProgram, TRegionProgram, TSelector, TRegion>(this SchemaLearner<TSchemaProgram, TSequenceProgram, TRegionProgram, TSelector, TRegion> learner, string schema, string input, IDictionary<string, IEnumerable<TSelector>> positiveExamples, IDictionary<string, IEnumerable<TSelector>> negativeExamples, int k = 1, bool learnAll = false) where TSchemaProgram : SchemaProgram<TSequenceProgram, TRegionProgram, TSelector, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
		{
			TRegion doc = learner.StringToInput(input);
			Func<TSelector, TRegion> <>9__2;
			Func<TSelector, TRegion> <>9__3;
			return learner.LearnSchema(schema, Seq.Of<DocumentSpec<TRegion>>(new DocumentSpec<TRegion>[]
			{
				new DocumentSpec<TRegion>(doc, positiveExamples.Select(delegate(KeyValuePair<string, IEnumerable<TSelector>> keyvalue)
				{
					string key = keyvalue.Key;
					IEnumerable<TSelector> value = keyvalue.Value;
					Func<TSelector, TRegion> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (TSelector x) => learner.Select(doc, x));
					}
					return Record.Create<string, IEnumerable<TRegion>>(key, value.Select(func));
				}).ToDictionary<string, IEnumerable<TRegion>>(), negativeExamples.Select(delegate(KeyValuePair<string, IEnumerable<TSelector>> keyvalue)
				{
					string key2 = keyvalue.Key;
					IEnumerable<TSelector> value2 = keyvalue.Value;
					Func<TSelector, TRegion> func2;
					if ((func2 = <>9__3) == null)
					{
						func2 = (<>9__3 = (TSelector x) => learner.Select(doc, x));
					}
					return Record.Create<string, IEnumerable<TRegion>>(key2, value2.Select(func2));
				}).ToDictionary<string, IEnumerable<TRegion>>())
			}), k, learnAll, null);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000FCFC File Offset: 0x0000DEFC
		public static IEnumerable<TSchemaProgram> LearnSchemaExt<TSchemaProgram, TSequenceProgram, TRegionProgram, TSelector, TRegion>(this SchemaLearner<TSchemaProgram, TSequenceProgram, TRegionProgram, TSelector, TRegion> learner, string schema, IEnumerable<Tuple<TRegion, IDictionary<string, IEnumerable<TSelector>>, IDictionary<string, IEnumerable<TSelector>>>> specs, int k = 1, bool learnAll = false, ConvertSchemaElementInterface[] converters = null) where TSchemaProgram : SchemaProgram<TSequenceProgram, TRegionProgram, TSelector, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
		{
			List<DocumentSpec<TRegion>> list = new List<DocumentSpec<TRegion>>();
			foreach (Tuple<TRegion, IDictionary<string, IEnumerable<TSelector>>, IDictionary<string, IEnumerable<TSelector>>> tuple in specs)
			{
				TRegion doc = tuple.Item1;
				IEnumerable<KeyValuePair<string, IEnumerable<TSelector>>> item = tuple.Item2;
				IDictionary<string, IEnumerable<TSelector>> item2 = tuple.Item3;
				Func<TSelector, TRegion> <>9__4;
				Dictionary<string, IEnumerable<TRegion>> dictionary = item.ToDictionary((KeyValuePair<string, IEnumerable<TSelector>> pair) => pair.Key, delegate(KeyValuePair<string, IEnumerable<TSelector>> pair)
				{
					IEnumerable<TSelector> value = pair.Value;
					Func<TSelector, TRegion> func;
					if ((func = <>9__4) == null)
					{
						func = (<>9__4 = (TSelector x) => learner.Select(doc, x));
					}
					return value.Select(func).ToList<TRegion>().AsEnumerable<TRegion>();
				});
				Func<TSelector, TRegion> <>9__5;
				Dictionary<string, IEnumerable<TRegion>> dictionary2 = item2.ToDictionary((KeyValuePair<string, IEnumerable<TSelector>> pair) => pair.Key, delegate(KeyValuePair<string, IEnumerable<TSelector>> pair)
				{
					IEnumerable<TSelector> value2 = pair.Value;
					Func<TSelector, TRegion> func2;
					if ((func2 = <>9__5) == null)
					{
						func2 = (<>9__5 = (TSelector x) => learner.Select(doc, x));
					}
					return value2.Select(func2).ToList<TRegion>().AsEnumerable<TRegion>();
				});
				list.Add(new DocumentSpec<TRegion>(doc, dictionary, dictionary2));
			}
			return learner.LearnSchema(schema, list, k, learnAll, converters);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000FE10 File Offset: 0x0000E010
		public static IEnumerable<TSchemaProgram> LearnSchemaExt<TSchemaProgram, TSequenceProgram, TRegionProgram, TSelector, TRegion>(this SchemaLearner<TSchemaProgram, TSequenceProgram, TRegionProgram, TSelector, TRegion> learner, string schema, IEnumerable<Tuple<TRegion, IDictionary<string, IEnumerable<TRegion>>, IDictionary<string, IEnumerable<TRegion>>>> specs, int k = 1, bool learnAll = false, ConvertSchemaElementInterface[] converters = null) where TSchemaProgram : SchemaProgram<TSequenceProgram, TRegionProgram, TSelector, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
		{
			List<DocumentSpec<TRegion>> list = specs.Select((Tuple<TRegion, IDictionary<string, IEnumerable<TRegion>>, IDictionary<string, IEnumerable<TRegion>>> spec) => new DocumentSpec<TRegion>(spec.Item1, spec.Item2, spec.Item3)).ToList<DocumentSpec<TRegion>>();
			return learner.LearnSchema(schema, list, k, learnAll, converters);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000FE55 File Offset: 0x0000E055
		public static IEnumerable<TSchemaProgram> LearnSchemaExt<TSchemaProgram, TSequenceProgram, TRegionProgram, TSelector, TRegion>(this SchemaLearner<TSchemaProgram, TSequenceProgram, TRegionProgram, TSelector, TRegion> learner, string schema, TRegion doc, IDictionary<string, IEnumerable<TRegion>> positiveExamples, IDictionary<string, IEnumerable<TRegion>> negativeExamples, int k = 1, bool learnAll = false, ConvertSchemaElementInterface[] converters = null) where TSchemaProgram : SchemaProgram<TSequenceProgram, TRegionProgram, TSelector, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
		{
			return learner.LearnSchema(schema, Seq.Of<DocumentSpec<TRegion>>(new DocumentSpec<TRegion>[]
			{
				new DocumentSpec<TRegion>(doc, positiveExamples, negativeExamples)
			}), k, learnAll, converters);
		}
	}
}
