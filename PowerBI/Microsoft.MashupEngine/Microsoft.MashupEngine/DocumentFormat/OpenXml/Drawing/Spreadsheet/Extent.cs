using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002891 RID: 10385
	[GeneratedCode("DomGen", "2.0")]
	internal class Extent : OpenXmlLeafElement
	{
		// Token: 0x170067A6 RID: 26534
		// (get) Token: 0x06014636 RID: 83510 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170067A7 RID: 26535
		// (get) Token: 0x06014637 RID: 83511 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170067A8 RID: 26536
		// (get) Token: 0x06014638 RID: 83512 RVA: 0x00312C17 File Offset: 0x00310E17
		internal override int ElementTypeId
		{
			get
			{
				return 10746;
			}
		}

		// Token: 0x06014639 RID: 83513 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170067A9 RID: 26537
		// (get) Token: 0x0601463A RID: 83514 RVA: 0x00312C1E File Offset: 0x00310E1E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Extent.attributeTagNames;
			}
		}

		// Token: 0x170067AA RID: 26538
		// (get) Token: 0x0601463B RID: 83515 RVA: 0x00312C25 File Offset: 0x00310E25
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Extent.attributeNamespaceIds;
			}
		}

		// Token: 0x170067AB RID: 26539
		// (get) Token: 0x0601463C RID: 83516 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x0601463D RID: 83517 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "cx")]
		public Int64Value Cx
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

		// Token: 0x170067AC RID: 26540
		// (get) Token: 0x0601463E RID: 83518 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x0601463F RID: 83519 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cy")]
		public Int64Value Cy
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06014641 RID: 83521 RVA: 0x002FCAAF File Offset: 0x002FACAF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "cx" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "cy" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014642 RID: 83522 RVA: 0x00312C2C File Offset: 0x00310E2C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Extent>(deep);
		}

		// Token: 0x06014643 RID: 83523 RVA: 0x00312C38 File Offset: 0x00310E38
		// Note: this type is marked as 'beforefieldinit'.
		static Extent()
		{
			byte[] array = new byte[2];
			Extent.attributeNamespaceIds = array;
		}

		// Token: 0x04008DDF RID: 36319
		private const string tagName = "ext";

		// Token: 0x04008DE0 RID: 36320
		private const byte tagNsId = 18;

		// Token: 0x04008DE1 RID: 36321
		internal const int ElementTypeIdConst = 10746;

		// Token: 0x04008DE2 RID: 36322
		private static string[] attributeTagNames = new string[] { "cx", "cy" };

		// Token: 0x04008DE3 RID: 36323
		private static byte[] attributeNamespaceIds;
	}
}
