using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000C5 RID: 197
	internal class TimeZoneTransitionGroup : ComplexProperty
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x0001D45B File Offset: 0x0001C45B
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			this.LoadFromXml(reader, "TransitionsGroup");
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001D469 File Offset: 0x0001C469
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			this.WriteToXml(writer, "TransitionsGroup");
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001D477 File Offset: 0x0001C477
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.id = reader.ReadAttributeValue("Id");
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001D48C File Offset: 0x0001C48C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.LoadFromJson(jsonProperty, service);
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "Id"))
					{
						if (text2 == "Transition")
						{
							foreach (object obj in jsonProperty.ReadAsArray(text))
							{
								JsonObject jsonObject = obj as JsonObject;
								TimeZoneTransition timeZoneTransition = TimeZoneTransition.Create(this.timeZoneDefinition, jsonObject.ReadTypeString());
								timeZoneTransition.LoadFromJson(jsonObject, service);
								this.transitions.Add(timeZoneTransition);
							}
						}
					}
					else
					{
						this.id = jsonProperty.ReadAsString(text);
					}
				}
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001D56C File Offset: 0x0001C56C
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("Id", this.id);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001D580 File Offset: 0x0001C580
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			reader.EnsureCurrentNodeIsStartElement();
			TimeZoneTransition timeZoneTransition = TimeZoneTransition.Create(this.timeZoneDefinition, reader.LocalName);
			timeZoneTransition.LoadFromXml(reader);
			EwsUtilities.Assert(timeZoneTransition.TargetPeriod != null, "TimeZoneTransitionGroup.TryReadElementFromXml", "The transition's target period is null.");
			this.transitions.Add(timeZoneTransition);
			return true;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001D5D4 File Offset: 0x0001C5D4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			foreach (TimeZoneTransition timeZoneTransition in this.transitions)
			{
				timeZoneTransition.WriteToXml(writer);
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001D628 File Offset: 0x0001C628
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Id", this.id);
			List<object> list = new List<object>();
			foreach (TimeZoneTransition timeZoneTransition in this.transitions)
			{
				list.Add(timeZoneTransition.InternalToJson(service));
			}
			jsonObject.Add("Transitions", list.ToArray());
			return jsonObject;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001D6B0 File Offset: 0x0001C6B0
		internal virtual void InitializeFromAdjustmentRule(TimeZoneInfo.AdjustmentRule adjustmentRule, TimeZonePeriod standardPeriod)
		{
			TimeZonePeriod timeZonePeriod = new TimeZonePeriod();
			timeZonePeriod.Id = string.Format("{0}/{1}", "Dlt", adjustmentRule.DateStart.Year);
			timeZonePeriod.Name = "Daylight";
			timeZonePeriod.Bias = standardPeriod.Bias - adjustmentRule.DaylightDelta;
			this.timeZoneDefinition.Periods.Add(timeZonePeriod.Id, timeZonePeriod);
			this.transitionToDaylight = TimeZoneTransition.CreateTimeZoneTransition(this.timeZoneDefinition, timeZonePeriod, adjustmentRule.DaylightTransitionStart);
			this.transitionToStandard = TimeZoneTransition.CreateTimeZoneTransition(this.timeZoneDefinition, standardPeriod, adjustmentRule.DaylightTransitionEnd);
			this.transitions.Add(this.transitionToDaylight);
			this.transitions.Add(this.transitionToStandard);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001D778 File Offset: 0x0001C778
		internal void Validate()
		{
			if (this.transitions.Count < 1 || this.transitions.Count > 2)
			{
				throw new ServiceLocalException(Strings.InvalidOrUnsupportedTimeZoneDefinition);
			}
			if (this.transitions.Count == 1 && this.transitions[0].GetType() != typeof(TimeZoneTransition))
			{
				throw new ServiceLocalException(Strings.InvalidOrUnsupportedTimeZoneDefinition);
			}
			if (this.transitions.Count == 2)
			{
				foreach (TimeZoneTransition timeZoneTransition in this.transitions)
				{
					if (timeZoneTransition.GetType() == typeof(TimeZoneTransition))
					{
						throw new ServiceLocalException(Strings.InvalidOrUnsupportedTimeZoneDefinition);
					}
				}
			}
			foreach (TimeZoneTransition timeZoneTransition2 in this.transitions)
			{
				if (timeZoneTransition2.TargetPeriod == null)
				{
					throw new ServiceLocalException(Strings.InvalidOrUnsupportedTimeZoneDefinition);
				}
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0001D8B0 File Offset: 0x0001C8B0
		internal bool SupportsDaylight
		{
			get
			{
				return this.transitions.Count == 2;
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001D8C0 File Offset: 0x0001C8C0
		private void InitializeTransitions()
		{
			if (this.transitionToStandard == null)
			{
				foreach (TimeZoneTransition timeZoneTransition in this.transitions)
				{
					if (timeZoneTransition.TargetPeriod.IsStandardPeriod || this.transitions.Count == 1)
					{
						this.transitionToStandard = timeZoneTransition;
					}
					else
					{
						this.transitionToDaylight = timeZoneTransition;
					}
				}
			}
			if (this.transitionToStandard == null)
			{
				throw new ServiceLocalException(Strings.InvalidOrUnsupportedTimeZoneDefinition);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0001D958 File Offset: 0x0001C958
		private TimeZoneTransition TransitionToDaylight
		{
			get
			{
				this.InitializeTransitions();
				return this.transitionToDaylight;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001D966 File Offset: 0x0001C966
		private TimeZoneTransition TransitionToStandard
		{
			get
			{
				this.InitializeTransitions();
				return this.transitionToStandard;
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001D974 File Offset: 0x0001C974
		internal TimeZoneTransitionGroup.CustomTimeZoneCreateParams GetCustomTimeZoneCreationParams()
		{
			TimeZoneTransitionGroup.CustomTimeZoneCreateParams customTimeZoneCreateParams = new TimeZoneTransitionGroup.CustomTimeZoneCreateParams();
			if (this.TransitionToDaylight != null)
			{
				customTimeZoneCreateParams.DaylightDisplayName = this.TransitionToDaylight.TargetPeriod.Name;
			}
			customTimeZoneCreateParams.StandardDisplayName = this.TransitionToStandard.TargetPeriod.Name;
			customTimeZoneCreateParams.BaseOffsetToUtc = -this.TransitionToStandard.TargetPeriod.Bias;
			return customTimeZoneCreateParams;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001D9D7 File Offset: 0x0001C9D7
		internal TimeSpan GetDaylightDelta()
		{
			if (this.SupportsDaylight)
			{
				return this.TransitionToStandard.TargetPeriod.Bias - this.TransitionToDaylight.TargetPeriod.Bias;
			}
			return TimeSpan.Zero;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001DA0C File Offset: 0x0001CA0C
		internal TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime startDate, DateTime endDate)
		{
			if (this.transitions.Count == 1)
			{
				return null;
			}
			return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate.Date, endDate.Date, this.GetDaylightDelta(), this.TransitionToDaylight.CreateTransitionTime(), this.TransitionToStandard.CreateTransitionTime());
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001DA58 File Offset: 0x0001CA58
		internal TimeZoneTransitionGroup(TimeZoneDefinition timeZoneDefinition)
		{
			this.timeZoneDefinition = timeZoneDefinition;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001DA72 File Offset: 0x0001CA72
		internal TimeZoneTransitionGroup(TimeZoneDefinition timeZoneDefinition, string id)
			: this(timeZoneDefinition)
		{
			this.id = id;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0001DA82 File Offset: 0x0001CA82
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x0001DA8A File Offset: 0x0001CA8A
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

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001DA93 File Offset: 0x0001CA93
		internal List<TimeZoneTransition> Transitions
		{
			get
			{
				return this.transitions;
			}
		}

		// Token: 0x040002BB RID: 699
		private TimeZoneDefinition timeZoneDefinition;

		// Token: 0x040002BC RID: 700
		private string id;

		// Token: 0x040002BD RID: 701
		private List<TimeZoneTransition> transitions = new List<TimeZoneTransition>();

		// Token: 0x040002BE RID: 702
		private TimeZoneTransition transitionToStandard;

		// Token: 0x040002BF RID: 703
		private TimeZoneTransition transitionToDaylight;

		// Token: 0x020000C6 RID: 198
		internal class CustomTimeZoneCreateParams
		{
			// Token: 0x060008BE RID: 2238 RVA: 0x0001DA9B File Offset: 0x0001CA9B
			internal CustomTimeZoneCreateParams()
			{
			}

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x060008BF RID: 2239 RVA: 0x0001DAA3 File Offset: 0x0001CAA3
			// (set) Token: 0x060008C0 RID: 2240 RVA: 0x0001DAAB File Offset: 0x0001CAAB
			internal TimeSpan BaseOffsetToUtc
			{
				get
				{
					return this.baseOffsetToUtc;
				}
				set
				{
					this.baseOffsetToUtc = value;
				}
			}

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001DAB4 File Offset: 0x0001CAB4
			// (set) Token: 0x060008C2 RID: 2242 RVA: 0x0001DABC File Offset: 0x0001CABC
			internal string StandardDisplayName
			{
				get
				{
					return this.standardDisplayName;
				}
				set
				{
					this.standardDisplayName = value;
				}
			}

			// Token: 0x17000220 RID: 544
			// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0001DAC5 File Offset: 0x0001CAC5
			// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0001DACD File Offset: 0x0001CACD
			internal string DaylightDisplayName
			{
				get
				{
					return this.daylightDisplayName;
				}
				set
				{
					this.daylightDisplayName = value;
				}
			}

			// Token: 0x17000221 RID: 545
			// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0001DAD6 File Offset: 0x0001CAD6
			internal bool HasDaylightPeriod
			{
				get
				{
					return !string.IsNullOrEmpty(this.daylightDisplayName);
				}
			}

			// Token: 0x040002C0 RID: 704
			private TimeSpan baseOffsetToUtc;

			// Token: 0x040002C1 RID: 705
			private string standardDisplayName;

			// Token: 0x040002C2 RID: 706
			private string daylightDisplayName;
		}
	}
}
