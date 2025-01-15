using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200039B RID: 923
	public sealed class TextLoader : IDataLoader, IDataView, ISchematized, ICanSaveModel
	{
		// Token: 0x060013CE RID: 5070 RVA: 0x00070C62 File Offset: 0x0006EE62
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("TXTLOADR", 65547U, 65546U, 65545U, "TextLoader", null);
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x00070C83 File Offset: 0x0006EE83
		private bool HasHeader
		{
			get
			{
				return (this._flags & TextLoader.Options.HasHeader) != (TextLoader.Options)0U;
			}
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00070C94 File Offset: 0x0006EE94
		public TextLoader(TextLoader.Arguments args, IHostEnvironment env, IMultiStreamSource files)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("TextLoader");
			Contracts.CheckValue<TextLoader.Arguments>(this._host, args, "args");
			Contracts.CheckValue<IMultiStreamSource>(this._host, files, "files");
			this._files = files;
			IMultiStreamSource multiStreamSource = null;
			if (!string.IsNullOrWhiteSpace(args.headerFile))
			{
				multiStreamSource = new MultiFileSource(args.headerFile);
			}
			TextLoader.Column[] array = args.column;
			bool flag;
			if (Utils.Size<TextLoader.Column>(array) == 0 && !TextLoader.TryParseSchema(this._host, multiStreamSource ?? this._files, ref args, out array, out flag))
			{
				if (flag)
				{
					throw Contracts.Except(this._host, "TextLoader options embedded in the file are invalid");
				}
				array = new TextLoader.Column[]
				{
					TextLoader.Column.Parse("Label:0"),
					TextLoader.Column.Parse("Features:1-*")
				};
			}
			this._useThreads = args.useThreads;
			if (args.trimWhitespace)
			{
				this._flags |= TextLoader.Options.TrimWhitespace;
			}
			if (multiStreamSource == null && args.hasHeader)
			{
				this._flags |= TextLoader.Options.HasHeader;
			}
			if (args.allowQuoting)
			{
				this._flags |= TextLoader.Options.AllowQuoting;
			}
			if (args.allowSparse)
			{
				this._flags |= TextLoader.Options.AllowSparse;
			}
			this._maxRows = args.maxRows ?? long.MaxValue;
			Contracts.CheckUserArg(this._host, this._maxRows >= 0L, "maxRows");
			this._inputSize = args.inputSize ?? 0;
			Contracts.Check(this._host, this._inputSize >= 0, "inputSize");
			if (this._inputSize >= 2147483647)
			{
				this._inputSize = 2147483646;
			}
			Contracts.CheckNonEmpty(this._host, args.separator, "separator", "Must specify a separator");
			string text = args.separator.ToLowerInvariant();
			if (text == ",")
			{
				this._separators = new char[] { ',' };
			}
			else
			{
				HashSet<char> hashSet = new HashSet<char>();
				foreach (string text2 in text.Split(new char[] { ',' }))
				{
					if (!string.IsNullOrEmpty(text2))
					{
						char c = this.NormalizeSeparator(text2);
						hashSet.Add(c);
					}
				}
				this._separators = hashSet.ToArray<char>();
				if (this._separators.Length == 0)
				{
					this._separators = new char[] { ',' };
				}
			}
			this._bindings = new TextLoader.Bindings(this, array, multiStreamSource);
			this._parser = new TextLoader.Parser(this);
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00070F58 File Offset: 0x0006F158
		private char NormalizeSeparator(string sep)
		{
			switch (sep)
			{
			case "space":
			case " ":
				return ' ';
			case "tab":
			case "\t":
				return '\t';
			case "comma":
			case ",":
				return ',';
			case "colon":
			case ":":
				Contracts.CheckUserArg(this._host, (this._flags & TextLoader.Options.AllowSparse) == (TextLoader.Options)0U, "separator", "When the separator is colon, turn off allowSparse");
				return ':';
			case "semicolon":
			case ";":
				return ';';
			case "bar":
			case "|":
				return '|';
			}
			char c = sep[0];
			if (sep.Length != 1 || c < ' ' || ('0' <= c && c <= '9') || c == '"')
			{
				throw Contracts.ExceptUserArg(this._host, "separator", "Illegal separator: '{0}'", new object[] { sep });
			}
			return sep[0];
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00071108 File Offset: 0x0006F308
		private static bool TryParseSchema(IHost host, IMultiStreamSource files, ref TextLoader.Arguments args, out TextLoader.Column[] cols, out bool error)
		{
			cols = null;
			error = false;
			string settings = CmdParser.GetSettings(args, new TextLoader.ArgumentsCore(), 3);
			string embeddedArgs = TextLoader.Cursor.GetEmbeddedArgs(files);
			if (string.IsNullOrWhiteSpace(embeddedArgs))
			{
				return false;
			}
			bool flag;
			using (IChannel ch = host.Start("Parsing options from file"))
			{
				if (!string.IsNullOrWhiteSpace(settings))
				{
					ch.Warning("Arguments cannot be embedded in the file and in the command line. The embedded arguments will be ignored");
					flag = false;
				}
				else
				{
					error = true;
					TextLoader.LoaderHolder loaderHolder = new TextLoader.LoaderHolder();
					if (CmdParser.ParseArguments("loader = " + embeddedArgs, loaderHolder, delegate(string msg)
					{
						ch.Error(msg);
					}) && loaderHolder.loader != null && !string.IsNullOrWhiteSpace(loaderHolder.loader.Kind))
					{
						ComponentCatalog.LoadableClassInfo loadableClassInfo = ComponentCatalog.GetLoadableClassInfo<SignatureDataLoader>(loaderHolder.loader.Kind);
						if (!(loadableClassInfo.Type != typeof(TextLoader)) && !(loadableClassInfo.ArgType != typeof(TextLoader.Arguments)))
						{
							TextLoader.Arguments arguments = new TextLoader.Arguments();
							CmdParser.ParseArguments(CmdParser.GetSettings(args, new TextLoader.Arguments(), 3), arguments);
							if (CmdParser.ParseArguments(loaderHolder.loader.SubComponentSettings, arguments, typeof(TextLoader.ArgumentsCore), delegate(string msg)
							{
								ch.Error(msg);
							}))
							{
								cols = arguments.column;
								if (Utils.Size<TextLoader.Column>(cols) != 0)
								{
									error = false;
									args = arguments;
								}
							}
						}
					}
					ch.Done();
					flag = !error;
				}
			}
			return flag;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x000712C8 File Offset: 0x0006F4C8
		public static bool FileContainsValidSchema(IHostEnvironment env, IMultiStreamSource files, out TextLoader.Arguments args)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("TextLoader");
			Contracts.CheckValue<IMultiStreamSource>(host, files, "files");
			args = new TextLoader.Arguments();
			TextLoader.Column[] array;
			bool flag2;
			bool flag = TextLoader.TryParseSchema(host, files, ref args, out array, out flag2);
			return flag && !flag2 && args.IsValid();
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0007131C File Offset: 0x0006F51C
		private TextLoader(ModelLoadContext ctx, IHost host, IMultiStreamSource files)
		{
			this._host = host;
			this._files = files;
			this._useThreads = true;
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(host, num == 4);
			this._maxRows = ctx.Reader.ReadInt64();
			Contracts.CheckDecode(host, this._maxRows > 0L);
			this._flags = (TextLoader.Options)ctx.Reader.ReadUInt32();
			Contracts.CheckDecode(host, (this._flags & ~(TextLoader.Options.TrimWhitespace | TextLoader.Options.HasHeader | TextLoader.Options.AllowQuoting | TextLoader.Options.AllowSparse)) == (TextLoader.Options)0U, "Illegal option flag values");
			this._inputSize = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(host, 0 <= this._inputSize && this._inputSize < int.MaxValue);
			this._separators = Utils.ReadCharArray(ctx.Reader);
			Contracts.CheckDecode(host, Utils.Size<char>(this._separators) > 0);
			string text = "\0\r\n\"0123456789";
			foreach (char c in this._separators)
			{
				if (text.IndexOf(c) >= 0)
				{
					throw Contracts.ExceptDecode(host, "Illegal separator");
				}
			}
			if (this._separators.Contains(':'))
			{
				Contracts.CheckDecode(host, (this._flags & TextLoader.Options.AllowSparse) == (TextLoader.Options)0U, "When the separator is colon, turn off allowSparse");
			}
			this._bindings = new TextLoader.Bindings(ctx, this);
			this._parser = new TextLoader.Parser(this);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00071494 File Offset: 0x0006F694
		public static TextLoader Create(ModelLoadContext ctx, IHostEnvironment env, IMultiStreamSource files)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("TextLoader");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(TextLoader.GetVersionInfo());
			Contracts.CheckValue<IMultiStreamSource>(h, files, "files");
			return HostExtensions.Apply<TextLoader>(h, "Loading Model", (IChannel ch) => new TextLoader(ctx, h, files));
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0007152C File Offset: 0x0006F72C
		public void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(TextLoader.GetVersionInfo());
			ctx.Writer.Write(4);
			ctx.Writer.Write(this._maxRows);
			ctx.Writer.Write((uint)this._flags);
			ctx.Writer.Write(this._inputSize);
			Utils.WriteCharArray(ctx.Writer, this._separators);
			this._bindings.Save(ctx);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x000715B8 File Offset: 0x0006F7B8
		public long? GetRowCount(bool lazy = true)
		{
			return null;
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x000715CE File Offset: 0x0006F7CE
		public bool CanShuffle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x000715D1 File Offset: 0x0006F7D1
		public ISchema Schema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x000715DC File Offset: 0x0006F7DC
		public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			bool[] array = Utils.BuildArray<bool>(this._bindings.ColumnCount, predicate);
			return TextLoader.Cursor.Create(this, array);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00071614 File Offset: 0x0006F814
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			bool[] array = Utils.BuildArray<bool>(this._bindings.ColumnCount, predicate);
			return TextLoader.Cursor.CreateSet(out consolidator, this, array, n);
		}

		// Token: 0x04000B85 RID: 2949
		internal const string Summary = "Loads text data file.";

		// Token: 0x04000B86 RID: 2950
		public const string LoaderSignature = "TextLoader";

		// Token: 0x04000B87 RID: 2951
		private const uint verForceVectorSupported = 65546U;

		// Token: 0x04000B88 RID: 2952
		private const int SrcLim = 2147483647;

		// Token: 0x04000B89 RID: 2953
		private const string RegistrationName = "TextLoader";

		// Token: 0x04000B8A RID: 2954
		private readonly bool _useThreads;

		// Token: 0x04000B8B RID: 2955
		private readonly TextLoader.Options _flags;

		// Token: 0x04000B8C RID: 2956
		private readonly long _maxRows;

		// Token: 0x04000B8D RID: 2957
		private readonly int _inputSize;

		// Token: 0x04000B8E RID: 2958
		private readonly char[] _separators;

		// Token: 0x04000B8F RID: 2959
		private readonly TextLoader.Bindings _bindings;

		// Token: 0x04000B90 RID: 2960
		private readonly IMultiStreamSource _files;

		// Token: 0x04000B91 RID: 2961
		private readonly TextLoader.Parser _parser;

		// Token: 0x04000B92 RID: 2962
		private readonly IHost _host;

		// Token: 0x0200039C RID: 924
		public sealed class Column
		{
			// Token: 0x060013DC RID: 5084 RVA: 0x00071650 File Offset: 0x0006F850
			public static TextLoader.Column Parse(string str)
			{
				TextLoader.Column column = new TextLoader.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x060013DD RID: 5085 RVA: 0x00071670 File Offset: 0x0006F870
			private bool TryParse(string str)
			{
				string[] array = str.Split(new char[] { ':' });
				if (array.Length < 2 || array.Length > 3)
				{
					return false;
				}
				int num = 0;
				if (string.IsNullOrWhiteSpace(this.name = array[num++]))
				{
					return false;
				}
				if (array.Length == 3)
				{
					DataKind dataKind;
					if (!TypeParsingUtils.TryParseDataKind(array[num++], out dataKind, out this.keyRange))
					{
						return false;
					}
					this.type = ((dataKind == null) ? null : new DataKind?(dataKind));
				}
				return this.TryParseSource(array[num++]);
			}

			// Token: 0x060013DE RID: 5086 RVA: 0x00071704 File Offset: 0x0006F904
			private bool TryParseSource(string str)
			{
				string[] array = str.Split(new char[] { ',' });
				if (str.Length == 0)
				{
					return false;
				}
				this.source = new TextLoader.Range[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					if ((this.source[i] = TextLoader.Range.Parse(array[i])) == null)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060013DF RID: 5087 RVA: 0x00071764 File Offset: 0x0006F964
			public bool TryUnparse(StringBuilder sb)
			{
				if (string.IsNullOrWhiteSpace(this.name))
				{
					return false;
				}
				if (CmdQuoter.NeedsQuoting(this.name))
				{
					return false;
				}
				if (Utils.Size<TextLoader.Range>(this.source) == 0)
				{
					return false;
				}
				int length = sb.Length;
				sb.Append(this.name);
				sb.Append(':');
				if (this.type != null || this.keyRange != null)
				{
					if (this.type != null)
					{
						sb.Append(DataKindExtensions.GetString(this.type.Value));
					}
					if (this.keyRange != null)
					{
						sb.Append('[');
						if (!this.keyRange.TryUnparse(sb))
						{
							sb.Length = length;
							return false;
						}
						sb.Append(']');
					}
					sb.Append(':');
				}
				string text = "";
				foreach (TextLoader.Range range in this.source)
				{
					sb.Append(text);
					if (!range.TryUnparse(sb))
					{
						sb.Length = length;
						return false;
					}
					text = ",";
				}
				return true;
			}

			// Token: 0x060013E0 RID: 5088 RVA: 0x00071884 File Offset: 0x0006FA84
			public bool IsValid()
			{
				if (Utils.Size<TextLoader.Range>(this.source) == 0)
				{
					return false;
				}
				List<TextLoader.Range> list = this.source.OrderBy((TextLoader.Range x) => x.min).ToList<TextLoader.Range>();
				TextLoader.Range range = list[0];
				if (range.min < 0 || range.min > range.max)
				{
					return false;
				}
				for (int i = 1; i < list.Count; i++)
				{
					TextLoader.Range range2 = list[i];
					if (range2.min > range2.max)
					{
						return false;
					}
					TextLoader.Range range3 = list[i - 1];
					if (range3.max == null && (range3.autoEnd || range3.variableEnd))
					{
						return false;
					}
					if (range2.min <= range3.max)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04000B93 RID: 2963
			[Argument(0, HelpText = "Name of the column")]
			public string name;

			// Token: 0x04000B94 RID: 2964
			[Argument(0, HelpText = "Type of the items in the column")]
			public DataKind? type;

			// Token: 0x04000B95 RID: 2965
			[Argument(4, HelpText = "Source index range(s) of the column", ShortName = "src")]
			public TextLoader.Range[] source;

			// Token: 0x04000B96 RID: 2966
			[Argument(4, HelpText = "For a key column, this defines the range of values", ShortName = "key")]
			public KeyRange keyRange;
		}

		// Token: 0x0200039D RID: 925
		public sealed class Range
		{
			// Token: 0x060013E3 RID: 5091 RVA: 0x000719B0 File Offset: 0x0006FBB0
			public static TextLoader.Range Parse(string str)
			{
				TextLoader.Range range = new TextLoader.Range();
				if (range.TryParse(str))
				{
					return range;
				}
				return null;
			}

			// Token: 0x060013E4 RID: 5092 RVA: 0x000719D0 File Offset: 0x0006FBD0
			private bool TryParse(string str)
			{
				int num = str.IndexOfAny(new char[] { '-', '~' });
				if (num < 0)
				{
					if (!int.TryParse(str, out this.min))
					{
						return false;
					}
					this.max = new int?(this.min);
					return true;
				}
				else
				{
					this.allOther = str[num] == '~';
					this.forceVector = true;
					if (num == 0)
					{
						if (!this.allOther)
						{
							return false;
						}
						this.min = 0;
					}
					else if (!int.TryParse(str.Substring(0, num), out this.min))
					{
						return false;
					}
					string text = str.Substring(num + 1);
					if (string.IsNullOrEmpty(text) || text == "*")
					{
						this.autoEnd = true;
						return true;
					}
					if (text == "**")
					{
						this.variableEnd = true;
						return true;
					}
					int num2;
					if (!int.TryParse(text, out num2))
					{
						return false;
					}
					this.max = new int?(num2);
					return true;
				}
			}

			// Token: 0x060013E5 RID: 5093 RVA: 0x00071ABC File Offset: 0x0006FCBC
			public bool TryUnparse(StringBuilder sb)
			{
				char c = (this.allOther ? '~' : '-');
				if (this.min < 0)
				{
					return false;
				}
				sb.Append(this.min);
				if (this.max != null)
				{
					if (this.max != this.min || this.forceVector || this.allOther)
					{
						sb.Append(c).Append(this.max);
					}
				}
				else if (this.autoEnd)
				{
					sb.Append(c).Append("*");
				}
				else if (this.variableEnd)
				{
					sb.Append(c).Append("**");
				}
				return true;
			}

			// Token: 0x04000B98 RID: 2968
			[Argument(1, HelpText = "First index in the range")]
			public int min;

			// Token: 0x04000B99 RID: 2969
			[Argument(0, HelpText = "Last index in the range")]
			public int? max;

			// Token: 0x04000B9A RID: 2970
			[Argument(0, HelpText = "This range extends to the end of the line, but should be a fixed number of items", ShortName = "auto")]
			public bool autoEnd;

			// Token: 0x04000B9B RID: 2971
			[Argument(0, HelpText = "This range extends to the end of the line, which can vary from line to line", ShortName = "var")]
			public bool variableEnd;

			// Token: 0x04000B9C RID: 2972
			[Argument(0, HelpText = "This range includes only other indices not specified", ShortName = "other")]
			public bool allOther;

			// Token: 0x04000B9D RID: 2973
			[Argument(0, HelpText = "Force scalar columns to be treated as vectors of length one", ShortName = "vector")]
			public bool forceVector;
		}

		// Token: 0x0200039E RID: 926
		public class ArgumentsCore
		{
			// Token: 0x060013E7 RID: 5095 RVA: 0x00071B95 File Offset: 0x0006FD95
			public bool IsValid()
			{
				if (Utils.Size<TextLoader.Column>(this.column) != 0)
				{
					return this.column.All((TextLoader.Column x) => x.IsValid());
				}
				return true;
			}

			// Token: 0x04000B9E RID: 2974
			[Argument(0, HelpText = "Whether the input may include quoted values, which can contain separator characters, colons, and distinguish empty values from missing values. When true, consecutive separators denote a missing value and an empty value is denoted by \"\". When false, consecutive separators denote an empty value.", ShortName = "quote")]
			public bool allowQuoting = true;

			// Token: 0x04000B9F RID: 2975
			[Argument(0, HelpText = "Whether the input may include sparse representations", ShortName = "sparse")]
			public bool allowSparse = true;

			// Token: 0x04000BA0 RID: 2976
			[Argument(0, HelpText = "Number of source columns in the text data. Default is that sparse rows contain their size information.", ShortName = "size")]
			public int? inputSize;

			// Token: 0x04000BA1 RID: 2977
			[Argument(0, HelpText = "Source column separator. Options: tab, space, comma, single character", ShortName = "sep")]
			public string separator = "tab";

			// Token: 0x04000BA2 RID: 2978
			[Argument(4, HelpText = "Column groups. Each group is specified as name:type:numeric-ranges, eg, col=Features:R4:1-17,26,35-40", ShortName = "col", SortOrder = 1)]
			public TextLoader.Column[] column;

			// Token: 0x04000BA3 RID: 2979
			[Argument(0, HelpText = "Remove trailing whitespace from lines", ShortName = "trim")]
			public bool trimWhitespace;

			// Token: 0x04000BA4 RID: 2980
			[Argument(0, ShortName = "header", HelpText = "Data file has header with feature names. Header is read only if options 'hs' and 'hf' are not specified.")]
			public bool hasHeader;
		}

		// Token: 0x0200039F RID: 927
		public sealed class Arguments : TextLoader.ArgumentsCore
		{
			// Token: 0x04000BA6 RID: 2982
			[Argument(0, HelpText = "Use separate parsing threads?", ShortName = "threads", Hide = true)]
			public bool useThreads = true;

			// Token: 0x04000BA7 RID: 2983
			[Argument(0, HelpText = "File containing a header with feature names. If specified, header defined in the data file (header+) is ignored.", ShortName = "hf", IsInputFileName = true)]
			public string headerFile;

			// Token: 0x04000BA8 RID: 2984
			[Argument(0, HelpText = "Maximum number of rows to produce", ShortName = "rows", Hide = true)]
			public long? maxRows;
		}

		// Token: 0x020003A0 RID: 928
		private struct Segment
		{
			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x060013EB RID: 5099 RVA: 0x00071BFE File Offset: 0x0006FDFE
			public bool IsVariable
			{
				get
				{
					return this.Lim == int.MaxValue;
				}
			}

			// Token: 0x060013EC RID: 5100 RVA: 0x00071C0D File Offset: 0x0006FE0D
			public Segment(int min, int lim, bool forceVector)
			{
				this.Min = min;
				this.Lim = lim;
				this.ForceVector = forceVector;
			}

			// Token: 0x060013ED RID: 5101 RVA: 0x00071C24 File Offset: 0x0006FE24
			public Segment(int min)
			{
				this.Min = min;
				this.Lim = int.MaxValue;
				this.ForceVector = true;
			}

			// Token: 0x04000BA9 RID: 2985
			public int Min;

			// Token: 0x04000BAA RID: 2986
			public int Lim;

			// Token: 0x04000BAB RID: 2987
			public bool ForceVector;
		}

		// Token: 0x020003A1 RID: 929
		private sealed class ColInfo
		{
			// Token: 0x060013EE RID: 5102 RVA: 0x00071C3F File Offset: 0x0006FE3F
			private ColInfo(string name, ColumnType colType, TextLoader.Segment[] segs, int isegVar, int sizeBase)
			{
				this.Name = name;
				this.Kind = colType.ItemType.RawKind;
				this.ColType = colType;
				this.Segments = segs;
				this.SizeBase = sizeBase;
				this.IsegVariable = isegVar;
			}

			// Token: 0x060013EF RID: 5103 RVA: 0x00071CB0 File Offset: 0x0006FEB0
			public static TextLoader.ColInfo Create(string name, PrimitiveType itemType, TextLoader.Segment[] segs, bool user)
			{
				int[] identityPermutation = Utils.GetIdentityPermutation(segs.Length);
				Array.Sort<int>(identityPermutation, (int x, int y) => segs[x].Min.CompareTo(segs[y].Min));
				for (int i = 1; i < identityPermutation.Length; i++)
				{
					int num = identityPermutation[i - 1];
					int num2 = identityPermutation[i];
					if (segs[num].Lim > segs[num2].Min)
					{
						throw user ? Contracts.ExceptUserArg("src", "Intervals specified for column '{0}' overlap", new object[] { name }) : Contracts.ExceptDecode("Intervals specified for column '{0}' overlap", new object[] { name });
					}
				}
				int num3 = -1;
				int num4 = 0;
				for (int j = 0; j < segs.Length; j++)
				{
					TextLoader.Segment segment = segs[j];
					if (segment.IsVariable)
					{
						num3 = j;
					}
					else
					{
						num4 += segment.Lim - segment.Min;
					}
				}
				ColumnType columnType = itemType;
				if (num3 >= 0)
				{
					columnType = new VectorType(itemType, 0);
				}
				else if (num4 > 1 || segs[0].ForceVector)
				{
					columnType = new VectorType(itemType, num4);
				}
				return new TextLoader.ColInfo(name, columnType, segs, num3, num4);
			}

			// Token: 0x04000BAC RID: 2988
			public readonly string Name;

			// Token: 0x04000BAD RID: 2989
			public readonly DataKind Kind;

			// Token: 0x04000BAE RID: 2990
			public readonly ColumnType ColType;

			// Token: 0x04000BAF RID: 2991
			public readonly TextLoader.Segment[] Segments;

			// Token: 0x04000BB0 RID: 2992
			public readonly int IsegVariable;

			// Token: 0x04000BB1 RID: 2993
			public readonly int SizeBase;
		}

		// Token: 0x020003A2 RID: 930
		private sealed class Bindings : ISchema
		{
			// Token: 0x060013F0 RID: 5104 RVA: 0x00071E08 File Offset: 0x00070008
			private Bindings()
			{
				this._getSlotNames = new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.GetSlotNames);
			}

			// Token: 0x060013F1 RID: 5105 RVA: 0x00071E68 File Offset: 0x00070068
			public Bindings(TextLoader parent, TextLoader.Column[] cols, IMultiStreamSource headerFile)
				: this()
			{
				using (IChannel channel = parent._host.Start("Binding"))
				{
					bool flag = false;
					foreach (TextLoader.Column column in cols)
					{
						if (Utils.Size<TextLoader.Range>(column.source) == 0)
						{
							throw Contracts.ExceptUserArg(channel, "source", "Must specify some source column indices");
						}
						if (!flag)
						{
							if (column.source.Any((TextLoader.Range r) => r.autoEnd && r.max == null))
							{
								flag = true;
							}
						}
					}
					int num = parent._inputSize;
					List<DvText> list = null;
					if (headerFile != null)
					{
						TextLoader.Cursor.GetSomeLines(headerFile, 1, ref list);
					}
					if (flag && num == 0)
					{
						TextLoader.Cursor.GetSomeLines(parent._files, 100, ref list);
					}
					else if (headerFile == null && parent.HasHeader)
					{
						TextLoader.Cursor.GetSomeLines(parent._files, 1, ref list);
					}
					if (flag && num == 0)
					{
						int num2 = 0;
						int num3 = 0;
						if (Utils.Size<DvText>(list) > 0)
						{
							TextLoader.Parser.GetInputSize(parent, list, out num2, out num3);
						}
						if (num3 == 0)
						{
							throw Contracts.ExceptUserArg(channel, "source", "Can't determine the number of source columns without valid data");
						}
						if (num2 < num3)
						{
							throw Contracts.ExceptUserArg(channel, "source", "The size of input lines is not consistent");
						}
						num = Math.Min(num2, 2147483646);
					}
					int num4 = -1;
					PrimitiveType primitiveType = null;
					TextLoader.Segment[] array = null;
					int isegOther = -1;
					this.Infos = new TextLoader.ColInfo[cols.Length];
					this.NameToInfoIndex = new Dictionary<string, int>(this.Infos.Length);
					for (int j = 0; j < this.Infos.Length; j++)
					{
						TextLoader.Column column2 = cols[j];
						Contracts.CheckUserArg(channel, !string.IsNullOrWhiteSpace(column2.name), "name");
						string text = column2.name.Trim();
						if (j == this.NameToInfoIndex.Count && this.NameToInfoIndex.ContainsKey(text))
						{
							channel.Info("Duplicate name(s) specified - later columns will hide earlier ones");
						}
						PrimitiveType primitiveType2;
						if (column2.keyRange != null)
						{
							primitiveType2 = TypeParsingUtils.ConstructKeyType(column2.type, column2.keyRange);
						}
						else
						{
							DataKind dataKind = column2.type ?? 9;
							Contracts.CheckUserArg(channel, Enum.IsDefined(typeof(DataKind), dataKind), "type", "Bad item type");
							primitiveType2 = PrimitiveType.FromKind(dataKind);
						}
						TextLoader.Segment[] array2 = new TextLoader.Segment[column2.source.Length];
						for (int k = 0; k < array2.Length; k++)
						{
							TextLoader.Range range = column2.source[k];
							if (range.allOther)
							{
								Contracts.CheckUserArg(channel, num4 < 0, "allOther", "At most one all other range can be specified");
								num4 = j;
								isegOther = k;
								primitiveType = primitiveType2;
								array = array2;
							}
							int min = range.min;
							Contracts.CheckUserArg(channel, 0 <= min && min < 2147483646, "min");
							TextLoader.Segment segment;
							if (range.max != null)
							{
								int value = range.max.Value;
								Contracts.CheckUserArg(channel, min <= value && value < 2147483646, "max");
								segment = new TextLoader.Segment(min, value + 1, range.forceVector);
							}
							else if (range.autoEnd)
							{
								if (min >= num)
								{
									throw Contracts.ExceptUserArg(channel, "min", "Column #{0} not found in the dataset (it only has {1} columns)", new object[] { min, num });
								}
								segment = new TextLoader.Segment(min, num, true);
							}
							else if (range.variableEnd)
							{
								segment = new TextLoader.Segment(min);
							}
							else
							{
								segment = new TextLoader.Segment(min, min + 1, range.forceVector);
							}
							array2[k] = segment;
						}
						if (num4 != j)
						{
							this.Infos[j] = TextLoader.ColInfo.Create(text, primitiveType2, array2, true);
						}
						this.NameToInfoIndex[text] = j;
					}
					if (num4 >= 0)
					{
						List<TextLoader.Segment> list2 = new List<TextLoader.Segment>();
						for (int l = 0; l < this.Infos.Length; l++)
						{
							if (l == num4)
							{
								list2.AddRange(array.Where((TextLoader.Segment s, int i) => i != isegOther));
							}
							else
							{
								list2.AddRange(this.Infos[l].Segments);
							}
						}
						List<TextLoader.Segment> list3 = new List<TextLoader.Segment>();
						TextLoader.Segment segment2 = array[isegOther];
						for (int m = 0; m < array.Length; m++)
						{
							if (m != isegOther)
							{
								list3.Add(array[m]);
							}
							else
							{
								list2.Sort((TextLoader.Segment s1, TextLoader.Segment s2) => s1.Min.CompareTo(s2.Min));
								int num5 = segment2.Min;
								int lim = segment2.Lim;
								foreach (TextLoader.Segment segment3 in list2)
								{
									if (num5 < segment3.Min)
									{
										list3.Add(new TextLoader.Segment(num5, segment3.Min, true));
									}
									if (num5 < segment3.Lim)
									{
										num5 = segment3.Lim;
									}
									if (num5 >= lim)
									{
										break;
									}
								}
								if (num5 < lim)
								{
									list3.Add(new TextLoader.Segment(num5, lim, true));
								}
							}
						}
						Contracts.CheckUserArg(channel, list3.Count > 0, "allOther", "No index is selected as all other indexes.");
						this.Infos[num4] = TextLoader.ColInfo.Create(cols[num4].name.Trim(), primitiveType, list3.ToArray(), true);
					}
					this._slotNames = new VBuffer<DvText>[this.Infos.Length];
					if ((parent.HasHeader || headerFile != null) && Utils.Size<DvText>(list) > 0)
					{
						this._header = list[0];
					}
					if (this._header.HasChars)
					{
						TextLoader.Parser.ParseSlotNames(parent, this._header, this.Infos, this._slotNames);
					}
					channel.Done();
				}
			}

			// Token: 0x060013F2 RID: 5106 RVA: 0x000724B0 File Offset: 0x000706B0
			public Bindings(ModelLoadContext ctx, TextLoader parent)
				: this()
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(num > 0);
				this.Infos = new TextLoader.ColInfo[num];
				this.NameToInfoIndex = new Dictionary<string, int>(this.Infos.Length);
				for (int i = 0; i < num; i++)
				{
					string text = ctx.LoadNonEmptyString();
					DataKind dataKind = ctx.Reader.ReadByte();
					if (!Enum.IsDefined(typeof(DataKind), dataKind))
					{
						throw Contracts.ExceptDecode("Unknown item type code: '{0}'", new object[] { dataKind });
					}
					bool flag = Utils.ReadBoolByte(ctx.Reader);
					PrimitiveType primitiveType;
					if (flag)
					{
						Contracts.CheckDecode(KeyType.IsValidDataKind(dataKind));
						bool flag2 = Utils.ReadBoolByte(ctx.Reader);
						ulong num2 = ctx.Reader.ReadUInt64();
						Contracts.CheckDecode(num2 >= 0UL);
						int num3 = ctx.Reader.ReadInt32();
						if (num3 == 0)
						{
							primitiveType = new KeyType(dataKind, num2, 0, flag2);
						}
						else
						{
							Contracts.CheckDecode(flag2);
							Contracts.CheckDecode(2 <= num3 && (long)num3 <= (long)DataKindExtensions.ToMaxInt(dataKind));
							primitiveType = new KeyType(dataKind, num2, num3, true);
						}
					}
					else
					{
						primitiveType = PrimitiveType.FromKind(dataKind);
					}
					int num4 = ctx.Reader.ReadInt32();
					Contracts.CheckDecode(num4 > 0);
					TextLoader.Segment[] array = new TextLoader.Segment[num4];
					for (int j = 0; j < num4; j++)
					{
						int num5 = ctx.Reader.ReadInt32();
						int num6 = ctx.Reader.ReadInt32();
						Contracts.CheckDecode(0 <= num5 && num5 < num6 && num6 <= int.MaxValue);
						bool flag3 = false;
						if (ctx.Header.ModelVerWritten >= 65546U)
						{
							flag3 = Utils.ReadBoolByte(ctx.Reader);
						}
						array[j] = new TextLoader.Segment(num5, num6, flag3);
					}
					this.Infos[i] = TextLoader.ColInfo.Create(text, primitiveType, array, false);
					this.NameToInfoIndex[text] = i;
				}
				this._slotNames = new VBuffer<DvText>[this.Infos.Length];
				List<DvText> list = null;
				if (parent.HasHeader)
				{
					TextLoader.Cursor.GetSomeLines(parent._files, 2, ref list);
				}
				if (Utils.Size<DvText>(list) == 0)
				{
					string result = null;
					ctx.TryLoadTextStream("Header.txt", delegate(TextReader reader)
					{
						result = reader.ReadLine();
					});
					if (!string.IsNullOrEmpty(result))
					{
						Utils.Add<DvText>(ref list, new DvText(result));
					}
				}
				if (Utils.Size<DvText>(list) > 0)
				{
					TextLoader.Parser.ParseSlotNames(parent, this._header = list[0], this.Infos, this._slotNames);
				}
			}

			// Token: 0x060013F3 RID: 5107 RVA: 0x00072790 File Offset: 0x00070990
			public void Save(ModelSaveContext ctx)
			{
				ctx.Writer.Write(this.Infos.Length);
				for (int i = 0; i < this.Infos.Length; i++)
				{
					TextLoader.ColInfo colInfo = this.Infos[i];
					ctx.SaveNonEmptyString(colInfo.Name);
					ColumnType itemType = colInfo.ColType.ItemType;
					ctx.Writer.Write(itemType.RawKind);
					Utils.WriteBoolByte(ctx.Writer, itemType.IsKey);
					if (itemType.IsKey)
					{
						KeyType asKey = itemType.AsKey;
						Utils.WriteBoolByte(ctx.Writer, asKey.Contiguous);
						ctx.Writer.Write(asKey.Min);
						ctx.Writer.Write(asKey.Count);
					}
					ctx.Writer.Write(colInfo.Segments.Length);
					foreach (TextLoader.Segment segment in colInfo.Segments)
					{
						ctx.Writer.Write(segment.Min);
						ctx.Writer.Write(segment.Lim);
						Utils.WriteBoolByte(ctx.Writer, segment.ForceVector);
					}
				}
				if (this._header.HasChars)
				{
					ctx.SaveTextStream("Header.txt", delegate(TextWriter writer)
					{
						writer.WriteLine(this._header.ToString());
					});
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x060013F4 RID: 5108 RVA: 0x000728EA File Offset: 0x00070AEA
			public int ColumnCount
			{
				get
				{
					return this.Infos.Length;
				}
			}

			// Token: 0x060013F5 RID: 5109 RVA: 0x000728F4 File Offset: 0x00070AF4
			public bool TryGetColumnIndex(string name, out int col)
			{
				return this.NameToInfoIndex.TryGetValue(name, out col);
			}

			// Token: 0x060013F6 RID: 5110 RVA: 0x00072903 File Offset: 0x00070B03
			public string GetColumnName(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.Infos.Length, "col");
				return this.Infos[col].Name;
			}

			// Token: 0x060013F7 RID: 5111 RVA: 0x0007292E File Offset: 0x00070B2E
			public ColumnType GetColumnType(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.Infos.Length, "col");
				return this.Infos[col].ColType;
			}

			// Token: 0x060013F8 RID: 5112 RVA: 0x00072A9C File Offset: 0x00070C9C
			public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				VBuffer<DvText> names = this._slotNames[col];
				if (names.Length > 0)
				{
					yield return MetadataUtils.GetSlotNamesPair(names.Length);
				}
				yield break;
			}

			// Token: 0x060013F9 RID: 5113 RVA: 0x00072AC0 File Offset: 0x00070CC0
			public ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				Contracts.CheckNonEmpty(kind, "kind");
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				if (kind == null || !(kind == "SlotNames"))
				{
					return null;
				}
				VBuffer<DvText> vbuffer = this._slotNames[col];
				if (vbuffer.Length == 0)
				{
					return null;
				}
				return MetadataUtils.GetNamesType(vbuffer.Length);
			}

			// Token: 0x060013FA RID: 5114 RVA: 0x00072B30 File Offset: 0x00070D30
			public void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				Contracts.CheckNonEmpty(kind, "kind");
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				if (kind != null && kind == "SlotNames")
				{
					MetadataUtils.Marshal<VBuffer<DvText>, TValue>(this._getSlotNames, col, ref value);
					return;
				}
				throw MetadataUtils.ExceptGetMetadata();
			}

			// Token: 0x060013FB RID: 5115 RVA: 0x00072B88 File Offset: 0x00070D88
			private void GetSlotNames(int col, ref VBuffer<DvText> dst)
			{
				VBuffer<DvText> vbuffer = this._slotNames[col];
				if (vbuffer.Length == 0)
				{
					throw MetadataUtils.ExceptGetMetadata();
				}
				vbuffer.CopyTo(ref dst);
			}

			// Token: 0x04000BB2 RID: 2994
			public readonly TextLoader.ColInfo[] Infos;

			// Token: 0x04000BB3 RID: 2995
			public readonly Dictionary<string, int> NameToInfoIndex;

			// Token: 0x04000BB4 RID: 2996
			private readonly VBuffer<DvText>[] _slotNames;

			// Token: 0x04000BB5 RID: 2997
			private readonly DvText _header;

			// Token: 0x04000BB6 RID: 2998
			private readonly MetadataUtils.MetadataGetter<VBuffer<DvText>> _getSlotNames;
		}

		// Token: 0x020003A3 RID: 931
		[Flags]
		private enum Options : uint
		{
			// Token: 0x04000BBA RID: 3002
			TrimWhitespace = 1U,
			// Token: 0x04000BBB RID: 3003
			HasHeader = 2U,
			// Token: 0x04000BBC RID: 3004
			AllowQuoting = 4U,
			// Token: 0x04000BBD RID: 3005
			AllowSparse = 8U,
			// Token: 0x04000BBE RID: 3006
			All = 15U
		}

		// Token: 0x020003A4 RID: 932
		private sealed class LoaderHolder
		{
			// Token: 0x04000BBF RID: 3007
			[Argument(4)]
			public SubComponent<IDataLoader, SignatureDataLoader> loader;
		}

		// Token: 0x020003A5 RID: 933
		private sealed class Cursor : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x06001400 RID: 5120 RVA: 0x00072BC6 File Offset: 0x00070DC6
			public override long Batch
			{
				get
				{
					return this._batch;
				}
			}

			// Token: 0x06001401 RID: 5121 RVA: 0x00072BD0 File Offset: 0x00070DD0
			private static void SetupCursor(TextLoader parent, bool[] active, int n, out int srcNeeded, out int cthd)
			{
				TextLoader.Bindings bindings = parent._bindings;
				int num = 1;
				for (int i = 0; i < bindings.Infos.Length; i++)
				{
					if (active == null || active[i])
					{
						TextLoader.ColInfo colInfo = bindings.Infos[i];
						foreach (TextLoader.Segment segment in colInfo.Segments)
						{
							if (num < segment.Lim)
							{
								num = segment.Lim;
							}
						}
					}
				}
				if (num > parent._inputSize && parent._inputSize > 0)
				{
					num = parent._inputSize;
				}
				srcNeeded = num - 1;
				cthd = DataViewUtils.GetThreadCount(parent._host, n, !parent._useThreads);
				long num2 = parent._maxRows / 64L;
				if ((long)cthd > num2)
				{
					cthd = Math.Max(1, (int)num2);
				}
			}

			// Token: 0x06001402 RID: 5122 RVA: 0x00072CA0 File Offset: 0x00070EA0
			private Cursor(TextLoader parent, TextLoader.ParseStats stats, bool[] active, TextLoader.Cursor.LineReader reader, int srcNeeded, int cthd)
				: base(parent._host)
			{
				this._total = -1L;
				this._batch = -1L;
				this._bindings = parent._bindings;
				this._parser = parent._parser;
				this._active = active;
				this._reader = reader;
				this._stats = stats;
				this._srcNeeded = srcNeeded;
				TextLoader.Cursor.ParallelState parallelState = null;
				if (cthd > 1)
				{
					parallelState = new TextLoader.Cursor.ParallelState(this, out this._rows, cthd);
				}
				else
				{
					this._rows = this._parser.CreateRowSet(this._stats, 1, this._active);
				}
				try
				{
					this._getters = new Delegate[this._bindings.Infos.Length];
					for (int i = 0; i < this._getters.Length; i++)
					{
						if (this._active == null || this._active[i])
						{
							TextLoader.ColumnPipe columnPipe = this._rows.Pipes[i];
							this._getters[i] = columnPipe.GetGetter();
						}
					}
					if (parallelState != null)
					{
						this._ator = this.ParseParallel(parallelState).GetEnumerator();
						parallelState = null;
					}
					else
					{
						this._ator = this.ParseSequential().GetEnumerator();
					}
				}
				finally
				{
					if (parallelState != null)
					{
						parallelState.Dispose();
					}
				}
			}

			// Token: 0x06001403 RID: 5123 RVA: 0x00072DD4 File Offset: 0x00070FD4
			public static IRowCursor Create(TextLoader parent, bool[] active)
			{
				int num;
				int num2;
				TextLoader.Cursor.SetupCursor(parent, active, 0, out num, out num2);
				TextLoader.Cursor.LineReader lineReader = new TextLoader.Cursor.LineReader(parent._files, 64, 100, parent.HasHeader, parent._maxRows, 1);
				TextLoader.ParseStats parseStats = new TextLoader.ParseStats(parent._host, 1, 10L);
				return new TextLoader.Cursor(parent, parseStats, active, lineReader, num, num2);
			}

			// Token: 0x06001404 RID: 5124 RVA: 0x00072E28 File Offset: 0x00071028
			public static IRowCursor[] CreateSet(out IRowCursorConsolidator consolidator, TextLoader parent, bool[] active, int n)
			{
				int num;
				int num2;
				TextLoader.Cursor.SetupCursor(parent, active, n, out num, out num2);
				TextLoader.Cursor.LineReader lineReader = new TextLoader.Cursor.LineReader(parent._files, 64, 100, parent.HasHeader, parent._maxRows, num2);
				TextLoader.ParseStats parseStats = new TextLoader.ParseStats(parent._host, num2, 10L);
				if (num2 <= 1)
				{
					consolidator = null;
					return new IRowCursor[]
					{
						new TextLoader.Cursor(parent, parseStats, active, lineReader, num, 1)
					};
				}
				consolidator = new TextLoader.Cursor.Consolidator(num2);
				IRowCursor[] array = new IRowCursor[num2];
				IRowCursor[] array3;
				try
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = new TextLoader.Cursor(parent, parseStats, active, lineReader, num, 1);
					}
					IRowCursor[] array2 = array;
					array = null;
					array3 = array2;
				}
				finally
				{
					if (array != null)
					{
						foreach (IRowCursor rowCursor in array)
						{
							if (rowCursor != null)
							{
								rowCursor.Dispose();
							}
							else
							{
								lineReader.Release();
								parseStats.Release();
							}
						}
					}
				}
				return array3;
			}

			// Token: 0x06001405 RID: 5125 RVA: 0x00072F4B File Offset: 0x0007114B
			public override ValueGetter<UInt128> GetIdGetter()
			{
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._ch, base.IsGood, "Cannot call ID getter in current state");
					val = new UInt128((ulong)this._total, 0UL);
				};
			}

			// Token: 0x06001406 RID: 5126 RVA: 0x00072F5C File Offset: 0x0007115C
			public static void GetSomeLines(IMultiStreamSource source, int count, ref List<DvText> lines)
			{
				if (count < 2)
				{
					count = 2;
				}
				TextLoader.Cursor.LineReader lineReader = new TextLoader.Cursor.LineReader(source, count, 1, false, (long)count, 1);
				TextLoader.Cursor.LineBatch batch;
				try
				{
					batch = lineReader.GetBatch();
					if (Utils.Size<TextLoader.Cursor.LineInfo>(batch.Infos) == 0)
					{
						return;
					}
				}
				finally
				{
					lineReader.Release();
				}
				for (int i = 0; i < batch.Infos.Length; i++)
				{
					Utils.Add<DvText>(ref lines, new DvText(batch.Infos[i].Text));
				}
			}

			// Token: 0x06001407 RID: 5127 RVA: 0x00072FE4 File Offset: 0x000711E4
			public static string GetEmbeddedArgs(IMultiStreamSource files)
			{
				if (files.Count == 0)
				{
					return null;
				}
				StringBuilder stringBuilder = new StringBuilder();
				using (TextReader textReader = files.OpenTextReader(0))
				{
					string text = "";
					for (;;)
					{
						string text2 = textReader.ReadLine();
						if (text2 == null)
						{
							break;
						}
						if (text2.Length != 0 && !text2.StartsWith("//"))
						{
							if (text2[0] != '#')
							{
								break;
							}
							if (text2.Length > 2 && text2[1] == '@')
							{
								stringBuilder.Append(text).Append(text2.Substring(2).Trim());
								text = " ";
							}
						}
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x06001408 RID: 5128 RVA: 0x00073090 File Offset: 0x00071290
			public ISchema Schema
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x06001409 RID: 5129 RVA: 0x00073098 File Offset: 0x00071298
			public override void Dispose()
			{
				if (this._disposed)
				{
					return;
				}
				this._disposed = true;
				this._ator.Dispose();
				this._reader.Release();
				this._stats.Release();
				base.Dispose();
			}

			// Token: 0x0600140A RID: 5130 RVA: 0x000730D1 File Offset: 0x000712D1
			protected override bool MoveNextCore()
			{
				if (this._ator.MoveNext())
				{
					this._rows.Index = this._ator.Current;
					return true;
				}
				this._rows.Index = -1;
				return false;
			}

			// Token: 0x0600140B RID: 5131 RVA: 0x00073105 File Offset: 0x00071305
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._bindings.Infos.Length);
				return this._active == null || this._active[col];
			}

			// Token: 0x0600140C RID: 5132 RVA: 0x0007313C File Offset: 0x0007133C
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col));
				ValueGetter<TValue> valueGetter = this._getters[col] as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue in GetGetter: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x0600140D RID: 5133 RVA: 0x00073424 File Offset: 0x00071624
			private IEnumerable<int> ParseSequential()
			{
				TextLoader.Parser.Helper helper = this._parser.CreateHelper(this._rows.Stats, this._srcNeeded);
				TextLoader.Cursor.LineBatch batch;
				while ((batch = this._reader.GetBatch()).Infos != null)
				{
					this._total = batch.Total;
					foreach (TextLoader.Cursor.LineInfo info in batch.Infos)
					{
						this._parser.ParseRow(this._rows, 0, helper, this._active, batch.Path, info.Line, info.Text);
						this._batch = batch.Batch;
						yield return 0;
						this._total += 1L;
					}
					this._batch = long.MaxValue;
				}
				yield break;
			}

			// Token: 0x0600140E RID: 5134 RVA: 0x00073704 File Offset: 0x00071904
			private IEnumerable<int> ParseParallel(TextLoader.Cursor.ParallelState state)
			{
				try
				{
					foreach (TextLoader.Cursor.RowBatch batch in state.GetBatches())
					{
						this._total = batch.Total - 1L;
						for (int irow = batch.IrowMin; irow < batch.IrowLim; irow++)
						{
							this._total += 1L;
							yield return irow;
						}
					}
					if (state.ParsingException != null)
					{
						throw Contracts.ExceptDecode(this._ch, state.ParsingException, "Parsing failed with an exception: {0}", new object[] { state.ParsingException.Message });
					}
				}
				finally
				{
					if (state != null)
					{
						((IDisposable)state).Dispose();
					}
				}
				yield break;
			}

			// Token: 0x04000BC0 RID: 3008
			private const int BatchSize = 64;

			// Token: 0x04000BC1 RID: 3009
			private const int TimeOut = 100;

			// Token: 0x04000BC2 RID: 3010
			private readonly TextLoader.Bindings _bindings;

			// Token: 0x04000BC3 RID: 3011
			private readonly TextLoader.Parser _parser;

			// Token: 0x04000BC4 RID: 3012
			private readonly bool[] _active;

			// Token: 0x04000BC5 RID: 3013
			private readonly int _srcNeeded;

			// Token: 0x04000BC6 RID: 3014
			private readonly TextLoader.Cursor.LineReader _reader;

			// Token: 0x04000BC7 RID: 3015
			private readonly IEnumerator<int> _ator;

			// Token: 0x04000BC8 RID: 3016
			private readonly Delegate[] _getters;

			// Token: 0x04000BC9 RID: 3017
			private readonly TextLoader.ParseStats _stats;

			// Token: 0x04000BCA RID: 3018
			private readonly TextLoader.RowSet _rows;

			// Token: 0x04000BCB RID: 3019
			private long _total;

			// Token: 0x04000BCC RID: 3020
			private long _batch;

			// Token: 0x04000BCD RID: 3021
			private bool _disposed;

			// Token: 0x020003A6 RID: 934
			private struct LineBatch
			{
				// Token: 0x06001410 RID: 5136 RVA: 0x00073728 File Offset: 0x00071928
				public LineBatch(string path, long total, long batch, TextLoader.Cursor.LineInfo[] infos)
				{
					this.Path = path;
					this.Total = total;
					this.Batch = batch;
					this.Infos = infos;
					this.Exception = null;
				}

				// Token: 0x06001411 RID: 5137 RVA: 0x0007374E File Offset: 0x0007194E
				public LineBatch(Exception ex)
				{
					this.Path = null;
					this.Total = 0L;
					this.Batch = 0L;
					this.Infos = null;
					this.Exception = ex;
				}

				// Token: 0x04000BCE RID: 3022
				public readonly string Path;

				// Token: 0x04000BCF RID: 3023
				public readonly long Total;

				// Token: 0x04000BD0 RID: 3024
				public readonly long Batch;

				// Token: 0x04000BD1 RID: 3025
				public readonly TextLoader.Cursor.LineInfo[] Infos;

				// Token: 0x04000BD2 RID: 3026
				public readonly Exception Exception;
			}

			// Token: 0x020003A7 RID: 935
			private struct LineInfo
			{
				// Token: 0x06001412 RID: 5138 RVA: 0x00073775 File Offset: 0x00071975
				public LineInfo(long line, string text)
				{
					this.Line = line;
					this.Text = text;
				}

				// Token: 0x04000BD3 RID: 3027
				public readonly long Line;

				// Token: 0x04000BD4 RID: 3028
				public readonly string Text;
			}

			// Token: 0x020003A8 RID: 936
			private sealed class LineReader
			{
				// Token: 0x06001413 RID: 5139 RVA: 0x00073788 File Offset: 0x00071988
				public LineReader(IMultiStreamSource files, int batchSize, int bufSize, bool hasHeader, long limit, int cref)
				{
					this._limit = limit;
					this._hasHeader = hasHeader;
					this._batchSize = batchSize;
					this._files = files;
					this._cref = cref;
					this._queue = new BlockingCollection<TextLoader.Cursor.LineBatch>(bufSize);
					this._thdRead = Utils.CreateBackgroundThread(new ThreadStart(this.ThreadProc));
					this._thdRead.Start();
				}

				// Token: 0x06001414 RID: 5140 RVA: 0x000737F0 File Offset: 0x000719F0
				public void Release()
				{
					int num = Interlocked.Decrement(ref this._cref);
					if (num != 0)
					{
						return;
					}
					if (this._thdRead != null)
					{
						this._abort = true;
						this._thdRead.Join();
						this._thdRead = null;
					}
					if (this._queue != null)
					{
						this._queue.Dispose();
						this._queue = null;
					}
				}

				// Token: 0x06001415 RID: 5141 RVA: 0x0007384C File Offset: 0x00071A4C
				public TextLoader.Cursor.LineBatch GetBatch()
				{
					Exception ex = null;
					try
					{
						TextLoader.Cursor.LineBatch lineBatch = this._queue.Take();
						if (lineBatch.Exception == null)
						{
							return lineBatch;
						}
						ex = lineBatch.Exception;
					}
					catch (InvalidOperationException)
					{
						if (this._queue.IsAddingCompleted)
						{
							return default(TextLoader.Cursor.LineBatch);
						}
						throw;
					}
					throw Contracts.ExceptDecode(ex, "Stream reading encountered exception");
				}

				// Token: 0x06001416 RID: 5142 RVA: 0x000738B8 File Offset: 0x00071AB8
				private void ThreadProc()
				{
					try
					{
						if (this._limit > 0L)
						{
							long num = 0L;
							long num2 = -1L;
							for (int i = 0; i < this._files.Count; i++)
							{
								string pathOrNull = this._files.GetPathOrNull(i);
								using (TextReader textReader = this._files.OpenTextReader(i))
								{
									long num3 = 0L;
									string text;
									do
									{
										text = textReader.ReadLine();
										if (text == null)
										{
											goto IL_01C9;
										}
										num3 += 1L;
									}
									while (text.Length <= 0 || text[0] == '#' || text.StartsWith("//"));
									int num4 = 0;
									TextLoader.Cursor.LineInfo[] array = new TextLoader.Cursor.LineInfo[this._batchSize];
									if (!this._hasHeader)
									{
										array[num4++] = new TextLoader.Cursor.LineInfo(num3, text);
										if ((num += 1L) >= this._limit)
										{
											this.PostPartial(pathOrNull, num - (long)num4, ref num2, num4, array);
											break;
										}
									}
									while (!this._abort)
									{
										text = textReader.ReadLine();
										if (text == null)
										{
											this.PostPartial(pathOrNull, num - (long)num4, ref num2, num4, array);
											goto IL_01C9;
										}
										num3 += 1L;
										if (text.Length >= 2)
										{
											if (text[0] == '/' && text[1] == '/')
											{
												continue;
											}
										}
										else if (text.Length == 0)
										{
											continue;
										}
										array[num4] = new TextLoader.Cursor.LineInfo(num3, text);
										if (++num4 >= array.Length)
										{
											num2 += 1L;
											TextLoader.Cursor.LineBatch lineBatch = new TextLoader.Cursor.LineBatch(pathOrNull, num - (long)num4 + 1L, num2, array);
											while (!this._queue.TryAdd(lineBatch, 100))
											{
												if (this._abort)
												{
													return;
												}
											}
											array = new TextLoader.Cursor.LineInfo[this._batchSize];
											num4 = 0;
										}
										if ((num += 1L) >= this._limit)
										{
											this.PostPartial(pathOrNull, num - (long)num4, ref num2, num4, array);
											break;
										}
									}
									break;
									IL_01C9:;
								}
							}
						}
					}
					catch (Exception ex)
					{
						while (!this._queue.TryAdd(new TextLoader.Cursor.LineBatch(ex), 100))
						{
							if (this._abort)
							{
								break;
							}
						}
					}
					finally
					{
						this._queue.CompleteAdding();
					}
				}

				// Token: 0x06001417 RID: 5143 RVA: 0x00073B38 File Offset: 0x00071D38
				private void PostPartial(string path, long total, ref long batch, int index, TextLoader.Cursor.LineInfo[] infos)
				{
					if (index <= 0)
					{
						return;
					}
					Array.Resize<TextLoader.Cursor.LineInfo>(ref infos, index);
					batch += 1L;
					while (!this._queue.TryAdd(new TextLoader.Cursor.LineBatch(path, total, batch, infos), 100))
					{
						if (this._abort)
						{
							return;
						}
					}
				}

				// Token: 0x04000BD5 RID: 3029
				private readonly long _limit;

				// Token: 0x04000BD6 RID: 3030
				private readonly bool _hasHeader;

				// Token: 0x04000BD7 RID: 3031
				private readonly int _batchSize;

				// Token: 0x04000BD8 RID: 3032
				private readonly IMultiStreamSource _files;

				// Token: 0x04000BD9 RID: 3033
				private int _cref;

				// Token: 0x04000BDA RID: 3034
				private BlockingCollection<TextLoader.Cursor.LineBatch> _queue;

				// Token: 0x04000BDB RID: 3035
				private Thread _thdRead;

				// Token: 0x04000BDC RID: 3036
				private volatile bool _abort;
			}

			// Token: 0x020003A9 RID: 937
			private struct RowBatch
			{
				// Token: 0x06001418 RID: 5144 RVA: 0x00073B77 File Offset: 0x00071D77
				public RowBatch(int irowMin, int irowLim, long total)
				{
					this.IrowMin = irowMin;
					this.IrowLim = irowLim;
					this.Total = total;
				}

				// Token: 0x04000BDD RID: 3037
				public int IrowMin;

				// Token: 0x04000BDE RID: 3038
				public int IrowLim;

				// Token: 0x04000BDF RID: 3039
				public long Total;
			}

			// Token: 0x020003AA RID: 938
			private sealed class ParallelState : IDisposable
			{
				// Token: 0x06001419 RID: 5145 RVA: 0x00073B90 File Offset: 0x00071D90
				public ParallelState(TextLoader.Cursor curs, out TextLoader.RowSet rows, int cthd)
				{
					this._curs = curs;
					this._reader = this._curs._reader;
					this._blockCount = cthd + 3;
					TextLoader.RowSet rowSet;
					rows = (rowSet = this._curs._parser.CreateRowSet(this._curs._stats, checked(this._blockCount * 64), this._curs._active));
					this._rows = rowSet;
					this._waiterReading = new OrderedWaiter(false);
					this._waiterPublish = new OrderedWaiter(false);
					this._queue = new BlockingCollection<TextLoader.Cursor.RowBatch>(2);
					this._threads = new Thread[cthd];
					this._threadsRunning = cthd;
					for (int i = 0; i < this._threads.Length; i++)
					{
						Thread thread = (this._threads[i] = Utils.CreateBackgroundThread(new ParameterizedThreadStart(this.ThreadProc)));
						thread.Start(i);
					}
				}

				// Token: 0x0600141A RID: 5146 RVA: 0x00073C74 File Offset: 0x00071E74
				public void Dispose()
				{
					this.Quit();
					for (int i = 0; i < this._threads.Length; i++)
					{
						this._threads[i].Join();
					}
				}

				// Token: 0x0600141B RID: 5147 RVA: 0x00073CA7 File Offset: 0x00071EA7
				private void Quit()
				{
					this._done = true;
					this._waiterReading.IncrementAll();
					this._waiterPublish.IncrementAll();
				}

				// Token: 0x0600141C RID: 5148 RVA: 0x00073CCA File Offset: 0x00071ECA
				public IEnumerable<TextLoader.Cursor.RowBatch> GetBatches()
				{
					this._waiterReading.Increment();
					this._waiterPublish.Increment();
					return this._queue.GetConsumingEnumerable();
				}

				// Token: 0x0600141D RID: 5149 RVA: 0x00073CF0 File Offset: 0x00071EF0
				private void ThreadProc(object obj)
				{
					int num = (int)obj;
					try
					{
						this.Parse(num);
					}
					catch (Exception ex)
					{
						this.ParsingException = ex;
						this.Quit();
					}
					finally
					{
						if (Interlocked.Decrement(ref this._threadsRunning) <= 0)
						{
							this._queue.CompleteAdding();
						}
					}
				}

				// Token: 0x0600141E RID: 5150 RVA: 0x00073D58 File Offset: 0x00071F58
				private void Parse(int tid)
				{
					long num = (long)tid;
					int num2 = tid;
					TextLoader.Parser.Helper helper = this._curs._parser.CreateHelper(this._rows.Stats, this._curs._srcNeeded);
					while (!this._done)
					{
						this._waiterReading.Wait(num, default(CancellationToken));
						if (this._done)
						{
							return;
						}
						TextLoader.Cursor.LineBatch batch;
						try
						{
							batch = this._reader.GetBatch();
						}
						finally
						{
							this._waiterReading.Increment();
						}
						if (batch.Infos == null || this._done)
						{
							return;
						}
						TextLoader.Cursor.RowBatch rowBatch = new TextLoader.Cursor.RowBatch(num2 * 64, num2 * 64 + batch.Infos.Length, batch.Total);
						int num3 = rowBatch.IrowMin;
						foreach (TextLoader.Cursor.LineInfo lineInfo in batch.Infos)
						{
							if (this._done)
							{
								return;
							}
							this._curs._parser.ParseRow(this._rows, num3, helper, this._curs._active, batch.Path, lineInfo.Line, lineInfo.Text);
							num3++;
						}
						if (this._done)
						{
							return;
						}
						this._waiterPublish.Wait(num, default(CancellationToken));
						if (this._done)
						{
							return;
						}
						while (!this._queue.TryAdd(rowBatch, 100))
						{
							if (this._done)
							{
								return;
							}
						}
						this._waiterPublish.Increment();
						num += (long)this._threads.Length;
						num2 += this._threads.Length;
						if (num2 >= this._blockCount)
						{
							num2 -= this._blockCount;
						}
					}
				}

				// Token: 0x04000BE0 RID: 3040
				private const int BlockSize = 64;

				// Token: 0x04000BE1 RID: 3041
				private readonly TextLoader.Cursor _curs;

				// Token: 0x04000BE2 RID: 3042
				private readonly TextLoader.Cursor.LineReader _reader;

				// Token: 0x04000BE3 RID: 3043
				private readonly TextLoader.RowSet _rows;

				// Token: 0x04000BE4 RID: 3044
				private readonly int _blockCount;

				// Token: 0x04000BE5 RID: 3045
				private readonly OrderedWaiter _waiterReading;

				// Token: 0x04000BE6 RID: 3046
				private readonly OrderedWaiter _waiterPublish;

				// Token: 0x04000BE7 RID: 3047
				private readonly BlockingCollection<TextLoader.Cursor.RowBatch> _queue;

				// Token: 0x04000BE8 RID: 3048
				private readonly Thread[] _threads;

				// Token: 0x04000BE9 RID: 3049
				private int _threadsRunning;

				// Token: 0x04000BEA RID: 3050
				private volatile bool _done;

				// Token: 0x04000BEB RID: 3051
				public volatile Exception ParsingException;
			}

			// Token: 0x020003AB RID: 939
			private sealed class Consolidator : IRowCursorConsolidator
			{
				// Token: 0x0600141F RID: 5151 RVA: 0x00073F20 File Offset: 0x00072120
				public Consolidator(int cthd)
				{
					this._cthd = cthd;
				}

				// Token: 0x06001420 RID: 5152 RVA: 0x00073F30 File Offset: 0x00072130
				public IRowCursor CreateCursor(IChannelProvider provider, IRowCursor[] inputs)
				{
					int num = Interlocked.Exchange(ref this._cthd, 0);
					Contracts.Check(provider, num > 1, "Consolidator can only be used once");
					Contracts.Check(provider, Utils.Size<IRowCursor>(inputs) == num, "Unexpected number of cursors");
					IRowCursor rowCursor2;
					using (IChannel channel = provider.Start("Consolidator"))
					{
						IRowCursor rowCursor = DataViewUtils.ConsolidateGeneric(channel, inputs, 64);
						channel.Done();
						rowCursor2 = rowCursor;
					}
					return rowCursor2;
				}

				// Token: 0x04000BEC RID: 3052
				private int _cthd;
			}
		}

		// Token: 0x020003AC RID: 940
		private sealed class ValueCreatorCache
		{
			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06001421 RID: 5153 RVA: 0x00073FA8 File Offset: 0x000721A8
			public static TextLoader.ValueCreatorCache Instance
			{
				get
				{
					if (TextLoader.ValueCreatorCache._instance == null)
					{
						Interlocked.CompareExchange<TextLoader.ValueCreatorCache>(ref TextLoader.ValueCreatorCache._instance, new TextLoader.ValueCreatorCache(), null);
					}
					return TextLoader.ValueCreatorCache._instance;
				}
			}

			// Token: 0x06001422 RID: 5154 RVA: 0x00073FCC File Offset: 0x000721CC
			private ValueCreatorCache()
			{
				this._conv = Conversions.Instance;
				this._methOne = new Func<PrimitiveType, Func<TextLoader.RowSet, TextLoader.ColumnPipe>>(this.GetCreatorOneCore<int>).GetMethodInfo().GetGenericMethodDefinition();
				this._methVec = new Func<PrimitiveType, Func<TextLoader.RowSet, TextLoader.ColumnPipe>>(this.GetCreatorVecCore<int>).GetMethodInfo().GetGenericMethodDefinition();
				this._creatorsOne = new Func<TextLoader.RowSet, TextLoader.ColumnPipe>[16];
				this._creatorsVec = new Func<TextLoader.RowSet, TextLoader.ColumnPipe>[16];
				for (DataKind dataKind = 1; dataKind < 17; dataKind++)
				{
					PrimitiveType primitiveType = PrimitiveType.FromKind(dataKind);
					this._creatorsOne[DataKindExtensions.ToIndex(dataKind)] = this.GetCreatorOneCore(primitiveType);
					this._creatorsVec[DataKindExtensions.ToIndex(dataKind)] = this.GetCreatorVecCore(primitiveType);
				}
			}

			// Token: 0x06001423 RID: 5155 RVA: 0x0007407C File Offset: 0x0007227C
			private Func<TextLoader.RowSet, TextLoader.ColumnPipe> GetCreatorOneCore(PrimitiveType type)
			{
				MethodInfo methodInfo = this._methOne.MakeGenericMethod(new Type[] { type.RawType });
				return (Func<TextLoader.RowSet, TextLoader.ColumnPipe>)methodInfo.Invoke(this, new object[] { type });
			}

			// Token: 0x06001424 RID: 5156 RVA: 0x000740D4 File Offset: 0x000722D4
			private Func<TextLoader.RowSet, TextLoader.ColumnPipe> GetCreatorOneCore<T>(PrimitiveType type)
			{
				TryParseMapper<T> fn = this._conv.GetParseConversion<T>(type);
				return (TextLoader.RowSet rows) => new TextLoader.PrimitivePipe<T>(rows, fn);
			}

			// Token: 0x06001425 RID: 5157 RVA: 0x00074108 File Offset: 0x00072308
			private Func<TextLoader.RowSet, TextLoader.ColumnPipe> GetCreatorVecCore(PrimitiveType type)
			{
				MethodInfo methodInfo = this._methVec.MakeGenericMethod(new Type[] { type.RawType });
				return (Func<TextLoader.RowSet, TextLoader.ColumnPipe>)methodInfo.Invoke(this, new object[] { type });
			}

			// Token: 0x06001426 RID: 5158 RVA: 0x00074160 File Offset: 0x00072360
			private Func<TextLoader.RowSet, TextLoader.ColumnPipe> GetCreatorVecCore<T>(PrimitiveType type)
			{
				TryParseMapper<T> fn = this._conv.GetParseConversion<T>(type);
				return (TextLoader.RowSet rows) => new TextLoader.VectorPipe<T>(rows, fn);
			}

			// Token: 0x06001427 RID: 5159 RVA: 0x00074194 File Offset: 0x00072394
			public Func<TextLoader.RowSet, TextLoader.ColumnPipe> GetCreatorOne(KeyType key)
			{
				MethodInfo methodInfo = this._methOne.MakeGenericMethod(new Type[] { key.RawType });
				return (Func<TextLoader.RowSet, TextLoader.ColumnPipe>)methodInfo.Invoke(this, new object[] { key });
			}

			// Token: 0x06001428 RID: 5160 RVA: 0x000741D8 File Offset: 0x000723D8
			public Func<TextLoader.RowSet, TextLoader.ColumnPipe> GetCreatorVec(KeyType key)
			{
				MethodInfo methodInfo = this._methVec.MakeGenericMethod(new Type[] { key.RawType });
				return (Func<TextLoader.RowSet, TextLoader.ColumnPipe>)methodInfo.Invoke(this, new object[] { key });
			}

			// Token: 0x06001429 RID: 5161 RVA: 0x0007421C File Offset: 0x0007241C
			public Func<TextLoader.RowSet, TextLoader.ColumnPipe> GetCreatorOne(DataKind kind)
			{
				int num = DataKindExtensions.ToIndex(kind);
				return this._creatorsOne[num];
			}

			// Token: 0x0600142A RID: 5162 RVA: 0x00074238 File Offset: 0x00072438
			public Func<TextLoader.RowSet, TextLoader.ColumnPipe> GetCreatorVec(DataKind kind)
			{
				int num = DataKindExtensions.ToIndex(kind);
				return this._creatorsVec[num];
			}

			// Token: 0x04000BED RID: 3053
			private static volatile TextLoader.ValueCreatorCache _instance;

			// Token: 0x04000BEE RID: 3054
			private readonly Conversions _conv;

			// Token: 0x04000BEF RID: 3055
			private readonly MethodInfo _methOne;

			// Token: 0x04000BF0 RID: 3056
			private readonly MethodInfo _methVec;

			// Token: 0x04000BF1 RID: 3057
			private readonly Func<TextLoader.RowSet, TextLoader.ColumnPipe>[] _creatorsOne;

			// Token: 0x04000BF2 RID: 3058
			private readonly Func<TextLoader.RowSet, TextLoader.ColumnPipe>[] _creatorsVec;
		}

		// Token: 0x020003AD RID: 941
		private sealed class ParseStats
		{
			// Token: 0x0600142B RID: 5163 RVA: 0x00074254 File Offset: 0x00072454
			public ParseStats(IChannelProvider provider, int cref, long maxShow = 10L)
			{
				Contracts.CheckValue<IChannelProvider>(provider, "provider");
				this._ch = provider.Start("ParseStats");
				this._cref = cref;
				this._maxShow = maxShow;
			}

			// Token: 0x0600142C RID: 5164 RVA: 0x00074288 File Offset: 0x00072488
			public void Release()
			{
				if (Interlocked.Decrement(ref this._cref) == 0)
				{
					if (this._badCount > 0L || this._fmtCount > 0L)
					{
						this._ch.Info("Processed {0} rows with {1} bad values and {2} format errors", new object[] { this._rowCount, this._badCount, this._fmtCount });
					}
					this._ch.Done();
					this._ch.Dispose();
				}
			}

			// Token: 0x0600142D RID: 5165 RVA: 0x00074310 File Offset: 0x00072510
			public void LogRow()
			{
				Interlocked.Increment(ref this._rowCount);
			}

			// Token: 0x0600142E RID: 5166 RVA: 0x00074320 File Offset: 0x00072520
			public void LogBadValue(long line, string colName, int slot)
			{
				long num = Interlocked.Increment(ref this._badCount);
				if (num <= this._maxShow)
				{
					this._ch.Info("  Bad value at line {0} in column {1} at slot {2}", new object[] { line, colName, slot });
					if (num == this._maxShow)
					{
						this._ch.Info("  Suppressing further bad value messages");
					}
				}
			}

			// Token: 0x0600142F RID: 5167 RVA: 0x0007438C File Offset: 0x0007258C
			public void LogBadValue(long line, string colName)
			{
				long num = Interlocked.Increment(ref this._badCount);
				if (num <= this._maxShow)
				{
					this._ch.Info("  Bad value at line {0} in column {1}", new object[] { line, colName });
					if (num == this._maxShow)
					{
						this._ch.Info("  Suppressing further bad value messages");
					}
				}
			}

			// Token: 0x06001430 RID: 5168 RVA: 0x000743EC File Offset: 0x000725EC
			public void LogBadFmt(ref TextLoader.ScanInfo scan, string msg)
			{
				long num = Interlocked.Increment(ref this._fmtCount);
				if (num <= this._maxShow)
				{
					if (scan.Line > 0L)
					{
						int num2 = scan.IchMinBuf - 1;
						this._ch.Warning("Format error at {0}({1},{2})-({1},{3}): {4}", new object[]
						{
							scan.Path,
							scan.Line,
							scan.IchMin - num2,
							scan.IchLim - num2,
							msg
						});
					}
					else
					{
						this._ch.Warning("Format error: {0}", new object[] { msg });
					}
					if (num == this._maxShow)
					{
						this._ch.Warning("Suppressing further format error messages");
					}
				}
			}

			// Token: 0x04000BF3 RID: 3059
			private const long MaxShow = 10L;

			// Token: 0x04000BF4 RID: 3060
			private readonly long _maxShow;

			// Token: 0x04000BF5 RID: 3061
			private readonly IChannel _ch;

			// Token: 0x04000BF6 RID: 3062
			private volatile int _cref;

			// Token: 0x04000BF7 RID: 3063
			private long _rowCount;

			// Token: 0x04000BF8 RID: 3064
			private long _badCount;

			// Token: 0x04000BF9 RID: 3065
			private long _fmtCount;
		}

		// Token: 0x020003AE RID: 942
		private abstract class ColumnPipe
		{
			// Token: 0x06001431 RID: 5169 RVA: 0x000744AE File Offset: 0x000726AE
			protected ColumnPipe(TextLoader.RowSet rows)
			{
				this.Rows = rows;
			}

			// Token: 0x06001432 RID: 5170
			public abstract void Reset(int irow, int size);

			// Token: 0x06001433 RID: 5171
			public abstract bool Consume(int irow, int index, ref DvText text);

			// Token: 0x06001434 RID: 5172
			public abstract Delegate GetGetter();

			// Token: 0x04000BFA RID: 3066
			public readonly TextLoader.RowSet Rows;
		}

		// Token: 0x020003AF RID: 943
		private sealed class PrimitivePipe<TResult> : TextLoader.ColumnPipe
		{
			// Token: 0x06001435 RID: 5173 RVA: 0x000744BD File Offset: 0x000726BD
			public PrimitivePipe(TextLoader.RowSet rows, TryParseMapper<TResult> conv)
				: base(rows)
			{
				this._conv = conv;
				this._values = new TResult[this.Rows.Count];
			}

			// Token: 0x06001436 RID: 5174 RVA: 0x000744E4 File Offset: 0x000726E4
			public override void Reset(int irow, int size)
			{
				this._values[irow] = default(TResult);
			}

			// Token: 0x06001437 RID: 5175 RVA: 0x00074506 File Offset: 0x00072706
			public override bool Consume(int irow, int index, ref DvText text)
			{
				return this._conv(ref text, out this._values[irow]);
			}

			// Token: 0x06001438 RID: 5176 RVA: 0x00074520 File Offset: 0x00072720
			public void Get(ref TResult value)
			{
				int index = this.Rows.Index;
				Contracts.Check(index >= 0);
				value = this._values[index];
			}

			// Token: 0x06001439 RID: 5177 RVA: 0x00074557 File Offset: 0x00072757
			public override Delegate GetGetter()
			{
				return new ValueGetter<TResult>(this.Get);
			}

			// Token: 0x04000BFB RID: 3067
			private readonly TryParseMapper<TResult> _conv;

			// Token: 0x04000BFC RID: 3068
			private TResult[] _values;
		}

		// Token: 0x020003B0 RID: 944
		private sealed class VectorPipe<TItem> : TextLoader.ColumnPipe
		{
			// Token: 0x0600143A RID: 5178 RVA: 0x00074568 File Offset: 0x00072768
			public VectorPipe(TextLoader.RowSet rows, TryParseMapper<TItem> conv)
				: base(rows)
			{
				this._conv = conv;
				this._values = new TextLoader.VectorPipe<TItem>.VectorValue[this.Rows.Count];
				for (int i = 0; i < this._values.Length; i++)
				{
					this._values[i] = new TextLoader.VectorPipe<TItem>.VectorValue(this);
				}
			}

			// Token: 0x0600143B RID: 5179 RVA: 0x000745BA File Offset: 0x000727BA
			public override void Reset(int irow, int size)
			{
				this._values[irow].Reset(size);
			}

			// Token: 0x0600143C RID: 5180 RVA: 0x000745CA File Offset: 0x000727CA
			public override bool Consume(int irow, int index, ref DvText text)
			{
				return this._values[irow].Consume(index, ref text);
			}

			// Token: 0x0600143D RID: 5181 RVA: 0x000745DC File Offset: 0x000727DC
			public void Get(ref VBuffer<TItem> dst)
			{
				int index = this.Rows.Index;
				Contracts.Check(index >= 0);
				this._values[index].Get(ref dst);
			}

			// Token: 0x0600143E RID: 5182 RVA: 0x0007460F File Offset: 0x0007280F
			public override Delegate GetGetter()
			{
				return new ValueGetter<VBuffer<TItem>>(this.Get);
			}

			// Token: 0x04000BFD RID: 3069
			private readonly TryParseMapper<TItem> _conv;

			// Token: 0x04000BFE RID: 3070
			private TextLoader.VectorPipe<TItem>.VectorValue[] _values;

			// Token: 0x020003B1 RID: 945
			private class VectorValue
			{
				// Token: 0x0600143F RID: 5183 RVA: 0x0007461D File Offset: 0x0007281D
				public VectorValue(TextLoader.VectorPipe<TItem> pipe)
				{
					this._pipe = pipe;
					this._conv = pipe._conv;
					this._values = new TItem[4];
					this._indices = new int[4];
				}

				// Token: 0x06001440 RID: 5184 RVA: 0x00074650 File Offset: 0x00072850
				[Conditional("DEBUG")]
				public void AssertValid()
				{
					if (this._size == 0)
					{
						return;
					}
					int count = this._count;
					int size = this._size;
				}

				// Token: 0x06001441 RID: 5185 RVA: 0x00074669 File Offset: 0x00072869
				public void Reset(int size)
				{
					this._size = size;
					this._count = 0;
					this._indexPrev = -1;
				}

				// Token: 0x06001442 RID: 5186 RVA: 0x00074680 File Offset: 0x00072880
				public bool Consume(int index, ref DvText text)
				{
					TItem titem = default(TItem);
					bool flag = this._conv(ref text, out titem);
					if (this._count < this._size)
					{
						if (this._count < this._size / 2)
						{
							if (this._values.Length <= this._count)
							{
								Array.Resize<TItem>(ref this._values, 2 * this._count);
							}
							if (this._indices.Length <= this._count)
							{
								Array.Resize<int>(ref this._indices, 2 * this._count);
							}
							this._values[this._count] = titem;
							this._indices[this._count] = index;
							this._count++;
							return flag;
						}
						if (this._values.Length >= this._size)
						{
							Array.Clear(this._values, this._count, this._size - this._count);
						}
						else
						{
							if (this._values.Length > this._count)
							{
								Array.Clear(this._values, this._count, this._values.Length - this._count);
							}
							Array.Resize<TItem>(ref this._values, this._size);
						}
						int num = this._count;
						while (--num >= 0)
						{
							int num2 = this._indices[num];
							if (num >= num2)
							{
								break;
							}
							this._values[num2] = this._values[num];
							this._values[num] = default(TItem);
						}
						this._count = this._size;
					}
					this._values[index] = titem;
					return flag;
				}

				// Token: 0x06001443 RID: 5187 RVA: 0x00074814 File Offset: 0x00072A14
				public void Get(ref VBuffer<TItem> dst)
				{
					TItem[] array = dst.Values;
					int[] array2 = dst.Indices;
					if (this._count == 0)
					{
						dst = new VBuffer<TItem>(this._size, 0, array, array2);
						return;
					}
					if (Utils.Size<TItem>(array) < this._count)
					{
						array = new TItem[this._count];
					}
					Array.Copy(this._values, array, this._count);
					if (this._count == this._size)
					{
						dst = new VBuffer<TItem>(this._size, array, array2);
						return;
					}
					if (Utils.Size<int>(array2) < this._count)
					{
						array2 = new int[this._count];
					}
					Array.Copy(this._indices, array2, this._count);
					dst = new VBuffer<TItem>(this._size, this._count, array, array2);
				}

				// Token: 0x04000BFF RID: 3071
				private readonly TextLoader.VectorPipe<TItem> _pipe;

				// Token: 0x04000C00 RID: 3072
				private readonly TryParseMapper<TItem> _conv;

				// Token: 0x04000C01 RID: 3073
				private int _size;

				// Token: 0x04000C02 RID: 3074
				private int _count;

				// Token: 0x04000C03 RID: 3075
				private int _indexPrev;

				// Token: 0x04000C04 RID: 3076
				private TItem[] _values;

				// Token: 0x04000C05 RID: 3077
				private int[] _indices;
			}
		}

		// Token: 0x020003B2 RID: 946
		private sealed class RowSet
		{
			// Token: 0x06001444 RID: 5188 RVA: 0x000748DF File Offset: 0x00072ADF
			public RowSet(TextLoader.ParseStats stats, int count, int ccol)
			{
				this.Stats = stats;
				this.Count = count;
				this.Pipes = new TextLoader.ColumnPipe[ccol];
				this.Index = -1;
			}

			// Token: 0x04000C06 RID: 3078
			public readonly TextLoader.ParseStats Stats;

			// Token: 0x04000C07 RID: 3079
			public readonly int Count;

			// Token: 0x04000C08 RID: 3080
			public readonly TextLoader.ColumnPipe[] Pipes;

			// Token: 0x04000C09 RID: 3081
			public int Index;
		}

		// Token: 0x020003B3 RID: 947
		private struct ScanInfo
		{
			// Token: 0x06001445 RID: 5189 RVA: 0x00074908 File Offset: 0x00072B08
			public ScanInfo(ref DvText text, string path, long line)
			{
				this = default(TextLoader.ScanInfo);
				this.Path = path;
				this.Line = line;
				this.TextBuf = text.GetRawUnderlyingBufferInfo(ref this.IchMinBuf, ref this.IchLimBuf);
				this.IchMinNext = this.IchMinBuf;
			}

			// Token: 0x04000C0A RID: 3082
			public readonly string Path;

			// Token: 0x04000C0B RID: 3083
			public readonly long Line;

			// Token: 0x04000C0C RID: 3084
			public readonly string TextBuf;

			// Token: 0x04000C0D RID: 3085
			public readonly int IchMinBuf;

			// Token: 0x04000C0E RID: 3086
			public readonly int IchLimBuf;

			// Token: 0x04000C0F RID: 3087
			public int IchMinNext;

			// Token: 0x04000C10 RID: 3088
			public DvText Span;

			// Token: 0x04000C11 RID: 3089
			public bool QuotingError;

			// Token: 0x04000C12 RID: 3090
			public int Index;

			// Token: 0x04000C13 RID: 3091
			public int IchMin;

			// Token: 0x04000C14 RID: 3092
			public int IchLim;
		}

		// Token: 0x020003B4 RID: 948
		private sealed class Parser
		{
			// Token: 0x06001446 RID: 5190 RVA: 0x00074944 File Offset: 0x00072B44
			public Parser(TextLoader parent)
			{
				this._infos = parent._bindings.Infos;
				this._creator = new Func<TextLoader.RowSet, TextLoader.ColumnPipe>[this._infos.Length];
				TextLoader.ValueCreatorCache instance = TextLoader.ValueCreatorCache.Instance;
				Dictionary<DataKind, Func<TextLoader.RowSet, TextLoader.ColumnPipe>> dictionary = new Dictionary<DataKind, Func<TextLoader.RowSet, TextLoader.ColumnPipe>>();
				Dictionary<DataKind, Func<TextLoader.RowSet, TextLoader.ColumnPipe>> dictionary2 = new Dictionary<DataKind, Func<TextLoader.RowSet, TextLoader.ColumnPipe>>();
				for (int i = 0; i < this._creator.Length; i++)
				{
					TextLoader.ColInfo colInfo = this._infos[i];
					if (colInfo.ColType.ItemType.IsKey)
					{
						if (!colInfo.ColType.IsVector)
						{
							this._creator[i] = instance.GetCreatorOne(colInfo.ColType.AsKey);
						}
						else
						{
							this._creator[i] = instance.GetCreatorVec(colInfo.ColType.ItemType.AsKey);
						}
					}
					else
					{
						DataKind rawKind = colInfo.ColType.ItemType.RawKind;
						Dictionary<DataKind, Func<TextLoader.RowSet, TextLoader.ColumnPipe>> dictionary3 = (colInfo.ColType.IsVector ? dictionary2 : dictionary);
						if (!dictionary3.TryGetValue(colInfo.Kind, out this._creator[i]))
						{
							Func<TextLoader.RowSet, TextLoader.ColumnPipe> func = (colInfo.ColType.IsVector ? instance.GetCreatorVec(colInfo.Kind) : instance.GetCreatorOne(colInfo.Kind));
							dictionary3.Add(colInfo.Kind, func);
							this._creator[i] = func;
						}
					}
				}
				this._separators = parent._separators;
				this._flags = parent._flags;
				this._inputSize = parent._inputSize;
			}

			// Token: 0x06001447 RID: 5191 RVA: 0x00074AC0 File Offset: 0x00072CC0
			public static void GetInputSize(TextLoader parent, List<DvText> lines, out int minSize, out int maxSize)
			{
				minSize = int.MaxValue;
				maxSize = 0;
				TextLoader.ParseStats parseStats = new TextLoader.ParseStats(parent._host, 1, 0L);
				TextLoader.Parser.HelperImpl helperImpl = new TextLoader.Parser.HelperImpl(parseStats, parent._flags, parent._separators, 0, int.MaxValue);
				try
				{
					foreach (DvText dvText in lines)
					{
						DvText dvText2 = (((parent._flags & TextLoader.Options.TrimWhitespace) != (TextLoader.Options)0U) ? dvText.TrimEndWhiteSpace() : dvText);
						if (dvText2.HasChars)
						{
							int num = helperImpl.GatherFields(dvText2, null, 0L);
							helperImpl.Fields.Clear();
							if (num != 0)
							{
								if (minSize > num)
								{
									minSize = num;
								}
								if (maxSize < num)
								{
									maxSize = num;
								}
							}
						}
					}
				}
				finally
				{
					parseStats.Release();
				}
			}

			// Token: 0x06001448 RID: 5192 RVA: 0x00074B9C File Offset: 0x00072D9C
			public static void ParseSlotNames(TextLoader parent, DvText textHeader, TextLoader.ColInfo[] infos, VBuffer<DvText>[] slotNames)
			{
				new StringBuilder();
				TextLoader.ParseStats parseStats = new TextLoader.ParseStats(parent._host, 1, 0L);
				TextLoader.Parser.HelperImpl helperImpl = new TextLoader.Parser.HelperImpl(parseStats, parent._flags, parent._separators, parent._inputSize, int.MaxValue);
				try
				{
					helperImpl.GatherFields(textHeader, null, 0L);
				}
				finally
				{
					parseStats.Release();
				}
				TextLoader.Parser.FieldSet fields = helperImpl.Fields;
				TextBufferBuilder textBufferBuilder = new TextBufferBuilder();
				for (int i = 0; i < infos.Length; i++)
				{
					TextLoader.ColInfo colInfo = infos[i];
					if (colInfo.ColType.IsKnownSizeVector)
					{
						textBufferBuilder.Reset(colInfo.SizeBase, false);
						int num = 0;
						for (int j = 0; j < colInfo.Segments.Length; j++)
						{
							TextLoader.Segment segment = colInfo.Segments[j];
							int min = segment.Min;
							int lim = segment.Lim;
							int num2 = lim - min;
							int k = Utils.FindIndexSorted(fields.Indices, 0, fields.Count, min);
							if (k < fields.Count && fields.Indices[k] < lim)
							{
								int num3 = num - min;
								int num4 = Utils.FindIndexSorted(fields.Indices, k, fields.Count, lim);
								while (k < num4)
								{
									int num5 = fields.Indices[k];
									textBufferBuilder.AddFeature(num3 + num5, fields.Spans[k].TrimWhiteSpace());
									k++;
								}
							}
							num += num2;
						}
						if (!textBufferBuilder.IsEmpty)
						{
							textBufferBuilder.GetResult(ref slotNames[i]);
						}
					}
				}
			}

			// Token: 0x06001449 RID: 5193 RVA: 0x00074D38 File Offset: 0x00072F38
			public TextLoader.RowSet CreateRowSet(TextLoader.ParseStats stats, int count, bool[] active)
			{
				TextLoader.RowSet rowSet = new TextLoader.RowSet(stats, count, this._creator.Length);
				for (int i = 0; i < rowSet.Pipes.Length; i++)
				{
					if (active == null || active[i])
					{
						rowSet.Pipes[i] = this._creator[i](rowSet);
					}
				}
				return rowSet;
			}

			// Token: 0x0600144A RID: 5194 RVA: 0x00074D88 File Offset: 0x00072F88
			public void ParseRow(TextLoader.RowSet rows, int irow, TextLoader.Parser.Helper helper, bool[] active, string path, long line, string text)
			{
				TextLoader.Parser.HelperImpl helperImpl = (TextLoader.Parser.HelperImpl)helper;
				DvText dvText;
				dvText..ctor(text);
				if ((this._flags & TextLoader.Options.TrimWhitespace) != (TextLoader.Options)0U)
				{
					dvText = dvText.TrimEndWhiteSpace();
				}
				try
				{
					int num = helperImpl.GatherFields(dvText, path, line);
					this.ProcessItems(rows, irow, active, helperImpl.Fields, num, line);
					rows.Stats.LogRow();
				}
				finally
				{
					helperImpl.Fields.Clear();
				}
			}

			// Token: 0x0600144B RID: 5195 RVA: 0x00074E00 File Offset: 0x00073000
			public TextLoader.Parser.Helper CreateHelper(TextLoader.ParseStats stats, int srcNeeded)
			{
				return new TextLoader.Parser.HelperImpl(stats, this._flags, this._separators, this._inputSize, srcNeeded);
			}

			// Token: 0x0600144C RID: 5196 RVA: 0x00074E1C File Offset: 0x0007301C
			private void ProcessItems(TextLoader.RowSet rows, int irow, bool[] active, TextLoader.Parser.FieldSet fields, int srcLim, long line)
			{
				for (int i = 0; i < this._infos.Length; i++)
				{
					if (active == null || active[i])
					{
						TextLoader.ColInfo colInfo = this._infos[i];
						TextLoader.ColumnPipe columnPipe = rows.Pipes[i];
						if (!colInfo.ColType.IsVector)
						{
							this.ProcessOne(fields, colInfo, columnPipe, irow, line);
						}
						else
						{
							this.ProcessVec(srcLim, fields, colInfo, columnPipe, irow, line);
						}
					}
				}
			}

			// Token: 0x0600144D RID: 5197 RVA: 0x00074E84 File Offset: 0x00073084
			private void ProcessVec(int srcLim, TextLoader.Parser.FieldSet fields, TextLoader.ColInfo info, TextLoader.ColumnPipe v, int irow, long line)
			{
				int num = 0;
				if (info.IsegVariable >= 0)
				{
					TextLoader.Segment segment = info.Segments[info.IsegVariable];
					if (segment.Min < srcLim)
					{
						num = srcLim - segment.Min;
					}
				}
				int num2 = checked(info.SizeBase + num);
				v.Reset(irow, num2);
				int num3 = 0;
				for (int i = 0; i < info.Segments.Length; i++)
				{
					TextLoader.Segment segment2 = info.Segments[i];
					int min = segment2.Min;
					int num4 = segment2.Lim;
					if (i == info.IsegVariable)
					{
						num4 = srcLim;
					}
					int num5 = num4 - min;
					int j = Utils.FindIndexSorted(fields.Indices, 0, fields.Count, min);
					if (j < fields.Count && fields.Indices[j] < num4)
					{
						int num6 = num3 - min;
						int num7 = Utils.FindIndexSorted(fields.Indices, j, fields.Count, num4);
						while (j < num7)
						{
							int num8 = fields.Indices[j];
							if (!v.Consume(irow, num6 + num8, ref fields.Spans[j]))
							{
								v.Rows.Stats.LogBadValue(line, info.Name, num6 + num8);
							}
							j++;
						}
					}
					num3 += num5;
				}
			}

			// Token: 0x0600144E RID: 5198 RVA: 0x00074FD8 File Offset: 0x000731D8
			private void ProcessOne(TextLoader.Parser.FieldSet vs, TextLoader.ColInfo info, TextLoader.ColumnPipe v, int irow, long line)
			{
				int min = info.Segments[0].Min;
				int num = Utils.FindIndexSorted(vs.Indices, 0, vs.Count, min);
				if (num < vs.Count && vs.Indices[num] == min)
				{
					if (!v.Consume(irow, 0, ref vs.Spans[num]))
					{
						v.Rows.Stats.LogBadValue(line, info.Name);
						return;
					}
				}
				else
				{
					v.Reset(irow, 0);
				}
			}

			// Token: 0x0600144F RID: 5199 RVA: 0x00075058 File Offset: 0x00073258
			private void VerifyColumnCount(int csrc)
			{
				if (csrc == this._csrc)
				{
					return;
				}
				Interlocked.CompareExchange(ref this._csrc, csrc, 0);
				if (csrc == this._csrc)
				{
					return;
				}
				if (Interlocked.Increment(ref this._mismatchCount) == 1)
				{
					Console.WriteLine("Warning: Feature count mismatch: {0} vs {1}", csrc, this._csrc);
				}
			}

			// Token: 0x04000C15 RID: 3093
			private readonly char[] _separators;

			// Token: 0x04000C16 RID: 3094
			private readonly TextLoader.Options _flags;

			// Token: 0x04000C17 RID: 3095
			private readonly int _inputSize;

			// Token: 0x04000C18 RID: 3096
			private readonly TextLoader.ColInfo[] _infos;

			// Token: 0x04000C19 RID: 3097
			private readonly Func<TextLoader.RowSet, TextLoader.ColumnPipe>[] _creator;

			// Token: 0x04000C1A RID: 3098
			private volatile int _csrc;

			// Token: 0x04000C1B RID: 3099
			private volatile int _mismatchCount;

			// Token: 0x020003B5 RID: 949
			private sealed class FieldSet
			{
				// Token: 0x06001450 RID: 5200 RVA: 0x000750B6 File Offset: 0x000732B6
				public FieldSet()
				{
					this.Spans = new DvText[8];
					this.Indices = new int[8];
				}

				// Token: 0x06001451 RID: 5201 RVA: 0x000750D6 File Offset: 0x000732D6
				[Conditional("DEBUG")]
				public void AssertValid()
				{
				}

				// Token: 0x06001452 RID: 5202 RVA: 0x000750D8 File Offset: 0x000732D8
				[Conditional("DEBUG")]
				public void AssertEmpty()
				{
				}

				// Token: 0x06001453 RID: 5203 RVA: 0x000750DC File Offset: 0x000732DC
				public void EnsureSpace()
				{
					if (this.Count >= this.Indices.Length)
					{
						int num = 2 * this.Count;
						if (this.Spans.Length < num)
						{
							Array.Resize<DvText>(ref this.Spans, num);
						}
						Array.Resize<int>(ref this.Indices, num);
					}
				}

				// Token: 0x06001454 RID: 5204 RVA: 0x00075125 File Offset: 0x00073325
				public void Clear()
				{
					Array.Clear(this.Spans, 0, this.Count);
					this.Count = 0;
				}

				// Token: 0x04000C1C RID: 3100
				public int Count;

				// Token: 0x04000C1D RID: 3101
				public int[] Indices;

				// Token: 0x04000C1E RID: 3102
				public DvText[] Spans;
			}

			// Token: 0x020003B6 RID: 950
			public abstract class Helper
			{
			}

			// Token: 0x020003B7 RID: 951
			private sealed class HelperImpl : TextLoader.Parser.Helper
			{
				// Token: 0x06001456 RID: 5206 RVA: 0x00075148 File Offset: 0x00073348
				public HelperImpl(TextLoader.ParseStats stats, TextLoader.Options flags, char[] seps, int inputSize, int srcNeeded)
				{
					this._stats = stats;
					this._seps = seps;
					this._sep0 = this._seps[0];
					this._sep1 = ((this._seps.Length > 1) ? this._seps[1] : '\0');
					this._sepContainsSpace = this.IsSep(' ');
					this._inputSize = inputSize;
					this._srcNeeded = srcNeeded;
					this._quoting = (flags & TextLoader.Options.AllowQuoting) != (TextLoader.Options)0U;
					this._sparse = (flags & TextLoader.Options.AllowSparse) != (TextLoader.Options)0U;
					this._sb = new StringBuilder();
					this._blank = (this._quoting ? DvText.NA : DvText.Empty);
					this.Fields = new TextLoader.Parser.FieldSet();
				}

				// Token: 0x06001457 RID: 5207 RVA: 0x00075200 File Offset: 0x00073400
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool IsSep(char ch)
				{
					if (ch == this._sep0)
					{
						return true;
					}
					for (int i = 1; i < this._seps.Length; i++)
					{
						if (ch == this._seps[i])
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x06001458 RID: 5208 RVA: 0x0007523C File Offset: 0x0007343C
				public int GatherFields(DvText lineSpan, string path = null, long line = 0L)
				{
					TextLoader.ScanInfo scanInfo = new TextLoader.ScanInfo(ref lineSpan, path, line);
					int num = 0;
					if (!this._sparse)
					{
						bool flag;
						do
						{
							flag = this.FetchNextField(ref scanInfo);
							if (scanInfo.QuotingError)
							{
								this._stats.LogBadFmt(ref scanInfo, "Illegal quoting");
							}
							if (!scanInfo.Span.IsEmpty)
							{
								this.Fields.EnsureSpace();
								this.Fields.Spans[this.Fields.Count] = scanInfo.Span;
								this.Fields.Indices[this.Fields.Count++] = num;
							}
						}
						while (++num <= this._srcNeeded && flag);
						return num;
					}
					int num2 = -1;
					int num3 = -1;
					int num4 = -1;
					int num5 = this._inputSize;
					int num6 = this._srcNeeded;
					for (;;)
					{
						bool flag2 = this.FetchNextField(ref scanInfo);
						if (scanInfo.QuotingError)
						{
							this._stats.LogBadFmt(ref scanInfo, "Illegal quoting");
						}
						if (scanInfo.Index < 0)
						{
							if (num4 >= 0)
							{
								break;
							}
							if (!scanInfo.Span.IsEmpty)
							{
								this.Fields.EnsureSpace();
								this.Fields.Spans[this.Fields.Count] = scanInfo.Span;
								this.Fields.Indices[this.Fields.Count++] = num;
							}
							if (num++ > num6)
							{
								goto IL_03B1;
							}
							if (!flag2)
							{
								goto Block_9;
							}
						}
						else
						{
							if (num4 < 0)
							{
								if (num5 == 0)
								{
									if (this.Fields.Count <= 0)
									{
										goto Block_12;
									}
									if (this.Fields.Indices[this.Fields.Count - 1] != num - 1)
									{
										goto Block_13;
									}
									DvText dvText = this.Fields.Spans[this.Fields.Count - 1];
									DvInt4 dvInt = default(DvInt4);
									Conversions.Instance.Convert(ref dvText, ref dvInt);
									num2 = dvInt.RawValue;
									if (num2 <= 0)
									{
										goto Block_14;
									}
									num4 = this.Fields.Indices[--this.Fields.Count];
									if (num2 >= 2147483647 - num4)
									{
										num2 = int.MaxValue - num4 - 1;
									}
									num5 = num4 + num2;
									if (num6 >= num5)
									{
										num6 = num5 - 1;
									}
								}
								else
								{
									num4 = this.Fields.Count;
									num2 = num5 - this.Fields.Count;
								}
								num = -1;
							}
							if (scanInfo.Index > num6 - num4)
							{
								if (scanInfo.Index >= num2)
								{
									goto Block_18;
								}
								if (scanInfo.Index > num6 - num4 + 1)
								{
									goto IL_03B1;
								}
							}
							if (num3 >= scanInfo.Index)
							{
								goto Block_19;
							}
							num3 = scanInfo.Index;
							if (!scanInfo.Span.IsEmpty)
							{
								this.Fields.EnsureSpace();
								this.Fields.Spans[this.Fields.Count] = scanInfo.Span;
								this.Fields.Indices[this.Fields.Count++] = num4 + scanInfo.Index;
							}
							if (!flag2)
							{
								goto IL_03B1;
							}
						}
					}
					this._stats.LogBadFmt(ref scanInfo, "Non-sparse formatted value follows sparse formatted value");
					Block_9:
					goto IL_03B1;
					Block_12:
					this._stats.LogBadFmt(ref scanInfo, "Missing dimensionality or ambiguous sparse item. Use sparse=- for non-sparse file, and/or quote the value.");
					goto IL_03B1;
					Block_13:
					this._stats.LogBadFmt(ref scanInfo, "Missing dimensionality or ambiguous sparse item. Use sparse=- for non-sparse file, and/or quote the value.");
					goto IL_03B1;
					Block_14:
					this._stats.LogBadFmt(ref scanInfo, "Bad dimensionality or ambiguous sparse item. Use sparse=- for non-sparse file, and/or quote the value.");
					goto IL_03B1;
					Block_18:
					this._stats.LogBadFmt(ref scanInfo, "Sparse item index larger than expected. Is the specified size incorrect?");
					goto IL_03B1;
					Block_19:
					this._stats.LogBadFmt(ref scanInfo, "Sparse indices out of order");
					IL_03B1:
					if (num4 < 0)
					{
						return Math.Max(num, num5);
					}
					return num5;
				}

				// Token: 0x06001459 RID: 5209 RVA: 0x0007560C File Offset: 0x0007380C
				private bool FetchNextField(ref TextLoader.ScanInfo scan)
				{
					string textBuf = scan.TextBuf;
					int ichLimBuf = scan.IchLimBuf;
					int i = scan.IchMinNext;
					if (!this._sepContainsSpace)
					{
						while (i < ichLimBuf && textBuf[i] == ' ')
						{
							i++;
						}
					}
					scan.QuotingError = false;
					scan.Index = -1;
					scan.IchMin = i;
					if (i >= ichLimBuf)
					{
						scan.IchMinNext = (scan.IchLim = i);
						scan.Span = this._blank;
						return false;
					}
					if (this._sparse && textBuf[i] - '0' <= '\t')
					{
						int num = Math.Min(ichLimBuf, i + 9);
						int num2 = i + 1;
						while (num2 < num && textBuf[num2] - '0' <= '\t')
						{
							num2++;
						}
						if (num2 < ichLimBuf && textBuf[num2] == ':')
						{
							int num3 = 0;
							for (int j = i; j < num2; j++)
							{
								num3 = num3 * 10 + (int)(textBuf[j] - '0');
							}
							i = num2 + 1;
							scan.Index = num3;
							if (!this._sepContainsSpace)
							{
								while (i < ichLimBuf && textBuf[i] == ' ')
								{
									i++;
								}
							}
							if (i >= ichLimBuf)
							{
								scan.IchMinNext = (scan.IchLim = i);
								scan.Span = this._blank;
								return false;
							}
						}
					}
					if (textBuf[i] == '"' && this._quoting)
					{
						i++;
						this._sb.Clear();
						int num4 = i;
						while (i < ichLimBuf)
						{
							if (textBuf[i] == '"')
							{
								if (i > num4)
								{
									this._sb.Append(textBuf, num4, i - num4);
								}
								if (++i < ichLimBuf && textBuf[i] == '"')
								{
									num4 = i;
								}
								else
								{
									IL_01E3:
									while (i < ichLimBuf)
									{
										if (textBuf[i] == ' ')
										{
											if (this._sepContainsSpace)
											{
												break;
											}
										}
										else
										{
											if (this.IsSep(textBuf[i]))
											{
												break;
											}
											scan.QuotingError = true;
										}
										i++;
									}
									if (scan.QuotingError)
									{
										scan.Span = DvText.NA;
										goto IL_02CC;
									}
									if (this._sb.Length == 0)
									{
										scan.Span = DvText.Empty;
										goto IL_02CC;
									}
									scan.Span = new DvText(this._sb.ToString());
									goto IL_02CC;
								}
							}
							i++;
						}
						scan.QuotingError = true;
						goto IL_01E3;
					}
					int num5 = i;
					if (this._seps.Length == 1)
					{
						while (i < ichLimBuf && this._sep0 != textBuf[i])
						{
							i++;
						}
					}
					else if (this._seps.Length == 2)
					{
						while (i < ichLimBuf && this._sep0 != textBuf[i] && this._sep1 != textBuf[i])
						{
							i++;
						}
					}
					else
					{
						while (i < ichLimBuf && !this.IsSep(textBuf[i]))
						{
							i++;
						}
					}
					if (num5 >= i)
					{
						scan.Span = this._blank;
					}
					else
					{
						scan.Span = new DvText(textBuf, num5, i);
					}
					IL_02CC:
					scan.IchLim = i;
					if (i >= ichLimBuf)
					{
						scan.IchMinNext = i;
						return false;
					}
					scan.IchMinNext = i + 1;
					return true;
				}

				// Token: 0x04000C1F RID: 3103
				private readonly TextLoader.ParseStats _stats;

				// Token: 0x04000C20 RID: 3104
				private readonly char[] _seps;

				// Token: 0x04000C21 RID: 3105
				private readonly char _sep0;

				// Token: 0x04000C22 RID: 3106
				private readonly char _sep1;

				// Token: 0x04000C23 RID: 3107
				private readonly bool _sepContainsSpace;

				// Token: 0x04000C24 RID: 3108
				private readonly int _inputSize;

				// Token: 0x04000C25 RID: 3109
				private readonly int _srcNeeded;

				// Token: 0x04000C26 RID: 3110
				private readonly bool _quoting;

				// Token: 0x04000C27 RID: 3111
				private readonly bool _sparse;

				// Token: 0x04000C28 RID: 3112
				private readonly StringBuilder _sb;

				// Token: 0x04000C29 RID: 3113
				private readonly DvText _blank;

				// Token: 0x04000C2A RID: 3114
				public readonly TextLoader.Parser.FieldSet Fields;
			}
		}
	}
}
