using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200274A RID: 10058
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TextFontType : OpenXmlLeafElement
	{
		// Token: 0x17006082 RID: 24706
		// (get) Token: 0x060135B4 RID: 79284 RVA: 0x00306442 File Offset: 0x00304642
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextFontType.attributeTagNames;
			}
		}

		// Token: 0x17006083 RID: 24707
		// (get) Token: 0x060135B5 RID: 79285 RVA: 0x00306449 File Offset: 0x00304649
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextFontType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006084 RID: 24708
		// (get) Token: 0x060135B6 RID: 79286 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060135B7 RID: 79287 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "typeface")]
		public StringValue Typeface
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006085 RID: 24709
		// (get) Token: 0x060135B8 RID: 79288 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x060135B9 RID: 79289 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "panose")]
		public HexBinaryValue Panose
		{
			get
			{
				return (HexBinaryValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006086 RID: 24710
		// (get) Token: 0x060135BA RID: 79290 RVA: 0x00306450 File Offset: 0x00304650
		// (set) Token: 0x060135BB RID: 79291 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "pitchFamily")]
		public SByteValue PitchFamily
		{
			get
			{
				return (SByteValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17006087 RID: 24711
		// (get) Token: 0x060135BC RID: 79292 RVA: 0x0030645F File Offset: 0x0030465F
		// (set) Token: 0x060135BD RID: 79293 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "charset")]
		public SByteValue CharacterSet
		{
			get
			{
				return (SByteValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060135BE RID: 79294 RVA: 0x00306470 File Offset: 0x00304670
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "typeface" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "panose" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "pitchFamily" == name)
			{
				return new SByteValue();
			}
			if (namespaceId == 0 && "charset" == name)
			{
				return new SByteValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060135C0 RID: 79296 RVA: 0x003064E0 File Offset: 0x003046E0
		// Note: this type is marked as 'beforefieldinit'.
		static TextFontType()
		{
			byte[] array = new byte[4];
			TextFontType.attributeNamespaceIds = array;
		}

		// Token: 0x040085D0 RID: 34256
		private static string[] attributeTagNames = new string[] { "typeface", "panose", "pitchFamily", "charset" };

		// Token: 0x040085D1 RID: 34257
		private static byte[] attributeNamespaceIds;
	}
}
