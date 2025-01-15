using System;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Dracula.FeatureInference
{
	// Token: 0x02000419 RID: 1049
	public abstract class DraculaSlotMapBuilderBase : IDraculaSlotMapBuilder
	{
		// Token: 0x060015E6 RID: 5606 RVA: 0x0007FAF0 File Offset: 0x0007DCF0
		protected DraculaSlotMapBuilderBase(DraculaSlotMapBuilderBase.ArgumentsBase args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<DraculaSlotMapBuilderBase.ArgumentsBase>(env, args, "args");
			Contracts.CheckValue<IDataView>(env, input, "input");
			int num;
			if (!input.Schema.TryGetColumnIndex(args.column, ref num))
			{
				throw Contracts.ExceptUserArg(env, "column", "Could not find column '{0}'", new object[] { args.column });
			}
			ColumnType columnType = input.Schema.GetColumnType(num);
			if (!columnType.IsVector || columnType.VectorSize == 0)
			{
				throw Contracts.ExceptUserArg(env, "column", "Column '{0}' is of incorrect type: expected a known-size vector", new object[] { args.column });
			}
			this._srcVectorSize = columnType.VectorSize;
			if (args.featureCount < this._srcVectorSize)
			{
				throw Contracts.ExceptUserArg(env, "featureCount", "Too few output features. Expected at least {0}", new object[] { this._srcVectorSize });
			}
		}

		// Token: 0x060015E7 RID: 5607
		public abstract int[][] CreateSlotMap();

		// Token: 0x04000D6D RID: 3437
		protected readonly int _srcVectorSize;

		// Token: 0x0200041A RID: 1050
		public class ArgumentsBase
		{
			// Token: 0x04000D6E RID: 3438
			[Argument(1, HelpText = "Column to infer combinations from", ShortName = "col")]
			public string column;

			// Token: 0x04000D6F RID: 3439
			[Argument(0, HelpText = "Label column", ShortName = "label,lab")]
			public string labelColumn;

			// Token: 0x04000D70 RID: 3440
			[Argument(1, HelpText = "Maximum total number of features to infer", ShortName = "count")]
			public int featureCount;
		}
	}
}
