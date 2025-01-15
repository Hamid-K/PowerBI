using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B1A RID: 11034
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotCacheRecord))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class PivotCacheRecords : OpenXmlPartRootElement
	{
		// Token: 0x170075F6 RID: 30198
		// (get) Token: 0x060166B2 RID: 91826 RVA: 0x002A8397 File Offset: 0x002A6597
		public override string LocalName
		{
			get
			{
				return "pivotCacheRecords";
			}
		}

		// Token: 0x170075F7 RID: 30199
		// (get) Token: 0x060166B3 RID: 91827 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170075F8 RID: 30200
		// (get) Token: 0x060166B4 RID: 91828 RVA: 0x00329F07 File Offset: 0x00328107
		internal override int ElementTypeId
		{
			get
			{
				return 11032;
			}
		}

		// Token: 0x060166B5 RID: 91829 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170075F9 RID: 30201
		// (get) Token: 0x060166B6 RID: 91830 RVA: 0x00329F0E File Offset: 0x0032810E
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotCacheRecords.attributeTagNames;
			}
		}

		// Token: 0x170075FA RID: 30202
		// (get) Token: 0x060166B7 RID: 91831 RVA: 0x00329F15 File Offset: 0x00328115
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotCacheRecords.attributeNamespaceIds;
			}
		}

		// Token: 0x170075FB RID: 30203
		// (get) Token: 0x060166B8 RID: 91832 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060166B9 RID: 91833 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060166BA RID: 91834 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal PivotCacheRecords(PivotTableCacheRecordsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060166BB RID: 91835 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(PivotTableCacheRecordsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170075FC RID: 30204
		// (get) Token: 0x060166BC RID: 91836 RVA: 0x00329F1C File Offset: 0x0032811C
		// (set) Token: 0x060166BD RID: 91837 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public PivotTableCacheRecordsPart PivotTableCacheRecordsPart
		{
			get
			{
				return base.OpenXmlPart as PivotTableCacheRecordsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060166BE RID: 91838 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public PivotCacheRecords(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060166BF RID: 91839 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public PivotCacheRecords(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060166C0 RID: 91840 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public PivotCacheRecords(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060166C1 RID: 91841 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public PivotCacheRecords()
		{
		}

		// Token: 0x060166C2 RID: 91842 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(PivotTableCacheRecordsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x060166C3 RID: 91843 RVA: 0x00329F29 File Offset: 0x00328129
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "r" == name)
			{
				return new PivotCacheRecord();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x060166C4 RID: 91844 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060166C5 RID: 91845 RVA: 0x00329F5C File Offset: 0x0032815C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotCacheRecords>(deep);
		}

		// Token: 0x060166C6 RID: 91846 RVA: 0x00329F68 File Offset: 0x00328168
		// Note: this type is marked as 'beforefieldinit'.
		static PivotCacheRecords()
		{
			byte[] array = new byte[1];
			PivotCacheRecords.attributeNamespaceIds = array;
		}

		// Token: 0x040098E1 RID: 39137
		private const string tagName = "pivotCacheRecords";

		// Token: 0x040098E2 RID: 39138
		private const byte tagNsId = 22;

		// Token: 0x040098E3 RID: 39139
		internal const int ElementTypeIdConst = 11032;

		// Token: 0x040098E4 RID: 39140
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x040098E5 RID: 39141
		private static byte[] attributeNamespaceIds;
	}
}
