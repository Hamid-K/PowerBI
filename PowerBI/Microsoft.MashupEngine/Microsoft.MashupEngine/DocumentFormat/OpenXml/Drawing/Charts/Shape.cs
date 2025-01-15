using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025DD RID: 9693
	[GeneratedCode("DomGen", "2.0")]
	internal class Shape : OpenXmlLeafElement
	{
		// Token: 0x1700586B RID: 22635
		// (get) Token: 0x060123B8 RID: 74680 RVA: 0x002C1364 File Offset: 0x002BF564
		public override string LocalName
		{
			get
			{
				return "shape";
			}
		}

		// Token: 0x1700586C RID: 22636
		// (get) Token: 0x060123B9 RID: 74681 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700586D RID: 22637
		// (get) Token: 0x060123BA RID: 74682 RVA: 0x002F7A93 File Offset: 0x002F5C93
		internal override int ElementTypeId
		{
			get
			{
				return 10536;
			}
		}

		// Token: 0x060123BB RID: 74683 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700586E RID: 22638
		// (get) Token: 0x060123BC RID: 74684 RVA: 0x002F7A9A File Offset: 0x002F5C9A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shape.attributeTagNames;
			}
		}

		// Token: 0x1700586F RID: 22639
		// (get) Token: 0x060123BD RID: 74685 RVA: 0x002F7AA1 File Offset: 0x002F5CA1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shape.attributeNamespaceIds;
			}
		}

		// Token: 0x17005870 RID: 22640
		// (get) Token: 0x060123BE RID: 74686 RVA: 0x002F7AA8 File Offset: 0x002F5CA8
		// (set) Token: 0x060123BF RID: 74687 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<ShapeValues> Val
		{
			get
			{
				return (EnumValue<ShapeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060123C1 RID: 74689 RVA: 0x002F7AB7 File Offset: 0x002F5CB7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<ShapeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060123C2 RID: 74690 RVA: 0x002F7AD7 File Offset: 0x002F5CD7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shape>(deep);
		}

		// Token: 0x060123C3 RID: 74691 RVA: 0x002F7AE0 File Offset: 0x002F5CE0
		// Note: this type is marked as 'beforefieldinit'.
		static Shape()
		{
			byte[] array = new byte[1];
			Shape.attributeNamespaceIds = array;
		}

		// Token: 0x04007EC0 RID: 32448
		private const string tagName = "shape";

		// Token: 0x04007EC1 RID: 32449
		private const byte tagNsId = 11;

		// Token: 0x04007EC2 RID: 32450
		internal const int ElementTypeIdConst = 10536;

		// Token: 0x04007EC3 RID: 32451
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007EC4 RID: 32452
		private static byte[] attributeNamespaceIds;
	}
}
