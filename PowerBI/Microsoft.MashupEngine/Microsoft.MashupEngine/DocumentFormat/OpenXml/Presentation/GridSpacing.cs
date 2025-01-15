using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A8E RID: 10894
	[GeneratedCode("DomGen", "2.0")]
	internal class GridSpacing : OpenXmlLeafElement
	{
		// Token: 0x170073BF RID: 29631
		// (get) Token: 0x0601618F RID: 90511 RVA: 0x00326839 File Offset: 0x00324A39
		public override string LocalName
		{
			get
			{
				return "gridSpacing";
			}
		}

		// Token: 0x170073C0 RID: 29632
		// (get) Token: 0x06016190 RID: 90512 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170073C1 RID: 29633
		// (get) Token: 0x06016191 RID: 90513 RVA: 0x00326840 File Offset: 0x00324A40
		internal override int ElementTypeId
		{
			get
			{
				return 12307;
			}
		}

		// Token: 0x06016192 RID: 90514 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170073C2 RID: 29634
		// (get) Token: 0x06016193 RID: 90515 RVA: 0x00326847 File Offset: 0x00324A47
		internal override string[] AttributeTagNames
		{
			get
			{
				return GridSpacing.attributeTagNames;
			}
		}

		// Token: 0x170073C3 RID: 29635
		// (get) Token: 0x06016194 RID: 90516 RVA: 0x0032684E File Offset: 0x00324A4E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GridSpacing.attributeNamespaceIds;
			}
		}

		// Token: 0x170073C4 RID: 29636
		// (get) Token: 0x06016195 RID: 90517 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06016196 RID: 90518 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "cx")]
		public Int64Value Cx
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170073C5 RID: 29637
		// (get) Token: 0x06016197 RID: 90519 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x06016198 RID: 90520 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cy")]
		public Int64Value Cy
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601619A RID: 90522 RVA: 0x002FCAAF File Offset: 0x002FACAF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "cx" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "cy" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601619B RID: 90523 RVA: 0x00326855 File Offset: 0x00324A55
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GridSpacing>(deep);
		}

		// Token: 0x0601619C RID: 90524 RVA: 0x00326860 File Offset: 0x00324A60
		// Note: this type is marked as 'beforefieldinit'.
		static GridSpacing()
		{
			byte[] array = new byte[2];
			GridSpacing.attributeNamespaceIds = array;
		}

		// Token: 0x04009631 RID: 38449
		private const string tagName = "gridSpacing";

		// Token: 0x04009632 RID: 38450
		private const byte tagNsId = 24;

		// Token: 0x04009633 RID: 38451
		internal const int ElementTypeIdConst = 12307;

		// Token: 0x04009634 RID: 38452
		private static string[] attributeTagNames = new string[] { "cx", "cy" };

		// Token: 0x04009635 RID: 38453
		private static byte[] attributeNamespaceIds;
	}
}
