using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C14 RID: 11284
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class CellStyle : OpenXmlCompositeElement
	{
		// Token: 0x17008003 RID: 32771
		// (get) Token: 0x06017CA4 RID: 97444 RVA: 0x0033B397 File Offset: 0x00339597
		public override string LocalName
		{
			get
			{
				return "cellStyle";
			}
		}

		// Token: 0x17008004 RID: 32772
		// (get) Token: 0x06017CA5 RID: 97445 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008005 RID: 32773
		// (get) Token: 0x06017CA6 RID: 97446 RVA: 0x0033B39E File Offset: 0x0033959E
		internal override int ElementTypeId
		{
			get
			{
				return 11265;
			}
		}

		// Token: 0x06017CA7 RID: 97447 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008006 RID: 32774
		// (get) Token: 0x06017CA8 RID: 97448 RVA: 0x0033B3A5 File Offset: 0x003395A5
		internal override string[] AttributeTagNames
		{
			get
			{
				return CellStyle.attributeTagNames;
			}
		}

		// Token: 0x17008007 RID: 32775
		// (get) Token: 0x06017CA9 RID: 97449 RVA: 0x0033B3AC File Offset: 0x003395AC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CellStyle.attributeNamespaceIds;
			}
		}

		// Token: 0x17008008 RID: 32776
		// (get) Token: 0x06017CAA RID: 97450 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017CAB RID: 97451 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008009 RID: 32777
		// (get) Token: 0x06017CAC RID: 97452 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017CAD RID: 97453 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "xfId")]
		public UInt32Value FormatId
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

		// Token: 0x1700800A RID: 32778
		// (get) Token: 0x06017CAE RID: 97454 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06017CAF RID: 97455 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "builtinId")]
		public UInt32Value BuiltinId
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

		// Token: 0x1700800B RID: 32779
		// (get) Token: 0x06017CB0 RID: 97456 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06017CB1 RID: 97457 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "iLevel")]
		public UInt32Value OutlineLevel
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700800C RID: 32780
		// (get) Token: 0x06017CB2 RID: 97458 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017CB3 RID: 97459 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "hidden")]
		public BooleanValue Hidden
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700800D RID: 32781
		// (get) Token: 0x06017CB4 RID: 97460 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017CB5 RID: 97461 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "customBuiltin")]
		public BooleanValue CustomBuiltin
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x06017CB6 RID: 97462 RVA: 0x00293ECF File Offset: 0x002920CF
		public CellStyle()
		{
		}

		// Token: 0x06017CB7 RID: 97463 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CellStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017CB8 RID: 97464 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CellStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017CB9 RID: 97465 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CellStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017CBA RID: 97466 RVA: 0x003328DF File Offset: 0x00330ADF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700800E RID: 32782
		// (get) Token: 0x06017CBB RID: 97467 RVA: 0x0033B3B3 File Offset: 0x003395B3
		internal override string[] ElementTagNames
		{
			get
			{
				return CellStyle.eleTagNames;
			}
		}

		// Token: 0x1700800F RID: 32783
		// (get) Token: 0x06017CBC RID: 97468 RVA: 0x0033B3BA File Offset: 0x003395BA
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CellStyle.eleNamespaceIds;
			}
		}

		// Token: 0x17008010 RID: 32784
		// (get) Token: 0x06017CBD RID: 97469 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008011 RID: 32785
		// (get) Token: 0x06017CBE RID: 97470 RVA: 0x00332908 File Offset: 0x00330B08
		// (set) Token: 0x06017CBF RID: 97471 RVA: 0x00332911 File Offset: 0x00330B11
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06017CC0 RID: 97472 RVA: 0x0033B3C4 File Offset: 0x003395C4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "xfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "builtinId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "iLevel" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "customBuiltin" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017CC1 RID: 97473 RVA: 0x0033B45D File Offset: 0x0033965D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellStyle>(deep);
		}

		// Token: 0x06017CC2 RID: 97474 RVA: 0x0033B468 File Offset: 0x00339668
		// Note: this type is marked as 'beforefieldinit'.
		static CellStyle()
		{
			byte[] array = new byte[6];
			CellStyle.attributeNamespaceIds = array;
			CellStyle.eleTagNames = new string[] { "extLst" };
			CellStyle.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009D93 RID: 40339
		private const string tagName = "cellStyle";

		// Token: 0x04009D94 RID: 40340
		private const byte tagNsId = 22;

		// Token: 0x04009D95 RID: 40341
		internal const int ElementTypeIdConst = 11265;

		// Token: 0x04009D96 RID: 40342
		private static string[] attributeTagNames = new string[] { "name", "xfId", "builtinId", "iLevel", "hidden", "customBuiltin" };

		// Token: 0x04009D97 RID: 40343
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009D98 RID: 40344
		private static readonly string[] eleTagNames;

		// Token: 0x04009D99 RID: 40345
		private static readonly byte[] eleNamespaceIds;
	}
}
