using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002689 RID: 9865
	[GeneratedCode("DomGen", "2.0")]
	internal class StyleDisplayCategory : OpenXmlLeafElement
	{
		// Token: 0x17005CD7 RID: 23767
		// (get) Token: 0x06012DB9 RID: 77241 RVA: 0x002F7174 File Offset: 0x002F5374
		public override string LocalName
		{
			get
			{
				return "cat";
			}
		}

		// Token: 0x17005CD8 RID: 23768
		// (get) Token: 0x06012DBA RID: 77242 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CD9 RID: 23769
		// (get) Token: 0x06012DBB RID: 77243 RVA: 0x003001B7 File Offset: 0x002FE3B7
		internal override int ElementTypeId
		{
			get
			{
				return 10680;
			}
		}

		// Token: 0x06012DBC RID: 77244 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005CDA RID: 23770
		// (get) Token: 0x06012DBD RID: 77245 RVA: 0x003001BE File Offset: 0x002FE3BE
		internal override string[] AttributeTagNames
		{
			get
			{
				return StyleDisplayCategory.attributeTagNames;
			}
		}

		// Token: 0x17005CDB RID: 23771
		// (get) Token: 0x06012DBE RID: 77246 RVA: 0x003001C5 File Offset: 0x002FE3C5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StyleDisplayCategory.attributeNamespaceIds;
			}
		}

		// Token: 0x17005CDC RID: 23772
		// (get) Token: 0x06012DBF RID: 77247 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012DC0 RID: 77248 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005CDD RID: 23773
		// (get) Token: 0x06012DC1 RID: 77249 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06012DC2 RID: 77250 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06012DC4 RID: 77252 RVA: 0x002FD892 File Offset: 0x002FBA92
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

		// Token: 0x06012DC5 RID: 77253 RVA: 0x003001CC File Offset: 0x002FE3CC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleDisplayCategory>(deep);
		}

		// Token: 0x06012DC6 RID: 77254 RVA: 0x003001D8 File Offset: 0x002FE3D8
		// Note: this type is marked as 'beforefieldinit'.
		static StyleDisplayCategory()
		{
			byte[] array = new byte[2];
			StyleDisplayCategory.attributeNamespaceIds = array;
		}

		// Token: 0x040081F3 RID: 33267
		private const string tagName = "cat";

		// Token: 0x040081F4 RID: 33268
		private const byte tagNsId = 14;

		// Token: 0x040081F5 RID: 33269
		internal const int ElementTypeIdConst = 10680;

		// Token: 0x040081F6 RID: 33270
		private static string[] attributeTagNames = new string[] { "type", "pri" };

		// Token: 0x040081F7 RID: 33271
		private static byte[] attributeNamespaceIds;
	}
}
