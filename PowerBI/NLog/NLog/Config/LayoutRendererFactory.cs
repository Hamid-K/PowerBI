using System;
using System.Collections.Generic;
using NLog.LayoutRenderers;

namespace NLog.Config
{
	// Token: 0x02000184 RID: 388
	internal class LayoutRendererFactory : Factory<LayoutRenderer, LayoutRendererAttribute>
	{
		// Token: 0x060011B9 RID: 4537 RVA: 0x0002E343 File Offset: 0x0002C543
		public LayoutRendererFactory(ConfigurationItemFactory parentFactory)
			: base(parentFactory)
		{
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0002E34C File Offset: 0x0002C54C
		public void ClearFuncLayouts()
		{
			this._funcRenderers = null;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0002E355 File Offset: 0x0002C555
		public void RegisterFuncLayout(string name, FuncLayoutRenderer renderer)
		{
			this._funcRenderers = this._funcRenderers ?? new Dictionary<string, FuncLayoutRenderer>(StringComparer.OrdinalIgnoreCase);
			this._funcRenderers[name] = renderer;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0002E380 File Offset: 0x0002C580
		public override bool TryCreateInstance(string itemName, out LayoutRenderer result)
		{
			FuncLayoutRenderer funcLayoutRenderer;
			if (this._funcRenderers != null && this._funcRenderers.TryGetValue(itemName, out funcLayoutRenderer))
			{
				result = funcLayoutRenderer;
				return true;
			}
			return base.TryCreateInstance(itemName, out result);
		}

		// Token: 0x040004D8 RID: 1240
		private Dictionary<string, FuncLayoutRenderer> _funcRenderers;
	}
}
