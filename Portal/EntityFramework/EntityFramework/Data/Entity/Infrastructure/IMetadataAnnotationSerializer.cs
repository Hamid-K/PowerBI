using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000251 RID: 593
	public interface IMetadataAnnotationSerializer
	{
		// Token: 0x06001EBC RID: 7868
		string Serialize(string name, object value);

		// Token: 0x06001EBD RID: 7869
		object Deserialize(string name, string value);
	}
}
