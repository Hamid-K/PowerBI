using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002995 RID: 10645
	[GeneratedCode("DomGen", "2.0")]
	internal class FunctionName : OfficeMathArgumentType
	{
		// Token: 0x17006CDB RID: 27867
		// (get) Token: 0x0601525B RID: 86619 RVA: 0x0031C380 File Offset: 0x0031A580
		public override string LocalName
		{
			get
			{
				return "fName";
			}
		}

		// Token: 0x17006CDC RID: 27868
		// (get) Token: 0x0601525C RID: 86620 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CDD RID: 27869
		// (get) Token: 0x0601525D RID: 86621 RVA: 0x0031C387 File Offset: 0x0031A587
		internal override int ElementTypeId
		{
			get
			{
				return 10906;
			}
		}

		// Token: 0x0601525E RID: 86622 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601525F RID: 86623 RVA: 0x0031C326 File Offset: 0x0031A526
		public FunctionName()
		{
		}

		// Token: 0x06015260 RID: 86624 RVA: 0x0031C32E File Offset: 0x0031A52E
		public FunctionName(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015261 RID: 86625 RVA: 0x0031C337 File Offset: 0x0031A537
		public FunctionName(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015262 RID: 86626 RVA: 0x0031C340 File Offset: 0x0031A540
		public FunctionName(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015263 RID: 86627 RVA: 0x0031C38E File Offset: 0x0031A58E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FunctionName>(deep);
		}

		// Token: 0x040091D1 RID: 37329
		private const string tagName = "fName";

		// Token: 0x040091D2 RID: 37330
		private const byte tagNsId = 21;

		// Token: 0x040091D3 RID: 37331
		internal const int ElementTypeIdConst = 10906;
	}
}
