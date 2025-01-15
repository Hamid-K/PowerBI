using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000A2 RID: 162
	public abstract class ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion> : IProgramLoader<TRegionProgram, IEnumerable<TRegion>, IEnumerable<TRegion>>, IProgramLoader<TSequenceProgram, IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00004FAE File Offset: 0x000031AE
		public IProgramLoader<TRegionProgram, IEnumerable<TRegion>, IEnumerable<TRegion>> Region
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00004FAE File Offset: 0x000031AE
		public IProgramLoader<TSequenceProgram, IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>> Sequence
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003D2 RID: 978
		public abstract Grammar Grammar { get; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003D3 RID: 979
		protected abstract SortedDictionary<Version, ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.VersionParser> VersionParsers { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003D4 RID: 980
		protected abstract ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.VersionParser DefaultVersionParser { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003D5 RID: 981
		protected abstract Dictionary<Record<Version, Version>, Func<XElement, Version, string>> VersionConverters { get; }

		// Token: 0x060003D6 RID: 982 RVA: 0x0000D86A File Offset: 0x0000BA6A
		TRegionProgram IProgramLoader<TRegionProgram, IEnumerable<TRegion>, IEnumerable<TRegion>>.Load(string serializedProgram, ASTSerializationFormat serializationFormat, DeserializationContext context)
		{
			return this.Load(serializedProgram, serializationFormat, context) as TRegionProgram;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000D87F File Offset: 0x0000BA7F
		TRegionProgram IProgramLoader<TRegionProgram, IEnumerable<TRegion>, IEnumerable<TRegion>>.Load(string serializedProgram, ASTSerializationFormat serializationFormat, DeserializationContext context, ProgramNodeParser programNodeParser)
		{
			return this.Load(serializedProgram, serializationFormat, context, programNodeParser) as TRegionProgram;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000D898 File Offset: 0x0000BA98
		TSequenceProgram IProgramLoader<TSequenceProgram, IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>.Create(ProgramNode program)
		{
			return (TSequenceProgram)((object)this.CreateProgram(program, ExtractionKind.Sequence, ReferenceKind.Invalid, null));
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000D8BC File Offset: 0x0000BABC
		TRegionProgram IProgramLoader<TRegionProgram, IEnumerable<TRegion>, IEnumerable<TRegion>>.Create(ProgramNode program)
		{
			return (TRegionProgram)((object)this.CreateProgram(program, ExtractionKind.Region, ReferenceKind.Invalid, null));
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		TSequenceProgram IProgramLoader<TSequenceProgram, IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>.Load(string serializedProgram, ASTSerializationFormat serializationFormat, DeserializationContext context)
		{
			return this.Load(serializedProgram, serializationFormat, context) as TSequenceProgram;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000D8F5 File Offset: 0x0000BAF5
		TSequenceProgram IProgramLoader<TSequenceProgram, IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>.Load(string serializedProgram, ASTSerializationFormat serializationFormat, DeserializationContext context, ProgramNodeParser programNodeParser)
		{
			return this.Load(serializedProgram, serializationFormat, context, programNodeParser) as TSequenceProgram;
		}

		// Token: 0x060003DC RID: 988
		public abstract IExtractionProgram<TRegion> CreateProgram(ProgramNode programNode, ExtractionKind extractionKind, ReferenceKind refKind, double? score = null);

		// Token: 0x060003DD RID: 989 RVA: 0x0000D90C File Offset: 0x0000BB0C
		public IExtractionProgram<TRegion> Load(string serializedProgram, ASTSerializationFormat serializationFormat = ASTSerializationFormat.XML, DeserializationContext context = default(DeserializationContext))
		{
			ProgramNodeParser programNodeParser;
			if ((programNodeParser = ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.<>O.<0>__Parse) == null)
			{
				programNodeParser = (ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.<>O.<0>__Parse = new ProgramNodeParser(ProgramNode.Parse));
			}
			return this.Load(serializedProgram, serializationFormat, context, programNodeParser);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000D934 File Offset: 0x0000BB34
		public IExtractionProgram<TRegion> Load(string serializedProgram, ASTSerializationFormat serializationFormat, DeserializationContext context, ProgramNodeParser programNodeParser)
		{
			XElement xelement;
			try
			{
				xelement = XElement.Parse(serializedProgram);
			}
			catch (XmlException ex)
			{
				throw new InvalidOperationException("Invalid program text!", ex);
			}
			ProgramNodeParser programNodeParser2;
			if ((programNodeParser2 = programNodeParser) == null && (programNodeParser2 = ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.<>O.<0>__Parse) == null)
			{
				programNodeParser2 = (ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.<>O.<0>__Parse = new ProgramNodeParser(ProgramNode.Parse));
			}
			programNodeParser = programNodeParser2;
			XAttribute xattribute = xelement.Attribute("version");
			Version version;
			if (xattribute == null || !Version.TryParse(xattribute.Value, out version))
			{
				return this.DefaultVersionParser(xelement, this.Grammar, new Func<ProgramNode, ExtractionKind, ReferenceKind, double?, IExtractionProgram<TRegion>>(this.CreateProgram), serializationFormat, context, programNodeParser);
			}
			foreach (KeyValuePair<Version, ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.VersionParser> keyValuePair in this.VersionParsers)
			{
				if (version >= keyValuePair.Key)
				{
					return keyValuePair.Value(xelement, this.Grammar, new Func<ProgramNode, ExtractionKind, ReferenceKind, double?, IExtractionProgram<TRegion>>(this.CreateProgram), serializationFormat, context, programNodeParser);
				}
			}
			throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text! Unrecognized version {0}", new object[] { xattribute.Value })));
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000DA6C File Offset: 0x0000BC6C
		protected ProgramNode ParseProgramNode(XElement progElement, Grammar grammar, ASTSerializationFormat serializationFormat, ParseSettings settings, ProgramNodeParser programNodeParser, out double score)
		{
			XAttribute xattribute = progElement.Attribute("score");
			double.TryParse((xattribute != null) ? xattribute.Value : null, NumberStyles.Any, CultureInfo.InvariantCulture, out score);
			XAttribute xattribute2 = progElement.Attribute("symbol");
			if (xattribute2 == null)
			{
				throw new InvalidOperationException("Invalid program text! No program symbol found.");
			}
			Symbol symbol = grammar.Symbol(xattribute2.Value);
			if (symbol == null)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text! Unknown program symbol {0}", new object[] { xattribute2 })));
			}
			string text = ((serializationFormat == ASTSerializationFormat.XML) ? progElement.Elements().First<XElement>().ToString() : progElement.Value);
			ProgramNode programNode = programNodeParser(text, symbol, serializationFormat, settings);
			if (programNode == null)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text! Program node is not parsable.\n{0}", new object[] { text })));
			}
			return programNode;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000DB50 File Offset: 0x0000BD50
		public string Convert(string serializedProgram, Version from, Version to)
		{
			if (from >= to)
			{
				throw new ArgumentException("Cannot convert newer version to older version!", "to");
			}
			XElement xelement;
			try
			{
				xelement = XElement.Parse(serializedProgram);
			}
			catch (XmlException ex)
			{
				throw new ArgumentException("Invalid program text!", ex);
			}
			KeyValuePair<Version, ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.VersionParser> keyValuePair = this.VersionParsers.FirstOrDefault((KeyValuePair<Version, ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.VersionParser> p) => from >= p.Key);
			KeyValuePair<Version, ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.VersionParser> keyValuePair2 = this.VersionParsers.FirstOrDefault((KeyValuePair<Version, ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.VersionParser> p) => to >= p.Key);
			if (keyValuePair.Equals(default(KeyValuePair<Version, ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.VersionParser>)))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot find a suitable version for {0}", new object[] { from })), "from");
			}
			if (keyValuePair2.Equals(default(KeyValuePair<Version, ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion>.VersionParser>)))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot find a suitable version for {0}", new object[] { to })), "to");
			}
			Func<XElement, Version, string> func;
			if (!this.VersionConverters.TryGetValue(Record.Create<Version, Version>(keyValuePair.Key, keyValuePair2.Key), out func))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot convert program from version {0} to version {1}!", new object[] { from, to })));
			}
			return func(xelement, to);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000DCD8 File Offset: 0x0000BED8
		protected IExtractionProgram<TRegion> LoadProgramVersion00(XElement progElement, Grammar grammar, Func<ProgramNode, ExtractionKind, ReferenceKind, double?, IExtractionProgram<TRegion>> createProgram, ASTSerializationFormat serializationFormat, DeserializationContext context, ProgramNodeParser programNodeParser)
		{
			ExtractionKind extractionKind;
			if (progElement.Name == "SequenceProgram")
			{
				extractionKind = ExtractionKind.Sequence;
			}
			else
			{
				if (!(progElement.Name == "SubstringProgram"))
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text! Unknown kind: {0}", new object[] { progElement.Name })));
				}
				extractionKind = ExtractionKind.Region;
			}
			XElement xelement = ProgramVersion00Converter.Convert(progElement);
			ParseSettings parseSettings = new ParseSettings(context, null);
			double num;
			ProgramNode programNode = this.ParseProgramNode(xelement, grammar, serializationFormat, parseSettings, programNodeParser, out num);
			return createProgram(programNode, extractionKind, ReferenceKind.Parent, new double?(num));
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000DD70 File Offset: 0x0000BF70
		protected IExtractionProgram<TRegion> LoadProgramVersion01(XElement progElement, Grammar grammar, Func<ProgramNode, ExtractionKind, ReferenceKind, double?, IExtractionProgram<TRegion>> createProgram, ASTSerializationFormat serializationFormat, DeserializationContext context, ProgramNodeParser programNodeParser)
		{
			ExtractionKind extractionKind;
			try
			{
				extractionKind = (ExtractionKind)Enum.Parse(typeof(ExtractionKind), progElement.Name.ToString());
			}
			catch (ArgumentException)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text! Unknown program kind: {0}", new object[] { progElement.Name })));
			}
			XAttribute xattribute = progElement.Attribute("refkind");
			if (xattribute == null)
			{
				throw new InvalidOperationException("Invalid program text! No reference kind");
			}
			ReferenceKind referenceKind;
			try
			{
				referenceKind = (ReferenceKind)Enum.Parse(typeof(ReferenceKind), xattribute.Value);
			}
			catch (ArgumentException)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text! Unknown reference kind: {0}", new object[] { xattribute.Value })));
			}
			ParseSettings parseSettings = new ParseSettings(context, null);
			double num;
			ProgramNode programNode = this.ParseProgramNode(progElement, grammar, serializationFormat, parseSettings, programNodeParser, out num);
			return createProgram(programNode, extractionKind, referenceKind, new double?(num));
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000DE70 File Offset: 0x0000C070
		protected string ConvertProgramFrom00To01(XElement progElement, Version to)
		{
			ExtractionKind extractionKind;
			if (progElement.Name == "SequenceProgram")
			{
				extractionKind = ExtractionKind.Sequence;
			}
			else
			{
				if (!(progElement.Name == "SubstringProgram"))
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program text! Unknown kind: {0}", new object[] { progElement.Name })));
				}
				extractionKind = ExtractionKind.Region;
			}
			progElement.Name = extractionKind.ToString();
			progElement.SetAttributeValue("version", to);
			progElement.SetAttributeValue("refkind", ReferenceKind.Parent);
			return progElement.ToString();
		}

		// Token: 0x020000A3 RID: 163
		// (Invoke) Token: 0x060003E6 RID: 998
		protected delegate IExtractionProgram<TRegion> VersionParser(XElement progElement, Grammar grammar, Func<ProgramNode, ExtractionKind, ReferenceKind, double?, IExtractionProgram<TRegion>> createProg, ASTSerializationFormat serializationFormat, DeserializationContext context, ProgramNodeParser parser);

		// Token: 0x020000A4 RID: 164
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040001B7 RID: 439
			public static ProgramNodeParser <0>__Parse;
		}
	}
}
