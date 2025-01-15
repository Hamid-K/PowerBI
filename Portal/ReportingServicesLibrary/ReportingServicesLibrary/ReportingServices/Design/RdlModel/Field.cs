using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003BF RID: 959
	[Serializable]
	public sealed class Field
	{
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x0007DDBC File Offset: 0x0007BFBC
		// (set) Token: 0x06001EFD RID: 7933 RVA: 0x0007DDC4 File Offset: 0x0007BFC4
		public string DataField
		{
			get
			{
				return this.m_dataField;
			}
			set
			{
				this.m_dataField = value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x0007DDCD File Offset: 0x0007BFCD
		// (set) Token: 0x06001EFF RID: 7935 RVA: 0x0007DDD5 File Offset: 0x0007BFD5
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException(SRErrors.InvalidIdentifier(value));
				}
				this.m_name = value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x0007DDF5 File Offset: 0x0007BFF5
		// (set) Token: 0x06001F01 RID: 7937 RVA: 0x0007DDFD File Offset: 0x0007BFFD
		public Expression Value
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

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x0007DE06 File Offset: 0x0007C006
		// (set) Token: 0x06001F03 RID: 7939 RVA: 0x0007DE0E File Offset: 0x0007C00E
		[DesignOnly(true)]
		[DefaultValue("")]
		public string TypeName
		{
			get
			{
				return this.m_typeName;
			}
			set
			{
				this.m_typeName = value;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x0007DE18 File Offset: 0x0007C018
		[XmlIgnore]
		public bool IsNumeric
		{
			get
			{
				if (this.m_typeChecked)
				{
					return this.m_isNumeric;
				}
				this.m_typeChecked = true;
				this.m_isNumeric = true;
				if (this.m_typeName != null)
				{
					this.m_isNumeric = false;
					Type type = Type.GetType(this.m_typeName, false);
					if (type != null && ((type != typeof(bool) && type.IsPrimitive) || type == typeof(decimal)))
					{
						this.m_isNumeric = true;
					}
				}
				return this.m_isNumeric;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0007DEA0 File Offset: 0x0007C0A0
		[XmlIgnore]
		public bool IsCalculated
		{
			get
			{
				return this.m_value != null;
			}
		}

		// Token: 0x04000D77 RID: 3447
		private string m_name;

		// Token: 0x04000D78 RID: 3448
		private string m_dataField;

		// Token: 0x04000D79 RID: 3449
		public string Collation;

		// Token: 0x04000D7A RID: 3450
		private Expression m_value;

		// Token: 0x04000D7B RID: 3451
		private string m_typeName;

		// Token: 0x04000D7C RID: 3452
		private bool m_typeChecked;

		// Token: 0x04000D7D RID: 3453
		private bool m_isNumeric;
	}
}
