using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E49 RID: 11849
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Column))]
	internal class Columns : OpenXmlCompositeElement
	{
		// Token: 0x17008A0D RID: 35341
		// (get) Token: 0x060192EA RID: 103146 RVA: 0x00340B40 File Offset: 0x0033ED40
		public override string LocalName
		{
			get
			{
				return "cols";
			}
		}

		// Token: 0x17008A0E RID: 35342
		// (get) Token: 0x060192EB RID: 103147 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A0F RID: 35343
		// (get) Token: 0x060192EC RID: 103148 RVA: 0x00347488 File Offset: 0x00345688
		internal override int ElementTypeId
		{
			get
			{
				return 11535;
			}
		}

		// Token: 0x060192ED RID: 103149 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008A10 RID: 35344
		// (get) Token: 0x060192EE RID: 103150 RVA: 0x0034748F File Offset: 0x0034568F
		internal override string[] AttributeTagNames
		{
			get
			{
				return Columns.attributeTagNames;
			}
		}

		// Token: 0x17008A11 RID: 35345
		// (get) Token: 0x060192EF RID: 103151 RVA: 0x00347496 File Offset: 0x00345696
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Columns.attributeNamespaceIds;
			}
		}

		// Token: 0x17008A12 RID: 35346
		// (get) Token: 0x060192F0 RID: 103152 RVA: 0x002EBFC4 File Offset: 0x002EA1C4
		// (set) Token: 0x060192F1 RID: 103153 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "equalWidth")]
		public OnOffValue EqualWidth
		{
			get
			{
				return (OnOffValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008A13 RID: 35347
		// (get) Token: 0x060192F2 RID: 103154 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060192F3 RID: 103155 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "space")]
		public StringValue Space
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

		// Token: 0x17008A14 RID: 35348
		// (get) Token: 0x060192F4 RID: 103156 RVA: 0x0034749D File Offset: 0x0034569D
		// (set) Token: 0x060192F5 RID: 103157 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "num")]
		public Int16Value ColumnCount
		{
			get
			{
				return (Int16Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008A15 RID: 35349
		// (get) Token: 0x060192F6 RID: 103158 RVA: 0x003474AC File Offset: 0x003456AC
		// (set) Token: 0x060192F7 RID: 103159 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "sep")]
		public OnOffValue Separator
		{
			get
			{
				return (OnOffValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060192F8 RID: 103160 RVA: 0x00293ECF File Offset: 0x002920CF
		public Columns()
		{
		}

		// Token: 0x060192F9 RID: 103161 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Columns(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060192FA RID: 103162 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Columns(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060192FB RID: 103163 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Columns(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060192FC RID: 103164 RVA: 0x003474BB File Offset: 0x003456BB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "col" == name)
			{
				return new Column();
			}
			return null;
		}

		// Token: 0x060192FD RID: 103165 RVA: 0x003474D8 File Offset: 0x003456D8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "equalWidth" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "space" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "num" == name)
			{
				return new Int16Value();
			}
			if (23 == namespaceId && "sep" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060192FE RID: 103166 RVA: 0x0034754D File Offset: 0x0034574D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Columns>(deep);
		}

		// Token: 0x0400A770 RID: 42864
		private const string tagName = "cols";

		// Token: 0x0400A771 RID: 42865
		private const byte tagNsId = 23;

		// Token: 0x0400A772 RID: 42866
		internal const int ElementTypeIdConst = 11535;

		// Token: 0x0400A773 RID: 42867
		private static string[] attributeTagNames = new string[] { "equalWidth", "space", "num", "sep" };

		// Token: 0x0400A774 RID: 42868
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
