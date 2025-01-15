using System;
using System.IO;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Model;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004D8 RID: 1240
	public static class PredictorUtils
	{
		// Token: 0x0600195E RID: 6494 RVA: 0x0008F4B8 File Offset: 0x0008D6B8
		public static void SaveSummary(IChannel ch, IPredictor predictor, FeatureNameCollection names, TextWriter writer)
		{
			Contracts.CheckValue<IChannel>(ch, "ch");
			ICanSaveSummary canSaveSummary = predictor as ICanSaveSummary;
			if (canSaveSummary != null)
			{
				canSaveSummary.SaveSummary(writer, names);
				return;
			}
			writer.WriteLine("'{0}' does not support saving summary", predictor.GetType().Name);
			ch.Error("'{0}' does not support saving summary", new object[] { predictor.GetType().Name });
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0008F51C File Offset: 0x0008D71C
		public static void SaveText(IChannel ch, IPredictor predictor, FeatureNameCollection names, TextWriter writer)
		{
			Contracts.CheckValue<IChannel>(ch, "ch");
			ICanSaveInTextFormat canSaveInTextFormat = predictor as ICanSaveInTextFormat;
			if (canSaveInTextFormat != null)
			{
				canSaveInTextFormat.SaveAsText(writer, names);
				return;
			}
			ICanSaveSummary canSaveSummary = predictor as ICanSaveSummary;
			if (canSaveSummary != null)
			{
				writer.WriteLine("'{0}' does not support saving in text format, writing out model summary instead", predictor.GetType().Name);
				ch.Error("'{0}' doesn't currently have standardized text format for /mt, will save model summary instead", new object[] { predictor.GetType().Name });
				canSaveSummary.SaveSummary(writer, names);
				return;
			}
			writer.WriteLine("'{0}' does not support saving in text format", predictor.GetType().Name);
			ch.Error("'{0}' doesn't currently have standardized text format for /mt", new object[] { predictor.GetType().Name });
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0008F5C8 File Offset: 0x0008D7C8
		public static void SaveBinary(IChannel ch, IPredictor predictor, BinaryWriter writer)
		{
			Contracts.CheckValue<IChannel>(ch, "ch");
			ICanSaveInBinaryFormat canSaveInBinaryFormat = predictor as ICanSaveInBinaryFormat;
			if (canSaveInBinaryFormat == null)
			{
				ch.Error("'{0}' doesn't currently have standardized binary format for /mb", new object[] { predictor.GetType().Name });
				return;
			}
			canSaveInBinaryFormat.SaveAsBinary(writer);
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0008F614 File Offset: 0x0008D814
		public static void SaveIni(IChannel ch, IPredictor predictor, FeatureNameCollection names, TextWriter writer)
		{
			Contracts.CheckValue<IChannel>(ch, "ch");
			ICanSaveInIniFormatOld canSaveInIniFormatOld = predictor as ICanSaveInIniFormatOld;
			if (canSaveInIniFormatOld != null)
			{
				canSaveInIniFormatOld.SaveAsIniOld(writer, names, null);
				return;
			}
			ICanSaveSummary canSaveSummary = predictor as ICanSaveSummary;
			if (canSaveSummary != null)
			{
				writer.WriteLine("'{0}' does not support saving in INI format, writing out model summary instead", predictor.GetType().Name);
				ch.Error("'{0}' doesn't currently have standardized INI format output, will save model summary instead", new object[] { predictor.GetType().Name });
				canSaveSummary.SaveSummary(writer, names);
				return;
			}
			writer.WriteLine("'{0}' does not support saving in INI format", predictor.GetType().Name);
			ch.Error("'{0}' doesn't currently have standardized INI format output", new object[] { predictor.GetType().Name });
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0008F6C4 File Offset: 0x0008D8C4
		public static void SaveCode(IChannel ch, IPredictor predictor, FeatureNameCollection names, TextWriter writer)
		{
			Contracts.CheckValue<IChannel>(ch, "ch");
			ICanSaveInSourceCode canSaveInSourceCode = predictor as ICanSaveInSourceCode;
			if (canSaveInSourceCode != null)
			{
				canSaveInSourceCode.SaveAsCode(writer, names);
				return;
			}
			writer.WriteLine("'{0}' does not support saving in code.", predictor.GetType().Name);
			ch.Error("'{0}' doesn't currently support saving the model as code", new object[] { predictor.GetType().Name });
		}
	}
}
