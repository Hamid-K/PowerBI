using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B7A RID: 11130
	[ChildElementInfo(typeof(PivotArea))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotAreas : OpenXmlCompositeElement
	{
		// Token: 0x17007A06 RID: 31238
		// (get) Token: 0x06016FD5 RID: 94165 RVA: 0x002E9D77 File Offset: 0x002E7F77
		public override string LocalName
		{
			get
			{
				return "pivotAreas";
			}
		}

		// Token: 0x17007A07 RID: 31239
		// (get) Token: 0x06016FD6 RID: 94166 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A08 RID: 31240
		// (get) Token: 0x06016FD7 RID: 94167 RVA: 0x0033170B File Offset: 0x0032F90B
		internal override int ElementTypeId
		{
			get
			{
				return 11110;
			}
		}

		// Token: 0x06016FD8 RID: 94168 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007A09 RID: 31241
		// (get) Token: 0x06016FD9 RID: 94169 RVA: 0x00331712 File Offset: 0x0032F912
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotAreas.attributeTagNames;
			}
		}

		// Token: 0x17007A0A RID: 31242
		// (get) Token: 0x06016FDA RID: 94170 RVA: 0x00331719 File Offset: 0x0032F919
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotAreas.attributeNamespaceIds;
			}
		}

		// Token: 0x17007A0B RID: 31243
		// (get) Token: 0x06016FDB RID: 94171 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016FDC RID: 94172 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x06016FDD RID: 94173 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotAreas()
		{
		}

		// Token: 0x06016FDE RID: 94174 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotAreas(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016FDF RID: 94175 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotAreas(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016FE0 RID: 94176 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotAreas(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016FE1 RID: 94177 RVA: 0x002E9D93 File Offset: 0x002E7F93
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pivotArea" == name)
			{
				return new PivotArea();
			}
			return null;
		}

		// Token: 0x06016FE2 RID: 94178 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016FE3 RID: 94179 RVA: 0x00331720 File Offset: 0x0032F920
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotAreas>(deep);
		}

		// Token: 0x06016FE4 RID: 94180 RVA: 0x0033172C File Offset: 0x0032F92C
		// Note: this type is marked as 'beforefieldinit'.
		static PivotAreas()
		{
			byte[] array = new byte[1];
			PivotAreas.attributeNamespaceIds = array;
		}

		// Token: 0x04009AA6 RID: 39590
		private const string tagName = "pivotAreas";

		// Token: 0x04009AA7 RID: 39591
		private const byte tagNsId = 22;

		// Token: 0x04009AA8 RID: 39592
		internal const int ElementTypeIdConst = 11110;

		// Token: 0x04009AA9 RID: 39593
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009AAA RID: 39594
		private static byte[] attributeNamespaceIds;
	}
}
