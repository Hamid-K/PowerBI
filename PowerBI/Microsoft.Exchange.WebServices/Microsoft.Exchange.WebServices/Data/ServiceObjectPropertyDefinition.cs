using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002C3 RID: 707
	public abstract class ServiceObjectPropertyDefinition : PropertyDefinitionBase
	{
		// Token: 0x0600192C RID: 6444 RVA: 0x0004497C File Offset: 0x0004397C
		internal override string GetXmlElementName()
		{
			return "FieldURI";
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00044983 File Offset: 0x00043983
		protected override string GetJsonType()
		{
			return "PropertyUri";
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x0004498A File Offset: 0x0004398A
		public override ExchangeVersion Version
		{
			get
			{
				return ExchangeVersion.Exchange2007_SP1;
			}
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0004498D File Offset: 0x0004398D
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("FieldURI", this.Uri);
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x000449A0 File Offset: 0x000439A0
		internal override void AddJsonProperties(JsonObject jsonPropertyDefinition, ExchangeService service)
		{
			jsonPropertyDefinition.Add("FieldURI", this.Uri);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x000449B3 File Offset: 0x000439B3
		internal ServiceObjectPropertyDefinition()
		{
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x000449BB File Offset: 0x000439BB
		internal ServiceObjectPropertyDefinition(string uri)
		{
			EwsUtilities.Assert(!string.IsNullOrEmpty(uri), "ServiceObjectPropertyDefinition.ctor", "uri is null or empty");
			this.uri = uri;
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x000449E2 File Offset: 0x000439E2
		internal string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x040013ED RID: 5101
		private string uri;
	}
}
