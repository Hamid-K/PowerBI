using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C0E RID: 11278
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Fill : OpenXmlCompositeElement
	{
		// Token: 0x17007FD2 RID: 32722
		// (get) Token: 0x06017C33 RID: 97331 RVA: 0x002BF458 File Offset: 0x002BD658
		public override string LocalName
		{
			get
			{
				return "fill";
			}
		}

		// Token: 0x17007FD3 RID: 32723
		// (get) Token: 0x06017C34 RID: 97332 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007FD4 RID: 32724
		// (get) Token: 0x06017C35 RID: 97333 RVA: 0x0033AE64 File Offset: 0x00339064
		internal override int ElementTypeId
		{
			get
			{
				return 11259;
			}
		}

		// Token: 0x06017C36 RID: 97334 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017C37 RID: 97335 RVA: 0x00293ECF File Offset: 0x002920CF
		public Fill()
		{
		}

		// Token: 0x06017C38 RID: 97336 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Fill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017C39 RID: 97337 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Fill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017C3A RID: 97338 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Fill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017C3B RID: 97339 RVA: 0x0033AE6B File Offset: 0x0033906B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "patternFill" == name)
			{
				return new PatternFill();
			}
			if (22 == namespaceId && "gradientFill" == name)
			{
				return new GradientFill();
			}
			return null;
		}

		// Token: 0x17007FD5 RID: 32725
		// (get) Token: 0x06017C3C RID: 97340 RVA: 0x0033AE9E File Offset: 0x0033909E
		internal override string[] ElementTagNames
		{
			get
			{
				return Fill.eleTagNames;
			}
		}

		// Token: 0x17007FD6 RID: 32726
		// (get) Token: 0x06017C3D RID: 97341 RVA: 0x0033AEA5 File Offset: 0x003390A5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Fill.eleNamespaceIds;
			}
		}

		// Token: 0x17007FD7 RID: 32727
		// (get) Token: 0x06017C3E RID: 97342 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17007FD8 RID: 32728
		// (get) Token: 0x06017C3F RID: 97343 RVA: 0x0033AEAC File Offset: 0x003390AC
		// (set) Token: 0x06017C40 RID: 97344 RVA: 0x0033AEB5 File Offset: 0x003390B5
		public PatternFill PatternFill
		{
			get
			{
				return base.GetElement<PatternFill>(0);
			}
			set
			{
				base.SetElement<PatternFill>(0, value);
			}
		}

		// Token: 0x17007FD9 RID: 32729
		// (get) Token: 0x06017C41 RID: 97345 RVA: 0x0033AEBF File Offset: 0x003390BF
		// (set) Token: 0x06017C42 RID: 97346 RVA: 0x0033AEC8 File Offset: 0x003390C8
		public GradientFill GradientFill
		{
			get
			{
				return base.GetElement<GradientFill>(1);
			}
			set
			{
				base.SetElement<GradientFill>(1, value);
			}
		}

		// Token: 0x06017C43 RID: 97347 RVA: 0x0033AED2 File Offset: 0x003390D2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Fill>(deep);
		}

		// Token: 0x04009D77 RID: 40311
		private const string tagName = "fill";

		// Token: 0x04009D78 RID: 40312
		private const byte tagNsId = 22;

		// Token: 0x04009D79 RID: 40313
		internal const int ElementTypeIdConst = 11259;

		// Token: 0x04009D7A RID: 40314
		private static readonly string[] eleTagNames = new string[] { "patternFill", "gradientFill" };

		// Token: 0x04009D7B RID: 40315
		private static readonly byte[] eleNamespaceIds = new byte[] { 22, 22 };
	}
}
