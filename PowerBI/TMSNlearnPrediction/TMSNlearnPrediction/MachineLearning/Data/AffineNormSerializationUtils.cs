using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200037B RID: 891
	public static class AffineNormSerializationUtils
	{
		// Token: 0x06001331 RID: 4913 RVA: 0x0006B906 File Offset: 0x00069B06
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("AFF NORM", 65539U, 65539U, 65539U, "AffineNormExec", null);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0006BA0C File Offset: 0x00069C0C
		public static void SaveModel(ModelSaveContext ctx, int numFeatures, int[] indices, double[] scales, double[] offsets, bool saveText = false)
		{
			ctx.CheckAtModel();
			Contracts.Check(numFeatures > 0);
			Contracts.CheckValue<double[]>(scales, "scales");
			ctx.SetVersionInfo(AffineNormSerializationUtils.GetVersionInfo());
			ctx.Writer.Write(8);
			ctx.Writer.Write(numFeatures);
			if (indices == null)
			{
				ctx.Writer.Write(-1);
			}
			else
			{
				Utils.WriteIntArray(ctx.Writer, indices);
			}
			Utils.WriteDoubleArray(ctx.Writer, scales);
			Utils.WriteDoubleArray(ctx.Writer, offsets);
			if (saveText)
			{
				ctx.SaveTextStream("AffineNormalizer.txt", delegate(TextWriter writer)
				{
					writer.WriteLine("NumNormalizationFeatures={0}", numFeatures);
					if (indices == null)
					{
						for (int i = 0; i < numFeatures; i++)
						{
							writer.WriteLine("{0}\t{1}\t{2}", i, (offsets != null) ? offsets[i] : 0.0, scales[i]);
						}
					}
					else
					{
						for (int j = 0; j < indices.Length; j++)
						{
							writer.WriteLine("{0}\t{1}\t{2}", indices[j], (offsets != null) ? offsets[j] : 0.0, scales[j]);
						}
					}
					writer.WriteLine();
				});
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0006BAF4 File Offset: 0x00069CF4
		public static void LoadModel(ModelLoadContext ctx, ref List<int> indicesShift, out int numFeatures, out double[] scales, out double[] offsets, out int[] indicesMorph, out double[] scalesSparse, out double[] offsetsSparse)
		{
			Contracts.CheckValue<ModelLoadContext>(ctx, "ctx");
			ctx.CheckAtModel(AffineNormSerializationUtils.GetVersionInfo());
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num == 8);
			int num2 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num2 > 0);
			numFeatures = num2;
			int num3 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode((-1 <= num3) & (num3 < num2));
			if (indicesShift != null)
			{
				indicesShift.Clear();
			}
			if (num3 == -1)
			{
				indicesMorph = null;
				int num4 = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(num4 == num2);
				scalesSparse = Utils.ReadDoubleArray(ctx.Reader, num4);
				int num5 = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(num5 == 0 || num5 == num2);
				offsetsSparse = Utils.ReadDoubleArray(ctx.Reader, num5);
				scales = scalesSparse;
				offsets = offsetsSparse;
				for (int i = 0; i < scales.Length; i++)
				{
					double num6 = scales[i];
					Contracts.CheckDecode(!double.IsNaN(num6));
					if (offsets != null)
					{
						if (num6 == 0.0)
						{
							offsets[i] = 0.0;
						}
						else
						{
							double num7 = offsets[i];
							Contracts.CheckDecode(!double.IsNaN(num7));
							if (num7 != 0.0)
							{
								Utils.Add<int>(ref indicesShift, i);
							}
						}
					}
				}
				return;
			}
			indicesMorph = Utils.ReadIntArray(ctx.Reader, num3) ?? new int[0];
			int num8 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num8 == num3);
			scalesSparse = Utils.ReadDoubleArray(ctx.Reader, num8) ?? new double[0];
			int num9 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num9 == 0 || num9 == num3);
			offsetsSparse = Utils.ReadDoubleArray(ctx.Reader, num9);
			scales = Utils.CreateArray<double>(numFeatures, 1.0);
			offsets = ((offsetsSparse != null) ? new double[numFeatures] : null);
			int num10 = -1;
			for (int j = 0; j < indicesMorph.Length; j++)
			{
				int num11 = indicesMorph[j];
				Contracts.CheckDecode((num10 < num11) & (num11 < numFeatures));
				num10 = num11;
				double num12 = (scales[num11] = scalesSparse[j]);
				Contracts.CheckDecode(!double.IsNaN(num12));
				if (offsetsSparse != null)
				{
					if (num12 == 0.0)
					{
						offsetsSparse[j] = 0.0;
					}
					else
					{
						double num13 = (offsets[num11] = offsetsSparse[j]);
						Contracts.CheckDecode(!double.IsNaN(num13));
						if (num13 != 0.0)
						{
							Utils.Add<int>(ref indicesShift, num11);
						}
					}
				}
			}
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0006BE7C File Offset: 0x0006A07C
		public static void SaveModel(ModelSaveContext ctx, int numFeatures, int[] indices, float[] scales, float[] offsets, bool saveText = false)
		{
			ctx.CheckAtModel();
			Contracts.Check(numFeatures > 0);
			Contracts.CheckValue<float[]>(scales, "scales");
			ctx.SetVersionInfo(AffineNormSerializationUtils.GetVersionInfo());
			ctx.Writer.Write(4);
			ctx.Writer.Write(numFeatures);
			if (indices == null)
			{
				ctx.Writer.Write(-1);
			}
			else
			{
				Utils.WriteIntArray(ctx.Writer, indices);
			}
			Utils.WriteSingleArray(ctx.Writer, scales);
			Utils.WriteSingleArray(ctx.Writer, offsets);
			if (saveText)
			{
				ctx.SaveTextStream("AffineNormalizer.txt", delegate(TextWriter writer)
				{
					writer.WriteLine("NumNormalizationFeatures={0}", numFeatures);
					if (indices == null)
					{
						for (int i = 0; i < numFeatures; i++)
						{
							writer.WriteLine("{0}\t{1}\t{2}", i, (offsets != null) ? offsets[i] : 0f, scales[i]);
						}
					}
					else
					{
						for (int j = 0; j < indices.Length; j++)
						{
							writer.WriteLine("{0}\t{1}\t{2}", indices[j], (offsets != null) ? offsets[j] : 0f, scales[j]);
						}
					}
					writer.WriteLine();
				});
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0006BF64 File Offset: 0x0006A164
		public static void LoadModel(ModelLoadContext ctx, ref List<int> indicesShift, out int numFeatures, out float[] scales, out float[] offsets, out int[] indicesMorph, out float[] scalesSparse, out float[] offsetsSparse)
		{
			Contracts.CheckValue<ModelLoadContext>(ctx, "ctx");
			ctx.CheckAtModel(AffineNormSerializationUtils.GetVersionInfo());
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num == 4);
			int num2 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num2 > 0);
			numFeatures = num2;
			int num3 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode((-1 <= num3) & (num3 < num2));
			if (indicesShift != null)
			{
				indicesShift.Clear();
			}
			if (num3 == -1)
			{
				indicesMorph = null;
				int num4 = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(num4 == num2);
				scalesSparse = Utils.ReadSingleArray(ctx.Reader, num4);
				int num5 = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(num5 == 0 || num5 == num2);
				offsetsSparse = Utils.ReadSingleArray(ctx.Reader, num5);
				scales = scalesSparse;
				offsets = offsetsSparse;
				for (int i = 0; i < scales.Length; i++)
				{
					float num6 = scales[i];
					Contracts.CheckDecode(!float.IsNaN(num6));
					if (offsets != null)
					{
						if (num6 == 0f)
						{
							offsets[i] = 0f;
						}
						else
						{
							float num7 = offsets[i];
							Contracts.CheckDecode(!float.IsNaN(num7));
							if (num7 != 0f)
							{
								Utils.Add<int>(ref indicesShift, i);
							}
						}
					}
				}
				return;
			}
			indicesMorph = Utils.ReadIntArray(ctx.Reader, num3) ?? new int[0];
			int num8 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num8 == num3);
			scalesSparse = Utils.ReadSingleArray(ctx.Reader, num8) ?? new float[0];
			int num9 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num9 == 0 || num9 == num3);
			offsetsSparse = Utils.ReadSingleArray(ctx.Reader, num9);
			scales = Utils.CreateArray<float>(numFeatures, 1f);
			offsets = ((offsetsSparse != null) ? new float[numFeatures] : null);
			int num10 = -1;
			for (int j = 0; j < indicesMorph.Length; j++)
			{
				int num11 = indicesMorph[j];
				Contracts.CheckDecode((num10 < num11) & (num11 < numFeatures));
				num10 = num11;
				float num12 = (scales[num11] = scalesSparse[j]);
				Contracts.CheckDecode(!float.IsNaN(num12));
				if (offsetsSparse != null)
				{
					if (num12 == 0f)
					{
						offsetsSparse[j] = 0f;
					}
					else
					{
						float num13 = (offsets[num11] = offsetsSparse[j]);
						Contracts.CheckDecode(!float.IsNaN(num13));
						if (num13 != 0f)
						{
							Utils.Add<int>(ref indicesShift, num11);
						}
					}
				}
			}
		}

		// Token: 0x04000B10 RID: 2832
		public const string LoaderSignature = "AffineNormExec";
	}
}
