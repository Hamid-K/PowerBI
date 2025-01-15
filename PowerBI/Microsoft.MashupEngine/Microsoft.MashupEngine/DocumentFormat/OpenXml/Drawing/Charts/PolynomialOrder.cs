using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002598 RID: 9624
	[GeneratedCode("DomGen", "2.0")]
	internal class PolynomialOrder : OpenXmlLeafElement
	{
		// Token: 0x170056B2 RID: 22194
		// (get) Token: 0x06011FE9 RID: 73705 RVA: 0x002F1956 File Offset: 0x002EFB56
		public override string LocalName
		{
			get
			{
				return "order";
			}
		}

		// Token: 0x170056B3 RID: 22195
		// (get) Token: 0x06011FEA RID: 73706 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056B4 RID: 22196
		// (get) Token: 0x06011FEB RID: 73707 RVA: 0x002F4977 File Offset: 0x002F2B77
		internal override int ElementTypeId
		{
			get
			{
				return 10438;
			}
		}

		// Token: 0x06011FEC RID: 73708 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170056B5 RID: 22197
		// (get) Token: 0x06011FED RID: 73709 RVA: 0x002F497E File Offset: 0x002F2B7E
		internal override string[] AttributeTagNames
		{
			get
			{
				return PolynomialOrder.attributeTagNames;
			}
		}

		// Token: 0x170056B6 RID: 22198
		// (get) Token: 0x06011FEE RID: 73710 RVA: 0x002F4985 File Offset: 0x002F2B85
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PolynomialOrder.attributeNamespaceIds;
			}
		}

		// Token: 0x170056B7 RID: 22199
		// (get) Token: 0x06011FEF RID: 73711 RVA: 0x002DE388 File Offset: 0x002DC588
		// (set) Token: 0x06011FF0 RID: 73712 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public ByteValue Val
		{
			get
			{
				return (ByteValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011FF2 RID: 73714 RVA: 0x002DE397 File Offset: 0x002DC597
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new ByteValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011FF3 RID: 73715 RVA: 0x002F498C File Offset: 0x002F2B8C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PolynomialOrder>(deep);
		}

		// Token: 0x06011FF4 RID: 73716 RVA: 0x002F4998 File Offset: 0x002F2B98
		// Note: this type is marked as 'beforefieldinit'.
		static PolynomialOrder()
		{
			byte[] array = new byte[1];
			PolynomialOrder.attributeNamespaceIds = array;
		}

		// Token: 0x04007DA2 RID: 32162
		private const string tagName = "order";

		// Token: 0x04007DA3 RID: 32163
		private const byte tagNsId = 11;

		// Token: 0x04007DA4 RID: 32164
		internal const int ElementTypeIdConst = 10438;

		// Token: 0x04007DA5 RID: 32165
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007DA6 RID: 32166
		private static byte[] attributeNamespaceIds;
	}
}
