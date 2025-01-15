using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C04 RID: 11268
	[ChildElementInfo(typeof(XmlCellProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SingleXmlCell : OpenXmlCompositeElement
	{
		// Token: 0x17007F5B RID: 32603
		// (get) Token: 0x06017B3A RID: 97082 RVA: 0x0033A18B File Offset: 0x0033838B
		public override string LocalName
		{
			get
			{
				return "singleXmlCell";
			}
		}

		// Token: 0x17007F5C RID: 32604
		// (get) Token: 0x06017B3B RID: 97083 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F5D RID: 32605
		// (get) Token: 0x06017B3C RID: 97084 RVA: 0x0033A192 File Offset: 0x00338392
		internal override int ElementTypeId
		{
			get
			{
				return 11247;
			}
		}

		// Token: 0x06017B3D RID: 97085 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F5E RID: 32606
		// (get) Token: 0x06017B3E RID: 97086 RVA: 0x0033A199 File Offset: 0x00338399
		internal override string[] AttributeTagNames
		{
			get
			{
				return SingleXmlCell.attributeTagNames;
			}
		}

		// Token: 0x17007F5F RID: 32607
		// (get) Token: 0x06017B3F RID: 97087 RVA: 0x0033A1A0 File Offset: 0x003383A0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SingleXmlCell.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F60 RID: 32608
		// (get) Token: 0x06017B40 RID: 97088 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017B41 RID: 97089 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
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

		// Token: 0x17007F61 RID: 32609
		// (get) Token: 0x06017B42 RID: 97090 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017B43 RID: 97091 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "r")]
		public StringValue CellReference
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007F62 RID: 32610
		// (get) Token: 0x06017B44 RID: 97092 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06017B45 RID: 97093 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "connectionId")]
		public UInt32Value ConnectionId
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06017B46 RID: 97094 RVA: 0x00293ECF File Offset: 0x002920CF
		public SingleXmlCell()
		{
		}

		// Token: 0x06017B47 RID: 97095 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SingleXmlCell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017B48 RID: 97096 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SingleXmlCell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017B49 RID: 97097 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SingleXmlCell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017B4A RID: 97098 RVA: 0x0033A1A7 File Offset: 0x003383A7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "xmlCellPr" == name)
			{
				return new XmlCellProperties();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007F63 RID: 32611
		// (get) Token: 0x06017B4B RID: 97099 RVA: 0x0033A1DA File Offset: 0x003383DA
		internal override string[] ElementTagNames
		{
			get
			{
				return SingleXmlCell.eleTagNames;
			}
		}

		// Token: 0x17007F64 RID: 32612
		// (get) Token: 0x06017B4C RID: 97100 RVA: 0x0033A1E1 File Offset: 0x003383E1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SingleXmlCell.eleNamespaceIds;
			}
		}

		// Token: 0x17007F65 RID: 32613
		// (get) Token: 0x06017B4D RID: 97101 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007F66 RID: 32614
		// (get) Token: 0x06017B4E RID: 97102 RVA: 0x0033A1E8 File Offset: 0x003383E8
		// (set) Token: 0x06017B4F RID: 97103 RVA: 0x0033A1F1 File Offset: 0x003383F1
		public XmlCellProperties XmlCellProperties
		{
			get
			{
				return base.GetElement<XmlCellProperties>(0);
			}
			set
			{
				base.SetElement<XmlCellProperties>(0, value);
			}
		}

		// Token: 0x17007F67 RID: 32615
		// (get) Token: 0x06017B50 RID: 97104 RVA: 0x002E96EA File Offset: 0x002E78EA
		// (set) Token: 0x06017B51 RID: 97105 RVA: 0x002E96F3 File Offset: 0x002E78F3
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06017B52 RID: 97106 RVA: 0x0033A1FC File Offset: 0x003383FC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "r" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "connectionId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017B53 RID: 97107 RVA: 0x0033A253 File Offset: 0x00338453
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SingleXmlCell>(deep);
		}

		// Token: 0x06017B54 RID: 97108 RVA: 0x0033A25C File Offset: 0x0033845C
		// Note: this type is marked as 'beforefieldinit'.
		static SingleXmlCell()
		{
			byte[] array = new byte[3];
			SingleXmlCell.attributeNamespaceIds = array;
			SingleXmlCell.eleTagNames = new string[] { "xmlCellPr", "extLst" };
			SingleXmlCell.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009D3B RID: 40251
		private const string tagName = "singleXmlCell";

		// Token: 0x04009D3C RID: 40252
		private const byte tagNsId = 22;

		// Token: 0x04009D3D RID: 40253
		internal const int ElementTypeIdConst = 11247;

		// Token: 0x04009D3E RID: 40254
		private static string[] attributeTagNames = new string[] { "id", "r", "connectionId" };

		// Token: 0x04009D3F RID: 40255
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009D40 RID: 40256
		private static readonly string[] eleTagNames;

		// Token: 0x04009D41 RID: 40257
		private static readonly byte[] eleNamespaceIds;
	}
}
