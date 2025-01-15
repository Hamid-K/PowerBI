using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001EE RID: 494
	internal sealed class GetPoliciesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0003AA96 File Offset: 0x00038C96
		// (set) Token: 0x060010E2 RID: 4322 RVA: 0x0003AA9E File Offset: 0x00038C9E
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0003AAA7 File Offset: 0x00038CA7
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x0003AAAF File Offset: 0x00038CAF
		public bool InheritParent
		{
			get
			{
				return this.m_inheritParent;
			}
			set
			{
				this.m_inheritParent = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0003AAB8 File Offset: 0x00038CB8
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x0003AAC0 File Offset: 0x00038CC0
		public Policy[] Policies
		{
			get
			{
				return this.m_policies;
			}
			set
			{
				this.m_policies = value;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0003AAC9 File Offset: 0x00038CC9
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0003AAD1 File Offset: 0x00038CD1
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("Item");
			}
		}

		// Token: 0x04000674 RID: 1652
		private string m_itemPath;

		// Token: 0x04000675 RID: 1653
		private bool m_inheritParent;

		// Token: 0x04000676 RID: 1654
		private Policy[] m_policies;
	}
}
