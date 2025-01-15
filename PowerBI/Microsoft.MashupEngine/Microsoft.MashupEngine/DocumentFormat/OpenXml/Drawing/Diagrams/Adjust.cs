using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002666 RID: 9830
	[GeneratedCode("DomGen", "2.0")]
	internal class Adjust : OpenXmlLeafElement
	{
		// Token: 0x17005BE2 RID: 23522
		// (get) Token: 0x06012B8D RID: 76685 RVA: 0x002FE7B6 File Offset: 0x002FC9B6
		public override string LocalName
		{
			get
			{
				return "adj";
			}
		}

		// Token: 0x17005BE3 RID: 23523
		// (get) Token: 0x06012B8E RID: 76686 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005BE4 RID: 23524
		// (get) Token: 0x06012B8F RID: 76687 RVA: 0x002FE7BD File Offset: 0x002FC9BD
		internal override int ElementTypeId
		{
			get
			{
				return 10647;
			}
		}

		// Token: 0x06012B90 RID: 76688 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005BE5 RID: 23525
		// (get) Token: 0x06012B91 RID: 76689 RVA: 0x002FE7C4 File Offset: 0x002FC9C4
		internal override string[] AttributeTagNames
		{
			get
			{
				return Adjust.attributeTagNames;
			}
		}

		// Token: 0x17005BE6 RID: 23526
		// (get) Token: 0x06012B92 RID: 76690 RVA: 0x002FE7CB File Offset: 0x002FC9CB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Adjust.attributeNamespaceIds;
			}
		}

		// Token: 0x17005BE7 RID: 23527
		// (get) Token: 0x06012B93 RID: 76691 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06012B94 RID: 76692 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "idx")]
		public UInt32Value Index
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005BE8 RID: 23528
		// (get) Token: 0x06012B95 RID: 76693 RVA: 0x002E7DD4 File Offset: 0x002E5FD4
		// (set) Token: 0x06012B96 RID: 76694 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "val")]
		public DoubleValue Val
		{
			get
			{
				return (DoubleValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06012B98 RID: 76696 RVA: 0x002FE7D2 File Offset: 0x002FC9D2
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "idx" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012B99 RID: 76697 RVA: 0x002FE808 File Offset: 0x002FCA08
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Adjust>(deep);
		}

		// Token: 0x06012B9A RID: 76698 RVA: 0x002FE814 File Offset: 0x002FCA14
		// Note: this type is marked as 'beforefieldinit'.
		static Adjust()
		{
			byte[] array = new byte[2];
			Adjust.attributeNamespaceIds = array;
		}

		// Token: 0x04008156 RID: 33110
		private const string tagName = "adj";

		// Token: 0x04008157 RID: 33111
		private const byte tagNsId = 14;

		// Token: 0x04008158 RID: 33112
		internal const int ElementTypeIdConst = 10647;

		// Token: 0x04008159 RID: 33113
		private static string[] attributeTagNames = new string[] { "idx", "val" };

		// Token: 0x0400815A RID: 33114
		private static byte[] attributeNamespaceIds;
	}
}
