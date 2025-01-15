using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002590 RID: 9616
	[GeneratedCode("DomGen", "2.0")]
	internal class DepthPercent : OpenXmlLeafElement
	{
		// Token: 0x17005678 RID: 22136
		// (get) Token: 0x06011F6F RID: 73583 RVA: 0x002F426F File Offset: 0x002F246F
		public override string LocalName
		{
			get
			{
				return "depthPercent";
			}
		}

		// Token: 0x17005679 RID: 22137
		// (get) Token: 0x06011F70 RID: 73584 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700567A RID: 22138
		// (get) Token: 0x06011F71 RID: 73585 RVA: 0x002F4276 File Offset: 0x002F2476
		internal override int ElementTypeId
		{
			get
			{
				return 10420;
			}
		}

		// Token: 0x06011F72 RID: 73586 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700567B RID: 22139
		// (get) Token: 0x06011F73 RID: 73587 RVA: 0x002F427D File Offset: 0x002F247D
		internal override string[] AttributeTagNames
		{
			get
			{
				return DepthPercent.attributeTagNames;
			}
		}

		// Token: 0x1700567C RID: 22140
		// (get) Token: 0x06011F74 RID: 73588 RVA: 0x002F4284 File Offset: 0x002F2484
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DepthPercent.attributeNamespaceIds;
			}
		}

		// Token: 0x1700567D RID: 22141
		// (get) Token: 0x06011F75 RID: 73589 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x06011F76 RID: 73590 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public UInt16Value Val
		{
			get
			{
				return (UInt16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011F78 RID: 73592 RVA: 0x002F41C3 File Offset: 0x002F23C3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011F79 RID: 73593 RVA: 0x002F428B File Offset: 0x002F248B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DepthPercent>(deep);
		}

		// Token: 0x06011F7A RID: 73594 RVA: 0x002F4294 File Offset: 0x002F2494
		// Note: this type is marked as 'beforefieldinit'.
		static DepthPercent()
		{
			byte[] array = new byte[1];
			DepthPercent.attributeNamespaceIds = array;
		}

		// Token: 0x04007D7A RID: 32122
		private const string tagName = "depthPercent";

		// Token: 0x04007D7B RID: 32123
		private const byte tagNsId = 11;

		// Token: 0x04007D7C RID: 32124
		internal const int ElementTypeIdConst = 10420;

		// Token: 0x04007D7D RID: 32125
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007D7E RID: 32126
		private static byte[] attributeNamespaceIds;
	}
}
