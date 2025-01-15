using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TreeOutput;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Semantics
{
	// Token: 0x02000B71 RID: 2929
	public static class Semantics
	{
		// Token: 0x06004A5F RID: 19039 RVA: 0x000E9A28 File Offset: 0x000E7C28
		public static ITreeOutput<JsonRegion> Sequence(string id, IEnumerable<JsonRegion> selectSequence)
		{
			return new SequenceOutput<JsonRegion>(id, selectSequence.Select((JsonRegion n) => new StructOutput<JsonRegion>(id, n, null)));
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x000E9A5F File Offset: 0x000E7C5F
		public static ITreeOutput<JsonRegion> DummySequence(IEnumerable<ITreeOutput<JsonRegion>> sequenceBody)
		{
			return new SequenceOutput<JsonRegion>(string.Empty, sequenceBody);
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x000E9A6C File Offset: 0x000E7C6C
		public static ITreeOutput<JsonRegion> Struct(JsonRegion v, IEnumerable<ITreeOutput<JsonRegion>> structBodyRec)
		{
			return new StructOutput<JsonRegion>(string.Empty, v, structBodyRec);
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x000E9A7A File Offset: 0x000E7C7A
		[LazySemantics]
		public static ITreeOutput<JsonRegion> Field(JsonRegion v, string id, JsonRegion selectRegion)
		{
			return new StructOutput<JsonRegion>(id, selectRegion, null);
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x000E9A84 File Offset: 0x000E7C84
		public static IEnumerable<ITreeOutput<JsonRegion>> Concat(ITreeOutput<JsonRegion> treeOutput, IEnumerable<ITreeOutput<JsonRegion>> structBodyRec)
		{
			return Semantics.ToList(treeOutput).Concat(structBodyRec).ToArray<ITreeOutput<JsonRegion>>();
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x000E9A97 File Offset: 0x000E7C97
		public static IEnumerable<ITreeOutput<JsonRegion>> ToList(ITreeOutput<JsonRegion> treeOutput)
		{
			return new ITreeOutput<JsonRegion>[] { treeOutput };
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x000E9AA3 File Offset: 0x000E7CA3
		public static IEnumerable<ITreeOutput<JsonRegion>> Empty()
		{
			return Enumerable.Empty<ITreeOutput<JsonRegion>>();
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x000E9AAC File Offset: 0x000E7CAC
		public static JsonRegion SelectRegion(JsonRegion v, JPath path)
		{
			JToken jtoken = v.Token;
			JPathStep[] steps = path.Steps;
			for (int i = 0; i < steps.Length; i++)
			{
				jtoken = steps[i].Apply(jtoken).SingleOrDefault<JToken>();
				if (jtoken.IsNullOrNullType())
				{
					return null;
				}
			}
			return JsonRegion.Create(jtoken);
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x000E9AF4 File Offset: 0x000E7CF4
		public static IEnumerable<JsonRegion> SelectSequence(JsonRegion v, JPath path)
		{
			IEnumerable<JToken> enumerable = new JToken[] { v.Token };
			enumerable = path.Steps.Aggregate(enumerable, (IEnumerable<JToken> current, JPathStep step) => current.SelectMany(new Func<JToken, IEnumerable<JToken>>(step.Apply)));
			return enumerable.Select(delegate(JToken token)
			{
				if (!token.IsNullOrNullType())
				{
					return JsonRegion.Create(token);
				}
				return null;
			});
		}
	}
}
