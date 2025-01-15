using System;
using System.IO;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200037E RID: 894
	public static class CdfNormSerializationUtils
	{
		// Token: 0x0600133C RID: 4924 RVA: 0x0006C670 File Offset: 0x0006A870
		public static void SaveModel(ModelSaveContext ctx, bool useLog, double[] mean, double[] stddev)
		{
			ctx.Writer.Write(8);
			Utils.WriteBoolByte(ctx.Writer, useLog);
			ctx.Writer.Write(mean.Length);
			Utils.WriteDoublesNoCount(ctx.Writer, mean, mean.Length);
			Utils.WriteDoublesNoCount(ctx.Writer, stddev, mean.Length);
			ctx.SaveTextStream("CdfNormalizer.txt", delegate(TextWriter writer)
			{
				writer.WriteLine("NumNormalizationFeatures={0}", mean.Length);
				writer.WriteLine("Log={0}", useLog);
				for (int i = 0; i < mean.Length; i++)
				{
					writer.WriteLine("{0}\t{1}", mean[i], stddev[i]);
				}
			});
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0006C714 File Offset: 0x0006A914
		public static void LoadModel(ModelLoadContext ctx, int cv, out bool useLog, out double[] mean, out double[] stddev)
		{
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num == 8);
			useLog = Utils.ReadBoolByte(ctx.Reader);
			int num2 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num2 > 0);
			if (num2 != cv)
			{
				throw Contracts.Except("Normalizer expected {0} slots, but the input data column has {1} slots.", new object[] { num2, cv });
			}
			mean = Utils.ReadDoubleArray(ctx.Reader, num2);
			stddev = Utils.ReadDoubleArray(ctx.Reader, num2);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0006C81C File Offset: 0x0006AA1C
		public static void SaveModel(ModelSaveContext ctx, bool useLog, float[] mean, float[] stddev)
		{
			ctx.Writer.Write(4);
			Utils.WriteBoolByte(ctx.Writer, useLog);
			ctx.Writer.Write(mean.Length);
			Utils.WriteSinglesNoCount(ctx.Writer, mean, mean.Length);
			Utils.WriteSinglesNoCount(ctx.Writer, stddev, mean.Length);
			ctx.SaveTextStream("CdfNormalizer.txt", delegate(TextWriter writer)
			{
				writer.WriteLine("NumNormalizationFeatures={0}", mean.Length);
				writer.WriteLine("Log={0}", useLog);
				for (int i = 0; i < mean.Length; i++)
				{
					writer.WriteLine("{0}\t{1}", mean[i], stddev[i]);
				}
			});
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0006C8C0 File Offset: 0x0006AAC0
		public static void LoadModel(ModelLoadContext ctx, int cv, out bool useLog, out float[] mean, out float[] stddev)
		{
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num == 4);
			useLog = Utils.ReadBoolByte(ctx.Reader);
			int num2 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num2 > 0);
			if (num2 != cv)
			{
				throw Contracts.Except("Normalizer expected {0} slots, but the input data column has {1} slots.", new object[] { num2, cv });
			}
			mean = Utils.ReadSingleArray(ctx.Reader, num2);
			stddev = Utils.ReadSingleArray(ctx.Reader, num2);
		}
	}
}
