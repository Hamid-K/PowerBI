using System;

namespace Microsoft.ProgramSynthesis.Detection.DataType.DataTypes
{
	// Token: 0x02000B06 RID: 2822
	public class IntegerType : DataType
	{
		// Token: 0x0600469C RID: 18076 RVA: 0x000DCA27 File Offset: 0x000DAC27
		private IntegerType()
			: base(Type.Integer, typeof(int))
		{
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x0600469D RID: 18077 RVA: 0x000DCA3A File Offset: 0x000DAC3A
		public static IntegerType Instance { get; } = new IntegerType();
	}
}
