using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002217 RID: 8727
	[GeneratedCode("DomGen", "2.0")]
	internal class Entry : OpenXmlLeafElement
	{
		// Token: 0x17003913 RID: 14611
		// (get) Token: 0x0600DFC7 RID: 57287 RVA: 0x002BF684 File Offset: 0x002BD884
		public override string LocalName
		{
			get
			{
				return "entry";
			}
		}

		// Token: 0x17003914 RID: 14612
		// (get) Token: 0x0600DFC8 RID: 57288 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17003915 RID: 14613
		// (get) Token: 0x0600DFC9 RID: 57289 RVA: 0x002BF68B File Offset: 0x002BD88B
		internal override int ElementTypeId
		{
			get
			{
				return 12420;
			}
		}

		// Token: 0x0600DFCA RID: 57290 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003916 RID: 14614
		// (get) Token: 0x0600DFCB RID: 57291 RVA: 0x002BF692 File Offset: 0x002BD892
		internal override string[] AttributeTagNames
		{
			get
			{
				return Entry.attributeTagNames;
			}
		}

		// Token: 0x17003917 RID: 14615
		// (get) Token: 0x0600DFCC RID: 57292 RVA: 0x002BF699 File Offset: 0x002BD899
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Entry.attributeNamespaceIds;
			}
		}

		// Token: 0x17003918 RID: 14616
		// (get) Token: 0x0600DFCD RID: 57293 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0600DFCE RID: 57294 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "new")]
		public Int32Value New
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

		// Token: 0x17003919 RID: 14617
		// (get) Token: 0x0600DFCF RID: 57295 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0600DFD0 RID: 57296 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "old")]
		public Int32Value Old
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

		// Token: 0x0600DFD2 RID: 57298 RVA: 0x002BF6BE File Offset: 0x002BD8BE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "new" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "old" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DFD3 RID: 57299 RVA: 0x002BF6F4 File Offset: 0x002BD8F4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Entry>(deep);
		}

		// Token: 0x0600DFD4 RID: 57300 RVA: 0x002BF700 File Offset: 0x002BD900
		// Note: this type is marked as 'beforefieldinit'.
		static Entry()
		{
			byte[] array = new byte[2];
			Entry.attributeNamespaceIds = array;
		}

		// Token: 0x04006DB7 RID: 28087
		private const string tagName = "entry";

		// Token: 0x04006DB8 RID: 28088
		private const byte tagNsId = 27;

		// Token: 0x04006DB9 RID: 28089
		internal const int ElementTypeIdConst = 12420;

		// Token: 0x04006DBA RID: 28090
		private static string[] attributeTagNames = new string[] { "new", "old" };

		// Token: 0x04006DBB RID: 28091
		private static byte[] attributeNamespaceIds;
	}
}
