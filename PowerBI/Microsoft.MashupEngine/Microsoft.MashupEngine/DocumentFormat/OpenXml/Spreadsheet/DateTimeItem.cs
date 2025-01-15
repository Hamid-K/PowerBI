using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B59 RID: 11097
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MemberPropertyIndex))]
	internal class DateTimeItem : OpenXmlCompositeElement
	{
		// Token: 0x170078B4 RID: 30900
		// (get) Token: 0x06016CF3 RID: 93427 RVA: 0x003198B5 File Offset: 0x00317AB5
		public override string LocalName
		{
			get
			{
				return "d";
			}
		}

		// Token: 0x170078B5 RID: 30901
		// (get) Token: 0x06016CF4 RID: 93428 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170078B6 RID: 30902
		// (get) Token: 0x06016CF5 RID: 93429 RVA: 0x0032F5BC File Offset: 0x0032D7BC
		internal override int ElementTypeId
		{
			get
			{
				return 11080;
			}
		}

		// Token: 0x06016CF6 RID: 93430 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170078B7 RID: 30903
		// (get) Token: 0x06016CF7 RID: 93431 RVA: 0x0032F5C3 File Offset: 0x0032D7C3
		internal override string[] AttributeTagNames
		{
			get
			{
				return DateTimeItem.attributeTagNames;
			}
		}

		// Token: 0x170078B8 RID: 30904
		// (get) Token: 0x06016CF8 RID: 93432 RVA: 0x0032F5CA File Offset: 0x0032D7CA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DateTimeItem.attributeNamespaceIds;
			}
		}

		// Token: 0x170078B9 RID: 30905
		// (get) Token: 0x06016CF9 RID: 93433 RVA: 0x0032F5D1 File Offset: 0x0032D7D1
		// (set) Token: 0x06016CFA RID: 93434 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "v")]
		public DateTimeValue Val
		{
			get
			{
				return (DateTimeValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170078BA RID: 30906
		// (get) Token: 0x06016CFB RID: 93435 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016CFC RID: 93436 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "u")]
		public BooleanValue Unused
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170078BB RID: 30907
		// (get) Token: 0x06016CFD RID: 93437 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016CFE RID: 93438 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "f")]
		public BooleanValue Calculated
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170078BC RID: 30908
		// (get) Token: 0x06016CFF RID: 93439 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06016D00 RID: 93440 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "c")]
		public StringValue Caption
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170078BD RID: 30909
		// (get) Token: 0x06016D01 RID: 93441 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06016D02 RID: 93442 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "cp")]
		public UInt32Value PropertyCount
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06016D03 RID: 93443 RVA: 0x00293ECF File Offset: 0x002920CF
		public DateTimeItem()
		{
		}

		// Token: 0x06016D04 RID: 93444 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DateTimeItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D05 RID: 93445 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DateTimeItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D06 RID: 93446 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DateTimeItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016D07 RID: 93447 RVA: 0x0032F0D8 File Offset: 0x0032D2D8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "x" == name)
			{
				return new MemberPropertyIndex();
			}
			return null;
		}

		// Token: 0x06016D08 RID: 93448 RVA: 0x0032F5E0 File Offset: 0x0032D7E0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "v" == name)
			{
				return new DateTimeValue();
			}
			if (namespaceId == 0 && "u" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "f" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "c" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cp" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016D09 RID: 93449 RVA: 0x0032F663 File Offset: 0x0032D863
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DateTimeItem>(deep);
		}

		// Token: 0x06016D0A RID: 93450 RVA: 0x0032F66C File Offset: 0x0032D86C
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimeItem()
		{
			byte[] array = new byte[5];
			DateTimeItem.attributeNamespaceIds = array;
		}

		// Token: 0x040099FD RID: 39421
		private const string tagName = "d";

		// Token: 0x040099FE RID: 39422
		private const byte tagNsId = 22;

		// Token: 0x040099FF RID: 39423
		internal const int ElementTypeIdConst = 11080;

		// Token: 0x04009A00 RID: 39424
		private static string[] attributeTagNames = new string[] { "v", "u", "f", "c", "cp" };

		// Token: 0x04009A01 RID: 39425
		private static byte[] attributeNamespaceIds;
	}
}
