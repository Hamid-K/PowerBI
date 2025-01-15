using System;
using System.Web.Http.Validation;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000070 RID: 112
	public sealed class ComplexModelDtoResult
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000882B File Offset: 0x00006A2B
		public ComplexModelDtoResult(object model, ModelValidationNode validationNode)
		{
			if (validationNode == null)
			{
				throw Error.ArgumentNull("validationNode");
			}
			this.Model = model;
			this.ValidationNode = validationNode;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000884F File Offset: 0x00006A4F
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00008857 File Offset: 0x00006A57
		public object Model { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00008860 File Offset: 0x00006A60
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00008868 File Offset: 0x00006A68
		public ModelValidationNode ValidationNode { get; private set; }
	}
}
