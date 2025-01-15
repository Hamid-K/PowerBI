using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002892 RID: 10386
	[GeneratedCode("DomGen", "2.0")]
	internal class Position : OpenXmlLeafElement
	{
		// Token: 0x170067AD RID: 26541
		// (get) Token: 0x06014644 RID: 83524 RVA: 0x0030BA47 File Offset: 0x00309C47
		public override string LocalName
		{
			get
			{
				return "pos";
			}
		}

		// Token: 0x170067AE RID: 26542
		// (get) Token: 0x06014645 RID: 83525 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170067AF RID: 26543
		// (get) Token: 0x06014646 RID: 83526 RVA: 0x00312C6F File Offset: 0x00310E6F
		internal override int ElementTypeId
		{
			get
			{
				return 10747;
			}
		}

		// Token: 0x06014647 RID: 83527 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170067B0 RID: 26544
		// (get) Token: 0x06014648 RID: 83528 RVA: 0x00312C76 File Offset: 0x00310E76
		internal override string[] AttributeTagNames
		{
			get
			{
				return Position.attributeTagNames;
			}
		}

		// Token: 0x170067B1 RID: 26545
		// (get) Token: 0x06014649 RID: 83529 RVA: 0x00312C7D File Offset: 0x00310E7D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Position.attributeNamespaceIds;
			}
		}

		// Token: 0x170067B2 RID: 26546
		// (get) Token: 0x0601464A RID: 83530 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x0601464B RID: 83531 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "x")]
		public Int64Value X
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

		// Token: 0x170067B3 RID: 26547
		// (get) Token: 0x0601464C RID: 83532 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x0601464D RID: 83533 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "y")]
		public Int64Value Y
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

		// Token: 0x0601464F RID: 83535 RVA: 0x00308403 File Offset: 0x00306603
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "x" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "y" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014650 RID: 83536 RVA: 0x00312C84 File Offset: 0x00310E84
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Position>(deep);
		}

		// Token: 0x06014651 RID: 83537 RVA: 0x00312C90 File Offset: 0x00310E90
		// Note: this type is marked as 'beforefieldinit'.
		static Position()
		{
			byte[] array = new byte[2];
			Position.attributeNamespaceIds = array;
		}

		// Token: 0x04008DE4 RID: 36324
		private const string tagName = "pos";

		// Token: 0x04008DE5 RID: 36325
		private const byte tagNsId = 18;

		// Token: 0x04008DE6 RID: 36326
		internal const int ElementTypeIdConst = 10747;

		// Token: 0x04008DE7 RID: 36327
		private static string[] attributeTagNames = new string[] { "x", "y" };

		// Token: 0x04008DE8 RID: 36328
		private static byte[] attributeNamespaceIds;
	}
}
