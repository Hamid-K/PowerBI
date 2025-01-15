using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Converters;
using System.Threading;

namespace System.Text.Json.Nodes
{
	// Token: 0x02000060 RID: 96
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("JsonObject[{Count}]")]
	[DebuggerTypeProxy(typeof(JsonObject.DebugView))]
	public sealed class JsonObject : JsonNode, IDictionary<string, JsonNode>, ICollection<KeyValuePair<string, JsonNode>>, IEnumerable<KeyValuePair<string, JsonNode>>, IEnumerable
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x000222E5 File Offset: 0x000204E5
		public JsonObject(JsonNodeOptions? options = null)
			: base(options)
		{
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000222F0 File Offset: 0x000204F0
		public JsonObject([Nullable(new byte[] { 1, 0, 1, 2 })] IEnumerable<KeyValuePair<string, JsonNode>> properties, JsonNodeOptions? options = null)
			: this(options)
		{
			foreach (KeyValuePair<string, JsonNode> keyValuePair in properties)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0002234C File Offset: 0x0002054C
		[NullableContext(2)]
		public static JsonObject Create(JsonElement element, JsonNodeOptions? options = null)
		{
			JsonValueKind valueKind = element.ValueKind;
			JsonObject jsonObject;
			if (valueKind != JsonValueKind.Object)
			{
				if (valueKind != JsonValueKind.Null)
				{
					throw new InvalidOperationException(SR.Format(SR.NodeElementWrongType, "Object"));
				}
				jsonObject = null;
			}
			else
			{
				jsonObject = new JsonObject(element, options);
			}
			return jsonObject;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0002238D File Offset: 0x0002058D
		internal JsonObject(JsonElement element, JsonNodeOptions? options = null)
			: this(options)
		{
			this._jsonElement = new JsonElement?(element);
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x000223A4 File Offset: 0x000205A4
		[Nullable(new byte[] { 1, 2 })]
		internal JsonPropertyDictionary<JsonNode> Dictionary
		{
			get
			{
				JsonPropertyDictionary<JsonNode> dictionary = this._dictionary;
				if (dictionary == null)
				{
					return this.InitializeDictionary();
				}
				return dictionary;
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000223C4 File Offset: 0x000205C4
		internal override JsonNode DeepCloneCore()
		{
			JsonPropertyDictionary<JsonNode> jsonPropertyDictionary;
			JsonElement? jsonElement;
			this.GetUnderlyingRepresentation(out jsonPropertyDictionary, out jsonElement);
			if (jsonPropertyDictionary != null)
			{
				bool flag = base.Options != null && base.Options.Value.PropertyNameCaseInsensitive;
				JsonObject jsonObject = new JsonObject(base.Options)
				{
					_dictionary = new JsonPropertyDictionary<JsonNode>(flag, jsonPropertyDictionary.Count)
				};
				foreach (KeyValuePair<string, JsonNode> keyValuePair in jsonPropertyDictionary)
				{
					JsonObject jsonObject2 = jsonObject;
					string key = keyValuePair.Key;
					JsonNode value = keyValuePair.Value;
					jsonObject2.Add(key, (value != null) ? value.DeepCloneCore() : null);
				}
				return jsonObject;
			}
			if (jsonElement == null)
			{
				return new JsonObject(base.Options);
			}
			return new JsonObject(jsonElement.Value.Clone(), base.Options);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000224B8 File Offset: 0x000206B8
		internal string GetPropertyName(JsonNode node)
		{
			KeyValuePair<string, JsonNode>? keyValuePair = this.Dictionary.FindValue(node);
			if (keyValuePair == null)
			{
				return string.Empty;
			}
			return keyValuePair.Value.Key;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000224F0 File Offset: 0x000206F0
		public bool TryGetPropertyValue(string propertyName, [Nullable(2)] out JsonNode jsonNode)
		{
			return ((IDictionary<string, JsonNode>)this).TryGetValue(propertyName, out jsonNode);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000224FC File Offset: 0x000206FC
		public override void WriteTo(Utf8JsonWriter writer, [Nullable(2)] JsonSerializerOptions options = null)
		{
			if (writer == null)
			{
				ThrowHelper.ThrowArgumentNullException("writer");
			}
			JsonPropertyDictionary<JsonNode> jsonPropertyDictionary;
			JsonElement? jsonElement;
			this.GetUnderlyingRepresentation(out jsonPropertyDictionary, out jsonElement);
			if (jsonPropertyDictionary == null && jsonElement != null)
			{
				jsonElement.Value.WriteTo(writer);
				return;
			}
			writer.WriteStartObject();
			foreach (KeyValuePair<string, JsonNode> keyValuePair in this.Dictionary)
			{
				writer.WritePropertyName(keyValuePair.Key);
				if (keyValuePair.Value == null)
				{
					writer.WriteNullValue();
				}
				else
				{
					keyValuePair.Value.WriteTo(writer, options);
				}
			}
			writer.WriteEndObject();
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000225B8 File Offset: 0x000207B8
		internal override JsonValueKind GetValueKindCore()
		{
			return JsonValueKind.Object;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000225BC File Offset: 0x000207BC
		internal override bool DeepEqualsCore(JsonNode node)
		{
			if (node == null || node is JsonArray)
			{
				return false;
			}
			JsonValue jsonValue = node as JsonValue;
			if (jsonValue != null)
			{
				return jsonValue.DeepEqualsCore(this);
			}
			JsonObject jsonObject = node as JsonObject;
			if (jsonObject == null)
			{
				return false;
			}
			JsonPropertyDictionary<JsonNode> dictionary = this.Dictionary;
			JsonPropertyDictionary<JsonNode> dictionary2 = jsonObject.Dictionary;
			if (dictionary.Count != dictionary2.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, JsonNode> keyValuePair in dictionary)
			{
				JsonNode jsonNode = dictionary2[keyValuePair.Key];
				if (!JsonNode.DeepEquals(keyValuePair.Value, jsonNode))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00022678 File Offset: 0x00020878
		internal JsonNode GetItem(string propertyName)
		{
			JsonNode jsonNode;
			if (this.TryGetPropertyValue(propertyName, out jsonNode))
			{
				return jsonNode;
			}
			return null;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00022694 File Offset: 0x00020894
		internal override void GetPath(List<string> path, JsonNode child)
		{
			if (child != null)
			{
				string key = this.Dictionary.FindValue(child).Value.Key;
				if (key.AsSpan().ContainsSpecialCharacters())
				{
					path.Add("['" + key + "']");
				}
				else
				{
					path.Add("." + key);
				}
			}
			JsonNode parent = base.Parent;
			if (parent == null)
			{
				return;
			}
			parent.GetPath(path, this);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0002270C File Offset: 0x0002090C
		internal void SetItem(string propertyName, JsonNode value)
		{
			bool flag;
			JsonNode jsonNode = this.Dictionary.SetValue(propertyName, value, out flag);
			if (!flag && value != null)
			{
				value.AssignParent(this);
			}
			this.DetachParent(jsonNode);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0002273D File Offset: 0x0002093D
		private void DetachParent(JsonNode item)
		{
			if (item != null)
			{
				item.Parent = null;
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00022749 File Offset: 0x00020949
		public void Add(string propertyName, [Nullable(2)] JsonNode value)
		{
			this.Dictionary.Add(propertyName, value);
			if (value != null)
			{
				value.AssignParent(this);
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00022762 File Offset: 0x00020962
		public void Add([Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, JsonNode> property)
		{
			this.Add(property.Key, property.Value);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00022778 File Offset: 0x00020978
		public void Clear()
		{
			JsonPropertyDictionary<JsonNode> dictionary = this._dictionary;
			if (dictionary == null)
			{
				this._jsonElement = null;
				return;
			}
			foreach (JsonNode jsonNode in dictionary.GetValueCollection())
			{
				this.DetachParent(jsonNode);
			}
			dictionary.Clear();
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000227E4 File Offset: 0x000209E4
		public bool ContainsKey(string propertyName)
		{
			return this.Dictionary.ContainsKey(propertyName);
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x000227F2 File Offset: 0x000209F2
		public int Count
		{
			get
			{
				return this.Dictionary.Count;
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00022800 File Offset: 0x00020A00
		public bool Remove(string propertyName)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			JsonNode jsonNode;
			bool flag = this.Dictionary.TryRemoveProperty(propertyName, out jsonNode);
			if (flag)
			{
				this.DetachParent(jsonNode);
			}
			return flag;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00022834 File Offset: 0x00020A34
		bool ICollection<KeyValuePair<string, JsonNode>>.Contains(KeyValuePair<string, JsonNode> item)
		{
			return this.Dictionary.Contains(item);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00022842 File Offset: 0x00020A42
		void ICollection<KeyValuePair<string, JsonNode>>.CopyTo(KeyValuePair<string, JsonNode>[] array, int index)
		{
			this.Dictionary.CopyTo(array, index);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00022851 File Offset: 0x00020A51
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		public IEnumerator<KeyValuePair<string, JsonNode>> GetEnumerator()
		{
			return this.Dictionary.GetEnumerator();
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00022863 File Offset: 0x00020A63
		bool ICollection<KeyValuePair<string, JsonNode>>.Remove(KeyValuePair<string, JsonNode> item)
		{
			return this.Remove(item.Key);
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00022872 File Offset: 0x00020A72
		ICollection<string> IDictionary<string, JsonNode>.Keys
		{
			get
			{
				return this.Dictionary.Keys;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0002287F File Offset: 0x00020A7F
		[Nullable(new byte[] { 1, 2 })]
		ICollection<JsonNode> IDictionary<string, JsonNode>.Values
		{
			get
			{
				return this.Dictionary.Values;
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0002288C File Offset: 0x00020A8C
		bool IDictionary<string, JsonNode>.TryGetValue(string propertyName, out JsonNode jsonNode)
		{
			return this.Dictionary.TryGetValue(propertyName, out jsonNode);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0002289B File Offset: 0x00020A9B
		bool ICollection<KeyValuePair<string, JsonNode>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0002289E File Offset: 0x00020A9E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Dictionary.GetEnumerator();
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000228B0 File Offset: 0x00020AB0
		private JsonPropertyDictionary<JsonNode> InitializeDictionary()
		{
			JsonPropertyDictionary<JsonNode> jsonPropertyDictionary;
			JsonElement? jsonElement;
			this.GetUnderlyingRepresentation(out jsonPropertyDictionary, out jsonElement);
			if (jsonPropertyDictionary == null)
			{
				bool flag = base.Options != null && base.Options.Value.PropertyNameCaseInsensitive;
				jsonPropertyDictionary = new JsonPropertyDictionary<JsonNode>(flag);
				if (jsonElement != null)
				{
					foreach (JsonProperty jsonProperty in jsonElement.Value.EnumerateObject())
					{
						JsonNode jsonNode = JsonNodeConverter.Create(jsonProperty.Value, base.Options);
						if (jsonNode != null)
						{
							jsonNode.Parent = this;
						}
						jsonPropertyDictionary.Add(new KeyValuePair<string, JsonNode>(jsonProperty.Name, jsonNode));
					}
				}
				this._dictionary = jsonPropertyDictionary;
				Interlocked.MemoryBarrier();
				this._jsonElement = null;
			}
			return jsonPropertyDictionary;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000229A4 File Offset: 0x00020BA4
		private void GetUnderlyingRepresentation(out JsonPropertyDictionary<JsonNode> dictionary, out JsonElement? jsonElement)
		{
			jsonElement = this._jsonElement;
			Interlocked.MemoryBarrier();
			dictionary = this._dictionary;
		}

		// Token: 0x04000285 RID: 645
		private JsonElement? _jsonElement;

		// Token: 0x04000286 RID: 646
		private JsonPropertyDictionary<JsonNode> _dictionary;

		// Token: 0x0200012F RID: 303
		[ExcludeFromCodeCoverage]
		private sealed class DebugView
		{
			// Token: 0x06000DCC RID: 3532 RVA: 0x00035BE2 File Offset: 0x00033DE2
			public DebugView(JsonObject node)
			{
				this._node = node;
			}

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00035BF1 File Offset: 0x00033DF1
			public string Json
			{
				get
				{
					return this._node.ToJsonString(null);
				}
			}

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00035BFF File Offset: 0x00033DFF
			public string Path
			{
				get
				{
					return this._node.GetPath();
				}
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00035C0C File Offset: 0x00033E0C
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			private JsonObject.DebugView.DebugViewProperty[] Items
			{
				get
				{
					JsonObject.DebugView.DebugViewProperty[] array = new JsonObject.DebugView.DebugViewProperty[this._node.Count];
					int num = 0;
					foreach (KeyValuePair<string, JsonNode> keyValuePair in this._node)
					{
						array[num].PropertyName = keyValuePair.Key;
						array[num].Value = keyValuePair.Value;
						num++;
					}
					return array;
				}
			}

			// Token: 0x040004BE RID: 1214
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private readonly JsonObject _node;

			// Token: 0x02000177 RID: 375
			[DebuggerDisplay("{Display,nq}")]
			private struct DebugViewProperty
			{
				// Token: 0x17000307 RID: 775
				// (get) Token: 0x06000E85 RID: 3717 RVA: 0x000378E4 File Offset: 0x00035AE4
				[DebuggerBrowsable(DebuggerBrowsableState.Never)]
				public string Display
				{
					get
					{
						if (this.Value == null)
						{
							return this.PropertyName + " = null";
						}
						if (this.Value is JsonValue)
						{
							return this.PropertyName + " = " + this.Value.ToJsonString(null);
						}
						JsonObject jsonObject = this.Value as JsonObject;
						if (jsonObject != null)
						{
							return string.Format("{0} = JsonObject[{1}]", this.PropertyName, jsonObject.Count);
						}
						JsonArray jsonArray = (JsonArray)this.Value;
						return string.Format("{0} = JsonArray[{1}]", this.PropertyName, jsonArray.Count);
					}
				}

				// Token: 0x0400056E RID: 1390
				[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
				public JsonNode Value;

				// Token: 0x0400056F RID: 1391
				[DebuggerBrowsable(DebuggerBrowsableState.Never)]
				public string PropertyName;
			}
		}
	}
}
