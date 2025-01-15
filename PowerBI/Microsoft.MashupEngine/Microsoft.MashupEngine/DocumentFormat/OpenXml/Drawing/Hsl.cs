using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200271B RID: 10011
	[GeneratedCode("DomGen", "2.0")]
	internal class Hsl : OpenXmlLeafElement
	{
		// Token: 0x17005F3B RID: 24379
		// (get) Token: 0x060132E2 RID: 78562 RVA: 0x00304859 File Offset: 0x00302A59
		public override string LocalName
		{
			get
			{
				return "hsl";
			}
		}

		// Token: 0x17005F3C RID: 24380
		// (get) Token: 0x060132E3 RID: 78563 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F3D RID: 24381
		// (get) Token: 0x060132E4 RID: 78564 RVA: 0x00304860 File Offset: 0x00302A60
		internal override int ElementTypeId
		{
			get
			{
				return 10073;
			}
		}

		// Token: 0x060132E5 RID: 78565 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005F3E RID: 24382
		// (get) Token: 0x060132E6 RID: 78566 RVA: 0x00304867 File Offset: 0x00302A67
		internal override string[] AttributeTagNames
		{
			get
			{
				return Hsl.attributeTagNames;
			}
		}

		// Token: 0x17005F3F RID: 24383
		// (get) Token: 0x060132E7 RID: 78567 RVA: 0x0030486E File Offset: 0x00302A6E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Hsl.attributeNamespaceIds;
			}
		}

		// Token: 0x17005F40 RID: 24384
		// (get) Token: 0x060132E8 RID: 78568 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060132E9 RID: 78569 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "hue")]
		public Int32Value Hue
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

		// Token: 0x17005F41 RID: 24385
		// (get) Token: 0x060132EA RID: 78570 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060132EB RID: 78571 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sat")]
		public Int32Value Saturation
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005F42 RID: 24386
		// (get) Token: 0x060132EC RID: 78572 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x060132ED RID: 78573 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "lum")]
		public Int32Value Luminance
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060132EF RID: 78575 RVA: 0x00304878 File Offset: 0x00302A78
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "hue" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "sat" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "lum" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060132F0 RID: 78576 RVA: 0x003048CF File Offset: 0x00302ACF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Hsl>(deep);
		}

		// Token: 0x060132F1 RID: 78577 RVA: 0x003048D8 File Offset: 0x00302AD8
		// Note: this type is marked as 'beforefieldinit'.
		static Hsl()
		{
			byte[] array = new byte[3];
			Hsl.attributeNamespaceIds = array;
		}

		// Token: 0x0400850C RID: 34060
		private const string tagName = "hsl";

		// Token: 0x0400850D RID: 34061
		private const byte tagNsId = 10;

		// Token: 0x0400850E RID: 34062
		internal const int ElementTypeIdConst = 10073;

		// Token: 0x0400850F RID: 34063
		private static string[] attributeTagNames = new string[] { "hue", "sat", "lum" };

		// Token: 0x04008510 RID: 34064
		private static byte[] attributeNamespaceIds;
	}
}
