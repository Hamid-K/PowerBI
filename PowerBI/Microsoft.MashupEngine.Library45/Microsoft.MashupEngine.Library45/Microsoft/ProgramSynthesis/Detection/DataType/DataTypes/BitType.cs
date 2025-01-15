using System;

namespace Microsoft.ProgramSynthesis.Detection.DataType.DataTypes
{
	// Token: 0x02000B01 RID: 2817
	public sealed class BitType : DataType
	{
		// Token: 0x0600468C RID: 18060 RVA: 0x000DC93F File Offset: 0x000DAB3F
		private BitType()
			: base(Type.Bit, typeof(bool))
		{
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x0600468D RID: 18061 RVA: 0x000DC952 File Offset: 0x000DAB52
		public static BitType Instance { get; } = new BitType();
	}
}
