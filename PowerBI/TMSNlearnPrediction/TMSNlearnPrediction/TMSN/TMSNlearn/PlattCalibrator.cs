using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004CF RID: 1231
	public sealed class PlattCalibrator : ICalibrator, IParameterMixer, ICanSaveModel
	{
		// Token: 0x0600193C RID: 6460 RVA: 0x0008EA92 File Offset: 0x0008CC92
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("PLATTCAL", 65537U, 65537U, 65537U, "PlattCaliExec", null);
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x0008EAB3 File Offset: 0x0008CCB3
		public double ParamA
		{
			get
			{
				return this._paramA;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x0008EABB File Offset: 0x0008CCBB
		public double ParamB
		{
			get
			{
				return this._paramB;
			}
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0008EAC3 File Offset: 0x0008CCC3
		public PlattCalibrator(IHostEnvironment env, double paramA, double paramB)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("PlattCalibrator");
			this._paramA = paramA;
			this._paramB = paramB;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0008EAF8 File Offset: 0x0008CCF8
		private PlattCalibrator(ModelLoadContext ctx, IHostEnvironment env)
		{
			this._host = env.Register("PlattCalibrator");
			this._paramA = ctx.Reader.ReadDouble();
			Contracts.CheckDecode(this._host, FloatUtils.IsFinite(this._paramA));
			this._paramB = ctx.Reader.ReadDouble();
			Contracts.CheckDecode(this._host, FloatUtils.IsFinite(this._paramB));
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0008EB6A File Offset: 0x0008CD6A
		public static PlattCalibrator Create(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(PlattCalibrator.GetVersionInfo());
			return new PlattCalibrator(ctx, env);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0008EB95 File Offset: 0x0008CD95
		public void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			this.SaveCore(ctx);
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0008EC08 File Offset: 0x0008CE08
		private void SaveCore(ModelSaveContext ctx)
		{
			ctx.SetVersionInfo(PlattCalibrator.GetVersionInfo());
			ctx.Writer.Write(this._paramA);
			ctx.Writer.Write(this._paramB);
			if (ctx.InRepository)
			{
				ctx.SaveTextStream("Calibrator.txt", delegate(TextWriter writer)
				{
					writer.WriteLine("Platt calibrator");
					writer.WriteLine("P(y=1|x) = 1/1+exp(A*x + B)");
					writer.WriteLine("A={0:R}", this._paramA);
					writer.WriteLine("B={0:R}", this._paramB);
				});
			}
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0008EC68 File Offset: 0x0008CE68
		public float PredictProbability(float output)
		{
			if (float.IsNaN(output))
			{
				return output;
			}
			return PlattCalibrator.PredictProbability(output, this._paramA, this._paramB);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0008EC86 File Offset: 0x0008CE86
		public static float PredictProbability(float output, double A, double B)
		{
			return (float)(1.0 / (1.0 + Math.Exp(A * (double)output + B)));
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0008ECA8 File Offset: 0x0008CEA8
		public string GetSummary()
		{
			return string.Format("Platt calibrator parameters: A={0}, B={1}", this._paramA, this._paramB);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0008ECCC File Offset: 0x0008CECC
		public IParameterMixer CombineParameters(IList<IParameterMixer> calibrators)
		{
			double num = 0.0;
			double num2 = 0.0;
			foreach (IParameterMixer parameterMixer in calibrators)
			{
				PlattCalibrator plattCalibrator = parameterMixer as PlattCalibrator;
				num += plattCalibrator._paramA;
				num2 += plattCalibrator._paramB;
			}
			return new PlattCalibrator(this._host, num / (double)calibrators.Count, num2 / (double)calibrators.Count);
		}

		// Token: 0x04000F1A RID: 3866
		public const string LoaderSignature = "PlattCaliExec";

		// Token: 0x04000F1B RID: 3867
		public const string RegistrationName = "PlattCalibrator";

		// Token: 0x04000F1C RID: 3868
		private readonly IHost _host;

		// Token: 0x04000F1D RID: 3869
		private readonly double _paramA;

		// Token: 0x04000F1E RID: 3870
		private readonly double _paramB;
	}
}
