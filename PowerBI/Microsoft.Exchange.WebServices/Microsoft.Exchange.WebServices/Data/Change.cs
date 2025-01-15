using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000305 RID: 773
	[EditorBrowsable(1)]
	public abstract class Change
	{
		// Token: 0x06001B7F RID: 7039 RVA: 0x00049862 File Offset: 0x00048862
		internal Change()
		{
		}

		// Token: 0x06001B80 RID: 7040
		internal abstract ServiceId CreateId();

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x0004986A File Offset: 0x0004886A
		// (set) Token: 0x06001B82 RID: 7042 RVA: 0x00049872 File Offset: 0x00048872
		public ChangeType ChangeType
		{
			get
			{
				return this.changeType;
			}
			internal set
			{
				this.changeType = value;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0004987B File Offset: 0x0004887B
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x00049883 File Offset: 0x00048883
		internal ServiceObject ServiceObject
		{
			get
			{
				return this.serviceObject;
			}
			set
			{
				this.serviceObject = value;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x0004988C File Offset: 0x0004888C
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x000498A8 File Offset: 0x000488A8
		internal ServiceId Id
		{
			get
			{
				if (this.ServiceObject == null)
				{
					return this.id;
				}
				return this.ServiceObject.GetId();
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x04001457 RID: 5207
		private ChangeType changeType;

		// Token: 0x04001458 RID: 5208
		private ServiceObject serviceObject;

		// Token: 0x04001459 RID: 5209
		private ServiceId id;
	}
}
