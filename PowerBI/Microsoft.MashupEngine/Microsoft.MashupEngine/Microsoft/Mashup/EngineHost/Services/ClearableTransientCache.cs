using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x0200199B RID: 6555
	public sealed class ClearableTransientCache : IClearableTransientCache, IDisposable
	{
		// Token: 0x0600A641 RID: 42561 RVA: 0x00226440 File Offset: 0x00224640
		public void Add(Action clearAction)
		{
			List<Action> list = this.actions;
			lock (list)
			{
				this.actions.Add(clearAction);
			}
		}

		// Token: 0x0600A642 RID: 42562 RVA: 0x00226488 File Offset: 0x00224688
		public void Clear()
		{
			Action[] array = null;
			List<Action> list = this.actions;
			lock (list)
			{
				if (this.actions.Count > 0)
				{
					array = this.actions.ToArray();
					this.actions.Clear();
				}
			}
			if (array != null)
			{
				Action[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i]();
				}
			}
		}

		// Token: 0x0600A643 RID: 42563 RVA: 0x0022650C File Offset: 0x0022470C
		public void Dispose()
		{
			List<Action> list = this.actions;
			lock (list)
			{
				this.actions.Clear();
			}
		}

		// Token: 0x04005686 RID: 22150
		private readonly List<Action> actions = new List<Action>();
	}
}
