using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002479 RID: 9337
	[GeneratedCode("DomGen", "2.0")]
	internal class ToolbarData : OpenXmlLeafElement
	{
		// Token: 0x1700512E RID: 20782
		// (get) Token: 0x060113A4 RID: 70564 RVA: 0x002EBE64 File Offset: 0x002EA064
		public override string LocalName
		{
			get
			{
				return "toolbarData";
			}
		}

		// Token: 0x1700512F RID: 20783
		// (get) Token: 0x060113A5 RID: 70565 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x17005130 RID: 20784
		// (get) Token: 0x060113A6 RID: 70566 RVA: 0x002EBE6B File Offset: 0x002EA06B
		internal override int ElementTypeId
		{
			get
			{
				return 12565;
			}
		}

		// Token: 0x060113A7 RID: 70567 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005131 RID: 20785
		// (get) Token: 0x060113A8 RID: 70568 RVA: 0x002EBE72 File Offset: 0x002EA072
		internal override string[] AttributeTagNames
		{
			get
			{
				return ToolbarData.attributeTagNames;
			}
		}

		// Token: 0x17005132 RID: 20786
		// (get) Token: 0x060113A9 RID: 70569 RVA: 0x002EBE79 File Offset: 0x002EA079
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ToolbarData.attributeNamespaceIds;
			}
		}

		// Token: 0x17005133 RID: 20787
		// (get) Token: 0x060113AA RID: 70570 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060113AB RID: 70571 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060113AD RID: 70573 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060113AE RID: 70574 RVA: 0x002EBE80 File Offset: 0x002EA080
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ToolbarData>(deep);
		}

		// Token: 0x040078C1 RID: 30913
		private const string tagName = "toolbarData";

		// Token: 0x040078C2 RID: 30914
		private const byte tagNsId = 33;

		// Token: 0x040078C3 RID: 30915
		internal const int ElementTypeIdConst = 12565;

		// Token: 0x040078C4 RID: 30916
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x040078C5 RID: 30917
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
