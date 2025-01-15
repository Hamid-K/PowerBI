using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002748 RID: 10056
	[GeneratedCode("DomGen", "2.0")]
	internal class BulletSizePoints : OpenXmlLeafElement
	{
		// Token: 0x17006079 RID: 24697
		// (get) Token: 0x060135A2 RID: 79266 RVA: 0x003063D7 File Offset: 0x003045D7
		public override string LocalName
		{
			get
			{
				return "buSzPts";
			}
		}

		// Token: 0x1700607A RID: 24698
		// (get) Token: 0x060135A3 RID: 79267 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700607B RID: 24699
		// (get) Token: 0x060135A4 RID: 79268 RVA: 0x003063DE File Offset: 0x003045DE
		internal override int ElementTypeId
		{
			get
			{
				return 10106;
			}
		}

		// Token: 0x060135A5 RID: 79269 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700607C RID: 24700
		// (get) Token: 0x060135A6 RID: 79270 RVA: 0x003063E5 File Offset: 0x003045E5
		internal override string[] AttributeTagNames
		{
			get
			{
				return BulletSizePoints.attributeTagNames;
			}
		}

		// Token: 0x1700607D RID: 24701
		// (get) Token: 0x060135A7 RID: 79271 RVA: 0x003063EC File Offset: 0x003045EC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BulletSizePoints.attributeNamespaceIds;
			}
		}

		// Token: 0x1700607E RID: 24702
		// (get) Token: 0x060135A8 RID: 79272 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060135A9 RID: 79273 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public Int32Value Val
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060135AB RID: 79275 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060135AC RID: 79276 RVA: 0x003063F3 File Offset: 0x003045F3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BulletSizePoints>(deep);
		}

		// Token: 0x060135AD RID: 79277 RVA: 0x003063FC File Offset: 0x003045FC
		// Note: this type is marked as 'beforefieldinit'.
		static BulletSizePoints()
		{
			byte[] array = new byte[1];
			BulletSizePoints.attributeNamespaceIds = array;
		}

		// Token: 0x040085C8 RID: 34248
		private const string tagName = "buSzPts";

		// Token: 0x040085C9 RID: 34249
		private const byte tagNsId = 10;

		// Token: 0x040085CA RID: 34250
		internal const int ElementTypeIdConst = 10106;

		// Token: 0x040085CB RID: 34251
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040085CC RID: 34252
		private static byte[] attributeNamespaceIds;
	}
}
