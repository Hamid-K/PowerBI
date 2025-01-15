using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200014B RID: 331
	[Serializable]
	public class ServiceResponse
	{
		// Token: 0x0600101B RID: 4123 RVA: 0x0002F1EB File Offset: 0x0002E1EB
		internal ServiceResponse()
		{
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0002F20C File Offset: 0x0002E20C
		internal ServiceResponse(SoapFaultDetails soapFaultDetails)
		{
			this.result = ServiceResult.Error;
			this.errorCode = soapFaultDetails.ResponseCode;
			this.errorMessage = soapFaultDetails.FaultString;
			this.errorDetails = soapFaultDetails.ErrorDetails;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0002F260 File Offset: 0x0002E260
		internal ServiceResponse(ServiceError responseCode, string errorMessage)
		{
			this.result = ServiceResult.Error;
			this.errorCode = responseCode;
			this.errorMessage = errorMessage;
			this.errorDetails = null;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0002F29C File Offset: 0x0002E29C
		internal void LoadFromXml(EwsServiceXmlReader reader, string xmlElementName)
		{
			if (!reader.IsStartElement(XmlNamespace.Messages, xmlElementName))
			{
				reader.ReadStartElement(XmlNamespace.Messages, xmlElementName);
			}
			this.result = reader.ReadAttributeValue<ServiceResult>("ResponseClass");
			if (this.result == ServiceResult.Success || this.result == ServiceResult.Warning)
			{
				if (this.result == ServiceResult.Warning)
				{
					this.errorMessage = reader.ReadElementValue(XmlNamespace.Messages, "MessageText");
				}
				this.errorCode = reader.ReadElementValue<ServiceError>(XmlNamespace.Messages, "ResponseCode");
				if (this.result == ServiceResult.Warning)
				{
					reader.ReadElementValue<int>(XmlNamespace.Messages, "DescriptiveLinkKey");
				}
				if (this.BatchProcessingStopped)
				{
					do
					{
						reader.Read();
					}
					while (!reader.IsEndElement(XmlNamespace.Messages, xmlElementName));
				}
				else
				{
					this.ReadElementsFromXml(reader);
					reader.ReadEndElementIfNecessary(XmlNamespace.Messages, xmlElementName);
				}
			}
			else
			{
				this.errorMessage = reader.ReadElementValue(XmlNamespace.Messages, "MessageText");
				this.errorCode = reader.ReadElementValue<ServiceError>(XmlNamespace.Messages, "ResponseCode");
				reader.ReadElementValue<int>(XmlNamespace.Messages, "DescriptiveLinkKey");
				while (!reader.IsEndElement(XmlNamespace.Messages, xmlElementName))
				{
					reader.Read();
					if (reader.IsStartElement() && !this.LoadExtraErrorDetailsFromXml(reader, reader.LocalName))
					{
						reader.SkipCurrentElement();
					}
				}
			}
			this.MapErrorCodeToErrorMessage();
			this.Loaded();
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0002F3B8 File Offset: 0x0002E3B8
		internal void LoadFromJson(JsonObject responseObject, ExchangeService service)
		{
			this.result = (ServiceResult)Enum.Parse(typeof(ServiceResult), responseObject.ReadAsString("ResponseClass"));
			this.errorCode = (ServiceError)Enum.Parse(typeof(ServiceError), responseObject.ReadAsString("ResponseCode"));
			if (this.result == ServiceResult.Warning || this.result == ServiceResult.Error)
			{
				this.errorMessage = responseObject.ReadAsString("MessageText");
			}
			if ((this.result == ServiceResult.Success || this.result == ServiceResult.Warning) && !this.BatchProcessingStopped)
			{
				this.ReadElementsFromJson(responseObject, service);
			}
			this.MapErrorCodeToErrorMessage();
			this.Loaded();
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0002F460 File Offset: 0x0002E460
		private void ParseMessageXml(EwsServiceXmlReader reader)
		{
			do
			{
				reader.Read();
				string localName;
				if (reader.IsStartElement() && (localName = reader.LocalName) != null)
				{
					if (!(localName == "Value"))
					{
						if (!(localName == "FieldURI"))
						{
							if (!(localName == "IndexedFieldURI"))
							{
								if (localName == "ExtendedFieldURI")
								{
									ExtendedPropertyDefinition extendedPropertyDefinition = new ExtendedPropertyDefinition();
									extendedPropertyDefinition.LoadFromXml(reader);
									this.errorProperties.Add(extendedPropertyDefinition);
								}
							}
							else
							{
								this.errorProperties.Add(new IndexedPropertyDefinition(reader.ReadAttributeValue("FieldURI"), reader.ReadAttributeValue("FieldIndex")));
							}
						}
						else
						{
							this.errorProperties.Add(ServiceObjectSchema.FindPropertyDefinition(reader.ReadAttributeValue("FieldURI")));
						}
					}
					else
					{
						this.errorDetails.Add(reader.ReadAttributeValue("Name"), reader.ReadElementValue());
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Messages, "MessageXml"));
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0002F54E File Offset: 0x0002E54E
		internal virtual void Loaded()
		{
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0002F550 File Offset: 0x0002E550
		internal void MapErrorCodeToErrorMessage()
		{
			if (this.ErrorCode == ServiceError.ErrorIrresolvableConflict)
			{
				this.ErrorMessage = Strings.ItemIsOutOfDate;
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0002F56F File Offset: 0x0002E56F
		internal virtual void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0002F571 File Offset: 0x0002E571
		internal virtual void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0002F573 File Offset: 0x0002E573
		internal virtual bool LoadExtraErrorDetailsFromXml(EwsServiceXmlReader reader, string xmlElementName)
		{
			if (reader.IsStartElement(XmlNamespace.Messages, "MessageXml") && !reader.IsEmptyElement)
			{
				this.ParseMessageXml(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0002F595 File Offset: 0x0002E595
		internal void ThrowIfNecessary()
		{
			this.InternalThrowIfNecessary();
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0002F59D File Offset: 0x0002E59D
		internal virtual void InternalThrowIfNecessary()
		{
			if (this.Result == ServiceResult.Error)
			{
				throw new ServiceResponseException(this);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0002F5AF File Offset: 0x0002E5AF
		internal bool BatchProcessingStopped
		{
			get
			{
				return this.result == ServiceResult.Warning && this.errorCode == ServiceError.ErrorBatchProcessingStopped;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x0002F5C6 File Offset: 0x0002E5C6
		public ServiceResult Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0002F5CE File Offset: 0x0002E5CE
		public ServiceError ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0002F5D6 File Offset: 0x0002E5D6
		// (set) Token: 0x0600102C RID: 4140 RVA: 0x0002F5DE File Offset: 0x0002E5DE
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
			internal set
			{
				this.errorMessage = value;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0002F5E7 File Offset: 0x0002E5E7
		public IDictionary<string, string> ErrorDetails
		{
			get
			{
				return this.errorDetails;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0002F5EF File Offset: 0x0002E5EF
		public Collection<PropertyDefinitionBase> ErrorProperties
		{
			get
			{
				return this.errorProperties;
			}
		}

		// Token: 0x04000989 RID: 2441
		private ServiceResult result;

		// Token: 0x0400098A RID: 2442
		private ServiceError errorCode;

		// Token: 0x0400098B RID: 2443
		private string errorMessage;

		// Token: 0x0400098C RID: 2444
		private Dictionary<string, string> errorDetails = new Dictionary<string, string>();

		// Token: 0x0400098D RID: 2445
		private Collection<PropertyDefinitionBase> errorProperties = new Collection<PropertyDefinitionBase>();
	}
}
