using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AD9 RID: 10969
	[GeneratedCode("DomGen", "2.0")]
	internal class WipeTransition : SideDirectionTransitionType
	{
		// Token: 0x17007581 RID: 30081
		// (get) Token: 0x060165A0 RID: 91552 RVA: 0x0032934E File Offset: 0x0032754E
		public override string LocalName
		{
			get
			{
				return "wipe";
			}
		}

		// Token: 0x17007582 RID: 30082
		// (get) Token: 0x060165A1 RID: 91553 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007583 RID: 30083
		// (get) Token: 0x060165A2 RID: 91554 RVA: 0x00329355 File Offset: 0x00327555
		internal override int ElementTypeId
		{
			get
			{
				return 12394;
			}
		}

		// Token: 0x060165A3 RID: 91555 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060165A5 RID: 91557 RVA: 0x0032935C File Offset: 0x0032755C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WipeTransition>(deep);
		}

		// Token: 0x04009762 RID: 38754
		private const string tagName = "wipe";

		// Token: 0x04009763 RID: 38755
		private const byte tagNsId = 24;

		// Token: 0x04009764 RID: 38756
		internal const int ElementTypeIdConst = 12394;
	}
}
