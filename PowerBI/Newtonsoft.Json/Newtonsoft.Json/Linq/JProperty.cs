using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000BF RID: 191
	[NullableContext(1)]
	[Nullable(0)]
	public class JProperty : JContainer
	{
		// Token: 0x06000A7C RID: 2684 RVA: 0x0002A8C4 File Offset: 0x00028AC4
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			Task task = writer.WritePropertyNameAsync(this._name, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this.WriteValueAsync(writer, cancellationToken, converters);
			}
			return this.WriteToAsync(task, writer, cancellationToken, converters);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002A8FC File Offset: 0x00028AFC
		private async Task WriteToAsync(Task task, JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			await task.ConfigureAwait(false);
			await this.WriteValueAsync(writer, cancellationToken, converters).ConfigureAwait(false);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002A960 File Offset: 0x00028B60
		private Task WriteValueAsync(JsonWriter writer, CancellationToken cancellationToken, JsonConverter[] converters)
		{
			JToken value = this.Value;
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			return value.WriteToAsync(writer, cancellationToken, converters);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002A988 File Offset: 0x00028B88
		public new static Task<JProperty> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JProperty.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002A994 File Offset: 0x00028B94
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
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002A9E7 File Offset: 0x00028BE7
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0002A9EF File Offset: 0x00028BEF
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this._name;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002A9F7 File Offset: 0x00028BF7
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x0002AA04 File Offset: 0x00028C04
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
					this.InsertItem(0, jtoken, false, true);
					return;
				}
				this.SetItem(0, jtoken);
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002AA43 File Offset: 0x00028C43
		public JProperty(JProperty other)
			: base(other, null)
		{
			this._name = other.Name;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002AA64 File Offset: 0x00028C64
		internal JProperty(JProperty other, [Nullable(2)] JsonCloneSettings settings)
			: base(other, settings)
		{
			this._name = other.Name;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002AA85 File Offset: 0x00028C85
		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.Value;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002AA98 File Offset: 0x00028C98
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

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002AAF2 File Offset: 0x00028CF2
		[NullableContext(2)]
		internal override bool RemoveItem(JToken item)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002AB12 File Offset: 0x00028D12
		internal override void RemoveItemAt(int index)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002AB32 File Offset: 0x00028D32
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._content.IndexOf(item);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002AB48 File Offset: 0x00028D48
		[NullableContext(2)]
		internal override bool InsertItem(int index, JToken item, bool skipParentCheck, bool copyAnnotations)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return false;
			}
			if (this.Value != null)
			{
				throw new JsonException("{0} cannot have multiple values.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
			}
			return base.InsertItem(0, item, false, copyAnnotations);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002AB95 File Offset: 0x00028D95
		[NullableContext(2)]
		internal override bool ContainsItem(JToken item)
		{
			return this.Value == item;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002ABA0 File Offset: 0x00028DA0
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JProperty jproperty = content as JProperty;
			JToken jtoken = ((jproperty != null) ? jproperty.Value : null);
			if (jtoken != null && jtoken.Type != JTokenType.Null)
			{
				this.Value = jtoken;
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002ABD4 File Offset: 0x00028DD4
		internal override void ClearItems()
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002ABF4 File Offset: 0x00028DF4
		internal override bool DeepEquals(JToken node)
		{
			JProperty jproperty = node as JProperty;
			return jproperty != null && this._name == jproperty.Name && base.ContentsEqual(jproperty);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002AC27 File Offset: 0x00028E27
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings)
		{
			return new JProperty(this, settings);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0002AC30 File Offset: 0x00028E30
		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002AC33 File Offset: 0x00028E33
		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002AC58 File Offset: 0x00028E58
		public JProperty(string name, params object[] content)
			: this(name, content)
		{
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002AC64 File Offset: 0x00028E64
		public JProperty(string name, [Nullable(2)] object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
			this.Value = (base.IsMultiContent(content) ? new JArray(content) : JContainer.CreateFromContent(content));
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002ACB4 File Offset: 0x00028EB4
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

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002ACE6 File Offset: 0x00028EE6
		internal override int GetDeepHashCode()
		{
			int hashCode = this._name.GetHashCode();
			JToken value = this.Value;
			return hashCode ^ ((value != null) ? value.GetDeepHashCode() : 0);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002AD06 File Offset: 0x00028F06
		public new static JProperty Load(JsonReader reader)
		{
			return JProperty.Load(reader, null);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002AD10 File Offset: 0x00028F10
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

		// Token: 0x04000386 RID: 902
		private readonly JProperty.JPropertyList _content = new JProperty.JPropertyList();

		// Token: 0x04000387 RID: 903
		private readonly string _name;

		// Token: 0x020001CB RID: 459
		[Nullable(0)]
		private class JPropertyList : IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
		{
			// Token: 0x06000FBA RID: 4026 RVA: 0x00045642 File Offset: 0x00043842
			public IEnumerator<JToken> GetEnumerator()
			{
				if (this._token != null)
				{
					yield return this._token;
				}
				yield break;
			}

			// Token: 0x06000FBB RID: 4027 RVA: 0x00045651 File Offset: 0x00043851
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000FBC RID: 4028 RVA: 0x00045659 File Offset: 0x00043859
			public void Add(JToken item)
			{
				this._token = item;
			}

			// Token: 0x06000FBD RID: 4029 RVA: 0x00045662 File Offset: 0x00043862
			public void Clear()
			{
				this._token = null;
			}

			// Token: 0x06000FBE RID: 4030 RVA: 0x0004566B File Offset: 0x0004386B
			public bool Contains(JToken item)
			{
				return this._token == item;
			}

			// Token: 0x06000FBF RID: 4031 RVA: 0x00045676 File Offset: 0x00043876
			public void CopyTo(JToken[] array, int arrayIndex)
			{
				if (this._token != null)
				{
					array[arrayIndex] = this._token;
				}
			}

			// Token: 0x06000FC0 RID: 4032 RVA: 0x00045689 File Offset: 0x00043889
			public bool Remove(JToken item)
			{
				if (this._token == item)
				{
					this._token = null;
					return true;
				}
				return false;
			}

			// Token: 0x170002A1 RID: 673
			// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x0004569E File Offset: 0x0004389E
			public int Count
			{
				get
				{
					if (this._token == null)
					{
						return 0;
					}
					return 1;
				}
			}

			// Token: 0x170002A2 RID: 674
			// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x000456AB File Offset: 0x000438AB
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000FC3 RID: 4035 RVA: 0x000456AE File Offset: 0x000438AE
			public int IndexOf(JToken item)
			{
				if (this._token != item)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x06000FC4 RID: 4036 RVA: 0x000456BC File Offset: 0x000438BC
			public void Insert(int index, JToken item)
			{
				if (index == 0)
				{
					this._token = item;
				}
			}

			// Token: 0x06000FC5 RID: 4037 RVA: 0x000456C8 File Offset: 0x000438C8
			public void RemoveAt(int index)
			{
				if (index == 0)
				{
					this._token = null;
				}
			}

			// Token: 0x170002A3 RID: 675
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

			// Token: 0x040007CA RID: 1994
			[Nullable(2)]
			internal JToken _token;
		}
	}
}
