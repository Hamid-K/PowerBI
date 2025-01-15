using System;
using System.Collections.Generic;
using dotless.Core.configuration;
using dotless.Core.Exceptions;
using dotless.Core.Loggers;
using dotless.Core.Parser;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Tree;
using dotless.Core.Plugins;
using dotless.Core.Stylizers;

namespace dotless.Core
{
	// Token: 0x02000007 RID: 7
	public class LessEngine : ILessEngine
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002489 File Offset: 0x00000689
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002491 File Offset: 0x00000691
		public Parser Parser { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000249A File Offset: 0x0000069A
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000024A2 File Offset: 0x000006A2
		public ILogger Logger { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000024AB File Offset: 0x000006AB
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000024B3 File Offset: 0x000006B3
		public bool Compress { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000024BC File Offset: 0x000006BC
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000024C4 File Offset: 0x000006C4
		public bool Debug { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000024CD File Offset: 0x000006CD
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000024D5 File Offset: 0x000006D5
		[Obsolete("The Variable Redefines feature has been removed to align with less.js")]
		public bool DisableVariableRedefines { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000024DE File Offset: 0x000006DE
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000024E6 File Offset: 0x000006E6
		[Obsolete("The Color Compression feature has been removed to align with less.js")]
		public bool DisableColorCompression { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000024EF File Offset: 0x000006EF
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000024F7 File Offset: 0x000006F7
		public bool KeepFirstSpecialComment { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002500 File Offset: 0x00000700
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002508 File Offset: 0x00000708
		public bool StrictMath { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002511 File Offset: 0x00000711
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002519 File Offset: 0x00000719
		public Env Env { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002522 File Offset: 0x00000722
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000252A File Offset: 0x0000072A
		public IEnumerable<IPluginConfigurator> Plugins { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002533 File Offset: 0x00000733
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000253B File Offset: 0x0000073B
		public bool LastTransformationSuccessful { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002544 File Offset: 0x00000744
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002551 File Offset: 0x00000751
		public string CurrentDirectory
		{
			get
			{
				return this.Parser.CurrentDirectory;
			}
			set
			{
				this.Parser.CurrentDirectory = value;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002560 File Offset: 0x00000760
		public LessEngine(Parser parser, ILogger logger, DotlessConfiguration config)
			: this(parser, logger, config.MinifyOutput, config.Debug, config.DisableVariableRedefines, config.DisableColorCompression, config.KeepFirstSpecialComment, config.Plugins)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002599 File Offset: 0x00000799
		public LessEngine(Parser parser, ILogger logger, bool compress, bool debug, bool disableVariableRedefines, bool disableColorCompression, bool keepFirstSpecialComment, bool strictMath, IEnumerable<IPluginConfigurator> plugins)
		{
			this.Parser = parser;
			this.Logger = logger;
			this.Compress = compress;
			this.Debug = debug;
			this.Plugins = plugins;
			this.KeepFirstSpecialComment = keepFirstSpecialComment;
			this.StrictMath = strictMath;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000025D8 File Offset: 0x000007D8
		public LessEngine(Parser parser, ILogger logger, bool compress, bool debug, bool disableVariableRedefines, bool disableColorCompression, bool keepFirstSpecialComment, IEnumerable<IPluginConfigurator> plugins)
			: this(parser, logger, compress, debug, disableVariableRedefines, disableColorCompression, keepFirstSpecialComment, false, plugins)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000025FC File Offset: 0x000007FC
		public LessEngine(Parser parser, ILogger logger, bool compress, bool debug)
			: this(parser, logger, compress, debug, false, false, false, null)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002618 File Offset: 0x00000818
		public LessEngine(Parser parser, ILogger logger, bool compress, bool debug, bool disableVariableRedefines)
			: this(parser, logger, compress, debug, disableVariableRedefines, false, false, null)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002638 File Offset: 0x00000838
		public LessEngine(Parser parser)
			: this(parser, new ConsoleLogger(LogLevel.Error), false, false, false, false, false, null)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002658 File Offset: 0x00000858
		public LessEngine()
			: this(new Parser())
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002668 File Offset: 0x00000868
		public string TransformToCss(string source, string fileName)
		{
			try
			{
				LessEngine.<>c__DisplayClass54_0 CS$<>8__locals1 = new LessEngine.<>c__DisplayClass54_0();
				CS$<>8__locals1.<>4__this = this;
				this.Parser.StrictMath = this.StrictMath;
				Ruleset ruleset = this.Parser.Parse(source, fileName);
				LessEngine.<>c__DisplayClass54_0 CS$<>8__locals2 = CS$<>8__locals1;
				Env env;
				if ((env = this.Env) == null)
				{
					Env env2 = new Env(this.Parser);
					env2.Compress = this.Compress;
					env2.Debug = this.Debug;
					env = env2;
					env2.KeepFirstSpecialComment = this.KeepFirstSpecialComment;
				}
				CS$<>8__locals2.env = env;
				if (this.Plugins != null)
				{
					foreach (IPluginConfigurator pluginConfigurator in this.Plugins)
					{
						CS$<>8__locals1.env.AddPlugin(pluginConfigurator.CreatePlugin());
					}
				}
				string text = ruleset.ToCSS(CS$<>8__locals1.env);
				CS$<>8__locals1.stylizer = new PlainStylizer();
				foreach (Extender extender in CS$<>8__locals1.env.FindUnmatchedExtensions())
				{
					this.Logger.Warn("Warning: extend '{0}' has no matches {1}\n", new object[]
					{
						extender.BaseSelector.ToCSS(CS$<>8__locals1.env).Trim(),
						CS$<>8__locals1.stylizer.Stylize(new Zone(extender.Extend.Location)).Trim()
					});
				}
				ruleset.Accept(DelegateVisitor.For<Media>(delegate(Media m)
				{
					foreach (Extender extender2 in m.FindUnmatchedExtensions())
					{
						CS$<>8__locals1.<>4__this.Logger.Warn("Warning: extend '{0}' has no matches {1}\n", new object[]
						{
							extender2.BaseSelector.ToCSS(CS$<>8__locals1.env).Trim(),
							CS$<>8__locals1.stylizer.Stylize(new Zone(extender2.Extend.Location)).Trim()
						});
					}
				}));
				this.LastTransformationSuccessful = true;
				return text;
			}
			catch (ParserException ex)
			{
				this.LastTransformationSuccessful = false;
				this.LastTransformationError = ex;
				this.Logger.Error(ex.Message);
			}
			return "";
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002860 File Offset: 0x00000A60
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002868 File Offset: 0x00000A68
		public ParserException LastTransformationError { get; set; }

		// Token: 0x0600004C RID: 76 RVA: 0x00002871 File Offset: 0x00000A71
		public IEnumerable<string> GetImports()
		{
			return this.Parser.Importer.GetImports();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002883 File Offset: 0x00000A83
		public void ResetImports()
		{
			this.Parser.Importer.ResetImports();
		}
	}
}
