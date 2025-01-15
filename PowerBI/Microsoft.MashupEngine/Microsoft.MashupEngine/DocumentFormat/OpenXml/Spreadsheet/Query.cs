using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B6F RID: 11119
	[ChildElementInfo(typeof(Tuples))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Query : OpenXmlCompositeElement
	{
		// Token: 0x17007948 RID: 31048
		// (get) Token: 0x06016E4D RID: 93773 RVA: 0x003303A7 File Offset: 0x0032E5A7
		public override string LocalName
		{
			get
			{
				return "query";
			}
		}

		// Token: 0x17007949 RID: 31049
		// (get) Token: 0x06016E4E RID: 93774 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700794A RID: 31050
		// (get) Token: 0x06016E4F RID: 93775 RVA: 0x003303AE File Offset: 0x0032E5AE
		internal override int ElementTypeId
		{
			get
			{
				return 11099;
			}
		}

		// Token: 0x06016E50 RID: 93776 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700794B RID: 31051
		// (get) Token: 0x06016E51 RID: 93777 RVA: 0x003303B5 File Offset: 0x0032E5B5
		internal override string[] AttributeTagNames
		{
			get
			{
				return Query.attributeTagNames;
			}
		}

		// Token: 0x1700794C RID: 31052
		// (get) Token: 0x06016E52 RID: 93778 RVA: 0x003303BC File Offset: 0x0032E5BC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Query.attributeNamespaceIds;
			}
		}

		// Token: 0x1700794D RID: 31053
		// (get) Token: 0x06016E53 RID: 93779 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016E54 RID: 93780 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "mdx")]
		public StringValue Mdx
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

		// Token: 0x06016E55 RID: 93781 RVA: 0x00293ECF File Offset: 0x002920CF
		public Query()
		{
		}

		// Token: 0x06016E56 RID: 93782 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Query(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016E57 RID: 93783 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Query(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016E58 RID: 93784 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Query(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016E59 RID: 93785 RVA: 0x003303C3 File Offset: 0x0032E5C3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "tpls" == name)
			{
				return new Tuples();
			}
			return null;
		}

		// Token: 0x1700794E RID: 31054
		// (get) Token: 0x06016E5A RID: 93786 RVA: 0x003303DE File Offset: 0x0032E5DE
		internal override string[] ElementTagNames
		{
			get
			{
				return Query.eleTagNames;
			}
		}

		// Token: 0x1700794F RID: 31055
		// (get) Token: 0x06016E5B RID: 93787 RVA: 0x003303E5 File Offset: 0x0032E5E5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Query.eleNamespaceIds;
			}
		}

		// Token: 0x17007950 RID: 31056
		// (get) Token: 0x06016E5C RID: 93788 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007951 RID: 31057
		// (get) Token: 0x06016E5D RID: 93789 RVA: 0x0032F1F2 File Offset: 0x0032D3F2
		// (set) Token: 0x06016E5E RID: 93790 RVA: 0x0032F1FB File Offset: 0x0032D3FB
		public Tuples Tuples
		{
			get
			{
				return base.GetElement<Tuples>(0);
			}
			set
			{
				base.SetElement<Tuples>(0, value);
			}
		}

		// Token: 0x06016E5F RID: 93791 RVA: 0x003303EC File Offset: 0x0032E5EC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "mdx" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016E60 RID: 93792 RVA: 0x0033040C File Offset: 0x0032E60C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Query>(deep);
		}

		// Token: 0x06016E61 RID: 93793 RVA: 0x00330418 File Offset: 0x0032E618
		// Note: this type is marked as 'beforefieldinit'.
		static Query()
		{
			byte[] array = new byte[1];
			Query.attributeNamespaceIds = array;
			Query.eleTagNames = new string[] { "tpls" };
			Query.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009A5F RID: 39519
		private const string tagName = "query";

		// Token: 0x04009A60 RID: 39520
		private const byte tagNsId = 22;

		// Token: 0x04009A61 RID: 39521
		internal const int ElementTypeIdConst = 11099;

		// Token: 0x04009A62 RID: 39522
		private static string[] attributeTagNames = new string[] { "mdx" };

		// Token: 0x04009A63 RID: 39523
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009A64 RID: 39524
		private static readonly string[] eleTagNames;

		// Token: 0x04009A65 RID: 39525
		private static readonly byte[] eleNamespaceIds;
	}
}
