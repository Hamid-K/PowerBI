using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025C7 RID: 9671
	[GeneratedCode("DomGen", "2.0")]
	internal class Floor : SurfaceType
	{
		// Token: 0x170057AE RID: 22446
		// (get) Token: 0x0601221C RID: 74268 RVA: 0x002F5EBC File Offset: 0x002F40BC
		public override string LocalName
		{
			get
			{
				return "floor";
			}
		}

		// Token: 0x170057AF RID: 22447
		// (get) Token: 0x0601221D RID: 74269 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170057B0 RID: 22448
		// (get) Token: 0x0601221E RID: 74270 RVA: 0x002F5EC3 File Offset: 0x002F40C3
		internal override int ElementTypeId
		{
			get
			{
				return 10497;
			}
		}

		// Token: 0x0601221F RID: 74271 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012220 RID: 74272 RVA: 0x002F5ECA File Offset: 0x002F40CA
		public Floor()
		{
		}

		// Token: 0x06012221 RID: 74273 RVA: 0x002F5ED2 File Offset: 0x002F40D2
		public Floor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012222 RID: 74274 RVA: 0x002F5EDB File Offset: 0x002F40DB
		public Floor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012223 RID: 74275 RVA: 0x002F5EE4 File Offset: 0x002F40E4
		public Floor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012224 RID: 74276 RVA: 0x002F5EED File Offset: 0x002F40ED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Floor>(deep);
		}

		// Token: 0x04007E5B RID: 32347
		private const string tagName = "floor";

		// Token: 0x04007E5C RID: 32348
		private const byte tagNsId = 11;

		// Token: 0x04007E5D RID: 32349
		internal const int ElementTypeIdConst = 10497;
	}
}
