using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000BA RID: 186
	[NullableContext(1)]
	[Nullable(0)]
	public class JArray : JContainer, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x000283B4 File Offset: 0x000265B4
		public override async Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			await writer.WriteStartArrayAsync(cancellationToken).ConfigureAwait(false);
			for (int i = 0; i < this._values.Count; i++)
			{
				await this._values[i].WriteToAsync(writer, cancellationToken, converters).ConfigureAwait(false);
			}
			await writer.WriteEndArrayAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002840F File Offset: 0x0002660F
		public new static Task<JArray> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JArray.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002841C File Offset: 0x0002661C
		public new static async Task<JArray> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
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
					throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray a = new JArray();
			a.SetLineInfo(reader as IJsonLineInfo, settings);
			await a.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return a;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0002846F File Offset: 0x0002666F
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00028477 File Offset: 0x00026677
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Array;
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002847A File Offset: 0x0002667A
		public JArray()
		{
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002848D File Offset: 0x0002668D
		public JArray(JArray other)
			: base(other, null)
		{
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000284A2 File Offset: 0x000266A2
		internal JArray(JArray other, [Nullable(2)] JsonCloneSettings settings)
			: base(other, settings)
		{
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000284B7 File Offset: 0x000266B7
		public JArray(params object[] content)
			: this(content)
		{
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x000284C0 File Offset: 0x000266C0
		public JArray(object content)
		{
			this.Add(content);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x000284DC File Offset: 0x000266DC
		internal override bool DeepEquals(JToken node)
		{
			JArray jarray = node as JArray;
			return jarray != null && base.ContentsEqual(jarray);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000284FC File Offset: 0x000266FC
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings = null)
		{
			return new JArray(this, settings);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00028505 File Offset: 0x00026705
		public new static JArray Load(JsonReader reader)
		{
			return JArray.Load(reader, null);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00028510 File Offset: 0x00026710
		public new static JArray Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray jarray = new JArray();
			jarray.SetLineInfo(reader as IJsonLineInfo, settings);
			jarray.ReadTokenFrom(reader, settings);
			return jarray;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00028584 File Offset: 0x00026784
		public new static JArray Parse(string json)
		{
			return JArray.Parse(json, null);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00028590 File Offset: 0x00026790
		public new static JArray Parse(string json, [Nullable(2)] JsonLoadSettings settings)
		{
			JArray jarray2;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JArray jarray = JArray.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				jarray2 = jarray;
			}
			return jarray2;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000285D8 File Offset: 0x000267D8
		public new static JArray FromObject(object o)
		{
			return JArray.FromObject(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000285E8 File Offset: 0x000267E8
		public new static JArray FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Array)
			{
				throw new ArgumentException("Object serialized to {0}. JArray instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JArray)jtoken;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002862C File Offset: 0x0002682C
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartArray();
			for (int i = 0; i < this._values.Count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndArray();
		}

		// Token: 0x170001BD RID: 445
		[Nullable(2)]
		public override JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this.GetItem((int)key);
			}
			[param: Nullable(2)]
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Set JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this.SetItem((int)key, value);
			}
		}

		// Token: 0x170001BE RID: 446
		public JToken this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000286FA File Offset: 0x000268FA
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._values.IndexOfReference(item);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00028710 File Offset: 0x00026910
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			IEnumerable enumerable = ((base.IsMultiContent(content) || content is JArray) ? ((IEnumerable)content) : null);
			if (enumerable == null)
			{
				return;
			}
			JContainer.MergeEnumerableContent(this, enumerable, settings);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00028744 File Offset: 0x00026944
		public int IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002874D File Offset: 0x0002694D
		public void Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false, true);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002875A File Offset: 0x0002695A
		public void RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00028764 File Offset: 0x00026964
		public IEnumerator<JToken> GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0002877F File Offset: 0x0002697F
		public void Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00028788 File Offset: 0x00026988
		public void Clear()
		{
			this.ClearItems();
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00028790 File Offset: 0x00026990
		public bool Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00028799 File Offset: 0x00026999
		public void CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x000287A3 File Offset: 0x000269A3
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000287A6 File Offset: 0x000269A6
		public bool Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000287AF File Offset: 0x000269AF
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x04000379 RID: 889
		private readonly List<JToken> _values = new List<JToken>();
	}
}
