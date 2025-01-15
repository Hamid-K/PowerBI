using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BBE RID: 11198
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Reviewed))]
	internal class ReviewedList : OpenXmlCompositeElement
	{
		// Token: 0x17007C60 RID: 31840
		// (get) Token: 0x060174D8 RID: 95448 RVA: 0x003352B7 File Offset: 0x003334B7
		public override string LocalName
		{
			get
			{
				return "reviewedList";
			}
		}

		// Token: 0x17007C61 RID: 31841
		// (get) Token: 0x060174D9 RID: 95449 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007C62 RID: 31842
		// (get) Token: 0x060174DA RID: 95450 RVA: 0x003352BE File Offset: 0x003334BE
		internal override int ElementTypeId
		{
			get
			{
				return 11169;
			}
		}

		// Token: 0x060174DB RID: 95451 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007C63 RID: 31843
		// (get) Token: 0x060174DC RID: 95452 RVA: 0x003352C5 File Offset: 0x003334C5
		internal override string[] AttributeTagNames
		{
			get
			{
				return ReviewedList.attributeTagNames;
			}
		}

		// Token: 0x17007C64 RID: 31844
		// (get) Token: 0x060174DD RID: 95453 RVA: 0x003352CC File Offset: 0x003334CC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ReviewedList.attributeNamespaceIds;
			}
		}

		// Token: 0x17007C65 RID: 31845
		// (get) Token: 0x060174DE RID: 95454 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060174DF RID: 95455 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060174E0 RID: 95456 RVA: 0x00293ECF File Offset: 0x002920CF
		public ReviewedList()
		{
		}

		// Token: 0x060174E1 RID: 95457 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ReviewedList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060174E2 RID: 95458 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ReviewedList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060174E3 RID: 95459 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ReviewedList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060174E4 RID: 95460 RVA: 0x003352D3 File Offset: 0x003334D3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "reviewed" == name)
			{
				return new Reviewed();
			}
			return null;
		}

		// Token: 0x060174E5 RID: 95461 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060174E6 RID: 95462 RVA: 0x003352EE File Offset: 0x003334EE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ReviewedList>(deep);
		}

		// Token: 0x060174E7 RID: 95463 RVA: 0x003352F8 File Offset: 0x003334F8
		// Note: this type is marked as 'beforefieldinit'.
		static ReviewedList()
		{
			byte[] array = new byte[1];
			ReviewedList.attributeNamespaceIds = array;
		}

		// Token: 0x04009BDF RID: 39903
		private const string tagName = "reviewedList";

		// Token: 0x04009BE0 RID: 39904
		private const byte tagNsId = 22;

		// Token: 0x04009BE1 RID: 39905
		internal const int ElementTypeIdConst = 11169;

		// Token: 0x04009BE2 RID: 39906
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009BE3 RID: 39907
		private static byte[] attributeNamespaceIds;
	}
}
