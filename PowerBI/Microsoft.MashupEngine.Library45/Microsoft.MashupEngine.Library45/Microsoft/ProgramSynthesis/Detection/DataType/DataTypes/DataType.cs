using System;

namespace Microsoft.ProgramSynthesis.Detection.DataType.DataTypes
{
	// Token: 0x02000B03 RID: 2819
	public abstract class DataType
	{
		// Token: 0x06004692 RID: 18066 RVA: 0x000DC98B File Offset: 0x000DAB8B
		protected DataType(Type type, Type sysType)
		{
			this.Type = type;
			this.SystemType = sysType;
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06004693 RID: 18067 RVA: 0x000DC9A1 File Offset: 0x000DABA1
		public Type Type { get; }

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06004694 RID: 18068 RVA: 0x000DC9A9 File Offset: 0x000DABA9
		public Type SystemType { get; }

		// Token: 0x06004695 RID: 18069 RVA: 0x000DC9B4 File Offset: 0x000DABB4
		public override int GetHashCode()
		{
			return 241 * this.Type.GetHashCode();
		}
	}
}
