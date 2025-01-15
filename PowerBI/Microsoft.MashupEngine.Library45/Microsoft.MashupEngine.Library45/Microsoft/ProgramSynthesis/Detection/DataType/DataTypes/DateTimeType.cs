using System;

namespace Microsoft.ProgramSynthesis.Detection.DataType.DataTypes
{
	// Token: 0x02000B04 RID: 2820
	public class DateTimeType : DataType
	{
		// Token: 0x06004696 RID: 18070 RVA: 0x000DC9DB File Offset: 0x000DABDB
		private DateTimeType()
			: base(Type.DateTime, typeof(DateTime))
		{
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06004697 RID: 18071 RVA: 0x000DC9EE File Offset: 0x000DABEE
		public static DateTimeType Instance { get; } = new DateTimeType();
	}
}
