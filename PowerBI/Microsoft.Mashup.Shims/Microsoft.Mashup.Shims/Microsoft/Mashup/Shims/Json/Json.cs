using System;
using System.Web.Script.Serialization;

namespace Microsoft.Mashup.Shims.Json
{
	// Token: 0x02000012 RID: 18
	public static class Json
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000023C2 File Offset: 0x000005C2
		public static string SerializeObject(object obj)
		{
			return new JavaScriptSerializer().Serialize(obj);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023CF File Offset: 0x000005CF
		public static T DeserializeObject<T>(string json)
		{
			return new JavaScriptSerializer
			{
				MaxJsonLength = int.MaxValue
			}.Deserialize<T>(json);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023E7 File Offset: 0x000005E7
		public static T DeserializeObject<T>(string json, int maxDepth)
		{
			return new JavaScriptSerializer
			{
				MaxJsonLength = int.MaxValue,
				RecursionLimit = maxDepth
			}.Deserialize<T>(json);
		}
	}
}
