using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200051F RID: 1311
	public interface IEntitySerializer
	{
		// Token: 0x06002889 RID: 10377
		void SerializeToStream(Stream target, object o, Type type, SerializationOptions options, IEnumerable<Type> knownTypes);

		// Token: 0x0600288A RID: 10378
		object Deserialize(Stream stream, Type type, SerializationOptions options, IEnumerable<Type> knownTypes);
	}
}
