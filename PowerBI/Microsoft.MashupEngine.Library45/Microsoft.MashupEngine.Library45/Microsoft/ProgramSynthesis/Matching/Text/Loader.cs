using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011B1 RID: 4529
	public class Loader : IProgramLoader<Program, string, bool>
	{
		// Token: 0x060086E1 RID: 34529 RVA: 0x00002130 File Offset: 0x00000330
		private Loader()
		{
		}

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x060086E2 RID: 34530 RVA: 0x001C516B File Offset: 0x001C336B
		public static Loader Instance { get; } = new Loader();

		// Token: 0x060086E3 RID: 34531 RVA: 0x001C5172 File Offset: 0x001C3372
		public Program Load(string serializedProgram, ASTSerializationFormat serializationFormat = ASTSerializationFormat.XML, DeserializationContext context = default(DeserializationContext))
		{
			ProgramNodeParser programNodeParser;
			if ((programNodeParser = Loader.<>O.<0>__Parse) == null)
			{
				programNodeParser = (Loader.<>O.<0>__Parse = new ProgramNodeParser(ProgramNode.Parse));
			}
			return this.Load(serializedProgram, serializationFormat, context, programNodeParser);
		}

		// Token: 0x060086E4 RID: 34532 RVA: 0x001C5198 File Offset: 0x001C3398
		public Program Load(string serializedProgram, ASTSerializationFormat serializationFormat, DeserializationContext context, ProgramNodeParser programNodeParser)
		{
			ParseSettings parseSettings = new ParseSettings(context, null);
			if (serializationFormat != ASTSerializationFormat.XML)
			{
				if (serializationFormat != ASTSerializationFormat.HumanReadable)
				{
					throw new ArgumentOutOfRangeException("serializationFormat", serializationFormat, FormattableString.Invariant(FormattableStringFactory.Create("Deserializing from the {0} format is not supported.", new object[] { serializationFormat })));
				}
			}
			else
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
				{
					CheckCharacters = false,
					DtdProcessing = DtdProcessing.Prohibit,
					XmlResolver = null
				};
				using (StringReader stringReader = new StringReader(serializedProgram))
				{
					using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
					{
						XElement xelement = XElement.Load(xmlReader);
						ProgramNode programNode = ProgramNode.ParseXML(Language.Grammar, xelement.Elements().FirstOrDefault<XElement>(), parseSettings);
						if (programNode == null)
						{
							return null;
						}
						XAttribute xattribute = xelement.Attribute("score");
						string text = ((xattribute != null) ? xattribute.Value : null);
						return string.IsNullOrEmpty(text) ? new Program(programNode) : new Program(programNode, double.Parse(text, CultureInfo.InvariantCulture));
					}
				}
			}
			ProgramNode programNode2 = programNodeParser(serializedProgram, Language.Grammar.StartSymbol, serializationFormat, parseSettings);
			if (!(programNode2 == null))
			{
				return new Program(programNode2);
			}
			return null;
		}

		// Token: 0x060086E5 RID: 34533 RVA: 0x001C5073 File Offset: 0x001C3273
		public Program Create(ProgramNode program)
		{
			return new Program(program);
		}

		// Token: 0x020011B2 RID: 4530
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040037BB RID: 14267
			public static ProgramNodeParser <0>__Parse;
		}
	}
}
