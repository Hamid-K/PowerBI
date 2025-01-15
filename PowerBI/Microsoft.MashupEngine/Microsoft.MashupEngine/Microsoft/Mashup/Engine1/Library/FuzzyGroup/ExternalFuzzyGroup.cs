using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.DataIntegration.FuzzyClustering;
using Microsoft.DataIntegration.FuzzyMatching;
using Microsoft.Mashup.Engine1.Library.FuzzyMatching;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyGroup
{
	// Token: 0x02000B5F RID: 2911
	internal class ExternalFuzzyGroup
	{
		// Token: 0x0600508D RID: 20621 RVA: 0x0010DA4E File Offset: 0x0010BC4E
		public static IEnumerable<DuplicateGroupWithValues> GetFuzzyGroups(DataTable inputTable, FuzzyGroupOptions fuzzyGroupOptions, List<RecordValue> records)
		{
			DuplicateGroup[] duplicateGroups = ExternalFuzzyGroup.GetDuplicateGroups(inputTable, fuzzyGroupOptions);
			BitArray isRecordGrouped = new BitArray(records.Count);
			new List<DuplicateGroupWithValues>();
			foreach (DuplicateGroup duplicateGroup in duplicateGroups)
			{
				RecordValue recordValue = records[duplicateGroup.RepresentativeId];
				RecordValue[] array2 = new RecordValue[duplicateGroup.Duplicates.Count];
				int num = 0;
				foreach (int num2 in duplicateGroup.Duplicates.Keys)
				{
					RecordValue recordValue2 = records[num2];
					if (fuzzyGroupOptions.SimilarityColumnName != null)
					{
						recordValue2 = Library.Record.AddField.Invoke(recordValue2, TextValue.New(fuzzyGroupOptions.SimilarityColumnName), NumberValue.New(FuzzyUtils.TruncateSimilarity((double)duplicateGroup.Duplicates[num2]))).AsRecord;
					}
					array2[num++] = recordValue2;
					isRecordGrouped[num2] = true;
				}
				yield return new DuplicateGroupWithValues(recordValue, array2);
			}
			DuplicateGroup[] array = null;
			int num3;
			for (int i = 0; i < records.Count; i = num3 + 1)
			{
				if (!isRecordGrouped[i])
				{
					RecordValue recordValue3 = records[i];
					if (fuzzyGroupOptions.SimilarityColumnName != null)
					{
						recordValue3 = Library.Record.AddField.Invoke(recordValue3, TextValue.New(fuzzyGroupOptions.SimilarityColumnName), NumberValue.New(1)).AsRecord;
					}
					RecordValue[] array3 = new RecordValue[] { recordValue3 };
					yield return new DuplicateGroupWithValues(records[i], array3);
				}
				num3 = i;
			}
			yield break;
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x0010DA6C File Offset: 0x0010BC6C
		public static Dictionary<string, RepresentativeValueWithSimilarity> GetRepresentativeValues(DataTable inputTable, FuzzyGroupOptions fuzzyGroupOptions, List<RecordValue> records, int columnIndex)
		{
			DuplicateGroup[] duplicateGroups = ExternalFuzzyGroup.GetDuplicateGroups(inputTable, fuzzyGroupOptions);
			Dictionary<string, RepresentativeValueWithSimilarity> dictionary = new Dictionary<string, RepresentativeValueWithSimilarity>();
			foreach (DuplicateGroup duplicateGroup in duplicateGroups)
			{
				foreach (int num in duplicateGroup.Duplicates.Keys)
				{
					Value value = records[num][columnIndex];
					if (value.IsText)
					{
						string @string = value.AsText.String;
						dictionary[@string] = new RepresentativeValueWithSimilarity(@string, records[duplicateGroup.RepresentativeId][columnIndex].AsText.String, FuzzyUtils.TruncateSimilarity((double)duplicateGroup.Duplicates[num]));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x0010DB4C File Offset: 0x0010BD4C
		public static FuzzyLookupEntry.FuzzyLookupParameters ToFuzzyLookupParameters(FuzzyGroupOptions fuzzyGroupOptions)
		{
			double threshold = fuzzyGroupOptions.Threshold;
			string cultureKey = fuzzyGroupOptions.CultureKey;
			return new FuzzyLookupEntry.FuzzyLookupParameters(threshold, fuzzyGroupOptions.IgnoreCase, fuzzyGroupOptions.IgnoreSpace, int.MaxValue, cultureKey, 1, false);
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x0010DB80 File Offset: 0x0010BD80
		private static DuplicateGroup[] GetDuplicateGroups(DataTable inputTable, FuzzyGroupOptions fuzzyGroupOptions)
		{
			FuzzyLookupEntry.FuzzyLookupParameters fuzzyLookupParameters = ExternalFuzzyGroup.ToFuzzyLookupParameters(fuzzyGroupOptions);
			DataTable dataTable = FuzzyDataTableCreator.CreateTransformationDataTableFromTableValue(fuzzyGroupOptions.TransformationTable);
			return FuzzyDedupEntry.Dedup(inputTable, fuzzyLookupParameters, dataTable, null).ToArray<DuplicateGroup>();
		}
	}
}
