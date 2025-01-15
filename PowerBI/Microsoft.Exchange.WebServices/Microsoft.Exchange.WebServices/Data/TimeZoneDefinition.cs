using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000C3 RID: 195
	internal class TimeZoneDefinition : ComplexProperty
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x0001C5A8 File Offset: 0x0001B5A8
		private int CompareTransitions(TimeZoneTransition x, TimeZoneTransition y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x.GetType() == typeof(TimeZoneTransition))
			{
				return -1;
			}
			if (y.GetType() == typeof(TimeZoneTransition))
			{
				return 1;
			}
			AbsoluteDateTransition absoluteDateTransition = (AbsoluteDateTransition)x;
			AbsoluteDateTransition absoluteDateTransition2 = (AbsoluteDateTransition)y;
			return DateTime.Compare(absoluteDateTransition.DateTime, absoluteDateTransition2.DateTime);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001C602 File Offset: 0x0001B602
		internal TimeZoneDefinition()
		{
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001C62C File Offset: 0x0001B62C
		internal TimeZoneDefinition(TimeZoneInfo timeZoneInfo)
			: this()
		{
			this.Id = timeZoneInfo.Id;
			this.Name = timeZoneInfo.DisplayName;
			TimeZonePeriod timeZonePeriod = new TimeZonePeriod();
			timeZonePeriod.Id = "Std";
			timeZonePeriod.Name = "Standard";
			timeZonePeriod.Bias = -timeZoneInfo.BaseUtcOffset;
			this.periods.Add(timeZonePeriod.Id, timeZonePeriod);
			TimeZoneInfo.AdjustmentRule[] adjustmentRules = timeZoneInfo.GetAdjustmentRules();
			TimeZoneTransition timeZoneTransition = new TimeZoneTransition(this, timeZonePeriod);
			if (adjustmentRules.Length == 0)
			{
				TimeZoneTransitionGroup timeZoneTransitionGroup = new TimeZoneTransitionGroup(this, "0");
				timeZoneTransitionGroup.Transitions.Add(timeZoneTransition);
				this.transitionGroups.Add(timeZoneTransitionGroup.Id, timeZoneTransitionGroup);
				TimeZoneTransition timeZoneTransition2 = new TimeZoneTransition(this, timeZoneTransitionGroup);
				this.transitions.Add(timeZoneTransition2);
				return;
			}
			for (int i = 0; i < adjustmentRules.Length; i++)
			{
				TimeZoneTransitionGroup timeZoneTransitionGroup2 = new TimeZoneTransitionGroup(this, this.transitionGroups.Count.ToString());
				timeZoneTransitionGroup2.InitializeFromAdjustmentRule(adjustmentRules[i], timeZonePeriod);
				this.transitionGroups.Add(timeZoneTransitionGroup2.Id, timeZoneTransitionGroup2);
				TimeZoneTransition timeZoneTransition4;
				if (i == 0)
				{
					DateTime dateStart = adjustmentRules[i].DateStart;
					DateTime minValue = DateTime.MinValue;
					if (dateStart > minValue.Date)
					{
						TimeZoneTransition timeZoneTransition3 = new TimeZoneTransition(this, this.CreateTransitionGroupToPeriod(timeZonePeriod));
						this.transitions.Add(timeZoneTransition3);
						timeZoneTransition4 = new AbsoluteDateTransition(this, timeZoneTransitionGroup2)
						{
							DateTime = adjustmentRules[i].DateStart
						};
					}
					else
					{
						timeZoneTransition4 = new TimeZoneTransition(this, timeZoneTransitionGroup2);
					}
				}
				else
				{
					timeZoneTransition4 = new AbsoluteDateTransition(this, timeZoneTransitionGroup2)
					{
						DateTime = adjustmentRules[i].DateStart
					};
				}
				this.transitions.Add(timeZoneTransition4);
			}
			DateTime dateEnd = adjustmentRules[adjustmentRules.Length - 1].DateEnd;
			DateTime dateTime = dateEnd;
			DateTime maxValue = DateTime.MaxValue;
			if (dateTime < maxValue.Date)
			{
				AbsoluteDateTransition absoluteDateTransition = new AbsoluteDateTransition(this, this.CreateTransitionGroupToPeriod(timeZonePeriod));
				absoluteDateTransition.DateTime = dateEnd.AddDays(1.0);
				this.transitions.Add(absoluteDateTransition);
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001C82C File Offset: 0x0001B82C
		private TimeZoneTransitionGroup CreateTransitionGroupToPeriod(TimeZonePeriod timeZonePeriod)
		{
			TimeZoneTransition timeZoneTransition = new TimeZoneTransition(this, timeZonePeriod);
			TimeZoneTransitionGroup timeZoneTransitionGroup = new TimeZoneTransitionGroup(this, this.transitionGroups.Count.ToString());
			timeZoneTransitionGroup.Transitions.Add(timeZoneTransition);
			this.transitionGroups.Add(timeZoneTransitionGroup.Id, timeZoneTransitionGroup);
			return timeZoneTransitionGroup;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001C87C File Offset: 0x0001B87C
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.name = reader.ReadAttributeValue("Name");
			this.id = reader.ReadAttributeValue("Id");
			if (string.IsNullOrEmpty(this.id))
			{
				string text = (string.IsNullOrEmpty(this.Name) ? string.Empty : this.Name);
				this.Id = "NoId_" + Math.Abs(text.GetHashCode()).ToString();
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001C8F6 File Offset: 0x0001B8F6
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			if (writer.Service.RequestedServerVersion != ExchangeVersion.Exchange2007_SP1)
			{
				writer.WriteAttributeValue("Name", this.name);
			}
			writer.WriteAttributeValue("Id", this.id);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001C928 File Offset: 0x0001B928
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "Periods")
				{
					do
					{
						reader.Read();
						if (reader.IsStartElement(XmlNamespace.Types, "Period"))
						{
							TimeZonePeriod timeZonePeriod = new TimeZonePeriod();
							timeZonePeriod.LoadFromXml(reader);
							this.periods.Add(timeZonePeriod.Id, timeZonePeriod);
						}
					}
					while (!reader.IsEndElement(XmlNamespace.Types, "Periods"));
					return true;
				}
				if (localName == "TransitionsGroups")
				{
					do
					{
						reader.Read();
						if (reader.IsStartElement(XmlNamespace.Types, "TransitionsGroup"))
						{
							TimeZoneTransitionGroup timeZoneTransitionGroup = new TimeZoneTransitionGroup(this);
							timeZoneTransitionGroup.LoadFromXml(reader);
							this.transitionGroups.Add(timeZoneTransitionGroup.Id, timeZoneTransitionGroup);
						}
					}
					while (!reader.IsEndElement(XmlNamespace.Types, "TransitionsGroups"));
					return true;
				}
				if (localName == "Transitions")
				{
					do
					{
						reader.Read();
						if (reader.IsStartElement())
						{
							TimeZoneTransition timeZoneTransition = TimeZoneTransition.Create(this, reader.LocalName);
							timeZoneTransition.LoadFromXml(reader);
							this.transitions.Add(timeZoneTransition);
						}
					}
					while (!reader.IsEndElement(XmlNamespace.Types, "Transitions"));
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001CA37 File Offset: 0x0001BA37
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			this.LoadFromXml(reader, "TimeZoneDefinition");
			this.transitions.Sort(new Comparison<TimeZoneTransition>(this.CompareTransitions));
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001CA5C File Offset: 0x0001BA5C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.LoadFromJson(jsonProperty, service);
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "Name"))
					{
						if (!(text2 == "Id"))
						{
							if (!(text2 == "Periods"))
							{
								if (!(text2 == "TransitionsGroups"))
								{
									if (text2 == "Transitions")
									{
										JsonObject jsonObject = jsonProperty.ReadAsJsonObject(text);
										foreach (object obj in jsonObject.ReadAsArray("Transition"))
										{
											JsonObject jsonObject2 = obj as JsonObject;
											TimeZoneTransition timeZoneTransition = TimeZoneTransition.Create(this, jsonObject2.ReadTypeString());
											timeZoneTransition.LoadFromJson(jsonObject2, service);
											this.transitions.Add(timeZoneTransition);
										}
									}
								}
								else
								{
									foreach (object obj2 in jsonProperty.ReadAsArray(text))
									{
										TimeZoneTransitionGroup timeZoneTransitionGroup = new TimeZoneTransitionGroup(this);
										timeZoneTransitionGroup.LoadFromJson(obj2 as JsonObject, service);
										this.transitionGroups.Add(timeZoneTransitionGroup.Id, timeZoneTransitionGroup);
									}
								}
							}
							else
							{
								foreach (object obj3 in jsonProperty.ReadAsArray(text))
								{
									TimeZonePeriod timeZonePeriod = new TimeZonePeriod();
									timeZonePeriod.LoadFromJson(obj3 as JsonObject, service);
									this.periods.Add(timeZonePeriod.Id, timeZonePeriod);
								}
							}
						}
						else
						{
							this.id = jsonProperty.ReadAsString(text);
						}
					}
					else
					{
						this.name = jsonProperty.ReadAsString(text);
					}
				}
			}
			if (string.IsNullOrEmpty(this.id))
			{
				string text3 = (string.IsNullOrEmpty(this.Name) ? string.Empty : this.Name);
				this.Id = "NoId_" + Math.Abs(text3.GetHashCode()).ToString();
			}
			this.transitions.Sort(new Comparison<TimeZoneTransition>(this.CompareTransitions));
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001CCA0 File Offset: 0x0001BCA0
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (writer.Service.RequestedServerVersion != ExchangeVersion.Exchange2007_SP1)
			{
				if (this.periods.Count > 0)
				{
					writer.WriteStartElement(XmlNamespace.Types, "Periods");
					foreach (KeyValuePair<string, TimeZonePeriod> keyValuePair in this.periods)
					{
						keyValuePair.Value.WriteToXml(writer);
					}
					writer.WriteEndElement();
				}
				if (this.transitionGroups.Count > 0)
				{
					writer.WriteStartElement(XmlNamespace.Types, "TransitionsGroups");
					foreach (KeyValuePair<string, TimeZoneTransitionGroup> keyValuePair2 in this.transitionGroups)
					{
						keyValuePair2.Value.WriteToXml(writer);
					}
					writer.WriteEndElement();
				}
				if (this.transitions.Count > 0)
				{
					writer.WriteStartElement(XmlNamespace.Types, "Transitions");
					foreach (TimeZoneTransition timeZoneTransition in this.transitions)
					{
						timeZoneTransition.WriteToXml(writer);
					}
					writer.WriteEndElement();
				}
			}
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001CDF8 File Offset: 0x0001BDF8
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Id", this.id);
			if (service.RequestedServerVersion != ExchangeVersion.Exchange2007_SP1)
			{
				jsonObject.Add("Name", this.name);
				if (this.periods.Count > 0)
				{
					List<object> list = new List<object>();
					foreach (KeyValuePair<string, TimeZonePeriod> keyValuePair in this.periods)
					{
						list.Add(keyValuePair.Value.InternalToJson(service));
					}
					jsonObject.Add("Periods", list.ToArray());
				}
				if (this.transitionGroups.Count > 0)
				{
					List<object> list2 = new List<object>();
					foreach (KeyValuePair<string, TimeZoneTransitionGroup> keyValuePair2 in this.transitionGroups)
					{
						list2.Add(keyValuePair2.Value.InternalToJson(service));
					}
					jsonObject.Add("TransitionsGroups", list2.ToArray());
				}
				if (this.transitions.Count > 0)
				{
					JsonObject jsonObject2 = new JsonObject();
					List<object> list3 = new List<object>();
					foreach (TimeZoneTransition timeZoneTransition in this.transitions)
					{
						list3.Add(timeZoneTransition.InternalToJson(service));
					}
					jsonObject2.Add("Transition", list3.ToArray());
					jsonObject.Add("Transitions", jsonObject2);
				}
			}
			return jsonObject;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001CFB0 File Offset: 0x0001BFB0
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			this.WriteToXml(writer, "TimeZoneDefinition");
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001CFC0 File Offset: 0x0001BFC0
		internal void Validate()
		{
			if (this.periods.Count < 1 || this.transitions.Count < 1 || this.transitionGroups.Count < 1 || this.transitionGroups.Count != this.transitions.Count)
			{
				throw new ServiceLocalException(Strings.InvalidOrUnsupportedTimeZoneDefinition);
			}
			if (this.transitions[0].GetType() != typeof(TimeZoneTransition))
			{
				throw new ServiceLocalException(Strings.InvalidOrUnsupportedTimeZoneDefinition);
			}
			foreach (TimeZoneTransition timeZoneTransition in this.transitions)
			{
				Type type = timeZoneTransition.GetType();
				if (type != typeof(TimeZoneTransition) && type != typeof(AbsoluteDateTransition))
				{
					throw new ServiceLocalException(Strings.InvalidOrUnsupportedTimeZoneDefinition);
				}
				if (timeZoneTransition.TargetGroup == null)
				{
					throw new ServiceLocalException(Strings.InvalidOrUnsupportedTimeZoneDefinition);
				}
			}
			foreach (TimeZoneTransitionGroup timeZoneTransitionGroup in this.transitionGroups.Values)
			{
				timeZoneTransitionGroup.Validate();
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001D11C File Offset: 0x0001C11C
		internal TimeZoneInfo ToTimeZoneInfo()
		{
			this.Validate();
			TimeZoneTransitionGroup.CustomTimeZoneCreateParams customTimeZoneCreationParams = this.transitions[this.transitions.Count - 1].TargetGroup.GetCustomTimeZoneCreationParams();
			List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>();
			DateTime dateTime = DateTime.MinValue;
			for (int i = 0; i < this.transitions.Count; i++)
			{
				DateTime dateTime2;
				DateTime dateTime3;
				if (i < this.transitions.Count - 1)
				{
					dateTime2 = (this.transitions[i + 1] as AbsoluteDateTransition).DateTime;
					dateTime3 = dateTime2.AddDays(-1.0);
				}
				else
				{
					dateTime2 = DateTime.MaxValue;
					dateTime3 = dateTime2;
				}
				TimeZoneInfo.AdjustmentRule adjustmentRule = this.transitions[i].TargetGroup.CreateAdjustmentRule(dateTime, dateTime3);
				if (adjustmentRule != null)
				{
					list.Add(adjustmentRule);
				}
				dateTime = dateTime2;
			}
			TimeZoneInfo timeZoneInfo;
			if (list.Count == 0)
			{
				timeZoneInfo = TimeZoneInfo.CreateCustomTimeZone(this.Id, customTimeZoneCreationParams.BaseOffsetToUtc, this.Name, customTimeZoneCreationParams.StandardDisplayName);
			}
			else
			{
				timeZoneInfo = TimeZoneInfo.CreateCustomTimeZone(this.Id, customTimeZoneCreationParams.BaseOffsetToUtc, this.Name, customTimeZoneCreationParams.StandardDisplayName, customTimeZoneCreationParams.DaylightDisplayName, list.ToArray());
			}
			return timeZoneInfo;
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0001D244 File Offset: 0x0001C244
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0001D24C File Offset: 0x0001C24C
		internal string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0001D255 File Offset: 0x0001C255
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x0001D25D File Offset: 0x0001C25D
		internal string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0001D266 File Offset: 0x0001C266
		internal Dictionary<string, TimeZonePeriod> Periods
		{
			get
			{
				return this.periods;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001D26E File Offset: 0x0001C26E
		internal Dictionary<string, TimeZoneTransitionGroup> TransitionGroups
		{
			get
			{
				return this.transitionGroups;
			}
		}

		// Token: 0x040002AE RID: 686
		private const string NoIdPrefix = "NoId_";

		// Token: 0x040002AF RID: 687
		private string name;

		// Token: 0x040002B0 RID: 688
		private string id;

		// Token: 0x040002B1 RID: 689
		private Dictionary<string, TimeZonePeriod> periods = new Dictionary<string, TimeZonePeriod>();

		// Token: 0x040002B2 RID: 690
		private Dictionary<string, TimeZoneTransitionGroup> transitionGroups = new Dictionary<string, TimeZoneTransitionGroup>();

		// Token: 0x040002B3 RID: 691
		private List<TimeZoneTransition> transitions = new List<TimeZoneTransition>();
	}
}
