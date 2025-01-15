using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json
{
	// Token: 0x02000B2C RID: 2860
	public class Loader : IProgramLoader<Program, string, ITable<string>>
	{
		// Token: 0x0600475E RID: 18270 RVA: 0x00002130 File Offset: 0x00000330
		private Loader()
		{
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x0600475F RID: 18271 RVA: 0x000DFA71 File Offset: 0x000DDC71
		public static Loader Instance { get; } = new Loader();

		// Token: 0x06004760 RID: 18272 RVA: 0x000DFA78 File Offset: 0x000DDC78
		public Program Load(string serializedProgram, ASTSerializationFormat serializationFormat = ASTSerializationFormat.XML, DeserializationContext context = default(DeserializationContext))
		{
			ProgramNodeParser programNodeParser;
			if ((programNodeParser = Loader.<>O.<0>__Parse) == null)
			{
				programNodeParser = (Loader.<>O.<0>__Parse = new ProgramNodeParser(ProgramNode.Parse));
			}
			return this.Load(serializedProgram, serializationFormat, context, programNodeParser);
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x000DFAA0 File Offset: 0x000DDCA0
		public Program Load(string serializedProgram, ASTSerializationFormat serializationFormat, DeserializationContext context, ProgramNodeParser programNodeParser)
		{
			ParseSettings parseSettings = new ParseSettings(context, null);
			Program program;
			try
			{
				XElement xelement = XElement.Parse(serializedProgram);
				if (xelement.Name != "JsonExtraction")
				{
					program = Loader.LoadProgramWithoutVersion(serializationFormat, parseSettings, programNodeParser, xelement);
				}
				else
				{
					double num = ((xelement.Attribute("score") != null) ? double.Parse(xelement.Attribute("score").Value, CultureInfo.InvariantCulture) : 0.0);
					bool flag = xelement.Attribute("ndjson") != null && bool.Parse(xelement.Attribute("ndjson").Value);
					XAttribute xattribute = xelement.Attribute("start-delimiter");
					string text = ((xattribute != null) ? xattribute.Value : null);
					if (text == string.Empty)
					{
						text = null;
					}
					XAttribute xattribute2 = xelement.Attribute("end-delimiter");
					string text2 = ((xattribute2 != null) ? xattribute2.Value : null);
					if (text2 == string.Empty)
					{
						text2 = null;
					}
					bool flag2 = true;
					if (xelement.Attribute("invalid-json") != null)
					{
						flag2 = bool.Parse(xelement.Attribute("invalid-json").Value);
					}
					Version version;
					if (xelement.Attribute("version") == null || !Version.TryParse(xelement.Attribute("version").Value, out version))
					{
						throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text! No version found.", Array.Empty<object>())));
					}
					XAttribute xattribute3 = xelement.Attribute("symbol");
					if (xattribute3 == null)
					{
						throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text! No program symbol found.", Array.Empty<object>())));
					}
					Symbol symbol = Language.Grammar.Symbol(xattribute3.Value);
					if (symbol == null)
					{
						throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text! Unknown program symbol {0}", new object[] { xattribute3 })));
					}
					string text3 = ((serializationFormat == ASTSerializationFormat.XML) ? xelement.Elements().First<XElement>().ToString() : xelement.Value);
					ProgramNode programNode = programNodeParser(text3, symbol, serializationFormat, parseSettings);
					if (programNode == null)
					{
						throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text! Program node is not parsable.\n{0}", new object[] { text3 })));
					}
					program = Loader.LoadProgramWithVersion(Language.Build.Node.Cast.output(programNode), version, flag, text, text2, flag2, num);
				}
			}
			catch (XmlException ex)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text!", Array.Empty<object>())), ex);
			}
			return program;
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x000DFD54 File Offset: 0x000DDF54
		public Program Create(ProgramNode program)
		{
			return new Program(Language.Build.Node.Cast.output(program), false, null, null, true, 0.0, null);
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x000DFD80 File Offset: 0x000DDF80
		private static Program LoadProgramWithVersion(output programNode, Version version, bool ndJson, string startDelimiter, string endDelimiter, bool handleInvalidJson, double score)
		{
			if (version <= new Version(0, 1) && ndJson)
			{
				output_sequence? output_sequence;
				DummySequence? dummySequence;
				output? output = ((programNode.As_output_sequence(Language.Build) != null) ? ((output_sequence.GetValueOrDefault().sequence.As_DummySequence(Language.Build) != null) ? new output?(dummySequence.GetValueOrDefault().sequenceBody.Cast_SequenceBody().wrapStruct.Cast_WrapStructLet().output) : null) : null);
				if (output == null)
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text!", Array.Empty<object>())));
				}
				programNode = output.Value;
			}
			return new Program(programNode, ndJson, startDelimiter, endDelimiter, handleInvalidJson, score, null);
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x000DFE6C File Offset: 0x000DE06C
		private static Program LoadProgramWithoutVersion(ASTSerializationFormat serializationFormat, ParseSettings settings, ProgramNodeParser programNodeParser, XElement element)
		{
			string text = ((serializationFormat == ASTSerializationFormat.XML) ? element.ToString() : element.Value);
			ProgramNode programNode = programNodeParser(text, Language.Grammar.StartSymbol, serializationFormat, settings);
			if (programNode == null)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text! Program node is not parsable.\n{0}", new object[] { text })));
			}
			return new Program(Language.Build.Node.Cast.output(programNode), false, null, null, true, 0.0, null);
		}

		// Token: 0x02000B2D RID: 2861
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400209B RID: 8347
			public static ProgramNodeParser <0>__Parse;
		}
	}
}
