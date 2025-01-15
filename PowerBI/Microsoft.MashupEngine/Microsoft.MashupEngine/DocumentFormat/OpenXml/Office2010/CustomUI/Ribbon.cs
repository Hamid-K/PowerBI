using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x0200230B RID: 8971
	[ChildElementInfo(typeof(QuickAccessToolbar), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Tabs), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ContextualTabs), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Ribbon : OpenXmlCompositeElement
	{
		// Token: 0x17004810 RID: 18448
		// (get) Token: 0x0600FF5F RID: 65375 RVA: 0x002D0638 File Offset: 0x002CE838
		public override string LocalName
		{
			get
			{
				return "ribbon";
			}
		}

		// Token: 0x17004811 RID: 18449
		// (get) Token: 0x0600FF60 RID: 65376 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004812 RID: 18450
		// (get) Token: 0x0600FF61 RID: 65377 RVA: 0x002DDE9C File Offset: 0x002DC09C
		internal override int ElementTypeId
		{
			get
			{
				return 13113;
			}
		}

		// Token: 0x0600FF62 RID: 65378 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004813 RID: 18451
		// (get) Token: 0x0600FF63 RID: 65379 RVA: 0x002DDEA3 File Offset: 0x002DC0A3
		internal override string[] AttributeTagNames
		{
			get
			{
				return Ribbon.attributeTagNames;
			}
		}

		// Token: 0x17004814 RID: 18452
		// (get) Token: 0x0600FF64 RID: 65380 RVA: 0x002DDEAA File Offset: 0x002DC0AA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Ribbon.attributeNamespaceIds;
			}
		}

		// Token: 0x17004815 RID: 18453
		// (get) Token: 0x0600FF65 RID: 65381 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0600FF66 RID: 65382 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "startFromScratch")]
		public BooleanValue StartFromScratch
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

		// Token: 0x0600FF67 RID: 65383 RVA: 0x00293ECF File Offset: 0x002920CF
		public Ribbon()
		{
		}

		// Token: 0x0600FF68 RID: 65384 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Ribbon(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FF69 RID: 65385 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Ribbon(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FF6A RID: 65386 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Ribbon(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FF6B RID: 65387 RVA: 0x002DDEB4 File Offset: 0x002DC0B4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "qat" == name)
			{
				return new QuickAccessToolbar();
			}
			if (57 == namespaceId && "tabs" == name)
			{
				return new Tabs();
			}
			if (57 == namespaceId && "contextualTabs" == name)
			{
				return new ContextualTabs();
			}
			return null;
		}

		// Token: 0x17004816 RID: 18454
		// (get) Token: 0x0600FF6C RID: 65388 RVA: 0x002DDF0A File Offset: 0x002DC10A
		internal override string[] ElementTagNames
		{
			get
			{
				return Ribbon.eleTagNames;
			}
		}

		// Token: 0x17004817 RID: 18455
		// (get) Token: 0x0600FF6D RID: 65389 RVA: 0x002DDF11 File Offset: 0x002DC111
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Ribbon.eleNamespaceIds;
			}
		}

		// Token: 0x17004818 RID: 18456
		// (get) Token: 0x0600FF6E RID: 65390 RVA: 0x0000240C File Offset: 0x0000060C
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneAll;
			}
		}

		// Token: 0x17004819 RID: 18457
		// (get) Token: 0x0600FF6F RID: 65391 RVA: 0x002DDF18 File Offset: 0x002DC118
		// (set) Token: 0x0600FF70 RID: 65392 RVA: 0x002DDF21 File Offset: 0x002DC121
		public QuickAccessToolbar QuickAccessToolbar
		{
			get
			{
				return base.GetElement<QuickAccessToolbar>(0);
			}
			set
			{
				base.SetElement<QuickAccessToolbar>(0, value);
			}
		}

		// Token: 0x1700481A RID: 18458
		// (get) Token: 0x0600FF71 RID: 65393 RVA: 0x002DDF2B File Offset: 0x002DC12B
		// (set) Token: 0x0600FF72 RID: 65394 RVA: 0x002DDF34 File Offset: 0x002DC134
		public Tabs Tabs
		{
			get
			{
				return base.GetElement<Tabs>(1);
			}
			set
			{
				base.SetElement<Tabs>(1, value);
			}
		}

		// Token: 0x1700481B RID: 18459
		// (get) Token: 0x0600FF73 RID: 65395 RVA: 0x002DDF3E File Offset: 0x002DC13E
		// (set) Token: 0x0600FF74 RID: 65396 RVA: 0x002DDF47 File Offset: 0x002DC147
		public ContextualTabs ContextualTabs
		{
			get
			{
				return base.GetElement<ContextualTabs>(2);
			}
			set
			{
				base.SetElement<ContextualTabs>(2, value);
			}
		}

		// Token: 0x0600FF75 RID: 65397 RVA: 0x002D071C File Offset: 0x002CE91C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "startFromScratch" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FF76 RID: 65398 RVA: 0x002DDF51 File Offset: 0x002DC151
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Ribbon>(deep);
		}

		// Token: 0x0600FF77 RID: 65399 RVA: 0x002DDF5C File Offset: 0x002DC15C
		// Note: this type is marked as 'beforefieldinit'.
		static Ribbon()
		{
			byte[] array = new byte[1];
			Ribbon.attributeNamespaceIds = array;
			Ribbon.eleTagNames = new string[] { "qat", "tabs", "contextualTabs" };
			Ribbon.eleNamespaceIds = new byte[] { 57, 57, 57 };
		}

		// Token: 0x04007245 RID: 29253
		private const string tagName = "ribbon";

		// Token: 0x04007246 RID: 29254
		private const byte tagNsId = 57;

		// Token: 0x04007247 RID: 29255
		internal const int ElementTypeIdConst = 13113;

		// Token: 0x04007248 RID: 29256
		private static string[] attributeTagNames = new string[] { "startFromScratch" };

		// Token: 0x04007249 RID: 29257
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400724A RID: 29258
		private static readonly string[] eleTagNames;

		// Token: 0x0400724B RID: 29259
		private static readonly byte[] eleNamespaceIds;
	}
}
