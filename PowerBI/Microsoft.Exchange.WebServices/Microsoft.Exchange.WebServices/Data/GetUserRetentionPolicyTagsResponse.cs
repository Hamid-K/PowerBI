using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000177 RID: 375
	public sealed class GetUserRetentionPolicyTagsResponse : ServiceResponse
	{
		// Token: 0x060010E7 RID: 4327 RVA: 0x00031908 File Offset: 0x00030908
		internal GetUserRetentionPolicyTagsResponse()
		{
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0003191C File Offset: 0x0003091C
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			this.retentionPolicyTags.Clear();
			base.ReadElementsFromXml(reader);
			reader.ReadStartElement(XmlNamespace.Messages, "RetentionPolicyTags");
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.IsStartElement(XmlNamespace.Types, "RetentionPolicyTag"))
					{
						this.retentionPolicyTags.Add(RetentionPolicyTag.LoadFromXml(reader));
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Messages, "RetentionPolicyTags"));
				reader.ReadEndElementIfNecessary(XmlNamespace.Messages, "RetentionPolicyTags");
			}
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00031990 File Offset: 0x00030990
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			this.retentionPolicyTags.Clear();
			base.ReadElementsFromJson(responseObject, service);
			if (responseObject.ContainsKey("RetentionPolicyTags"))
			{
				foreach (object obj in responseObject.ReadAsArray("RetentionPolicyTags"))
				{
					JsonObject jsonObject = obj as JsonObject;
					this.retentionPolicyTags.Add(RetentionPolicyTag.LoadFromJson(jsonObject));
				}
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x000319F3 File Offset: 0x000309F3
		public RetentionPolicyTag[] RetentionPolicyTags
		{
			get
			{
				return this.retentionPolicyTags.ToArray();
			}
		}

		// Token: 0x040009D1 RID: 2513
		private List<RetentionPolicyTag> retentionPolicyTags = new List<RetentionPolicyTag>();
	}
}
