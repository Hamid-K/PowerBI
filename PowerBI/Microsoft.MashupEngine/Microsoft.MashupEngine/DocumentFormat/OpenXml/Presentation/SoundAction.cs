using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ADE RID: 10974
	[ChildElementInfo(typeof(EndSoundAction))]
	[ChildElementInfo(typeof(StartSoundAction))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SoundAction : OpenXmlCompositeElement
	{
		// Token: 0x1700759D RID: 30109
		// (get) Token: 0x060165D8 RID: 91608 RVA: 0x00329523 File Offset: 0x00327723
		public override string LocalName
		{
			get
			{
				return "sndAc";
			}
		}

		// Token: 0x1700759E RID: 30110
		// (get) Token: 0x060165D9 RID: 91609 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700759F RID: 30111
		// (get) Token: 0x060165DA RID: 91610 RVA: 0x0032952A File Offset: 0x0032772A
		internal override int ElementTypeId
		{
			get
			{
				return 12396;
			}
		}

		// Token: 0x060165DB RID: 91611 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060165DC RID: 91612 RVA: 0x00293ECF File Offset: 0x002920CF
		public SoundAction()
		{
		}

		// Token: 0x060165DD RID: 91613 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SoundAction(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060165DE RID: 91614 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SoundAction(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060165DF RID: 91615 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SoundAction(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060165E0 RID: 91616 RVA: 0x00329531 File Offset: 0x00327731
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "stSnd" == name)
			{
				return new StartSoundAction();
			}
			if (24 == namespaceId && "endSnd" == name)
			{
				return new EndSoundAction();
			}
			return null;
		}

		// Token: 0x170075A0 RID: 30112
		// (get) Token: 0x060165E1 RID: 91617 RVA: 0x00329564 File Offset: 0x00327764
		internal override string[] ElementTagNames
		{
			get
			{
				return SoundAction.eleTagNames;
			}
		}

		// Token: 0x170075A1 RID: 30113
		// (get) Token: 0x060165E2 RID: 91618 RVA: 0x0032956B File Offset: 0x0032776B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SoundAction.eleNamespaceIds;
			}
		}

		// Token: 0x170075A2 RID: 30114
		// (get) Token: 0x060165E3 RID: 91619 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170075A3 RID: 30115
		// (get) Token: 0x060165E4 RID: 91620 RVA: 0x00329572 File Offset: 0x00327772
		// (set) Token: 0x060165E5 RID: 91621 RVA: 0x0032957B File Offset: 0x0032777B
		public StartSoundAction StartSoundAction
		{
			get
			{
				return base.GetElement<StartSoundAction>(0);
			}
			set
			{
				base.SetElement<StartSoundAction>(0, value);
			}
		}

		// Token: 0x170075A4 RID: 30116
		// (get) Token: 0x060165E6 RID: 91622 RVA: 0x00329585 File Offset: 0x00327785
		// (set) Token: 0x060165E7 RID: 91623 RVA: 0x0032958E File Offset: 0x0032778E
		public EndSoundAction EndSoundAction
		{
			get
			{
				return base.GetElement<EndSoundAction>(1);
			}
			set
			{
				base.SetElement<EndSoundAction>(1, value);
			}
		}

		// Token: 0x060165E8 RID: 91624 RVA: 0x00329598 File Offset: 0x00327798
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SoundAction>(deep);
		}

		// Token: 0x04009779 RID: 38777
		private const string tagName = "sndAc";

		// Token: 0x0400977A RID: 38778
		private const byte tagNsId = 24;

		// Token: 0x0400977B RID: 38779
		internal const int ElementTypeIdConst = 12396;

		// Token: 0x0400977C RID: 38780
		private static readonly string[] eleTagNames = new string[] { "stSnd", "endSnd" };

		// Token: 0x0400977D RID: 38781
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24 };
	}
}
