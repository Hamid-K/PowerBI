using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C13 RID: 11283
	[GeneratedCode("DomGen", "2.0")]
	internal class RgbColor : OpenXmlLeafElement
	{
		// Token: 0x17007FFD RID: 32765
		// (get) Token: 0x06017C98 RID: 97432 RVA: 0x0033B323 File Offset: 0x00339523
		public override string LocalName
		{
			get
			{
				return "rgbColor";
			}
		}

		// Token: 0x17007FFE RID: 32766
		// (get) Token: 0x06017C99 RID: 97433 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007FFF RID: 32767
		// (get) Token: 0x06017C9A RID: 97434 RVA: 0x0033B32A File Offset: 0x0033952A
		internal override int ElementTypeId
		{
			get
			{
				return 11264;
			}
		}

		// Token: 0x06017C9B RID: 97435 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008000 RID: 32768
		// (get) Token: 0x06017C9C RID: 97436 RVA: 0x0033B331 File Offset: 0x00339531
		internal override string[] AttributeTagNames
		{
			get
			{
				return RgbColor.attributeTagNames;
			}
		}

		// Token: 0x17008001 RID: 32769
		// (get) Token: 0x06017C9D RID: 97437 RVA: 0x0033B338 File Offset: 0x00339538
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RgbColor.attributeNamespaceIds;
			}
		}

		// Token: 0x17008002 RID: 32770
		// (get) Token: 0x06017C9E RID: 97438 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x06017C9F RID: 97439 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rgb")]
		public HexBinaryValue Rgb
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06017CA1 RID: 97441 RVA: 0x0033B33F File Offset: 0x0033953F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rgb" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017CA2 RID: 97442 RVA: 0x0033B35F File Offset: 0x0033955F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RgbColor>(deep);
		}

		// Token: 0x06017CA3 RID: 97443 RVA: 0x0033B368 File Offset: 0x00339568
		// Note: this type is marked as 'beforefieldinit'.
		static RgbColor()
		{
			byte[] array = new byte[1];
			RgbColor.attributeNamespaceIds = array;
		}

		// Token: 0x04009D8E RID: 40334
		private const string tagName = "rgbColor";

		// Token: 0x04009D8F RID: 40335
		private const byte tagNsId = 22;

		// Token: 0x04009D90 RID: 40336
		internal const int ElementTypeIdConst = 11264;

		// Token: 0x04009D91 RID: 40337
		private static string[] attributeTagNames = new string[] { "rgb" };

		// Token: 0x04009D92 RID: 40338
		private static byte[] attributeNamespaceIds;
	}
}
