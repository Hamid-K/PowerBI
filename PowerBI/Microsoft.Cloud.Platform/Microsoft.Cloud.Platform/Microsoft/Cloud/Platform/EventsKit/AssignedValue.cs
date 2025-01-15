using System;
using System.Globalization;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200036E RID: 878
	public class AssignedValue
	{
		// Token: 0x06001A16 RID: 6678 RVA: 0x000606BB File Offset: 0x0005E8BB
		public AssignedValue(string variable)
			: this(variable, "")
		{
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x000606C9 File Offset: 0x0005E8C9
		public AssignedValue(string variable, string property)
		{
			this.m_valueVariable = variable;
			this.m_valueProperty = property;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000606DF File Offset: 0x0005E8DF
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.m_valueProperty))
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { this.m_valueVariable, this.m_valueProperty });
			}
			return this.m_valueVariable;
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0006071C File Offset: 0x0005E91C
		public string Property
		{
			get
			{
				return this.m_valueProperty;
			}
		}

		// Token: 0x04000902 RID: 2306
		private readonly string m_valueVariable;

		// Token: 0x04000903 RID: 2307
		private readonly string m_valueProperty;
	}
}
