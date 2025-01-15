using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002599 RID: 9625
	[GeneratedCode("DomGen", "2.0")]
	internal class Period : OpenXmlLeafElement
	{
		// Token: 0x170056B8 RID: 22200
		// (get) Token: 0x06011FF5 RID: 73717 RVA: 0x002F49C7 File Offset: 0x002F2BC7
		public override string LocalName
		{
			get
			{
				return "period";
			}
		}

		// Token: 0x170056B9 RID: 22201
		// (get) Token: 0x06011FF6 RID: 73718 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056BA RID: 22202
		// (get) Token: 0x06011FF7 RID: 73719 RVA: 0x002F49CE File Offset: 0x002F2BCE
		internal override int ElementTypeId
		{
			get
			{
				return 10439;
			}
		}

		// Token: 0x06011FF8 RID: 73720 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170056BB RID: 22203
		// (get) Token: 0x06011FF9 RID: 73721 RVA: 0x002F49D5 File Offset: 0x002F2BD5
		internal override string[] AttributeTagNames
		{
			get
			{
				return Period.attributeTagNames;
			}
		}

		// Token: 0x170056BC RID: 22204
		// (get) Token: 0x06011FFA RID: 73722 RVA: 0x002F49DC File Offset: 0x002F2BDC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Period.attributeNamespaceIds;
			}
		}

		// Token: 0x170056BD RID: 22205
		// (get) Token: 0x06011FFB RID: 73723 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06011FFC RID: 73724 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06011FFE RID: 73726 RVA: 0x002E4A8C File Offset: 0x002E2C8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011FFF RID: 73727 RVA: 0x002F49E3 File Offset: 0x002F2BE3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Period>(deep);
		}

		// Token: 0x06012000 RID: 73728 RVA: 0x002F49EC File Offset: 0x002F2BEC
		// Note: this type is marked as 'beforefieldinit'.
		static Period()
		{
			byte[] array = new byte[1];
			Period.attributeNamespaceIds = array;
		}

		// Token: 0x04007DA7 RID: 32167
		private const string tagName = "period";

		// Token: 0x04007DA8 RID: 32168
		private const byte tagNsId = 11;

		// Token: 0x04007DA9 RID: 32169
		internal const int ElementTypeIdConst = 10439;

		// Token: 0x04007DAA RID: 32170
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007DAB RID: 32171
		private static byte[] attributeNamespaceIds;
	}
}
