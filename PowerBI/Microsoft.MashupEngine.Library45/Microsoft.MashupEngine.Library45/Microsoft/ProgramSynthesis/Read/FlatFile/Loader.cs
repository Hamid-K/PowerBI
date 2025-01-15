using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile
{
	// Token: 0x02001249 RID: 4681
	public class Loader : IProgramLoader<Program, string, ITable<string>>
	{
		// Token: 0x06008CD1 RID: 36049 RVA: 0x00002130 File Offset: 0x00000330
		private Loader()
		{
		}

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x06008CD2 RID: 36050 RVA: 0x001D92F6 File Offset: 0x001D74F6
		public static Loader Instance { get; } = new Loader();

		// Token: 0x06008CD3 RID: 36051 RVA: 0x001D9300 File Offset: 0x001D7500
		public Program Create(ProgramNode program)
		{
			return Language.Build.Node.Cast.readFlatFile(program).Switch<Program>(Language.Build, (Csv csv) => new CsvProgram(csv, null, 0, false, false, null), (Fw fw) => new FwProgram(fw, null, 0, false, false, null), (StringRegionToStringTable etext) => new ExtractionTextProgram(etext));
		}

		// Token: 0x06008CD4 RID: 36052 RVA: 0x001D938C File Offset: 0x001D758C
		public Program Load(string serializedProgram, ASTSerializationFormat serializationFormat, DeserializationContext context, ProgramNodeParser programNodeParser)
		{
			Program program;
			try
			{
				Loader.<>c__DisplayClass6_0 CS$<>8__locals1;
				CS$<>8__locals1.root = XElement.Parse(serializedProgram);
				XElement xelement = CS$<>8__locals1.root.Descendants("Program").Single<XElement>();
				string text = ((serializationFormat == ASTSerializationFormat.XML) ? xelement.Elements().First<XElement>().ToString() : xelement.Value);
				ProgramNode programNode = programNodeParser(text, Language.Grammar.StartSymbol, serializationFormat, new ParseSettings(context, null));
				if (programNode == null)
				{
					throw new InvalidOperationException("Invalid program text");
				}
				readFlatFile? readFlatFile = readFlatFile.CreateSafe(Loader.Builder, programNode);
				if (readFlatFile == null)
				{
					throw new InvalidOperationException("Unknown program type");
				}
				int skipEmptyAndCommentCount = Loader.<Load>g__readInt|6_0("SkipEmptyAndCommentCount", ref CS$<>8__locals1);
				bool hasEmptyLines = Loader.<Load>g__readBool|6_1("HasEmptyLines", ref CS$<>8__locals1);
				bool hasMultiLineRows = Loader.<Load>g__readBool|6_1("HasMultiLineRows", ref CS$<>8__locals1);
				XElement xelement2 = CS$<>8__locals1.root.Descendants("NewLineStrings").FirstOrDefault<XElement>();
				IReadOnlyList<string> newLineStrs = ((xelement2 != null) ? StdLiteralParsing.TryParse<IReadOnlyList<string>>(xelement2, default(DeserializationContext)).OrElseDefault<IReadOnlyList<string>>() : null);
				XElement xelement3 = CS$<>8__locals1.root.Descendants("RawColumnNames").FirstOrDefault<XElement>();
				IReadOnlyList<string> rawColumnNames = ((xelement3 != null) ? StdLiteralParsing.TryParse<IReadOnlyList<string>>(xelement3, default(DeserializationContext)).OrElseDefault<IReadOnlyList<string>>() : null);
				program = readFlatFile.Value.Switch<Program>(Loader.Builder, (Csv csv) => new CsvProgram(csv, rawColumnNames, skipEmptyAndCommentCount, hasEmptyLines, hasMultiLineRows, newLineStrs), (Fw fw) => new FwProgram(fw, rawColumnNames, skipEmptyAndCommentCount, hasEmptyLines, hasMultiLineRows, newLineStrs), (StringRegionToStringTable etext) => new ExtractionTextProgram(etext));
			}
			catch (XmlException ex)
			{
				throw new InvalidOperationException("Invalid serialized text", ex);
			}
			return program;
		}

		// Token: 0x06008CD5 RID: 36053 RVA: 0x001D956C File Offset: 0x001D776C
		public Program Load(string serializedProgram, ASTSerializationFormat serializationFormat = ASTSerializationFormat.XML, DeserializationContext context = default(DeserializationContext))
		{
			ProgramNodeParser programNodeParser;
			if ((programNodeParser = Loader.<>O.<0>__Parse) == null)
			{
				programNodeParser = (Loader.<>O.<0>__Parse = new ProgramNodeParser(ProgramNode.Parse));
			}
			return this.Load(serializedProgram, serializationFormat, context, programNodeParser);
		}

		// Token: 0x06008CD7 RID: 36055 RVA: 0x001D95B0 File Offset: 0x001D77B0
		[CompilerGenerated]
		internal static int <Load>g__readInt|6_0(string attrName, ref Loader.<>c__DisplayClass6_0 A_1)
		{
			XAttribute xattribute = A_1.root.Attribute(attrName);
			if (xattribute != null)
			{
				return int.Parse(xattribute.Value);
			}
			return 0;
		}

		// Token: 0x06008CD8 RID: 36056 RVA: 0x001D95E0 File Offset: 0x001D77E0
		[CompilerGenerated]
		internal static bool <Load>g__readBool|6_1(string attrName, ref Loader.<>c__DisplayClass6_0 A_1)
		{
			XAttribute xattribute = A_1.root.Attribute(attrName);
			return xattribute != null && bool.Parse(xattribute.Value);
		}

		// Token: 0x040039A8 RID: 14760
		protected static readonly GrammarBuilders Builder = GrammarBuilders.Instance(Language.Grammar);

		// Token: 0x0200124A RID: 4682
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040039AA RID: 14762
			public static ProgramNodeParser <0>__Parse;
		}
	}
}
