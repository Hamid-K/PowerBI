using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Design
{
	// Token: 0x0200038B RID: 907
	public class EnumPropertyDef : PropertyDef
	{
		// Token: 0x06001E02 RID: 7682 RVA: 0x0007AE12 File Offset: 0x00079012
		public EnumPropertyDef(string name, object defaultValue, IList values, IList<string> displayStrings)
			: base(name)
		{
			this.m_default = defaultValue;
			this.m_values = values;
			this.m_displayStrings = displayStrings;
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x0007AE31 File Offset: 0x00079031
		public EnumPropertyDef(string name, object defaultValue, IList values)
			: base(name)
		{
			this.m_default = defaultValue;
			this.m_values = values;
			this.m_displayStrings = null;
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0007AE4F File Offset: 0x0007904F
		public object Default
		{
			get
			{
				return this.m_default;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x0007AE57 File Offset: 0x00079057
		public IList Values
		{
			get
			{
				return this.m_values;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x0007AE5F File Offset: 0x0007905F
		public IList<string> DisplayStrings
		{
			get
			{
				return this.m_displayStrings;
			}
		}

		// Token: 0x04000CB8 RID: 3256
		private object m_default;

		// Token: 0x04000CB9 RID: 3257
		private IList m_values;

		// Token: 0x04000CBA RID: 3258
		private IList<string> m_displayStrings;
	}
}
