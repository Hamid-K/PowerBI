using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002DB RID: 731
	internal class ScopedDateTimePropertyDefinition : DateTimePropertyDefinition
	{
		// Token: 0x060019E1 RID: 6625 RVA: 0x00046570 File Offset: 0x00045570
		private PropertyDefinition GetTimeZoneProperty(ExchangeVersion version)
		{
			PropertyDefinition propertyDefinition = this.getPropertyDefinitionCallback(version);
			EwsUtilities.Assert(propertyDefinition != null, "ScopedDateTimePropertyDefinition.GetTimeZoneProperty", "timeZoneProperty is null.");
			return propertyDefinition;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000465A1 File Offset: 0x000455A1
		internal ScopedDateTimePropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version, GetPropertyDefinitionCallback getPropertyDefinitionCallback)
			: base(xmlElementName, uri, flags, version)
		{
			EwsUtilities.Assert(getPropertyDefinitionCallback != null, "ScopedDateTimePropertyDefinition.ctor", "getPropertyDefinitionCallback is null.");
			this.getPropertyDefinitionCallback = getPropertyDefinitionCallback;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000465D0 File Offset: 0x000455D0
		internal override DateTime ScopeToTimeZone(ExchangeServiceBase service, DateTime dateTime, PropertyBag propertyBag, bool isUpdateOperation)
		{
			if (!propertyBag.Owner.GetIsCustomDateTimeScopingRequired())
			{
				return base.ScopeToTimeZone(service, dateTime, propertyBag, isUpdateOperation);
			}
			PropertyDefinition timeZoneProperty = this.GetTimeZoneProperty(service.RequestedServerVersion);
			object obj = null;
			propertyBag.TryGetProperty(timeZoneProperty, out obj);
			if (obj != null && propertyBag.IsPropertyUpdated(timeZoneProperty))
			{
				try
				{
					return new DateTime(EwsUtilities.ConvertTime(dateTime, (TimeZoneInfo)obj, TimeZoneInfo.Utc).Ticks, 1);
				}
				catch (TimeZoneConversionException ex)
				{
					throw new PropertyException(string.Format(Strings.InvalidDateTime, dateTime), base.Name, ex);
				}
			}
			if (!isUpdateOperation)
			{
				return base.ScopeToTimeZone(service, dateTime, propertyBag, isUpdateOperation);
			}
			if (service.RequestedServerVersion == ExchangeVersion.Exchange2007_SP1)
			{
				return base.ScopeToTimeZone(service, dateTime, propertyBag, isUpdateOperation);
			}
			return dateTime;
		}

		// Token: 0x04001408 RID: 5128
		private GetPropertyDefinitionCallback getPropertyDefinitionCallback;
	}
}
