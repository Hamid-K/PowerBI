using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028A8 RID: 10408
	[ChildElementInfo(typeof(LineTo))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StartPoint))]
	internal class WrapPolygon : OpenXmlCompositeElement
	{
		// Token: 0x1700687B RID: 26747
		// (get) Token: 0x060147F3 RID: 83955 RVA: 0x0031405F File Offset: 0x0031225F
		public override string LocalName
		{
			get
			{
				return "wrapPolygon";
			}
		}

		// Token: 0x1700687C RID: 26748
		// (get) Token: 0x060147F4 RID: 83956 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x1700687D RID: 26749
		// (get) Token: 0x060147F5 RID: 83957 RVA: 0x00314066 File Offset: 0x00312266
		internal override int ElementTypeId
		{
			get
			{
				return 10704;
			}
		}

		// Token: 0x060147F6 RID: 83958 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700687E RID: 26750
		// (get) Token: 0x060147F7 RID: 83959 RVA: 0x0031406D File Offset: 0x0031226D
		internal override string[] AttributeTagNames
		{
			get
			{
				return WrapPolygon.attributeTagNames;
			}
		}

		// Token: 0x1700687F RID: 26751
		// (get) Token: 0x060147F8 RID: 83960 RVA: 0x00314074 File Offset: 0x00312274
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WrapPolygon.attributeNamespaceIds;
			}
		}

		// Token: 0x17006880 RID: 26752
		// (get) Token: 0x060147F9 RID: 83961 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060147FA RID: 83962 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "edited")]
		public BooleanValue Edited
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060147FB RID: 83963 RVA: 0x00293ECF File Offset: 0x002920CF
		public WrapPolygon()
		{
		}

		// Token: 0x060147FC RID: 83964 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WrapPolygon(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060147FD RID: 83965 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WrapPolygon(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060147FE RID: 83966 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WrapPolygon(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060147FF RID: 83967 RVA: 0x0031407B File Offset: 0x0031227B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (16 == namespaceId && "start" == name)
			{
				return new StartPoint();
			}
			if (16 == namespaceId && "lineTo" == name)
			{
				return new LineTo();
			}
			return null;
		}

		// Token: 0x17006881 RID: 26753
		// (get) Token: 0x06014800 RID: 83968 RVA: 0x003140AE File Offset: 0x003122AE
		internal override string[] ElementTagNames
		{
			get
			{
				return WrapPolygon.eleTagNames;
			}
		}

		// Token: 0x17006882 RID: 26754
		// (get) Token: 0x06014801 RID: 83969 RVA: 0x003140B5 File Offset: 0x003122B5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WrapPolygon.eleNamespaceIds;
			}
		}

		// Token: 0x17006883 RID: 26755
		// (get) Token: 0x06014802 RID: 83970 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006884 RID: 26756
		// (get) Token: 0x06014803 RID: 83971 RVA: 0x003140BC File Offset: 0x003122BC
		// (set) Token: 0x06014804 RID: 83972 RVA: 0x003140C5 File Offset: 0x003122C5
		public StartPoint StartPoint
		{
			get
			{
				return base.GetElement<StartPoint>(0);
			}
			set
			{
				base.SetElement<StartPoint>(0, value);
			}
		}

		// Token: 0x06014805 RID: 83973 RVA: 0x003140CF File Offset: 0x003122CF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "edited" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014806 RID: 83974 RVA: 0x003140EF File Offset: 0x003122EF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WrapPolygon>(deep);
		}

		// Token: 0x06014807 RID: 83975 RVA: 0x003140F8 File Offset: 0x003122F8
		// Note: this type is marked as 'beforefieldinit'.
		static WrapPolygon()
		{
			byte[] array = new byte[1];
			WrapPolygon.attributeNamespaceIds = array;
			WrapPolygon.eleTagNames = new string[] { "start", "lineTo" };
			WrapPolygon.eleNamespaceIds = new byte[] { 16, 16 };
		}

		// Token: 0x04008E5A RID: 36442
		private const string tagName = "wrapPolygon";

		// Token: 0x04008E5B RID: 36443
		private const byte tagNsId = 16;

		// Token: 0x04008E5C RID: 36444
		internal const int ElementTypeIdConst = 10704;

		// Token: 0x04008E5D RID: 36445
		private static string[] attributeTagNames = new string[] { "edited" };

		// Token: 0x04008E5E RID: 36446
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008E5F RID: 36447
		private static readonly string[] eleTagNames;

		// Token: 0x04008E60 RID: 36448
		private static readonly byte[] eleNamespaceIds;
	}
}
