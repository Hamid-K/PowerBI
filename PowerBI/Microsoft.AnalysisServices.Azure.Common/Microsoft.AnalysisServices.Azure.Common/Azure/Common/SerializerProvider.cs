using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000B1 RID: 177
	[BlockServiceProvider(typeof(ISerializerProvider))]
	public sealed class SerializerProvider : Block, ISerializerProvider
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x00011080 File Offset: 0x0000F280
		public SerializerProvider()
			: base(typeof(SerializerProvider).Name)
		{
			this.lockObj = new object();
			this.serializers = new Dictionary<Type, object>();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000110B0 File Offset: 0x0000F2B0
		public ISerializer<T> GetSerializer<T>()
		{
			object obj = this.lockObj;
			ISerializer<T> serializer;
			lock (obj)
			{
				if (!this.serializers.ContainsKey(typeof(T)))
				{
					this.serializers[typeof(T)] = new Serializer<T>();
				}
				serializer = (ISerializer<T>)this.serializers[typeof(T)];
			}
			return serializer;
		}

		// Token: 0x0400022B RID: 555
		private readonly Dictionary<Type, object> serializers;

		// Token: 0x0400022C RID: 556
		private readonly object lockObj;
	}
}
