using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CCD RID: 11469
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CalculatedItem))]
	internal class CalculatedItems : OpenXmlCompositeElement
	{
		// Token: 0x1700854E RID: 34126
		// (get) Token: 0x0601892B RID: 100651 RVA: 0x00342BA4 File Offset: 0x00340DA4
		public override string LocalName
		{
			get
			{
				return "calculatedItems";
			}
		}

		// Token: 0x1700854F RID: 34127
		// (get) Token: 0x0601892C RID: 100652 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008550 RID: 34128
		// (get) Token: 0x0601892D RID: 100653 RVA: 0x00342BAB File Offset: 0x00340DAB
		internal override int ElementTypeId
		{
			get
			{
				return 11450;
			}
		}

		// Token: 0x0601892E RID: 100654 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008551 RID: 34129
		// (get) Token: 0x0601892F RID: 100655 RVA: 0x00342BB2 File Offset: 0x00340DB2
		internal override string[] AttributeTagNames
		{
			get
			{
				return CalculatedItems.attributeTagNames;
			}
		}

		// Token: 0x17008552 RID: 34130
		// (get) Token: 0x06018930 RID: 100656 RVA: 0x00342BB9 File Offset: 0x00340DB9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CalculatedItems.attributeNamespaceIds;
			}
		}

		// Token: 0x17008553 RID: 34131
		// (get) Token: 0x06018931 RID: 100657 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018932 RID: 100658 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018933 RID: 100659 RVA: 0x00293ECF File Offset: 0x002920CF
		public CalculatedItems()
		{
		}

		// Token: 0x06018934 RID: 100660 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CalculatedItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018935 RID: 100661 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CalculatedItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018936 RID: 100662 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CalculatedItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018937 RID: 100663 RVA: 0x00342BC0 File Offset: 0x00340DC0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "calculatedItem" == name)
			{
				return new CalculatedItem();
			}
			return null;
		}

		// Token: 0x06018938 RID: 100664 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018939 RID: 100665 RVA: 0x00342BDB File Offset: 0x00340DDB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculatedItems>(deep);
		}

		// Token: 0x0601893A RID: 100666 RVA: 0x00342BE4 File Offset: 0x00340DE4
		// Note: this type is marked as 'beforefieldinit'.
		static CalculatedItems()
		{
			byte[] array = new byte[1];
			CalculatedItems.attributeNamespaceIds = array;
		}

		// Token: 0x0400A0E0 RID: 41184
		private const string tagName = "calculatedItems";

		// Token: 0x0400A0E1 RID: 41185
		private const byte tagNsId = 22;

		// Token: 0x0400A0E2 RID: 41186
		internal const int ElementTypeIdConst = 11450;

		// Token: 0x0400A0E3 RID: 41187
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A0E4 RID: 41188
		private static byte[] attributeNamespaceIds;
	}
}
