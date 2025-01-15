using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000184 RID: 388
	[EditorBrowsable(1)]
	public abstract class SyncResponse<TServiceObject, TChange> : ServiceResponse where TServiceObject : ServiceObject where TChange : Change
	{
		// Token: 0x0600111C RID: 4380 RVA: 0x00031F76 File Offset: 0x00030F76
		internal SyncResponse(PropertySet propertySet)
		{
			this.propertySet = propertySet;
			EwsUtilities.Assert(this.propertySet != null, "SyncResponse.ctor", "PropertySet should not be null");
		}

		// Token: 0x0600111D RID: 4381
		internal abstract string GetIncludesLastInRangeXmlElementName();

		// Token: 0x0600111E RID: 4382
		internal abstract TChange CreateChangeInstance();

		// Token: 0x0600111F RID: 4383
		internal abstract string GetChangeElementName();

		// Token: 0x06001120 RID: 4384
		internal abstract string GetChangeIdElementName();

		// Token: 0x06001121 RID: 4385 RVA: 0x00031FAC File Offset: 0x00030FAC
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			this.Changes.SyncState = reader.ReadElementValue(XmlNamespace.Messages, "SyncState");
			this.Changes.MoreChangesAvailable = !reader.ReadElementValue<bool>(XmlNamespace.Messages, this.GetIncludesLastInRangeXmlElementName());
			reader.ReadStartElement(XmlNamespace.Messages, "Changes");
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.IsStartElement())
					{
						TChange tchange = this.CreateChangeInstance();
						string localName;
						if ((localName = reader.LocalName) == null)
						{
							goto IL_00E1;
						}
						if (!(localName == "Create"))
						{
							if (!(localName == "Update"))
							{
								if (!(localName == "Delete"))
								{
									if (!(localName == "ReadFlagChange"))
									{
										goto IL_00E1;
									}
									tchange.ChangeType = ChangeType.ReadFlagChange;
								}
								else
								{
									tchange.ChangeType = ChangeType.Delete;
								}
							}
							else
							{
								tchange.ChangeType = ChangeType.Update;
							}
						}
						else
						{
							tchange.ChangeType = ChangeType.Create;
						}
						IL_00E7:
						if (tchange != null)
						{
							reader.Read();
							reader.EnsureCurrentNodeIsStartElement();
							switch (tchange.ChangeType)
							{
							case ChangeType.Delete:
							case ChangeType.ReadFlagChange:
								tchange.Id = tchange.CreateId();
								tchange.Id.LoadFromXml(reader, tchange.Id.GetXmlElementName());
								if (tchange.ChangeType == ChangeType.ReadFlagChange)
								{
									reader.Read();
									reader.EnsureCurrentNodeIsStartElement();
									ItemChange itemChange = tchange as ItemChange;
									EwsUtilities.Assert(itemChange != null, "SyncResponse.ReadElementsFromXml", "ReadFlagChange is only valid on ItemChange");
									itemChange.IsRead = reader.ReadElementValue<bool>(XmlNamespace.Types, "IsRead");
								}
								break;
							default:
								tchange.ServiceObject = EwsUtilities.CreateEwsObjectFromXmlElementName<TServiceObject>(reader.Service, reader.LocalName);
								tchange.ServiceObject.LoadFromXml(reader, true, this.propertySet, this.SummaryPropertiesOnly);
								break;
							}
							reader.ReadEndElementIfNecessary(XmlNamespace.Types, tchange.ChangeType.ToString());
							this.changes.Add(tchange);
							goto IL_0222;
						}
						goto IL_0222;
						IL_00E1:
						reader.SkipCurrentElement();
						goto IL_00E7;
					}
					IL_0222:;
				}
				while (!reader.IsEndElement(XmlNamespace.Messages, "Changes"));
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000321EC File Offset: 0x000311EC
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			this.Changes.SyncState = responseObject.ReadAsString("SyncState");
			this.Changes.MoreChangesAvailable = !responseObject.ReadAsBool(this.GetIncludesLastInRangeXmlElementName());
			JsonObject jsonObject = responseObject.ReadAsJsonObject("Changes");
			foreach (object obj in jsonObject.ReadAsArray("Changes"))
			{
				JsonObject jsonObject2 = obj as JsonObject;
				TChange tchange = this.CreateChangeInstance();
				string text = jsonObject2.ReadAsString("ChangeType");
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "Create"))
					{
						if (!(text2 == "Update"))
						{
							if (!(text2 == "Delete"))
							{
								if (text2 == "ReadFlagChange")
								{
									tchange.ChangeType = ChangeType.ReadFlagChange;
								}
							}
							else
							{
								tchange.ChangeType = ChangeType.Delete;
							}
						}
						else
						{
							tchange.ChangeType = ChangeType.Update;
						}
					}
					else
					{
						tchange.ChangeType = ChangeType.Create;
					}
				}
				if (tchange != null)
				{
					switch (tchange.ChangeType)
					{
					case ChangeType.Delete:
					case ChangeType.ReadFlagChange:
					{
						tchange.Id = tchange.CreateId();
						JsonObject jsonObject3 = jsonObject2.ReadAsJsonObject(this.GetChangeIdElementName());
						tchange.Id.LoadFromJson(jsonObject3, service);
						if (tchange.ChangeType == ChangeType.ReadFlagChange)
						{
							ItemChange itemChange = tchange as ItemChange;
							EwsUtilities.Assert(itemChange != null, "SyncResponse.ReadElementsFromJson", "ReadFlagChange is only valid on ItemChange");
							itemChange.IsRead = jsonObject2.ReadAsBool("IsRead");
						}
						break;
					}
					default:
					{
						JsonObject jsonObject4 = jsonObject2.ReadAsJsonObject(this.GetChangeElementName());
						tchange.ServiceObject = EwsUtilities.CreateEwsObjectFromXmlElementName<TServiceObject>(service, jsonObject4.ReadTypeString());
						tchange.ServiceObject.LoadFromJson(jsonObject4, service, true, this.propertySet, this.SummaryPropertiesOnly);
						break;
					}
					}
					this.changes.Add(tchange);
				}
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0003240E File Offset: 0x0003140E
		public ChangeCollection<TChange> Changes
		{
			get
			{
				return this.changes;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001124 RID: 4388
		internal abstract bool SummaryPropertiesOnly { get; }

		// Token: 0x040009DD RID: 2525
		private ChangeCollection<TChange> changes = new ChangeCollection<TChange>();

		// Token: 0x040009DE RID: 2526
		private PropertySet propertySet;
	}
}
