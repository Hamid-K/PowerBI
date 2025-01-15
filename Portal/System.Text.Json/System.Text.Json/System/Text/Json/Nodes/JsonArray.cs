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
	// Token: 0x0200005D RID: 93
	[NullableContext(2)]
	[Nullable(0)]
	[DebuggerDisplay("JsonArray[{List.Count}]")]
	[DebuggerTypeProxy(typeof(JsonArray.DebugView))]
	public sealed class JsonArray : JsonNode, IList<JsonNode>, ICollection<JsonNode>, IEnumerable<JsonNode>, IEnumerable
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x0002114E File Offset: 0x0001F34E
		public JsonArray(JsonNodeOptions? options = null)
			: base(options)
		{
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00021157 File Offset: 0x0001F357
		public JsonArray(JsonNodeOptions options, [Nullable(new byte[] { 1, 2 })] params JsonNode[] items)
			: base(new JsonNodeOptions?(options))
		{
			this.InitializeFromArray(items);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0002116C File Offset: 0x0001F36C
		public JsonArray([Nullable(new byte[] { 1, 2 })] params JsonNode[] items)
			: base(null)
		{
			this.InitializeFromArray(items);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0002118F File Offset: 0x0001F38F
		internal override JsonValueKind GetValueKindCore()
		{
			return JsonValueKind.Array;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00021194 File Offset: 0x0001F394
		internal override JsonNode DeepCloneCore()
		{
			List<JsonNode> list;
			JsonElement? jsonElement;
			this.GetUnderlyingRepresentation(out list, out jsonElement);
			if (list != null)
			{
				JsonArray jsonArray = new JsonArray(base.Options)
				{
					_list = new List<JsonNode>(list.Count)
				};
				for (int i = 0; i < list.Count; i++)
				{
					JsonArray jsonArray2 = jsonArray;
					JsonNode jsonNode = list[i];
					jsonArray2.Add((jsonNode != null) ? jsonNode.DeepCloneCore() : null);
				}
				return jsonArray;
			}
			if (jsonElement == null)
			{
				return new JsonArray(base.Options);
			}
			return new JsonArray(jsonElement.Value.Clone(), base.Options);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0002122C File Offset: 0x0001F42C
		internal override bool DeepEqualsCore(JsonNode node)
		{
			if (node == null || node is JsonObject)
			{
				return false;
			}
			JsonValue jsonValue = node as JsonValue;
			if (jsonValue != null)
			{
				return jsonValue.DeepEqualsCore(this);
			}
			JsonArray jsonArray = node as JsonArray;
			if (jsonArray == null)
			{
				return false;
			}
			List<JsonNode> list = this.List;
			List<JsonNode> list2 = jsonArray.List;
			if (list.Count != list2.Count)
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (!JsonNode.DeepEquals(list[i], list2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x000212B3 File Offset: 0x0001F4B3
		internal int GetElementIndex(JsonNode node)
		{
			return this.List.IndexOf(node);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x000212C1 File Offset: 0x0001F4C1
		[NullableContext(1)]
		public IEnumerable<T> GetValues<[Nullable(2)] T>()
		{
			foreach (JsonNode jsonNode in this.List)
			{
				yield return (jsonNode == null) ? ((T)((object)null)) : jsonNode.GetValue<T>();
			}
			List<JsonNode>.Enumerator enumerator = default(List<JsonNode>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000212D4 File Offset: 0x0001F4D4
		private void InitializeFromArray(JsonNode[] items)
		{
			List<JsonNode> list = new List<JsonNode>(items);
			foreach (JsonNode jsonNode in items)
			{
				if (jsonNode != null)
				{
					jsonNode.AssignParent(this);
				}
			}
			this._list = list;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0002130C File Offset: 0x0001F50C
		public static JsonArray Create(JsonElement element, JsonNodeOptions? options = null)
		{
			JsonValueKind valueKind = element.ValueKind;
			JsonArray jsonArray;
			if (valueKind != JsonValueKind.Array)
			{
				if (valueKind != JsonValueKind.Null)
				{
					throw new InvalidOperationException(SR.Format(SR.NodeElementWrongType, "Array"));
				}
				jsonArray = null;
			}
			else
			{
				jsonArray = new JsonArray(element, options);
			}
			return jsonArray;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0002134D File Offset: 0x0001F54D
		internal JsonArray(JsonElement element, JsonNodeOptions? options = null)
			: base(options)
		{
			this._jsonElement = new JsonElement?(element);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00021364 File Offset: 0x0001F564
		[RequiresUnreferencedCode("Creating JsonValue instances with non-primitive types is not compatible with trimming. It can result in non-primitive types being serialized, which may have their members trimmed.")]
		[RequiresDynamicCode("Creating JsonValue instances with non-primitive types requires generating code at runtime.")]
		public void Add<T>(T value)
		{
			JsonNode jsonNode = JsonNode.ConvertFromValue<T>(value, base.Options);
			this.Add(jsonNode);
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00021388 File Offset: 0x0001F588
		[Nullable(new byte[] { 1, 2 })]
		internal List<JsonNode> List
		{
			get
			{
				List<JsonNode> list = this._list;
				if (list == null)
				{
					return this.InitializeList();
				}
				return list;
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000213A7 File Offset: 0x0001F5A7
		internal JsonNode GetItem(int index)
		{
			return this.List[index];
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000213B5 File Offset: 0x0001F5B5
		internal void SetItem(int index, JsonNode value)
		{
			if (value != null)
			{
				value.AssignParent(this);
			}
			JsonArray.DetachParent(this.List[index]);
			this.List[index] = value;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000213E0 File Offset: 0x0001F5E0
		internal override void GetPath(List<string> path, JsonNode child)
		{
			if (child != null)
			{
				int num = this.List.IndexOf(child);
				path.Add(string.Format("[{0}]", num));
			}
			JsonNode parent = base.Parent;
			if (parent == null)
			{
				return;
			}
			parent.GetPath(path, this);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00021428 File Offset: 0x0001F628
		[NullableContext(1)]
		public override void WriteTo(Utf8JsonWriter writer, [Nullable(2)] JsonSerializerOptions options = null)
		{
			if (writer == null)
			{
				ThrowHelper.ThrowArgumentNullException("writer");
			}
			List<JsonNode> list;
			JsonElement? jsonElement;
			this.GetUnderlyingRepresentation(out list, out jsonElement);
			if (list == null && jsonElement != null)
			{
				jsonElement.Value.WriteTo(writer);
				return;
			}
			writer.WriteStartArray();
			foreach (JsonNode jsonNode in this.List)
			{
				if (jsonNode == null)
				{
					writer.WriteNullValue();
				}
				else
				{
					jsonNode.WriteTo(writer, options);
				}
			}
			writer.WriteEndArray();
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000214CC File Offset: 0x0001F6CC
		private List<JsonNode> InitializeList()
		{
			List<JsonNode> list;
			JsonElement? jsonElement;
			this.GetUnderlyingRepresentation(out list, out jsonElement);
			if (list == null)
			{
				if (jsonElement != null)
				{
					JsonElement value = jsonElement.Value;
					list = new List<JsonNode>(value.GetArrayLength());
					using (JsonElement.ArrayEnumerator enumerator = value.EnumerateArray().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							JsonElement jsonElement2 = enumerator.Current;
							JsonNode jsonNode = JsonNodeConverter.Create(jsonElement2, base.Options);
							if (jsonNode != null)
							{
								jsonNode.AssignParent(this);
							}
							list.Add(jsonNode);
						}
						goto IL_008C;
					}
				}
				list = new List<JsonNode>();
				IL_008C:
				this._list = list;
				Interlocked.MemoryBarrier();
				this._jsonElement = null;
			}
			return list;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00021590 File Offset: 0x0001F790
		private void GetUnderlyingRepresentation(out List<JsonNode> list, out JsonElement? jsonElement)
		{
			jsonElement = this._jsonElement;
			Interlocked.MemoryBarrier();
			list = this._list;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x000215AB File Offset: 0x0001F7AB
		public int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000215B8 File Offset: 0x0001F7B8
		public void Add(JsonNode item)
		{
			if (item != null)
			{
				item.AssignParent(this);
			}
			this.List.Add(item);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000215D0 File Offset: 0x0001F7D0
		public void Clear()
		{
			List<JsonNode> list = this._list;
			if (list == null)
			{
				this._jsonElement = null;
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				JsonArray.DetachParent(list[i]);
			}
			list.Clear();
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00021617 File Offset: 0x0001F817
		public bool Contains(JsonNode item)
		{
			return this.List.Contains(item);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00021625 File Offset: 0x0001F825
		public int IndexOf(JsonNode item)
		{
			return this.List.IndexOf(item);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00021633 File Offset: 0x0001F833
		public void Insert(int index, JsonNode item)
		{
			if (item != null)
			{
				item.AssignParent(this);
			}
			this.List.Insert(index, item);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0002164C File Offset: 0x0001F84C
		public bool Remove(JsonNode item)
		{
			if (this.List.Remove(item))
			{
				JsonArray.DetachParent(item);
				return true;
			}
			return false;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00021668 File Offset: 0x0001F868
		public void RemoveAt(int index)
		{
			JsonNode jsonNode = this.List[index];
			this.List.RemoveAt(index);
			JsonArray.DetachParent(jsonNode);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00021694 File Offset: 0x0001F894
		void ICollection<JsonNode>.CopyTo(JsonNode[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000216A3 File Offset: 0x0001F8A3
		[return: Nullable(new byte[] { 1, 2 })]
		public IEnumerator<JsonNode> GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000216B5 File Offset: 0x0001F8B5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.List).GetEnumerator();
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x000216C2 File Offset: 0x0001F8C2
		bool ICollection<JsonNode>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000216C5 File Offset: 0x0001F8C5
		private static void DetachParent(JsonNode item)
		{
			if (item != null)
			{
				item.Parent = null;
			}
		}

		// Token: 0x04000280 RID: 640
		private JsonElement? _jsonElement;

		// Token: 0x04000281 RID: 641
		private List<JsonNode> _list;

		// Token: 0x0200012C RID: 300
		[ExcludeFromCodeCoverage]
		private sealed class DebugView
		{
			// Token: 0x06000DBD RID: 3517 RVA: 0x000358C6 File Offset: 0x00033AC6
			public DebugView(JsonArray node)
			{
				this._node = node;
			}

			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x06000DBE RID: 3518 RVA: 0x000358D5 File Offset: 0x00033AD5
			public string Json
			{
				get
				{
					return this._node.ToJsonString(null);
				}
			}

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000358E3 File Offset: 0x00033AE3
			public string Path
			{
				get
				{
					return this._node.GetPath();
				}
			}

			// Token: 0x170002EB RID: 747
			// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x000358F0 File Offset: 0x00033AF0
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			private JsonArray.DebugView.DebugViewItem[] Items
			{
				get
				{
					JsonArray.DebugView.DebugViewItem[] array = new JsonArray.DebugView.DebugViewItem[this._node.List.Count];
					for (int i = 0; i < this._node.List.Count; i++)
					{
						array[i].Value = this._node.List[i];
					}
					return array;
				}
			}

			// Token: 0x040004B1 RID: 1201
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private readonly JsonArray _node;

			// Token: 0x02000176 RID: 374
			[DebuggerDisplay("{Display,nq}")]
			private struct DebugViewItem
			{
				// Token: 0x17000306 RID: 774
				// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00037864 File Offset: 0x00035A64
				[DebuggerBrowsable(DebuggerBrowsableState.Never)]
				public string Display
				{
					get
					{
						if (this.Value == null)
						{
							return "null";
						}
						if (this.Value is JsonValue)
						{
							return this.Value.ToJsonString(null);
						}
						JsonObject jsonObject = this.Value as JsonObject;
						if (jsonObject != null)
						{
							return string.Format("JsonObject[{0}]", jsonObject.Count);
						}
						JsonArray jsonArray = (JsonArray)this.Value;
						return string.Format("JsonArray[{0}]", jsonArray.List.Count);
					}
				}

				// Token: 0x0400056D RID: 1389
				[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
				public JsonNode Value;
			}
		}
	}
}
