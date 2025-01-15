using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace System.Net.Http
{
	// Token: 0x02000024 RID: 36
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class UriExtensions
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00004ACB File Offset: 0x00002CCB
		public static NameValueCollection ParseQueryString(this Uri address)
		{
			if (address == null)
			{
				throw Error.ArgumentNull("address");
			}
			return new FormDataCollection(address).ReadAsNameValueCollection();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004AEC File Offset: 0x00002CEC
		public static bool TryReadQueryAsJson(this Uri address, out JObject value)
		{
			if (address == null)
			{
				throw Error.ArgumentNull("address");
			}
			return FormUrlEncodedJson.TryParse(new FormDataCollection(address), out value);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004B10 File Offset: 0x00002D10
		public static bool TryReadQueryAs(this Uri address, Type type, out object value)
		{
			if (address == null)
			{
				throw Error.ArgumentNull("address");
			}
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			JObject jobject;
			if (FormUrlEncodedJson.TryParse(new FormDataCollection(address), out jobject))
			{
				using (JTokenReader jtokenReader = new JTokenReader(jobject))
				{
					value = new JsonSerializer().Deserialize(jtokenReader, type);
				}
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004B8C File Offset: 0x00002D8C
		public static bool TryReadQueryAs<T>(this Uri address, out T value)
		{
			if (address == null)
			{
				throw Error.ArgumentNull("address");
			}
			JObject jobject;
			if (FormUrlEncodedJson.TryParse(new FormDataCollection(address), out jobject))
			{
				value = jobject.ToObject<T>();
				return true;
			}
			value = default(T);
			return false;
		}
	}
}
