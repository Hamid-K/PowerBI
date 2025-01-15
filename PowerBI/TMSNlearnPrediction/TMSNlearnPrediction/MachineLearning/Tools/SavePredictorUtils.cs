using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Tools
{
	// Token: 0x02000484 RID: 1156
	public static class SavePredictorUtils
	{
		// Token: 0x0600181C RID: 6172 RVA: 0x0008A434 File Offset: 0x00088634
		public static void SavePredictor(IHostEnvironment env, Stream modelStream, Stream binaryModelStream = null, Stream summaryModelStream = null, Stream textModelStream = null, Stream iniModelStream = null, Stream codeModelStream = null)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<Stream>(env, modelStream, "modelStream");
			bool flag = codeModelStream != null || iniModelStream != null || summaryModelStream != null || textModelStream != null;
			IPredictor predictor;
			FeatureNameCollection featureNameCollection;
			RoleMappedSchema roleMappedSchema;
			SavePredictorUtils.LoadModel(env, modelStream, flag, out predictor, out featureNameCollection, out roleMappedSchema);
			using (IChannel channel = env.Start("Saving predictor"))
			{
				SavePredictorUtils.SavePredictor(channel, predictor, featureNameCollection, roleMappedSchema, binaryModelStream, summaryModelStream, textModelStream, iniModelStream, codeModelStream);
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0008A4BC File Offset: 0x000886BC
		public static void SavePredictor(IChannel ch, IPredictor predictor, FeatureNameCollection names, RoleMappedSchema schema, Stream binaryModelStream = null, Stream summaryModelStream = null, Stream textModelStream = null, Stream iniModelStream = null, Stream codeModelStream = null)
		{
			Contracts.CheckValue<IPredictor>(ch, predictor, "predictor");
			int num = 0;
			if (binaryModelStream != null)
			{
				ch.Info("Saving predictor as binary");
				using (BinaryWriter binaryWriter = new BinaryWriter(binaryModelStream, Encoding.UTF8, true))
				{
					PredictorUtils.SaveBinary(ch, predictor, binaryWriter);
				}
				num++;
			}
			if (summaryModelStream != null)
			{
				ch.Info("Saving predictor summary");
				using (StreamWriter streamWriter = Utils.OpenWriter(summaryModelStream, null, 1024, true))
				{
					PredictorUtils.SaveSummary(ch, predictor, names, streamWriter);
				}
				num++;
			}
			if (textModelStream != null)
			{
				ch.Info("Saving predictor as text");
				using (StreamWriter streamWriter2 = Utils.OpenWriter(textModelStream, null, 1024, true))
				{
					PredictorUtils.SaveText(ch, predictor, names, streamWriter2);
				}
				num++;
			}
			if (iniModelStream != null)
			{
				ch.Info("Saving predictor as ini");
				using (StreamWriter streamWriter3 = Utils.OpenWriter(iniModelStream, null, 1024, true))
				{
					ICanSaveInIniFormat canSaveInIniFormat = predictor as ICanSaveInIniFormat;
					if (canSaveInIniFormat == null)
					{
						PredictorUtils.SaveIni(ch, predictor, names, streamWriter3);
					}
					else
					{
						canSaveInIniFormat.SaveAsIni(streamWriter3, schema, null);
					}
				}
				num++;
			}
			if (codeModelStream != null)
			{
				ch.Info("Saving predictor as code");
				using (StreamWriter streamWriter4 = Utils.OpenWriter(codeModelStream, null, 1024, true))
				{
					PredictorUtils.SaveCode(ch, predictor, names, streamWriter4);
				}
				num++;
			}
			if (num == 0)
			{
				ch.Info("No files saved. Must specify at least one output file.");
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0008A658 File Offset: 0x00088858
		public static void LoadModel(IHostEnvironment env, Stream modelStream, bool loadNames, out IPredictor predictor, out FeatureNameCollection names, out RoleMappedSchema schema)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<Stream>(env, modelStream, "modelStream");
			using (RepositoryReader repositoryReader = RepositoryReader.Open(modelStream, true))
			{
				ModelLoadContext.LoadModel<IPredictor, SignatureLoadModel>(out predictor, repositoryReader, "Predictor", new object[] { env });
				names = null;
				schema = null;
				if (loadNames)
				{
					ModelFileUtils.TryLoadFeatureNames(out names, repositoryReader);
					IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = ModelFileUtils.LoadRoleMappingsOrNull(env, repositoryReader);
					if (enumerable != null)
					{
						IDataView dataView = ModelFileUtils.LoadPipeline(env, repositoryReader, new MultiFileSource(null), false);
						schema = RoleMappedSchema.CreateOpt(dataView.Schema, enumerable);
					}
				}
			}
		}
	}
}
