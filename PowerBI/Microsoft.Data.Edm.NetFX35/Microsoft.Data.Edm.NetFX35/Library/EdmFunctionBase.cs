using System;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001D1 RID: 465
	public abstract class EdmFunctionBase : EdmNamedElement, IEdmFunctionBase, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x000205C0 File Offset: 0x0001E7C0
		protected EdmFunctionBase(string name, IEdmTypeReference returnType)
			: base(name)
		{
			this.returnType = returnType;
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x000205DB File Offset: 0x0001E7DB
		public IEdmTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x000205E3 File Offset: 0x0001E7E3
		public IEnumerable<IEdmFunctionParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000205EC File Offset: 0x0001E7EC
		public IEdmFunctionParameter FindParameter(string name)
		{
			foreach (IEdmFunctionParameter edmFunctionParameter in this.parameters)
			{
				if (edmFunctionParameter.Name == name)
				{
					return edmFunctionParameter;
				}
			}
			return null;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00020650 File Offset: 0x0001E850
		public EdmFunctionParameter AddParameter(string name, IEdmTypeReference type)
		{
			EdmFunctionParameter edmFunctionParameter = new EdmFunctionParameter(this, name, type);
			this.parameters.Add(edmFunctionParameter);
			return edmFunctionParameter;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00020674 File Offset: 0x0001E874
		public EdmFunctionParameter AddParameter(string name, IEdmTypeReference type, EdmFunctionParameterMode mode)
		{
			EdmFunctionParameter edmFunctionParameter = new EdmFunctionParameter(this, name, type, mode);
			this.parameters.Add(edmFunctionParameter);
			return edmFunctionParameter;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00020698 File Offset: 0x0001E898
		public void AddParameter(IEdmFunctionParameter parameter)
		{
			EdmUtil.CheckArgumentNull<IEdmFunctionParameter>(parameter, "parameter");
			this.parameters.Add(parameter);
		}

		// Token: 0x04000530 RID: 1328
		private readonly List<IEdmFunctionParameter> parameters = new List<IEdmFunctionParameter>();

		// Token: 0x04000531 RID: 1329
		private IEdmTypeReference returnType;
	}
}
