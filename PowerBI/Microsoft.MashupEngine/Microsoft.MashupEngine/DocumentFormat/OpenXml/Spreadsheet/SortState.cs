using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B2C RID: 11052
	[ChildElementInfo(typeof(SortCondition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SortCondition))]
	internal class SortState : OpenXmlCompositeElement
	{
		// Token: 0x1700774E RID: 30542
		// (get) Token: 0x060169C5 RID: 92613 RVA: 0x0032D328 File Offset: 0x0032B528
		public override string LocalName
		{
			get
			{
				return "sortState";
			}
		}

		// Token: 0x1700774F RID: 30543
		// (get) Token: 0x060169C6 RID: 92614 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007750 RID: 30544
		// (get) Token: 0x060169C7 RID: 92615 RVA: 0x0032D32F File Offset: 0x0032B52F
		internal override int ElementTypeId
		{
			get
			{
				return 11050;
			}
		}

		// Token: 0x060169C8 RID: 92616 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007751 RID: 30545
		// (get) Token: 0x060169C9 RID: 92617 RVA: 0x0032D336 File Offset: 0x0032B536
		internal override string[] AttributeTagNames
		{
			get
			{
				return SortState.attributeTagNames;
			}
		}

		// Token: 0x17007752 RID: 30546
		// (get) Token: 0x060169CA RID: 92618 RVA: 0x0032D33D File Offset: 0x0032B53D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SortState.attributeNamespaceIds;
			}
		}

		// Token: 0x17007753 RID: 30547
		// (get) Token: 0x060169CB RID: 92619 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060169CC RID: 92620 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "columnSort")]
		public BooleanValue ColumnSort
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

		// Token: 0x17007754 RID: 30548
		// (get) Token: 0x060169CD RID: 92621 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060169CE RID: 92622 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "caseSensitive")]
		public BooleanValue CaseSensitive
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

		// Token: 0x17007755 RID: 30549
		// (get) Token: 0x060169CF RID: 92623 RVA: 0x0032D344 File Offset: 0x0032B544
		// (set) Token: 0x060169D0 RID: 92624 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "sortMethod")]
		public EnumValue<SortMethodValues> SortMethod
		{
			get
			{
				return (EnumValue<SortMethodValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007756 RID: 30550
		// (get) Token: 0x060169D1 RID: 92625 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x060169D2 RID: 92626 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x060169D3 RID: 92627 RVA: 0x00293ECF File Offset: 0x002920CF
		public SortState()
		{
		}

		// Token: 0x060169D4 RID: 92628 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SortState(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060169D5 RID: 92629 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SortState(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060169D6 RID: 92630 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SortState(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060169D7 RID: 92631 RVA: 0x0032D354 File Offset: 0x0032B554
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "sortCondition" == name)
			{
				return new SortCondition();
			}
			if (22 == namespaceId && "sortCondition" == name)
			{
				return new SortCondition();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x060169D8 RID: 92632 RVA: 0x0032D3AC File Offset: 0x0032B5AC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "columnSort" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "caseSensitive" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sortMethod" == name)
			{
				return new EnumValue<SortMethodValues>();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060169D9 RID: 92633 RVA: 0x0032D419 File Offset: 0x0032B619
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SortState>(deep);
		}

		// Token: 0x060169DA RID: 92634 RVA: 0x0032D424 File Offset: 0x0032B624
		// Note: this type is marked as 'beforefieldinit'.
		static SortState()
		{
			byte[] array = new byte[4];
			SortState.attributeNamespaceIds = array;
		}

		// Token: 0x0400993B RID: 39227
		private const string tagName = "sortState";

		// Token: 0x0400993C RID: 39228
		private const byte tagNsId = 22;

		// Token: 0x0400993D RID: 39229
		internal const int ElementTypeIdConst = 11050;

		// Token: 0x0400993E RID: 39230
		private static string[] attributeTagNames = new string[] { "columnSort", "caseSensitive", "sortMethod", "ref" };

		// Token: 0x0400993F RID: 39231
		private static byte[] attributeNamespaceIds;
	}
}
