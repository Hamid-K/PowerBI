using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CCF RID: 11471
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Dimension))]
	internal class Dimensions : OpenXmlCompositeElement
	{
		// Token: 0x1700855A RID: 34138
		// (get) Token: 0x0601894B RID: 100683 RVA: 0x00342C63 File Offset: 0x00340E63
		public override string LocalName
		{
			get
			{
				return "dimensions";
			}
		}

		// Token: 0x1700855B RID: 34139
		// (get) Token: 0x0601894C RID: 100684 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700855C RID: 34140
		// (get) Token: 0x0601894D RID: 100685 RVA: 0x00342C6A File Offset: 0x00340E6A
		internal override int ElementTypeId
		{
			get
			{
				return 11452;
			}
		}

		// Token: 0x0601894E RID: 100686 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700855D RID: 34141
		// (get) Token: 0x0601894F RID: 100687 RVA: 0x00342C71 File Offset: 0x00340E71
		internal override string[] AttributeTagNames
		{
			get
			{
				return Dimensions.attributeTagNames;
			}
		}

		// Token: 0x1700855E RID: 34142
		// (get) Token: 0x06018950 RID: 100688 RVA: 0x00342C78 File Offset: 0x00340E78
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Dimensions.attributeNamespaceIds;
			}
		}

		// Token: 0x1700855F RID: 34143
		// (get) Token: 0x06018951 RID: 100689 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018952 RID: 100690 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018953 RID: 100691 RVA: 0x00293ECF File Offset: 0x002920CF
		public Dimensions()
		{
		}

		// Token: 0x06018954 RID: 100692 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Dimensions(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018955 RID: 100693 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Dimensions(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018956 RID: 100694 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Dimensions(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018957 RID: 100695 RVA: 0x00342C7F File Offset: 0x00340E7F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "dimension" == name)
			{
				return new Dimension();
			}
			return null;
		}

		// Token: 0x06018958 RID: 100696 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018959 RID: 100697 RVA: 0x00342C9A File Offset: 0x00340E9A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Dimensions>(deep);
		}

		// Token: 0x0601895A RID: 100698 RVA: 0x00342CA4 File Offset: 0x00340EA4
		// Note: this type is marked as 'beforefieldinit'.
		static Dimensions()
		{
			byte[] array = new byte[1];
			Dimensions.attributeNamespaceIds = array;
		}

		// Token: 0x0400A0EA RID: 41194
		private const string tagName = "dimensions";

		// Token: 0x0400A0EB RID: 41195
		private const byte tagNsId = 22;

		// Token: 0x0400A0EC RID: 41196
		internal const int ElementTypeIdConst = 11452;

		// Token: 0x0400A0ED RID: 41197
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A0EE RID: 41198
		private static byte[] attributeNamespaceIds;
	}
}
