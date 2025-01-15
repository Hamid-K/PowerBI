using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023F7 RID: 9207
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DataValidationForumla1), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DataValidationForumla2), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ReferenceSequence))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DataValidation : OpenXmlCompositeElement
	{
		// Token: 0x17004E54 RID: 20052
		// (get) Token: 0x06010D25 RID: 68901 RVA: 0x002E79FD File Offset: 0x002E5BFD
		public override string LocalName
		{
			get
			{
				return "dataValidation";
			}
		}

		// Token: 0x17004E55 RID: 20053
		// (get) Token: 0x06010D26 RID: 68902 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004E56 RID: 20054
		// (get) Token: 0x06010D27 RID: 68903 RVA: 0x002E7A04 File Offset: 0x002E5C04
		internal override int ElementTypeId
		{
			get
			{
				return 12933;
			}
		}

		// Token: 0x06010D28 RID: 68904 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004E57 RID: 20055
		// (get) Token: 0x06010D29 RID: 68905 RVA: 0x002E7A0B File Offset: 0x002E5C0B
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataValidation.attributeTagNames;
			}
		}

		// Token: 0x17004E58 RID: 20056
		// (get) Token: 0x06010D2A RID: 68906 RVA: 0x002E7A12 File Offset: 0x002E5C12
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataValidation.attributeNamespaceIds;
			}
		}

		// Token: 0x17004E59 RID: 20057
		// (get) Token: 0x06010D2B RID: 68907 RVA: 0x002E7A19 File Offset: 0x002E5C19
		// (set) Token: 0x06010D2C RID: 68908 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<DataValidationValues> Type
		{
			get
			{
				return (EnumValue<DataValidationValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004E5A RID: 20058
		// (get) Token: 0x06010D2D RID: 68909 RVA: 0x002E7A28 File Offset: 0x002E5C28
		// (set) Token: 0x06010D2E RID: 68910 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "errorStyle")]
		public EnumValue<DataValidationErrorStyleValues> ErrorStyle
		{
			get
			{
				return (EnumValue<DataValidationErrorStyleValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004E5B RID: 20059
		// (get) Token: 0x06010D2F RID: 68911 RVA: 0x002E7A37 File Offset: 0x002E5C37
		// (set) Token: 0x06010D30 RID: 68912 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "imeMode")]
		public EnumValue<DataValidationImeModeValues> ImeMode
		{
			get
			{
				return (EnumValue<DataValidationImeModeValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004E5C RID: 20060
		// (get) Token: 0x06010D31 RID: 68913 RVA: 0x002E7A46 File Offset: 0x002E5C46
		// (set) Token: 0x06010D32 RID: 68914 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "operator")]
		public EnumValue<DataValidationOperatorValues> Operator
		{
			get
			{
				return (EnumValue<DataValidationOperatorValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004E5D RID: 20061
		// (get) Token: 0x06010D33 RID: 68915 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06010D34 RID: 68916 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "allowBlank")]
		public BooleanValue AllowBlank
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

		// Token: 0x17004E5E RID: 20062
		// (get) Token: 0x06010D35 RID: 68917 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06010D36 RID: 68918 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "showDropDown")]
		public BooleanValue ShowDropDown
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

		// Token: 0x17004E5F RID: 20063
		// (get) Token: 0x06010D37 RID: 68919 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06010D38 RID: 68920 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "showInputMessage")]
		public BooleanValue ShowInputMessage
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

		// Token: 0x17004E60 RID: 20064
		// (get) Token: 0x06010D39 RID: 68921 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06010D3A RID: 68922 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "showErrorMessage")]
		public BooleanValue ShowErrorMessage
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

		// Token: 0x17004E61 RID: 20065
		// (get) Token: 0x06010D3B RID: 68923 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x06010D3C RID: 68924 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "errorTitle")]
		public StringValue ErrorTitle
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17004E62 RID: 20066
		// (get) Token: 0x06010D3D RID: 68925 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x06010D3E RID: 68926 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "error")]
		public StringValue Error
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17004E63 RID: 20067
		// (get) Token: 0x06010D3F RID: 68927 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x06010D40 RID: 68928 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "promptTitle")]
		public StringValue PromptTitle
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17004E64 RID: 20068
		// (get) Token: 0x06010D41 RID: 68929 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x06010D42 RID: 68930 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "prompt")]
		public StringValue Prompt
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x06010D43 RID: 68931 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataValidation()
		{
		}

		// Token: 0x06010D44 RID: 68932 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataValidation(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D45 RID: 68933 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataValidation(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D46 RID: 68934 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataValidation(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010D47 RID: 68935 RVA: 0x002E7A58 File Offset: 0x002E5C58
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "formula1" == name)
			{
				return new DataValidationForumla1();
			}
			if (53 == namespaceId && "formula2" == name)
			{
				return new DataValidationForumla2();
			}
			if (32 == namespaceId && "sqref" == name)
			{
				return new ReferenceSequence();
			}
			return null;
		}

		// Token: 0x17004E65 RID: 20069
		// (get) Token: 0x06010D48 RID: 68936 RVA: 0x002E7AAE File Offset: 0x002E5CAE
		internal override string[] ElementTagNames
		{
			get
			{
				return DataValidation.eleTagNames;
			}
		}

		// Token: 0x17004E66 RID: 20070
		// (get) Token: 0x06010D49 RID: 68937 RVA: 0x002E7AB5 File Offset: 0x002E5CB5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DataValidation.eleNamespaceIds;
			}
		}

		// Token: 0x17004E67 RID: 20071
		// (get) Token: 0x06010D4A RID: 68938 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004E68 RID: 20072
		// (get) Token: 0x06010D4B RID: 68939 RVA: 0x002E7ABC File Offset: 0x002E5CBC
		// (set) Token: 0x06010D4C RID: 68940 RVA: 0x002E7AC5 File Offset: 0x002E5CC5
		public DataValidationForumla1 DataValidationForumla1
		{
			get
			{
				return base.GetElement<DataValidationForumla1>(0);
			}
			set
			{
				base.SetElement<DataValidationForumla1>(0, value);
			}
		}

		// Token: 0x17004E69 RID: 20073
		// (get) Token: 0x06010D4D RID: 68941 RVA: 0x002E7ACF File Offset: 0x002E5CCF
		// (set) Token: 0x06010D4E RID: 68942 RVA: 0x002E7AD8 File Offset: 0x002E5CD8
		public DataValidationForumla2 DataValidationForumla2
		{
			get
			{
				return base.GetElement<DataValidationForumla2>(1);
			}
			set
			{
				base.SetElement<DataValidationForumla2>(1, value);
			}
		}

		// Token: 0x17004E6A RID: 20074
		// (get) Token: 0x06010D4F RID: 68943 RVA: 0x002E7AE2 File Offset: 0x002E5CE2
		// (set) Token: 0x06010D50 RID: 68944 RVA: 0x002E7AEB File Offset: 0x002E5CEB
		public ReferenceSequence ReferenceSequence
		{
			get
			{
				return base.GetElement<ReferenceSequence>(2);
			}
			set
			{
				base.SetElement<ReferenceSequence>(2, value);
			}
		}

		// Token: 0x06010D51 RID: 68945 RVA: 0x002E7AF8 File Offset: 0x002E5CF8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<DataValidationValues>();
			}
			if (namespaceId == 0 && "errorStyle" == name)
			{
				return new EnumValue<DataValidationErrorStyleValues>();
			}
			if (namespaceId == 0 && "imeMode" == name)
			{
				return new EnumValue<DataValidationImeModeValues>();
			}
			if (namespaceId == 0 && "operator" == name)
			{
				return new EnumValue<DataValidationOperatorValues>();
			}
			if (namespaceId == 0 && "allowBlank" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showDropDown" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showInputMessage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showErrorMessage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "errorTitle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "error" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "promptTitle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "prompt" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010D52 RID: 68946 RVA: 0x002E7C15 File Offset: 0x002E5E15
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataValidation>(deep);
		}

		// Token: 0x06010D53 RID: 68947 RVA: 0x002E7C20 File Offset: 0x002E5E20
		// Note: this type is marked as 'beforefieldinit'.
		static DataValidation()
		{
			byte[] array = new byte[12];
			DataValidation.attributeNamespaceIds = array;
			DataValidation.eleTagNames = new string[] { "formula1", "formula2", "sqref" };
			DataValidation.eleNamespaceIds = new byte[] { 53, 53, 32 };
		}

		// Token: 0x0400766E RID: 30318
		private const string tagName = "dataValidation";

		// Token: 0x0400766F RID: 30319
		private const byte tagNsId = 53;

		// Token: 0x04007670 RID: 30320
		internal const int ElementTypeIdConst = 12933;

		// Token: 0x04007671 RID: 30321
		private static string[] attributeTagNames = new string[]
		{
			"type", "errorStyle", "imeMode", "operator", "allowBlank", "showDropDown", "showInputMessage", "showErrorMessage", "errorTitle", "error",
			"promptTitle", "prompt"
		};

		// Token: 0x04007672 RID: 30322
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007673 RID: 30323
		private static readonly string[] eleTagNames;

		// Token: 0x04007674 RID: 30324
		private static readonly byte[] eleNamespaceIds;
	}
}
