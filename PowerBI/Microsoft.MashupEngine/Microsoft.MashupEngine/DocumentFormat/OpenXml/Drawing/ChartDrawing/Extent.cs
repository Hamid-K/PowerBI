using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002642 RID: 9794
	[GeneratedCode("DomGen", "2.0")]
	internal class Extent : OpenXmlLeafElement
	{
		// Token: 0x17005ADD RID: 23261
		// (get) Token: 0x0601291C RID: 76060 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x17005ADE RID: 23262
		// (get) Token: 0x0601291D RID: 76061 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005ADF RID: 23263
		// (get) Token: 0x0601291E RID: 76062 RVA: 0x002FCA9A File Offset: 0x002FAC9A
		internal override int ElementTypeId
		{
			get
			{
				return 10612;
			}
		}

		// Token: 0x0601291F RID: 76063 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005AE0 RID: 23264
		// (get) Token: 0x06012920 RID: 76064 RVA: 0x002FCAA1 File Offset: 0x002FACA1
		internal override string[] AttributeTagNames
		{
			get
			{
				return Extent.attributeTagNames;
			}
		}

		// Token: 0x17005AE1 RID: 23265
		// (get) Token: 0x06012921 RID: 76065 RVA: 0x002FCAA8 File Offset: 0x002FACA8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Extent.attributeNamespaceIds;
			}
		}

		// Token: 0x17005AE2 RID: 23266
		// (get) Token: 0x06012922 RID: 76066 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06012923 RID: 76067 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "cx")]
		public Int64Value Cx
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005AE3 RID: 23267
		// (get) Token: 0x06012924 RID: 76068 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x06012925 RID: 76069 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cy")]
		public Int64Value Cy
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06012927 RID: 76071 RVA: 0x002FCAAF File Offset: 0x002FACAF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "cx" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "cy" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012928 RID: 76072 RVA: 0x002FCAE5 File Offset: 0x002FACE5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Extent>(deep);
		}

		// Token: 0x06012929 RID: 76073 RVA: 0x002FCAF0 File Offset: 0x002FACF0
		// Note: this type is marked as 'beforefieldinit'.
		static Extent()
		{
			byte[] array = new byte[2];
			Extent.attributeNamespaceIds = array;
		}

		// Token: 0x040080B5 RID: 32949
		private const string tagName = "ext";

		// Token: 0x040080B6 RID: 32950
		private const byte tagNsId = 12;

		// Token: 0x040080B7 RID: 32951
		internal const int ElementTypeIdConst = 10612;

		// Token: 0x040080B8 RID: 32952
		private static string[] attributeTagNames = new string[] { "cx", "cy" };

		// Token: 0x040080B9 RID: 32953
		private static byte[] attributeNamespaceIds;
	}
}
