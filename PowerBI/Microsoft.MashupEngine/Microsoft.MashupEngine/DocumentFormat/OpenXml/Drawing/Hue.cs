using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026DE RID: 9950
	[GeneratedCode("DomGen", "2.0")]
	internal class Hue : OpenXmlLeafElement
	{
		// Token: 0x17005DBC RID: 23996
		// (get) Token: 0x06012FA3 RID: 77731 RVA: 0x00301766 File Offset: 0x002FF966
		public override string LocalName
		{
			get
			{
				return "hue";
			}
		}

		// Token: 0x17005DBD RID: 23997
		// (get) Token: 0x06012FA4 RID: 77732 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DBE RID: 23998
		// (get) Token: 0x06012FA5 RID: 77733 RVA: 0x0030176D File Offset: 0x002FF96D
		internal override int ElementTypeId
		{
			get
			{
				return 10014;
			}
		}

		// Token: 0x06012FA6 RID: 77734 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005DBF RID: 23999
		// (get) Token: 0x06012FA7 RID: 77735 RVA: 0x00301774 File Offset: 0x002FF974
		internal override string[] AttributeTagNames
		{
			get
			{
				return Hue.attributeTagNames;
			}
		}

		// Token: 0x17005DC0 RID: 24000
		// (get) Token: 0x06012FA8 RID: 77736 RVA: 0x0030177B File Offset: 0x002FF97B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Hue.attributeNamespaceIds;
			}
		}

		// Token: 0x17005DC1 RID: 24001
		// (get) Token: 0x06012FA9 RID: 77737 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06012FAA RID: 77738 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012FAC RID: 77740 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012FAD RID: 77741 RVA: 0x00301782 File Offset: 0x002FF982
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Hue>(deep);
		}

		// Token: 0x06012FAE RID: 77742 RVA: 0x0030178C File Offset: 0x002FF98C
		// Note: this type is marked as 'beforefieldinit'.
		static Hue()
		{
			byte[] array = new byte[1];
			Hue.attributeNamespaceIds = array;
		}

		// Token: 0x04008401 RID: 33793
		private const string tagName = "hue";

		// Token: 0x04008402 RID: 33794
		private const byte tagNsId = 10;

		// Token: 0x04008403 RID: 33795
		internal const int ElementTypeIdConst = 10014;

		// Token: 0x04008404 RID: 33796
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04008405 RID: 33797
		private static byte[] attributeNamespaceIds;
	}
}
