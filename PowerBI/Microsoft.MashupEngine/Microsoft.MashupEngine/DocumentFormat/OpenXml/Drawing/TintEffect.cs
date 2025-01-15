using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002723 RID: 10019
	[GeneratedCode("DomGen", "2.0")]
	internal class TintEffect : OpenXmlLeafElement
	{
		// Token: 0x17005FA3 RID: 24483
		// (get) Token: 0x060133B5 RID: 78773 RVA: 0x002EC978 File Offset: 0x002EAB78
		public override string LocalName
		{
			get
			{
				return "tint";
			}
		}

		// Token: 0x17005FA4 RID: 24484
		// (get) Token: 0x060133B6 RID: 78774 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005FA5 RID: 24485
		// (get) Token: 0x060133B7 RID: 78775 RVA: 0x00305273 File Offset: 0x00303473
		internal override int ElementTypeId
		{
			get
			{
				return 10081;
			}
		}

		// Token: 0x060133B8 RID: 78776 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005FA6 RID: 24486
		// (get) Token: 0x060133B9 RID: 78777 RVA: 0x0030527A File Offset: 0x0030347A
		internal override string[] AttributeTagNames
		{
			get
			{
				return TintEffect.attributeTagNames;
			}
		}

		// Token: 0x17005FA7 RID: 24487
		// (get) Token: 0x060133BA RID: 78778 RVA: 0x00305281 File Offset: 0x00303481
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TintEffect.attributeNamespaceIds;
			}
		}

		// Token: 0x17005FA8 RID: 24488
		// (get) Token: 0x060133BB RID: 78779 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060133BC RID: 78780 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005FA9 RID: 24489
		// (get) Token: 0x060133BD RID: 78781 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060133BE RID: 78782 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "amt")]
		public Int32Value Amount
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

		// Token: 0x060133C0 RID: 78784 RVA: 0x00305288 File Offset: 0x00303488
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "hue" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "amt" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060133C1 RID: 78785 RVA: 0x003052BE File Offset: 0x003034BE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TintEffect>(deep);
		}

		// Token: 0x060133C2 RID: 78786 RVA: 0x003052C8 File Offset: 0x003034C8
		// Note: this type is marked as 'beforefieldinit'.
		static TintEffect()
		{
			byte[] array = new byte[2];
			TintEffect.attributeNamespaceIds = array;
		}

		// Token: 0x0400853A RID: 34106
		private const string tagName = "tint";

		// Token: 0x0400853B RID: 34107
		private const byte tagNsId = 10;

		// Token: 0x0400853C RID: 34108
		internal const int ElementTypeIdConst = 10081;

		// Token: 0x0400853D RID: 34109
		private static string[] attributeTagNames = new string[] { "hue", "amt" };

		// Token: 0x0400853E RID: 34110
		private static byte[] attributeNamespaceIds;
	}
}
