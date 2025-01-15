using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E7A RID: 11898
	[ChildElementInfo(typeof(RubyContent))]
	[ChildElementInfo(typeof(RubyBase))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RubyProperties))]
	internal class Ruby : OpenXmlCompositeElement
	{
		// Token: 0x17008AC6 RID: 35526
		// (get) Token: 0x06019476 RID: 103542 RVA: 0x00348277 File Offset: 0x00346477
		public override string LocalName
		{
			get
			{
				return "ruby";
			}
		}

		// Token: 0x17008AC7 RID: 35527
		// (get) Token: 0x06019477 RID: 103543 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AC8 RID: 35528
		// (get) Token: 0x06019478 RID: 103544 RVA: 0x0034827E File Offset: 0x0034647E
		internal override int ElementTypeId
		{
			get
			{
				return 11568;
			}
		}

		// Token: 0x06019479 RID: 103545 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601947A RID: 103546 RVA: 0x00293ECF File Offset: 0x002920CF
		public Ruby()
		{
		}

		// Token: 0x0601947B RID: 103547 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Ruby(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601947C RID: 103548 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Ruby(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601947D RID: 103549 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Ruby(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601947E RID: 103550 RVA: 0x00348288 File Offset: 0x00346488
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rubyPr" == name)
			{
				return new RubyProperties();
			}
			if (23 == namespaceId && "rt" == name)
			{
				return new RubyContent();
			}
			if (23 == namespaceId && "rubyBase" == name)
			{
				return new RubyBase();
			}
			return null;
		}

		// Token: 0x17008AC9 RID: 35529
		// (get) Token: 0x0601947F RID: 103551 RVA: 0x003482DE File Offset: 0x003464DE
		internal override string[] ElementTagNames
		{
			get
			{
				return Ruby.eleTagNames;
			}
		}

		// Token: 0x17008ACA RID: 35530
		// (get) Token: 0x06019480 RID: 103552 RVA: 0x003482E5 File Offset: 0x003464E5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Ruby.eleNamespaceIds;
			}
		}

		// Token: 0x17008ACB RID: 35531
		// (get) Token: 0x06019481 RID: 103553 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008ACC RID: 35532
		// (get) Token: 0x06019482 RID: 103554 RVA: 0x003482EC File Offset: 0x003464EC
		// (set) Token: 0x06019483 RID: 103555 RVA: 0x003482F5 File Offset: 0x003464F5
		public RubyProperties RubyProperties
		{
			get
			{
				return base.GetElement<RubyProperties>(0);
			}
			set
			{
				base.SetElement<RubyProperties>(0, value);
			}
		}

		// Token: 0x17008ACD RID: 35533
		// (get) Token: 0x06019484 RID: 103556 RVA: 0x003482FF File Offset: 0x003464FF
		// (set) Token: 0x06019485 RID: 103557 RVA: 0x00348308 File Offset: 0x00346508
		public RubyContent RubyContent
		{
			get
			{
				return base.GetElement<RubyContent>(1);
			}
			set
			{
				base.SetElement<RubyContent>(1, value);
			}
		}

		// Token: 0x17008ACE RID: 35534
		// (get) Token: 0x06019486 RID: 103558 RVA: 0x00348312 File Offset: 0x00346512
		// (set) Token: 0x06019487 RID: 103559 RVA: 0x0034831B File Offset: 0x0034651B
		public RubyBase RubyBase
		{
			get
			{
				return base.GetElement<RubyBase>(2);
			}
			set
			{
				base.SetElement<RubyBase>(2, value);
			}
		}

		// Token: 0x06019488 RID: 103560 RVA: 0x00348325 File Offset: 0x00346525
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Ruby>(deep);
		}

		// Token: 0x0400A810 RID: 43024
		private const string tagName = "ruby";

		// Token: 0x0400A811 RID: 43025
		private const byte tagNsId = 23;

		// Token: 0x0400A812 RID: 43026
		internal const int ElementTypeIdConst = 11568;

		// Token: 0x0400A813 RID: 43027
		private static readonly string[] eleTagNames = new string[] { "rubyPr", "rt", "rubyBase" };

		// Token: 0x0400A814 RID: 43028
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
