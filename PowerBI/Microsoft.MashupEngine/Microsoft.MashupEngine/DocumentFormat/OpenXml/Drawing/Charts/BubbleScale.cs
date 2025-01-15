using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025DB RID: 9691
	[GeneratedCode("DomGen", "2.0")]
	internal class BubbleScale : OpenXmlLeafElement
	{
		// Token: 0x1700585F RID: 22623
		// (get) Token: 0x060123A0 RID: 74656 RVA: 0x002F79B9 File Offset: 0x002F5BB9
		public override string LocalName
		{
			get
			{
				return "bubbleScale";
			}
		}

		// Token: 0x17005860 RID: 22624
		// (get) Token: 0x060123A1 RID: 74657 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005861 RID: 22625
		// (get) Token: 0x060123A2 RID: 74658 RVA: 0x002F79C0 File Offset: 0x002F5BC0
		internal override int ElementTypeId
		{
			get
			{
				return 10533;
			}
		}

		// Token: 0x060123A3 RID: 74659 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005862 RID: 22626
		// (get) Token: 0x060123A4 RID: 74660 RVA: 0x002F79C7 File Offset: 0x002F5BC7
		internal override string[] AttributeTagNames
		{
			get
			{
				return BubbleScale.attributeTagNames;
			}
		}

		// Token: 0x17005863 RID: 22627
		// (get) Token: 0x060123A5 RID: 74661 RVA: 0x002F79CE File Offset: 0x002F5BCE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BubbleScale.attributeNamespaceIds;
			}
		}

		// Token: 0x17005864 RID: 22628
		// (get) Token: 0x060123A6 RID: 74662 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060123A7 RID: 74663 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public UInt32Value Val
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060123A9 RID: 74665 RVA: 0x002E4A8C File Offset: 0x002E2C8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060123AA RID: 74666 RVA: 0x002F79D5 File Offset: 0x002F5BD5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BubbleScale>(deep);
		}

		// Token: 0x060123AB RID: 74667 RVA: 0x002F79E0 File Offset: 0x002F5BE0
		// Note: this type is marked as 'beforefieldinit'.
		static BubbleScale()
		{
			byte[] array = new byte[1];
			BubbleScale.attributeNamespaceIds = array;
		}

		// Token: 0x04007EB6 RID: 32438
		private const string tagName = "bubbleScale";

		// Token: 0x04007EB7 RID: 32439
		private const byte tagNsId = 11;

		// Token: 0x04007EB8 RID: 32440
		internal const int ElementTypeIdConst = 10533;

		// Token: 0x04007EB9 RID: 32441
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007EBA RID: 32442
		private static byte[] attributeNamespaceIds;
	}
}
