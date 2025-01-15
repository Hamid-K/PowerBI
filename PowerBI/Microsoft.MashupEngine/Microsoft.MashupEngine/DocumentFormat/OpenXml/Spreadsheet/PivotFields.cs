using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CAE RID: 11438
	[ChildElementInfo(typeof(PivotField))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotFields : OpenXmlCompositeElement
	{
		// Token: 0x1700847C RID: 33916
		// (get) Token: 0x0601871B RID: 100123 RVA: 0x00341923 File Offset: 0x0033FB23
		public override string LocalName
		{
			get
			{
				return "pivotFields";
			}
		}

		// Token: 0x1700847D RID: 33917
		// (get) Token: 0x0601871C RID: 100124 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700847E RID: 33918
		// (get) Token: 0x0601871D RID: 100125 RVA: 0x0034192A File Offset: 0x0033FB2A
		internal override int ElementTypeId
		{
			get
			{
				return 11418;
			}
		}

		// Token: 0x0601871E RID: 100126 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700847F RID: 33919
		// (get) Token: 0x0601871F RID: 100127 RVA: 0x00341931 File Offset: 0x0033FB31
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotFields.attributeTagNames;
			}
		}

		// Token: 0x17008480 RID: 33920
		// (get) Token: 0x06018720 RID: 100128 RVA: 0x00341938 File Offset: 0x0033FB38
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotFields.attributeNamespaceIds;
			}
		}

		// Token: 0x17008481 RID: 33921
		// (get) Token: 0x06018721 RID: 100129 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018722 RID: 100130 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018723 RID: 100131 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotFields()
		{
		}

		// Token: 0x06018724 RID: 100132 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotFields(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018725 RID: 100133 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotFields(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018726 RID: 100134 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotFields(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018727 RID: 100135 RVA: 0x0034193F File Offset: 0x0033FB3F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pivotField" == name)
			{
				return new PivotField();
			}
			return null;
		}

		// Token: 0x06018728 RID: 100136 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018729 RID: 100137 RVA: 0x0034195A File Offset: 0x0033FB5A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotFields>(deep);
		}

		// Token: 0x0601872A RID: 100138 RVA: 0x00341964 File Offset: 0x0033FB64
		// Note: this type is marked as 'beforefieldinit'.
		static PivotFields()
		{
			byte[] array = new byte[1];
			PivotFields.attributeNamespaceIds = array;
		}

		// Token: 0x0400A04B RID: 41035
		private const string tagName = "pivotFields";

		// Token: 0x0400A04C RID: 41036
		private const byte tagNsId = 22;

		// Token: 0x0400A04D RID: 41037
		internal const int ElementTypeIdConst = 11418;

		// Token: 0x0400A04E RID: 41038
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A04F RID: 41039
		private static byte[] attributeNamespaceIds;
	}
}
