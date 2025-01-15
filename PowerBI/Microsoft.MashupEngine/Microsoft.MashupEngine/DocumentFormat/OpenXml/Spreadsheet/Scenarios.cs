using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C97 RID: 11415
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Scenario))]
	internal class Scenarios : OpenXmlCompositeElement
	{
		// Token: 0x17008401 RID: 33793
		// (get) Token: 0x060185D2 RID: 99794 RVA: 0x00340F1C File Offset: 0x0033F11C
		public override string LocalName
		{
			get
			{
				return "scenarios";
			}
		}

		// Token: 0x17008402 RID: 33794
		// (get) Token: 0x060185D3 RID: 99795 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008403 RID: 33795
		// (get) Token: 0x060185D4 RID: 99796 RVA: 0x00340F23 File Offset: 0x0033F123
		internal override int ElementTypeId
		{
			get
			{
				return 11395;
			}
		}

		// Token: 0x060185D5 RID: 99797 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008404 RID: 33796
		// (get) Token: 0x060185D6 RID: 99798 RVA: 0x00340F2A File Offset: 0x0033F12A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Scenarios.attributeTagNames;
			}
		}

		// Token: 0x17008405 RID: 33797
		// (get) Token: 0x060185D7 RID: 99799 RVA: 0x00340F31 File Offset: 0x0033F131
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Scenarios.attributeNamespaceIds;
			}
		}

		// Token: 0x17008406 RID: 33798
		// (get) Token: 0x060185D8 RID: 99800 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060185D9 RID: 99801 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "current")]
		public UInt32Value Current
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008407 RID: 33799
		// (get) Token: 0x060185DA RID: 99802 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x060185DB RID: 99803 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "show")]
		public UInt32Value Show
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008408 RID: 33800
		// (get) Token: 0x060185DC RID: 99804 RVA: 0x002C8283 File Offset: 0x002C6483
		// (set) Token: 0x060185DD RID: 99805 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "sqref")]
		public ListValue<StringValue> SequenceOfReferences
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060185DE RID: 99806 RVA: 0x00293ECF File Offset: 0x002920CF
		public Scenarios()
		{
		}

		// Token: 0x060185DF RID: 99807 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Scenarios(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060185E0 RID: 99808 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Scenarios(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060185E1 RID: 99809 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Scenarios(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060185E2 RID: 99810 RVA: 0x00340F38 File Offset: 0x0033F138
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "scenario" == name)
			{
				return new Scenario();
			}
			return null;
		}

		// Token: 0x060185E3 RID: 99811 RVA: 0x00340F54 File Offset: 0x0033F154
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "current" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "show" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "sqref" == name)
			{
				return new ListValue<StringValue>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060185E4 RID: 99812 RVA: 0x00340FAB File Offset: 0x0033F1AB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Scenarios>(deep);
		}

		// Token: 0x060185E5 RID: 99813 RVA: 0x00340FB4 File Offset: 0x0033F1B4
		// Note: this type is marked as 'beforefieldinit'.
		static Scenarios()
		{
			byte[] array = new byte[3];
			Scenarios.attributeNamespaceIds = array;
		}

		// Token: 0x04009FEE RID: 40942
		private const string tagName = "scenarios";

		// Token: 0x04009FEF RID: 40943
		private const byte tagNsId = 22;

		// Token: 0x04009FF0 RID: 40944
		internal const int ElementTypeIdConst = 11395;

		// Token: 0x04009FF1 RID: 40945
		private static string[] attributeTagNames = new string[] { "current", "show", "sqref" };

		// Token: 0x04009FF2 RID: 40946
		private static byte[] attributeNamespaceIds;
	}
}
