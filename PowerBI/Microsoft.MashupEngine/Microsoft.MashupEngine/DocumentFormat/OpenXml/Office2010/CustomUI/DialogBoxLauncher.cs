using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022EA RID: 8938
	[ChildElementInfo(typeof(ButtonRegular), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DialogBoxLauncher : OpenXmlCompositeElement
	{
		// Token: 0x1700468A RID: 18058
		// (get) Token: 0x0600FBF9 RID: 64505 RVA: 0x002CF66F File Offset: 0x002CD86F
		public override string LocalName
		{
			get
			{
				return "dialogBoxLauncher";
			}
		}

		// Token: 0x1700468B RID: 18059
		// (get) Token: 0x0600FBFA RID: 64506 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700468C RID: 18060
		// (get) Token: 0x0600FBFB RID: 64507 RVA: 0x002DB119 File Offset: 0x002D9319
		internal override int ElementTypeId
		{
			get
			{
				return 13083;
			}
		}

		// Token: 0x0600FBFC RID: 64508 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FBFD RID: 64509 RVA: 0x00293ECF File Offset: 0x002920CF
		public DialogBoxLauncher()
		{
		}

		// Token: 0x0600FBFE RID: 64510 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DialogBoxLauncher(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FBFF RID: 64511 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DialogBoxLauncher(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FC00 RID: 64512 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DialogBoxLauncher(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FC01 RID: 64513 RVA: 0x002DB120 File Offset: 0x002D9320
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "button" == name)
			{
				return new ButtonRegular();
			}
			return null;
		}

		// Token: 0x1700468D RID: 18061
		// (get) Token: 0x0600FC02 RID: 64514 RVA: 0x002DB13B File Offset: 0x002D933B
		internal override string[] ElementTagNames
		{
			get
			{
				return DialogBoxLauncher.eleTagNames;
			}
		}

		// Token: 0x1700468E RID: 18062
		// (get) Token: 0x0600FC03 RID: 64515 RVA: 0x002DB142 File Offset: 0x002D9342
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DialogBoxLauncher.eleNamespaceIds;
			}
		}

		// Token: 0x1700468F RID: 18063
		// (get) Token: 0x0600FC04 RID: 64516 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004690 RID: 18064
		// (get) Token: 0x0600FC05 RID: 64517 RVA: 0x002DB149 File Offset: 0x002D9349
		// (set) Token: 0x0600FC06 RID: 64518 RVA: 0x002DB152 File Offset: 0x002D9352
		public ButtonRegular ButtonRegular
		{
			get
			{
				return base.GetElement<ButtonRegular>(0);
			}
			set
			{
				base.SetElement<ButtonRegular>(0, value);
			}
		}

		// Token: 0x0600FC07 RID: 64519 RVA: 0x002DB15C File Offset: 0x002D935C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DialogBoxLauncher>(deep);
		}

		// Token: 0x040071C1 RID: 29121
		private const string tagName = "dialogBoxLauncher";

		// Token: 0x040071C2 RID: 29122
		private const byte tagNsId = 57;

		// Token: 0x040071C3 RID: 29123
		internal const int ElementTypeIdConst = 13083;

		// Token: 0x040071C4 RID: 29124
		private static readonly string[] eleTagNames = new string[] { "button" };

		// Token: 0x040071C5 RID: 29125
		private static readonly byte[] eleNamespaceIds = new byte[] { 57 };
	}
}
