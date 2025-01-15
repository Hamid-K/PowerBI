using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
	// Token: 0x02000053 RID: 83
	public class ReportServerInfo
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00003244 File Offset: 0x00001444
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000324C File Offset: 0x0000144C
		[ReadOnly(true)]
		public Guid Id { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00003255 File Offset: 0x00001455
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000325D File Offset: 0x0000145D
		public string ReportServerUrl { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00003266 File Offset: 0x00001466
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000326E File Offset: 0x0000146E
		public string VirtualDirectory { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00003277 File Offset: 0x00001477
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000327F File Offset: 0x0000147F
		public string WebAppUrl { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00003288 File Offset: 0x00001488
		public IList<SystemPolicy> Policies
		{
			get
			{
				IList<SystemPolicy> list;
				if ((list = this._policies) == null)
				{
					list = (this._policies = this.LoadSystemPolicies());
				}
				return list;
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00002F17 File Offset: 0x00001117
		protected virtual IList<SystemPolicy> LoadSystemPolicies()
		{
			return new List<SystemPolicy>();
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000032B0 File Offset: 0x000014B0
		public IList<Role> Roles
		{
			get
			{
				IList<Role> list;
				if ((list = this._roles) == null)
				{
					list = (this._roles = this.LoadSystemRoles());
				}
				return list;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00002F46 File Offset: 0x00001146
		protected virtual IList<Role> LoadSystemRoles()
		{
			return new List<Role>();
		}

		// Token: 0x040001AB RID: 427
		private IList<SystemPolicy> _policies;

		// Token: 0x040001AC RID: 428
		private IList<Role> _roles;
	}
}
