using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Identity.Json.Linq;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Schema
{
	// Token: 0x020000A8 RID: 168
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaBuilder
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x00024CCD File Offset: 0x00022ECD
		public JsonSchemaBuilder(JsonSchemaResolver resolver)
		{
			this._stack = new List<JsonSchema>();
			this._documentSchemas = new Dictionary<string, JsonSchema>();
			this._resolver = resolver;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00024CF2 File Offset: 0x00022EF2
		private void Push(JsonSchema value)
		{
			this._currentSchema = value;
			this._stack.Add(value);
			this._resolver.LoadedSchemas.Add(value);
			this._documentSchemas.Add(value.Location, value);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00024D2A File Offset: 0x00022F2A
		private JsonSchema Pop()
		{
			JsonSchema currentSchema = this._currentSchema;
			this._stack.RemoveAt(this._stack.Count - 1);
			this._currentSchema = this._stack.LastOrDefault<JsonSchema>();
			return currentSchema;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00024D5B File Offset: 0x00022F5B
		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00024D64 File Offset: 0x00022F64
		internal JsonSchema Read(JsonReader reader)
		{
			JToken jtoken = JToken.ReadFrom(reader);
			this._rootSchema = jtoken as JObject;
			JsonSchema jsonSchema = this.BuildSchema(jtoken);
			this.ResolveReferences(jsonSchema);
			return jsonSchema;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00024D95 File Offset: 0x00022F95
		private string UnescapeReference(string reference)
		{
			return Uri.UnescapeDataString(reference).Replace("~1", "/").Replace("~0", "~");
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00024DBC File Offset: 0x00022FBC
		private JsonSchema ResolveReferences(JsonSchema schema)
		{
			if (schema.DeferredReference != null)
			{
				string text = schema.DeferredReference;
				bool flag = text.StartsWith("#", StringComparison.Ordinal);
				if (flag)
				{
					text = this.UnescapeReference(text);
				}
				JsonSchema jsonSchema = this._resolver.GetSchema(text);
				if (jsonSchema == null)
				{
					if (flag)
					{
						string[] array = schema.DeferredReference.TrimStart(new char[] { '#' }).Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
						JToken jtoken = this._rootSchema;
						foreach (string text2 in array)
						{
							string text3 = this.UnescapeReference(text2);
							if (jtoken.Type == JTokenType.Object)
							{
								jtoken = jtoken[text3];
							}
							else if (jtoken.Type == JTokenType.Array || jtoken.Type == JTokenType.Constructor)
							{
								int num;
								if (int.TryParse(text3, out num) && num >= 0 && num < jtoken.Count<JToken>())
								{
									jtoken = jtoken[num];
								}
								else
								{
									jtoken = null;
								}
							}
							if (jtoken == null)
							{
								break;
							}
						}
						if (jtoken != null)
						{
							jsonSchema = this.BuildSchema(jtoken);
						}
					}
					if (jsonSchema == null)
					{
						throw new JsonException("Could not resolve schema reference '{0}'.".FormatWith(CultureInfo.InvariantCulture, schema.DeferredReference));
					}
				}
				schema = jsonSchema;
			}
			if (schema.ReferencesResolved)
			{
				return schema;
			}
			schema.ReferencesResolved = true;
			if (schema.Extends != null)
			{
				for (int j = 0; j < schema.Extends.Count; j++)
				{
					schema.Extends[j] = this.ResolveReferences(schema.Extends[j]);
				}
			}
			if (schema.Items != null)
			{
				for (int k = 0; k < schema.Items.Count; k++)
				{
					schema.Items[k] = this.ResolveReferences(schema.Items[k]);
				}
			}
			if (schema.AdditionalItems != null)
			{
				schema.AdditionalItems = this.ResolveReferences(schema.AdditionalItems);
			}
			if (schema.PatternProperties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair in schema.PatternProperties.ToList<KeyValuePair<string, JsonSchema>>())
				{
					schema.PatternProperties[keyValuePair.Key] = this.ResolveReferences(keyValuePair.Value);
				}
			}
			if (schema.Properties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair2 in schema.Properties.ToList<KeyValuePair<string, JsonSchema>>())
				{
					schema.Properties[keyValuePair2.Key] = this.ResolveReferences(keyValuePair2.Value);
				}
			}
			if (schema.AdditionalProperties != null)
			{
				schema.AdditionalProperties = this.ResolveReferences(schema.AdditionalProperties);
			}
			return schema;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00025084 File Offset: 0x00023284
		private JsonSchema BuildSchema(JToken token)
		{
			JObject jobject = token as JObject;
			if (jobject == null)
			{
				throw JsonException.Create(token, token.Path, "Expected object while parsing schema object, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			JToken jtoken;
			if (jobject.TryGetValue("$ref", out jtoken))
			{
				return new JsonSchema
				{
					DeferredReference = (string)jtoken
				};
			}
			string text = token.Path.Replace(".", "/").Replace("[", "/").Replace("]", string.Empty);
			if (!StringUtils.IsNullOrEmpty(text))
			{
				text = "/" + text;
			}
			text = "#" + text;
			JsonSchema jsonSchema;
			if (this._documentSchemas.TryGetValue(text, out jsonSchema))
			{
				return jsonSchema;
			}
			this.Push(new JsonSchema
			{
				Location = text
			});
			this.ProcessSchemaProperties(jobject);
			return this.Pop();
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00025168 File Offset: 0x00023368
		private void ProcessSchemaProperties(JObject schemaObject)
		{
			foreach (KeyValuePair<string, JToken> keyValuePair in schemaObject)
			{
				string key = keyValuePair.Key;
				if (key != null)
				{
					switch (key.Length)
					{
					case 2:
						if (key == "id")
						{
							this.CurrentSchema.Id = (string)keyValuePair.Value;
						}
						break;
					case 4:
					{
						char c = key[0];
						if (c != 'e')
						{
							if (c == 't')
							{
								if (key == "type")
								{
									this.CurrentSchema.Type = this.ProcessType(keyValuePair.Value);
								}
							}
						}
						else if (key == "enum")
						{
							this.ProcessEnum(keyValuePair.Value);
						}
						break;
					}
					case 5:
					{
						char c = key[0];
						if (c != 'i')
						{
							if (c == 't')
							{
								if (key == "title")
								{
									this.CurrentSchema.Title = (string)keyValuePair.Value;
								}
							}
						}
						else if (key == "items")
						{
							this.ProcessItems(keyValuePair.Value);
						}
						break;
					}
					case 6:
					{
						char c = key[0];
						if (c != 'f')
						{
							if (c == 'h')
							{
								if (key == "hidden")
								{
									this.CurrentSchema.Hidden = new bool?((bool)keyValuePair.Value);
								}
							}
						}
						else if (key == "format")
						{
							this.CurrentSchema.Format = (string)keyValuePair.Value;
						}
						break;
					}
					case 7:
					{
						char c = key[0];
						if (c <= 'e')
						{
							if (c != 'd')
							{
								if (c == 'e')
								{
									if (key == "extends")
									{
										this.ProcessExtends(keyValuePair.Value);
									}
								}
							}
							else if (key == "default")
							{
								this.CurrentSchema.Default = keyValuePair.Value.DeepClone();
							}
						}
						else if (c != 'm')
						{
							if (c == 'p')
							{
								if (key == "pattern")
								{
									this.CurrentSchema.Pattern = (string)keyValuePair.Value;
								}
							}
						}
						else if (!(key == "minimum"))
						{
							if (key == "maximum")
							{
								this.CurrentSchema.Maximum = new double?((double)keyValuePair.Value);
							}
						}
						else
						{
							this.CurrentSchema.Minimum = new double?((double)keyValuePair.Value);
						}
						break;
					}
					case 8:
					{
						char c = key[2];
						if (c <= 'n')
						{
							if (c != 'a')
							{
								if (c == 'n')
								{
									if (key == "minItems")
									{
										this.CurrentSchema.MinimumItems = new int?((int)keyValuePair.Value);
									}
								}
							}
							else if (key == "readonly")
							{
								this.CurrentSchema.ReadOnly = new bool?((bool)keyValuePair.Value);
							}
						}
						else if (c != 'q')
						{
							if (c != 's')
							{
								if (c == 'x')
								{
									if (key == "maxItems")
									{
										this.CurrentSchema.MaximumItems = new int?((int)keyValuePair.Value);
									}
								}
							}
							else if (key == "disallow")
							{
								this.CurrentSchema.Disallow = this.ProcessType(keyValuePair.Value);
							}
						}
						else if (!(key == "required"))
						{
							if (key == "requires")
							{
								this.CurrentSchema.Requires = (string)keyValuePair.Value;
							}
						}
						else
						{
							this.CurrentSchema.Required = new bool?((bool)keyValuePair.Value);
						}
						break;
					}
					case 9:
					{
						char c = key[1];
						if (c != 'a')
						{
							if (c == 'i')
							{
								if (key == "minLength")
								{
									this.CurrentSchema.MinimumLength = new int?((int)keyValuePair.Value);
								}
							}
						}
						else if (key == "maxLength")
						{
							this.CurrentSchema.MaximumLength = new int?((int)keyValuePair.Value);
						}
						break;
					}
					case 10:
						if (key == "properties")
						{
							this.CurrentSchema.Properties = this.ProcessProperties(keyValuePair.Value);
						}
						break;
					case 11:
					{
						char c = key[1];
						if (c != 'e')
						{
							if (c != 'i')
							{
								if (c == 'n')
								{
									if (key == "uniqueItems")
									{
										this.CurrentSchema.UniqueItems = (bool)keyValuePair.Value;
									}
								}
							}
							else if (key == "divisibleBy")
							{
								this.CurrentSchema.DivisibleBy = new double?((double)keyValuePair.Value);
							}
						}
						else if (key == "description")
						{
							this.CurrentSchema.Description = (string)keyValuePair.Value;
						}
						break;
					}
					case 15:
						if (key == "additionalItems")
						{
							this.ProcessAdditionalItems(keyValuePair.Value);
						}
						break;
					case 16:
					{
						char c = key[10];
						if (c != 'a')
						{
							if (c == 'i')
							{
								if (key == "exclusiveMinimum")
								{
									this.CurrentSchema.ExclusiveMinimum = new bool?((bool)keyValuePair.Value);
								}
							}
						}
						else if (key == "exclusiveMaximum")
						{
							this.CurrentSchema.ExclusiveMaximum = new bool?((bool)keyValuePair.Value);
						}
						break;
					}
					case 17:
						if (key == "patternProperties")
						{
							this.CurrentSchema.PatternProperties = this.ProcessProperties(keyValuePair.Value);
						}
						break;
					case 20:
						if (key == "additionalProperties")
						{
							this.ProcessAdditionalProperties(keyValuePair.Value);
						}
						break;
					}
				}
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x000258F8 File Offset: 0x00023AF8
		private void ProcessExtends(JToken token)
		{
			IList<JsonSchema> list = new List<JsonSchema>();
			if (token.Type == JTokenType.Array)
			{
				using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)token).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JToken jtoken = enumerator.Current;
						list.Add(this.BuildSchema(jtoken));
					}
					goto IL_0052;
				}
			}
			JsonSchema jsonSchema = this.BuildSchema(token);
			if (jsonSchema != null)
			{
				list.Add(jsonSchema);
			}
			IL_0052:
			if (list.Count > 0)
			{
				this.CurrentSchema.Extends = list;
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002597C File Offset: 0x00023B7C
		private void ProcessEnum(JToken token)
		{
			if (token.Type != JTokenType.Array)
			{
				throw JsonException.Create(token, token.Path, "Expected Array token while parsing enum values, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			this.CurrentSchema.Enum = new List<JToken>();
			foreach (JToken jtoken in ((IEnumerable<JToken>)token))
			{
				this.CurrentSchema.Enum.Add(jtoken.DeepClone());
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00025A14 File Offset: 0x00023C14
		private void ProcessAdditionalProperties(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalProperties = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalProperties = this.BuildSchema(token);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00025A44 File Offset: 0x00023C44
		private void ProcessAdditionalItems(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalItems = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalItems = this.BuildSchema(token);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00025A74 File Offset: 0x00023C74
		private IDictionary<string, JsonSchema> ProcessProperties(JToken token)
		{
			IDictionary<string, JsonSchema> dictionary = new Dictionary<string, JsonSchema>();
			if (token.Type != JTokenType.Object)
			{
				throw JsonException.Create(token, token.Path, "Expected Object token while parsing schema properties, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			foreach (JToken jtoken in ((IEnumerable<JToken>)token))
			{
				JProperty jproperty = (JProperty)jtoken;
				if (dictionary.ContainsKey(jproperty.Name))
				{
					throw new JsonException("Property {0} has already been defined in schema.".FormatWith(CultureInfo.InvariantCulture, jproperty.Name));
				}
				dictionary.Add(jproperty.Name, this.BuildSchema(jproperty.Value));
			}
			return dictionary;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00025B34 File Offset: 0x00023D34
		private void ProcessItems(JToken token)
		{
			this.CurrentSchema.Items = new List<JsonSchema>();
			JTokenType type = token.Type;
			if (type != JTokenType.Object)
			{
				if (type == JTokenType.Array)
				{
					this.CurrentSchema.PositionalItemsValidation = true;
					using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)token).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							JToken jtoken = enumerator.Current;
							this.CurrentSchema.Items.Add(this.BuildSchema(jtoken));
						}
						return;
					}
				}
				throw JsonException.Create(token, token.Path, "Expected array or JSON schema object, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			this.CurrentSchema.Items.Add(this.BuildSchema(token));
			this.CurrentSchema.PositionalItemsValidation = false;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00025C04 File Offset: 0x00023E04
		private JsonSchemaType? ProcessType(JToken token)
		{
			JTokenType type = token.Type;
			if (type == JTokenType.Array)
			{
				JsonSchemaType? jsonSchemaType = new JsonSchemaType?(JsonSchemaType.None);
				foreach (JToken jtoken in ((IEnumerable<JToken>)token))
				{
					if (jtoken.Type != JTokenType.String)
					{
						throw JsonException.Create(jtoken, jtoken.Path, "Expected JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
					}
					jsonSchemaType |= JsonSchemaBuilder.MapType((string)jtoken);
				}
				return jsonSchemaType;
			}
			if (type != JTokenType.String)
			{
				throw JsonException.Create(token, token.Path, "Expected array or JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			return new JsonSchemaType?(JsonSchemaBuilder.MapType((string)token));
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00025D04 File Offset: 0x00023F04
		internal static JsonSchemaType MapType(string type)
		{
			JsonSchemaType jsonSchemaType;
			if (!JsonSchemaConstants.JsonSchemaTypeMapping.TryGetValue(type, out jsonSchemaType))
			{
				throw new JsonException("Invalid JSON schema type: {0}".FormatWith(CultureInfo.InvariantCulture, type));
			}
			return jsonSchemaType;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00025D38 File Offset: 0x00023F38
		internal static string MapType(JsonSchemaType type)
		{
			return JsonSchemaConstants.JsonSchemaTypeMapping.Single((KeyValuePair<string, JsonSchemaType> kv) => kv.Value == type).Key;
		}

		// Token: 0x040002F7 RID: 759
		private readonly IList<JsonSchema> _stack;

		// Token: 0x040002F8 RID: 760
		private readonly JsonSchemaResolver _resolver;

		// Token: 0x040002F9 RID: 761
		private readonly IDictionary<string, JsonSchema> _documentSchemas;

		// Token: 0x040002FA RID: 762
		private JsonSchema _currentSchema;

		// Token: 0x040002FB RID: 763
		private JObject _rootSchema;
	}
}
