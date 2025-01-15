using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Linq
{
	// Token: 0x020000C0 RID: 192
	[NullableContext(1)]
	[Nullable(0)]
	internal class JProperty : JContainer
	{
		// Token: 0x06000A7A RID: 2682 RVA: 0x0002A820 File Offset: 0x00028A20
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			Task task = writer.WritePropertyNameAsync(this._name, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this.WriteValueAsync(writer, cancellationToken, converters);
			}
			return this.WriteToAsync(task, writer, cancellationToken, converters);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002A858 File Offset: 0x00028A58
		private async Task WriteToAsync(Task task, JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			await task.ConfigureAwait(false);
			await this.WriteValueAsync(writer, cancellationToken, converters).ConfigureAwait(false);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002A8BC File Offset: 0x00028ABC
		private Task WriteValueAsync(JsonWriter writer, CancellationToken cancellationToken, JsonConverter[] converters)
		{
			JToken value = this.Value;
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			return value.WriteToAsync(writer, cancellationToken, converters);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002A8E4 File Offset: 0x00028AE4
		public new static Task<JProperty> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JProperty.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002A8F0 File Offset: 0x00028AF0
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
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002A943 File Offset: 0x00028B43
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0002A94B File Offset: 0x00028B4B
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this._name;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002A953 File Offset: 0x00028B53
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x0002A960 File Offset: 0x00028B60
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

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002A99E File Offset: 0x00028B9E
		public JProperty(JProperty other)
			: base(other)
		{
			this._name = other.Name;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002A9BE File Offset: 0x00028BBE
		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.Value;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002A9D0 File Offset: 0x00028BD0
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

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002AA2A File Offset: 0x00028C2A
		[NullableContext(2)]
		internal override bool RemoveItem(JToken item)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002AA4A File Offset: 0x00028C4A
		internal override void RemoveItemAt(int index)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002AA6A File Offset: 0x00028C6A
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._content.IndexOf(item);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002AA7D File Offset: 0x00028C7D
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

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002AABD File Offset: 0x00028CBD
		[NullableContext(2)]
		internal override bool ContainsItem(JToken item)
		{
			return this.Value == item;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002AAC8 File Offset: 0x00028CC8
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JProperty jproperty = content as JProperty;
			JToken jtoken = ((jproperty != null) ? jproperty.Value : null);
			if (jtoken != null && jtoken.Type != JTokenType.Null)
			{
				this.Value = jtoken;
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002AAFC File Offset: 0x00028CFC
		internal override void ClearItems()
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002AB1C File Offset: 0x00028D1C
		internal override bool DeepEquals(JToken node)
		{
			JProperty jproperty = node as JProperty;
			return jproperty != null && this._name == jproperty.Name && base.ContentsEqual(jproperty);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002AB4F File Offset: 0x00028D4F
		internal override JToken CloneToken()
		{
			return new JProperty(this);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0002AB57 File Offset: 0x00028D57
		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002AB5A File Offset: 0x00028D5A
		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002AB7F File Offset: 0x00028D7F
		public JProperty(string name, params object[] content)
			: this(name, content)
		{
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002AB8C File Offset: 0x00028D8C
		public JProperty(string name, [Nullable(2)] object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
			this.Value = (base.IsMultiContent(content) ? new JArray(content) : JContainer.CreateFromContent(content));
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002ABDC File Offset: 0x00028DDC
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

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002AC0E File Offset: 0x00028E0E
		internal override int GetDeepHashCode()
		{
			int hashCode = this._name.GetHashCode();
			JToken value = this.Value;
			return hashCode ^ ((value != null) ? value.GetDeepHashCode() : 0);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002AC2E File Offset: 0x00028E2E
		public new static JProperty Load(JsonReader reader)
		{
			return JProperty.Load(reader, null);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002AC38 File Offset: 0x00028E38
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

		// Token: 0x04000387 RID: 903
		private readonly JProperty.JPropertyList _content = new JProperty.JPropertyList();

		// Token: 0x04000388 RID: 904
		private readonly string _name;

		// Token: 0x02000250 RID: 592
		[Nullable(0)]
		private class JPropertyList : IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
		{
			// Token: 0x0600141C RID: 5148 RVA: 0x000524CA File Offset: 0x000506CA
			public IEnumerator<JToken> GetEnumerator()
			{
				if (this._token != null)
				{
					yield return this._token;
				}
				yield break;
			}

			// Token: 0x0600141D RID: 5149 RVA: 0x000524D9 File Offset: 0x000506D9
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600141E RID: 5150 RVA: 0x000524E1 File Offset: 0x000506E1
			public void Add(JToken item)
			{
				this._token = item;
			}

			// Token: 0x0600141F RID: 5151 RVA: 0x000524EA File Offset: 0x000506EA
			public void Clear()
			{
				this._token = null;
			}

			// Token: 0x06001420 RID: 5152 RVA: 0x000524F3 File Offset: 0x000506F3
			public bool Contains(JToken item)
			{
				return this._token == item;
			}

			// Token: 0x06001421 RID: 5153 RVA: 0x000524FE File Offset: 0x000506FE
			public void CopyTo(JToken[] array, int arrayIndex)
			{
				if (this._token != null)
				{
					array[arrayIndex] = this._token;
				}
			}

			// Token: 0x06001422 RID: 5154 RVA: 0x00052511 File Offset: 0x00050711
			public bool Remove(JToken item)
			{
				if (this._token == item)
				{
					this._token = null;
					return true;
				}
				return false;
			}

			// Token: 0x170003A4 RID: 932
			// (get) Token: 0x06001423 RID: 5155 RVA: 0x00052526 File Offset: 0x00050726
			public int Count
			{
				get
				{
					return (this._token != null) ? 1 : 0;
				}
			}

			// Token: 0x170003A5 RID: 933
			// (get) Token: 0x06001424 RID: 5156 RVA: 0x00052531 File Offset: 0x00050731
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001425 RID: 5157 RVA: 0x00052534 File Offset: 0x00050734
			public int IndexOf(JToken item)
			{
				if (this._token != item)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x06001426 RID: 5158 RVA: 0x00052542 File Offset: 0x00050742
			public void Insert(int index, JToken item)
			{
				if (index == 0)
				{
					this._token = item;
				}
			}

			// Token: 0x06001427 RID: 5159 RVA: 0x0005254E File Offset: 0x0005074E
			public void RemoveAt(int index)
			{
				if (index == 0)
				{
					this._token = null;
				}
			}

			// Token: 0x170003A6 RID: 934
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

			// Token: 0x04000A5E RID: 2654
			[Nullable(2)]
			internal JToken _token;
		}
	}
}
