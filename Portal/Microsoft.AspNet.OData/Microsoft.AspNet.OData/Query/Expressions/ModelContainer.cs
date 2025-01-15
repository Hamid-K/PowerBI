using System;
using System.Collections.Concurrent;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000F4 RID: 244
	internal static class ModelContainer
	{
		// Token: 0x06000853 RID: 2131 RVA: 0x0002085C File Offset: 0x0001EA5C
		public static string GetModelID(IEdmModel model)
		{
			string orAdd = ModelContainer._map.GetOrAdd(model, (IEdmModel m) => Guid.NewGuid().ToString());
			ModelContainer._reverseMap.TryAdd(orAdd, model);
			return orAdd;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000208A2 File Offset: 0x0001EAA2
		public static IEdmModel GetModel(string id)
		{
			return ModelContainer._reverseMap[id];
		}

		// Token: 0x04000278 RID: 632
		private static ConcurrentDictionary<IEdmModel, string> _map = new ConcurrentDictionary<IEdmModel, string>();

		// Token: 0x04000279 RID: 633
		private static ConcurrentDictionary<string, IEdmModel> _reverseMap = new ConcurrentDictionary<string, IEdmModel>();
	}
}
