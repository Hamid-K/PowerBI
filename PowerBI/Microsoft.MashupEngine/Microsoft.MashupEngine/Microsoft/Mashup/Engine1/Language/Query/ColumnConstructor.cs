using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001802 RID: 6146
	public class ColumnConstructor
	{
		// Token: 0x06009B68 RID: 39784 RVA: 0x002019A6 File Offset: 0x001FFBA6
		public ColumnConstructor(string name, FunctionValue function)
			: this(name, function, null)
		{
		}

		// Token: 0x06009B69 RID: 39785 RVA: 0x002019B1 File Offset: 0x001FFBB1
		public ColumnConstructor(string name, FunctionValue function, IValueReference type)
		{
			this.name = name;
			this.function = function;
			this.type = type ?? this.function.Type.AsFunctionType.ReturnType;
		}

		// Token: 0x170027FB RID: 10235
		// (get) Token: 0x06009B6A RID: 39786 RVA: 0x002019E7 File Offset: 0x001FFBE7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170027FC RID: 10236
		// (get) Token: 0x06009B6B RID: 39787 RVA: 0x002019EF File Offset: 0x001FFBEF
		public FunctionValue Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x170027FD RID: 10237
		// (get) Token: 0x06009B6C RID: 39788 RVA: 0x002019F7 File Offset: 0x001FFBF7
		public IValueReference Type
		{
			get
			{
				return this.type ?? Value.Null;
			}
		}

		// Token: 0x0400521B RID: 21019
		private string name;

		// Token: 0x0400521C RID: 21020
		private FunctionValue function;

		// Token: 0x0400521D RID: 21021
		private IValueReference type;
	}
}
