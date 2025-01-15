using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B1D RID: 11037
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SharedStringItem))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class SharedStringTable : OpenXmlPartRootElement
	{
		// Token: 0x1700767B RID: 30331
		// (get) Token: 0x060167CB RID: 92107 RVA: 0x0032B17E File Offset: 0x0032937E
		public override string LocalName
		{
			get
			{
				return "sst";
			}
		}

		// Token: 0x1700767C RID: 30332
		// (get) Token: 0x060167CC RID: 92108 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700767D RID: 30333
		// (get) Token: 0x060167CD RID: 92109 RVA: 0x0032B185 File Offset: 0x00329385
		internal override int ElementTypeId
		{
			get
			{
				return 11035;
			}
		}

		// Token: 0x060167CE RID: 92110 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700767E RID: 30334
		// (get) Token: 0x060167CF RID: 92111 RVA: 0x0032B18C File Offset: 0x0032938C
		internal override string[] AttributeTagNames
		{
			get
			{
				return SharedStringTable.attributeTagNames;
			}
		}

		// Token: 0x1700767F RID: 30335
		// (get) Token: 0x060167D0 RID: 92112 RVA: 0x0032B193 File Offset: 0x00329393
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SharedStringTable.attributeNamespaceIds;
			}
		}

		// Token: 0x17007680 RID: 30336
		// (get) Token: 0x060167D1 RID: 92113 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060167D2 RID: 92114 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007681 RID: 30337
		// (get) Token: 0x060167D3 RID: 92115 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x060167D4 RID: 92116 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "uniqueCount")]
		public UInt32Value UniqueCount
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060167D5 RID: 92117 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal SharedStringTable(SharedStringTablePart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060167D6 RID: 92118 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(SharedStringTablePart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17007682 RID: 30338
		// (get) Token: 0x060167D7 RID: 92119 RVA: 0x0032B19A File Offset: 0x0032939A
		// (set) Token: 0x060167D8 RID: 92120 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public SharedStringTablePart SharedStringTablePart
		{
			get
			{
				return base.OpenXmlPart as SharedStringTablePart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060167D9 RID: 92121 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public SharedStringTable(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060167DA RID: 92122 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public SharedStringTable(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060167DB RID: 92123 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public SharedStringTable(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060167DC RID: 92124 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public SharedStringTable()
		{
		}

		// Token: 0x060167DD RID: 92125 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(SharedStringTablePart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x060167DE RID: 92126 RVA: 0x0032B1A7 File Offset: 0x003293A7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "si" == name)
			{
				return new SharedStringItem();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x060167DF RID: 92127 RVA: 0x0032B1DA File Offset: 0x003293DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "uniqueCount" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060167E0 RID: 92128 RVA: 0x0032B210 File Offset: 0x00329410
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SharedStringTable>(deep);
		}

		// Token: 0x060167E1 RID: 92129 RVA: 0x0032B21C File Offset: 0x0032941C
		// Note: this type is marked as 'beforefieldinit'.
		static SharedStringTable()
		{
			byte[] array = new byte[2];
			SharedStringTable.attributeNamespaceIds = array;
		}

		// Token: 0x040098F4 RID: 39156
		private const string tagName = "sst";

		// Token: 0x040098F5 RID: 39157
		private const byte tagNsId = 22;

		// Token: 0x040098F6 RID: 39158
		internal const int ElementTypeIdConst = 11035;

		// Token: 0x040098F7 RID: 39159
		private static string[] attributeTagNames = new string[] { "count", "uniqueCount" };

		// Token: 0x040098F8 RID: 39160
		private static byte[] attributeNamespaceIds;
	}
}
