using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C98 RID: 11416
	[ChildElementInfo(typeof(DataReferences))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DataConsolidate : OpenXmlCompositeElement
	{
		// Token: 0x17008409 RID: 33801
		// (get) Token: 0x060185E6 RID: 99814 RVA: 0x00340FF3 File Offset: 0x0033F1F3
		public override string LocalName
		{
			get
			{
				return "dataConsolidate";
			}
		}

		// Token: 0x1700840A RID: 33802
		// (get) Token: 0x060185E7 RID: 99815 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700840B RID: 33803
		// (get) Token: 0x060185E8 RID: 99816 RVA: 0x00340FFA File Offset: 0x0033F1FA
		internal override int ElementTypeId
		{
			get
			{
				return 11396;
			}
		}

		// Token: 0x060185E9 RID: 99817 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700840C RID: 33804
		// (get) Token: 0x060185EA RID: 99818 RVA: 0x00341001 File Offset: 0x0033F201
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataConsolidate.attributeTagNames;
			}
		}

		// Token: 0x1700840D RID: 33805
		// (get) Token: 0x060185EB RID: 99819 RVA: 0x00341008 File Offset: 0x0033F208
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataConsolidate.attributeNamespaceIds;
			}
		}

		// Token: 0x1700840E RID: 33806
		// (get) Token: 0x060185EC RID: 99820 RVA: 0x0034100F File Offset: 0x0033F20F
		// (set) Token: 0x060185ED RID: 99821 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "function")]
		public EnumValue<DataConsolidateFunctionValues> Function
		{
			get
			{
				return (EnumValue<DataConsolidateFunctionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700840F RID: 33807
		// (get) Token: 0x060185EE RID: 99822 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060185EF RID: 99823 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "leftLabels")]
		public BooleanValue LeftLabels
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008410 RID: 33808
		// (get) Token: 0x060185F0 RID: 99824 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060185F1 RID: 99825 RVA: 0x002BD494 File Offset: 0x002BB694
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(0, "startLabels")]
		public BooleanValue StartLabels
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008411 RID: 33809
		// (get) Token: 0x060185F2 RID: 99826 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060185F3 RID: 99827 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "topLabels")]
		public BooleanValue TopLabels
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008412 RID: 33810
		// (get) Token: 0x060185F4 RID: 99828 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060185F5 RID: 99829 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "link")]
		public BooleanValue Link
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

		// Token: 0x060185F6 RID: 99830 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataConsolidate()
		{
		}

		// Token: 0x060185F7 RID: 99831 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataConsolidate(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060185F8 RID: 99832 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataConsolidate(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060185F9 RID: 99833 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataConsolidate(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060185FA RID: 99834 RVA: 0x0034101E File Offset: 0x0033F21E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "dataRefs" == name)
			{
				return new DataReferences();
			}
			return null;
		}

		// Token: 0x17008413 RID: 33811
		// (get) Token: 0x060185FB RID: 99835 RVA: 0x00341039 File Offset: 0x0033F239
		internal override string[] ElementTagNames
		{
			get
			{
				return DataConsolidate.eleTagNames;
			}
		}

		// Token: 0x17008414 RID: 33812
		// (get) Token: 0x060185FC RID: 99836 RVA: 0x00341040 File Offset: 0x0033F240
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DataConsolidate.eleNamespaceIds;
			}
		}

		// Token: 0x17008415 RID: 33813
		// (get) Token: 0x060185FD RID: 99837 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008416 RID: 33814
		// (get) Token: 0x060185FE RID: 99838 RVA: 0x00341047 File Offset: 0x0033F247
		// (set) Token: 0x060185FF RID: 99839 RVA: 0x00341050 File Offset: 0x0033F250
		public DataReferences DataReferences
		{
			get
			{
				return base.GetElement<DataReferences>(0);
			}
			set
			{
				base.SetElement<DataReferences>(0, value);
			}
		}

		// Token: 0x06018600 RID: 99840 RVA: 0x0034105C File Offset: 0x0033F25C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "function" == name)
			{
				return new EnumValue<DataConsolidateFunctionValues>();
			}
			if (namespaceId == 0 && "leftLabels" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "startLabels" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "topLabels" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "link" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018601 RID: 99841 RVA: 0x003410DF File Offset: 0x0033F2DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataConsolidate>(deep);
		}

		// Token: 0x06018602 RID: 99842 RVA: 0x003410E8 File Offset: 0x0033F2E8
		// Note: this type is marked as 'beforefieldinit'.
		static DataConsolidate()
		{
			byte[] array = new byte[5];
			DataConsolidate.attributeNamespaceIds = array;
			DataConsolidate.eleTagNames = new string[] { "dataRefs" };
			DataConsolidate.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009FF3 RID: 40947
		private const string tagName = "dataConsolidate";

		// Token: 0x04009FF4 RID: 40948
		private const byte tagNsId = 22;

		// Token: 0x04009FF5 RID: 40949
		internal const int ElementTypeIdConst = 11396;

		// Token: 0x04009FF6 RID: 40950
		private static string[] attributeTagNames = new string[] { "function", "leftLabels", "startLabels", "topLabels", "link" };

		// Token: 0x04009FF7 RID: 40951
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009FF8 RID: 40952
		private static readonly string[] eleTagNames;

		// Token: 0x04009FF9 RID: 40953
		private static readonly byte[] eleNamespaceIds;
	}
}
