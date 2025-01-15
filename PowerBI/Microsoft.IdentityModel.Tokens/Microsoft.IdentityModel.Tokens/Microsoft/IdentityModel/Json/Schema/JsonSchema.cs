using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.IdentityModel.Json.Linq;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Schema
{
	// Token: 0x020000A8 RID: 168
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchema
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00024F9C File Offset: 0x0002319C
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x00024FA4 File Offset: 0x000231A4
		public string Id { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00024FAD File Offset: 0x000231AD
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x00024FB5 File Offset: 0x000231B5
		public string Title { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00024FBE File Offset: 0x000231BE
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x00024FC6 File Offset: 0x000231C6
		public bool? Required { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00024FCF File Offset: 0x000231CF
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x00024FD7 File Offset: 0x000231D7
		public bool? ReadOnly { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00024FE0 File Offset: 0x000231E0
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x00024FE8 File Offset: 0x000231E8
		public bool? Hidden { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00024FF1 File Offset: 0x000231F1
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x00024FF9 File Offset: 0x000231F9
		public bool? Transient { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00025002 File Offset: 0x00023202
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x0002500A File Offset: 0x0002320A
		public string Description { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00025013 File Offset: 0x00023213
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x0002501B File Offset: 0x0002321B
		public JsonSchemaType? Type { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00025024 File Offset: 0x00023224
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x0002502C File Offset: 0x0002322C
		public string Pattern { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00025035 File Offset: 0x00023235
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x0002503D File Offset: 0x0002323D
		public int? MinimumLength { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00025046 File Offset: 0x00023246
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x0002504E File Offset: 0x0002324E
		public int? MaximumLength { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00025057 File Offset: 0x00023257
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x0002505F File Offset: 0x0002325F
		public double? DivisibleBy { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00025068 File Offset: 0x00023268
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00025070 File Offset: 0x00023270
		public double? Minimum { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00025079 File Offset: 0x00023279
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00025081 File Offset: 0x00023281
		public double? Maximum { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0002508A File Offset: 0x0002328A
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x00025092 File Offset: 0x00023292
		public bool? ExclusiveMinimum { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0002509B File Offset: 0x0002329B
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x000250A3 File Offset: 0x000232A3
		public bool? ExclusiveMaximum { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x000250AC File Offset: 0x000232AC
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x000250B4 File Offset: 0x000232B4
		public int? MinimumItems { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x000250BD File Offset: 0x000232BD
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x000250C5 File Offset: 0x000232C5
		public int? MaximumItems { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x000250CE File Offset: 0x000232CE
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x000250D6 File Offset: 0x000232D6
		public IList<JsonSchema> Items { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x000250DF File Offset: 0x000232DF
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x000250E7 File Offset: 0x000232E7
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x000250F0 File Offset: 0x000232F0
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x000250F8 File Offset: 0x000232F8
		public JsonSchema AdditionalItems { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00025101 File Offset: 0x00023301
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x00025109 File Offset: 0x00023309
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00025112 File Offset: 0x00023312
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x0002511A File Offset: 0x0002331A
		public bool UniqueItems { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00025123 File Offset: 0x00023323
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x0002512B File Offset: 0x0002332B
		public IDictionary<string, JsonSchema> Properties { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00025134 File Offset: 0x00023334
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x0002513C File Offset: 0x0002333C
		public JsonSchema AdditionalProperties { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00025145 File Offset: 0x00023345
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0002514D File Offset: 0x0002334D
		public IDictionary<string, JsonSchema> PatternProperties { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00025156 File Offset: 0x00023356
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x0002515E File Offset: 0x0002335E
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00025167 File Offset: 0x00023367
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x0002516F File Offset: 0x0002336F
		public string Requires { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00025178 File Offset: 0x00023378
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x00025180 File Offset: 0x00023380
		public IList<JToken> Enum { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00025189 File Offset: 0x00023389
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x00025191 File Offset: 0x00023391
		public JsonSchemaType? Disallow { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0002519A File Offset: 0x0002339A
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x000251A2 File Offset: 0x000233A2
		public JToken Default { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x000251AB File Offset: 0x000233AB
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x000251B3 File Offset: 0x000233B3
		public IList<JsonSchema> Extends { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x000251BC File Offset: 0x000233BC
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x000251C4 File Offset: 0x000233C4
		public string Format { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x000251CD File Offset: 0x000233CD
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x000251D5 File Offset: 0x000233D5
		internal string Location { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x000251DE File Offset: 0x000233DE
		internal string InternalId
		{
			get
			{
				return this._internalId;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x000251E6 File Offset: 0x000233E6
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x000251EE File Offset: 0x000233EE
		internal string DeferredReference { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x000251F7 File Offset: 0x000233F7
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x000251FF File Offset: 0x000233FF
		internal bool ReferencesResolved { get; set; }

		// Token: 0x060008E6 RID: 2278 RVA: 0x00025208 File Offset: 0x00023408
		public JsonSchema()
		{
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00025241 File Offset: 0x00023441
		public static JsonSchema Read(JsonReader reader)
		{
			return JsonSchema.Read(reader, new JsonSchemaResolver());
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002524E File Offset: 0x0002344E
		public static JsonSchema Read(JsonReader reader, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			return new JsonSchemaBuilder(resolver).Read(reader);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00025272 File Offset: 0x00023472
		public static JsonSchema Parse(string json)
		{
			return JsonSchema.Parse(json, new JsonSchemaResolver());
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00025280 File Offset: 0x00023480
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

		// Token: 0x060008EB RID: 2283 RVA: 0x000252CC File Offset: 0x000234CC
		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, new JsonSchemaResolver());
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000252DA File Offset: 0x000234DA
		public void WriteTo(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			new JsonSchemaWriter(writer, resolver).WriteSchema(this);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00025300 File Offset: 0x00023500
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteTo(new JsonTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			return stringWriter.ToString();
		}

		// Token: 0x0400030F RID: 783
		private readonly string _internalId = Guid.NewGuid().ToString("N");
	}
}
