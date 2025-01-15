using System;
using System.Collections.Generic;
using System.Text;

namespace AngleSharp.Html.Submitters.Json
{
	// Token: 0x020000C9 RID: 201
	internal sealed class JsonObject : JsonElement
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x0002EA9D File Offset: 0x0002CC9D
		public JsonObject()
		{
			this._properties = new Dictionary<string, JsonElement>();
		}

		// Token: 0x17000111 RID: 273
		public override JsonElement this[string key]
		{
			get
			{
				JsonElement jsonElement = null;
				this._properties.TryGetValue(key.ToString(), out jsonElement);
				return jsonElement;
			}
			set
			{
				this._properties[key] = value;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0002EAE4 File Offset: 0x0002CCE4
		public override string ToString()
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder().Append('{');
			bool flag = false;
			foreach (KeyValuePair<string, JsonElement> keyValuePair in this._properties)
			{
				if (flag)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append('"').Append(keyValuePair.Key).Append('"');
				stringBuilder.Append(':').Append(keyValuePair.Value.ToString());
				flag = true;
			}
			return stringBuilder.Append('}').ToPool();
		}

		// Token: 0x040005F2 RID: 1522
		private readonly Dictionary<string, JsonElement> _properties;
	}
}
