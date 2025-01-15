using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002724 RID: 10020
	[GeneratedCode("DomGen", "2.0")]
	internal class TransformEffect : OpenXmlLeafElement
	{
		// Token: 0x17005FAA RID: 24490
		// (get) Token: 0x060133C3 RID: 78787 RVA: 0x002E002B File Offset: 0x002DE22B
		public override string LocalName
		{
			get
			{
				return "xfrm";
			}
		}

		// Token: 0x17005FAB RID: 24491
		// (get) Token: 0x060133C4 RID: 78788 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005FAC RID: 24492
		// (get) Token: 0x060133C5 RID: 78789 RVA: 0x003052FF File Offset: 0x003034FF
		internal override int ElementTypeId
		{
			get
			{
				return 10082;
			}
		}

		// Token: 0x060133C6 RID: 78790 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005FAD RID: 24493
		// (get) Token: 0x060133C7 RID: 78791 RVA: 0x00305306 File Offset: 0x00303506
		internal override string[] AttributeTagNames
		{
			get
			{
				return TransformEffect.attributeTagNames;
			}
		}

		// Token: 0x17005FAE RID: 24494
		// (get) Token: 0x060133C8 RID: 78792 RVA: 0x0030530D File Offset: 0x0030350D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TransformEffect.attributeNamespaceIds;
			}
		}

		// Token: 0x17005FAF RID: 24495
		// (get) Token: 0x060133C9 RID: 78793 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060133CA RID: 78794 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "sx")]
		public Int32Value HorizontalRatio
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

		// Token: 0x17005FB0 RID: 24496
		// (get) Token: 0x060133CB RID: 78795 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060133CC RID: 78796 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sy")]
		public Int32Value VerticalRatio
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

		// Token: 0x17005FB1 RID: 24497
		// (get) Token: 0x060133CD RID: 78797 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x060133CE RID: 78798 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "kx")]
		public Int32Value HorizontalSkew
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

		// Token: 0x17005FB2 RID: 24498
		// (get) Token: 0x060133CF RID: 78799 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x060133D0 RID: 78800 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "ky")]
		public Int32Value VerticalSkew
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17005FB3 RID: 24499
		// (get) Token: 0x060133D1 RID: 78801 RVA: 0x00305314 File Offset: 0x00303514
		// (set) Token: 0x060133D2 RID: 78802 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "tx")]
		public Int64Value HorizontalShift
		{
			get
			{
				return (Int64Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17005FB4 RID: 24500
		// (get) Token: 0x060133D3 RID: 78803 RVA: 0x002ED54C File Offset: 0x002EB74C
		// (set) Token: 0x060133D4 RID: 78804 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "ty")]
		public Int64Value VerticalShift
		{
			get
			{
				return (Int64Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x060133D6 RID: 78806 RVA: 0x00305324 File Offset: 0x00303524
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "sx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "sy" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "kx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "ky" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "tx" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "ty" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060133D7 RID: 78807 RVA: 0x003053BD File Offset: 0x003035BD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TransformEffect>(deep);
		}

		// Token: 0x060133D8 RID: 78808 RVA: 0x003053C8 File Offset: 0x003035C8
		// Note: this type is marked as 'beforefieldinit'.
		static TransformEffect()
		{
			byte[] array = new byte[6];
			TransformEffect.attributeNamespaceIds = array;
		}

		// Token: 0x0400853F RID: 34111
		private const string tagName = "xfrm";

		// Token: 0x04008540 RID: 34112
		private const byte tagNsId = 10;

		// Token: 0x04008541 RID: 34113
		internal const int ElementTypeIdConst = 10082;

		// Token: 0x04008542 RID: 34114
		private static string[] attributeTagNames = new string[] { "sx", "sy", "kx", "ky", "tx", "ty" };

		// Token: 0x04008543 RID: 34115
		private static byte[] attributeNamespaceIds;
	}
}
