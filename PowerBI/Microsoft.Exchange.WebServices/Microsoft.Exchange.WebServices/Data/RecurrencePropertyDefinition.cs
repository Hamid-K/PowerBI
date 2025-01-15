using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002D8 RID: 728
	internal sealed class RecurrencePropertyDefinition : PropertyDefinition
	{
		// Token: 0x060019CD RID: 6605 RVA: 0x00046034 File Offset: 0x00045034
		internal RecurrencePropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00046044 File Offset: 0x00045044
		internal override void LoadPropertyValueFromXml(EwsServiceXmlReader reader, PropertyBag propertyBag)
		{
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, "Recurrence");
			reader.Read(1);
			Recurrence recurrenceFromString = RecurrencePropertyDefinition.GetRecurrenceFromString(reader.LocalName);
			recurrenceFromString.LoadFromXml(reader, reader.LocalName);
			reader.Read(1);
			RecurrenceRange recurrenceRange = RecurrencePropertyDefinition.GetRecurrenceRange(reader.LocalName);
			recurrenceRange.LoadFromXml(reader, reader.LocalName);
			recurrenceRange.SetupRecurrence(recurrenceFromString);
			reader.ReadEndElementIfNecessary(XmlNamespace.Types, "Recurrence");
			propertyBag[this] = recurrenceFromString;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x000460BC File Offset: 0x000450BC
		internal override void LoadPropertyValueFromJson(object value, ExchangeService service, PropertyBag propertyBag)
		{
			JsonObject jsonObject = value as JsonObject;
			JsonObject jsonObject2 = jsonObject.ReadAsJsonObject("RecurrencePattern");
			Recurrence recurrenceFromString = RecurrencePropertyDefinition.GetRecurrenceFromString(jsonObject2.ReadTypeString());
			recurrenceFromString.LoadFromJson(jsonObject2, service);
			JsonObject jsonObject3 = jsonObject.ReadAsJsonObject("RecurrenceRange");
			RecurrenceRange recurrenceRange = RecurrencePropertyDefinition.GetRecurrenceRange(jsonObject3.ReadTypeString());
			recurrenceRange.LoadFromJson(jsonObject3, service);
			recurrenceRange.SetupRecurrence(recurrenceFromString);
			propertyBag[this] = recurrenceFromString;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00046124 File Offset: 0x00045124
		private static RecurrenceRange GetRecurrenceRange(string recurrenceRangeString)
		{
			if (recurrenceRangeString != null)
			{
				RecurrenceRange recurrenceRange;
				if (!(recurrenceRangeString == "NoEndRecurrence"))
				{
					if (!(recurrenceRangeString == "EndDateRecurrence"))
					{
						if (!(recurrenceRangeString == "NumberedRecurrence"))
						{
							goto IL_0046;
						}
						recurrenceRange = new NumberedRecurrenceRange();
					}
					else
					{
						recurrenceRange = new EndDateRecurrenceRange();
					}
				}
				else
				{
					recurrenceRange = new NoEndRecurrenceRange();
				}
				return recurrenceRange;
			}
			IL_0046:
			throw new ServiceXmlDeserializationException(string.Format(Strings.InvalidRecurrenceRange, recurrenceRangeString));
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00046190 File Offset: 0x00045190
		private static Recurrence GetRecurrenceFromString(string recurranceString)
		{
			if (recurranceString != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60018b5-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(10);
					dictionary.Add("RelativeYearlyRecurrence", 0);
					dictionary.Add("AbsoluteYearlyRecurrence", 1);
					dictionary.Add("RelativeMonthlyRecurrence", 2);
					dictionary.Add("AbsoluteMonthlyRecurrence", 3);
					dictionary.Add("DailyRecurrence", 4);
					dictionary.Add("DailyRegeneration", 5);
					dictionary.Add("WeeklyRecurrence", 6);
					dictionary.Add("WeeklyRegeneration", 7);
					dictionary.Add("MonthlyRegeneration", 8);
					dictionary.Add("YearlyRegeneration", 9);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60018b5-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60018b5-1.TryGetValue(recurranceString, ref num))
				{
					Recurrence recurrence;
					switch (num)
					{
					case 0:
						recurrence = new Recurrence.RelativeYearlyPattern();
						break;
					case 1:
						recurrence = new Recurrence.YearlyPattern();
						break;
					case 2:
						recurrence = new Recurrence.RelativeMonthlyPattern();
						break;
					case 3:
						recurrence = new Recurrence.MonthlyPattern();
						break;
					case 4:
						recurrence = new Recurrence.DailyPattern();
						break;
					case 5:
						recurrence = new Recurrence.DailyRegenerationPattern();
						break;
					case 6:
						recurrence = new Recurrence.WeeklyPattern();
						break;
					case 7:
						recurrence = new Recurrence.WeeklyRegenerationPattern();
						break;
					case 8:
						recurrence = new Recurrence.MonthlyRegenerationPattern();
						break;
					case 9:
						recurrence = new Recurrence.YearlyRegenerationPattern();
						break;
					default:
						goto IL_0131;
					}
					return recurrence;
				}
			}
			IL_0131:
			throw new ServiceXmlDeserializationException(string.Format(Strings.InvalidRecurrencePattern, recurranceString));
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000462E8 File Offset: 0x000452E8
		internal override void WritePropertyValueToXml(EwsServiceXmlWriter writer, PropertyBag propertyBag, bool isUpdateOperation)
		{
			Recurrence recurrence = (Recurrence)propertyBag[this];
			if (recurrence != null)
			{
				recurrence.WriteToXml(writer, "Recurrence");
			}
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00046314 File Offset: 0x00045314
		internal override void WriteJsonValue(JsonObject jsonObject, PropertyBag propertyBag, ExchangeService service, bool isUpdateOperation)
		{
			Recurrence recurrence = propertyBag[this] as Recurrence;
			if (recurrence != null)
			{
				jsonObject.Add(base.XmlElementName, recurrence.InternalToJson(service));
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x00046344 File Offset: 0x00045344
		public override Type Type
		{
			get
			{
				return typeof(Recurrence);
			}
		}
	}
}
