using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027C4 RID: 10180
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeGuide : OpenXmlLeafElement
	{
		// Token: 0x17006378 RID: 25464
		// (get) Token: 0x06013C62 RID: 80994 RVA: 0x0030B939 File Offset: 0x00309B39
		public override string LocalName
		{
			get
			{
				return "gd";
			}
		}

		// Token: 0x17006379 RID: 25465
		// (get) Token: 0x06013C63 RID: 80995 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700637A RID: 25466
		// (get) Token: 0x06013C64 RID: 80996 RVA: 0x0030B940 File Offset: 0x00309B40
		internal override int ElementTypeId
		{
			get
			{
				return 10215;
			}
		}

		// Token: 0x06013C65 RID: 80997 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700637B RID: 25467
		// (get) Token: 0x06013C66 RID: 80998 RVA: 0x0030B947 File Offset: 0x00309B47
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeGuide.attributeTagNames;
			}
		}

		// Token: 0x1700637C RID: 25468
		// (get) Token: 0x06013C67 RID: 80999 RVA: 0x0030B94E File Offset: 0x00309B4E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeGuide.attributeNamespaceIds;
			}
		}

		// Token: 0x1700637D RID: 25469
		// (get) Token: 0x06013C68 RID: 81000 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013C69 RID: 81001 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x1700637E RID: 25470
		// (get) Token: 0x06013C6A RID: 81002 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06013C6B RID: 81003 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fmla")]
		public StringValue Formula
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06013C6D RID: 81005 RVA: 0x0030B955 File Offset: 0x00309B55
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fmla" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013C6E RID: 81006 RVA: 0x0030B98B File Offset: 0x00309B8B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeGuide>(deep);
		}

		// Token: 0x06013C6F RID: 81007 RVA: 0x0030B994 File Offset: 0x00309B94
		// Note: this type is marked as 'beforefieldinit'.
		static ShapeGuide()
		{
			byte[] array = new byte[2];
			ShapeGuide.attributeNamespaceIds = array;
		}

		// Token: 0x040087B7 RID: 34743
		private const string tagName = "gd";

		// Token: 0x040087B8 RID: 34744
		private const byte tagNsId = 10;

		// Token: 0x040087B9 RID: 34745
		internal const int ElementTypeIdConst = 10215;

		// Token: 0x040087BA RID: 34746
		private static string[] attributeTagNames = new string[] { "name", "fmla" };

		// Token: 0x040087BB RID: 34747
		private static byte[] attributeNamespaceIds;
	}
}
