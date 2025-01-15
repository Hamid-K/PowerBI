using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
	// Token: 0x02000043 RID: 67
	public class System
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00002E39 File Offset: 0x00001039
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00002E41 File Offset: 0x00001041
		[ReadOnly(true)]
		public Guid Id { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00002E4C File Offset: 0x0000104C
		public IList<AllowedAction> AllowedActions
		{
			get
			{
				IList<AllowedAction> list;
				if ((list = this._allowedActions) == null)
				{
					list = (this._allowedActions = this.LoadAllowedActions());
				}
				return list;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00002E72 File Offset: 0x00001072
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00002E7A File Offset: 0x0000107A
		public string ReportServerAbsoluteUrl { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00002E83 File Offset: 0x00001083
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00002E8B File Offset: 0x0000108B
		public string ReportServerRelativeUrl { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00002E94 File Offset: 0x00001094
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00002E9C File Offset: 0x0000109C
		public string WebPortalRelativeUrl { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00002EA5 File Offset: 0x000010A5
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00002EAD File Offset: 0x000010AD
		public string ProductName { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00002EB6 File Offset: 0x000010B6
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00002EBE File Offset: 0x000010BE
		public string ProductVersion { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00002EC8 File Offset: 0x000010C8
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00002EEE File Offset: 0x000010EE
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00002EF6 File Offset: 0x000010F6
		public string TimeZone { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00002EFF File Offset: 0x000010FF
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00002F07 File Offset: 0x00001107
		public ProductType ProductType { get; set; }

		// Token: 0x060001AD RID: 429 RVA: 0x00002F10 File Offset: 0x00001110
		protected virtual IList<AllowedAction> LoadAllowedActions()
		{
			return new List<AllowedAction>();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00002F17 File Offset: 0x00001117
		protected virtual IList<SystemPolicy> LoadSystemPolicies()
		{
			return new List<SystemPolicy>();
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00002F20 File Offset: 0x00001120
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

		// Token: 0x060001B0 RID: 432 RVA: 0x00002F46 File Offset: 0x00001146
		protected virtual IList<Role> LoadSystemRoles()
		{
			return new List<Role>();
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00002F50 File Offset: 0x00001150
		public IList<Property> Properties
		{
			get
			{
				IList<Property> list;
				if ((list = this._properties) == null)
				{
					list = (this._properties = this.LoadProperties());
				}
				return list;
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00002F76 File Offset: 0x00001176
		protected virtual IList<Property> LoadProperties()
		{
			return new List<Property>();
		}

		// Token: 0x04000171 RID: 369
		private IList<AllowedAction> _allowedActions;

		// Token: 0x04000172 RID: 370
		private IList<SystemPolicy> _policies;

		// Token: 0x04000173 RID: 371
		private IList<Role> _roles;

		// Token: 0x04000174 RID: 372
		private IList<Property> _properties;
	}
}
