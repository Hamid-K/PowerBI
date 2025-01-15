using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002722 RID: 10018
	[GeneratedCode("DomGen", "2.0")]
	internal class SoftEdge : OpenXmlLeafElement
	{
		// Token: 0x17005F9D RID: 24477
		// (get) Token: 0x060133A9 RID: 78761 RVA: 0x0030521F File Offset: 0x0030341F
		public override string LocalName
		{
			get
			{
				return "softEdge";
			}
		}

		// Token: 0x17005F9E RID: 24478
		// (get) Token: 0x060133AA RID: 78762 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F9F RID: 24479
		// (get) Token: 0x060133AB RID: 78763 RVA: 0x00305226 File Offset: 0x00303426
		internal override int ElementTypeId
		{
			get
			{
				return 10080;
			}
		}

		// Token: 0x060133AC RID: 78764 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005FA0 RID: 24480
		// (get) Token: 0x060133AD RID: 78765 RVA: 0x0030522D File Offset: 0x0030342D
		internal override string[] AttributeTagNames
		{
			get
			{
				return SoftEdge.attributeTagNames;
			}
		}

		// Token: 0x17005FA1 RID: 24481
		// (get) Token: 0x060133AE RID: 78766 RVA: 0x00305234 File Offset: 0x00303434
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SoftEdge.attributeNamespaceIds;
			}
		}

		// Token: 0x17005FA2 RID: 24482
		// (get) Token: 0x060133AF RID: 78767 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x060133B0 RID: 78768 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rad")]
		public Int64Value Radius
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

		// Token: 0x060133B2 RID: 78770 RVA: 0x00303F1B File Offset: 0x0030211B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rad" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060133B3 RID: 78771 RVA: 0x0030523B File Offset: 0x0030343B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SoftEdge>(deep);
		}

		// Token: 0x060133B4 RID: 78772 RVA: 0x00305244 File Offset: 0x00303444
		// Note: this type is marked as 'beforefieldinit'.
		static SoftEdge()
		{
			byte[] array = new byte[1];
			SoftEdge.attributeNamespaceIds = array;
		}

		// Token: 0x04008535 RID: 34101
		private const string tagName = "softEdge";

		// Token: 0x04008536 RID: 34102
		private const byte tagNsId = 10;

		// Token: 0x04008537 RID: 34103
		internal const int ElementTypeIdConst = 10080;

		// Token: 0x04008538 RID: 34104
		private static string[] attributeTagNames = new string[] { "rad" };

		// Token: 0x04008539 RID: 34105
		private static byte[] attributeNamespaceIds;
	}
}
