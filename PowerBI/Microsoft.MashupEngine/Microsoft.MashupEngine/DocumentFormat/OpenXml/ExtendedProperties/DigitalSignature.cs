using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.VariantTypes;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x0200294E RID: 10574
	[ChildElementInfo(typeof(VTBlob))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DigitalSignature : OpenXmlCompositeElement
	{
		// Token: 0x17006B6A RID: 27498
		// (get) Token: 0x06014F2F RID: 85807 RVA: 0x00318F53 File Offset: 0x00317153
		public override string LocalName
		{
			get
			{
				return "DigSig";
			}
		}

		// Token: 0x17006B6B RID: 27499
		// (get) Token: 0x06014F30 RID: 85808 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B6C RID: 27500
		// (get) Token: 0x06014F31 RID: 85809 RVA: 0x00318F5A File Offset: 0x0031715A
		internal override int ElementTypeId
		{
			get
			{
				return 11022;
			}
		}

		// Token: 0x06014F32 RID: 85810 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014F33 RID: 85811 RVA: 0x00293ECF File Offset: 0x002920CF
		public DigitalSignature()
		{
		}

		// Token: 0x06014F34 RID: 85812 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DigitalSignature(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F35 RID: 85813 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DigitalSignature(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F36 RID: 85814 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DigitalSignature(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014F37 RID: 85815 RVA: 0x00318F61 File Offset: 0x00317161
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (5 == namespaceId && "blob" == name)
			{
				return new VTBlob();
			}
			return null;
		}

		// Token: 0x17006B6D RID: 27501
		// (get) Token: 0x06014F38 RID: 85816 RVA: 0x00318F7B File Offset: 0x0031717B
		internal override string[] ElementTagNames
		{
			get
			{
				return DigitalSignature.eleTagNames;
			}
		}

		// Token: 0x17006B6E RID: 27502
		// (get) Token: 0x06014F39 RID: 85817 RVA: 0x00318F82 File Offset: 0x00317182
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DigitalSignature.eleNamespaceIds;
			}
		}

		// Token: 0x17006B6F RID: 27503
		// (get) Token: 0x06014F3A RID: 85818 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006B70 RID: 27504
		// (get) Token: 0x06014F3B RID: 85819 RVA: 0x00318F89 File Offset: 0x00317189
		// (set) Token: 0x06014F3C RID: 85820 RVA: 0x00318F92 File Offset: 0x00317192
		public VTBlob VTBlob
		{
			get
			{
				return base.GetElement<VTBlob>(0);
			}
			set
			{
				base.SetElement<VTBlob>(0, value);
			}
		}

		// Token: 0x06014F3D RID: 85821 RVA: 0x00318F9C File Offset: 0x0031719C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DigitalSignature>(deep);
		}

		// Token: 0x040090C5 RID: 37061
		private const string tagName = "DigSig";

		// Token: 0x040090C6 RID: 37062
		private const byte tagNsId = 3;

		// Token: 0x040090C7 RID: 37063
		internal const int ElementTypeIdConst = 11022;

		// Token: 0x040090C8 RID: 37064
		private static readonly string[] eleTagNames = new string[] { "blob" };

		// Token: 0x040090C9 RID: 37065
		private static readonly byte[] eleNamespaceIds = new byte[] { 5 };
	}
}
