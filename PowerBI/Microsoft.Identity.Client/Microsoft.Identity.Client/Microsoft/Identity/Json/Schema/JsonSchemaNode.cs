using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Identity.Json.Schema
{
	// Token: 0x020000AE RID: 174
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNode
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0002704A File Offset: 0x0002524A
		public string Id { get; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00027052 File Offset: 0x00025252
		public ReadOnlyCollection<JsonSchema> Schemas { get; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0002705A File Offset: 0x0002525A
		public Dictionary<string, JsonSchemaNode> Properties { get; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00027062 File Offset: 0x00025262
		public Dictionary<string, JsonSchemaNode> PatternProperties { get; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0002706A File Offset: 0x0002526A
		public List<JsonSchemaNode> Items { get; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00027072 File Offset: 0x00025272
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x0002707A File Offset: 0x0002527A
		public JsonSchemaNode AdditionalProperties { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00027083 File Offset: 0x00025283
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0002708B File Offset: 0x0002528B
		public JsonSchemaNode AdditionalItems { get; set; }

		// Token: 0x06000958 RID: 2392 RVA: 0x00027094 File Offset: 0x00025294
		public JsonSchemaNode(JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(new JsonSchema[] { schema });
			this.Properties = new Dictionary<string, JsonSchemaNode>();
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>();
			this.Items = new List<JsonSchemaNode>();
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000270F0 File Offset: 0x000252F0
		private JsonSchemaNode(JsonSchemaNode source, JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(source.Schemas.Union(new JsonSchema[] { schema }).ToList<JsonSchema>());
			this.Properties = new Dictionary<string, JsonSchemaNode>(source.Properties);
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>(source.PatternProperties);
			this.Items = new List<JsonSchemaNode>(source.Items);
			this.AdditionalProperties = source.AdditionalProperties;
			this.AdditionalItems = source.AdditionalItems;
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00027184 File Offset: 0x00025384
		public JsonSchemaNode Combine(JsonSchema schema)
		{
			return new JsonSchemaNode(this, schema);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00027190 File Offset: 0x00025390
		public static string GetId(IEnumerable<JsonSchema> schemata)
		{
			return string.Join("-", schemata.Select((JsonSchema s) => s.InternalId).OrderBy((string id) => id, StringComparer.Ordinal));
		}
	}
}
