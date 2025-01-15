using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AC9 RID: 10953
	[GeneratedCode("DomGen", "2.0")]
	internal class OleChartElement : OpenXmlLeafElement
	{
		// Token: 0x17007543 RID: 30019
		// (get) Token: 0x0601651C RID: 91420 RVA: 0x00328F29 File Offset: 0x00327129
		public override string LocalName
		{
			get
			{
				return "oleChartEl";
			}
		}

		// Token: 0x17007544 RID: 30020
		// (get) Token: 0x0601651D RID: 91421 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007545 RID: 30021
		// (get) Token: 0x0601651E RID: 91422 RVA: 0x00328F30 File Offset: 0x00327130
		internal override int ElementTypeId
		{
			get
			{
				return 12372;
			}
		}

		// Token: 0x0601651F RID: 91423 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007546 RID: 30022
		// (get) Token: 0x06016520 RID: 91424 RVA: 0x00328F37 File Offset: 0x00327137
		internal override string[] AttributeTagNames
		{
			get
			{
				return OleChartElement.attributeTagNames;
			}
		}

		// Token: 0x17007547 RID: 30023
		// (get) Token: 0x06016521 RID: 91425 RVA: 0x00328F3E File Offset: 0x0032713E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OleChartElement.attributeNamespaceIds;
			}
		}

		// Token: 0x17007548 RID: 30024
		// (get) Token: 0x06016522 RID: 91426 RVA: 0x00328F45 File Offset: 0x00327145
		// (set) Token: 0x06016523 RID: 91427 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<ChartSubElementValues> Type
		{
			get
			{
				return (EnumValue<ChartSubElementValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007549 RID: 30025
		// (get) Token: 0x06016524 RID: 91428 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06016525 RID: 91429 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "lvl")]
		public UInt32Value Level
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06016527 RID: 91431 RVA: 0x00328F54 File Offset: 0x00327154
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ChartSubElementValues>();
			}
			if (namespaceId == 0 && "lvl" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016528 RID: 91432 RVA: 0x00328F8A File Offset: 0x0032718A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OleChartElement>(deep);
		}

		// Token: 0x06016529 RID: 91433 RVA: 0x00328F94 File Offset: 0x00327194
		// Note: this type is marked as 'beforefieldinit'.
		static OleChartElement()
		{
			byte[] array = new byte[2];
			OleChartElement.attributeNamespaceIds = array;
		}

		// Token: 0x04009730 RID: 38704
		private const string tagName = "oleChartEl";

		// Token: 0x04009731 RID: 38705
		private const byte tagNsId = 24;

		// Token: 0x04009732 RID: 38706
		internal const int ElementTypeIdConst = 12372;

		// Token: 0x04009733 RID: 38707
		private static string[] attributeTagNames = new string[] { "type", "lvl" };

		// Token: 0x04009734 RID: 38708
		private static byte[] attributeNamespaceIds;
	}
}
