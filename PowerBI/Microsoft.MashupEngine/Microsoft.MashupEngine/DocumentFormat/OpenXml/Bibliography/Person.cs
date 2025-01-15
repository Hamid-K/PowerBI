using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028B7 RID: 10423
	[ChildElementInfo(typeof(Last))]
	[ChildElementInfo(typeof(First))]
	[ChildElementInfo(typeof(Middle))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Person : OpenXmlCompositeElement
	{
		// Token: 0x170068CD RID: 26829
		// (get) Token: 0x060148AA RID: 84138 RVA: 0x003147C3 File Offset: 0x003129C3
		public override string LocalName
		{
			get
			{
				return "Person";
			}
		}

		// Token: 0x170068CE RID: 26830
		// (get) Token: 0x060148AB RID: 84139 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068CF RID: 26831
		// (get) Token: 0x060148AC RID: 84140 RVA: 0x003147CA File Offset: 0x003129CA
		internal override int ElementTypeId
		{
			get
			{
				return 10759;
			}
		}

		// Token: 0x060148AD RID: 84141 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060148AE RID: 84142 RVA: 0x00293ECF File Offset: 0x002920CF
		public Person()
		{
		}

		// Token: 0x060148AF RID: 84143 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Person(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060148B0 RID: 84144 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Person(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060148B1 RID: 84145 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Person(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060148B2 RID: 84146 RVA: 0x003147D4 File Offset: 0x003129D4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (9 == namespaceId && "Last" == name)
			{
				return new Last();
			}
			if (9 == namespaceId && "First" == name)
			{
				return new First();
			}
			if (9 == namespaceId && "Middle" == name)
			{
				return new Middle();
			}
			return null;
		}

		// Token: 0x060148B3 RID: 84147 RVA: 0x0031482A File Offset: 0x00312A2A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Person>(deep);
		}

		// Token: 0x04008EB1 RID: 36529
		private const string tagName = "Person";

		// Token: 0x04008EB2 RID: 36530
		private const byte tagNsId = 9;

		// Token: 0x04008EB3 RID: 36531
		internal const int ElementTypeIdConst = 10759;
	}
}
