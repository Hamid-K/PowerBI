using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngleSharp.Html.Submitters.Json
{
	// Token: 0x020000C7 RID: 199
	internal sealed class JsonArray : JsonElement, IEnumerable<JsonElement>, IEnumerable
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x0002E970 File Offset: 0x0002CB70
		public JsonArray()
		{
			this._elements = new List<JsonElement>();
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0002E983 File Offset: 0x0002CB83
		public int Length
		{
			get
			{
				return this._elements.Count;
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0002E990 File Offset: 0x0002CB90
		public void Push(JsonElement element)
		{
			this._elements.Add(element);
		}

		// Token: 0x1700010F RID: 271
		public JsonElement this[int key]
		{
			get
			{
				return this._elements.ElementAtOrDefault(key);
			}
			set
			{
				for (int i = this._elements.Count; i <= key; i++)
				{
					this._elements.Add(null);
				}
				this._elements[key] = value;
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0002E9E8 File Offset: 0x0002CBE8
		public override string ToString()
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder().Append('[');
			bool flag = false;
			foreach (JsonElement jsonElement in this._elements)
			{
				if (flag)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(((jsonElement != null) ? jsonElement.ToString() : null) ?? "null");
				flag = true;
			}
			return stringBuilder.Append(']').ToPool();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0002EA7C File Offset: 0x0002CC7C
		public IEnumerator<JsonElement> GetEnumerator()
		{
			return this._elements.GetEnumerator();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0002EA8E File Offset: 0x0002CC8E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040005F1 RID: 1521
		private readonly List<JsonElement> _elements;
	}
}
