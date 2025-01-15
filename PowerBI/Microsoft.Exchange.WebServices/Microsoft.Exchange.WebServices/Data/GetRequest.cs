using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000112 RID: 274
	internal abstract class GetRequest<TServiceObject, TResponse> : MultiResponseServiceRequest<TResponse>, IJsonSerializable where TServiceObject : ServiceObject where TResponse : ServiceResponse
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x0002B1AB File Offset: 0x0002A1AB
		internal GetRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0002B1B5 File Offset: 0x0002A1B5
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.PropertySet, "PropertySet");
			this.PropertySet.ValidateForRequest(this, false);
		}

		// Token: 0x06000DA9 RID: 3497
		internal abstract ServiceObjectType GetServiceObjectType();

		// Token: 0x06000DAA RID: 3498 RVA: 0x0002B1DA File Offset: 0x0002A1DA
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.propertySet.WriteToXml(writer, this.GetServiceObjectType());
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0002B1EE File Offset: 0x0002A1EE
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x0002B1F6 File Offset: 0x0002A1F6
		public PropertySet PropertySet
		{
			get
			{
				return this.propertySet;
			}
			set
			{
				this.propertySet = value;
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0002B200 File Offset: 0x0002A200
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			this.propertySet.WriteGetShapeToJson(jsonObject, service, this.GetServiceObjectType());
			this.AddIdsToRequest(jsonObject, service);
			return jsonObject;
		}

		// Token: 0x06000DAE RID: 3502
		internal abstract void AddIdsToRequest(JsonObject jsonRequest, ExchangeService service);

		// Token: 0x04000909 RID: 2313
		private PropertySet propertySet;
	}
}
