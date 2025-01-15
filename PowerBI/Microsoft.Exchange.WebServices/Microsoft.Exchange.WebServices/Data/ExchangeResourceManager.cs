using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002E1 RID: 737
	internal sealed class ExchangeResourceManager : ResourceManager
	{
		// Token: 0x060019FB RID: 6651 RVA: 0x000469C4 File Offset: 0x000459C4
		public static ExchangeResourceManager GetResourceManager(string baseName, Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			string text = baseName + assembly.GetName().Name;
			ExchangeResourceManager exchangeResourceManager2;
			lock (ExchangeResourceManager.lockObject)
			{
				ExchangeResourceManager exchangeResourceManager = (ExchangeResourceManager)ExchangeResourceManager.resourceManagers[text];
				if (exchangeResourceManager == null)
				{
					exchangeResourceManager = new ExchangeResourceManager(baseName, assembly);
					ExchangeResourceManager.resourceManagers[text] = exchangeResourceManager;
				}
				exchangeResourceManager2 = exchangeResourceManager;
			}
			return exchangeResourceManager2;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00046A44 File Offset: 0x00045A44
		private ExchangeResourceManager(string baseName, Assembly assembly)
			: base(baseName, assembly)
		{
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x00046A4E File Offset: 0x00045A4E
		public override string BaseName
		{
			get
			{
				return base.BaseName;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x00046A56 File Offset: 0x00045A56
		public string AssemblyName
		{
			get
			{
				return this.MainAssembly.GetName().Name;
			}
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00046A68 File Offset: 0x00045A68
		public override string GetString(string name)
		{
			return this.GetString(name, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00046A76 File Offset: 0x00045A76
		public override string GetString(string name, CultureInfo culture)
		{
			return base.GetString(name, culture);
		}

		// Token: 0x0400140D RID: 5133
		private static HybridDictionary resourceManagers = new HybridDictionary();

		// Token: 0x0400140E RID: 5134
		private static object lockObject = new object();
	}
}
