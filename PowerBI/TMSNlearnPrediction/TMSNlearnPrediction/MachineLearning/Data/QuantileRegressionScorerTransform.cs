using System;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000130 RID: 304
	public static class QuantileRegressionScorerTransform
	{
		// Token: 0x0600062F RID: 1583 RVA: 0x000217D7 File Offset: 0x0001F9D7
		public static IDataScorerTransform Create(QuantileRegressionScorerTransform.Arguments args, IHostEnvironment env, IDataView data, ISchemaBoundMapper mapper)
		{
			return new GenericScorer(args, env, data, mapper);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000217E4 File Offset: 0x0001F9E4
		public static ISchemaBindableMapper Create(QuantileRegressionScorerTransform.Arguments args, IHostEnvironment env, IPredictor predictor)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<QuantileRegressionScorerTransform.Arguments>(env, args, "args");
			Contracts.CheckValue<IPredictor>(env, predictor, "predictor");
			IQuantileRegressionPredictor quantileRegressionPredictor = predictor as IQuantileRegressionPredictor;
			Contracts.Check(env, quantileRegressionPredictor != null, "Predictor doesn't support quantile regression");
			double[] array = QuantileRegressionScorerTransform.ParseQuantiles(args.quantiles);
			return quantileRegressionPredictor.CreateMapper(array);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000218A4 File Offset: 0x0001FAA4
		private static double[] ParseQuantiles(string quantiles)
		{
			Contracts.CheckUserArg(quantiles != null, "quantiles", "Quantiles are required");
			double[] array = quantiles.Split(new char[] { ',' }).Select(delegate(string v)
			{
				double num;
				if (!double.TryParse(v, out num))
				{
					throw Contracts.ExceptUserArg("quantiles", "Cannot parse quantile '{0}' as double.", new object[] { v });
				}
				Contracts.CheckUserArg(0.0 <= num && num <= 1.0, "quantiles", "Quantile must be between 0 and 1.");
				return num;
			}).ToArray<double>();
			Contracts.CheckUserArg(array.Length > 0, "quantiles", "There must be at least one quantile.");
			return array;
		}

		// Token: 0x02000131 RID: 305
		public sealed class Arguments : ScorerArgumentsBase
		{
			// Token: 0x0400032C RID: 812
			[Argument(4, HelpText = "List of numbers between 0 and 1 (comma-separated) to get quantile statistics. The default value outputs Five point summary")]
			public string quantiles = "0,0.25,0.5,0.75,1";
		}
	}
}
