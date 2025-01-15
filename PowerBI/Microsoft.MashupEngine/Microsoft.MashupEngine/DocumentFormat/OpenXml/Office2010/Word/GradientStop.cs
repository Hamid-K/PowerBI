using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024C3 RID: 9411
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SchemeColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RgbColorModelHex), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class GradientStop : OpenXmlCompositeElement
	{
		// Token: 0x170052BD RID: 21181
		// (get) Token: 0x0601171E RID: 71454 RVA: 0x002EE827 File Offset: 0x002ECA27
		public override string LocalName
		{
			get
			{
				return "gs";
			}
		}

		// Token: 0x170052BE RID: 21182
		// (get) Token: 0x0601171F RID: 71455 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052BF RID: 21183
		// (get) Token: 0x06011720 RID: 71456 RVA: 0x002EE82E File Offset: 0x002ECA2E
		internal override int ElementTypeId
		{
			get
			{
				return 12883;
			}
		}

		// Token: 0x06011721 RID: 71457 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170052C0 RID: 21184
		// (get) Token: 0x06011722 RID: 71458 RVA: 0x002EE835 File Offset: 0x002ECA35
		internal override string[] AttributeTagNames
		{
			get
			{
				return GradientStop.attributeTagNames;
			}
		}

		// Token: 0x170052C1 RID: 21185
		// (get) Token: 0x06011723 RID: 71459 RVA: 0x002EE83C File Offset: 0x002ECA3C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GradientStop.attributeNamespaceIds;
			}
		}

		// Token: 0x170052C2 RID: 21186
		// (get) Token: 0x06011724 RID: 71460 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06011725 RID: 71461 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "pos")]
		public Int32Value StopPosition
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011726 RID: 71462 RVA: 0x00293ECF File Offset: 0x002920CF
		public GradientStop()
		{
		}

		// Token: 0x06011727 RID: 71463 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GradientStop(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011728 RID: 71464 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GradientStop(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011729 RID: 71465 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GradientStop(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601172A RID: 71466 RVA: 0x002ED009 File Offset: 0x002EB209
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "srgbClr" == name)
			{
				return new RgbColorModelHex();
			}
			if (52 == namespaceId && "schemeClr" == name)
			{
				return new SchemeColor();
			}
			return null;
		}

		// Token: 0x170052C3 RID: 21187
		// (get) Token: 0x0601172B RID: 71467 RVA: 0x002EE843 File Offset: 0x002ECA43
		internal override string[] ElementTagNames
		{
			get
			{
				return GradientStop.eleTagNames;
			}
		}

		// Token: 0x170052C4 RID: 21188
		// (get) Token: 0x0601172C RID: 71468 RVA: 0x002EE84A File Offset: 0x002ECA4A
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GradientStop.eleNamespaceIds;
			}
		}

		// Token: 0x170052C5 RID: 21189
		// (get) Token: 0x0601172D RID: 71469 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170052C6 RID: 21190
		// (get) Token: 0x0601172E RID: 71470 RVA: 0x002ED04A File Offset: 0x002EB24A
		// (set) Token: 0x0601172F RID: 71471 RVA: 0x002ED053 File Offset: 0x002EB253
		public RgbColorModelHex RgbColorModelHex
		{
			get
			{
				return base.GetElement<RgbColorModelHex>(0);
			}
			set
			{
				base.SetElement<RgbColorModelHex>(0, value);
			}
		}

		// Token: 0x170052C7 RID: 21191
		// (get) Token: 0x06011730 RID: 71472 RVA: 0x002ED05D File Offset: 0x002EB25D
		// (set) Token: 0x06011731 RID: 71473 RVA: 0x002ED066 File Offset: 0x002EB266
		public SchemeColor SchemeColor
		{
			get
			{
				return base.GetElement<SchemeColor>(1);
			}
			set
			{
				base.SetElement<SchemeColor>(1, value);
			}
		}

		// Token: 0x06011732 RID: 71474 RVA: 0x002EE851 File Offset: 0x002ECA51
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "pos" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011733 RID: 71475 RVA: 0x002EE873 File Offset: 0x002ECA73
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GradientStop>(deep);
		}

		// Token: 0x040079DB RID: 31195
		private const string tagName = "gs";

		// Token: 0x040079DC RID: 31196
		private const byte tagNsId = 52;

		// Token: 0x040079DD RID: 31197
		internal const int ElementTypeIdConst = 12883;

		// Token: 0x040079DE RID: 31198
		private static string[] attributeTagNames = new string[] { "pos" };

		// Token: 0x040079DF RID: 31199
		private static byte[] attributeNamespaceIds = new byte[] { 52 };

		// Token: 0x040079E0 RID: 31200
		private static readonly string[] eleTagNames = new string[] { "srgbClr", "schemeClr" };

		// Token: 0x040079E1 RID: 31201
		private static readonly byte[] eleNamespaceIds = new byte[] { 52, 52 };
	}
}
