using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002949 RID: 10569
	[GeneratedCode("DomGen", "2.0")]
	internal class HyperlinksChanged : OpenXmlLeafTextElement
	{
		// Token: 0x17006B56 RID: 27478
		// (get) Token: 0x06014EFA RID: 85754 RVA: 0x00318E18 File Offset: 0x00317018
		public override string LocalName
		{
			get
			{
				return "HyperlinksChanged";
			}
		}

		// Token: 0x17006B57 RID: 27479
		// (get) Token: 0x06014EFB RID: 85755 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B58 RID: 27480
		// (get) Token: 0x06014EFC RID: 85756 RVA: 0x00318E1F File Offset: 0x0031701F
		internal override int ElementTypeId
		{
			get
			{
				return 11021;
			}
		}

		// Token: 0x06014EFD RID: 85757 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014EFE RID: 85758 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public HyperlinksChanged()
		{
		}

		// Token: 0x06014EFF RID: 85759 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public HyperlinksChanged(string text)
			: base(text)
		{
		}

		// Token: 0x06014F00 RID: 85760 RVA: 0x00318E28 File Offset: 0x00317028
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new BooleanValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014F01 RID: 85761 RVA: 0x00318E43 File Offset: 0x00317043
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HyperlinksChanged>(deep);
		}

		// Token: 0x040090B5 RID: 37045
		private const string tagName = "HyperlinksChanged";

		// Token: 0x040090B6 RID: 37046
		private const byte tagNsId = 3;

		// Token: 0x040090B7 RID: 37047
		internal const int ElementTypeIdConst = 11021;
	}
}
