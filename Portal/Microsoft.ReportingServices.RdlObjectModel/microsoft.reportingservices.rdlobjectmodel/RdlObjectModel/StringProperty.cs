using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001BA RID: 442
	public class StringProperty : PropertyDefinition, IPropertyDefinition
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00023AFA File Offset: 0x00021CFA
		public object Default
		{
			get
			{
				return this.m_default;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00023B02 File Offset: 0x00021D02
		object IPropertyDefinition.Minimum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00023B05 File Offset: 0x00021D05
		object IPropertyDefinition.Maximum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00023B08 File Offset: 0x00021D08
		void IPropertyDefinition.Validate(object component, object value)
		{
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00023B0A File Offset: 0x00021D0A
		public StringProperty(string name, string defaultValue)
			: base(name)
		{
			this.m_default = defaultValue;
		}

		// Token: 0x04000540 RID: 1344
		private readonly string m_default;
	}
}
