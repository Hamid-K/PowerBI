using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000078 RID: 120
	public interface ITomJsonProcessingHelper
	{
		// Token: 0x0600037E RID: 894
		JsonOperatorResult SortAllJsonModelFields(JsonOperatorResult jsonResult);

		// Token: 0x0600037F RID: 895
		JsonOperatorResult SortJsonModelFieldsForGit(JsonOperatorResult jsonResult);

		// Token: 0x06000380 RID: 896
		JsonOperatorResult RemoveVolatileFields(JObject serializedTomDatabase, IEnumerable<JsonProcessingHelper.JsonOperator> jsonOperators = null);

		// Token: 0x06000381 RID: 897
		JsonOperatorResult RemoveVolatileFields(string serializedTomDatabase, IEnumerable<JsonProcessingHelper.JsonOperator> jsonOperators = null);
	}
}
