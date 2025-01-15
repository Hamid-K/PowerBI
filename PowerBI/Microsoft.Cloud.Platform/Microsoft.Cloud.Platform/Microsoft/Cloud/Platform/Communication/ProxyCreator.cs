using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004E9 RID: 1257
	public class ProxyCreator
	{
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x00087783 File Offset: 0x00085983
		// (set) Token: 0x06002632 RID: 9778 RVA: 0x0008778B File Offset: 0x0008598B
		public Assembly Assembly { get; set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x00087794 File Offset: 0x00085994
		// (set) Token: 0x06002634 RID: 9780 RVA: 0x0008779C File Offset: 0x0008599C
		public string ProxyNamespace { get; private set; }

		// Token: 0x06002635 RID: 9781 RVA: 0x000877A8 File Offset: 0x000859A8
		public ProxyCreator(Assembly assembly, string proxyNamespace, string proxyGeneratedName, bool definedClassName = false)
		{
			this.Assembly = assembly;
			this.m_proxyGeneratedName = proxyGeneratedName;
			this.m_proxyNamespace = proxyNamespace;
			this.m_definedClassName = definedClassName;
			if (this.m_definedClassName)
			{
				this.ProxyNamespace = this.m_proxyNamespace;
				return;
			}
			this.ProxyNamespace = null;
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x000877F4 File Offset: 0x000859F4
		public T Create<T>(object proxyInvoker) where T : class
		{
			T t = default(T);
			if (this.m_definedClassName)
			{
				t = (T)((object)this.Assembly.CreateInstance(this.m_proxyGeneratedName, false, BindingFlags.Instance | BindingFlags.Public, null, new object[] { proxyInvoker }, CultureInfo.CurrentCulture, null));
			}
			else
			{
				t = (T)((object)this.Assembly.CreateInstance("{0}.{1}.{2}{3}".FormatWithCurrentCulture(new object[] { this.m_proxyNamespace, "ECFProxies", this.m_proxyGeneratedName, "Proxy" }), false, BindingFlags.Instance | BindingFlags.Public, null, new object[] { proxyInvoker }, CultureInfo.CurrentCulture, null));
			}
			ExtendedDiagnostics.EnsureNotNull<T>(t, "Proxy was null");
			return t;
		}

		// Token: 0x04000D9A RID: 3482
		private string m_proxyGeneratedName;

		// Token: 0x04000D9B RID: 3483
		private string m_proxyNamespace;

		// Token: 0x04000D9C RID: 3484
		private bool m_definedClassName;
	}
}
