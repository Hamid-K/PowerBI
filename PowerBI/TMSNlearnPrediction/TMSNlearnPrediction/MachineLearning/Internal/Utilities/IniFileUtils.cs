using System;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Internal.Utilities
{
	// Token: 0x020004AA RID: 1194
	public static class IniFileUtils
	{
		// Token: 0x060018AB RID: 6315 RVA: 0x0008BD70 File Offset: 0x00089F70
		public static string AddEvaluator(string ini, string evaluator)
		{
			int num = IniFileUtils.NumEvaluators(ini);
			if (!ini.Contains("Evaluators=" + num))
			{
				throw Contracts.ExceptNotImpl("Need to make the replacing of Evaluators= more robust");
			}
			ini = ini.Replace("Evaluators=" + num, "Evaluators=" + (num + 1));
			StringBuilder stringBuilder = new StringBuilder(ini);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Evaluator:" + (num + 1) + "]");
			stringBuilder.AppendLine(evaluator);
			return stringBuilder.ToString();
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0008BE14 File Offset: 0x0008A014
		public static int NumEvaluators(string ini)
		{
			Regex regex = new Regex("Evaluators=([0-9]+)");
			Match match = regex.Match(ini);
			Contracts.Check(match.Success, "Unable to retrieve number of evaluators from ini");
			string value = match.Groups[1].Value;
			return int.Parse(value);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0008BE5C File Offset: 0x0008A05C
		public static string GetCalibratorEvaluatorIni(string originalIni, PlattCalibrator calibrator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("EvaluatorType=Aggregator");
			stringBuilder.AppendLine("Type=Sigmoid");
			stringBuilder.AppendLine("Bias=" + -calibrator.ParamB);
			stringBuilder.AppendLine("NumNodes=1");
			stringBuilder.AppendLine("Nodes=E:" + IniFileUtils.NumEvaluators(originalIni));
			stringBuilder.AppendLine("Weights=" + -calibrator.ParamA);
			return stringBuilder.ToString();
		}
	}
}
