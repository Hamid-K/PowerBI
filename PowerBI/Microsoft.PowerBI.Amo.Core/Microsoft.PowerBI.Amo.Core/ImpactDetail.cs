using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200008E RID: 142
	[Guid("370355BF-7C27-49f8-9D89-4D2F9466D856")]
	public sealed class ImpactDetail
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x000247D3 File Offset: 0x000229D3
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x000247DB File Offset: 0x000229DB
		internal IObjectReference ObjectReference
		{
			get
			{
				return this.objectReference;
			}
			set
			{
				this.objectReference = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x000247E4 File Offset: 0x000229E4
		public MajorObject Object
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000247EC File Offset: 0x000229EC
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x000247F4 File Offset: 0x000229F4
		public ImpactAnalysisType Impact
		{
			get
			{
				return this.impact;
			}
			set
			{
				this.impact = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x000247FD File Offset: 0x000229FD
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00024805 File Offset: 0x00022A05
		public string Severity
		{
			get
			{
				return this.severity;
			}
			set
			{
				this.severity = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0002480E File Offset: 0x00022A0E
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x00024816 File Offset: 0x00022A16
		public string FaultCode
		{
			get
			{
				return this.faultCode;
			}
			set
			{
				this.faultCode = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0002481F File Offset: 0x00022A1F
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x00024827 File Offset: 0x00022A27
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x0400046E RID: 1134
		private IObjectReference objectReference;

		// Token: 0x0400046F RID: 1135
		internal MajorObject obj;

		// Token: 0x04000470 RID: 1136
		private ImpactAnalysisType impact;

		// Token: 0x04000471 RID: 1137
		private string severity;

		// Token: 0x04000472 RID: 1138
		private string faultCode;

		// Token: 0x04000473 RID: 1139
		private string description;
	}
}
