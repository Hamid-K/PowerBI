using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F43 RID: 12099
	[GeneratedCode("DomGen", "2.0")]
	internal class Format : OpenXmlLeafElement
	{
		// Token: 0x17008FC3 RID: 36803
		// (get) Token: 0x06019F62 RID: 106338 RVA: 0x003314EB File Offset: 0x0032F6EB
		public override string LocalName
		{
			get
			{
				return "format";
			}
		}

		// Token: 0x17008FC4 RID: 36804
		// (get) Token: 0x06019F63 RID: 106339 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FC5 RID: 36805
		// (get) Token: 0x06019F64 RID: 106340 RVA: 0x0035A4A4 File Offset: 0x003586A4
		internal override int ElementTypeId
		{
			get
			{
				return 11746;
			}
		}

		// Token: 0x06019F65 RID: 106341 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008FC6 RID: 36806
		// (get) Token: 0x06019F66 RID: 106342 RVA: 0x0035A4AB File Offset: 0x003586AB
		internal override string[] AttributeTagNames
		{
			get
			{
				return Format.attributeTagNames;
			}
		}

		// Token: 0x17008FC7 RID: 36807
		// (get) Token: 0x06019F67 RID: 106343 RVA: 0x0035A4B2 File Offset: 0x003586B2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Format.attributeNamespaceIds;
			}
		}

		// Token: 0x17008FC8 RID: 36808
		// (get) Token: 0x06019F68 RID: 106344 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019F69 RID: 106345 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x06019F6B RID: 106347 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019F6C RID: 106348 RVA: 0x0035A4B9 File Offset: 0x003586B9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Format>(deep);
		}

		// Token: 0x0400AB21 RID: 43809
		private const string tagName = "format";

		// Token: 0x0400AB22 RID: 43810
		private const byte tagNsId = 23;

		// Token: 0x0400AB23 RID: 43811
		internal const int ElementTypeIdConst = 11746;

		// Token: 0x0400AB24 RID: 43812
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AB25 RID: 43813
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
