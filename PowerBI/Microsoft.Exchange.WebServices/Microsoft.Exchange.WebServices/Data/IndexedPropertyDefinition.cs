using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002D3 RID: 723
	public sealed class IndexedPropertyDefinition : ServiceObjectPropertyDefinition
	{
		// Token: 0x060019B2 RID: 6578 RVA: 0x00045DA1 File Offset: 0x00044DA1
		internal IndexedPropertyDefinition(string uri, string index)
			: base(uri)
		{
			this.index = index;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00045DB1 File Offset: 0x00044DB1
		internal static bool IsEqualTo(IndexedPropertyDefinition idxPropDef1, IndexedPropertyDefinition idxPropDef2)
		{
			return object.ReferenceEquals(idxPropDef1, idxPropDef2) || (idxPropDef1 != null && idxPropDef2 != null && idxPropDef1.Uri == idxPropDef2.Uri && idxPropDef1.Index == idxPropDef2.Index);
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x00045DEA File Offset: 0x00044DEA
		public string Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x00045DF2 File Offset: 0x00044DF2
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			writer.WriteAttributeValue("FieldIndex", this.Index);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00045E0C File Offset: 0x00044E0C
		internal override void AddJsonProperties(JsonObject jsonPropertyDefinition, ExchangeService service)
		{
			base.AddJsonProperties(jsonPropertyDefinition, service);
			jsonPropertyDefinition.Add("FieldIndex", this.Index);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00045E27 File Offset: 0x00044E27
		internal override string GetXmlElementName()
		{
			return "IndexedFieldURI";
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00045E2E File Offset: 0x00044E2E
		protected override string GetJsonType()
		{
			return "DictionaryPropertyUri";
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00045E35 File Offset: 0x00044E35
		internal override string GetPrintableName()
		{
			return string.Format("{0}:{1}", base.Uri, this.Index);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00045E4D File Offset: 0x00044E4D
		public static bool operator ==(IndexedPropertyDefinition idxPropDef1, IndexedPropertyDefinition idxPropDef2)
		{
			return IndexedPropertyDefinition.IsEqualTo(idxPropDef1, idxPropDef2);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00045E56 File Offset: 0x00044E56
		public static bool operator !=(IndexedPropertyDefinition idxPropDef1, IndexedPropertyDefinition idxPropDef2)
		{
			return !IndexedPropertyDefinition.IsEqualTo(idxPropDef1, idxPropDef2);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00045E64 File Offset: 0x00044E64
		public override bool Equals(object obj)
		{
			IndexedPropertyDefinition indexedPropertyDefinition = obj as IndexedPropertyDefinition;
			return IndexedPropertyDefinition.IsEqualTo(indexedPropertyDefinition, this);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00045E7F File Offset: 0x00044E7F
		public override int GetHashCode()
		{
			return base.Uri.GetHashCode() ^ this.Index.GetHashCode();
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x00045E98 File Offset: 0x00044E98
		public override Type Type
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x04001407 RID: 5127
		private string index;
	}
}
