using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B44 RID: 11076
	[ChildElementInfo(typeof(DataBinding))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Map : OpenXmlCompositeElement
	{
		// Token: 0x170077B8 RID: 30648
		// (get) Token: 0x06016ACF RID: 92879 RVA: 0x0032DB9F File Offset: 0x0032BD9F
		public override string LocalName
		{
			get
			{
				return "Map";
			}
		}

		// Token: 0x170077B9 RID: 30649
		// (get) Token: 0x06016AD0 RID: 92880 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170077BA RID: 30650
		// (get) Token: 0x06016AD1 RID: 92881 RVA: 0x0032DBA6 File Offset: 0x0032BDA6
		internal override int ElementTypeId
		{
			get
			{
				return 11059;
			}
		}

		// Token: 0x06016AD2 RID: 92882 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170077BB RID: 30651
		// (get) Token: 0x06016AD3 RID: 92883 RVA: 0x0032DBAD File Offset: 0x0032BDAD
		internal override string[] AttributeTagNames
		{
			get
			{
				return Map.attributeTagNames;
			}
		}

		// Token: 0x170077BC RID: 30652
		// (get) Token: 0x06016AD4 RID: 92884 RVA: 0x0032DBB4 File Offset: 0x0032BDB4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Map.attributeNamespaceIds;
			}
		}

		// Token: 0x170077BD RID: 30653
		// (get) Token: 0x06016AD5 RID: 92885 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016AD6 RID: 92886 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ID")]
		public UInt32Value ID
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

		// Token: 0x170077BE RID: 30654
		// (get) Token: 0x06016AD7 RID: 92887 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016AD8 RID: 92888 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "Name")]
		public StringValue Name
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

		// Token: 0x170077BF RID: 30655
		// (get) Token: 0x06016AD9 RID: 92889 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016ADA RID: 92890 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "RootElement")]
		public StringValue RootElement
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170077C0 RID: 30656
		// (get) Token: 0x06016ADB RID: 92891 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06016ADC RID: 92892 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "SchemaID")]
		public StringValue SchemaId
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170077C1 RID: 30657
		// (get) Token: 0x06016ADD RID: 92893 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06016ADE RID: 92894 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "ShowImportExportValidationErrors")]
		public BooleanValue ShowImportExportErrors
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

		// Token: 0x170077C2 RID: 30658
		// (get) Token: 0x06016ADF RID: 92895 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06016AE0 RID: 92896 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "AutoFit")]
		public BooleanValue AutoFit
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

		// Token: 0x170077C3 RID: 30659
		// (get) Token: 0x06016AE1 RID: 92897 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06016AE2 RID: 92898 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "Append")]
		public BooleanValue AppendData
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170077C4 RID: 30660
		// (get) Token: 0x06016AE3 RID: 92899 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06016AE4 RID: 92900 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "PreserveSortAFLayout")]
		public BooleanValue PreserveAutoFilterState
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170077C5 RID: 30661
		// (get) Token: 0x06016AE5 RID: 92901 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06016AE6 RID: 92902 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "PreserveFormat")]
		public BooleanValue PreserveFormat
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x06016AE7 RID: 92903 RVA: 0x00293ECF File Offset: 0x002920CF
		public Map()
		{
		}

		// Token: 0x06016AE8 RID: 92904 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Map(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016AE9 RID: 92905 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Map(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016AEA RID: 92906 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Map(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016AEB RID: 92907 RVA: 0x0032DBBB File Offset: 0x0032BDBB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "DataBinding" == name)
			{
				return new DataBinding();
			}
			return null;
		}

		// Token: 0x170077C6 RID: 30662
		// (get) Token: 0x06016AEC RID: 92908 RVA: 0x0032DBD6 File Offset: 0x0032BDD6
		internal override string[] ElementTagNames
		{
			get
			{
				return Map.eleTagNames;
			}
		}

		// Token: 0x170077C7 RID: 30663
		// (get) Token: 0x06016AED RID: 92909 RVA: 0x0032DBDD File Offset: 0x0032BDDD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Map.eleNamespaceIds;
			}
		}

		// Token: 0x170077C8 RID: 30664
		// (get) Token: 0x06016AEE RID: 92910 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170077C9 RID: 30665
		// (get) Token: 0x06016AEF RID: 92911 RVA: 0x0032DBE4 File Offset: 0x0032BDE4
		// (set) Token: 0x06016AF0 RID: 92912 RVA: 0x0032DBED File Offset: 0x0032BDED
		public DataBinding DataBinding
		{
			get
			{
				return base.GetElement<DataBinding>(0);
			}
			set
			{
				base.SetElement<DataBinding>(0, value);
			}
		}

		// Token: 0x06016AF1 RID: 92913 RVA: 0x0032DBF8 File Offset: 0x0032BDF8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ID" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "Name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "RootElement" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "SchemaID" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "ShowImportExportValidationErrors" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "AutoFit" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "Append" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "PreserveSortAFLayout" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "PreserveFormat" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016AF2 RID: 92914 RVA: 0x0032DCD3 File Offset: 0x0032BED3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Map>(deep);
		}

		// Token: 0x06016AF3 RID: 92915 RVA: 0x0032DCDC File Offset: 0x0032BEDC
		// Note: this type is marked as 'beforefieldinit'.
		static Map()
		{
			byte[] array = new byte[9];
			Map.attributeNamespaceIds = array;
			Map.eleTagNames = new string[] { "DataBinding" };
			Map.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x0400998E RID: 39310
		private const string tagName = "Map";

		// Token: 0x0400998F RID: 39311
		private const byte tagNsId = 22;

		// Token: 0x04009990 RID: 39312
		internal const int ElementTypeIdConst = 11059;

		// Token: 0x04009991 RID: 39313
		private static string[] attributeTagNames = new string[] { "ID", "Name", "RootElement", "SchemaID", "ShowImportExportValidationErrors", "AutoFit", "Append", "PreserveSortAFLayout", "PreserveFormat" };

		// Token: 0x04009992 RID: 39314
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009993 RID: 39315
		private static readonly string[] eleTagNames;

		// Token: 0x04009994 RID: 39316
		private static readonly byte[] eleNamespaceIds;
	}
}
