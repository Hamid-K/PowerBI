using System;
using System.Collections;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000026 RID: 38
	internal sealed class WindowsAcl : CollectionBase
	{
		// Token: 0x0600009E RID: 158 RVA: 0x000046A1 File Offset: 0x000028A1
		internal WindowsAcl()
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000046AC File Offset: 0x000028AC
		internal WindowsAcl(AceCollection genericAces)
		{
			foreach (object obj in genericAces)
			{
				WindowsAce windowsAce = new WindowsAce((AceStruct)obj);
				this.Add(windowsAce);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000470C File Offset: 0x0000290C
		internal int Add(WindowsAce ace)
		{
			return base.InnerList.Add(ace);
		}

		// Token: 0x1700000A RID: 10
		internal WindowsAce this[int index]
		{
			get
			{
				return (WindowsAce)base.InnerList[index];
			}
		}
	}
}
