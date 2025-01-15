using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C88 RID: 11400
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(WebPublishItem))]
	internal class WebPublishItems : OpenXmlCompositeElement
	{
		// Token: 0x17008389 RID: 33673
		// (get) Token: 0x060184B9 RID: 99513 RVA: 0x003403E0 File Offset: 0x0033E5E0
		public override string LocalName
		{
			get
			{
				return "webPublishItems";
			}
		}

		// Token: 0x1700838A RID: 33674
		// (get) Token: 0x060184BA RID: 99514 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700838B RID: 33675
		// (get) Token: 0x060184BB RID: 99515 RVA: 0x003403E7 File Offset: 0x0033E5E7
		internal override int ElementTypeId
		{
			get
			{
				return 11379;
			}
		}

		// Token: 0x060184BC RID: 99516 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700838C RID: 33676
		// (get) Token: 0x060184BD RID: 99517 RVA: 0x003403EE File Offset: 0x0033E5EE
		internal override string[] AttributeTagNames
		{
			get
			{
				return WebPublishItems.attributeTagNames;
			}
		}

		// Token: 0x1700838D RID: 33677
		// (get) Token: 0x060184BE RID: 99518 RVA: 0x003403F5 File Offset: 0x0033E5F5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WebPublishItems.attributeNamespaceIds;
			}
		}

		// Token: 0x1700838E RID: 33678
		// (get) Token: 0x060184BF RID: 99519 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060184C0 RID: 99520 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060184C1 RID: 99521 RVA: 0x00293ECF File Offset: 0x002920CF
		public WebPublishItems()
		{
		}

		// Token: 0x060184C2 RID: 99522 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WebPublishItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060184C3 RID: 99523 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WebPublishItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060184C4 RID: 99524 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WebPublishItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060184C5 RID: 99525 RVA: 0x003403FC File Offset: 0x0033E5FC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "webPublishItem" == name)
			{
				return new WebPublishItem();
			}
			return null;
		}

		// Token: 0x060184C6 RID: 99526 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060184C7 RID: 99527 RVA: 0x00340417 File Offset: 0x0033E617
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WebPublishItems>(deep);
		}

		// Token: 0x060184C8 RID: 99528 RVA: 0x00340420 File Offset: 0x0033E620
		// Note: this type is marked as 'beforefieldinit'.
		static WebPublishItems()
		{
			byte[] array = new byte[1];
			WebPublishItems.attributeNamespaceIds = array;
		}

		// Token: 0x04009FAD RID: 40877
		private const string tagName = "webPublishItems";

		// Token: 0x04009FAE RID: 40878
		private const byte tagNsId = 22;

		// Token: 0x04009FAF RID: 40879
		internal const int ElementTypeIdConst = 11379;

		// Token: 0x04009FB0 RID: 40880
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009FB1 RID: 40881
		private static byte[] attributeNamespaceIds;
	}
}
