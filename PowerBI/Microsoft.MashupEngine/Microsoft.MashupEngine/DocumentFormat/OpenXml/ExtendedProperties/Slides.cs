using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x0200293F RID: 10559
	[GeneratedCode("DomGen", "2.0")]
	internal class Slides : OpenXmlLeafTextElement
	{
		// Token: 0x17006B38 RID: 27448
		// (get) Token: 0x06014EAA RID: 85674 RVA: 0x00318C10 File Offset: 0x00316E10
		public override string LocalName
		{
			get
			{
				return "Slides";
			}
		}

		// Token: 0x17006B39 RID: 27449
		// (get) Token: 0x06014EAB RID: 85675 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B3A RID: 27450
		// (get) Token: 0x06014EAC RID: 85676 RVA: 0x00318C17 File Offset: 0x00316E17
		internal override int ElementTypeId
		{
			get
			{
				return 11008;
			}
		}

		// Token: 0x06014EAD RID: 85677 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014EAE RID: 85678 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Slides()
		{
		}

		// Token: 0x06014EAF RID: 85679 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Slides(string text)
			: base(text)
		{
		}

		// Token: 0x06014EB0 RID: 85680 RVA: 0x00318C20 File Offset: 0x00316E20
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014EB1 RID: 85681 RVA: 0x00318C3B File Offset: 0x00316E3B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Slides>(deep);
		}

		// Token: 0x04009097 RID: 37015
		private const string tagName = "Slides";

		// Token: 0x04009098 RID: 37016
		private const byte tagNsId = 3;

		// Token: 0x04009099 RID: 37017
		internal const int ElementTypeIdConst = 11008;
	}
}
