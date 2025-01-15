using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002948 RID: 10568
	[GeneratedCode("DomGen", "2.0")]
	internal class SharedDocument : OpenXmlLeafTextElement
	{
		// Token: 0x17006B53 RID: 27475
		// (get) Token: 0x06014EF2 RID: 85746 RVA: 0x00318DE4 File Offset: 0x00316FE4
		public override string LocalName
		{
			get
			{
				return "SharedDoc";
			}
		}

		// Token: 0x17006B54 RID: 27476
		// (get) Token: 0x06014EF3 RID: 85747 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B55 RID: 27477
		// (get) Token: 0x06014EF4 RID: 85748 RVA: 0x00318DEB File Offset: 0x00316FEB
		internal override int ElementTypeId
		{
			get
			{
				return 11018;
			}
		}

		// Token: 0x06014EF5 RID: 85749 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014EF6 RID: 85750 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public SharedDocument()
		{
		}

		// Token: 0x06014EF7 RID: 85751 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public SharedDocument(string text)
			: base(text)
		{
		}

		// Token: 0x06014EF8 RID: 85752 RVA: 0x00318DF4 File Offset: 0x00316FF4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new BooleanValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014EF9 RID: 85753 RVA: 0x00318E0F File Offset: 0x0031700F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SharedDocument>(deep);
		}

		// Token: 0x040090B2 RID: 37042
		private const string tagName = "SharedDoc";

		// Token: 0x040090B3 RID: 37043
		private const byte tagNsId = 3;

		// Token: 0x040090B4 RID: 37044
		internal const int ElementTypeIdConst = 11018;
	}
}
