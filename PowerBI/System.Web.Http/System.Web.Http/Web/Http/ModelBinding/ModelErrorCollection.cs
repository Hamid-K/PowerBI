using System;
using System.Collections.ObjectModel;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public class ModelErrorCollection : Collection<ModelError>
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x00007E9A File Offset: 0x0000609A
		public void Add(Exception exception)
		{
			base.Add(new ModelError(exception));
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00007EA8 File Offset: 0x000060A8
		public void Add(string errorMessage)
		{
			base.Add(new ModelError(errorMessage));
		}
	}
}
