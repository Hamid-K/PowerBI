using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003FF RID: 1023
	public static class NgramHashExtractorTransform
	{
		// Token: 0x06001571 RID: 5489 RVA: 0x0007D414 File Offset: 0x0007B614
		public static IDataTransform Create(NgramHashExtractorTransform.Arguments args, IHostEnvironment env, IDataView input, TermLoaderArguments termLoaderArgs = null)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("NgramHashExtractor");
			Contracts.CheckValue<NgramHashExtractorTransform.Arguments>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckUserArg(host, Utils.Size<NgramHashExtractorTransform.Column>(args.column) > 0, "column", "Columns must be specified");
			IDataView dataView = input;
			List<TermTransform.Column> list = null;
			if (termLoaderArgs != null)
			{
				list = new List<TermTransform.Column>();
			}
			List<HashTransform.Column> list2 = new List<HashTransform.Column>();
			NgramHashTransform.Column[] array = new NgramHashTransform.Column[args.column.Length];
			int num = args.column.Length;
			for (int i = 0; i < num; i++)
			{
				NgramHashExtractorTransform.Column column = args.column[i];
				Contracts.CheckUserArg(host, !string.IsNullOrWhiteSpace(column.name), "name");
				IExceptionContext exceptionContext = host;
				bool flag;
				if (Utils.Size<string>(column.source) > 0)
				{
					flag = column.source.All((string src) => !string.IsNullOrWhiteSpace(src));
				}
				else
				{
					flag = false;
				}
				Contracts.CheckUserArg(exceptionContext, flag, "source");
				int num2 = column.source.Length;
				for (int j = 0; j < num2; j++)
				{
					if (termLoaderArgs != null)
					{
						list.Add(new TermTransform.Column
						{
							name = column.source[j],
							source = column.source[j]
						});
					}
					list2.Add(new HashTransform.Column
					{
						name = column.source[j],
						source = column.source[j],
						hashBits = new int?(30),
						seed = column.seed,
						ordered = new bool?(false),
						invertHash = column.invertHash
					});
				}
				array[i] = new NgramHashTransform.Column
				{
					name = column.name,
					source = column.source,
					allLengths = column.allLengths,
					hashBits = column.hashBits,
					ngramLength = column.ngramLength,
					rehashUnigrams = new bool?(false),
					seed = column.seed,
					skipLength = column.skipLength,
					ordered = column.ordered,
					invertHash = column.invertHash,
					friendlyNames = column.friendlyNames
				};
			}
			if (termLoaderArgs != null)
			{
				TermTransform.Arguments arguments = new TermTransform.Arguments
				{
					maxNumTerms = int.MaxValue,
					terms = termLoaderArgs.terms,
					dataFile = termLoaderArgs.dataFile,
					loader = termLoaderArgs.loader,
					termsColumn = termLoaderArgs.termsColumn,
					sort = termLoaderArgs.sort,
					column = list.ToArray()
				};
				dataView = new TermTransform(arguments, host, dataView);
				if (termLoaderArgs.dropUnknowns)
				{
					NADropTransform.Arguments arguments2 = new NADropTransform.Arguments
					{
						column = new NADropTransform.Column[list.Count]
					};
					for (int k = 0; k < list.Count; k++)
					{
						arguments2.column[k] = new NADropTransform.Column
						{
							name = list[k].name,
							source = list[k].source
						};
					}
					dataView = new NADropTransform(arguments2, host, dataView);
				}
			}
			HashTransform.Arguments arguments3 = new HashTransform.Arguments
			{
				hashBits = 31,
				seed = args.seed,
				ordered = false,
				column = list2.ToArray(),
				invertHash = args.invertHash
			};
			dataView = new HashTransform(arguments3, host, dataView);
			NgramHashTransform.Arguments arguments4 = new NgramHashTransform.Arguments
			{
				allLengths = args.allLengths,
				hashBits = args.hashBits,
				ngramLength = args.ngramLength,
				skipLength = args.skipLength,
				rehashUnigrams = false,
				ordered = args.ordered,
				seed = args.seed,
				column = array,
				invertHash = args.invertHash
			};
			return new NgramHashTransform(arguments4, host, dataView);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0007D844 File Offset: 0x0007BA44
		public static IDataTransform Create(NgramHashExtractorTransform.NgramHashExtractorArguments extractorArgs, IHostEnvironment env, IDataView input, ExtractorColumn[] cols, TermLoaderArguments termLoaderArgs = null)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("NgramHashExtractor");
			Contracts.CheckValue<NgramHashExtractorTransform.NgramHashExtractorArguments>(host, extractorArgs, "extractorArgs");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckUserArg(host, Utils.Size<ExtractorColumn>(cols) > 0, "cols", "cols must be specified");
			NgramHashExtractorTransform.Column[] array = new NgramHashExtractorTransform.Column[cols.Length];
			for (int i = 0; i < cols.Length; i++)
			{
				array[i] = new NgramHashExtractorTransform.Column
				{
					name = cols[i].name,
					source = cols[i].source,
					friendlyNames = cols[i].friendlyNames
				};
			}
			NgramHashExtractorTransform.Arguments arguments = new NgramHashExtractorTransform.Arguments
			{
				column = array,
				ngramLength = extractorArgs.ngramLength,
				skipLength = extractorArgs.skipLength,
				hashBits = extractorArgs.hashBits,
				invertHash = extractorArgs.invertHash,
				ordered = extractorArgs.ordered,
				seed = extractorArgs.seed,
				allLengths = extractorArgs.allLengths
			};
			return NgramHashExtractorTransform.Create(arguments, host, input, termLoaderArgs);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0007D960 File Offset: 0x0007BB60
		public static INgramExtractorFactory Create(NgramHashExtractorTransform.NgramHashExtractorArguments extractorArgs, IHostEnvironment env, TermLoaderArguments termLoaderArgs)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("NgramHashExtractor");
			Contracts.CheckValue<NgramHashExtractorTransform.NgramHashExtractorArguments>(host, extractorArgs, "extractorArgs");
			return new NgramHashExtractorFactory(extractorArgs, termLoaderArgs);
		}

		// Token: 0x04000D21 RID: 3361
		internal const string Summary = "A transform\u00a0that turns a collection of tokenized text (vector of DvText) into numerical feature vectors using the hashing trick.";

		// Token: 0x04000D22 RID: 3362
		internal const string LoaderSignature = "NgramHashExtractor";

		// Token: 0x02000400 RID: 1024
		public abstract class ColumnBase : ManyToOneColumn
		{
			// Token: 0x04000D24 RID: 3364
			[Argument(0, HelpText = "Ngram length (stores all lengths up to the specified Ngram length)", ShortName = "ngram")]
			public int? ngramLength;

			// Token: 0x04000D25 RID: 3365
			[Argument(0, HelpText = "Maximum number of tokens to skip when constructing an ngram", ShortName = "skips")]
			public int? skipLength;

			// Token: 0x04000D26 RID: 3366
			[Argument(0, HelpText = "Number of bits to hash into. Must be between 1 and 30, inclusive.", ShortName = "bits")]
			public int? hashBits;

			// Token: 0x04000D27 RID: 3367
			[Argument(0, HelpText = "Hashing seed")]
			public uint? seed;

			// Token: 0x04000D28 RID: 3368
			[Argument(0, HelpText = "Whether the position of each source column should be included in the hash (when there are multiple source columns).", ShortName = "ord")]
			public bool? ordered;

			// Token: 0x04000D29 RID: 3369
			[Argument(0, HelpText = "Limit the number of keys used to generate the slot name to this many. 0 means no invert hashing, -1 means no limit.", ShortName = "ih")]
			public int? invertHash;

			// Token: 0x04000D2A RID: 3370
			[Argument(0, HelpText = "Whether to include all ngram lengths up to ngramLength, or only ngramLength", ShortName = "all", SortOrder = 4)]
			public bool? allLengths;
		}

		// Token: 0x02000401 RID: 1025
		public sealed class Column : NgramHashExtractorTransform.ColumnBase
		{
			// Token: 0x06001576 RID: 5494 RVA: 0x0007D9A0 File Offset: 0x0007BBA0
			public static NgramHashExtractorTransform.Column Parse(string str)
			{
				NgramHashExtractorTransform.Column column = new NgramHashExtractorTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06001577 RID: 5495 RVA: 0x0007D9C0 File Offset: 0x0007BBC0
			protected override bool TryParse(string str)
			{
				string text;
				if (!base.TryParse(str, out text))
				{
					return false;
				}
				if (text == null)
				{
					return true;
				}
				int num;
				if (!int.TryParse(text, out num))
				{
					return false;
				}
				this.hashBits = new int?(num);
				return true;
			}

			// Token: 0x06001578 RID: 5496 RVA: 0x0007D9F8 File Offset: 0x0007BBF8
			public bool TryUnparse(StringBuilder sb)
			{
				if (this.ngramLength != null || this.skipLength != null || this.seed != null || this.ordered != null || this.invertHash != null)
				{
					return false;
				}
				if (this.hashBits == null)
				{
					return this.TryUnparseCore(sb);
				}
				string text = this.hashBits.Value.ToString();
				return this.TryUnparseCore(sb, text);
			}

			// Token: 0x04000D2B RID: 3371
			public string[] friendlyNames;
		}

		// Token: 0x02000402 RID: 1026
		public abstract class ArgumentsBase
		{
			// Token: 0x04000D2C RID: 3372
			[Argument(0, HelpText = "Ngram length", ShortName = "ngram", SortOrder = 3)]
			public int ngramLength = 1;

			// Token: 0x04000D2D RID: 3373
			[Argument(0, HelpText = "Maximum number of tokens to skip when constructing an ngram", ShortName = "skips", SortOrder = 4)]
			public int skipLength;

			// Token: 0x04000D2E RID: 3374
			[Argument(0, HelpText = "Number of bits to hash into. Must be between 1 and 30, inclusive.", ShortName = "bits", SortOrder = 2)]
			public int hashBits = 16;

			// Token: 0x04000D2F RID: 3375
			[Argument(0, HelpText = "Hashing seed")]
			public uint seed = 314489979U;

			// Token: 0x04000D30 RID: 3376
			[Argument(0, HelpText = "Whether the position of each source column should be included in the hash (when there are multiple source columns).", ShortName = "ord")]
			public bool ordered = true;

			// Token: 0x04000D31 RID: 3377
			[Argument(0, HelpText = "Limit the number of keys used to generate the slot name to this many. 0 means no invert hashing, -1 means no limit.", ShortName = "ih")]
			public int invertHash;

			// Token: 0x04000D32 RID: 3378
			[Argument(0, HelpText = "Whether to include all ngram lengths up to ngramLength, or only ngramLength", ShortName = "all", SortOrder = 4)]
			public bool allLengths = true;
		}

		// Token: 0x02000403 RID: 1027
		public sealed class NgramHashExtractorArguments : NgramHashExtractorTransform.ArgumentsBase
		{
		}

		// Token: 0x02000404 RID: 1028
		public sealed class Arguments : NgramHashExtractorTransform.ArgumentsBase
		{
			// Token: 0x04000D33 RID: 3379
			[Argument(4, HelpText = "New column definition(s) (optional form: name:srcs)", ShortName = "col", SortOrder = 1)]
			public NgramHashExtractorTransform.Column[] column;
		}
	}
}
