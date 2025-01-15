using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002A2 RID: 674
	public sealed class NameResolutionCollection : IEnumerable<NameResolution>, IEnumerable
	{
		// Token: 0x060017C7 RID: 6087 RVA: 0x00040E84 File Offset: 0x0003FE84
		internal NameResolutionCollection(ExchangeService service)
		{
			EwsUtilities.Assert(service != null, "NameResolutionSet.ctor", "service is null.");
			this.service = service;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00040EB4 File Offset: 0x0003FEB4
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Messages, "ResolutionSet");
			int num = reader.ReadAttributeValue<int>("TotalItemsInView");
			this.includesAllResolutions = reader.ReadAttributeValue<bool>("IncludesLastItemInRange");
			for (int i = 0; i < num; i++)
			{
				NameResolution nameResolution = new NameResolution(this);
				nameResolution.LoadFromXml(reader);
				this.items.Add(nameResolution);
			}
			reader.ReadEndElement(XmlNamespace.Messages, "ResolutionSet");
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00040F1C File Offset: 0x0003FF1C
		internal void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "TotalItemsInView"))
					{
						if (!(text2 == "IncludesLastItemInRange"))
						{
							if (text2 == "Resolutions")
							{
								object[] array = jsonProperty.ReadAsArray(text);
								foreach (object obj in array)
								{
									JsonObject jsonObject = obj as JsonObject;
									if (jsonObject != null)
									{
										NameResolution nameResolution = new NameResolution(this);
										nameResolution.LoadFromJson(jsonObject, service);
										this.items.Add(nameResolution);
									}
								}
							}
						}
						else
						{
							this.includesAllResolutions = jsonProperty.ReadAsBool(text);
						}
					}
					else
					{
						jsonProperty.ReadAsInt(text);
					}
				}
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0004100C File Offset: 0x0004000C
		internal ExchangeService Session
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x00041014 File Offset: 0x00040014
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x00041021 File Offset: 0x00040021
		public bool IncludesAllResolutions
		{
			get
			{
				return this.includesAllResolutions;
			}
		}

		// Token: 0x170005C7 RID: 1479
		public NameResolution this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
				}
				return this.items[index];
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00041059 File Offset: 0x00040059
		public IEnumerator<NameResolution> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0004106B File Offset: 0x0004006B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0400137F RID: 4991
		private ExchangeService service;

		// Token: 0x04001380 RID: 4992
		private bool includesAllResolutions;

		// Token: 0x04001381 RID: 4993
		private List<NameResolution> items = new List<NameResolution>();
	}
}
