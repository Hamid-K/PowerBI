using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FAA RID: 12202
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(NoWrap))]
	[ChildElementInfo(typeof(TableCellMargin))]
	[ChildElementInfo(typeof(TableCellVerticalAlignment))]
	internal class StyleTableCellProperties : OpenXmlCompositeElement
	{
		// Token: 0x17009326 RID: 37670
		// (get) Token: 0x0601A6A6 RID: 108198 RVA: 0x0030D556 File Offset: 0x0030B756
		public override string LocalName
		{
			get
			{
				return "tcPr";
			}
		}

		// Token: 0x17009327 RID: 37671
		// (get) Token: 0x0601A6A7 RID: 108199 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009328 RID: 37672
		// (get) Token: 0x0601A6A8 RID: 108200 RVA: 0x00361F1C File Offset: 0x0036011C
		internal override int ElementTypeId
		{
			get
			{
				return 11909;
			}
		}

		// Token: 0x0601A6A9 RID: 108201 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A6AA RID: 108202 RVA: 0x00293ECF File Offset: 0x002920CF
		public StyleTableCellProperties()
		{
		}

		// Token: 0x0601A6AB RID: 108203 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StyleTableCellProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A6AC RID: 108204 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StyleTableCellProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A6AD RID: 108205 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StyleTableCellProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A6AE RID: 108206 RVA: 0x00361F24 File Offset: 0x00360124
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "shd" == name)
			{
				return new Shading();
			}
			if (23 == namespaceId && "noWrap" == name)
			{
				return new NoWrap();
			}
			if (23 == namespaceId && "tcMar" == name)
			{
				return new TableCellMargin();
			}
			if (23 == namespaceId && "vAlign" == name)
			{
				return new TableCellVerticalAlignment();
			}
			return null;
		}

		// Token: 0x17009329 RID: 37673
		// (get) Token: 0x0601A6AF RID: 108207 RVA: 0x00361F92 File Offset: 0x00360192
		internal override string[] ElementTagNames
		{
			get
			{
				return StyleTableCellProperties.eleTagNames;
			}
		}

		// Token: 0x1700932A RID: 37674
		// (get) Token: 0x0601A6B0 RID: 108208 RVA: 0x00361F99 File Offset: 0x00360199
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return StyleTableCellProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700932B RID: 37675
		// (get) Token: 0x0601A6B1 RID: 108209 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700932C RID: 37676
		// (get) Token: 0x0601A6B2 RID: 108210 RVA: 0x00361FA0 File Offset: 0x003601A0
		// (set) Token: 0x0601A6B3 RID: 108211 RVA: 0x00361FA9 File Offset: 0x003601A9
		public Shading Shading
		{
			get
			{
				return base.GetElement<Shading>(0);
			}
			set
			{
				base.SetElement<Shading>(0, value);
			}
		}

		// Token: 0x1700932D RID: 37677
		// (get) Token: 0x0601A6B4 RID: 108212 RVA: 0x00361FB3 File Offset: 0x003601B3
		// (set) Token: 0x0601A6B5 RID: 108213 RVA: 0x00361FBC File Offset: 0x003601BC
		public NoWrap NoWrap
		{
			get
			{
				return base.GetElement<NoWrap>(1);
			}
			set
			{
				base.SetElement<NoWrap>(1, value);
			}
		}

		// Token: 0x1700932E RID: 37678
		// (get) Token: 0x0601A6B6 RID: 108214 RVA: 0x00361FC6 File Offset: 0x003601C6
		// (set) Token: 0x0601A6B7 RID: 108215 RVA: 0x00361FCF File Offset: 0x003601CF
		public TableCellMargin TableCellMargin
		{
			get
			{
				return base.GetElement<TableCellMargin>(2);
			}
			set
			{
				base.SetElement<TableCellMargin>(2, value);
			}
		}

		// Token: 0x1700932F RID: 37679
		// (get) Token: 0x0601A6B8 RID: 108216 RVA: 0x00361FD9 File Offset: 0x003601D9
		// (set) Token: 0x0601A6B9 RID: 108217 RVA: 0x00361FE2 File Offset: 0x003601E2
		public TableCellVerticalAlignment TableCellVerticalAlignment
		{
			get
			{
				return base.GetElement<TableCellVerticalAlignment>(3);
			}
			set
			{
				base.SetElement<TableCellVerticalAlignment>(3, value);
			}
		}

		// Token: 0x0601A6BA RID: 108218 RVA: 0x00361FEC File Offset: 0x003601EC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleTableCellProperties>(deep);
		}

		// Token: 0x0400ACE3 RID: 44259
		private const string tagName = "tcPr";

		// Token: 0x0400ACE4 RID: 44260
		private const byte tagNsId = 23;

		// Token: 0x0400ACE5 RID: 44261
		internal const int ElementTypeIdConst = 11909;

		// Token: 0x0400ACE6 RID: 44262
		private static readonly string[] eleTagNames = new string[] { "shd", "noWrap", "tcMar", "vAlign" };

		// Token: 0x0400ACE7 RID: 44263
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
