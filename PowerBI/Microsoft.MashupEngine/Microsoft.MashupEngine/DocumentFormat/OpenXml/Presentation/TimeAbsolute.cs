using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A0D RID: 10765
	[GeneratedCode("DomGen", "2.0")]
	internal class TimeAbsolute : OpenXmlLeafElement
	{
		// Token: 0x17006FA2 RID: 28578
		// (get) Token: 0x06015887 RID: 88199 RVA: 0x00320496 File Offset: 0x0031E696
		public override string LocalName
		{
			get
			{
				return "tmAbs";
			}
		}

		// Token: 0x17006FA3 RID: 28579
		// (get) Token: 0x06015888 RID: 88200 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006FA4 RID: 28580
		// (get) Token: 0x06015889 RID: 88201 RVA: 0x0032049D File Offset: 0x0031E69D
		internal override int ElementTypeId
		{
			get
			{
				return 12191;
			}
		}

		// Token: 0x0601588A RID: 88202 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006FA5 RID: 28581
		// (get) Token: 0x0601588B RID: 88203 RVA: 0x003204A4 File Offset: 0x0031E6A4
		internal override string[] AttributeTagNames
		{
			get
			{
				return TimeAbsolute.attributeTagNames;
			}
		}

		// Token: 0x17006FA6 RID: 28582
		// (get) Token: 0x0601588C RID: 88204 RVA: 0x003204AB File Offset: 0x0031E6AB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TimeAbsolute.attributeNamespaceIds;
			}
		}

		// Token: 0x17006FA7 RID: 28583
		// (get) Token: 0x0601588D RID: 88205 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601588E RID: 88206 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
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

		// Token: 0x06015890 RID: 88208 RVA: 0x002E6B2F File Offset: 0x002E4D2F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015891 RID: 88209 RVA: 0x003204B2 File Offset: 0x0031E6B2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TimeAbsolute>(deep);
		}

		// Token: 0x06015892 RID: 88210 RVA: 0x003204BC File Offset: 0x0031E6BC
		// Note: this type is marked as 'beforefieldinit'.
		static TimeAbsolute()
		{
			byte[] array = new byte[1];
			TimeAbsolute.attributeNamespaceIds = array;
		}

		// Token: 0x040093BF RID: 37823
		private const string tagName = "tmAbs";

		// Token: 0x040093C0 RID: 37824
		private const byte tagNsId = 24;

		// Token: 0x040093C1 RID: 37825
		internal const int ElementTypeIdConst = 12191;

		// Token: 0x040093C2 RID: 37826
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040093C3 RID: 37827
		private static byte[] attributeNamespaceIds;
	}
}
