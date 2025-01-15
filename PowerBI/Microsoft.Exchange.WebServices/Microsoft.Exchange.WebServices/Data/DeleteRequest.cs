using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000FA RID: 250
	internal abstract class DeleteRequest<TResponse> : MultiResponseServiceRequest<TResponse>, IJsonSerializable where TResponse : ServiceResponse
	{
		// Token: 0x06000C88 RID: 3208 RVA: 0x0002929F File Offset: 0x0002829F
		internal DeleteRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x000292B0 File Offset: 0x000282B0
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			writer.WriteAttributeValue("DeleteType", this.DeleteMode);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000292D0 File Offset: 0x000282D0
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("DeleteType", this.DeleteMode.ToString());
			this.InternalToJson(jsonObject);
			return jsonObject;
		}

		// Token: 0x06000C8B RID: 3211
		protected abstract void InternalToJson(JsonObject body);

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00029306 File Offset: 0x00028306
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x0002930E File Offset: 0x0002830E
		public DeleteMode DeleteMode
		{
			get
			{
				return this.deleteMode;
			}
			set
			{
				this.deleteMode = value;
			}
		}

		// Token: 0x040008CC RID: 2252
		private DeleteMode deleteMode = DeleteMode.SoftDelete;
	}
}
