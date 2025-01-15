using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x0200248E RID: 9358
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class HueModulation : OpenXmlLeafElement
	{
		// Token: 0x1700517C RID: 20860
		// (get) Token: 0x06011462 RID: 70754 RVA: 0x002EC9C5 File Offset: 0x002EABC5
		public override string LocalName
		{
			get
			{
				return "hueMod";
			}
		}

		// Token: 0x1700517D RID: 20861
		// (get) Token: 0x06011463 RID: 70755 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700517E RID: 20862
		// (get) Token: 0x06011464 RID: 70756 RVA: 0x002EC9CC File Offset: 0x002EABCC
		internal override int ElementTypeId
		{
			get
			{
				return 12835;
			}
		}

		// Token: 0x06011465 RID: 70757 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700517F RID: 20863
		// (get) Token: 0x06011466 RID: 70758 RVA: 0x002EC9D3 File Offset: 0x002EABD3
		internal override string[] AttributeTagNames
		{
			get
			{
				return HueModulation.attributeTagNames;
			}
		}

		// Token: 0x17005180 RID: 20864
		// (get) Token: 0x06011467 RID: 70759 RVA: 0x002EC9DA File Offset: 0x002EABDA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HueModulation.attributeNamespaceIds;
			}
		}

		// Token: 0x17005181 RID: 20865
		// (get) Token: 0x06011468 RID: 70760 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06011469 RID: 70761 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "val")]
		public Int32Value Val
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601146B RID: 70763 RVA: 0x002EC920 File Offset: 0x002EAB20
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601146C RID: 70764 RVA: 0x002EC9E1 File Offset: 0x002EABE1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HueModulation>(deep);
		}

		// Token: 0x04007902 RID: 30978
		private const string tagName = "hueMod";

		// Token: 0x04007903 RID: 30979
		private const byte tagNsId = 52;

		// Token: 0x04007904 RID: 30980
		internal const int ElementTypeIdConst = 12835;

		// Token: 0x04007905 RID: 30981
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007906 RID: 30982
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
