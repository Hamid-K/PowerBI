using System;
using dotless.Core.configuration;
using dotless.Core.Exceptions;
using dotless.Core.Importers;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Tree;
using dotless.Core.Stylizers;

namespace dotless.Core.Parser
{
	// Token: 0x02000023 RID: 35
	public class Parser
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000044EE File Offset: 0x000026EE
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x000044F6 File Offset: 0x000026F6
		public Tokenizer Tokenizer { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000044FF File Offset: 0x000026FF
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004507 File Offset: 0x00002707
		public IStylizer Stylizer { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004510 File Offset: 0x00002710
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004518 File Offset: 0x00002718
		public string FileName { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004521 File Offset: 0x00002721
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004529 File Offset: 0x00002729
		public bool Debug { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004532 File Offset: 0x00002732
		// (set) Token: 0x060000DC RID: 220 RVA: 0x0000453F File Offset: 0x0000273F
		public string CurrentDirectory
		{
			get
			{
				return this.Importer.CurrentDirectory;
			}
			set
			{
				this.Importer.CurrentDirectory = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004550 File Offset: 0x00002750
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00004575 File Offset: 0x00002775
		public INodeProvider NodeProvider
		{
			get
			{
				INodeProvider nodeProvider;
				if ((nodeProvider = this._nodeProvider) == null)
				{
					nodeProvider = (this._nodeProvider = new DefaultNodeProvider());
				}
				return nodeProvider;
			}
			set
			{
				this._nodeProvider = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000457E File Offset: 0x0000277E
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004586 File Offset: 0x00002786
		public IImporter Importer
		{
			get
			{
				return this._importer;
			}
			set
			{
				this._importer = value;
				this._importer.Parser = () => new Parser(this.Tokenizer.Optimization, this.Stylizer, this._importer)
				{
					NodeProvider = this.NodeProvider,
					Debug = this.Debug,
					CurrentDirectory = this.CurrentDirectory,
					StrictMath = this.StrictMath
				};
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000045A6 File Offset: 0x000027A6
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000045AE File Offset: 0x000027AE
		public bool StrictMath { get; set; }

		// Token: 0x060000E3 RID: 227 RVA: 0x000045B7 File Offset: 0x000027B7
		public Parser()
			: this(1, false)
		{
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000045C1 File Offset: 0x000027C1
		public Parser(bool debug)
			: this(1, debug)
		{
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000045CB File Offset: 0x000027CB
		public Parser(int optimization)
			: this(optimization, new PlainStylizer(), new Importer(), false)
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000045DF File Offset: 0x000027DF
		public Parser(int optimization, bool debug)
			: this(optimization, new PlainStylizer(), new Importer(), debug)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000045F3 File Offset: 0x000027F3
		public Parser(IStylizer stylizer, IImporter importer)
			: this(1, stylizer, importer, false)
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000045FF File Offset: 0x000027FF
		public Parser(IStylizer stylizer, IImporter importer, bool debug)
			: this(1, stylizer, importer, debug)
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000460B File Offset: 0x0000280B
		public Parser(int optimization, IStylizer stylizer, IImporter importer)
			: this(optimization, stylizer, importer, false)
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004617 File Offset: 0x00002817
		public Parser(int optimization, IStylizer stylizer, IImporter importer, bool debug)
		{
			this.Stylizer = stylizer;
			this.Importer = importer;
			this.Debug = debug;
			this.Tokenizer = new Tokenizer(optimization);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004641 File Offset: 0x00002841
		public Parser(DotlessConfiguration config, IStylizer stylizer, IImporter importer)
			: this(config.Optimization, stylizer, importer, config.Debug)
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004658 File Offset: 0x00002858
		public Ruleset Parse(string input, string fileName)
		{
			this.FileName = fileName;
			Ruleset ruleset;
			try
			{
				this.Tokenizer.SetupInput(input, fileName);
				ruleset = new Root(new Parsers(this.NodeProvider).Primary(this), new Func<ParsingException, ParserException>(this.GenerateParserError));
			}
			catch (ParsingException ex)
			{
				throw this.GenerateParserError(ex);
			}
			if (!this.Tokenizer.HasCompletedParsing())
			{
				throw this.GenerateParserError(new ParsingException("Content after finishing parsing (missing opening bracket?)", this.Tokenizer.GetNodeLocation(this.Tokenizer.Location.Index)));
			}
			return ruleset;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000046F4 File Offset: 0x000028F4
		private ParserException GenerateParserError(ParsingException parsingException)
		{
			NodeLocation location = parsingException.Location;
			string message = parsingException.Message;
			NodeLocation callLocation = parsingException.CallLocation;
			Zone zone = new Zone(location, message, (callLocation != null) ? new Zone(callLocation) : null);
			return new ParserException(this.Stylizer.Stylize(zone), parsingException, zone);
		}

		// Token: 0x0400002B RID: 43
		private INodeProvider _nodeProvider;

		// Token: 0x0400002C RID: 44
		private IImporter _importer;

		// Token: 0x0400002E RID: 46
		private const int defaultOptimization = 1;

		// Token: 0x0400002F RID: 47
		private const bool defaultDebug = false;
	}
}
