using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000076 RID: 118
	public static class ReportExplorationNormalizer
	{
		// Token: 0x06000374 RID: 884 RVA: 0x00009F9D File Offset: 0x0000819D
		public static JObject NormalizeExploration(JObject serializedExploration, string prefixForFloatKeyStringValues)
		{
			serializedExploration = JsonProcessingHelper.SetFloatingPrecisionForKeys(serializedExploration, ReportExplorationNormalizer.floatKeys, 2, prefixForFloatKeyStringValues);
			serializedExploration = JsonProcessingHelper.RemoveUnnecessaryElements(serializedExploration);
			return ReportExplorationNormalizer.NormalizeObjectContent(serializedExploration, ReportExplorationNormalizer.removePropertiesOperators);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00009FC1 File Offset: 0x000081C1
		public static JObject NormalizeV2Exploration(JObject v2ExplorationReport)
		{
			return ReportExplorationNormalizer.NormalizeObjectContent(v2ExplorationReport, ReportExplorationNormalizer.removeV2PropertiesOperators);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00009FCE File Offset: 0x000081CE
		private static JObject NormalizeObjectContent(JObject content, IList<JsonProcessingHelper.JsonOperator> operators)
		{
			return JsonProcessingHelper.NormalizeJsonWithOperators(content, operators).NormalizedJObject;
		}

		// Token: 0x040001CF RID: 463
		private static readonly IList<JsonProcessingHelper.JsonOperator> removePropertiesOperators = new JsonProcessingHelper.JsonOperator[]
		{
			JsonProcessingHelper.JsonOperator.ForParentProperty("$..id", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty("$..reportId", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty("$..objectId", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty("$..explorationId", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty("$..resourcePackageId", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty("$..resourcePackageItemBlobInfoId", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty("$.sections[*].visualContainers[*].query", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty("$.sections[*].visualContainers[*].queryHash", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty("$.sections[*].visualContainers[*].dataBinaryBase64Encoded", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty("$.sections[*].visualContainers[*].dataTransforms", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty("$.sections[*].visualContainers[*].dataUpdatedTime", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken))
		};

		// Token: 0x040001D0 RID: 464
		private static readonly IList<JsonProcessingHelper.JsonOperator> removeV2PropertiesOperators = new JsonProcessingHelper.JsonOperator[] { JsonProcessingHelper.JsonOperator.ForParentProperty("$.resourcePackages[*]..id", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)) };

		// Token: 0x040001D1 RID: 465
		public static readonly List<KeyAndType> sortOrder = new List<KeyAndType>
		{
			new KeyAndType("name", JTokenType.String),
			new KeyAndType("config", JTokenType.String)
		};

		// Token: 0x040001D2 RID: 466
		private static readonly string[] floatKeys = new string[] { "width", "height", "x", "y", "z" };
	}
}
