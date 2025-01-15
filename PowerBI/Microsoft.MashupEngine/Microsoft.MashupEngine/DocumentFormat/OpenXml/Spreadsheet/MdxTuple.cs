using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BFF RID: 11263
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NameIndex))]
	internal class MdxTuple : OpenXmlCompositeElement
	{
		// Token: 0x17007F2E RID: 32558
		// (get) Token: 0x06017AD8 RID: 96984 RVA: 0x00300F6A File Offset: 0x002FF16A
		public override string LocalName
		{
			get
			{
				return "t";
			}
		}

		// Token: 0x17007F2F RID: 32559
		// (get) Token: 0x06017AD9 RID: 96985 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F30 RID: 32560
		// (get) Token: 0x06017ADA RID: 96986 RVA: 0x00339D32 File Offset: 0x00337F32
		internal override int ElementTypeId
		{
			get
			{
				return 11242;
			}
		}

		// Token: 0x06017ADB RID: 96987 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F31 RID: 32561
		// (get) Token: 0x06017ADC RID: 96988 RVA: 0x00339D39 File Offset: 0x00337F39
		internal override string[] AttributeTagNames
		{
			get
			{
				return MdxTuple.attributeTagNames;
			}
		}

		// Token: 0x17007F32 RID: 32562
		// (get) Token: 0x06017ADD RID: 96989 RVA: 0x00339D40 File Offset: 0x00337F40
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MdxTuple.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F33 RID: 32563
		// (get) Token: 0x06017ADE RID: 96990 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017ADF RID: 96991 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "c")]
		public UInt32Value MemberIndexCount
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

		// Token: 0x17007F34 RID: 32564
		// (get) Token: 0x06017AE0 RID: 96992 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017AE1 RID: 96993 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ct")]
		public StringValue CultureCurrency
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007F35 RID: 32565
		// (get) Token: 0x06017AE2 RID: 96994 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06017AE3 RID: 96995 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "si")]
		public UInt32Value FormattingStringIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007F36 RID: 32566
		// (get) Token: 0x06017AE4 RID: 96996 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06017AE5 RID: 96997 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "fi")]
		public UInt32Value FormatIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007F37 RID: 32567
		// (get) Token: 0x06017AE6 RID: 96998 RVA: 0x002EB784 File Offset: 0x002E9984
		// (set) Token: 0x06017AE7 RID: 96999 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "bc")]
		public HexBinaryValue BackgroundColor
		{
			get
			{
				return (HexBinaryValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007F38 RID: 32568
		// (get) Token: 0x06017AE8 RID: 97000 RVA: 0x003137E6 File Offset: 0x003119E6
		// (set) Token: 0x06017AE9 RID: 97001 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "fc")]
		public HexBinaryValue ForegroundColor
		{
			get
			{
				return (HexBinaryValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007F39 RID: 32569
		// (get) Token: 0x06017AEA RID: 97002 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017AEB RID: 97003 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "i")]
		public BooleanValue Italic
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007F3A RID: 32570
		// (get) Token: 0x06017AEC RID: 97004 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017AED RID: 97005 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "u")]
		public BooleanValue Underline
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007F3B RID: 32571
		// (get) Token: 0x06017AEE RID: 97006 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06017AEF RID: 97007 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "st")]
		public BooleanValue Strikethrough
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007F3C RID: 32572
		// (get) Token: 0x06017AF0 RID: 97008 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06017AF1 RID: 97009 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "b")]
		public BooleanValue Bold
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x06017AF2 RID: 97010 RVA: 0x00293ECF File Offset: 0x002920CF
		public MdxTuple()
		{
		}

		// Token: 0x06017AF3 RID: 97011 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MdxTuple(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017AF4 RID: 97012 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MdxTuple(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017AF5 RID: 97013 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MdxTuple(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017AF6 RID: 97014 RVA: 0x00339D47 File Offset: 0x00337F47
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "n" == name)
			{
				return new NameIndex();
			}
			return null;
		}

		// Token: 0x06017AF7 RID: 97015 RVA: 0x00339D64 File Offset: 0x00337F64
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "c" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "ct" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "si" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "fi" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "bc" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "fc" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "i" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "u" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "st" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "b" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017AF8 RID: 97016 RVA: 0x00339E55 File Offset: 0x00338055
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MdxTuple>(deep);
		}

		// Token: 0x06017AF9 RID: 97017 RVA: 0x00339E60 File Offset: 0x00338060
		// Note: this type is marked as 'beforefieldinit'.
		static MdxTuple()
		{
			byte[] array = new byte[10];
			MdxTuple.attributeNamespaceIds = array;
		}

		// Token: 0x04009D22 RID: 40226
		private const string tagName = "t";

		// Token: 0x04009D23 RID: 40227
		private const byte tagNsId = 22;

		// Token: 0x04009D24 RID: 40228
		internal const int ElementTypeIdConst = 11242;

		// Token: 0x04009D25 RID: 40229
		private static string[] attributeTagNames = new string[] { "c", "ct", "si", "fi", "bc", "fc", "i", "u", "st", "b" };

		// Token: 0x04009D26 RID: 40230
		private static byte[] attributeNamespaceIds;
	}
}
