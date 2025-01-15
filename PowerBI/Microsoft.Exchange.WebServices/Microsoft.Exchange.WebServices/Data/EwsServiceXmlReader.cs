using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000CE RID: 206
	internal class EwsServiceXmlReader : EwsXmlReader
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x0001E58E File Offset: 0x0001D58E
		internal EwsServiceXmlReader(Stream stream, ExchangeService service)
			: base(stream)
		{
			this.service = service;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001E59E File Offset: 0x0001D59E
		private DateTime? ConvertStringToDateTime(string dateTimeString)
		{
			return this.Service.ConvertUniversalDateTimeStringToLocalDateTime(dateTimeString);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0001E5AC File Offset: 0x0001D5AC
		private DateTime? ConvertStringToUnspecifiedDate(string dateTimeString)
		{
			return this.Service.ConvertStartDateToUnspecifiedDateTime(dateTimeString);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001E5BA File Offset: 0x0001D5BA
		public DateTime? ReadElementValueAsDateTime()
		{
			return this.ConvertStringToDateTime(base.ReadElementValue());
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001E5C8 File Offset: 0x0001D5C8
		public DateTime? ReadElementValueAsUnspecifiedDate()
		{
			return this.ConvertStringToUnspecifiedDate(base.ReadElementValue());
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001E5D8 File Offset: 0x0001D5D8
		public DateTime ReadElementValueAsUnbiasedDateTimeScopedToServiceTimeZone()
		{
			string text = base.ReadElementValue();
			return EwsUtilities.ParseAsUnbiasedDatetimescopedToServicetimeZone(text, this.Service);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0001E5F8 File Offset: 0x0001D5F8
		public DateTime? ReadElementValueAsDateTime(XmlNamespace xmlNamespace, string localName)
		{
			return this.ConvertStringToDateTime(base.ReadElementValue(xmlNamespace, localName));
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001E608 File Offset: 0x0001D608
		public List<TServiceObject> ReadServiceObjectsCollectionFromXml<TServiceObject>(XmlNamespace collectionXmlNamespace, string collectionXmlElementName, GetObjectInstanceDelegate<TServiceObject> getObjectInstanceDelegate, bool clearPropertyBag, PropertySet requestedPropertySet, bool summaryPropertiesOnly) where TServiceObject : ServiceObject
		{
			List<TServiceObject> list = new List<TServiceObject>();
			TServiceObject tserviceObject = default(TServiceObject);
			if (!base.IsStartElement(collectionXmlNamespace, collectionXmlElementName))
			{
				base.ReadStartElement(collectionXmlNamespace, collectionXmlElementName);
			}
			if (!base.IsEmptyElement)
			{
				for (;;)
				{
					base.Read();
					if (base.IsStartElement())
					{
						tserviceObject = getObjectInstanceDelegate(this.Service, base.LocalName);
						if (tserviceObject == null)
						{
							base.SkipCurrentElement();
						}
						else
						{
							if (string.Compare(base.LocalName, tserviceObject.GetXmlElementName(), 4) != 0)
							{
								break;
							}
							tserviceObject.LoadFromXml(this, clearPropertyBag, requestedPropertySet, summaryPropertiesOnly);
							list.Add(tserviceObject);
						}
					}
					if (base.IsEndElement(collectionXmlNamespace, collectionXmlElementName))
					{
						return list;
					}
				}
				throw new ServiceLocalException(string.Format("The type of the object in the store ({0}) does not match that of the local object ({1}).", base.LocalName, tserviceObject.GetXmlElementName()));
			}
			return list;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0001E6D8 File Offset: 0x0001D6D8
		public List<TServiceObject> ReadServiceObjectsCollectionFromXml<TServiceObject>(string collectionXmlElementName, GetObjectInstanceDelegate<TServiceObject> getObjectInstanceDelegate, bool clearPropertyBag, PropertySet requestedPropertySet, bool summaryPropertiesOnly) where TServiceObject : ServiceObject
		{
			return this.ReadServiceObjectsCollectionFromXml<TServiceObject>(XmlNamespace.Messages, collectionXmlElementName, getObjectInstanceDelegate, clearPropertyBag, requestedPropertySet, summaryPropertiesOnly);
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0001E6E8 File Offset: 0x0001D6E8
		public ExchangeService Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x040002C8 RID: 712
		private ExchangeService service;
	}
}
