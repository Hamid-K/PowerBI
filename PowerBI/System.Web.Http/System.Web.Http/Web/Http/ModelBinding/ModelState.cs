using System;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public class ModelState
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00007EBE File Offset: 0x000060BE
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00007EC6 File Offset: 0x000060C6
		public ValueProviderResult Value { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00007ECF File Offset: 0x000060CF
		public ModelErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x0400009D RID: 157
		private ModelErrorCollection _errors = new ModelErrorCollection();
	}
}
