using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000A7 RID: 167
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchema
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00024F78 File Offset: 0x00023178
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x00024F80 File Offset: 0x00023180
		public string Id { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00024F89 File Offset: 0x00023189
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x00024F91 File Offset: 0x00023191
		public string Title { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00024F9A File Offset: 0x0002319A
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x00024FA2 File Offset: 0x000231A2
		public bool? Required { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00024FAB File Offset: 0x000231AB
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x00024FB3 File Offset: 0x000231B3
		public bool? ReadOnly { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00024FBC File Offset: 0x000231BC
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x00024FC4 File Offset: 0x000231C4
		public bool? Hidden { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00024FCD File Offset: 0x000231CD
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x00024FD5 File Offset: 0x000231D5
		public bool? Transient { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00024FDE File Offset: 0x000231DE
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00024FE6 File Offset: 0x000231E6
		public string Description { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00024FEF File Offset: 0x000231EF
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x00024FF7 File Offset: 0x000231F7
		public JsonSchemaType? Type { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00025000 File Offset: 0x00023200
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x00025008 File Offset: 0x00023208
		public string Pattern { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00025011 File Offset: 0x00023211
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x00025019 File Offset: 0x00023219
		public int? MinimumLength { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00025022 File Offset: 0x00023222
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0002502A File Offset: 0x0002322A
		public int? MaximumLength { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00025033 File Offset: 0x00023233
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0002503B File Offset: 0x0002323B
		public double? DivisibleBy { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00025044 File Offset: 0x00023244
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x0002504C File Offset: 0x0002324C
		public double? Minimum { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00025055 File Offset: 0x00023255
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x0002505D File Offset: 0x0002325D
		public double? Maximum { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00025066 File Offset: 0x00023266
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x0002506E File Offset: 0x0002326E
		public bool? ExclusiveMinimum { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00025077 File Offset: 0x00023277
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x0002507F File Offset: 0x0002327F
		public bool? ExclusiveMaximum { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00025088 File Offset: 0x00023288
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00025090 File Offset: 0x00023290
		public int? MinimumItems { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00025099 File Offset: 0x00023299
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x000250A1 File Offset: 0x000232A1
		public int? MaximumItems { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x000250AA File Offset: 0x000232AA
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x000250B2 File Offset: 0x000232B2
		public IList<JsonSchema> Items { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x000250BB File Offset: 0x000232BB
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x000250C3 File Offset: 0x000232C3
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x000250CC File Offset: 0x000232CC
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x000250D4 File Offset: 0x000232D4
		public JsonSchema AdditionalItems { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x000250DD File Offset: 0x000232DD
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x000250E5 File Offset: 0x000232E5
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x000250EE File Offset: 0x000232EE
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x000250F6 File Offset: 0x000232F6
		public bool UniqueItems { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x000250FF File Offset: 0x000232FF
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x00025107 File Offset: 0x00023307
		public IDictionary<string, JsonSchema> Properties { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00025110 File Offset: 0x00023310
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x00025118 File Offset: 0x00023318
		public JsonSchema AdditionalProperties { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00025121 File Offset: 0x00023321
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x00025129 File Offset: 0x00023329
		public IDictionary<string, JsonSchema> PatternProperties { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00025132 File Offset: 0x00023332
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x0002513A File Offset: 0x0002333A
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00025143 File Offset: 0x00023343
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x0002514B File Offset: 0x0002334B
		public string Requires { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00025154 File Offset: 0x00023354
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x0002515C File Offset: 0x0002335C
		public IList<JToken> Enum { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00025165 File Offset: 0x00023365
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x0002516D File Offset: 0x0002336D
		public JsonSchemaType? Disallow { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00025176 File Offset: 0x00023376
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x0002517E File Offset: 0x0002337E
		public JToken Default { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00025187 File Offset: 0x00023387
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0002518F File Offset: 0x0002338F
		public IList<JsonSchema> Extends { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00025198 File Offset: 0x00023398
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x000251A0 File Offset: 0x000233A0
		public string Format { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x000251A9 File Offset: 0x000233A9
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x000251B1 File Offset: 0x000233B1
		internal string Location { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x000251BA File Offset: 0x000233BA
		internal string InternalId
		{
			get
			{
				return this._internalId;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x000251C2 File Offset: 0x000233C2
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x000251CA File Offset: 0x000233CA
		internal string DeferredReference { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x000251D3 File Offset: 0x000233D3
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x000251DB File Offset: 0x000233DB
		internal bool ReferencesResolved { get; set; }

		// Token: 0x060008E5 RID: 2277 RVA: 0x000251E4 File Offset: 0x000233E4
		public JsonSchema()
		{
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0002521D File Offset: 0x0002341D
		public static JsonSchema Read(JsonReader reader)
		{
			return JsonSchema.Read(reader, new JsonSchemaResolver());
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002522A File Offset: 0x0002342A
		public static JsonSchema Read(JsonReader reader, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			return new JsonSchemaBuilder(resolver).Read(reader);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002524E File Offset: 0x0002344E
		public static JsonSchema Parse(string json)
		{
			return JsonSchema.Parse(json, new JsonSchemaResolver());
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0002525C File Offset: 0x0002345C
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

		// Token: 0x060008EA RID: 2282 RVA: 0x000252A8 File Offset: 0x000234A8
		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, new JsonSchemaResolver());
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000252B6 File Offset: 0x000234B6
		public void WriteTo(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			new JsonSchemaWriter(writer, resolver).WriteSchema(this);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000252DC File Offset: 0x000234DC
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteTo(new JsonTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			return stringWriter.ToString();
		}

		// Token: 0x0400030E RID: 782
		private readonly string _internalId = Guid.NewGuid().ToString("N");
	}
}
