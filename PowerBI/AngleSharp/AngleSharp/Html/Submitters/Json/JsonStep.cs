using System;
using System.Collections.Generic;
using AngleSharp.Extensions;

namespace AngleSharp.Html.Submitters.Json
{
	// Token: 0x020000CA RID: 202
	internal abstract class JsonStep
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0002EB90 File Offset: 0x0002CD90
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0002EB98 File Offset: 0x0002CD98
		public bool Append { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0002EBA1 File Offset: 0x0002CDA1
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0002EBA9 File Offset: 0x0002CDA9
		public JsonStep Next { get; set; }

		// Token: 0x060005E3 RID: 1507 RVA: 0x0002EBB4 File Offset: 0x0002CDB4
		public static IEnumerable<JsonStep> Parse(string path)
		{
			List<JsonStep> list = new List<JsonStep>();
			int i = 0;
			while (i < path.Length && path[i] != '[')
			{
				i++;
			}
			if (i == 0)
			{
				return JsonStep.FailedJsonSteps(path);
			}
			list.Add(new JsonStep.ObjectStep(path.Substring(0, i)));
			while (i < path.Length)
			{
				if (i + 1 >= path.Length || path[i] != '[')
				{
					return JsonStep.FailedJsonSteps(path);
				}
				if (path[i + 1] == ']')
				{
					list[list.Count - 1].Append = true;
					i += 2;
					if (i < path.Length)
					{
						return JsonStep.FailedJsonSteps(path);
					}
				}
				else if (path[i + 1].IsDigit())
				{
					int num;
					i = (num = i + 1);
					while (i < path.Length && path[i] != ']')
					{
						if (!path[i].IsDigit())
						{
							return JsonStep.FailedJsonSteps(path);
						}
						i++;
					}
					if (i == path.Length)
					{
						return JsonStep.FailedJsonSteps(path);
					}
					list.Add(new JsonStep.ArrayStep(path.Substring(num, i - num).ToInteger(0)));
					i++;
				}
				else
				{
					int num2;
					i = (num2 = i + 1);
					while (i < path.Length && path[i] != ']')
					{
						i++;
					}
					if (i == path.Length)
					{
						return JsonStep.FailedJsonSteps(path);
					}
					list.Add(new JsonStep.ObjectStep(path.Substring(num2, i - num2)));
					i++;
				}
			}
			int j = 0;
			int num3 = list.Count - 1;
			while (j < num3)
			{
				list[j].Next = list[j + 1];
				j++;
			}
			return list;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0002ED58 File Offset: 0x0002CF58
		private static IEnumerable<JsonStep> FailedJsonSteps(string original)
		{
			return new JsonStep.ObjectStep[]
			{
				new JsonStep.ObjectStep(original)
			};
		}

		// Token: 0x060005E5 RID: 1509
		protected abstract JsonElement CreateElement();

		// Token: 0x060005E6 RID: 1510
		protected abstract JsonElement SetValue(JsonElement context, JsonElement value);

		// Token: 0x060005E7 RID: 1511
		protected abstract JsonElement GetValue(JsonElement context);

		// Token: 0x060005E8 RID: 1512
		protected abstract JsonElement ConvertArray(JsonArray value);

		// Token: 0x060005E9 RID: 1513 RVA: 0x0002ED69 File Offset: 0x0002CF69
		public JsonElement Run(JsonElement context, JsonElement value, bool file = false)
		{
			if (this.Next == null)
			{
				return this.JsonEncodeLastValue(context, value, file);
			}
			return this.JsonEncodeValue(context, value, file);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0002ED88 File Offset: 0x0002CF88
		private JsonElement JsonEncodeValue(JsonElement context, JsonElement value, bool file)
		{
			JsonElement value2 = this.GetValue(context);
			if (value2 == null)
			{
				JsonElement jsonElement = this.Next.CreateElement();
				return this.SetValue(context, jsonElement);
			}
			if (value2 is JsonObject)
			{
				return value2;
			}
			if (value2 is JsonArray)
			{
				return this.SetValue(context, this.Next.ConvertArray((JsonArray)value2));
			}
			JsonObject jsonObject = new JsonObject();
			string empty = string.Empty;
			jsonObject[empty] = value2;
			JsonObject jsonObject2 = jsonObject;
			return this.SetValue(context, jsonObject2);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0002EDFC File Offset: 0x0002CFFC
		private JsonElement JsonEncodeLastValue(JsonElement context, JsonElement value, bool file)
		{
			JsonElement value2 = this.GetValue(context);
			if (value2 == null)
			{
				if (this.Append)
				{
					JsonArray jsonArray = new JsonArray();
					jsonArray.Push(value);
					value = jsonArray;
				}
				this.SetValue(context, value);
			}
			else if (value2 is JsonArray)
			{
				((JsonArray)value2).Push(value);
			}
			else
			{
				if (value2 is JsonObject && !file)
				{
					return new JsonStep.ObjectStep(string.Empty).JsonEncodeLastValue(value2, value, true);
				}
				JsonArray jsonArray2 = new JsonArray();
				jsonArray2.Push(value2);
				jsonArray2.Push(value);
				this.SetValue(context, jsonArray2);
			}
			return context;
		}

		// Token: 0x02000475 RID: 1141
		private sealed class ObjectStep : JsonStep
		{
			// Token: 0x060023EF RID: 9199 RVA: 0x0005DB1A File Offset: 0x0005BD1A
			public ObjectStep(string key)
			{
				this.Key = key;
			}

			// Token: 0x17000A6E RID: 2670
			// (get) Token: 0x060023F0 RID: 9200 RVA: 0x0005DB29 File Offset: 0x0005BD29
			// (set) Token: 0x060023F1 RID: 9201 RVA: 0x0005DB31 File Offset: 0x0005BD31
			public string Key { get; private set; }

			// Token: 0x060023F2 RID: 9202 RVA: 0x0005DB3A File Offset: 0x0005BD3A
			protected override JsonElement GetValue(JsonElement context)
			{
				return context[this.Key];
			}

			// Token: 0x060023F3 RID: 9203 RVA: 0x0005DB48 File Offset: 0x0005BD48
			protected override JsonElement SetValue(JsonElement context, JsonElement value)
			{
				context[this.Key] = value;
				return value;
			}

			// Token: 0x060023F4 RID: 9204 RVA: 0x0005DB58 File Offset: 0x0005BD58
			protected override JsonElement CreateElement()
			{
				return new JsonObject();
			}

			// Token: 0x060023F5 RID: 9205 RVA: 0x0005DB60 File Offset: 0x0005BD60
			protected override JsonElement ConvertArray(JsonArray values)
			{
				JsonObject jsonObject = new JsonObject();
				for (int i = 0; i < values.Length; i++)
				{
					JsonElement jsonElement = values[i];
					if (jsonElement != null)
					{
						jsonObject[i.ToString()] = jsonElement;
					}
				}
				return jsonObject;
			}
		}

		// Token: 0x02000476 RID: 1142
		private sealed class ArrayStep : JsonStep
		{
			// Token: 0x060023F6 RID: 9206 RVA: 0x0005DB9E File Offset: 0x0005BD9E
			public ArrayStep(int key)
			{
				this.Key = key;
			}

			// Token: 0x17000A6F RID: 2671
			// (get) Token: 0x060023F7 RID: 9207 RVA: 0x0005DBAD File Offset: 0x0005BDAD
			// (set) Token: 0x060023F8 RID: 9208 RVA: 0x0005DBB5 File Offset: 0x0005BDB5
			public int Key { get; private set; }

			// Token: 0x060023F9 RID: 9209 RVA: 0x0005DBC0 File Offset: 0x0005BDC0
			protected override JsonElement GetValue(JsonElement context)
			{
				JsonArray jsonArray = context as JsonArray;
				if (jsonArray == null)
				{
					return context[this.Key.ToString()];
				}
				return jsonArray[this.Key];
			}

			// Token: 0x060023FA RID: 9210 RVA: 0x0005DBF8 File Offset: 0x0005BDF8
			protected override JsonElement SetValue(JsonElement context, JsonElement value)
			{
				JsonArray jsonArray = context as JsonArray;
				if (jsonArray != null)
				{
					jsonArray[this.Key] = value;
				}
				else
				{
					context[this.Key.ToString()] = value;
				}
				return value;
			}

			// Token: 0x060023FB RID: 9211 RVA: 0x0005DC34 File Offset: 0x0005BE34
			protected override JsonElement CreateElement()
			{
				return new JsonArray();
			}

			// Token: 0x060023FC RID: 9212 RVA: 0x00018A45 File Offset: 0x00016C45
			protected override JsonElement ConvertArray(JsonArray value)
			{
				return value;
			}
		}
	}
}
