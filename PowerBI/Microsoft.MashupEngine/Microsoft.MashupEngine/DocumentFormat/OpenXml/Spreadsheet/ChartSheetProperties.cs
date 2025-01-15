using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C7E RID: 11390
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TabColor))]
	internal class ChartSheetProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700833F RID: 33599
		// (get) Token: 0x0601841B RID: 99355 RVA: 0x0033FD79 File Offset: 0x0033DF79
		public override string LocalName
		{
			get
			{
				return "sheetPr";
			}
		}

		// Token: 0x17008340 RID: 33600
		// (get) Token: 0x0601841C RID: 99356 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008341 RID: 33601
		// (get) Token: 0x0601841D RID: 99357 RVA: 0x0033FD80 File Offset: 0x0033DF80
		internal override int ElementTypeId
		{
			get
			{
				return 11370;
			}
		}

		// Token: 0x0601841E RID: 99358 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008342 RID: 33602
		// (get) Token: 0x0601841F RID: 99359 RVA: 0x0033FD87 File Offset: 0x0033DF87
		internal override string[] AttributeTagNames
		{
			get
			{
				return ChartSheetProperties.attributeTagNames;
			}
		}

		// Token: 0x17008343 RID: 33603
		// (get) Token: 0x06018420 RID: 99360 RVA: 0x0033FD8E File Offset: 0x0033DF8E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ChartSheetProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17008344 RID: 33604
		// (get) Token: 0x06018421 RID: 99361 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018422 RID: 99362 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "published")]
		public BooleanValue Published
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008345 RID: 33605
		// (get) Token: 0x06018423 RID: 99363 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06018424 RID: 99364 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "codeName")]
		public StringValue CodeName
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

		// Token: 0x06018425 RID: 99365 RVA: 0x00293ECF File Offset: 0x002920CF
		public ChartSheetProperties()
		{
		}

		// Token: 0x06018426 RID: 99366 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ChartSheetProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018427 RID: 99367 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ChartSheetProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018428 RID: 99368 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ChartSheetProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018429 RID: 99369 RVA: 0x0033FD95 File Offset: 0x0033DF95
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "tabColor" == name)
			{
				return new TabColor();
			}
			return null;
		}

		// Token: 0x17008346 RID: 33606
		// (get) Token: 0x0601842A RID: 99370 RVA: 0x0033FDB0 File Offset: 0x0033DFB0
		internal override string[] ElementTagNames
		{
			get
			{
				return ChartSheetProperties.eleTagNames;
			}
		}

		// Token: 0x17008347 RID: 33607
		// (get) Token: 0x0601842B RID: 99371 RVA: 0x0033FDB7 File Offset: 0x0033DFB7
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ChartSheetProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17008348 RID: 33608
		// (get) Token: 0x0601842C RID: 99372 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008349 RID: 33609
		// (get) Token: 0x0601842D RID: 99373 RVA: 0x0033FDBE File Offset: 0x0033DFBE
		// (set) Token: 0x0601842E RID: 99374 RVA: 0x0033FDC7 File Offset: 0x0033DFC7
		public TabColor TabColor
		{
			get
			{
				return base.GetElement<TabColor>(0);
			}
			set
			{
				base.SetElement<TabColor>(0, value);
			}
		}

		// Token: 0x0601842F RID: 99375 RVA: 0x0033FDD1 File Offset: 0x0033DFD1
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "published" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "codeName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018430 RID: 99376 RVA: 0x0033FE07 File Offset: 0x0033E007
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartSheetProperties>(deep);
		}

		// Token: 0x06018431 RID: 99377 RVA: 0x0033FE10 File Offset: 0x0033E010
		// Note: this type is marked as 'beforefieldinit'.
		static ChartSheetProperties()
		{
			byte[] array = new byte[2];
			ChartSheetProperties.attributeNamespaceIds = array;
			ChartSheetProperties.eleTagNames = new string[] { "tabColor" };
			ChartSheetProperties.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009F84 RID: 40836
		private const string tagName = "sheetPr";

		// Token: 0x04009F85 RID: 40837
		private const byte tagNsId = 22;

		// Token: 0x04009F86 RID: 40838
		internal const int ElementTypeIdConst = 11370;

		// Token: 0x04009F87 RID: 40839
		private static string[] attributeTagNames = new string[] { "published", "codeName" };

		// Token: 0x04009F88 RID: 40840
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009F89 RID: 40841
		private static readonly string[] eleTagNames;

		// Token: 0x04009F8A RID: 40842
		private static readonly byte[] eleNamespaceIds;
	}
}
