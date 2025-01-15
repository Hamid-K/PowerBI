using System;

namespace Microsoft.ProgramSynthesis.Detection.DataType.DataTypes
{
	// Token: 0x02000B05 RID: 2821
	public class DoubleType : DataType
	{
		// Token: 0x06004699 RID: 18073 RVA: 0x000DCA01 File Offset: 0x000DAC01
		private DoubleType()
			: base(Type.Double, typeof(double))
		{
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x0600469A RID: 18074 RVA: 0x000DCA14 File Offset: 0x000DAC14
		public static DoubleType Instance { get; } = new DoubleType();
	}
}
