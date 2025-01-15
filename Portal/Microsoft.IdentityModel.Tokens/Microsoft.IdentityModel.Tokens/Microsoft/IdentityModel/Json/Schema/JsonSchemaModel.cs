using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Json.Linq;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Schema
{
	// Token: 0x020000AD RID: 173
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaModel
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00026E5C File Offset: 0x0002505C
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x00026E64 File Offset: 0x00025064
		public bool Required { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00026E6D File Offset: 0x0002506D
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x00026E75 File Offset: 0x00025075
		public JsonSchemaType Type { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00026E7E File Offset: 0x0002507E
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x00026E86 File Offset: 0x00025086
		public int? MinimumLength { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00026E8F File Offset: 0x0002508F
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x00026E97 File Offset: 0x00025097
		public int? MaximumLength { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00026EA0 File Offset: 0x000250A0
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x00026EA8 File Offset: 0x000250A8
		public double? DivisibleBy { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00026EB1 File Offset: 0x000250B1
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x00026EB9 File Offset: 0x000250B9
		public double? Minimum { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00026EC2 File Offset: 0x000250C2
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x00026ECA File Offset: 0x000250CA
		public double? Maximum { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00026ED3 File Offset: 0x000250D3
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x00026EDB File Offset: 0x000250DB
		public bool ExclusiveMinimum { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00026EE4 File Offset: 0x000250E4
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x00026EEC File Offset: 0x000250EC
		public bool ExclusiveMaximum { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00026EF5 File Offset: 0x000250F5
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x00026EFD File Offset: 0x000250FD
		public int? MinimumItems { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00026F06 File Offset: 0x00025106
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x00026F0E File Offset: 0x0002510E
		public int? MaximumItems { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00026F17 File Offset: 0x00025117
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x00026F1F File Offset: 0x0002511F
		public IList<string> Patterns { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00026F28 File Offset: 0x00025128
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00026F30 File Offset: 0x00025130
		public IList<JsonSchemaModel> Items { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00026F39 File Offset: 0x00025139
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x00026F41 File Offset: 0x00025141
		public IDictionary<string, JsonSchemaModel> Properties { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00026F4A File Offset: 0x0002514A
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x00026F52 File Offset: 0x00025152
		public IDictionary<string, JsonSchemaModel> PatternProperties { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00026F5B File Offset: 0x0002515B
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x00026F63 File Offset: 0x00025163
		public JsonSchemaModel AdditionalProperties { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00026F6C File Offset: 0x0002516C
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x00026F74 File Offset: 0x00025174
		public JsonSchemaModel AdditionalItems { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00026F7D File Offset: 0x0002517D
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x00026F85 File Offset: 0x00025185
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00026F8E File Offset: 0x0002518E
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x00026F96 File Offset: 0x00025196
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00026F9F File Offset: 0x0002519F
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x00026FA7 File Offset: 0x000251A7
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00026FB0 File Offset: 0x000251B0
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x00026FB8 File Offset: 0x000251B8
		public bool UniqueItems { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00026FC1 File Offset: 0x000251C1
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x00026FC9 File Offset: 0x000251C9
		public IList<JToken> Enum { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00026FD2 File Offset: 0x000251D2
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x00026FDA File Offset: 0x000251DA
		public JsonSchemaType Disallow { get; set; }

		// Token: 0x0600094D RID: 2381 RVA: 0x00026FE3 File Offset: 0x000251E3
		public JsonSchemaModel()
		{
			this.Type = JsonSchemaType.Any;
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
			this.Required = false;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00027008 File Offset: 0x00025208
		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema jsonSchema in schemata)
			{
				JsonSchemaModel.Combine(jsonSchemaModel, jsonSchema);
			}
			return jsonSchemaModel;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00027058 File Offset: 0x00025258
		private static void Combine(JsonSchemaModel model, JsonSchema schema)
		{
			model.Required = model.Required || schema.Required.GetValueOrDefault();
			model.Type &= schema.Type ?? JsonSchemaType.Any;
			model.MinimumLength = MathUtils.Max(model.MinimumLength, schema.MinimumLength);
			model.MaximumLength = MathUtils.Min(model.MaximumLength, schema.MaximumLength);
			model.DivisibleBy = MathUtils.Max(model.DivisibleBy, schema.DivisibleBy);
			model.Minimum = MathUtils.Max(model.Minimum, schema.Minimum);
			model.Maximum = MathUtils.Max(model.Maximum, schema.Maximum);
			model.ExclusiveMinimum = model.ExclusiveMinimum || schema.ExclusiveMinimum.GetValueOrDefault();
			model.ExclusiveMaximum = model.ExclusiveMaximum || schema.ExclusiveMaximum.GetValueOrDefault();
			model.MinimumItems = MathUtils.Max(model.MinimumItems, schema.MinimumItems);
			model.MaximumItems = MathUtils.Min(model.MaximumItems, schema.MaximumItems);
			model.PositionalItemsValidation = model.PositionalItemsValidation || schema.PositionalItemsValidation;
			model.AllowAdditionalProperties = model.AllowAdditionalProperties && schema.AllowAdditionalProperties;
			model.AllowAdditionalItems = model.AllowAdditionalItems && schema.AllowAdditionalItems;
			model.UniqueItems = model.UniqueItems || schema.UniqueItems;
			if (schema.Enum != null)
			{
				if (model.Enum == null)
				{
					model.Enum = new List<JToken>();
				}
				model.Enum.AddRangeDistinct(schema.Enum, JToken.EqualityComparer);
			}
			model.Disallow |= schema.Disallow.GetValueOrDefault();
			if (schema.Pattern != null)
			{
				if (model.Patterns == null)
				{
					model.Patterns = new List<string>();
				}
				model.Patterns.AddDistinct(schema.Pattern);
			}
		}
	}
}
