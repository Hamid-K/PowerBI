using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028CF RID: 10447
	[GeneratedCode("DomGen", "2.0")]
	internal class InternetSiteTitle : OpenXmlLeafTextElement
	{
		// Token: 0x17006915 RID: 26901
		// (get) Token: 0x0601496C RID: 84332 RVA: 0x00314CE0 File Offset: 0x00312EE0
		public override string LocalName
		{
			get
			{
				return "InternetSiteTitle";
			}
		}

		// Token: 0x17006916 RID: 26902
		// (get) Token: 0x0601496D RID: 84333 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006917 RID: 26903
		// (get) Token: 0x0601496E RID: 84334 RVA: 0x00314CE7 File Offset: 0x00312EE7
		internal override int ElementTypeId
		{
			get
			{
				return 10801;
			}
		}

		// Token: 0x0601496F RID: 84335 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014970 RID: 84336 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public InternetSiteTitle()
		{
		}

		// Token: 0x06014971 RID: 84337 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public InternetSiteTitle(string text)
			: base(text)
		{
		}

		// Token: 0x06014972 RID: 84338 RVA: 0x00314CF0 File Offset: 0x00312EF0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014973 RID: 84339 RVA: 0x00314D0B File Offset: 0x00312F0B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InternetSiteTitle>(deep);
		}

		// Token: 0x04008EF9 RID: 36601
		private const string tagName = "InternetSiteTitle";

		// Token: 0x04008EFA RID: 36602
		private const byte tagNsId = 9;

		// Token: 0x04008EFB RID: 36603
		internal const int ElementTypeIdConst = 10801;
	}
}
