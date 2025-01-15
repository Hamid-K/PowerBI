using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001D4 RID: 468
	public class EdmFunctionParameter : EdmNamedElement, IEdmFunctionParameter, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000B1F RID: 2847 RVA: 0x000207A3 File Offset: 0x0001E9A3
		public EdmFunctionParameter(IEdmFunctionBase declaringFunction, string name, IEdmTypeReference type)
			: this(declaringFunction, name, type, EdmFunctionParameterMode.In)
		{
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000207B0 File Offset: 0x0001E9B0
		public EdmFunctionParameter(IEdmFunctionBase declaringFunction, string name, IEdmTypeReference type, EdmFunctionParameterMode mode)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmFunctionBase>(declaringFunction, "declaringFunction");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.type = type;
			this.mode = mode;
			this.declaringFunction = declaringFunction;
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x000207FE File Offset: 0x0001E9FE
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00020806 File Offset: 0x0001EA06
		public IEdmFunctionBase DeclaringFunction
		{
			get
			{
				return this.declaringFunction;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0002080E File Offset: 0x0001EA0E
		public EdmFunctionParameterMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x04000539 RID: 1337
		private readonly IEdmTypeReference type;

		// Token: 0x0400053A RID: 1338
		private readonly EdmFunctionParameterMode mode;

		// Token: 0x0400053B RID: 1339
		private readonly IEdmFunctionBase declaringFunction;
	}
}
