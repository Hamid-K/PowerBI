using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning.Data.IO;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001BD RID: 445
	internal static class TextModelHelper
	{
		// Token: 0x060009F2 RID: 2546 RVA: 0x000351B0 File Offset: 0x000333B0
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("TEXTSPBF", 65537U, 65537U, 65537U, "TextSpanBuffer", null);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000351D4 File Offset: 0x000333D4
		private static void Load(IChannel ch, ModelLoadContext ctx, CodecFactory factory, ref VBuffer<DvText> values)
		{
			Contracts.CheckValue<ModelLoadContext>(ch, ctx, "ctx");
			ctx.CheckAtModel(TextModelHelper.GetVersionInfo());
			IValueCodec valueCodec;
			if (!factory.TryReadCodec(ctx.Reader.BaseStream, out valueCodec))
			{
				throw Contracts.ExceptDecode(ch, "Failure to read the term coding scheme");
			}
			Contracts.CheckDecode(ch, valueCodec.Type.IsVector);
			Contracts.CheckDecode(ch, valueCodec.Type.ItemType.IsText);
			IValueCodec<VBuffer<DvText>> valueCodec2 = (IValueCodec<VBuffer<DvText>>)valueCodec;
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(ch, num >= 0);
			using (SubsetStream subsetStream = new SubsetStream(ctx.Reader.BaseStream, new long?((long)num)))
			{
				using (IValueReader<VBuffer<DvText>> valueReader = valueCodec2.OpenReader(subsetStream, 1))
				{
					valueReader.MoveNext();
					values = default(VBuffer<DvText>);
					valueReader.Get(ref values);
				}
				Contracts.CheckDecode(ch, subsetStream.ReadByte() == -1);
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000353F0 File Offset: 0x000335F0
		private static void Save(IChannel ch, ModelSaveContext ctx, CodecFactory factory, ref VBuffer<DvText> values)
		{
			Contracts.CheckValue<ModelSaveContext>(ch, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(TextModelHelper.GetVersionInfo());
			IValueCodec valueCodec;
			factory.TryGetCodec(new VectorType(TextType.Instance, 0), out valueCodec);
			IValueCodec<VBuffer<DvText>> valueCodec2 = (IValueCodec<VBuffer<DvText>>)valueCodec;
			factory.WriteCodec(ctx.Writer.BaseStream, valueCodec);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (IValueWriter<VBuffer<DvText>> valueWriter = valueCodec2.OpenWriter(memoryStream))
				{
					valueWriter.Write(ref values);
					valueWriter.Commit();
				}
				Utils.WriteByteArray(ctx.Writer, memoryStream.ToArray());
			}
			VBuffer<DvText> v = values;
			char[] buffer = null;
			ctx.SaveTextStream("Terms.txt", delegate(TextWriter writer)
			{
				writer.WriteLine("# Number of terms = {0} of length {1}", v.Count, v.Length);
				foreach (KeyValuePair<int, DvText> keyValuePair in v.Items(false))
				{
					DvText value = keyValuePair.Value;
					if (!value.IsEmpty)
					{
						writer.Write("{0}\t", keyValuePair.Key);
						if (!value.HasChars)
						{
							writer.WriteLine();
						}
						else
						{
							Utils.EnsureSize<char>(ref buffer, value.Length, true);
							int num;
							int num2;
							string rawUnderlyingBufferInfo = value.GetRawUnderlyingBufferInfo(ref num, ref num2);
							rawUnderlyingBufferInfo.CopyTo(num, buffer, 0, value.Length);
							writer.WriteLine(buffer, 0, value.Length);
						}
					}
				}
			});
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000355D8 File Offset: 0x000337D8
		public static void LoadAll(IHost host, ModelLoadContext ctx, int infoLim, out VBuffer<DvText>[] keyValues, out ColumnType[] kvTypes)
		{
			using (IChannel ch = host.Start("LoadTextValues"))
			{
				TextModelHelper.<>c__DisplayClass8 CS$<>8__locals3 = new TextModelHelper.<>c__DisplayClass8();
				CS$<>8__locals3.keyValuesLocal = null;
				CS$<>8__locals3.kvTypesLocal = null;
				CS$<>8__locals3.factory = null;
				int iinfo;
				for (iinfo = 0; iinfo < infoLim; iinfo++)
				{
					ctx.TryProcessSubModel(string.Format("Vocabulary_{0:000}", iinfo), delegate(ModelLoadContext c)
					{
						if (CS$<>8__locals3.keyValuesLocal == null)
						{
							CS$<>8__locals3.keyValuesLocal = new VBuffer<DvText>[infoLim];
							CS$<>8__locals3.kvTypesLocal = new ColumnType[infoLim];
							CS$<>8__locals3.factory = new CodecFactory(host, null);
						}
						TextModelHelper.Load(ch, c, CS$<>8__locals3.factory, ref CS$<>8__locals3.keyValuesLocal[iinfo]);
						CS$<>8__locals3.kvTypesLocal[iinfo] = new VectorType(TextType.Instance, CS$<>8__locals3.keyValuesLocal[iinfo].Length);
					});
				}
				keyValues = CS$<>8__locals3.keyValuesLocal;
				kvTypes = CS$<>8__locals3.kvTypesLocal;
				ch.Done();
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0003573C File Offset: 0x0003393C
		public static void SaveAll(IHost host, ModelSaveContext ctx, int infoLim, VBuffer<DvText>[] keyValues)
		{
			if (keyValues == null)
			{
				return;
			}
			using (IChannel ch = host.Start("SaveTextValues"))
			{
				TextModelHelper.<>c__DisplayClass12 CS$<>8__locals3 = new TextModelHelper.<>c__DisplayClass12();
				CS$<>8__locals3.factory = new CodecFactory(host, null);
				int iinfo;
				for (iinfo = 0; iinfo < infoLim; iinfo++)
				{
					if (keyValues[iinfo].Length != 0)
					{
						ctx.SaveSubModel(string.Format("Vocabulary_{0:000}", iinfo), delegate(ModelSaveContext c)
						{
							TextModelHelper.Save(ch, c, CS$<>8__locals3.factory, ref keyValues[iinfo]);
						});
					}
				}
				ch.Done();
			}
		}

		// Token: 0x04000522 RID: 1314
		private const string LoaderSignature = "TextSpanBuffer";
	}
}
