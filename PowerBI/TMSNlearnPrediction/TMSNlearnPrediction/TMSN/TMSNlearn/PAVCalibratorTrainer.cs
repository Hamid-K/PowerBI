using System;
using System.Collections.Generic;
using Microsoft.MachineLearning;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004D0 RID: 1232
	public class PAVCalibratorTrainer : CalibratorTrainerBase
	{
		// Token: 0x06001949 RID: 6473 RVA: 0x0008ED60 File Offset: 0x0008CF60
		public PAVCalibratorTrainer(IHostEnvironment env)
			: base(env, "PAVCalibration")
		{
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0008ED70 File Offset: 0x0008CF70
		public override ICalibrator CreateCalibrator(IChannel ch)
		{
			Stack<PAVCalibratorTrainer.Piece> stack = new Stack<PAVCalibratorTrainer.Piece>();
			PAVCalibratorTrainer.Piece piece = default(PAVCalibratorTrainer.Piece);
			foreach (CalibrationDataStore.DataItem dataItem in this._data)
			{
				PAVCalibratorTrainer.Piece piece2 = new PAVCalibratorTrainer.Piece(dataItem.Score, dataItem.Score, (float)(dataItem.Target ? 1 : 0), dataItem.Weight);
				while (stack.Count > 0 && (piece.MaxX >= piece2.MinX || piece2.Value <= piece.Value))
				{
					float num = piece.N + piece2.N;
					piece2 = new PAVCalibratorTrainer.Piece(piece.MinX, piece2.MaxX, (piece.Value * piece.N + piece2.Value * piece2.N) / num, num);
					stack.Pop();
					if (stack.Count > 0)
					{
						piece = stack.Peek();
					}
				}
				stack.Push(piece2);
				piece = piece2;
			}
			ch.Info("PAV calibrator:  piecewise function approximation has {0} components.", new object[] { stack.Count });
			float[] array = new float[stack.Count];
			float[] array2 = new float[stack.Count];
			float[] array3 = new float[stack.Count];
			int num2 = stack.Count - 1;
			while (stack.Count > 0)
			{
				piece = stack.Pop();
				array[num2] = piece.MinX;
				array2[num2] = piece.MaxX;
				array3[num2] = piece.Value;
				num2--;
			}
			return new PAVCalibrator(this._host, array, array2, array3);
		}

		// Token: 0x04000F1F RID: 3871
		internal const string UserName = "PAV Calibration";

		// Token: 0x04000F20 RID: 3872
		internal const string LoadName = "PAVCalibration";

		// Token: 0x04000F21 RID: 3873
		internal const string Summary = "Piecewise linear calibrator.";

		// Token: 0x020004D1 RID: 1233
		private struct Piece
		{
			// Token: 0x0600194B RID: 6475 RVA: 0x0008EF34 File Offset: 0x0008D134
			public Piece(float minX, float maxX, float value, float n)
			{
				this.MinX = minX;
				this.MaxX = maxX;
				this.Value = value;
				this.N = n;
			}

			// Token: 0x04000F22 RID: 3874
			public readonly float MinX;

			// Token: 0x04000F23 RID: 3875
			public readonly float MaxX;

			// Token: 0x04000F24 RID: 3876
			public readonly float Value;

			// Token: 0x04000F25 RID: 3877
			public readonly float N;
		}
	}
}
