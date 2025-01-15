using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000AB RID: 171
	public sealed class WorkingHours : ComplexProperty
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x0001A0F4 File Offset: 0x000190F4
		internal WorkingHours()
		{
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001A108 File Offset: 0x00019108
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "TimeZone")
				{
					LegacyAvailabilityTimeZone legacyAvailabilityTimeZone = new LegacyAvailabilityTimeZone();
					legacyAvailabilityTimeZone.LoadFromXml(reader, reader.LocalName);
					this.timeZone = legacyAvailabilityTimeZone.ToTimeZoneInfo();
					return true;
				}
				if (localName == "WorkingPeriodArray")
				{
					List<WorkingPeriod> list = new List<WorkingPeriod>();
					do
					{
						reader.Read();
						if (reader.IsStartElement(XmlNamespace.Types, "WorkingPeriod"))
						{
							WorkingPeriod workingPeriod = new WorkingPeriod();
							workingPeriod.LoadFromXml(reader, reader.LocalName);
							list.Add(workingPeriod);
						}
					}
					while (!reader.IsEndElement(XmlNamespace.Types, "WorkingPeriodArray"));
					this.startTime = list[0].StartTime;
					this.endTime = list[0].EndTime;
					foreach (WorkingPeriod workingPeriod2 in list)
					{
						foreach (DayOfTheWeek dayOfTheWeek in list[0].DaysOfWeek)
						{
							if (!this.daysOfTheWeek.Contains(dayOfTheWeek))
							{
								this.daysOfTheWeek.Add(dayOfTheWeek);
							}
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001A264 File Offset: 0x00019264
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "TimeZone"))
					{
						if (text2 == "WorkingPeriodArray")
						{
							List<WorkingPeriod> list = new List<WorkingPeriod>();
							object[] array = jsonProperty.ReadAsArray(text);
							foreach (object obj in array)
							{
								JsonObject jsonObject = obj as JsonObject;
								if (jsonObject != null)
								{
									WorkingPeriod workingPeriod = new WorkingPeriod();
									workingPeriod.LoadFromJson(jsonObject, service);
									list.Add(workingPeriod);
								}
							}
							this.startTime = list[0].StartTime;
							this.endTime = list[0].EndTime;
							foreach (WorkingPeriod workingPeriod2 in list)
							{
								foreach (DayOfTheWeek dayOfTheWeek in list[0].DaysOfWeek)
								{
									if (!this.daysOfTheWeek.Contains(dayOfTheWeek))
									{
										this.daysOfTheWeek.Add(dayOfTheWeek);
									}
								}
							}
						}
					}
					else
					{
						LegacyAvailabilityTimeZone legacyAvailabilityTimeZone = new LegacyAvailabilityTimeZone();
						legacyAvailabilityTimeZone.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
						this.timeZone = legacyAvailabilityTimeZone.ToTimeZoneInfo();
					}
				}
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0001A434 File Offset: 0x00019434
		public TimeZoneInfo TimeZone
		{
			get
			{
				return this.timeZone;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001A43C File Offset: 0x0001943C
		public Collection<DayOfTheWeek> DaysOfTheWeek
		{
			get
			{
				return this.daysOfTheWeek;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001A444 File Offset: 0x00019444
		public TimeSpan StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0001A44C File Offset: 0x0001944C
		public TimeSpan EndTime
		{
			get
			{
				return this.endTime;
			}
		}

		// Token: 0x04000289 RID: 649
		private TimeZoneInfo timeZone;

		// Token: 0x0400028A RID: 650
		private Collection<DayOfTheWeek> daysOfTheWeek = new Collection<DayOfTheWeek>();

		// Token: 0x0400028B RID: 651
		private TimeSpan startTime;

		// Token: 0x0400028C RID: 652
		private TimeSpan endTime;
	}
}
