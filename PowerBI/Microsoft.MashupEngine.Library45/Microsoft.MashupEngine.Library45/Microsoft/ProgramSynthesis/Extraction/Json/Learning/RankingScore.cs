using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Wrangling.Json;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning
{
	// Token: 0x02000B81 RID: 2945
	public class RankingScore : Feature<double>
	{
		// Token: 0x06004AB3 RID: 19123 RVA: 0x000BFE5F File Offset: 0x000BE05F
		public RankingScore(Grammar grammar)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x0001AF59 File Offset: 0x00019159
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0;
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("Sequence")]
		public static double Score_Sequence(double id, double selectSequence)
		{
			return selectSequence;
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("DummySequence")]
		public static double Score_DummySequence(double sequenceBody)
		{
			return sequenceBody;
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("Struct")]
		public static double Score_Struct(double v, double structBodyRec)
		{
			return structBodyRec;
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x0003B61D File Offset: 0x0003981D
		[FeatureCalculator("Field")]
		public static double Score_Field(double v, double id, double selectRegion)
		{
			return selectRegion;
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("SequenceBody")]
		public static double Score_SequenceBody(double wrapStruct, double sequence)
		{
			return wrapStruct + sequence;
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("Concat")]
		public static double Score_Concat(double output, double structBodyRec)
		{
			return output + structBodyRec;
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("ToList")]
		public static double Score_ToList(double output)
		{
			return output;
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("Empty")]
		public static double Score_Empty()
		{
			return 0.0;
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("SelectRegion")]
		public static double Score_SelectRegion(double v, double path)
		{
			return path;
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("SelectSequence")]
		public static double Score_SelectSequence(double v, double path)
		{
			return path;
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x000EB288 File Offset: 0x000E9488
		[FeatureCalculator("path", Method = CalculationMethod.FromLiteral)]
		public static double Score_Path(JPath path)
		{
			return path.Score;
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("id", Method = CalculationMethod.FromLiteral)]
		public static double Score_Id(string id)
		{
			return 0.0;
		}
	}
}
