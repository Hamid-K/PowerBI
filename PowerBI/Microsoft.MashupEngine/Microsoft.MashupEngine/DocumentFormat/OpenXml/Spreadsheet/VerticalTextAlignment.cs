using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BA0 RID: 11168
	[GeneratedCode("DomGen", "2.0")]
	internal class VerticalTextAlignment : OpenXmlLeafElement
	{
		// Token: 0x17007B3F RID: 31551
		// (get) Token: 0x0601727C RID: 94844 RVA: 0x003334AB File Offset: 0x003316AB
		public override string LocalName
		{
			get
			{
				return "vertAlign";
			}
		}

		// Token: 0x17007B40 RID: 31552
		// (get) Token: 0x0601727D RID: 94845 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B41 RID: 31553
		// (get) Token: 0x0601727E RID: 94846 RVA: 0x003334B2 File Offset: 0x003316B2
		internal override int ElementTypeId
		{
			get
			{
				return 11143;
			}
		}

		// Token: 0x0601727F RID: 94847 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007B42 RID: 31554
		// (get) Token: 0x06017280 RID: 94848 RVA: 0x003334B9 File Offset: 0x003316B9
		internal override string[] AttributeTagNames
		{
			get
			{
				return VerticalTextAlignment.attributeTagNames;
			}
		}

		// Token: 0x17007B43 RID: 31555
		// (get) Token: 0x06017281 RID: 94849 RVA: 0x003334C0 File Offset: 0x003316C0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VerticalTextAlignment.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B44 RID: 31556
		// (get) Token: 0x06017282 RID: 94850 RVA: 0x003334C7 File Offset: 0x003316C7
		// (set) Token: 0x06017283 RID: 94851 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<VerticalAlignmentRunValues> Val
		{
			get
			{
				return (EnumValue<VerticalAlignmentRunValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06017285 RID: 94853 RVA: 0x003334D6 File Offset: 0x003316D6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<VerticalAlignmentRunValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017286 RID: 94854 RVA: 0x003334F6 File Offset: 0x003316F6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VerticalTextAlignment>(deep);
		}

		// Token: 0x06017287 RID: 94855 RVA: 0x00333500 File Offset: 0x00331700
		// Note: this type is marked as 'beforefieldinit'.
		static VerticalTextAlignment()
		{
			byte[] array = new byte[1];
			VerticalTextAlignment.attributeNamespaceIds = array;
		}

		// Token: 0x04009B51 RID: 39761
		private const string tagName = "vertAlign";

		// Token: 0x04009B52 RID: 39762
		private const byte tagNsId = 22;

		// Token: 0x04009B53 RID: 39763
		internal const int ElementTypeIdConst = 11143;

		// Token: 0x04009B54 RID: 39764
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009B55 RID: 39765
		private static byte[] attributeNamespaceIds;
	}
}
