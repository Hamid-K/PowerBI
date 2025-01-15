using System;
using System.Collections.Generic;
using Microsoft.Data.DeltaLake.Commands;
using Microsoft.Data.DeltaLake.Serialization;
using Microsoft.Data.DeltaLake.Types;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.DeltaLake
{
	// Token: 0x02001EF9 RID: 7929
	internal static class DeltaStatistics
	{
		// Token: 0x06010B3F RID: 68415 RVA: 0x003982F0 File Offset: 0x003964F0
		public static string Convert(Metadata metadata, List<KeyValuePair<string, ListStatistics>> statistics, long numRecords)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(metadata.Schema.Fields.Length);
			if (metadata.HasColumnMappingEnabled)
			{
				for (int i = 0; i < metadata.Schema.Fields.Length; i++)
				{
					string physicalNameOrNull = metadata.Schema.Fields[i].PhysicalNameOrNull;
					if (physicalNameOrNull != null)
					{
						dictionary[physicalNameOrNull] = metadata.Schema.Fields[i].Name;
					}
				}
			}
			Dictionary<string, long> dictionary2 = new Dictionary<string, long>();
			Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
			foreach (KeyValuePair<string, ListStatistics> keyValuePair in statistics)
			{
				string text = keyValuePair.Key;
				string text2;
				if (metadata.HasColumnMappingEnabled && dictionary.TryGetValue(text, out text2))
				{
					text = text2;
				}
				long asInteger = keyValuePair.Value.NullCount.AsNumber.AsInteger64;
				dictionary2[text] = asInteger;
				if (asInteger < numRecords)
				{
					dictionary3[text] = ValueMarshaller.MarshalToClr(keyValuePair.Value.Minimum);
					dictionary4[text] = ValueMarshaller.MarshalToClr(keyValuePair.Value.Maximum);
				}
			}
			Statistics statistics2 = new Statistics
			{
				NumRecords = new long?(numRecords),
				NullCount = dictionary2,
				MinValues = dictionary3,
				MaxValues = dictionary4
			};
			return StatisticsSerializer.Serialize(metadata.Schema, statistics2);
		}

		// Token: 0x06010B40 RID: 68416 RVA: 0x0039846C File Offset: 0x0039666C
		public static string CreateEmptyStatistics(long numRecords)
		{
			return StatisticsSerializer.Serialize(new StructType(), new Statistics
			{
				NumRecords = new long?(numRecords)
			});
		}
	}
}
