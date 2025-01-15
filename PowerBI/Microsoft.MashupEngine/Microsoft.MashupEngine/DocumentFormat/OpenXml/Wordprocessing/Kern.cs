using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E91 RID: 11921
	[GeneratedCode("DomGen", "2.0")]
	internal class Kern : OpenXmlLeafElement
	{
		// Token: 0x17008B33 RID: 35635
		// (get) Token: 0x06019556 RID: 103766 RVA: 0x00348AC8 File Offset: 0x00346CC8
		public override string LocalName
		{
			get
			{
				return "kern";
			}
		}

		// Token: 0x17008B34 RID: 35636
		// (get) Token: 0x06019557 RID: 103767 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B35 RID: 35637
		// (get) Token: 0x06019558 RID: 103768 RVA: 0x00348ACF File Offset: 0x00346CCF
		internal override int ElementTypeId
		{
			get
			{
				return 11595;
			}
		}

		// Token: 0x06019559 RID: 103769 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008B36 RID: 35638
		// (get) Token: 0x0601955A RID: 103770 RVA: 0x00348AD6 File Offset: 0x00346CD6
		internal override string[] AttributeTagNames
		{
			get
			{
				return Kern.attributeTagNames;
			}
		}

		// Token: 0x17008B37 RID: 35639
		// (get) Token: 0x0601955B RID: 103771 RVA: 0x00348ADD File Offset: 0x00346CDD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Kern.attributeNamespaceIds;
			}
		}

		// Token: 0x17008B38 RID: 35640
		// (get) Token: 0x0601955C RID: 103772 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601955D RID: 103773 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public UInt32Value Val
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

		// Token: 0x0601955F RID: 103775 RVA: 0x00348AE4 File Offset: 0x00346CE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019560 RID: 103776 RVA: 0x00348B06 File Offset: 0x00346D06
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Kern>(deep);
		}

		// Token: 0x0400A861 RID: 43105
		private const string tagName = "kern";

		// Token: 0x0400A862 RID: 43106
		private const byte tagNsId = 23;

		// Token: 0x0400A863 RID: 43107
		internal const int ElementTypeIdConst = 11595;

		// Token: 0x0400A864 RID: 43108
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A865 RID: 43109
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
