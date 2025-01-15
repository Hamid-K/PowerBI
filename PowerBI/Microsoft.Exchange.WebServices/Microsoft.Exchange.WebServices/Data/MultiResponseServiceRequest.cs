using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000E7 RID: 231
	internal abstract class MultiResponseServiceRequest<TResponse> : SimpleServiceRequestBase where TResponse : ServiceResponse
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x00027EBC File Offset: 0x00026EBC
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			ServiceResponseCollection<TResponse> serviceResponseCollection = new ServiceResponseCollection<TResponse>();
			reader.ReadStartElement(XmlNamespace.Messages, "ResponseMessages");
			for (int i = 0; i < this.GetExpectedResponseMessageCount(); i++)
			{
				reader.Read();
				if (reader.IsEndElement(XmlNamespace.Messages, "ResponseMessages"))
				{
					break;
				}
				TResponse tresponse = this.CreateServiceResponse(reader.Service, i);
				tresponse.LoadFromXml(reader, this.GetResponseMessageXmlElementName());
				serviceResponseCollection.Add(tresponse);
			}
			if (serviceResponseCollection.Count < this.GetExpectedResponseMessageCount())
			{
				if (serviceResponseCollection.Count == 1)
				{
					TResponse tresponse2 = serviceResponseCollection[0];
					if (tresponse2.Result == ServiceResult.Error)
					{
						throw new ServiceResponseException(serviceResponseCollection[0]);
					}
				}
				throw new ServiceXmlDeserializationException(string.Format(Strings.TooFewServiceReponsesReturned, this.GetResponseMessageXmlElementName(), this.GetExpectedResponseMessageCount(), serviceResponseCollection.Count));
			}
			reader.ReadEndElementIfNecessary(XmlNamespace.Messages, "ResponseMessages");
			return serviceResponseCollection;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00027FA8 File Offset: 0x00026FA8
		internal override object ParseResponse(JsonObject jsonBody)
		{
			ServiceResponseCollection<TResponse> serviceResponseCollection = new ServiceResponseCollection<TResponse>();
			object[] array = jsonBody.ReadAsJsonObject("ResponseMessages").ReadAsArray("Items");
			int num = 0;
			foreach (object obj in array)
			{
				TResponse tresponse = this.CreateServiceResponse(base.Service, num);
				tresponse.LoadFromJson(obj as JsonObject, base.Service);
				serviceResponseCollection.Add(tresponse);
				num++;
			}
			if (serviceResponseCollection.Count < this.GetExpectedResponseMessageCount())
			{
				if (serviceResponseCollection.Count == 1)
				{
					TResponse tresponse2 = serviceResponseCollection[0];
					if (tresponse2.Result == ServiceResult.Error)
					{
						throw new ServiceResponseException(serviceResponseCollection[0]);
					}
				}
				throw new ServiceJsonDeserializationException();
			}
			return serviceResponseCollection;
		}

		// Token: 0x06000BCF RID: 3023
		internal abstract TResponse CreateServiceResponse(ExchangeService service, int responseIndex);

		// Token: 0x06000BD0 RID: 3024
		internal abstract string GetResponseMessageXmlElementName();

		// Token: 0x06000BD1 RID: 3025
		internal abstract int GetExpectedResponseMessageCount();

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002806C File Offset: 0x0002706C
		internal MultiResponseServiceRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service)
		{
			this.errorHandlingMode = errorHandlingMode;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002807C File Offset: 0x0002707C
		internal ServiceResponseCollection<TResponse> Execute()
		{
			ServiceResponseCollection<TResponse> serviceResponseCollection = (ServiceResponseCollection<TResponse>)base.InternalExecute();
			if (this.ErrorHandlingMode == ServiceErrorHandling.ThrowOnError)
			{
				EwsUtilities.Assert(serviceResponseCollection.Count == 1, "MultiResponseServiceRequest.Execute", "ServiceErrorHandling.ThrowOnError error handling is only valid for singleton request");
				TResponse tresponse = serviceResponseCollection[0];
				tresponse.ThrowIfNecessary();
			}
			return serviceResponseCollection;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x000280CC File Offset: 0x000270CC
		internal ServiceResponseCollection<TResponse> EndExecute(IAsyncResult asyncResult)
		{
			ServiceResponseCollection<TResponse> serviceResponseCollection = (ServiceResponseCollection<TResponse>)base.EndInternalExecute(asyncResult);
			if (this.ErrorHandlingMode == ServiceErrorHandling.ThrowOnError)
			{
				EwsUtilities.Assert(serviceResponseCollection.Count == 1, "MultiResponseServiceRequest.Execute", "ServiceErrorHandling.ThrowOnError error handling is only valid for singleton request");
				TResponse tresponse = serviceResponseCollection[0];
				tresponse.ThrowIfNecessary();
			}
			return serviceResponseCollection;
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0002811D File Offset: 0x0002711D
		internal ServiceErrorHandling ErrorHandlingMode
		{
			get
			{
				return this.errorHandlingMode;
			}
		}

		// Token: 0x040008B2 RID: 2226
		private ServiceErrorHandling errorHandlingMode;
	}
}
