using System;

namespace Microsoft.ProgramSynthesis.Detection.DataType.DataTypes
{
	// Token: 0x02000B07 RID: 2823
	public class StringType : DataType
	{
		// Token: 0x0600469F RID: 18079 RVA: 0x000DCA4D File Offset: 0x000DAC4D
		private StringType()
			: base(Type.String, typeof(string))
		{
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x060046A0 RID: 18080 RVA: 0x000DCA60 File Offset: 0x000DAC60
		public static StringType Instance { get; } = new StringType();
	}
}
