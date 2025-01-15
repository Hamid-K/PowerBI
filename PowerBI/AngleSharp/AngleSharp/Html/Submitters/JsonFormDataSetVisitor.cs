using System;
using System.Collections.Generic;
using System.IO;
using AngleSharp.Dom.Io;
using AngleSharp.Extensions;
using AngleSharp.Html.Submitters.Json;

namespace AngleSharp.Html.Submitters
{
	// Token: 0x020000C3 RID: 195
	internal sealed class JsonFormDataSetVisitor : IFormSubmitter, IFormDataSetVisitor
	{
		// Token: 0x060005BC RID: 1468 RVA: 0x0002E43B File Offset: 0x0002C63B
		public JsonFormDataSetVisitor()
		{
			this._context = new JsonObject();
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0002E450 File Offset: 0x0002C650
		public void Text(FormDataSetEntry entry, string value)
		{
			JsonValue jsonValue = JsonFormDataSetVisitor.CreateValue(entry.Type, value);
			IEnumerable<JsonStep> enumerable = JsonStep.Parse(entry.Name);
			JsonElement jsonElement = this._context;
			foreach (JsonStep jsonStep in enumerable)
			{
				jsonElement = jsonStep.Run(jsonElement, jsonValue, false);
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0002E4B8 File Offset: 0x0002C6B8
		public void File(FormDataSetEntry entry, string fileName, string contentType, IFile file)
		{
			JsonElement jsonElement = this._context;
			Stream stream = ((file != null && file.Body != null && file.Type != null) ? file.Body : Stream.Null);
			MemoryStream memoryStream = new MemoryStream();
			stream.CopyTo(memoryStream);
			byte[] array = memoryStream.ToArray();
			IEnumerable<JsonStep> enumerable = JsonStep.Parse(entry.Name);
			JsonObject jsonObject = new JsonObject();
			string type = AttributeNames.Type;
			jsonObject[type] = new JsonValue(contentType);
			string name = AttributeNames.Name;
			jsonObject[name] = new JsonValue(fileName);
			string body = AttributeNames.Body;
			jsonObject[body] = new JsonValue(Convert.ToBase64String(array));
			JsonObject jsonObject2 = jsonObject;
			foreach (JsonStep jsonStep in enumerable)
			{
				jsonElement = jsonStep.Run(jsonElement, jsonObject2, true);
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0002E598 File Offset: 0x0002C798
		public void Serialize(StreamWriter stream)
		{
			string text = this._context.ToString();
			stream.Write(text);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0002E5B8 File Offset: 0x0002C7B8
		private static JsonValue CreateValue(string type, string value)
		{
			if (type.Is(InputTypeNames.Checkbox))
			{
				return new JsonValue(value.Is(Keywords.On));
			}
			if (type.Is(InputTypeNames.Number))
			{
				return new JsonValue(value.ToDouble(0.0));
			}
			return new JsonValue(value);
		}

		// Token: 0x040005E7 RID: 1511
		private readonly JsonObject _context;
	}
}
