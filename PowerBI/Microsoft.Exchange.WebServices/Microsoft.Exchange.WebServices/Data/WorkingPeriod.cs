using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000AC RID: 172
	internal sealed class WorkingPeriod : ComplexProperty
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x0001A454 File Offset: 0x00019454
		internal WorkingPeriod()
		{
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001A468 File Offset: 0x00019468
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "DayOfWeek")
				{
					EwsUtilities.ParseEnumValueList<DayOfTheWeek>(this.daysOfWeek, reader.ReadElementValue(), new char[] { ' ' });
					return true;
				}
				if (localName == "StartTimeInMinutes")
				{
					this.startTime = TimeSpan.FromMinutes((double)reader.ReadElementValue<int>());
					return true;
				}
				if (localName == "EndTimeInMinutes")
				{
					this.endTime = TimeSpan.FromMinutes((double)reader.ReadElementValue<int>());
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001A4F4 File Offset: 0x000194F4
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "DayOfWeek"))
					{
						if (!(text2 == "StartTimeInMinutes"))
						{
							if (text2 == "EndTimeInMinutes")
							{
								this.endTime = TimeSpan.FromMinutes((double)jsonProperty.ReadAsInt(text));
							}
						}
						else
						{
							this.startTime = TimeSpan.FromMinutes((double)jsonProperty.ReadAsInt(text));
						}
					}
					else
					{
						EwsUtilities.ParseEnumValueList<DayOfTheWeek>(this.daysOfWeek, jsonProperty.ReadAsString(text), new char[] { ' ' });
					}
				}
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001A5BC File Offset: 0x000195BC
		internal Collection<DayOfTheWeek> DaysOfWeek
		{
			get
			{
				return this.daysOfWeek;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0001A5C4 File Offset: 0x000195C4
		internal TimeSpan StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0001A5CC File Offset: 0x000195CC
		internal TimeSpan EndTime
		{
			get
			{
				return this.endTime;
			}
		}

		// Token: 0x0400028D RID: 653
		private Collection<DayOfTheWeek> daysOfWeek = new Collection<DayOfTheWeek>();

		// Token: 0x0400028E RID: 654
		private TimeSpan startTime;

		// Token: 0x0400028F RID: 655
		private TimeSpan endTime;
	}
}
