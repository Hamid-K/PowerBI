using System;
using System.IO;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000143 RID: 323
	internal abstract class Serializer
	{
		// Token: 0x06001975 RID: 6517
		public abstract object Deserialize(Stream s);

		// Token: 0x06001976 RID: 6518
		public abstract void Serialize(Stream s, object o);

		// Token: 0x06001977 RID: 6519 RVA: 0x0006B394 File Offset: 0x00069594
		protected Serializer(Type t)
		{
			this._type = t;
		}

		// Token: 0x040009CE RID: 2510
		protected Type _type;
	}
}
