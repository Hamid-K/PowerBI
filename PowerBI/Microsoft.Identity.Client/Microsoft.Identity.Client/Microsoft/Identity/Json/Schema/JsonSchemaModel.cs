using System;
using System.Collections.Generic;
using Microsoft.Identity.Json.Linq;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Schema
{
	// Token: 0x020000AC RID: 172
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaModel
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000267F0 File Offset: 0x000249F0
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x000267F8 File Offset: 0x000249F8
		public bool Required { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00026801 File Offset: 0x00024A01
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00026809 File Offset: 0x00024A09
		public JsonSchemaType Type { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00026812 File Offset: 0x00024A12
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0002681A File Offset: 0x00024A1A
		public int? MinimumLength { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00026823 File Offset: 0x00024A23
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0002682B File Offset: 0x00024A2B
		public int? MaximumLength { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00026834 File Offset: 0x00024A34
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x0002683C File Offset: 0x00024A3C
		public double? DivisibleBy { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00026845 File Offset: 0x00024A45
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x0002684D File Offset: 0x00024A4D
		public double? Minimum { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00026856 File Offset: 0x00024A56
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x0002685E File Offset: 0x00024A5E
		public double? Maximum { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00026867 File Offset: 0x00024A67
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0002686F File Offset: 0x00024A6F
		public bool ExclusiveMinimum { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00026878 File Offset: 0x00024A78
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x00026880 File Offset: 0x00024A80
		public bool ExclusiveMaximum { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00026889 File Offset: 0x00024A89
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x00026891 File Offset: 0x00024A91
		public int? MinimumItems { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0002689A File Offset: 0x00024A9A
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x000268A2 File Offset: 0x00024AA2
		public int? MaximumItems { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x000268AB File Offset: 0x00024AAB
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x000268B3 File Offset: 0x00024AB3
		public IList<string> Patterns { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000268BC File Offset: 0x00024ABC
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x000268C4 File Offset: 0x00024AC4
		public IList<JsonSchemaModel> Items { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x000268CD File Offset: 0x00024ACD
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x000268D5 File Offset: 0x00024AD5
		public IDictionary<string, JsonSchemaModel> Properties { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x000268DE File Offset: 0x00024ADE
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x000268E6 File Offset: 0x00024AE6
		public IDictionary<string, JsonSchemaModel> PatternProperties { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000268EF File Offset: 0x00024AEF
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x000268F7 File Offset: 0x00024AF7
		public JsonSchemaModel AdditionalProperties { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00026900 File Offset: 0x00024B00
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x00026908 File Offset: 0x00024B08
		public JsonSchemaModel AdditionalItems { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00026911 File Offset: 0x00024B11
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00026919 File Offset: 0x00024B19
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00026922 File Offset: 0x00024B22
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x0002692A File Offset: 0x00024B2A
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00026933 File Offset: 0x00024B33
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x0002693B File Offset: 0x00024B3B
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00026944 File Offset: 0x00024B44
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x0002694C File Offset: 0x00024B4C
		public bool UniqueItems { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00026955 File Offset: 0x00024B55
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x0002695D File Offset: 0x00024B5D
		public IList<JToken> Enum { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00026966 File Offset: 0x00024B66
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x0002696E File Offset: 0x00024B6E
		public JsonSchemaType Disallow { get; set; }

		// Token: 0x06000943 RID: 2371 RVA: 0x00026977 File Offset: 0x00024B77
		public JsonSchemaModel()
		{
			this.Type = JsonSchemaType.Any;
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
			this.Required = false;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0002699C File Offset: 0x00024B9C
		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema jsonSchema in schemata)
			{
				JsonSchemaModel.Combine(jsonSchemaModel, jsonSchema);
			}
			return jsonSchemaModel;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000269EC File Offset: 0x00024BEC
		private static void Combine(JsonSchemaModel model, JsonSchema schema)
		{
			model.Required = model.Required || schema.Required.GetValueOrDefault();
			model.Type &= schema.Type.GetValueOrDefault(JsonSchemaType.Any);
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
