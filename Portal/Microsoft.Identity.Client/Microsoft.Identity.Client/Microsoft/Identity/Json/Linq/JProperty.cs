using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Linq
{
	// Token: 0x020000BF RID: 191
	internal class JProperty : JContainer
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x0002A130 File Offset: 0x00028330
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			Task task = writer.WritePropertyNameAsync(this._name, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return this.WriteValueAsync(writer, cancellationToken, converters);
			}
			return this.WriteToAsync(task, writer, cancellationToken, converters);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002A168 File Offset: 0x00028368
		private async Task WriteToAsync(Task task, JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			await task.ConfigureAwait(false);
			await this.WriteValueAsync(writer, cancellationToken, converters).ConfigureAwait(false);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002A1CC File Offset: 0x000283CC
		private Task WriteValueAsync(JsonWriter writer, CancellationToken cancellationToken, JsonConverter[] converters)
		{
			JToken value = this.Value;
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			return value.WriteToAsync(writer, cancellationToken, converters);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002A1F4 File Offset: 0x000283F4
		public new static Task<JProperty> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JProperty.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002A200 File Offset: 0x00028400
		public new static async Task<JProperty> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (reader.TokenType == JsonToken.None)
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty p = new JProperty((string)reader.Value);
			p.SetLineInfo(reader as IJsonLineInfo, settings);
			await p.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return p;
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0002A253 File Offset: 0x00028453
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0002A25B File Offset: 0x0002845B
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this._name;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0002A263 File Offset: 0x00028463
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x0002A270 File Offset: 0x00028470
		public new JToken Value
		{
			[DebuggerStepThrough]
			get
			{
				return this._content._token;
			}
			set
			{
				base.CheckReentrancy();
				JToken jtoken = value ?? JValue.CreateNull();
				if (this._content._token == null)
				{
					this.InsertItem(0, jtoken, false);
					return;
				}
				this.SetItem(0, jtoken);
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002A2AE File Offset: 0x000284AE
		public JProperty(JProperty other)
			: base(other)
		{
			this._name = other.Name;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002A2CE File Offset: 0x000284CE
		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.Value;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002A2E0 File Offset: 0x000284E0
		[NullableContext(2)]
		internal override void SetItem(int index, JToken item)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (JContainer.IsTokenUnchanged(this.Value, item))
			{
				return;
			}
			JObject jobject = (JObject)base.Parent;
			if (jobject != null)
			{
				jobject.InternalPropertyChanging(this);
			}
			base.SetItem(0, item);
			JObject jobject2 = (JObject)base.Parent;
			if (jobject2 == null)
			{
				return;
			}
			jobject2.InternalPropertyChanged(this);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002A33A File Offset: 0x0002853A
		[NullableContext(2)]
		internal override bool RemoveItem(JToken item)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002A35A File Offset: 0x0002855A
		internal override void RemoveItemAt(int index)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002A37A File Offset: 0x0002857A
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._content.IndexOf(item);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002A38D File Offset: 0x0002858D
		[NullableContext(2)]
		internal override bool InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return false;
			}
			if (this.Value != null)
			{
				throw new JsonException("{0} cannot have multiple values.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
			}
			return base.InsertItem(0, item, false);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002A3CD File Offset: 0x000285CD
		[NullableContext(2)]
		internal override bool ContainsItem(JToken item)
		{
			return this.Value == item;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002A3D8 File Offset: 0x000285D8
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JProperty jproperty = content as JProperty;
			JToken jtoken = ((jproperty != null) ? jproperty.Value : null);
			if (jtoken != null && jtoken.Type != JTokenType.Null)
			{
				this.Value = jtoken;
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002A40C File Offset: 0x0002860C
		internal override void ClearItems()
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002A42C File Offset: 0x0002862C
		internal override bool DeepEquals(JToken node)
		{
			JProperty jproperty = node as JProperty;
			return jproperty != null && this._name == jproperty.Name && base.ContentsEqual(jproperty);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002A45F File Offset: 0x0002865F
		internal override JToken CloneToken()
		{
			return new JProperty(this);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0002A467 File Offset: 0x00028667
		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002A46A File Offset: 0x0002866A
		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002A48F File Offset: 0x0002868F
		public JProperty(string name, params object[] content)
			: this(name, content)
		{
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002A49C File Offset: 0x0002869C
		public JProperty(string name, [Nullable(2)] object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
			this.Value = (base.IsMultiContent(content) ? new JArray(content) : JContainer.CreateFromContent(content));
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002A4EC File Offset: 0x000286EC
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WritePropertyName(this._name);
			JToken value = this.Value;
			if (value != null)
			{
				value.WriteTo(writer, converters);
				return;
			}
			writer.WriteNull();
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002A51E File Offset: 0x0002871E
		internal override int GetDeepHashCode()
		{
			int hashCode = this._name.GetHashCode();
			JToken value = this.Value;
			return hashCode ^ ((value != null) ? value.GetDeepHashCode() : 0);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002A53E File Offset: 0x0002873E
		public new static JProperty Load(JsonReader reader)
		{
			return JProperty.Load(reader, null);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002A548 File Offset: 0x00028748
		public new static JProperty Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty jproperty = new JProperty((string)reader.Value);
			jproperty.SetLineInfo(reader as IJsonLineInfo, settings);
			jproperty.ReadTokenFrom(reader, settings);
			return jproperty;
		}

		// Token: 0x0400036C RID: 876
		private readonly JProperty.JPropertyList _content = new JProperty.JPropertyList();

		// Token: 0x0400036D RID: 877
		private readonly string _name;

		// Token: 0x020003A0 RID: 928
		private class JPropertyList : IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
		{
			// Token: 0x06001D31 RID: 7473 RVA: 0x0006580A File Offset: 0x00063A0A
			public IEnumerator<JToken> GetEnumerator()
			{
				if (this._token != null)
				{
					yield return this._token;
				}
				yield break;
			}

			// Token: 0x06001D32 RID: 7474 RVA: 0x00065819 File Offset: 0x00063A19
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06001D33 RID: 7475 RVA: 0x00065821 File Offset: 0x00063A21
			public void Add(JToken item)
			{
				this._token = item;
			}

			// Token: 0x06001D34 RID: 7476 RVA: 0x0006582A File Offset: 0x00063A2A
			public void Clear()
			{
				this._token = null;
			}

			// Token: 0x06001D35 RID: 7477 RVA: 0x00065833 File Offset: 0x00063A33
			public bool Contains(JToken item)
			{
				return this._token == item;
			}

			// Token: 0x06001D36 RID: 7478 RVA: 0x0006583E File Offset: 0x00063A3E
			public void CopyTo(JToken[] array, int arrayIndex)
			{
				if (this._token != null)
				{
					array[arrayIndex] = this._token;
				}
			}

			// Token: 0x06001D37 RID: 7479 RVA: 0x00065851 File Offset: 0x00063A51
			public bool Remove(JToken item)
			{
				if (this._token == item)
				{
					this._token = null;
					return true;
				}
				return false;
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001D38 RID: 7480 RVA: 0x00065866 File Offset: 0x00063A66
			public int Count
			{
				get
				{
					return (this._token != null) ? 1 : 0;
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x06001D39 RID: 7481 RVA: 0x00065871 File Offset: 0x00063A71
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001D3A RID: 7482 RVA: 0x00065874 File Offset: 0x00063A74
			public int IndexOf(JToken item)
			{
				if (this._token != item)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x06001D3B RID: 7483 RVA: 0x00065882 File Offset: 0x00063A82
			public void Insert(int index, JToken item)
			{
				if (index == 0)
				{
					this._token = item;
				}
			}

			// Token: 0x06001D3C RID: 7484 RVA: 0x0006588E File Offset: 0x00063A8E
			public void RemoveAt(int index)
			{
				if (index == 0)
				{
					this._token = null;
				}
			}

			// Token: 0x170005FB RID: 1531
			public JToken this[int index]
			{
				get
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					return this._token;
				}
				set
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					this._token = value;
				}
			}

			// Token: 0x04000FDC RID: 4060
			[Nullable(2)]
			internal JToken _token;
		}
	}
}
