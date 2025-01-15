using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001B5 RID: 437
	public abstract class PropertyDefinition<T> : PropertyDefinition, IPropertyDefinition where T : struct
	{
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00023847 File Offset: 0x00021A47
		public T? Default
		{
			get
			{
				return this.m_default;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0002384F File Offset: 0x00021A4F
		object IPropertyDefinition.Default
		{
			get
			{
				return this.m_default;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0002385C File Offset: 0x00021A5C
		object IPropertyDefinition.Minimum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0002385F File Offset: 0x00021A5F
		object IPropertyDefinition.Maximum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00023862 File Offset: 0x00021A62
		void IPropertyDefinition.Validate(object component, object value)
		{
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00023864 File Offset: 0x00021A64
		protected PropertyDefinition(string name, T? defaultValue)
			: base(name)
		{
			this.m_default = defaultValue;
		}

		// Token: 0x0400053D RID: 1341
		private readonly T? m_default;
	}
}
