using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F7F RID: 12159
	[ChildElementInfo(typeof(RunPropertiesBaseStyle))]
	[GeneratedCode("DomGen", "2.0")]
	internal class RunPropertiesDefault : OpenXmlCompositeElement
	{
		// Token: 0x17009181 RID: 37249
		// (get) Token: 0x0601A32A RID: 107306 RVA: 0x0035F053 File Offset: 0x0035D253
		public override string LocalName
		{
			get
			{
				return "rPrDefault";
			}
		}

		// Token: 0x17009182 RID: 37250
		// (get) Token: 0x0601A32B RID: 107307 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009183 RID: 37251
		// (get) Token: 0x0601A32C RID: 107308 RVA: 0x0035F05A File Offset: 0x0035D25A
		internal override int ElementTypeId
		{
			get
			{
				return 11833;
			}
		}

		// Token: 0x0601A32D RID: 107309 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A32E RID: 107310 RVA: 0x00293ECF File Offset: 0x002920CF
		public RunPropertiesDefault()
		{
		}

		// Token: 0x0601A32F RID: 107311 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RunPropertiesDefault(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A330 RID: 107312 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RunPropertiesDefault(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A331 RID: 107313 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RunPropertiesDefault(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A332 RID: 107314 RVA: 0x0035F061 File Offset: 0x0035D261
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rPr" == name)
			{
				return new RunPropertiesBaseStyle();
			}
			return null;
		}

		// Token: 0x17009184 RID: 37252
		// (get) Token: 0x0601A333 RID: 107315 RVA: 0x0035F07C File Offset: 0x0035D27C
		internal override string[] ElementTagNames
		{
			get
			{
				return RunPropertiesDefault.eleTagNames;
			}
		}

		// Token: 0x17009185 RID: 37253
		// (get) Token: 0x0601A334 RID: 107316 RVA: 0x0035F083 File Offset: 0x0035D283
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RunPropertiesDefault.eleNamespaceIds;
			}
		}

		// Token: 0x17009186 RID: 37254
		// (get) Token: 0x0601A335 RID: 107317 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009187 RID: 37255
		// (get) Token: 0x0601A336 RID: 107318 RVA: 0x0035F08A File Offset: 0x0035D28A
		// (set) Token: 0x0601A337 RID: 107319 RVA: 0x0035F093 File Offset: 0x0035D293
		public RunPropertiesBaseStyle RunPropertiesBaseStyle
		{
			get
			{
				return base.GetElement<RunPropertiesBaseStyle>(0);
			}
			set
			{
				base.SetElement<RunPropertiesBaseStyle>(0, value);
			}
		}

		// Token: 0x0601A338 RID: 107320 RVA: 0x0035F09D File Offset: 0x0035D29D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunPropertiesDefault>(deep);
		}

		// Token: 0x0400AC25 RID: 44069
		private const string tagName = "rPrDefault";

		// Token: 0x0400AC26 RID: 44070
		private const byte tagNsId = 23;

		// Token: 0x0400AC27 RID: 44071
		internal const int ElementTypeIdConst = 11833;

		// Token: 0x0400AC28 RID: 44072
		private static readonly string[] eleTagNames = new string[] { "rPr" };

		// Token: 0x0400AC29 RID: 44073
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
