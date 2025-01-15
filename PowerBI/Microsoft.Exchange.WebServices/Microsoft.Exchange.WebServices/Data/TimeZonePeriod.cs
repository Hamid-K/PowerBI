using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000C4 RID: 196
	internal class TimeZonePeriod : ComplexProperty
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x0001D276 File Offset: 0x0001C276
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.id = reader.ReadAttributeValue("Id");
			this.name = reader.ReadAttributeValue("Name");
			this.bias = EwsUtilities.XSDurationToTimeSpan(reader.ReadAttributeValue("Bias"));
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001D2B0 File Offset: 0x0001C2B0
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("Bias", EwsUtilities.TimeSpanToXSDuration(this.bias));
			writer.WriteAttributeValue("Name", this.name);
			writer.WriteAttributeValue("Id", this.id);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001D2EC File Offset: 0x0001C2EC
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Bias", EwsUtilities.TimeSpanToXSDuration(this.bias));
			jsonObject.Add("Name", this.name);
			jsonObject.Add("Id", this.id);
			return jsonObject;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001D338 File Offset: 0x0001C338
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			this.LoadFromXml(reader, "Period");
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001D348 File Offset: 0x0001C348
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
						if (!(text2 == "Name"))
						{
							if (text2 == "Bias")
							{
								this.bias = EwsUtilities.XSDurationToTimeSpan(jsonProperty.ReadAsString(text));
							}
						}
						else
						{
							this.name = jsonProperty.ReadAsString(text);
						}
					}
					else
					{
						this.id = jsonProperty.ReadAsString(text);
					}
				}
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001D3FC File Offset: 0x0001C3FC
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			this.WriteToXml(writer, "Period");
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001D40A File Offset: 0x0001C40A
		internal TimeZonePeriod()
		{
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001D412 File Offset: 0x0001C412
		internal bool IsStandardPeriod
		{
			get
			{
				return string.Compare(this.name, "Standard", 5) == 0;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0001D428 File Offset: 0x0001C428
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x0001D430 File Offset: 0x0001C430
		internal TimeSpan Bias
		{
			get
			{
				return this.bias;
			}
			set
			{
				this.bias = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0001D439 File Offset: 0x0001C439
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x0001D441 File Offset: 0x0001C441
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

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0001D44A File Offset: 0x0001C44A
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x0001D452 File Offset: 0x0001C452
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

		// Token: 0x040002B4 RID: 692
		internal const string StandardPeriodId = "Std";

		// Token: 0x040002B5 RID: 693
		internal const string StandardPeriodName = "Standard";

		// Token: 0x040002B6 RID: 694
		internal const string DaylightPeriodId = "Dlt";

		// Token: 0x040002B7 RID: 695
		internal const string DaylightPeriodName = "Daylight";

		// Token: 0x040002B8 RID: 696
		private TimeSpan bias;

		// Token: 0x040002B9 RID: 697
		private string name;

		// Token: 0x040002BA RID: 698
		private string id;
	}
}
