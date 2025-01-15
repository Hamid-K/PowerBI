using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000AA RID: 170
	public sealed class TimeSuggestion : ComplexProperty
	{
		// Token: 0x0600079F RID: 1951 RVA: 0x00019DBD File Offset: 0x00018DBD
		internal TimeSuggestion()
		{
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00019DD0 File Offset: 0x00018DD0
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "MeetingTime")
				{
					this.meetingTime = reader.ReadElementValueAsUnbiasedDateTimeScopedToServiceTimeZone();
					return true;
				}
				if (localName == "IsWorkTime")
				{
					this.isWorkTime = reader.ReadElementValue<bool>();
					return true;
				}
				if (localName == "SuggestionQuality")
				{
					this.quality = reader.ReadElementValue<SuggestionQuality>();
					return true;
				}
				if (localName == "AttendeeConflictDataArray")
				{
					if (!reader.IsEmptyElement)
					{
						do
						{
							reader.Read();
							if (reader.IsStartElement())
							{
								Conflict conflict = null;
								string localName2;
								if ((localName2 = reader.LocalName) == null)
								{
									goto IL_00F2;
								}
								if (!(localName2 == "UnknownAttendeeConflictData"))
								{
									if (!(localName2 == "TooBigGroupAttendeeConflictData"))
									{
										if (!(localName2 == "IndividualAttendeeConflictData"))
										{
											if (!(localName2 == "GroupAttendeeConflictData"))
											{
												goto IL_00F2;
											}
											conflict = new Conflict(ConflictType.GroupConflict);
										}
										else
										{
											conflict = new Conflict(ConflictType.IndividualAttendeeConflict);
										}
									}
									else
									{
										conflict = new Conflict(ConflictType.GroupTooBigConflict);
									}
								}
								else
								{
									conflict = new Conflict(ConflictType.UnknownAttendeeConflict);
								}
								IL_010D:
								conflict.LoadFromXml(reader, reader.LocalName);
								this.conflicts.Add(conflict);
								goto IL_0126;
								IL_00F2:
								EwsUtilities.Assert(false, "TimeSuggestion.TryReadElementFromXml", string.Format("The {0} element name does not map to any AttendeeConflict descendant.", reader.LocalName));
								goto IL_010D;
							}
							IL_0126:;
						}
						while (!reader.IsEndElement(XmlNamespace.Types, "AttendeeConflictDataArray"));
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00019F18 File Offset: 0x00018F18
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "MeetingTime"))
					{
						if (!(text2 == "IsWorkTime"))
						{
							if (!(text2 == "SuggestionQuality"))
							{
								if (text2 == "AttendeeConflictDataArray")
								{
									object[] array = jsonProperty.ReadAsArray(text);
									foreach (object obj in array)
									{
										JsonObject jsonObject = obj as JsonObject;
										if (jsonObject != null)
										{
											Conflict conflict = null;
											string text3;
											if ((text3 = jsonObject.ReadTypeString()) == null)
											{
												goto IL_0132;
											}
											if (!(text3 == "UnknownAttendeeConflictData"))
											{
												if (!(text3 == "TooBigGroupAttendeeConflictData"))
												{
													if (!(text3 == "IndividualAttendeeConflictData"))
													{
														if (!(text3 == "GroupAttendeeConflictData"))
														{
															goto IL_0132;
														}
														conflict = new Conflict(ConflictType.GroupConflict);
													}
													else
													{
														conflict = new Conflict(ConflictType.IndividualAttendeeConflict);
													}
												}
												else
												{
													conflict = new Conflict(ConflictType.GroupTooBigConflict);
												}
											}
											else
											{
												conflict = new Conflict(ConflictType.UnknownAttendeeConflict);
											}
											IL_014D:
											conflict.LoadFromJson(jsonObject, service);
											this.conflicts.Add(conflict);
											goto IL_0163;
											IL_0132:
											EwsUtilities.Assert(false, "TimeSuggestion.TryReadElementFromJson", string.Format("The {0} element name does not map to any AttendeeConflict descendant.", jsonObject.ReadTypeString()));
											goto IL_014D;
										}
										IL_0163:;
									}
								}
							}
							else
							{
								this.quality = jsonProperty.ReadEnumValue<SuggestionQuality>(text);
							}
						}
						else
						{
							this.isWorkTime = jsonProperty.ReadAsBool(text);
						}
					}
					else
					{
						this.meetingTime = EwsUtilities.ParseAsUnbiasedDatetimescopedToServicetimeZone(jsonProperty.ReadAsString(text), service);
					}
				}
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0001A0D4 File Offset: 0x000190D4
		public DateTime MeetingTime
		{
			get
			{
				return this.meetingTime;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001A0DC File Offset: 0x000190DC
		public bool IsWorkTime
		{
			get
			{
				return this.isWorkTime;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0001A0E4 File Offset: 0x000190E4
		public SuggestionQuality Quality
		{
			get
			{
				return this.quality;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0001A0EC File Offset: 0x000190EC
		public Collection<Conflict> Conflicts
		{
			get
			{
				return this.conflicts;
			}
		}

		// Token: 0x04000285 RID: 645
		private DateTime meetingTime;

		// Token: 0x04000286 RID: 646
		private bool isWorkTime;

		// Token: 0x04000287 RID: 647
		private SuggestionQuality quality;

		// Token: 0x04000288 RID: 648
		private Collection<Conflict> conflicts = new Collection<Conflict>();
	}
}
