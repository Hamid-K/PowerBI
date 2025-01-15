using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200023C RID: 572
	public static class Json
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x000331F2 File Offset: 0x000313F2
		public static string JsonEmptyObject
		{
			get
			{
				return Json.s_jsonEmptyObject;
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x000331FC File Offset: 0x000313FC
		public static string SerializeToString([NotNull] object obj)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<object>(obj, "obj");
			string text = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlDictionaryWriter xmlDictionaryWriter = JsonReaderWriterFactory.CreateJsonWriter(memoryStream, Encoding.UTF8, true))
				{
					new DataContractJsonSerializer(obj.GetType()).WriteObject(xmlDictionaryWriter, obj);
					xmlDictionaryWriter.Flush();
					text = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			return text;
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00033288 File Offset: 0x00031488
		public static T DeserializeFromString<T>(string data) where T : class
		{
			T t = default(T);
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
			{
				using (XmlDictionaryReader xmlDictionaryReader = JsonReaderWriterFactory.CreateJsonReader(memoryStream, Encoding.UTF8, XmlDictionaryReaderQuotas.Max, null))
				{
					t = (T)((object)new DataContractJsonSerializer(typeof(T)).ReadObject(xmlDictionaryReader));
				}
			}
			return t;
		}

		// Token: 0x040005A9 RID: 1449
		private static string s_jsonEmptyObject = "{}";
	}
}
