using System;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001038 RID: 4152
	internal sealed class CompositeDisposable : IDisposable
	{
		// Token: 0x06006C51 RID: 27729 RVA: 0x00175558 File Offset: 0x00173758
		public CompositeDisposable(params IDisposable[] disposables)
		{
			this.disposables = disposables;
		}

		// Token: 0x06006C52 RID: 27730 RVA: 0x00175568 File Offset: 0x00173768
		public void Dispose()
		{
			if (this.disposables != null)
			{
				IDisposable[] array = this.disposables;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Dispose();
				}
				this.disposables = null;
			}
		}

		// Token: 0x04003C4A RID: 15434
		private IDisposable[] disposables;
	}
}
