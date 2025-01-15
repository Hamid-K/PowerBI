using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x0200230A RID: 8970
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Command), FileFormatVersions.Office2010)]
	internal class Commands : OpenXmlCompositeElement
	{
		// Token: 0x1700480D RID: 18445
		// (get) Token: 0x0600FF55 RID: 65365 RVA: 0x002D0606 File Offset: 0x002CE806
		public override string LocalName
		{
			get
			{
				return "commands";
			}
		}

		// Token: 0x1700480E RID: 18446
		// (get) Token: 0x0600FF56 RID: 65366 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700480F RID: 18447
		// (get) Token: 0x0600FF57 RID: 65367 RVA: 0x002DDE71 File Offset: 0x002DC071
		internal override int ElementTypeId
		{
			get
			{
				return 13112;
			}
		}

		// Token: 0x0600FF58 RID: 65368 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FF59 RID: 65369 RVA: 0x00293ECF File Offset: 0x002920CF
		public Commands()
		{
		}

		// Token: 0x0600FF5A RID: 65370 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Commands(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FF5B RID: 65371 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Commands(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FF5C RID: 65372 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Commands(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FF5D RID: 65373 RVA: 0x002DDE78 File Offset: 0x002DC078
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "command" == name)
			{
				return new Command();
			}
			return null;
		}

		// Token: 0x0600FF5E RID: 65374 RVA: 0x002DDE93 File Offset: 0x002DC093
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Commands>(deep);
		}

		// Token: 0x04007242 RID: 29250
		private const string tagName = "commands";

		// Token: 0x04007243 RID: 29251
		private const byte tagNsId = 57;

		// Token: 0x04007244 RID: 29252
		internal const int ElementTypeIdConst = 13112;
	}
}
