using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025FF RID: 9727
	[GeneratedCode("DomGen", "2.0")]
	internal class Style : OpenXmlLeafElement
	{
		// Token: 0x17005988 RID: 22920
		// (get) Token: 0x0601262D RID: 75309 RVA: 0x002DE36C File Offset: 0x002DC56C
		public override string LocalName
		{
			get
			{
				return "style";
			}
		}

		// Token: 0x17005989 RID: 22921
		// (get) Token: 0x0601262E RID: 75310 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700598A RID: 22922
		// (get) Token: 0x0601262F RID: 75311 RVA: 0x002FA5FB File Offset: 0x002F87FB
		internal override int ElementTypeId
		{
			get
			{
				return 10574;
			}
		}

		// Token: 0x06012630 RID: 75312 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700598B RID: 22923
		// (get) Token: 0x06012631 RID: 75313 RVA: 0x002FA602 File Offset: 0x002F8802
		internal override string[] AttributeTagNames
		{
			get
			{
				return Style.attributeTagNames;
			}
		}

		// Token: 0x1700598C RID: 22924
		// (get) Token: 0x06012632 RID: 75314 RVA: 0x002FA609 File Offset: 0x002F8809
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Style.attributeNamespaceIds;
			}
		}

		// Token: 0x1700598D RID: 22925
		// (get) Token: 0x06012633 RID: 75315 RVA: 0x002DE388 File Offset: 0x002DC588
		// (set) Token: 0x06012634 RID: 75316 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012636 RID: 75318 RVA: 0x002DE397 File Offset: 0x002DC597
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new ByteValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012637 RID: 75319 RVA: 0x002FA610 File Offset: 0x002F8810
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Style>(deep);
		}

		// Token: 0x06012638 RID: 75320 RVA: 0x002FA61C File Offset: 0x002F881C
		// Note: this type is marked as 'beforefieldinit'.
		static Style()
		{
			byte[] array = new byte[1];
			Style.attributeNamespaceIds = array;
		}

		// Token: 0x04007F5F RID: 32607
		private const string tagName = "style";

		// Token: 0x04007F60 RID: 32608
		private const byte tagNsId = 11;

		// Token: 0x04007F61 RID: 32609
		internal const int ElementTypeIdConst = 10574;

		// Token: 0x04007F62 RID: 32610
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007F63 RID: 32611
		private static byte[] attributeNamespaceIds;
	}
}
