using System;
using System.Collections.ObjectModel;

namespace Microsoft.IdentityModel.Json.Schema
{
	// Token: 0x020000B0 RID: 176
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNodeCollection : KeyedCollection<string, JsonSchemaNode>
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x0002786D File Offset: 0x00025A6D
		protected override string GetKeyForItem(JsonSchemaNode item)
		{
			return item.Id;
		}
	}
}
