using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Json
{
	// Token: 0x0200002E RID: 46
	public static class JsonExtensions
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00003697 File Offset: 0x00001897
		public static bool HasProperty(this JObject obj, string name)
		{
			return obj.Property(name) != null;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000036A4 File Offset: 0x000018A4
		public static void VisitDictionaryElements(this JObject dictionary, Action<JObject> visitElement)
		{
			foreach (JObject jobject in dictionary.PropertyValues().OfType<JObject>())
			{
				visitElement(jobject);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000036FC File Offset: 0x000018FC
		public static void VisitArrayElements(this JArray array, Action<JObject> visitElement)
		{
			foreach (JObject jobject in array.OfType<JObject>())
			{
				visitElement(jobject);
			}
		}
	}
}
