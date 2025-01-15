using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000BE RID: 190
	internal class TimeZoneTransition : ComplexProperty
	{
		// Token: 0x06000856 RID: 2134 RVA: 0x0001BDAC File Offset: 0x0001ADAC
		internal static TimeZoneTransition Create(TimeZoneDefinition timeZoneDefinition, string xmlElementName)
		{
			if (xmlElementName != null)
			{
				if (xmlElementName == "AbsoluteDateTransition")
				{
					return new AbsoluteDateTransition(timeZoneDefinition);
				}
				if (xmlElementName == "RecurringDayTransition")
				{
					return new RelativeDayOfMonthTransition(timeZoneDefinition);
				}
				if (xmlElementName == "RecurringDateTransition")
				{
					return new AbsoluteDayOfMonthTransition(timeZoneDefinition);
				}
				if (xmlElementName == "Transition")
				{
					return new TimeZoneTransition(timeZoneDefinition);
				}
			}
			throw new ServiceLocalException(string.Format(Strings.UnknownTimeZonePeriodTransitionType, xmlElementName));
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001BE28 File Offset: 0x0001AE28
		internal static TimeZoneTransition CreateTimeZoneTransition(TimeZoneDefinition timeZoneDefinition, TimeZonePeriod targetPeriod, TimeZoneInfo.TransitionTime transitionTime)
		{
			TimeZoneTransition timeZoneTransition;
			if (transitionTime.IsFixedDateRule)
			{
				timeZoneTransition = new AbsoluteDayOfMonthTransition(timeZoneDefinition, targetPeriod);
			}
			else
			{
				timeZoneTransition = new RelativeDayOfMonthTransition(timeZoneDefinition, targetPeriod);
			}
			timeZoneTransition.InitializeFromTransitionTime(transitionTime);
			return timeZoneTransition;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001BE58 File Offset: 0x0001AE58
		internal virtual string GetXmlElementName()
		{
			return "Transition";
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001BE5F File Offset: 0x0001AE5F
		internal virtual TimeZoneInfo.TransitionTime CreateTransitionTime()
		{
			throw new ServiceLocalException(Strings.InvalidOrUnsupportedTimeZoneDefinition);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001BE70 File Offset: 0x0001AE70
		internal virtual void InitializeFromTransitionTime(TimeZoneInfo.TransitionTime transitionTime)
		{
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001BE74 File Offset: 0x0001AE74
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null && localName == "To")
			{
				string text = reader.ReadAttributeValue("Kind");
				string text2 = reader.ReadElementValue();
				string text3;
				if ((text3 = text) != null)
				{
					if (!(text3 == "Period"))
					{
						if (!(text3 == "Group"))
						{
							goto IL_00AF;
						}
						if (!this.timeZoneDefinition.TransitionGroups.TryGetValue(text2, ref this.targetGroup))
						{
							throw new ServiceLocalException(string.Format(Strings.TransitionGroupNotFound, text2));
						}
					}
					else if (!this.timeZoneDefinition.Periods.TryGetValue(text2, ref this.targetPeriod))
					{
						throw new ServiceLocalException(string.Format(Strings.PeriodNotFound, text2));
					}
					return true;
				}
				IL_00AF:
				throw new ServiceLocalException(Strings.UnsupportedTimeZonePeriodTransitionTarget);
			}
			return false;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001BF44 File Offset: 0x0001AF44
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.LoadFromJson(jsonProperty, service);
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null && text2 == "To")
				{
					string text3 = jsonProperty.ReadAsJsonObject(text).ReadAsString("Kind");
					string text4 = jsonProperty.ReadAsJsonObject(text).ReadAsString("Value");
					string text5;
					if ((text5 = text3) != null)
					{
						if (!(text5 == "Period"))
						{
							if (text5 == "Group")
							{
								if (!this.timeZoneDefinition.TransitionGroups.TryGetValue(text4, ref this.targetGroup))
								{
									throw new ServiceLocalException(string.Format(Strings.TransitionGroupNotFound, text4));
								}
								continue;
							}
						}
						else
						{
							if (!this.timeZoneDefinition.Periods.TryGetValue(text4, ref this.targetPeriod))
							{
								throw new ServiceLocalException(string.Format(Strings.PeriodNotFound, text4));
							}
							continue;
						}
					}
					throw new ServiceLocalException(Strings.UnsupportedTimeZonePeriodTransitionTarget);
				}
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001C070 File Offset: 0x0001B070
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			JsonObject jsonObject2 = new JsonObject();
			jsonObject.Add("To", jsonObject2);
			if (this.targetPeriod != null)
			{
				jsonObject2.Add("Kind", "Period");
				jsonObject2.Add("Value", this.targetPeriod.Id);
			}
			else
			{
				jsonObject2.Add("Kind", "Group");
				jsonObject2.Add("Value", this.targetGroup.Id);
			}
			return jsonObject;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001C0EC File Offset: 0x0001B0EC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, "To");
			if (this.targetPeriod != null)
			{
				writer.WriteAttributeValue("Kind", "Period");
				writer.WriteValue(this.targetPeriod.Id, "To");
			}
			else
			{
				writer.WriteAttributeValue("Kind", "Group");
				writer.WriteValue(this.targetGroup.Id, "To");
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001C161 File Offset: 0x0001B161
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			this.LoadFromXml(reader, this.GetXmlElementName());
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001C170 File Offset: 0x0001B170
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			this.WriteToXml(writer, this.GetXmlElementName());
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001C17F File Offset: 0x0001B17F
		internal TimeZoneTransition(TimeZoneDefinition timeZoneDefinition)
		{
			this.timeZoneDefinition = timeZoneDefinition;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001C18E File Offset: 0x0001B18E
		internal TimeZoneTransition(TimeZoneDefinition timeZoneDefinition, TimeZoneTransitionGroup targetGroup)
			: this(timeZoneDefinition)
		{
			this.targetGroup = targetGroup;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001C19E File Offset: 0x0001B19E
		internal TimeZoneTransition(TimeZoneDefinition timeZoneDefinition, TimeZonePeriod targetPeriod)
			: this(timeZoneDefinition)
		{
			this.targetPeriod = targetPeriod;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0001C1AE File Offset: 0x0001B1AE
		internal TimeZonePeriod TargetPeriod
		{
			get
			{
				return this.targetPeriod;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001C1B6 File Offset: 0x0001B1B6
		internal TimeZoneTransitionGroup TargetGroup
		{
			get
			{
				return this.targetGroup;
			}
		}

		// Token: 0x040002A3 RID: 675
		private const string PeriodTarget = "Period";

		// Token: 0x040002A4 RID: 676
		private const string GroupTarget = "Group";

		// Token: 0x040002A5 RID: 677
		private TimeZoneDefinition timeZoneDefinition;

		// Token: 0x040002A6 RID: 678
		private TimeZonePeriod targetPeriod;

		// Token: 0x040002A7 RID: 679
		private TimeZoneTransitionGroup targetGroup;
	}
}
