using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B68 RID: 11112
	[ChildElementInfo(typeof(MissingItem))]
	[ChildElementInfo(typeof(StringItem))]
	[ChildElementInfo(typeof(NumberItem))]
	[ChildElementInfo(typeof(ErrorItem))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Entries : OpenXmlCompositeElement
	{
		// Token: 0x17007917 RID: 30999
		// (get) Token: 0x06016DD7 RID: 93655 RVA: 0x0032FF0F File Offset: 0x0032E10F
		public override string LocalName
		{
			get
			{
				return "entries";
			}
		}

		// Token: 0x17007918 RID: 31000
		// (get) Token: 0x06016DD8 RID: 93656 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007919 RID: 31001
		// (get) Token: 0x06016DD9 RID: 93657 RVA: 0x0032FF16 File Offset: 0x0032E116
		internal override int ElementTypeId
		{
			get
			{
				return 11091;
			}
		}

		// Token: 0x06016DDA RID: 93658 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700791A RID: 31002
		// (get) Token: 0x06016DDB RID: 93659 RVA: 0x0032FF1D File Offset: 0x0032E11D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Entries.attributeTagNames;
			}
		}

		// Token: 0x1700791B RID: 31003
		// (get) Token: 0x06016DDC RID: 93660 RVA: 0x0032FF24 File Offset: 0x0032E124
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Entries.attributeNamespaceIds;
			}
		}

		// Token: 0x1700791C RID: 31004
		// (get) Token: 0x06016DDD RID: 93661 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016DDE RID: 93662 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06016DDF RID: 93663 RVA: 0x00293ECF File Offset: 0x002920CF
		public Entries()
		{
		}

		// Token: 0x06016DE0 RID: 93664 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Entries(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016DE1 RID: 93665 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Entries(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016DE2 RID: 93666 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Entries(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016DE3 RID: 93667 RVA: 0x0032FF2C File Offset: 0x0032E12C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "m" == name)
			{
				return new MissingItem();
			}
			if (22 == namespaceId && "n" == name)
			{
				return new NumberItem();
			}
			if (22 == namespaceId && "e" == name)
			{
				return new ErrorItem();
			}
			if (22 == namespaceId && "s" == name)
			{
				return new StringItem();
			}
			return null;
		}

		// Token: 0x06016DE4 RID: 93668 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016DE5 RID: 93669 RVA: 0x0032FF9A File Offset: 0x0032E19A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Entries>(deep);
		}

		// Token: 0x06016DE6 RID: 93670 RVA: 0x0032FFA4 File Offset: 0x0032E1A4
		// Note: this type is marked as 'beforefieldinit'.
		static Entries()
		{
			byte[] array = new byte[1];
			Entries.attributeNamespaceIds = array;
		}

		// Token: 0x04009A3C RID: 39484
		private const string tagName = "entries";

		// Token: 0x04009A3D RID: 39485
		private const byte tagNsId = 22;

		// Token: 0x04009A3E RID: 39486
		internal const int ElementTypeIdConst = 11091;

		// Token: 0x04009A3F RID: 39487
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009A40 RID: 39488
		private static byte[] attributeNamespaceIds;
	}
}
