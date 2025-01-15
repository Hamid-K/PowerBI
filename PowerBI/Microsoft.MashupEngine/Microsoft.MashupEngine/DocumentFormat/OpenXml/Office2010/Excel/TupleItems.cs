using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002426 RID: 9254
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Xstring), FileFormatVersions.Office2010)]
	internal class TupleItems : OpenXmlCompositeElement
	{
		// Token: 0x17004F84 RID: 20356
		// (get) Token: 0x06010FD2 RID: 69586 RVA: 0x002E9619 File Offset: 0x002E7819
		public override string LocalName
		{
			get
			{
				return "tupleItems";
			}
		}

		// Token: 0x17004F85 RID: 20357
		// (get) Token: 0x06010FD3 RID: 69587 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F86 RID: 20358
		// (get) Token: 0x06010FD4 RID: 69588 RVA: 0x002E9620 File Offset: 0x002E7820
		internal override int ElementTypeId
		{
			get
			{
				return 12978;
			}
		}

		// Token: 0x06010FD5 RID: 69589 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010FD6 RID: 69590 RVA: 0x00293ECF File Offset: 0x002920CF
		public TupleItems()
		{
		}

		// Token: 0x06010FD7 RID: 69591 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TupleItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010FD8 RID: 69592 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TupleItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010FD9 RID: 69593 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TupleItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010FDA RID: 69594 RVA: 0x002E9627 File Offset: 0x002E7827
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "tupleItem" == name)
			{
				return new Xstring();
			}
			return null;
		}

		// Token: 0x06010FDB RID: 69595 RVA: 0x002E9642 File Offset: 0x002E7842
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TupleItems>(deep);
		}

		// Token: 0x0400772F RID: 30511
		private const string tagName = "tupleItems";

		// Token: 0x04007730 RID: 30512
		private const byte tagNsId = 53;

		// Token: 0x04007731 RID: 30513
		internal const int ElementTypeIdConst = 12978;
	}
}
