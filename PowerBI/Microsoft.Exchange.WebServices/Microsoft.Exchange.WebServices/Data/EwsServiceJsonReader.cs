using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000D0 RID: 208
	internal class EwsServiceJsonReader
	{
		// Token: 0x0600096A RID: 2410 RVA: 0x0001E762 File Offset: 0x0001D762
		internal EwsServiceJsonReader(ExchangeService service)
		{
			this.Service = service;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001E774 File Offset: 0x0001D774
		internal List<TServiceObject> ReadServiceObjectsCollectionFromJson<TServiceObject>(JsonObject jsonResponse, string collectionJsonElementName, GetObjectInstanceDelegate<TServiceObject> getObjectInstanceDelegate, bool clearPropertyBag, PropertySet requestedPropertySet, bool summaryPropertiesOnly) where TServiceObject : ServiceObject
		{
			List<TServiceObject> list = new List<TServiceObject>();
			TServiceObject tserviceObject = default(TServiceObject);
			object[] array = jsonResponse.ReadAsArray(collectionJsonElementName);
			foreach (object obj in array)
			{
				JsonObject jsonObject = obj as JsonObject;
				if (jsonObject != null)
				{
					tserviceObject = getObjectInstanceDelegate(this.Service, jsonObject.ReadTypeString());
					if (tserviceObject != null)
					{
						if (string.Compare(jsonObject.ReadTypeString(), tserviceObject.GetXmlElementName(), 4) != 0)
						{
							throw new ServiceLocalException(string.Format("The type of the object in the store ({0}) does not match that of the local object ({1}).", jsonObject.ReadTypeString(), tserviceObject.GetXmlElementName()));
						}
						tserviceObject.LoadFromJson(jsonObject, this.Service, clearPropertyBag, requestedPropertySet, summaryPropertiesOnly);
						list.Add(tserviceObject);
					}
				}
			}
			return list;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0001E844 File Offset: 0x0001D844
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0001E84C File Offset: 0x0001D84C
		internal ExchangeService Service { get; set; }
	}
}
