using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Identity.Json.Schema
{
	// Token: 0x020000B0 RID: 176
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaResolver
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00027205 File Offset: 0x00025405
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x0002720D File Offset: 0x0002540D
		public IList<JsonSchema> LoadedSchemas { get; protected set; }

		// Token: 0x06000960 RID: 2400 RVA: 0x00027216 File Offset: 0x00025416
		public JsonSchemaResolver()
		{
			this.LoadedSchemas = new List<JsonSchema>();
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0002722C File Offset: 0x0002542C
		public virtual JsonSchema GetSchema(string reference)
		{
			JsonSchema jsonSchema = this.LoadedSchemas.SingleOrDefault((JsonSchema s) => string.Equals(s.Id, reference, StringComparison.Ordinal));
			if (jsonSchema == null)
			{
				jsonSchema = this.LoadedSchemas.SingleOrDefault((JsonSchema s) => string.Equals(s.Location, reference, StringComparison.Ordinal));
			}
			return jsonSchema;
		}
	}
}
