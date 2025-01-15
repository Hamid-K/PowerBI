using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026DF RID: 9951
	[GeneratedCode("DomGen", "2.0")]
	internal class HueOffset : OpenXmlLeafElement
	{
		// Token: 0x17005DC2 RID: 24002
		// (get) Token: 0x06012FAF RID: 77743 RVA: 0x003017BB File Offset: 0x002FF9BB
		public override string LocalName
		{
			get
			{
				return "hueOff";
			}
		}

		// Token: 0x17005DC3 RID: 24003
		// (get) Token: 0x06012FB0 RID: 77744 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DC4 RID: 24004
		// (get) Token: 0x06012FB1 RID: 77745 RVA: 0x003017C2 File Offset: 0x002FF9C2
		internal override int ElementTypeId
		{
			get
			{
				return 10015;
			}
		}

		// Token: 0x06012FB2 RID: 77746 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005DC5 RID: 24005
		// (get) Token: 0x06012FB3 RID: 77747 RVA: 0x003017C9 File Offset: 0x002FF9C9
		internal override string[] AttributeTagNames
		{
			get
			{
				return HueOffset.attributeTagNames;
			}
		}

		// Token: 0x17005DC6 RID: 24006
		// (get) Token: 0x06012FB4 RID: 77748 RVA: 0x003017D0 File Offset: 0x002FF9D0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HueOffset.attributeNamespaceIds;
			}
		}

		// Token: 0x17005DC7 RID: 24007
		// (get) Token: 0x06012FB5 RID: 77749 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06012FB6 RID: 77750 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
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

		// Token: 0x06012FB8 RID: 77752 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012FB9 RID: 77753 RVA: 0x003017D7 File Offset: 0x002FF9D7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HueOffset>(deep);
		}

		// Token: 0x06012FBA RID: 77754 RVA: 0x003017E0 File Offset: 0x002FF9E0
		// Note: this type is marked as 'beforefieldinit'.
		static HueOffset()
		{
			byte[] array = new byte[1];
			HueOffset.attributeNamespaceIds = array;
		}

		// Token: 0x04008406 RID: 33798
		private const string tagName = "hueOff";

		// Token: 0x04008407 RID: 33799
		private const byte tagNsId = 10;

		// Token: 0x04008408 RID: 33800
		internal const int ElementTypeIdConst = 10015;

		// Token: 0x04008409 RID: 33801
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400840A RID: 33802
		private static byte[] attributeNamespaceIds;
	}
}
