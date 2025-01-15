using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002761 RID: 10081
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RunProperties))]
	internal class Break : OpenXmlCompositeElement
	{
		// Token: 0x170060E6 RID: 24806
		// (get) Token: 0x060136A0 RID: 79520 RVA: 0x00306C25 File Offset: 0x00304E25
		public override string LocalName
		{
			get
			{
				return "br";
			}
		}

		// Token: 0x170060E7 RID: 24807
		// (get) Token: 0x060136A1 RID: 79521 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060E8 RID: 24808
		// (get) Token: 0x060136A2 RID: 79522 RVA: 0x00306C2C File Offset: 0x00304E2C
		internal override int ElementTypeId
		{
			get
			{
				return 10118;
			}
		}

		// Token: 0x060136A3 RID: 79523 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060136A4 RID: 79524 RVA: 0x00293ECF File Offset: 0x002920CF
		public Break()
		{
		}

		// Token: 0x060136A5 RID: 79525 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Break(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060136A6 RID: 79526 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Break(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060136A7 RID: 79527 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Break(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060136A8 RID: 79528 RVA: 0x00306C33 File Offset: 0x00304E33
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "rPr" == name)
			{
				return new RunProperties();
			}
			return null;
		}

		// Token: 0x170060E9 RID: 24809
		// (get) Token: 0x060136A9 RID: 79529 RVA: 0x00306C4E File Offset: 0x00304E4E
		internal override string[] ElementTagNames
		{
			get
			{
				return Break.eleTagNames;
			}
		}

		// Token: 0x170060EA RID: 24810
		// (get) Token: 0x060136AA RID: 79530 RVA: 0x00306C55 File Offset: 0x00304E55
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Break.eleNamespaceIds;
			}
		}

		// Token: 0x170060EB RID: 24811
		// (get) Token: 0x060136AB RID: 79531 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170060EC RID: 24812
		// (get) Token: 0x060136AC RID: 79532 RVA: 0x00306BB4 File Offset: 0x00304DB4
		// (set) Token: 0x060136AD RID: 79533 RVA: 0x00306BBD File Offset: 0x00304DBD
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

		// Token: 0x060136AE RID: 79534 RVA: 0x00306C5C File Offset: 0x00304E5C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Break>(deep);
		}

		// Token: 0x0400861D RID: 34333
		private const string tagName = "br";

		// Token: 0x0400861E RID: 34334
		private const byte tagNsId = 10;

		// Token: 0x0400861F RID: 34335
		internal const int ElementTypeIdConst = 10118;

		// Token: 0x04008620 RID: 34336
		private static readonly string[] eleTagNames = new string[] { "rPr" };

		// Token: 0x04008621 RID: 34337
		private static readonly byte[] eleNamespaceIds = new byte[] { 10 };
	}
}
