using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002596 RID: 9622
	[ChildElementInfo(typeof(ApplyToFront))]
	[ChildElementInfo(typeof(PictureFormat))]
	[ChildElementInfo(typeof(PictureStackUnit))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ApplyToSides))]
	[ChildElementInfo(typeof(ApplyToEnd))]
	internal class PictureOptions : OpenXmlCompositeElement
	{
		// Token: 0x170056A1 RID: 22177
		// (get) Token: 0x06011FC5 RID: 73669 RVA: 0x002F4790 File Offset: 0x002F2990
		public override string LocalName
		{
			get
			{
				return "pictureOptions";
			}
		}

		// Token: 0x170056A2 RID: 22178
		// (get) Token: 0x06011FC6 RID: 73670 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056A3 RID: 22179
		// (get) Token: 0x06011FC7 RID: 73671 RVA: 0x002F4797 File Offset: 0x002F2997
		internal override int ElementTypeId
		{
			get
			{
				return 10435;
			}
		}

		// Token: 0x06011FC8 RID: 73672 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011FC9 RID: 73673 RVA: 0x00293ECF File Offset: 0x002920CF
		public PictureOptions()
		{
		}

		// Token: 0x06011FCA RID: 73674 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PictureOptions(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011FCB RID: 73675 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PictureOptions(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011FCC RID: 73676 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PictureOptions(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011FCD RID: 73677 RVA: 0x002F47A0 File Offset: 0x002F29A0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "applyToFront" == name)
			{
				return new ApplyToFront();
			}
			if (11 == namespaceId && "applyToSides" == name)
			{
				return new ApplyToSides();
			}
			if (11 == namespaceId && "applyToEnd" == name)
			{
				return new ApplyToEnd();
			}
			if (11 == namespaceId && "pictureFormat" == name)
			{
				return new PictureFormat();
			}
			if (11 == namespaceId && "pictureStackUnit" == name)
			{
				return new PictureStackUnit();
			}
			return null;
		}

		// Token: 0x170056A4 RID: 22180
		// (get) Token: 0x06011FCE RID: 73678 RVA: 0x002F4826 File Offset: 0x002F2A26
		internal override string[] ElementTagNames
		{
			get
			{
				return PictureOptions.eleTagNames;
			}
		}

		// Token: 0x170056A5 RID: 22181
		// (get) Token: 0x06011FCF RID: 73679 RVA: 0x002F482D File Offset: 0x002F2A2D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PictureOptions.eleNamespaceIds;
			}
		}

		// Token: 0x170056A6 RID: 22182
		// (get) Token: 0x06011FD0 RID: 73680 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170056A7 RID: 22183
		// (get) Token: 0x06011FD1 RID: 73681 RVA: 0x002F4834 File Offset: 0x002F2A34
		// (set) Token: 0x06011FD2 RID: 73682 RVA: 0x002F483D File Offset: 0x002F2A3D
		public ApplyToFront ApplyToFront
		{
			get
			{
				return base.GetElement<ApplyToFront>(0);
			}
			set
			{
				base.SetElement<ApplyToFront>(0, value);
			}
		}

		// Token: 0x170056A8 RID: 22184
		// (get) Token: 0x06011FD3 RID: 73683 RVA: 0x002F4847 File Offset: 0x002F2A47
		// (set) Token: 0x06011FD4 RID: 73684 RVA: 0x002F4850 File Offset: 0x002F2A50
		public ApplyToSides ApplyToSides
		{
			get
			{
				return base.GetElement<ApplyToSides>(1);
			}
			set
			{
				base.SetElement<ApplyToSides>(1, value);
			}
		}

		// Token: 0x170056A9 RID: 22185
		// (get) Token: 0x06011FD5 RID: 73685 RVA: 0x002F485A File Offset: 0x002F2A5A
		// (set) Token: 0x06011FD6 RID: 73686 RVA: 0x002F4863 File Offset: 0x002F2A63
		public ApplyToEnd ApplyToEnd
		{
			get
			{
				return base.GetElement<ApplyToEnd>(2);
			}
			set
			{
				base.SetElement<ApplyToEnd>(2, value);
			}
		}

		// Token: 0x170056AA RID: 22186
		// (get) Token: 0x06011FD7 RID: 73687 RVA: 0x002F486D File Offset: 0x002F2A6D
		// (set) Token: 0x06011FD8 RID: 73688 RVA: 0x002F4876 File Offset: 0x002F2A76
		public PictureFormat PictureFormat
		{
			get
			{
				return base.GetElement<PictureFormat>(3);
			}
			set
			{
				base.SetElement<PictureFormat>(3, value);
			}
		}

		// Token: 0x170056AB RID: 22187
		// (get) Token: 0x06011FD9 RID: 73689 RVA: 0x002F4880 File Offset: 0x002F2A80
		// (set) Token: 0x06011FDA RID: 73690 RVA: 0x002F4889 File Offset: 0x002F2A89
		public PictureStackUnit PictureStackUnit
		{
			get
			{
				return base.GetElement<PictureStackUnit>(4);
			}
			set
			{
				base.SetElement<PictureStackUnit>(4, value);
			}
		}

		// Token: 0x06011FDB RID: 73691 RVA: 0x002F4893 File Offset: 0x002F2A93
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PictureOptions>(deep);
		}

		// Token: 0x04007D98 RID: 32152
		private const string tagName = "pictureOptions";

		// Token: 0x04007D99 RID: 32153
		private const byte tagNsId = 11;

		// Token: 0x04007D9A RID: 32154
		internal const int ElementTypeIdConst = 10435;

		// Token: 0x04007D9B RID: 32155
		private static readonly string[] eleTagNames = new string[] { "applyToFront", "applyToSides", "applyToEnd", "pictureFormat", "pictureStackUnit" };

		// Token: 0x04007D9C RID: 32156
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11 };
	}
}
