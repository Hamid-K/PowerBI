using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BAD RID: 11181
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RunProperties))]
	[ChildElementInfo(typeof(Text))]
	internal class Run : OpenXmlCompositeElement
	{
		// Token: 0x17007B76 RID: 31606
		// (get) Token: 0x060172F0 RID: 94960 RVA: 0x002BF737 File Offset: 0x002BD937
		public override string LocalName
		{
			get
			{
				return "r";
			}
		}

		// Token: 0x17007B77 RID: 31607
		// (get) Token: 0x060172F1 RID: 94961 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B78 RID: 31608
		// (get) Token: 0x060172F2 RID: 94962 RVA: 0x0033399F File Offset: 0x00331B9F
		internal override int ElementTypeId
		{
			get
			{
				return 11152;
			}
		}

		// Token: 0x060172F3 RID: 94963 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060172F4 RID: 94964 RVA: 0x00293ECF File Offset: 0x002920CF
		public Run()
		{
		}

		// Token: 0x060172F5 RID: 94965 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Run(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060172F6 RID: 94966 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Run(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060172F7 RID: 94967 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Run(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060172F8 RID: 94968 RVA: 0x003339A6 File Offset: 0x00331BA6
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "rPr" == name)
			{
				return new RunProperties();
			}
			if (22 == namespaceId && "t" == name)
			{
				return new Text();
			}
			return null;
		}

		// Token: 0x17007B79 RID: 31609
		// (get) Token: 0x060172F9 RID: 94969 RVA: 0x003339D9 File Offset: 0x00331BD9
		internal override string[] ElementTagNames
		{
			get
			{
				return Run.eleTagNames;
			}
		}

		// Token: 0x17007B7A RID: 31610
		// (get) Token: 0x060172FA RID: 94970 RVA: 0x003339E0 File Offset: 0x00331BE0
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Run.eleNamespaceIds;
			}
		}

		// Token: 0x17007B7B RID: 31611
		// (get) Token: 0x060172FB RID: 94971 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007B7C RID: 31612
		// (get) Token: 0x060172FC RID: 94972 RVA: 0x003339E7 File Offset: 0x00331BE7
		// (set) Token: 0x060172FD RID: 94973 RVA: 0x003339F0 File Offset: 0x00331BF0
		public RunProperties RunProperties
		{
			get
			{
				return base.GetElement<RunProperties>(0);
			}
			set
			{
				base.SetElement<RunProperties>(0, value);
			}
		}

		// Token: 0x17007B7D RID: 31613
		// (get) Token: 0x060172FE RID: 94974 RVA: 0x003339FA File Offset: 0x00331BFA
		// (set) Token: 0x060172FF RID: 94975 RVA: 0x00333A03 File Offset: 0x00331C03
		public Text Text
		{
			get
			{
				return base.GetElement<Text>(1);
			}
			set
			{
				base.SetElement<Text>(1, value);
			}
		}

		// Token: 0x06017300 RID: 94976 RVA: 0x00333A0D File Offset: 0x00331C0D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Run>(deep);
		}

		// Token: 0x04009B7E RID: 39806
		private const string tagName = "r";

		// Token: 0x04009B7F RID: 39807
		private const byte tagNsId = 22;

		// Token: 0x04009B80 RID: 39808
		internal const int ElementTypeIdConst = 11152;

		// Token: 0x04009B81 RID: 39809
		private static readonly string[] eleTagNames = new string[] { "rPr", "t" };

		// Token: 0x04009B82 RID: 39810
		private static readonly byte[] eleNamespaceIds = new byte[] { 22, 22 };
	}
}
