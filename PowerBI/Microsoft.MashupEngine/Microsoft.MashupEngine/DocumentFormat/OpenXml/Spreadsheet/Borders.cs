using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C74 RID: 11380
	[ChildElementInfo(typeof(Border))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Borders : OpenXmlCompositeElement
	{
		// Token: 0x170082E1 RID: 33505
		// (get) Token: 0x0601833F RID: 99135 RVA: 0x0033F52B File Offset: 0x0033D72B
		public override string LocalName
		{
			get
			{
				return "borders";
			}
		}

		// Token: 0x170082E2 RID: 33506
		// (get) Token: 0x06018340 RID: 99136 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082E3 RID: 33507
		// (get) Token: 0x06018341 RID: 99137 RVA: 0x0033F532 File Offset: 0x0033D732
		internal override int ElementTypeId
		{
			get
			{
				return 11360;
			}
		}

		// Token: 0x06018342 RID: 99138 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170082E4 RID: 33508
		// (get) Token: 0x06018343 RID: 99139 RVA: 0x0033F539 File Offset: 0x0033D739
		internal override string[] AttributeTagNames
		{
			get
			{
				return Borders.attributeTagNames;
			}
		}

		// Token: 0x170082E5 RID: 33509
		// (get) Token: 0x06018344 RID: 99140 RVA: 0x0033F540 File Offset: 0x0033D740
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Borders.attributeNamespaceIds;
			}
		}

		// Token: 0x170082E6 RID: 33510
		// (get) Token: 0x06018345 RID: 99141 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018346 RID: 99142 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018347 RID: 99143 RVA: 0x00293ECF File Offset: 0x002920CF
		public Borders()
		{
		}

		// Token: 0x06018348 RID: 99144 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Borders(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018349 RID: 99145 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Borders(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601834A RID: 99146 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Borders(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601834B RID: 99147 RVA: 0x0033F547 File Offset: 0x0033D747
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "border" == name)
			{
				return new Border();
			}
			return null;
		}

		// Token: 0x0601834C RID: 99148 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601834D RID: 99149 RVA: 0x0033F562 File Offset: 0x0033D762
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Borders>(deep);
		}

		// Token: 0x0601834E RID: 99150 RVA: 0x0033F56C File Offset: 0x0033D76C
		// Note: this type is marked as 'beforefieldinit'.
		static Borders()
		{
			byte[] array = new byte[1];
			Borders.attributeNamespaceIds = array;
		}

		// Token: 0x04009F50 RID: 40784
		private const string tagName = "borders";

		// Token: 0x04009F51 RID: 40785
		private const byte tagNsId = 22;

		// Token: 0x04009F52 RID: 40786
		internal const int ElementTypeIdConst = 11360;

		// Token: 0x04009F53 RID: 40787
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009F54 RID: 40788
		private static byte[] attributeNamespaceIds;
	}
}
