using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A3C RID: 10812
	[ChildElementInfo(typeof(TimeNodeList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Template : OpenXmlCompositeElement
	{
		// Token: 0x1700712A RID: 28970
		// (get) Token: 0x06015BE4 RID: 89060 RVA: 0x00322A54 File Offset: 0x00320C54
		public override string LocalName
		{
			get
			{
				return "tmpl";
			}
		}

		// Token: 0x1700712B RID: 28971
		// (get) Token: 0x06015BE5 RID: 89061 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700712C RID: 28972
		// (get) Token: 0x06015BE6 RID: 89062 RVA: 0x00322A5B File Offset: 0x00320C5B
		internal override int ElementTypeId
		{
			get
			{
				return 12230;
			}
		}

		// Token: 0x06015BE7 RID: 89063 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700712D RID: 28973
		// (get) Token: 0x06015BE8 RID: 89064 RVA: 0x00322A62 File Offset: 0x00320C62
		internal override string[] AttributeTagNames
		{
			get
			{
				return Template.attributeTagNames;
			}
		}

		// Token: 0x1700712E RID: 28974
		// (get) Token: 0x06015BE9 RID: 89065 RVA: 0x00322A69 File Offset: 0x00320C69
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Template.attributeNamespaceIds;
			}
		}

		// Token: 0x1700712F RID: 28975
		// (get) Token: 0x06015BEA RID: 89066 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06015BEB RID: 89067 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "lvl")]
		public UInt32Value Level
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

		// Token: 0x06015BEC RID: 89068 RVA: 0x00293ECF File Offset: 0x002920CF
		public Template()
		{
		}

		// Token: 0x06015BED RID: 89069 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Template(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BEE RID: 89070 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Template(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BEF RID: 89071 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Template(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015BF0 RID: 89072 RVA: 0x00322A70 File Offset: 0x00320C70
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "tnLst" == name)
			{
				return new TimeNodeList();
			}
			return null;
		}

		// Token: 0x17007130 RID: 28976
		// (get) Token: 0x06015BF1 RID: 89073 RVA: 0x00322A8B File Offset: 0x00320C8B
		internal override string[] ElementTagNames
		{
			get
			{
				return Template.eleTagNames;
			}
		}

		// Token: 0x17007131 RID: 28977
		// (get) Token: 0x06015BF2 RID: 89074 RVA: 0x00322A92 File Offset: 0x00320C92
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Template.eleNamespaceIds;
			}
		}

		// Token: 0x17007132 RID: 28978
		// (get) Token: 0x06015BF3 RID: 89075 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007133 RID: 28979
		// (get) Token: 0x06015BF4 RID: 89076 RVA: 0x00322A99 File Offset: 0x00320C99
		// (set) Token: 0x06015BF5 RID: 89077 RVA: 0x00322AA2 File Offset: 0x00320CA2
		public TimeNodeList TimeNodeList
		{
			get
			{
				return base.GetElement<TimeNodeList>(0);
			}
			set
			{
				base.SetElement<TimeNodeList>(0, value);
			}
		}

		// Token: 0x06015BF6 RID: 89078 RVA: 0x00322AAC File Offset: 0x00320CAC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "lvl" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015BF7 RID: 89079 RVA: 0x00322ACC File Offset: 0x00320CCC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Template>(deep);
		}

		// Token: 0x06015BF8 RID: 89080 RVA: 0x00322AD8 File Offset: 0x00320CD8
		// Note: this type is marked as 'beforefieldinit'.
		static Template()
		{
			byte[] array = new byte[1];
			Template.attributeNamespaceIds = array;
			Template.eleTagNames = new string[] { "tnLst" };
			Template.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x040094A0 RID: 38048
		private const string tagName = "tmpl";

		// Token: 0x040094A1 RID: 38049
		private const byte tagNsId = 24;

		// Token: 0x040094A2 RID: 38050
		internal const int ElementTypeIdConst = 12230;

		// Token: 0x040094A3 RID: 38051
		private static string[] attributeTagNames = new string[] { "lvl" };

		// Token: 0x040094A4 RID: 38052
		private static byte[] attributeNamespaceIds;

		// Token: 0x040094A5 RID: 38053
		private static readonly string[] eleTagNames;

		// Token: 0x040094A6 RID: 38054
		private static readonly byte[] eleNamespaceIds;
	}
}
