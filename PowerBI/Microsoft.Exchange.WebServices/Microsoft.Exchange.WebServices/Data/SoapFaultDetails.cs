using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002A5 RID: 677
	internal class SoapFaultDetails
	{
		// Token: 0x060017DB RID: 6107 RVA: 0x0004119D File Offset: 0x0004019D
		private SoapFaultDetails()
		{
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x000411B8 File Offset: 0x000401B8
		internal static SoapFaultDetails Parse(EwsXmlReader reader, XmlNamespace soapNamespace)
		{
			SoapFaultDetails soapFaultDetails = new SoapFaultDetails();
			do
			{
				reader.Read();
				string localName;
				if (reader.NodeType == 1 && (localName = reader.LocalName) != null)
				{
					if (!(localName == "faultcode"))
					{
						if (!(localName == "faultstring"))
						{
							if (!(localName == "faultactor"))
							{
								if (localName == "detail")
								{
									soapFaultDetails.ParseDetailNode(reader);
								}
							}
							else
							{
								soapFaultDetails.FaultActor = reader.ReadElementValue();
							}
						}
						else
						{
							soapFaultDetails.FaultString = reader.ReadElementValue();
						}
					}
					else
					{
						soapFaultDetails.FaultCode = reader.ReadElementValue();
					}
				}
			}
			while (!reader.IsEndElement(soapNamespace, "Fault"));
			return soapFaultDetails;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00041260 File Offset: 0x00040260
		internal static SoapFaultDetails Parse(JsonObject jsonObject)
		{
			SoapFaultDetails soapFaultDetails = new SoapFaultDetails();
			foreach (string text in jsonObject.Keys)
			{
				string text2;
				if ((text2 = text) != null && text2 == "FaultMessage")
				{
					soapFaultDetails.FaultString = jsonObject.ReadAsString(text);
				}
			}
			return soapFaultDetails;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x000412D4 File Offset: 0x000402D4
		private void ParseDetailNode(EwsXmlReader reader)
		{
			do
			{
				reader.Read();
				string localName;
				if (reader.NodeType == 1 && (localName = reader.LocalName) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60016c6-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
						dictionary.Add("ResponseCode", 0);
						dictionary.Add("Message", 1);
						dictionary.Add("Line", 2);
						dictionary.Add("Position", 3);
						dictionary.Add("ErrorCode", 4);
						dictionary.Add("ExceptionType", 5);
						dictionary.Add("MessageXml", 6);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60016c6-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60016c6-1.TryGetValue(localName, ref num))
					{
						switch (num)
						{
						case 0:
							try
							{
								this.ResponseCode = reader.ReadElementValue<ServiceError>();
								goto IL_0132;
							}
							catch (ArgumentException)
							{
								this.ResponseCode = ServiceError.ErrorInternalServerError;
								goto IL_0132;
							}
							break;
						case 1:
							break;
						case 2:
							this.LineNumber = reader.ReadElementValue<int>();
							goto IL_0132;
						case 3:
							this.PositionWithinLine = reader.ReadElementValue<int>();
							goto IL_0132;
						case 4:
							try
							{
								this.ErrorCode = reader.ReadElementValue<ServiceError>();
								goto IL_0132;
							}
							catch (ArgumentException)
							{
								this.ErrorCode = ServiceError.ErrorInternalServerError;
								goto IL_0132;
							}
							goto IL_011D;
						case 5:
							goto IL_011D;
						case 6:
							this.ParseMessageXml(reader);
							goto IL_0132;
						default:
							goto IL_0132;
						}
						this.Message = reader.ReadElementValue();
						goto IL_0132;
						IL_011D:
						this.ExceptionType = reader.ReadElementValue();
					}
				}
				IL_0132:;
			}
			while (!reader.IsEndElement(XmlNamespace.NotSpecified, "detail"));
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00041440 File Offset: 0x00040440
		private void ParseMessageXml(EwsXmlReader reader)
		{
			XmlNamespace namespaceFromUri = EwsUtilities.GetNamespaceFromUri(reader.NamespaceUri);
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					string localName;
					if (reader.IsStartElement() && !reader.IsEmptyElement && (localName = reader.LocalName) != null && localName == "Value")
					{
						this.errorDetails.Add(reader.ReadAttributeValue("Name"), reader.ReadElementValue());
					}
				}
				while (!reader.IsEndElement(namespaceFromUri, "MessageXml"));
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x000414B8 File Offset: 0x000404B8
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x000414C0 File Offset: 0x000404C0
		internal string FaultCode
		{
			get
			{
				return this.faultCode;
			}
			set
			{
				this.faultCode = value;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x000414C9 File Offset: 0x000404C9
		// (set) Token: 0x060017E3 RID: 6115 RVA: 0x000414D1 File Offset: 0x000404D1
		internal string FaultString
		{
			get
			{
				return this.faultString;
			}
			set
			{
				this.faultString = value;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x000414DA File Offset: 0x000404DA
		// (set) Token: 0x060017E5 RID: 6117 RVA: 0x000414E2 File Offset: 0x000404E2
		internal string FaultActor
		{
			get
			{
				return this.faultActor;
			}
			set
			{
				this.faultActor = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x000414EB File Offset: 0x000404EB
		// (set) Token: 0x060017E7 RID: 6119 RVA: 0x000414F3 File Offset: 0x000404F3
		internal ServiceError ResponseCode
		{
			get
			{
				return this.responseCode;
			}
			set
			{
				this.responseCode = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x000414FC File Offset: 0x000404FC
		// (set) Token: 0x060017E9 RID: 6121 RVA: 0x00041504 File Offset: 0x00040504
		internal string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x0004150D File Offset: 0x0004050D
		// (set) Token: 0x060017EB RID: 6123 RVA: 0x00041515 File Offset: 0x00040515
		internal ServiceError ErrorCode
		{
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x0004151E File Offset: 0x0004051E
		// (set) Token: 0x060017ED RID: 6125 RVA: 0x00041526 File Offset: 0x00040526
		internal string ExceptionType
		{
			get
			{
				return this.exceptionType;
			}
			set
			{
				this.exceptionType = value;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x0004152F File Offset: 0x0004052F
		// (set) Token: 0x060017EF RID: 6127 RVA: 0x00041537 File Offset: 0x00040537
		internal int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00041540 File Offset: 0x00040540
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x00041548 File Offset: 0x00040548
		internal int PositionWithinLine
		{
			get
			{
				return this.positionWithinLine;
			}
			set
			{
				this.positionWithinLine = value;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00041551 File Offset: 0x00040551
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x00041559 File Offset: 0x00040559
		internal Dictionary<string, string> ErrorDetails
		{
			get
			{
				return this.errorDetails;
			}
			set
			{
				this.errorDetails = value;
			}
		}

		// Token: 0x0400138A RID: 5002
		private string faultCode;

		// Token: 0x0400138B RID: 5003
		private string faultString;

		// Token: 0x0400138C RID: 5004
		private string faultActor;

		// Token: 0x0400138D RID: 5005
		private ServiceError responseCode = ServiceError.ErrorInternalServerError;

		// Token: 0x0400138E RID: 5006
		private string message;

		// Token: 0x0400138F RID: 5007
		private ServiceError errorCode;

		// Token: 0x04001390 RID: 5008
		private string exceptionType;

		// Token: 0x04001391 RID: 5009
		private int lineNumber;

		// Token: 0x04001392 RID: 5010
		private int positionWithinLine;

		// Token: 0x04001393 RID: 5011
		private Dictionary<string, string> errorDetails = new Dictionary<string, string>();
	}
}
