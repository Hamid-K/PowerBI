using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002CE RID: 718
	internal class DateTimePropertyDefinition : PropertyDefinition
	{
		// Token: 0x0600197A RID: 6522 RVA: 0x00045059 File Offset: 0x00044059
		internal DateTimePropertyDefinition(string xmlElementName, string uri, ExchangeVersion version)
			: base(xmlElementName, uri, version)
		{
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00045064 File Offset: 0x00044064
		internal DateTimePropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00045071 File Offset: 0x00044071
		internal DateTimePropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version, bool isNullable)
			: base(xmlElementName, uri, flags, version)
		{
			this.isNullable = isNullable;
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00045088 File Offset: 0x00044088
		internal override void LoadPropertyValueFromXml(EwsServiceXmlReader reader, PropertyBag propertyBag)
		{
			string text = reader.ReadElementValue(XmlNamespace.Types, base.XmlElementName);
			propertyBag[this] = reader.Service.ConvertUniversalDateTimeStringToLocalDateTime(text);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000450BC File Offset: 0x000440BC
		internal override void LoadPropertyValueFromJson(object value, ExchangeService service, PropertyBag propertyBag)
		{
			string text = value as string;
			if (!string.IsNullOrEmpty(text))
			{
				propertyBag[this] = service.ConvertUniversalDateTimeStringToLocalDateTime(text);
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x000450EC File Offset: 0x000440EC
		internal virtual DateTime ScopeToTimeZone(ExchangeServiceBase service, DateTime dateTime, PropertyBag propertyBag, bool isUpdateOperation)
		{
			DateTime dateTime2;
			try
			{
				dateTime2 = new DateTime(EwsUtilities.ConvertTime(dateTime, service.TimeZone, TimeZoneInfo.Utc).Ticks, 1);
			}
			catch (TimeZoneConversionException ex)
			{
				throw new PropertyException(string.Format(Strings.InvalidDateTime, dateTime), base.Name, ex);
			}
			return dateTime2;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00045150 File Offset: 0x00044150
		internal override void WritePropertyValueToXml(EwsServiceXmlWriter writer, PropertyBag propertyBag, bool isUpdateOperation)
		{
			object obj = propertyBag[this];
			if (obj != null)
			{
				writer.WriteStartElement(XmlNamespace.Types, base.XmlElementName);
				DateTime convertedDateTime = this.GetConvertedDateTime(writer.Service, propertyBag, isUpdateOperation, obj);
				writer.WriteValue(EwsUtilities.DateTimeToXSDateTime(convertedDateTime), base.Name);
				writer.WriteEndElement();
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000451A0 File Offset: 0x000441A0
		internal override void WriteJsonValue(JsonObject jsonObject, PropertyBag propertyBag, ExchangeService service, bool isUpdateOperation)
		{
			object obj = propertyBag[this];
			if (obj != null)
			{
				DateTime convertedDateTime = this.GetConvertedDateTime(service, propertyBag, isUpdateOperation, obj);
				jsonObject.Add(base.XmlElementName, EwsUtilities.DateTimeToXSDateTime(convertedDateTime));
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000451D8 File Offset: 0x000441D8
		private DateTime GetConvertedDateTime(ExchangeServiceBase service, PropertyBag propertyBag, bool isUpdateOperation, object value)
		{
			DateTime dateTime = (DateTime)value;
			DateTime dateTime2;
			if (dateTime.Kind == null)
			{
				dateTime2 = this.ScopeToTimeZone(service, (DateTime)value, propertyBag, isUpdateOperation);
			}
			else
			{
				dateTime2 = dateTime;
			}
			return dateTime2;
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x0004520C File Offset: 0x0004420C
		internal override bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x00045214 File Offset: 0x00044214
		public override Type Type
		{
			get
			{
				if (!this.IsNullable)
				{
					return typeof(DateTime);
				}
				return typeof(DateTime?);
			}
		}

		// Token: 0x040013F7 RID: 5111
		private bool isNullable;
	}
}
