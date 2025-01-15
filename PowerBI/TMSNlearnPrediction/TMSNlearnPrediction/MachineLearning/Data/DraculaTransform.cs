using System;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Dracula;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002DD RID: 733
	public static class DraculaTransform
	{
		// Token: 0x060010B1 RID: 4273 RVA: 0x0005D238 File Offset: 0x0005B438
		public static IDataTransform Create(DraculaTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("DraculaTransform");
			Contracts.CheckValue<DraculaTransform.Arguments>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckUserArg(host, Utils.Size<DraculaTransform.Column>(args.column) > 0, "column", "Columns must be specified");
			Contracts.CheckUserArg(host, !string.IsNullOrWhiteSpace(args.labelColumn), "labelColumn", "Must specify the label column name");
			HashJoinTransform.Arguments arguments = new HashJoinTransform.Arguments
			{
				hashBits = args.hashBits,
				join = args.join,
				seed = args.seed,
				ordered = false
			};
			arguments.column = new HashJoinTransform.Column[args.column.Length];
			for (int i = 0; i < args.column.Length; i++)
			{
				DraculaTransform.Column column = args.column[i];
				if (!column.TrySanitize())
				{
					throw Contracts.ExceptUserArg(host, "name");
				}
				arguments.column[i] = new HashJoinTransform.Column
				{
					join = column.join,
					customSlotMap = column.customSlotMap,
					name = column.name,
					source = column.source
				};
			}
			HashJoinTransform hashJoinTransform = new HashJoinTransform(arguments, host, input);
			CountTableTransform.Arguments arguments2 = new CountTableTransform.Arguments
			{
				countTable = args.countTable,
				featurizer = args.featurizer,
				labelColumn = args.labelColumn,
				externalCountsFile = args.externalCountsFile,
				externalCountsSchema = args.externalCountsSchema,
				sharedTable = args.sharedTable,
				column = new CountTableTransform.Column[args.column.Length]
			};
			for (int j = 0; j < args.column.Length; j++)
			{
				DraculaTransform.Column column2 = args.column[j];
				arguments2.column[j] = new CountTableTransform.Column
				{
					name = column2.name,
					source = column2.name,
					countTable = column2.countTable,
					featurizer = column2.featurizer
				};
			}
			return new CountTableTransform(arguments2, host, hashJoinTransform);
		}

		// Token: 0x04000968 RID: 2408
		private const string RegistrationName = "DraculaTransform";

		// Token: 0x04000969 RID: 2409
		internal const string Summary = "Transforms the categorical column into the set of features: count of each label class, log-odds for each label class, back-off indicator. The columns can be of arbitrary type.";

		// Token: 0x020002DE RID: 734
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x060010B2 RID: 4274 RVA: 0x0005D460 File Offset: 0x0005B660
			public static DraculaTransform.Column Parse(string str)
			{
				DraculaTransform.Column column = new DraculaTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x060010B3 RID: 4275 RVA: 0x0005D47F File Offset: 0x0005B67F
			public bool TryUnparse(StringBuilder sb)
			{
				return !SubComponentExtensions.IsGood(this.countTable) && !SubComponentExtensions.IsGood(this.featurizer) && this.join == null && string.IsNullOrEmpty(this.customSlotMap) && this.TryUnparseCore(sb);
			}

			// Token: 0x0400096A RID: 2410
			[Argument(4, HelpText = "Count table settings", ShortName = "table")]
			public SubComponent<ICountTableBuilder, SignatureCountTableBuilder> countTable;

			// Token: 0x0400096B RID: 2411
			[Argument(4, HelpText = "Featurizer for counts", ShortName = "feat")]
			public SubComponent<ICountFeaturizer, SignatureCountFeaturizer> featurizer;

			// Token: 0x0400096C RID: 2412
			[Argument(0, HelpText = "Whether the values need to be combined for a single hash")]
			public bool? join;

			// Token: 0x0400096D RID: 2413
			[Argument(0, HelpText = "Which slots should be combined together. Example: 0,3,5;0,1;3;2,1,0. Overrides 'join'.")]
			public string customSlotMap;
		}

		// Token: 0x020002DF RID: 735
		public sealed class Arguments
		{
			// Token: 0x0400096E RID: 2414
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public DraculaTransform.Column[] column;

			// Token: 0x0400096F RID: 2415
			[Argument(4, HelpText = "Count table settings", ShortName = "table")]
			public SubComponent<ICountTableBuilder, SignatureCountTableBuilder> countTable = new SubComponent<ICountTableBuilder, SignatureCountTableBuilder>("CMSketch");

			// Token: 0x04000970 RID: 2416
			[Argument(4, HelpText = "Featurizer for counts", ShortName = "feat")]
			public SubComponent<ICountFeaturizer, SignatureCountFeaturizer> featurizer = new SubComponent<ICountFeaturizer, SignatureCountFeaturizer>("Dracula");

			// Token: 0x04000971 RID: 2417
			[Argument(1, HelpText = "Label column", ShortName = "label,lab", SortOrder = 2, Purpose = "ColumnName")]
			public string labelColumn;

			// Token: 0x04000972 RID: 2418
			[Argument(0, HelpText = "Optional text file to load counts from", ShortName = "extfile")]
			public string externalCountsFile;

			// Token: 0x04000973 RID: 2419
			[Argument(0, HelpText = "Comma-separated list of column IDs in the external count file", ShortName = "extschema")]
			public string externalCountsSchema;

			// Token: 0x04000974 RID: 2420
			[Argument(0, HelpText = "Whether the values need to be combined for a single hash", SortOrder = 3)]
			public bool join = true;

			// Token: 0x04000975 RID: 2421
			[Argument(0, HelpText = "Number of bits to hash into. Must be between 1 and 31, inclusive.", ShortName = "bits")]
			public int hashBits = 31;

			// Token: 0x04000976 RID: 2422
			[Argument(0, HelpText = "Hashing seed")]
			public uint seed = 314489979U;

			// Token: 0x04000977 RID: 2423
			[Argument(0, HelpText = "Keep counts for all columns in one shared count table", ShortName = "shared")]
			public bool sharedTable;
		}
	}
}
