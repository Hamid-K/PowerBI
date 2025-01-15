using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Json.Build;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Schema.Element;

namespace Microsoft.ProgramSynthesis.Extraction.Json
{
	// Token: 0x02000B33 RID: 2867
	public static class SchemaParser
	{
		// Token: 0x06004789 RID: 18313 RVA: 0x000E0858 File Offset: 0x000DEA58
		internal static ISchemaElement<JsonRegion> ParseOutput(output programNode)
		{
			return programNode.Switch<ISchemaElement<JsonRegion>>(Language.Build, delegate(output_struct @struct)
			{
				@struct struct2 = @struct.@struct;
				GrammarBuilders build = Language.Build;
				Func<Struct, ISchemaElement<JsonRegion>> func;
				if ((func = SchemaParser.<>O.<0>__ParseStruct) == null)
				{
					func = (SchemaParser.<>O.<0>__ParseStruct = new Func<Struct, ISchemaElement<JsonRegion>>(SchemaParser.ParseStruct));
				}
				Func<Field, ISchemaElement<JsonRegion>> func2;
				if ((func2 = SchemaParser.<>O.<1>__ParseField) == null)
				{
					func2 = (SchemaParser.<>O.<1>__ParseField = new Func<Field, ISchemaElement<JsonRegion>>(SchemaParser.ParseField));
				}
				return struct2.Switch<ISchemaElement<JsonRegion>>(build, func, func2);
			}, delegate(output_sequence sequence)
			{
				sequence sequence2 = sequence.sequence;
				GrammarBuilders build2 = Language.Build;
				Func<Sequence, ISchemaElement<JsonRegion>> func3;
				if ((func3 = SchemaParser.<>O.<2>__ParseSequence) == null)
				{
					func3 = (SchemaParser.<>O.<2>__ParseSequence = new Func<Sequence, ISchemaElement<JsonRegion>>(SchemaParser.ParseSequence));
				}
				Func<DummySequence, ISchemaElement<JsonRegion>> func4;
				if ((func4 = SchemaParser.<>O.<3>__ParseDummySequence) == null)
				{
					func4 = (SchemaParser.<>O.<3>__ParseDummySequence = new Func<DummySequence, ISchemaElement<JsonRegion>>(SchemaParser.ParseDummySequence));
				}
				return sequence2.Switch<ISchemaElement<JsonRegion>>(build2, func3, func4);
			});
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x000E08AF File Offset: 0x000DEAAF
		private static ISchemaElement<JsonRegion> ParseStruct(Struct @struct)
		{
			return new StructElement<JsonRegion>(string.Empty, true, false, SchemaParser.ParseStructBodyRec(@struct.structBodyRec));
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x000E08CC File Offset: 0x000DEACC
		private static ISchemaElement<JsonRegion> ParseField(Field programNode)
		{
			return new StructElement<JsonRegion>(programNode.id.Value, true, true, null);
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x000E08F0 File Offset: 0x000DEAF0
		private static IEnumerable<ISchemaElement<JsonRegion>> ParseStructBodyRec(structBodyRec node)
		{
			return node.Switch<IEnumerable<ISchemaElement<JsonRegion>>>(Language.Build, delegate(Concat concat)
			{
				List<ISchemaElement<JsonRegion>> list = new List<ISchemaElement<JsonRegion>>();
				Concat concat2 = concat;
				structBodyRec structBodyRec;
				do
				{
					list.Add(SchemaParser.ParseOutput(concat2.output));
					structBodyRec = concat2.structBodyRec;
				}
				while (structBodyRec.Is_Concat(Language.Build, out concat2));
				return list.Concat(SchemaParser.ParseStructBodyRec(structBodyRec));
			}, (ToList toList) => new ISchemaElement<JsonRegion>[] { SchemaParser.ParseOutput(toList.output) }, (Empty empty) => Enumerable.Empty<ISchemaElement<JsonRegion>>());
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x000E0968 File Offset: 0x000DEB68
		private static ISchemaElement<JsonRegion> ParseSequence(Sequence programNode)
		{
			return new SequenceElement<JsonRegion>(programNode.id.Value, true, true, null);
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x000E098C File Offset: 0x000DEB8C
		private static ISchemaElement<JsonRegion> ParseDummySequence(DummySequence programNode)
		{
			return SchemaParser.ParseSequenceBody(programNode.sequenceBody);
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x000E099C File Offset: 0x000DEB9C
		private static ISchemaElement<JsonRegion> ParseSequenceBody(sequenceBody programNode)
		{
			return new SequenceElement<JsonRegion>(string.Empty, true, false, SchemaParser.ParseWrapStruct(programNode.Cast_SequenceBody().wrapStruct));
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x000E09CC File Offset: 0x000DEBCC
		private static ISchemaElement<JsonRegion> ParseWrapStruct(wrapStruct programNode)
		{
			return SchemaParser.ParseOutput(programNode.Cast_WrapStructLet().output);
		}

		// Token: 0x02000B34 RID: 2868
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040020BF RID: 8383
			public static Func<Struct, ISchemaElement<JsonRegion>> <0>__ParseStruct;

			// Token: 0x040020C0 RID: 8384
			public static Func<Field, ISchemaElement<JsonRegion>> <1>__ParseField;

			// Token: 0x040020C1 RID: 8385
			public static Func<Sequence, ISchemaElement<JsonRegion>> <2>__ParseSequence;

			// Token: 0x040020C2 RID: 8386
			public static Func<DummySequence, ISchemaElement<JsonRegion>> <3>__ParseDummySequence;
		}
	}
}
