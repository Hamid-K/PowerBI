using System;
using System.Runtime.CompilerServices;

namespace AngleSharp.Dom
{
	// Token: 0x02000148 RID: 328
	internal sealed class AttachedProperty<TObj, TProp> where TObj : class where TProp : class
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x00040740 File Offset: 0x0003E940
		public TProp Get(TObj item)
		{
			TProp tprop = default(TProp);
			this._properties.TryGetValue(item, out tprop);
			return tprop;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00040765 File Offset: 0x0003E965
		public void Set(TObj item, TProp value)
		{
			this._properties.Add(item, value);
		}

		// Token: 0x04000907 RID: 2311
		private readonly ConditionalWeakTable<TObj, TProp> _properties = new ConditionalWeakTable<TObj, TProp>();
	}
}
