using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000AC RID: 172
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaModel
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00026E78 File Offset: 0x00025078
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x00026E80 File Offset: 0x00025080
		public bool Required { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00026E89 File Offset: 0x00025089
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x00026E91 File Offset: 0x00025091
		public JsonSchemaType Type { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00026E9A File Offset: 0x0002509A
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x00026EA2 File Offset: 0x000250A2
		public int? MinimumLength { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00026EAB File Offset: 0x000250AB
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x00026EB3 File Offset: 0x000250B3
		public int? MaximumLength { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x00026EBC File Offset: 0x000250BC
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x00026EC4 File Offset: 0x000250C4
		public double? DivisibleBy { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00026ECD File Offset: 0x000250CD
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x00026ED5 File Offset: 0x000250D5
		public double? Minimum { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00026EDE File Offset: 0x000250DE
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x00026EE6 File Offset: 0x000250E6
		public double? Maximum { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00026EEF File Offset: 0x000250EF
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x00026EF7 File Offset: 0x000250F7
		public bool ExclusiveMinimum { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00026F00 File Offset: 0x00025100
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x00026F08 File Offset: 0x00025108
		public bool ExclusiveMaximum { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00026F11 File Offset: 0x00025111
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x00026F19 File Offset: 0x00025119
		public int? MinimumItems { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00026F22 File Offset: 0x00025122
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x00026F2A File Offset: 0x0002512A
		public int? MaximumItems { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00026F33 File Offset: 0x00025133
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x00026F3B File Offset: 0x0002513B
		public IList<string> Patterns { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00026F44 File Offset: 0x00025144
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x00026F4C File Offset: 0x0002514C
		public IList<JsonSchemaModel> Items { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00026F55 File Offset: 0x00025155
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x00026F5D File Offset: 0x0002515D
		public IDictionary<string, JsonSchemaModel> Properties { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00026F66 File Offset: 0x00025166
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x00026F6E File Offset: 0x0002516E
		public IDictionary<string, JsonSchemaModel> PatternProperties { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00026F77 File Offset: 0x00025177
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x00026F7F File Offset: 0x0002517F
		public JsonSchemaModel AdditionalProperties { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00026F88 File Offset: 0x00025188
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x00026F90 File Offset: 0x00025190
		public JsonSchemaModel AdditionalItems { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00026F99 File Offset: 0x00025199
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00026FA1 File Offset: 0x000251A1
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00026FAA File Offset: 0x000251AA
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x00026FB2 File Offset: 0x000251B2
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00026FBB File Offset: 0x000251BB
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x00026FC3 File Offset: 0x000251C3
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00026FCC File Offset: 0x000251CC
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x00026FD4 File Offset: 0x000251D4
		public bool UniqueItems { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00026FDD File Offset: 0x000251DD
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x00026FE5 File Offset: 0x000251E5
		public IList<JToken> Enum { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00026FEE File Offset: 0x000251EE
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x00026FF6 File Offset: 0x000251F6
		public JsonSchemaType Disallow { get; set; }

		// Token: 0x0600094C RID: 2380 RVA: 0x00026FFF File Offset: 0x000251FF
		public JsonSchemaModel()
		{
			this.Type = JsonSchemaType.Any;
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
			this.Required = false;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00027024 File Offset: 0x00025224
		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema jsonSchema in schemata)
			{
				JsonSchemaModel.Combine(jsonSchemaModel, jsonSchema);
			}
			return jsonSchemaModel;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00027074 File Offset: 0x00025274
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
