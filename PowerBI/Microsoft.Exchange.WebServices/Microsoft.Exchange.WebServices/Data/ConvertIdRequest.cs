using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000EC RID: 236
	internal sealed class ConvertIdRequest : MultiResponseServiceRequest<ConvertIdResponse>, IJsonSerializable
	{
		// Token: 0x06000C06 RID: 3078 RVA: 0x0002856E File Offset: 0x0002756E
		internal ConvertIdRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002858A File Offset: 0x0002758A
		internal override ConvertIdResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ConvertIdResponse();
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00028591 File Offset: 0x00027591
		internal override string GetResponseXmlElementName()
		{
			return "ConvertIdResponse";
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00028598 File Offset: 0x00027598
		internal override string GetResponseMessageXmlElementName()
		{
			return "ConvertIdResponseMessage";
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002859F File Offset: 0x0002759F
		internal override int GetExpectedResponseMessageCount()
		{
			return this.Ids.Count;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x000285AC File Offset: 0x000275AC
		internal override string GetXmlElementName()
		{
			return "ConvertId";
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x000285B3 File Offset: 0x000275B3
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParamCollection(this.Ids, "Ids");
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x000285CC File Offset: 0x000275CC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("DestinationFormat", this.DestinationFormat);
			writer.WriteStartElement(XmlNamespace.Messages, "SourceIds");
			foreach (AlternateIdBase alternateIdBase in this.Ids)
			{
				alternateIdBase.WriteToXml(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00028648 File Offset: 0x00027648
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0002864B File Offset: 0x0002764B
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x00028653 File Offset: 0x00027653
		public IdFormat DestinationFormat
		{
			get
			{
				return this.destinationFormat;
			}
			set
			{
				this.destinationFormat = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0002865C File Offset: 0x0002765C
		public List<AlternateIdBase> Ids
		{
			get
			{
				return this.ids;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00028664 File Offset: 0x00027664
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("DestinationFormat", this.DestinationFormat);
			List<object> list = new List<object>();
			foreach (AlternateIdBase alternateIdBase in this.Ids)
			{
				list.Add(((IJsonSerializable)alternateIdBase).ToJson(service));
			}
			jsonObject.Add("SourceIds", list.ToArray());
			return jsonObject;
		}

		// Token: 0x040008BE RID: 2238
		private IdFormat destinationFormat = IdFormat.EwsId;

		// Token: 0x040008BF RID: 2239
		private List<AlternateIdBase> ids = new List<AlternateIdBase>();
	}
}
