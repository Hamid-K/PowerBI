using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B0 RID: 176
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaResolver
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00027899 File Offset: 0x00025A99
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x000278A1 File Offset: 0x00025AA1
		public IList<JsonSchema> LoadedSchemas { get; protected set; }

		// Token: 0x06000969 RID: 2409 RVA: 0x000278AA File Offset: 0x00025AAA
		public JsonSchemaResolver()
		{
			this.LoadedSchemas = new List<JsonSchema>();
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x000278C0 File Offset: 0x00025AC0
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
