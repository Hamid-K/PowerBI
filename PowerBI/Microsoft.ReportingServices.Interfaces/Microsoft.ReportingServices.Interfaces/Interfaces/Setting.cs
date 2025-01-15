using System;
using System.Collections;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000035 RID: 53
	public class Setting
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000020FB File Offset: 0x000002FB
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002103 File Offset: 0x00000303
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000210C File Offset: 0x0000030C
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002114 File Offset: 0x00000314
		public string DisplayName
		{
			get
			{
				return this.m_displayName;
			}
			set
			{
				this.m_displayName = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000211D File Offset: 0x0000031D
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002125 File Offset: 0x00000325
		public string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000212E File Offset: 0x0000032E
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002136 File Offset: 0x00000336
		public bool Required
		{
			get
			{
				return this.m_required;
			}
			set
			{
				this.m_required = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000213F File Offset: 0x0000033F
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002147 File Offset: 0x00000347
		public bool ReadOnly
		{
			get
			{
				return this.m_readOnly;
			}
			set
			{
				this.m_readOnly = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002150 File Offset: 0x00000350
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002158 File Offset: 0x00000358
		public string Field
		{
			get
			{
				return this.m_field;
			}
			set
			{
				this.m_field = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002161 File Offset: 0x00000361
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00002169 File Offset: 0x00000369
		public string Error
		{
			get
			{
				return this.m_error;
			}
			set
			{
				this.m_error = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002172 File Offset: 0x00000372
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000218E File Offset: 0x0000038E
		public ValidValue[] ValidValues
		{
			get
			{
				return this.m_validValues.ToArray(typeof(ValidValue)) as ValidValue[];
			}
			set
			{
				if (value == null)
				{
					this.m_validValues = new ArrayList();
					return;
				}
				this.m_validValues = new ArrayList(value);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000021AB File Offset: 0x000003AB
		public void AddValidValue(ValidValue val)
		{
			this.m_validValues.Add(val);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000021BC File Offset: 0x000003BC
		public void AddValidValue(string label, string val)
		{
			this.AddValidValue(new ValidValue
			{
				Value = val,
				Label = label
			});
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000021E4 File Offset: 0x000003E4
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000021EC File Offset: 0x000003EC
		public bool Encrypted
		{
			get
			{
				return this.m_encrypted;
			}
			set
			{
				this.m_encrypted = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000021F5 File Offset: 0x000003F5
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000021FD File Offset: 0x000003FD
		public bool IsPassword
		{
			get
			{
				return this.m_isPassword;
			}
			set
			{
				this.m_isPassword = value;
			}
		}

		// Token: 0x0400017D RID: 381
		private string m_name;

		// Token: 0x0400017E RID: 382
		private string m_displayName;

		// Token: 0x0400017F RID: 383
		private string m_value;

		// Token: 0x04000180 RID: 384
		private bool m_required;

		// Token: 0x04000181 RID: 385
		private bool m_readOnly;

		// Token: 0x04000182 RID: 386
		private string m_field;

		// Token: 0x04000183 RID: 387
		private string m_error;

		// Token: 0x04000184 RID: 388
		private bool m_encrypted;

		// Token: 0x04000185 RID: 389
		private bool m_isPassword;

		// Token: 0x04000186 RID: 390
		private ArrayList m_validValues = new ArrayList();
	}
}
