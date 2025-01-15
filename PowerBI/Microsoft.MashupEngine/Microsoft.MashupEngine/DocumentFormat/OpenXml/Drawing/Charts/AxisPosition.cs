using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002553 RID: 9555
	[GeneratedCode("DomGen", "2.0")]
	internal class AxisPosition : OpenXmlLeafElement
	{
		// Token: 0x17005563 RID: 21859
		// (get) Token: 0x06011CF7 RID: 72951 RVA: 0x002F2AB8 File Offset: 0x002F0CB8
		public override string LocalName
		{
			get
			{
				return "axPos";
			}
		}

		// Token: 0x17005564 RID: 21860
		// (get) Token: 0x06011CF8 RID: 72952 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005565 RID: 21861
		// (get) Token: 0x06011CF9 RID: 72953 RVA: 0x002F2ABF File Offset: 0x002F0CBF
		internal override int ElementTypeId
		{
			get
			{
				return 10376;
			}
		}

		// Token: 0x06011CFA RID: 72954 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005566 RID: 21862
		// (get) Token: 0x06011CFB RID: 72955 RVA: 0x002F2AC6 File Offset: 0x002F0CC6
		internal override string[] AttributeTagNames
		{
			get
			{
				return AxisPosition.attributeTagNames;
			}
		}

		// Token: 0x17005567 RID: 21863
		// (get) Token: 0x06011CFC RID: 72956 RVA: 0x002F2ACD File Offset: 0x002F0CCD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AxisPosition.attributeNamespaceIds;
			}
		}

		// Token: 0x17005568 RID: 21864
		// (get) Token: 0x06011CFD RID: 72957 RVA: 0x002F2AD4 File Offset: 0x002F0CD4
		// (set) Token: 0x06011CFE RID: 72958 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<AxisPositionValues> Val
		{
			get
			{
				return (EnumValue<AxisPositionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011D00 RID: 72960 RVA: 0x002F2AE3 File Offset: 0x002F0CE3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<AxisPositionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011D01 RID: 72961 RVA: 0x002F2B03 File Offset: 0x002F0D03
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AxisPosition>(deep);
		}

		// Token: 0x06011D02 RID: 72962 RVA: 0x002F2B0C File Offset: 0x002F0D0C
		// Note: this type is marked as 'beforefieldinit'.
		static AxisPosition()
		{
			byte[] array = new byte[1];
			AxisPosition.attributeNamespaceIds = array;
		}

		// Token: 0x04007CA3 RID: 31907
		private const string tagName = "axPos";

		// Token: 0x04007CA4 RID: 31908
		private const byte tagNsId = 11;

		// Token: 0x04007CA5 RID: 31909
		internal const int ElementTypeIdConst = 10376;

		// Token: 0x04007CA6 RID: 31910
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007CA7 RID: 31911
		private static byte[] attributeNamespaceIds;
	}
}
