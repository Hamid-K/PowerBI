using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003FE RID: 1022
	public static class WordHashBagTransform
	{
		// Token: 0x06001570 RID: 5488 RVA: 0x0007D160 File Offset: 0x0007B360
		public static IDataTransform Create(WordHashBagTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("WordHashBagTransform");
			Contracts.CheckValue<WordHashBagTransform.Arguments>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckUserArg(host, Utils.Size<WordHashBagTransform.Column>(args.column) > 0, "column", "Columns must be specified");
			Contracts.CheckUserArg(host, SubComponentExtensions.IsGood(args.tokenizer), "tokenizer", "tokenizer must be specified");
			string[][] array = NgramExtractionUtils.GenerateUniqueSourceNames(host, args.column, input.Schema);
			List<WordBagTransform.TokenizeColumn> list = new List<WordBagTransform.TokenizeColumn>();
			NgramHashExtractorTransform.Column[] array2 = new NgramHashExtractorTransform.Column[args.column.Length];
			int num = args.column.Length;
			List<string> list2 = new List<string>();
			for (int i = 0; i < num; i++)
			{
				WordHashBagTransform.Column column = args.column[i];
				int num2 = column.source.Length;
				string[] array3 = new string[num2];
				for (int j = 0; j < num2; j++)
				{
					list.Add(new WordBagTransform.TokenizeColumn
					{
						name = (array3[j] = array[i][j]),
						source = args.column[i].source[j]
					});
				}
				list2.AddRange(array3);
				array2[i] = new NgramHashExtractorTransform.Column
				{
					name = column.name,
					source = array3,
					hashBits = column.hashBits,
					ngramLength = column.ngramLength,
					seed = column.seed,
					skipLength = column.skipLength,
					ordered = column.ordered,
					invertHash = column.invertHash,
					friendlyNames = args.column[i].source,
					allLengths = column.allLengths
				};
			}
			IDataView dataView = ComponentCatalog.CreateInstance<ITokenizeTransform, SignatureTokenizeTransform>(args.tokenizer, new object[]
			{
				host,
				input,
				list.ToArray()
			});
			NgramHashExtractorTransform.Arguments arguments = new NgramHashExtractorTransform.Arguments
			{
				allLengths = args.allLengths,
				hashBits = args.hashBits,
				ngramLength = args.ngramLength,
				skipLength = args.skipLength,
				ordered = args.ordered,
				seed = args.seed,
				column = array2.ToArray<NgramHashExtractorTransform.Column>(),
				invertHash = args.invertHash
			};
			dataView = NgramHashExtractorTransform.Create(arguments, host, dataView, null);
			DropColumnsTransform.Arguments arguments2 = new DropColumnsTransform.Arguments
			{
				column = list2.ToArray()
			};
			return new DropColumnsTransform(arguments2, host, dataView);
		}

		// Token: 0x04000D1F RID: 3359
		private const string RegistrationName = "WordHashBagTransform";

		// Token: 0x04000D20 RID: 3360
		internal const string Summary = "Produces a bag of counts of ngrams (sequences of consecutive words of length 1-n) in a given text. It does so by hashing each ngram and using the hash value as the index in the bag.";

		// Token: 0x02000405 RID: 1029
		public sealed class Column : NgramHashExtractorTransform.ColumnBase
		{
			// Token: 0x0600157D RID: 5501 RVA: 0x0007DAC4 File Offset: 0x0007BCC4
			public static WordHashBagTransform.Column Parse(string str)
			{
				WordHashBagTransform.Column column = new WordHashBagTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x0600157E RID: 5502 RVA: 0x0007DAE4 File Offset: 0x0007BCE4
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

			// Token: 0x0600157F RID: 5503 RVA: 0x0007DB1C File Offset: 0x0007BD1C
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
		}

		// Token: 0x02000406 RID: 1030
		public sealed class Arguments : NgramHashExtractorTransform.ArgumentsBase
		{
			// Token: 0x04000D34 RID: 3380
			[Argument(4, HelpText = "New column definition(s) (optional form: name:hashBits:srcs)", ShortName = "col", SortOrder = 1)]
			public WordHashBagTransform.Column[] column;

			// Token: 0x04000D35 RID: 3381
			[Argument(4, HelpText = "Tokenizer to use", ShortName = "tok")]
			public SubComponent<ITokenizeTransform, SignatureTokenizeTransform> tokenizer = new SubComponent<ITokenizeTransform, SignatureTokenizeTransform>("Token");
		}
	}
}
