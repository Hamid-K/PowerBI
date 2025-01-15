using System;

namespace Microsoft.ProgramSynthesis.Detection.DataType.DataTypes
{
	// Token: 0x02000B02 RID: 2818
	public sealed class BoolType : DataType
	{
		// Token: 0x0600468F RID: 18063 RVA: 0x000DC965 File Offset: 0x000DAB65
		private BoolType()
			: base(Type.Bool, typeof(bool))
		{
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06004690 RID: 18064 RVA: 0x000DC978 File Offset: 0x000DAB78
		public static BoolType Instance { get; } = new BoolType();
	}
}
