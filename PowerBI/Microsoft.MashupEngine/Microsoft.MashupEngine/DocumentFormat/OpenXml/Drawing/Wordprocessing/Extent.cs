using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028AB RID: 10411
	[GeneratedCode("DomGen", "2.0")]
	internal class Extent : OpenXmlLeafElement
	{
		// Token: 0x1700689D RID: 26781
		// (get) Token: 0x0601483A RID: 84026 RVA: 0x00314406 File Offset: 0x00312606
		public override string LocalName
		{
			get
			{
				return "extent";
			}
		}

		// Token: 0x1700689E RID: 26782
		// (get) Token: 0x0601483B RID: 84027 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x1700689F RID: 26783
		// (get) Token: 0x0601483C RID: 84028 RVA: 0x0031440D File Offset: 0x0031260D
		internal override int ElementTypeId
		{
			get
			{
				return 10708;
			}
		}

		// Token: 0x0601483D RID: 84029 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170068A0 RID: 26784
		// (get) Token: 0x0601483E RID: 84030 RVA: 0x00314414 File Offset: 0x00312614
		internal override string[] AttributeTagNames
		{
			get
			{
				return Extent.attributeTagNames;
			}
		}

		// Token: 0x170068A1 RID: 26785
		// (get) Token: 0x0601483F RID: 84031 RVA: 0x0031441B File Offset: 0x0031261B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Extent.attributeNamespaceIds;
			}
		}

		// Token: 0x170068A2 RID: 26786
		// (get) Token: 0x06014840 RID: 84032 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06014841 RID: 84033 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170068A3 RID: 26787
		// (get) Token: 0x06014842 RID: 84034 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x06014843 RID: 84035 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06014845 RID: 84037 RVA: 0x002FCAAF File Offset: 0x002FACAF
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

		// Token: 0x06014846 RID: 84038 RVA: 0x00314422 File Offset: 0x00312622
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Extent>(deep);
		}

		// Token: 0x06014847 RID: 84039 RVA: 0x0031442C File Offset: 0x0031262C
		// Note: this type is marked as 'beforefieldinit'.
		static Extent()
		{
			byte[] array = new byte[2];
			Extent.attributeNamespaceIds = array;
		}

		// Token: 0x04008E6F RID: 36463
		private const string tagName = "extent";

		// Token: 0x04008E70 RID: 36464
		private const byte tagNsId = 16;

		// Token: 0x04008E71 RID: 36465
		internal const int ElementTypeIdConst = 10708;

		// Token: 0x04008E72 RID: 36466
		private static string[] attributeTagNames = new string[] { "cx", "cy" };

		// Token: 0x04008E73 RID: 36467
		private static byte[] attributeNamespaceIds;
	}
}
