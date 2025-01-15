using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029C5 RID: 10693
	[ChildElementInfo(typeof(ZeroWidth))]
	[ChildElementInfo(typeof(ControlProperties))]
	[ChildElementInfo(typeof(Transparent))]
	[ChildElementInfo(typeof(ZeroDescent))]
	[ChildElementInfo(typeof(ZeroAscent))]
	[ChildElementInfo(typeof(ShowPhantom))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PhantomProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006DDC RID: 28124
		// (get) Token: 0x06015495 RID: 87189 RVA: 0x0031D9D4 File Offset: 0x0031BBD4
		public override string LocalName
		{
			get
			{
				return "phantPr";
			}
		}

		// Token: 0x17006DDD RID: 28125
		// (get) Token: 0x06015496 RID: 87190 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DDE RID: 28126
		// (get) Token: 0x06015497 RID: 87191 RVA: 0x0031D9DB File Offset: 0x0031BBDB
		internal override int ElementTypeId
		{
			get
			{
				return 10934;
			}
		}

		// Token: 0x06015498 RID: 87192 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015499 RID: 87193 RVA: 0x00293ECF File Offset: 0x002920CF
		public PhantomProperties()
		{
		}

		// Token: 0x0601549A RID: 87194 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PhantomProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601549B RID: 87195 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PhantomProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601549C RID: 87196 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PhantomProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601549D RID: 87197 RVA: 0x0031D9E4 File Offset: 0x0031BBE4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "show" == name)
			{
				return new ShowPhantom();
			}
			if (21 == namespaceId && "zeroWid" == name)
			{
				return new ZeroWidth();
			}
			if (21 == namespaceId && "zeroAsc" == name)
			{
				return new ZeroAscent();
			}
			if (21 == namespaceId && "zeroDesc" == name)
			{
				return new ZeroDescent();
			}
			if (21 == namespaceId && "transp" == name)
			{
				return new Transparent();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006DDF RID: 28127
		// (get) Token: 0x0601549E RID: 87198 RVA: 0x0031DA82 File Offset: 0x0031BC82
		internal override string[] ElementTagNames
		{
			get
			{
				return PhantomProperties.eleTagNames;
			}
		}

		// Token: 0x17006DE0 RID: 28128
		// (get) Token: 0x0601549F RID: 87199 RVA: 0x0031DA89 File Offset: 0x0031BC89
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PhantomProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006DE1 RID: 28129
		// (get) Token: 0x060154A0 RID: 87200 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006DE2 RID: 28130
		// (get) Token: 0x060154A1 RID: 87201 RVA: 0x0031DA90 File Offset: 0x0031BC90
		// (set) Token: 0x060154A2 RID: 87202 RVA: 0x0031DA99 File Offset: 0x0031BC99
		public ShowPhantom ShowPhantom
		{
			get
			{
				return base.GetElement<ShowPhantom>(0);
			}
			set
			{
				base.SetElement<ShowPhantom>(0, value);
			}
		}

		// Token: 0x17006DE3 RID: 28131
		// (get) Token: 0x060154A3 RID: 87203 RVA: 0x0031DAA3 File Offset: 0x0031BCA3
		// (set) Token: 0x060154A4 RID: 87204 RVA: 0x0031DAAC File Offset: 0x0031BCAC
		public ZeroWidth ZeroWidth
		{
			get
			{
				return base.GetElement<ZeroWidth>(1);
			}
			set
			{
				base.SetElement<ZeroWidth>(1, value);
			}
		}

		// Token: 0x17006DE4 RID: 28132
		// (get) Token: 0x060154A5 RID: 87205 RVA: 0x0031DAB6 File Offset: 0x0031BCB6
		// (set) Token: 0x060154A6 RID: 87206 RVA: 0x0031DABF File Offset: 0x0031BCBF
		public ZeroAscent ZeroAscent
		{
			get
			{
				return base.GetElement<ZeroAscent>(2);
			}
			set
			{
				base.SetElement<ZeroAscent>(2, value);
			}
		}

		// Token: 0x17006DE5 RID: 28133
		// (get) Token: 0x060154A7 RID: 87207 RVA: 0x0031DAC9 File Offset: 0x0031BCC9
		// (set) Token: 0x060154A8 RID: 87208 RVA: 0x0031DAD2 File Offset: 0x0031BCD2
		public ZeroDescent ZeroDescent
		{
			get
			{
				return base.GetElement<ZeroDescent>(3);
			}
			set
			{
				base.SetElement<ZeroDescent>(3, value);
			}
		}

		// Token: 0x17006DE6 RID: 28134
		// (get) Token: 0x060154A9 RID: 87209 RVA: 0x0031DADC File Offset: 0x0031BCDC
		// (set) Token: 0x060154AA RID: 87210 RVA: 0x0031DAE5 File Offset: 0x0031BCE5
		public Transparent Transparent
		{
			get
			{
				return base.GetElement<Transparent>(4);
			}
			set
			{
				base.SetElement<Transparent>(4, value);
			}
		}

		// Token: 0x17006DE7 RID: 28135
		// (get) Token: 0x060154AB RID: 87211 RVA: 0x0031C65F File Offset: 0x0031A85F
		// (set) Token: 0x060154AC RID: 87212 RVA: 0x0031C668 File Offset: 0x0031A868
		public ControlProperties ControlProperties
		{
			get
			{
				return base.GetElement<ControlProperties>(5);
			}
			set
			{
				base.SetElement<ControlProperties>(5, value);
			}
		}

		// Token: 0x060154AD RID: 87213 RVA: 0x0031DAEF File Offset: 0x0031BCEF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PhantomProperties>(deep);
		}

		// Token: 0x04009282 RID: 37506
		private const string tagName = "phantPr";

		// Token: 0x04009283 RID: 37507
		private const byte tagNsId = 21;

		// Token: 0x04009284 RID: 37508
		internal const int ElementTypeIdConst = 10934;

		// Token: 0x04009285 RID: 37509
		private static readonly string[] eleTagNames = new string[] { "show", "zeroWid", "zeroAsc", "zeroDesc", "transp", "ctrlPr" };

		// Token: 0x04009286 RID: 37510
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21, 21, 21, 21 };
	}
}
