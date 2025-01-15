using System;
using System.Linq;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003F0 RID: 1008
	public static class WordBagTransform
	{
		// Token: 0x06001553 RID: 5459 RVA: 0x0007C638 File Offset: 0x0007A838
		public static IDataTransform Create(WordBagTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("WordBagTransform");
			Contracts.CheckValue<WordBagTransform.Arguments>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckUserArg(host, Utils.Size<WordBagTransform.Column>(args.column) > 0, "column", "Columns must be specified");
			Contracts.CheckUserArg(host, SubComponentExtensions.IsGood(args.tokenizer), "tokenizer", "tokenizer must be specified");
			WordBagTransform.TokenizeColumn[] array = new WordBagTransform.TokenizeColumn[args.column.Length];
			NgramExtractorTransform.Arguments arguments = new NgramExtractorTransform.Arguments
			{
				maxNumTerms = args.maxNumTerms,
				ngramLength = args.ngramLength,
				skipLength = args.skipLength,
				weighting = args.weighting,
				column = new NgramExtractorTransform.Column[args.column.Length]
			};
			for (int i = 0; i < args.column.Length; i++)
			{
				WordBagTransform.Column column = args.column[i];
				Contracts.CheckUserArg(host, !string.IsNullOrWhiteSpace(column.name), "name");
				Contracts.CheckUserArg(host, Utils.Size<string>(column.source) > 0, "source");
				Contracts.CheckUserArg(host, column.source.All((string src) => !string.IsNullOrWhiteSpace(src)), "source");
				array[i] = new WordBagTransform.TokenizeColumn
				{
					name = column.name,
					source = ((column.source.Length > 1) ? column.name : column.source[0])
				};
				arguments.column[i] = new NgramExtractorTransform.Column
				{
					name = column.name,
					source = column.name,
					maxNumTerms = column.maxNumTerms,
					ngramLength = column.ngramLength,
					skipLength = column.skipLength,
					weighting = column.weighting
				};
			}
			IDataView dataView = NgramExtractionUtils.ApplyConcatOnSources(host, args.column, input);
			dataView = ComponentCatalog.CreateInstance<ITokenizeTransform, SignatureTokenizeTransform>(args.tokenizer, new object[] { host, dataView, array });
			return NgramExtractorTransform.Create(arguments, host, dataView, null);
		}

		// Token: 0x04000CFF RID: 3327
		private const string RegistrationName = "WordBagTransform";

		// Token: 0x04000D00 RID: 3328
		internal const string Summary = "Produces a bag of counts of ngrams (sequences of consecutive words of length 1-n) in a given text. It does so by building a dictionary of ngrams and using the id in the dictionary as the index in the bag.";

		// Token: 0x020003F1 RID: 1009
		public sealed class Column : ManyToOneColumn
		{
			// Token: 0x06001555 RID: 5461 RVA: 0x0007C880 File Offset: 0x0007AA80
			public static WordBagTransform.Column Parse(string str)
			{
				WordBagTransform.Column column = new WordBagTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06001556 RID: 5462 RVA: 0x0007C89F File Offset: 0x0007AA9F
			public bool TryUnparse(StringBuilder sb)
			{
				return this.ngramLength == null && this.skipLength == null && Utils.Size<int>(this.maxNumTerms) <= 0 && this.weighting == null && this.TryUnparseCore(sb);
			}

			// Token: 0x04000D02 RID: 3330
			[Argument(0, HelpText = "Ngram length", ShortName = "ngram")]
			public int? ngramLength;

			// Token: 0x04000D03 RID: 3331
			[Argument(0, HelpText = "Maximum number of tokens to skip when constructing an ngram", ShortName = "skips")]
			public int? skipLength;

			// Token: 0x04000D04 RID: 3332
			[Argument(4, HelpText = "Maximum number of ngrams to store in the dictionary", ShortName = "max")]
			public int[] maxNumTerms;

			// Token: 0x04000D05 RID: 3333
			[Argument(0, HelpText = "Statistical measure used to evaluate how important a word is to a document in a corpus")]
			public NgramTransform.WeightingCriteria? weighting;
		}

		// Token: 0x020003F2 RID: 1010
		public sealed class TokenizeColumn : OneToOneColumn
		{
		}

		// Token: 0x020003F8 RID: 1016
		public sealed class Arguments : NgramExtractorTransform.ArgumentsBase
		{
			// Token: 0x04000D11 RID: 3345
			[Argument(4, HelpText = "New column definition(s) (optional form: name:srcs)", ShortName = "col", SortOrder = 1)]
			public WordBagTransform.Column[] column;

			// Token: 0x04000D12 RID: 3346
			[Argument(4, HelpText = "Tokenizer to use", ShortName = "tok")]
			public SubComponent<ITokenizeTransform, SignatureTokenizeTransform> tokenizer = new SubComponent<ITokenizeTransform, SignatureTokenizeTransform>("Token");
		}
	}
}
