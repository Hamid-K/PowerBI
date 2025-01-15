using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026CC RID: 9932
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(StartTime))]
	[ChildElementInfo(typeof(EndTime))]
	[GeneratedCode("DomGen", "2.0")]
	internal class AudioFromCD : OpenXmlCompositeElement
	{
		// Token: 0x17005D66 RID: 23910
		// (get) Token: 0x06012EEF RID: 77551 RVA: 0x0030122B File Offset: 0x002FF42B
		public override string LocalName
		{
			get
			{
				return "audioCd";
			}
		}

		// Token: 0x17005D67 RID: 23911
		// (get) Token: 0x06012EF0 RID: 77552 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005D68 RID: 23912
		// (get) Token: 0x06012EF1 RID: 77553 RVA: 0x00301232 File Offset: 0x002FF432
		internal override int ElementTypeId
		{
			get
			{
				return 10001;
			}
		}

		// Token: 0x06012EF2 RID: 77554 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012EF3 RID: 77555 RVA: 0x00293ECF File Offset: 0x002920CF
		public AudioFromCD()
		{
		}

		// Token: 0x06012EF4 RID: 77556 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AudioFromCD(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012EF5 RID: 77557 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AudioFromCD(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012EF6 RID: 77558 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AudioFromCD(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012EF7 RID: 77559 RVA: 0x0030123C File Offset: 0x002FF43C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "st" == name)
			{
				return new StartTime();
			}
			if (10 == namespaceId && "end" == name)
			{
				return new EndTime();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005D69 RID: 23913
		// (get) Token: 0x06012EF8 RID: 77560 RVA: 0x00301292 File Offset: 0x002FF492
		internal override string[] ElementTagNames
		{
			get
			{
				return AudioFromCD.eleTagNames;
			}
		}

		// Token: 0x17005D6A RID: 23914
		// (get) Token: 0x06012EF9 RID: 77561 RVA: 0x00301299 File Offset: 0x002FF499
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AudioFromCD.eleNamespaceIds;
			}
		}

		// Token: 0x17005D6B RID: 23915
		// (get) Token: 0x06012EFA RID: 77562 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005D6C RID: 23916
		// (get) Token: 0x06012EFB RID: 77563 RVA: 0x003012A0 File Offset: 0x002FF4A0
		// (set) Token: 0x06012EFC RID: 77564 RVA: 0x003012A9 File Offset: 0x002FF4A9
		public StartTime StartTime
		{
			get
			{
				return base.GetElement<StartTime>(0);
			}
			set
			{
				base.SetElement<StartTime>(0, value);
			}
		}

		// Token: 0x17005D6D RID: 23917
		// (get) Token: 0x06012EFD RID: 77565 RVA: 0x003012B3 File Offset: 0x002FF4B3
		// (set) Token: 0x06012EFE RID: 77566 RVA: 0x003012BC File Offset: 0x002FF4BC
		public EndTime EndTime
		{
			get
			{
				return base.GetElement<EndTime>(1);
			}
			set
			{
				base.SetElement<EndTime>(1, value);
			}
		}

		// Token: 0x17005D6E RID: 23918
		// (get) Token: 0x06012EFF RID: 77567 RVA: 0x003012C6 File Offset: 0x002FF4C6
		// (set) Token: 0x06012F00 RID: 77568 RVA: 0x003012CF File Offset: 0x002FF4CF
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x06012F01 RID: 77569 RVA: 0x003012D9 File Offset: 0x002FF4D9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AudioFromCD>(deep);
		}

		// Token: 0x040083BE RID: 33726
		private const string tagName = "audioCd";

		// Token: 0x040083BF RID: 33727
		private const byte tagNsId = 10;

		// Token: 0x040083C0 RID: 33728
		internal const int ElementTypeIdConst = 10001;

		// Token: 0x040083C1 RID: 33729
		private static readonly string[] eleTagNames = new string[] { "st", "end", "extLst" };

		// Token: 0x040083C2 RID: 33730
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10 };
	}
}
