using System;
using System.Collections.Generic;

namespace System.Web.Script.Serialization
{
	// Token: 0x02000005 RID: 5
	public abstract class JavaScriptConverter<T> : JavaScriptConverter
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020AE File Offset: 0x000002AE
		public bool CanConvert(Type objectType)
		{
			return objectType == typeof(T) || typeof(T).IsAssignableFrom(objectType);
		}

		// Token: 0x06000005 RID: 5
		public abstract T Deserialize(IDictionary<string, object> dictionary);

		// Token: 0x06000006 RID: 6
		public abstract IDictionary<string, object> Serialize(T value);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020D4 File Offset: 0x000002D4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[] { typeof(T) };
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E9 File Offset: 0x000002E9
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			return this.Deserialize(dictionary);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020F7 File Offset: 0x000002F7
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			return this.Serialize((T)((object)obj));
		}
	}
}
