using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Identity.Json.Linq;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Schema
{
	// Token: 0x020000A7 RID: 167
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchema
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00024938 File Offset: 0x00022B38
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x00024940 File Offset: 0x00022B40
		public string Id { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00024949 File Offset: 0x00022B49
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x00024951 File Offset: 0x00022B51
		public string Title { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0002495A File Offset: 0x00022B5A
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x00024962 File Offset: 0x00022B62
		public bool? Required { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0002496B File Offset: 0x00022B6B
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x00024973 File Offset: 0x00022B73
		public bool? ReadOnly { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0002497C File Offset: 0x00022B7C
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x00024984 File Offset: 0x00022B84
		public bool? Hidden { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0002498D File Offset: 0x00022B8D
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x00024995 File Offset: 0x00022B95
		public bool? Transient { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0002499E File Offset: 0x00022B9E
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x000249A6 File Offset: 0x00022BA6
		public string Description { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x000249AF File Offset: 0x00022BAF
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x000249B7 File Offset: 0x00022BB7
		public JsonSchemaType? Type { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000249C0 File Offset: 0x00022BC0
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x000249C8 File Offset: 0x00022BC8
		public string Pattern { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x000249D1 File Offset: 0x00022BD1
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x000249D9 File Offset: 0x00022BD9
		public int? MinimumLength { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x000249E2 File Offset: 0x00022BE2
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x000249EA File Offset: 0x00022BEA
		public int? MaximumLength { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x000249F3 File Offset: 0x00022BF3
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x000249FB File Offset: 0x00022BFB
		public double? DivisibleBy { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00024A04 File Offset: 0x00022C04
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x00024A0C File Offset: 0x00022C0C
		public double? Minimum { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00024A15 File Offset: 0x00022C15
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x00024A1D File Offset: 0x00022C1D
		public double? Maximum { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00024A26 File Offset: 0x00022C26
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00024A2E File Offset: 0x00022C2E
		public bool? ExclusiveMinimum { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00024A37 File Offset: 0x00022C37
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00024A3F File Offset: 0x00022C3F
		public bool? ExclusiveMaximum { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00024A48 File Offset: 0x00022C48
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00024A50 File Offset: 0x00022C50
		public int? MinimumItems { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00024A59 File Offset: 0x00022C59
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00024A61 File Offset: 0x00022C61
		public int? MaximumItems { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00024A6A File Offset: 0x00022C6A
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00024A72 File Offset: 0x00022C72
		public IList<JsonSchema> Items { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00024A7B File Offset: 0x00022C7B
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x00024A83 File Offset: 0x00022C83
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00024A8C File Offset: 0x00022C8C
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x00024A94 File Offset: 0x00022C94
		public JsonSchema AdditionalItems { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00024A9D File Offset: 0x00022C9D
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x00024AA5 File Offset: 0x00022CA5
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00024AAE File Offset: 0x00022CAE
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x00024AB6 File Offset: 0x00022CB6
		public bool UniqueItems { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00024ABF File Offset: 0x00022CBF
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x00024AC7 File Offset: 0x00022CC7
		public IDictionary<string, JsonSchema> Properties { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00024AD0 File Offset: 0x00022CD0
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x00024AD8 File Offset: 0x00022CD8
		public JsonSchema AdditionalProperties { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00024AE1 File Offset: 0x00022CE1
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x00024AE9 File Offset: 0x00022CE9
		public IDictionary<string, JsonSchema> PatternProperties { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00024AF2 File Offset: 0x00022CF2
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x00024AFA File Offset: 0x00022CFA
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00024B03 File Offset: 0x00022D03
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x00024B0B File Offset: 0x00022D0B
		public string Requires { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00024B14 File Offset: 0x00022D14
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x00024B1C File Offset: 0x00022D1C
		public IList<JToken> Enum { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00024B25 File Offset: 0x00022D25
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x00024B2D File Offset: 0x00022D2D
		public JsonSchemaType? Disallow { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00024B36 File Offset: 0x00022D36
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x00024B3E File Offset: 0x00022D3E
		public JToken Default { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00024B47 File Offset: 0x00022D47
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x00024B4F File Offset: 0x00022D4F
		public IList<JsonSchema> Extends { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00024B58 File Offset: 0x00022D58
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x00024B60 File Offset: 0x00022D60
		public string Format { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00024B69 File Offset: 0x00022D69
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x00024B71 File Offset: 0x00022D71
		internal string Location { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00024B7A File Offset: 0x00022D7A
		internal string InternalId
		{
			get
			{
				return this._internalId;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00024B82 File Offset: 0x00022D82
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x00024B8A File Offset: 0x00022D8A
		internal string DeferredReference { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00024B93 File Offset: 0x00022D93
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x00024B9B File Offset: 0x00022D9B
		internal bool ReferencesResolved { get; set; }

		// Token: 0x060008DC RID: 2268 RVA: 0x00024BA4 File Offset: 0x00022DA4
		public JsonSchema()
		{
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00024BDD File Offset: 0x00022DDD
		public static JsonSchema Read(JsonReader reader)
		{
			return JsonSchema.Read(reader, new JsonSchemaResolver());
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00024BEA File Offset: 0x00022DEA
		public static JsonSchema Read(JsonReader reader, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			return new JsonSchemaBuilder(resolver).Read(reader);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00024C0E File Offset: 0x00022E0E
		public static JsonSchema Parse(string json)
		{
			return JsonSchema.Parse(json, new JsonSchemaResolver());
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00024C1C File Offset: 0x00022E1C
		public static JsonSchema Parse(string json, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(json, "json");
			JsonSchema jsonSchema;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				jsonSchema = JsonSchema.Read(jsonReader, resolver);
			}
			return jsonSchema;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00024C68 File Offset: 0x00022E68
		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, new JsonSchemaResolver());
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00024C76 File Offset: 0x00022E76
		public void WriteTo(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			new JsonSchemaWriter(writer, resolver).WriteSchema(this);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00024C9C File Offset: 0x00022E9C
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteTo(new JsonTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			return stringWriter.ToString();
		}

		// Token: 0x040002F4 RID: 756
		private readonly string _internalId = Guid.NewGuid().ToString("N");
	}
}
