using System;
using System.IO;
using System.Linq;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004CA RID: 1226
	public sealed class NaiveCalibrator : ICalibrator, ICanSaveInBinaryFormat
	{
		// Token: 0x06001926 RID: 6438 RVA: 0x0008E1F2 File Offset: 0x0008C3F2
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("NAIVECAL", 65537U, 65537U, 65537U, "NaiveCaliExec", null);
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0008E213 File Offset: 0x0008C413
		public NaiveCalibrator(IHostEnvironment env, float min, float binSize, float[] binProbs)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("NaiveCalibrator");
			this._min = min;
			this._binSize = binSize;
			this._binProbs = binProbs;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0008E264 File Offset: 0x0008C464
		private NaiveCalibrator(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("NaiveCalibrator");
			Contracts.CheckValue<ModelLoadContext>(this._host, ctx, "ctx");
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4);
			this._binSize = Utils.ReadFloat(ctx.Reader);
			Contracts.CheckDecode(this._host, 0f < this._binSize && this._binSize < float.PositiveInfinity);
			this._min = Utils.ReadFloat(ctx.Reader);
			Contracts.CheckDecode(this._host, FloatUtils.IsFinite(this._min));
			this._binProbs = Utils.ReadFloatArray(ctx.Reader);
			Contracts.CheckDecode(this._host, Utils.Size<float>(this._binProbs) > 0);
			Contracts.CheckDecode(this._host, this._binProbs.All((float x) => 0f <= x && x <= 1f));
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0008E37C File Offset: 0x0008C57C
		public static NaiveCalibrator Create(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(NaiveCalibrator.GetVersionInfo());
			return new NaiveCalibrator(ctx, env);
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0008E3A7 File Offset: 0x0008C5A7
		public void SaveAsBinary(BinaryWriter writer)
		{
			ModelSaveContext.Save(writer, new Action<ModelSaveContext>(this.SaveCore));
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x0008E3BC File Offset: 0x0008C5BC
		private void SaveCore(ModelSaveContext ctx)
		{
			ctx.SetVersionInfo(NaiveCalibrator.GetVersionInfo());
			ctx.Writer.Write(4);
			ctx.Writer.Write(this._binSize);
			ctx.Writer.Write(this._min);
			Utils.WriteFloatArray(ctx.Writer, this._binProbs);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0008E414 File Offset: 0x0008C614
		public float PredictProbability(float output)
		{
			if (float.IsNaN(output))
			{
				return output;
			}
			int binIdx = NaiveCalibrator.GetBinIdx(output, this._min, this._binSize, this._binProbs.Length);
			return this._binProbs[binIdx];
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0008E450 File Offset: 0x0008C650
		internal static int GetBinIdx(float output, float min, float binSize, int numBins)
		{
			int num = (int)((output - min) / binSize);
			if (num >= numBins)
			{
				num = numBins - 1;
			}
			if (num < 0)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0008E473 File Offset: 0x0008C673
		public string GetSummary()
		{
			return string.Format("Naive Calibrator has {0} bins, starting at {1}, with bin size of {2}", this._binProbs.Length, this._min, this._binSize);
		}

		// Token: 0x04000F02 RID: 3842
		public const string LoaderSignature = "NaiveCaliExec";

		// Token: 0x04000F03 RID: 3843
		public const string RegistrationName = "NaiveCalibrator";

		// Token: 0x04000F04 RID: 3844
		private readonly IHost _host;

		// Token: 0x04000F05 RID: 3845
		private readonly float _binSize;

		// Token: 0x04000F06 RID: 3846
		private readonly float _min;

		// Token: 0x04000F07 RID: 3847
		private readonly float[] _binProbs;
	}
}
