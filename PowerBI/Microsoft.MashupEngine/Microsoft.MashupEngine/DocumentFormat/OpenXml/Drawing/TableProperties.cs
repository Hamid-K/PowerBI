using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027F4 RID: 10228
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(TableStyleId))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(GroupFill))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(TableStyle))]
	internal class TableProperties : OpenXmlCompositeElement
	{
		// Token: 0x170064E7 RID: 25831
		// (get) Token: 0x06013FA0 RID: 81824 RVA: 0x0030DFE2 File Offset: 0x0030C1E2
		public override string LocalName
		{
			get
			{
				return "tblPr";
			}
		}

		// Token: 0x170064E8 RID: 25832
		// (get) Token: 0x06013FA1 RID: 81825 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170064E9 RID: 25833
		// (get) Token: 0x06013FA2 RID: 81826 RVA: 0x0030DFE9 File Offset: 0x0030C1E9
		internal override int ElementTypeId
		{
			get
			{
				return 10264;
			}
		}

		// Token: 0x06013FA3 RID: 81827 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170064EA RID: 25834
		// (get) Token: 0x06013FA4 RID: 81828 RVA: 0x0030DFF0 File Offset: 0x0030C1F0
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableProperties.attributeTagNames;
			}
		}

		// Token: 0x170064EB RID: 25835
		// (get) Token: 0x06013FA5 RID: 81829 RVA: 0x0030DFF7 File Offset: 0x0030C1F7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170064EC RID: 25836
		// (get) Token: 0x06013FA6 RID: 81830 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06013FA7 RID: 81831 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rtl")]
		public BooleanValue RightToLeft
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

		// Token: 0x170064ED RID: 25837
		// (get) Token: 0x06013FA8 RID: 81832 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06013FA9 RID: 81833 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "firstRow")]
		public BooleanValue FirstRow
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

		// Token: 0x170064EE RID: 25838
		// (get) Token: 0x06013FAA RID: 81834 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06013FAB RID: 81835 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "firstCol")]
		public BooleanValue FirstColumn
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

		// Token: 0x170064EF RID: 25839
		// (get) Token: 0x06013FAC RID: 81836 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06013FAD RID: 81837 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "lastRow")]
		public BooleanValue LastRow
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

		// Token: 0x170064F0 RID: 25840
		// (get) Token: 0x06013FAE RID: 81838 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06013FAF RID: 81839 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "lastCol")]
		public BooleanValue LastColumn
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

		// Token: 0x170064F1 RID: 25841
		// (get) Token: 0x06013FB0 RID: 81840 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06013FB1 RID: 81841 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "bandRow")]
		public BooleanValue BandRow
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

		// Token: 0x170064F2 RID: 25842
		// (get) Token: 0x06013FB2 RID: 81842 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06013FB3 RID: 81843 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "bandCol")]
		public BooleanValue BandColumn
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

		// Token: 0x06013FB4 RID: 81844 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableProperties()
		{
		}

		// Token: 0x06013FB5 RID: 81845 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FB6 RID: 81846 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FB7 RID: 81847 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013FB8 RID: 81848 RVA: 0x0030E000 File Offset: 0x0030C200
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "noFill" == name)
			{
				return new NoFill();
			}
			if (10 == namespaceId && "solidFill" == name)
			{
				return new SolidFill();
			}
			if (10 == namespaceId && "gradFill" == name)
			{
				return new GradientFill();
			}
			if (10 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (10 == namespaceId && "pattFill" == name)
			{
				return new PatternFill();
			}
			if (10 == namespaceId && "grpFill" == name)
			{
				return new GroupFill();
			}
			if (10 == namespaceId && "effectLst" == name)
			{
				return new EffectList();
			}
			if (10 == namespaceId && "effectDag" == name)
			{
				return new EffectDag();
			}
			if (10 == namespaceId && "tableStyle" == name)
			{
				return new TableStyle();
			}
			if (10 == namespaceId && "tableStyleId" == name)
			{
				return new TableStyleId();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06013FB9 RID: 81849 RVA: 0x0030E118 File Offset: 0x0030C318
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rtl" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "firstRow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "firstCol" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "lastRow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "lastCol" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "bandRow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "bandCol" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013FBA RID: 81850 RVA: 0x0030E1C7 File Offset: 0x0030C3C7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableProperties>(deep);
		}

		// Token: 0x06013FBB RID: 81851 RVA: 0x0030E1D0 File Offset: 0x0030C3D0
		// Note: this type is marked as 'beforefieldinit'.
		static TableProperties()
		{
			byte[] array = new byte[7];
			TableProperties.attributeNamespaceIds = array;
		}

		// Token: 0x0400887D RID: 34941
		private const string tagName = "tblPr";

		// Token: 0x0400887E RID: 34942
		private const byte tagNsId = 10;

		// Token: 0x0400887F RID: 34943
		internal const int ElementTypeIdConst = 10264;

		// Token: 0x04008880 RID: 34944
		private static string[] attributeTagNames = new string[] { "rtl", "firstRow", "firstCol", "lastRow", "lastCol", "bandRow", "bandCol" };

		// Token: 0x04008881 RID: 34945
		private static byte[] attributeNamespaceIds;
	}
}
