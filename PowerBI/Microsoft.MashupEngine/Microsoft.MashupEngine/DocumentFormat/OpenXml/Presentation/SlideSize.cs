using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AB2 RID: 10930
	[GeneratedCode("DomGen", "2.0")]
	internal class SlideSize : OpenXmlLeafElement
	{
		// Token: 0x170074BF RID: 29887
		// (get) Token: 0x060163F4 RID: 91124 RVA: 0x00328277 File Offset: 0x00326477
		public override string LocalName
		{
			get
			{
				return "sldSz";
			}
		}

		// Token: 0x170074C0 RID: 29888
		// (get) Token: 0x060163F5 RID: 91125 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074C1 RID: 29889
		// (get) Token: 0x060163F6 RID: 91126 RVA: 0x0032827E File Offset: 0x0032647E
		internal override int ElementTypeId
		{
			get
			{
				return 12345;
			}
		}

		// Token: 0x060163F7 RID: 91127 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170074C2 RID: 29890
		// (get) Token: 0x060163F8 RID: 91128 RVA: 0x00328285 File Offset: 0x00326485
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlideSize.attributeTagNames;
			}
		}

		// Token: 0x170074C3 RID: 29891
		// (get) Token: 0x060163F9 RID: 91129 RVA: 0x0032828C File Offset: 0x0032648C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlideSize.attributeNamespaceIds;
			}
		}

		// Token: 0x170074C4 RID: 29892
		// (get) Token: 0x060163FA RID: 91130 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060163FB RID: 91131 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "cx")]
		public Int32Value Cx
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

		// Token: 0x170074C5 RID: 29893
		// (get) Token: 0x060163FC RID: 91132 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060163FD RID: 91133 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cy")]
		public Int32Value Cy
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

		// Token: 0x170074C6 RID: 29894
		// (get) Token: 0x060163FE RID: 91134 RVA: 0x00328293 File Offset: 0x00326493
		// (set) Token: 0x060163FF RID: 91135 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "type")]
		public EnumValue<SlideSizeValues> Type
		{
			get
			{
				return (EnumValue<SlideSizeValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06016401 RID: 91137 RVA: 0x003282A4 File Offset: 0x003264A4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "cx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "cy" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<SlideSizeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016402 RID: 91138 RVA: 0x003282FB File Offset: 0x003264FB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideSize>(deep);
		}

		// Token: 0x06016403 RID: 91139 RVA: 0x00328304 File Offset: 0x00326504
		// Note: this type is marked as 'beforefieldinit'.
		static SlideSize()
		{
			byte[] array = new byte[3];
			SlideSize.attributeNamespaceIds = array;
		}

		// Token: 0x040096DA RID: 38618
		private const string tagName = "sldSz";

		// Token: 0x040096DB RID: 38619
		private const byte tagNsId = 24;

		// Token: 0x040096DC RID: 38620
		internal const int ElementTypeIdConst = 12345;

		// Token: 0x040096DD RID: 38621
		private static string[] attributeTagNames = new string[] { "cx", "cy", "type" };

		// Token: 0x040096DE RID: 38622
		private static byte[] attributeNamespaceIds;
	}
}
