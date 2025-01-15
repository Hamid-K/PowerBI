using System;
using System.Collections.Generic;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003DD RID: 989
	internal class FailPointContext : IPropertyContext, IReadablePropertyContext, IWritablePropertyContext
	{
		// Token: 0x06002295 RID: 8853 RVA: 0x0006AB62 File Offset: 0x00068D62
		public FailPointContext(string component)
		{
			this.m_component = component;
			this.m_properties = new Dictionary<string, object>();
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0006AB7C File Offset: 0x00068D7C
		public FailPointContext(string component, string key, object value)
			: this(component)
		{
			this[key] = value;
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x0006AB8D File Offset: 0x00068D8D
		// (set) Token: 0x06002298 RID: 8856 RVA: 0x0006AB95 File Offset: 0x00068D95
		public string Component
		{
			get
			{
				return this.m_component;
			}
			set
			{
				this.m_component = value;
			}
		}

		// Token: 0x170006F3 RID: 1779
		public object this[string name]
		{
			get
			{
				if (name == "component")
				{
					return this.m_component;
				}
				object obj = null;
				lock (this.m_properties)
				{
					this.m_properties.TryGetValue(name, out obj);
				}
				return obj;
			}
			set
			{
				lock (this.m_properties)
				{
					this.m_properties[name] = value;
				}
			}
		}

		// Token: 0x040015BB RID: 5563
		private const string ComponentPropertyName = "component";

		// Token: 0x040015BC RID: 5564
		private string m_component;

		// Token: 0x040015BD RID: 5565
		private Dictionary<string, object> m_properties;
	}
}
