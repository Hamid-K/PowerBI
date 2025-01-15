using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000765 RID: 1893
	internal static class TypeCatalogExtensions
	{
		// Token: 0x060037DB RID: 14299 RVA: 0x000B2FCC File Offset: 0x000B11CC
		public static bool TryGetFunction(this IDictionary<ODataSchemaItem, TypeValue> typeCatalog, string functionName, out TypeValue value)
		{
			foreach (KeyValuePair<ODataSchemaItem, TypeValue> keyValuePair in typeCatalog)
			{
				if (keyValuePair.Key.Name.Equals(functionName))
				{
					value = keyValuePair.Value;
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000B3034 File Offset: 0x000B1234
		public static void AddFunction(this IDictionary<ODataSchemaItem, TypeValue> typeCatalog, ODataSchemaItem schemaItem, TypeValue type)
		{
			ODataSchemaItem odataSchemaItem;
			if (typeCatalog.TryGetKey(schemaItem.Name, out odataSchemaItem))
			{
				typeCatalog.Remove(odataSchemaItem);
			}
			typeCatalog[schemaItem] = type;
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000B3064 File Offset: 0x000B1264
		private static bool TryGetKey(this IDictionary<ODataSchemaItem, TypeValue> typeCatalog, string functionName, out ODataSchemaItem key)
		{
			foreach (KeyValuePair<ODataSchemaItem, TypeValue> keyValuePair in typeCatalog)
			{
				if (keyValuePair.Key.Name.Equals(functionName))
				{
					key = keyValuePair.Key;
					return true;
				}
			}
			key = null;
			return false;
		}
	}
}
