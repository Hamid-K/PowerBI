using System;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200025A RID: 602
	public static class CategoricalHashTransform
	{
		// Token: 0x06000D70 RID: 3440 RVA: 0x0004AA44 File Offset: 0x00048C44
		public static IDataTransform Create(CategoricalHashTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("CategoricalHash");
			Contracts.CheckValue<CategoricalHashTransform.Arguments>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckUserArg(host, Utils.Size<CategoricalHashTransform.Column>(args.column) > 0, "column", "Columns must be specified");
			if (args.hashBits < 1 || args.hashBits >= 31)
			{
				throw Contracts.ExceptUserArg(host, "hashBits", "Number of bits must be between 1 and {0}", new object[] { 30 });
			}
			HashTransform.Arguments arguments = new HashTransform.Arguments
			{
				hashBits = args.hashBits,
				seed = args.seed,
				ordered = args.ordered,
				invertHash = args.invertHash,
				column = new HashTransform.Column[args.column.Length]
			};
			for (int i = 0; i < args.column.Length; i++)
			{
				CategoricalHashTransform.Column column = args.column[i];
				if (!column.TrySanitize())
				{
					throw Contracts.ExceptUserArg(host, "name");
				}
				arguments.column[i] = new HashTransform.Column
				{
					hashBits = column.hashBits,
					seed = column.seed,
					ordered = column.ordered,
					name = column.name,
					source = column.source,
					invertHash = column.invertHash
				};
			}
			HashTransform hashTransform = new HashTransform(arguments, host, input);
			KeyToVectorTransform.Arguments arguments2 = new KeyToVectorTransform.Arguments
			{
				bag = args.bag,
				column = new KeyToVectorTransform.Column[args.column.Length]
			};
			for (int j = 0; j < args.column.Length; j++)
			{
				CategoricalHashTransform.Column column2 = args.column[j];
				arguments2.column[j] = new KeyToVectorTransform.Column
				{
					name = column2.name,
					source = column2.name,
					bag = column2.bag
				};
			}
			return new KeyToVectorTransform(arguments2, host, hashTransform);
		}

		// Token: 0x04000794 RID: 1940
		public const int NumBitsLim = 31;

		// Token: 0x04000795 RID: 1941
		internal const string Summary = "Converts the categorical value into an indicator array by hashing the value and using the hash as an index in the bag. If the input column is a vector, a single indicator bag is returned for it.";

		// Token: 0x0200025F RID: 607
		public sealed class Column : KeyToVectorTransform.ColumnBase
		{
			// Token: 0x06000D86 RID: 3462 RVA: 0x0004B840 File Offset: 0x00049A40
			public static CategoricalHashTransform.Column Parse(string str)
			{
				CategoricalHashTransform.Column column = new CategoricalHashTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000D87 RID: 3463 RVA: 0x0004B860 File Offset: 0x00049A60
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

			// Token: 0x06000D88 RID: 3464 RVA: 0x0004B898 File Offset: 0x00049A98
			public bool TryUnparse(StringBuilder sb)
			{
				if (this.seed != null || this.ordered != null || this.invertHash != null)
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

			// Token: 0x0400079F RID: 1951
			[Argument(0, HelpText = "The number of bits to hash into. Must be between 1 and 30, inclusive.", ShortName = "bits")]
			public int? hashBits;

			// Token: 0x040007A0 RID: 1952
			[Argument(0, HelpText = "Hashing seed")]
			public uint? seed;

			// Token: 0x040007A1 RID: 1953
			[Argument(0, HelpText = "Whether the position of each term should be included in the hash", ShortName = "ord")]
			public bool? ordered;

			// Token: 0x040007A2 RID: 1954
			[Argument(0, HelpText = "Limit the number of keys used to generate the slot name to this many. 0 means no invert hashing, -1 means no limit.", ShortName = "ih")]
			public int? invertHash;
		}

		// Token: 0x02000260 RID: 608
		public sealed class Arguments
		{
			// Token: 0x040007A3 RID: 1955
			[Argument(4, HelpText = "New column definition(s) (optional form: name:hashBits:src)", ShortName = "col", SortOrder = 1)]
			public CategoricalHashTransform.Column[] column;

			// Token: 0x040007A4 RID: 1956
			[Argument(0, HelpText = "Number of bits to hash into. Must be between 1 and 30, inclusive.", ShortName = "bits", SortOrder = 2)]
			public int hashBits = 16;

			// Token: 0x040007A5 RID: 1957
			[Argument(0, HelpText = "Hashing seed")]
			public uint seed = 314489979U;

			// Token: 0x040007A6 RID: 1958
			[Argument(0, HelpText = "Whether the position of each term should be included in the hash", ShortName = "ord")]
			public bool ordered = true;

			// Token: 0x040007A7 RID: 1959
			[Argument(0, HelpText = "Limit the number of keys used to generate the slot name to this many. 0 means no invert hashing, -1 means no limit.", ShortName = "ih")]
			public int invertHash;

			// Token: 0x040007A8 RID: 1960
			[Argument(0, HelpText = "Whether to combine multiple indicator vectors into a single bag vector instead of concatenating them. This is only relevant when the input is a vector.")]
			public bool bag = true;
		}
	}
}
