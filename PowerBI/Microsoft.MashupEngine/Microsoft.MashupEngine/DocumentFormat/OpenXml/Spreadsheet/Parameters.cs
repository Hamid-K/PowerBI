using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CD7 RID: 11479
	[ChildElementInfo(typeof(Parameter))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Parameters : OpenXmlCompositeElement
	{
		// Token: 0x170085B4 RID: 34228
		// (get) Token: 0x06018A11 RID: 100881 RVA: 0x00343536 File Offset: 0x00341736
		public override string LocalName
		{
			get
			{
				return "parameters";
			}
		}

		// Token: 0x170085B5 RID: 34229
		// (get) Token: 0x06018A12 RID: 100882 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170085B6 RID: 34230
		// (get) Token: 0x06018A13 RID: 100883 RVA: 0x0034353D File Offset: 0x0034173D
		internal override int ElementTypeId
		{
			get
			{
				return 11460;
			}
		}

		// Token: 0x06018A14 RID: 100884 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170085B7 RID: 34231
		// (get) Token: 0x06018A15 RID: 100885 RVA: 0x00343544 File Offset: 0x00341744
		internal override string[] AttributeTagNames
		{
			get
			{
				return Parameters.attributeTagNames;
			}
		}

		// Token: 0x170085B8 RID: 34232
		// (get) Token: 0x06018A16 RID: 100886 RVA: 0x0034354B File Offset: 0x0034174B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Parameters.attributeNamespaceIds;
			}
		}

		// Token: 0x170085B9 RID: 34233
		// (get) Token: 0x06018A17 RID: 100887 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018A18 RID: 100888 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018A19 RID: 100889 RVA: 0x00293ECF File Offset: 0x002920CF
		public Parameters()
		{
		}

		// Token: 0x06018A1A RID: 100890 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Parameters(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018A1B RID: 100891 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Parameters(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018A1C RID: 100892 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Parameters(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018A1D RID: 100893 RVA: 0x00343552 File Offset: 0x00341752
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "parameter" == name)
			{
				return new Parameter();
			}
			return null;
		}

		// Token: 0x06018A1E RID: 100894 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018A1F RID: 100895 RVA: 0x0034356D File Offset: 0x0034176D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Parameters>(deep);
		}

		// Token: 0x06018A20 RID: 100896 RVA: 0x00343578 File Offset: 0x00341778
		// Note: this type is marked as 'beforefieldinit'.
		static Parameters()
		{
			byte[] array = new byte[1];
			Parameters.attributeNamespaceIds = array;
		}

		// Token: 0x0400A114 RID: 41236
		private const string tagName = "parameters";

		// Token: 0x0400A115 RID: 41237
		private const byte tagNsId = 22;

		// Token: 0x0400A116 RID: 41238
		internal const int ElementTypeIdConst = 11460;

		// Token: 0x0400A117 RID: 41239
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A118 RID: 41240
		private static byte[] attributeNamespaceIds;
	}
}
