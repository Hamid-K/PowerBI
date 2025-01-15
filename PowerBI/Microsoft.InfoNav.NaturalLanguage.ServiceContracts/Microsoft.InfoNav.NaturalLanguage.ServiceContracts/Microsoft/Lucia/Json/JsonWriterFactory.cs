using System;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Json
{
	// Token: 0x02000032 RID: 50
	public static class JsonWriterFactory
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00003911 File Offset: 0x00001B11
		public static JsonWriter Create(TextWriter writer, Formatting formatting = Formatting.None)
		{
			return new JsonTextWriter(writer)
			{
				Formatting = formatting
			};
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003920 File Offset: 0x00001B20
		public static string ToString(Action<JsonWriter> write)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
			{
				write(jsonTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003968 File Offset: 0x00001B68
		public static T To<T>(Action<JsonWriter> write) where T : JToken
		{
			JTokenWriter jtokenWriter = new JTokenWriter();
			write(jtokenWriter);
			return (T)((object)jtokenWriter.Token);
		}
	}
}
