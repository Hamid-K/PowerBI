using System;
using System.Collections.Generic;
using AngleSharp.Extensions;
using AngleSharp.Services.Styling;

namespace AngleSharp.Services.Default
{
	// Token: 0x02000054 RID: 84
	public class StylingService : IStylingProvider
	{
		// Token: 0x0600019F RID: 415 RVA: 0x0000CBBD File Offset: 0x0000ADBD
		public StylingService()
		{
			this._engines = new List<IStyleEngine>();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		public virtual void Register(IStyleEngine engine)
		{
			this._engines.Add(engine);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000CBDE File Offset: 0x0000ADDE
		public virtual void Unregister(IStyleEngine engine)
		{
			this._engines.Remove(engine);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		public virtual IStyleEngine GetEngine(string mimeType)
		{
			foreach (IStyleEngine styleEngine in this._engines)
			{
				if (styleEngine.Type.Isi(mimeType))
				{
					return styleEngine;
				}
			}
			return null;
		}

		// Token: 0x040001D1 RID: 465
		private readonly List<IStyleEngine> _engines;
	}
}
