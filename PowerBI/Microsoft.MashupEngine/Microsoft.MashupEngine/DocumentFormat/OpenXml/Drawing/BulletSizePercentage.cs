using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002747 RID: 10055
	[GeneratedCode("DomGen", "2.0")]
	internal class BulletSizePercentage : OpenXmlLeafElement
	{
		// Token: 0x17006073 RID: 24691
		// (get) Token: 0x06013596 RID: 79254 RVA: 0x00306380 File Offset: 0x00304580
		public override string LocalName
		{
			get
			{
				return "buSzPct";
			}
		}

		// Token: 0x17006074 RID: 24692
		// (get) Token: 0x06013597 RID: 79255 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006075 RID: 24693
		// (get) Token: 0x06013598 RID: 79256 RVA: 0x00306387 File Offset: 0x00304587
		internal override int ElementTypeId
		{
			get
			{
				return 10105;
			}
		}

		// Token: 0x06013599 RID: 79257 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006076 RID: 24694
		// (get) Token: 0x0601359A RID: 79258 RVA: 0x0030638E File Offset: 0x0030458E
		internal override string[] AttributeTagNames
		{
			get
			{
				return BulletSizePercentage.attributeTagNames;
			}
		}

		// Token: 0x17006077 RID: 24695
		// (get) Token: 0x0601359B RID: 79259 RVA: 0x00306395 File Offset: 0x00304595
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BulletSizePercentage.attributeNamespaceIds;
			}
		}

		// Token: 0x17006078 RID: 24696
		// (get) Token: 0x0601359C RID: 79260 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601359D RID: 79261 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601359F RID: 79263 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060135A0 RID: 79264 RVA: 0x0030639C File Offset: 0x0030459C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BulletSizePercentage>(deep);
		}

		// Token: 0x060135A1 RID: 79265 RVA: 0x003063A8 File Offset: 0x003045A8
		// Note: this type is marked as 'beforefieldinit'.
		static BulletSizePercentage()
		{
			byte[] array = new byte[1];
			BulletSizePercentage.attributeNamespaceIds = array;
		}

		// Token: 0x040085C3 RID: 34243
		private const string tagName = "buSzPct";

		// Token: 0x040085C4 RID: 34244
		private const byte tagNsId = 10;

		// Token: 0x040085C5 RID: 34245
		internal const int ElementTypeIdConst = 10105;

		// Token: 0x040085C6 RID: 34246
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040085C7 RID: 34247
		private static byte[] attributeNamespaceIds;
	}
}
