using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023E8 RID: 9192
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class PivotHierarchy : OpenXmlLeafElement
	{
		// Token: 0x17004DBD RID: 19901
		// (get) Token: 0x06010BD2 RID: 68562 RVA: 0x002E6972 File Offset: 0x002E4B72
		public override string LocalName
		{
			get
			{
				return "pivotHierarchy";
			}
		}

		// Token: 0x17004DBE RID: 19902
		// (get) Token: 0x06010BD3 RID: 68563 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DBF RID: 19903
		// (get) Token: 0x06010BD4 RID: 68564 RVA: 0x002E6979 File Offset: 0x002E4B79
		internal override int ElementTypeId
		{
			get
			{
				return 12918;
			}
		}

		// Token: 0x06010BD5 RID: 68565 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DC0 RID: 19904
		// (get) Token: 0x06010BD6 RID: 68566 RVA: 0x002E6980 File Offset: 0x002E4B80
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotHierarchy.attributeTagNames;
			}
		}

		// Token: 0x17004DC1 RID: 19905
		// (get) Token: 0x06010BD7 RID: 68567 RVA: 0x002E6987 File Offset: 0x002E4B87
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotHierarchy.attributeNamespaceIds;
			}
		}

		// Token: 0x17004DC2 RID: 19906
		// (get) Token: 0x06010BD8 RID: 68568 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010BD9 RID: 68569 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ignore")]
		public BooleanValue Ignore
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06010BDB RID: 68571 RVA: 0x002E698E File Offset: 0x002E4B8E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ignore" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010BDC RID: 68572 RVA: 0x002E69AE File Offset: 0x002E4BAE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotHierarchy>(deep);
		}

		// Token: 0x06010BDD RID: 68573 RVA: 0x002E69B8 File Offset: 0x002E4BB8
		// Note: this type is marked as 'beforefieldinit'.
		static PivotHierarchy()
		{
			byte[] array = new byte[1];
			PivotHierarchy.attributeNamespaceIds = array;
		}

		// Token: 0x04007623 RID: 30243
		private const string tagName = "pivotHierarchy";

		// Token: 0x04007624 RID: 30244
		private const byte tagNsId = 53;

		// Token: 0x04007625 RID: 30245
		internal const int ElementTypeIdConst = 12918;

		// Token: 0x04007626 RID: 30246
		private static string[] attributeTagNames = new string[] { "ignore" };

		// Token: 0x04007627 RID: 30247
		private static byte[] attributeNamespaceIds;
	}
}
