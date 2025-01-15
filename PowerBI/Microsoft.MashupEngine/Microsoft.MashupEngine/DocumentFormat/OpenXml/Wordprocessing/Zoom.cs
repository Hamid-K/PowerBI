using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FDB RID: 12251
	[GeneratedCode("DomGen", "2.0")]
	internal class Zoom : OpenXmlLeafElement
	{
		// Token: 0x17009494 RID: 38036
		// (get) Token: 0x0601A9C3 RID: 108995 RVA: 0x003294CF File Offset: 0x003276CF
		public override string LocalName
		{
			get
			{
				return "zoom";
			}
		}

		// Token: 0x17009495 RID: 38037
		// (get) Token: 0x0601A9C4 RID: 108996 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009496 RID: 38038
		// (get) Token: 0x0601A9C5 RID: 108997 RVA: 0x00364DE0 File Offset: 0x00362FE0
		internal override int ElementTypeId
		{
			get
			{
				return 11960;
			}
		}

		// Token: 0x0601A9C6 RID: 108998 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009497 RID: 38039
		// (get) Token: 0x0601A9C7 RID: 108999 RVA: 0x00364DE7 File Offset: 0x00362FE7
		internal override string[] AttributeTagNames
		{
			get
			{
				return Zoom.attributeTagNames;
			}
		}

		// Token: 0x17009498 RID: 38040
		// (get) Token: 0x0601A9C8 RID: 109000 RVA: 0x00364DEE File Offset: 0x00362FEE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Zoom.attributeNamespaceIds;
			}
		}

		// Token: 0x17009499 RID: 38041
		// (get) Token: 0x0601A9C9 RID: 109001 RVA: 0x00364DF5 File Offset: 0x00362FF5
		// (set) Token: 0x0601A9CA RID: 109002 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<PresetZoomValues> Val
		{
			get
			{
				return (EnumValue<PresetZoomValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700949A RID: 38042
		// (get) Token: 0x0601A9CB RID: 109003 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601A9CC RID: 109004 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "percent")]
		public StringValue Percent
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

		// Token: 0x0601A9CE RID: 109006 RVA: 0x00364E04 File Offset: 0x00363004
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<PresetZoomValues>();
			}
			if (23 == namespaceId && "percent" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A9CF RID: 109007 RVA: 0x00364E3E File Offset: 0x0036303E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Zoom>(deep);
		}

		// Token: 0x0400ADB6 RID: 44470
		private const string tagName = "zoom";

		// Token: 0x0400ADB7 RID: 44471
		private const byte tagNsId = 23;

		// Token: 0x0400ADB8 RID: 44472
		internal const int ElementTypeIdConst = 11960;

		// Token: 0x0400ADB9 RID: 44473
		private static string[] attributeTagNames = new string[] { "val", "percent" };

		// Token: 0x0400ADBA RID: 44474
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
