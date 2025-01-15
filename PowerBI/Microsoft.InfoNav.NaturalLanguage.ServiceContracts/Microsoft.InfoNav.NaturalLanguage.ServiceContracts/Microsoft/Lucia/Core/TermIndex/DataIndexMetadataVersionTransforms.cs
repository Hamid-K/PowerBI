using System;
using System.Runtime.CompilerServices;
using Microsoft.Lucia.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x0200015E RID: 350
	internal static class DataIndexMetadataVersionTransforms
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		internal static bool TryUpgrade(JObject metadata, Version sourceVersion, Version targetVersion)
		{
			DataIndexVersion dataIndexVersion;
			DataIndexVersion dataIndexVersion2;
			if (sourceVersion > targetVersion || !DataIndexVersion.SupportedVersions.TryGetValue(sourceVersion, out dataIndexVersion) || !DataIndexVersion.SupportedVersions.TryGetValue(targetVersion, out dataIndexVersion2))
			{
				return false;
			}
			while (dataIndexVersion.Value != targetVersion)
			{
				if (dataIndexVersion.Next == null)
				{
					return false;
				}
				IDataIndexMetadataUpgradeTransform upgradeTransform = dataIndexVersion.UpgradeTransform;
				if (upgradeTransform != null)
				{
					upgradeTransform.Upgrade(metadata);
				}
				dataIndexVersion = DataIndexVersion.SupportedVersions[dataIndexVersion.Next];
			}
			return true;
		}

		// Token: 0x0200020F RID: 527
		internal class V1_0ToV1_1 : IDataIndexMetadataUpgradeTransform
		{
			// Token: 0x06000B4D RID: 2893 RVA: 0x00014F48 File Offset: 0x00013148
			internal V1_0ToV1_1()
			{
			}

			// Token: 0x17000336 RID: 822
			// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00014F50 File Offset: 0x00013150
			public virtual Version SourceVersion
			{
				get
				{
					return DataIndexVersion.V1_0;
				}
			}

			// Token: 0x17000337 RID: 823
			// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00014F57 File Offset: 0x00013157
			public Version TargetVersion
			{
				get
				{
					return DataIndexVersion.V1_1;
				}
			}

			// Token: 0x06000B50 RID: 2896 RVA: 0x00014F60 File Offset: 0x00013160
			public void Upgrade(JObject metadata)
			{
				JArray jarray = metadata["IndexedElements"] as JArray;
				if (jarray == null)
				{
					jarray = new JArray();
					metadata["IndexedElements"] = jarray;
				}
				JArray jarray2 = metadata["SkippedElements"] as JArray;
				if (jarray2 != null)
				{
					JArray jarray3 = jarray2;
					Action<JObject> action;
					if ((action = DataIndexMetadataVersionTransforms.V1_0ToV1_1.<>O.<0>__UpgradeSkippedElement) == null)
					{
						action = (DataIndexMetadataVersionTransforms.V1_0ToV1_1.<>O.<0>__UpgradeSkippedElement = new Action<JObject>(DataIndexMetadataVersionTransforms.V1_0ToV1_1.UpgradeSkippedElement));
					}
					jarray3.VisitArrayElements(action);
					foreach (JToken jtoken in jarray2.Children())
					{
						jarray.Add(jtoken);
					}
					metadata.Remove("SkippedElements");
				}
				JArray jarray4 = jarray;
				Action<JObject> action2;
				if ((action2 = DataIndexMetadataVersionTransforms.V1_0ToV1_1.<>O.<1>__UpgradeIndexedElement) == null)
				{
					action2 = (DataIndexMetadataVersionTransforms.V1_0ToV1_1.<>O.<1>__UpgradeIndexedElement = new Action<JObject>(DataIndexMetadataVersionTransforms.V1_0ToV1_1.UpgradeIndexedElement));
				}
				jarray4.VisitArrayElements(action2);
			}

			// Token: 0x06000B51 RID: 2897 RVA: 0x0001503C File Offset: 0x0001323C
			private static void UpgradeIndexedElement(JObject indexedElement)
			{
				if (!indexedElement.HasProperty("Status"))
				{
					indexedElement.Add("Status", "Indexed");
				}
			}

			// Token: 0x06000B52 RID: 2898 RVA: 0x00015060 File Offset: 0x00013260
			private static void UpgradeSkippedElement(JObject skippedElement)
			{
				skippedElement["Status"] = "IndexLimitReached";
			}

			// Token: 0x04000844 RID: 2116
			private const string IndexedElementsKey = "IndexedElements";

			// Token: 0x04000845 RID: 2117
			private const string SkippedElementsKey = "SkippedElements";

			// Token: 0x04000846 RID: 2118
			private const string StatusKey = "Status";

			// Token: 0x04000847 RID: 2119
			private const string IndexedStatusValue = "Indexed";

			// Token: 0x04000848 RID: 2120
			private const string IndexLimitReachedStatusValue = "IndexLimitReached";

			// Token: 0x02000256 RID: 598
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04000996 RID: 2454
				public static Action<JObject> <0>__UpgradeSkippedElement;

				// Token: 0x04000997 RID: 2455
				public static Action<JObject> <1>__UpgradeIndexedElement;
			}
		}
	}
}
