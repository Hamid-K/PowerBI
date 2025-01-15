using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x0200014F RID: 335
	internal class InterceptorsCollection : ConfigurationElementCollection
	{
		// Token: 0x060015A4 RID: 5540 RVA: 0x00038588 File Offset: 0x00036788
		protected override ConfigurationElement CreateNewElement()
		{
			int nextKey = this._nextKey;
			this._nextKey = nextKey + 1;
			return new InterceptorElement(nextKey);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x000385AB File Offset: 0x000367AB
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((InterceptorElement)element).Key;
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x000385BD File Offset: 0x000367BD
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x000385C0 File Offset: 0x000367C0
		protected override string ElementName
		{
			get
			{
				return "interceptor";
			}
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000385C7 File Offset: 0x000367C7
		public void AddElement(InterceptorElement element)
		{
			base.BaseAdd(element);
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x000385D0 File Offset: 0x000367D0
		public virtual IEnumerable<IDbInterceptor> Interceptors
		{
			get
			{
				return (from e in this.OfType<InterceptorElement>()
					select e.CreateInterceptor()).ToList<IDbInterceptor>();
			}
		}

		// Token: 0x040009E7 RID: 2535
		private const string ElementKey = "interceptor";

		// Token: 0x040009E8 RID: 2536
		private int _nextKey;
	}
}
