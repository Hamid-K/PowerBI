using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B4D RID: 11085
	[GeneratedCode("DomGen", "2.0")]
	internal class TextField : OpenXmlLeafElement
	{
		// Token: 0x17007820 RID: 30752
		// (get) Token: 0x06016BAD RID: 93101 RVA: 0x0032E683 File Offset: 0x0032C883
		public override string LocalName
		{
			get
			{
				return "textField";
			}
		}

		// Token: 0x17007821 RID: 30753
		// (get) Token: 0x06016BAE RID: 93102 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007822 RID: 30754
		// (get) Token: 0x06016BAF RID: 93103 RVA: 0x0032E68A File Offset: 0x0032C88A
		internal override int ElementTypeId
		{
			get
			{
				return 11068;
			}
		}

		// Token: 0x06016BB0 RID: 93104 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007823 RID: 30755
		// (get) Token: 0x06016BB1 RID: 93105 RVA: 0x0032E691 File Offset: 0x0032C891
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextField.attributeTagNames;
			}
		}

		// Token: 0x17007824 RID: 30756
		// (get) Token: 0x06016BB2 RID: 93106 RVA: 0x0032E698 File Offset: 0x0032C898
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextField.attributeNamespaceIds;
			}
		}

		// Token: 0x17007825 RID: 30757
		// (get) Token: 0x06016BB3 RID: 93107 RVA: 0x0032E69F File Offset: 0x0032C89F
		// (set) Token: 0x06016BB4 RID: 93108 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<ExternalConnectionValues> Type
		{
			get
			{
				return (EnumValue<ExternalConnectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007826 RID: 30758
		// (get) Token: 0x06016BB5 RID: 93109 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06016BB6 RID: 93110 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "position")]
		public UInt32Value Position
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

		// Token: 0x06016BB8 RID: 93112 RVA: 0x0032E6AE File Offset: 0x0032C8AE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ExternalConnectionValues>();
			}
			if (namespaceId == 0 && "position" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016BB9 RID: 93113 RVA: 0x0032E6E4 File Offset: 0x0032C8E4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextField>(deep);
		}

		// Token: 0x06016BBA RID: 93114 RVA: 0x0032E6F0 File Offset: 0x0032C8F0
		// Note: this type is marked as 'beforefieldinit'.
		static TextField()
		{
			byte[] array = new byte[2];
			TextField.attributeNamespaceIds = array;
		}

		// Token: 0x040099BD RID: 39357
		private const string tagName = "textField";

		// Token: 0x040099BE RID: 39358
		private const byte tagNsId = 22;

		// Token: 0x040099BF RID: 39359
		internal const int ElementTypeIdConst = 11068;

		// Token: 0x040099C0 RID: 39360
		private static string[] attributeTagNames = new string[] { "type", "position" };

		// Token: 0x040099C1 RID: 39361
		private static byte[] attributeNamespaceIds;
	}
}
