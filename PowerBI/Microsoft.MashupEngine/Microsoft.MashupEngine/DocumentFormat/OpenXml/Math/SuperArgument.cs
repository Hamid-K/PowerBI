using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002998 RID: 10648
	[GeneratedCode("DomGen", "2.0")]
	internal class SuperArgument : OfficeMathArgumentType
	{
		// Token: 0x17006CE4 RID: 27876
		// (get) Token: 0x06015276 RID: 86646 RVA: 0x0031C3C5 File Offset: 0x0031A5C5
		public override string LocalName
		{
			get
			{
				return "sup";
			}
		}

		// Token: 0x17006CE5 RID: 27877
		// (get) Token: 0x06015277 RID: 86647 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CE6 RID: 27878
		// (get) Token: 0x06015278 RID: 86648 RVA: 0x0031C3CC File Offset: 0x0031A5CC
		internal override int ElementTypeId
		{
			get
			{
				return 10928;
			}
		}

		// Token: 0x06015279 RID: 86649 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601527A RID: 86650 RVA: 0x0031C326 File Offset: 0x0031A526
		public SuperArgument()
		{
		}

		// Token: 0x0601527B RID: 86651 RVA: 0x0031C32E File Offset: 0x0031A52E
		public SuperArgument(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601527C RID: 86652 RVA: 0x0031C337 File Offset: 0x0031A537
		public SuperArgument(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601527D RID: 86653 RVA: 0x0031C340 File Offset: 0x0031A540
		public SuperArgument(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601527E RID: 86654 RVA: 0x0031C3D3 File Offset: 0x0031A5D3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SuperArgument>(deep);
		}

		// Token: 0x040091DA RID: 37338
		private const string tagName = "sup";

		// Token: 0x040091DB RID: 37339
		private const byte tagNsId = 21;

		// Token: 0x040091DC RID: 37340
		internal const int ElementTypeIdConst = 10928;
	}
}
