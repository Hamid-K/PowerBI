using System;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200213E RID: 8510
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	internal sealed class EnumStringAttribute : Attribute
	{
		// Token: 0x0600D351 RID: 54097 RVA: 0x0029F28C File Offset: 0x0029D48C
		public EnumStringAttribute(string value)
		{
			this.Value = value;
		}

		// Token: 0x17003308 RID: 13064
		// (get) Token: 0x0600D352 RID: 54098 RVA: 0x0029F29B File Offset: 0x0029D49B
		// (set) Token: 0x0600D353 RID: 54099 RVA: 0x0029F2A3 File Offset: 0x0029D4A3
		public string Value { get; private set; }
	}
}
