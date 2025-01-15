using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003C0 RID: 960
	public sealed class FieldCollection : List<Field>
	{
		// Token: 0x06001F07 RID: 7943 RVA: 0x0007DEAC File Offset: 0x0007C0AC
		public Field FindDataField(string name)
		{
			for (int i = 0; i < base.Count; i++)
			{
				if (string.Compare(base[i].DataField, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return base[i];
				}
			}
			return null;
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0007DEE8 File Offset: 0x0007C0E8
		public Field Find(string name)
		{
			for (int i = 0; i < base.Count; i++)
			{
				if (string.Compare(base[i].Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return base[i];
				}
			}
			return null;
		}
	}
}
