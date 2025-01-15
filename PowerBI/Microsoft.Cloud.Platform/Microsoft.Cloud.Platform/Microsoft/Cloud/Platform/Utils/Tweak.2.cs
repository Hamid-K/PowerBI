using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002D7 RID: 727
	public class Tweak<T> : Tweak
	{
		// Token: 0x06001374 RID: 4980 RVA: 0x00043816 File Offset: 0x00041A16
		internal Tweak(string name, string description, Action<Tweak> notifyChangesTo)
			: base(name, description, notifyChangesTo)
		{
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00043824 File Offset: 0x00041A24
		public override string ToString()
		{
			if (this.m_value == null)
			{
				return string.Concat(new string[]
				{
					"Tweak<",
					typeof(T).ToString(),
					">(",
					base.Name,
					"=null)"
				});
			}
			return string.Concat(new object[]
			{
				"Tweak<",
				typeof(T).ToString(),
				">(",
				base.Name,
				"=",
				this.Value,
				")"
			});
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x000438D1 File Offset: 0x00041AD1
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x000438D9 File Offset: 0x00041AD9
		public T Value
		{
			get
			{
				return this.m_value;
			}
			internal set
			{
				this.m_value = value;
			}
		}

		// Token: 0x04000750 RID: 1872
		private T m_value;
	}
}
