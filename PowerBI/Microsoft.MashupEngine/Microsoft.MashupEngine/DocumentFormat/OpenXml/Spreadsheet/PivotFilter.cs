using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B85 RID: 11141
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(AutoFilter))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotFilter : OpenXmlCompositeElement
	{
		// Token: 0x17007A60 RID: 31328
		// (get) Token: 0x06017090 RID: 94352 RVA: 0x002E6B13 File Offset: 0x002E4D13
		public override string LocalName
		{
			get
			{
				return "filter";
			}
		}

		// Token: 0x17007A61 RID: 31329
		// (get) Token: 0x06017091 RID: 94353 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A62 RID: 31330
		// (get) Token: 0x06017092 RID: 94354 RVA: 0x00331F0B File Offset: 0x0033010B
		internal override int ElementTypeId
		{
			get
			{
				return 11119;
			}
		}

		// Token: 0x06017093 RID: 94355 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007A63 RID: 31331
		// (get) Token: 0x06017094 RID: 94356 RVA: 0x00331F12 File Offset: 0x00330112
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotFilter.attributeTagNames;
			}
		}

		// Token: 0x17007A64 RID: 31332
		// (get) Token: 0x06017095 RID: 94357 RVA: 0x00331F19 File Offset: 0x00330119
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotFilter.attributeNamespaceIds;
			}
		}

		// Token: 0x17007A65 RID: 31333
		// (get) Token: 0x06017096 RID: 94358 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017097 RID: 94359 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "fld")]
		public UInt32Value Field
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

		// Token: 0x17007A66 RID: 31334
		// (get) Token: 0x06017098 RID: 94360 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017099 RID: 94361 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "mpFld")]
		public UInt32Value MemberPropertyFieldId
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

		// Token: 0x17007A67 RID: 31335
		// (get) Token: 0x0601709A RID: 94362 RVA: 0x00331F20 File Offset: 0x00330120
		// (set) Token: 0x0601709B RID: 94363 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "type")]
		public EnumValue<PivotFilterValues> Type
		{
			get
			{
				return (EnumValue<PivotFilterValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007A68 RID: 31336
		// (get) Token: 0x0601709C RID: 94364 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x0601709D RID: 94365 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "evalOrder")]
		public Int32Value EvaluationOrder
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007A69 RID: 31337
		// (get) Token: 0x0601709E RID: 94366 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x0601709F RID: 94367 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "id")]
		public UInt32Value Id
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007A6A RID: 31338
		// (get) Token: 0x060170A0 RID: 94368 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x060170A1 RID: 94369 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "iMeasureHier")]
		public UInt32Value MeasureHierarchy
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007A6B RID: 31339
		// (get) Token: 0x060170A2 RID: 94370 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x060170A3 RID: 94371 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "iMeasureFld")]
		public UInt32Value MeasureField
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007A6C RID: 31340
		// (get) Token: 0x060170A4 RID: 94372 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x060170A5 RID: 94373 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007A6D RID: 31341
		// (get) Token: 0x060170A6 RID: 94374 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x060170A7 RID: 94375 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17007A6E RID: 31342
		// (get) Token: 0x060170A8 RID: 94376 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x060170A9 RID: 94377 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "stringValue1")]
		public StringValue StringValue1
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

		// Token: 0x17007A6F RID: 31343
		// (get) Token: 0x060170AA RID: 94378 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x060170AB RID: 94379 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "stringValue2")]
		public StringValue StringValue2
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

		// Token: 0x060170AC RID: 94380 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotFilter()
		{
		}

		// Token: 0x060170AD RID: 94381 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotFilter(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060170AE RID: 94382 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotFilter(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060170AF RID: 94383 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotFilter(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060170B0 RID: 94384 RVA: 0x00331F2F File Offset: 0x0033012F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "autoFilter" == name)
			{
				return new AutoFilter();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007A70 RID: 31344
		// (get) Token: 0x060170B1 RID: 94385 RVA: 0x00331F62 File Offset: 0x00330162
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotFilter.eleTagNames;
			}
		}

		// Token: 0x17007A71 RID: 31345
		// (get) Token: 0x060170B2 RID: 94386 RVA: 0x00331F69 File Offset: 0x00330169
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotFilter.eleNamespaceIds;
			}
		}

		// Token: 0x17007A72 RID: 31346
		// (get) Token: 0x060170B3 RID: 94387 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007A73 RID: 31347
		// (get) Token: 0x060170B4 RID: 94388 RVA: 0x0032C860 File Offset: 0x0032AA60
		// (set) Token: 0x060170B5 RID: 94389 RVA: 0x0032C869 File Offset: 0x0032AA69
		public AutoFilter AutoFilter
		{
			get
			{
				return base.GetElement<AutoFilter>(0);
			}
			set
			{
				base.SetElement<AutoFilter>(0, value);
			}
		}

		// Token: 0x17007A74 RID: 31348
		// (get) Token: 0x060170B6 RID: 94390 RVA: 0x002E96EA File Offset: 0x002E78EA
		// (set) Token: 0x060170B7 RID: 94391 RVA: 0x002E96F3 File Offset: 0x002E78F3
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

		// Token: 0x060170B8 RID: 94392 RVA: 0x00331F70 File Offset: 0x00330170
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "fld" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "mpFld" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<PivotFilterValues>();
			}
			if (namespaceId == 0 && "evalOrder" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "iMeasureHier" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "iMeasureFld" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "stringValue1" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "stringValue2" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060170B9 RID: 94393 RVA: 0x00332077 File Offset: 0x00330277
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotFilter>(deep);
		}

		// Token: 0x060170BA RID: 94394 RVA: 0x00332080 File Offset: 0x00330280
		// Note: this type is marked as 'beforefieldinit'.
		static PivotFilter()
		{
			byte[] array = new byte[11];
			PivotFilter.attributeNamespaceIds = array;
			PivotFilter.eleTagNames = new string[] { "autoFilter", "extLst" };
			PivotFilter.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009ADA RID: 39642
		private const string tagName = "filter";

		// Token: 0x04009ADB RID: 39643
		private const byte tagNsId = 22;

		// Token: 0x04009ADC RID: 39644
		internal const int ElementTypeIdConst = 11119;

		// Token: 0x04009ADD RID: 39645
		private static string[] attributeTagNames = new string[]
		{
			"fld", "mpFld", "type", "evalOrder", "id", "iMeasureHier", "iMeasureFld", "name", "description", "stringValue1",
			"stringValue2"
		};

		// Token: 0x04009ADE RID: 39646
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009ADF RID: 39647
		private static readonly string[] eleTagNames;

		// Token: 0x04009AE0 RID: 39648
		private static readonly byte[] eleNamespaceIds;
	}
}
