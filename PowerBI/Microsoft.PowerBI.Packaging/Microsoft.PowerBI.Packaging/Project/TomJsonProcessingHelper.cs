using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000079 RID: 121
	public class TomJsonProcessingHelper : ITomJsonProcessingHelper
	{
		// Token: 0x06000382 RID: 898 RVA: 0x0000A225 File Offset: 0x00008425
		public JsonOperatorResult SortAllJsonModelFields(JsonOperatorResult jsonResult)
		{
			return this.SortJsonModelFieldsInternal(jsonResult, null);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000A22F File Offset: 0x0000842F
		public JsonOperatorResult SortJsonModelFieldsForGit(JsonOperatorResult jsonResult)
		{
			return this.SortJsonModelFieldsInternal(jsonResult, null);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000A239 File Offset: 0x00008439
		public JsonOperatorResult RemoveVolatileFields(string serializedTomDatabase, IEnumerable<JsonProcessingHelper.JsonOperator> jsonOperators = null)
		{
			return this.RemoveVolatileFields(JObject.Parse(serializedTomDatabase), jsonOperators);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000A248 File Offset: 0x00008448
		public JsonOperatorResult RemoveVolatileFields(JObject serializedTomDatabase, IEnumerable<JsonProcessingHelper.JsonOperator> jsonOperators = null)
		{
			jsonOperators = jsonOperators ?? Enumerable.Empty<JsonProcessingHelper.JsonOperator>();
			List<JsonProcessingHelper.JsonOperator> list = TomJsonProcessingHelper.volatilePropertiesTomJsonOperators.Concat(jsonOperators).ToList<JsonProcessingHelper.JsonOperator>();
			return JsonProcessingHelper.NormalizeJsonWithOperators(serializedTomDatabase, list);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000A279 File Offset: 0x00008479
		private JsonOperatorResult SortJsonModelFieldsInternal(JsonOperatorResult jsonResult, string[] sortedArrays = null)
		{
			return new JsonOperatorResult(JsonProcessingHelper.JsonOperator.SortJsonObjectTree(jsonResult.NormalizedJObject, "name", TomJsonProcessingHelper.sortOrder, sortedArrays, false), jsonResult.OperandWarningsCount);
		}

		// Token: 0x040001D5 RID: 469
		private const string nameProperty = "name";

		// Token: 0x040001D6 RID: 470
		private const string ordinalProperty = "ordinal";

		// Token: 0x040001D7 RID: 471
		private static readonly IList<JsonProcessingHelper.JsonOperator> volatilePropertiesTomJsonOperators = new JsonProcessingHelper.JsonOperator[]
		{
			JsonProcessingHelper.JsonOperator.ForParentProperty("$..state", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty("$..errorMessage", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty("$.tables[?(@.refreshPolicy)].partitions", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForSelfObject("$.tables[?(@.systemManaged)]", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty(".id", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken)),
			JsonProcessingHelper.JsonOperator.ForParentProperty(".name", new Action<JToken>(JsonProcessingHelper.JsonOperator.RemoveToken))
		};

		// Token: 0x040001D8 RID: 472
		private static readonly List<KeyAndType> sortOrder = new List<KeyAndType>
		{
			new KeyAndType("ordinal", JTokenType.Integer),
			new KeyAndType("name", JTokenType.String)
		};
	}
}
