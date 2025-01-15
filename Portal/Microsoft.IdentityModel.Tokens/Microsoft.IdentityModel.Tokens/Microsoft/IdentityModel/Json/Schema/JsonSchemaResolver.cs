using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.IdentityModel.Json.Schema
{
	// Token: 0x020000B1 RID: 177
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaResolver
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0002787D File Offset: 0x00025A7D
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x00027885 File Offset: 0x00025A85
		public IList<JsonSchema> LoadedSchemas { get; protected set; }

		// Token: 0x0600096A RID: 2410 RVA: 0x0002788E File Offset: 0x00025A8E
		public JsonSchemaResolver()
		{
			this.LoadedSchemas = new List<JsonSchema>();
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x000278A4 File Offset: 0x00025AA4
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
