using System;

namespace Microsoft.Owin.Security.DataHandler.Serializer
{
	// Token: 0x0200002E RID: 46
	public interface IDataSerializer<TModel>
	{
		// Token: 0x060000C9 RID: 201
		byte[] Serialize(TModel model);

		// Token: 0x060000CA RID: 202
		TModel Deserialize(byte[] data);
	}
}
