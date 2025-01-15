using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000093 RID: 147
	public sealed class Rule : ComplexProperty
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x0001683C File Offset: 0x0001583C
		public Rule()
		{
			this.priority = 1;
			this.isEnabled = true;
			this.conditions = new RulePredicates();
			this.actions = new RuleActions();
			this.exceptions = new RulePredicates();
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00016873 File Offset: 0x00015873
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0001687B File Offset: 0x0001587B
		public string Id
		{
			get
			{
				return this.ruleId;
			}
			set
			{
				this.SetFieldValue<string>(ref this.ruleId, value);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001688A File Offset: 0x0001588A
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00016892 File Offset: 0x00015892
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.SetFieldValue<string>(ref this.displayName, value);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x000168A1 File Offset: 0x000158A1
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x000168A9 File Offset: 0x000158A9
		public int Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.SetFieldValue<int>(ref this.priority, value);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x000168B8 File Offset: 0x000158B8
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x000168C0 File Offset: 0x000158C0
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isEnabled, value);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x000168CF File Offset: 0x000158CF
		public bool IsNotSupported
		{
			get
			{
				return this.isNotSupported;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x000168D7 File Offset: 0x000158D7
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x000168DF File Offset: 0x000158DF
		public bool IsInError
		{
			get
			{
				return this.isInError;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isInError, value);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x000168EE File Offset: 0x000158EE
		public RulePredicates Conditions
		{
			get
			{
				return this.conditions;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x000168F6 File Offset: 0x000158F6
		public RuleActions Actions
		{
			get
			{
				return this.actions;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x000168FE File Offset: 0x000158FE
		public RulePredicates Exceptions
		{
			get
			{
				return this.exceptions;
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00016908 File Offset: 0x00015908
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600064f-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(9);
					dictionary.Add("DisplayName", 0);
					dictionary.Add("RuleId", 1);
					dictionary.Add("Priority", 2);
					dictionary.Add("IsEnabled", 3);
					dictionary.Add("IsNotSupported", 4);
					dictionary.Add("IsInError", 5);
					dictionary.Add("Conditions", 6);
					dictionary.Add("Actions", 7);
					dictionary.Add("Exceptions", 8);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600064f-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600064f-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.displayName = reader.ReadElementValue();
						return true;
					case 1:
						this.ruleId = reader.ReadElementValue();
						return true;
					case 2:
						this.priority = reader.ReadElementValue<int>();
						return true;
					case 3:
						this.isEnabled = reader.ReadElementValue<bool>();
						return true;
					case 4:
						this.isNotSupported = reader.ReadElementValue<bool>();
						return true;
					case 5:
						this.isInError = reader.ReadElementValue<bool>();
						return true;
					case 6:
						this.conditions.LoadFromXml(reader, reader.LocalName);
						return true;
					case 7:
						this.actions.LoadFromXml(reader, reader.LocalName);
						return true;
					case 8:
						this.exceptions.LoadFromXml(reader, reader.LocalName);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00016A7C File Offset: 0x00015A7C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000650-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(9);
						dictionary.Add("DisplayName", 0);
						dictionary.Add("RuleId", 1);
						dictionary.Add("Priority", 2);
						dictionary.Add("IsEnabled", 3);
						dictionary.Add("IsNotSupported", 4);
						dictionary.Add("IsInError", 5);
						dictionary.Add("Conditions", 6);
						dictionary.Add("Actions", 7);
						dictionary.Add("Exceptions", 8);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000650-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000650-1.TryGetValue(text2, ref num))
					{
						switch (num)
						{
						case 0:
							this.displayName = jsonProperty.ReadAsString(text);
							break;
						case 1:
							this.ruleId = jsonProperty.ReadAsString(text);
							break;
						case 2:
							this.priority = jsonProperty.ReadAsInt(text);
							break;
						case 3:
							this.isEnabled = jsonProperty.ReadAsBool(text);
							break;
						case 4:
							this.isNotSupported = jsonProperty.ReadAsBool(text);
							break;
						case 5:
							this.isInError = jsonProperty.ReadAsBool(text);
							break;
						case 6:
							this.conditions.LoadFromJson(jsonProperty, service);
							break;
						case 7:
							this.actions.LoadFromJson(jsonProperty, service);
							break;
						case 8:
							this.exceptions.LoadFromJson(jsonProperty, service);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00016C30 File Offset: 0x00015C30
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Id))
			{
				writer.WriteElementValue(XmlNamespace.Types, "RuleId", this.Id);
			}
			writer.WriteElementValue(XmlNamespace.Types, "DisplayName", this.DisplayName);
			writer.WriteElementValue(XmlNamespace.Types, "Priority", this.Priority);
			writer.WriteElementValue(XmlNamespace.Types, "IsEnabled", this.IsEnabled);
			writer.WriteElementValue(XmlNamespace.Types, "IsInError", this.IsInError);
			this.Conditions.WriteToXml(writer, "Conditions");
			this.Exceptions.WriteToXml(writer, "Exceptions");
			this.Actions.WriteToXml(writer, "Actions");
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00016CE8 File Offset: 0x00015CE8
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			if (!string.IsNullOrEmpty(this.Id))
			{
				jsonObject.Add("RuleId", this.Id);
			}
			jsonObject.Add("DisplayName", this.DisplayName);
			jsonObject.Add("Priority", this.Priority);
			jsonObject.Add("IsEnabled", this.IsEnabled);
			jsonObject.Add("IsInError", this.IsInError);
			jsonObject.Add("Conditions", this.Conditions.InternalToJson(service));
			jsonObject.Add("Exceptions", this.Exceptions.InternalToJson(service));
			jsonObject.Add("Actions", this.Actions.InternalToJson(service));
			return jsonObject;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00016DA4 File Offset: 0x00015DA4
		internal override void InternalValidate()
		{
			base.InternalValidate();
			EwsUtilities.ValidateParam(this.displayName, "DisplayName");
			EwsUtilities.ValidateParam(this.conditions, "Conditions");
			EwsUtilities.ValidateParam(this.exceptions, "Exceptions");
			EwsUtilities.ValidateParam(this.actions, "Actions");
		}

		// Token: 0x0400021D RID: 541
		private string ruleId;

		// Token: 0x0400021E RID: 542
		private string displayName;

		// Token: 0x0400021F RID: 543
		private int priority;

		// Token: 0x04000220 RID: 544
		private bool isEnabled;

		// Token: 0x04000221 RID: 545
		private bool isNotSupported;

		// Token: 0x04000222 RID: 546
		private bool isInError;

		// Token: 0x04000223 RID: 547
		private RulePredicates conditions;

		// Token: 0x04000224 RID: 548
		private RuleActions actions;

		// Token: 0x04000225 RID: 549
		private RulePredicates exceptions;
	}
}
