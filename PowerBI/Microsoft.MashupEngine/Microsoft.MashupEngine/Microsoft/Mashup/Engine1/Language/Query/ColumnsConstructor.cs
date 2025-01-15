using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001803 RID: 6147
	public class ColumnsConstructor
	{
		// Token: 0x06009B6D RID: 39789 RVA: 0x00201A08 File Offset: 0x001FFC08
		public ColumnsConstructor(Keys names, FunctionValue function, IValueReference[] types)
		{
			this.names = names;
			this.function = function;
			this.types = types;
		}

		// Token: 0x170027FE RID: 10238
		// (get) Token: 0x06009B6E RID: 39790 RVA: 0x00201A25 File Offset: 0x001FFC25
		public Keys Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x170027FF RID: 10239
		// (get) Token: 0x06009B6F RID: 39791 RVA: 0x00201A2D File Offset: 0x001FFC2D
		public FunctionValue Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17002800 RID: 10240
		// (get) Token: 0x06009B70 RID: 39792 RVA: 0x00201A35 File Offset: 0x001FFC35
		public IValueReference[] Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x17002801 RID: 10241
		// (get) Token: 0x06009B71 RID: 39793 RVA: 0x00201A3D File Offset: 0x001FFC3D
		public int Length
		{
			get
			{
				return this.names.Length;
			}
		}

		// Token: 0x0400521E RID: 21022
		private Keys names;

		// Token: 0x0400521F RID: 21023
		private FunctionValue function;

		// Token: 0x04005220 RID: 21024
		private IValueReference[] types;
	}
}
