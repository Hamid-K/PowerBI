using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000257 RID: 599
	public static class InnerExceptionCreator
	{
		// Token: 0x06000FC9 RID: 4041 RVA: 0x00035D34 File Offset: 0x00033F34
		public static Exception GetInnerException(Exception inner)
		{
			if (inner != null && !(inner is SubstituteNonSerializableException) && inner.GetType().GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, InnerExceptionCreator.s_serializedCtorSignature, null) == null)
			{
				IDictionary data = inner.Data;
				inner = new SubstituteNonSerializableException(inner.GetType().Name, inner.Message, inner.ToString(), inner.StackTrace);
				if (data != null && inner.Data != null)
				{
					foreach (object obj in data)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						inner.Data.Add(dictionaryEntry.Key, dictionaryEntry.Value);
					}
				}
			}
			return inner;
		}

		// Token: 0x040005E6 RID: 1510
		private static readonly Type[] s_serializedCtorSignature = new Type[]
		{
			typeof(SerializationInfo),
			typeof(StreamingContext)
		};
	}
}
