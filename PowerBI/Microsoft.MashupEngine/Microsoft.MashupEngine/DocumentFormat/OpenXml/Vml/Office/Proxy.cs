using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x0200221E RID: 8734
	[GeneratedCode("DomGen", "2.0")]
	internal class Proxy : OpenXmlLeafElement
	{
		// Token: 0x1700393B RID: 14651
		// (get) Token: 0x0600E025 RID: 57381 RVA: 0x002BFA3C File Offset: 0x002BDC3C
		public override string LocalName
		{
			get
			{
				return "proxy";
			}
		}

		// Token: 0x1700393C RID: 14652
		// (get) Token: 0x0600E026 RID: 57382 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x1700393D RID: 14653
		// (get) Token: 0x0600E027 RID: 57383 RVA: 0x002BFA43 File Offset: 0x002BDC43
		internal override int ElementTypeId
		{
			get
			{
				return 12427;
			}
		}

		// Token: 0x0600E028 RID: 57384 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700393E RID: 14654
		// (get) Token: 0x0600E029 RID: 57385 RVA: 0x002BFA4A File Offset: 0x002BDC4A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Proxy.attributeTagNames;
			}
		}

		// Token: 0x1700393F RID: 14655
		// (get) Token: 0x0600E02A RID: 57386 RVA: 0x002BFA51 File Offset: 0x002BDC51
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Proxy.attributeNamespaceIds;
			}
		}

		// Token: 0x17003940 RID: 14656
		// (get) Token: 0x0600E02B RID: 57387 RVA: 0x002BFA58 File Offset: 0x002BDC58
		// (set) Token: 0x0600E02C RID: 57388 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "start")]
		public TrueFalseBlankValue Start
		{
			get
			{
				return (TrueFalseBlankValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003941 RID: 14657
		// (get) Token: 0x0600E02D RID: 57389 RVA: 0x002BFA67 File Offset: 0x002BDC67
		// (set) Token: 0x0600E02E RID: 57390 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "end")]
		public TrueFalseBlankValue End
		{
			get
			{
				return (TrueFalseBlankValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003942 RID: 14658
		// (get) Token: 0x0600E02F RID: 57391 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E030 RID: 57392 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "idref")]
		public StringValue ShapeReference
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003943 RID: 14659
		// (get) Token: 0x0600E031 RID: 57393 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x0600E032 RID: 57394 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "connectloc")]
		public Int32Value ConnectionLocation
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0600E034 RID: 57396 RVA: 0x002BFA88 File Offset: 0x002BDC88
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "start" == name)
			{
				return new TrueFalseBlankValue();
			}
			if (namespaceId == 0 && "end" == name)
			{
				return new TrueFalseBlankValue();
			}
			if (namespaceId == 0 && "idref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "connectloc" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E035 RID: 57397 RVA: 0x002BFAF5 File Offset: 0x002BDCF5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Proxy>(deep);
		}

		// Token: 0x0600E036 RID: 57398 RVA: 0x002BFB00 File Offset: 0x002BDD00
		// Note: this type is marked as 'beforefieldinit'.
		static Proxy()
		{
			byte[] array = new byte[4];
			Proxy.attributeNamespaceIds = array;
		}

		// Token: 0x04006DD4 RID: 28116
		private const string tagName = "proxy";

		// Token: 0x04006DD5 RID: 28117
		private const byte tagNsId = 27;

		// Token: 0x04006DD6 RID: 28118
		internal const int ElementTypeIdConst = 12427;

		// Token: 0x04006DD7 RID: 28119
		private static string[] attributeTagNames = new string[] { "start", "end", "idref", "connectloc" };

		// Token: 0x04006DD8 RID: 28120
		private static byte[] attributeNamespaceIds;
	}
}
