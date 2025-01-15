using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F0C RID: 12044
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingStart : OpenXmlLeafElement
	{
		// Token: 0x17008E18 RID: 36376
		// (get) Token: 0x06019B87 RID: 105351 RVA: 0x003544BC File Offset: 0x003526BC
		public override string LocalName
		{
			get
			{
				return "numStart";
			}
		}

		// Token: 0x17008E19 RID: 36377
		// (get) Token: 0x06019B88 RID: 105352 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E1A RID: 36378
		// (get) Token: 0x06019B89 RID: 105353 RVA: 0x003544C3 File Offset: 0x003526C3
		internal override int ElementTypeId
		{
			get
			{
				return 11682;
			}
		}

		// Token: 0x06019B8A RID: 105354 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008E1B RID: 36379
		// (get) Token: 0x06019B8B RID: 105355 RVA: 0x003544CA File Offset: 0x003526CA
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberingStart.attributeTagNames;
			}
		}

		// Token: 0x17008E1C RID: 36380
		// (get) Token: 0x06019B8C RID: 105356 RVA: 0x003544D1 File Offset: 0x003526D1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberingStart.attributeNamespaceIds;
			}
		}

		// Token: 0x17008E1D RID: 36381
		// (get) Token: 0x06019B8D RID: 105357 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x06019B8E RID: 105358 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public UInt16Value Val
		{
			get
			{
				return (UInt16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019B90 RID: 105360 RVA: 0x003544D8 File Offset: 0x003526D8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019B91 RID: 105361 RVA: 0x003544FA File Offset: 0x003526FA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingStart>(deep);
		}

		// Token: 0x0400AA46 RID: 43590
		private const string tagName = "numStart";

		// Token: 0x0400AA47 RID: 43591
		private const byte tagNsId = 23;

		// Token: 0x0400AA48 RID: 43592
		internal const int ElementTypeIdConst = 11682;

		// Token: 0x0400AA49 RID: 43593
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AA4A RID: 43594
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
