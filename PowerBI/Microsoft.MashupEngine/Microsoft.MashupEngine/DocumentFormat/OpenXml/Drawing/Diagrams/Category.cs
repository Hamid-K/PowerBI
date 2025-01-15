using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002677 RID: 9847
	[GeneratedCode("DomGen", "2.0")]
	internal class Category : OpenXmlLeafElement
	{
		// Token: 0x17005C6F RID: 23663
		// (get) Token: 0x06012CD8 RID: 77016 RVA: 0x002F7174 File Offset: 0x002F5374
		public override string LocalName
		{
			get
			{
				return "cat";
			}
		}

		// Token: 0x17005C70 RID: 23664
		// (get) Token: 0x06012CD9 RID: 77017 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C71 RID: 23665
		// (get) Token: 0x06012CDA RID: 77018 RVA: 0x002FFA94 File Offset: 0x002FDC94
		internal override int ElementTypeId
		{
			get
			{
				return 10662;
			}
		}

		// Token: 0x06012CDB RID: 77019 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005C72 RID: 23666
		// (get) Token: 0x06012CDC RID: 77020 RVA: 0x002FFA9B File Offset: 0x002FDC9B
		internal override string[] AttributeTagNames
		{
			get
			{
				return Category.attributeTagNames;
			}
		}

		// Token: 0x17005C73 RID: 23667
		// (get) Token: 0x06012CDD RID: 77021 RVA: 0x002FFAA2 File Offset: 0x002FDCA2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Category.attributeNamespaceIds;
			}
		}

		// Token: 0x17005C74 RID: 23668
		// (get) Token: 0x06012CDE RID: 77022 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012CDF RID: 77023 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public StringValue Type
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005C75 RID: 23669
		// (get) Token: 0x06012CE0 RID: 77024 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06012CE1 RID: 77025 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pri")]
		public UInt32Value Priority
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

		// Token: 0x06012CE3 RID: 77027 RVA: 0x002FD892 File Offset: 0x002FBA92
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "pri" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012CE4 RID: 77028 RVA: 0x002FFAA9 File Offset: 0x002FDCA9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Category>(deep);
		}

		// Token: 0x06012CE5 RID: 77029 RVA: 0x002FFAB4 File Offset: 0x002FDCB4
		// Note: this type is marked as 'beforefieldinit'.
		static Category()
		{
			byte[] array = new byte[2];
			Category.attributeNamespaceIds = array;
		}

		// Token: 0x040081A2 RID: 33186
		private const string tagName = "cat";

		// Token: 0x040081A3 RID: 33187
		private const byte tagNsId = 14;

		// Token: 0x040081A4 RID: 33188
		internal const int ElementTypeIdConst = 10662;

		// Token: 0x040081A5 RID: 33189
		private static string[] attributeTagNames = new string[] { "type", "pri" };

		// Token: 0x040081A6 RID: 33190
		private static byte[] attributeNamespaceIds;
	}
}
