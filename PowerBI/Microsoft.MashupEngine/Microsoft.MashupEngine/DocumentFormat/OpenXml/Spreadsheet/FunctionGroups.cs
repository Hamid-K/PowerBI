using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C56 RID: 11350
	[ChildElementInfo(typeof(FunctionGroup))]
	[GeneratedCode("DomGen", "2.0")]
	internal class FunctionGroups : OpenXmlCompositeElement
	{
		// Token: 0x1700823D RID: 33341
		// (get) Token: 0x060181A6 RID: 98726 RVA: 0x0033E891 File Offset: 0x0033CA91
		public override string LocalName
		{
			get
			{
				return "functionGroups";
			}
		}

		// Token: 0x1700823E RID: 33342
		// (get) Token: 0x060181A7 RID: 98727 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700823F RID: 33343
		// (get) Token: 0x060181A8 RID: 98728 RVA: 0x0033E898 File Offset: 0x0033CA98
		internal override int ElementTypeId
		{
			get
			{
				return 11331;
			}
		}

		// Token: 0x060181A9 RID: 98729 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008240 RID: 33344
		// (get) Token: 0x060181AA RID: 98730 RVA: 0x0033E89F File Offset: 0x0033CA9F
		internal override string[] AttributeTagNames
		{
			get
			{
				return FunctionGroups.attributeTagNames;
			}
		}

		// Token: 0x17008241 RID: 33345
		// (get) Token: 0x060181AB RID: 98731 RVA: 0x0033E8A6 File Offset: 0x0033CAA6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FunctionGroups.attributeNamespaceIds;
			}
		}

		// Token: 0x17008242 RID: 33346
		// (get) Token: 0x060181AC RID: 98732 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060181AD RID: 98733 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "builtInGroupCount")]
		public UInt32Value BuiltInGroupCount
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

		// Token: 0x060181AE RID: 98734 RVA: 0x00293ECF File Offset: 0x002920CF
		public FunctionGroups()
		{
		}

		// Token: 0x060181AF RID: 98735 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FunctionGroups(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060181B0 RID: 98736 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FunctionGroups(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060181B1 RID: 98737 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FunctionGroups(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060181B2 RID: 98738 RVA: 0x0033E8AD File Offset: 0x0033CAAD
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "functionGroup" == name)
			{
				return new FunctionGroup();
			}
			return null;
		}

		// Token: 0x060181B3 RID: 98739 RVA: 0x0033E8C8 File Offset: 0x0033CAC8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "builtInGroupCount" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060181B4 RID: 98740 RVA: 0x0033E8E8 File Offset: 0x0033CAE8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FunctionGroups>(deep);
		}

		// Token: 0x060181B5 RID: 98741 RVA: 0x0033E8F4 File Offset: 0x0033CAF4
		// Note: this type is marked as 'beforefieldinit'.
		static FunctionGroups()
		{
			byte[] array = new byte[1];
			FunctionGroups.attributeNamespaceIds = array;
		}

		// Token: 0x04009EDB RID: 40667
		private const string tagName = "functionGroups";

		// Token: 0x04009EDC RID: 40668
		private const byte tagNsId = 22;

		// Token: 0x04009EDD RID: 40669
		internal const int ElementTypeIdConst = 11331;

		// Token: 0x04009EDE RID: 40670
		private static string[] attributeTagNames = new string[] { "builtInGroupCount" };

		// Token: 0x04009EDF RID: 40671
		private static byte[] attributeNamespaceIds;
	}
}
