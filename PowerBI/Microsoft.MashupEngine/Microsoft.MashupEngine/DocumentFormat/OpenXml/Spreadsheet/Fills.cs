using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C73 RID: 11379
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Fill))]
	internal class Fills : OpenXmlCompositeElement
	{
		// Token: 0x170082DB RID: 33499
		// (get) Token: 0x0601832F RID: 99119 RVA: 0x0033F4BC File Offset: 0x0033D6BC
		public override string LocalName
		{
			get
			{
				return "fills";
			}
		}

		// Token: 0x170082DC RID: 33500
		// (get) Token: 0x06018330 RID: 99120 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082DD RID: 33501
		// (get) Token: 0x06018331 RID: 99121 RVA: 0x0033F4C3 File Offset: 0x0033D6C3
		internal override int ElementTypeId
		{
			get
			{
				return 11359;
			}
		}

		// Token: 0x06018332 RID: 99122 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170082DE RID: 33502
		// (get) Token: 0x06018333 RID: 99123 RVA: 0x0033F4CA File Offset: 0x0033D6CA
		internal override string[] AttributeTagNames
		{
			get
			{
				return Fills.attributeTagNames;
			}
		}

		// Token: 0x170082DF RID: 33503
		// (get) Token: 0x06018334 RID: 99124 RVA: 0x0033F4D1 File Offset: 0x0033D6D1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Fills.attributeNamespaceIds;
			}
		}

		// Token: 0x170082E0 RID: 33504
		// (get) Token: 0x06018335 RID: 99125 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018336 RID: 99126 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018337 RID: 99127 RVA: 0x00293ECF File Offset: 0x002920CF
		public Fills()
		{
		}

		// Token: 0x06018338 RID: 99128 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Fills(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018339 RID: 99129 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Fills(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601833A RID: 99130 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Fills(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601833B RID: 99131 RVA: 0x0033F4D8 File Offset: 0x0033D6D8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "fill" == name)
			{
				return new Fill();
			}
			return null;
		}

		// Token: 0x0601833C RID: 99132 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601833D RID: 99133 RVA: 0x0033F4F3 File Offset: 0x0033D6F3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Fills>(deep);
		}

		// Token: 0x0601833E RID: 99134 RVA: 0x0033F4FC File Offset: 0x0033D6FC
		// Note: this type is marked as 'beforefieldinit'.
		static Fills()
		{
			byte[] array = new byte[1];
			Fills.attributeNamespaceIds = array;
		}

		// Token: 0x04009F4B RID: 40779
		private const string tagName = "fills";

		// Token: 0x04009F4C RID: 40780
		private const byte tagNsId = 22;

		// Token: 0x04009F4D RID: 40781
		internal const int ElementTypeIdConst = 11359;

		// Token: 0x04009F4E RID: 40782
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009F4F RID: 40783
		private static byte[] attributeNamespaceIds;
	}
}
