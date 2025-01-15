using System;
using System.IO;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200037C RID: 892
	public static class BinNormSerializationUtils
	{
		// Token: 0x06001336 RID: 4918 RVA: 0x0006C1F3 File Offset: 0x0006A3F3
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("BIN NORM", 65537U, 65537U, 65537U, "BinNormExec", null);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0006C294 File Offset: 0x0006A494
		public static void SaveModel(ModelSaveContext ctx, double[][] binUpperBounds, bool saveText = false)
		{
			ctx.SetVersionInfo(BinNormSerializationUtils.GetVersionInfo());
			ctx.Writer.Write(8);
			ctx.Writer.Write(binUpperBounds.Length);
			foreach (double[] array in binUpperBounds)
			{
				Utils.WriteDoubleArray(ctx.Writer, array);
			}
			if (saveText)
			{
				ctx.SaveTextStream("BinNormalizer.txt", delegate(TextWriter writer)
				{
					writer.WriteLine("NumNormalizationFeatures={0}", binUpperBounds.Length);
					for (int j = 0; j < binUpperBounds.Length; j++)
					{
						string text = "";
						for (int k = 0; k < binUpperBounds[j].Length - 1; k++)
						{
							writer.Write(text);
							text = "\t";
							writer.Write(binUpperBounds[j][k]);
						}
						writer.WriteLine();
					}
				});
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0006C324 File Offset: 0x0006A524
		public static void LoadModel(ModelLoadContext ctx, out double[][] binUpperBounds)
		{
			Contracts.CheckValue<ModelLoadContext>(ctx, "ctx");
			ctx.CheckAtModel(BinNormSerializationUtils.GetVersionInfo());
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num == 8);
			int num2 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num2 > 0);
			binUpperBounds = new double[num2][];
			for (int i = 0; i < num2; i++)
			{
				double[] array = Utils.ReadDoubleArray(ctx.Reader);
				Contracts.CheckDecode(Utils.Size<double>(array) > 0);
				binUpperBounds[i] = array;
				for (int j = 1; j < array.Length; j++)
				{
					Contracts.CheckDecode(array[j - 1] < array[j]);
				}
				Contracts.CheckDecode(array[array.Length - 1] == double.PositiveInfinity);
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0006C460 File Offset: 0x0006A660
		public static void SaveModel(ModelSaveContext ctx, float[][] binUpperBounds, bool saveText = false)
		{
			ctx.SetVersionInfo(BinNormSerializationUtils.GetVersionInfo());
			ctx.Writer.Write(4);
			ctx.Writer.Write(binUpperBounds.Length);
			foreach (float[] array in binUpperBounds)
			{
				Utils.WriteSingleArray(ctx.Writer, array);
			}
			if (saveText)
			{
				ctx.SaveTextStream("BinNormalizer.txt", delegate(TextWriter writer)
				{
					writer.WriteLine("NumNormalizationFeatures={0}", binUpperBounds.Length);
					for (int j = 0; j < binUpperBounds.Length; j++)
					{
						string text = "";
						for (int k = 0; k < binUpperBounds[j].Length - 1; k++)
						{
							writer.Write(text);
							text = "\t";
							writer.Write(binUpperBounds[j][k]);
						}
						writer.WriteLine();
					}
				});
			}
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0006C4F0 File Offset: 0x0006A6F0
		public static void LoadModel(ModelLoadContext ctx, out float[][] binUpperBounds)
		{
			Contracts.CheckValue<ModelLoadContext>(ctx, "ctx");
			ctx.CheckAtModel(BinNormSerializationUtils.GetVersionInfo());
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num == 4);
			int num2 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num2 > 0);
			binUpperBounds = new float[num2][];
			for (int i = 0; i < num2; i++)
			{
				float[] array = Utils.ReadSingleArray(ctx.Reader);
				Contracts.CheckDecode(Utils.Size<float>(array) > 0);
				binUpperBounds[i] = array;
				for (int j = 1; j < array.Length; j++)
				{
					Contracts.CheckDecode(array[j - 1] < array[j]);
				}
				Contracts.CheckDecode(array[array.Length - 1] == float.PositiveInfinity);
			}
		}

		// Token: 0x04000B11 RID: 2833
		public const string LoaderSignature = "BinNormExec";
	}
}
