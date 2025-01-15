using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Providers
{
	// Token: 0x020000F1 RID: 241
	internal class EdmEnumTypeWithDelayLoadedMembers : EdmEnumType
	{
		// Token: 0x06000A21 RID: 2593 RVA: 0x000258BC File Offset: 0x00023ABC
		internal EdmEnumTypeWithDelayLoadedMembers(string namespaceName, string name, IEdmPrimitiveType underlyingType, bool isFlags, Action<EdmEnumTypeWithDelayLoadedMembers> memberLoadAction)
			: base(namespaceName, name, underlyingType, isFlags)
		{
			this.memberLoadAction = memberLoadAction;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000258DC File Offset: 0x00023ADC
		public override IEnumerable<IEdmEnumMember> Members
		{
			get
			{
				this.EnsureMemberLoaded();
				return base.Members;
			}
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000258EC File Offset: 0x00023AEC
		private void EnsureMemberLoaded()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.memberLoadAction != null)
				{
					this.memberLoadAction(this);
					this.memberLoadAction = null;
				}
			}
		}

		// Token: 0x040005E0 RID: 1504
		private readonly object lockObject = new object();

		// Token: 0x040005E1 RID: 1505
		private Action<EdmEnumTypeWithDelayLoadedMembers> memberLoadAction;
	}
}
