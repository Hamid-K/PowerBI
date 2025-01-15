using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C26 RID: 11302
	[ChildElementInfo(typeof(ExternalDefinedNames))]
	[ChildElementInfo(typeof(SheetDataSet))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SheetNames))]
	internal class ExternalBook : OpenXmlCompositeElement
	{
		// Token: 0x17008092 RID: 32914
		// (get) Token: 0x06017DE7 RID: 97767 RVA: 0x0033BFF0 File Offset: 0x0033A1F0
		public override string LocalName
		{
			get
			{
				return "externalBook";
			}
		}

		// Token: 0x17008093 RID: 32915
		// (get) Token: 0x06017DE8 RID: 97768 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008094 RID: 32916
		// (get) Token: 0x06017DE9 RID: 97769 RVA: 0x0033BFF7 File Offset: 0x0033A1F7
		internal override int ElementTypeId
		{
			get
			{
				return 11283;
			}
		}

		// Token: 0x06017DEA RID: 97770 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008095 RID: 32917
		// (get) Token: 0x06017DEB RID: 97771 RVA: 0x0033BFFE File Offset: 0x0033A1FE
		internal override string[] AttributeTagNames
		{
			get
			{
				return ExternalBook.attributeTagNames;
			}
		}

		// Token: 0x17008096 RID: 32918
		// (get) Token: 0x06017DEC RID: 97772 RVA: 0x0033C005 File Offset: 0x0033A205
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ExternalBook.attributeNamespaceIds;
			}
		}

		// Token: 0x17008097 RID: 32919
		// (get) Token: 0x06017DED RID: 97773 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017DEE RID: 97774 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x06017DEF RID: 97775 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExternalBook()
		{
		}

		// Token: 0x06017DF0 RID: 97776 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExternalBook(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017DF1 RID: 97777 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExternalBook(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017DF2 RID: 97778 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExternalBook(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017DF3 RID: 97779 RVA: 0x0033C00C File Offset: 0x0033A20C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "sheetNames" == name)
			{
				return new SheetNames();
			}
			if (22 == namespaceId && "definedNames" == name)
			{
				return new ExternalDefinedNames();
			}
			if (22 == namespaceId && "sheetDataSet" == name)
			{
				return new SheetDataSet();
			}
			return null;
		}

		// Token: 0x17008098 RID: 32920
		// (get) Token: 0x06017DF4 RID: 97780 RVA: 0x0033C062 File Offset: 0x0033A262
		internal override string[] ElementTagNames
		{
			get
			{
				return ExternalBook.eleTagNames;
			}
		}

		// Token: 0x17008099 RID: 32921
		// (get) Token: 0x06017DF5 RID: 97781 RVA: 0x0033C069 File Offset: 0x0033A269
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ExternalBook.eleNamespaceIds;
			}
		}

		// Token: 0x1700809A RID: 32922
		// (get) Token: 0x06017DF6 RID: 97782 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700809B RID: 32923
		// (get) Token: 0x06017DF7 RID: 97783 RVA: 0x0033C070 File Offset: 0x0033A270
		// (set) Token: 0x06017DF8 RID: 97784 RVA: 0x0033C079 File Offset: 0x0033A279
		public SheetNames SheetNames
		{
			get
			{
				return base.GetElement<SheetNames>(0);
			}
			set
			{
				base.SetElement<SheetNames>(0, value);
			}
		}

		// Token: 0x1700809C RID: 32924
		// (get) Token: 0x06017DF9 RID: 97785 RVA: 0x0033C083 File Offset: 0x0033A283
		// (set) Token: 0x06017DFA RID: 97786 RVA: 0x0033C08C File Offset: 0x0033A28C
		public ExternalDefinedNames ExternalDefinedNames
		{
			get
			{
				return base.GetElement<ExternalDefinedNames>(1);
			}
			set
			{
				base.SetElement<ExternalDefinedNames>(1, value);
			}
		}

		// Token: 0x1700809D RID: 32925
		// (get) Token: 0x06017DFB RID: 97787 RVA: 0x0033C096 File Offset: 0x0033A296
		// (set) Token: 0x06017DFC RID: 97788 RVA: 0x0033C09F File Offset: 0x0033A29F
		public SheetDataSet SheetDataSet
		{
			get
			{
				return base.GetElement<SheetDataSet>(2);
			}
			set
			{
				base.SetElement<SheetDataSet>(2, value);
			}
		}

		// Token: 0x06017DFD RID: 97789 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017DFE RID: 97790 RVA: 0x0033C0A9 File Offset: 0x0033A2A9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExternalBook>(deep);
		}

		// Token: 0x04009DED RID: 40429
		private const string tagName = "externalBook";

		// Token: 0x04009DEE RID: 40430
		private const byte tagNsId = 22;

		// Token: 0x04009DEF RID: 40431
		internal const int ElementTypeIdConst = 11283;

		// Token: 0x04009DF0 RID: 40432
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04009DF1 RID: 40433
		private static byte[] attributeNamespaceIds = new byte[] { 19 };

		// Token: 0x04009DF2 RID: 40434
		private static readonly string[] eleTagNames = new string[] { "sheetNames", "definedNames", "sheetDataSet" };

		// Token: 0x04009DF3 RID: 40435
		private static readonly byte[] eleNamespaceIds = new byte[] { 22, 22, 22 };
	}
}
