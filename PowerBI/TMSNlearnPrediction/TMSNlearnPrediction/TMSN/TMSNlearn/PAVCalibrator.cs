using System;
using System.IO;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004D2 RID: 1234
	public sealed class PAVCalibrator : ICalibrator, ICanSaveInBinaryFormat
	{
		// Token: 0x0600194C RID: 6476 RVA: 0x0008EF53 File Offset: 0x0008D153
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("PAV  CAL", 65537U, 65537U, 65537U, "PAVCaliExec", null);
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0008EF74 File Offset: 0x0008D174
		internal PAVCalibrator(IHostEnvironment env, float[] mins, float[] maxes, float[] values)
		{
			this._host = env.Register("PAVCalibrator");
			this._mins = mins;
			this._maxes = maxes;
			this._values = values;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0008EFA4 File Offset: 0x0008D1A4
		private PAVCalibrator(ModelLoadContext ctx, IHostEnvironment env)
		{
			this._host = env.Register("PAVCalibrator");
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4);
			int num2 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num2 >= 0);
			this._mins = new float[num2];
			this._maxes = new float[num2];
			this._values = new float[num2];
			float num3 = 0f;
			float num4 = float.NegativeInfinity;
			for (int i = 0; i < num2; i++)
			{
				float num5 = Utils.ReadFloat(ctx.Reader);
				float num6 = Utils.ReadFloat(ctx.Reader);
				float num7 = Utils.ReadFloat(ctx.Reader);
				Contracts.CheckDecode(this._host, num5 <= num6);
				Contracts.CheckDecode(this._host, num5 > num4);
				Contracts.CheckDecode(this._host, num7 > num3 || (num7 == num3 && i == 0));
				num3 = num7;
				num4 = num6;
				this._mins[i] = num5;
				this._maxes[i] = num6;
				this._values[i] = num7;
			}
			Contracts.CheckDecode(this._host, num3 <= 1f);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0008F0EF File Offset: 0x0008D2EF
		public static PAVCalibrator Create(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(PAVCalibrator.GetVersionInfo());
			return new PAVCalibrator(ctx, env);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0008F11A File Offset: 0x0008D31A
		public void SaveAsBinary(BinaryWriter writer)
		{
			ModelSaveContext.Save(writer, new Action<ModelSaveContext>(this.SaveCore));
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0008F130 File Offset: 0x0008D330
		private void SaveCore(ModelSaveContext ctx)
		{
			ctx.SetVersionInfo(PAVCalibrator.GetVersionInfo());
			ctx.Writer.Write(4);
			ctx.Writer.Write(this._mins.Length);
			float num = 0f;
			for (int i = 0; i < this._mins.Length; i++)
			{
				num = this._values[i];
				float num2 = this._maxes[i];
				ctx.Writer.Write(this._mins[i]);
				ctx.Writer.Write(this._maxes[i]);
				ctx.Writer.Write(this._values[i]);
			}
			Contracts.CheckDecode(this._host, num <= 1f);
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0008F1E4 File Offset: 0x0008D3E4
		public float PredictProbability(float output)
		{
			if (float.IsNaN(output))
			{
				return output;
			}
			float num = this.FindValue(output);
			if (num < 1E-15f)
			{
				return 1E-15f;
			}
			if (num > 1f)
			{
				return 1f;
			}
			return num;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0008F220 File Offset: 0x0008D420
		private float FindValue(float score)
		{
			int num = this._mins.Length;
			if (num == 0)
			{
				return 0f;
			}
			if (score < this._mins[0])
			{
				return this._values[0];
			}
			if (score > this._maxes[num - 1])
			{
				return this._values[num - 1];
			}
			int num2 = Utils.FindIndexSorted(this._maxes, score);
			if (score >= this._mins[num2])
			{
				return this._values[num2];
			}
			float num3 = (score - this._maxes[num2 - 1]) / (this._mins[num2] - this._maxes[num2 - 1]);
			return this._values[num2 - 1] + num3 * (this._values[num2] - this._values[num2 - 1]);
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0008F2CD File Offset: 0x0008D4CD
		public string GetSummary()
		{
			return string.Format("PAV calibrator with {0} intervals", this._mins.Length);
		}

		// Token: 0x04000F26 RID: 3878
		public const string LoaderSignature = "PAVCaliExec";

		// Token: 0x04000F27 RID: 3879
		public const string RegistrationName = "PAVCalibrator";

		// Token: 0x04000F28 RID: 3880
		private const float EPSILON = 1E-15f;

		// Token: 0x04000F29 RID: 3881
		private const float MinToReturn = 1E-15f;

		// Token: 0x04000F2A RID: 3882
		private const float MaxToReturn = 1f;

		// Token: 0x04000F2B RID: 3883
		private readonly IHost _host;

		// Token: 0x04000F2C RID: 3884
		private readonly float[] _mins;

		// Token: 0x04000F2D RID: 3885
		private readonly float[] _maxes;

		// Token: 0x04000F2E RID: 3886
		private readonly float[] _values;
	}
}
