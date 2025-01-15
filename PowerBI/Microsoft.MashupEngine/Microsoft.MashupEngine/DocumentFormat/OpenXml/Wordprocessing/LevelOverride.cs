using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F9E RID: 12190
	[ChildElementInfo(typeof(Level))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StartOverrideNumberingValue))]
	internal class LevelOverride : OpenXmlCompositeElement
	{
		// Token: 0x17009276 RID: 37494
		// (get) Token: 0x0601A534 RID: 107828 RVA: 0x00360AB8 File Offset: 0x0035ECB8
		public override string LocalName
		{
			get
			{
				return "lvlOverride";
			}
		}

		// Token: 0x17009277 RID: 37495
		// (get) Token: 0x0601A535 RID: 107829 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009278 RID: 37496
		// (get) Token: 0x0601A536 RID: 107830 RVA: 0x00360ABF File Offset: 0x0035ECBF
		internal override int ElementTypeId
		{
			get
			{
				return 11883;
			}
		}

		// Token: 0x0601A537 RID: 107831 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009279 RID: 37497
		// (get) Token: 0x0601A538 RID: 107832 RVA: 0x00360AC6 File Offset: 0x0035ECC6
		internal override string[] AttributeTagNames
		{
			get
			{
				return LevelOverride.attributeTagNames;
			}
		}

		// Token: 0x1700927A RID: 37498
		// (get) Token: 0x0601A539 RID: 107833 RVA: 0x00360ACD File Offset: 0x0035ECCD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LevelOverride.attributeNamespaceIds;
			}
		}

		// Token: 0x1700927B RID: 37499
		// (get) Token: 0x0601A53A RID: 107834 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601A53B RID: 107835 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "ilvl")]
		public Int32Value LevelIndex
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

		// Token: 0x0601A53C RID: 107836 RVA: 0x00293ECF File Offset: 0x002920CF
		public LevelOverride()
		{
		}

		// Token: 0x0601A53D RID: 107837 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LevelOverride(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A53E RID: 107838 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LevelOverride(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A53F RID: 107839 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LevelOverride(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A540 RID: 107840 RVA: 0x00360AD4 File Offset: 0x0035ECD4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "startOverride" == name)
			{
				return new StartOverrideNumberingValue();
			}
			if (23 == namespaceId && "lvl" == name)
			{
				return new Level();
			}
			return null;
		}

		// Token: 0x1700927C RID: 37500
		// (get) Token: 0x0601A541 RID: 107841 RVA: 0x00360B07 File Offset: 0x0035ED07
		internal override string[] ElementTagNames
		{
			get
			{
				return LevelOverride.eleTagNames;
			}
		}

		// Token: 0x1700927D RID: 37501
		// (get) Token: 0x0601A542 RID: 107842 RVA: 0x00360B0E File Offset: 0x0035ED0E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LevelOverride.eleNamespaceIds;
			}
		}

		// Token: 0x1700927E RID: 37502
		// (get) Token: 0x0601A543 RID: 107843 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700927F RID: 37503
		// (get) Token: 0x0601A544 RID: 107844 RVA: 0x00360B15 File Offset: 0x0035ED15
		// (set) Token: 0x0601A545 RID: 107845 RVA: 0x00360B1E File Offset: 0x0035ED1E
		public StartOverrideNumberingValue StartOverrideNumberingValue
		{
			get
			{
				return base.GetElement<StartOverrideNumberingValue>(0);
			}
			set
			{
				base.SetElement<StartOverrideNumberingValue>(0, value);
			}
		}

		// Token: 0x17009280 RID: 37504
		// (get) Token: 0x0601A546 RID: 107846 RVA: 0x00360B28 File Offset: 0x0035ED28
		// (set) Token: 0x0601A547 RID: 107847 RVA: 0x00360B31 File Offset: 0x0035ED31
		public Level Level
		{
			get
			{
				return base.GetElement<Level>(1);
			}
			set
			{
				base.SetElement<Level>(1, value);
			}
		}

		// Token: 0x0601A548 RID: 107848 RVA: 0x00360B3B File Offset: 0x0035ED3B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "ilvl" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A549 RID: 107849 RVA: 0x00360B5D File Offset: 0x0035ED5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LevelOverride>(deep);
		}

		// Token: 0x0400ACA1 RID: 44193
		private const string tagName = "lvlOverride";

		// Token: 0x0400ACA2 RID: 44194
		private const byte tagNsId = 23;

		// Token: 0x0400ACA3 RID: 44195
		internal const int ElementTypeIdConst = 11883;

		// Token: 0x0400ACA4 RID: 44196
		private static string[] attributeTagNames = new string[] { "ilvl" };

		// Token: 0x0400ACA5 RID: 44197
		private static byte[] attributeNamespaceIds = new byte[] { 23 };

		// Token: 0x0400ACA6 RID: 44198
		private static readonly string[] eleTagNames = new string[] { "startOverride", "lvl" };

		// Token: 0x0400ACA7 RID: 44199
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23 };
	}
}
