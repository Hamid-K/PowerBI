using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200276B RID: 10091
	[ChildElementInfo(typeof(TableStyleEntry))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TableStyleList : OpenXmlPartRootElement
	{
		// Token: 0x17006130 RID: 24880
		// (get) Token: 0x06013749 RID: 79689 RVA: 0x003074D0 File Offset: 0x003056D0
		public override string LocalName
		{
			get
			{
				return "tblStyleLst";
			}
		}

		// Token: 0x17006131 RID: 24881
		// (get) Token: 0x0601374A RID: 79690 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006132 RID: 24882
		// (get) Token: 0x0601374B RID: 79691 RVA: 0x003074D7 File Offset: 0x003056D7
		internal override int ElementTypeId
		{
			get
			{
				return 10126;
			}
		}

		// Token: 0x0601374C RID: 79692 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006133 RID: 24883
		// (get) Token: 0x0601374D RID: 79693 RVA: 0x003074DE File Offset: 0x003056DE
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableStyleList.attributeTagNames;
			}
		}

		// Token: 0x17006134 RID: 24884
		// (get) Token: 0x0601374E RID: 79694 RVA: 0x003074E5 File Offset: 0x003056E5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableStyleList.attributeNamespaceIds;
			}
		}

		// Token: 0x17006135 RID: 24885
		// (get) Token: 0x0601374F RID: 79695 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013750 RID: 79696 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "def")]
		public StringValue Default
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

		// Token: 0x06013751 RID: 79697 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal TableStyleList(TableStylesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06013752 RID: 79698 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(TableStylesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006136 RID: 24886
		// (get) Token: 0x06013753 RID: 79699 RVA: 0x003074EC File Offset: 0x003056EC
		// (set) Token: 0x06013754 RID: 79700 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public TableStylesPart TableStylesPart
		{
			get
			{
				return base.OpenXmlPart as TableStylesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06013755 RID: 79701 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public TableStyleList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013756 RID: 79702 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public TableStyleList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013757 RID: 79703 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public TableStyleList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013758 RID: 79704 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public TableStyleList()
		{
		}

		// Token: 0x06013759 RID: 79705 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(TableStylesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601375A RID: 79706 RVA: 0x003074F9 File Offset: 0x003056F9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "tblStyle" == name)
			{
				return new TableStyleEntry();
			}
			return null;
		}

		// Token: 0x0601375B RID: 79707 RVA: 0x00307514 File Offset: 0x00305714
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "def" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601375C RID: 79708 RVA: 0x00307534 File Offset: 0x00305734
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyleList>(deep);
		}

		// Token: 0x0601375D RID: 79709 RVA: 0x00307540 File Offset: 0x00305740
		// Note: this type is marked as 'beforefieldinit'.
		static TableStyleList()
		{
			byte[] array = new byte[1];
			TableStyleList.attributeNamespaceIds = array;
		}

		// Token: 0x0400864A RID: 34378
		private const string tagName = "tblStyleLst";

		// Token: 0x0400864B RID: 34379
		private const byte tagNsId = 10;

		// Token: 0x0400864C RID: 34380
		internal const int ElementTypeIdConst = 10126;

		// Token: 0x0400864D RID: 34381
		private static string[] attributeTagNames = new string[] { "def" };

		// Token: 0x0400864E RID: 34382
		private static byte[] attributeNamespaceIds;
	}
}
