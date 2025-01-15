using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200273A RID: 10042
	[GeneratedCode("DomGen", "2.0")]
	internal class NormalAutoFit : OpenXmlLeafElement
	{
		// Token: 0x17006042 RID: 24642
		// (get) Token: 0x06013519 RID: 79129 RVA: 0x003060CD File Offset: 0x003042CD
		public override string LocalName
		{
			get
			{
				return "normAutofit";
			}
		}

		// Token: 0x17006043 RID: 24643
		// (get) Token: 0x0601351A RID: 79130 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006044 RID: 24644
		// (get) Token: 0x0601351B RID: 79131 RVA: 0x003060D4 File Offset: 0x003042D4
		internal override int ElementTypeId
		{
			get
			{
				return 10100;
			}
		}

		// Token: 0x0601351C RID: 79132 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006045 RID: 24645
		// (get) Token: 0x0601351D RID: 79133 RVA: 0x003060DB File Offset: 0x003042DB
		internal override string[] AttributeTagNames
		{
			get
			{
				return NormalAutoFit.attributeTagNames;
			}
		}

		// Token: 0x17006046 RID: 24646
		// (get) Token: 0x0601351E RID: 79134 RVA: 0x003060E2 File Offset: 0x003042E2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NormalAutoFit.attributeNamespaceIds;
			}
		}

		// Token: 0x17006047 RID: 24647
		// (get) Token: 0x0601351F RID: 79135 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013520 RID: 79136 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "fontScale")]
		public Int32Value FontScale
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

		// Token: 0x17006048 RID: 24648
		// (get) Token: 0x06013521 RID: 79137 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06013522 RID: 79138 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "lnSpcReduction")]
		public Int32Value LineSpaceReduction
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06013524 RID: 79140 RVA: 0x003060E9 File Offset: 0x003042E9
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "fontScale" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "lnSpcReduction" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013525 RID: 79141 RVA: 0x0030611F File Offset: 0x0030431F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NormalAutoFit>(deep);
		}

		// Token: 0x06013526 RID: 79142 RVA: 0x00306128 File Offset: 0x00304328
		// Note: this type is marked as 'beforefieldinit'.
		static NormalAutoFit()
		{
			byte[] array = new byte[2];
			NormalAutoFit.attributeNamespaceIds = array;
		}

		// Token: 0x0400859B RID: 34203
		private const string tagName = "normAutofit";

		// Token: 0x0400859C RID: 34204
		private const byte tagNsId = 10;

		// Token: 0x0400859D RID: 34205
		internal const int ElementTypeIdConst = 10100;

		// Token: 0x0400859E RID: 34206
		private static string[] attributeTagNames = new string[] { "fontScale", "lnSpcReduction" };

		// Token: 0x0400859F RID: 34207
		private static byte[] attributeNamespaceIds;
	}
}
