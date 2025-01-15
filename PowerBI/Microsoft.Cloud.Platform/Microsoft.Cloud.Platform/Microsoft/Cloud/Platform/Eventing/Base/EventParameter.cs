using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003B9 RID: 953
	public sealed class EventParameter : IObjectDumperUsesToString
	{
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x00070304 File Offset: 0x0006E504
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x0007030C File Offset: 0x0006E50C
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x00070314 File Offset: 0x0006E514
		public Type Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0007031C File Offset: 0x0006E51C
		public EventParameter([NotNull] string name, object value, [NotNull] Type type)
		{
			this.Set(name, value, type);
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0007032E File Offset: 0x0006E52E
		public EventParameter Set([NotNull] string name, object value, [NotNull] Type type)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(type, "type");
			this.m_name = name;
			this.m_value = value;
			this.m_type = type;
			return this;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0007035C File Offset: 0x0006E55C
		public override string ToString()
		{
			return "<EventParameter: Name='{0}' Value='{1}' Type='{2}'>".FormatWithInvariantCulture(new object[] { this.m_name, this.m_value, this.m_type });
		}

		// Token: 0x04000A07 RID: 2567
		private string m_name;

		// Token: 0x04000A08 RID: 2568
		private object m_value;

		// Token: 0x04000A09 RID: 2569
		private Type m_type;
	}
}
