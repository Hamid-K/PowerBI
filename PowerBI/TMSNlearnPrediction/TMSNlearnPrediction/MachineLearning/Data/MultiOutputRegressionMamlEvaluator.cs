using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000202 RID: 514
	public sealed class MultiOutputRegressionMamlEvaluator : MamlEvaluatorBase
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0003E106 File Offset: 0x0003C306
		protected override IEvaluator Evaluator
		{
			get
			{
				return this._evaluator;
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0003E110 File Offset: 0x0003C310
		public MultiOutputRegressionMamlEvaluator(MultiOutputRegressionMamlEvaluator.Arguments args, IHostEnvironment env)
			: base(args, env, "MultiOutputRegression", "RegressionMamlEvaluator")
		{
			Contracts.CheckUserArg(this._host, SubComponentExtensions.IsGood(args.lossFunction), "loss", "Loss function must be specified");
			this._supressScoresAndLabels = args.supressScoresAndLabels;
			this._evaluator = new MultiOutputRegressionEvaluator(new MultiOutputRegressionEvaluator.Arguments
			{
				lossFunction = args.lossFunction
			}, this._host);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0003E370 File Offset: 0x0003C570
		protected override IEnumerable<string> GetPerInstanceColumnsToSave(RoleMappedSchema schema)
		{
			Contracts.CheckValue<RoleMappedSchema>(this._host, schema, "schema");
			Contracts.CheckValue<ColumnInfo>(this._host, schema.Label, "perInstance", "Data must contain a label column");
			if (!this._supressScoresAndLabels)
			{
				yield return schema.Label.Name;
				ColumnInfo scoreInfo = EvaluateUtils.GetScoreColumnInfo(this._host, schema.Schema, this._scoreCol, "scoreColumn", "MultiOutputRegression", "Score", null);
				yield return scoreInfo.Name;
			}
			yield return "L1-loss";
			yield return "L2-loss";
			yield return "Euclidean-Distance";
			yield break;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0003E3AC File Offset: 0x0003C5AC
		protected override void PrintFoldResultsCore(IChannel ch, Dictionary<string, IDataView> metrics)
		{
			IDataView dataView;
			if (!metrics.TryGetValue("OverallMetrics", out dataView))
			{
				throw Contracts.Except(this._host, "No overall metrics found");
			}
			int num;
			bool flag = dataView.Schema.TryGetColumnIndex("IsWeighted", ref num);
			int num2;
			bool flag2 = dataView.Schema.TryGetColumnIndex("StratCol", ref num2);
			int num3;
			dataView.Schema.TryGetColumnIndex("StratVal", ref num3);
			int columnCount = dataView.Schema.ColumnCount;
			ValueGetter<VBuffer<double>>[] array = new ValueGetter<VBuffer<double>>[columnCount];
			using (IRowCursor rowCursor = dataView.GetRowCursor((int col) => true, null))
			{
				DvBool @false = DvBool.False;
				ValueGetter<DvBool> valueGetter;
				if (flag)
				{
					valueGetter = rowCursor.GetGetter<DvBool>(num);
				}
				else
				{
					valueGetter = delegate(ref DvBool dst)
					{
						dst = DvBool.False;
					};
				}
				ValueGetter<uint> valueGetter2;
				if (flag2)
				{
					ColumnType columnType = rowCursor.Schema.GetColumnType(num2);
					valueGetter2 = RowCursorUtils.GetGetterAs<uint>(columnType, rowCursor, num2);
				}
				else
				{
					valueGetter2 = delegate(ref uint dst)
					{
						dst = 0U;
					};
				}
				int num4 = 0;
				for (int i = 0; i < dataView.Schema.ColumnCount; i++)
				{
					if (!MetadataUtils.IsHidden(dataView.Schema, i) && (!flag || i != num) && (!flag2 || (i != num2 && i != num3)))
					{
						ColumnType columnType2 = dataView.Schema.GetColumnType(i);
						if (columnType2.IsKnownSizeVector && columnType2.ItemType == NumberType.R8)
						{
							array[i] = rowCursor.GetGetter<VBuffer<double>>(i);
							if (num4 == 0)
							{
								num4 = columnType2.VectorSize;
							}
							else
							{
								Contracts.Check(this._host, num4 == columnType2.VectorSize, "All vector metrics should contain the same number of slots");
							}
						}
					}
				}
				DvText[] array2 = new DvText[num4];
				for (int j = 0; j < num4; j++)
				{
					array2[j] = new DvText(string.Format("Label_{0}", j));
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Per-label metrics:");
				stringBuilder.AppendFormat("{0,12} ", " ");
				for (int k = 0; k < num4; k++)
				{
					stringBuilder.AppendFormat(" {0,20}", array2[k]);
				}
				stringBuilder.AppendLine();
				VBuffer<double> vbuffer = default(VBuffer<double>);
				bool flag3 = false;
				bool flag4 = false;
				uint num5 = 0U;
				while (rowCursor.MoveNext())
				{
					valueGetter.Invoke(ref @false);
					if ((flag3 && @false.IsTrue) || (flag4 && @false.IsFalse))
					{
						throw Contracts.Except(this._host, "Multiple {0} rows found in overall metrics data view", new object[] { @false.IsTrue ? "weighted" : "unweighted" });
					}
					if (@false.IsTrue)
					{
						flag3 = true;
					}
					else
					{
						flag4 = true;
					}
					valueGetter2.Invoke(ref num5);
					if (num5 <= 0U)
					{
						for (int l = 0; l < columnCount; l++)
						{
							if (array[l] != null)
							{
								array[l].Invoke(ref vbuffer);
								stringBuilder.AppendFormat("{0}{1,12}:", @false.IsTrue ? "Weighted " : "", dataView.Schema.GetColumnName(l));
								foreach (KeyValuePair<int, double> keyValuePair in vbuffer.Items(true))
								{
									stringBuilder.AppendFormat(" {0,20:G20}", keyValuePair.Value);
								}
								stringBuilder.AppendLine();
							}
						}
					}
				}
				ch.Info(stringBuilder.ToString());
			}
		}

		// Token: 0x04000640 RID: 1600
		private readonly MultiOutputRegressionEvaluator _evaluator;

		// Token: 0x04000641 RID: 1601
		private readonly bool _supressScoresAndLabels;

		// Token: 0x02000203 RID: 515
		public sealed class Arguments : MamlEvaluatorBase.ArgumentsBase
		{
			// Token: 0x04000645 RID: 1605
			[Argument(4, HelpText = "Loss function", ShortName = "loss")]
			public SubComponent<IRegressionLoss, SignatureRegressionLoss> lossFunction = new SubComponent<IRegressionLoss, SignatureRegressionLoss>("SquaredLoss");

			// Token: 0x04000646 RID: 1606
			[Argument(0, HelpText = "Supress labels and scores in per-instance outputs?", ShortName = "noScores")]
			public bool supressScoresAndLabels;
		}
	}
}
