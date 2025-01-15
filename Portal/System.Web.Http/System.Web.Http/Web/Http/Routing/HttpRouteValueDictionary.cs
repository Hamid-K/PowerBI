using System;
using System.Collections.Generic;
using System.Web.Http.Internal;

namespace System.Web.Http.Routing
{
	// Token: 0x02000163 RID: 355
	public class HttpRouteValueDictionary : Dictionary<string, object>
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x00003EA5 File Offset: 0x000020A5
		public HttpRouteValueDictionary()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00018B5C File Offset: 0x00016D5C
		public HttpRouteValueDictionary(IDictionary<string, object> dictionary)
			: base(StringComparer.OrdinalIgnoreCase)
		{
			if (dictionary != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					base.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00018BC0 File Offset: 0x00016DC0
		public HttpRouteValueDictionary(object values)
			: base(StringComparer.OrdinalIgnoreCase)
		{
			IDictionary<string, object> dictionary = values as IDictionary<string, object>;
			if (dictionary != null)
			{
				using (IEnumerator<KeyValuePair<string, object>> enumerator = dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, object> keyValuePair = enumerator.Current;
						base.Add(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			if (values != null)
			{
				foreach (PropertyHelper propertyHelper in PropertyHelper.GetProperties(values))
				{
					base.Add(propertyHelper.Name, propertyHelper.GetValue(values));
				}
			}
		}
	}
}
