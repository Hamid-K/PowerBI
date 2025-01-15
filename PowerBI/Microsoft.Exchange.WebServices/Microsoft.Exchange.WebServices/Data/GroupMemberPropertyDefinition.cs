using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002D2 RID: 722
	internal sealed class GroupMemberPropertyDefinition : ServiceObjectPropertyDefinition
	{
		// Token: 0x060019A8 RID: 6568 RVA: 0x00045D09 File Offset: 0x00044D09
		public GroupMemberPropertyDefinition(string key)
			: base("distributionlist:Members:Member")
		{
			this.key = key;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00045D1D File Offset: 0x00044D1D
		internal GroupMemberPropertyDefinition()
			: base("distributionlist:Members:Member")
		{
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x00045D2A File Offset: 0x00044D2A
		// (set) Token: 0x060019AB RID: 6571 RVA: 0x00045D32 File Offset: 0x00044D32
		public string Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00045D3B File Offset: 0x00044D3B
		internal override string GetXmlElementName()
		{
			return "IndexedFieldURI";
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00045D42 File Offset: 0x00044D42
		protected override string GetJsonType()
		{
			return "DictionaryPropertyUri";
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00045D49 File Offset: 0x00044D49
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			writer.WriteAttributeValue("FieldIndex", this.Key);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00045D63 File Offset: 0x00044D63
		internal override void AddJsonProperties(JsonObject jsonPropertyDefinition, ExchangeService service)
		{
			base.AddJsonProperties(jsonPropertyDefinition, service);
			jsonPropertyDefinition.Add("FieldIndex", this.Key);
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00045D7E File Offset: 0x00044D7E
		internal override string GetPrintableName()
		{
			return string.Format("{0}:{1}", "distributionlist:Members:Member", this.Key);
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00045D95 File Offset: 0x00044D95
		public override Type Type
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x04001405 RID: 5125
		private const string FieldUri = "distributionlist:Members:Member";

		// Token: 0x04001406 RID: 5126
		private string key;
	}
}
