using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C61 RID: 11361
	[ChildElementInfo(typeof(WebPublishObject))]
	[GeneratedCode("DomGen", "2.0")]
	internal class WebPublishObjects : OpenXmlCompositeElement
	{
		// Token: 0x17008287 RID: 33415
		// (get) Token: 0x06018252 RID: 98898 RVA: 0x0033EF2F File Offset: 0x0033D12F
		public override string LocalName
		{
			get
			{
				return "webPublishObjects";
			}
		}

		// Token: 0x17008288 RID: 33416
		// (get) Token: 0x06018253 RID: 98899 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008289 RID: 33417
		// (get) Token: 0x06018254 RID: 98900 RVA: 0x0033EF36 File Offset: 0x0033D136
		internal override int ElementTypeId
		{
			get
			{
				return 11342;
			}
		}

		// Token: 0x06018255 RID: 98901 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700828A RID: 33418
		// (get) Token: 0x06018256 RID: 98902 RVA: 0x0033EF3D File Offset: 0x0033D13D
		internal override string[] AttributeTagNames
		{
			get
			{
				return WebPublishObjects.attributeTagNames;
			}
		}

		// Token: 0x1700828B RID: 33419
		// (get) Token: 0x06018257 RID: 98903 RVA: 0x0033EF44 File Offset: 0x0033D144
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WebPublishObjects.attributeNamespaceIds;
			}
		}

		// Token: 0x1700828C RID: 33420
		// (get) Token: 0x06018258 RID: 98904 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018259 RID: 98905 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601825A RID: 98906 RVA: 0x00293ECF File Offset: 0x002920CF
		public WebPublishObjects()
		{
		}

		// Token: 0x0601825B RID: 98907 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WebPublishObjects(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601825C RID: 98908 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WebPublishObjects(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601825D RID: 98909 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WebPublishObjects(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601825E RID: 98910 RVA: 0x0033EF4B File Offset: 0x0033D14B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "webPublishObject" == name)
			{
				return new WebPublishObject();
			}
			return null;
		}

		// Token: 0x0601825F RID: 98911 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018260 RID: 98912 RVA: 0x0033EF66 File Offset: 0x0033D166
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WebPublishObjects>(deep);
		}

		// Token: 0x06018261 RID: 98913 RVA: 0x0033EF70 File Offset: 0x0033D170
		// Note: this type is marked as 'beforefieldinit'.
		static WebPublishObjects()
		{
			byte[] array = new byte[1];
			WebPublishObjects.attributeNamespaceIds = array;
		}

		// Token: 0x04009F08 RID: 40712
		private const string tagName = "webPublishObjects";

		// Token: 0x04009F09 RID: 40713
		private const byte tagNsId = 22;

		// Token: 0x04009F0A RID: 40714
		internal const int ElementTypeIdConst = 11342;

		// Token: 0x04009F0B RID: 40715
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009F0C RID: 40716
		private static byte[] attributeNamespaceIds;
	}
}
