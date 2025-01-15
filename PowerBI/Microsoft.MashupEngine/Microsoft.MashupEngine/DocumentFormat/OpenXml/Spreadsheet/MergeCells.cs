using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C9A RID: 11418
	[ChildElementInfo(typeof(MergeCell))]
	[GeneratedCode("DomGen", "2.0")]
	internal class MergeCells : OpenXmlCompositeElement
	{
		// Token: 0x1700841A RID: 33818
		// (get) Token: 0x0601860D RID: 99853 RVA: 0x00341189 File Offset: 0x0033F389
		public override string LocalName
		{
			get
			{
				return "mergeCells";
			}
		}

		// Token: 0x1700841B RID: 33819
		// (get) Token: 0x0601860E RID: 99854 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700841C RID: 33820
		// (get) Token: 0x0601860F RID: 99855 RVA: 0x00341190 File Offset: 0x0033F390
		internal override int ElementTypeId
		{
			get
			{
				return 11398;
			}
		}

		// Token: 0x06018610 RID: 99856 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700841D RID: 33821
		// (get) Token: 0x06018611 RID: 99857 RVA: 0x00341197 File Offset: 0x0033F397
		internal override string[] AttributeTagNames
		{
			get
			{
				return MergeCells.attributeTagNames;
			}
		}

		// Token: 0x1700841E RID: 33822
		// (get) Token: 0x06018612 RID: 99858 RVA: 0x0034119E File Offset: 0x0033F39E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MergeCells.attributeNamespaceIds;
			}
		}

		// Token: 0x1700841F RID: 33823
		// (get) Token: 0x06018613 RID: 99859 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018614 RID: 99860 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x06018615 RID: 99861 RVA: 0x00293ECF File Offset: 0x002920CF
		public MergeCells()
		{
		}

		// Token: 0x06018616 RID: 99862 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MergeCells(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018617 RID: 99863 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MergeCells(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018618 RID: 99864 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MergeCells(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018619 RID: 99865 RVA: 0x003411A5 File Offset: 0x0033F3A5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "mergeCell" == name)
			{
				return new MergeCell();
			}
			return null;
		}

		// Token: 0x0601861A RID: 99866 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601861B RID: 99867 RVA: 0x003411C0 File Offset: 0x0033F3C0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MergeCells>(deep);
		}

		// Token: 0x0601861C RID: 99868 RVA: 0x003411CC File Offset: 0x0033F3CC
		// Note: this type is marked as 'beforefieldinit'.
		static MergeCells()
		{
			byte[] array = new byte[1];
			MergeCells.attributeNamespaceIds = array;
		}

		// Token: 0x04009FFD RID: 40957
		private const string tagName = "mergeCells";

		// Token: 0x04009FFE RID: 40958
		private const byte tagNsId = 22;

		// Token: 0x04009FFF RID: 40959
		internal const int ElementTypeIdConst = 11398;

		// Token: 0x0400A000 RID: 40960
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A001 RID: 40961
		private static byte[] attributeNamespaceIds;
	}
}
