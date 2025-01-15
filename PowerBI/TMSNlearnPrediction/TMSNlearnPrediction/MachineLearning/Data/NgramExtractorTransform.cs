using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003F3 RID: 1011
	public static class NgramExtractorTransform
	{
		// Token: 0x06001559 RID: 5465 RVA: 0x0007C8F0 File Offset: 0x0007AAF0
		public static IDataTransform Create(NgramExtractorTransform.Arguments args, IHostEnvironment env, IDataView input, TermLoaderArguments termLoaderArgs = null)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("NgramExtractor");
			Contracts.CheckValue<NgramExtractorTransform.Arguments>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckUserArg(host, Utils.Size<NgramExtractorTransform.Column>(args.column) > 0, "column", "Columns must be specified");
			IDataView dataView = input;
			List<NgramExtractorTransform.Column> list = new List<NgramExtractorTransform.Column>();
			foreach (NgramExtractorTransform.Column column2 in args.column)
			{
				Contracts.CheckUserArg(host, !string.IsNullOrWhiteSpace(column2.name), "name");
				Contracts.CheckUserArg(host, !string.IsNullOrWhiteSpace(column2.source), "source");
				int num;
				if (input.Schema.TryGetColumnIndex(column2.source, ref num) && input.Schema.GetColumnType(num).ItemType.IsText)
				{
					list.Add(column2);
				}
			}
			if (list.Count > 0)
			{
				NADropTransform.Arguments arguments = null;
				TermTransform.Arguments arguments2;
				if (termLoaderArgs != null)
				{
					arguments2 = new TermTransform.Arguments
					{
						maxNumTerms = int.MaxValue,
						terms = termLoaderArgs.terms,
						dataFile = termLoaderArgs.dataFile,
						loader = termLoaderArgs.loader,
						termsColumn = termLoaderArgs.termsColumn,
						sort = termLoaderArgs.sort,
						column = new TermTransform.Column[list.Count]
					};
					if (termLoaderArgs.dropUnknowns)
					{
						arguments = new NADropTransform.Arguments
						{
							column = new NADropTransform.Column[list.Count]
						};
					}
				}
				else
				{
					arguments2 = new TermTransform.Arguments
					{
						maxNumTerms = ((Utils.Size<int>(args.maxNumTerms) > 0) ? args.maxNumTerms[0] : 10000000),
						column = new TermTransform.Column[list.Count]
					};
				}
				for (int j = 0; j < list.Count; j++)
				{
					NgramExtractorTransform.Column column3 = list[j];
					arguments2.column[j] = new TermTransform.Column
					{
						name = column3.name,
						source = column3.name,
						maxNumTerms = ((Utils.Size<int>(column3.maxNumTerms) > 0) ? new int?(column3.maxNumTerms[0]) : null)
					};
					if (arguments != null)
					{
						arguments.column[j] = new NADropTransform.Column
						{
							name = column3.name,
							source = column3.name
						};
					}
				}
				dataView = new TermTransform(arguments2, host, dataView);
				if (arguments != null)
				{
					dataView = new NADropTransform(arguments, host, dataView);
				}
			}
			NgramTransform.Arguments arguments3 = new NgramTransform.Arguments
			{
				allLengths = true,
				maxNumTerms = args.maxNumTerms,
				ngramLength = args.ngramLength,
				skipLength = args.skipLength,
				weighting = args.weighting,
				column = new NgramTransform.Column[args.column.Length]
			};
			for (int k = 0; k < args.column.Length; k++)
			{
				NgramExtractorTransform.Column column4 = args.column[k];
				arguments3.column[k] = new NgramTransform.Column
				{
					name = column4.name,
					source = column4.name,
					allLengths = new bool?(true),
					maxNumTerms = column4.maxNumTerms,
					ngramLength = column4.ngramLength,
					skipLength = column4.skipLength,
					weighting = column4.weighting
				};
			}
			return new NgramTransform(arguments3, host, dataView);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0007CC94 File Offset: 0x0007AE94
		public static IDataTransform Create(NgramExtractorTransform.NgramExtractorArguments extractorArgs, IHostEnvironment env, IDataView input, ExtractorColumn[] cols, TermLoaderArguments termLoaderArgs = null)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("NgramExtractor");
			Contracts.CheckValue<NgramExtractorTransform.NgramExtractorArguments>(host, extractorArgs, "extractorArgs");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckUserArg(host, Utils.Size<ExtractorColumn>(cols) > 0, "cols", "cols must be specified");
			NgramExtractorTransform.Column[] array = new NgramExtractorTransform.Column[cols.Length];
			for (int i = 0; i < cols.Length; i++)
			{
				Contracts.Check(Utils.Size<string>(cols[i].source) == 1, "too many source columns");
				array[i] = new NgramExtractorTransform.Column
				{
					name = cols[i].name,
					source = cols[i].source[0]
				};
			}
			NgramExtractorTransform.Arguments arguments = new NgramExtractorTransform.Arguments
			{
				column = array,
				ngramLength = extractorArgs.ngramLength,
				skipLength = extractorArgs.skipLength,
				maxNumTerms = extractorArgs.maxNumTerms,
				weighting = extractorArgs.weighting
			};
			return NgramExtractorTransform.Create(arguments, host, input, termLoaderArgs);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0007CD94 File Offset: 0x0007AF94
		public static INgramExtractorFactory Create(NgramExtractorTransform.NgramExtractorArguments extractorArgs, IHostEnvironment env, TermLoaderArguments termLoaderArgs)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("NgramExtractor");
			Contracts.CheckValue<NgramExtractorTransform.NgramExtractorArguments>(host, extractorArgs, "extractorArgs");
			return new NgramExtractorFactory(extractorArgs, termLoaderArgs);
		}

		// Token: 0x04000D06 RID: 3334
		internal const string Summary = "A transform\u00a0that turns a collection of tokenized text (vector of DvText), or vectors of keys into numerical feature vectors. The feature vectors are counts of ngrams (sequences of consecutive *tokens* -words or keys- of length 1-n).";

		// Token: 0x04000D07 RID: 3335
		internal const string LoaderSignature = "NgramExtractor";

		// Token: 0x020003F4 RID: 1012
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x0600155C RID: 5468 RVA: 0x0007CDCC File Offset: 0x0007AFCC
			public static NgramExtractorTransform.Column Parse(string str)
			{
				NgramExtractorTransform.Column column = new NgramExtractorTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x0600155D RID: 5469 RVA: 0x0007CDEB File Offset: 0x0007AFEB
			public bool TryUnparse(StringBuilder sb)
			{
				return this.ngramLength == null && this.skipLength == null && Utils.Size<int>(this.maxNumTerms) <= 0 && this.weighting == null && this.TryUnparseCore(sb);
			}

			// Token: 0x04000D08 RID: 3336
			[Argument(0, HelpText = "Ngram length (stores all lengths up to the specified Ngram length)", ShortName = "ngram")]
			public int? ngramLength;

			// Token: 0x04000D09 RID: 3337
			[Argument(0, HelpText = "Maximum number of tokens to skip when constructing an ngram", ShortName = "skips")]
			public int? skipLength;

			// Token: 0x04000D0A RID: 3338
			[Argument(4, HelpText = "Maximum number of ngrams to store in the dictionary", ShortName = "max")]
			public int[] maxNumTerms;

			// Token: 0x04000D0B RID: 3339
			[Argument(0, HelpText = "The weighting criteria")]
			public NgramTransform.WeightingCriteria? weighting;
		}

		// Token: 0x020003F5 RID: 1013
		public abstract class ArgumentsBase
		{
			// Token: 0x04000D0C RID: 3340
			[Argument(0, HelpText = "Ngram length", ShortName = "ngram")]
			public int ngramLength = 1;

			// Token: 0x04000D0D RID: 3341
			[Argument(0, HelpText = "Maximum number of tokens to skip when constructing an ngram", ShortName = "skips")]
			public int skipLength;

			// Token: 0x04000D0E RID: 3342
			[Argument(4, HelpText = "Maximum number of ngrams to store in the dictionary", ShortName = "max")]
			public int[] maxNumTerms = new int[] { 10000000 };

			// Token: 0x04000D0F RID: 3343
			[Argument(0, HelpText = "The weighting criteria")]
			public NgramTransform.WeightingCriteria weighting;
		}

		// Token: 0x020003F6 RID: 1014
		public sealed class NgramExtractorArguments : NgramExtractorTransform.ArgumentsBase
		{
		}

		// Token: 0x020003F7 RID: 1015
		public sealed class Arguments : NgramExtractorTransform.ArgumentsBase
		{
			// Token: 0x04000D10 RID: 3344
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public NgramExtractorTransform.Column[] column;
		}
	}
}
