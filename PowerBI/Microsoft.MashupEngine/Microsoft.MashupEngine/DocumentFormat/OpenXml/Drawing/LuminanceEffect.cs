using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200271D RID: 10013
	[GeneratedCode("DomGen", "2.0")]
	internal class LuminanceEffect : OpenXmlLeafElement
	{
		// Token: 0x17005F54 RID: 24404
		// (get) Token: 0x06013315 RID: 78613 RVA: 0x002ECAB1 File Offset: 0x002EACB1
		public override string LocalName
		{
			get
			{
				return "lum";
			}
		}

		// Token: 0x17005F55 RID: 24405
		// (get) Token: 0x06013316 RID: 78614 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F56 RID: 24406
		// (get) Token: 0x06013317 RID: 78615 RVA: 0x00304AD2 File Offset: 0x00302CD2
		internal override int ElementTypeId
		{
			get
			{
				return 10075;
			}
		}

		// Token: 0x06013318 RID: 78616 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005F57 RID: 24407
		// (get) Token: 0x06013319 RID: 78617 RVA: 0x00304AD9 File Offset: 0x00302CD9
		internal override string[] AttributeTagNames
		{
			get
			{
				return LuminanceEffect.attributeTagNames;
			}
		}

		// Token: 0x17005F58 RID: 24408
		// (get) Token: 0x0601331A RID: 78618 RVA: 0x00304AE0 File Offset: 0x00302CE0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LuminanceEffect.attributeNamespaceIds;
			}
		}

		// Token: 0x17005F59 RID: 24409
		// (get) Token: 0x0601331B RID: 78619 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601331C RID: 78620 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "bright")]
		public Int32Value Brightness
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

		// Token: 0x17005F5A RID: 24410
		// (get) Token: 0x0601331D RID: 78621 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601331E RID: 78622 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "contrast")]
		public Int32Value Contrast
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

		// Token: 0x06013320 RID: 78624 RVA: 0x002E23FB File Offset: 0x002E05FB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bright" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "contrast" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013321 RID: 78625 RVA: 0x00304AE7 File Offset: 0x00302CE7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LuminanceEffect>(deep);
		}

		// Token: 0x06013322 RID: 78626 RVA: 0x00304AF0 File Offset: 0x00302CF0
		// Note: this type is marked as 'beforefieldinit'.
		static LuminanceEffect()
		{
			byte[] array = new byte[2];
			LuminanceEffect.attributeNamespaceIds = array;
		}

		// Token: 0x04008518 RID: 34072
		private const string tagName = "lum";

		// Token: 0x04008519 RID: 34073
		private const byte tagNsId = 10;

		// Token: 0x0400851A RID: 34074
		internal const int ElementTypeIdConst = 10075;

		// Token: 0x0400851B RID: 34075
		private static string[] attributeTagNames = new string[] { "bright", "contrast" };

		// Token: 0x0400851C RID: 34076
		private static byte[] attributeNamespaceIds;
	}
}
