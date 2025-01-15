using System;
using System.IO;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;
using YamlDotNet.Core;

namespace Microsoft.Lucia.Json
{
	// Token: 0x02000031 RID: 49
	public static class JsonReaderFactory
	{
		// Token: 0x060000CA RID: 202 RVA: 0x000038BB File Offset: 0x00001ABB
		public static JsonReader Create(TextReader reader, bool leaveOpen = false)
		{
			return new JsonTextReader(reader)
			{
				CloseInput = !leaveOpen
			};
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000038CD File Offset: 0x00001ACD
		public static JsonReader Create(Stream stream, bool leaveOpen = false)
		{
			return JsonReaderFactory.Create(new StreamReader(stream), leaveOpen);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000038DB File Offset: 0x00001ADB
		public static JsonReader CreateFromYaml(TextReader reader, bool leaveOpen = false)
		{
			return new YamlJsonReader(reader)
			{
				CloseInput = !leaveOpen
			};
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000038ED File Offset: 0x00001AED
		public static JsonReader CreateFromYaml(IParser parser)
		{
			return new YamlJsonReader(parser);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000038F5 File Offset: 0x00001AF5
		public static JsonReader FromString(string json)
		{
			return JsonReaderFactory.Create(new StringReader(json), false);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003903 File Offset: 0x00001B03
		public static JsonReader FromYamlString(string yaml)
		{
			return JsonReaderFactory.CreateFromYaml(new StringReader(yaml), false);
		}
	}
}
